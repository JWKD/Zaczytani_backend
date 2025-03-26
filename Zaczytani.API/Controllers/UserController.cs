using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("Profile/{userId}")]
    [Authorize(Roles = UserRoles.User)]
    public async Task<ActionResult<UserProfileDto>> GetUserProfileById([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetUserProfileByIdQuery(userId);
        var profile = await _mediator.Send(query, cancellationToken);
        return Ok(profile);
    }

    [HttpGet("CurrentUserId")]
    [Authorize(Roles = UserRoles.User)]
    [SetUserId]
    public async Task<ActionResult<Guid>> GetCurrentUserId(CancellationToken cancellationToken)
    {
        var userId = await _mediator.Send(new GetCurrentUserIdQuery(), cancellationToken);
        return Ok(userId);
    }

}
