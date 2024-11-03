using DotNetHtmxTypescriptTemplate.Models;

namespace DotNetHtmxTypescriptTemplate.Repository.Services
{
    public interface IDatabaseService
    {
        Task<(List<MovieModel> hits, int totalHits)> GetMovies(int pageSize, int startFrom);
    }
}