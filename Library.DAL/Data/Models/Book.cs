namespace Library.DAL;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public int AvailableQuantity { get; set; }
    public IEnumerable<BorrowedBook> BorrowedBooks { get; set; }=new HashSet<BorrowedBook>();
    public IEnumerable<ReturnedBook> ReturnedBooks { get; set; } = new HashSet<ReturnedBook>();


}
