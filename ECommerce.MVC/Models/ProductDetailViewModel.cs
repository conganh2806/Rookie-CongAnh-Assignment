public class ProductDetailViewModel
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public float Discount { get; set; }
    public int Quantity { get; set; }
    public int Sold { get; set; }
    public string ImageURL { get; set; } = default!;
    public List<string> CategoryName { get; set; } = new();
}
