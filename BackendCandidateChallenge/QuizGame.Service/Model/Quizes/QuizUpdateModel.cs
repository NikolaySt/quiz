using AutoMapper;
using QuizGame.Service.Data.Models;

namespace QuizGame.Service.Model.Quizes;

public class QuizUpdateModel : IMapFrom<Quiz>
{
    public string Title { get; set; }

    public void Mapping(Profile profile)
        => profile
            .CreateMap<QuizUpdateModel, Quiz>();
}