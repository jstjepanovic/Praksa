using System;
using System.Collections.Generic;
using Cocktail.Repository;
using Cocktail.Model;

namespace Cocktail.Service
{
    public class IngredientService
    {
        public List<Ingredient> GetAllIngredients()
        {
            var ingredientRepository = new IngredientRepository();
            return ingredientRepository.GetAllIngredients();
        }

        public Ingredient GetOneIngredient(Guid ingredientID)
        {
            var ingredientRepository = new IngredientRepository();
            return ingredientRepository.GetOneIngredient(ingredientID);
        }

        public Ingredient AddIngredient(Ingredient ingredient)
        {
            var ingredientRepository = new IngredientRepository();
            return ingredientRepository.AddIngredient(ingredient);
        }

        public Ingredient UpdateIngredient(Guid ingredientID, Ingredient Ingredient)
        {
            var ingredientRepository = new IngredientRepository();
            return ingredientRepository.UpdateIngredient(ingredientID, Ingredient);
        }

        public void DeleteIngredient(Guid ingredientID)
        {
            var IngredientRepository = new IngredientRepository();
            IngredientRepository.DeleteIngredient(ingredientID);
        }
    }
}
