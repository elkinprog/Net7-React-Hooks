using Aplicacion.ExcepcionMidleware;
using Dominio.Models;
using MediatR;
using Persistencia.DapperConexion.InstructorRepositorio;
using System.Net;

namespace Aplicacion.Instructores
{
    public class DeleteInstructores
    {

        public class Delete: IRequest<Instructor>
        {
            public Guid Id {get;set;}
        }

        public class DeleteHandler : IRequestHandler<Delete, Instructor>
        {
            private readonly IInstructor _instructorRepositoy;

            public DeleteHandler(IInstructor instructorRepositoy)
            {
                this._instructorRepositoy  = instructorRepositoy;   
            }

            public async Task<Instructor>Handle(Delete request, CancellationToken cancellationToken)    
            {

                var resultado = await _instructorRepositoy.Eliminar(request.Id);

                if (resultado > 0)
                {
                    throw new GenericResponse(HttpStatusCode.OK, "Bien echo!", "se elimino Instructor");
                }

                throw new GenericResponse(HttpStatusCode.NotFound, "Atención!", "No existe instructor con este Id");
            }
        }
    }
}
