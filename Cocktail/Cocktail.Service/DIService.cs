using Autofac;
using Cocktail.Service.Common;

namespace Cocktail.Service
{
    public class DIService : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CocktailService>().As<ICocktailService>();
            builder.RegisterType<IngredientService>().As<IIngredientService>();
            base.Load(builder);
        }
    }
}
