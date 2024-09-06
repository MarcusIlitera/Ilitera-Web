using System;
using System.Data;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;


namespace Ilitera.Opsa.Data
{
	/// <summary>
	/// Summary description for Recomendacao.
	/// </summary>
	[Database("opsa", "Recomendacao", "IdRecomendacao")]
	public class Recomendacao: Ilitera.Data.Table
	{
		private int _IdRecomendacao;
		private Cliente _IdCliente;
		private string _Descricao=string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Recomendacao()
		{

		}
		public override int Id
		{
			get{return _IdRecomendacao;}
			set{_IdRecomendacao = value;}
		}
		public Cliente IdCliente
		{
			get{return _IdCliente;}
			set{_IdCliente = value;}
		}
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
	}
}
