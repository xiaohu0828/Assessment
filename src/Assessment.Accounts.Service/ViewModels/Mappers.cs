namespace Assessment.Accounts.Service.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Mappers
    {
        public static CustomerInfoViewModel Map(Store.Models.Customer customer, List<Store.Models.Account> accounts)
        {
            var result = new CustomerInfoViewModel()
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                Accounts = Map(accounts)
            };

            return result;
        }

        public static List<AccountViewModel> Map(List<Store.Models.Account> items)
        {
            return items?.Select(item => Map(item)).ToList();
        }

        public static AccountViewModel Map(Store.Models.Account item)
        {
            return item == null ? null : new AccountViewModel
            {
                Id = item.Id,
                CustomerId = item.CustomerId,
                Balance = item.Balance,
                Transations = item.Transations,
                IsDeleted = item.IsDeleted,
            };
        }

        public static List<CustomerViewModel> Map(List<Store.Models.Customer> items)
        {
            return items?.Select(item => Map(item)).ToList();
        }

        public static CustomerViewModel Map(Store.Models.Customer item)
        {
            return item == null ? null : new CustomerViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Surname = item.Surname,
                IsDeleted = item.IsDeleted,
            };
        }
    }
}
