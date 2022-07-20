using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cocktail.WebAPI.Models
{
    public class IngredientCreateRest
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public IngredientCreateRest(string name, string color)
        {
            this.Name = name;
            this.Color = color;
        }
    }
}