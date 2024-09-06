using System;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	[Database("opsa","PcmsoFrasePadraoDicionario","IdPcmsoFrasePadraoDicionario")]
	public class PcmsoFrasePadraoDicionario : Ilitera.Data.Table
	{
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PcmsoFrasePadraoDicionario()
		{
		}
		private int _IdPcmsoFrasePadraoDicionario;
		private string _Descricao=string.Empty; 

		public override int Id
		{
			get{return _IdPcmsoFrasePadraoDicionario;}
			set{_IdPcmsoFrasePadraoDicionario = value;}
		}
		[Obrigatorio(true, "Descrição é campo obrigatório.")]
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}	
	}
}
