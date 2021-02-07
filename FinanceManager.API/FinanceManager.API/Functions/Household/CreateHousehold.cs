using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FinanceManager.Models.Data;
using FinanceManager.Common;
using Microsoft.Extensions.Configuration;
using FinanceManager.Services;

namespace FinanceManager.API.Functions.Household
{
    public class CreateHousehold
    {
        private readonly IConfiguration config;

        public CreateHousehold(IConfiguration config)
        {
            this.config = config;
        }

        [FunctionName("CreateHousehold")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "household")] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var household = JsonConvert.DeserializeObject<Models.Household>(requestBody);

                if (household is null)
                    return new BadRequestObjectResult("No body found.");

                if (string.IsNullOrWhiteSpace(household.Id))
                    household.Initialise();

                var credentials = new CosmosCredentials(config[ConfigSettings.CosmosDataUri],
                    config[ConfigSettings.CosmosDataKey],
                    config[ConfigSettings.FinanceManagerDatabase],
                    CollectionConstants.Households, household.Id);

                var cosmosService = new CosmosService(credentials);

                var result = await cosmosService.CreateItemAsync(household);

                if (result is null)
                    return new BadRequestObjectResult("Something went wrong creating the household");

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);

                return new BadRequestObjectResult(ex);
            }
        }
    }
}
