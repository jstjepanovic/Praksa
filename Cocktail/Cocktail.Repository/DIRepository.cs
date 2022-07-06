using Autofac;
using Cocktail.Repository.Common;

namespace Cocktail.Repository
{
    public class DIRepository : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CocktailRepository>().As<ICocktailRepository>();
            builder.RegisterType<IngredientRepository>().As<IIngredientRepository>();
            base.Load(builder);
        }
    }
}
