using AutoMapper;
using QuizGame.Common.Models;
using QuizGame.Service.Data.Models;

namespace QuizGame.Service.Model.Questions;

public class QuestionUpdateModel : IMapFrom<Question>
{
    public string Text { get; set; }
    public int CorrectAnswerId { get; set; }

    public void Mapping(Profile profile)
        => profile
            .CreateMap<QuestionUpdateModel, Question>();
}