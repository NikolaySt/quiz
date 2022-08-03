using System.Collections.Generic;
using System.Threading.Tasks;
using QuizGame.Common.Services;
using QuizGame.Service.Data.Models;
using QuizGame.Service.Model.Quizes;

namespace QuizGame.Service.Services.Quizes
{
    public interface IQuizService : IDataService<Quiz>
    {
        Task<Quiz> Find(int id);

        Task<int> Update(int id, QuizUpdateModel model);

        Task<bool> Exist(int id);

        Task<int> Create(QuizCreateModel model);

        Task<bool> Delete(int id);

        Task<QuizResponseModel> GetDetails(int id);

        Task<IEnumerable<QuizResponseModel>> GetAll();
    }
}