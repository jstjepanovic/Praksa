namespace Cocktail.WebAPI.Models
{
    public class IngredientGetRest
    {
        public System.Guid IngredientID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public IngredientGetRest(System.Guid guid, string name, string color)
        {
            this.IngredientID = guid;
            this.Name = name;
            this.Color = color;
        }
    }
}