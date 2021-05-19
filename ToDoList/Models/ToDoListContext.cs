using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
  public class ToDoListContext : DbContext
  {
    public virtual DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }

    public ToDoListContext(DbContextOptions options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { //Notice that we add the OnConfiguring method to enable lazy-loading.
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}

// Our ToDoListContext class inherits, or extends, from Entity Framework's DbContext. This ensures it includes all default built-in DbContext functionality.

// ToDoListContext also contains a property of type DbSet named Items that represents the Items table in our ToDoList database and lets us interact with it. DbSet needs to know what C# object it’s going to represent, so we must include Item in the angle brackets (<>) after DbSet

// We also include a constructor that inherits the behavior of its parent class constructor. As ToDoListContext is an extension of the DbContext class, we're invoking some constructor behavior from that class. Further, we are passing a variable of DbContextOptions called options to our constructor through its argument, instantiating our ToDoListContext with the options we defined in Startup.cs using is called a dependency injection.