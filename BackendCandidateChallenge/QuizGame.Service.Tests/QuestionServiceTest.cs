using System.Threading.Tasks;
using QuizGame.Service.Model.Questions;
using Xunit;

namespace QuizGame.Service.Tests;

public class QuestionServiceTest : SuperTest
{
    [Fact]
    public async Task Create()
    {
        var mapper = GetMapper();

        await using var db = GetInMemoryDbContext();

        var service = new Services.Questions.QuestionService(db, mapper);

        var id = await service.Create(1, new QuestionCreateModel
        {
            Text = "Question 1",
        });

        var result = await service.Find(id);

        Assert.NotNull(result);
        Assert.Equal("Question 1", result.Text);
    }
}