namespace Cocktail.WebAPI.Models
{
    public class CocktailCreateRest
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public CocktailCreateRest(string name, double price)
        {
            this.Name = name;
            this.Price = price;
        }
    }
}