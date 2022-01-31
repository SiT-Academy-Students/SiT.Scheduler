namespace SiT.Scheduler.API.Factories;

using SiT.Scheduler.API.Contracts.Factories;
using SiT.Scheduler.API.ViewModels.Performer;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public class PerformerFactory : IPerformerFactory
{
    public PerformerViewModel ToViewModel(IPerformerMinifiedLayout layout)
    {
        if (layout is null) return null;

        var viewModel = new PerformerViewModel { Id = layout.Id, Name = layout.Name };
        return viewModel;
    }
}
