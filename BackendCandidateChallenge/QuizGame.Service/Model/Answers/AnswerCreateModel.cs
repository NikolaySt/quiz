using AutoMapper;
using QuizGame.Common.Models;
using QuizGame.Service.Data.Models;

namespace QuizGame.Service.Model.Answers;

public class AnswerCreateModel : IMapFrom<Answer>
{
    public AnswerCreateModel(string text)
    {
        Text = text;
    }

    public AnswerCreateModel()
    {

    }

    public string Text { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AnswerCreateModel, Answer>();
        profile.CreateMap<Answer, AnswerCreateModel>();
    }
}