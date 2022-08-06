using System.Collections.Generic;
using AutoMapper;
using QuizGame.Common.Models;
using QuizGame.Service.Data.Models;

namespace QuizGame.Service.Model.Quizzes;

public class QuizResponseModel : IMapFrom<Quiz>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public IEnumerable<QuestionItem> Questions { get; set; }
    public IDictionary<string, string> Links { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Quiz, QuizResponseModel>();
        profile.CreateMap<QuizResponseModel, Quiz>();
    }
}

public class AnswerItem : IMapFrom<Answer>
{
    public int Id { get; set; }
    public string Text { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Answer, AnswerItem>();
        profile.CreateMap<AnswerItem, Answer>();
    }
}

public class QuestionItem : IMapFrom<Question>
{
    public int Id { get; set; }
    public string Text { get; set; }
    public IEnumerable<AnswerItem> Answers { get; set; }
    public int CorrectAnswerId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Question, QuestionItem>();
        profile.CreateMap<QuestionItem, Question>();
    }
}