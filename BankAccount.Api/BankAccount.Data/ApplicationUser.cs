using BankAccount.Data.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Data
{
    public class ApplicationUser:IdentityUser
    {
        public ApplicationUser()
        {

        }
        public string Address { get; set; }
        public string Compaany { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ApplicationUser(UserModel model,bool confirmed=true)
        {
            this.PhoneNumber = model.phone;
            this.UserName = model.email.ToUpper();
            this.NormalizedEmail = model.email.ToUpper();
            this.NormalizedUserName =model.email.ToUpper();
            this.Email = model.email.ToUpper();
            this.Id = Guid.NewGuid().ToString();
            this.EmailConfirmed = confirmed;
            this.Address = model.address;
            this.Compaany = model.company;
            this.FirstName = model.name.first;
            this.LastName= model.name.last;

        }
    }
}
