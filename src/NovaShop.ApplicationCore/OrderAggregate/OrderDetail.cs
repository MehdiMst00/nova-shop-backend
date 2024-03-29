﻿namespace NovaShop.ApplicationCore.OrderAggregate;

public class OrderDetail : EntityBase
{
    public int OrderId { get; private set; }
    public int CatalogItemId { get; private set; }
    public int Count { get; private set; }
    public decimal Price { get; private set; }

    public CatalogItem CatalogItem { get; set; } = null!;

    private OrderDetail() { }

    public OrderDetail(int orderId, int catalogItemId, int count, decimal price)
    {
        OrderId = orderId;
        CatalogItemId = catalogItemId;
        Count = count;
        Price = price;
    }

    public void UpdateOrder(int orderId)
    {
        OrderId = Guard.Against.NegativeOrZero(orderId, nameof(orderId));
    }

    public void UpdateCatalog(int productId)
    {
        CatalogItemId = Guard.Against.NegativeOrZero(productId, nameof(productId));
    }

    public void UpdateCount(int count)
    {
        Count = Guard.Against.NegativeOrZero(count, nameof(count));
    }

    public void AddQuantity(int quantity)
    {
        Guard.Against.OutOfRange(quantity, nameof(quantity), 0, int.MaxValue);

        Count += quantity;
    }

    public void UpdateProductPrice(int productPrice)
    {
        Price = Guard.Against.Negative(productPrice);
    }
}