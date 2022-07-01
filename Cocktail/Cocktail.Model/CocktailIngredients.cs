using System.Collections.Generic;

namespace Cocktail.Model
{
    public class CocktailIngredients
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public CocktailIngredients(string name, List<Ingredient> ingredients)
        {
            this.Name = name;
            this.Ingredients = ingredients;
        }
    }
}
