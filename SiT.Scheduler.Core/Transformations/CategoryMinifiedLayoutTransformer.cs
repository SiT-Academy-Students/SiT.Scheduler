namespace SiT.Scheduler.Core.Transformations;

using System;
using System.Linq.Expressions;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Core.OperativeModels.Layouts;
using SiT.Scheduler.Data.Models;

public class CategoryMinifiedLayoutTransformer : IDataTransformer<Category, ICategoryMinifiedLayout>
{
    public Expression<Func<Category, ICategoryMinifiedLayout>> Projection => GetProjection();

    private static Expression<Func<Category, ICategoryMinifiedLayout>> GetProjection() => c => new CategoryMinifiedLayout(c.Id, c.Name, c.Description);
}
