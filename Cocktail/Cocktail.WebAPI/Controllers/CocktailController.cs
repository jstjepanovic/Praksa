using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
        public HttpResponseMessage GetAllCocktails()
        {
            var cocktailService = new CocktailService();
            var allCocktails = cocktailService.GetAllCocktails();
            var allCocktailsRest = new List<CocktailRest>();
            foreach (var cocktail in allCocktails)
                allCocktailsRest.Add(new CocktailRest(cocktail.Name, cocktail.Price));
            return Request.CreateResponse(HttpStatusCode.OK, allCocktailsRest);
        }

        // GET ONE
        [HttpGet]
        [Route("get_one_cocktail")]
        public HttpResponseMessage GetOneCocktail(Guid cocktailID)
        {
            var cocktailService = new CocktailService();
            var cocktail = cocktailService.GetOneCocktail(cocktailID);
            return Request.CreateResponse(HttpStatusCode.OK, cocktail);
        }

        // POST 
        [HttpPost]
        [Route("add_cocktail")]
        public HttpResponseMessage AddCocktail(CocktailRest cocktailCreate)
        {
            var cocktail = new CocktailDB(cocktailCreate.Name, cocktailCreate.Price);
            var cocktailService = new CocktailService();
            var newCocktail = cocktailService.AddCocktail(cocktail);
            return Request.CreateResponse(HttpStatusCode.OK, newCocktail);
        }

        // PUT
        [HttpPut]
        [Route("update_cocktail")]
        public HttpResponseMessage UpdateCocktail([FromUri]Guid cocktailID, [FromBody]CocktailDB cocktail)
        {
            var cocktailService = new CocktailService();
            var newCocktail = cocktailService.UpdateCocktail(cocktailID, cocktail);
            return Request.CreateResponse(HttpStatusCode.OK, newCocktail);
        }

        // DELETE 
        [HttpDelete]
        [Route("delete_cocktail")]
        public HttpResponseMessage DeleteCocktail(Guid cocktailID)
        {
            var cocktailService = new CocktailService();
            cocktailService.DeleteCocktail(cocktailID);
            return Request.CreateResponse(HttpStatusCode.OK, "Cocktail deleted.");
        }

        [HttpGet]
        [Route("get_all_cocktail_ingredients")]
        public HttpResponseMessage AllCocktailIngredients(Guid cocktailID)
        {
            var cocktailService = new CocktailService();
            var cocktailIngredients = cocktailService.AllCocktailIngredients(cocktailID);
            return Request.CreateResponse(HttpStatusCode.OK, cocktailIngredients);
        }

        [HttpPost]
        [Route("add_cocktail_ingredient")]
        public HttpResponseMessage AddCocktailIngredient(Guid cocktailID, Guid ingredientID)
        {
            var cocktailService = new CocktailService();
            var cocktailIngredient = cocktailService.AddCocktailIngredient(cocktailID, ingredientID);
            return Request.CreateResponse(HttpStatusCode.OK, cocktailIngredient);
        }
    }
}