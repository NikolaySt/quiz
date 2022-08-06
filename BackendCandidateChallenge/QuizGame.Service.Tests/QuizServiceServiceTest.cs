using System.Linq;
using System.Threading.Tasks;
using QuizGame.Service.Data.Models;
using Xunit;

namespace QuizGame.Service.Tests;

public class QuizServiceServiceTest : SuperTest
{
    [Fact]
    public async Task GetAll()
    {
        var mapper = GetMapper();

        await using var db = GetInMemoryDbContext();

        db.Set<Quiz>().Add(new Quiz() { Id = 1, Title = "Test quiz 1" });
        db.Set<Quiz>().Add(new Quiz() { Id = 2, Title = "Test quiz 2" });
        await db.SaveChangesAsync();

        var service = new Services.Quizzes.QuizService(db, mapper);

        var result = await service.GetAll();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(2, result.Last().Id);
        Assert.Equal("Test quiz 2", result.Last().Title);
    }
}