using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zaczytani.Application.Client.Commands;
using Zaczytani.Application.Client.Queries;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;
using Zaczytani.Application.Shared.Commands;
using Zaczytani.Domain.Constants;

namespace Zaczytani.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPut()]
    public async Task<ActionResult> UpdateProfile([FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [Authorize(Roles = UserRoles.User)]
    [SetUserId]
    [HttpGet("Profile")]
    public async Task<ActionResult<UserProfileDto>> GetProfile(CancellationToken cancellationToken)
    {
        var query = new GetUserProfileQuery();
        var profile = await _mediator.Send(query, cancellationToken);
        return Ok(profile);
    }

    [Authorize(Roles = UserRoles.User)]
    [SetUserId]
    [HttpPost("Follow/{followedId}")]
    public async Task<IActionResult> Follow([FromRoute] Guid followedId)
    {
        var command = new FollowUserCommand(followedId);
        await _mediator.Send(command);
        return NoContent();
    }
}
