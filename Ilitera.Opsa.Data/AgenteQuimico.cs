using System;
using System.Data;
using System.Collections;
using System.Text;

using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	public enum GrauInsalubridade: int
	{
		GrauMaximo=1,
		GrauMedio,
		GrauMinimo
	}

	[Database("opsa", "AgenteQuimico", "IdAgenteQuimico")]
	public class AgenteQuimico: Ilitera.Data.Table
	{
		private int _IdAgenteQuimico;
		private Risco _IdRisco;
		private GrauInsalubridade _IndGrauInsalubridade;
		private string _Nome=string.Empty;
		private string _Sinonimos=string.Empty;
		private string _DescricaoFrase=string.Empty;
        //		private string _EPC = string.Empty;
        //		private int _IndViasPenetracao;
        //		private string _RamoAtividade=string.Empty;
        //		private string _MetodologiaAvaliacao=string.Empty;
        //		private string _DanosRelacionadosSaude=string.Empty;
        //		private string _MedidasControle=string.Empty;
        //		private string _PropriedadesQuimicas=string.Empty;
        //		private string _AtividadesRisco=string.Empty;
        //		private string _RecomendacaoMedico=string.Empty;

        private string _DanosRelacionadosSaude = string.Empty;

        private string _NumeroCAS;
        private string _Codigo_eSocial;
        private string _Arquivo = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AgenteQuimico()
		{

		}
		public override int Id
		{														  
			get	{return _IdAgenteQuimico;}
			set {_IdAgenteQuimico = value;}
		}
		[Obrigatorio(true, "Risco do agente químico é campo obrigatório!")]
		public Risco IdRisco
		{														  
			get	{return _IdRisco;}
			set {_IdRisco = value;}
		}
		[Obrigatorio(true, "Nome do agente químico é campo obrigatório!")]
		public string Nome
		{														  
			get	{return _Nome;}
			set {_Nome = value;}
		}
		public string Sinonimos
		{														  
			get	{return _Sinonimos;}
			set {_Sinonimos = value;}
		}
		public string DescricaoFrase
		{														  
			get	{return _DescricaoFrase;}
			set {_DescricaoFrase = value;}
		}
        public GrauInsalubridade IndGrauInsalubridade
		{														  
			get	{return _IndGrauInsalubridade;}
			set {_IndGrauInsalubridade = value;}
		}
//		public string EPC
//		{														  
//			get	{return _EPC;}
//			set {_EPC = value;}
//		}
//		public int IndViasPenetracao
//		{
//			get	{return _IndViasPenetracao;}
//			set {_IndViasPenetracao = value;}
//		}
//		public string RamoAtividade
//		{														  
//			get	{return _RamoAtividade;}
//			set {_RamoAtividade = value;}
//		}
//		public string MetodologiaAvaliacao
//		{														  
//			get	{return _MetodologiaAvaliacao;}
//			set {_MetodologiaAvaliacao = value;}
//		}
		public string DanosRelacionadosSaude
		{														  
			get	{return _DanosRelacionadosSaude;}
			set {_DanosRelacionadosSaude = value;}
		}
//		public string MedidasControle
//		{														  
//			get	{return _MedidasControle;}
//			set {_MedidasControle = value;}
//		}		
//		public string PropriedadesQuimicas
//		{														  
//			get	{return _PropriedadesQuimicas;}
//			set {_PropriedadesQuimicas = value;}
//		}
//		public string AtividadesRisco
//		{														  
//			get	{return _AtividadesRisco;}
//			set {_AtividadesRisco = value;}
//		}
//		public string RecomendacaoMedico
//		{														  
//			get	{return _RecomendacaoMedico;}
//			set {_RecomendacaoMedico = value;}
//		}
		public string Arquivo
		{														  
			get	{return _Arquivo;}
			set {_Arquivo = value;}
		}

        public string NumeroCAS
        {
            get { return _NumeroCAS; }
            set { _NumeroCAS = value; }
        }

        public string Codigo_eSocial
        {
            get { return _Codigo_eSocial; }
            set { _Codigo_eSocial = value; }
        }



        public override string ToString()
		{
            if (this.mirrorOld == null)
                this.Find();

			string sRisco; 
			if(this.IdRisco.Id==(int)Riscos.AgentesQuimicos)
				sRisco = "Anexo 11 - ";
			else if(this.IdRisco.Id==(int)Riscos.ACGIH)
				sRisco = "ACGIH - ";
			else if(this.IdRisco.Id==(int)Riscos.AsbestosPoeirasMinerais)
				sRisco = "Anexo 12 - ";
			else if(this.IdRisco.Id==(int)Riscos.AgentesQuimicosAnexo13)
				sRisco = "Anexo 13 - ";
			else if(this.IdRisco.Id==(int)Riscos.AgentesBiologicos)
				sRisco = "Biológico - ";
			else 
				sRisco = string.Empty;

			if(this.DescricaoFrase!=string.Empty)
				return sRisco + this.Nome + " - " + this.DescricaoFrase;
			else
				return sRisco + this.Nome;
		}	
	
		public override void Delete()
		{
			DataSet ds = new PPRA().Get("nID_AG_NCV="+this.Id);

			if(ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
			{
				LaudoTecnico laudo = new LaudoTecnico(Convert.ToInt32(ds.Tables[0].Rows[0]["nID_LAUD_TEC"]));

				laudo.nID_EMPR.Find();

				Ghe ghe = new Ghe();
				ghe.Find(Convert.ToInt32(ds.Tables[0].Rows[0]["nID_FUNC"]));

				string strMessageError  = "Agente químico está sendo usado em "
					+ds.Tables[0].Rows.Count.ToString() + " laudos "
					+" (Laudo de "+
					laudo.hDT_LAUDO.ToString("dd-MM-yyyy") 
					+ " da empresa " + laudo.nID_EMPR.NomeAbreviado 
					+" no " + ghe.tNO_FUNC+")";

				throw new Exception(strMessageError);
			}

			base.Delete ();
		}

        public LimiteToleranciaAgenteQuimico LimiteToleranciaNR15(Unidade unidade)
        {
            LimiteToleranciaAgenteQuimico limite = new LimiteToleranciaAgenteQuimico();

            StringBuilder where = new StringBuilder();
            where.Append("IdAgenteQuimico=" + this.Id);
            where.Append(" AND IdRequisitoLegal=" + (int)RequisitosLegais.NR15);

            if (unidade.Id != 0)
                where.Append(" AND IdUnidade = " + unidade.Id);
            
            ArrayList list = limite.Find(where.ToString());

            if (unidade.Id == 0 && list.Count > 1)
                limite = (LimiteToleranciaAgenteQuimico)list[0];

            return limite;
        }

        public string GetNomeGrauInsalubridade()
        {
            string strGrauInsalubridade = string.Empty;

            switch (this.IndGrauInsalubridade)
            {
                case GrauInsalubridade.GrauMinimo:
                    strGrauInsalubridade = "Grau Mínimo";
                    break;
                case GrauInsalubridade.GrauMedio:
                    strGrauInsalubridade = "Grau Médio";
                    break;
                case GrauInsalubridade.GrauMaximo:
                    strGrauInsalubridade = "Grau Máximo";
                    break;
            }

            return strGrauInsalubridade;
        }
	}

	[Database("opsa", "LimiteToleranciaAgenteQuimico", "IdLimiteToleranciaAgenteQuimico")]
	public class LimiteToleranciaAgenteQuimico : Ilitera.Data.Table
	{
		private int _IdLimiteToleranciaAgenteQuimico;
		private AgenteQuimico _IdAgenteQuimico;
		private RequisitoLegal _IdRequisitoLegal;
		private Unidade _IdUnidade;
		private decimal _ValorLimiteTolTWA;		
		private decimal _ValorLimiteTolTWAStel;	
		private decimal _ValorLimiteTolTeto;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public LimiteToleranciaAgenteQuimico()
		{

		}		
		public override int Id
		{														  
			get	{return _IdLimiteToleranciaAgenteQuimico;}
			set {_IdLimiteToleranciaAgenteQuimico = value;}
		}	
		[Obrigatorio(true, "Agente Químico é campo obrigatório.")]
		public AgenteQuimico IdAgenteQuimico
		{														  
			get	{return _IdAgenteQuimico;}
			set {_IdAgenteQuimico = value;}
		}
		[Obrigatorio(true, "Requisito Legal é campo obrigatório.")]
		public RequisitoLegal IdRequisitoLegal
		{														  
			get	{return _IdRequisitoLegal;}
			set {_IdRequisitoLegal = value;}
		}		
		[Obrigatorio(true, "Unidade é campo obrigatório.")]
		public Unidade IdUnidade
		{														  
			get	{return _IdUnidade;}
			set {_IdUnidade = value;}
		}
		public decimal ValorLimiteTolTWA
		{														  
			get	{return _ValorLimiteTolTWA;}
			set {_ValorLimiteTolTWA = value;}
		}
		public decimal ValorLimiteTolTWAStel
		{														  
			get	{return _ValorLimiteTolTWAStel;}
			set {_ValorLimiteTolTWAStel = value;}
		}
		public decimal ValorLimiteTolTeto
		{														  
			get	{return _ValorLimiteTolTeto;}
			set {_ValorLimiteTolTeto = value;}
		}
		public override string ToString()
		{
            if (this.IdUnidade.mirrorOld == null)
                this.IdUnidade.Find();

            if (this.IdUnidade.Id == (int)Unidade.Unidades.AsfixianteSimples)
                return this.IdUnidade.Descricao;
            else
                return this.ValorLimiteTolTWA.ToString() + " " + this.IdUnidade.Descricao;
		}
	}	
}
