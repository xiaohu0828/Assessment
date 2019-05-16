namespace Assessment.Transactions.Service.ViewModels
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;

    public class TransactionViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "accountIdFrom")]
        public Guid AccountIdFrom { get; set; }

        [JsonProperty(PropertyName = "accountIdTo")]
        public Guid AccountIdTo { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "transactionTime")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime TransactionTime { get; set; }
    }
}
