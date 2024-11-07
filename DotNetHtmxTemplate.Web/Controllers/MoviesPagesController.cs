using DotNetHtmxTemplate.Models;
using DotNetHtmxTemplate.Repository.Data;
using DotNetHtmxTemplate.Repository.Services;
using DotNetHtmxTemplate.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetHtmxTemplate.Controllers
{
    [Authorize]
    [DisableRequestSizeLimit]
    [RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue)]
    public class MoviesPagesController : BaseController
    {   
        private readonly AppDbContext appDbContext;
        private readonly IDatabaseService databaseService;
        private readonly IWebHostEnvironment environment;

        public MoviesPagesController(AppDbContext appDbContext, IDatabaseService databaseService, IWebHostEnvironment environment)
        {
            this.appDbContext = appDbContext;
            this.databaseService = databaseService;
            this.environment = environment;
        }

        [Route("/")]
        [Authorize]
        public IActionResult Index()
        {
            return GetPage("Pages/Index.cshtml");
        }
        
        [Route("/addmovies")]
        [Authorize]
        public IActionResult AddMovies()
        {
            return GetPage("Pages/Movies/AddMovies.cshtml");
        }
        
        [Route("/viewmovies")]
        [Authorize]
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

        [Route("/images")]
        [Authorize]
        public async Task<IActionResult> ViewImages()
        {
            return GetPage("Pages/Movies/Images.cshtml");
        }

        [Route("/upload")]
        [RequestFormLimits(MultipartBodyLengthLimit = 52428800)]
        [RequestSizeLimit(52428800)]
        [Authorize]
        public async Task<IActionResult> UploadImage()
        {
            try
            {
                var uploadedFiles = new List<string>();
                var files = Request.Form.Files;

                if (files == null || files.Count == 0)
                {
                    return PartialView("Pages/Movies/FileUploadStatus.cshtml", uploadedFiles);
                }

                string uploadsFolder = Path.Combine(environment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var safeFileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid()}_{fileName}";
                        var filePath = Path.Combine(uploadsFolder, safeFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        uploadedFiles.Add(fileName);
                    }
                }

                Response.Headers.Add("HX-Trigger", "fileUploadComplete");
                return PartialView("Pages/Movies/FileUploadStatus.cshtml", uploadedFiles);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return PartialView("Pages/Movies/FileUploadStatus.cshtml", new List<string> { "Error uploading files" });
            }
        }

        [HttpPost]
        [Route("movies/paginated")]
        [Authorize]
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
