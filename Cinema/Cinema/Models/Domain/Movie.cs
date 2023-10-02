using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema.Models.Domain
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public Genre[] Genres { get; set; }
        public int MinAge { get; set; }
        public string Director { get; set; }
        public string ImgUrl { get; set; }
        public float Rating { get; set; }
        public int? ReleaseDate { get; set; }
    }
}