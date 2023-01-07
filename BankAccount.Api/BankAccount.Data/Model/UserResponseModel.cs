using BankAccount.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Data.Model
{
    public class UserResponseModel : UserModel
    {
        public UserResponseModel(User user, bool first = true, Guid? guid = null) : base(user, first, guid)
        {
            UserId = user.UserId;
        }

        public Guid? UserId { get; set; }
    }


   

}
