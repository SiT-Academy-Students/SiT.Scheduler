namespace SiT.Scheduler.Core.Transformations;
using System.Collections.Generic;
using System.Linq;
using SiT.Scheduler.Core.Contracts.Transformations;

internal static class TransformationExtensions
{
    public static IEnumerable<TLayout> Project<TBase, TLayout>(this IEnumerable<TBase> collection, IDataTransformer<TBase, TLayout> dataTransformer)
    {
        if (dataTransformer is null) return Enumerable.Empty<TLayout>();
        return collection.AsQueryable().Select(dataTransformer.Projection).ToList();
    }

    public static TLayout Project<TBase, TLayout>(this TBase data, IDataTransformer<TBase, TLayout> dataTransformer)
    {
        if (dataTransformer is null) return default;
        var compiled = dataTransformer.Projection.Compile();
        return compiled(data);
    }
}
