using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Enums;

namespace Zaczytani.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<List<BookGenre>> GetFavoriteGenresAsync(Guid userId, CancellationToken cancellationToken);
        IQueryable<User> GetBySearchPhrase(string searchPhrase);
    }
}
