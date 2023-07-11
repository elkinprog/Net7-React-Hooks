using Aplicacion.Cursos;
using Dominio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace webAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CursosController : MiControllerBase
    {
       
        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Get()
        {
            return await Mediator!.Send(new GetCursoQuery.GetCursoQueryRequest());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetId(int id)
        {
            return await Mediator!.Send(new GetCursoByIdQuery.GetCursoByIdQueryRequest { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCursosComand.CreateCursosComandRequest data)
        {
            await Mediator!.Send(data);
            return Ok(); 
        }
         

        [HttpPut("{id}")]
        public async Task<ActionResult<Curso>> Put(int id, UpdateCurso.UpdateCursoRequest data)
        {
            data.Id = id;
            var valor = await Mediator!.Send(data);
            return valor;
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Curso>>Delete(int id)
        {
            var command = new DeleteCurso() { Id = id };
            var valor = await Mediator!.Send(command);
            return valor;
        }

    } 
}