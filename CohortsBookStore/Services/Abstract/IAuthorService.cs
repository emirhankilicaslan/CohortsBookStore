using CohortsBookStore.DTOs.AuthorDtos;

namespace CohortsBookStore.Services.Abstract;

public interface IAuthorService
{
    Task<List<ResultAuthorDto>> GetAllAuthors();
    Task CreateAuthor(CreateAuthorDto createAuthorDto);
    Task UpdateAuthor(UpdateAuthorDto updateAuthorDto);
    Task DeleteAuthor(int authorId);
    Task<GetAuthorByIdDto> GetAuthorById(int authorId);
}