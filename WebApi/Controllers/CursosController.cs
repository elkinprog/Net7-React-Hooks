using Aplicacion.Cursos;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace webAPI.Controllers
{
    //[AllowAnonymous]

    [ApiController]
    [Route("api/[controller]")]

    public class CursosController : MiControllerBase
    {
       
        [HttpGet]
        public async Task<ActionResult<List<CursoDto>>> Get()
        {
            return await Mediator!.Send(new GetCurso.GetCursoRequest());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDto>> GetId(Guid id)
        {
            return await Mediator!.Send(new GetCursoId.GetCursoById { Id = id }); 
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCursos.CreateCursosComand data)
        {
            await Mediator!.Send(data);
            return Ok(); 
        }
         

        [HttpPut("{id}")]
        public async Task<ActionResult<CursoDto>> Put(Guid id, UpdateCurso.UpdateCursoRequest data)
        {
            data.Id = id;
            var valor = await Mediator!.Send(data);
            return valor;
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<CursoDto>>Delete(Guid id)
        {
            var command = new DeleteCurso() { Id = id };
            var valor = await Mediator!.Send(command);
            return valor;
        }

    } 
}