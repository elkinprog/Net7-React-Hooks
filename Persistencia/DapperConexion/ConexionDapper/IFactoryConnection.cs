using System.Data;


namespace Persistencia.DapperConexion.ConexionDapper
{
    public interface IFactoryConnection
    {
        void CloseConnection();
        IDbConnection GetConnection();

    }
}
