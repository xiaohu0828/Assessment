namespace Assessment.Accounts.Service.Store
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class Store
    {
        private static Store uniqueInstance;
        private List<Models.Account> accounts;
        private List<Models.Customer> customers;

        private Store()
        {
            customers = new List<Models.Customer>();
            accounts = new List<Models.Account>();

            customers.Add(new Models.Customer() {
                Id = new Guid("2DB0AB71-E14C-436A-BA59-D58205D8EA6C"),
                Name = "Blue",
                Surname = "Harvest",
                IsDeleted = false,
                CreatedTime = DateTime.Now
            });

            customers.Add(new Models.Customer()
            {
                Id = new Guid("DF8E3ED7-4896-4714-BD47-37C12FD5392E"),
                Name = "Stockholm",
                Surname = "Sweden",
                IsDeleted = false,
                CreatedTime = DateTime.Now
            });

            accounts.Add(new Models.Account(){
                Id = new Guid("4F3660BE-1D10-4433-9F09-4EC150CF3CEB"),
                CustomerId = new Guid("2DB0AB71-E14C-436A-BA59-D58205D8EA6C"),
                Name = "First Account",
                Balance =100,
                Currency = "SEK",
                IsDeleted = false,
                CreatedTime = DateTime.Now,
                Transations = new List<dynamic>()
            });

            accounts.Add(new Models.Account()
            {
                Id = new Guid("850E5D08-24C2-42D2-A09E-AB7B79DB3890"),
                CustomerId = new Guid("2DB0AB71-E14C-436A-BA59-D58205D8EA6C"),
                Name = "Second Account",
                Balance = 100,
                Currency = "SEK",
                IsDeleted = false,
                CreatedTime = DateTime.Now,
                Transations = new List<dynamic>()
            });

            accounts.Add(new Models.Account()
            {
                Id = new Guid("0C83AD04-BA33-418F-88B9-D0C53ADE4540"),
                CustomerId = new Guid("DF8E3ED7-4896-4714-BD47-37C12FD5392E"),
                Name = "Account",
                Balance = 1000,
                Currency = "SEK",
                IsDeleted = false,
                CreatedTime = DateTime.Now,
                Transations = new List<dynamic>()
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

        public Task<Models.Customer> GetUser(Guid userId)
        {
            return Task.FromResult(customers.Where(item => item.Id == userId).FirstOrDefault());
        }

        public Task<List<Models.Account>> GetUserAccounts(Guid userId)
        {
            return Task.FromResult(accounts.Where(item => item.CustomerId == userId).ToList());
        }

        public Task<Models.Account> CreateAccount(Guid userId)
        {
            var account = new Models.Account()
            {
                Id = Guid.NewGuid(),
                CustomerId = userId,
                Name = "New Account",
                Balance = 0,
                Currency = "SEK",
                IsDeleted = false,
                CreatedTime = DateTime.Now,
                Transations = new List<dynamic>()
            };

            accounts.Add(account);
            return Task.FromResult(account);
        }
    }
}
