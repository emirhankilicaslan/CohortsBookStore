using AutoMapper;
using CohortsBookStore.DTO_s.BookDtos;
using CohortsBookStore.DTOs.AuthorDtos;
using CohortsBookStore.DTOs.GenreDtos;
using CohortsBookStore.Entities;

namespace CohortsBookStore.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, CreateBookDto>().ReverseMap();
        CreateMap<Book, UpdateBookDto>().ReverseMap();
        CreateMap<Book, GetBookByIdDto>().ReverseMap();
        
        CreateMap<Genre, CreateGenreDto>().ReverseMap();
        CreateMap<Genre, UpdateGenreDto>().ReverseMap();
        CreateMap<Genre, GetGenreByIdDto>().ReverseMap();
        
        CreateMap<Author, CreateAuthorDto>().ReverseMap();
        CreateMap<Author, UpdateAuthorDto>().ReverseMap();
        CreateMap<Author, GetAuthorByIdDto>().ReverseMap();
    }
}