using System;
using Cocktail.Model;
using Cocktail.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocktail.Service
{
    public class CocktailService
    {
        public async Task<List<CocktailDB>> GetAllCocktailsAsync()
        {
            var cocktailRepository = new CocktailRepository();
            return await cocktailRepository.GetAllCocktailsAsync();
        }

        public async Task<CocktailDB> GetOneCocktailAsync(Guid cocktailID)
        {
            var cocktailRepository = new CocktailRepository();
            return await cocktailRepository.GetOneCocktailAsync(cocktailID);
        }

        public async Task<CocktailDB> AddCocktailAsync(CocktailDB cocktail)
        {
            var cocktailRepository = new CocktailRepository();
            return await cocktailRepository.AddCocktailAsync(cocktail);
        }

        public async Task<CocktailDB> UpdateCocktailAsync(Guid cocktailID, CocktailDB cocktail)
        {
            var cocktailRepository = new CocktailRepository();
            return await cocktailRepository.UpdateCocktailAsync(cocktailID, cocktail);
        }

        public async Task DeleteCocktailAsync(Guid cocktailID)
        {
            var cocktailRepository = new CocktailRepository();
            await cocktailRepository.DeleteCocktailAsync(cocktailID);
        }

        public async Task<CocktailIngredients> AllCocktailIngredientsAsync(Guid cocktailID)
        {
            var cocktailRepository = new CocktailRepository();
            return await cocktailRepository.AllCocktailIngredientsAsync(cocktailID);
        }
        
        public async Task<CocktailIngredient> AddCocktailIngredientAsync(Guid cocktailID, Guid ingredientID)
        {
            var cocktailRepository = new CocktailRepository();
            return await cocktailRepository.AddCocktailIngredientAsync(cocktailID, ingredientID);
        }
        
    }
}
