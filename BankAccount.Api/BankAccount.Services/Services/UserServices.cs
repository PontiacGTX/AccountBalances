using BankAccount.Data.Entity;
using BankAccount.Data.Model;
using BankAccount.DataAccess.Interfaces;
using BankAccount.DataAccess.Repository;
using BankAccount.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Services.Services
{
    public class UserServices : IUserServices
    {
        private IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User Add(User account)
        {
            return _userRepository.Add(account);
        }

        public bool Delete(Guid id)
        {
           return _userRepository.Delete(id);
        }

        public User GetUser(Guid id)
        {
            return _userRepository.Get(id);
        }

        public LoginResponseEnum GetLogin(string email, string password)
        {
            var response = _userRepository.Get(x => x.email.ToLower() == email.ToLower());
            var passwordMatch = _userRepository.Get(x => x.email.ToLower() == email.ToLower() && x.password == password);
            LoginResponseEnum result = LoginResponseEnum.UNKNOWN;
            if(response !=null && passwordMatch!=null)
            {
                result = LoginResponseEnum.OK;
            }
            else if(response!=null && passwordMatch!=null)
            {
                result = LoginResponseEnum.ERROR_PASSWORD_NOT_MATCH;
            }
            else if(response==null&& passwordMatch==null)
            {
                result = LoginResponseEnum.ERROR_EMAIL_AND_PASSWORD_NOT_MATCH;
            }
            return result;
        }
        public User GetUser(Expression<Func<User, bool>> selector)
        {
            return _userRepository.Get(selector);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.Get(x=>x.email.ToLower() == email.ToLower());
        }

        public IList<User> GetUsers()
        {
            return _userRepository.GetAll();
        }
        public bool Exist(Expression<Func<User,bool>> selector)
        {
            return _userRepository.Exist(selector);
        }
        public bool Exist(Guid id)
        {
            return _userRepository.Exist(id);
        }
        public User Update(User user, Guid id)
        {
            return _userRepository.Update(user, id);
        }
    }
}
