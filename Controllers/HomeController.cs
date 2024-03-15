using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormApp.Controllers;

public class HomeController : Controller
{


    public HomeController()
    {

    }

    public IActionResult Index(string searchstring, string category)
    {
        var product = Repository.Products;

        if(!String.IsNullOrEmpty(searchstring))
        {
            ViewBag.SearchString = searchstring;
            product = product.Where(p =>p.BookName.ToLower().Contains(searchstring)).ToList();
        }
        if(!String.IsNullOrEmpty(category) && category != "0")
        {
            product = product.Where(p=>p.CategoryId == int.Parse(category)).ToList();
        }
        ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","CategoryName");


        return View(product);
        

    }
    public IActionResult Admin()
    {
        return View(Repository.Products);
    }

}
