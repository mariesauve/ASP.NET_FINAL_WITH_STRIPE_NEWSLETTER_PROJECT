using System.ComponentModel.DataAnnotations;

namespace KoolStuff.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a Product Name")]
        [MaxLength(100)]
        public string? Name { get; set; }
        [MaxLength(300)]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Don't forget the price")]
        public decimal Price { get; set; } = decimal.Zero;
        public int Quantity { get; set; }
        public string Image { get; set; }
      
    }
}
