namespace SiT.Scheduler.Core.OperativeModels.Layouts;

using System;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public record IdentityAuthenticationLayout(Guid Id) : BaseLayout(Id), IIdentityAuthenticationLayout;