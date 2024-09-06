using System;
using Ilitera.Data;
using System.Text;

namespace Ilitera.Common
{
    public enum FuncionalidadeTipo : int
    {
        Entity,
        Formulario,
        Relatorio,
        Metodo,
        WebPage
    }

    public enum TipoPermissao : int
    {
        SemPermissao,
        ComPermissao,
        NegarPermissao
    }

    public enum AcaoPermissao : int
    {
        Selecionar,
        Incluir,
        Alterar,
        Excluir,
        Executar
    }

	[Database("opsa", "Funcionalidade", "IdFuncionalidade")]
	public class Funcionalidade: Ilitera.Data.Table
	{
		private int _IdFuncionalidade;
		private string _Nome = string.Empty;
		private string _Descricao = string.Empty;
        private string _Method = string.Empty;
		private string _ClassName = string.Empty;
		private string _Assembly = string.Empty;
		private string _Pacote = string.Empty;
        private string _DetalheFuncionalidade = string.Empty;
		private int _IndFuncionalidadeTipo;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Funcionalidade()
		{

		}
		public override int Id
		{														  
			get	{return _IdFuncionalidade;}
			set {_IdFuncionalidade = value;}
		}
		[Obrigatorio(true, "Nome da Funcionalidade é obrigatório!")]
		public string Nome
		{														  
			get	{return _Nome;}
			set {_Nome = value;}
		}
		public string Descricao
		{														  
			get	{return _Descricao;}
			set {_Descricao = value;}
		}
        public string Method
        {
            get { return _Method; }
            set { _Method = value; }
        }
		public string ClassName
		{														  
			get	{return _ClassName;}
			set {_ClassName = value;}
		}
		public string Assembly
		{														  
			get	{return _Assembly;}
			set {_Assembly = value;}
		}
		public string Pacote
		{														  
			get	{return _Pacote;}
			set {_Pacote = value;}
		}
        public string DetalheFuncionalidade
        {
            get { return _DetalheFuncionalidade; }
            set { _DetalheFuncionalidade = value; }
        }
		public int IndFuncionalidadeTipo
		{														  
			get	{return _IndFuncionalidadeTipo;}
			set {_IndFuncionalidadeTipo = value;}
		}
		public override string ToString()
		{			
			return this.Nome;
		}		
		public static string VerificaTipoFuncionalidade(Funcionalidade funcionalidade)
		{
			string tipoFunc = string.Empty;
			if(funcionalidade.IndFuncionalidadeTipo == (int)FuncionalidadeTipo.Entity)
				tipoFunc = "Entity";
			else if(funcionalidade.IndFuncionalidadeTipo == (int)FuncionalidadeTipo.Formulario)
				tipoFunc = "Formulário";
			else if(funcionalidade.IndFuncionalidadeTipo == (int)FuncionalidadeTipo.Metodo)
				tipoFunc = "Método";
			else
				tipoFunc = "Relatório";
			return tipoFunc;
		}

        public static string GetTipoFuncionalidade(FuncionalidadeTipo tipo)
        {
            string tipoFunc = string.Empty;

            if (tipo == FuncionalidadeTipo.Entity)
                tipoFunc = "Entity";
            else if (tipo == FuncionalidadeTipo.Formulario)
                tipoFunc = "Formulário";
            else if (tipo == FuncionalidadeTipo.Metodo)
                tipoFunc = "Método";
            else if (tipo == FuncionalidadeTipo.WebPage)
                tipoFunc = "WebPage";
            else
                tipoFunc = "Relatório";

            return tipoFunc;
        }

        public static Funcionalidade GetFuncionalidade(FuncionalidadeTipo tipo, Type type)
        {
            return GetFuncionalidade(tipo, type, string.Empty);
        }

        public static Funcionalidade GetFuncionalidade(FuncionalidadeTipo tipo, Type type, string Method)
        {
            return GetFuncionalidade(tipo, type, Method, string.Empty);
        }

