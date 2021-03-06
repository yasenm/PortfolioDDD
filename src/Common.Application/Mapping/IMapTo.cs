namespace Portfolio.Common.Application.Mapping;

using AutoMapper;

public interface IMapTo<T>
{
    void Mapping(Profile mapper) => mapper.CreateMap(this.GetType(), typeof(T));
}
