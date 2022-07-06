namespace Cocktail.Model.Common
{
    public interface ICocktailDB
    {
        System.Guid CocktailID { get; set; }
        string Name { get; set; }
        double Price { get; set; }
    }
}
