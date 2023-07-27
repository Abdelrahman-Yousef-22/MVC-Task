namespace Library.DAL;

public interface IReturnedBooksRepo : IGenericRepo<ReturnedBook>
{
    int Getcount(int bookId, int clientId);
}
