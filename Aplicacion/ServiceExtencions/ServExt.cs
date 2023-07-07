using Dominio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Persistencia;

namespace Aplicacion.ServiceExtencions
{
    public static  class ServExt
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<Usuario>(q => q.User.RequireUniqueEmail= true);
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole),services);
            builder.AddEntityFrameworkStores<CursosOnlineContext>().AddDefaultTokenProviders();
            builder.AddSignInManager<SignInManager<Usuario>>();


            //var builder2 = builder.Services.AddIdentityCore<Usuario>();
            //var identityBuilder = new IdentityBuilder(builder2.UserType,builder.Services);
            //identityBuilder.AddEntityFrameworkStores<CursosOnlineContext>();
            //identityBuilder.AddSignInManager<SignInManager<Usuario>>();

        }
    }
}
