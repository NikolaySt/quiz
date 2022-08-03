using AutoMapper;
using QuizGame.Service.Data.Models;

namespace QuizGame.Service.Model.Questions;

public class QuestionCreateModel : IMapFrom<Question>
{
    public QuestionCreateModel(string text)
    {
        Text = text;
    }

    public string Text { get; set; }

    public void Mapping(Profile profile)
        => profile
            .CreateMap<QuestionCreateModel, Question>();
}