using Cocktail.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Cocktail.Repository
{
    public class IngredientRepository
    {
        public string conStr = "Data Source=DESKTOP-RDKIF3O\\SQLEXPRESS;Initial Catalog=PraksaDB;Integrated Security=True"; // connection string

        public int IngredientCount(Guid ingredientID)
        {
            SqlConnection con = new SqlConnection(conStr);
            using (con)
            {
                using (SqlCommand count = new SqlCommand("SELECT COUNT(*) from dbo.Ingredient where dbo.Ingredient.Ingredient_ID = @ID", con))
                {
                    int ingredientCount;
                    try
                    {
                        con.Open();
                        count.Parameters.AddWithValue("@ID", ingredientID);
                        ingredientCount = (int)count.ExecuteScalar();
                        con.Close();
                    }
                    catch
                    {
                        throw new Exception("Sql command error.");
                    }

                    return ingredientCount;
                }
            }
        }

        public List<Ingredient> GetAllIngredients()
        {
            SqlConnection con = new SqlConnection(conStr);
            List<Ingredient> ingredientList = new List<Ingredient>();
            using (con)
            {
                SqlCommand command = new SqlCommand("select * from dbo.Ingredient;", con);
                con.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                        ingredientList.Add(new Ingredient(reader.GetGuid(0), reader.GetString(1), reader.GetString(2)));
                }
                else
                {
                    reader.Close();
                    throw new Exception("Empty table.");
                }
                reader.Close();

                return ingredientList;
            }
        }

        public Ingredient GetOneIngredient(Guid ingredientID)
        {
            int ingredientCount;
            ingredientCount = IngredientCount(ingredientID);

            if (ingredientCount == 0)
                throw new Exception("No elements with such ID.");

            SqlConnection con = new SqlConnection(conStr);

            Ingredient ingredient;

            using (con)
            {
                using (SqlCommand cmd = new SqlCommand("select * from dbo.Ingredient where dbo.Ingredient.Ingredient_ID = @ID;", con))
                {

                    cmd.Parameters.AddWithValue("@ID", ingredientID);

                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            ingredient = new Ingredient(reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
                            return ingredient;
                        }
                        else
                        {
                            throw new Exception("Cannot read data.");
                        }
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("Sql command error.");
                    }
                }
            }
        }

        public Ingredient AddIngredient(Ingredient ingredient)
        {
            SqlConnection con = new SqlConnection(conStr);
            Guid ingredientId = Guid.NewGuid();
            using (con)
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"INSERT INTO dbo.Ingredient(Ingredient_ID, Name, Color) 
                            VALUES(@param1,@param2,@param3)";

                    cmd.Parameters.AddWithValue("@param1", ingredientId);
                    cmd.Parameters.AddWithValue("@param2", ingredient.Name);
                    cmd.Parameters.AddWithValue("@param3", ingredient.Color);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        return new Ingredient (ingredientId, ingredient.Name, ingredient.Color);
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("Sql command error.");
                    }

                }
            }
        }

        public Ingredient UpdateIngredient(Guid ingredientID, Ingredient ingredient)
        {
            int ingredientCount;
            ingredientCount = IngredientCount(ingredientID);

            if (ingredientCount == 0)
                throw new Exception("No elements with such ID.");

            SqlConnection con = new SqlConnection(conStr);

            using (con)
            {
                using (SqlCommand cmd = new SqlCommand("update dbo.Ingredient set dbo.Ingredient.Name = @name, dbo.Ingredient.Color = @color where dbo.Ingredient.Ingredient_ID = @ID", con))
                {

                    cmd.Parameters.AddWithValue("@ID", ingredientID);
                    cmd.Parameters.AddWithValue("@name", ingredient.Name);
                    cmd.Parameters.AddWithValue("@price", ingredient.Color);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        return new Ingredient(ingredientID, ingredient.Name, ingredient.Color);
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("Sql command error.");
                    }
                }
            }
        }

        public void DeleteIngredient(Guid ingredientID)
        {
            int ingredientCount;
            ingredientCount = IngredientCount(ingredientID);

            if (ingredientCount == 0)
                throw new Exception("No elements with such ID.");

            SqlConnection con = new SqlConnection(conStr);

            using (con)
            {
                using (SqlCommand cmd = new SqlCommand("delete from dbo.Ingredient where dbo.Ingredient.Ingredient_ID = @ID", con))
                {
                    cmd.Parameters.AddWithValue("@ID", ingredientID);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        return;
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("Sql command error.");
                    }
                }
            }
        }

    }
}
