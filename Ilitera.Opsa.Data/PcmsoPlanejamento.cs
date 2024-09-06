using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
    #region enum PeriodicidadeExame

    public enum PeriodicidadeExame : int
    {
        Nenhuma,
        Dia,
        Semana,
        Mes,
        Ano
    }
    #endregion

    #region interface IPeriodicidadeExame

    public interface IPeriodicidadeExame
    {
        bool NaAdmissao
        {
            get;
            set;
        }
        bool NaDemissao
        {
            get;
            set;
        }
        bool NoRetornoTrabalho
        {
            get;
            set;
        }
        bool NaMudancaFuncao
        {
            get;
            set;
        }
        bool AposAdmissao
        {
            get;
            set;
        }
        int IndPeriodicidadeAposAdmissao
        {
            get;
            set;
        }
        int IntervaloAposAdmissao
        {
            get;
            set;
        }
        bool Periodico
        {
            get;
            set;
        }
        int IndPeriodicidade
        {
            get;
            set;
        }
        int Intervalo
        {
            get;
            set;
        }
        bool DependeIdade
        {
            get;
            set;
        }
    }
    #endregion

    #region class PcmsoPlanejamento

    [Database("opsa", "PcmsoPlanejamento", "IdPcmsoPlanejamento")]
    public class PcmsoPlanejamento : Ilitera.Data.Table, IPeriodicidadeExame
    {
        #region Properties

        private int _IdPcmsoPlanejamento;
        private Pcmso _IdPcmso;
        private Ghe _IdGHE;
        private ExameDicionario _IdExameDicionario;
        private bool _Preventivo;
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
        private string _Observacao = string.Empty;
        private bool _Temporario;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PcmsoPlanejamento()
        {

        }
        public override int Id
        {
            get { return _IdPcmsoPlanejamento; }
            set { _IdPcmsoPlanejamento = value; }
        }
        public Pcmso IdPcmso
        {
            get { return _IdPcmso; }
            set { _IdPcmso = value; }
        }
        public Ghe IdGHE
        {
            get { return _IdGHE; }
            set { _IdGHE = value; }
        }
        public ExameDicionario IdExameDicionario
        {
            get { return _IdExameDicionario; }
            set { _IdExameDicionario = value; }
        }
        public bool Preventivo
        {
            get { return _Preventivo; }
            set { _Preventivo = value; }
        }
        public bool NaAdmissao
        {
            get { return _NaAdmissao; }
            set { _NaAdmissao = value; }
        }
        public bool NaDemissao
        {
            get { return _NaDemissao; }
            set { _NaDemissao = value; }
        }
        public bool NoRetornoTrabalho
        {
            get { return _NoRetornoTrabalho; }
            set { _NoRetornoTrabalho = value; }
        }
        public bool NaMudancaFuncao
        {
            get { return _NaMudancaFuncao; }
            set { _NaMudancaFuncao = value; }
        }
        public bool AposAdmissao
        {
            get { return _AposAdmissao; }
            set { _AposAdmissao = value; }
        }
        public int IndPeriodicidadeAposAdmissao
        {
            get { return _IndPeriodicidadeAposAdmissao; }
            set { _IndPeriodicidadeAposAdmissao = value; }
        }
        public int IntervaloAposAdmissao
        {
            get { return _IntervaloAposAdmissao; }
            set { _IntervaloAposAdmissao = value; }
        }
        public bool Periodico
        {
            get { return _Periodico; }
            set { _Periodico = value; }
        }
        public int IndPeriodicidade
        {
            get { return _IndPeriodicidade; }
            set { _IndPeriodicidade = value; }
        }
        public int Intervalo
        {
            get { return _Intervalo; }
            set { _Intervalo = value; }
        }
        public bool DependeIdade
        {
            get { return _DependeIdade; }
            set { _DependeIdade = value; }
        }
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }

        public bool Temporario
        {
            get { return _Temporario; }
            set { _Temporario = value; }
        }


        public override string ToString()
        {
            if (this.IdExameDicionario == null)
                this.Inicialize();

            if (this.IdExameDicionario.mirrorOld == null)
                this.IdExameDicionario.Find();

            return this.IdExameDicionario.Nome + "  - " + ExameDicionario.GetPeriodicidadeExame(this);
        }
        #endregion

        #region static metodos

        #region ImportaPlanejamento

        #region ImportaTodoPlanejamentoParaGhe

        public static void ImportaTodoPlanejamentoParaGhe()
        {
            Pcmso pcmso = new Pcmso();
            pcmso.Find(-1382830731);

            Ghe ghe = new Ghe();
            ghe.Find(-154428734);

            ImportaTodoPlanejamentoParaGhe(pcmso, ghe);
        }

        public static void ImportaTodoPlanejamentoParaGhe(Pcmso pcmso, Ghe ghe)
        {
            new PcmsoPlanejamento().Delete("IdGhe=" + ghe.Id);

            ArrayList listGhe = new Ghe().Find("nID_LAUD_TEC = " + pcmso.IdLaudoTecnico.Id
                                                + " AND nID_FUNC <>" + ghe.Id
                                                + " ORDER BY tNO_FUNC");

            foreach (Ghe gheDe in listGhe)
                ImportaPlanejamento(pcmso, gheDe, ghe);
        }
        #endregion

        #region ImportaPlanejamento

        private static void ImportaPlanejamento(Pcmso pcmso, Ghe gheDe, Ghe ghePara)
        {
            ArrayList listPcmsoPlan = new PcmsoPlanejamento().Find("IdPcmso=" + pcmso.Id
                                                                + " AND IdGhe = " + gheDe.Id);
            foreach (PcmsoPlanejamento pcmsoPlan in listPcmsoPlan)
                AddImportaPlanejamento(pcmso, ghePara, pcmsoPlan);
        }
        #endregion

        #region AddImportaPlanejamento

        public static void AddImportaPlanejamento(Pcmso pcmso,
                                                    Ghe ghePara,
                                                    PcmsoPlanejamento pcmsoPlan)
        {
            PcmsoPlanejamento pcmsoPlanNew = new PcmsoPlanejamento();
            pcmsoPlanNew.Find("IdPcmso=" + pcmso.Id
                                    + " AND IdGHE=" + ghePara.Id
                                    + " AND IdExameDicionario=" + pcmsoPlan.IdExameDicionario.Id);

            if (pcmsoPlanNew.Id == 0)
            {
                pcmsoPlanNew.Inicialize();
                pcmsoPlanNew.IdPcmso.Id = pcmso.Id;
                pcmsoPlanNew.IdGHE.Id = ghePara.Id;
                pcmsoPlanNew.IdExameDicionario.Id = pcmsoPlan.IdExameDicionario.Id;
            }
            pcmsoPlanNew.Preventivo = pcmsoPlan.Preventivo;

            IPeriodicidadeExame IPeriodicidadeExame = pcmsoPlan;
            IPeriodicidadeExame IPeriodicidadePcmso = pcmsoPlanNew;

            IPeriodicidadePcmso.AposAdmissao = IPeriodicidadeExame.AposAdmissao;
            IPeriodicidadePcmso.IndPeriodicidade = IPeriodicidadeExame.IndPeriodicidade;
            IPeriodicidadePcmso.Intervalo = IPeriodicidadeExame.Intervalo;
            IPeriodicidadePcmso.IntervaloAposAdmissao = IPeriodicidadeExame.IntervaloAposAdmissao;
            IPeriodicidadePcmso.IndPeriodicidadeAposAdmissao = IPeriodicidadeExame.IndPeriodicidadeAposAdmissao;
            IPeriodicidadePcmso.NaAdmissao = IPeriodicidadeExame.NaAdmissao;
            IPeriodicidadePcmso.NaDemissao = IPeriodicidadeExame.NaDemissao;
            IPeriodicidadePcmso.NoRetornoTrabalho = IPeriodicidadeExame.NoRetornoTrabalho;
            IPeriodicidadePcmso.NaMudancaFuncao = IPeriodicidadeExame.NaMudancaFuncao;
            IPeriodicidadePcmso.Periodico = IPeriodicidadeExame.Periodico;
            IPeriodicidadePcmso.DependeIdade = IPeriodicidadeExame.DependeIdade;

            pcmsoPlanNew.Save();

            new PcmsoPlanejamentoIdade().Delete("IdPcmsoPlanejamento=" + pcmsoPlanNew.Id);

            if (IPeriodicidadeExame.Periodico && IPeriodicidadeExame.DependeIdade)
            {
                ArrayList list = new PcmsoPlanejamentoIdade().Find("IdPcmsoPlanejamento=" + pcmsoPlan.Id);

                foreach (PcmsoPlanejamentoIdade pcmsoPlanejamentoIdade in list)
                {
                    PcmsoPlanejamentoIdade pcmsoPlanejamentoIdadeNew = new PcmsoPlanejamentoIdade();
                    pcmsoPlanejamentoIdadeNew.Inicialize();
                    pcmsoPlanejamentoIdadeNew.IdPcmsoPlanejamento = pcmsoPlanNew;
                    pcmsoPlanejamentoIdadeNew.IndPeriodicidade = pcmsoPlanejamentoIdade.IndPeriodicidade;
                    pcmsoPlanejamentoIdadeNew.Intervalo = pcmsoPlanejamentoIdade.Intervalo;
                    pcmsoPlanejamentoIdadeNew.IndSexoIdade = pcmsoPlanejamentoIdade.IndSexoIdade;
                    pcmsoPlanejamentoIdadeNew.AnoInicio = pcmsoPlanejamentoIdade.AnoInicio;
                    pcmsoPlanejamentoIdadeNew.AnoTermino = pcmsoPlanejamentoIdade.AnoTermino;
                    pcmsoPlanejamentoIdadeNew.Save();
                }
            }
        }
        #endregion

        #endregion

        #region GetPcmsoPlanejemanto

        public static PcmsoPlanejamento GetPcmsoPlanejemanto(Pcmso pcmso,
                                                    Ghe ghe,
                                                    ExameDicionario exameDicionario,
                                                    bool IsPreventivo)
        {
            PcmsoPlanejamento pcmsoPlanejamento = new PcmsoPlanejamento();
            pcmsoPlanejamento.Find("IdPcmso=" + pcmso.Id
                                    + " AND IdGHE=" + ghe.Id
                                    + " AND IdExameDicionario=" + exameDicionario.Id);

            if (pcmsoPlanejamento.Id == 0)
            {
                pcmsoPlanejamento.Inicialize();
                pcmsoPlanejamento.IdPcmso.Id = pcmso.Id;
                pcmsoPlanejamento.IdGHE.Id = ghe.Id;
                pcmsoPlanejamento.IdExameDicionario.Id = exameDicionario.Id;
            }

            pcmsoPlanejamento.Preventivo = IsPreventivo;

            return pcmsoPlanejamento;
        }

        #endregion

        #region Adiciona

        /*
        public static void AdicionaBak(Pcmso pcmso, 
                                    Ghe ghe, 
                                    ExameDicionario exameDicionario, 
                                    bool IsPreventivo)
        {
            PcmsoPlanejamento pcmsoPlanejamento = new PcmsoPlanejamento();
            pcmsoPlanejamento.Find("IdPcmso=" + pcmso.Id 
                                    + " AND IdGHE=" + ghe.Id 
                                    + " AND IdExameDicionario=" + exameDicionario.Id);

            if (pcmsoPlanejamento.Id == 0)
            {
                pcmsoPlanejamento.Inicialize();
                pcmsoPlanejamento.IdPcmso.Id = pcmso.Id;
                pcmsoPlanejamento.IdGHE.Id = ghe.Id;
                pcmsoPlanejamento.IdExameDicionario.Id = exameDicionario.Id;
            }
            pcmsoPlanejamento.Preventivo = IsPreventivo;

            IPeriodicidadeExame IPeriodicidadeExame = exameDicionario;
            IPeriodicidadeExame IPeriodicidadePcmso = pcmsoPlanejamento;

            IPeriodicidadePcmso.AposAdmissao = IPeriodicidadeExame.AposAdmissao;
            IPeriodicidadePcmso.IndPeriodicidade = IPeriodicidadeExame.IndPeriodicidade;
            IPeriodicidadePcmso.Intervalo = IPeriodicidadeExame.Intervalo;
            IPeriodicidadePcmso.IntervaloAposAdmissao = IPeriodicidadeExame.IntervaloAposAdmissao;
            IPeriodicidadePcmso.IndPeriodicidadeAposAdmissao = IPeriodicidadeExame.IndPeriodicidadeAposAdmissao;
            IPeriodicidadePcmso.NaAdmissao = IPeriodicidadeExame.NaAdmissao;
            IPeriodicidadePcmso.NaDemissao = IPeriodicidadeExame.NaDemissao;
            IPeriodicidadePcmso.NoRetornoTrabalho = IPeriodicidadeExame.NoRetornoTrabalho;
            IPeriodicidadePcmso.NaMudancaFuncao = IPeriodicidadeExame.NaMudancaFuncao;
            IPeriodicidadePcmso.Periodico = IPeriodicidadeExame.Periodico;
            IPeriodicidadePcmso.DependeIdade = IPeriodicidadeExame.DependeIdade;

            pcmsoPlanejamento.Save();

            new PcmsoPlanejamentoIdade().Delete("IdPcmsoPlanejamento=" + pcmsoPlanejamento.Id);

            if (IPeriodicidadeExame.Periodico && IPeriodicidadeExame.DependeIdade)
            {
                ArrayList list = new ExameDicionarioIdade().Find("IdExameDicionario=" + exameDicionario.Id);

                foreach (ExameDicionarioIdade exameDicionarioIdade in list)
                {
                    PcmsoPlanejamentoIdade pcmsoPlanejamentoIdade = new PcmsoPlanejamentoIdade();
                    pcmsoPlanejamentoIdade.Inicialize();
                    pcmsoPlanejamentoIdade.IdPcmsoPlanejamento = pcmsoPlanejamento;
                    pcmsoPlanejamentoIdade.IndPeriodicidade = exameDicionarioIdade.IndPeriodicidade;
                    pcmsoPlanejamentoIdade.Intervalo = exameDicionarioIdade.Intervalo;
                    pcmsoPlanejamentoIdade.IndSexoIdade = exameDicionarioIdade.IndSexoIdade;
                    pcmsoPlanejamentoIdade.AnoInicio = exameDicionarioIdade.AnoInicio;
                    pcmsoPlanejamentoIdade.AnoTermino = exameDicionarioIdade.AnoTermino;
                    pcmsoPlanejamentoIdade.Save();
                }
            }
        }
         * */

        public static void Adiciona(    Pcmso pcmso,
                                        Ghe ghe,
                                        ExameDicionario exameDicionario,
                                        bool IsPreventivo)
        {
            PcmsoPlanejamento pcmsoPlanejamento = GetPcmsoPlanejemanto(pcmso, ghe, exameDicionario, IsPreventivo);

            ArrayList list = new ExameDicionarioIdade().Find("IdExameDicionario=" + exameDicionario.Id);

            Adiciona(pcmsoPlanejamento, exameDicionario, list);
        }


        public static void Adiciona( PcmsoPlanejamento pcmsoPlanejamento,
                                     IPeriodicidadeExame IPeriodicidadeExame,
                                     ArrayList listPeriodicidadeIdade)
        {
            IPeriodicidadeExame IPeriodicidadePcmso = pcmsoPlanejamento;

            IPeriodicidadePcmso.AposAdmissao = IPeriodicidadeExame.AposAdmissao;
            IPeriodicidadePcmso.IndPeriodicidade = IPeriodicidadeExame.IndPeriodicidade;
            IPeriodicidadePcmso.Intervalo = IPeriodicidadeExame.Intervalo;
            IPeriodicidadePcmso.IntervaloAposAdmissao = IPeriodicidadeExame.IntervaloAposAdmissao;
            IPeriodicidadePcmso.IndPeriodicidadeAposAdmissao = IPeriodicidadeExame.IndPeriodicidadeAposAdmissao;
            IPeriodicidadePcmso.NaAdmissao = IPeriodicidadeExame.NaAdmissao;
            IPeriodicidadePcmso.NaDemissao = IPeriodicidadeExame.NaDemissao;
            IPeriodicidadePcmso.NoRetornoTrabalho = IPeriodicidadeExame.NoRetornoTrabalho;
            IPeriodicidadePcmso.NaMudancaFuncao = IPeriodicidadeExame.NaMudancaFuncao;
            IPeriodicidadePcmso.Periodico = IPeriodicidadeExame.Periodico;
            IPeriodicidadePcmso.DependeIdade = IPeriodicidadeExame.DependeIdade;

            pcmsoPlanejamento.Save();

            new PcmsoPlanejamentoIdade().Delete("IdPcmsoPlanejamento=" + pcmsoPlanejamento.Id);

            if (IPeriodicidadeExame.Periodico && IPeriodicidadeExame.DependeIdade)
            {
                foreach (IPeriodicidadeIdade periodicidadeIdade in listPeriodicidadeIdade)
                {
                    PcmsoPlanejamentoIdade pcmsoPlanejamentoIdade = new PcmsoPlanejamentoIdade();
                    pcmsoPlanejamentoIdade.Inicialize();
                    pcmsoPlanejamentoIdade.IdPcmsoPlanejamento = pcmsoPlanejamento;
                    pcmsoPlanejamentoIdade.IndPeriodicidade = periodicidadeIdade.IndPeriodicidade;
                    pcmsoPlanejamentoIdade.Intervalo = periodicidadeIdade.Intervalo;
                    pcmsoPlanejamentoIdade.IndSexoIdade = periodicidadeIdade.IndSexoIdade;
                    pcmsoPlanejamentoIdade.AnoInicio = periodicidadeIdade.AnoInicio;
                    pcmsoPlanejamentoIdade.AnoTermino = periodicidadeIdade.AnoTermino;
                    pcmsoPlanejamentoIdade.Save();
                }
            }
        }
        #endregion

        #endregion
    }
    #endregion
}
