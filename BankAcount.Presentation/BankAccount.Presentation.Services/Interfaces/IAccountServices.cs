using BankAccount.Common.Responses;
using BankAccount.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Presentation.Services.Interfaces
{
    public interface IAccountServices
    {
        Task<HttpResponse> Exist(Guid id);
        Task<HttpResponse> GetAccount(Guid id);
        Task<HttpResponse> GetAll();
        Task<HttpResponse> Add(Account account);
        Task<HttpResponse> Update(Account account,Guid id);
        Task<HttpResponse> Delete(Guid id);
    }
}
