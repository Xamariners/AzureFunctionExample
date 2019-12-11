using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AzureFunctionExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using AzureFunctionExample.Services.Calc.Api.Extensions;
using AzureFunctionExample.Services.Calc.Interfaces;

namespace AzureFunctionExample.Services.Calc.Api
{
    public class AddFunction
    {
        private readonly ICalcService _calcService;

        public AddFunction(ICalcService calcService)
        {
            _calcService = calcService;
        }

        [FunctionName(nameof(AddFunction))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "calc/add")] HttpRequestMessage req, ILogger log)
        {
       
            try
            {
                var calreq = await req.ParseContentAndThrow<CalcRequest>();

                var result = await _calcService.Add(calreq.A, calreq.B);
                return new OkObjectResult(result);
               
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
