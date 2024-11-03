using DotNetHtmxTypescriptTemplate.Models;

namespace DotNetHtmxTypescriptTemplate.Repository.Services
{
    public interface IDatabaseService
    {
        Task<List<MovieModel>> GetMovies(int pageSize, int startFrom);
    }
}