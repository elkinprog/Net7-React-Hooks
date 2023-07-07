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

        }
    }
}
