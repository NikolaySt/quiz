using System.Collections.Generic;
using QuizGame.Common.Services;
using QuizGame.Service.Data.Models;

namespace QuizGame.Service.Data
{
    public class QuizDataSeeder : IDataSeeder
    {
        private readonly QuizDbContext db;

        public QuizDataSeeder(QuizDbContext db) => this.db = db;

        public void SeedData()
        {
            this.db.Answer.RemoveRange(db.Answer);
            this.db.Question.RemoveRange(db.Question);
            this.db.Quiz.RemoveRange(db.Quiz);

            this.db.Quiz.Add(new Quiz()
            {
                Title = "My first quiz",
                Questions = new List<Question>()
                {
                    new Question()
                    {
                        Text = "My first question",
                        CorrectAnswerId = 1,
                        Answers = new List<Answer>()
                        {
                            new Answer(){Text = "My first answer to first q"},
                            new Answer(){Text = "My second answer to first q"}
                        }
                    },
                    new Question()
                    {
                        Text = "My second question",
                        CorrectAnswerId = 5,
                        Answers = new List<Answer>()
                        {
                            new Answer(){Text = "My first answer to second q"},
                            new Answer(){Text = "My second answer to second q"},
                            new Answer(){Text = "My third answer to second q"}
                        }
                    }
                }
            });
            this.db.Quiz.Add(new Quiz()
            {
                Title = "My second quiz",
                Questions = new List<Question>()
            });
            this.db.SaveChanges();
        }
    }
}