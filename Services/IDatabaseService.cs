using DotNetHtmxTypescriptTemplate.Movies;

namespace DotNetHtmxTypescriptTemplate.Services
{
    public interface IDatabaseService
    {
        Task<List<MovieModel>> GetMovies(int pageSize, int startFrom);
    }
}