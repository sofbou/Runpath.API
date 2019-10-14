using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunPath.API.Models;
using RunPath.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunPath.API.Controllers
{
    [Route("api/albums/{albumId}/photos")]
    [ApiController]
    public class PhotosController : Controller
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PhotosController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPhotos(int albumId, int? userId = null)
        {
            var photosFromRunPath = await _repository.GetAlbumPhotos(albumId);
            if (photosFromRunPath == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<PhotoDto>>(photosFromRunPath));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoto(int albumId, int id, int? userId = null)
        {
            var photosFromRunPath = await _repository.GetAlbumPhotos(albumId);
            if (photosFromRunPath == null)
                return NotFound();

            var photoFromRunPath = photosFromRunPath.FirstOrDefault(x => x.Id == id);
            if (photoFromRunPath == null)
                return NotFound();

            return Ok(_mapper.Map<PhotoDto>(photoFromRunPath));
        }

    }
}
