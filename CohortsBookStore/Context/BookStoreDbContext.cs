using CohortsBookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace CohortsBookStore.Context;

public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base (options)
    {
        
    }
    public DbSet<Book> Books { get; set; }
}