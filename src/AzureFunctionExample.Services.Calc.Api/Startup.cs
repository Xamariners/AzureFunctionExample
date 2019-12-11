using System;
using System.Collections.Generic;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using AzureFunctionExample.Services.Calc.Interfaces;

[assembly: FunctionsStartup(typeof(AzureFunctionExample.Services.Calc.Api.Startup))]

namespace AzureFunctionExample.Services.Calc.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            ConfigureServices(builder.Services)
                  .BuildServiceProvider(true);
        }

        private IServiceCollection ConfigureServices(IServiceCollection services)
        {
            // setup svc here
            services.AddScoped<ICalcService, CalcService>();
            return services;
        }

    }
}
