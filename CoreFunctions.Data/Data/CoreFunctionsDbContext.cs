using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CoreFunctions.Data.Data
{
	public class CoreFunctionsDbContext : DbContext
	{
		public CoreFunctionsDbContext(DbContextOptions<CoreFunctionsDbContext> options)
			: base(options)
		{
		}

		public DbSet<FunctionModel> Functions { get; set; }
	}

	public class FunctionModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ShouldExecuteDelegate { get; set; }
		public string Script { get; set; }
		public int Order { get; set; }
		public bool IsActive { get; set; }
		public DateTime Created { get; set; }
		public string Imports { get; set; }
		public string References { get; set; }
	}
}
