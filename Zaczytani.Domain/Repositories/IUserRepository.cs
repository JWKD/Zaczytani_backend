using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Enums;

namespace Zaczytani.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<List<BookGenre>> GetFavoriteGenresAsync(Guid userId, CancellationToken cancellationToken);
        IQueryable<User> GetBySearchPhrase(string searchPhrase);
        Task AddAsync(Follower entity, CancellationToken cancellationToken);
        Task<bool> IsFollowingAsync(Guid followerId, Guid followedId, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
