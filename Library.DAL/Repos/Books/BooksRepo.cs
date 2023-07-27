using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Library.DAL;

public class BooksRepo:GenericRepo<Book>, IBooksRepo
{

    private readonly LibraryContext _context;

    public BooksRepo(LibraryContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<Book> GetByIdnow(int id)
    {
        return _context.Set<Book>().AsQueryable();
    }

    public Book? GetByIDWithClients(int id)
    {
        return _context.Set<Book>()
                .Include(b => b.BorrowedBooks)
                    .ThenInclude(b => b.Client)
                .FirstOrDefault(b => b.Id == id);
        
    }
}
