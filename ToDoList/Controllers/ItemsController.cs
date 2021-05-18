using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
      List<Item> model = _db.Items.ToList();
      return View(model);
      // Instead of using a verbose GetAll() method with raw SQL, we can instead access all our Items in List form by doing the following: db.Items.ToList()
    }
    //db is an instance of our DbContext class. It's holding a reference to our database.

    // Once there, it looks for an object named Items. This is the DbSet we declared in ToDoListContext.cs.

    // LINQ turns this DbSet into a list using the ToList() method, which comes from the System.Linq namespace.

    // This expression is what creates the model we'll use for the Index view.

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Item item)
    {
      _db.Items.Add(item);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
      // First, the Details method takes the id of the entry we want to view as its sole parameter. Remember that this needs to match the property from the anonymous object we created using the ActionLink() method with the code new { id = item.ItemId }
    }

    public ActionResult Edit(int id)
    {
      var thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      return View(thisItem);
    }

    [HttpPost]
    public ActionResult Edit(Item item)
    {
      _db.Entry(item).State = EntityState.Modified;
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
  }
}