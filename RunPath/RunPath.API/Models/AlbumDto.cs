using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunPath.API.Models
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public List<PhotoDto> Photos { get; set; }
    }
}

