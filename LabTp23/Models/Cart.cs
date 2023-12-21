namespace LabTp23.Models;

public class Cart
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public List<Product> Products { get; set; } = new List<Product>();
    public int TotalCost => Products.Sum(x => x.Price);
}