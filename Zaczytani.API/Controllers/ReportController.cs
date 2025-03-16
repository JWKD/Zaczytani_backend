using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zaczytani.Application.Admin.Commands;
using Zaczytani.Application.Admin.Queries;
using Zaczytani.Application.Client.Commands;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;
using Zaczytani.Domain.Enums;

namespace Zaczytani.API.Controllers;

[ApiController]
[Authorize]
[SetUserId]
[Route("api/[controller]")]
public class ReportController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("{reviewId}")]
    public async Task<ActionResult> ReportUser([FromRoute] Guid reviewId, [FromBody] ReportUserCommand command)
    {
        command.SetReviewId(reviewId);
        await _mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPatch("{reportId}/Reject")]
    public async Task<ActionResult> RejectReport([FromRoute] Guid reportId)
    {
        var command = new ProcessReportCommand(reportId, ReportStatus.Rejected);
        await _mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPatch("{reportId}/Block")]
    public async Task<ActionResult> BlockReport([FromRoute] Guid reportId)
    {
        var command = new ProcessReportCommand(reportId, ReportStatus.Blocked);
        await _mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "ADMIN")]
    [HttpGet("Pending")]
    public async Task<ActionResult<IEnumerable<ReportDto>>> GetPendingReports()
    {
        var reports = await _mediator.Send(new GetPendingReports());
        return Ok(reports);
    }
}
