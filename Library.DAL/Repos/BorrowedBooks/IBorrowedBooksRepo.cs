namespace Library.DAL;

public interface IBorrowedBooksRepo : IGenericRepo<BorrowedBook>
{
    BorrowedBook? CheckUnReturnedBorrowedbooks(int id);
    int Getcount(int bookId, int clientId);
    int GetcountOfUnReturnedCopies(int id);
    List<BorrowedBook> GetForSpecificBook(int id);
    BorrowedBook? GetLastBorrow(int bookId, int clientId);
}
