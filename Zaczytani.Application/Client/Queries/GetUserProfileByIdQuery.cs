using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Zaczytani.Application.Dtos;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Enums;
using Zaczytani.Domain.Exceptions;
using Zaczytani.Domain.Helpers;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Client.Queries;

public record GetUserProfileByIdQuery(Guid UserId) : IRequest<UserProfileDto>;

internal class Handler(
    UserManager<User> userManager,
    IUserRepository userRepository,
    IBookShelfRepository bookShelfRepository,
    IFileStorageRepository fileStorageRepository,
    IChallengeRepository challengeRepository,
    IMapper mapper
) : IRequestHandler<GetUserProfileByIdQuery, UserProfileDto>
{
    public async Task<UserProfileDto> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString())
            ?? throw new NotFoundException("User not found");

        var userImage = fileStorageRepository.GetFileUrl(user.Image);

        var favoriteGenres = await userRepository.GetFavoriteGenresAsync(request.UserId, cancellationToken);
        var bookShelves = await bookShelfRepository.GetAllByUserIdAsync(request.UserId, cancellationToken);

        var readBooksShelf = bookShelves.FirstOrDefault(bs => bs.Type == BookShelfType.Read);
        var currentlyReadingShelf = bookShelves.FirstOrDefault(bs => bs.Type == BookShelfType.Reading);

        var readBookDtos = readBooksShelf?.Books.Select(b =>
        {
            var book = mapper.Map<BookDto>(b);
            book.ImageUrl = fileStorageRepository.GetFileUrl(b.Image);
            return book;
        });

        var currentlyReading = currentlyReadingShelf?.Books.Select(b =>
        {
            var book = mapper.Map<BookDto>(b);
            book.ImageUrl = fileStorageRepository.GetFileUrl(b.Image);
            return book;
        });

        var challengeProgresses = (await challengeRepository
            .GetChallengesWithProgressByUserId(request.UserId, cancellationToken))
            .Take(2)
            .Select(cp =>
            {
                var dto = mapper.Map<ChallengeProgressDto>(cp);
                if (dto.Criteria == ChallengeType.Genre &&
                    Enum.TryParse(dto.CriteriaValue, false, out BookGenre bookGenre))
                {
                    dto.CriteriaValue = EnumHelper.GetEnumDescription(bookGenre);
                }
                return dto;
            });

        return new UserProfileDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            ImageUrl = userImage,
            TotalBooksRead = readBooksShelf?.Books.Count ?? 0,
            FavoriteGenres = favoriteGenres.Select(g => g.ToString()).ToList(),
            ReadBooks = readBookDtos ?? [],
            CurrentlyReading = currentlyReading ?? [],
            Badges = new List<string> { "First Book Read", "100 Books Read" },
            ChallengeProgresses = challengeProgresses
        };
    }
}
