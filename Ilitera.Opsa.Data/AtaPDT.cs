using System;
using System.Data;
using System.Collections;
using System.Text;
using Ilitera.Common;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	[Database("pdtdos","ATAS","IdATAS")]
	public class AtaPDT : Ilitera.Data.Table
	{
		private int _IdATAS;
		private float _CODCAD;
		private float _REUNIAO;
		private float _TEXTO_PADR;
		private DateTime _DATA = new DateTime();
		private string _LOCAL = string.Empty;
		private string _HORARIO = string.Empty;
		private string _TXT_ACID = string.Empty;
		private string _TXT_SEGMED = string.Empty;
		private string _PRESID = string.Empty;
		private string _P_MEMBRO1 = string.Empty;
		private string _P_MEMBRO2 = string.Empty;
		private string _P_MEMBRO3 = string.Empty;
		private string _P_MEMBRO4 = string.Empty;
		private string _P_MEMBRO5 = string.Empty;
		private string _VICE_PRES = string.Empty;
		private string _E_MEMBRO1 = string.Empty;
		private string _E_MEMBRO2 = string.Empty;
		private string _E_MEMBRO3 = string.Empty;
		private string _E_MEMBRO4 = string.Empty;
		private string _E_MEMBRO5 = string.Empty;
		private string _SECRET = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AtaPDT()
		{

		}

		public override int Id
		{														  
			get{return _IdATAS;}
			set	{_IdATAS = value;}
		}
		public float CODCAD
		{	  
			get	{return _CODCAD;}
			set {_CODCAD = value;}
		}
		public float REUNIAO
		{			  
			get	{return _REUNIAO;}
			set {_REUNIAO = value;}
		}
		public float TEXTO_PADR
		{														  
			get	{return _TEXTO_PADR;}
			set {_TEXTO_PADR = value;}
		}
		public DateTime DATA
		{							  
			get	{return _DATA;}
			set {_DATA = value;}
		}
		public string LOCAL
		{														  
			get	{return _LOCAL;}
			set {_LOCAL = value;}
		}
		public string HORARIO
		{														  
			get	{return _HORARIO;}
			set {_HORARIO = value;}
		}
		public string TXT_ACID
		{														  
			get	{return _TXT_ACID;}
			set {_TXT_ACID = value;}
		}
		public string TXT_SEGMED
		{														  
			get	{return _TXT_SEGMED;}
			set {_TXT_SEGMED = value;}
		}		
		public string PRESID
		{														  
			get	{return _PRESID;}
			set {_PRESID = value;}
		}
		public string P_MEMBRO1
		{														  
			get	{return _P_MEMBRO1;}
			set {_P_MEMBRO1 = value;}
		}
		public string P_MEMBRO2
		{														  
			get	{return _P_MEMBRO2;}
			set {_P_MEMBRO2 = value;}
		}
		public string P_MEMBRO3
		{														  
			get	{return _P_MEMBRO3;}
			set {_P_MEMBRO3 = value;}
		}
		public string P_MEMBRO4
		{														  
			get	{return _P_MEMBRO4;}
			set {_P_MEMBRO4 = value;}
		}
		public string P_MEMBRO5
		{														  
			get	{return _P_MEMBRO5;}
			set {_P_MEMBRO5 = value;}
		}
		public string VICE_PRES
		{														  
			get	{return _VICE_PRES;}
			set {_VICE_PRES = value;}
		}
		public string E_MEMBRO1
		{														  
			get	{return _E_MEMBRO1;}
			set {_E_MEMBRO1 = value;}
		}
		public string E_MEMBRO2
		{														  
			get	{return _E_MEMBRO2;}
			set {_E_MEMBRO2 = value;}
		}
		public string E_MEMBRO3
		{														  
			get	{return _E_MEMBRO3;}
			set {_E_MEMBRO3 = value;}
		}
		public string E_MEMBRO4
		{														  
			get	{return _E_MEMBRO4;}
			set {_E_MEMBRO4 = value;}
		}
		public string E_MEMBRO5
		{														  
			get	{return _E_MEMBRO5;}
			set {_E_MEMBRO5 = value;}
		}
		public string SECRET
		{														  
			get	{return _SECRET;}
			set {_SECRET = value;}
		}

        public static string GetResumoTextoPadrao(int IdTextoPadrao)
        {
            string ret;

            switch (IdTextoPadrao)
            {
                case 1:
                    ret = "PTRAB.MR.,PPRA,PCMSO,INSP.SEG";
                    break;

                case 2:
                    ret = "AVAL.DE METAS, INF.TRABALHADOR";
                    break;

                case 3:
                    ret = "SIPAT";
                    break;

                case 4:
                    ret = "EPI,PARTIC.REUNIAO EMPREGADOR";
                    break;

                case 5:
                    ret = "CURSOS E INSPECOES PREV.ACID.";
                    break;

                case 6:
                    ret = "ANÁLISE ERGONÔMICA DO TRABALHO";
                    break;

                case 7:
                    ret = "PLANO DE TRABALHO DA CIPA";
                    break;

                case 8:
                    ret = "INVESTIGACAO DE ACIDENTES";
                    break;

                case 9:
                    ret = "EPI FORNEC.E USO ADEQUADO";
                    break;

                case 10:
                    ret = "EMPRESAS CONTRATADAS SEG.TRAB";
                    break;

                case 11:
                    ret = "PROTEÇÃO CONTRA INCÊNCIOS";
                    break;

                case 12:
                    ret = "EXTINTORES DE INCÊNDIO";
                    break;

                case 13:
                    ret = "CAMPANHA PREVENTIVA";
                    break;

                case 14:
                    ret = "MEDIDAS PREVENTIVAS";
                    break;

                case 15:
                    ret = "NORMAS DE SEGURANÇA";
                    break;

                case 16:
                    ret = "INVESTIGAÇÃO DOS ACIDENTES";
                    break;

                case 17:
                    ret = "CONDIÇÕES INSEGURAS";
                    break;

                case 18:
                    ret = "INFORMAÇÕES SOBRE RISCOS";
                    break;

                case 19:
                    ret = "ARMAZENAGEM COMB. INFLAMÁVEIS";
                    break;

                case 20:
                    ret = "SANEAMENTO";
                    break;

                case 21:
                    ret = "INSP. TÉC. - CONDIÇÃO INSEGURA";
                    break;

                case 22:
                    ret = "SINALIZAÇÃO DE SEGURANÇA";
                    break;

                case 23:
                    ret = "PCMAT";
                    break;

                case 24:
                    ret = "SUGESTÕES CURSO, TREIN. CAMP.";
                    break;

                case 25:
                    ret = "INVESTIGAÇÃO ACID. - CONVOCAÇÃO";
                    break;

                case 26:
                    ret = "NORMAS REGULAMENTADORAS";
                    break;

                case 27:
                    ret = "NORMAS P/INSPEÇÕES SEGURANÇA";
                    break;

                case 28:
                    ret = "USO ADEGUADO E TREIN. DE EPI";
                    break;

                case 29:
                    ret = "TEXTO 2ª REUNIÃO (PPRA E M.R.)";
                    break;

                case 30:
                    ret = "EMPRESA TEXTO PRÓPRIO";
                    break;

                default:
                    ret = string.Empty;
                    break;
            }

            return ret;
        }

        public DataSet GetListaAtas(int IdCliente)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdATAS", Type.GetType("System.String"));
            table.Columns.Add("Data", Type.GetType("System.String"));
            table.Columns.Add("Reunião", Type.GetType("System.String"));
            table.Columns.Add("Texto Padrão", Type.GetType("System.String"));
            ds.Tables.Add(table);
            DataRow newRow;

            ArrayList listAtas = this.Find("CODCAD=" + IdCliente + " ORDER BY DATA Desc");

            foreach (AtaPDT ata in listAtas)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["IdATAS"] = ata.Id;
                newRow["Data"] = ata.DATA.ToString("dd-MM-yyyy");
                newRow["Reunião"] = ata.REUNIAO;
                newRow["Texto Padrão"] = Convert.ToInt32(ata.TEXTO_PADR).ToString()+ " - " +  AtaPDT.GetResumoTextoPadrao(Convert.ToInt32(ata.TEXTO_PADR));
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }

		public DataSet DataSourceRptAtaReuniaoCipa()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("tNomeCompleto", Type.GetType("System.String"));
			table.Columns.Add("tLogradouro", Type.GetType("System.String"));
			table.Columns.Add("tNumero", Type.GetType("System.String"));
			table.Columns.Add("tMunicipio", Type.GetType("System.String"));
			table.Columns.Add("tNomeCodigo", Type.GetType("System.String"));
			table.Columns.Add("tTituloDoRelatorio", Type.GetType("System.String"));
			table.Columns.Add("dDT_REUNIAO", Type.GetType("System.DateTime"));
			table.Columns.Add("tHORARIO", Type.GetType("System.String"));
			table.Columns.Add("tLOCAL", Type.GetType("System.String"));
			table.Columns.Add("tTEXTO", Type.GetType("System.String"));
			ds.Tables.Add(table);
            Cliente cliente = new Cliente();
            cliente.Find("CodigoAntigo =" + this.CODCAD);
			Endereco endereco = cliente.GetEndereco();
			DataRow newRow;
			newRow = ds.Tables[0].NewRow();
			newRow["tNomeCompleto"] = cliente.NomeCompleto;
			newRow["tLogradouro"] = endereco.GetEndereco();
			newRow["tNumero"] = endereco.Numero;
			endereco.IdMunicipio.Find();
			newRow["tMunicipio"] = endereco.IdMunicipio.NomeAbreviado;
			newRow["tNomeCodigo"] = cliente.NomeCodigo;
			newRow["tTituloDoRelatorio"] = this.GetTituloReuniao();
			newRow["dDT_REUNIAO"] = this.DATA.ToString("dd-MM-yyyy");
			newRow["tHORARIO"] = this.HORARIO;
			newRow["tLOCAL"] = this.LOCAL;
			newRow["tTEXTO"] = this.GetTextoAta();
			ds.Tables[0].Rows.Add(newRow);
			return ds;
		}

		private string GetTituloReuniao()
		{
			StringBuilder txt = new StringBuilder();			
			txt.Append("ATA DA REUNIÃO MENSAL Nº ");
			txt.Append(this.REUNIAO.ToString("00"));
			txt.Append(" DA CIPA");
			return txt.ToString();
		}

        private string GetTextoAta()
        {
            StringBuilder textoAta = new StringBuilder();

            int IdEventoBase = (Convert.ToInt32(this.REUNIAO) + 12);

            FrasePadrao frasePadrao = new FrasePadrao();
            frasePadrao.Find("IdEventoBaseCipa=" + IdEventoBase);

            textoAta.Append(frasePadrao.Abertura + " ");
            textoAta.Append(this.TXT_ACID + " ");
            textoAta.Append(this.TXT_SEGMED + " ");
            textoAta.Append(frasePadrao.Encerramento);
            textoAta.Replace("<<secretaria>>", this.SECRET);
            textoAta.Replace("<<nomeSecretaria(o)>>", this.SECRET);

            return textoAta.ToString();
        }

		public DataSet DataSourceRptAtaReuniaoCipaParticipantes()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("tINDICADO", Type.GetType("System.String"));
			table.Columns.Add("tCARGO", Type.GetType("System.String"));
			table.Columns.Add("nNUMERO", Type.GetType("System.Int32"));
			table.Columns.Add("tNOME", Type.GetType("System.String"));
			table.Columns.Add("tTEXTO", Type.GetType("System.String"));
			ds.Tables.Add(table);
			DataRow newRow;			
			
			if(this.PRESID!=string.Empty)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["tINDICADO"] = "Representantes do Empregador (Designados)";
				newRow["nNUMERO"]= 0;
				newRow["tCARGO"] = "Presidente";
				newRow["tNOME"] = this.PRESID;
				ds.Tables[0].Rows.Add(newRow);
			}
			
			if(this.P_MEMBRO1!=string.Empty)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["tINDICADO"] = "Representantes do Empregador (Designados)";
				newRow["nNUMERO"]= 1;
				newRow["tCARGO"] = "Membro";
				newRow["tNOME"] = this.P_MEMBRO1;
				ds.Tables[0].Rows.Add(newRow);
			}

			if(this.P_MEMBRO2!=string.Empty)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["tINDICADO"] = "Representantes do Empregador (Designados)";
				newRow["nNUMERO"]= 2;
				newRow["tCARGO"] = "Membro";
				newRow["tNOME"] = this.P_MEMBRO2;
				ds.Tables[0].Rows.Add(newRow);
			}

			if(this.P_MEMBRO3!=string.Empty)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["tINDICADO"] = "Representantes do Empregador (Designados)";
				newRow["nNUMERO"]= 3;
				newRow["tCARGO"] = "Membro";
				newRow["tNOME"] = this.P_MEMBRO3;
				ds.Tables[0].Rows.Add(newRow);
			}

			if(this.P_MEMBRO4!=string.Empty)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["tINDICADO"] = "Representantes do Empregador (Designados)";
				newRow["nNUMERO"]= 4;
				newRow["tCARGO"] = "Membro";
				newRow["tNOME"] = this.P_MEMBRO4;
				ds.Tables[0].Rows.Add(newRow);
			}

			if(this.P_MEMBRO5!=string.Empty)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["tINDICADO"] = "Representantes do Empregador (Designados)";
				newRow["nNUMERO"]= 5;
				newRow["tCARGO"] = "Membro";
				newRow["tNOME"] = this.P_MEMBRO5;
				ds.Tables[0].Rows.Add(newRow);
			}
			
			if(this.VICE_PRES!=string.Empty)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["tINDICADO"] = "Representantes dos Empregados (Eleitos)";
				newRow["nNUMERO"]= 0;
				newRow["tCARGO"] = "Vice - Presidente";
				newRow["tNOME"] = this.VICE_PRES;
				ds.Tables[0].Rows.Add(newRow);
			}
			
			if(this.E_MEMBRO1!=string.Empty)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["tINDICADO"] = "Representantes dos Empregados (Eleitos)";
				newRow["nNUMERO"]= 1;
				newRow["tCARGO"] = "Membro";
				newRow["tNOME"] = this.E_MEMBRO1;
				ds.Tables[0].Rows.Add(newRow);
			}

			if(this.E_MEMBRO2!=string.Empty)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["tINDICADO"] = "Representantes dos Empregados (Eleitos)";
				newRow["nNUMERO"]= 2;
				newRow["tCARGO"] = "Membro";
				newRow["tNOME"] = this.E_MEMBRO2;
				ds.Tables[0].Rows.Add(newRow);
			}

			if(this.E_MEMBRO3!=string.Empty)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["tINDICADO"] = "Representantes dos Empregados (Eleitos)";
				newRow["nNUMERO"]= 3;
				newRow["tCARGO"] = "Membro";
				newRow["tNOME"] = this.E_MEMBRO3;
				ds.Tables[0].Rows.Add(newRow);
			}

			if(this.E_MEMBRO4!=string.Empty)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["tINDICADO"] = "Representantes dos Empregados (Eleitos)";
				newRow["nNUMERO"]= 4;
				newRow["tCARGO"] = "Membro";
				newRow["tNOME"] = this.E_MEMBRO4;
				ds.Tables[0].Rows.Add(newRow);
			}

			if(this.E_MEMBRO5!=string.Empty)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["tINDICADO"] = "Representantes dos Empregados (Eleitos)";
				newRow["nNUMERO"]= 5;
				newRow["tCARGO"] = "Membro";
				newRow["tNOME"] = this.E_MEMBRO5;
				ds.Tables[0].Rows.Add(newRow);
			}

			if(this.SECRET!=string.Empty)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["tINDICADO"] = "Secretaria";
				newRow["nNUMERO"]= 0;
				newRow["tCARGO"] = "Secretário(a)";
				newRow["tNOME"] = this.SECRET;
				ds.Tables[0].Rows.Add(newRow);		
			}
			
			return ds;
		}	
	}
}
