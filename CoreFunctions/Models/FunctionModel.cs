using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CoreFunctions.Models
{
	public class FunctionModel
	{
		public string Name { get; set; }
		public Func<HttpContext, Func<Task>, Task> Func { get; set; }
		//TODO: Add delegate to control execution

	}
}
