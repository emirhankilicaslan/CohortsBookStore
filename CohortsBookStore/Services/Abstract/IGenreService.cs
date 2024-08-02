using CohortsBookStore.DTOs.GenreDtos;

namespace CohortsBookStore.Services.Abstract;

public interface IGenreService
{
    Task<List<ResultGenreDto>> GetAllGenres();
    Task CreateGenre(CreateGenreDto createGenreDto);
    Task UpdateGenre(UpdateGenreDto updateGenreDto);
    Task DeleteGenre(int genreId);
    Task<GetGenreByIdDto> GetGenreById(int genreId);
}