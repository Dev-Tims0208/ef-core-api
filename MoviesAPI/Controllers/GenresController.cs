using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Filters;
using MoviesAPI.Helpers;

namespace MoviesAPI.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ILogger<GenresController> logger;
        private readonly ApplicationDbContext ctx;
        private readonly IMapper mapper;

        public GenresController(ILogger<GenresController> logger,
            ApplicationDbContext ctx,
            IMapper mapper)
        {
            this.logger = logger;
            this.ctx = ctx;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = ctx.Genres.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            var genres = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
            return mapper.Map<List<GenreDTO>>(genres);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<GenreDTO>> Get(int Id)
        {
            var genre = await ctx.Genres.FirstOrDefaultAsync(x => x.Id == Id);

            if (genre == null)
            {
                return NotFound();
            }

            return mapper.Map<GenreDTO>(genre);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenreCreationDTO genreCreationDTO)
        {
            var genre = mapper.Map<Genre>(genreCreationDTO);
            ctx.Add(genre);
            await ctx.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] GenreCreationDTO genreCreationDTO)
        {
            var genre = await ctx.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            genre = mapper.Map(genreCreationDTO, genre);
            await ctx.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public void Delete()
        {

            throw new NotImplementedException();
        }



    }
}
