using System.Collections.Generic;

namespace QuizGame.Service.Data.Models;

public class Question
{
    public int Id { get; set; }

    public int QuizId { get; set; }

    public string Text { get; set; }

    public int CorrectAnswerId { get; set; }

    public ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public Quiz Quiz { get; init; }
}