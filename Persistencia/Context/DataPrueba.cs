﻿using Dominio.Models;
using Microsoft.AspNetCore.Identity;

namespace Persistencia.Context
{
    public  class DataPrueba
    {

        public static async Task InsertarData (CursosOnlineContext context ,UserManager<Usuario> usuarioManager)
        {
            if (!usuarioManager.Users.Any())
            {
                var usuario = new Usuario { NombreCompleto = "Elkin Rojas", UserName = "ElkinProg", Email = "elkinprog@gmail.com" };
                await  usuarioManager.CreateAsync(usuario, "Password123@");
            }
        }

    }
}
