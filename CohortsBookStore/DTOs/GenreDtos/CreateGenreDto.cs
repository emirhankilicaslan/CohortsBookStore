namespace CohortsBookStore.DTOs.GenreDtos;

public class CreateGenreDto
{
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
}