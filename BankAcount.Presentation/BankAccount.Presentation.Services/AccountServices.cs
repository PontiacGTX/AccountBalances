using BankAccount.Common.Responses;
using BankAccount.Data.Entity;
using BankAccount.Data.Model;
using BankAccount.Presentation.Services.Interfaces;
using System.Net.Http.Json;

namespace BankAccount.Presentation.Services
{
    public class AccountServices : IAccountServices
    {
        string BuildUrl(string baseUrl, string endpoint,string param=null) =>$"{baseUrl}{endpoint}{param}";
        static HttpClient _httpClient { get; set; }

        private readonly ServicesEndpoints _servicesEndpoints;

        public AccountServices(HttpClient httpClient,ServicesEndpoints servicesEndpoints)
        {
            _httpClient = httpClient;
            _servicesEndpoints = servicesEndpoints;
        }
       

        public async Task<HttpResponse> Delete(Guid id)
        {
            var url = BuildUrl(_servicesEndpoints.BaseServicesUrl, _servicesEndpoints.DeleteAccountById,id.ToString());
            HttpResponseMessage responseMessage = await _httpClient.DeleteAsync(url);
            var apiResponse = System.Text.Json.JsonSerializer.Deserialize<BankAccount.Common.Responses.HttpResponse>(await responseMessage.Content.ReadAsStringAsync(),  new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive  = true });
            return apiResponse!;
        }

        public async Task<HttpResponse> Exist(Guid id)
        {
            var url = BuildUrl(_servicesEndpoints.BaseServicesUrl, _servicesEndpoints.ExistAccountId, id.ToString());
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);
            var apiResponse = System.Text.Json.JsonSerializer.Deserialize<BankAccount.Common.Responses.HttpResponse>(await responseMessage.Content.ReadAsStringAsync(),  new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive  = true });
            return apiResponse!;
        }

        public async Task<HttpResponse> GetAll()
        {
            var url = BuildUrl(_servicesEndpoints.BaseServicesUrl, _servicesEndpoints.GetAllAccount);
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);
            var apiResponse = System.Text.Json.JsonSerializer.Deserialize<BankAccount.Common.Responses.HttpResponse>(await responseMessage.Content.ReadAsStringAsync(),  new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive  = true });
            return apiResponse!;
        }

        public async Task<HttpResponse> GetAccount(Guid id)
        {
            var url = BuildUrl(_servicesEndpoints.BaseServicesUrl, _servicesEndpoints.GetAccountById,id.ToString());
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);
            var apiResponse = System.Text.Json.JsonSerializer.Deserialize<BankAccount.Common.Responses.HttpResponse>(await responseMessage.Content.ReadAsStringAsync(),  new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive  = true });
            return apiResponse!;
        }

        
        public async Task<HttpResponse> Add(Account account)
        {
            var url = BuildUrl(_servicesEndpoints.BaseServicesUrl, _servicesEndpoints.AddAccount);
            HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync(url, account);
            var apiResponse = System.Text.Json.JsonSerializer.Deserialize<BankAccount.Common.Responses.HttpResponse>(await responseMessage.Content.ReadAsStringAsync(),  new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive  = true });
            return apiResponse!;
        }

        public async Task<HttpResponse> Update(Account account, Guid id)
        {
            var url = BuildUrl(_servicesEndpoints.BaseServicesUrl, _servicesEndpoints.UpdateAccount,id.ToString());
            HttpResponseMessage responseMessage = await _httpClient.PutAsJsonAsync(url, account);
            var apiResponse = System.Text.Json.JsonSerializer.Deserialize<BankAccount.Common.Responses.HttpResponse>(await responseMessage.Content.ReadAsStringAsync(),  new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive  = true });
            return apiResponse!;
        }
    }
}
