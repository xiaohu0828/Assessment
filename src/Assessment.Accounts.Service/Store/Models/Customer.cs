namespace Assessment.Accounts.Service.Store.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;

    public class Customer
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }

        [JsonProperty(PropertyName = "isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty(PropertyName = "createdTime")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreatedTime { get; set; }
    }
}
