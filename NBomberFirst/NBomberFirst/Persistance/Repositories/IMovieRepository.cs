using NBomberFirst.Entities;

namespace NBomberFirst.Persistance.Repositories;

public interface IMovieRepository
{
    Task<Movie?> GetByIdAsync(Guid id);
    Task AddAsync(Movie movie);
}