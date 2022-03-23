namespace SiT.Scheduler.Core.OperativeModels.Layouts;

using System;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public record IdentityContextualLayout(Guid Id) : BaseLayout(Id), IIdentityContextualLayout;