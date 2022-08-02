using System.Collections.Generic;
using System.Threading.Tasks;
using QuizService.Data.Models;
using QuizService.Model.Quizes;

namespace QuizService.Services.Quizes
{
    public interface IQuizService : IDataService<Quiz>
    {
        Task<Quiz> Find(int id);

        Task<bool> Delete(int id);

        Task<QuizResponseModel> GetDetails(int id);

        Task<IEnumerable<QuizResponseModel>> GetAll();
    }
}