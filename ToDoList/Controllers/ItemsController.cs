using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDoList.Controllers
{
  public class ItemsController : Controller
  {
    private readonly ToDoListContext _db;
    // The line private readonly ToDoListContext _db; declares a private and readonly field of type ToDoListContext.

    public ItemsController(ToDoListContext db)
    {
      _db = db;
    }
    // The constructor above allows us to set the value of our new _db property to our ToDoListContext. This is achievable due to a dependency injection we set up in our AddDbContext method in the ConfigureServices method in our Startup.cs file.

    public ActionResult Index()
    {
      return View(_db.Items.ToList());
      // List<Item> model = _db.Items.Include(item => item.Category).ToList();
      // return View(model);
    }
    //     // Instead of using a verbose GetAll() method with raw SQL, we can instead access all our Items in List form by doing the following: db.Items.ToList()

    //     //db is an instance of our DbContext class. It's holding a reference to our database.

    //     // Once there, it looks for an object named Items. This is the DbSet we declared in ToDoListContext.cs.

    //     // LINQ turns this DbSet into a list using the ToList() method, which comes from the System.Linq namespace.

    //     // This expression is what creates the model we'll use for the Index view.

    public ActionResult Create()
    {
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Item item, int CategoryId)
    {
      _db.Items.Add(item);
      _db.SaveChanges();
      if (CategoryId != 0)
      { //Then we add a conditional to handle cases where a CategoryId doesn't get passed in to the route (such as when there are no Categories).
        _db.CategoryItem.Add(new CategoryItem() { CategoryId = CategoryId, ItemId = item.ItemId });
        //This combines the ItemId with the CategoryId specified in the dropdown menu and passed in through our route's parameters.
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Item thisItem = _db.Items //_db.Items expression gives us a list of Item objects from the database
      .Include(item => item.JoinEntities)
      .ThenInclude(join => join.Category)
      //We need the actual Category objects themselves, so we use ThenInclude() method to load the Category of each CategoryItem. A CategoryItem is simply a reference to a relationship. Each CategoryItem includes the id of an Item as well as the id of a Category. We are actually returning the associated Category of a CategoryItem here.
      .FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
      // First, the Details method takes the id of the entry we want to view as its sole parameter. Remember that this needs to match the property from the anonymous object we created using the ActionLink() method with the code new { id = item.ItemId }
    }

    public ActionResult Edit(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View(thisItem);
    }

    [HttpPost]
    public ActionResult Edit(Item item, int categoryId)
    {
      if (categoryId != 0)
      //Notice that we again use a conditional in the case that no Categories yet exist or are being used.
      {
        _db.CategoryItem.Add(new CategoryItem()
        {
          CategoryId = categoryId,
          ItemId = item.ItemId
        });
      }
      _db.Entry(item).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddCategory(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View(thisItem);
    }

    //It may seem redundant to have a separate page where we add a categories because we already have the ability to add a category in our edit view. However, in that case, we had only established a one-to-many relationship as we never gave the user the ability to add more than one category to an item at a time, only the ability to edit an item's category. By adding a separate route to add categories, we give the user the option of adding many categories to an item.

    [HttpPost]
    public ActionResult AddCategory(Item item, int CategoryId)
    {
      if (CategoryId != 0)
      {
        _db.CategoryItem.Add(new CategoryItem() { CategoryId = CategoryId, ItemId = item.ItemId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      _db.Items.Remove(thisItem);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteCategory(int joinId)
    {
      var joinEntry = _db.CategoryItem.FirstOrDefault(entry => entry.CategoryItemId == joinId);
      _db.CategoryItem.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}