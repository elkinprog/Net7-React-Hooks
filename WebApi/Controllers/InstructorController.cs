using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aplicacion.Instructores;
using Dominio.StoresProcedures;
using System;
using Microsoft.Identity.Client;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class InstructorController : MiControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<Instructor>>> ObtenerInstructores()
        {
            return await Mediator!.Send(new GetInstructores.Lista());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Instructor>> ObtenerId(Guid id)
        {
            var instructor = await Mediator!.Send(new GetInstructorId.GetId { Id = id });
            return Ok(instructor);
        }

        [HttpPost]
        public async Task <ActionResult>CrearInstructor(CreateInstructor.Create create)
        {
            await Mediator!.Send(create);
            return Ok();    
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Instructor>> ActualizarInstructor(Guid id, UpdateInstructores.Update update)
        {
            update.Id = id;
            await Mediator!.Send(update);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Instructor>> EliminaInstructor(Guid id)
        {
            var resultado = await Mediator!.Send(new DeleteInstructores.Delete { Id = id });
            return Ok(resultado);
        }


    }
}
