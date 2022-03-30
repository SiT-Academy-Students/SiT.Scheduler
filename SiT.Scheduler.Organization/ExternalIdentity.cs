namespace SiT.Scheduler.Organization;

using SiT.Scheduler.Organization.Contracts;

public record ExternalIdentity(Guid Id, string DisplayName, string PrincipalName) : IExternalIdentity;