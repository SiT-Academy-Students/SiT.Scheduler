namespace SiT.Scheduler.Utilitites
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CollectionExtensions
    {
        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> collection)
            => collection ?? Enumerable.Empty<T>();

        public static IEnumerable<T> IgnoreNullValues<T>(this IEnumerable<T> collection)
            where T : class
            => collection.Where(el => el is not null);

        public static IEnumerable<T> IgnoreDefaultValues<T>(this IEnumerable<T> collection)
            where T : struct, IEquatable<T>
            => collection.Where(el => el.Equals(default) == false);
    }
}
