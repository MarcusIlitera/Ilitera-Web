using System;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	[Database("opsa","qryCliente","IdCliente")]
	public class DadosTecnicos : Ilitera.Data.Table 
	{
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public DadosTecnicos()
		{
		}
		private int _IdCliente;
		private string _Atividade = string.Empty;
		private Sindicato _IdSindicato;
		private DRT _IdDRT;
		private CNAE _IdCNAE;
		private DateTime _DataRegistroDRT = DateTime.Today;
		private string _NumeroRegistroDRT = string.Empty;
		private int _QtdEmpregados;
		private string _QtdCaldeiras = string.Empty;
		private string _QtdVasoPressao = string.Empty;
		private int _QtdEmpilhadeiras;
		private int _QtdPontes;
		private bool _ContrataCipa;
		private bool _RecargaExtintor;
		private bool _ObraCivil;


		public override int Id
		{
			get{return _IdCliente;}
			set{_IdCliente = value;}
		}
		public string Atividade
		{
			get{return _Atividade;}
			set{_Atividade = value;}
		}
		public Sindicato IdSindicato
		{
			get{return _IdSindicato;}
			set{_IdSindicato = value;}
		}
		public DRT IdDRT
		{
			get{return _IdDRT;}
			set{_IdDRT = value;}
		}
		
		[Obrigatorio(true,"O Campo CNAE é Obrigatório")]
		public CNAE IdCNAE
		{
			get
			{
				DadosCNAE();
				return _IdCNAE;
			}
			set
			{
				_IdCNAE = value;
				DadosCNAE();
			}
		}
		public DateTime DataRegistroDRT
		{
			get{return _DataRegistroDRT;}
			set{_DataRegistroDRT = value;}
		}
		public string NumeroRegistroDRT
		{
			get{return _NumeroRegistroDRT;}
			set{_NumeroRegistroDRT = value;}
		}
		public int QtdEmpregados
		{
			get{return _QtdEmpregados;}
			set{_QtdEmpregados = value;}
		}
		public string QtdCaldeiras
		{
			get{return _QtdCaldeiras;}
			set{_QtdCaldeiras = value;}
		}
		public string QtdVasoPressao
		{
			get{return _QtdVasoPressao;}
			set{_QtdVasoPressao = value;}
		}
		public int QtdEmpilhadeiras
		{
			get{return _QtdEmpilhadeiras;}
			set{_QtdEmpilhadeiras = value;}
		}
		public int QtdPontes
		{
			get{return _QtdPontes;}
			set{_QtdPontes = value;}
		}
		public bool ContrataCipa
		{
			get{return _ContrataCipa;}
			set{_ContrataCipa = value;}
		}
		public bool RecargaExtintor
		{
			get{return _RecargaExtintor;}
			set{_RecargaExtintor = value;}
		}
		public bool ObraCivil
		{
			get{return _ObraCivil;}
			set{_ObraCivil = value;}
		}

		public string _NomeCNAE;
		public string _GrupoCipa;
		public int _GrauRisco;

		private void DadosCNAE()
		{
			_IdCNAE.Find(); 
			_NomeCNAE = _IdCNAE.Descricao;
			_GrauRisco = _IdCNAE.GrauRisco;
			_IdCNAE.IdGrupoCipa.Find();
			_GrupoCipa = _IdCNAE.IdGrupoCipa.Descricao; 
		}
	}
}// END CLAss DEFINITION DadosTecnicos
