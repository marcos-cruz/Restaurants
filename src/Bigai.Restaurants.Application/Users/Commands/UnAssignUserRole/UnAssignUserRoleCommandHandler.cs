using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Exceptions;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Application.Users.Commands.UnAssignUserRole;

public class UnAssignUserRoleCommandHandler : IRequestHandler<UnAssignUserRoleCommand, bool>
{
    private readonly ILogger<UnAssignUserRoleCommandHandler> _logger;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserContext _userContext;

    public UnAssignUserRoleCommandHandler(ILogger<UnAssignUserRoleCommandHandler> logger,
                                          UserManager<User> userManager,
                                          RoleManager<IdentityRole> roleManager,
                                          IUserContext userContext)
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
        _userContext = userContext;
    }

    public async Task<bool> Handle(UnAssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Unassigning user role: {@Request}", request);

        var existingUser = await _userManager.FindByEmailAsync(request.UserEmail) ?? throw new NotFoundException(nameof(User), request.UserEmail);

        var existingRole = await _roleManager.FindByNameAsync(request.RoleName) ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        await _userManager.RemoveFromRoleAsync(existingUser, existingRole.Name!);

        return true;
    }
}