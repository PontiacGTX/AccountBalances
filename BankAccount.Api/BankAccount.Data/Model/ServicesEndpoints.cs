using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Data.Model
{
   
    public class ServicesEndpoints
    {
        
        
        public string BaseServicesUrl { get; set; }
        public string ExistAccountId { get; set; }
        public string AddAccount { get; set; }
        public string UpdateAccount { get; set; }
        public string GetAllAccount { get; set; }
        public string GetAccountById { get; set; }
        public string DeleteAccountById { get; set; }
        public string DeleteUserById { get; set; }
        public string Login { get; set; }
        public string ExistUserId { get; set; }
        public string GetUserById { get; set; }
        public string GetUserByEmail { get; set; }
        public string UpdateUser { get; set; }
        public string GetAllUsers { get; set; }
        public string AddUser { get; set; }
    }


}
