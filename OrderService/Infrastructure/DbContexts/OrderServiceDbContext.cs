﻿using Microsoft.EntityFrameworkCore;
using OrderEntities;

namespace OrderService.Infrastructure.DbContexts;

public partial class OrderServiceDbContext : DbContext
{
    public OrderServiceDbContext()
    {
    }

    public OrderServiceDbContext(DbContextOptions<OrderServiceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Order { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Order_pkey");

            entity.ToTable("Order");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
