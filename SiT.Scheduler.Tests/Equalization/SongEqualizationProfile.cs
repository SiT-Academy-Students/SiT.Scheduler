namespace SiT.Scheduler.Tests.Equalization;
using SiT.Scheduler.Data.Models;
using TryAtSoftware.Equalizer.Core.Profiles.Complex;

public class SongEqualizationProfile : ComplexEqualizationProfile<Song, Song>
{
    public SongEqualizationProfile()
    {
        this.Equalize(s => s.Id, s => s.Id);
        this.Equalize(s => s.Name, s => s.Name);
        this.Equalize(s => s.Author, s => s.Author);
    }
}
