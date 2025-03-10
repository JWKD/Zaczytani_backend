using MediatR;
using Zaczytani.Application.Filters;
using Zaczytani.Domain.Exceptions;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Client.Commands;

public record DeleteChallengeProgressCommand(Guid ChallengeId) : IRequest, IUserIdAssignable
{
    public Guid UserId { get; private set; }
    public void SetUserId(Guid userId) => UserId = userId;

    private class Handler(IChallengeRepository challengeRepository) : IRequestHandler<DeleteChallengeProgressCommand>
    {
        private readonly IChallengeRepository _challengeRepository = challengeRepository;

        public async Task Handle(DeleteChallengeProgressCommand request, CancellationToken cancellationToken)
        {
            var progress = (await _challengeRepository.GetChallengesWithProgressByUserId(request.UserId, cancellationToken))
                .FirstOrDefault(p => p.ChallengeId == request.ChallengeId);

            if (progress == null)
            {
                throw new NotFoundException("You are not part of this challenge.");
            }

            await _challengeRepository.DeleteProgressAsync(progress.Id, cancellationToken);
            await _challengeRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
