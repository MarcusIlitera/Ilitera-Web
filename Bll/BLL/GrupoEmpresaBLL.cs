using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using DAL;

namespace BLL
{
    public class GrupoEmpresaBLL
    {
        public GrupoEmpresa RetornarGrupoEmpresa(int IdGrupoEmpresa)
        {
            GrupoEmpresaDAL grupoEmpresaDal = new GrupoEmpresaDAL();
            return grupoEmpresaDal.RetornarGrupoEmpresa(IdGrupoEmpresa);
        }
    }
}
