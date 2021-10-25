namespace Portfolio.Common.Domain.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types!.Where(t => t is not null)!;
            }
        }

        public static IEnumerable<Type> GetTypesByType<T>(this Assembly assembly) =>
            assembly
                .GetLoadableTypes()
                .Where(x => x == typeof(T))
                .ToList();

        public static IEnumerable<Type> GetTypesByType(this Assembly assembly, Type type) =>
            assembly
                .GetLoadableTypes()
                .Where(x => x == type)
                .ToList();

        public static IEnumerable<KeyValuePair<Type, Type>> GetTypesByGenericInterfaceType(this Assembly assembly, Type type) =>
               assembly
                    .GetLoadableTypes()
                    .Select(x =>
                    {
                        var genericInterfaceType = x.GetInterfaces()
                            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == type);

                        if (genericInterfaceType is not null)
                        {
                            return new Tuple<Type, Type>(genericInterfaceType!, x);
                        }

                        return null;
                    })
                    .Where(x => x is not null)
                    .Select(x => new KeyValuePair<Type, Type>(x!.Item1, x.Item2))
                    .ToList();
    }
}
