namespace QuizGame.Service.Data.Models;

public class Answer
{
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public string Text { get; set; }

    public Question Question { get; set; }
}