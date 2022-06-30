using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using Cocktail.WebAPI.Models;
using System.Collections.Generic;
using System.Data;

namespace Cocktail.WebAPI.Controllers
{
    public class CocktailController : ApiController
    {
        public string conStr = "Data Source=DESKTOP-RDKIF3O\\SQLEXPRESS;Initial Catalog=PraksaDB;Integrated Security=True"; // connection string
        
        // GET ALL
        [HttpGet]
        [Route("get_all_cocktails")]
        public HttpResponseMessage GetAll()
        {
            SqlConnection con = new SqlConnection(conStr);
            List<CocktailDB> ret = new List<CocktailDB>();
            using (con)
            {
                SqlCommand command = new SqlCommand("select * from dbo.Cocktail;", con);
                con.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                        ret.Add(new CocktailDB(reader.GetGuid(0), reader.GetString(1), reader.GetDouble(2)));
                }else
                {
                    reader.Close();
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Cocktails non existent.");
                }
                reader.Close();

                return Request.CreateResponse(HttpStatusCode.OK, ret);
            }
        }

        // GET ONE
        [HttpGet]
        [Route("get_one_cocktail")]
        public HttpResponseMessage GetOne(System.Guid cocktailID)
        {
            SqlConnection con = new SqlConnection(conStr);
            using (con)
            {
                CocktailDB ret;
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
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Unsuccessful sql.");
                    }

                    if (cocktailCount == 0)
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Cocktail non existent.");
                }

                using (SqlCommand cmd = new SqlCommand("select * from dbo.Cocktail where dbo.Cocktail.Cocktail_ID = @ID;", con))
                {

                    cmd.Parameters.AddWithValue("@ID", cocktailID);

                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            ret = new CocktailDB(reader.GetGuid(0), reader.GetString(1), reader.GetDouble(2));
                            return Request.CreateResponse(HttpStatusCode.OK, ret);
                        }
                        else { return Request.CreateResponse(HttpStatusCode.NotFound, "Can not read."); }
                    }
                    catch (SqlException e)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Unsuccessful sql.");
                    }
                }

                
            }
        }

        // POST 
        [HttpPost]
        [Route("add_cocktail")]
        public HttpResponseMessage Add(CocktailDB cocktail)
        {
            SqlConnection con = new SqlConnection(conStr);
            System.Guid g = System.Guid.NewGuid();
            using (con)
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"INSERT INTO dbo.Cocktail(Cocktail_ID, Name, Price) 
                            VALUES(@param1,@param2,@param3)";

                    cmd.Parameters.AddWithValue("@param1", g);
                    cmd.Parameters.AddWithValue("@param2", cocktail.Name);
                    cmd.Parameters.AddWithValue("@param3", cocktail.Price);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        return Request.CreateResponse(HttpStatusCode.OK, "Cocktail added.");
                    }
                    catch (SqlException e)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Unsuccessful insert.");
                    }

                }
            }
        }

        // PUT
        [HttpPut]
        [Route("update_cocktail")]
        public HttpResponseMessage Update([FromUri]System.Guid cocktailID, [FromBody]CocktailDB cocktail)
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
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Unsuccessful sql.");
                    }

                    if (cocktailCount == 0)
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Cocktail non existent.");
                }

                using (SqlCommand cmd = new SqlCommand("update dbo.Cocktail set dbo.Cocktail.Name = @name, dbo.Cocktail.Price = @price where dbo.Cocktail.Cocktail_ID = @ID", con))
                    {

                        cmd.Parameters.AddWithValue("@ID", cocktailID);
                        cmd.Parameters.AddWithValue("@name", cocktail.Name);
                        cmd.Parameters.AddWithValue("@price", cocktail.Price);

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            return Request.CreateResponse(HttpStatusCode.OK, "Cocktail updated.");
                        }
                        catch (SqlException e)
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "Unsuccessful update.");
                        }
                    }
            }
        }

        // DELETE 
        [HttpDelete]
        [Route("delete_cocktail")]
        public HttpResponseMessage Delete(System.Guid cocktailID)
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
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Unsuccessful sql.");
                    }

                    if (cocktailCount == 0)
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Cocktail non existent.");
                }

                using (SqlCommand cmd = new SqlCommand("delete from dbo.Cocktail where dbo.Cocktail.Cocktail_ID = @ID", con))
                    {
                        cmd.Parameters.AddWithValue("@ID", cocktailID);

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            return Request.CreateResponse(HttpStatusCode.OK, "Cocktail deleted.");
                        }
                        catch (SqlException e)
                        {
                            return Request.CreateResponse(HttpStatusCode.NotFound, "Unsuccessful delete.");
                        }
                    }
            }
        }

    }
}