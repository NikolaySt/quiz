using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using QuizGame.Client.Models;

namespace QuizGame.Client;

public class QuizClient : SuperClient
{
    public QuizClient(Uri quizServiceUri, HttpClient httpClient)
        : base(quizServiceUri, httpClient)
    {
    }

    public async Task<Response<IEnumerable<Quiz>>> GetQuizzesAsync(CancellationToken cancellationToken)
    {
        var response = await ExecuteAsync(HttpMethod.Get, "/api/quizzes", cancellationToken);
        return await GetResponse<IEnumerable<Quiz>>(response);
    }

    public async Task<Response<Quiz>> GetQuizAsync(int id, CancellationToken cancellationToken)
    {
        var response = await ExecuteAsync(HttpMethod.Get, "/api/quizzes/" + id, cancellationToken);
        return await GetResponse<Quiz>(response);
    }

    public async Task<Response<Uri>> PostQuizAsync(Quiz quiz, CancellationToken cancellationToken)
    {
        var response = await ExecuteAsync(HttpMethod.Post, "/api/quizzes", quiz, cancellationToken);
        return await GetResponse<Uri>(response, response.Headers.Location, HttpStatusCode.Created);
    }

    public async Task<Response<Uri>> PostAnswerAsync(int quizId, int questionId, Answer answer, CancellationToken cancellationToken)
    {
        var response = await ExecuteAsync(HttpMethod.Post, $"/api/quizzes/{quizId}/questions/{questionId}/answers", answer, cancellationToken);
        return await GetResponse<Uri>(response, response.Headers.Location);
    }

    public async Task<Response<Uri>> PostQuestionAsync(int quizId, QuizQuestion question, CancellationToken cancellationToken)
    {
        var response = await ExecuteAsync(HttpMethod.Post, $"/api/quizzes/{quizId}/questions", question, cancellationToken);
        return await GetResponse<Uri>(response, response.Headers.Location, HttpStatusCode.Created);
    }

    public async Task<Response<object>> PutQuestionAsync(int quizId, int questionId, QuizQuestion question, CancellationToken cancellationToken)
    {
        var response = await ExecuteAsync(HttpMethod.Put, $"/api/quizzes/{quizId}/questions/{questionId}", question, cancellationToken);
        return await GetResponse<object>(response, null, HttpStatusCode.NoContent);
    }

    public async Task<Response<Uri>> PostQuizResponseAsync(QuestionResponse questionResponse, int quizId, CancellationToken cancellationToken)
    {
        var response = await ExecuteAsync(HttpMethod.Post, $"/api/quizzes/{quizId}/responses", questionResponse, cancellationToken);
        return await GetResponse<Uri>(response, response.Headers.Location, HttpStatusCode.Created);
    }
}