using AutoMapper;
using QuizGame.Service.Data.Models;

namespace QuizGame.Service.Model.Answers;

public class AnswerUpdateModel : IMapFrom<Answer>
{
    public string Text { get; set; }

    public void Mapping(Profile profile)
        => profile
            .CreateMap<AnswerUpdateModel, Answer>();
}