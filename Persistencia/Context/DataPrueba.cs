using Dominio.Models;
using Microsoft.AspNet.Identity;


namespace Persistencia.Context
{
    public  class DataPrueba
    {

        public static async Task InsertarData (CursosOnlineContext context ,UserManager<Usuario> usuarioManager)
        {
            if (!usuarioManager.Users.Any())
            {
                var usuario = new Usuario { NombreCompleto = "Elkin Rojas ", UserName = "Elkin Prog", Email = "elkinprog@gmail.com" };
                await  usuarioManager.CreateAsync(usuario, "password123");
            }
        }

    }
}
