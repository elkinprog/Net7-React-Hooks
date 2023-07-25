using Aplicacion.Contratos;
using Aplicacion.ExcepcionMidleware;
using AutoMapper;
using Dominio.Dtos;
using Dominio.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
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
            private readonly IMapper _mapper;

            public UsuarioActualRequestHandler(UserManager<Usuario> userManager, IJwtGenerador jwtGenerador, IUsuarioSesion usuarioSesion, IMapper mapper)
            {
                this._userManager = userManager;
                this._jwtGenerador = jwtGenerador;
                this._usuarioSesion= usuarioSesion;
                this._mapper = mapper;   
            }

            public async Task<UsuarioDto> Handle(UsuarioActualRequest request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());

                if (usuario == null)
                {
                    throw new GenericResponse(HttpStatusCode.OK, "Algo salio mal!", "no existe usuario");
                }

                var usuarioDto = _mapper.Map<Usuario,UsuarioDto>(usuario); 

                return new UsuarioDto
                {
                    NombreCompleto = usuarioDto.NombreCompleto,
                    UserName = usuarioDto.UserName,
                    Token = _jwtGenerador.CrearToken(usuarioDto),
                    Imagen = null,
                    Email   = usuarioDto.Email,
                };
            }
        }
    }
}
