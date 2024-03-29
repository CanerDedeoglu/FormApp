﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormApp.Controllers;

public class HomeController : Controller
{


    public HomeController()
    {

    }

    public IActionResult Index(string searchString, string category)
    {
        var product = Repository.Products;

        if (!String.IsNullOrEmpty(searchString))
        {

            ViewBag.SearchString = searchString;
            product = product.Where(p => p.BookName.ToLower().Contains(searchString)).ToList();
        }

        if (!String.IsNullOrEmpty(category) && category != "0")
        {
            product = product.Where(p => p.CategoryId == int.Parse(category)).ToList();
        }

        var model = new ProductViewModel
        {
            Products = product,
            Categories = Repository.Categories,
            SelectedCategory = category
        };

        return View(model);
    }
    public IActionResult Admin()
    {
        return View(Repository.Products);
    }

    public IActionResult Details(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if (entity == null)
        {
            return NotFound();
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "CategoryName");
        return View(entity);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "CategoryName");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product model, IFormFile imageFile)
    {
        var allowedExtensions = new[] {".jpg",".jpeg",".png"};
        var extension = Path.GetExtension(imageFile.FileName);
        var randomfileName = string.Format($"{Guid.NewGuid().ToString()}{extension}") ;
        var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img",randomfileName);

        if(imageFile != null){
            if(!allowedExtensions.Contains(extension)){
                ModelState.AddModelError("","Lütfen geçerli formatta resim yükleyiniz.");
            }
        }

        if(ModelState.IsValid){
            if(imageFile != null){
            using(var stream = new FileStream(path,FileMode.Create)){
                await imageFile.CopyToAsync(stream);
            }}
            model.Image = randomfileName;
            model.ProductId = Repository.Products.Count +1;
            Repository.CreateProduct(model);
            return RedirectToAction("Index");  
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId","CategoryName");

        return View(model);    
            
    }

    public IActionResult Edit(int? id)
    {
        if(id == null){
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if(entity == null){
            return NotFound();
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId","CategoryName");
        return View(entity);
    }

    [HttpPost]

    public async Task<IActionResult> Edit(int id, Product model, IFormFile? imageFile){

        if(id != model.ProductId){
            return NotFound();
        }

        if(ModelState.IsValid){
            if(imageFile != null){
                var allowedExtensions = new[] {".jpg",".jpeg",".png"};
                var extension = Path.GetExtension(imageFile.FileName);
                var randomfileName = string.Format($"{Guid.NewGuid().ToString()}{extension}") ;
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img",randomfileName);

                using(var stream = new FileStream(path,FileMode.Create)){
                await imageFile.CopyToAsync(stream);
                }
                model.Image = randomfileName;
            }
            Repository.EditProduct(model);
            return RedirectToAction("Admin");
        }    
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId","CategoryName");
        return View(model);
    }
    
    public IActionResult Delete(int? id){
        if(id == null){
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if(entity == null){
            return NotFound();
        }
        return View(entity);
    }
    [HttpPost]
    public IActionResult Delete(int id, int ProductId){
        if(id != ProductId){
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == ProductId);
        if(entity == null){
            return NotFound();
        }
        Repository.DeleteProduct(entity);
        return RedirectToAction("Admin");
    }

 }
