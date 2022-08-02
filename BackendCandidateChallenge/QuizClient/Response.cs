using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuizClient
{
    public struct Response<T>
    {
        public Response(HttpStatusCode statusCode, T value, string errorMessage = null)
        {
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
            Value = value;
        }

        public HttpStatusCode StatusCode { get; }
        public T Value { get; }
        public string ErrorMessage { get; }
    }
}