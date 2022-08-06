using AutoMapper;
using QuizGame.Common.Models;
using QuizGame.Service.Data.Models;

namespace QuizGame.Service.Model.Quizzes;

public class QuizUpdateModel : IMapFrom<Quiz>
{
    public string Title { get; set; }

    public void Mapping(Profile profile)
        => profile
            .CreateMap<QuizUpdateModel, Quiz>();
}