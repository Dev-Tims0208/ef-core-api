using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class RatingDTO
    {
        [Range(1, 5)]
        public int Ratings { get; set; }
        public int MovieId { get; set; }
    }
}
