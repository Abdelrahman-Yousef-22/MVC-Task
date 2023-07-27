namespace Library.BL;

public interface IBooksManager
{
    IEnumerable<BooksReadVM> GetAll();
    BooksReadVM GetByID(int id);
    BookEditVM GetByIdToEdit(int id);


    //IEnumerable<BooksReadVM> GetByPerformance(int id);
    BookReadWithClientsVM? GetByIDWithClients(int id);

    RequestStatus Add(BookAddVM doctor);
    RequestStatus Edit(BookEditVM doctor);


    RequestStatus Delete(int id);
    RequestStatus Borrow(BookBorrowVM book);
    RequestStatus Return(BookReturnVM book);
    //void borrow(BookBorrowVM book);
}
