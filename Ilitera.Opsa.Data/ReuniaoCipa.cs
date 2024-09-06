using System;
using Ilitera.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    #region ReuniaoCipa

    [Database("opsa","ReuniaoCipa","IdReuniaoCipa")]
	public class ReuniaoCipa : EventoCipa 
	{
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ReuniaoCipa()
		{

		}
		private	int _IdReuniaoCipa;
        private string _AssuntosDiscutidos = string.Empty;
        private string _FraseAcidentes = "que não houve acidente do trabalho no período em questão.";

        public override int Id
		{
			get{return _IdReuniaoCipa;}
			set{_IdReuniaoCipa = value;}
		}
		public string AssuntosDiscutidos
		{
			get{return _AssuntosDiscutidos;}
			set{_AssuntosDiscutidos = value;}
		}
        public string FraseAcidentes
        {
            get { return _FraseAcidentes; }
            set { _FraseAcidentes = value; }
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public int GetNumeroReuniao()
		{
			return this.IdEventoBaseCipa.Id - (int)EventoBase.RegistroDRT;
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public string GetTituloReuniao()
		{
			StringBuilder txt = new StringBuilder();
			if(this.IdEventoBaseCipa.Id!=(short)EventoBase.ReuniaoExtraordinaria)
			{
				txt.Append("ATA DA REUNIÃO MENSAL Nº ");
				txt.Append(this.GetNumeroReuniao().ToString("00"));
				txt.Append(" DA CIPA");
			}
			else
				txt.Append("ATA DA REUNIÃO EXTRAORDINÁRIA");
			return txt.ToString();
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public string GetTextoAta()
        {
            FrasePadrao frasePadrao = new FrasePadrao();
            frasePadrao.Find("IdEventoBaseCipa=" + this.IdEventoBaseCipa.Id);

            StringBuilder textoAta = new StringBuilder();

            textoAta.Append(frasePadrao.Abertura + " ");
            textoAta.Replace("FRASE_ACIDENTE", this.FraseAcidentes);

            List<Ata> list = new Ata().Find<Ata>("IdReuniaoCipa=" + this.Id 
                                                + " ORDER BY NumOrdem");
            foreach (Ata ata in list)
                textoAta.Append(ata.Texto + " ");

            textoAta.Append(" " + frasePadrao.Encerramento);

            List<ReuniaoPresencaCipa> secretarios
                = new ReuniaoPresencaCipa().Find<ReuniaoPresencaCipa>("IdReuniaoCipa=" + this.Id
                + " AND IdMembroCipa IN (SELECT IdMembroCipa FROM MembroCipa WHERE "
                + " IdCipa = " + this.IdCipa.Id
                + " AND IndGrupoMembro=" + (int)GrupoMembro.Secretario + ")");

            if (secretarios.Count > 0)
                textoAta.Replace("NOME_SECRETARIA", secretarios[0].ToString());
        
            return textoAta.ToString();
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public string GetTextoAtaHtml()
        {
            FrasePadrao frasePadrao = new FrasePadrao();
            frasePadrao.Find("IdEventoBaseCipa=" + this.IdEventoBaseCipa.Id);

            StringBuilder textoAta = new StringBuilder();

            textoAta.Append(@"<span style=""color: #008000"">");
            textoAta.Append(frasePadrao.Abertura + "&nbsp;");
            textoAta.Append("</span>");
            textoAta.Append("<br>");

            textoAta.Replace("FRASE_ACIDENTE", this.FraseAcidentes);

            List<Ata> list = new Ata().Find<Ata>("IdReuniaoCipa=" + this.Id
                                                + " ORDER BY NumOrdem");
            foreach (Ata ata in list)
            {
                textoAta.Append(@"<span style=""font-weight: bold; color: #ff0000"">");
                textoAta.Append(ata.Texto + "&nbsp;");
                textoAta.Append("</span> ");
            }

            textoAta.Append("<br>");
            textoAta.Append(@"<span style=""color: #000080"">");
            textoAta.Append(frasePadrao.Encerramento);
            textoAta.Append("</span>");

            List<ReuniaoPresencaCipa> secretarios
                = new ReuniaoPresencaCipa().Find<ReuniaoPresencaCipa>("IdReuniaoCipa=" + this.Id
                + " AND IdMembroCipa IN (SELECT IdMembroCipa FROM MembroCipa WHERE "
                + " IdCipa = " + this.IdCipa.Id
                + " AND IndGrupoMembro=" + (int)GrupoMembro.Secretario + ")");

            if (secretarios.Count > 0)
                textoAta.Replace("NOME_SECRETARIA", secretarios[0].ToString());

            return textoAta.ToString();
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public string AtaReuniao()
		{
			StringBuilder textoAta = new StringBuilder();
			Ata ata = new Ata();			
			textoAta.Append("COMISSÃO INTERNA DE PREVENÇÃO DE ACIDENTES");
			textoAta.Append('\n');
			textoAta.Append('\n');
			textoAta.Append(this.GetTituloReuniao());
			textoAta.Append('\n');
			textoAta.Append('\n');
			textoAta.Append("DATA: "+this.DataSolicitacao.ToString("dd-MM-yyyy")); 
			textoAta.Append("        ");
			textoAta.Append("HORÁRIO: "+this.HoraInicio); 
			textoAta.Append(" ÀS "+this.HoraFim);
			this.IdCipa.Find();
			this.IdCipa.IdCliente.Find();
			textoAta.Append('\n');
			textoAta.Append('\n');
			textoAta.Append(this.IdCipa.IdCliente.NomeCompleto);
			textoAta.Append('\n');
			textoAta.Append('\n');
			textoAta.Append('\t');
			textoAta.Append(this.GetTextoAta());
			textoAta.Append('\n');
			return textoAta.ToString();
		}
    }

    #endregion

    #region Ata

    [Database("opsa","Ata","IdAta")]
	public class Ata : Ilitera.Data.Table 
	{
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Ata()
		{

		}
		private	int _IdAta;
		private ReuniaoCipa _IdReuniaoCipa;
		private Frase _IdFrase;
		private string _Texto = string.Empty;
		private int _NumOrdem;
		
		public override int Id
		{
			get{return _IdAta;}
			set{_IdAta = value;}
		}
		public ReuniaoCipa IdReuniaoCipa
		{
			get{return _IdReuniaoCipa;}
			set{_IdReuniaoCipa = value;}
		}
		public Frase IdFrase
		{
			get{return _IdFrase;}
			set{_IdFrase = value;}
		}
		public string Texto
		{
			get{return _Texto;}
			set{_Texto = value;}
		}
		public int NumOrdem
		{
			get{return _NumOrdem;}
			set{_NumOrdem = value;}
		}
        public override string ToString()
        {
            string ret = string.Empty;

            if (IdFrase.Id != 0)
            {
                if (this.IdFrase.mirrorOld == null)
                    this.IdFrase.Find();

                ret = this.IdFrase.Descricao;
            }
            else
            {
                ret = this.Texto;
            }
            return ret;
        }

        
        public override int Save()
		{
			if(this.Id==0)
			{
				this.IdReuniaoCipa.Find();
				Ata ata = new Ata();
				ArrayList listAtas = ata.Find("IdFrase=" + this.IdFrase.Id
					+" AND IdReuniaoCipa IN (SELECT IdEventoCipa FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.EventoCipa WHERE IdCipa="+this.IdReuniaoCipa.IdCipa.Id+")"
					+" AND IdFrase IN (SELECT IdFrase FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Frase WHERE Exclusiva=1)");
				if(ata.Id>0 || listAtas.Count>0)
				{
					ata.IdReuniaoCipa.Find();	
					if(!ata.IdReuniaoCipa.DataSolicitacao.Equals(new DateTime()))
						throw new Exception("Essa frase já foi usada na ata de "+ata.IdReuniaoCipa.DataSolicitacao.ToString("dd-MM-yyyy")+"!");
				}
			}
			return base.Save();
		}
    }
    #endregion

    #region Frase

    [Database("opsa","Frase","IdFrase")]
	public class Frase : Ilitera.Data.Table 
	{
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Frase()
		{
		}
		private	int _IdFrase;
		private Obrigacao _IdObrigacao;
		private	string _Descricao =  string.Empty;
		private string _Texto = string.Empty;
		private	bool _Automatica;
		private	bool _Exclusiva;
				
		public override int Id
		{
			get{return _IdFrase;}
			set{_IdFrase = value;}
		}
		public Obrigacao IdObrigacao
		{
			get{return _IdObrigacao;}
			set{_IdObrigacao = value;}
		}
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
		public string Texto
		{
			get{return _Texto;}
			set{_Texto = value;}
		}
		public bool Automatica
		{
			get{return _Automatica;}
			set{_Automatica = value;}
		}
		public bool Exclusiva
		{
			get{return _Exclusiva;}
			set{_Exclusiva = value;}
		}
		public override string ToString()
		{
			return _Descricao;
		}
		public override void Delete()
		{
			if(this.Id==99)
				throw new Exception("Essa frase não pode ser excluída!");
			base.Delete ();
		}
    }
    #endregion

    #region FraseBotao

    [Database("opsa","FraseBotao","IdFraseBotao")]
	public class FraseBotao : Ilitera.Data.Table 
	{

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FraseBotao()
		{
		}
		private	int _IdFraseBotao;
		private Frase _IdFrase;
		private string _Descricao;
        		
		public override int Id
		{
			get{return _IdFraseBotao;}
			set{_IdFraseBotao = value;}
		}
		public Frase IdFrase
		{
			get{return _IdFrase;}
			set{_IdFrase = value;}
		}
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
    }
    #endregion

    #region FrasePadrao

    [Database("opsa","FrasePadrao","IdFrasePadrao")]
	public class FrasePadrao : Ilitera.Data.Table 
	{
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FrasePadrao()
		{

		}
		private	int _IdFrasePadrao;
		private EventoBaseCipa _IdEventoBaseCipa;
		private string _Abertura = string.Empty;
		private string _Encerramento = string.Empty;
        		
		public override int Id
		{
			get{return _IdFrasePadrao;}
			set{_IdFrasePadrao = value;}
		}
		public EventoBaseCipa IdEventoBaseCipa
		{
			get{return _IdEventoBaseCipa;}
			set{_IdEventoBaseCipa = value;}
		}
		public string Abertura
		{
			get{return _Abertura;}
			set{_Abertura = value;}
		}
		public string Encerramento
		{
			get{return _Encerramento;}
			set{_Encerramento = value;}
		}
    }
    #endregion

    #region FraseBotaoRapido

    public class FraseBotaoRapido
	{
		public FraseBotaoRapido()
		{		
			_fraseBotao1 = new FraseBotao();
			_fraseBotao1.Find(1);
			_fraseBotao2 = new FraseBotao();
			_fraseBotao2.Find(2);
			_fraseBotao3 = new FraseBotao();
			_fraseBotao3.Find(3);
			_fraseBotao4 = new FraseBotao();
			_fraseBotao4.Find(4);
			_fraseBotao5 = new FraseBotao();
			_fraseBotao5.Find(5);
			_fraseBotao6 = new FraseBotao();
			_fraseBotao6.Find(6);
			_fraseBotao7 = new FraseBotao();
			_fraseBotao7.Find(7);
			_fraseBotao8 = new FraseBotao();
			_fraseBotao8.Find(8);
			_fraseBotao9 = new FraseBotao();
			_fraseBotao9.Find(9);
			_fraseBotao10 = new FraseBotao();
			_fraseBotao10.Find(10);
			_fraseBotao11 = new FraseBotao();
			_fraseBotao11.Find(11);
			_fraseBotao12 = new FraseBotao();
			_fraseBotao12.Find(12);
		}
		private FraseBotao _fraseBotao1;
		private FraseBotao _fraseBotao2;
		private FraseBotao _fraseBotao3;
		private FraseBotao _fraseBotao4;
		private FraseBotao _fraseBotao5;
		private FraseBotao _fraseBotao6;
		private FraseBotao _fraseBotao7;
		private FraseBotao _fraseBotao8;
		private FraseBotao _fraseBotao9;
		private FraseBotao _fraseBotao10;
		private FraseBotao _fraseBotao11;
		private FraseBotao _fraseBotao12;

		public string DescricaoFrase1
		{
			get{return _fraseBotao1.Descricao;}
			set{_fraseBotao1.Descricao = value;}
		}
		public Frase IdFrase1
		{
			get{return _fraseBotao1.IdFrase;}
			set{_fraseBotao1.IdFrase = value;}
		}
		public string DescricaoFrase2
		{
			get{return _fraseBotao2.Descricao;}
			set{_fraseBotao2.Descricao = value;}
		}
		public Frase IdFrase2
		{
			get{return _fraseBotao2.IdFrase;}
			set{_fraseBotao2.IdFrase = value;}
		}
		public string DescricaoFrase3
		{
			get{return _fraseBotao3.Descricao;}
			set{_fraseBotao3.Descricao = value;}
		}
		public Frase IdFrase3
		{
			get{return _fraseBotao3.IdFrase;}
			set{_fraseBotao3.IdFrase = value;}
		}
		public string DescricaoFrase4
		{
			get{return _fraseBotao4.Descricao;}
			set{_fraseBotao4.Descricao = value;}
		}
		public Frase IdFrase4
		{
			get{return _fraseBotao4.IdFrase;}
			set{_fraseBotao4.IdFrase = value;}
		}
		public string DescricaoFrase5
		{
			get{return _fraseBotao5.Descricao;}
			set{_fraseBotao5.Descricao = value;}
		}
		public Frase IdFrase5
		{
			get{return _fraseBotao5.IdFrase;}
			set{_fraseBotao5.IdFrase = value;}
		}
		public string DescricaoFrase6
		{
			get{return _fraseBotao6.Descricao;}
			set{_fraseBotao6.Descricao = value;}
		}
		public Frase IdFrase6
		{
			get{return _fraseBotao6.IdFrase;}
			set{_fraseBotao6.IdFrase = value;}
		}
		public string DescricaoFrase7
		{
			get{return _fraseBotao7.Descricao;}
			set{_fraseBotao7.Descricao = value;}
		}
		public Frase IdFrase7
		{
			get{return _fraseBotao7.IdFrase;}
			set{_fraseBotao7.IdFrase = value;}
		}
		public string DescricaoFrase8
		{
			get{return _fraseBotao8.Descricao;}
			set{_fraseBotao8.Descricao = value;}
		}
		public Frase IdFrase8
		{
			get{return _fraseBotao8.IdFrase;}
			set{_fraseBotao8.IdFrase = value;}
		}
		public string DescricaoFrase9
		{
			get{return _fraseBotao9.Descricao;}
			set{_fraseBotao9.Descricao = value;}
		}
		public Frase IdFrase9
		{
			get{return _fraseBotao9.IdFrase;}
			set{_fraseBotao9.IdFrase = value;}
		}
		public string DescricaoFrase10
		{
			get{return _fraseBotao10.Descricao;}
			set{_fraseBotao10.Descricao = value;}
		}
		public Frase IdFrase10
		{
			get{return _fraseBotao10.IdFrase;}
			set{_fraseBotao10.IdFrase = value;}
		}
		public string DescricaoFrase11
		{
			get{return _fraseBotao11.Descricao;}
			set{_fraseBotao11.Descricao = value;}
		}
		public Frase IdFrase11
		{
			get{return _fraseBotao11.IdFrase;}
			set{_fraseBotao11.IdFrase = value;}
		}
		public string DescricaoFrase12
		{
			get{return _fraseBotao12.Descricao;}
			set{_fraseBotao12.Descricao = value;}
		}
		public Frase IdFrase12
		{
			get{return _fraseBotao12.IdFrase;}
			set{_fraseBotao12.IdFrase = value;}
		}

		public void Save()
		{
			_fraseBotao1.Save();
			_fraseBotao2.Save();
			_fraseBotao3.Save();
			_fraseBotao4.Save();
			_fraseBotao5.Save();
			_fraseBotao6.Save();
			_fraseBotao7.Save();
			_fraseBotao8.Save();
			_fraseBotao9.Save();
			_fraseBotao10.Save();
			_fraseBotao11.Save();
			_fraseBotao12.Save();
		}
    }
    #endregion

    #region ReuniaoPresencaCipa

    [Database("opsa","ReuniaoPresencaCipa","IdReuniaoPresencaCipa")]
	public class ReuniaoPresencaCipa : Ilitera.Data.Table 
	{

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ReuniaoPresencaCipa()
		{

		}

        public enum Presenca : int
        {
            Presente,
            Ausente,
            FaltaJustificada,
            FaltaSemJustificativa,
            Ferias,
            Afastado
        }

		private	int _IdReuniaoPresencaCipa;
		private	ReuniaoCipa _IdReuniaoCipa;
		private	MembroCipa _IdMembroCipa;
        private Presenca _IndPresenca;
			
		public override int Id
		{
			get{return _IdReuniaoPresencaCipa;}
			set{_IdReuniaoPresencaCipa = value;}
		}
		public ReuniaoCipa IdReuniaoCipa
		{
			get{return _IdReuniaoCipa;}
			set{_IdReuniaoCipa = value;}
		}
		public MembroCipa IdMembroCipa
		{
			get{return _IdMembroCipa;}
			set{_IdMembroCipa = value;}
		}
        public Presenca IndPresenca
        {
            get { return _IndPresenca; }
            set { _IndPresenca = value; }
        }
		public override string ToString()
		{
            if (this.IdMembroCipa.mirrorOld == null)
                this.IdMembroCipa.Find();

			return this.IdMembroCipa.ToString();
		}

        public static string GetPresenca(int IndPreseca)
        {
            string ret;

            switch (IndPreseca)
            {
                case (int)Presenca.Presente:
                    ret = "presente";
                    break;

                case (int)Presenca.Ausente:
                    ret = "ausente";
                    break;

                case (int)Presenca.FaltaJustificada:
                    ret = "falta justificada";
                    break;

                case (int)Presenca.FaltaSemJustificativa:
                    ret = "falta sem justificativa";
                    break;

                case (int)Presenca.Ferias:
                    ret = "férias";
                    break;

                case (int)Presenca.Afastado:
                    ret = "afastado";
                    break;

                default:
                    ret = "-";
                    break;

            }

            return ret;
        }
    }
    #endregion
}
