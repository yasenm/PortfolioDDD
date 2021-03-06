namespace Portfolio.Common.Domain.Models
{
    using System.Collections.Generic;

    public interface IEntity
    {
        IReadOnlyCollection<IDomainEvent> Events { get; }

        void ClearEvents();
    }
}
