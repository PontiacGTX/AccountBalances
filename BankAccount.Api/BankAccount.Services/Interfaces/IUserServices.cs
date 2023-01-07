using BankAccount.Data.Entity;
using BankAccount.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Services.Interfaces
{
    public interface IUserServices
    {
        IList<User> GetUsers();
        User GetUser(Guid id);
        User GetUser(Expression<Func<User, bool>> selector);

        bool Exist(Guid id);

        bool Exist(Expression<Func<User, bool>> selector);
        User GetUserByEmail(string email);
        LoginResponseEnum GetLogin(string email, string password);
        User Add(User account);
        User Update(User account, Guid id);
        bool Delete(Guid id);
    }
}
