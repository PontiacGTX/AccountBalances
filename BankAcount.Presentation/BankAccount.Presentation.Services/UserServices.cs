using BankAccount.Common.Responses;
using BankAccount.Data.Entity;
using BankAccount.Data.Model;
using BankAccount.Presentation.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Presentation.Services
{

    public class UserServices : IUserServices
    {
        string BuildUrl(string baseUrl, string endpoint, string param = null) => $"{baseUrl}{endpoint}{param}";
        static HttpClient _httpClient { get; set; }

        private readonly ServicesEndpoints _servicesEndpoints;

        public UserServices(HttpClient httpClient, ServicesEndpoints servicesEndpoints)
        {
            _httpClient = httpClient;
            _servicesEndpoints = servicesEndpoints;
        }


        public async Task<HttpResponse> Add(User user)
        {
            var url = BuildUrl(_servicesEndpoints.BaseServicesUrl, _servicesEndpoints.DeleteUserById);
            HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync(url,user);
            var apiResponse = System.Text.Json.JsonSerializer.Deserialize<BankAccount.Common.Responses.HttpResponse>(await responseMessage.Content.ReadAsStringAsync(),  new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive  = true });
            return apiResponse!;
        }

        public async Task<HttpResponse> Delete(Guid id)
        {

            var url = BuildUrl(_servicesEndpoints.BaseServicesUrl, _servicesEndpoints.DeleteUserById, id.ToString());
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);
            var apiResponse = System.Text.Json.JsonSerializer.Deserialize<BankAccount.Common.Responses.HttpResponse>(await responseMessage.Content.ReadAsStringAsync(),  new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive  = true });
            return apiResponse!;
        }

        public async Task<HttpResponse> Exist(Guid id)
        {
            var url = BuildUrl(_servicesEndpoints.BaseServicesUrl, _servicesEndpoints.ExistUserId,id.ToString());
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);
            var apiResponse = System.Text.Json.JsonSerializer.Deserialize<BankAccount.Common.Responses.HttpResponse>(await responseMessage.Content.ReadAsStringAsync(),  new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive  = true });
            return apiResponse!;
        }

        public async Task<HttpResponse> GetAll()
        {
            var url = BuildUrl(_servicesEndpoints.BaseServicesUrl, _servicesEndpoints.GetAllUsers);
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);
            var apiResponse = System.Text.Json.JsonSerializer.Deserialize<BankAccount.Common.Responses.HttpResponse>(await responseMessage.Content.ReadAsStringAsync(),  new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive  = true });
            return apiResponse!;
        }

        public async Task<HttpResponse> GetUser(Guid id)
        {
            var url = BuildUrl(_servicesEndpoints.BaseServicesUrl, _servicesEndpoints.GetUserById,id.ToString());
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);
            var apiResponse = System.Text.Json.JsonSerializer.Deserialize<BankAccount.Common.Responses.HttpResponse>(await responseMessage.Content.ReadAsStringAsync(),  new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive  = true });
            return apiResponse!;
        } 
        
        public async Task<HttpResponse> GetUser(string email)
        {
            var url = BuildUrl(_servicesEndpoints.BaseServicesUrl, _servicesEndpoints.GetUserByEmail, email);
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);
            var apiResponse = System.Text.Json.JsonSerializer.Deserialize<BankAccount.Common.Responses.HttpResponse>(await responseMessage.Content.ReadAsStringAsync(),  new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive  = true });
            return apiResponse!;
        }

        public async Task<HttpResponse> Login(LoginModel model)
        {
            var url = BuildUrl(_servicesEndpoints.BaseServicesUrl, _servicesEndpoints.Login);
            HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync(url, model);
            var apiResponse = System.Text.Json.JsonSerializer.Deserialize<BankAccount.Common.Responses.HttpResponse>(await responseMessage.Content.ReadAsStringAsync(),  new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive  = true });
            return apiResponse!;
        }

        public async Task<HttpResponse> Update(User user, Guid id)
        {
            var url = BuildUrl(_servicesEndpoints.BaseServicesUrl, _servicesEndpoints.UpdateUser,id.ToString());
            HttpResponseMessage responseMessage = await _httpClient.PutAsJsonAsync(url, user);
            var apiResponse = System.Text.Json.JsonSerializer.Deserialize<BankAccount.Common.Responses.HttpResponse>(await responseMessage.Content.ReadAsStringAsync(),  new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive  = true });
            return apiResponse!;
        }
    }
}
