namespace Portfolio.Common.Application.Mapping
{
    using AutoMapper;

    public interface IMapFrom<T>
    {
        void Map(Profile mapper) => mapper.CreateMap(typeof(T), this.GetType());
    }
}
