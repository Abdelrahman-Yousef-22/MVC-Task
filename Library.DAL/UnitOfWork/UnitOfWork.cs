using Microsoft.EntityFrameworkCore;

namespace Library.DAL;

public class UnitOfWork : IUnitOfWork
{
    public IClientsRepo ClientsRepo { get; }
    public IBooksRepo BooksRepo { get; }
    public IBorrowedBooksRepo BorrowedBooksRepo { get; }
    public IReturnedBooksRepo ReturnedBooksRepo { get; }

    private readonly LibraryContext _context;

    public UnitOfWork(LibraryContext context,
        IClientsRepo clientsRepo,
        IBooksRepo booksRepo,
        IBorrowedBooksRepo borrowedBooksRepo,
        IReturnedBooksRepo returnedBooksRepo
    )
    {
        _context = context;
        ClientsRepo = clientsRepo;
        BooksRepo = booksRepo;
        BorrowedBooksRepo = borrowedBooksRepo;
        ReturnedBooksRepo = returnedBooksRepo;
    }



    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
}
