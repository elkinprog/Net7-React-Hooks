using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aplicacion.Instructores;
using Dominio.StoresProcedures;

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
    }
}
