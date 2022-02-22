namespace SiT.Scheduler.API.Contracts.Factories;

using SiT.Scheduler.API.ViewModels.Category;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public interface ICategoryFactory
{
    CategoryViewModel ToViewModel(ICategoryMinifiedLayout layout);
}
