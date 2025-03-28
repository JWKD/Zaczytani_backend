using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zaczytani.Application.Client.Queries;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;
using Zaczytani.Application.Shared.Commands;
using Zaczytani.Application.Shared.Queries;
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

    [SetUserId]
    [HttpGet("Info")]
    public async Task<ActionResult<UserProfileDto>> GetInfo(CancellationToken cancellationToken)
    {
        var info = await _mediator.Send(new GetUserInfoQuery(), cancellationToken);
        return Ok(info);
    }
}
