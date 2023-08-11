using Aplicacion.Comentarios;
using Aplicacion.Cursos;
using Dominio.Dtos;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Aplicacion.Comentarios.DeleteComentario;

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



  
        [HttpDelete("{id}")]
        public async Task<ActionResult<ComentarioDto>> EliminaComentario(Guid id)
        {
            return  await Mediator!.Send(new DeleteComentario.Ejecuta { Id = id });
        }








    }
}
