namespace SiT.Scheduler.Organization.Contracts;

public interface IExternalIdentity
{
    Guid Id { get; }
    string DisplayName { get; }
}