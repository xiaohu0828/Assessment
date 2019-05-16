namespace Assessment.Transactions.Service.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Mappers
    {
        public static List<TransactionViewModel> Map(List<Store.Models.Transaction> items)
        {
            return items?.Select(item => Map(item)).ToList();
        }

        public static TransactionViewModel Map(Store.Models.Transaction item)
        {
            return item == null ? null : new TransactionViewModel
            {
                Id = item.Id,
                AccountIdFrom = item.AccountIdFrom,
                AccountIdTo = item.AccountIdTo,
                Amount = item.Amount,
                Currency = item.Currency,
                TransactionTime = item.TransactionTime
            };
        }
    }
}
