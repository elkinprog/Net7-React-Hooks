using Aplicacion.ExcepcionMidleware;
using Dominio.StoresProcedures;
using MediatR;
using Persistencia.DapperConexion.InstructorRepositorio;
using System.Net;

namespace Aplicacion.Instructores
{
    public class GetInstructores
    {

        public class Lista : IRequest<List<Instructor>> { }

        public class Manejador : IRequestHandler<Lista, List<Instructor>>
        {
            private readonly IInstructor _instructorRepository;

            public Manejador(IInstructor instructorRepository)
            {
                _instructorRepository = instructorRepository;
            }
             
            public async Task<List<Instructor>> Handle(Lista request, CancellationToken cancellationToken)
            {
                    var resultado =  await _instructorRepository.ObtenerLista();

                if (resultado == null)
                {
                    throw new GenericResponse(HttpStatusCode.OK, "Atención!", "No existen instructores registrados");
                }

                return resultado.ToList();
            }
        }

    }
}
