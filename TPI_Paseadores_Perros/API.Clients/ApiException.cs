using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Clients
{
    // Error propio de la capa de clientes: lleva el mensaje que devolvió la WebAPI.
    public class ApiException : Exception
    {
        public int StatusCode { get; }

        public ApiException(int statusCode, string mensaje) : base(mensaje)
        {
            StatusCode = statusCode;
        }
    }
}
