using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Data.Model
{
    public class LoginResponseModel
    {
        public LoginResponseEnum Result { get; set; }
        public Guid? Id { get; set; }
    }
}
