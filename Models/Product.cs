#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProductsCategories.Models;

public class Product
{
    [Key]
    public int ProductID {get; set;}
    [Required]
    [MinLength(2, ErrorMessage="Name must be at least 2 characters")]
    public string Name {get; set;}
    [MinLength(5, ErrorMessage="Description must be at least 5 characters")]
    public string Description {get; set;}
    [Required]
    [Range(0.01, Int32.MaxValue)]
    public float Price {get; set;}
    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
    // nav property
    public List<Association> Items {get; set;} = new List<Association>();
}