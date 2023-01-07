using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Common.Helpers
{
    public static class ConfigurationReader
    {
        public static T TryGetSection<T>(this IConfiguration config,string section) where T :struct, IComparable, IConvertible, IComparable<T>, IEquatable<T>
        {
            object val = config.GetSection(section).Value;
            T outp = default;
            try
            {
                outp =  (T)Convert.ChangeType(val, typeof(T));
            }
            catch (Exception ex)
            {
                return default;
            }
            return outp;
        }
    }
}
