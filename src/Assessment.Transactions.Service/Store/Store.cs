namespace Assessment.Transactions.Service.Store
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Assessment.Transactions.Service.Store.Models;

    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Transaction Entity { get; set; }
    }

    public sealed class Store
    {
        private static Store uniqueInstance;
        private List<Transaction> transactions;

        private Store()
        {
            transactions = new List<Transaction>();

            transactions.Add(new Transaction()
            {
                Id = new Guid("6E0C976D-2410-4A45-993E-A2A3DED15990"),
                AccountIdFrom = new Guid("4F3660BE-1D10-4433-9F09-4EC150CF3CEB"),
                AccountIdTo = new Guid("850E5D08-24C2-42D2-A09E-AB7B79DB3890"),
                Amount = 30.1M,
                Currency = "SEK",
                TransactionTime = DateTime.Now,
                CreatedTime = DateTime.Now
            });

            transactions.Add(new Transaction()
            {
                Id = new Guid("CA31D81E-200A-49D5-A423-7D3B22B49B40"),
                AccountIdFrom = new Guid("0C83AD04-BA33-418F-88B9-D0C53ADE4540"),
                AccountIdTo = new Guid("850E5D08-24C2-42D2-A09E-AB7B79DB3890"),
                Amount = 50M,
                Currency = "SEK",
                TransactionTime = DateTime.Now,
                CreatedTime = DateTime.Now
            });
        }

        public static Store GetInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new Store();
            }

            return uniqueInstance;
        }

        public Task<List<Transaction>> GetTransactions(Guid accountId)
        {
            return Task.FromResult(transactions.Where(item => item.AccountIdFrom == accountId || item.AccountIdTo == accountId).ToList());
        }

        internal Task<Response> AddTransactionAsync(Transaction transation)
        {
            try
            {
                transactions.Add(transation);
            }
            catch(Exception e)
            {
                return Task.FromResult(new Response()
                {
                    IsSuccess = false,
                    Message = e.ToString(),
                    Entity = null
                });
            }

            return Task.FromResult(new Response()
            {
                IsSuccess = true,
                Message = "",
                Entity = transation
            });
        }
    }
}
