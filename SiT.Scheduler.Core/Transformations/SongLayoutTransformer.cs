namespace SiT.Scheduler.Core.Transformations;

using System;
using System.Linq.Expressions;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Core.OperativeModels.Layouts;
using SiT.Scheduler.Data.Models;

public class SongLayoutTransformer : IDataTransformer<Song, ISongLayout>
{
    public Expression<Func<Song, ISongLayout>> Projection => GetProjection();

    private static Expression<Func<Song, ISongLayout>> GetProjection() => s => new SongLayout(s.Id, s.Name, s.Author);
}
