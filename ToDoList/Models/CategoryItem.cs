namespace ToDoList.Models
{ //We need to create a class that will hold information about the relationship between a Category and an Item -- CategoryItem, which is an alphabetical combination of the two classes.
  public class CategoryItem
  {
    public int CategoryItemId { get; set; }
    public int ItemId { get; set; }
    public int CategoryId { get; set; }
    public virtual Item Item { get; set; }
    public virtual Category Category { get; set; }
  }
  //We have three different Id properties: one for CategoryItem, one for Item, and one for Category. In addition to that, we also have both Item and Category included as objects. Note that there is no constructor for this model. That's all we need for now. Entity will take care of the rest once we are ready to run our migration.
}