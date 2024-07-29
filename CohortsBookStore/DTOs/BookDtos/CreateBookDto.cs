namespace CohortsBookStore.DTO_s.BookDtos;

public class CreateBookDto
{
    public string Title { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
    public int GenreId { get; set; }
}