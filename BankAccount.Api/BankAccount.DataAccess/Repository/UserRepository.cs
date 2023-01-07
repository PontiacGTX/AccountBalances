using BankAccount.Data.Entity;
using BankAccount.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public User Add(User user)
        {
           var entry = _context.Users.Add(user);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool Delete(Guid id)
        {
            var user =_context.Users.FirstOrDefault(x => x.UserId == id);
            user.isActiveUser = false;
            _context.SaveChanges();
            return !(bool)Get(id).isActiveUser;
        }

        public bool Delete(User usr)
        {
            return Delete(usr.UserId);
        }

        public User Get(Guid id)
        {
          return  Get(x => x.UserId == id);

        }

        public bool Exist(Guid id)
        {
            return _context.Users.Any(x => x.UserId == id);

        }
        public bool Exist(Expression<Func<User, bool>> selector)
        {
            return _context.Users.Any(selector);

        }
        public User Get(Expression<Func<User, bool>> selector)
        {
            return _context.Users.Include(x=>x.Accounts).FirstOrDefault(selector);
        }

        public IList<User> GetAll()
        {
            return _context.Users.Include(x=>x.Accounts).ToList();
        }

        public IList<User> GetAll(Expression<Func<User, bool>> selector)
        {
            return _context.Users.Where(selector).ToList();
        }

        public User Update(User user, Guid id)
        {
            var oguser = Get(id);

            if (oguser == null) 
                return null;

            var entry = _context.Entry(oguser);
            entry.CurrentValues.SetValues(user);
            _context.SaveChanges();
            return entry.Entity;
        }
    }
}
