using System.Net;
using System.Net.Http;

namespace QuizGame.Client;

public class QuizClientException : HttpRequestException
{
    public HttpStatusCode ResponseStatusCode { get; }

    public QuizClientException(HttpStatusCode responseStatusCode)
    {
        ResponseStatusCode = responseStatusCode;
    }
}