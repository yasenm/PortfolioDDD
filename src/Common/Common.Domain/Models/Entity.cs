namespace Portfolio.Common.Domain.Models
{
    using Portfolio.Common.Domain;
    using System.Collections.Generic;

    public abstract class Entity<TId> : IEntity
        where TId : struct
    {
        private readonly List<IDomainEvent> events;

        protected Entity() { this.events = new List<IDomainEvent>(); }

        public TId Id { get; private set; } = default;

        public void ClearEvents() => this.events.Clear();

        public IReadOnlyCollection<IDomainEvent> Events => this.events.AsReadOnly();

        public void RaiseEvent(IDomainEvent @event) => this.events.Add(@event);

        public override bool Equals(object? obj)
        {
            if (obj is not Entity<TId> other)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.GetType() != other.GetType())
            {
                return false;
            }

            if (this.Id.Equals(default) || other.Id.Equals(default))
            {
                return false;
            }

            return this.Id.Equals(other.Id);
        }

        public static bool operator ==(Entity<TId> first, Entity<TId> second)
            => (first, second) switch
            {
                (null, null) => true,
                (null, not null) => false,
                (not null, null) => false,
                (_, _) => first.Equals(second)
            };

        public static bool operator !=(Entity<TId> first, Entity<TId> second) => !(first == second);

        public override int GetHashCode() => (this.GetType().ToString() + this.Id).GetHashCode();
    }
}