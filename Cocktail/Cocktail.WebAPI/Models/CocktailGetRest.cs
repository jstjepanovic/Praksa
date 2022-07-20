namespace Cocktail.WebAPI.Models
{
    public class CocktailGetRest
    {
        public System.Guid CocktailID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public CocktailGetRest(System.Guid guid, string name, double price)
        {
            this.CocktailID = guid;
            this.Name = name;
            this.Price = price;
        }
    }
}