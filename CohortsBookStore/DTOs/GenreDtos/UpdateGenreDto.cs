namespace CohortsBookStore.DTOs.GenreDtos;

public class UpdateGenreDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public bool IsActive { get; set; } = true;
}