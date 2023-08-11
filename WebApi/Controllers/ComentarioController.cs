using Aplicacion.Comentarios;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : MiControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> CreaComentario(CreateComentario.NuevoComentario data)
        {
           await Mediator!.Send(data);
           return Ok();
        }
    }
}
