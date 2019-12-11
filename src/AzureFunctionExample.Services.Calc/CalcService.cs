using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureFunctionExample.Services.Calc.Interfaces;

namespace AzureFunctionExample.Services.Calc
{
    public class CalcService : ICalcService
    {
        public async Task<int> Add(int a, int b)
        {
            var result = a + b;
            return await Task.FromResult<int>(result);
        }

        public async Task<int> Multiply(int a, int b)
        {   
            var result = a * b;
            return await Task.FromResult<int>(result);
        }
    }
}
