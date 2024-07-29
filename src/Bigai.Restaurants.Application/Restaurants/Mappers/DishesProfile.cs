using AutoMapper;

using Bigai.Restaurants.Application.Dishes.Commands.CreateDish;
using Bigai.Restaurants.Application.Restaurants.Dtos;
using Bigai.Restaurants.Domain.Entities;

namespace Bigai.Restaurants.Application.Restaurants.Mappers;

public class DishesProfile : Profile
{
    public DishesProfile()
    {
        CreateMap<CreateDishCommand, Dish>();
        CreateMap<Dish, DishDto>();
    }
}