using AutoMapper;

namespace QuizGame.Service.Model
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile mapper) => mapper.CreateMap(typeof(T), GetType());
    }
}