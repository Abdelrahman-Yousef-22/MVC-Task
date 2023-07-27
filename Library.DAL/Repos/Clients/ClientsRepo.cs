using Microsoft.EntityFrameworkCore;

namespace Library.DAL;

public class ClientsRepo:GenericRepo<Client>, IClientsRepo
{
    private readonly LibraryContext _context;

    public ClientsRepo(LibraryContext context) : base(context)
    {
        _context = context;
    }

    public Client? GetByIDWithBorrowedBooks(int id)
    {
        return _context.Set<Client>()
              .Include(c => c.BorrowedBooks)
                .ThenInclude(b=>b.Book)
              .FirstOrDefault(c => c.Id == id);
    }
}
