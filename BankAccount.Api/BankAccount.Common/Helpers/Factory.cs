using BankAccount.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankAccount.Common.Helpers
{
    public static class Factory
    {
        public static  Response ErrorResponse()
        =>  Factory.GetResponse<ServerErrorResponse>(null,
                    500,
                    "Various internal unexpected errors happened",
                    false);
        public static byte[] GetBytes(this Response response) => Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(response,new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        public static Response GetResponse<T>(object response, int statusCode = 200, string message = "Ok", bool success = true, IEnumerable<string> validation = null)

        {
            var tipo = typeof(T);
            if (tipo == typeof(Response))
            {
                return new Response { Data = response, Message = message, StatusCode = statusCode, Success = success };
            }
            else if (tipo == typeof(ServerErrorResponse))
            {
                if (response is not null)
                    return new ServerErrorResponse { Data = response, StatusCode = statusCode, Message = message, Success = false, Validation = validation };

                return new ServerErrorResponse { Data = null, StatusCode = statusCode, Message = message, Success = false, Validation = validation };
            }
            return null!;
        }
    }
}
