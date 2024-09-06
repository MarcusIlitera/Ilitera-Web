using System;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	public enum TipoServico:int
	{
		SegurancaTrabalho,
		Pcmso,
		Juridico
	}

	[Database("opsa","Servico","IdServico")]
	public class Servico : Ilitera.Data.Table
	{
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Servico()
		{

		}
		private int _IdServico;	
		private string _Descricao = string.Empty;
		private string _DescricaoNF = string.Empty;
		private Empresa _IdEmpresa;
		private	Conta _IdConta;
		private int _IndTipoServico;
		
		public override int Id
		{
			get{return _IdServico;}
			set{_IdServico = value;}
		}
        [Obrigatorio(true, "A descrição é campo obrigatório!")]
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
		public string DescricaoNF
		{
			get{return _DescricaoNF;}
			set{_DescricaoNF = value;}
		}
        [Obrigatorio(true, "A Empresa é campo obrigatório!")]
		public Empresa IdEmpresa
		{
			get{return _IdEmpresa;}
			set{_IdEmpresa = value;}
		}	
		public Conta IdConta
		{
			get{return _IdConta;}
			set{_IdConta = value;}
		}	
		public int IndTipoServico
		{
			get{return _IndTipoServico;}
			set{_IndTipoServico = value;}
		}

        public static string GetNomeTipoServico(int indTipoServico)
        {
            string ret = string.Empty;

            if (indTipoServico == (int)TipoServico.Pcmso)
                ret = "PCMSO";
            else if (indTipoServico == (int)TipoServico.Juridico)
                ret = "Consultoria Juridica";
            else if (indTipoServico == (int)TipoServico.SegurancaTrabalho)
                ret = " Segurança do Trabalho";

            return ret;
        }

		public string GetNomeTipoServico()
		{
            if (this.mirrorOld == null)
                this.Find();

            return GetNomeTipoServico(this._IndTipoServico);
		}
	}
}
