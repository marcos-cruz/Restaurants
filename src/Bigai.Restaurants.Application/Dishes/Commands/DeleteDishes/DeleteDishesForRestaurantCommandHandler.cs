using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Exceptions;
using Bigai.Restaurants.Domain.Repositories;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Application.Dishes.Commands.DeleteDishes;

public class DeleteDishesForRestaurantCommandHandler : IRequestHandler<DeleteDishesForRestaurantCommand>
{
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly IDishesRepository _dishesRepository;
    private readonly ILogger<DeleteDishesForRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;

    public DeleteDishesForRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository,
                                                   IDishesRepository dishesRepository,
                                                   ILogger<DeleteDishesForRestaurantCommandHandler> logger,
                                                   IMapper mapper)
    {
        _restaurantsRepository = restaurantsRepository;
        _dishesRepository = dishesRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Removing all dishes from restaurant: {RestaurantId}", request.RestaurantId);

        var restaurant = await _restaurantsRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant is null)
        {
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }

        await _dishesRepository.DeleteAsync(restaurant.Dishes);
    }
}