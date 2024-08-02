namespace CohortsBookStore.DTOs.AuthorDtos;

public class GetAuthorByIdDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
}