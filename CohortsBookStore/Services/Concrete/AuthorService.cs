using AutoMapper;
using CohortsBookStore.Context;
using CohortsBookStore.DTOs.AuthorDtos;
using CohortsBookStore.Entities;
using CohortsBookStore.Exceptions;
using CohortsBookStore.Services.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CohortsBookStore.Services.Concrete;

public class AuthorService : IAuthorService
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    
    public AuthorService(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<ResultAuthorDto>> GetAllAuthors()
    {
        var authors = await _context.Authors.ToListAsync();
        return _mapper.Map<List<ResultAuthorDto>>(authors);
    }

    public async Task CreateAuthor(CreateAuthorDto createAuthorDto)
    {
        var author = _mapper.Map<Author>(createAuthorDto);
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAuthor(UpdateAuthorDto updateAuthorDto)
    {
        var authorExist = await _context.Authors.FindAsync(updateAuthorDto.Id);

        if (authorExist == null)
            throw new NotFoundException($"Author with ID {updateAuthorDto.Id} not found.");

        _mapper.Map(updateAuthorDto, authorExist);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAuthor(int authorId)
    {
        var author = await _context.Authors.FindAsync(authorId);

        if (author == null)
            throw new NotFoundException($"Author with ID {authorId} not found.");
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
    }

    public async Task<GetAuthorByIdDto> GetAuthorById(int authorId)
    {
        var author = await _context.Authors.FindAsync(authorId);

        if (author == null)
            throw new NotFoundException($"Author with ID {authorId} not found.");

        return _mapper.Map<GetAuthorByIdDto>(author);
    }
}