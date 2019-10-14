using RunPath.API.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunPath.API.Services
{
    public interface IRepository
    {
        Task<IEnumerable<Album>> GetFullAlbums(int? userId = null);
        Task<IEnumerable<Photo>> GetAlbumPhotos(int albumId);
    }
}
