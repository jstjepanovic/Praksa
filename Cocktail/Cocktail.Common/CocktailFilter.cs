namespace Cocktail.Common
{
    public class CocktailFilter
    {
        public string? NameSearch { get; set; }
        public double? PriceLower { get; set; }
        public double? PriceUpper { get; set; }

        public CocktailFilter(string? nameSearch, double? priceLower, double? priceUpper)
        {
            NameSearch = nameSearch;
            PriceLower = priceLower;
            PriceUpper = priceUpper;
        }
    }
}
