using System.Collections.Generic;

namespace QuizService.Data.Models;

public class Quiz
{
    public int Id { get; set; }
    public string Title { get; set; }

    public ICollection<Question> Questions { get; set; } = new List<Question>();
}