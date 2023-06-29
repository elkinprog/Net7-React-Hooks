using Aplicacion.Cursos;
using Aplicacion.ExcepcionMiddleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

app.Run();
