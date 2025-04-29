using System.ComponentModel.DataAnnotations;

namespace ProductService.Domain.Entities;

public partial class Product
{
    [Range(0, int.MaxValue, ErrorMessage = "amount must be >= 0")]
    public int Id { get; set; }

    [MaxLength(50, ErrorMessage = "Name length must be <= 50")]
    public string Name { get; set; } = null!;
    [Range(0, 999999.99, ErrorMessage = "Invalid value of price")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Amount must be >= 0")]
    public int Amount { get; set; }
}
