using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConexion.Instructor;
using Aplicacion.Instructores;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class InstructorController : MiControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<InstructorModel>>> ObtenerInstructores()
        {
            return await Mediator!.Send(new Consulta.Lista());
        }
    }
}
