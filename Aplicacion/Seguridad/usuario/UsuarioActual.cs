using Aplicacion.Contratos;
using Dominio.Dtos;
using Dominio.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using WebApi.Responses;

namespace Aplicacion.Seguridad.usuario
{
    public  class UsuarioActual
    {

        public class UsuarioActualRequest: IRequest<UsuarioDto>{ }

        public class UsuarioActualRequestHandler : IRequestHandler<UsuarioActualRequest, UsuarioDto>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;
            private readonly IUsuarioSesion _usuarioSesion;

            public UsuarioActualRequestHandler(UserManager<Usuario> userManager, IJwtGenerador jwtGenerador, IUsuarioSesion usuarioSesion)
            {
                this._userManager = userManager;
                this._jwtGenerador = jwtGenerador;
                this._usuarioSesion= usuarioSesion;
            }

            public async Task<UsuarioDto> Handle(UsuarioActualRequest request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerusuarioSesion());

                if (usuario == null)
                {
                    throw new GenericResponse(HttpStatusCode.OK, "Algo salio mal!", "no existe usuario" + usuario.NombreCompleto);
                }

                return new UsuarioDto
                {
                    NombreCompleto = usuario.NombreCompleto,
                    UserName = usuario.UserName,
                    Token = _jwtGenerador.CrearToken(usuario),
                    Imagen = null,
                    Email   = usuario.Email,
                };
            }
        }
    }
}
