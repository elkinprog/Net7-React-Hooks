using Microsoft.AspNetCore.Identity;

namespace Dominio.Models
{
    public class Usuario: IdentityUser
    {
        public string NombreCompleto { get; set; }

    }
}