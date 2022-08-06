using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizGame.Common.Services;
using QuizGame.Service.Data;
using QuizGame.Service.Data.Models;
using QuizGame.Service.Model.Quizzes;

namespace QuizGame.Service.Services.Quizzes;

public class QuizService : DataService<Quiz, QuizDbContext>, IQuizService
{
    private readonly IMapper _mapper;

    public QuizService(QuizDbContext db, IMapper mapper)
        : base(db)
        => _mapper = mapper;

    public async Task<Quiz> Find(int id)
        => await All()
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<bool> Exist(int id)
    {
        return await All().AnyAsync(it => it.Id == id);
    }

    public async Task<int> Update(int id, QuizUpdateModel model)
    {
        var item = _mapper.Map<Quiz>(model);
        item.Id = id;
        await Save(item);
        return item.Id;
    }

    public async Task<int> Create(QuizCreateModel model)
    {
        var quiz = _mapper.Map<Quiz>(model);
        await Save(quiz);
        return quiz.Id;
    }

    public async Task<QuizResponseModel> GetDetails(int id)
    {
        var result = await _mapper.ProjectTo<QuizResponseModel>(Data.Quiz).FirstOrDefaultAsync(it => it.Id == id);
        return result;
    }

    public async Task<bool> Delete(int id)
    {
        var item = await Data.Quiz.FindAsync(id);

        if (item == null) return false;

        Data.Quiz.Remove(item);

        await Data.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<QuizResponseModel>> GetAll()
        => await _mapper
            .ProjectTo<QuizResponseModel>(Data.Quiz).ToListAsync();
}