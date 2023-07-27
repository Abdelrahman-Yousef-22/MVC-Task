namespace Library.DAL;

public class ReturnedBook
{
    public int BookId { get; set; }
    public int ClientId { get; set; }
    public DateTime DateReturned { get; set; } = DateTime.Now;
    public Book Book { get; set; } = null!;
    public Client Client { get; set; } = null!;
}
