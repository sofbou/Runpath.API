using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RunPath.API.Client
{
    public class Album
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int UserId { get; set; }

        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}
