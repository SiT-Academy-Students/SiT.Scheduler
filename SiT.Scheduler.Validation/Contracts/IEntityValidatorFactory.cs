namespace SiT.Scheduler.Validation.Contracts;

public interface IEntityValidatorFactory
{
    IEntityValidator<TEntity> BuildValidator<TEntity>() where TEntity : class;
}
