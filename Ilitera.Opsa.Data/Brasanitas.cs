using System;
using System.Data;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{

	[Database("opsa", "_BRASANITAS_CADFUNC", "MATRICULA")]
	public class BrasanitasCadastroFuncionario: Ilitera.Data.Table
	{
		private int	_MATRICULA;
		private int	_CODIGOEMPRESA;
		private string _FUNCIONARIO = string.Empty;
		private DateTime _DATANASCIMENTO;
		private DateTime _DATAADMISSAO;
		private string _RG;
//		private float _PISPASEP;
//		private float _CPF;
//		private float _CODIGOCCUSTO;
//		private float _CODIGOCARGO;
//		private string _SEXO;
//		private float _CARTEIRAPROFISSIONAL;
//		private float _SERIEPROFISSIONAL;
//		private string _UF;
//		private float _CEP;
//		private string _ENDERECO;
//		private float _NUMERO;
		private string _COMPLEMENTO = string.Empty;
		private string _BAIRRO = string.Empty;
		private string _CIDADE = string.Empty;
		private string _UF1 = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public BrasanitasCadastroFuncionario()
		{

		}
		public override int Id
		{														  
			get{return _MATRICULA;}
			set	{_MATRICULA = value;}
		}
		public int CODIGOEMPRESA
		{														  
			get{return _CODIGOEMPRESA;}
			set	{_CODIGOEMPRESA = value;}
		}
		public string FUNCIONARIO
		{														  
			get{return _FUNCIONARIO;}
			set	{_FUNCIONARIO = value;}
		}
		public DateTime DATANASCIMENTO
		{														  
			get{return _DATANASCIMENTO;}
			set	{_DATANASCIMENTO = value;}
		}
		public DateTime DATAADMISSAO
		{														  
			get{return _DATAADMISSAO;}
			set	{_DATAADMISSAO = value;}
		}
		public string RG
		{														  
			get{return _RG;}
			set	{_RG = value;}
		}
//		public string RG
//		{														  
//			get{return _RG;}
//			set	{_RG = value;}
//		}
	}
}
