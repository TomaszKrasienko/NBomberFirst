using NBomberFirst.Entities;

namespace NBomberFirst.Persistance.Repositories;

public interface IMovieRepository
{
    Task<Movie?> GetById(Guid id);
    Task Add(Movie movie);
}