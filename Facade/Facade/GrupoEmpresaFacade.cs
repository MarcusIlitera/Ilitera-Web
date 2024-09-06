using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using BLL;

namespace Facade
{
    public class GrupoEmpresaFacade
    {
        public static GrupoEmpresa RetornarGrupoEmpresa(int IdGrupoEmpresa)
        {
            GrupoEmpresaBLL grupoEmpresaBll = new GrupoEmpresaBLL();
            return grupoEmpresaBll.RetornarGrupoEmpresa(IdGrupoEmpresa);
        }
    }
}
