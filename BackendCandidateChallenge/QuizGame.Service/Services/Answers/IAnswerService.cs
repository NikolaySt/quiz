using System.Threading.Tasks;
using QuizGame.Common.Services;
using QuizGame.Service.Data.Models;
using QuizGame.Service.Model.Answers;

namespace QuizGame.Service.Services.Answers
{
    public interface IAnswerService : IDataService<Answer>
    {
        Task<int> Create(int questionId, AnswerCreateModel model);

        Task<bool> Exist(int id);

        Task<int> Update(int questionId, int answerId, AnswerUpdateModel model);

        Task<Answer> Find(int id);

        Task<bool> Delete(int id);
    }
}