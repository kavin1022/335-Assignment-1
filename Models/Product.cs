using System.ComponentModel.DataAnnotations;

namespace A1.Models
{
    public class Product
    {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public float Price { get; set; }
}
}
