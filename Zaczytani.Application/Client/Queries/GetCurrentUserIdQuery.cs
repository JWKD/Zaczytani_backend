using MediatR;
using Zaczytani.Application.Filters;

namespace Zaczytani.Application.Client.Queries;

public class GetCurrentUserIdQuery : IRequest<Guid>, IUserIdAssignable
{
    public Guid UserId { get; private set; }
    public void SetUserId(Guid userId) => UserId = userId;

    private class Handler : IRequestHandler<GetCurrentUserIdQuery, Guid>
    {
        public Task<Guid> Handle(GetCurrentUserIdQuery request, CancellationToken cancellationToken)
            => Task.FromResult(request.UserId);
    }
}
