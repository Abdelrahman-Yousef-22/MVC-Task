namespace Library.DAL;

public interface IClientsRepo : IGenericRepo<Client>
{
    Client? GetByIDWithBorrowedBooks(int id);
}
