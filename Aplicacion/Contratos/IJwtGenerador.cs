using Dominio.Dtos;

namespace Aplicacion.Contratos
{
    public  interface IJwtGenerador
    {
        string CrearToken(UsuarioDto usuario);
    }
}
