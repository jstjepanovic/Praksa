using System.Collections.Generic;

namespace Cocktail.WebAPI.Models
{
    public class CocktailSetup
    {
        static List<CocktailClass> Cocktails { get; }
        static int nextId = 1;
        static CocktailSetup()
        {
            Cocktails = new List<CocktailClass> {};
        }

        public static List<CocktailClass> GetAll() => Cocktails;

        public static CocktailClass Get(int id)
        {
            foreach (var c in Cocktails)
                if (id == c.Id)
                    return c;
            return null;
        }

        public static void Add(CocktailClass cocktail)
        {
            cocktail.Id = nextId++;
            Cocktails.Add(cocktail);
        }

        public static void Delete(int id)
        {
            var cocktail = Get(id);

            if (cocktail is null)
                return;

            Cocktails.Remove(cocktail);
        }

        public static void Update(CocktailClass cocktail)
        {
            var idx = Cocktails.FindIndex(c => c.Id == cocktail.Id);
            if (idx == -1)
                return;

            Cocktails[idx] = cocktail;
        }

    }
}