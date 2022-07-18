using Cocktail.Common;
using Cocktail.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocktail.Service.Common
{
    public interface IIngredientService
    {
        Task<List<Ingredient>> GetAllIngredientsAsync(Paging paging, Sorting sorting, IngredientFilter filter);
        Task<Ingredient> GetOneIngredientAsync(Guid ingredientID);
        Task<Ingredient> AddIngredientAsync(Ingredient ingredient);
        Task<Ingredient> UpdateIngredientAsync(Guid ingredientID, Ingredient Ingredient);
        Task DeleteIngredientAsync(Guid ingredientID);
        
    }
}
