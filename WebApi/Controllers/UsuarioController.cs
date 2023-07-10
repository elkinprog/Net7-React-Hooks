using Aplicacion.Cursos;
using Aplicacion.Seguridad.Login;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : MiControllerBase
    {

        [HttpPost("login")]
        public async  Task<ActionResult<Usuario>> Login(GetLogin.getLoginRequest parametros)
        {
           return await Mediator!.Send(parametros);
 
        }

    }

}
