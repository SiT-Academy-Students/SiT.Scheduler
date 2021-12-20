namespace SiT.Scheduler.Core.Contracts.Transformations;
public interface IDataTransformerFactory
{
    IDataTransformer<TEntity, TLayout> ConstructTransformer<TEntity, TLayout>();
}
