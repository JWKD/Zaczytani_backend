using MediatR;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;

namespace Zaczytani.Application.Shared.Queries;

public class GetUserInfoQuery : IRequest<UserInfoDto>, IUserIdAssignable
{
    private Guid UserId { get; set; }
    public void SetUserId(Guid userId) => UserId = userId;

    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserInfoDto>
    {
        public async Task<UserInfoDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            return new UserInfoDto(request.UserId);
        }
    }
}
