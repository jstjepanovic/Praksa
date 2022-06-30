
namespace Cocktail.WebAPI.Models
{
    public class CocktailDB
    {
        public System.Guid CocktailID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public CocktailDB(System.Guid guid, string name, double price)
        {
            this.CocktailID = guid;
            this.Name = name;
            this.Price = price;
        }
    }
}