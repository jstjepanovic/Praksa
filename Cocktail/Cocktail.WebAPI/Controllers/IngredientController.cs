using System;
using System.Collections.Generic;
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
    public class IngredientController : ApiController
    {
        protected IIngredientService IngredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            this.IngredientService = ingredientService;
        }

        // GET ALL
        [HttpGet]
        [Route("get_all_ingredients")]
        public async Task<HttpResponseMessage> GetAllIngredientsAsync(int rpp = 5,
                                                                    int pageNumber = 1,
                                                                    string orderBy = "Name",
                                                                    string sortOrder = "asc",
                                                                    string? nameSearch = null,
                                                                    string? colorSearch = null)
        {
            var allIngredients = await IngredientService.GetAllIngredientsAsync(new Paging(rpp, pageNumber), new Sorting(orderBy, sortOrder), new IngredientFilter(nameSearch, colorSearch));

            return Request.CreateResponse(HttpStatusCode.OK, allIngredients);
        }

        // GET ONE
        [HttpGet]
        [Route("get_one_ingredient")]
        public async Task<HttpResponseMessage> GetOneIngredientAsync(Guid ingredientID)
        {
            var ingredient = await IngredientService.GetOneIngredientAsync(ingredientID);
            return Request.CreateResponse(HttpStatusCode.OK, ingredient);
        }

        // POST 
        [HttpPost]
        [Route("add_ingredient")]
        public async Task<HttpResponseMessage> AddIngredientAsync(IngredientCreateRest ingredientCreate)
        {
            var ingredient = new Ingredient(ingredientCreate.Name, ingredientCreate.Color);
            var newIngredient = await IngredientService.AddIngredientAsync(ingredient);
            return Request.CreateResponse(HttpStatusCode.OK, newIngredient);
        }

        // PUT
        [HttpPut]
        [Route("update_ingredient")]
        public async Task<HttpResponseMessage> UpdateAsync([FromUri] Guid ingredientID, [FromBody] Ingredient ingredient)
        {
            var newCocktail = await IngredientService.UpdateIngredientAsync(ingredientID, ingredient);
            return Request.CreateResponse(HttpStatusCode.OK, newCocktail);
        }

        // DELETE 
        [HttpDelete]
        [Route("delete_ingredient")]
        public async Task<HttpResponseMessage> DeleteAsync(Guid ingredientID)
        {
            await IngredientService.DeleteIngredientAsync(ingredientID);
            return Request.CreateResponse(HttpStatusCode.OK, "Ingredient deleted.");
        }
    }
}
