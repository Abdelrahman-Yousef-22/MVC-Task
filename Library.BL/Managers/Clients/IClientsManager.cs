namespace Library.BL;

public interface IClientsManager
{
    IEnumerable<ClientReadVM> GetAll();
    ClientReadVM GetByID(int id);
    ClientEditVM GetByIdToEdit(int id);
    RequestStatus Add(ClientAddVM doctor);
    RequestStatus Edit(ClientEditVM doctor);
    RequestStatus Delete(int id);
    ClientReadWithBorrowedBooks? GetByIDWithBooks(int id);
    //ClientReadWithBorrowedBooks GetByIDWithBorrowedBooks(int id);
}
