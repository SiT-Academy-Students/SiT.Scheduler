namespace SiT.Scheduler.Core.Transformations;
using System;
using System.Linq.Expressions;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Core.OperativeModels.Layouts;
using SiT.Scheduler.Data.Models;

public class PerformerMinifiedLayoutTransformer : IDataTransformer<Performer, IPerformerMinifiedLayout>
{
    public Expression<Func<Performer, IPerformerMinifiedLayout>> Projection => GetProjection();

    private static Expression<Func<Performer, IPerformerMinifiedLayout>> GetProjection() => p => new PerformerMinifiedLayout(p.Id, p.Name);
}
