using AutoMapper;

namespace QuizGame.Common.Models
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile mapper) => mapper.CreateMap(typeof(T), GetType());
    }
}