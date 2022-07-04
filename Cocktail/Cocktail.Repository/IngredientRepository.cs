using Cocktail.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Cocktail.Repository
{
    public class IngredientRepository
    {
        public string conStr = "Data Source=DESKTOP-RDKIF3O\\SQLEXPRESS;Initial Catalog=PraksaDB;Integrated Security=True"; // connection string

        public async Task<int> IngredientCountAsync(Guid ingredientID)
        {
            SqlConnection con = new SqlConnection(conStr);
            using (con)
            {
                using (SqlCommand count = new SqlCommand("SELECT COUNT(*) from dbo.Ingredient where dbo.Ingredient.Ingredient_ID = @ID", con))
                {
                    int ingredientCount;
                    try
                    {
                        await con.OpenAsync();
                        count.Parameters.AddWithValue("@ID", ingredientID);
                        ingredientCount = (int)await count.ExecuteScalarAsync();
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

        public async Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            SqlConnection con = new SqlConnection(conStr);
            List<Ingredient> ingredientList = new List<Ingredient>();
            using (con)
            {
                SqlCommand command = new SqlCommand("select * from dbo.Ingredient;", con);
                await con.OpenAsync();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
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

        public async Task<Ingredient> GetOneIngredientAsync(Guid ingredientID)
        {
            int ingredientCount;
            ingredientCount = await IngredientCountAsync(ingredientID);

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
                        await con.OpenAsync();
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
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

        public async Task<Ingredient> AddIngredientAsync(Ingredient ingredient)
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
                        await con.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        return new Ingredient (ingredientId, ingredient.Name, ingredient.Color);
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("Sql command error.");
                    }

                }
            }
        }

        public async Task<Ingredient> UpdateIngredientAsync(Guid ingredientID, Ingredient ingredient)
        {
            int ingredientCount;
            ingredientCount = await IngredientCountAsync(ingredientID);

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
                        await con.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        return new Ingredient(ingredientID, ingredient.Name, ingredient.Color);
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("Sql command error.");
                    }
                }
            }
        }

        public async Task DeleteIngredientAsync(Guid ingredientID)
        {
            int ingredientCount;
            ingredientCount = await IngredientCountAsync(ingredientID);

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
                        await con.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
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
