using System;
using System.Collections.Generic;

namespace ProductEntities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }
    public int Amount { get; set; }
}
