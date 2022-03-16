namespace SiT.Scheduler.Core.OperativeModels.Layouts;

using System;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public record CategoryMinifiedLayout(Guid Id, string Name, string Description) : BaseLayout(Id), ICategoryMinifiedLayout;