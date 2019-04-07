using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreFunctions.Models;
using CoreFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreFunctions.Controllers
{

	public class HomeController : Controller
	{
		private IHostingEnvironment _env;

		public HomeController(IHostingEnvironment env)
		{
			_env = env;
		}
		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> EnableScript(string name)
		{
			var options = ScriptOptions.Default.AddReferences(
					GetType().GetTypeInfo().Assembly,
					typeof(object).GetTypeInfo().Assembly,
					typeof(HttpResponseWritingExtensions).GetTypeInfo().Assembly).
				AddImports("CoreFunctions", "Microsoft.AspNetCore.Http",
					"System.Net", "System");

			var script = await System.IO.File
				.ReadAllTextAsync($"{_env.ContentRootPath }/Scripts/{name}.csx");
			var scriptCompiled =
				CSharpScript.Create<Func<HttpContext, Func<Task>, Task>>(script, options);
			var diag = scriptCompiled.Compile();
			if (diag.Any())
			{
				return BadRequest();
			}
			var final = (await scriptCompiled.RunAsync()).ReturnValue;
			var mgr = HttpContext.RequestServices.GetRequiredService<FunctionManager>();
			mgr.AddFunction(new FunctionModel{Name = name, Func = final});
			return Ok();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
