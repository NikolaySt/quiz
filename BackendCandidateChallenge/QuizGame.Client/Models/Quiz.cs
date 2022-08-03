namespace QuizGame.Client.Models;

public struct Quiz
{
    public int Id;
    public string Title;
    public static Quiz NotFound => default;
}