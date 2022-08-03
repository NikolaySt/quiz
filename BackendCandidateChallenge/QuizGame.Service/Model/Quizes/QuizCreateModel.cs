using AutoMapper;
using QuizGame.Service.Data.Models;

namespace QuizGame.Service.Model.Quizes;

public class QuizCreateModel : IMapFrom<Quiz>
{
    public QuizCreateModel(string title)
    {
        Title = title;
    }

    public string Title { get; set; }

    public void Mapping(Profile profile)
        => profile
            .CreateMap<QuizCreateModel, Quiz>();
}