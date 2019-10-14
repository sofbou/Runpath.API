using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunPath.API.Models;
using RunPath.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace RunPath.API.Controllers
{
    [Route("api/albums")]
    [ApiController]
    public class AlbumsController : Controller
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AlbumsController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlbums(int? userId)
        {
            var albumsFromRunpath =  await _repository.GetFullAlbums(userId);
            if (albumsFromRunpath == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<AlbumDto>>(albumsFromRunpath));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbum(int id)
        {
            var albumsFromRunPath = await _repository.GetFullAlbums();
            if (albumsFromRunPath == null)
                return NotFound();

            var albumFromRunPath = albumsFromRunPath.FirstOrDefault(x => x.Id == id);

            if (albumFromRunPath == null)
                return NotFound();

            return Ok(_mapper.Map<AlbumDto>(albumFromRunPath));
        }
    }
}
