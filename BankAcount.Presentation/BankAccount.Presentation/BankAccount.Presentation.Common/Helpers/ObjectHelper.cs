using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Presentation.Common.Helpers
{
    public static class ObjectHelper
    {
        public static T CastJsonAs<T>(this object o)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(o.ToString(),new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            })!;
        }
    }
}
