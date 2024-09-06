using System;
using System.Data;
using System.Collections;
using Ilitera.Common;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	[Database("opsa", "ClinicaClienteExameDicionario", "IdClinicaClienteExameDicionario")]
	public class ClinicaClienteExameDicionario : Ilitera.Data.Table 
	{
		private int	_IdClinicaClienteExameDicionario;
		private ClinicaCliente	_IdClinicaCliente;
		private ClinicaExameDicionario	_IdClinicaExameDicionario;
		private bool _IsAutorizado;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ClinicaClienteExameDicionario()
		{

		}
		public override int Id
		{														  
			get{return _IdClinicaClienteExameDicionario;}
			set	{_IdClinicaClienteExameDicionario = value;}
		}
		public ClinicaCliente IdClinicaCliente 
		{														  
			get{return _IdClinicaCliente;}
			set	{_IdClinicaCliente = value;}
		}
		public ClinicaExameDicionario IdClinicaExameDicionario 
		{														  
			get{return _IdClinicaExameDicionario;}
			set	{_IdClinicaExameDicionario = value;}
		}
		public bool IsAutorizado 
		{														  
			get{return _IsAutorizado;}
			set	{_IsAutorizado = value;}
		}
	}
}
