using Cocktail.Model.Common;

namespace Cocktail.Model
{
    public class CocktailIngredient : ICocktailIngredient
    {
        public System.Guid CocktailID { get; set; }
        public string CocktailName { get; set; }
        public double Price { get; set; }
        public System.Guid IngredientID { get; set; }
        public string IngredientName { get; set; }
        public string Color { get; set; }

        public CocktailIngredient(System.Guid cocktailId, string cocktailName, double price, System.Guid ingredientId, string ingredientName, string color)
        {
            this.CocktailID = cocktailId;
            this.CocktailName = cocktailName;
            this.Price = price;
            this.IngredientID = ingredientId;
            this.IngredientName = ingredientName;
            this.Color = color;
        }

        public CocktailIngredient(CocktailDB cocktail, Ingredient ingredient)
        {
            this.CocktailID = cocktail.CocktailID;
            this.CocktailName = cocktail.Name;
            this.Price = cocktail.Price;
            this.IngredientID = ingredient.IngredientID;
            this.IngredientName = ingredient.Name;
            this.Color = ingredient.Color;
        }
    }
}
