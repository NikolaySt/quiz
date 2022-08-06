using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizGame.Common.Services;
using QuizGame.Service.Data;
using QuizGame.Service.Data.Models;
using QuizGame.Service.Model.Questions;

namespace QuizGame.Service.Services.Questions;

public class QuestionService : DataService<Question, QuizDbContext>, IQuestionService
{
    private readonly IMapper _mapper;

    public QuestionService(QuizDbContext db, IMapper mapper)
        : base(db)
        => _mapper = mapper;

    public async Task<int> Create(int quizId, QuestionCreateModel model)
    {
        var item = _mapper.Map<Question>(model);
        item.QuizId = quizId;
        await Save(item);
        return item.Id;
    }

    public async Task<bool> Exist(int id)
    {
        return await All().AnyAsync(it => it.Id == id);
    }

    public async Task<int> Update(int quizId, int questionId, QuestionUpdateModel model)
    {
        var item = _mapper.Map<Question>(model);
        item.QuizId = quizId;
        item.Id = questionId;
        await Save(item);
        return item.Id;
    }

    public async Task<Question> Find(int id)
        => await Data.Question.FindAsync(id);

    public async Task<bool> Delete(int id)
    {
        var item = await Data.Question.FindAsync(id);

        if (item == null)
        {
            return false;
        }

        Data.Question.Remove(item);

        await Data.SaveChangesAsync();

        return true;
    }
}