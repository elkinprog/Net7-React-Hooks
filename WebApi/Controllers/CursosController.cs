using Aplicacion.Cursos;
using Aplicacion.ManejadorErrores;
using Dominio.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace webAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CursosController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Get()
        {
            return await _mediator.Send(new GetCursoQuery.GetCursoQueryRequest());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetId(int id)
        {
            return await _mediator.Send(new GetCursoByIdQuery.GetCursoByIdQueryRequest { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCursosComand.CreateCursosComandRequest data)
        {
            await _mediator.Send(data);
            return StatusCode(201); 
        }
         

        [HttpPut("{id}")]
        public async Task<ActionResult<Curso>> Put(int id, UpdateCurso.UpdateCursoRequest data)
        {
            data.Id = id;
            await _mediator.Send(data);
            return StatusCode(204);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Curso>>Delete(int id)
        {
            var command = new DeleteCurso() { Id = id };
            var valor = await _mediator.Send(command);
            return valor;
        }



      





    } 
}