namespace Cocktail.WebAPI.Models
{
    public class CocktailRest
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public CocktailRest(string name, double price)
        {
            this.Name = name;
            this.Price = price;
        }
    }
}