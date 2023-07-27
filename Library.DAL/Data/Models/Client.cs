namespace Library.DAL;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IEnumerable<BorrowedBook> BorrowedBooks { get; set; } = new HashSet<BorrowedBook>();
    public IEnumerable<ReturnedBook> ReturnedBooks { get; set; } = new HashSet<ReturnedBook>();



}
