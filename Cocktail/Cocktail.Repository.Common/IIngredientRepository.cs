using Cocktail.Common;
using Cocktail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cocktail.Repository.Common
{
    public interface IIngredientRepository
    {
        Task<List<Ingredient>> GetAllIngredientsAsync(Paging paging, Sorting sorting, IngredientFilter filter);
        Task<Ingredient> GetOneIngredientAsync(Guid ingredientID);
        Task<Ingredient> AddIngredientAsync(Ingredient ingredient);
        Task<Ingredient> UpdateIngredientAsync(Guid ingredientID, Ingredient ingredient);
        Task DeleteIngredientAsync(Guid ingredientID);

    }
}