        public static Funcionalidade GetFuncionalidade(FuncionalidadeTipo tipo, Type type, string Method, string DetalheFuncionalidade)
        {
            StringBuilder sqlstm = new StringBuilder();
            
            sqlstm.Append("IndFuncionalidadeTipo=" + (int)tipo);
            sqlstm.Append(" AND ClassName='" + type.Name + "'");

            sqlstm.Append(" AND (Method='" + Method + "'");
            if (Method.Equals(string.Empty))
                sqlstm.Append(" OR Method IS NULL)");
            else
                sqlstm.Append(")");

            sqlstm.Append(" AND (DetalheFuncionalidade='" + DetalheFuncionalidade + "'");
            if (DetalheFuncionalidade.Equals(string.Empty))
                sqlstm.Append(" OR DetalheFuncionalidade IS NULL)");
            else
                sqlstm.Append(")");                
            
            Funcionalidade funcionalidade = new Funcionalidade();
            funcionalidade.Find(sqlstm.ToString());

            if (funcionalidade.Id.Equals(0))
            {
                funcionalidade.Inicialize();
                funcionalidade.IndFuncionalidadeTipo = (int)tipo;

                if (Method.Equals(string.Empty))
                {
                    if (DetalheFuncionalidade.Equals(string.Empty))
                        funcionalidade.Nome = GetTipoFuncionalidade(tipo) + " " + type.Name.Replace("Rpt", string.Empty);
                    else
                        funcionalidade.Nome = GetTipoFuncionalidade(tipo) + " " + type.Name.Replace("Rpt", string.Empty) + " " + DetalheFuncionalidade;
                }
                else
                {
                    if (DetalheFuncionalidade.Equals(string.Empty))
                        funcionalidade.Nome = GetTipoFuncionalidade(tipo) + " " + Method;
                    else
                        funcionalidade.Nome = GetTipoFuncionalidade(tipo) + " " + Method + " " + DetalheFuncionalidade;
                }

                funcionalidade.Descricao = "Cadastro automático";
                funcionalidade.Method = Method;
                funcionalidade.ClassName = type.Name;
                funcionalidade.Assembly = type.FullName.Substring(0, type.FullName.LastIndexOf("."));
                funcionalidade.Pacote = type.FullName;
                funcionalidade.DetalheFuncionalidade = DetalheFuncionalidade;
                funcionalidade.Save();
            }

            return funcionalidade;
        }
	}

	[Database("opsa", "FuncionalidadeGrupo", "IdFuncionalidadeGrupo")]
	public class FuncionalidadeGrupo: Ilitera.Data.Table
	{
		private int _IdFuncionalidadeGrupo;
		private Funcionalidade _IdFuncionalidade;
		private Grupo _IdGrupo;
		private short _IndSelecionar;
		private short _IndIncluir;
		private short _IndAlterar;
		private short _IndExcluir;
		private short _IndExecutar;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public FuncionalidadeGrupo()
		{
		}
		public override int Id
		{														  
			get	{return _IdFuncionalidadeGrupo;}
			set {_IdFuncionalidadeGrupo = value;}
		}		
		public Funcionalidade IdFuncionalidade
		{														  
			get	{return _IdFuncionalidade;}
			set {_IdFuncionalidade = value;}
		}
		public Grupo IdGrupo
		{														  
			get	{return _IdGrupo;}
			set {_IdGrupo = value;}
		}
		public short IndSelecionar
		{														  
			get	{return _IndSelecionar;}
			set {_IndSelecionar = value;}
		}
		public short IndIncluir
		{														  
			get	{return _IndIncluir;}
			set {_IndIncluir = value;}
		}
		public short IndAlterar
		{														  
			get	{return _IndAlterar;}
			set {_IndAlterar = value;}
		}
		public short IndExcluir
		{														  
			get	{return _IndExcluir;}
			set {_IndExcluir = value;}
		}
		public short IndExecutar
		{														  
			get	{return _IndExecutar;}
			set {_IndExecutar = value;}
		}
		public override string ToString()
		{
			this.IdFuncionalidade.Find();
			return Funcionalidade.VerificaTipoFuncionalidade(this.IdFuncionalidade) + " - " + this.IdFuncionalidade.Nome;
		}
	}

	[Database("opsa", "FuncionalidadeUsuario", "IdFuncionalidadeUsuario")]
	public class FuncionalidadeUsuario: Ilitera.Data.Table
	{
		private int _IdFuncionalidadeUsuario;
		private Funcionalidade _IdFuncionalidade;
		private Usuario _IdUsuario;
		private short _IndSelecionar;
		private short _IndIncluir;
		private short _IndAlterar;
		private short _IndExcluir;
		private short _IndExecutar;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FuncionalidadeUsuario()
		{
		}
		public override int Id
		{														  
			get	{return _IdFuncionalidadeUsuario;}
			set {_IdFuncionalidadeUsuario = value;}
		}		
		public Funcionalidade IdFuncionalidade
		{														  
			get	{return _IdFuncionalidade;}
			set {_IdFuncionalidade = value;}
		}
		public Usuario IdUsuario
		{														  
			get	{return _IdUsuario;}
			set {_IdUsuario = value;}
		}
		public short IndSelecionar
		{														  
			get	{return _IndSelecionar;}
			set {_IndSelecionar = value;}
		}
		public short IndIncluir
		{														  
			get	{return _IndIncluir;}
			set {_IndIncluir = value;}
		}
		public short IndAlterar
		{														  
			get	{return _IndAlterar;}
			set {_IndAlterar = value;}
		}
		public short IndExcluir
		{														  
			get	{return _IndExcluir;}
			set {_IndExcluir = value;}
		}
		public short IndExecutar
		{														  
			get	{return _IndExecutar;}
			set {_IndExecutar = value;}
		}
		public override string ToString()
		{
			this.IdFuncionalidade.Find();
			return Funcionalidade.VerificaTipoFuncionalidade(this.IdFuncionalidade) + " - " + this.IdFuncionalidade.Nome;
		}		
	}
}
