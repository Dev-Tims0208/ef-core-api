using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;

namespace MoviesAPI.Controllers
{
    [Route("api/movietheaters")]
    [ApiController]
    public class MovieTheatersController : ControllerBase
    {
        private readonly ApplicationDbContext ctx;
        private readonly IMapper mapper;

        public MovieTheatersController(ApplicationDbContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieTheaterDTO>>> Get()
        {
            var entities = await ctx.MovieTheaters.ToListAsync();
            return mapper.Map<List<MovieTheaterDTO>>(entities);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieTheaterDTO>> Get(int id)
        {
            var movieTheater = await ctx.MovieTheaters.FirstOrDefaultAsync(x => x.Id == id);
            if (movieTheater == null)
            {
                return NotFound();
            }

            return mapper.Map<MovieTheaterDTO>(movieTheater);
        }

        [HttpPost]
        public async Task<ActionResult> Post(MovieTheaterCreationDTO movieTheaterCreationDTO)
        {
            var movieTheater = mapper.Map<MovieTheater>(movieTheaterCreationDTO);
            ctx.Add(movieTheater);
            await ctx.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int")]
        public async Task<ActionResult> Put(int id, MovieTheaterCreationDTO movieTheaterCreationDTO)
        {
            var movieTheater = await ctx.MovieTheaters.FirstOrDefaultAsync(x => x.Id == id);

            if (movieTheater == null)
            {
                return NotFound();
            }

            movieTheater = mapper.Map(movieTheaterCreationDTO, movieTheater);
            await ctx.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movieTheater = await ctx.MovieTheaters.FirstOrDefaultAsync(x => x.Id == id);

            if (movieTheater == null)
            {
                return NotFound();
            }

            ctx.Remove(movieTheater);
            await ctx.SaveChangesAsync();
            return NoContent();
        }

    }
}
