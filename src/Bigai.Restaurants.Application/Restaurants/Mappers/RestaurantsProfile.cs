using AutoMapper;

using Bigai.Restaurants.Application.Restaurants.Dtos;
using Bigai.Restaurants.Domain.Entities;

namespace Bigai.Restaurants.Application.Restaurants.Mappers;

public class RestaurantsProfile : Profile
{
    public RestaurantsProfile()
    {
        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(d => d.City, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(d => d.PostalCode, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
            .ForMember(d => d.Street, opt =>
                opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
            .ForMember(d => d.Dishes, opt =>
                opt.MapFrom(src => src.Dishes));
    }
}