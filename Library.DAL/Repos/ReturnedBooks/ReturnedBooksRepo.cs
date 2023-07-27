using Microsoft.EntityFrameworkCore;

namespace Library.DAL;

public class ReturnedBooksRepo : GenericRepo<ReturnedBook>, IReturnedBooksRepo
{
    private readonly LibraryContext _context;

    public ReturnedBooksRepo(LibraryContext context) : base(context)
    {
        _context = context;
    }

    public int Getcount(int bookId, int clientId)
    {
        return _context.Set<ReturnedBook>()
                        .Where(b => b.BookId == bookId && b.ClientId == clientId)
                        .Count();
    }
}
