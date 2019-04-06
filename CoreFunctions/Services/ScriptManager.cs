using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreFunctions.Models;

namespace CoreFunctions.Services
{
	public class FunctionManager
	{
		private List<FunctionModel> _functions;

		public FunctionManager()
		{
			Functions = Functions ?? new List<FunctionModel>();
		}

		public List<FunctionModel> Functions
		{
			get => _functions;
			set => _functions = value;
		}

		public void AddFunction(FunctionModel f)
		{
			Functions.Add(f);
		}
	}
}
