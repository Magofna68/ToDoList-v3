using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Controllers
{
  public class CategoriesController : Controller
  {
    private readonly ToDoListContext _db;

    public CategoriesController(ToDoListContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Category> model = _db.Categories.ToList();
      return View(model);
      // Instead of using a verbose GetAll() method with raw SQL, we can instead access all our Items in List form by doing the following: db.Items.ToList()
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Category Category)
    {
      _db.Categories.Add(Category);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisCategory = _db.Categories
      .Include(category => category.JoinEntities)
      .ThenInclude(join => join.Item)
      .FirstOrDefault(category => category.CategoryId == id);
      return View(thisCategory);
      // First, the Details method takes the id of the entry we want to view as its sole parameter. Remember that this needs to match the property from the anonymous object we created using the ActionLink() method with the code new { id = item.ItemId }
    }

    public ActionResult Edit(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
      return View(thisCategory);
    }

    [HttpPost]
    public ActionResult Edit(Category category)
    {
      _db.Entry(category).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
      return View(thisCategory);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
      _db.Categories.Remove(thisCategory);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}

// __________________________________________________________



// namespace ToDoList.Controllers
// {
//   public class CategoriesController : Controller
//   {

//     [HttpGet("/categories")]
//     public ActionResult Index()
//     {
//       List<Category> allCategories = Category.GetAll();
//       return View(allCategories);
//     }

//     [HttpGet("/categories/new")]
//     public ActionResult New()
//     {
//       return View();
//     }

//     [HttpPost("/categories")]
//     public ActionResult Create(string categoryName)
//     {
//       Category newCategory = new Category(categoryName);
//       return RedirectToAction("Index");
//     }

//     [HttpGet("/categories/{id}")]
//     public ActionResult Show(int id)
//     {
//       Dictionary<string, object> model = new Dictionary<string, object>();
//       Category selectedCategory = Category.Find(id);
//       List<Item> categoryItems = selectedCategory.Items;
//       model.Add("category", selectedCategory);
//       model.Add("items", categoryItems);
//       return View(model);
//     }


// This one creates new Items within a given Category, not new Categories:

// [HttpPost("/categories/{categoryId}/items")]
// public ActionResult Create(int categoryId, string itemDescription)
// {
//   Dictionary<string, object> model = new Dictionary<string, object>();
//   Category foundCategory = Category.Find(categoryId);
//   Item newItem = new Item(itemDescription);
//   newItem.Save();
//   foundCategory.AddItem(newItem);
//   List<Item> categoryItems = foundCategory.Items;
//   model.Add("items", categoryItems);
//   model.Add("category", foundCategory);
//   return View("Show", model);
// }
