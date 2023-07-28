using Dapper;
using Dominio.StoresProcedures;
using Persistencia.DapperConexion.ConexionDapper;
using System.Data;
using System.Net;

namespace Persistencia.DapperConexion.InstructorRepositorio
{
    public class InstructorRepositorio : IInstructor
    {
        private readonly IFactoryConnection _factoryConnection;

        public InstructorRepositorio(IFactoryConnection factoryConnection)
        {
           this._factoryConnection = factoryConnection; 
        }



        #region Rutina para crear Instructor modo StoreProcedure
        public async Task<int> Crear(string nombre,string apellido,string grado)
        {
            var storeProcedure = "sp_nuevo_instructor";

            try
            {
                var connection = _factoryConnection.GetConnection();
                var resulatado = await connection.ExecuteAsync(
                storeProcedure,
                new
                {
                    Id       = Guid.NewGuid(),
                    Nombre   = nombre,
                    Apellido = apellido,
                    Grado    = grado,
                },
                commandType: CommandType.StoredProcedure
                );

                _factoryConnection.CloseConnection();
                return resulatado;
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo guardar instructor", e);
            }
        }
        #endregion


        #region  Rutina para obtener Instructor modo StoreProcedure
        public async Task<IEnumerable<Instructor>> ObtenerLista()
        {
            IEnumerable<Instructor> instructorList = null;
            var storeProcedure = "sp_obtener_instructores";

            try
            {
                var connection = _factoryConnection.GetConnection();
                instructorList = await connection.QueryAsync<Instructor>(storeProcedure, null, commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                throw new Exception("Error en la consulta de datos", e);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
            return instructorList;

        }
        #endregion


        #region Rutina para obtenerId Instructor modo StoreProcedure
        public async Task<Instructor> ObtenerPorId(Guid id)
        {
            var storeProcedure = "sp_obtenerid_instructor";

           Instructor instructor = null;
            try
            {
                var connection = _factoryConnection.GetConnection();
                    instructor = await connection.QueryFirstAsync<Instructor>(
                    storeProcedure,
                    new
                    {
                        Id = id,
                    },

                    commandType: CommandType.StoredProcedure

                    );
                return instructor;
            }
            catch (Exception e)
            {
                throw new Exception("No se encontro instructor con este id", e);
            }

        }
        #endregion


        #region Rutina para actualizar Instructor modo StoreProcedure
        public async Task<int> Actualizar(Guid Id, string nombre, string apellido, string grado)
        {
            var storeProcedure = "sp_editar_instructor";

            try
            {
                var connection = _factoryConnection.GetConnection();
                var resulatado = await connection.ExecuteAsync(
                storeProcedure,
                new
                {
                    Id = Id,
                    Nombre = nombre,
                    Apellido = apellido,
                    Grado = grado,
                },
                commandType: CommandType.StoredProcedure
                );

                _factoryConnection.CloseConnection();
                return resulatado;
            }
            catch (Exception e)
            {

                throw new Exception("No se pudo actualizar instructor", e);
            }
        }
        #endregion


        #region Rutina para eliminar Instructor modo StoreProcedure
        public async Task<int> Eliminar(Guid id)
        {
            var storeProcedure = "sp_eliminar_instructor";

            try
            {
                var connection = _factoryConnection.GetConnection();
                var resulatado = await connection.ExecuteAsync(
                storeProcedure, 
                new
                {
                    Id= id
                },
                  commandType: CommandType.StoredProcedure
                );

                _factoryConnection.CloseConnection();
                return resulatado;
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo eliminar instructor", e);
            }
        }
        #endregion


       

        


    }
}
