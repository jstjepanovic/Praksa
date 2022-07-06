using Cocktail.Model.Common;

namespace Cocktail.Model
{
    public class Ingredient : IIngredient
    {
        public System.Guid IngredientID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public Ingredient(System.Guid guid, string name, string color)
        {
            this.IngredientID = guid;
            this.Name = name;
            this.Color = color;
        }

        public Ingredient(string name, string color)
        {
            this.Name = name;
            this.Color = color;
        }
    }
}
