using Autofac;
using Cocktail.Model.Common;

namespace Cocktail.Model
{
    public class DIModel : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CocktailDB>().As<ICocktailDB>();
            builder.RegisterType<CocktailIngredients>().As<ICocktailIngredients>();
            builder.RegisterType<CocktailIngredient>().As<ICocktailIngredient>();
            builder.RegisterType<Ingredient>().As<IIngredient>();
            base.Load(builder);
        }
    }
}
