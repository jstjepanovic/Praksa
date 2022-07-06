namespace Cocktail.Common
{
    public class IngredientFilter
    {
        public string? NameSearch { get; set; }
        public string? ColorSearch { get; set; }

        public IngredientFilter(string? nameSearch, string? colorSearch)
        {
            NameSearch = nameSearch;
            ColorSearch = colorSearch;
        }
    }
}
