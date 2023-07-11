using Aplicacion.Contratos;
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

            public GetLoginHandler(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IJwtGenerador jwtGenerador)
            {
                this._userManager = userManager;
                this._signInManager = signInManager;
                this._jwtGenerador  = jwtGenerador; 
            }

            //public class GetLoginValidation: AbstractValidator<getLoginRequest>
            //{
            //    public GetLoginValidation()
            //    {
            //        RuleFor(x => x.Email).NotEmpty();
            //        RuleFor(x => x.Password).NotEmpty();
            //    }
            //}

            public async Task<UsuarioDto> Handle(getLoginRequest request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByEmailAsync(request.Email);

                if (usuario == null)
                {
                    throw new GenericResponse(HttpStatusCode.Unauthorized, "Algo salio mal!", "No ingresó correo registrado  " + request.Email);
                }

                var result = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);

                if (result.Succeeded)   
                {
                    return new UsuarioDto
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        Token          = _jwtGenerador.CrearToken(usuario),
                        Email          = usuario.Email,
                        UserName       = usuario.UserName,
                        Imagen         = null,       
                    };
                }

                throw new GenericResponse(HttpStatusCode.Unauthorized, "Algo salio mal!", "No ingresó password registrado  " + request.Password);
            }

        }

    }
}
