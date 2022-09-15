using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Entities;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly IRepository repository;
        public GenresController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<List<Genre>> Get()
        {
            return repository.GetAllGenres();
        }

        [HttpGet("{Id}")]
        public ActionResult<Genre> Get(int Id)
        {
            var genre = repository.GetGenreById(Id);

            if (genre == null)
            {
                //return NotFound();
            }

            return genre;
        }

        [HttpPost]
        public void Post()
        {

        }

        [HttpPut]
        public void Put()
        {

        }

        [HttpDelete]
        public void Delete()
        {

        }



    }
}
