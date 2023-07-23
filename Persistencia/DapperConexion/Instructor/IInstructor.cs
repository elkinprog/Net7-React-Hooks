namespace Persistencia.DapperConexion.Instructor
{
    public interface IInstructor
    {
        Task<IEnumerable<InstructorModel>> ObtenerLista();
        Task<InstructorModel> ObtenerPorId(Guid id);

        Task<int> Crear(InstructorModel instructorModel);
        Task<int> Actualizar(InstructorModel instructorModel);
        Task<int> Eliminar(Guid id);

    }
}
