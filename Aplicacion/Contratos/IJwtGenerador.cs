using Dominio.Dtos;
using Dominio.Models;

namespace Aplicacion.Contratos
{
    public  interface IJwtGenerador
    {
        string CrearToken(UsuarioDto usuario);
    }
}
