using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BL;

public class BookAddVM
{
    [Required]
    [MinLength(4, ErrorMessage = "{0} length must be more than {1}")] 
    [MaxLength(100, ErrorMessage = "{0} length must be less than {1}")] 
    public string Name { get; set; } = null!;

    [Required]
    [Range(0, 1000, ErrorMessage = "The {0} range is between {1} and {2}")]
    //[Range(0, int.MaxValue, ErrorMessage = "The value must be at least 0.")]
    public int Quantity { get; set; }
}
