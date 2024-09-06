using System;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	/// <summary>
	/// Summary description for AnamneseAudiologica.
	/// </summary>
	[Database("opsa", "AnamneseAudiologica", "IdAnamneseAudiologica")]
	public class AnamneseAudiologica: Table
	{
		private int _IdAnamneseAudiologica;
		private Audiometria _IdAudiometria;
		private int _HasSenteDificuldadeAuditiva;
		private int _HasTemPresencaAcufenos;
		private int _HasTemPresencaVertigens;
		private int _HasFamiliaresComProblemaAuditivo;
		private bool _IsAntecedenteCaxumba;
		private bool _IsAntecedenteSarampo;
		private bool _IsAntecedenteMenigite;
		private int _HasFazUsoMedicacao;
		private string _FazUsoMedicacaoQual=string.Empty;
		private string _TempoExposicaoRuidoOcupacional=string.Empty;
		private string _TempoUsoProtetorAuricular=string.Empty;
		private int _HasExposicaoRuidoExtraLaboral;
		private int _HasExposicaoProdutosOtotoxicos;
		private string _ExposicaoProdutosOtotoxicosQual=string.Empty;
		private int _HasAlteracaoMeatosAcusticos;
		private string _AlteracaoMeatosAcusticosQual = string.Empty;
		private string _Observacao=string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AnamneseAudiologica()
		{

		}
		public override int Id
		{
			get{return _IdAnamneseAudiologica;}
			set{_IdAnamneseAudiologica = value;}
		}
		public Audiometria IdAudiometria
		{
			get{return _IdAudiometria;}
			set{_IdAudiometria = value;}
		}
		public int HasSenteDificuldadeAuditiva
		{
			get{return _HasSenteDificuldadeAuditiva;}
			set{_HasSenteDificuldadeAuditiva = value;}
		}
		public int HasTemPresencaAcufenos
		{
			get{return _HasTemPresencaAcufenos;}
			set{_HasTemPresencaAcufenos = value;}
		}
		public int HasTemPresencaVertigens
		{
			get{return _HasTemPresencaVertigens;}
			set{_HasTemPresencaVertigens = value;}
		}
		public int HasFamiliaresComProblemaAuditivo
		{
			get{return _HasFamiliaresComProblemaAuditivo;}
			set{_HasFamiliaresComProblemaAuditivo = value;}
		}
		public bool IsAntecedenteCaxumba
		{
			get{return _IsAntecedenteCaxumba;}
			set{_IsAntecedenteCaxumba = value;}
		}
		public bool IsAntecedenteSarampo
		{
			get{return _IsAntecedenteSarampo;}
			set{_IsAntecedenteSarampo = value;}
		}
		public bool IsAntecedenteMenigite
		{
			get{return _IsAntecedenteMenigite;}
			set{_IsAntecedenteMenigite = value;}
		}
		public int HasFazUsoMedicacao
		{
			get{return _HasFazUsoMedicacao;}
			set{_HasFazUsoMedicacao = value;}
		}
		public string FazUsoMedicacaoQual
		{
			get{return _FazUsoMedicacaoQual;}
			set{_FazUsoMedicacaoQual = value;}
		}
		public string TempoExposicaoRuidoOcupacional
		{
			get{return _TempoExposicaoRuidoOcupacional;}
			set{_TempoExposicaoRuidoOcupacional = value;}
		}
		public string TempoUsoProtetorAuricular
		{
			get{return _TempoUsoProtetorAuricular;}
			set{_TempoUsoProtetorAuricular = value;}
		}
		public int HasExposicaoRuidoExtraLaboral
		{
			get{return _HasExposicaoRuidoExtraLaboral;}
			set{_HasExposicaoRuidoExtraLaboral = value;}
		}
		public int HasExposicaoProdutosOtotoxicos
		{
			get{return _HasExposicaoProdutosOtotoxicos;}
			set{_HasExposicaoProdutosOtotoxicos = value;}
		}
		public string ExposicaoProdutosOtotoxicosQual
		{
			get{return _ExposicaoProdutosOtotoxicosQual;}
			set{_ExposicaoProdutosOtotoxicosQual = value;}
		}
		public int HasAlteracaoMeatosAcusticos
		{
			get{return _HasAlteracaoMeatosAcusticos;}
			set{_HasAlteracaoMeatosAcusticos = value;}
		}
		public string AlteracaoMeatosAcusticosQual
		{
			get{return _AlteracaoMeatosAcusticosQual;}
			set{_AlteracaoMeatosAcusticosQual = value;}
		}

		public string Observacao
		{
			get{return _Observacao;}
			set{_Observacao = value;}
		}

		public static AnamneseAudiologica GetAnamneseAudiologica(Audiometria audiometria)
		{
			AnamneseAudiologica anamnese = new AnamneseAudiologica();
			anamnese.Find("IdAudiometria="+audiometria.Id);

			if(anamnese.Id==0)
			{
				anamnese.Inicialize();
				anamnese.IdAudiometria = audiometria;
			}

			return anamnese;
		}

		public static string GetRespostaAnamnese(int status)
		{
			string ret = string.Empty;

			if((int)StatusAnamnese.Sim==status)
				ret = "Sim";
			else if((int)StatusAnamnese.Nao==status)
				ret = "Não";

			return ret;
		}
	}
}
