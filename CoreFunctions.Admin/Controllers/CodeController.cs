using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreFunctions.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreFunctions.Admin.Controllers
{
    public class CodeController : Controller
	{
	    public async Task<IActionResult> GetCompletion(string code)
	    {
		    var data = await CodeCompletion.Run(code, code.Length -1);

			return Ok(data);
	    }
    }
}