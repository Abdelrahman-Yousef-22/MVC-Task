using Microsoft.EntityFrameworkCore;

namespace Library.DAL;

public class BorrowedBooksRepo : GenericRepo<BorrowedBook>, IBorrowedBooksRepo
{
    private readonly LibraryContext _context;

    public BorrowedBooksRepo(LibraryContext context) : base(context)
    {
        _context = context;
    }

    

    public int Getcount(int bookId, int clientId)
    {
        return _context.Set<BorrowedBook>()
                        .Where(b => b.BookId == bookId && b.ClientId == clientId)
                        .Count();
    }

    public int GetcountOfUnReturnedCopies(int id)
    {
        return _context.Set<BorrowedBook>()
                .Where(b => b.BookId == id && b.IsReturned==false)
                .Count();
    }

    public List<BorrowedBook> GetForSpecificBook(int id)
    {
        return _context.Set<BorrowedBook>()
                        .Include(b => b.Client)
                        .Include(b => b.Book)
                        .Where(b => b.BookId == id && b.IsReturned == false).ToList();
    }

    public BorrowedBook? GetLastBorrow(int bookId, int clientId)
    {
        return _context.Set<BorrowedBook>()
                       .Where(b => b.BookId == bookId && b.ClientId == clientId)
                       .OrderByDescending(b => b.DateBorrow)
                       .FirstOrDefault();
                       
    }

    public BorrowedBook? CheckUnReturnedBorrowedbooks(int id)
    {
        return _context.Set<BorrowedBook>()
                       .FirstOrDefault(b => b.ClientId == id && b.IsReturned == false);
                       
    }


}
