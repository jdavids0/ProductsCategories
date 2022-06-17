#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProductsCategories.Models;

public class Association
{
    [Key]
    public int AssociationID {get; set;}
    public int ProductID {get; set;}
    public int CategoryID {get; set;}
    // many to many nav properties
    public Product? Product {get; set;}
    public Category? Category {get; set;}
}