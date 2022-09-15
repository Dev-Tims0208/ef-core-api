using MoviesAPI.Entities;

namespace MoviesAPI.Services
{
    public class InMemoryRepository : IRepository
    {
        private List<Genre> _genres;
        public InMemoryRepository()
        {
            _genres = new List<Genre>()
            {
                new Genre(){Id = 1, Name = "Comedy" },
                new Genre(){Id = 2, Name = "Harem" },
            };

        }

        public async Task<List<Genre>> GetAllGenres()
        {
            await Task.Delay(1);
            return _genres;
        }

        public Genre GetGenreById(int Id)
        {
            return _genres.FirstOrDefault(x => x.Id == Id);
        }
    }
}
