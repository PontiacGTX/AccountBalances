using BankAccount.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.DataAccess.Interfaces
{
    public interface IAccountRepository
    {
        public IList<Account> GetAll();
        public Account Get(Guid id);
        public IList<Account> GetAll(Expression<Func<Account, bool>> selector);
        Account Get(Expression<Func<Account, bool>> selector);
        public Account Add(Account account);
        public Account Update(Account account,Guid id);
        public bool Delete(Account account);
        public bool Delete(Guid accountId);
        public bool Exist(Expression<Func<Account, bool>> selector);
        public bool Exist(Guid id);


    }
}
