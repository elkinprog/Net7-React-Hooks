using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aplicacion.Instructores;
using Dominio.StoresProcedures;
using System;

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


      



    }
}
