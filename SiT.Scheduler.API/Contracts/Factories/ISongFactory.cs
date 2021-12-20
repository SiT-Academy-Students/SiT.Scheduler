namespace SiT.Scheduler.API.Contracts.Factories;

using SiT.Scheduler.API.ViewModels.Song;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public interface ISongFactory
{
    SongViewModel ToViewModel(ISongLayout layout);
}
