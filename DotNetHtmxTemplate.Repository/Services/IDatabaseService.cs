using DotNetHtmxTemplate.Models;

namespace DotNetHtmxTemplate.Repository.Services
{
    public interface IDatabaseService
    {
        Task<(List<MovieModel> hits, int totalHits)> GetMovies(int pageSize, int startFrom);
    }
}