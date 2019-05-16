namespace Assessment.Accounts.Service.ViewModels
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class CustomerInfoViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }

        [JsonProperty(PropertyName = "accounts")]
        public List<AccountViewModel> Accounts { get; set; }
    }
}
