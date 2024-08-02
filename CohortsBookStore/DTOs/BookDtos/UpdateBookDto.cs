namespace CohortsBookStore.DTO_s.BookDtos;

public class UpdateBookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
}