using System.Threading.Tasks;
using QuizGame.Service.Model.Answers;
using Xunit;

namespace QuizGame.Service.Tests
{
    public class AnswerServiceTest : SuperTest
    {
        [Fact]
        public async Task Create()
        {
            var mapper = GetMapper();

            await using var db = GetInMemoryDbContext();

            var service = new Services.Answers.AnswerService(db, mapper);

            var id = await service.Create(1, new AnswerCreateModel
            {
                Text = "Answer 1",
            });

            var result = await service.Find(id);

            Assert.NotNull(result);
            Assert.Equal("Answer 1", result.Text);
        }
    }
}