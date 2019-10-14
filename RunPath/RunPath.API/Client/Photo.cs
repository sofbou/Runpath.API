using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RunPath.API.Client
{
    public class Photo
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string ThumbnailUrl { get; set; }

        [Required]
        public int AlbumId { get; set; }
    }
}
