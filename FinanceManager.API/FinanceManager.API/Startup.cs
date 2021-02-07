using Azure.Identity;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;

[assembly: FunctionsStartup(typeof(FinanceManager.API.Startup))]

namespace FinanceManager.API
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("FinManagerConfigConnectionString");

            builder.ConfigurationBuilder.AddAzureAppConfiguration(options =>
            {
                options.Connect(connectionString)
                    .ConfigureKeyVault(kv =>
                    {
                        kv.SetCredential(new DefaultAzureCredential());
                    });
            });
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
