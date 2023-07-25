using Aplicacion.ExcepcionMidleware;
using MediatR;
using Persistencia.DapperConexion.InstructorRepositorio;
using System.Net;

namespace Aplicacion.Instructores
{
    public class UpdateInstructores
    {

        public class Update: IRequest
        {
            public Guid   Id        {get;set;}
            public string Nombre    {get;set;}  
            public string Apellidos {get;set;}
            public string Grado     {get;set;}
        }

        public class UpdateHandler : IRequestHandler<Update>
        {
            private readonly IInstructor _InstructorRepository;

            public UpdateHandler(IInstructor InstructorRepository)
            {
                this._InstructorRepository = InstructorRepository;
            }

            public async  Task Handle(Update request, CancellationToken cancellationToken)
            {
               var resultado = await  _InstructorRepository.Actualizar(request.Id,request.Nombre,request.Apellidos,request.Grado);
                 
                if (resultado > 0 )
                {
                    throw new GenericResponse(HttpStatusCode.NotFound, "Atención!", "No se pudo actualizar instructor");
                }

                throw new GenericResponse(HttpStatusCode.OK, "Bien echo!", "Se actualizo Instructor");
            }
        }



    }
}
