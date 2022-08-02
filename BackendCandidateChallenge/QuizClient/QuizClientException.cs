using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuizClient
{
    public class QuizClientException : HttpRequestException
    {
        public HttpStatusCode ResponseStatusCode { get; }

        public QuizClientException(HttpStatusCode responseStatusCode)
        {
            ResponseStatusCode = responseStatusCode;
        }
    }
}