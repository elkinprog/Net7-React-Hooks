using Aplicacion.ManejadorErrores;
using Dominio.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Aplicacion.Seguridad.Login
{
    public  class GetLogin
    {
        public class getLoginRequest: IRequest<Usuario>
        {
            public string Email    {get; set;}
            public string Password {get; set;}
        }

        public class GetLoginHandler : IRequestHandler<getLoginRequest, Usuario>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly SignInManager<Usuario> _signInManager;

            public GetLoginHandler(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
            {
                this._userManager = userManager;
                this._signInManager = signInManager;
            }

            public class GetLoginValidation: AbstractValidator<getLoginRequest>
            {
                public GetLoginValidation()
                {
                    RuleFor(x => x.Email).NotEmpty();
                    RuleFor(x => x.Password).NotEmpty();
                }
            }

            public async Task<Usuario> Handle(getLoginRequest request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByEmailAsync(request.Email);

                if (usuario == null)
                {
                    throw new ExcepcionError(HttpStatusCode.Unauthorized, "Algo salio mal!", "No ingresó correo registrado  " + request.Email);
                }

                var result = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);

                if (result.Succeeded)
                {
                    return usuario;
                }

                throw new ExcepcionError(HttpStatusCode.Unauthorized, "Algo salio mal!", "No ingresó password registrado  " + request.Password);
            }
        }

    }
}
