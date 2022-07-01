using Cocktail.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Cocktail.Repository
{
    public class CocktailRepository
    {
        public string conStr = "Data Source=DESKTOP-RDKIF3O\\SQLEXPRESS;Initial Catalog=PraksaDB;Integrated Security=True"; // connection string
        public int CocktailCount(Guid cocktailID)
        {
            SqlConnection con = new SqlConnection(conStr);
            using (con)
            {
                using (SqlCommand count = new SqlCommand("SELECT COUNT(*) from dbo.Cocktail where dbo.Cocktail.Cocktail_ID = @ID", con))
                {
                    int cocktailCount;
                    try
                    {
                        con.Open();
                        count.Parameters.AddWithValue("@ID", cocktailID);
                        cocktailCount = (int)count.ExecuteScalar();
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

        public List<CocktailDB> GetAllCocktails()
        {
            SqlConnection con = new SqlConnection(conStr);
            List<CocktailDB> cocktailList = new List<CocktailDB>();
            using (con)
            {
                SqlCommand command = new SqlCommand("select * from dbo.Cocktail;", con);
                con.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                        cocktailList.Add(new CocktailDB(reader.GetGuid(0), reader.GetString(1), reader.GetDouble(2)));
                }
                else
                {
                    reader.Close();
                    throw new Exception("Empty table.");
                }
                reader.Close();

                return cocktailList;
            }
        }

        public CocktailDB GetOneCocktail(Guid cocktailID)
        {
            int cocktailCount;
            cocktailCount = CocktailCount(cocktailID);

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
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
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

        public CocktailDB AddCocktail(CocktailDB cocktail)
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
                        con.Open();
                        cmd.ExecuteNonQuery();
                        return new CocktailDB(cocktailId, cocktail.Name, cocktail.Price);
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("Sql command error.");
                    }

                }
            }
        }

        public CocktailDB UpdateCocktail(Guid cocktailID, CocktailDB cocktail)
        {
            int cocktailCount;
            cocktailCount = CocktailCount(cocktailID);

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
                        con.Open();
                        cmd.ExecuteNonQuery();
                        return new CocktailDB(cocktailID, cocktail.Name, cocktail.Price);
                    }
                    catch (SqlException e)
                    {
                        throw new Exception("Sql command error.");
                    }
                }
            }
        }

        public void DeleteCocktail(Guid cocktailID)
        {
            int cocktailCount;
            cocktailCount = CocktailCount(cocktailID);

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

        public CocktailIngredients AllCocktailIngredients(Guid cocktailID)
        {
            int cocktailCount;
            cocktailCount = CocktailCount(cocktailID);

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
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
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

                    return new CocktailIngredients(cocktailName, ingredientList);
                }
            }
            
        }

        public Tuple<CocktailDB, Ingredient> AddCocktailIngredient(Guid cocktailID, Guid ingredientID) // ostalo od prije nego što smo radili domain klase
        {
            int cocktailCount;
            cocktailCount = CocktailCount(cocktailID);
            if (cocktailCount == 0)
                throw new Exception("No cocktail with such ID.");

            var ingredientRepository = new IngredientRepository();
            int ingredientCount;
            ingredientCount = ingredientRepository.IngredientCount(ingredientID);
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
                        con.Open();
                        cmd.ExecuteNonQuery();
                        return new Tuple<CocktailDB, Ingredient>((GetOneCocktail(cocktailID)), ingredientRepository.GetOneIngredient(ingredientID));
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
