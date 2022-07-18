using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cocktail.WebAPI.Models
{
    public class IngredientRest
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public IngredientRest(string name, string color)
        {
            this.Name = name;
            this.Color = color;
        }
    }
}