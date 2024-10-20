
using DotNetHtmxTypescriptTemplate.Data;
using DotNetHtmxTypescriptTemplate.Movies;
using DotNetHtmxTypescriptTemplate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;


namespace DotNetHtmxTypescriptTemplate.ApiControllers
{
    //split APIControllers for json response and MVC Controllers for html
    //this makes more sense because now you also have an API that's capable of producing JSON without HTML formatting, if you wish to use this as both an API and a web UI.
    //useful if youre making a tool that needs a UI for less technical people but just an API for others
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


        //Extracted both methods to a common service with DI.
        //Controllers should be kept free of any logic whatsoever anyway.
        [HttpPost]
        [Route("/paginated")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<List<MovieModel>>> GetMoviesPaginated([FromBody] GetMoviesPaginatedRequest request)
        {
            var movies = await databaseService.GetMovies(request.PageSize, request.StartFrom);
            return Ok(movies);
        }
    }
}


