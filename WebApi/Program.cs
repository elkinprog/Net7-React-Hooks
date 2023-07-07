using Aplicacion.Cursos;
using Aplicacion.ExcepcionMiddleware;
using Aplicacion.ServiceExtencions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistencia;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddDbContext<CursosOnlineContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetCursoQuery.GetCursoQueryHandler).GetTypeInfo().Assembly));


builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();


//var builder2 = builder.Services.AddIdentityCore<Usuario>();
//var identityBuilder = new IdentityBuilder(builder2.UserType,builder.Services);
//identityBuilder.AddEntityFrameworkStores<CursosOnlineContext>();
//identityBuilder.AddSignInManager<SignInManager<Usuario>>();




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



builder.Services.AddControllers();
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
    }
    catch (Exception e)
    {
        var loggin = services.GetRequiredService<ILogger<Program>>();
        loggin.LogError(e, "Ocurrío un error en la migración");


    }
}




app.Run();
