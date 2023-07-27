using Library.DAL;

namespace Library.BL;

public class BookReadWithClientsVM
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public int AvailableQuantity { get; set; }

    public IEnumerable<ClientReadVM> Clients { get; set; }=new HashSet<ClientReadVM>();
}
