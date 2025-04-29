namespace OrderEntities;

public partial class Order
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int ProductAmount { get; set; }
}
