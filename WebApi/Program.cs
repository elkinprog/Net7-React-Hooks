using Aplicacion.Add_Authentication;
using Aplicacion.Configure_Identity;
using Aplicacion.Contratos;
using Aplicacion.Cursos;
using Aplicacion.ExcepcionMiddleware;
using Dominio.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
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
using Persistencia;
using Persistencia.Context;
using Persistencia.DapperConexion;
using Persistencia.DapperConexion.Instructor;
using Seguridad.TokenSeguridad;
using ServiceStack;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";


#region Logica conexion DB usando Context

builder.Services.AddDbContext<CursosOnlineContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

#endregion


#region Logica conexion DB usando Dapper 

builder.Services.AddOptions();
builder.Services.Configure<ConexionConfig> (builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddTransient<IFactoryConnection, FactoryConnection>();
builder.Services.AddScoped<IInstructor, InstructorRepositorio>();

#endregion


#region Logica configuración JWT 

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetCourse.GetCursoHandler).GetTypeInfo().Assembly));
builder.Services.TryAddSingleton<ISystemClock, SystemClock>();
builder.Services.AddScoped<IJwtGenerador, JwtGenerador>();
builder.Services.AddScoped<IUsuarioSesion, UsuarioSesion>();
builder.Services.ConfigureAuthentication();
builder.Services.ConfigureIdentity();

#endregion




builder.Services.AddAutoMapper(typeof (GetCourse.GetCursoHandler));





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
builder.Services.AddValidatorsFromAssemblyContaining<CreateCourse>();



builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ManagerMidleware>();

app.UseAuthentication();

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
