using Cocktail.Common;
using Cocktail.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocktail.Repository.Common
{
    public interface ICocktailRepository
    {
        Task<List<CocktailDB>> GetAllCocktailsAsync(Paging paging, Sorting sorting, CocktailFilter filter);
        Task<CocktailDB> GetOneCocktailAsync(Guid cocktailID);
        Task<CocktailDB> AddCocktailAsync(CocktailDB cocktail);
        Task<CocktailDB> UpdateCocktailAsync(Guid cocktailID, CocktailDB cocktail);
        Task DeleteCocktailAsync(Guid cocktailID);
        Task<CocktailIngredients> AllCocktailIngredientsAsync(Guid cocktailID);
        Task<CocktailIngredient> AddCocktailIngredientAsync(Guid cocktailID, Guid ingredientID);
    }
}
