using System;
using System.Collections.Generic;
using Cocktail.Repository;
using Cocktail.Model;
using System.Threading.Tasks;
using Cocktail.Service.Common;
using Cocktail.Repository.Common;
using Cocktail.Common;

namespace Cocktail.Service
{
    public class IngredientService : IIngredientService
    {
        protected IIngredientRepository IngredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            this.IngredientRepository = ingredientRepository;
        }
        public async Task<List<Ingredient>> GetAllIngredientsAsync(Paging paging, Sorting sorting, IngredientFilter filter)
        {
            var ingredientRepository = new IngredientRepository();
            return await ingredientRepository.GetAllIngredientsAsync(paging, sorting, filter);
        }

        public async Task<Ingredient> GetOneIngredientAsync(Guid ingredientID)
        {
            var ingredientRepository = new IngredientRepository();
            return await ingredientRepository.GetOneIngredientAsync(ingredientID);
        }

        public async Task<Ingredient> AddIngredientAsync(Ingredient ingredient)
        {
            var ingredientRepository = new IngredientRepository();
            return await ingredientRepository.AddIngredientAsync(ingredient);
        }

        public async Task<Ingredient> UpdateIngredientAsync(Guid ingredientID, Ingredient Ingredient)
        {
            var ingredientRepository = new IngredientRepository();
            return await ingredientRepository.UpdateIngredientAsync(ingredientID, Ingredient);
        }

        public async Task DeleteIngredientAsync(Guid ingredientID)
        {
            var IngredientRepository = new IngredientRepository();
            await IngredientRepository.DeleteIngredientAsync(ingredientID);
        }
    }
}
