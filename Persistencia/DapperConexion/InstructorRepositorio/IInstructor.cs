using Dominio.StoresProcedures;

namespace Persistencia.DapperConexion.InstructorRepositorio
{
    public interface IInstructor
    {
        Task<IEnumerable<Instructor>> ObtenerLista();
        Task<Instructor> ObtenerPorId(Guid id);
        Task<int> Crear(string nombre, string apellido, string grado);
        Task<int> Actualizar(Guid id ,string nombre, string apellido, string grado);
        Task<int> Eliminar(Guid id);

    }
}
