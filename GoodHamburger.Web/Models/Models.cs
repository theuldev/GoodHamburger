namespace GoodHamburger.Web.Models;

public class ProductModel
{
    public Guid   Id       { get; set; }
    public string Name     { get; set; } = "";
    public decimal Price   { get; set; }
    public string Category { get; set; } = "";
}

public class OrderItemModel
{
    public Guid    ProductId   { get; set; }
    public string  ProductName { get; set; } = "";
    public int     Quantity    { get; set; }
    public decimal UnitPrice   { get; set; }
    public decimal Total       { get; set; }
}

public class OrderModel
{
    public Guid             Id          { get; set; }
    public string           OrderNumber { get; set; } = "";
    public List<OrderItemModel> Items   { get; set; } = new();
    public decimal          Subtotal    { get; set; }
    public decimal          Discount    { get; set; }
    public decimal          Total       { get; set; }
}

public class PagedResponse<T>
{
    public int           Page     { get; set; }
    public int           PageSize { get; set; }
    public int           Total    { get; set; }
    public List<T>       Data     { get; set; } = new();
}

public class CreateOrderItemRequest
{
    public Guid ProductId { get; set; }
    public int  Quantity  { get; set; }
}

public class CreateOrderRequest
{
    public List<CreateOrderItemRequest> Items { get; set; } = new();
}
