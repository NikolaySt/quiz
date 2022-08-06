using AutoMapper;
using QuizGame.Common.Models;
using QuizGame.Service.Data.Models;

namespace QuizGame.Service.Model.Quizzes;

public class QuizCreateModel : IMapFrom<Quiz>
{
    public QuizCreateModel(string title)
    {
        Title = title;
    }

    public QuizCreateModel()
    {
    }

    public string Title { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<QuizCreateModel, Quiz>();
        profile.CreateMap<Quiz, QuizCreateModel>();
    }
}