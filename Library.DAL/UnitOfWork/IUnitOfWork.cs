namespace Library.DAL;

public interface IUnitOfWork
{
    public  IClientsRepo ClientsRepo { get; }
    public IBooksRepo BooksRepo { get; }
    public IBorrowedBooksRepo BorrowedBooksRepo { get; }
    public IReturnedBooksRepo ReturnedBooksRepo { get; }

    int SaveChanges();
}
