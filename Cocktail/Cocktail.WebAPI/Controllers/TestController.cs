using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cocktail.WebAPI.Models;

namespace Cocktail.WebAPI.Controllers
{
    public class TestController : ApiController
    {
        // GET all
        [HttpGet]
        [Route("get_all")]
        public HttpResponseMessage GetAll()
        {
            return Request.CreateResponse(HttpStatusCode.OK, CocktailSetup.GetAll());
        }

        // GET one
        [HttpGet]
        [Route("get_one_coc")]
        public HttpResponseMessage Get(int id)
        {
            var cocktail = CocktailSetup.Get(id);

            if (cocktail is null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Cocktail non existent.");

            return Request.CreateResponse(HttpStatusCode.OK, cocktail);
        }

        // POST 
        [HttpPost]
        [Route("add_coc")]
        public HttpResponseMessage Post(CocktailClass cocktail)
        {
            CocktailSetup.Add(cocktail);
            return Request.CreateResponse(HttpStatusCode.OK, cocktail);
        }

        // PUT 
        [HttpPut]
        [Route("update_coc")]
        public HttpResponseMessage Update(int id, CocktailClass cocktail)
        {
            if (id != cocktail.Id)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Query ID differentiates from body ID.");

            var existingCocktail = CocktailSetup.Get(id);
            if (existingCocktail is null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Cocktail non existent.");

            CocktailSetup.Update(cocktail);

            return Request.CreateResponse(HttpStatusCode.NoContent, cocktail);
        }

        // DELETE 
        [HttpDelete]
        [Route("delete_coc")]
        public HttpResponseMessage Delete(int id)
        {
            var cocktail = CocktailSetup.Get(id);

            if (cocktail is null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Cocktail non existent.");

            CocktailSetup.Delete(id);

            return Request.CreateResponse(HttpStatusCode.NoContent, "Successful removal.");
        }
    }
}
