namespace Assessment.Accounts.Service.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Assessment.Accounts.Service.ViewModels;
    using Newtonsoft.Json;

    public interface ITransactionsService
    {
        Task<List<TransactionViewModel>> GetTransactionsByAccountId(Guid accountId);
        Task<TransactionViewModel> CreateTransaction(TransactionViewModel transaction);
    }

    public class TransactionsService : ITransactionsService
    {
        private readonly string _createTransaction = "http://localhost:7010/api/v1/transactions";
        private readonly string _getTransactionsByAccountId = "http://localhost:7010/api/v1/transactions/{accountId}";
        private readonly HttpClient _httpClient;

        public TransactionsService(HttpMessageHandler httpMessageHandler)
        {
            _httpClient = new HttpClient(httpMessageHandler);
        }

        public async Task<List<TransactionViewModel>> GetTransactionsByAccountId(Guid accountId)
        {
            var url = _getTransactionsByAccountId.Replace("{accountId}", accountId.ToString());
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await _httpClient.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var transaction = JsonConvert.DeserializeObject<List<TransactionViewModel>>(responseString);
                return transaction;
            }

            return new List<TransactionViewModel>();
        }

        public async Task<TransactionViewModel> CreateTransaction(TransactionViewModel transaction)
        {
            var url = _createTransaction;
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
            
            httpRequest.Content = new StringContent(JsonConvert.SerializeObject(transaction));

            var response = await _httpClient.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TransactionViewModel>(responseString);
                return result;
            }

            throw new Exception($"Error response from UserService GetUserIdByEmail with status code {response.StatusCode} and reason: {response.ReasonPhrase}");
        }
    }
}
