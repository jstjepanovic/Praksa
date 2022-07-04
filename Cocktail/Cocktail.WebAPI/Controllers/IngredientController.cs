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
    public class IngredientController : ApiController
    {
        // GET ALL
        [HttpGet]
        [Route("get_all_ingredients")]
        public async Task<HttpResponseMessage> GetAllIngredientsAsync()
        {
            var ingredientService = new IngredientService();
            var allIngredients = await ingredientService.GetAllIngredientsAsync();
            var allIngredientsRest = new List<IngredientRest>();
            foreach (var ingredient in allIngredients)
                allIngredientsRest.Add(new IngredientRest(ingredient.Name, ingredient.Color));
            return Request.CreateResponse(HttpStatusCode.OK, allIngredientsRest);
        }

        // GET ONE
        [HttpGet]
        [Route("get_one_ingredient")]
        public async Task<HttpResponseMessage> GetOneIngredientAsync(Guid ingredientID)
        {
            var ingredientService = new IngredientService();
            var ingredient = await ingredientService.GetOneIngredientAsync(ingredientID);
            return Request.CreateResponse(HttpStatusCode.OK, ingredient);
        }

        // POST 
        [HttpPost]
        [Route("add_ingredient")]
        public async Task<HttpResponseMessage> AddIngredientAsync(IngredientRest ingredientCreate)
        {
            var ingredient = new Ingredient(ingredientCreate.Name, ingredientCreate.Color);
            var ingredientService = new IngredientService();
            var newIngredient = await ingredientService.AddIngredientAsync(ingredient);
            return Request.CreateResponse(HttpStatusCode.OK, newIngredient);
        }

        // PUT
        [HttpPut]
        [Route("update_ingredient")]
        public async Task<HttpResponseMessage> UpdateAsync([FromUri] Guid ingredientID, [FromBody] Ingredient ingredient)
        {
            var ingredientService = new IngredientService();
            var newCocktail = await ingredientService.UpdateIngredientAsync(ingredientID, ingredient);
            return Request.CreateResponse(HttpStatusCode.OK, newCocktail);
        }

        // DELETE 
        [HttpDelete]
        [Route("delete_ingredient")]
        public async Task<HttpResponseMessage> DeleteAsync(Guid ingredientID)
        {
            var ingredientService = new IngredientService();
            await ingredientService.DeleteIngredientAsync(ingredientID);
            return Request.CreateResponse(HttpStatusCode.OK, "Ingredient deleted.");
        }
    }
}
