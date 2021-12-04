namespace SiT.Scheduler.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;

public static class CollectionExtensions
{
    public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> collection)
        => collection ?? Enumerable.Empty<T>();

    public static IEnumerable<T> IgnoreNullValues<T>(this IEnumerable<T> collection)
        where T : class
    {
        if (collection is null)
            throw new ArgumentNullException(nameof(collection));

        return collection.Where(el => el is not null);
    }

    public static IEnumerable<T> IgnoreDefaultValues<T>(this IEnumerable<T> collection)
        where T : struct, IEquatable<T>
    {
        if (collection is null)
            throw new ArgumentNullException(nameof(collection));

        return collection.Where(el => !el.Equals(default));
    }
}
