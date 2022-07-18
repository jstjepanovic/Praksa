namespace Cocktail.Model.Common
{
    public interface ICocktailIngredient
    {
        System.Guid CocktailID { get; set; }
        string CocktailName { get; set; }
        double Price { get; set; }
        System.Guid IngredientID { get; set; }
        string IngredientName { get; set; }
        string Color { get; set; }
    }
}
