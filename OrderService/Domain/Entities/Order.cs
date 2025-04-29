using System.ComponentModel.DataAnnotations;

namespace OrderEntities;

public partial class Order
{
    [Range(0, int.MaxValue, ErrorMessage = "Id must be >= 0")]
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Id must be > 0")]
    public int ProductId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Invalid value of amount")]
    public int ProductAmount { get; set; }
}
