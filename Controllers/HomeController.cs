using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsCategories.Models;

namespace ProductsCategories.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {

        return RedirectToAction("Products");
    }

    // *** SHOW ALL PRODUCTS ***
    [HttpGet("products")]
    public IActionResult Products()
    {
        ViewBag.AllProducts = _context.Products.ToList();
        return View();
    }

    // ** CREATE NEW PRODUCT ***
    [HttpPost("product/add")]
    public IActionResult AddProduct(Product newProduct)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newProduct);
            _context.SaveChanges();
            return RedirectToAction("Products");
        } else {
            return View("Products");
        }
    }

    // *** SHOW ONE PRODUCT ***
    [HttpGet("products/{productID}")]
    public IActionResult OneProduct(int ProductID)
    {
        ViewBag.OneProduct = _context.Products.FirstOrDefault(p => p.ProductID == ProductID);
        // so getting all categories (i.e. list Types), then all products via .ThenInclude(), then all categories associated belonging to that product
        ViewBag.ProductCategories = _context.Categories.Include(c => c.Types).ThenInclude(prod => prod.Product).Where(c => c.Types.Any(p => p.ProductID == ProductID));
        // same as above but !belonging to that product
        ViewBag.AllCategories = _context.Categories.Include(c => c.Types).Where(c => !(c.Types.Any(p => p.ProductID == ProductID)));
        return View();
    }

    // *** ADD PRODUCT TO CATEGORIES ***
    [HttpPost("association/add")]
    public IActionResult CategoryToProduct(Association newAssociation)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newAssociation);
            _context.SaveChanges();
            return Redirect($"/products/{newAssociation.ProductID}");
        } else {
            return View("OneProduct");
        }
    }

    // *** SHOW ALL CATEGORIES ***

    [HttpGet("categories")]
    public IActionResult Categories()
    {
        ViewBag.AllCategories = _context.Categories.ToList();
        return View();
    }

    // *** CREATE NEW CATEGORY ***
    [HttpPost("category/add")]
    public IActionResult AddCategory(Category newCategory)
    {
        if(ModelState.IsValid)
        {
            _context.Add(newCategory);
            _context.SaveChanges();
            return RedirectToAction("Categories");
        } else {
            return View("Categories");
        }
    }

    // *** SHOW ONE CATEGORY ***
    [HttpGet("categories/{categoryID}")]
    public IActionResult OneCategory(int CategoryID)
    {
        ViewBag.OneCategory = _context.Categories.FirstOrDefault(c => c.CategoryID == CategoryID);
        ViewBag.CategoriesProducts = _context.Products.Include(p => p.Items).ThenInclude(c => c.Category).Where(j => j.Items.Any(k => k.CategoryID == CategoryID));
        ViewBag.AllProducts = _context.Products.Include(p => p.Items).Where(i => !(i.Items.Any(j => j.CategoryID == CategoryID)));
        return View();
    }

    // *** ADD CATEGORY TO PRODUCTS ***
    [HttpPost("association/category/add")]
    public IActionResult ProductToCategory(Association newAssociation)
    {
        if(ModelState.IsValid){
            _context.Add(newAssociation);
            _context.SaveChanges();
            return Redirect($"/categories/{newAssociation.CategoryID}");
        } else {
            return View("OneCategory");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
