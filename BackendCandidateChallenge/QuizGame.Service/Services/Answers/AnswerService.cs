using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizGame.Common.Services;
using QuizGame.Service.Data;
using QuizGame.Service.Data.Models;
using QuizGame.Service.Model.Answers;
using QuizGame.Service.Model.Questions;

namespace QuizGame.Service.Services.Answers
{
    public class AnswerService : DataService<Answer, QuizDbContext>, IAnswerService
    {
        private readonly IMapper _mapper;

        public AnswerService(QuizDbContext db, IMapper mapper)
            : base(db)
            => _mapper = mapper;

        public async Task<int> Create(int questionId, AnswerCreateModel model)
        {
            var item = _mapper.Map<Answer>(model);
            item.QuestionId = questionId;
            await Save(item);
            return item.Id;
        }

        public async Task<bool> Exist(int id)
        {
            return await All().AnyAsync(it => it.Id == id);
        }

        public async Task<int> Update(int questionId, int answerId, AnswerUpdateModel model)
        {
            var item = _mapper.Map<Answer>(model);
            item.Id = answerId;
            item.QuestionId = questionId;
            await Save(item);
            return item.Id;
        }

        public async Task<Answer> Find(int id)
            => await Data.Answer.FindAsync(id);

        public async Task<bool> Delete(int id)
        {
            var item = await Data.Answer.FindAsync(id);

            if (item == null)
            {
                return false;
            }

            Data.Answer.Remove(item);

            await Data.SaveChangesAsync();

            return true;
        }
    }
}