using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zaczytani.Application.Client.Commands;
using Zaczytani.Application.Client.Queries;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;

namespace Zaczytani.API.Controllers;

[ApiController]
[Authorize]
[SetUserId]
[Route("api/[controller]")]
public class ChallengeController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult> CreateChallenge([FromBody] CreateChallengeCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPost("{challengeId}/Join")]
    public async Task<ActionResult> JoinChallenge([FromRoute] Guid challengeId, CancellationToken cancellationToken)
    {
        var command = new JoinChallengeCommand(challengeId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChallengeDto>>> GetAllChallenges(CancellationToken cancellationToken)
    {
        var query = new GetAllChallengesQuery();
        var challenges = await _mediator.Send(query, cancellationToken);

        return Ok(challenges);
    }

    [HttpGet("Progress")]
    public async Task<ActionResult<IEnumerable<ChallengeProgressDto>>> GetChallengeProgresses(CancellationToken cancellationToken)
    {
        var query = new GetChallengeProgressesQuery();
        var challenges = await _mediator.Send(query, cancellationToken);

        return Ok(challenges);
    }

    [HttpDelete("{challengeId}")]
    public async Task<IActionResult> DeleteChallenge([FromRoute] Guid challengeId)
    {
        var command = new DeleteChallengeCommand(challengeId);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{challengeId}/Progress")]
    public async Task<IActionResult> DeleteChallengeProgress([FromRoute] Guid challengeId)
    {
        var command = new DeleteChallengeProgressCommand(challengeId);
        await _mediator.Send(command);
        return NoContent();
    }
}
