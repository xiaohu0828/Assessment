namespace Assessment.Transactions.Service.Functions
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Assessment.Transactions.Service.ViewModels;

    public class RequestBody
    {
        public Guid AccountIdFrom { get; set; }
        public Guid AccountIdTo { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }

    public static class CreateTransaction
    {
        private static Store.Store store = Store.Store.GetInstance();

        [FunctionName("CreateTransaction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/transactions")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Create a new transaction.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(requestBody))
            {
                return new BadRequestObjectResult("No request body");
            }

            RequestBody command = null;
            try
            {
                command = JsonConvert.DeserializeObject<RequestBody>(requestBody);
            }
            catch (Exception ex)
            {
                log.LogInformation("Failed to deserialize body, message: '{ExceptionMEssage}'", ex.Message);
                return new BadRequestObjectResult("Failed to deserialize request body. Please check json format and parameter types");
            }

            if (command.AccountIdFrom == null)
            {
                return new BadRequestObjectResult("Missing required string parameter: AccountIdFrom");
            }

            if (command.AccountIdTo == null)
            {
                return new BadRequestObjectResult("Missing required string parameter: AccountIdTo");
            }

            if (string.IsNullOrEmpty(command.Currency))
            {
                return new BadRequestObjectResult("Missing required string parameter: Currency");
            }

            var transation = new Store.Models.Transaction()
            {
                Id = Guid.NewGuid(),
                AccountIdFrom = command.AccountIdFrom,
                AccountIdTo = command.AccountIdTo,
                Amount = command.Amount,
                Currency = command.Currency,
                TransactionTime = DateTime.Now,
                CreatedTime = DateTime.Now
            };

            var result = await store.AddTransactionAsync(transation);

            if (!result.IsSuccess)
            {
                return new ObjectResult("Could not add transaction")
                {
                    StatusCode = 500
                };
            }

            return new OkObjectResult(Mappers.Map(result.Entity));
        }
    }
}
