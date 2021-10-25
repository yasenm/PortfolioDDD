namespace Portfolio.Common.Domain.Models
{
    using System.Linq;
    using System.Reflection;

    public abstract class ValueObject
    {
        private readonly BindingFlags privateBindingFlags =
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        public override bool Equals(object? other)
        {
            if (other is null)
            {
                return false;
            }

            if (other is not ValueObject)
            {
                return false;
            }

            var thisType = this.GetType();
            var otherType = other.GetType();

            if (thisType != otherType)
            {
                return false;
            }

            var props = thisType.GetFields(privateBindingFlags);
            foreach (var prop in props)
            {
                var thisVal = prop.GetValue(this);
                var otherVal = prop.GetValue(other);

                if (thisVal is null)
                {
                    if (otherVal is not null)
                    {
                        return false;
                    }
                }
                else if (!thisVal.Equals(otherVal))
                {
                    return false;
                }
            }

            return true;
        }
        public static bool operator ==(ValueObject first, ValueObject second) => first.Equals(second);
        public static bool operator !=(ValueObject first, ValueObject second) => !first.Equals(second);

        public override int GetHashCode()
        {
            var hashResult = this.GetType()
                .GetFields(privateBindingFlags)
                .Select(prop => prop.GetValue(this))
                .Where(val => val is not null)
                .Aggregate(25, (current, value) => current * 16 + value!.GetHashCode());

            return hashResult;
        }
    }
}
