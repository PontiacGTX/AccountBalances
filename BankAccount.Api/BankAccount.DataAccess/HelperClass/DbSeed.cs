using BankAccount.Data.Entity;
using BankAccount.Data.Model;
using BankAccount.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankAccount.Common.HelperClass
{
    public static class DbSeed
    {
        
        public static async Task FeedDb(this AppDbContext ctx,string path) 
        {
            
            
                UsersModel uM = new();
                if (ctx.Users.Count() == 0)
                {
                    uM = JsonSerializer.Deserialize<UsersModel>(File.ReadAllText(path));
                    foreach(var user in uM.users)
                    {
                        var person = ctx.Users
                            .FirstOrDefault(x=>x.last.ToLower() == user.name.last.ToLower() 
                                               && x.first.ToLower()  == user.name.first.ToLower()
                                               && x.eyeColor.ToLower() == user.eyeColor.ToLower()
                                               && x.age == user.age
                                               && x.address.ToLower() == user.address.ToLower()
                                               && x.email.ToLower() == user.email.ToLower()  
                                               && x.phone.Trim() == user.phone.Trim()
                                               );
                        if (person == null)
                        {
                            var entry = ctx.Users.Add(new User(user));
                            person = entry.Entity;
                        }
                        var account = new Account(user, person);
                        ctx.Accounts.Add(account);
                    }


                        await ctx.SaveChangesAsync();
                }

            



        }
    }
}
