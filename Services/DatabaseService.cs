using DotNetHtmxTypescriptTemplate.Data;
using DotNetHtmxTypescriptTemplate.Movies;
using Microsoft.EntityFrameworkCore;

namespace DotNetHtmxTypescriptTemplate.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly AppDbContext _appDbContext;

        public DatabaseService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<MovieModel>> GetMovies(int pageSize, int startFrom) 
        {
            var movies = await _appDbContext.Movies.AsQueryable()
                .Skip(startFrom)
                .Take(pageSize)
                .ToListAsync();
            return movies;
        }
    }
}
