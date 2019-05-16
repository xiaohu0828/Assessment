namespace Assessment.Accounts.Service.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Assessment.Accounts.Service.Services;
    using Assessment.Accounts.Service.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;

    public static class GetCustomerInfoById
    {
        private static Store.Store store = Store.Store.GetInstance();

        [FunctionName("GetCustomerInfoById")]
        public static async Task<IActionResult> Function(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/customers/{id}")] HttpRequest req, ILogger log, string id)
        {
            log.LogInformation("Get Customer information by customer id {Id}", id);

            if(!Guid.TryParse(id, out var customerId))
            {
                return new BadRequestObjectResult("Customer Id is not correct.");
            }

            var user = await store.GetUser(customerId);
            if (user == null || user.IsDeleted)
            {
                return new NotFoundResult();
            }

            var accounts = await store.GetUserAccounts(customerId);

            var transactionsService = new TransactionsService(new HttpClientHandler());

            if (accounts != null && accounts.Any())
            {
                foreach(var account in accounts)
                {
                    var transations = await transactionsService.GetTransactionsByAccountId(account.Id);
                    if (transations != null && transations.Any())
                    {
                        log.LogInformation($"Get transaction for accout {account.Id}");
                        account.Transations.AddRange(transations);
                    }
                }
            }

            return new OkObjectResult(Mappers.Map(user, accounts));
        }
    }
}
