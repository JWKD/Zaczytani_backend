namespace Zaczytani.Application.Dtos;

public class UserProfileDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int TotalBooksRead { get; set; }
    public List<string> FavoriteGenres { get; set; } = new();
    public List<string> Badges { get; set; } = new();
    public IEnumerable<BookDto> ReadBooks { get; set; } = [];
    public IEnumerable<BookDto> CurrentlyReading { get; set; } = [];
    public IEnumerable<ChallengeProgressDto> ChallengeProgresses { get; set; } = [];
}
