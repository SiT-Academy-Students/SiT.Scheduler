namespace SiT.Scheduler.API.ViewModels.Performer;

using System;
using JetBrains.Annotations;

public class PerformerViewModel
{
    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public Guid Id { get; set; }

    [UsedImplicitly(ImplicitUseKindFlags.Access)]
    public string Name { get; set; }
}
