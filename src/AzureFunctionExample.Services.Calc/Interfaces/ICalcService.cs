using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureFunctionExample.Services.Calc.Interfaces
{
    public interface ICalcService
    {
        Task<int> Add(int a, int b);
        Task<int> Multiply(int a, int b);
    }
}
