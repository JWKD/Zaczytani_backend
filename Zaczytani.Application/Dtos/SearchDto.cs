namespace Zaczytani.Application.Dtos;

public record SearchDto(IEnumerable<BookSearchDto> Books, IEnumerable<AuthorSearchDto> Authors, IEnumerable<UserSearchDto> Users);
