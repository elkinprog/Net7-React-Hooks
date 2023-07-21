using Aplicacion.Contratos;
using AutoMapper;
using Dominio.Dtos;
using Dominio.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using WebApi.Responses;

namespace Aplicacion.Seguridad.Login
{
    public  class GetLogin
    {
        public class getLoginRequest: IRequest<UsuarioDto>
        {
            public string Email    {get; set;}
            public string Password {get; set;}
        }

        public class GetLoginHandler : IRequestHandler<getLoginRequest, UsuarioDto>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly SignInManager<Usuario> _signInManager;
            private readonly IJwtGenerador _jwtGenerador;
            private readonly IMapper _mapper;

            public GetLoginHandler(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IJwtGenerador jwtGenerador, IMapper mapper)
            {
                this._userManager = userManager;
                this._signInManager = signInManager;
                this._jwtGenerador  = jwtGenerador;
                this._mapper = mapper;
            }

     
            public async Task<UsuarioDto> Handle(getLoginRequest request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByEmailAsync(request.Email);

                if (usuario == null)
                {
                    throw new GenericResponse(HttpStatusCode.Unauthorized, "Algo salio mal!", "No ingresó correo registrado  " + request.Email);
                }

                var result = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);

                var usuarioDto = _mapper.Map<Usuario, UsuarioDto>(usuario);

                if (result.Succeeded)   
                {
                    return new UsuarioDto
                    {
                        NombreCompleto = usuarioDto.NombreCompleto,
                        Token          = _jwtGenerador.CrearToken(usuarioDto),
                        Email          = usuarioDto.Email,
                        UserName       = usuarioDto.UserName,
                        Imagen         = null,       
                    };
                }

                throw new GenericResponse(HttpStatusCode.Unauthorized, "Algo salio mal!", "No ingresó password registrado  " + request.Password);
            }

        }

    }
}
