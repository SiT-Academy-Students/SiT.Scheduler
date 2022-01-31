namespace SiT.Scheduler.API.Contracts.Factories;

using SiT.Scheduler.API.ViewModels.Genre;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public interface IGenreFactory
{
    GenreViewModel ToViewModel(IGenreLayout layout);
}
