namespace SiT.Scheduler.API.Contracts.Factories;

using SiT.Scheduler.API.ViewModels.Performer;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public interface IPerformerFactory
{
    PerformerViewModel ToViewModel(IPerformerMinifiedLayout layout);
}
