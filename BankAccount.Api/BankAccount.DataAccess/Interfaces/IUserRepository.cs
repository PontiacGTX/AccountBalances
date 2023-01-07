using BankAccount.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        User Get(Guid id);
        bool Exist(Guid id);
        bool Exist(Expression<Func<User, bool>> selector);
        User Get(Expression<Func<User, bool>> selector);
        IList<User> GetAll();
        IList<User> GetAll(Expression<Func<User, bool>> selector);

        User Add(User user);
        User Update(User user,Guid id);

        bool Delete(Guid id);

        bool Delete(User usr);


    }
}
