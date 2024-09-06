using System;
using Ilitera.Common;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	public enum Empresas: int
	{
		MestraPaulista=310,
		MestraEPP=837,		
		Opsa=550425258,
	}
	
	[Database("opsa", "Empresa", "IdEmpresa")]
	public class Empresa : Ilitera.Common.Juridica
	{
		private int	_IdEmpresa;
		private string _TextoEnvioBoleto=string.Empty;
		public Empresa()
		{

		}
		public override int Id
		{														  
			get{return _IdEmpresa;}
			set	{_IdEmpresa = value;}
		}
		public string TextoEnvioBoleto
		{														  
			get{return _TextoEnvioBoleto;}
			set	{_TextoEnvioBoleto = value;}
		}		
	}
}
