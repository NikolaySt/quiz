using System.Threading.Tasks;
using QuizService.Data.Models;

namespace QuizService.Services.Questions
{
    public interface IQuestionService : IDataService<Question>
    {
        Task<Question> Find(int id);

        Task<bool> Delete(int id);
    }
}