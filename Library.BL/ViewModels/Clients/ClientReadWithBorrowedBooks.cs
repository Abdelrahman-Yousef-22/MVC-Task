namespace Library.BL;

public class ClientReadWithBorrowedBooks
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IEnumerable<BooksReadVM> ReturnedBooks { get; set; } = new HashSet<BooksReadVM>();
    public IEnumerable<BooksReadVM> UnReturnedBooks { get; set; } = new HashSet<BooksReadVM>();

}

