using System;
using System.Data;
using System.Text;
using System.Collections;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    #region enum TipoDocumento

    public enum TipoDocumento : int
    {
        Outros,
        PPP,
        PPRAIntroducao,
        PPRADocumentoBase,
        PPRAPlanilha,
        PPRAEmpregado,
        PPRAQuadroEPI,
        PCMSONormasGeraisAcao,
        PCMSOPlanilhaAnaliseGlobal,
        PCMSORelatorioAnual,
        PCMSOExamesPorEmpregado,
        PCMSOExamesPorExame,
        LTCAT,
        LaudoErgCronograma,
        LaudoErgonomico,
        QuadrosNR4,
        OSIntroducao,
        OSListagemProcedimentos,
        OSGenericos,
        OSEspecificos,
        OSCompleto,
        AuditoriaIntroducao,
        AuditoriaIrregularidade,
        CIPAAta,
        CertificadoTreinamentos,
        OSFichaPOPS,
        CIPACalendarioAnual,
        FichaExintor,
        PCMSOExamesRealizados,
        AuditoriaCompleta,
        PPRACompleto,
        PCMSOCompleto,
        APR,
        API,
        AuditoriaFlash,
        DownloadAuditoriaFlash,
        CaldeiraProjeto,
        CaldeiraInspecao,
        VasoProjeto,
        VasoInspecao,
        OS17
    }
    #endregion

	[Database("opsa", "ImpressaoDocUsuario", "IdImpressaoDocUsuario")]
	public class ImpressaoDocUsuario: Ilitera.Data.Table
    {
        #region Properties

        private int	_IdImpressaoDocUsuario;
		private Usuario _IdUsuario;
        private bool _IsFromWeb;
		private DateTime _DataImpressao;
        private TimeSpan _TempoProcessamento;
        private Pessoa _IdPessoaContato;
		private int _IndTipoDocumento;
        private Funcionalidade _IdFuncionalidade;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ImpressaoDocUsuario()
		{

		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ImpressaoDocUsuario(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{														  
			get{return _IdImpressaoDocUsuario;}
			set	{_IdImpressaoDocUsuario = value;}
		}
		public Usuario IdUsuario
		{														  
			get{return _IdUsuario;}
			set	{_IdUsuario = value;}
		}
        public bool IsFromWeb
        {
            get { return _IsFromWeb; }
            set { _IsFromWeb = value; }
        }
		public DateTime DataImpressao
		{														  
			get{return _DataImpressao;}
			set	{_DataImpressao = value;}
		}
		public int IndTipoDocumento
		{														  
			get{return _IndTipoDocumento;}
			set	{_IndTipoDocumento = value;}
		}
        public Pessoa IdPessoaContato
        {
            get { return _IdPessoaContato; }
            set { _IdPessoaContato = value; }
        }
        public TimeSpan TempoProcessamento
        {
            get { return _TempoProcessamento; }
            set { _TempoProcessamento = value; }
        }
        public Funcionalidade IdFuncionalidade
        {
            get { return _IdFuncionalidade; }
            set { _IdFuncionalidade = value; }
        }
        #endregion

        #region InsereRegistro

        public static void InsereRegistro(Usuario usuario, int TipoDoc)
        {
            ImpressaoDocUsuario impressao = new ImpressaoDocUsuario();
            impressao.Inicialize();
            impressao.IdUsuario = usuario;
            impressao.DataImpressao = DateTime.Now;
            impressao.IndTipoDocumento = TipoDoc;
            impressao.UsuarioId = usuario.Id;
            impressao.IsFromWeb = true;
            impressao.Save();
        }

        public static void InsereRegistro(Usuario usuario,
                                            Pessoa pessoaContato,
                                            long lngTempoProcessamento,
                                            bool IsFromWeb,
                                            Funcionalidade funcionalidade)
        {
            InsereRegistro(usuario, TipoDocumento.Outros, pessoaContato, lngTempoProcessamento, IsFromWeb, funcionalidade);
        }

        public static void InsereRegistro(Usuario usuario,
                                            TipoDocumento TipoDoc,
                                            Pessoa pessoaContato,
                                            long lngTempoProcessamento,
                                            bool IsFromWeb,
                                            Funcionalidade funcionalidade)
        {
            ImpressaoDocUsuario impressao = new ImpressaoDocUsuario();
            impressao.Inicialize();
            impressao.DataImpressao = DateTime.Now;
            impressao.IdUsuario.Id = usuario.Id;
            impressao.IndTipoDocumento = (int)TipoDoc;
            impressao.TempoProcessamento = new TimeSpan(lngTempoProcessamento);
            impressao.IdPessoaContato.Id = pessoaContato.Id;
            impressao.IdFuncionalidade.Id = funcionalidade.Id;
            impressao.IsFromWeb = IsFromWeb;
            impressao.Save();
        }
        #endregion

        #region GetNomeDocumento

        public static string GetNomeDocumento(int IndTipoDocumento)
		{
			string NomeDocumento = string.Empty;

			switch (IndTipoDocumento)
			{
				case (int)TipoDocumento.PPP:
					NomeDocumento = "PPP";
					break;
				case (int)TipoDocumento.PPRAIntroducao:
					NomeDocumento = "PPRA - Introdução";
					break;
				case (int)TipoDocumento.PPRADocumentoBase:
					NomeDocumento = "PPRA - Documento Base";
					break;
				case (int)TipoDocumento.PPRAPlanilha:
					NomeDocumento = "PPRA - Planilha";
					break;
				case (int)TipoDocumento.PPRAEmpregado:
					NomeDocumento = "PPRA - Empregado";
					break;
				case (int)TipoDocumento.PPRAQuadroEPI:
					NomeDocumento = "PPRA - Quadro EPI";
					break;
				case (int)TipoDocumento.PCMSONormasGeraisAcao:
					NomeDocumento = "PCMSO - Normas Gerais de Ação";
					break;
				case (int)TipoDocumento.PCMSOPlanilhaAnaliseGlobal:
					NomeDocumento = "PCMSO - Planilha Análise Global";
					break;
				case (int)TipoDocumento.PCMSORelatorioAnual:
					NomeDocumento = "PCMSO - Relatório Anual";
					break;
				case (int)TipoDocumento.PCMSOExamesPorEmpregado:
					NomeDocumento = "PCMSO - Planej. por Empregado";
					break;
				case (int)TipoDocumento.PCMSOExamesPorExame:
					NomeDocumento = "PCMSO - Planej. por Exame";
					break;
				case (int)TipoDocumento.LTCAT:
					NomeDocumento = "LTCAT";
					break;
				case (int)TipoDocumento.LaudoErgCronograma:
					NomeDocumento = "Laudo Ergonômico - Cronograma";
					break;
				case (int)TipoDocumento.LaudoErgonomico:
					NomeDocumento = "Laudo Ergonômico";
					break;
				case (int)TipoDocumento.QuadrosNR4:
					NomeDocumento = "Quadros da NR-4";
					break;
				case (int)TipoDocumento.OSIntroducao:
					NomeDocumento = "OS - Introdução";
					break;
				case (int)TipoDocumento.OSListagemProcedimentos:
					NomeDocumento = "OS - Listagem de Procedimentos";
					break;
				case (int)TipoDocumento.OSGenericos:
					NomeDocumento = "OS - Procedimentos Genéricos";
					break;
				case (int)TipoDocumento.OSEspecificos:
					NomeDocumento = "OS - Procedimentos Específicos";
					break;
				case (int)TipoDocumento.OSCompleto:
					NomeDocumento = "OS - Completo";
					break;
				case (int)TipoDocumento.AuditoriaIntroducao:
					NomeDocumento = "Auditoria - Introdução";
					break;
				case (int)TipoDocumento.AuditoriaIrregularidade:
					NomeDocumento = "Auditoria - Irregularidade";
					break;
				case (int)TipoDocumento.CIPAAta:
					NomeDocumento = "CIPA - Ata";
					break;
				case (int)TipoDocumento.CertificadoTreinamentos:
					NomeDocumento = "Certificados de Treinamentos";
					break;
				case (int)TipoDocumento.OSFichaPOPS:
					NomeDocumento = "OS - Ficha POPS";
					break;
				case (int)TipoDocumento.CIPACalendarioAnual:
					NomeDocumento = "CIPA - Calendário Anual";
					break;
				case (int)TipoDocumento.FichaExintor:
					NomeDocumento = "Ficha Extintor de Incêndio";
					break;
				case (int)TipoDocumento.PCMSOExamesRealizados:
					NomeDocumento = "PCMSO - Exames Realizados";
					break;
				case (int)TipoDocumento.AuditoriaCompleta:
					NomeDocumento = "Auditoria Completa";
					break;
                case (int)TipoDocumento.PPRACompleto:
                    NomeDocumento = "PPRA - Completo";
                    break;
                case (int)TipoDocumento.PCMSOCompleto:
                    NomeDocumento = "PCMSO - Completo";
                    break;
                case (int)TipoDocumento.APR:
                    NomeDocumento = "APR";
                    break;
                case (int)TipoDocumento.API:
                    NomeDocumento = "API";
                    break;
                case (int)TipoDocumento.AuditoriaFlash:
                    NomeDocumento = "Auditoria Formato Flash";
                    break;
                case (int)TipoDocumento.DownloadAuditoriaFlash:
                    NomeDocumento = "Auditoria Formato Flash - Download";
                    break;
                case (int)TipoDocumento.CaldeiraProjeto:
                    NomeDocumento = "Caldeira - Projeto";
                    break;
                case (int)TipoDocumento.CaldeiraInspecao:
                    NomeDocumento = "Caldeira - Inspeção";
                    break;
                case (int)TipoDocumento.VasoProjeto:
                    NomeDocumento = "Vaso de Pressão - Projeto";
                    break;
                case (int)TipoDocumento.VasoInspecao:
                    NomeDocumento = "Vaso de Pressão - Inspeção";
                    break;
                case (int)TipoDocumento.OS17:
                    NomeDocumento = "OS - NR 1.7";
                    break;
			}

			return NomeDocumento;
        }
        #endregion

        #region GetImpressaoMes

        public static int GetImpressaoMes(int IndTipoDocumento, int IdUsuario)
		{
			return new ImpressaoDocUsuario().ExecuteCount("IndTipoDocumento=" + IndTipoDocumento
				+" AND MONTH(DataImpressao)=" + DateTime.Now.Month
				+" AND YEAR(DataImpressao)=" + DateTime.Now.Year
				+" AND IdUsuario=" + IdUsuario);
		}

		public static int GetImpressaoMes(int IndTipoDocumento, int IdUsuario, DateTime specificDate)
		{
			return new ImpressaoDocUsuario().ExecuteCount("IndTipoDocumento=" + IndTipoDocumento
				+" AND MONTH(DataImpressao)=" + specificDate.Month
				+" AND YEAR(DataImpressao)=" + specificDate.Year
				+" AND IdUsuario=" + IdUsuario);
        }
        #endregion

        #region ExtratoMensalUser

        public static void ExtratoMensalUser()
		{
            Usuario usuario = new Usuario(1);

            ArrayList alUsuarios = new Usuario().Find("IdPessoa IN (SELECT IdPessoa FROM JuridicaPessoa WHERE"
                + " (IdJuridica IN (SELECT IdCliente FROM qryClienteAtivos) OR IdJuridica=" + (int)Empresas.MestraPaulista
                + ") AND IdPessoa IN (SELECT IdPessoa FROM Pessoa WHERE IsInativo=0))");

			string mesAtual = string.Empty;
            StringBuilder atualizacao = new StringBuilder();

            int numLogin, numTotalImp, impPPP=0, impPPRAIntroducao=0, impPPRADocBase=0, impPPRAPlanilha=0, impPPRAEmpregado=0, impPPRAQuadroEPI=0,
                impPCMSONormas=0, impPCMSOPlanilha=0, impPCMSORelatorio=0, impPCMSOPlanejEmp=0, impPCMSOPlanejEx=0,
                impLTCAT=0, impLaudoErgCrono=0, impLaudoErg=0, impQuadrosNR4=0, impOSIntr=0, impOSListProc=0, impOSProcGene=0, impOSProcEsp=0, impOSCompleto=0,
                impAudIntr=0, impAudIrreg=0, impCIPAAta=0, impCertificados=0, impOSFichaPOPS=0, impCIPACalendarioAnual=0, impFichaExt=0, impPCMSOExRealiz=0,
                impAuditoriaCompleta=0, impPPRACompleto=0, impPCMSOCompleto=0, impAPR=0, impAPI=0, impAudFlash=0, impAudFlashDownload=0;

            DateTime selectedDate = DateTime.Now.AddMonths(-1);

            switch (selectedDate.Month)
            {
                case 1:
                    mesAtual = "Janeiro/" + selectedDate.Year.ToString();
                    break;
                case 2:
                    mesAtual = "Fevereiro/" + selectedDate.Year.ToString();
                    break;
                case 3:
                    mesAtual = "Março/" + selectedDate.Year.ToString();
                    break;
                case 4:
                    mesAtual = "Abril/" + selectedDate.Year.ToString();
                    break;
                case 5:
                    mesAtual = "Maio/" + selectedDate.Year.ToString();
                    break;
                case 6:
                    mesAtual = "Junho/" + selectedDate.Year.ToString();
                    break;
                case 7:
                    mesAtual = "Julho/" + selectedDate.Year.ToString();
                    break;
                case 8:
                    mesAtual = "Agosto/" + selectedDate.Year.ToString();
                    break;
                case 9:
                    mesAtual = "Setembro/" + selectedDate.Year.ToString();
                    break;
                case 10:
                    mesAtual = "Outubro/" + selectedDate.Year.ToString();
                    break;
                case 11:
                    mesAtual = "Novembro/" + selectedDate.Year.ToString();
                    break;
                case 12:
                    mesAtual = "Dezembro/" + selectedDate.Year.ToString();
                    break;
            }

            foreach (Usuario user in alUsuarios)
            {
                numTotalImp = 0;
                atualizacao = new StringBuilder();

                if (user.IdPessoa.mirrorOld == null)
                    user.IdPessoa.Find();

                numLogin = new HistoricoLogin().ExecuteCount("IdUsuario=" + user.Id
                    + " AND MONTH(DataLogin)=" + selectedDate.Month
                    + " AND YEAR(DataLogin)=" + selectedDate.Year);

                Type documentos = typeof(TipoDocumento);

                foreach (int tipoDoc in Enum.GetValues(documentos))
                {
                    switch (tipoDoc)
                    {
                        case (int)TipoDocumento.PPP:
                            impPPP = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impPPP;
                            break;
                        case (int)TipoDocumento.PPRAIntroducao:
                            impPPRAIntroducao = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impPPRAIntroducao;
                            break;
                        case (int)TipoDocumento.PPRADocumentoBase:
                            impPPRADocBase = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impPPRADocBase;
                            break;
                        case (int)TipoDocumento.PPRAPlanilha:
                            impPPRAPlanilha = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impPPRAPlanilha;
                            break;
                        case (int)TipoDocumento.PPRAEmpregado:
                            impPPRAEmpregado = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impPPRAEmpregado;
                            break;
                        case (int)TipoDocumento.PPRAQuadroEPI:
                            impPPRAQuadroEPI = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impPPRAQuadroEPI;
                            break;
                        case (int)TipoDocumento.PCMSONormasGeraisAcao:
                            impPCMSONormas = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impPCMSONormas;
                            break;
                        case (int)TipoDocumento.PCMSOPlanilhaAnaliseGlobal:
                            impPCMSOPlanilha = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impPCMSOPlanilha;
                            break;
                        case (int)TipoDocumento.PCMSORelatorioAnual:
                            impPCMSORelatorio = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impPCMSORelatorio;
                            break;
                        case (int)TipoDocumento.PCMSOExamesPorEmpregado:
                            impPCMSOPlanejEmp = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impPCMSOPlanejEmp;
                            break;
                        case (int)TipoDocumento.PCMSOExamesPorExame:
                            impPCMSOPlanejEx = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impPCMSOPlanejEx;
                            break;
                        case (int)TipoDocumento.LTCAT:
                            impLTCAT = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impLTCAT;
                            break;
                        case (int)TipoDocumento.LaudoErgCronograma:
                            impLaudoErgCrono = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impLaudoErgCrono;
                            break;
                        case (int)TipoDocumento.LaudoErgonomico:
                            impLaudoErg = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impLaudoErg;
                            break;
                        case (int)TipoDocumento.QuadrosNR4:
                            impQuadrosNR4 = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impQuadrosNR4;
                            break;
                        case (int)TipoDocumento.OSIntroducao:
                            impOSIntr = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impOSIntr;
                            break;
                        case (int)TipoDocumento.OSListagemProcedimentos:
                            impOSListProc = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impOSListProc;
                            break;
                        case (int)TipoDocumento.OSGenericos:
                            impOSProcGene = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impOSProcGene;
                            break;
                        case (int)TipoDocumento.OSEspecificos:
                            impOSProcEsp = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impOSProcEsp;
                            break;
                        case (int)TipoDocumento.OSCompleto:
                            impOSCompleto = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impOSCompleto;
                            break;
                        case (int)TipoDocumento.AuditoriaIntroducao:
                            impAudIntr = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impAudIntr;
                            break;
                        case (int)TipoDocumento.AuditoriaIrregularidade:
                            impAudIrreg = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impAudIrreg;
                            break;
                        case (int)TipoDocumento.CIPAAta:
                            impCIPAAta = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impCIPAAta;
                            break;
                        case (int)TipoDocumento.CertificadoTreinamentos:
                            impCertificados = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impCertificados;
                            break;
                        case (int)TipoDocumento.OSFichaPOPS:
                            impOSFichaPOPS = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impOSFichaPOPS;
                            break;
                        case (int)TipoDocumento.CIPACalendarioAnual:
                            impCIPACalendarioAnual = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impCIPACalendarioAnual;
                            break;
                        case (int)TipoDocumento.FichaExintor:
                            impFichaExt = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impFichaExt;
                            break;
                        case (int)TipoDocumento.PCMSOExamesRealizados:
                            impPCMSOExRealiz = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impPCMSOExRealiz;
                            break;
                        case (int)TipoDocumento.AuditoriaCompleta:
                            impAuditoriaCompleta = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impAuditoriaCompleta;
                            break;
                        case (int)TipoDocumento.PPRACompleto:
                            impPPRACompleto = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impPPRACompleto;
                            break;
                        case (int)TipoDocumento.PCMSOCompleto:
                            impPCMSOCompleto = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impPCMSOCompleto;
                            break;
                        case (int)TipoDocumento.APR:
                            impAPR = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impAPR;
                            break;
                        case (int)TipoDocumento.API:
                            impAPI = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impAPI;
                            break;
                        case (int)TipoDocumento.AuditoriaFlash:
                            impAudFlash = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impAudFlash;
                            break;
                        case (int)TipoDocumento.DownloadAuditoriaFlash:
                            impAudFlashDownload = GetImpressaoMes(tipoDoc, user.Id, selectedDate);
                            numTotalImp += impAudFlashDownload;
                            break;
                    }
                }

                ArrayList alAtualizacao = new EntityUsuario().Find("IdUsuario=" + user.Id
                    + " AND MONTH(DataCommand)=" + selectedDate.Month
                    + " AND YEAR(DataCommand)=" + selectedDate.Year
                    + " AND ProcessoRealizado IS NOT NULL AND ProcessoRealizado<>''"
                    + " ORDER BY DataCommand DESC");

                string emailBody = string.Empty;

                StringBuilder st = new StringBuilder();
                st.Append(emailBody);
                st.Replace("NomePrestador", user.IdPessoa.NomeCompleto);
                st.Replace("NomeMes", mesAtual);
                st.Replace("NumAcessos", numLogin.ToString());
                st.Replace("NumImpressao", numTotalImp.ToString());
                st.Replace("NumAtualizacao", alAtualizacao.Count.ToString());
                st.Replace("NumPPP", impPPP.ToString());
                st.Replace("NumPPRAI", impPPRAIntroducao.ToString());
                st.Replace("NumPPRAD", impPPRADocBase.ToString());
                st.Replace("NumPPRAP", impPPRAPlanilha.ToString());
                st.Replace("NumPPRAE", impPPRAEmpregado.ToString());
                st.Replace("NumPPRAQ", impPPRAQuadroEPI.ToString());
                st.Replace("NumPPRACompl", impPPRACompleto.ToString());
                st.Replace("NumPCMSON", impPCMSONormas.ToString());
                st.Replace("NumPCMSOPla", impPCMSOPlanilha.ToString());
                st.Replace("NumPCMSOR", impPCMSORelatorio.ToString());
                st.Replace("NumPCMSOPEmp", impPCMSOPlanejEmp.ToString());
                st.Replace("NumPCMSOPE", impPCMSOPlanejEx.ToString());
                st.Replace("NumPCMSOExRe", impPCMSOExRealiz.ToString());
                st.Replace("NumPCMSOCompl", impPCMSOCompleto.ToString());
                st.Replace("NumLTCAT", impLTCAT.ToString());
                st.Replace("NumLauCrono", impLaudoErgCrono.ToString());
                st.Replace("NumLaudo", impLaudoErg.ToString());
                st.Replace("NumQuadNR", impQuadrosNR4.ToString());
                st.Replace("NumOSIntrod", impOSIntr.ToString());
                st.Replace("NumOSListProc", impOSListProc.ToString());
                st.Replace("NumOSProcGene", impOSProcGene.ToString());
                st.Replace("NumOSProcEsp", impOSProcEsp.ToString());
                st.Replace("NumOSCompleto", impOSCompleto.ToString());
                st.Replace("NumOSFichaPOPS", impOSFichaPOPS.ToString());
                st.Replace("NumAPR", impAPR.ToString());
                st.Replace("NumAPI", impAPI.ToString());
                st.Replace("NumAudIntr", impAudIntr.ToString());
                st.Replace("NumAudIrreg", impAudIrreg.ToString());
                st.Replace("NumAudCompl", impAuditoriaCompleta.ToString());
                st.Replace("NumAudFlashV", impAudFlash.ToString());
                st.Replace("NumAudFlashD", impAudFlashDownload.ToString());
                st.Replace("NumCIPAAta", impCIPAAta.ToString());
                st.Replace("NumCIPACalend", impCIPACalendarioAnual.ToString());
                st.Replace("NumCert", impCertificados.ToString());
                st.Replace("NumFichaExt", impFichaExt.ToString());

                foreach (EntityUsuario entity in alAtualizacao)
                {
                    atualizacao.Append("<b>");
                    atualizacao.Append(entity.DataCommand.ToString("dd-MM-yyyy"));
                    atualizacao.Append("</b> - ");
                    atualizacao.Append(entity.ProcessoRealizado);
                    atualizacao.Append("<br>");
                }

                if (!atualizacao.ToString().Equals(string.Empty))
                    st.Replace("DescAtualizacao", atualizacao.ToString());
                else
                    st.Replace("DescAtualizacao", "<div align=\"center\">Não houve atualizações</div>");

                //try
                //{
                //    if (!EmailBase.IsEmail(user.IdPessoa.Email))
                //        throw new Exception("O email cadastrado para o Prestador " + user.IdPessoa.NomeCompleto + " não é válido!");
                    
                //}
                //catch (Exception ex)
                //{
                //    System.Diagnostics.EventLog eventLog = new System.Diagnostics.EventLog("Application", "ReportServer", "Service Mestra");
                //    eventLog.WriteEntry("Extrato Mensal de Uso do Ilitera.NET. Erro: " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                //}
            }
        }
        #endregion
    }
}