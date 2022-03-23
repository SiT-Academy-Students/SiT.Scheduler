namespace SiT.Scheduler.Core.OperativeModels.Layouts;

using System;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public record TenantContextualLayout(Guid Id) : BaseLayout(Id), ITenantContextualLayout;