using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunPath.API.Models
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
        public int AlbumId { get; set; }
    }
}
