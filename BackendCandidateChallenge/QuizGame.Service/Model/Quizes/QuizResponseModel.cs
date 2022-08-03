using System.Collections.Generic;
using AutoMapper;
using QuizGame.Service.Data.Models;

namespace QuizGame.Service.Model.Quizes;

public class QuizResponseModel : IMapFrom<Quiz>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public IEnumerable<QuestionItem> Questions { get; set; }
    public IDictionary<string, string> Links { get; set; }

    public void Mapping(Profile profile)
        => profile
            .CreateMap<Quiz, QuizResponseModel>();
}

public class AnswerItem : IMapFrom<Answer>
{
    public int Id { get; set; }
    public string Text { get; set; }

    public void Mapping(Profile profile)
        => profile
            .CreateMap<Answer, AnswerItem>();
}

public class QuestionItem : IMapFrom<Question>
{
    public int Id { get; set; }
    public string Text { get; set; }
    public IEnumerable<AnswerItem> Answers { get; set; }
    public int CorrectAnswerId { get; set; }

    public void Mapping(Profile profile)
        => profile
            .CreateMap<Question, QuestionItem>();
}