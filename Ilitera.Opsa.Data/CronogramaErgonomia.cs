using System;
using System.Data;
using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Text;

namespace Ilitera.Opsa.Data
{	
	[Database("sied_novo", "tblCRN_ERG", "IdCRN_ERG", true)]
	public class CronogramaErgonomia : Ilitera.Data.Table
	{
		private int _IdCRN_ERG;
		private LaudoTecnico _nID_LAUD_TEC;
		private int _nQuestões;
		private string _AcaoLocalTrab = string.Empty;
		private bool _Mes01;
		private bool _Mes02;
		private bool _Mes03;
		private bool _Mes04;
		private bool _Mes05;
		private bool _Mes06;
		private bool _Mes07;
		private bool _Mes08;
		private bool _Mes09;
		private bool _Mes10;
		private bool _Mes11;
		private bool _Mes12;
		private string _Ano = string.Empty;
		private string _Serv = string.Empty;	
		private string _FormaReg = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CronogramaErgonomia()
		{

		}		
		public override int Id
		{														  
			get	{return _IdCRN_ERG;}
			set {_IdCRN_ERG = value;}
		}			
		public LaudoTecnico nID_LAUD_TEC
		{														  
			get	{return _nID_LAUD_TEC;}
			set {_nID_LAUD_TEC = value;}
		}	
		public int nQuestões
		{														  
			get	{return _nQuestões;}
			set {_nQuestões = value;}
		}	
		public string AcaoLocalTrab
		{														  
			get	{return _AcaoLocalTrab;}
			set {_AcaoLocalTrab = value;}
		}		
		public bool Mes01
		{														  
			get	{return _Mes01;}
			set {_Mes01 = value;}
		}	
		public bool Mes02
		{														  
			get	{return _Mes02;}
			set {_Mes02 = value;}
		}	
		public bool Mes03
		{														  
			get	{return _Mes03;}
			set {_Mes03 = value;}
		}	
		public bool Mes04
		{														  
			get	{return _Mes04;}
			set {_Mes04 = value;}
		}	
		public bool Mes05
		{														  
			get	{return _Mes05;}
			set {_Mes05 = value;}
		}	
		public bool Mes06
		{														  
			get	{return _Mes06;}
			set {_Mes06 = value;}
		}	
		public bool Mes07
		{														  
			get	{return _Mes07;}
			set {_Mes07 = value;}
		}	
		public bool Mes08
		{														  
			get	{return _Mes08;}
			set {_Mes08 = value;}
		}	
		public bool Mes09
		{														  
			get	{return _Mes09;}
			set {_Mes09 = value;}
		}	
		public bool Mes10
		{														  
			get	{return _Mes10;}
			set {_Mes10 = value;}
		}	
		public bool Mes11
		{														  
			get	{return _Mes11;}
			set {_Mes11 = value;}
		}	
		public bool Mes12
		{														  
			get	{return _Mes12;}
			set {_Mes12 = value;}
		}	
		public string Ano
		{														  
			get	{return _Ano;}
			set {_Ano = value;}
		}	
		public string Serv
		{														  
			get	{return _Serv;}
			set {_Serv = value;}
		}	
		public string FormaReg
		{														  
			get	{return _FormaReg;}
			set {_FormaReg = value;}
		}	

		public string GetNomeGhe()
		{			
			StringBuilder nomeGhe = new StringBuilder();
			
			StringBuilder sql = new StringBuilder();

            sql.Append("Use " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + " ");
            sql.Append("SELECT tblFUNC.tNO_FUNC ");
			sql.Append("FROM tblFUNC INNER JOIN (tblERG_NRs INNER JOIN tblERG_GHE ON ");
			sql.Append("tblERG_NRs.nID_NRs = tblERG_GHE.nID_NRs) ON (tblFUNC.nID_FUNC = tblERG_GHE.nID_FUNC) ");
			sql.Append("AND (tblFUNC.nID_LAUD_TEC = tblERG_GHE.nID_LAUD_TEC) ");
			sql.Append("WHERE (((tblERG_GHE.nID_LAUD_TEC)="+this.nID_LAUD_TEC.Id+") AND ((tblERG_GHE.nRSP)=2) ");
			sql.Append("AND ((tblERG_NRs.nQuestões)="+this.nQuestões +")) ORDER BY tblFUNC.tNO_FUNC ");
			
			DataSet ds = this.ExecuteDataset(sql.ToString());
			
			foreach(DataRow row in ds.Tables[0].Rows)
				nomeGhe.Append(row["tNO_FUNC"] + "\n");

			return nomeGhe.ToString();				
		}


