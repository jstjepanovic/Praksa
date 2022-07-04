using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Cocktail.Model;
using Cocktail.Service;
using Cocktail.WebAPI.Models;

namespace Cocktail.WebAPI.Controllers
{
    public class CocktailController : ApiController
    {   
        // GET ALL
        [HttpGet]
        [Route("get_all_cocktails")]
        public async Task<HttpResponseMessage> GetAllCocktailsAsync()
        {
            var cocktailService = new CocktailService();
            var allCocktails = await cocktailService.GetAllCocktailsAsync();
            var allCocktailsRest = new List<CocktailRest>();
            foreach (var cocktail in allCocktails)
                allCocktailsRest.Add(new CocktailRest(cocktail.Name, cocktail.Price));
            return Request.CreateResponse(HttpStatusCode.OK, allCocktailsRest);
        }

        // GET ONE
        [HttpGet]
        [Route("get_one_cocktail")]
        public async Task<HttpResponseMessage> GetOneCocktailAsync(Guid cocktailID)
        {
            var cocktailService = new CocktailService();
            var cocktail = await cocktailService.GetOneCocktailAsync(cocktailID);
            return Request.CreateResponse(HttpStatusCode.OK, cocktail);
        }

        // POST 
        [HttpPost]
        [Route("add_cocktail")]
        public async Task<HttpResponseMessage> AddCocktailAsync(CocktailRest cocktailCreate)
        {
            var cocktail = new CocktailDB(cocktailCreate.Name, cocktailCreate.Price);
            var cocktailService = new CocktailService();
            var newCocktail = await cocktailService.AddCocktailAsync(cocktail);
            return Request.CreateResponse(HttpStatusCode.OK, newCocktail);
        }

        // PUT
        [HttpPut]
        [Route("update_cocktail")]
        public async Task<HttpResponseMessage> UpdateCocktailAsync([FromUri]Guid cocktailID, [FromBody]CocktailDB cocktail)
        {
            var cocktailService = new CocktailService();
            var newCocktail = await cocktailService.UpdateCocktailAsync(cocktailID, cocktail);
            return Request.CreateResponse(HttpStatusCode.OK, newCocktail);
        }

        // DELETE 
        [HttpDelete]
        [Route("delete_cocktail")]
        public async Task<HttpResponseMessage> DeleteCocktailAsync(Guid cocktailID)
        {
            var cocktailService = new CocktailService();
            await cocktailService.DeleteCocktailAsync(cocktailID);
            return Request.CreateResponse(HttpStatusCode.OK, "Cocktail deleted.");
        }

        [HttpGet]
        [Route("get_all_cocktail_ingredients")]
        public async Task<HttpResponseMessage> AllCocktailIngredientsAsync(Guid cocktailID)
        {
            var cocktailService = new CocktailService();
            var cocktailIngredients = await cocktailService.AllCocktailIngredientsAsync(cocktailID);
            return Request.CreateResponse(HttpStatusCode.OK, cocktailIngredients);
        }
        
        [HttpPost]
        [Route("add_cocktail_ingredient")]
        public async Task<HttpResponseMessage> AddCocktailIngredientAsync(Guid cocktailID, Guid ingredientID)
        {
            var cocktailService = new CocktailService();
            var cocktailIngredient = await cocktailService.AddCocktailIngredientAsync(cocktailID, ingredientID);
            return Request.CreateResponse(HttpStatusCode.OK, cocktailIngredient);
        }
        
    }
}