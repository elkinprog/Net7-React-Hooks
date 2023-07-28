using Aplicacion.ExcepcionMidleware;
using Dominio.StoresProcedures;
using MediatR;
using Persistencia.DapperConexion.InstructorRepositorio;
using System.Net;

namespace Aplicacion.Instructores
{
    public class GetInstructorId
    {

        public class GetId : IRequest<Instructor>
        {
            public Guid Id {get;set;}
        }

        public class GetIdQuery : IRequestHandler<GetId,Instructor>
        {
            private readonly IInstructor _instructorRepository;

            public GetIdQuery(IInstructor instructorRepository)
            {
                this._instructorRepository = instructorRepository;   
            }

            public async Task<Instructor> Handle(GetId request, CancellationToken cancellationToken)
            {
                var instructor  = await _instructorRepository.ObtenerPorId(request.Id);
   
                return new Instructor
                {
                    Id = instructor.Id,
                    Nombre = instructor.Nombre,
                    Apellido = instructor.Apellido,
                    Grado = instructor.Grado
                };

            }
        }
    }
}
