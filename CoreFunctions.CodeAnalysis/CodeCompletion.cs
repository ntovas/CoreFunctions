using System.Collections.Generic;
using System.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.CodeAnalysis.Text;

namespace CoreFunctions.CodeAnalysis
{
	public static class CodeCompletion
	{
		public static async Task<List<string>> Run(string script, int position)
		{
			var assemblies = new[]
			{
				Assembly.Load("Microsoft.CodeAnalysis"),
				Assembly.Load("Microsoft.CodeAnalysis.CSharp"),
				Assembly.Load("Microsoft.CodeAnalysis.Features"),
				Assembly.Load("Microsoft.CodeAnalysis.CSharp.Features"),
			};

			var parts = MefHostServices.DefaultAssemblies.Concat(assemblies)
				.Distinct()
				.SelectMany(x => x.GetTypes())
				.ToArray();

			var ctx = new ContainerConfiguration()
				.WithParts(parts)
				.CreateContainer();
			var host = MefHostServices.Create(ctx);
			var workspace = new AdhocWorkspace(host);

			var compilationOptions = new CSharpCompilationOptions(
				OutputKind.DynamicallyLinkedLibrary, usings: new[] { "System",
					"CoreFunctions", "Microsoft.AspNetCore.Http",
					"System.Net" });
			var scriptProjectInfo = ProjectInfo.Create(ProjectId.CreateNewId(),
					VersionStamp.Create(), "Script", "Script",
					LanguageNames.CSharp, isSubmission: true)
				.WithMetadataReferences(new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) })
				.WithCompilationOptions(compilationOptions);

			var scriptProject = workspace.AddProject(scriptProjectInfo);
			var scriptDocumentInfo = DocumentInfo.Create(
				DocumentId.CreateNewId(scriptProject.Id), "Script",
				sourceCodeKind: SourceCodeKind.Script,
				loader: TextLoader.From(
					TextAndVersion.Create(SourceText.From(script),
						VersionStamp.Create())));
			var scriptDocument = workspace.AddDocument(scriptDocumentInfo);
			var compiler = CompletionService.GetService(scriptDocument);
			return (await compiler.GetCompletionsAsync(scriptDocument, position))
				.Items
				.Select(c=> c.DisplayText).ToList();
		}

	}
}