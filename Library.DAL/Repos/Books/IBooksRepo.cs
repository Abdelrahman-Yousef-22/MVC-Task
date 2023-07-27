namespace Library.DAL;

public interface IBooksRepo : IGenericRepo<Book>
{
    IQueryable<Book> GetByIdnow(int id);
    Book? GetByIDWithClients(int id);
}
