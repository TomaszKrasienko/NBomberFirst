using Microsoft.EntityFrameworkCore;
using NBomberFirst.Entities;

namespace NBomberFirst.Persistance.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MoviesDbContext _moviesDbContext;
    
    public MovieRepository(MoviesDbContext moviesDbContext)
    {
        _moviesDbContext = moviesDbContext;
    }
    
    public async Task<Movie?> GetByIdAsync(Guid id)
    {
        return await _moviesDbContext
            .Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id) ?? null;
    }

    public async Task AddAsync(Movie movie)
    {
        await _moviesDbContext
            .Movies
            .AddAsync(movie);
        await _moviesDbContext.SaveChangesAsync();
    }
}