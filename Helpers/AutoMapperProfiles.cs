using AutoMapper;
using HousingAPI.Dtos;
using HousingAPI.Dtos.PropertyDto;
using HousingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityDto>().ReverseMap();

            CreateMap<City, CityNameUpdateDto>().ReverseMap();

            CreateMap<Property, AddPropertyDto>().ReverseMap();

            CreateMap<Photo, PhotoDto>().ReverseMap();


            CreateMap<Property, PropertyListDto>()
                .ForMember(p => p.City, opt => opt.MapFrom(src => src.City.CityName))
                .ForMember(p => p.PropertyType, opt => opt.MapFrom(src => src.PropertyType.Name))
                .ForMember(p => p.Country, opt => opt.MapFrom(src => src.City.Country))
                .ForMember(p => p.FurnishingType, opt => opt.MapFrom(src => src.FurnishingType.Name))
                .ForMember(p => p.Photo, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsPrimary).ImageUrl));


            CreateMap<Property, PropertyDetailDto>()
                .ForMember(p => p.City, opt => opt.MapFrom(src => src.City.CityName))
                .ForMember(p => p.PropertyType, opt => opt.MapFrom(src => src.PropertyType.Name))
                .ForMember(p => p.Country, opt => opt.MapFrom(src => src.City.Country))
                .ForMember(p => p.FurnishingType, opt => opt.MapFrom(src => src.FurnishingType.Name));

            CreateMap<PropertyType, PropertyTypeDto>().ReverseMap();

            CreateMap<FurnishingType, FurnishingTypeDto>().ReverseMap();
        }
    }
}
