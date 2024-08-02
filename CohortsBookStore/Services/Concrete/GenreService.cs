using AutoMapper;
using CohortsBookStore.Context;
using CohortsBookStore.DTOs.GenreDtos;
using CohortsBookStore.Entities;
using CohortsBookStore.Exceptions;
using CohortsBookStore.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CohortsBookStore.Services.Concrete;

public class GenreService : IGenreService
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    
    public GenreService(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<ResultGenreDto>> GetAllGenres()
    {
        var genres = await _context.Genres.ToListAsync();
        return _mapper.Map<List<ResultGenreDto>>(genres);
    }

    public async Task CreateGenre(CreateGenreDto createGenreDto)
    {
        var genre = _mapper.Map<Genre>(createGenreDto);
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateGenre(UpdateGenreDto updateGenreDto)
    {
        var genreExist = await _context.Genres.FindAsync(updateGenreDto.Id);

        if (genreExist == null)
            throw new NotFoundException($"Genre with ID {updateGenreDto.Id} not found.");

        _mapper.Map(updateGenreDto, genreExist);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGenre(int genreId)
    {
        var genre = await _context.Genres.FindAsync(genreId);

        if (genre == null)
            throw new NotFoundException($"Genre with ID {genreId} not found.");

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
    }

    public async Task<GetGenreByIdDto> GetGenreById(int genreId)
    {
        var genre = await _context.Genres.FindAsync(genreId);

        if (genre == null)
            throw new NotFoundException($"Genre with ID {genreId} not found.");

        return _mapper.Map<GetGenreByIdDto>(genre);
    }
}