using System.ComponentModel.DataAnnotations;

namespace Library.BL;

public class ClientEditVM
{
    public int Id { get; set; }

    [Required]
    [MinLength(4, ErrorMessage = "{0} length must be more than {1}")]
    [MaxLength(100, ErrorMessage = "{0} length must be less than {1}")]
    public string Name { get; set; } = null!;
}
