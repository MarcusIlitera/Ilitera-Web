using System;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{

	public enum RiscoTipo: int
	{
		Fisico,
		Quimico,
		Biologico,
		Acidentes,
		Ergonomico
	}

	public enum Riscos: int
	{
		RuidoContinuo,
		RuidoImpacto,
		Calor,
		RadiacoesIonizantes,
		CondicoesHiperbaricas,
		RadiacoesNaoIonizantes,
		Vibracoes,
		Frio,
		Umidade,
		AgentesQuimicos=9, 
		AgentesQuimicosB=10, 
		AgentesQuimicosC=11,
		AgentesQuimicosD=12,
		AsbestosPoeirasMinerais=13,
		AgentesQuimicosAnexo13=14,
		AgentesBiologicos=15,
		ACGIH=16,
		Ergonomicos=17,
		Acidentes=18,
        VibracoesVCI=106
	}


	[Database("opsa", "Risco", "IdRisco")]
	public class Risco: Ilitera.Data.Table
	{
		private int _IdRisco;
		private string _Descricao = string.Empty;
		private string _DescricaoResumido = string.Empty;
        private GrauInsalubridade _IndGrauInsalubridade;
		private string _FraseSem = string.Empty;
		private string _FraseCom = string.Empty;
		private bool _Qualitativo;
		private RiscoTipo _IndRiscoTipo;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Risco()
		{

		}
		public override int Id
		{														  
			get	{return _IdRisco;}
			set {_IdRisco = value;}
		}
		public string Descricao
		{														  
			get	{return _Descricao;}
			set {_Descricao = value;}
		}
		public string DescricaoResumido
		{														  
			get	
			{
				if(this.Id==0)
					return "Ruído Contínuo ou Intermitente";
				else
					return _DescricaoResumido;
			}
			set {_DescricaoResumido = value;}
		}
        public GrauInsalubridade IndGrauInsalubridade
        {
            get { return _IndGrauInsalubridade; }
            set { _IndGrauInsalubridade = value; }
        }
		public string FraseSem
		{														  
			get	{return _FraseSem;}
			set {_FraseSem = value;}
		}	
		public string FraseCom
		{														  
			get	{return _FraseCom;}
			set {_FraseCom = value;}
		}	
		public bool Qualitativo
		{
			get	{return _Qualitativo;}
			set {_Qualitativo = value;}
		}
		public RiscoTipo IndRiscoTipo
		{
			get	{return _IndRiscoTipo;}
			set {_IndRiscoTipo = value;}
		}

		public string GetRiscoTipo()
		{
			string ret;

			if(this.IndRiscoTipo.Equals(RiscoTipo.Fisico))
				ret = "Físico";
            else if (this.IndRiscoTipo.Equals(RiscoTipo.Quimico))
				ret = "Químico";
            else if (this.IndRiscoTipo.Equals(RiscoTipo.Biologico))
				ret = "Biológico";
            else if (this.IndRiscoTipo.Equals(RiscoTipo.Acidentes))
				ret = "Acidentes";
            else if (this.IndRiscoTipo.Equals(RiscoTipo.Ergonomico))
				ret = "Ergonômicos";
			else
				ret = string.Empty;

			return ret;
		}

        public bool IsRuido()
        {
            if (this.mirrorOld == null)
                this.Find();

            return (this.Id == (int)Riscos.RuidoContinuo
                || this.Id == (int)Riscos.RuidoImpacto);
        }

        public bool IsCalor()
        {
            if (this.mirrorOld == null)
                this.Find();

            return (this.Id == (int)Riscos.Calor);
        }

        public bool IsQualitativo()
        {
            if (this.mirrorOld == null)
                this.Find();

            return (this.Id == (int)Riscos.RadiacoesNaoIonizantes
                || this.Id == (int)Riscos.Frio
                || this.Id == (int)Riscos.Umidade
                || this.Id == (int)Riscos.AgentesQuimicosAnexo13
                || this.Id == (int)Riscos.AgentesBiologicos
                || this.Id == (int)Riscos.Ergonomicos
                || this.Id == (int)Riscos.Acidentes);
        }

        public bool IsAgenteQuimico()
        {
            if (this.mirrorOld == null)
                this.Find();

            return (this.Id == (int)Riscos.AgentesQuimicos
                || this.Id == (int)Riscos.AgentesQuimicosB
                || this.Id == (int)Riscos.AgentesQuimicosC
                || this.Id == (int)Riscos.AgentesQuimicosD
                || this.Id == (int)Riscos.AsbestosPoeirasMinerais
                || this.Id == (int)Riscos.ACGIH);
        }

        public bool IsDemaisAgentes()
        {
            if (this.mirrorOld == null)
                this.Find();

            return (this.Id == (int)Riscos.RadiacoesIonizantes
                || this.Id == (int)Riscos.CondicoesHiperbaricas
                || this.Id == (int)Riscos.RadiacoesNaoIonizantes
                || this.Id == (int)Riscos.Vibracoes
                || this.Id == (int)Riscos.Frio
                || this.Id == (int)Riscos.Umidade
                || this.Id == (int)Riscos.AgentesQuimicosAnexo13
                || this.Id == (int)Riscos.AgentesBiologicos);
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return this.DescricaoResumido;
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
}
