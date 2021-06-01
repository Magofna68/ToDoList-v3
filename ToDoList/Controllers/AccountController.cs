using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ToDoList.Models;
using System.Threading.Tasks;
using ToDoList.ViewModels;
//System.Threading.Tasks. This will allow us to use asynchronous Tasks so we can use async and await to register new users.

namespace ToDoList.Controllers
{
  public class AccountController : Controller
  {
    //We have private preferences for _userManager and _signInManager. We'll use dependency injection in the AccountController constructor to configure these services for us.
    private readonly ToDoListContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ToDoListContext db)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }

    public ActionResult Index()
    {
      return View();
    }

    public IActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Register(RegisterViewModel model)
    {
      //Our Register() action doesn't return an ActionResult. Instead, it returns a Task containing an ActionResult

      var user = new ApplicationUser { UserName = model.Email };
      IdentityResult result = await _userManager.CreateAsync(user, model.Password);
      if (result.Succeeded)
      {
        return RedirectToAction("Index");
      }
      else
      {
        return View();
      }
    }

    public ActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
      Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
      if (result.Succeeded)
      {
        return RedirectToAction("Index");
      }
      else
      {
        return View();
      }
    }
  }
}