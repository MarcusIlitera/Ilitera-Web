using System;
using System.IO;
using System.Data;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	public enum Orelha : int
	{
		Direita,
		Esquerda
	}

	[Database("opsa", "AudiometriaAudiograma", "IdAudiometriaAudiograma")]
	public class AudiometriaAudiograma: Ilitera.Data.Table
	{

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AudiometriaAudiograma()
		{

		}
		private int _IdAudiometriaAudiograma;
		private Audiometria _IdAudiometria;
		private int _IndOrelha;
		private Diagnostico _IdDiagnostico;
		private string _ObsDiagnostico = string.Empty;
		private bool _IsMeatoscopiaAlterada;
		private string _ObsMeatoscopia= string.Empty;
		private bool _IsReferencial;
		private bool _IsAnormal;
		private bool _IsAgravamento;
		private bool _IsOcupacional;
		private AudiometriaAudiograma _IdAudiogramaReferencial;
		private string _Aereo250 = "NE";
		private string _Aereo500 = string.Empty;
		private string _Aereo1000 = string.Empty;
		private string _Aereo2000 = string.Empty;
		private string _Aereo3000 = string.Empty;
		private string _Aereo4000 = string.Empty;
		private string _Aereo6000 = string.Empty;
		private string _Aereo8000 = string.Empty;
		private bool _IsAereoMascarado250;
		private bool _IsAereoMascarado500;
		private bool _IsAereoMascarado1000;
		private bool _IsAereoMascarado2000;
		private bool _IsAereoMascarado3000;
		private bool _IsAereoMascarado4000;
		private bool _IsAereoMascarado6000;
		private bool _IsAereoMascarado8000;
		private string _Osseo500  = "NE";
		private string _Osseo1000 = "NE";
		private string _Osseo2000 = "NE";
		private string _Osseo3000 = "NE";
		private string _Osseo4000 = "NE";
		private string _Osseo6000 = "NE";
		private bool _IsOsseoMascarado500;
		private bool _IsOsseoMascarado1000;
		private bool _IsOsseoMascarado2000;
		private bool _IsOsseoMascarado3000;
		private bool _IsOsseoMascarado4000;
		private bool _IsOsseoMascarado6000;
        private string _Tipo_Perda;
        private string _Grau_Perda;
        private string _Rebaixamento;

		public override int Id
		{
			get{return _IdAudiometriaAudiograma;}
			set{_IdAudiometriaAudiograma = value;}
		}
		public Audiometria IdAudiometria
		{														  
			get	{return _IdAudiometria;}
			set {_IdAudiometria = value;}
		}
		public int IndOrelha
		{														  
			get	{return _IndOrelha;}
			set {_IndOrelha = value;}
		}
		public Diagnostico IdDiagnostico
		{														  
			get	{return _IdDiagnostico;}
			set {_IdDiagnostico = value;}
		}
		public string ObsDiagnostico
		{														  
			get	{return _ObsDiagnostico;}
			set {_ObsDiagnostico = value;}
		}
		public bool IsMeatoscopiaAlterada
		{														  
			get	{return _IsMeatoscopiaAlterada;}
			set {_IsMeatoscopiaAlterada = value;}
		}
		public string ObsMeatoscopia
		{														  
			get	{return _ObsMeatoscopia;}
			set {_ObsMeatoscopia = value;}
		}
		public bool IsReferencial
		{														  
			get	{return _IsReferencial;}
			set {_IsReferencial = value;}
		}
		public bool IsAnormal
		{														  
			get	{return _IsAnormal;}
			set {_IsAnormal = value;}
		}
		public bool IsAgravamento
		{														  
			get	{return _IsAgravamento;}
			set {_IsAgravamento = value;}
		}
		public bool IsOcupacional
		{														  
			get	{return _IsOcupacional;}
			set {_IsOcupacional = value;}
		}
		public AudiometriaAudiograma IdAudiogramaReferencial
		{														  
			get	{return _IdAudiogramaReferencial;}
			set {_IdAudiogramaReferencial = value;}
		}
		public string Aereo250
		{														  
			get	{return _Aereo250;}
			set {_Aereo250 = value;}
		}
		public string Aereo500
		{														  
			get	{return _Aereo500;}
			set {_Aereo500 = value;}
		}
		public string Aereo1000
		{														  
			get	{return _Aereo1000;}
			set {_Aereo1000 = value;}
		}
		public string Aereo2000
		{														  
			get	{return _Aereo2000;}
			set {_Aereo2000 = value;}
		}
		public string Aereo3000
		{														  
			get	{return _Aereo3000;}
			set {_Aereo3000 = value;}
		}
		public string Aereo4000
		{														  
			get	{return _Aereo4000;}
			set {_Aereo4000 = value;}
		}
		public string Aereo6000
		{														  
			get	{return _Aereo6000;}
			set {_Aereo6000 = value;}
		}
		public string Aereo8000
		{														  
			get	{return _Aereo8000;}
			set {_Aereo8000 = value;}
		}
		public bool IsAereoMascarado250
		{														  
			get	{return _IsAereoMascarado250;}
			set {_IsAereoMascarado250 = value;}
		}
		public bool IsAereoMascarado500
		{														  
			get	{return _IsAereoMascarado500;}
			set {_IsAereoMascarado500 = value;}
		}
		public bool IsAereoMascarado1000
		{														  
			get	{return _IsAereoMascarado1000;}
			set {_IsAereoMascarado1000 = value;}
		}
		public bool IsAereoMascarado2000
		{														  
			get	{return _IsAereoMascarado2000;}
			set {_IsAereoMascarado2000 = value;}
		}
		public bool IsAereoMascarado3000
		{														  
			get	{return _IsAereoMascarado3000;}
			set {_IsAereoMascarado3000 = value;}
		}
		public bool IsAereoMascarado4000
		{														  
			get	{return _IsAereoMascarado4000;}
			set {_IsAereoMascarado4000 = value;}
		}
		public bool IsAereoMascarado6000
		{														  
			get	{return _IsAereoMascarado6000;}
			set {_IsAereoMascarado6000 = value;}
		}
		public bool IsAereoMascarado8000
		{														  
			get	{return _IsAereoMascarado8000;}
			set {_IsAereoMascarado8000 = value;}
		}
		public string Osseo500
		{														  
			get	{return _Osseo500;}
			set {_Osseo500 = value;}
		}
		public string Osseo1000
		{														  
			get	{return _Osseo1000;}
			set {_Osseo1000 = value;}
		}
		public string Osseo2000
		{														  
			get	{return _Osseo2000;}
			set {_Osseo2000 = value;}
		}
		public string Osseo3000
		{														  
			get	{return _Osseo3000;}
			set {_Osseo3000 = value;}
		}
		public string Osseo4000
		{														  
			get	{return _Osseo4000;}
			set {_Osseo4000 = value;}
		}
		public string Osseo6000
		{														  
			get	{return _Osseo6000;}
			set {_Osseo6000 = value;}
		}
		public bool IsOsseoMascarado500
		{														  
			get	{return _IsOsseoMascarado500;}
			set {_IsOsseoMascarado500 = value;}
		}
		public bool IsOsseoMascarado1000
		{														  
			get	{return _IsOsseoMascarado1000;}
			set {_IsOsseoMascarado1000 = value;}
		}
		public bool IsOsseoMascarado2000
		{														  
			get	{return _IsOsseoMascarado2000;}
			set {_IsOsseoMascarado2000 = value;}
		}
		public bool IsOsseoMascarado3000
		{														  
			get	{return _IsOsseoMascarado3000;}
			set {_IsOsseoMascarado3000 = value;}
		}
		public bool IsOsseoMascarado4000
		{														  
			get	{return _IsOsseoMascarado4000;}
			set {_IsOsseoMascarado4000 = value;}
		}
		public bool IsOsseoMascarado6000
		{														  
			get	{return _IsOsseoMascarado6000;}
			set {_IsOsseoMascarado6000 = value;}
		}
		public bool IsAudiogramaEmBranco()
		{
			return IsAudiogramaAereoEmBranco()
				&& IsAudiogramaOsseoEmBranco();
		}

        public string Grau_Perda
        {
            get { return _Grau_Perda; }
            set { _Grau_Perda = value; }
        }

        public string Tipo_Perda
        {
            get { return _Tipo_Perda; }
            set { _Tipo_Perda = value; }
        }

        public string Rebaixamento
        {
            get { return _Rebaixamento; }
            set { _Rebaixamento = value; }
        }


		#region ValidarEntradas

		public bool IsAudiogramaAereoEmBranco()
		{
			return (this.Aereo250  == "NE" 
				 && this.Aereo500  == string.Empty 
				 && this.Aereo1000 == string.Empty 
				 && this.Aereo2000 == string.Empty 
				 && this.Aereo3000 == string.Empty 
				 && this.Aereo4000 == string.Empty 
				 && this.Aereo6000 == string.Empty 
				 && this.Aereo8000 == string.Empty);
		}

		public bool IsAudiogramaOsseoEmBranco()
		{
			return (this.Osseo500=="NE"
				&& this.Osseo1000=="NE"
				&& this.Osseo2000=="NE" 
				&& this.Osseo3000=="NE"
				&& this.Osseo4000=="NE"
				&& this.Osseo6000=="NE");
		}

		public bool IsAudiogramaCamposObrigatorios()
		{
			bool bVal = 
			 
				(!this.IsAudiogramaAereoEmBranco()
				&& (this.Aereo250==string.Empty 
				|| this.Aereo500==string.Empty 
				|| this.Aereo1000==string.Empty 
				|| this.Aereo2000==string.Empty 
				|| this.Aereo3000==string.Empty 
				|| this.Aereo4000==string.Empty 
				|| this.Aereo6000==string.Empty 
				|| this.Aereo8000==string.Empty)

				|| 

				   (!this.IsAudiogramaOsseoEmBranco()
				&& (this.Osseo500==string.Empty 
				|| this.Osseo1000==string.Empty 
				|| this.Osseo2000==string.Empty 
				|| this.Osseo3000==string.Empty 
				|| this.Osseo4000==string.Empty 
				|| this.Osseo6000==string.Empty)));

			return bVal;
		}

		public bool IsValidarEntradas()
		{
			bool bVal =	   IsAereo(this.Aereo250)
						&& IsAereo(this.Aereo500)
						&& IsAereo(this.Aereo1000)
						&& IsAereo(this.Aereo2000)
						&& IsAereo(this.Aereo3000)
						&& IsAereo(this.Aereo4000)
						&& IsAereo(this.Aereo6000)
						&& IsAereo(this.Aereo8000)
						&& IsOsseo(this.Osseo500)
						&& IsOsseo(this.Osseo1000)
						&& IsOsseo(this.Osseo2000)
						&& IsOsseo(this.Osseo3000)
						&& IsOsseo(this.Osseo4000)
						&& IsOsseo(this.Osseo6000);

			return bVal;
		}

		private static bool IsAereo(string sVal)
		{
			bool ret= false;

			if(sVal==string.Empty || sVal=="AR" || sVal=="NE")
				ret = true;
			else
			{
				float val;

				try
				{
					val = float.Parse(sVal);

					int resto = Convert.ToInt32(val) % 5;

					if((val >= -10F && val <= 120F) && (resto == 0))
						ret =  true;					
				}
				catch
				{
					ret = false;
				}
			}
			return ret;
		}

		private static bool IsOsseo(string sVal)
		{
			bool ret= false;

			if(sVal==string.Empty || sVal=="AR" || sVal=="NE")
				ret = true;
			else
			{
				float val;

				try
				{
					val = float.Parse(sVal);

					int resto = Convert.ToInt32(val) % 5;

					if((val >= 0F && val <= 80F) && (resto == 0))
						ret =  true;					
				}
				catch
				{
					ret = false;
				}
			}
			return ret;
		}

		#endregion

		#region Metodos
		
		public override int Save()
		{
			return this.Save(true);
		}

		public int Save(bool useValidation)
		{
            //if (useValidation)
            //{
            //    if (this.IsReferencial && this.IdAudiogramaReferencial.Id != 0)
            //        throw new Exception("O Exame Audiométrico Referencial a ser cadastrado não pode ter um Exame Referencial relacionado a ele!");

            //    if (!this.IsReferencial && this.IdAudiogramaReferencial.Id.Equals(0))
            //        throw new Exception("O Exame Audiométrico Sequencial a ser cadastrado deve ter um Exame Referencial relacionado a ele!");
            //}
			
			return base.Save();
		}

		public string GetArquivo (Cliente cliente)
		{
            string ret = string.Empty;

			ret = Fotos.PathFoto_Uri( Path.Combine(Audiometria.GetDiretorioPadrao(cliente), this.Id.ToString() + ".png") );

            if (ret.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 0)
            {
                if (ret.ToUpper().IndexOf("DRIVEI") > 0)
                {
                    ret = ret.Substring(0, ret.IndexOf("driveI") - 1) + "/driveI/FotosDocsDigitais" + ret.Substring(ret.IndexOf("driveI") + 6);
                }
                else
                {
                    ret = ret.Substring(0, 3) + "\\FotosDocsDigitais" + ret.Substring(3);
                }
                
                ret = ret.Replace("http://www.ilitera.net.br/driveI", "I:");
                ret = ret.Replace("/", "\\");

            }

            return ret;
		}

		public void AtualizarDadosPPP()
		{
			if(this.IsReferencial)
			{
				this.IsAnormal = (InterpretacaoReferencial() != (int)Interpretacao.Aceitavel);
				this.IsAgravamento = false;
			}
			else
			{
				int interpretacaoSeqDir = InterpretacaoSequencial();
				
				this.IsAnormal = (interpretacaoSeqDir != (int)Interpretacao.Aceitavel);
				this.IsAgravamento = (interpretacaoSeqDir == (int)Interpretacao.Agravamento);
			}
		}

		public AudiometriaAudiograma GetAudiogramaReferencial()
		{
			if(this.IdAudiometria.Id==0)
				return this.IdAudiogramaReferencial;

			if(this.IdAudiometria.mirrorOld==null)
				this.IdAudiometria.Find();
			
			return GetAudiogramaReferencial(this.IdAudiometria.IdEmpregado.Id);
		}

		public AudiometriaAudiograma GetAudiogramaReferencial(int IdEmpregado)
		{
			if(this.IdAudiogramaReferencial==null || this.IdAudiogramaReferencial.Id==0 )
			{
				this.IdAudiogramaReferencial = new AudiometriaAudiograma();
				this.IdAudiogramaReferencial.Inicialize();

				string find = "IdEmpregado="+IdEmpregado
					+" AND IdAudiometria IN (SELECT IdAudiometria FROM AudiometriaAudiograma WHERE IsReferencial=1 AND IndOrelha="+this.IndOrelha+")"
					+" ORDER BY DataExame DESC";
				
				ArrayList list = new Audiometria().Find(find);
				
				if(list.Count>0)
				{
					if(this.IndOrelha==(int)Orelha.Direita)
						this.IdAudiogramaReferencial = ((Audiometria)list[0]).GetAudiograma(Orelha.Direita);
					else
						this.IdAudiogramaReferencial = ((Audiometria)list[0]).GetAudiograma(Orelha.Esquerda);
				}
				else
					this.IsReferencial = true;
			}

			return this.IdAudiogramaReferencial;
		}

		public string GetResultadoExame()
		{
			string ret;
			
			if(this.IsAnormal && this.IsAgravamento)
				ret = "Anormal com Agravamento";
			if(this.IsAnormal && !this.IsAgravamento)
				ret = "Anormal Estável";    
			else
				ret = "Normal";

			return ret;
		}

		#endregion

		#region Interpretacao

		public string InterpretacaoPortaria19()
		{
			int ret = 0;
			
			string[] str = new string[6]{	"Sem Interpretação",
											"Limiares aceitáveis",
											"Sugestivo de PAI-NPSE",
											"Não-Sugestivo de PAI-NPSE",
											"Desencadeamento de PAI-NPSE",
											"Agravamento de PAI-NPSE"};
			
			if (this.IsReferencial)
				ret = (int)InterpretacaoReferencial();
			else
				ret = (int)InterpretacaoSequencial();


			return str[ret];
		}

		#region Interpretacao Sequencial

		public static bool IsAusenciaResposta(string sVal)
		{
			if(sVal=="AR")
				return true;
			else
				return false;
		}

        public static float GetVal(string sVal, bool isOsseo)
        {
            if (sVal == "AR" && !isOsseo)//Ausência de Resposta para Aereo
                return 120F;
            else if (sVal == "AR" && isOsseo)//Ausência de Resposta para Osseo
                return 70F;
            else if (sVal == string.Empty)
                return 0F;
            else if (sVal == "NE")//Não Informado
                return 0F;
            else
                return float.Parse(sVal);
        }

        private int InterpretacaoSequencial()
        {
            float mediaAereo512Atual;
            float mediaAereo512Ref;
            float mediaAereo346Atual;
            float mediaAereo346Ref;
            float mediaOsseo512Atual;
            float mediaOsseo512Ref;
            float mediaOsseo346Atual;
            float mediaOsseo346Ref;
            //Aereo
            float difAereo250;
            float difAereo500;
            float difAereo1000;
            float difAereo2000;
            float difAereo3000;
            float difAereo4000;
            float difAereo6000;
            float difAereo8000;
            //Osseo
            float difOsseo500;
            float difOsseo1000;
            float difOsseo2000;
            float difOsseo3000;
            float difOsseo4000;
            float difOsseo6000;

            int interpretacaoRef;
            int interpretacaoSeq = 0;

            AudiometriaAudiograma audiogramaReferencial = this.GetAudiogramaReferencial();

            if (audiogramaReferencial.mirrorOld == null)
                audiogramaReferencial.Find();

            mediaAereo512Atual = (GetVal(this.Aereo500, false) + GetVal(this.Aereo1000, false) + GetVal(this.Aereo2000, false)) / 3F;
            mediaAereo512Ref = (GetVal(audiogramaReferencial.Aereo500, false) + GetVal(audiogramaReferencial.Aereo1000, false) + GetVal(audiogramaReferencial.Aereo2000, false)) / 3F;

            mediaAereo346Atual = (GetVal(this.Aereo3000, false) + GetVal(this.Aereo4000, false) + GetVal(this.Aereo6000, false)) / 3F;
            mediaAereo346Ref = (GetVal(audiogramaReferencial.Aereo3000, false) + GetVal(audiogramaReferencial.Aereo4000, false) + GetVal(audiogramaReferencial.Aereo6000, false)) / 3F;

            mediaOsseo512Atual = (GetVal(this.Osseo500, true) + GetVal(this.Osseo1000, true) + GetVal(this.Osseo2000, true)) / 3F;
            mediaOsseo512Ref = (GetVal(audiogramaReferencial.Osseo500, true) + GetVal(audiogramaReferencial.Osseo1000, true) + GetVal(audiogramaReferencial.Osseo2000, true)) / 3F;

            mediaOsseo346Atual = (GetVal(this.Osseo3000, true) + GetVal(this.Osseo4000, true) + GetVal(this.Osseo6000, true)) / 3F;
            mediaOsseo346Ref = (GetVal(audiogramaReferencial.Osseo3000, true) + GetVal(audiogramaReferencial.Osseo4000, true) + GetVal(audiogramaReferencial.Osseo6000, true)) / 3F;

            difAereo250 = GetVal(this.Aereo250, false) - GetVal(audiogramaReferencial.Aereo250, false);
            difAereo500 = GetVal(this.Aereo500, false) - GetVal(audiogramaReferencial.Aereo500, false);
            difAereo1000 = GetVal(this.Aereo1000, false) - GetVal(audiogramaReferencial.Aereo1000, false);
            difAereo2000 = GetVal(this.Aereo2000, false) - GetVal(audiogramaReferencial.Aereo2000, false);
            difAereo3000 = GetVal(this.Aereo3000, false) - GetVal(audiogramaReferencial.Aereo3000, false);
            difAereo4000 = GetVal(this.Aereo4000, false) - GetVal(audiogramaReferencial.Aereo4000, false);
            difAereo6000 = GetVal(this.Aereo6000, false) - GetVal(audiogramaReferencial.Aereo6000, false);
            difAereo8000 = GetVal(this.Aereo8000, false) - GetVal(audiogramaReferencial.Aereo8000, false);

            difOsseo500 = GetVal(this.Osseo500, true) - GetVal(audiogramaReferencial.Osseo500, true);
            difOsseo1000 = GetVal(this.Osseo1000, true) - GetVal(audiogramaReferencial.Osseo1000, true);
            difOsseo2000 = GetVal(this.Osseo2000, true) - GetVal(audiogramaReferencial.Osseo2000, true);
            difOsseo3000 = GetVal(this.Osseo3000, true) - GetVal(audiogramaReferencial.Osseo3000, true);
            difOsseo4000 = GetVal(this.Osseo4000, true) - GetVal(audiogramaReferencial.Osseo4000, true);
            difOsseo6000 = GetVal(this.Osseo6000, true) - GetVal(audiogramaReferencial.Osseo6000, true);

            interpretacaoRef = audiogramaReferencial.InterpretacaoReferencial();

            if (interpretacaoRef == (int)Interpretacao.Aceitavel
                && ((mediaAereo346Atual - mediaAereo346Ref) >= 10F
                || (mediaOsseo346Atual - mediaOsseo346Ref) >= 10F
                || difAereo3000 >= 15F
                || difAereo4000 >= 15F
                || difAereo6000 >= 15F
                || difOsseo3000 >= 15F
                || difOsseo4000 >= 15F
                || difOsseo6000 >= 15F))
            {
                interpretacaoSeq = (int)Interpretacao.Desencadeamento;
            }
            else if (interpretacaoRef == (int)Interpretacao.Sugestivo
                && ((mediaAereo512Atual - mediaAereo512Ref) >= 10F
                || (mediaAereo346Atual - mediaAereo346Ref) >= 10F
                || (mediaOsseo512Atual - mediaOsseo512Ref) >= 10F
                || (mediaOsseo346Atual - mediaOsseo346Ref) >= 10F
                //Aereo
                || difAereo250 >= 15F
                || difAereo500 >= 15F
                || difAereo1000 >= 15F
                || difAereo2000 >= 15F
                || difAereo3000 >= 15F
                || difAereo4000 >= 15F
                || difAereo6000 >= 15F
                || difAereo8000 >= 15F
                //Osseo
                || difOsseo500 >= 15F
                || difOsseo1000 >= 15F
                || difOsseo2000 >= 15F
                || difOsseo3000 >= 15F
                || difOsseo4000 >= 15F
                || difOsseo6000 >= 15F))
            {
                interpretacaoSeq = (int)Interpretacao.Agravamento;
            }
            else
            {
                interpretacaoSeq = (int)Interpretacao.Aceitavel;
            }

            return interpretacaoSeq;
        }
		#endregion

		#region Interpretacao Referencial


        private int InterpretacaoReferencial()
        {
            if (this.Aereo250 == string.Empty)
                return 0;

            if (GetVal(this.Aereo250, false) <= 25F
                && GetVal(this.Aereo500, false) <= 25F
                && GetVal(this.Aereo1000, false) <= 25F
                && GetVal(this.Aereo2000, false) <= 25F
                && GetVal(this.Aereo3000, false) <= 25F
                && GetVal(this.Aereo4000, false) <= 25F
                && GetVal(this.Aereo6000, false) <= 25F
                && GetVal(this.Aereo8000, false) <= 25F

                && GetVal(this.Osseo500, true) <= 25F
                && GetVal(this.Osseo1000, true) <= 25F
                && GetVal(this.Osseo2000, true) <= 25F
                && GetVal(this.Osseo3000, true) <= 25F
                && GetVal(this.Osseo4000, true) <= 25F
                && GetVal(this.Osseo6000, true) <= 25F)
            {
                return (int)Interpretacao.Aceitavel;
            }


            if ((GetVal(this.Aereo3000, false) > 25F
                || GetVal(this.Aereo4000, false) > 25F
                || GetVal(this.Aereo6000, false) > 25F)

                && ((GetVal(this.Aereo250, false) < GetVal(this.Aereo3000, false)
                && GetVal(this.Aereo500, false) < GetVal(this.Aereo3000, false)
                && GetVal(this.Aereo1000, false) < GetVal(this.Aereo3000, false)
                && GetVal(this.Aereo2000, false) < GetVal(this.Aereo3000, false)
                && GetVal(this.Aereo8000, false) < GetVal(this.Aereo3000, false))
                ||
                (GetVal(this.Aereo250, false) < GetVal(this.Aereo4000, false)
                && GetVal(this.Aereo500, false) < GetVal(this.Aereo4000, false)
                && GetVal(this.Aereo1000, false) < GetVal(this.Aereo4000, false)
                && GetVal(this.Aereo2000, false) < GetVal(this.Aereo4000, false)
                && GetVal(this.Aereo8000, false) < GetVal(this.Aereo4000, false))

                || (GetVal(this.Aereo250, false) < GetVal(this.Aereo6000, false)
                && GetVal(this.Aereo500, false) < GetVal(this.Aereo6000, false)
                && GetVal(this.Aereo1000, false) < GetVal(this.Aereo6000, false)
                && GetVal(this.Aereo2000, false) < GetVal(this.Aereo6000, false)
                && GetVal(this.Aereo8000, false) < GetVal(this.Aereo6000, false))
                )
                )
            {
                return (int)Interpretacao.Sugestivo;
            }

            return (int)Interpretacao.NaoSugestivo;
        }

		#endregion

		#endregion
	}
}
