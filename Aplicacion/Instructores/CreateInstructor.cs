using Aplicacion.ExcepcionMidleware;
using MediatR;
using Persistencia.DapperConexion.InstructorRepositorio;
using System.Net;

namespace Aplicacion.Instructores
{
    public  class CreateInstructor
    {
        public class Create: IRequest
        {
            public string   Nombre    {get;set;}
            public string   Apellido  {get;set;}
            public string   Grado     {get;set;}
        }

        public class CreateHandler : IRequestHandler<Create>
        {
            private readonly IInstructor _instructorRepository;

            public CreateHandler(IInstructor instructorRepository)
            {
                this._instructorRepository = instructorRepository;
            }

            public async  Task Handle(Create request, CancellationToken cancellationToken)
            {
                var resultado =   await _instructorRepository.Crear(request.Nombre,request.Apellido,request.Grado);

                if (resultado > 0 )
                {
                    throw new GenericResponse(HttpStatusCode.NotFound, "Atención!", "No se pudo crear instructor");
                }

                throw new GenericResponse(HttpStatusCode.OK, "Bien echo!", "se creo Instructor");


            }
        }


    }
}
