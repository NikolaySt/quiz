using System.Collections.Generic;
using QuizGame.Common.Services;
using QuizGame.Service.Data.Models;

namespace QuizGame.Service.Data;

public class QuizDataSeeder : IDataSeeder
{
    private readonly QuizDbContext _db;

    public QuizDataSeeder(QuizDbContext db) => _db = db;

    public void SeedData()
    {
        _db.Answer.RemoveRange(_db.Answer);
        _db.Question.RemoveRange(_db.Question);
        _db.Quiz.RemoveRange(_db.Quiz);

        _db.Quiz.Add(new Quiz()
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
        _db.Quiz.Add(new Quiz()
        {
            Title = "My second quiz",
            Questions = new List<Question>()
        });
        _db.SaveChanges();
    }
}