using Cocktail.Common;
using Cocktail.Model;
using Cocktail.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Text;

namespace Cocktail.Repository
{
    public class CocktailRepository : ICocktailRepository
    {
        public string conStr = "Data Source=DESKTOP-RDKIF3O\\SQLEXPRESS;Initial Catalog=PraksaDB;Integrated Security=True"; // connection string
        public async Task<int> CocktailCountAsync(Guid cocktailID)
        {
            SqlConnection con = new SqlConnection(conStr);
            using (con)
            {
                using (SqlCommand count = new SqlCommand("SELECT COUNT(*) from dbo.Cocktail where dbo.Cocktail.Cocktail_ID = @ID", con))
                {
                    int cocktailCount;
                    try
                    {
                        await con.OpenAsync();
                        count.Parameters.AddWithValue("@ID", cocktailID);
                        cocktailCount = (int)await count.ExecuteScalarAsync();
                        con.Close();
                    }
                    catch
                    {
                        throw new Exception("Sql command error.");
                    }

                    return cocktailCount;
                }
            }
        }

        public async Task<List<CocktailDB>> GetAllCocktailsAsync(Paging paging, Sorting sorting, CocktailFilter filter)
        {
            SqlConnection con = new SqlConnection(conStr);
            List<CocktailDB> cocktailList = new List<CocktailDB>();
            using (con)
            {
                int offset = (paging.PageNumber - 1) * paging.Rpp;

                SqlCommand command = new SqlCommand();
                command.Connection = con;

                StringBuilder stringBuilder = new StringBuilder("select * from dbo.Cocktail where 1=1 ");

                if (filter.NameSearch != null)
                {
                    stringBuilder.Append("and Name like @NameSearch ");
                    command.Parameters.AddWithValue("@NameSearch", "%" + filter.NameSearch + "%");
                }
                if (filter.PriceUpper != null)
                {
                    stringBuilder.Append("and Price < @PriceUpper ");
                    command.Parameters.AddWithValue("@PriceUpper", filter.PriceUpper);
                }
                if (filter.PriceLower != null)
                {
                    stringBuilder.Append("and Price > @PriceLower ");
                    command.Parameters.AddWithValue("@PriceLower", filter.PriceLower);
                }
                
                stringBuilder.Append(string.Format("order by {0} {1} ", sorting.OrderBy, sorting.SortOrder));
                stringBuilder.Append("offset @offset rows fetch next @rpp rows only;");

                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@rpp", paging.Rpp);

                command.CommandText = stringBuilder.ToString();

                await con.OpenAsync();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                        cocktailList.Add(new CocktailDB(reader.GetGuid(0), reader.GetString(1), reader.GetDouble(2)));
                }

                reader.Close();
                return cocktailList;
            }
        }

