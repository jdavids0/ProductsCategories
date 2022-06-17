#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProductsCategories.Models;

public class Category
{
    [Key]
    public int CategoryID {get; set;}
    [Required]
    [MinLength(2, ErrorMessage="Name must be at least 2 characters")]
    public string CategoryName {get; set;}
    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
    // nav property
    public List<Association> Types {get; set;} = new List<Association>();
}