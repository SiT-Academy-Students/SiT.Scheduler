namespace SiT.Scheduler.Core.Transformations;

using System;
using System.Linq.Expressions;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Core.OperativeModels.Layouts;
using SiT.Scheduler.Data.Models;

public class SongLayoutTransformer : IDataTransformer<Song, ISongLayout>
{
    private readonly IDataTransformer<Genre, IGenreMinifiedLayout> _genreTransformer;
    private readonly IDataTransformer<Performer, IPerformerMinifiedLayout> _performerTransformer;
    private readonly IDataTransformer<Category, ICategoryMinifiedLayout> _categoryTransformer;

    public SongLayoutTransformer(IDataTransformer<Genre, IGenreMinifiedLayout> genreTransformer, IDataTransformer<Performer, IPerformerMinifiedLayout> performerTransformer, IDataTransformer<Category, ICategoryMinifiedLayout> categoryTransformer)
    {
        this._genreTransformer = genreTransformer ?? throw new ArgumentNullException(nameof(genreTransformer));
        this._performerTransformer = performerTransformer ?? throw new ArgumentNullException(nameof(performerTransformer));
        this._categoryTransformer = categoryTransformer ?? throw new ArgumentException(nameof(categoryTransformer));
    }

    public Expression<Func<Song, ISongLayout>> Projection => this.GetProjection();

    private Expression<Func<Song, ISongLayout>> GetProjection() => s => new SongLayout(s.Id, s.Name, s.Genres.Project(this._genreTransformer), s.Performers.Project(this._performerTransformer), s.Categories.Project(this._categoryTransformer));
}
