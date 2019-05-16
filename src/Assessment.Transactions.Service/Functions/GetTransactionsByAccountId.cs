namespace Assessment.Transactions.Service.Functions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Assessment.Transactions.Service.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;

    public static class GetTransactionsByAccountId
    {
        private static Store.Store store = Store.Store.GetInstance();

        [FunctionName("GetTransactionsByAccountId")]
        public static async Task<IActionResult> Function(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/transactions/{accountId}")] HttpRequest req, ILogger log, string accountId)
        {
            log.LogInformation("Get all transactions information by account id {Id}", accountId);

            if (string.IsNullOrWhiteSpace(accountId))
            {
                return new BadRequestObjectResult("No accountId specified.");
            }

            if(!Guid.TryParse(accountId, out var id))
            {
                return new BadRequestObjectResult("AccountId is not correct guid.");
            }

            var transactions = await store.GetTransactions(id);

            if (transactions == null || !transactions.Any())
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(Mappers.Map(transactions));
        }
    }
}
