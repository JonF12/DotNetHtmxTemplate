
using DotNetHtmxTemplate.Repository.Data;
using DotNetHtmxTemplate.Models;
using DotNetHtmxTemplate.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetHtmxTemplate.ApiControllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDatabaseService databaseService;

        public MoviesController(AppDbContext AppDbContext, IDatabaseService databaseService)
        {
            _appDbContext = AppDbContext;
            this.databaseService = databaseService;
        }

        [HttpPost]
        public async Task<ActionResult<MovieModel>> Add(List<MovieModel> movie)
        {
            await _appDbContext.Movies.AddRangeAsync(movie);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<MovieModel>> Update(int id, MovieModel movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }
            try
            {
                _appDbContext.Entry(movie).State = EntityState.Modified;
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return Ok();
        }

        private bool MovieExists(long id)
        {
            return _appDbContext.Movies?.Any(movie => movie.Id == id) ?? false;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieModel>> Delete(int id)
        {
            var movie = await _appDbContext.Movies.FindAsync(id);
            if (movie == null)
            {
                return Ok();
            }
            _appDbContext.Movies.Remove(movie);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("/paginated")]
        public async Task<ActionResult<List<MovieModel>>> GetMoviesPaginated([FromBody] PaginatedRequest request)
        {
            var movies = await databaseService.GetMovies(request.PageSize, request.StartFrom);
            return Ok(movies.hits);
        }
    }
}


