using Aplicacion.ExcepcionMidleware;
using Azure.Core;
using Dominio.Models;
using MediatR;
using Persistencia.DapperConexion.InstructorRepositorio;
using ServiceStack;
using ServiceStack.Text;
using System.Data.SqlTypes;
using System.Net;
using System.Runtime.InteropServices;

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
               var resultado = await  _InstructorRepository.Actualizar(request.Id,request.Nombre,request.Apellidos,request.Grado);

               var resultadoDB = await _InstructorRepository.ObtenerPorId(request.Id);

                if (resultado !> 0 && resultadoDB == null)
                {
                    throw new GenericResponse(HttpStatusCode.NotFound, "Atención!", "No existe instructor con este Id");
                }

              
                throw new GenericResponse(HttpStatusCode.OK, "Bien echo!", "Se actualizo Instructor");
            }
        }



    }
}
