using System;
using System.Collections;
using System.Data;
using System.Text;
using System.IO;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	[Database("opsa", "CedenteBaixaAutomatica", "IdCedenteBaixaAutomatica")]
	public class CedenteBaixaAutomatica: Ilitera.Data.Table
	{
		private int	_IdCedenteBaixaAutomatica;
		private Cedente	_IdCedente;
		private DateTime _DataBaixa;
		private string	_ArquivoBaixa=string.Empty;
		private string	_Observacao=string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CedenteBaixaAutomatica()
		{

		}
		public override int Id
		{														  
			get{return _IdCedenteBaixaAutomatica;}
			set	{_IdCedenteBaixaAutomatica = value;}
		}
		public Cedente IdCedente
		{														  
			get{return _IdCedente;}
			set	{_IdCedente = value;}
		}
		public DateTime DataBaixa
		{														  
			get{return _DataBaixa;}
			set	{_DataBaixa = value;}
		}
		public string ArquivoBaixa
		{														  
			get{return _ArquivoBaixa;}
			set	{_ArquivoBaixa = value;}
		}
		public string Observacao
		{														  
			get{return _Observacao;}
			set	{_Observacao = value;}
		}

	}
}
