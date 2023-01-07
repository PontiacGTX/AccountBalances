using BankAccount.Common.Responses;
using BankAccount.Data.Entity;
using BankAccount.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Presentation.Services.Interfaces
{
    public interface IUserServices
    {
        Task<HttpResponse> Exist(Guid id);
        Task<HttpResponse> GetUser(Guid id);
        Task<HttpResponse> GetUser(string email);
        Task<HttpResponse> Login(LoginModel model);
        Task<HttpResponse> GetAll();
        Task<HttpResponse> Add(User user);
        Task<HttpResponse> Update(User user, Guid id);
        Task<HttpResponse> Delete(Guid id);
    }
}
