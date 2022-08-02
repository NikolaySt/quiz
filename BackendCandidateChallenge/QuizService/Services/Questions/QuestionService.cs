using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizService.Data;
using QuizService.Data.Models;
using QuizService.Model.Questions;

namespace QuizService.Services.Questions
{
    public class QuestionService : DataService<Question>, IQuestionService
    {
        private readonly IMapper mapper;

        public QuestionService(QuizDbContext db, IMapper mapper)
            : base(db)
            => this.mapper = mapper;

        public async Task<Question> Find(int id)
            => await Data.Questions.FindAsync(id);

        public async Task<bool> Delete(int id)
        {
            var item = await Data.Questions.FindAsync(id);

            if (item == null)
            {
                return false;
            }

            Data.Questions.Remove(item);

            await Data.SaveChangesAsync();

            return true;
        }
    }
}