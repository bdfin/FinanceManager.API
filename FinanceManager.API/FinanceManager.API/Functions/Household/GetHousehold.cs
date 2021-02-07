using FinanceManager.Common;
using FinanceManager.Models.Data;
using FinanceManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FinanceManager.API.Functions.Household
{
    public class GetHousehold
    {
        private readonly IConfiguration config;

        public GetHousehold(IConfiguration config)
        {
            this.config = config;
        }

        [FunctionName("GetHousehold")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "household/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return new BadRequestObjectResult("Id cannot be null");

            try
            {
                var credentials = new CosmosCredentials(config[ConfigSettings.CosmosDataUri],
                    config[ConfigSettings.CosmosDataKey],
                    config[ConfigSettings.FinanceManagerDatabase],
                    CollectionConstants.Households, id);

                var cosmosService = new CosmosService(credentials);

                var household = await cosmosService.LoadItem<Models.Household>(id);

                if (household is null)
                    return null;

                return new OkObjectResult(household);
            }
            catch (Exception ex)
            {
                log.LogError("Error", ex);

                return new BadRequestObjectResult(ex);
            }
        }
    }
}
