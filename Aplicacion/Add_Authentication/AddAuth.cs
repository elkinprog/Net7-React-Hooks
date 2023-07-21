using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Aplicacion.Add_Authentication
{
    public static class AddAuth
    {
       
        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            var build = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));

                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, // cualquier tipo de request de un cliente tiene que ser validado directamente por la logica que se puso en el token 
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };
            });


        }


    }

}




