namespace SiT.Scheduler.Core.OperativeModels.Layouts;
using System;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public record SongLayout(Guid Id, string Name, string Author) : ISongLayout;