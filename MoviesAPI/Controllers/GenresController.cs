using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MoviesAPI.Entities;
using MoviesAPI.Filters;

namespace MoviesAPI.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ILogger<GenresController> logger;
        public GenresController(ILogger<GenresController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Genre>>> Get()
        {
            logger.LogInformation("Getting all the genres");
            return new List<Genre>() { new Genre() { Id = 1, Name = "Comedy" } };
        }

        [HttpGet("{Id:int}", Name = "getGenre")]
        public ActionResult<Genre> Get(int Id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public void Post()
        {

            throw new NotImplementedException();
        }

        [HttpPut]
        public void Put()
        {

            throw new NotImplementedException();
        }

        [HttpDelete]
        public void Delete()
        {

            throw new NotImplementedException();
        }



    }
}
