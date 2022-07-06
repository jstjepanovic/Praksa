namespace Cocktail.Model.Common
{
    public interface IIngredient
    {
        System.Guid IngredientID { get; set; }
        string Name { get; set; }
        string Color { get; set; }
    }
}
