using System;
using Cocktail.Model;
using Cocktail.Repository;
using System.Collections.Generic;

namespace Cocktail.Service
{
    public class CocktailService
    {
        public List<CocktailDB> GetAllCocktails()
        {
            var cocktailRepository = new CocktailRepository();
            return cocktailRepository.GetAllCocktails();
        }

        public CocktailDB GetOneCocktail(Guid cocktailID)
        {
            var cocktailRepository = new CocktailRepository();
            return cocktailRepository.GetOneCocktail(cocktailID);
        }

        public CocktailDB AddCocktail(CocktailDB cocktail)
        {
            var cocktailRepository = new CocktailRepository();
            return cocktailRepository.AddCocktail(cocktail);
        }

        public CocktailDB UpdateCocktail(Guid cocktailID, CocktailDB cocktail)
        {
            var cocktailRepository = new CocktailRepository();
            return cocktailRepository.UpdateCocktail(cocktailID, cocktail);
        }

        public void DeleteCocktail(Guid cocktailID)
        {
            var cocktailRepository = new CocktailRepository();
            cocktailRepository.DeleteCocktail(cocktailID);
        }

        public CocktailIngredients AllCocktailIngredients(Guid cocktailID)
        {
            var cocktailRepository = new CocktailRepository();
            return cocktailRepository.AllCocktailIngredients(cocktailID);
        }

        public Tuple<CocktailDB, Ingredient> AddCocktailIngredient(Guid cocktailID, Guid ingredientID)
        {
            var cocktailRepository = new CocktailRepository();
            return cocktailRepository.AddCocktailIngredient(cocktailID, ingredientID);
        }
    }
}
