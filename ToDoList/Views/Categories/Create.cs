
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