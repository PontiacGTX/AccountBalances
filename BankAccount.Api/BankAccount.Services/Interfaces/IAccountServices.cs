using BankAccount.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Services.Interfaces
{
    public interface IAccountServices
    {
        bool Exist(Guid id);
        bool Exist(Expression<Func<Account, bool>> selector);
        IList<Account> GetAccounts();
        IList<Account> GetAccounts(Expression<Func<Account, bool>> selector);
        Account GetAccount(Guid id);
        Account GetAccount(Expression<Func<Account,bool>> selector);
        Account GetAccountByEmail(string email);
        Account Add(Account account);
        Account Update(Account account,Guid id);
        bool Delete(Guid id);

    }
}
