using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Common.Responses
{
    public class HttpResponse:Response
    {
         public IEnumerable<string> Validation { get; set; }
    }
}
