using AutoMapper;
using CohortsBookStore.DTO_s.BookDtos;
using CohortsBookStore.Entities;

namespace CohortsBookStore.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, CreateBookDto>().ReverseMap();
        CreateMap<Book, UpdateBookDto>().ReverseMap();
        CreateMap<Book, ByIdBookDto>().ReverseMap();
    }
}