namespace Cocktail.Model
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

        public CocktailDB(string name, double price)
        {
            this.Name = name;
            this.Price = price;
        }
    }
}
