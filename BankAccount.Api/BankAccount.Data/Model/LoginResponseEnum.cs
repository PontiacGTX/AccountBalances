using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Data.Model
{
    public enum LoginResponseEnum
    {
        ERROR_EMAIL_AND_PASSWORD_NOT_MATCH=-2,
        ERROR_PASSWORD_NOT_MATCH=-1,
        UNKNOWN=0,
        OK =1,
    }
}
