using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NBomberFirst.Entities;

namespace NBomberFirst.Persistance.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MoviesDbContext _moviesDbContext;
    private readonly IMemoryCache _cache;

    public MovieRepository(MoviesDbContext moviesDbContext, IMemoryCache cache)
    {
        _moviesDbContext = moviesDbContext;
        _cache = cache;
    }
    
    public async Task<Movie?> GetByIdAsync(Guid id)
    {
        var cacheKey = $"movie: {id}";
        var movie = _cache.Get<Movie>(cacheKey);
        if (movie is null)
        {
            movie = await _moviesDbContext
                .Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id) ?? null;
            _cache.Set(cacheKey, movie);
        }
        return movie;
    }

    public async Task AddAsync(Movie movie)
    {
        await _moviesDbContext
            .Movies
            .AddAsync(movie);
        await _moviesDbContext.SaveChangesAsync();
    }
}