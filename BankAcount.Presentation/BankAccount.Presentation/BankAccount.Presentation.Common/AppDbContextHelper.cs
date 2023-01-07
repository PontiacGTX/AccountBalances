using BankAccount.Data;
using BankAccount.Data.Model;
using BankAccount.Presentation.DataAccess;
using Microsoft.AspNetCore.Identity;
using System.IO;
using System.Text.Json;

namespace BankAccount.Presentation.Common
{
    public static class AppDbContextHelper
    {
        public static async Task FeedDbUsers(this UserManager<ApplicationUser> userManager , string fileSrc)
        {
            if(userManager.Users.Count()==0)
            {
                UsersModel uM = new UsersModel();
                uM = JsonSerializer.Deserialize<UsersModel>(File.ReadAllText(fileSrc))!;

                foreach(var user in uM.users)
                {
                    try
                    {
                        var res = await userManager.CreateAsync(new ApplicationUser(user, true), user.password);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}