        public async Task<CocktailDB> GetOneCocktailAsync(Guid cocktailID)
        {
            int cocktailCount;
            cocktailCount = await CocktailCountAsync(cocktailID);

            if (cocktailCount == 0)
                throw new Exception("No elements with such ID.");

            SqlConnection con = new SqlConnection(conStr);

            CocktailDB cocktail;

            using (con)
            {
                using (SqlCommand cmd = new SqlCommand("select * from dbo.Cocktail where dbo.Cocktail.Cocktail_ID = @ID;", con))
                {

                    cmd.Parameters.AddWithValue("@ID", cocktailID);

                    try
                    {
                        await con.OpenAsync();
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        if (await reader.ReadAsync())
                        {
                            cocktail = new CocktailDB(reader.GetGuid(0), reader.GetString(1), reader.GetDouble(2));
                            return cocktail;
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

        public async Task<CocktailDB> AddCocktailAsync(CocktailDB cocktail)
        {
            SqlConnection con = new SqlConnection(conStr);
            Guid cocktailId = Guid.NewGuid();
            using (con)
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"INSERT INTO dbo.Cocktail(Cocktail_ID, Name, Price) 
                            VALUES(@param1,@param2,@param3)";

                    cmd.Parameters.AddWithValue("@param1", cocktailId);
                    cmd.Parameters.AddWithValue("@param2", cocktail.Name);
                    cmd.Parameters.AddWithValue("@param3", cocktail.Price);

                    try
                    {
                        await con.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        return new CocktailDB(cocktailId, cocktail.Name, cocktail.Price);
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("Sql command error.");
                    }

                }
            }
        }

        public async Task<CocktailDB> UpdateCocktailAsync(Guid cocktailID, CocktailDB cocktail)
        {
            int cocktailCount;
            cocktailCount = await CocktailCountAsync(cocktailID);

            if (cocktailCount == 0)
                throw new Exception("No elements with such ID.");

            SqlConnection con = new SqlConnection(conStr);

            using (con)
            {
                using (SqlCommand cmd = new SqlCommand("update dbo.Cocktail set dbo.Cocktail.Name = @name, dbo.Cocktail.Price = @price where dbo.Cocktail.Cocktail_ID = @ID", con))
                {

                    cmd.Parameters.AddWithValue("@ID", cocktailID);
                    cmd.Parameters.AddWithValue("@name", cocktail.Name);
                    cmd.Parameters.AddWithValue("@price", cocktail.Price);

                    try
                    {
                        await con.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        return new CocktailDB(cocktailID, cocktail.Name, cocktail.Price);
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("Sql command error.");
                    }
                }
            }
        }

        public async Task DeleteCocktailAsync(Guid cocktailID)
        {
            int cocktailCount;
            cocktailCount = await CocktailCountAsync(cocktailID);

            if (cocktailCount == 0)
                throw new Exception("No elements with such ID.");

            SqlConnection con = new SqlConnection(conStr);

            using (con)
            {
                using (SqlCommand cmd = new SqlCommand("delete from dbo.Cocktail where dbo.Cocktail.Cocktail_ID = @ID", con))
                {
                    cmd.Parameters.AddWithValue("@ID", cocktailID);

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

        public async Task<CocktailIngredients> AllCocktailIngredientsAsync(Guid cocktailID)
        {
            int cocktailCount;
            cocktailCount = await CocktailCountAsync(cocktailID);

            if (cocktailCount == 0)
                throw new Exception("No cocktail with such ID.");

            List<Ingredient> ingredientList = new List<Ingredient>();
            string cocktailName;

            SqlConnection con = new SqlConnection(conStr);

            using (con)
            {
                using (SqlCommand cmd = new SqlCommand("select name from dbo.Cocktail where dbo.Cocktail.Cocktail_ID = @ID;", con))
                {

                    cmd.Parameters.AddWithValue("@ID", cocktailID);

                    try
                    {
                        await con.OpenAsync();
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        if (await reader.ReadAsync())
                        {
                            cocktailName = reader.GetString(0);
                            reader.Close();
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

                using (SqlCommand command = new SqlCommand(@"select dbo.Ingredient.Ingredient_ID, dbo.Ingredient.Name, Color
                                         from dbo.Cocktail join dbo.Cocktail_Ingredient_Junction on (dbo.Cocktail.Cocktail_ID = dbo.Cocktail_Ingredient_Junction.Cocktail_ID)
                                         join dbo.Ingredient on (dbo.Cocktail_Ingredient_Junction.Ingredient_ID = dbo.Ingredient.Ingredient_ID) where dbo.Cocktail.Cocktail_ID = @ID;", con))
                {
                    command.Parameters.AddWithValue("@ID", cocktailID);

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

                    return new CocktailIngredients(cocktailName, ingredientList);
                }
            }
            
        }
        
        public async Task<CocktailIngredient> AddCocktailIngredientAsync(Guid cocktailID, Guid ingredientID)
        {
            int cocktailCount;
            cocktailCount = await CocktailCountAsync(cocktailID);
            if (cocktailCount == 0)
                throw new Exception("No cocktail with such ID.");

            var ingredientRepository = new IngredientRepository();
            int ingredientCount;
            ingredientCount = await ingredientRepository.IngredientCountAsync(ingredientID);
            if (ingredientCount == 0)
                throw new Exception("No elements with such ID.");

            SqlConnection con = new SqlConnection(conStr);

            using (con)
            {
                using (SqlCommand cmd = new SqlCommand("insert into dbo.Cocktail_Ingredient_Junction (Cocktail_ID, Ingredient_ID) values (@cocktailID, @ingredientID);", con))
                {
                    cmd.Parameters.AddWithValue("@cocktailID", cocktailID);
                    cmd.Parameters.AddWithValue("@ingredientID", ingredientID);

                    try
                    {
                        await con.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        return new CocktailIngredient(await GetOneCocktailAsync(cocktailID), await ingredientRepository.GetOneIngredientAsync(ingredientID));
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
