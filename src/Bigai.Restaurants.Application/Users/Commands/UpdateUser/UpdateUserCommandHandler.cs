using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Exceptions;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly ILogger<UpdateUserCommandHandler> _logger;
    private readonly IUserContext _userContext;
    private readonly IUserStore<User> _userStore;

    public UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger,
                                    IUserContext userContext,
                                    IUserStore<User> userStore)
    {
        _logger = logger;
        _userContext = userContext;
        _userStore = userStore;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _userContext.GetCurrentUser();

        _logger.LogInformation("Updating user {UserId} with {@UpdatedUser}", user!.UserId, request);

        var existingUser = await _userStore.FindByIdAsync(user!.UserId, cancellationToken);

        if (existingUser is null)
        {
            throw new NotFoundException(nameof(User), user!.UserId);
        }

        existingUser.DateOfBirth = request.DateOfBirth;
        existingUser.Nationality = request.Nationality;

        await _userStore.UpdateAsync(existingUser, cancellationToken);

        return true;
    }
}