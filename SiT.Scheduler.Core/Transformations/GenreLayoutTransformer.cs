namespace SiT.Scheduler.Core.Transformations;

using System;
using System.Linq.Expressions;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Core.OperativeModels.Layouts;
using SiT.Scheduler.Data.Models;

public class GenreLayoutTransformer : IDataTransformer<Genre, IGenreLayout>
{
    public Expression<Func<Genre, IGenreLayout>> Projection => GetProjection();

    private static Expression<Func<Genre, IGenreLayout>> GetProjection() => g => new GenreLayout(g.Id, g.Name, g.Description);
}
