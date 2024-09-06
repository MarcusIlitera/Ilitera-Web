using System;
using System.Data;
using System.Text;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	[Database("opsa", "ImportaEmpregado", "IdImportaEmpregado")]
	public class ImportaEmpregado: Ilitera.Data.Table
	{
		private int	_IdImportacaoEmpregado;
		private Cliente	_IdCliente;
		private string _Email = string.Empty;
		private string _Layout_Loja = string.Empty;
		private string _Layout_Matricula = string.Empty;
		private string _Layout_NomeEmpregado = string.Empty;
		private string _Layout_Admissao = string.Empty;
		private string _Layout_Demissao = string.Empty;
		private string _Layout_Nascimento = string.Empty;
		private string _Layout_CodigoSetor = string.Empty;
		private string _Layout_NomeSetor = string.Empty;
		private string _Layout_CodigoFuncao = string.Empty;
		private string _Layout_NomeFuncao = string.Empty;
		private string _Layout_InicioFuncao = string.Empty;
		private string _Layout_TerminoFuncao = string.Empty;
		private string _Layout_CBOFuncao = string.Empty;
		private string _Layout_LocalTrabalho = string.Empty;
		private string _Layout_GHE = string.Empty;
		private string _Layout_PIS = string.Empty;
		private string _Layout_RG = string.Empty;
		private string _Layout_CTPS = string.Empty;
		private string _Layout_CTPS_Numero = string.Empty;
		private string _Layout_CTPS_Serie = string.Empty;
		private string _Layout_CTPS_UF = string.Empty;
		private string _Layout_NumeroFoto = string.Empty;
		private string _Layout_Sexo = string.Empty;
		private string _Layout_Endereco_Logradouro = string.Empty;
		private string _Layout_Endereco_Numero = string.Empty;
		private string _Layout_Endereco_Complemento = string.Empty;
		private string _Layout_Endereco_Bairro = string.Empty;
		private string _Layout_Endereco_CEP = string.Empty;
		private string _Layout_Endereco_Municipio = string.Empty;
		private string _Layout_Endereco_UF = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ImportaEmpregado()
		{

		}
		public override int Id
		{														  
			get{return _IdImportacaoEmpregado;}
			set	{_IdImportacaoEmpregado = value;}
		}
		public Cliente IdCliente
		{														  
			get{return _IdCliente;}
			set	{_IdCliente = value;}
		}
		public string Email
		{														  
			get{return _Email;}
			set	{_Email = value;}
		}
		public string Layout_Loja
		{														  
			get{return _Layout_Loja;}
			set	{_Layout_Loja = value;}
		}
		public string Layout_Matricula
		{														  
			get{return _Layout_Matricula;}
			set	{_Layout_Matricula = value;}
		}
		public string Layout_NomeEmpregado
		{														  
			get{return _Layout_NomeEmpregado;}
			set	{_Layout_NomeEmpregado = value;}
		}
		public string Layout_Admissao
		{														  
			get{return _Layout_Admissao;}
			set	{_Layout_Admissao = value;}
		}
		public string Layout_Demissao
		{														  
			get{return _Layout_Demissao;}
			set	{_Layout_Demissao = value;}
		}
		public string Layout_Nascimento
		{														  
			get{return _Layout_Nascimento;}
			set	{_Layout_Nascimento = value;}
		}
		public string Layout_CodigoSetor
		{														  
			get{return _Layout_CodigoSetor;}
			set	{_Layout_CodigoSetor= value;}
		}
		public string Layout_NomeSetor
		{														  
			get{return _Layout_NomeSetor;}
			set	{_Layout_NomeSetor = value;}
		}
		public string Layout_CodigoFuncao
		{														  
			get{return _Layout_CodigoFuncao;}
			set	{_Layout_CodigoFuncao = value;}
		}
		public string Layout_NomeFuncao
		{														  
			get{return _Layout_NomeFuncao;}
			set	{_Layout_NomeFuncao = value;}
		}
		public string Layout_InicioFuncao
		{														  
			get{return _Layout_InicioFuncao;}
			set	{_Layout_InicioFuncao = value;}
		}
		public string Layout_TerminoFuncao
		{														  
			get{return _Layout_TerminoFuncao;}
			set	{_Layout_TerminoFuncao = value;}
		}
		public string Layout_CBOFuncao
		{														  
			get{return _Layout_CBOFuncao;}
			set	{_Layout_CBOFuncao = value;}
		}
		public string Layout_LocalTrabalho
		{														  
			get{return _Layout_LocalTrabalho;}
			set	{_Layout_LocalTrabalho = value;}
		}
		public string Layout_GHE
		{														  
			get{return _Layout_GHE;}
			set	{_Layout_GHE = value;}
		}
		public string Layout_PIS
		{														  
			get{return _Layout_PIS;}
			set	{_Layout_PIS = value;}
		}
		public string Layout_RG
		{														  
			get{return _Layout_RG;}
			set	{_Layout_RG = value;}
		}
		public string Layout_CTPS
		{														  
			get{return _Layout_CTPS;}
			set	{_Layout_CTPS = value;}
		}
		public string Layout_CTPS_Numero
		{														  
			get{return _Layout_CTPS_Numero;}
			set	{_Layout_CTPS_Numero = value;}
		}
		public string Layout_CTPS_Serie
		{														  
			get{return _Layout_CTPS_Serie;}
			set	{_Layout_CTPS_Serie = value;}
		}
		public string Layout_CTPS_UF
		{														  
			get{return _Layout_CTPS_UF;}
			set	{_Layout_CTPS_UF = value;}
		}
		public string Layout_NumeroFoto
		{														  
			get{return _Layout_NumeroFoto;}
			set	{_Layout_NumeroFoto = value;}
		}
		public string Layout_Sexo
		{														  
			get{return _Layout_Sexo;}
			set	{_Layout_Sexo = value;}
		}
		public string Layout_Endereco_Logradouro
		{														  
			get{return _Layout_Endereco_Logradouro;}
			set	{_Layout_Endereco_Logradouro = value;}
		}
		public string Layout_Endereco_Numero
		{														  
			get{return _Layout_Endereco_Numero;}
			set	{_Layout_Endereco_Numero = value;}
		}
		public string Layout_Endereco_Complemento
		{														  
			get{return _Layout_Endereco_Complemento;}
			set	{_Layout_Endereco_Complemento = value;}
		}
		public string Layout_Endereco_Bairro
		{														  
			get{return _Layout_Endereco_Bairro;}
			set	{_Layout_Endereco_Bairro = value;}
		}
		public string Layout_Endereco_CEP
		{														  
			get{return _Layout_Endereco_CEP;}
			set	{_Layout_Endereco_CEP = value;}
		}
		public string Layout_Endereco_Municipio
		{														  
			get{return _Layout_Endereco_Municipio;}
			set	{_Layout_Endereco_Municipio = value;}
		}
		public string Layout_Endereco_UF
		{														  
			get{return _Layout_Endereco_UF;}
			set	{_Layout_Endereco_UF = value;}
		}
	}
}
