using System.Collections.Generic;

namespace Cocktail.WebAPI.Models
{
    public class CocktailClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }

    }
}