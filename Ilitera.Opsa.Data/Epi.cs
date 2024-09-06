using System;
using System.Data;
using System.Collections;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	public enum EpiTipo: int
	{
		Auditiva = 1,
		Respiratoria=2,
		Outros=3,
	}

	[Database("opsa", "Epi", "IdEpi")]
	public class Epi : Ilitera.Data.Table
	{
		private int _IdEpi; 
//		private Risco _IdRisco; 
//		private AgenteQuimico _IdAgenteQuimico;
		private int _IndEpi;
//		private string _Nome = string.Empty;
		private string _Descricao = string.Empty;
//		private bool _Sugerir;
		private bool _Acidente;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Epi()
		{

		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Epi(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{														  
			get	{return _IdEpi;}
			set {_IdEpi = value;}
		}
//		public Risco IdRisco
//		{														  
//			get	{return _IdRisco;}
//			set {_IdRisco = value;}
//		}
//		public AgenteQuimico IdAgenteQuimico
//		{														  
//			get	{return _IdAgenteQuimico;}
//			set {_IdAgenteQuimico = value;}
//		}
		public int IndEpi
		{														  
			get	{return _IndEpi;}
			set {_IndEpi = value;}
		}
//		public string Nome
//		{														  
//			get	{return _Nome;}
//			set {_Nome = value;}
//		}
		public string Descricao
		{														  
			get	{return _Descricao;}
			set {_Descricao = value;}
		}
//		public bool Sugerir
//		{														  
//			get	{return _Sugerir;}
//			set {_Sugerir = value;}
//		}
		public bool Acidente
		{														  
			get	{return _Acidente;}
			set {_Acidente = value;}
		}
		public override string ToString()
		{
			return _Descricao;
		}
	}

    [Database("opsa", "EPIRepositorio", "IdEPIRepositorio")]
    public class EPIRepositorio : Ilitera.Data.Table
    {
        
        private int _IdEPIRepositorio;
        private int _IdEmpregado;
        private DateTime _DataHora;
        private string _Descricao;
        private string _Anexo;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EPIRepositorio()
        {

        }

        public override int Id
        {
            get { return _IdEPIRepositorio; }
            set { _IdEPIRepositorio = value; }
        }
        public int IdEmpregado
        {
            get { return _IdEmpregado; }
            set { _IdEmpregado = value; }
        }
        public DateTime DataHora
        {
            get { return _DataHora; }
            set { _DataHora = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public string Anexo
        {
            get { return _Anexo; }
            set { _Anexo = value; }
        }

    }



    [Database("opsa", "Repositorio", "IdRepositorio")]
    public class Repositorio : Ilitera.Data.Table
    {

        private int _IdRepositorio;
        private int _IdCliente;
        private string _TipoDocumento;
        private DateTime _DataHora;
        private string _Descricao;
        private string _Anexo;
        private int _nId_Laud_Tec;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Repositorio()
        {

        }

        public override int Id
        {
            get { return _IdRepositorio; }
            set { _IdRepositorio = value; }
        }
        public int IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        public string TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; }
        }

        public DateTime DataHora
        {
            get { return _DataHora; }
            set { _DataHora = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public string Anexo
        {
            get { return _Anexo; }
            set { _Anexo = value; }
        }
        public int nId_Laud_Tec
        {
            get { return _nId_Laud_Tec; }
            set { _nId_Laud_Tec = value; }
        }


    }


}
