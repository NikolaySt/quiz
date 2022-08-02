using System.Threading.Tasks;
using QuizService.Data.Models;

namespace QuizService.Services.Answers
{
    public interface IAnswerService : IDataService<Answer>
    {
        Task<Answer> Find(int id);

        Task<bool> Delete(int id);
    }
}