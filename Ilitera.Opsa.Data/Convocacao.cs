using System;
using System.Text;
using System.Data;
using System.Collections;
using Ilitera.Data;
namespace Ilitera.Opsa.Data
{
	[Database("sied_novo", "tblConvocacao", "nID_CONVOCACAO")]
	public class Convocacao: Ilitera.Data.Table 
	{
		private int _nID_CONVOCACAO;
        private int _nID_EMPR;
        private DateTime _hDT_CONVOCACAO;
        private int _nID_CLINICA;
        private int _nID_MEDICO;
        private int _nID_EXAME_TIPO;
        private string _tOBSERVACAO;
        private DateTime _hDT_PLANEJADA;
        private int _nID_EMPREGADO;
        private string _nSenha_Temp;
        private string _eMail_Envio;
        private DateTime _Data_Envio;


        public Convocacao()
		{

		}
		public override int Id
		{														  
			get	{return _nID_CONVOCACAO;}
			set {_nID_CONVOCACAO = value;}
		}
		public int nID_Empr
		{														  
			get	{return _nID_EMPR;}
			set {_nID_EMPR = value;}
		}	
		public DateTime hDt_Convocacao
		{														  
			get	{return _hDT_CONVOCACAO;}
			set {_hDT_CONVOCACAO= value;}
		}
		public int nID_Clinica
		{														  
			get	{return _nID_CLINICA;}
			set {_nID_CLINICA = value;}
		}
		public int nID_Medico
		{														  
			get	{return _nID_MEDICO;}
			set {_nID_MEDICO = value;}
		}
		public int nID_Exame_Tipo
		{														  
			get	{return _nID_EXAME_TIPO;}
			set {_nID_EXAME_TIPO = value;}
        }
        public string tObservacao
        {
            get { return _tOBSERVACAO; }
            set { _tOBSERVACAO = value; }
        }
        public DateTime hDt_Planejada
        {
            get { return _hDT_PLANEJADA; }
            set { _hDT_PLANEJADA = value; }
        }
        public int nID_Empregado
        {
            get { return _nID_EMPREGADO; }
            set { _nID_EMPREGADO = value; }
        }
        public string nSenha_Temp
        {
            get { return _nSenha_Temp; }
            set { _nSenha_Temp = value; }
        }
        public string eMail_Envio
        {
            get { return _eMail_Envio; }
            set { _eMail_Envio = value; }
        }
        public DateTime Data_Envio
        {
            get { return _Data_Envio; }
            set { _Data_Envio = value; }
        }
    }
}
