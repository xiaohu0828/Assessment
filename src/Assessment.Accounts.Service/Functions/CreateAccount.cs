namespace Assessment.Accounts.Service.Functions
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
    using Assessment.Accounts.Service.Services;
    using System.Net.Http;
    using Assessment.Accounts.Service.ViewModels;

    public class RequestBody
    {
        public Guid CustomerID { get; set; }
        public decimal InitialCredit { get; set; }
    }

    public static class CreateAccount
    {
        private static Store.Store store = Store.Store.GetInstance();

        [FunctionName("CreateAccount")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/accounts")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Create a new account for user.");

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

            if (command.CustomerID == null)
            {
                return new BadRequestObjectResult("Missing required string parameter: customerID");
            }

            var newAccount = await store.CreateAccount(command.CustomerID);

            var transactionsService = new TransactionsService(new HttpClientHandler());

            if(command.InitialCredit > 0)
            {
                var initialTransation = new TransactionViewModel()
                {
                    Id = Guid.NewGuid(),
                    AccountIdFrom = Guid.Empty,
                    AccountIdTo = newAccount.Id,
                    Amount = command.InitialCredit,
                    Currency = "SEK",
                    TransactionTime = DateTime.Now
                };

                var transation = await transactionsService.CreateTransaction(initialTransation);
                if (transation == null)
                {
                    return new BadRequestObjectResult($"Failed to create transaction for user {command.CustomerID}");
                }

                newAccount.Transations.Add(transation);
                newAccount.Balance = transation.Amount;
            }

            return new OkObjectResult(Mappers.Map(newAccount));
        }
    }
}
