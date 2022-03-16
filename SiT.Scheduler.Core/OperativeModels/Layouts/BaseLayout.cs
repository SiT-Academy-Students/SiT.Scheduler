namespace SiT.Scheduler.Core.OperativeModels.Layouts;

using System;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public abstract record BaseLayout(Guid Id) : ILayout;