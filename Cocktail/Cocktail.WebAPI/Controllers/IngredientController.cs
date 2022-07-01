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
    public class IngredientController : ApiController
    {
        // GET ALL
        [HttpGet]
        [Route("get_all_ingredients")]
        public HttpResponseMessage GetAllIngredients()
        {
            var ingredientService = new IngredientService();
            var allIngredients = ingredientService.GetAllIngredients();
            var allIngredientsRest = new List<IngredientRest>();
            foreach (var ingredient in allIngredients)
                allIngredientsRest.Add(new IngredientRest(ingredient.Name, ingredient.Color));
            return Request.CreateResponse(HttpStatusCode.OK, allIngredientsRest);
        }

        // GET ONE
        [HttpGet]
        [Route("get_one_ingredient")]
        public HttpResponseMessage GetOneIngredient(Guid ingredientID)
        {
            var ingredientService = new IngredientService();
            var ingredient = ingredientService.GetOneIngredient(ingredientID);
            return Request.CreateResponse(HttpStatusCode.OK, ingredient);
        }

        // POST 
        [HttpPost]
        [Route("add_ingredient")]
        public HttpResponseMessage AddIngredient(IngredientRest ingredientCreate)
        {
            var ingredient = new Ingredient(ingredientCreate.Name, ingredientCreate.Color);
            var ingredientService = new IngredientService();
            var newIngredient = ingredientService.AddIngredient(ingredient);
            return Request.CreateResponse(HttpStatusCode.OK, newIngredient);
        }

        // PUT
        [HttpPut]
        [Route("update_ingredient")]
        public HttpResponseMessage Update([FromUri] Guid ingredientID, [FromBody] Ingredient ingredient)
        {
            var ingredientService = new IngredientService();
            var newCocktail = ingredientService.UpdateIngredient(ingredientID, ingredient);
            return Request.CreateResponse(HttpStatusCode.OK, newCocktail);
        }

        // DELETE 
        [HttpDelete]
        [Route("delete_ingredient")]
        public HttpResponseMessage Delete(Guid ingredientID)
        {
            var ingredientService = new IngredientService();
            ingredientService.DeleteIngredient(ingredientID);
            return Request.CreateResponse(HttpStatusCode.OK, "Ingredient deleted.");
        }
    }
}
