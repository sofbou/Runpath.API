using AutoMapper;
using RunPath.API.Client;
using RunPath.API.Models;

namespace RunPath.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Album, AlbumDto>().ReverseMap();
            CreateMap<Photo, PhotoDto>().ReverseMap();
        }
    }
}
