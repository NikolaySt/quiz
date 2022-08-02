using AutoMapper;
using QuizService.Data.Models;

namespace QuizService.Model.Questions
{
    public class QuestionResponseModel : IMapFrom<Question>
    {
        public int Id { get; private set; }
        public int QuizId { get; private set; }
        public string Text { get; private set; } = default!;
        public int CorrectAnswerId { get; set; }

        public void Mapping(Profile profile)
            => profile
                .CreateMap<Question, QuestionResponseModel>();
    }
}