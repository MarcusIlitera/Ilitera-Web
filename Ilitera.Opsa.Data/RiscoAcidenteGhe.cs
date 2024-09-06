using System;
using System.Collections.Generic;
using System.Text;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "RiscoAcidenteGhe", "IdRiscoAcidenteGhe", "", "Riscos inerentes às atividades laborais")]
    public class RiscoAcidenteGhe : Ilitera.Data.Table
    {
        private int _IdRiscoAcidenteGhe;
        private Ghe _IdGhe;
        private int _Ordem;
        private string _Descricao = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public RiscoAcidenteGhe()
        {

        }

        public override int Id
        {
            get { return _IdRiscoAcidenteGhe; }
            set { _IdRiscoAcidenteGhe = value; }
        }

        public Ghe IdGhe
        {
            get { return _IdGhe; }
            set { _IdGhe = value; }
        }

        public int Ordem
        {
            get { return _Ordem; }
            set { _Ordem = value; }
        }

        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
    }


    [Database("opsa", "ProcedimentoInstrutivoGhe", "IdProcedimentoInstrutivoGhe", "", "Procedimentos instrutivos às atividades laborais")]
    public class ProcedimentoInstrutivoGhe : Ilitera.Data.Table
    {
        private int _IdProcedimentoInstrutivoGhe;
        private Ghe _IdGhe;
        private int _Ordem;
        private string _Descricao = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcedimentoInstrutivoGhe()
        {

        }

        public override int Id
        {
            get { return _IdProcedimentoInstrutivoGhe; }
            set { _IdProcedimentoInstrutivoGhe = value; }
        }

        public Ghe IdGhe
        {
            get { return _IdGhe; }
            set { _IdGhe = value; }
        }
        
        public int Ordem
        {
            get { return _Ordem; }
            set { _Ordem = value; }
        }

        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
    }
}
