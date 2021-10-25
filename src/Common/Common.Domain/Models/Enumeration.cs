namespace Portfolio.Common.Domain.Models
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class Enumeration
    {
        private static readonly ConcurrentDictionary<Type, IEnumerable<object>> EnumCache = new ();

        public int Value { get; }

        public string Name { get; }

        protected Enumeration(int value, string name)
        {
            this.Value = value;
            this.Name = name;
        }

        public override string ToString() => this.Name;

        public override bool Equals(object? other)
        {
            if (!(other is Enumeration otherEnumeration))
            {
                return false;
            }

            var thisType = this.GetType();
            var otherType = other.GetType();

            if (thisType != otherType)
            {
                return false;
            }

            return this.Value == otherEnumeration.Value;
        }

        public override int GetHashCode() => (this.GetType().ToString() + this.Value.GetHashCode()).GetHashCode();

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var type = typeof(T);

            var values = EnumCache.GetOrAdd(type, _ => type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null)!));

            return values.Cast<T>();
        }

        public static T FromValue<T>(int value) where T : Enumeration
            => Parse<T, int>(value, "value", item => item.Value == value);

        public static T FromName<T>(string name) where T : Enumeration
            => Parse<T, string>(name, "name", item => item.Name == name);

        public static string NameFromValue<T>(int value) where T : Enumeration
            => FromValue<T>(value).Name;

        public static bool HasValue<T>(int value) where T : Enumeration
        {
            try
            {
                FromValue<T>(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static T Parse<T, TValue>(TValue value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");
            }

            return matchingItem;
        }
    }
}
