using BankAccount.Common;
using BankAccount.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Data.Entity;

public class Account
{
    public Account()
    {

    }
    public Account(UserModel userModel,User person)
    {
   
       
        this.guid   =Guid.NewGuid();
        this.balance = userModel.balance;
        this._id = userModel._id;
        this.isActive = userModel.isActive;
        this.UserId = person?.UserId ?? Guid.Empty;
    }
    [Key]
    public Guid guid { get; set; }
    public string _id { get; set; }
    public bool isActive { get; set; }
    public string balance { get; set; }
    [ForeignKey("UserId")]
    public User  User { get; set; }
    public Guid UserId { get; set; }
   
}
