using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json.Linq;
using Xunit;

namespace AzureFunctionExample.Services.Calc.Tests
{
    public partial class CalcServiceTest
    {
        public CalcServiceTest()
        {

        }

        private CalcService GetCalcService()
        {
            return new CalcService();
        }

        //[Fact]
        //public void When_NoPayload_Upsert_Throws_Exception()
        //{
        //    var calcService = GetCalcService();
        //    var ex = Assert.ThrowsAsync<ArgumentNullException>(async () => await calcService.Add(null, null));
        //}

        [Fact]
        public async void When_Payload_Add_And_Return_Sum()
        {
            var calcService = GetCalcService();
            var result = await calcService.Add(1, 2);
            Assert.Equal(3, result);
        }
    }
}
