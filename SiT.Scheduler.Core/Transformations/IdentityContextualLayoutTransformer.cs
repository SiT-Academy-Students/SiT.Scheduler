namespace SiT.Scheduler.Core.Transformations;

using System;
using System.Linq.Expressions;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Core.OperativeModels.Layouts;
using SiT.Scheduler.Data.Models;

public class IdentityContextualLayoutTransformer : IDataTransformer<Identity, IIdentityContextualLayout>
{
    public Expression<Func<Identity, IIdentityContextualLayout>> Projection => i => new IdentityContextualLayout(i.Id);
}