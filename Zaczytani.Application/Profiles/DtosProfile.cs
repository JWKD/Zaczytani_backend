using AutoMapper;
using Zaczytani.Application.Client.Commands;
using Zaczytani.Application.Dtos;
using Zaczytani.Domain.Entities;

namespace Zaczytani.Application.Profiles;

internal class DtosProfile : Profile
{
    public DtosProfile()
    {
        #region Book
        CreateMap<Book, BookDto>()
            .ForMember(x => x.PublishingHouse, opt => opt.MapFrom(src => src.PublishingHouse.Name))
            .ForMember(x => x.Rating, opt => opt.MapFrom(src => src.Reviews.Where(r => r.Rating != null).Average(r => r.Rating)))
            .ForMember(x => x.RatingCount, opt => opt.MapFrom(src => src.Reviews.Where(r => r.Rating != null).Count()))
            .ForMember(x => x.Reviews, opt => opt.MapFrom(src => src.Reviews.Where(r => r.Content != null).Count()));

        CreateMap<Book, SearchBookDto>()
            .ForMember(x => x.PublishingHouse, opt => opt.MapFrom(src => src.PublishingHouse.Name))
            .ForMember(x => x.Rating, opt => opt.MapFrom(src => src.Reviews.Where(r => r.Rating != null).Average(r => r.Rating)))
            .ForMember(x => x.RatingCount, opt => opt.MapFrom(src => src.Reviews.Where(r => r.Rating != null).Count()))
            .ForMember(x => x.Reviews, opt => opt.MapFrom(src => src.Reviews.Where(r => r.Content != null).Count()));

        CreateMap<Book, ReadingBookDto>();

        CreateMap<Book, ReviewDetailsBookDto>();
        #endregion

        #region Author
        CreateMap<Author, AuthorDto>();
        #endregion

        #region PublishingHouse
        CreateMap<PublishingHouse, PublishingHouseDto>();
        #endregion

        #region BookRequest
        CreateMap<BookRequest, BookRequestDto>()
            .ForMember(x => x.FileName, opt => opt.MapFrom(src => src.Image));
        CreateMap<BookRequest, UserBookRequestDto>();
        #endregion

        #region User
        CreateMap<User, UserDto>();
        CreateMap<User, UserProfileDto>()
            .ForMember(dest => dest.FavoriteGenres, opt => opt.Ignore())
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image));
        #endregion

        #region Bookshelf
        CreateMap<BookShelf, BookShelfDto>();
        CreateMap<CreateBookShelfCommand, BookShelf>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(_ => false));
        #endregion

        #region Report
        CreateMap<Report, ReportDto>()
            .ForMember(dest => dest.Review, opt => opt.MapFrom(src => src.Review.Content))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Review.User));
        #endregion

        #region Review
        CreateMap<Review,BookReviewDto>()
            .ForMember(x => x.Comments, opt => opt.MapFrom(src => src.Comments.Count()))
            .ForMember(x => x.Likes, opt => opt.MapFrom(src => src.Likes.Count()));

        CreateMap<Review, ReviewDetailsDto>()
            .ForMember(x => x.Likes, opt => opt.MapFrom(src => src.Likes.Count()));

        CreateMap<Review, NoteDto>();
        #endregion

        #region Comment
        CreateMap<Comment, CommentDto>();
        #endregion

        #region Challenge
        CreateMap<Challenge, ChallengeDto>();

        CreateMap<ChallengeProgress, ChallengeProgressDto>()
            .ForMember(dest => dest.BooksToRead, opt => opt.MapFrom(src => src.Challenge.BooksToRead))
            .ForMember(dest => dest.CriteriaValue, opt => opt.MapFrom(src => src.Challenge.CriteriaValue))
            .ForMember(dest => dest.Criteria, opt => opt.MapFrom(src => src.Challenge.Criteria))
            .ForMember(dest => dest.BooksRead, opt => opt.MapFrom(src => src.BooksRead))
            .ForMember(dest => dest.ChallengeId, opt => opt.MapFrom(src => src.ChallengeId))
            .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.BooksRead >= src.Challenge.BooksToRead));
        #endregion
    }
}
