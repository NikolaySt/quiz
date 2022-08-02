using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizService.Data;
using QuizService.Data.Models;
using QuizService.Model.Answers;
using QuizService.Model.Questions;

namespace QuizService.Services.Answers
{
    public class AnswerService : DataService<Answer>, IAnswerService
    {
        private readonly IMapper mapper;

        public AnswerService(QuizDbContext db, IMapper mapper)
            : base(db)
            => this.mapper = mapper;

        public async Task<Answer> Find(int id)
            => await Data.Answers.FindAsync(id);

        public async Task<bool> Delete(int id)
        {
            var item = await Data.Answers.FindAsync(id);

            if (item == null)
            {
                return false;
            }

            Data.Answers.Remove(item);

            await Data.SaveChangesAsync();

            return true;
        }
    }
}