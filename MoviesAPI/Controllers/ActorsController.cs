using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Entities;
using MoviesAPI.Helpers;

namespace MoviesAPI.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly ApplicationDbContext ctx;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;
        private readonly string containerName = "actors";

        public ActorsController(ApplicationDbContext ctx, IMapper mapper, IFileStorageService fileStorageService)
        {
            this.ctx = ctx;
            this.mapper = mapper;
            this.fileStorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get()
        {
            var actors = await ctx.Actors.ToListAsync();
            return mapper.Map<List<ActorDTO>>(actors);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var actor = await ctx.Actors.FirstOrDefaultAsync(x => x.Id == id);

            if (actor == null)
            {
                return NotFound();
            }

            return mapper.Map<ActorDTO>(actor);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreationDTO actorCreationDTO)
        {
            var actor = mapper.Map<Actor>(actorCreationDTO);
            if (actorCreationDTO.Picture != null)
            {
                actor.Picture = await fileStorageService.SaveFile(containerName, actorCreationDTO.Picture);
            }
            ctx.Add(actor);
            await ctx.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ActorCreationDTO actorCreationDTO)
        {
            var actor = await ctx.Actors.FirstOrDefaultAsync(x => x.Id == id);
            if (actor == null)
            {
                return NotFound();
            }
            actor = mapper.Map(actorCreationDTO, actor);
            await ctx.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await ctx.Actors.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            ctx.Remove(new Actor() { Id = id });
            await ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}
