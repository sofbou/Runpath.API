using Newtonsoft.Json;
using RunPath.API.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace RunPath.API.Services
{
    public class AlbumPhotoRepository: IRepository
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private readonly AlbumPhotoClient _client;

        public AlbumPhotoRepository(AlbumPhotoClient client)
        {
            _client = client;
        }

        private async Task<IEnumerable<Album>> GetAlbums()
        {
            return await _client.GetItems<Album>("albums", _cancellationTokenSource.Token);
        }

        private async Task<IEnumerable<Photo>> GetPhotos()
        {
            return await _client.GetItems<Photo>("photos", _cancellationTokenSource.Token);
        }

        public async Task<IEnumerable<Photo>> GetAlbumPhotos(int albumId)
        {
            var photos = await GetPhotos();

            return photos.Where(x => x.AlbumId == albumId).ToList();
        }

        public async Task<IEnumerable<Album>> GetFullAlbums(int? userId = null)
        {
            var albums = await GetAlbums();
            var photos = await GetPhotos();

            foreach (var album in albums)
            {
                album.Photos = photos.Where(x => x.AlbumId == album.Id).ToList();
            }

            return userId == null ? albums : albums.Where(x => x.UserId == userId.Value);
        }
    }
}
