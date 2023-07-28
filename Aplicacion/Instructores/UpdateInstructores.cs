using Aplicacion.ExcepcionMidleware;
using Dominio.Models;
using MediatR;
using Persistencia.DapperConexion.InstructorRepositorio;
using System.Net;

namespace Aplicacion.Instructores
{
    public class UpdateInstructores
    {

        public class Update: IRequest<Instructor>
        {
            public Guid   Id        {get;set;}
            public string Nombre    {get;set;}  
            public string Apellidos {get;set;}
            public string Grado     {get;set;}
        }

        public class UpdateHandler : IRequestHandler<Update, Instructor>
        {
            private readonly IInstructor _InstructorRepository;

            public UpdateHandler(IInstructor InstructorRepository)
            {
                this._InstructorRepository = InstructorRepository;
            }

            public async  Task<Instructor> Handle(Update request, CancellationToken cancellationToken)
            {
               await  _InstructorRepository.Actualizar(request.Id,request.Nombre,request.Apellidos,request.Grado);

               await _InstructorRepository.ObtenerPorId(request.Id);

               
                throw new GenericResponse(HttpStatusCode.OK, "Bien echo!", "Se actualizo Instructor");
            }
        }



    }
}
