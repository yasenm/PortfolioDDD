namespace Portfolio.Common.Application.Mapping
{
    using AutoMapper;

    public interface IMapTo<T>
    {
        void Map(Profile mapper) => mapper.CreateMap(this.GetType(), typeof(T));
    }
}
