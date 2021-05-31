using System.Collections.Generic;
// using MySql.Data.MySqlClient;
using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
  public class Item
  {
    public Item()
    {
      this.JoinEntities = new HashSet<CategoryItem>();
      ItemCompleted = false;
    }
    public int ItemId { get; set; }
    public string Description { get; set; }


    [Display(Name = "Item Completed")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public bool ItemCompleted { get; set; }
    public DateTime DueDate { get; set; }

    public virtual ICollection<CategoryItem> JoinEntities { get; }
  }
} //JoinEntities, which will hold the list of relationships this Item is a part of -- which is how we will find its related Categories.

// public Item(string description)
// {
//   Description = description;
// }

// public Item(string description, int id)
// {
//   Description = description;
//   Id = id;
// }
// public override bool Equals(System.Object otherItem)
// {
//   if (!(otherItem is Item))
//   {
//     return false;
//   }
//   else
//   {
//     Item newItem = (Item)otherItem;
//     bool idEquality = (this.Id == newItem.Id);
//     bool descriptionEquality = (this.Description == newItem.Description);
//     return (idEquality && descriptionEquality);
//   }
// }

// public static List<Item> GetAll()
// {
//   List<Item> allItems = new List<Item> { };
//   MySqlConnection conn = DB.Connection();
//   conn.Open();
//   MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
//   cmd.CommandText = @"SELECT * FROM items;";
//   MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
//   while (rdr.Read())
//   {
//     int itemId = rdr.GetInt32(0);
//     string itemDescription = rdr.GetString(1);
//     Item newItem = new Item(itemDescription, itemId);
//     allItems.Add(newItem);
//   }
//   conn.Close();
//   if (conn != null)
//   {
//     conn.Dispose();
//   }
//   return allItems;
//   // Open a database connection;
//   // Construct a SQL query;
//   // Return the query results from the database;
//   // Close the connection.
// }

// public static void ClearAll()
// {
//   MySqlConnection conn = DB.Connection();
//   conn.Open();
//   MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
//   cmd.CommandText = @"DELETE FROM items;";
//   cmd.ExecuteNonQuery();
//   conn.Close();
//   if (conn != null)
//   {
//     conn.Dispose();
//   }
// }
// public static Item Find(int id)
// {
//   // We open a connection.
//   MySqlConnection conn = DB.Connection();
//   conn.Open();

//   // We create MySqlCommand object and add a query to its CommandText property. We always need to do this to make a SQL query.
//   var cmd = conn.CreateCommand() as MySqlCommand;
//   cmd.CommandText = @"SELECT * FROM items WHERE id = @thisId;";

//   // We have to use parameter placeholders (@thisId) and a `MySqlParameter` object to prevent SQL injection attacks. This is only necessary when we are passing parameters into a query. We also did this with our Save() method.
//   MySqlParameter thisId = new MySqlParameter();
//   thisId.ParameterName = "@thisId";
//   thisId.Value = id;
//   cmd.Parameters.Add(thisId);

//   // We use the ExecuteReader() method because our query will be returning results and we need this method to read these results. This is in contrast to the ExecuteNonQuery() method, which we use for SQL commands that don't return results like our Save() method.
//   var rdr = cmd.ExecuteReader() as MySqlDataReader;
//   int itemId = 0;
//   string itemDescription = "";
//   while (rdr.Read())
//   {
//     itemId = rdr.GetInt32(0);
//     itemDescription = rdr.GetString(1);
//   }
//   Item foundItem = new Item(itemDescription, itemId);

//   // We close the connection.
//   conn.Close();
//   if (conn != null)
//   {
//     conn.Dispose();
//   }
//   return foundItem;
// }

// public void Save()
// {
//   MySqlConnection conn = DB.Connection();
//   conn.Open();
//   var cmd = conn.CreateCommand() as MySqlCommand;

//   // Begin new code

//   cmd.CommandText = @"INSERT INTO items (description) VALUES (@ItemDescription);";
//   MySqlParameter description = new MySqlParameter();
//   description.ParameterName = "@ItemDescription";
//   description.Value = this.Description;
//   cmd.Parameters.Add(description);
//   cmd.ExecuteNonQuery();
//   Id = (int)cmd.LastInsertedId;

//   // End new code

//   conn.Close();
//   if (conn != null)
//   {
//     conn.Dispose();
//   }
// }
