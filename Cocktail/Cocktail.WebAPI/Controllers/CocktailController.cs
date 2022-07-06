using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Cocktail.Common;
using Cocktail.Model;
using Cocktail.Service.Common;
using Cocktail.WebAPI.Models;

namespace Cocktail.WebAPI.Controllers
{
    public class CocktailController : ApiController
    {
        protected ICocktailService CocktailService;

        public CocktailController(ICocktailService cocktailService)
        {
            this.CocktailService = cocktailService;
        }

        // GET ALL
        [HttpGet]
        [Route("get_all_cocktails")]
        public async Task<HttpResponseMessage> GetAllCocktailsAsync(int rpp = 5,
                                                                    int pageNumber = 1,
                                                                    string orderBy = "Name",
                                                                    string sortOrder = "asc",
                                                                    string? nameSearch = null,
                                                                    double? priceLower = null,
                                                                    double? priceUpper = null)
        {
            var allCocktails = await CocktailService.GetAllCocktailsAsync(new Paging(rpp, pageNumber), new Sorting(orderBy, sortOrder), new CocktailFilter(nameSearch, priceLower, priceUpper));
            if (allCocktails.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "No such elements");
            }
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
            var cocktail = await CocktailService.GetOneCocktailAsync(cocktailID);
            var cocktailRest = new CocktailRest(cocktail.Name, cocktail.Price);
            return Request.CreateResponse(HttpStatusCode.OK, cocktailRest);
        }

        // POST 
        [HttpPost]
        [Route("add_cocktail")]
        public async Task<HttpResponseMessage> AddCocktailAsync(CocktailRest cocktailCreate)
        {
            var cocktail = new CocktailDB(cocktailCreate.Name, cocktailCreate.Price);
            var newCocktail = await CocktailService.AddCocktailAsync(cocktail);
            return Request.CreateResponse(HttpStatusCode.OK, newCocktail);
        }

        // PUT
        [HttpPut]
        [Route("update_cocktail")]
        public async Task<HttpResponseMessage> UpdateCocktailAsync([FromUri]Guid cocktailID, [FromBody]CocktailDB cocktail)
        {
            var newCocktail = await CocktailService.UpdateCocktailAsync(cocktailID, cocktail);
            return Request.CreateResponse(HttpStatusCode.OK, newCocktail);
        }

        // DELETE 
        [HttpDelete]
        [Route("delete_cocktail")]
        public async Task<HttpResponseMessage> DeleteCocktailAsync(Guid cocktailID)
        {
            await CocktailService.DeleteCocktailAsync(cocktailID);
            return Request.CreateResponse(HttpStatusCode.OK, "Cocktail deleted.");
        }

        [HttpGet]
        [Route("get_all_cocktail_ingredients")]
        public async Task<HttpResponseMessage> AllCocktailIngredientsAsync(Guid cocktailID)
        {
            var cocktailIngredients = await CocktailService.AllCocktailIngredientsAsync(cocktailID);
            return Request.CreateResponse(HttpStatusCode.OK, cocktailIngredients);
        }
        
        [HttpPost]
        [Route("add_cocktail_ingredient")]
        public async Task<HttpResponseMessage> AddCocktailIngredientAsync(Guid cocktailID, Guid ingredientID)
        {
            var cocktailIngredient = await CocktailService.AddCocktailIngredientAsync(cocktailID, ingredientID);
            return Request.CreateResponse(HttpStatusCode.OK, cocktailIngredient);
        }
        
    }
}