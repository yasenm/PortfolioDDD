namespace Portfolio.Common.Domain
{
    public interface IFactory<out TEntity>
        where TEntity : IAggregateRoot
    {
        
    }
}