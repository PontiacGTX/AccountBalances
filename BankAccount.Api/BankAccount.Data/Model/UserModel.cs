using BankAccount.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankAccount.Data.Model
{


    public class Rootobject
    {
        
    }
    public class UserModel
    {
        public UserModel()
        {

        }
        public UserModel(User user, bool first = true, Guid? guid = null)
        {
            this.address = user.address;
            this.email = user.email;
            this.password = user.password;
            this.phone = user.phone;
            this.company = user.company;
            this.name = new NameModel { first = user?.first!, last = user?.last! };
            this.picture = user.picture;
            this.age = user.age;
            this.isActive = (first ? user.Accounts.FirstOrDefault()?.isActive : user.Accounts.FirstOrDefault(x => x.guid == guid)?.isActive) ?? false;
            this.guid = first ? user.Accounts.FirstOrDefault()?.guid.ToString()! : user.Accounts.FirstOrDefault(x => x.guid == guid)?.guid.ToString();
            this.balance = first ? user.Accounts.FirstOrDefault()?.balance : user.Accounts.FirstOrDefault(x => x.guid == guid)?.balance;
            this._id = first ? user.Accounts.FirstOrDefault()?._id : user.Accounts.FirstOrDefault(x => x.guid == guid)?._id;
        }
        public string _id { get; set; }
        public string guid { get; set; }
        public bool isActive { get; set; }
        public string balance { get; set; }
        public string picture { get; set; }
        public int age { get; set; }
        public string eyeColor { get; set; }
        public NameModel name { get; set; }
        public string company { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
    }
}
