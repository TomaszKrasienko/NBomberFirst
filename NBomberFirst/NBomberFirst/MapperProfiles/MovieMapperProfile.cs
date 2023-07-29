using AutoMapper;
using NBomberFirst.DTOs;
using NBomberFirst.Entities;

namespace NBomberFirst.MapperProfiles;

public class MovieMapperProfile : Profile
{
    public MovieMapperProfile()
    {
        CreateMap<MovieDto, Movie>();
    }
}