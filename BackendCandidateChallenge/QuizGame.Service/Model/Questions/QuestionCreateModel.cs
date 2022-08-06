using AutoMapper;
using QuizGame.Common.Models;
using QuizGame.Service.Data.Models;

namespace QuizGame.Service.Model.Questions;

public class QuestionCreateModel : IMapFrom<Question>
{
    public QuestionCreateModel(string text)
    {
        Text = text;
    }

    public QuestionCreateModel()
    {

    }

    public string Text { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<QuestionCreateModel, Question>();
        profile.CreateMap<Question, QuestionCreateModel>();
    }
}