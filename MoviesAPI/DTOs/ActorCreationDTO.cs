﻿using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTOs
{
    public class ActorCreationDTO
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Biography { get; set; }
        //public string Picture { get; set; }
    }
}
