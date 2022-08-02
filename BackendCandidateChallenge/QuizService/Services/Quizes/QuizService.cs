using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizService.Data;
using QuizService.Data.Models;
using QuizService.Model.Quizes;
using static QuizService.Model.Quizes.QuizResponseModel;

namespace QuizService.Services.Quizes
{
    public class QuizService : DataService<Quiz>, IQuizService
    {
        private readonly IMapper mapper;

        public QuizService(QuizDbContext db, IMapper mapper)
            : base(db)
            => this.mapper = mapper;

        public async Task<Quiz> Find(int id)
            => await All()
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<QuizResponseModel> GetDetails(int id)
        {
            var result = await Data.Quizes.AsQueryable()
                .Join(Data.Questions, quiz => quiz.Id, question => question.QuizId, (quiz, question) => new { quiz, question })
                .Join(Data.Answers, question => question.question.Id, answer => answer.QuestionId, (question, answer) => new { question, answer })
                .Where(it => it.question.quiz.Id == id)
                .Select(it => new QuizResponseModel
                {
                    Title = it.question.quiz.Title,
                    Id = it.question.quiz.Id,
                    Questions = it.question.quiz.Questions.Select(q => new QuestionItem
                    {
                        Id = q.Id,
                        Text = q.Text,
                        Answers = it.question.quiz.Questions.Where(i => i.Id == q.Id).Select(a => new AnswerItem
                        {
                            Id = a.Id,
                            Text = a.Text,
                        })
                    })
                })
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var item = await Data.Quizes.FindAsync(id);

            if (item == null)
            {
                return false;
            }

            Data.Quizes.Remove(item);

            await Data.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<QuizResponseModel>> GetAll()
            => await mapper
                .ProjectTo<QuizResponseModel>(Data.Quizes).ToListAsync();
    }
}