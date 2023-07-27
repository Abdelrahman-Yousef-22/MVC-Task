using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<BorrowedBook> BorrowedBooks => Set<BorrowedBook>();
    public DbSet<ReturnedBook> ReturnedBooks => Set<ReturnedBook>();

    public LibraryContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BorrowedBook>(entity =>
        {
            entity.HasKey(b => new { b.ClientId, b.BookId, b.DateBorrow });

        });

        modelBuilder.Entity<ReturnedBook>(entity =>
        {
            entity.HasKey(b => new { b.ClientId, b.BookId, b.DateReturned });

        });

    }
}
