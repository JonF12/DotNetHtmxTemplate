using DotNetHtmxTemplate.Models;
using DotNetHtmxTemplate.Repository.Data;
using DotNetHtmxTemplate.Repository.Services;
using DotNetHtmxTemplate.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DotNetHtmxTemplate.Controllers
{
    public class MoviesPagesController : BaseController
    {   
        private readonly AppDbContext appDbContext;
        private readonly IDatabaseService databaseService;

        public MoviesPagesController(AppDbContext appDbContext, IDatabaseService databaseService)
        {
            this.appDbContext = appDbContext;
            this.databaseService = databaseService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            return GetPage("Pages/Index.cshtml");
        }
        
        [Route("/addmovies")]
        public IActionResult AddMovies()
        {
            return GetPage("Pages/Movies/AddMovies.cshtml");
        }
        
        [Route("/viewmovies")]
        public async Task<IActionResult> ViewMovies(int startFrom = 0, int pageSize = 3)
        {
            var response = await databaseService.GetMovies(pageSize, startFrom);
            var responseModel = new PaginatedResponse<MovieModel>
            {
                Hits = response.hits,
                StartFrom = startFrom,
                PageSize = pageSize,
                TotalHits = response.totalHits
            };
            return GetPage("Pages/Movies/ViewMovies.cshtml", responseModel);
        }

        [HttpPost]
        [Route("movies/paginated")]
        [IgnoreAntiforgeryToken]
        public async Task<PartialViewResult> GetMoviesPaginated([FromBody] PaginatedRequest request)
        {
            var response = await databaseService.GetMovies(request.PageSize, request.StartFrom);
            var model = new PaginatedResponse<MovieModel>
            {
                PageSize = request.PageSize,
                StartFrom = request.StartFrom,
                TotalHits = response.totalHits,
                Hits = response.hits
            };

            return PartialView("Pages/Movies/MovieCard.cshtml", model);
        }
    }
}
