namespace Library.DAL;

public class BorrowedBook
{
    public int BookId { get; set; }
    public int ClientId { get; set; }
    public DateTime DateBorrow { get; set; } = DateTime.Now;
    public DateTime DateShouldReturn { get; set; }
    public bool IsReturned { get; set; } = false;
    public Book Book { get; set; } = null!;
    public Client Client { get; set; } = null!;



}
