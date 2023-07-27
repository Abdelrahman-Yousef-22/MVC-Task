using System.ComponentModel.DataAnnotations;

namespace Library.BL;

public class BookBorrowVM
{

    [Required]
    [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "{0} must be a non-negative integer begin from 1.")]
    //[Range(1, uint.MaxValue, ErrorMessage = "The {0} range is between {1} and {2}")]
    public int BookId { get; set; }

    [Required]
    [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "{0} must be a non-negative integer begin from 1.")]
   //[Range(1, uint.MaxValue, ErrorMessage = "The {0} range is between {1} and {2}")]
    public int ClientId { get; set; }

    public DateTime DateShouldReturn { get; set; } = DateTime.Now.AddDays(7);
}
