using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuizGame.Service.Model.Questions;
using QuizGame.Service.Model.Quizzes;
using Xunit;

namespace QuizGame.Service.Tests;

public class QuizzesControllerTest : SuperTest
{
    private const string QuizApiEndPoint = "/api/quizzes/";

    [Fact]
    public async Task PostNewQuizAddsQuiz()
    {
        var quiz = new QuizCreateModel("Test title");
        using var testHost = GetTestWebServer();
        var client = testHost.CreateClient();
        var content = new StringContent(JsonConvert.SerializeObject(quiz));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var response = await client.PostAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}"),
            content);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
    }

    [Fact]
    public async Task AQuizExistGetReturnsQuiz()
    {
        using var testHost = GetTestWebServer();
        var client = testHost.CreateClient();
        const long quizId = 1;
        var response = await client.GetAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}{quizId}"));
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Content);
        var quiz = JsonConvert.DeserializeObject<QuizResponseModel>(await response.Content.ReadAsStringAsync());
        Assert.Equal(quizId, quiz.Id);
        Assert.Equal("My first quiz", quiz.Title);
    }

    [Fact]
    public async Task AQuizDoesNotExistGetFails()
    {
        using var testHost = GetTestWebServer();
        var client = testHost.CreateClient();
        const long quizId = 999;
        var response = await client.GetAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}{quizId}"));
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task AQuizDoesNotExists_WhenPostingAQuestion_ReturnsNotFound()
    {
        const string quizApiEndPoint = "/api/quizzes/999/questions";

        using var testHost = GetTestWebServer();
        var client = testHost.CreateClient();

        var question = new QuestionCreateModel("The answer to everything is what?");
        var content = new StringContent(JsonConvert.SerializeObject(question));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var response = await client.PostAsync(new Uri(testHost.BaseAddress, $"{quizApiEndPoint}"), content);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}