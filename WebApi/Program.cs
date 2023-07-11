using Aplicacion.Contratos;
using Aplicacion.Cursos;
using Aplicacion.ExcepcionMiddleware;
using Aplicacion.ServiceExtencions;
using Azure.Core.GeoJson;
using Dominio.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Persistencia;
using Persistencia.Context;
using Seguridad.TokenSeguridad;
using System;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddDbContext<CursosOnlineContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetCursoQuery.GetCursoQueryHandler).GetTypeInfo().Assembly));
builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

builder.Services.AddScoped<IJwtGenerador, JwtGenerador>();


var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true, // cualquier tipo de request de un cliente tiene que ser validado directamente por la logica que se puso en el token 
        IssuerSigningKey = key,
        ValidateAudience = false,
        ValidateIssuer   = false,
    };

});

builder.Services.ConfigureIdentity();



builder.Services.AddCors(opt => {
        opt.AddPolicy(name: myAllowSpecificOrigins,
            builder => {
                builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
    });


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCursosComand>();



builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ManagerMidleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



using (var ambiente = app.Services.CreateScope())
{
    var services = ambiente.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<CursosOnlineContext>();
        await context.Database.MigrateAsync();

        var userManager = services.GetRequiredService<UserManager<Usuario>>();
        await DataPrueba.InsertarData(context,userManager);
    }
    catch (Exception e)
    {
        var loggin = services.GetRequiredService<ILogger<Program>>();
        loggin.LogError(e, "Ocurrío un error en la migración");


    }
}




app.Run();