        public string GetNomeGhe_New()
        {
            StringBuilder nomeGhe = new StringBuilder();

            StringBuilder sql = new StringBuilder();

            sql.Append("Use " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + " ");
            sql.Append("SELECT tblFUNC.tNO_FUNC ");
            sql.Append("FROM tblFUNC INNER JOIN (tblERG_NRs_NEW INNER JOIN tblERG_GHE_NEW ON ");
            sql.Append("tblERG_NRs_New.nID_NRs = tblERG_GHE_NEW.nID_NRs) ON (tblFUNC.nID_FUNC = tblERG_GHE_New.nID_FUNC) ");
            sql.Append("AND (tblFUNC.nID_LAUD_TEC = tblERG_GHE_NEW.nID_LAUD_TEC) ");
            sql.Append("WHERE (((tblERG_GHE_NEW.nID_LAUD_TEC)=" + this.nID_LAUD_TEC.Id + ") AND ((tblERG_GHE_NEW.nRSP)=2) ");
            sql.Append("AND ((tblERG_NRs_NEW.nQuestões)=" + this.nQuestões + ")) ORDER BY tblFUNC.tNO_FUNC ");

            DataSet ds = this.ExecuteDataset(sql.ToString());

            foreach (DataRow row in ds.Tables[0].Rows)
                nomeGhe.Append(row["tNO_FUNC"] + "\n");

            return nomeGhe.ToString();
        }


    }


    [Database("sied_novo", "tblCRN_PCA", "IdCRN_PCA", true)]
    public class CronogramaPCA : Ilitera.Data.Table
    {
        private int _IdCRN_PCA;
        private LaudoTecnico _nID_LAUD_TEC;
        private int _nQuestões;
        private string _AcaoLocalTrab = string.Empty;
        private bool _Mes01;
        private bool _Mes02;
        private bool _Mes03;
        private bool _Mes04;
        private bool _Mes05;
        private bool _Mes06;
        private bool _Mes07;
        private bool _Mes08;
        private bool _Mes09;
        private bool _Mes10;
        private bool _Mes11;
        private bool _Mes12;
        private string _Ano = string.Empty;
        private string _Serv = string.Empty;
        private string _FormaReg = string.Empty;

        public CronogramaPCA()
        {

        }
        public override int Id
        {
            get { return _IdCRN_PCA; }
            set { _IdCRN_PCA = value; }
        }
        public LaudoTecnico nID_LAUD_TEC
        {
            get { return _nID_LAUD_TEC; }
            set { _nID_LAUD_TEC = value; }
        }
        public int nQuestões
        {
            get { return _nQuestões; }
            set { _nQuestões = value; }
        }
        public string AcaoLocalTrab
        {
            get { return _AcaoLocalTrab; }
            set { _AcaoLocalTrab = value; }
        }
        public bool Mes01
        {
            get { return _Mes01; }
            set { _Mes01 = value; }
        }
        public bool Mes02
        {
            get { return _Mes02; }
            set { _Mes02 = value; }
        }
        public bool Mes03
        {
            get { return _Mes03; }
            set { _Mes03 = value; }
        }
        public bool Mes04
        {
            get { return _Mes04; }
            set { _Mes04 = value; }
        }
        public bool Mes05
        {
            get { return _Mes05; }
            set { _Mes05 = value; }
        }
        public bool Mes06
        {
            get { return _Mes06; }
            set { _Mes06 = value; }
        }
        public bool Mes07
        {
            get { return _Mes07; }
            set { _Mes07 = value; }
        }
        public bool Mes08
        {
            get { return _Mes08; }
            set { _Mes08 = value; }
        }
        public bool Mes09
        {
            get { return _Mes09; }
            set { _Mes09 = value; }
        }
        public bool Mes10
        {
            get { return _Mes10; }
            set { _Mes10 = value; }
        }
        public bool Mes11
        {
            get { return _Mes11; }
            set { _Mes11 = value; }
        }
        public bool Mes12
        {
            get { return _Mes12; }
            set { _Mes12 = value; }
        }
        public string Ano
        {
            get { return _Ano; }
            set { _Ano = value; }
        }
        public string Serv
        {
            get { return _Serv; }
            set { _Serv = value; }
        }
        public string FormaReg
        {
            get { return _FormaReg; }
            set { _FormaReg = value; }
        }



    }



    [Database("sied_novo", "tblCRN_PPR", "IdCRN_PPR", true)]
    public class CronogramaPPR : Ilitera.Data.Table
    {
        private int _IdCRN_PPR;
        private LaudoTecnico _nID_LAUD_TEC;
        private int _nQuestões;
        private string _AcaoLocalTrab = string.Empty;
        private bool _Mes01;
        private bool _Mes02;
        private bool _Mes03;
        private bool _Mes04;
        private bool _Mes05;
        private bool _Mes06;
        private bool _Mes07;
        private bool _Mes08;
        private bool _Mes09;
        private bool _Mes10;
        private bool _Mes11;
        private bool _Mes12;
        private string _Ano = string.Empty;
        private string _Serv = string.Empty;
        private string _FormaReg = string.Empty;

