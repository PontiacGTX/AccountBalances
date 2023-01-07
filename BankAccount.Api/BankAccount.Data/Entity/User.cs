using BankAccount.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Data.Entity
{
    public class User
    {
        public User()
        {

        }
        public User(UserModel userModel)
        {
            this.company = userModel.company;
            this.email = userModel.email;
            this.first = userModel.name.first;
            this.phone = userModel.phone;
            this.password = userModel.password;
            this.last= userModel.name.last;
            this.eyeColor = userModel.eyeColor;
            this.address = userModel.address;
            this.UserId = Guid.NewGuid();
            this.picture = userModel.picture;
            this.age= userModel.age;
            this.isActiveUser = isActiveUser?? true;
        }
        [Key]
        public Guid UserId { get; set; }
        [Required]
        public string first { get; set; }
        [Required]
        public string last { get; set; }
      
        public int age { get; set; }
        public string address { get; set; }
        public string eyeColor { get; set; }
        public string company { get; set; }
        [Required]
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string picture { get; set; }
        public bool? isActiveUser { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
