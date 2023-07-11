using Dominio.Models;

namespace Aplicacion.Contratos
{
    public  interface IJwtGenerador
    {
        string CrearToken(Usuario usuario);
    }
}
