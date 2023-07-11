using Aplicacion.Seguridad.Login;
using Dominio.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [AllowAnonymous]

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : MiControllerBase
    {

        [HttpPost("login")]
        public async  Task<ActionResult<UsuarioDto>> Login(GetLogin.getLoginRequest parametros)
        {
           return await Mediator!.Send(parametros);
        }

    }

}
