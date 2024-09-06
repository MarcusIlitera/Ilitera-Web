using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using BLL;

namespace Facade
{
    public class UsuarioFacade
    {
        public static Usuario AutenticarUsuaro(string usuario, string senha)
        {
            UsuarioBLL usuarioBll = new UsuarioBLL();
            return usuarioBll.AutenticarUsuaro(usuario, senha);
        }
    }
}
