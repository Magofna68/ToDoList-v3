@{
  Layout = "_Layout";
}

@model ToDoList.Models.Category

<h4>Add a new category</h4>
@using(Html.BeginForm())
{
  @Html.LabelFor(model => model.Name)
  @Html.TextBoxFor(model => model.Name)
  < input type = "submit" value = "Add new category" />
}
< p > @Html.ActionLink("Show all categories", "Index") </ p >



// @{
//   Layout = "_Layout";
// }

// @model ToDoList.Models.Category
// // @* A model directive tells our view what type of data will be passed into the view from the controller route. In this
// //   case, we're telling the view that it will receive a model that is an Item. *@

// <h4>Add a new Category:</h4>
// @using (Html.BeginForm())
// {
//   @Html.LabelFor(model => model.Name)
//   // lambda expression
//   @Html.TextBoxFor(model => model.Name)
//   // LabelFor() and TextBoxFor(). The first generates a label for a form field while the second generates a text box.
//   <input type="submit" value="Add new task" />
//   // this form will send a POST request to the Create() route.
// }
// <p>@Html.ActionLink("Show all categories", "Index")</p>