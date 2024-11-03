using DotNetHtmxTypescriptTemplate.Repository.Data;
using DotNetHtmxTypescriptTemplate.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetHtmxTypescriptTemplate.Repository.Services
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
