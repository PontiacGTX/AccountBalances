using BankAccount.Data.Entity;
using BankAccount.DataAccess.Interfaces;
using BankAccount.Services.Interfaces;
using System.Linq.Expressions;

namespace BankAccount.Services.Services
{
    public class AccountServices : IAccountServices
    {
        private IAccountRepository _accountRepository;

        public AccountServices(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public Account Add(Account account)
        {
           return _accountRepository.Add(account);
        }

        public bool Delete(Guid id)
        {
            return (_accountRepository.Delete(id));
        }

        public bool Exist(Guid id)
        {
            return _accountRepository.Exist(id);
        }
        public bool Exist(Expression<Func<Account,bool>> selector)
        {
            return _accountRepository.Exist(selector);
        }

        public Account GetAccount(Guid id)
        {
            return (Account)_accountRepository.Get(id);
        }

        public Account GetAccount(Expression<Func<Account, bool>> selector)
        {
            return _accountRepository.Get(selector);
        }

        public Account GetAccountByEmail(string email)
        {
            return _accountRepository.Get(x=>x.User.email== email);
        }

        public IList<Account> GetAccounts()
        {
           return _accountRepository.GetAll();
        }
        public IList<Account> GetAccounts(Expression<Func<Account, bool>> selector)
        {
           return _accountRepository.GetAll(selector);
        }
        public Account Update(Account account, Guid id)
        {
            return _accountRepository.Update(account, id);
        }
    }
}