        public CronogramaPPR()
        {

        }
        public override int Id
        {
            get { return _IdCRN_PPR; }
            set { _IdCRN_PPR = value; }
        }
        public LaudoTecnico nID_LAUD_TEC
        {
            get { return _nID_LAUD_TEC; }
            set { _nID_LAUD_TEC = value; }
        }
        public int nQuestões
        {
            get { return _nQuestões; }
            set { _nQuestões = value; }
        }
        public string AcaoLocalTrab
        {
            get { return _AcaoLocalTrab; }
            set { _AcaoLocalTrab = value; }
        }
        public bool Mes01
        {
            get { return _Mes01; }
            set { _Mes01 = value; }
        }
        public bool Mes02
        {
            get { return _Mes02; }
            set { _Mes02 = value; }
        }
        public bool Mes03
        {
            get { return _Mes03; }
            set { _Mes03 = value; }
        }
        public bool Mes04
        {
            get { return _Mes04; }
            set { _Mes04 = value; }
        }
        public bool Mes05
        {
            get { return _Mes05; }
            set { _Mes05 = value; }
        }
        public bool Mes06
        {
            get { return _Mes06; }
            set { _Mes06 = value; }
        }
        public bool Mes07
        {
            get { return _Mes07; }
            set { _Mes07 = value; }
        }
        public bool Mes08
        {
            get { return _Mes08; }
            set { _Mes08 = value; }
        }
        public bool Mes09
        {
            get { return _Mes09; }
            set { _Mes09 = value; }
        }
        public bool Mes10
        {
            get { return _Mes10; }
            set { _Mes10 = value; }
        }
        public bool Mes11
        {
            get { return _Mes11; }
            set { _Mes11 = value; }
        }
        public bool Mes12
        {
            get { return _Mes12; }
            set { _Mes12 = value; }
        }
        public string Ano
        {
            get { return _Ano; }
            set { _Ano = value; }
        }
        public string Serv
        {
            get { return _Serv; }
            set { _Serv = value; }
        }
        public string FormaReg
        {
            get { return _FormaReg; }
            set { _FormaReg = value; }
        }



    }






    [Database("sied_novo", "tblCRN_PGR", "IdCRN_PGR", true)]
    public class CronogramaPGR : Ilitera.Data.Table
    {
        private int _IdCRN_PGR;
        private LaudoTecnico _nID_LAUD_TEC;
        private int _nQuestões;
        private string _AcaoLocalTrab = string.Empty;
        private bool _Mes01;
        private bool _Mes02;
        private bool _Mes03;
        private bool _Mes04;
        private bool _Mes05;
        private bool _Mes06;
        private bool _Mes07;
        private bool _Mes08;
        private bool _Mes09;
        private bool _Mes10;
        private bool _Mes11;
        private bool _Mes12;
        private string _Acao = string.Empty;
        private string _Ano = string.Empty;
        private string _Serv = string.Empty;
        private string _FormaReg = string.Empty;
        private Int16 _Prioridade;
        private string _Metodologia = string.Empty;
        private int _nId_Func;

        public CronogramaPGR()
        {

        }
        public override int Id
        {
            get { return _IdCRN_PGR; }
            set { _IdCRN_PGR = value; }
        }
        public LaudoTecnico nID_LAUD_TEC
        {
            get { return _nID_LAUD_TEC; }
            set { _nID_LAUD_TEC = value; }
        }
        public int nQuestões
        {
            get { return _nQuestões; }
            set { _nQuestões = value; }
        }
        public string AcaoLocalTrab
        {
            get { return _AcaoLocalTrab; }
            set { _AcaoLocalTrab = value; }
        }
        public bool Mes01
        {
            get { return _Mes01; }
            set { _Mes01 = value; }
        }
        public bool Mes02
        {
            get { return _Mes02; }
            set { _Mes02 = value; }
        }
        public bool Mes03
        {
            get { return _Mes03; }
            set { _Mes03 = value; }
        }
        public bool Mes04
        {
            get { return _Mes04; }
            set { _Mes04 = value; }
        }
        public bool Mes05
        {
            get { return _Mes05; }
            set { _Mes05 = value; }
        }
        public bool Mes06
        {
            get { return _Mes06; }
            set { _Mes06 = value; }
        }
        public bool Mes07
        {
            get { return _Mes07; }
            set { _Mes07 = value; }
        }
        public bool Mes08
        {
            get { return _Mes08; }
            set { _Mes08 = value; }
        }
        public bool Mes09
        {
            get { return _Mes09; }
            set { _Mes09 = value; }
        }
        public bool Mes10
        {
            get { return _Mes10; }
            set { _Mes10 = value; }
        }
        public bool Mes11
        {
            get { return _Mes11; }
            set { _Mes11 = value; }
        }
        public bool Mes12
        {
            get { return _Mes12; }
            set { _Mes12 = value; }
        }
        public string Acao
        {
            get { return _Acao; }
            set { _Acao = value; }
        }
        public string Ano
        {
            get { return _Ano; }
            set { _Ano = value; }
        }
        public string Serv
        {
            get { return _Serv; }
            set { _Serv = value; }
        }
        public string FormaReg
        {
            get { return _FormaReg; }
            set { _FormaReg = value; }
        }
        public Int16 Prioridade
        {
            get { return _Prioridade; }
            set { _Prioridade = value; }
        }
        public string Metodologia
        {
            get { return _Metodologia; }
            set { _Metodologia = value; }
        }
        public int nId_Func
        {
            get { return _nId_Func; }
            set { _nId_Func = value; }
        }


    }



}

