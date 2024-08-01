namespace CohortsBookStore.DTOs.AuthorDtos;

public class CreateAuthorDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
}