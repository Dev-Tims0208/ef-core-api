using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    public class GenresController : Controller
    {
        private readonly IRepository repository;
        public GenresController(IRepository repository)
        {
            this.repository = repository;
        }
    }
}
