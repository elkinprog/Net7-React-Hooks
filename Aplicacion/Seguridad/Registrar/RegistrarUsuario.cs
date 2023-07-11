using Aplicacion.Contratos;
using Aplicacion.ManejadorErrores;
using Dominio.Dtos;
using Dominio.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Net;
using WebApi.Responses;

namespace Aplicacion.Seguridad.Registrar
{
    public  class RegistrarUsuario
    {
        public class RegistrarUsuarioRequest:IRequest<UsuarioDto>
        {
            public string Nombre   { get; set; }
            public string Apellidos{ get; set; }
            public string Email    { get; set; }
            public string Password { get; set; }
            public string Username { get; set; }
        }

        public class RegistrarUsuarioHandle : IRequestHandler<RegistrarUsuarioRequest, UsuarioDto>
        {
            private readonly CursosOnlineContext    _context;
            private readonly UserManager<Usuario>   _userManager;
            private readonly IJwtGenerador          _jwtGenerador;


            public RegistrarUsuarioHandle(CursosOnlineContext context,UserManager<Usuario> userManager, IJwtGenerador jwtGenerador)
            {
                this._context = context;    
                this._userManager = userManager;
                this._jwtGenerador = jwtGenerador; 
            }

            public async Task<UsuarioDto> Handle(RegistrarUsuarioRequest request, CancellationToken cancellationToken)
            {
                var existe = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
                if (existe)
                {
                    throw new GenericResponse(HttpStatusCode.BadRequest, "Atención!", "Ya está registrado este correo " + request.Email);
                }

                var existeUsername = await _context.Users.Where(x => x.UserName == request.Username).AnyAsync();

                if (existeUsername)
                {
                    throw new GenericResponse(HttpStatusCode.BadRequest, "Atención!", "Ya está registrado este usuario " + request.Email);
                }

                var usuario = new Usuario
                {
                    NombreCompleto = request.Nombre + "" + request.Apellidos,
                    Email = request.Email,
                    UserName = request.Username
                };

               var resultado = await  _userManager.CreateAsync(usuario,request.Password);

                if (resultado.Succeeded)
                {
                    //throw new GenericResponse(HttpStatusCode.BadRequest, "Bien echo!", "Se registro con exito " + request.Email);
                    return new UsuarioDto
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        Token = _jwtGenerador.CrearToken(usuario),
                        UserName = usuario.UserName

                    };
                }

                throw new ExcepcionError(HttpStatusCode.NotFound, "Algo salió mal!", "No se pudo agragar usuario" + request.Email);
            }
        }
        
    }
}
