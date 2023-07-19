﻿using Aplicacion.Contratos;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Seguridad.TokenSeguridad
{
    public class UsuarioSesion : IUsuarioSesion
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioSesion(IHttpContextAccessor httpContextAccessor)
        {
           this._httpContextAccessor = httpContextAccessor; 
        }
        public  string ObtenerusuarioSesion()
        {

            var userName =  _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return userName!;
        }
    }
}
