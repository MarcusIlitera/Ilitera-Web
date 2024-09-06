using System;
using System.Collections;
using System.Text;
using Ilitera.Data;
using Ilitera.Common;
using System.Data;

namespace Ilitera.Opsa.Data
{

	public enum IndTipoExame : int
	{
		Indefinido,
        Clinico,
		Complementar,
		Audiometrico,
		NaoOcupacional
	} 

	public enum IndTipoSexo : int
	{
		Nenhum,
		Ambos, 
		Masculino, 
		Feminino
	}
	
	public enum Exames : int
	{
		Audiometria = 6,
		NaoOcupacional = 7,
		Dermal = 1826968046,
		Oftalmologico = -1476525536,
		HemogramaCompleto = -817180620,
		HemogramaCompletoContPaquetas = -1309105778,
		ProvaFuncaoHepatica=-978160146,
		ProvaFuncaoRenal = 507801760
	}

	[Database("opsa","ExameDicionario","IdExameDicionario")]
	public class ExameDicionario: Ilitera.Data.Table, IPeriodicidadeExame
	{
		private int _IdExameDicionario;
		private string _Nome=string.Empty;
		private string _Descricao=string.Empty;
		private int _IndExame;
		private int _IndSaude;
		private int _IndSexo;
		private bool _NaAdmissao;
		private bool _NaDemissao;
		private bool _NoRetornoTrabalho;
		private bool _NaMudancaFuncao;
		private bool _AposAdmissao;
		private int _IndPeriodicidadeAposAdmissao;
		private int _IntervaloAposAdmissao;
		private bool _Periodico;
		private int _IndPeriodicidade;
		private int _Intervalo;
		private bool _DependeIdade;
		private bool _DependeAgente;
		private Risco _IdRisco;
		private bool _LimitePrimeiroExame;
		private bool _IsObservacao;
		//novos
		private RequisitoLegal _IdRequisitoLegal;
		private string _Agente=string.Empty;
		private string _MaterialBiologico=string.Empty;
		private string _IndicadorBiologico=string.Empty;
		private string _VR=string.Empty;
		private string _IBMP=string.Empty;
		private string _Metodo=string.Empty;
		private string _Amostragem=string.Empty;
		private string _AnaliseMedicaConsideracoes=string.Empty;
		private string _AnaliseMadicaDescricao=string.Empty;
		private float _Preco;
		private bool _IsToxicologico;
        private string _Codigo_eSocial = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ExameDicionario()
		{

		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ExameDicionario(string NomeExameTipo)
		{		
			this.Nome = NomeExameTipo;					
		}
		public override int Id
		{
			get{return _IdExameDicionario;}
			set{_IdExameDicionario = value;}
		}
		[Obrigatorio(true, "O Nome é obrigatório!")]
		public string Nome
		{
			get{return _Nome;}
			set{_Nome = value;}
		}
		[Obrigatorio(true, "A Descrição é obrigatório!")]
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
		[Obrigatorio(true, "O Tipo do Exame é obrigatório!")]
		public int IndExame
		{
			get{return _IndExame;}
			set{_IndExame = value;}
		}
		[Obrigatorio(true, "O Tipo de Saúde é obrigatório!")]
		public int IndSaude
		{
			get{return _IndSaude;}
			set{_IndSaude = value;}
		}
		[Obrigatorio(true, "O Sexo é obrigatório!")]
		public int IndSexo
		{
			get{return _IndSexo;}
			set{_IndSexo = value;}
		}
		public bool NaAdmissao
		{
			get{return _NaAdmissao;}
			set{_NaAdmissao = value;}
		}
		public bool NaDemissao
		{
			get{return _NaDemissao;}
			set{_NaDemissao = value;}
		}
		public bool NoRetornoTrabalho
		{
			get{return _NoRetornoTrabalho;}
			set{_NoRetornoTrabalho = value;}
		}
		public bool NaMudancaFuncao
		{
			get{return _NaMudancaFuncao;}
			set{_NaMudancaFuncao = value;}
		}
		public bool AposAdmissao
		{
			get{return _AposAdmissao;}
			set{_AposAdmissao = value;}
		}
		public int IndPeriodicidadeAposAdmissao
		{
			get{return _IndPeriodicidadeAposAdmissao;}
			set{_IndPeriodicidadeAposAdmissao = value;}
		}
		public int IntervaloAposAdmissao
		{
			get{return _IntervaloAposAdmissao;}
			set{_IntervaloAposAdmissao = value;}
		}
		public bool Periodico
		{
			get{return _Periodico;}
			set{_Periodico = value;}
		}
		public int IndPeriodicidade
		{
			get{return _IndPeriodicidade;}
			set{_IndPeriodicidade = value;}
		}
		public int Intervalo
		{
			get{return _Intervalo;}
			set{_Intervalo = value;}
		}
		public bool DependeIdade
		{
			get{return _DependeIdade;}
			set{_DependeIdade = value;}
		}
		public bool DependeAgente
		{
			get{return _DependeAgente;}
			set{_DependeAgente = value;}
		}
		public Risco IdRisco
		{
			get{return _IdRisco;}
			set{_IdRisco = value;}
		}
		public bool LimitePrimeiroExame
		{
			get{return _LimitePrimeiroExame;}
			set{_LimitePrimeiroExame = value;}
		}
		public bool IsObservacao
		{
			get{return _IsObservacao;}
			set{_IsObservacao = value;}
		}
		public RequisitoLegal IdRequisitoLegal
		{
			get{return _IdRequisitoLegal;}
			set{_IdRequisitoLegal = value;}
		}
		public string Agente
		{
			get{return _Agente;}
			set{_Agente = value;}
		}
		public string MaterialBiologico
		{
			get{return _MaterialBiologico;}
			set{_MaterialBiologico = value;}
		}
		public string IndicadorBiologico
		{
			get{return _IndicadorBiologico;}
			set{_IndicadorBiologico = value;}
		}
		public string VR
		{
			get{return _VR;}
			set{_VR = value;}
		}
		public string IBMP
		{
			get{return _IBMP;}
			set{_IBMP = value;}
		}
		public string Metodo
		{
			get{return _Metodo;}
			set{_Metodo = value;}
		}
		public string Amostragem
		{
			get{return _Amostragem;}
			set{_Amostragem = value;}
		}
		public string AnaliseMedicaConsideracoes
		{
			get{return _AnaliseMedicaConsideracoes;}
			set{_AnaliseMedicaConsideracoes = value;}
		}
		public string AnaliseMadicaDescricao
		{
			get{return _AnaliseMadicaDescricao;}
			set{_AnaliseMadicaDescricao = value;}
		}
		public float Preco
		{
			get{return _Preco;}
			set{_Preco = value;}
		}		
		public bool IsToxicologico
		{
			get{return _IsToxicologico;}
			set{_IsToxicologico = value;}
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

            return this.Nome;
        }

		public string GetRiscos()
		{
			StringBuilder riscos = new StringBuilder();
			
            ArrayList listRiscos = new Risco().Find("IdRisco IN (Select IdRisco FROM ExameDicionario Where IdExameDicionario = "+this.Id+") ORDER BY DescricaoResumido");
			
            foreach(Risco risco in listRiscos)
				riscos.Append(risco.DescricaoResumido + "\n");

            return riscos.ToString();			
		}

		public static string GeTipoExame(int IndExame)
		{
			string tipoExame = "";

			if(IndExame == 1)
				tipoExame = "Clinico";
			else if(IndExame == 2)
				tipoExame = "Complementar";
			else if(IndExame == 3)
				tipoExame = "Audiométrico";

			return tipoExame;
		}

        public static bool IsClinico(int IdExameDicionario)
        {
            return (IdExameDicionario == (int)IndExameClinico.Admissional
                    || IdExameDicionario == (int)IndExameClinico.Demissional
                    || IdExameDicionario == (int)IndExameClinico.MudancaDeFuncao
                    || IdExameDicionario == (int)IndExameClinico.Periodico
                    || IdExameDicionario == (int)IndExameClinico.RetornoAoTrabalho);
        }

		public static string GetAreaMedica(int IndSaude)
		{
			string saude = "";

			if(IndSaude == 1)
				saude = "Ocupacional";
			else if(IndSaude == 2)
				saude = "Assistencial";

			return saude;
		}

		public static string GetToxicologia(bool IsToxicologia)
		{
			string toxicologia = "";
			if(IsToxicologia == true)
				toxicologia = "Sim";
			else
				toxicologia = "Não";
			return toxicologia;
		}

        public static string GetAplicavelSexo(int IndSexo)
        {
            string sexo = "";

            if (IndSexo == (int)IndTipoSexo.Ambos)
                sexo = "Ambos";
            else if (IndSexo == (int)IndTipoSexo.Feminino)
                sexo = "Feminino";
            else if (IndSexo == (int)IndTipoSexo.Masculino)
                sexo = "Masculino";
            else if (IndSexo == (int)IndTipoSexo.Nenhum)
                sexo = "Nenhum";

            return sexo;
        }

        public static string GetPeriodicidadeExame(IPeriodicidadeExame iPeriodicidadeExame)
        {
            StringBuilder str = new StringBuilder();

            if (iPeriodicidadeExame.NaAdmissao)
                str.Append("Na Admissão, ");
            
            if (iPeriodicidadeExame.AposAdmissao)
                str.Append("Após " + GetIndPeriodicidade(iPeriodicidadeExame.IntervaloAposAdmissao, iPeriodicidadeExame.IndPeriodicidadeAposAdmissao).Replace("Mensal", "mês") + " da Admissão, ");
            
            if (iPeriodicidadeExame.Periodico)
            {
                if (iPeriodicidadeExame.Intervalo == 1)
                    str.Append(GetIndPeriodicidade(iPeriodicidadeExame.Intervalo, iPeriodicidadeExame.IndPeriodicidade) + ", ");
                else
                    str.Append("Periódico (" + GetIndPeriodicidade(iPeriodicidadeExame.Intervalo, iPeriodicidadeExame.IndPeriodicidade) + "), ");
            }
            if (iPeriodicidadeExame.NoRetornoTrabalho)
                str.Append("No Retorno ao Trabalho, ");

            if (iPeriodicidadeExame.NaMudancaFuncao)
                str.Append("Na Mudança de Função, ");

            if (iPeriodicidadeExame.NaDemissao)
                str.Append("Na Demissão, ");

            if (str.Length > 0)
                str.Remove(str.Length - 2, 2);

            return str.ToString();
        }

        public static string GetIndPeriodicidade(int IndIntervalo, int IndPeriodicidade)
        {
            string ret;

            if (IndPeriodicidade == (int)Periodicidade.Dia)
            {
                if (IndIntervalo == 1)
                    ret = "Diário";
                else
                    ret = IndIntervalo.ToString() + " dias";
            }
            else if (IndPeriodicidade == (int)Periodicidade.Semana)
            {
                if (IndIntervalo == 1)
                    ret = "Semanal";
                else
                    ret = IndIntervalo.ToString() + " semanas";
            }
            else if (IndPeriodicidade == (int)Periodicidade.Mes)
            {
                if (IndIntervalo == 1)
                    ret = "Mensal";
                else
                    ret = IndIntervalo.ToString() + " meses";
            }
            else if (IndPeriodicidade == (int)Periodicidade.Ano)
            {
                if (IndIntervalo == 1)
                    ret = "Anual";
                else
                    ret = IndIntervalo.ToString() + " anos";
            }
            else if (IndPeriodicidade == (int)Periodicidade.Semestral)
            {
                if (IndIntervalo == 1)
                    ret = "Semestral";
                else
                    ret = IndIntervalo.ToString() + " semestre";
            }
            else
                ret = string.Empty;

            return ret;
        }

        public DataSet ListaExameComplementar(AgenteQuimico agenteQuimico)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Id", Type.GetType("System.String"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            ds.Tables.Add(table);
            DataRow newRow;
            ArrayList list = this.Find("IdExameDicionario IN (Select IdExameDicionario FROM ExameDicionarioAgenteQuimico Where IdAgenteQuimico = " + agenteQuimico.Id + ") AND  IndExame= " + (int)IndTipoExame.Complementar);
            foreach (ExameDicionario exameDicionario in list)
            {
                exameDicionario.Find();
                newRow = ds.Tables[0].NewRow();
                newRow["Id"] = exameDicionario.Id;
                newRow["Tipo"] = exameDicionario.Nome;
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
	}
}
