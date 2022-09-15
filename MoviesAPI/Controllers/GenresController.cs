using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Entities;
using MoviesAPI.Filters;

namespace MoviesAPI.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ILogger<GenresController> logger;
        private readonly ApplicationDbContext ctx;
        public GenresController(ILogger<GenresController> logger, ApplicationDbContext ctx)
        {
            this.logger = logger;
            this.ctx = ctx;
        }

        [HttpGet]
        public async Task<ActionResult<List<Genre>>> Get()
        {
            logger.LogInformation("Getting all the genres");
            return await ctx.Genres.ToListAsync();
        }

        [HttpGet("{Id:int}", Name = "getGenre")]
        public ActionResult<Genre> Get(int Id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Genre genre)
        {
            ctx.Add(genre);
            await ctx.SaveChangesAsync();
            return NoContent();
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
