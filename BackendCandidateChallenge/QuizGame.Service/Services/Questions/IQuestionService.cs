using System.Threading.Tasks;
using QuizGame.Common.Services;
using QuizGame.Service.Data.Models;
using QuizGame.Service.Model.Questions;

namespace QuizGame.Service.Services.Questions;

public interface IQuestionService : IDataService<Question>
{
    Task<Question> Find(int id);

    Task<bool> Exist(int id);

    Task<int> Create(int quizId, QuestionCreateModel model);

    Task<int> Update(int quizId, int questionId, QuestionUpdateModel model);

    Task<bool> Delete(int id);
}