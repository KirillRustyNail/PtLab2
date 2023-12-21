namespace LabTp23.Models;

public class Purchase
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public Guid ProductID { get; set; }
    public string Person { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateTime Date { get; set; }
}