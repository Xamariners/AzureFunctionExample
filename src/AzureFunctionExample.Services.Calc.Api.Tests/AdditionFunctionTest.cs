using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using AzureFunctionExample.Models;
using AzureFunctionExample.Services.Calc.Api.Tests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using AzureFunctionExample.Services.Calc.Interfaces;
using Xunit;

namespace AzureFunctionExample.Services.Calc.Api.Tests
{
    public class InvoiceByCustomerFunctionTest
    {
        private Mock<ICalcService> _mockCalcService;
        private Guid _userId = Guid.NewGuid();

        public InvoiceByCustomerFunctionTest()
        {
            _mockCalcService = new Mock<ICalcService>();
        }

        private AddFunction GetAddFunction()
        {
            // when many tests and a complicated setup, useful to have common setup in one place
            return new AddFunction(_mockCalcService.Object);
        }

        [Fact]
        public async Task When_Add_Numbers_Return_Correct_Sum()
        {
            var numA = 1;
            var numB = 2;
            var expectedResult = 3;
            var request = JsonConvert.SerializeObject(new CalcRequest {A = numA, B = numB});

            // not needed here, but just to show how to mock
            _mockCalcService.Setup(i => i.Add(numA, numB)).ReturnsAsync(expectedResult);

            var mockRequest = MockHelpers.CreateRequestMessage(request);
            var logger = new Mock<ILogger<AddFunction>>();

            var resultObject =
                await new AddFunction(_mockCalcService.Object).Run(mockRequest, logger.Object) as OkObjectResult;
            var result = resultObject?.Value as int?;
            Assert.Equal(result, expectedResult);
        }
    }
}
