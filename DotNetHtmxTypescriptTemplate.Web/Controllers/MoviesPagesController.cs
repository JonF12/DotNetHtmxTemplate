using DotNetHtmxTypescriptTemplate.Repository.Data;
using DotNetHtmxTypescriptTemplate.Models;
using DotNetHtmxTypescriptTemplate.Repository.Services;
using DotNetHtmxTypescriptTemplate.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DotNetHtmxTypescriptTemplate.Controllers
{
    public class MoviesPagesController : Controller
    {   
        private readonly AppDbContext appDbContext;
        private readonly IDatabaseService databaseService;

        public MoviesPagesController(AppDbContext appDbContext, IDatabaseService databaseService)
        {
            this.appDbContext = appDbContext;
            this.databaseService = databaseService;
        }

        [Route("/")]
        [HtmxRequest("Pages/Index.cshtml")]
        public IActionResult Index()
        {
            return View();
        }
        
        [Route("/addmovies")]
        [HtmxRequest("Pages/Movies/AddMovies.cshtml")]
        public IActionResult AddMovies()
        {
            return View();
        }
        
        [Route("/ViewMovies")]
        [HtmxRequest("Pages/Movies/ViewMovies.cshtml")]
        public IActionResult ViewMovies()
        {

            return View();
        }

        //Extracted both methods to a common service with DI.
        //Controllers should be kept free of any logic whatsoever anyway.
        [HttpPost]
        [Route("movies/paginated")]
        [IgnoreAntiforgeryToken] //probably not needed, was testing something
        public async Task<PartialViewResult> GetMoviesPaginated([FromBody] GetMoviesPaginatedRequest request)
        {
            var movies = await databaseService.GetMovies(request.PageSize, request.StartFrom);
            return PartialView("Pages/Movies/MovieCard.cshtml", movies);
        }
    }
}
