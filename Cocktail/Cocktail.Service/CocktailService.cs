using System;
using Cocktail.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cocktail.Service.Common;
using Cocktail.Repository.Common;
using Cocktail.Common;

namespace Cocktail.Service
{
    public class CocktailService : ICocktailService
    {
        protected ICocktailRepository CocktailRepository;

        public CocktailService(ICocktailRepository cocktailRepository)
        {
            this.CocktailRepository = cocktailRepository;
        }
        public async Task<List<CocktailDB>> GetAllCocktailsAsync(Paging paging, Sorting sorting, CocktailFilter filter)
        {
            return await CocktailRepository.GetAllCocktailsAsync(paging, sorting, filter);
        }

        public async Task<CocktailDB> GetOneCocktailAsync(Guid cocktailID)
        {
            return await CocktailRepository.GetOneCocktailAsync(cocktailID);
        }

        public async Task<CocktailDB> AddCocktailAsync(CocktailDB cocktail)
        {
            return await CocktailRepository.AddCocktailAsync(cocktail);
        }

        public async Task<CocktailDB> UpdateCocktailAsync(Guid cocktailID, CocktailDB cocktail)
        {
            return await CocktailRepository.UpdateCocktailAsync(cocktailID, cocktail);
        }

        public async Task DeleteCocktailAsync(Guid cocktailID)
        {
            await CocktailRepository.DeleteCocktailAsync(cocktailID);
        }

        public async Task<CocktailIngredients> AllCocktailIngredientsAsync(Guid cocktailID)
        {
            return await CocktailRepository.AllCocktailIngredientsAsync(cocktailID);
        }
        
        public async Task<CocktailIngredient> AddCocktailIngredientAsync(Guid cocktailID, Guid ingredientID)
        {
            return await CocktailRepository.AddCocktailIngredientAsync(cocktailID, ingredientID);
        }
        
    }
}
