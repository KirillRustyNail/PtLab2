namespace LabTp23.Models;

public class Product
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public int Price { get; set; } 
    public List<Cart>? Carts { get; set; }
}