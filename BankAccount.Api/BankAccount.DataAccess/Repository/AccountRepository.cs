using BankAccount.Data.Entity;
using BankAccount.DataAccess.Interfaces;
using System.Linq.Expressions;

namespace BankAccount.DataAccess.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }
        public Account Add(Account account)
        {
            try
            {
                var entry = _context.Accounts.Add(account);
                _context.SaveChanges();
                return entry.Entity;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool Delete(Account account)
        {
            return Delete(account.guid);
           
        }

        public bool Delete(Guid accountId)
        {
            var account =Get(accountId);

            if (account == null)
                return true;

            account.isActive = false;
            _context.SaveChanges();

            return  !Get(x => x.guid == accountId).isActive;
        }

        public bool Exist(Guid id)
        {
            return _context.Accounts.Any(x=>x.guid==id);
        }


        public bool Exist(Expression<Func<Account, bool>> selector)
        {
            return _context.Accounts.Any(selector);
        }

        public Account Get(Guid id)
        {
            return _context.Accounts.FirstOrDefault(x => x.guid == id)!;
        }

        public IList<Account> GetAll(Expression<Func<Account, bool>> selector)
        {
            return _context.Accounts.Where(selector).ToList();
        }

        public Account Get(Expression<Func<Account, bool>> selector)
        {
            return _context.Accounts.FirstOrDefault(selector);
        }
        public IList<Account> GetAll()
        {
            return _context.Accounts.ToList();
        }

        public Account Update(Account account, Guid id)
        {
            var ogaccount =_context.Accounts.Find(id);

            if (ogaccount is null)
                return null;

            var entry  =_context.Entry(ogaccount);

            entry.CurrentValues.SetValues(account);

            _context.SaveChanges();

            return entry.Entity;
        }
    }
}
