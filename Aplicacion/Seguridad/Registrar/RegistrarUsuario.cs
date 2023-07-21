using Aplicacion.Contratos;
using AutoMapper;
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
            private readonly IMapper _mapper;


            public RegistrarUsuarioHandle(CursosOnlineContext context,UserManager<Usuario> userManager, IJwtGenerador jwtGenerador, IMapper mapper)
            {
                this._context = context;    
                this._userManager = userManager;
                this._jwtGenerador = jwtGenerador; 
                this._mapper    = mapper;   
            }

            public async Task<UsuarioDto> Handle(RegistrarUsuarioRequest request, CancellationToken cancellationToken)
            {
                var existeEmail = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
                if (existeEmail)
                {
                    throw new GenericResponse(HttpStatusCode.BadRequest, "Atención!", "Ya está registrado este correo " + request.Email);
                }

                var existeUsername = await _context.Users.Where(x => x.UserName == request.Username).AnyAsync();
                if (existeUsername)
                {
                    throw new GenericResponse(HttpStatusCode.BadRequest, "Atención!", "Ya está registrado este usuario " + request.Username);
                }

               
                var usuario = new Usuario
                {
                    NombreCompleto = request.Nombre + " " + request.Apellidos,
                    Email = request.Email,
                    UserName = request.Username
                };

               var resultado = await  _userManager.CreateAsync(usuario,request.Password);

                var usuarioDto = _mapper.Map<Usuario, UsuarioDto>(usuario);

                if (resultado.Succeeded)
                {
                    return new UsuarioDto
                    {
                        NombreCompleto = usuarioDto.NombreCompleto,
                        Token = _jwtGenerador.CrearToken(usuario),
                        UserName = usuarioDto.UserName,
                        Email = usuarioDto.Email
                    };
                }

                throw new GenericResponse(HttpStatusCode.NotFound, "Algo salió mal!", "Los campos no deben estar vacios " + request.Email);
            }
        }
        
    }
}
