using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;

using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Globalization;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceExameAsoPci : DataSourceBase
    {
        #region Eventos

        public override event EventProgress ProgressIniciar;
        public override event EventProgress ProgressAtualizar;
        public override event EventProgressFinalizar ProgressFinalizar;

        #endregion

        #region Variaveis

        private DataSet dsPrincipal = new DataSet();
        private DataSet dsExame = new DataSet();
        private DataSet dsPci = new DataSet();
        private DataSet dsAdendo = new DataSet();

        private bool PciComAso = false;
        private bool ExamesSomentePeriodoPcmso = false;
        private bool SemFoto = false;

        private ConvocacaoExame convocacao;
        private Clinico clinico;
        private Cliente cliente;
        private Pcmso pcmso;

        #endregion

        #region Construtor

        public DataSourceExameAsoPci(Clinico clinico)
        {
            InicializarTables();

            this.clinico = clinico;

            this.pcmso = clinico.IdPcmso;

            if (this.clinico.IdPcmso.Id != 0)
            {
                if (this.clinico.IdPcmso.IdCliente == null)
                    this.clinico.IdPcmso.Find();

                this.cliente = this.clinico.IdPcmso.IdCliente;
            }
            else
            {
                if (this.clinico.IdEmpregado.nID_EMPR == null)
                {
                    this.clinico.IdEmpregado.Find();
                    this.clinico.IdEmpregado.nID_EMPR.Find();
                }
                this.cliente = this.clinico.IdEmpregado.nID_EMPR;
            }
        }

        public DataSourceExameAsoPci(   ConvocacaoExame convocacao,   
                                        Pcmso pcmso, 
                                        bool ExamesSomentePeriodoPcmso, 
                                        bool SemFoto)
        {
            InicializarTables();

            this.convocacao = convocacao;
            this.ExamesSomentePeriodoPcmso = ExamesSomentePeriodoPcmso;
            this.SemFoto = SemFoto;

            this.pcmso = pcmso;
            this.cliente = this.pcmso.IdCliente;

            if (pcmso.Id == 0)
                throw new Exception("O PCMSO da empresa " + cliente.NomeAbreviado + " ainda não está configurado!");

            if (this.pcmso.mirrorOld == null)
                this.pcmso.Find();

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

        #endregion

        #region GetReport

        #region GetReportAso

        public RptAso GetReport()
        {
            RptAso report = new RptAso();

            if (clinico != null)
            {                
                DataSourcePrincipal(clinico);
                DataSourceAso(clinico);
            }
            else
                DataSourceAso();

            report.Subreports[0].SetDataSource(dsExame);
            report.Subreports[1].SetDataSource(dsExame);
            report.Subreports[2].SetDataSource(dsExame);
            report.Subreports[3].SetDataSource(dsExame);

            report.SetDataSource(dsPrincipal);
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

        public RptAso_Global GetReport_Global()
        {
            RptAso_Global report = new RptAso_Global();

            if (clinico != null)
            {
                DataSourcePrincipal(clinico);
                DataSourceAso(clinico);
            }
            else
                DataSourceAso();

            report.Subreports[0].SetDataSource(dsExame);
            report.Subreports[1].SetDataSource(dsExame);
            report.Subreports[2].SetDataSource(dsExame);
            report.Subreports[3].SetDataSource(dsExame);

            report.SetDataSource(dsPrincipal);
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

        #endregion

        #region GetReportAsoModeloPagina

        //public RptAsoModPagina GetReportAsoModeloPagina()
        //{
        //    RptAsoModPagina report = new RptAsoModPagina();

        //    if (clinico != null)
        //    {
        //        DataSourceAso(clinico);
        //        DataSourcePrincipal(clinico);
        //    }
        //    else
        //    {
        //        List<Clinico> clinicos = convocacao.GetExames(pcmso, ConvocacaoExame.ExamesSelecionados.NaoRealizados);

        //        foreach (Clinico cl in clinicos)
        //        {
        //            DataSourceAso(cl);
        //            DataSourcePrincipal(cl);
        //        }
        //    }
        //    report.Subreports[0].SetDataSource(dsExame);
        //    report.Subreports[1].SetDataSource(dsExame);

        //    report.SetDataSource(dsPrincipal);
        //    report.Refresh();

        //    SetTempoProcessamento(report);

        //    return report;
        //}
        #endregion

        #region GetReportAsoMod2

        //public RptAsoModFast GetReportAsoMod2()
        //{
        //    RptAsoModFast report = new RptAsoModFast();

        //    if (clinico != null)
        //        DataSourceAso(clinico);

        //    report.Subreports[0].SetDataSource(dsExame);
        //    report.Subreports[1].SetDataSource(dsExame);
        //    report.SetDataSource(DataSourcePrincipal(clinico));
        //    report.Refresh();

        //    SetTempoProcessamento(report);

        //    return report;
        //}
        #endregion

        #region GetReportPci

        public RptPci_Novo GetReportPci()
        {
            return GetReportPci(false);
        }

        public RptPci_Novo_Global GetReportPci_Global()
        {
            return GetReportPci_Global(false);
        }


        public RptPci_NovoEY GetReportPci_EY()
        {
            return GetReportPci_EY(false);
        }


        public RptPci_Novo_Capgemini GetReportPciCapgemini()
        {
            return GetReportPciCapgemini(false);
        }

        public RptPci_Novo_EY2 GetReportPciEY2()
        {
            return GetReportPciEY2(false);
        }


        public RptPci_Novo GetReportPci(bool PciComAso)
        {
            this.PciComAso = PciComAso;

            dsAdendo.Tables.Add(GetDataTableAdendo());

            if (clinico != null)
            {
                DataSourceAso(clinico);
                DataSourcePci(clinico);
                DataSourceAdendo(clinico);
            }
            else
                DataSourceAso(true);

            RptPci_Novo reportPci = new RptPci_Novo();

            if (PciComAso)
            {
                reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsExame);
                reportPci.OpenSubreport("RptAsoViaEmpregado.rpt").SetDataSource(dsExame);
            }
            reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
            reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);
            reportPci.SetDataSource(dsPci);
            reportPci.Refresh();

            SetTempoProcessamento(reportPci);

            return reportPci;
        }

        public RptPci_Novo_Global GetReportPci_Global(bool PciComAso)
        {
            this.PciComAso = PciComAso;

            dsAdendo.Tables.Add(GetDataTableAdendo());

            if (clinico != null)
            {
                DataSourceAso(clinico);
                DataSourcePci(clinico);
                DataSourceAdendo(clinico);
            }
            else
                DataSourceAso(true);

            RptPci_Novo_Global reportPci = new RptPci_Novo_Global();

            if (PciComAso)
            {
                reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsExame);
                reportPci.OpenSubreport("RptAsoViaEmpregado.rpt").SetDataSource(dsExame);
            }
            reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
            reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);
            reportPci.SetDataSource(dsPci);
            reportPci.Refresh();

            SetTempoProcessamento(reportPci);

            return reportPci;
        }


        public RptPci_NovoEY GetReportPci_EY(bool PciComAso)
        {
            this.PciComAso = PciComAso;

            dsAdendo.Tables.Add(GetDataTableAdendo());

            if (clinico != null)
            {
                DataSourceAso(clinico);
                DataSourcePci(clinico);
                DataSourceAdendo(clinico);
            }
            else
                DataSourceAso(true);

            RptPci_NovoEY reportPci = new RptPci_NovoEY();

            if (PciComAso)
            {
                reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsExame);
                reportPci.OpenSubreport("RptAsoViaEmpregado.rpt").SetDataSource(dsExame);
            }
            reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
            reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);
            reportPci.SetDataSource(dsPci);
            reportPci.Refresh();

            SetTempoProcessamento(reportPci);

            return reportPci;
        }


        public RptPci_Novo_Capgemini GetReportPciCapgemini(bool PciComAso)
        {
            this.PciComAso = PciComAso;

            dsAdendo.Tables.Add(GetDataTableAdendo());

            if (clinico != null)
            {
                DataSourceAso(clinico);
                DataSourcePci(clinico);
                DataSourceAdendo(clinico);
            }
            else
                DataSourceAso(true);

            RptPci_Novo_Capgemini reportPci = new RptPci_Novo_Capgemini();

            if (PciComAso)
            {
                reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsExame);
                reportPci.OpenSubreport("RptAsoViaEmpregado.rpt").SetDataSource(dsExame);
            }
            reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
            reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);
            reportPci.SetDataSource(dsPci);
            reportPci.Refresh();

            SetTempoProcessamento(reportPci);

            return reportPci;
        }


        public RptPci_Novo_EY2 GetReportPciEY2(bool PciComAso)
        {
            this.PciComAso = PciComAso;

            dsAdendo.Tables.Add(GetDataTableAdendo());

            if (clinico != null)
            {
                DataSourceAso(clinico);
                DataSourcePci(clinico);
                DataSourceAdendo(clinico);
            }
            else
                DataSourceAso(true);

            RptPci_Novo_EY2 reportPci = new RptPci_Novo_EY2();

            if (PciComAso)
            {
                reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsExame);
                reportPci.OpenSubreport("RptAsoViaEmpregado.rpt").SetDataSource(dsExame);
            }
            reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
            reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);
            reportPci.SetDataSource(dsPci);
            reportPci.Refresh();

            SetTempoProcessamento(reportPci);

            return reportPci;
        }




        public RptPci GetReportPci_Antigo()
        {
            return GetReportPci_Antigo(false);
        }

        public RptPci GetReportPci_Antigo(bool PciComAso)
        {
            this.PciComAso = PciComAso;

            dsAdendo.Tables.Add(GetDataTableAdendo());

            if (clinico != null)
            {
                DataSourceAso(clinico);
                DataSourcePci(clinico);
                DataSourceAdendo(clinico);
            }
            else
                DataSourceAso(true);

            RptPci reportPci = new RptPci();

            if (PciComAso)
            {
                reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsExame);
                reportPci.OpenSubreport("RptAsoViaEmpregado.rpt").SetDataSource(dsExame);
            }
            reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
            reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);
            reportPci.SetDataSource(dsPci);
            reportPci.Refresh();

            SetTempoProcessamento(reportPci);

            return reportPci;
        }

        #endregion

        #region GetReportPciModPagina

        public RptAsoPciModPagina GetReportPciModPagina(bool PciComAso)
        {
            this.PciComAso = PciComAso;

            dsAdendo.Tables.Add(GetDataTableAdendo());

            if (clinico != null)
            {
                DataSourceAso(clinico);
                DataSourcePci(clinico);
                DataSourceAdendo(clinico);
            }
            else
            {
                List<Clinico> clinicos = convocacao.GetExames(pcmso, ConvocacaoExame.ExamesSelecionados.NaoRealizados);

                foreach (Clinico clinico2 in clinicos)
                {
                    try
                    {
                        DataSourceAso(clinico2);
                        DataSourcePci(clinico2);
                        DataSourceAdendo(clinico2);
                    }
                    catch { }
                }
            }

            RptAsoPciModPagina reportPci = new RptAsoPciModPagina();

            if (PciComAso)
            {
                reportPci.OpenSubreport("RptAsoModPaginaViaEmpregador").SetDataSource(dsExame);
                reportPci.OpenSubreport("RptAsoModPaginaViaEmpregado").SetDataSource(dsExame);
            }
            reportPci.OpenSubreport("RptAsoViaPciModPagina").SetDataSource(dsExame);
            reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);

            reportPci.SetDataSource(dsPci);
            reportPci.Refresh();

            SetTempoProcessamento(reportPci);

            return reportPci;
        }
        #endregion

        #endregion

        #region DataTables

        #region InicializarTables

        private void InicializarTables()
        {
            dsPrincipal.Tables.Add(GetDataTablePrincipal());
            dsExame.Tables.Add(GetDataTableReportAso());
            dsPci.Tables.Add(GetDataTableReportPci());
        }
        #endregion

        #region GetDataTablePrincipal

        private static DataTable GetDataTablePrincipal()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdExame", Type.GetType("System.Int32"));
            return table;
        }
        #endregion

        #region GetDataTableAdendo

        public static DataTable GetDataTableAdendo()
        {
            DataTable table = new DataTable("ResultAdendo");

            table.Columns.Add("IdExameBase", Type.GetType("System.Int32"));
            table.Columns.Add("Data", Type.GetType("System.String"));
            table.Columns.Add("Medico", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            return table;
        }
        #endregion

        #region GetDataTableReportAso

        public static DataTable GetDataTableReportAso()
        {
            DataTable table = new DataTable("ResultAso");
            //Empregado
            table.Columns.Add("IdExame", Type.GetType("System.Int32"));
            table.Columns.Add("tNO_EMPG", Type.GetType("System.String"));
            table.Columns.Add("tNO_IDENTIDADE", Type.GetType("System.String"));
            table.Columns.Add("nSEXO", Type.GetType("System.String"));
            table.Columns.Add("tNO_STR_EMPR", Type.GetType("System.String"));
            table.Columns.Add("tNO_FUNC_EMPR", Type.GetType("System.String"));
            table.Columns.Add("tNO_GHE", Type.GetType("System.String"));
            table.Columns.Add("RiscosAmbientais", Type.GetType("System.String"));
            table.Columns.Add("RiscosOcupacionais", Type.GetType("System.String"));
            table.Columns.Add("ExamesComplementares", Type.GetType("System.String"));
            table.Columns.Add("iFOTO", Type.GetType("System.String"));

            //Empresa
            table.Columns.Add("RazaoSocial", Type.GetType("System.String"));
            table.Columns.Add("Endereco", Type.GetType("System.String"));
            table.Columns.Add("Cidade", Type.GetType("System.String"));
            table.Columns.Add("Estado", Type.GetType("System.String"));
            table.Columns.Add("CEP", Type.GetType("System.String"));
            table.Columns.Add("CGC", Type.GetType("System.String"));

            //Exame
            table.Columns.Add("tDS_EXAME_TIPO", Type.GetType("System.String"));
            table.Columns.Add("tNOME_MEDICO", Type.GetType("System.String"));
            table.Columns.Add("tTITULO_MEDICO", Type.GetType("System.String"));
            table.Columns.Add("tCONTATO_MEDICO", Type.GetType("System.String"));
            table.Columns.Add("dDT_EXAME", Type.GetType("System.String"));
            table.Columns.Add("tMEDICO_COORD_PCMSO", Type.GetType("System.String"));
            table.Columns.Add("tVIA", Type.GetType("System.String"));
            table.Columns.Add("tX_APTO", Type.GetType("System.String"));
            table.Columns.Add("tX_INAPTO", Type.GetType("System.String"));
            table.Columns.Add("tX_Espera", Type.GetType("System.String"));
            table.Columns.Add("ObservacaoResultado", Type.GetType("System.String"));
            table.Columns.Add("nRS_Exame", Type.GetType("System.String"));
            table.Columns.Add("ClinicaCidade", Type.GetType("System.String"));

            return table;
        }
        #endregion


        #region GetDataTableReportPci

        public static DataTable GetDataTableReportPci()
        {
            DataTable table = new DataTable("ResultPci");

            //ExameFisico
            table.Columns.Add("IdExame", Type.GetType("System.Int32"));
            table.Columns.Add("hasAso", Type.GetType("System.Boolean"));
            table.Columns.Add("hasAdendo", Type.GetType("System.Boolean"));
            table.Columns.Add("PressaoArterial", Type.GetType("System.String"));
            table.Columns.Add("Pulso", Type.GetType("System.String"));
            table.Columns.Add("Altura", Type.GetType("System.String"));
            table.Columns.Add("Peso", Type.GetType("System.String"));
            table.Columns.Add("nRS_Exame", Type.GetType("System.String"));
            table.Columns.Add("DataUltimaMenstruacao", Type.GetType("System.String"));
            table.Columns.Add("hasCabecaAlterado", Type.GetType("System.String"));
            table.Columns.Add("hasCoracaoAlterado", Type.GetType("System.String"));
            table.Columns.Add("hasPulmaoAlterado", Type.GetType("System.String"));
            table.Columns.Add("hasMIAlterado", Type.GetType("System.String"));
            table.Columns.Add("hasPeleAlterado", Type.GetType("System.String"));
            table.Columns.Add("hasMSAlterado", Type.GetType("System.String"));
            table.Columns.Add("hasAbdomemAlterado", Type.GetType("System.String"));
            table.Columns.Add("hasOsteoAlterado", Type.GetType("System.String"));

            table.Columns.Add("AP_Penultima_Empresa", Type.GetType("System.String"));
            table.Columns.Add("AP_Penultima_Funcao", Type.GetType("System.String"));
            table.Columns.Add("AP_Penultima_Tempo", Type.GetType("System.String"));
            table.Columns.Add("AP_Ultima_Empresa", Type.GetType("System.String"));
            table.Columns.Add("AP_Ultima_Funcao", Type.GetType("System.String"));
            table.Columns.Add("AP_Ultima_Tempo", Type.GetType("System.String"));



            //Anamnese
            table.Columns.Add("HasQueixasAtuais", Type.GetType("System.String"));
            table.Columns.Add("HasAfastamento", Type.GetType("System.String"));
            table.Columns.Add("HasTraumatismos", Type.GetType("System.String"));
            table.Columns.Add("HasCirurgia", Type.GetType("System.String"));
            table.Columns.Add("HasMedicacoes", Type.GetType("System.String"));
            table.Columns.Add("HasAntecedentes", Type.GetType("System.String"));
            table.Columns.Add("HasTabagismo", Type.GetType("System.String"));
            table.Columns.Add("HasAlcoolismo", Type.GetType("System.String"));
            table.Columns.Add("HasDeficienciaFisica", Type.GetType("System.String"));
            table.Columns.Add("HasDoencaCronica", Type.GetType("System.String"));

            table.Columns.Add("HasBronquite", Type.GetType("System.String"));
            table.Columns.Add("HasDigestiva", Type.GetType("System.String"));
            table.Columns.Add("HasEstomago", Type.GetType("System.String"));
            table.Columns.Add("HasEnxerga", Type.GetType("System.String"));
            table.Columns.Add("HasDorCabeca", Type.GetType("System.String"));
            table.Columns.Add("HasDesmaio", Type.GetType("System.String"));
            table.Columns.Add("HasCoracao", Type.GetType("System.String"));
            table.Columns.Add("HasUrinaria", Type.GetType("System.String"));
            table.Columns.Add("HasDiabetes", Type.GetType("System.String"));
            table.Columns.Add("HasGripado", Type.GetType("System.String"));
            table.Columns.Add("HasEscuta", Type.GetType("System.String"));
            table.Columns.Add("HasDoresCosta", Type.GetType("System.String"));
            table.Columns.Add("HasReumatismo", Type.GetType("System.String"));
            table.Columns.Add("HasAlergia", Type.GetType("System.String"));
            table.Columns.Add("Hasesporte", Type.GetType("System.String"));
            table.Columns.Add("HasAcidentou", Type.GetType("System.String"));
            table.Columns.Add("Has_AF_Hipertensao", Type.GetType("System.String"));
            table.Columns.Add("Has_AF_Diabetes", Type.GetType("System.String"));
            table.Columns.Add("Has_AF_Coracao", Type.GetType("System.String"));
            table.Columns.Add("Has_AF_Derrames", Type.GetType("System.String"));
            table.Columns.Add("Has_AF_Obesidade", Type.GetType("System.String"));
            table.Columns.Add("Has_AF_Cancer", Type.GetType("System.String"));
            table.Columns.Add("Has_AF_Colesterol", Type.GetType("System.String"));
            table.Columns.Add("Has_AF_Psiquiatricos", Type.GetType("System.String"));

            table.Columns.Add("Coord_PCMSO", Type.GetType("System.String"));
            table.Columns.Add("Franquia", Type.GetType("System.String"));

            table.Columns.Add("tCONTATO_MEDICO", Type.GetType("System.String"));
            table.Columns.Add("tMEDICO_COORD_PCMSO", Type.GetType("System.String"));

            table.Columns.Add("Observacao", Type.GetType("System.String"));

            //Exame
            table.Columns.Add("dDT_EXAME", Type.GetType("System.String"));
            table.Columns.Add("nRS_EXAME", Type.GetType("System.String"));
            table.Columns.Add("tNOME_MEDICO", Type.GetType("System.String"));
            table.Columns.Add("tTITULO_MEDICO", Type.GetType("System.String"));
            table.Columns.Add("Cidade", Type.GetType("System.String"));


            table.Columns.Add("CircunferenciaAbdominal", Type.GetType("System.String"));
            table.Columns.Add("ColunaVertebralAlterado", Type.GetType("System.String"));
            table.Columns.Add("MVsemRAAlterado", Type.GetType("System.String"));
            table.Columns.Add("BRNFAlterado", Type.GetType("System.String"));
            table.Columns.Add("PlanoAlterado", Type.GetType("System.String"));
            table.Columns.Add("DBAlterado", Type.GetType("System.String"));
            table.Columns.Add("RHAAlterado", Type.GetType("System.String"));
            table.Columns.Add("IndolorAlterado", Type.GetType("System.String"));
            table.Columns.Add("AbaulamentosAlterado", Type.GetType("System.String"));

            table.Columns.Add("PeleNormal", Type.GetType("System.String"));
            table.Columns.Add("PeleDescoradas", Type.GetType("System.String"));
            table.Columns.Add("PeleIctericas", Type.GetType("System.String"));
            table.Columns.Add("PeleCianoticas", Type.GetType("System.String"));
            table.Columns.Add("PeleDermatoses", Type.GetType("System.String"));

            table.Columns.Add("ColunaNormal", Type.GetType("System.String"));
            table.Columns.Add("ColunaCifose", Type.GetType("System.String"));
            table.Columns.Add("ColunaLordose", Type.GetType("System.String"));
            table.Columns.Add("ColunaEscoliose", Type.GetType("System.String"));
            table.Columns.Add("ColunaLasegue", Type.GetType("System.String"));
            table.Columns.Add("ColunaFlexao", Type.GetType("System.String"));

            table.Columns.Add("MINormal", Type.GetType("System.String"));
            table.Columns.Add("MIEdemas", Type.GetType("System.String"));
            table.Columns.Add("MIPulsos", Type.GetType("System.String"));
            table.Columns.Add("MILimitacao", Type.GetType("System.String"));
            table.Columns.Add("MIVarizes", Type.GetType("System.String"));

            table.Columns.Add("CardioBRNF", Type.GetType("System.String"));
            table.Columns.Add("CardioArritmias", Type.GetType("System.String"));
            table.Columns.Add("CardioSopros", Type.GetType("System.String"));
            table.Columns.Add("CardioOutros", Type.GetType("System.String"));

            table.Columns.Add("OmbrosNormal", Type.GetType("System.String"));
            table.Columns.Add("OmbrosRotacao", Type.GetType("System.String"));
            table.Columns.Add("OmbrosBiceps", Type.GetType("System.String"));
            table.Columns.Add("OmbrosSubacromial", Type.GetType("System.String"));

            table.Columns.Add("CabecaNormal", Type.GetType("System.String"));
            table.Columns.Add("CabecaTireoide", Type.GetType("System.String"));
            table.Columns.Add("CabecaDente", Type.GetType("System.String"));
            table.Columns.Add("CabecaGarganta", Type.GetType("System.String"));
            table.Columns.Add("CabecaAdenomegalia", Type.GetType("System.String"));
            table.Columns.Add("CabecaOuvido", Type.GetType("System.String"));

            table.Columns.Add("AbdomePlano", Type.GetType("System.String"));
            table.Columns.Add("AbdomePalpacao", Type.GetType("System.String"));
            table.Columns.Add("AbdomeHepato", Type.GetType("System.String"));
            table.Columns.Add("AbdomeMassas", Type.GetType("System.String"));

            table.Columns.Add("OlhosNormal", Type.GetType("System.String"));
            table.Columns.Add("OlhosOculos", Type.GetType("System.String"));

            table.Columns.Add("RespiratorioMVsemRA", Type.GetType("System.String"));
            table.Columns.Add("RespiratorioMVAlterado", Type.GetType("System.String"));
            table.Columns.Add("RespiratorioRuidos", Type.GetType("System.String"));

            table.Columns.Add("MaosNormal", Type.GetType("System.String"));

            table.Columns.Add("TinelD", Type.GetType("System.String"));
            table.Columns.Add("TinelE", Type.GetType("System.String"));
            table.Columns.Add("FinklsteinD", Type.GetType("System.String"));
            table.Columns.Add("FinklsteinE", Type.GetType("System.String"));
            table.Columns.Add("NodulosD", Type.GetType("System.String"));
            table.Columns.Add("NodulosE", Type.GetType("System.String"));
            table.Columns.Add("EdemaD", Type.GetType("System.String"));
            table.Columns.Add("EdemaE", Type.GetType("System.String"));
            table.Columns.Add("PrensaoD", Type.GetType("System.String"));
            table.Columns.Add("PrensaoE", Type.GetType("System.String"));
            table.Columns.Add("PhalenD", Type.GetType("System.String"));
            table.Columns.Add("PhalenE", Type.GetType("System.String"));
            table.Columns.Add("CrepitacaoD", Type.GetType("System.String"));
            table.Columns.Add("CrepitacaoE", Type.GetType("System.String"));

            table.Columns.Add("Has_Otologica_Obstrucao", Type.GetType("System.String"));
            table.Columns.Add("Has_Otologica_Cerumen", Type.GetType("System.String"));
            table.Columns.Add("Has_Otologica_Alteracao", Type.GetType("System.String"));


            return table;
        }
        #endregion

        #endregion

        #region DataSources

        #region DataSourcePrincipal

        public DataSet DataSourcePrincipal(Clinico clinico)
        {
            DataRow newRow = dsPrincipal.Tables[0].NewRow();
            newRow["IdExame"] = clinico.Id;
            dsPrincipal.Tables[0].Rows.Add(newRow);
            return dsPrincipal;
        }
        #endregion

        #region DataSourceAso

        private void DataSourceAso()
        {
            DataSourceAso(false);
        }

        private void DataSourceAso(bool ComPci)
        {
            List<Clinico> clinicos = convocacao.GetExames(pcmso, ConvocacaoExame.ExamesSelecionados.NaoRealizados);

            if (ProgressIniciar != null)
                ProgressIniciar(clinicos.Count);

            int OrdinalPosition = 1;

            List<Ghe> ghes = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + pcmso.IdLaudoTecnico.Id);

            string sNomeCoordenador = GetCoordenadorPCMSO(cliente, pcmso);

            foreach (Clinico clinico in clinicos)
            {
                if (ProgressAtualizar != null)
                    ProgressAtualizar(OrdinalPosition++);


                
                DataSourcePrincipal(clinico);

                clinico.ValidarDadosAso();

                DataRow newRow = dsExame.Tables[0].NewRow();

                PopularRow pRow = new PopularRow(newRow);

                pRow.ExameEmpregado(clinico);
                pRow.AvaliacaoAmbiental(clinico, pcmso, ghes, ExamesSomentePeriodoPcmso);
                pRow.Foto(clinico, cliente, SemFoto);
                pRow.DataNascimentoIdentidade(clinico);
                pRow.Resultado(clinico);
                pRow.ExameTipo(convocacao);
                pRow.Juridica(convocacao);
                pRow.Medico(convocacao);
                pRow.ContatoMedico(convocacao);
                pRow.CoordenadorPcmso(sNomeCoordenador);

                if (ComPci)
                {
                    DataSourcePci(clinico);
                    DataSourceAdendo(clinico);
                }

                dsExame.Tables[0].Rows.Add(newRow);
            }

            if (ProgressFinalizar != null)
                ProgressFinalizar();
        }

        private void DataSourceAso(Clinico clinico)
        {
            clinico.ValidarDadosAso();

            DataRow newRow = dsExame.Tables[0].NewRow();

            PopularRow pRow = new PopularRow(newRow);

            pRow.ExameEmpregado(clinico);
            if (pcmso.IdLaudoTecnico != null)
            {
                pRow.AvaliacaoAmbiental(clinico, pcmso, null, ExamesSomentePeriodoPcmso);
            }
            pRow.ExameTipo(clinico);
            pRow.Foto(clinico, cliente, SemFoto);
            pRow.DataNascimentoIdentidade(clinico);
            pRow.Juridica(clinico);
            pRow.Medico(clinico);
            pRow.ContatoMedico(clinico);
            pRow.Resultado(clinico);
            pRow.CoordenadorPcmso(GetCoordenadorPCMSO(cliente, pcmso));

            dsExame.Tables[0].Rows.Add(newRow);
        }

        #region InnerClass PopularRow

        private class PopularRow
        {
            private DataRow newRow;

            public PopularRow(DataRow newRow)
            {
                this.newRow = newRow;
            }

            #region ExameEmpregado

            public void ExameEmpregado(Clinico clinico)
            {
                newRow["IdExame"] = clinico.Id;
                newRow["tNO_EMPG"] = clinico.IdEmpregado.GetNomeEmpregadoComRE();
                newRow["nSEXO"] = "Sexo: " + clinico.IdEmpregado.tSEXO;
                newRow["tNO_STR_EMPR"] = "Setor de " + clinico.IdEmpregadoFuncao.GetNomeSetor() + "       CPF " + clinico.IdEmpregado.tNO_CPF;

                clinico.IdEmpregadoFuncao.nID_EMPR.Find();

                if ( clinico.IdEmpregadoFuncao.nID_EMPR.NomeAbreviado.ToUpper().IndexOf("DRIVEWAY") >= 0 )
                {
                    string zCargo = "";

                    clinico.IdEmpregadoFuncao.nID_CARGO.Find();

                    if (clinico.IdEmpregadoFuncao.nID_CARGO != null)
                    {
                        if (clinico.IdEmpregadoFuncao.nID_CARGO.tNO_CARGO.Trim() != "")
                        {
                            zCargo = clinico.IdEmpregadoFuncao.nID_CARGO.tNO_CARGO.Trim();
                        }
                    }

                    if ( zCargo != "")
                    {
                        //newRow["tNO_FUNC_EMPR"] = "Função: " + clinico.IdEmpregadoFuncao.GetNomeFuncao() + "   Cargo: " + zCargo;
                        newRow["tNO_FUNC_EMPR"] = "Cargo: " + zCargo;
                    }
                    else
                    {
                        newRow["tNO_FUNC_EMPR"] = "Cargo: " + clinico.IdEmpregadoFuncao.GetNomeFuncao();
                    }
                }
                else
                {
                    newRow["tNO_FUNC_EMPR"] = "Função de " + clinico.IdEmpregadoFuncao.GetNomeFuncao();
                }

                


                newRow["dDT_EXAME"] = clinico.DataExame.ToString("dd-MM-yyyy") + ".";
                newRow["nRS_Exame"] = clinico.IndResultado.ToString();

                string xAptidao = "";
                
                Empregado_Aptidao rAptidao = new Empregado_Aptidao();
                clinico.IdEmpregado.Find();
                rAptidao.Find(" nId_Empregado = " + clinico.IdEmpregado.Id.ToString());

                Int32 zIdGHE = clinico.IdEmpregadoFuncao.GetIdGheEmpregado(clinico.IdPcmso.IdLaudoTecnico);
                GHE_Aptidao zAptidao = new GHE_Aptidao();
                zAptidao.Find("nId_Func = " + zIdGHE.ToString());


                if (rAptidao.Id != 0 || zAptidao.Id != 0)
                {

                    clinico.IdPcmso.Find();
                    clinico.IdPcmso.IdCliente.Find();
                    Cliente zCliente = new Cliente();
                    zCliente.Find(clinico.IdPcmso.IdCliente.Id);


                    if (zCliente.Riscos_PPRA == false)
                    {
                        xAptidao = "Aptidão para: ";


                        if (rAptidao.apt_Alimento == true || zAptidao.apt_Alimento == true)
                            xAptidao = xAptidao + " manipular alimentos (item 4.6.1 da Resolução RDC nº216/2004) /";

                        if (rAptidao.apt_Aquaviario == true || zAptidao.apt_Aquaviario == true)
                            xAptidao = xAptidao + " serviços aquaviários (NR 30) /";

                        if (rAptidao.apt_Eletricidade == true || zAptidao.apt_Eletricidade == true)
                            xAptidao = xAptidao + " serviços em eletricidade (NR 10) /";

                        if (rAptidao.apt_Espaco_Confinado == true || zAptidao.apt_Espaco_Confinado == true)
                            xAptidao = xAptidao + " trabalho em espaços confinados (NR 33) /";

                        if (rAptidao.apt_Submerso == true || zAptidao.apt_Submerso == true)
                            xAptidao = xAptidao + " atividades submersas (NR 15) /";

                        if (rAptidao.apt_Trabalho_Altura == true || zAptidao.apt_Trabalho_Altura == true)
                            xAptidao = xAptidao + " trabalho em altura (NR 35) /";

                        if (rAptidao.apt_Transporte == true || zAptidao.apt_Transporte == true)
                            xAptidao = xAptidao + " operar equipamentos de transporte motorizados (NR 11) /";

                        if (rAptidao.apt_Brigadista == true || zAptidao.apt_Brigadista == true)
                            xAptidao = xAptidao + " trabalho como Brigadista /";

                        if (rAptidao.apt_PPR == true || zAptidao.apt_PPR == true)
                            xAptidao = xAptidao + " colaborador em PPR /";

                        if (rAptidao.apt_Radiacao == true || zAptidao.apt_Radiacao == true)
                            xAptidao = xAptidao + " radiação ionizante /";


                        if (rAptidao.apt_Socorrista == true || zAptidao.apt_Socorrista == true)
                            xAptidao = xAptidao + " trabalho como Socorrista /";

                        if (rAptidao.apt_Respirador == true )
                            xAptidao = xAptidao + " uso de Respiradores /";


                        if (xAptidao == "Aptidão para: ") xAptidao = "";
                        else xAptidao = xAptidao.Substring(0, xAptidao.Length - 1);

                    }
                    else
                    {

                        Int16 zPar = 0;
                        
                        if (rAptidao.apt_Alimento == true || zAptidao.apt_Alimento == true )
                        {
                            zPar++;
                            if ( clinico.apt_Alimento2 == null || clinico.apt_Alimento2.ToString().Trim()=="" || clinico.apt_Alimento2.ToString().Trim() == "False" || clinico.apt_Alimento2.ToString().Trim() == "0")
                               xAptidao = xAptidao + System.Environment.NewLine + " Manipular alimentos           ( )Apto ( )Inapto ";
                            else if ( clinico.apt_Alimento2 == "1")
                               xAptidao = xAptidao + System.Environment.NewLine + " Manipular alimentos           (X)Apto ( )Inapto ";
                            else if (clinico.apt_Alimento2 == "0")
                               xAptidao = xAptidao + System.Environment.NewLine + " Manipular alimentos           ( )Apto (X)Inapto ";
                        }

                        if (rAptidao.apt_Aquaviario == true || zAptidao.apt_Aquaviario==true)
                        {
                            zPar++;
                            if (clinico.apt_Aquaviario2 == null || clinico.apt_Aquaviario2.ToString().Trim() == "" ||  clinico.apt_Aquaviario2.ToString().Trim() == "False" || clinico.apt_Aquaviario2.ToString().Trim() == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Serviços aquaviários          ( )Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Serviços aquaviários         ( )Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Aquaviario2 == "1"  )
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Serviços aquaviários          (X)Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Serviços aquaviários         (X)Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Aquaviario2 == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Serviços aquaviários          ( )Apto (X)Inapto ";
                                else
                                    xAptidao = xAptidao + " Serviços aquaviários         ( )Apto (X)Inapto ";
                            }



                        }


                        if (rAptidao.apt_Eletricidade == true || zAptidao.apt_Eletricidade == true)
                        {
                            zPar++;
                            if (clinico.apt_Eletricidade2 == null || clinico.apt_Eletricidade2.ToString().Trim() == "" || clinico.apt_Eletricidade2.ToString().Trim() == "False" || clinico.apt_Eletricidade2.ToString().Trim() == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Serviços em eletricidade      ( )Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Serviços em eletricidade     ( )Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Eletricidade2 == "1")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Serviços em eletricidade      (X)Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Serviços em eletricidade     (X)Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Eletricidade2 == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Serviços em eletricidade      ( )Apto (X)Inapto ";
                                else
                                    xAptidao = xAptidao + " Serviços em eletricidade     ( )Apto (X)Inapto ";
                            }

                        }

                        if (rAptidao.apt_Espaco_Confinado == true || zAptidao.apt_Espaco_Confinado == true )
                        {
                            zPar++;
                            if (clinico.apt_Espaco_Confinado2 == null || clinico.apt_Espaco_Confinado2.ToString().Trim() == "" || clinico.apt_Espaco_Confinado2.ToString().Trim() == "False" || clinico.apt_Espaco_Confinado2.ToString().Trim() == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Espaços confinados            ( )Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Espaços confinados           ( )Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Espaco_Confinado2 == "1" )
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Espaços confinados            (X)Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Espaços confinados           (X)Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Espaco_Confinado2 == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Espaços confinados            ( )Apto (X)Inapto ";
                                else
                                    xAptidao = xAptidao + " Espaços confinados           ( )Apto (X)Inapto ";
                            }
                        }

                        if (rAptidao.apt_Submerso == true || zAptidao.apt_Submerso == true)
                        {
                            zPar++;
                            if (clinico.apt_Submerso2 == null || clinico.apt_Submerso2.ToString().Trim() == "" || clinico.apt_Submerso2.ToString().Trim() == "False" || clinico.apt_Submerso2.ToString().Trim() == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Atividades submersas          ( )Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Atividades submersas          ( )Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Submerso2 == "1")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Atividades submersas          (X)Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Atividades submersas          (X)Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Submerso2 == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Atividades submersas          ( )Apto (X)Inapto ";
                                else
                                    xAptidao = xAptidao + " Atividades submersas          ( )Apto (X)Inapto ";
                            }
                        }

                        if (rAptidao.apt_Radiacao == true || zAptidao.apt_Radiacao == true)
                        {
                            zPar++;
                            if (clinico.apt_Radiacao2 == null || clinico.apt_Radiacao2.ToString().Trim() == "" || clinico.apt_Radiacao2.ToString().Trim() == "False" || clinico.apt_Radiacao2.ToString().Trim() == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Radiação Ionizante            ( )Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Radiação Ionizante            ( )Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Radiacao2 == "1")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Radiação Ionizante            (X)Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Radiação Ionizante            (X)Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Radiacao2 == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Radiação Ionizante            ( )Apto (X)Inapto ";
                                else
                                    xAptidao = xAptidao + " Radiação Ionizante            ( )Apto (X)Inapto ";
                            }
                        }



                        if (rAptidao.apt_Trabalho_Altura == true || zAptidao.apt_Trabalho_Altura == true )
                        {
                            zPar++;
                            if (clinico.apt_Trabalho_Altura2 == null || clinico.apt_Trabalho_Altura2.ToString().Trim() == "" || clinico.apt_Trabalho_Altura2.ToString().Trim() == "False" || clinico.apt_Trabalho_Altura2.ToString().Trim() == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Trabalho em Altura            ( )Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Trabalho em Altura            ( )Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Trabalho_Altura2 == "1" )
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Trabalho em Altura            (X)Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Trabalho em Altura            (X)Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Trabalho_Altura2 == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Trabalho em Altura            ( )Apto (X)Inapto ";
                                else
                                    xAptidao = xAptidao + " Trabalho em Altura            ( )Apto (X)Inapto ";
                            }
                        }

                        if (rAptidao.apt_Transporte == true || zAptidao.apt_Transporte == true )
                        {
                            zPar++;
                            if (clinico.apt_Transporte2 == null || clinico.apt_Transporte2.ToString().Trim() == "" || clinico.apt_Transporte2.ToString().Trim() == "False" || clinico.apt_Transporte2.ToString().Trim() == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Operar Eq.Transp.Motorizados ( )Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Operar Eq.Transp.Motorizados ( )Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Transporte2 == "1")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Operar Eq.Transp.Motorizados (X)Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Operar Eq.Transp.Motorizados (X)Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Transporte2 == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Operar Eq.Transp.Motorizados ( )Apto (X)Inapto ";
                                else
                                    xAptidao = xAptidao + " Operar Eq.Transp.Motorizados ( )Apto (X)Inapto ";
                            }
                        }

                        if (rAptidao.apt_Brigadista == true || zAptidao.apt_Brigadista == true )
                        {
                            zPar++;
                            if (clinico.apt_Brigadista2 == null || clinico.apt_Brigadista2.ToString().Trim() == "" || clinico.apt_Brigadista2.ToString().Trim() == "False" || clinico.apt_Brigadista2.ToString().Trim() == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Trabalho como Brigadista      ( )Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Trabalho como Brigadista      ( )Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Brigadista2 == "1")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Trabalho como Brigadista      (X)Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Trabalho como Brigadista      (X)Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Brigadista2 == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Trabalho como Brigadista      ( )Apto (X)Inapto ";
                                else
                                    xAptidao = xAptidao + " Trabalho como Brigadista      ( )Apto (X)Inapto ";
                            }
                        }



                        if (rAptidao.apt_Socorrista == true || zAptidao.apt_Socorrista == true )
                        {
                            zPar++;
                            if (clinico.apt_Socorrista2 == null || clinico.apt_Socorrista2.ToString().Trim() == "" || clinico.apt_Socorrista2.ToString().Trim() == "False" || clinico.apt_Socorrista2.ToString().Trim() == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Trabalho como Socorrista      ( )Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Trabalho como Socorrista      ( )Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Socorrista2 == "1")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Trabalho como Socorrista      (X)Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Trabalho como Socorrista      (X)Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Socorrista2 == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Trabalho como Socorrista      ( )Apto (X)Inapto ";
                                else
                                    xAptidao = xAptidao + " Trabalho como Socorrista      ( )Apto (X)Inapto ";
                            }
                        }

                        if (rAptidao.apt_Respirador == true)
                        {
                            zPar++;
                            if (clinico.apt_Socorrista2 == null || clinico.apt_Socorrista2.ToString().Trim() == "" || clinico.apt_Socorrista2.ToString().Trim() == "False" || clinico.apt_Socorrista2.ToString().Trim() == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Uso de Respiradores           ( )Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Uso de Respiradores           ( )Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Socorrista2 == "1")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Uso de Respiradores           (X)Apto ( )Inapto ";
                                else
                                    xAptidao = xAptidao + " Uso de Respiradores           (X)Apto ( )Inapto ";
                            }
                            else if (clinico.apt_Socorrista2 == "0")
                            {
                                if (zPar != 0 && zPar % 2 == 1)
                                    xAptidao = xAptidao + System.Environment.NewLine + " Uso de Respiradores           ( )Apto (X)Inapto ";
                                else
                                    xAptidao = xAptidao + " Uso de Respiradores           ( )Apto (X)Inapto ";
                            }
                        }

                        if (xAptidao == "") xAptidao = "";
                        else xAptidao = xAptidao.Substring(2);                    
                    }
                }
                //else

                if ( xAptidao.Trim()=="" || (rAptidao.Id == 0 && zAptidao.Id == 0) )
                {
                    

                    //if (clinico.apt_Espaco_Confinado.ToString() == "1" && clinico.apt_Trabalho_Altura.ToString() == "1" && clinico.apt_Transporte.ToString() == "1")
                    //{
                    //    xAptidao = "O empregado está apto para trabalho em altura, apto para trabalho em espaços confinados e apto para operar máquinas e equipamentos de transporte motorizado.";
                    //}
                    //else if (clinico.apt_Espaco_Confinado.ToString() == "1" && clinico.apt_Trabalho_Altura.ToString() == "1" && clinico.apt_Transporte.ToString().Trim() == "")
                    //{
                    //    xAptidao = "O empregado está apto para trabalho em altura e apto para trabalho em espaços confinados.";
                    //}
                    //else if (clinico.apt_Espaco_Confinado.ToString() == "1" && clinico.apt_Trabalho_Altura.ToString().Trim() == "" && clinico.apt_Transporte.ToString().Trim() == "")
                    //{
                    //    xAptidao = "O empregado está apto para trabalho em espaços confinados.";
                    //}
                    //else if (clinico.apt_Espaco_Confinado.ToString() == "1" && clinico.apt_Trabalho_Altura.ToString().Trim() == "" && clinico.apt_Transporte.ToString() == "")
                    //{
                    //    xAptidao = "O empregado está apto para trabalho em espaços confinados e apto para operar máquinas e equipamentos de transporte motorizado.";
                    //}
                    //else if (clinico.apt_Espaco_Confinado.ToString().Trim() == "" && clinico.apt_Trabalho_Altura.ToString() == "1" && clinico.apt_Transporte.ToString() == "1")
                    //{
                    //    xAptidao = "O empregado está apto para trabalho em altura e apto para operar máquinas e equipamentos de transporte motorizado.";
                    //}
                    //else if (clinico.apt_Espaco_Confinado.ToString().Trim() == "" && clinico.apt_Trabalho_Altura.ToString() == "1" && clinico.apt_Transporte.ToString().Trim() == "")
                    //{
                    //    xAptidao = "O empregado está apto para trabalho em altura.";
                    //}
                    //else if (clinico.apt_Espaco_Confinado.ToString().Trim() == "" && clinico.apt_Trabalho_Altura.ToString().Trim() == "" && clinico.apt_Transporte.ToString() == "1")
                    //{
                    //    xAptidao = "O empregado está apto para operar máquinas e equipamentos de transporte motorizado.";
                    //}

                    //if (clinico.apt_Submerso.ToString() == "1" && clinico.apt_Eletricidade.ToString().Trim() == "")
                    //{
                    //    xAptidao = xAptidao + " Apto para atividades submersas.";
                    //}
                    //else if (clinico.apt_Submerso.ToString() == "1" && clinico.apt_Eletricidade.ToString() == "1")
                    //{
                    //    xAptidao = xAptidao + " Apto para serviço em eletricidade.";
                    //}
                    //else if (clinico.apt_Submerso.ToString() == "1" && clinico.apt_Eletricidade.ToString() == "1")
                    //{
                    //    xAptidao = xAptidao + " Apto para atividades submersas e serviço em eletricidade.";
                    //}


                    if ( clinico.apt_Submerso2!=null && ( clinico.apt_Submerso2.ToString().Trim() == "1" || clinico.apt_Submerso2.ToString().Trim().ToUpper()=="TRUE" ) )
                    {
                        xAptidao = xAptidao + " Apto para atividades submersas.";
                    }

                    if (clinico.apt_Radiacao2 != null && (clinico.apt_Radiacao2.ToString().Trim() == "1" || clinico.apt_Radiacao2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        xAptidao = xAptidao + " Apto para radiação ionizante.";
                    }


                    if ( clinico.apt_Espaco_Confinado2!=null && ( clinico.apt_Espaco_Confinado2.ToString().Trim() == "1" || clinico.apt_Espaco_Confinado2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        xAptidao = xAptidao + " Apto para trabalho em espaços confinados.";
                    }

                    if ( clinico.apt_Transporte2 != null && ( clinico.apt_Transporte2.ToString().Trim() == "1" || clinico.apt_Transporte2.ToString().Trim().ToUpper() == "TRUE") )
                    {
                        xAptidao = xAptidao + " Apto operar máquinas e equipamentos de transporte motorizado.";
                    }

                    if ( clinico.apt_Trabalho_Altura2 != null && ( clinico.apt_Trabalho_Altura2.ToString().Trim() == "1" || clinico.apt_Trabalho_Altura2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        xAptidao = xAptidao + " Apto para trabalho em altura.";
                    }

                    if ( clinico.apt_Eletricidade2 != null && ( clinico.apt_Eletricidade2.ToString().Trim() == "1" || clinico.apt_Eletricidade2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        xAptidao = xAptidao + " Apto para serviço em eletricidade.";
                    }


                    if ( clinico.apt_Aquaviario2 != null &&  ( clinico.apt_Aquaviario2.ToString().Trim() == "1" || clinico.apt_Aquaviario2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        xAptidao = xAptidao + " Apto para serviços aquaviários.";
                    }

                    if ( clinico.apt_Alimento2 != null &&  ( clinico.apt_Alimento2.ToString().Trim() == "1" || clinico.apt_Alimento2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        xAptidao = xAptidao + " Apto para manipular alimentos.";
                    }

                    if ( clinico.apt_Brigadista2 != null &&  ( clinico.apt_Brigadista2.ToString().Trim() == "1" || clinico.apt_Brigadista2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        xAptidao = xAptidao + " Apto para Brigadista.";
                    }

                    if ( clinico.apt_Socorrista2 != null && ( clinico.apt_Socorrista2.ToString().Trim() == "1" || clinico.apt_Socorrista2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        xAptidao = xAptidao + " Apto para Socorrista.";
                    }

                    if ( clinico.apt_Respirador2 != null &&  ( clinico.apt_Respirador2.ToString().Trim() == "1" || clinico.apt_Respirador2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        xAptidao = xAptidao + " Apto para Uso de Respiradores.";
                    }



                    if (clinico.apt_Submerso2 != null && (clinico.apt_Submerso2.ToString().Trim() == "0" || clinico.apt_Submerso2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        xAptidao = xAptidao + " Inapto para atividades submersas.";
                    }

                    if (clinico.apt_Radiacao2 != null && (clinico.apt_Radiacao2.ToString().Trim() == "0" || clinico.apt_Radiacao2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        xAptidao = xAptidao + " Inapto para radiação ionizante.";
                    }

                    if (clinico.apt_Espaco_Confinado2 != null && (clinico.apt_Espaco_Confinado2.ToString().Trim() == "0" || clinico.apt_Espaco_Confinado2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        xAptidao = xAptidao + " Inapto para trabalho em espaços confinados.";
                    }

                    if (clinico.apt_Transporte2 != null && (clinico.apt_Transporte2.ToString().Trim() == "0" || clinico.apt_Transporte2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        xAptidao = xAptidao + " Inapto operar máquinas e equipamentos de transporte motorizado.";
                    }

                    if (clinico.apt_Trabalho_Altura2 != null && (clinico.apt_Trabalho_Altura2.ToString().Trim() == "0" || clinico.apt_Trabalho_Altura2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        xAptidao = xAptidao + " Inapto para trabalho em altura.";
                    }

                    if (clinico.apt_Eletricidade2 != null && (clinico.apt_Eletricidade2.ToString().Trim() == "0" || clinico.apt_Eletricidade2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        xAptidao = xAptidao + " Inapto para serviço em eletricidade.";
                    }


                    if (clinico.apt_Aquaviario2 != null && (clinico.apt_Aquaviario2.ToString().Trim() == "0" || clinico.apt_Aquaviario2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        xAptidao = xAptidao + " Inapto para serviços aquaviários.";
                    }

                    if (clinico.apt_Alimento2 != null && (clinico.apt_Alimento2.ToString().Trim() == "0" || clinico.apt_Alimento2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        xAptidao = xAptidao + " Inapto para manipular alimentos.";
                    }

                    if (clinico.apt_Brigadista2 != null && (clinico.apt_Brigadista2.ToString().Trim() == "0" || clinico.apt_Brigadista2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        xAptidao = xAptidao + " Inapto para Brigadista.";
                    }

                    if (clinico.apt_Socorrista2 != null && (clinico.apt_Socorrista2.ToString().Trim() == "0" || clinico.apt_Socorrista2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        xAptidao = xAptidao + " Inapto para Socorrista.";
                    }

                    if (clinico.apt_Respirador2 != null && (clinico.apt_Respirador2.ToString().Trim() == "0" || clinico.apt_Respirador2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        xAptidao = xAptidao + " Inapto para Uso de Respiradores.";
                    }



                }




                string zObs = "";
                                
                switch (clinico.IdEmpregado.nIND_BENEFICIARIO)
                {
                    case (int)TipoBeneficiario.BeneficiarioReabilitado:
                        zObs = "( Beneficiário Reabilitado )     ";
                        break;
                    case (int)TipoBeneficiario.PortadorDeficiencia:
                        zObs = "( Pessoa com deficiência )     ";
                        break;
                    case (int)TipoBeneficiario.NaoAplicavel:
                        zObs = "";
                        break;
                    default:
                        zObs = "";
                        break;
                }

                //if (zObs + clinico.ObservacaoResultado != "")
                //   newRow["ObservacaoResultado"] = zObs + clinico.ObservacaoResultado + System.Environment.NewLine + xAptidao; 
                //else
                //    newRow["ObservacaoResultado"] = xAptidao;


                //if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0)
                //    xAptidao = "";

                if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("161.140") > 0) //JOHNSON
                {
                    if (zObs != "")
                        newRow["ObservacaoResultado"] = ( clinico.ObservacaoResultado.Trim() + "   " + zObs + System.Environment.NewLine + xAptidao ).Trim();
                    else
                        newRow["ObservacaoResultado"] = (clinico.ObservacaoResultado.Trim() + "   " + xAptidao).Trim();
                }
                else
                {
                    if (zObs != "")
                        newRow["ObservacaoResultado"] = zObs + System.Environment.NewLine + xAptidao;
                    else
                        newRow["ObservacaoResultado"] = xAptidao;
                }
            }

            #endregion

            #region ExameTipo

            public void ExameTipo(IExameBase iExameBase)
            {
                if (iExameBase.IdExameDicionario.mirrorOld == null)
                    iExameBase.IdExameDicionario.Find();

                if (iExameBase.IdExameDicionario.Descricao.ToUpper().Trim() == "MUDANÇA DE FUNÇÃO")
                    newRow["tDS_EXAME_TIPO"] = "Exame de Mudança de Riscos Ocupacionais";
                else
                    newRow["tDS_EXAME_TIPO"] = iExameBase.IdExameDicionario.Descricao;

            }

            #endregion

            #region AvaliacaoAmbiental

            public void AvaliacaoAmbiental(Clinico clinico,
                                                     Pcmso pcmso,
                                                     List<Ghe> ghes,
                                                     bool ExamesSomentePeriodoPcmso)
            {
                string sNomeGhe = string.Empty;
                string sRiscosAmbientais = string.Empty;
                string sRiscosOcupacionais = string.Empty;
                string sExamesOcupacionais = string.Empty;


                bool zDesconsiderar = false;
                string xDataBranco = "";

                Cliente xCliente = new Cliente();


                Ghe ghe_Apt = new Ghe();

                if (pcmso != null || pcmso.Id != 0)
                {
                    Ghe ghe;

                    if (ghes == null || ghes.Count == 0)
                        ghe = clinico.IdEmpregadoFuncao.GetGheEmpregado(pcmso.IdLaudoTecnico);
                    else
                    {
                        int IdGhe = clinico.IdEmpregadoFuncao.GetIdGheEmpregado(pcmso.IdLaudoTecnico);

                        ghe = ghes.Find(delegate(Ghe g) { return g.Id == IdGhe; });
                    }

                    if (ghe == null || ghe.Id == 0)
                        throw new Exception("O empregado " + clinico.IdEmpregado.tNO_EMPG
                            + " não está associado a nenhum GHE ou o PCMSO ainda não foi atualizado para o novo Laudo Técnico realizado!");

                    ghe_Apt.Find(ghe.Id);

                    sNomeGhe = ghe.tNO_FUNC;
                    sRiscosAmbientais = ghe.RiscosAmbientaisAso();

            
                    xCliente.Find( pcmso.IdCliente.Id );

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("PRAJNA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("QTECK") > 0 || Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0)
                        sRiscosOcupacionais = ghe.RiscosOcupacionaisAso_Prajna(xCliente.Exibir_Riscos_ASO, false, xCliente.Riscos_PPRA);
                    else
                        sRiscosOcupacionais = ghe.RiscosOcupacionaisAso(xCliente.Exibir_Riscos_ASO, true, xCliente.Riscos_PPRA);
                                        

                    if ( xCliente.InibirGHE == false )
                       newRow["tNO_GHE"] = "GHE : " + sNomeGhe;
                    else
                       newRow["tNO_GHE"] = "";



                    //se for para desconsiderar data de complementares a partir de certa data, usar o esquema que já existe com xDataBranco
                    if (xDataBranco == "")
                    {
                        if (xCliente.Ativar_DesconsiderarCompl == true)
                        {
                            if (xCliente.Dias_Desconsiderar > 0)
                            {
                                //System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                                //ExamePlanejamento xPlanej = new ExamePlanejamento();
                                //clinico.IdEmpregado.Find();
                                //clinico.IdExameDicionario.Find();
                                //clinico.IdPcmso.Find();

                                //xPlanej.Find(" IdEmpregado =" + clinico.IdEmpregado.Id.ToString() + " and IdExameDicionario = " + clinico.IdExameDicionario.Id.ToString() + " and IdPCMSO = " + clinico.IdPcmso.Id.ToString());

                                //if (xPlanej.Id == 0)
                                //{
                                //    xDataBranco = clinico.DataExame.AddDays(xCliente.Dias_Desconsiderar * (-1)).ToString("dd/MM/yyyy", ptBr);
                                //}
                                //else
                                //{
                                //    if (xPlanej.DataVencimento == new DateTime())
                                //        xDataBranco = "  /  /    ";
                                //    else
                                //        xDataBranco = xPlanej.DataVencimento.AddDays(xCliente.Dias_Desconsiderar * (-1)).ToString("dd/MM/yyyy", ptBr);
                                //}

                                zDesconsiderar = true;
                            }
                        }
                    }
                    else
                    {
                        zDesconsiderar = false;
                    }

                    //as horas do clinico e complementar são carregadas, e na comparação para exibir datas
                    //essas horas podem gerar problemas.  Por isso jogo 23 horas na data do exame - Wagner - 20/07/2018
                    clinico.DataExame = clinico.DataExame.AddHours(23 - clinico.DataExame.Hour);


                    if (xCliente.Bloquear_Data_Demissionais == true && clinico.IdExameDicionario.ToString().Trim() == "Demissional")
                    {
                        xDataBranco = "31/12/2050";
                        zDesconsiderar = false;
                    }



                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                    {
                        if (clinico.IdExameDicionario.ToString().Trim() == "Demissional")
                        {
                            if (zDesconsiderar == false)
                                sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Asterisco(ghe, ExamesSomentePeriodoPcmso, xCliente.Exibir_Datas_Exames_ASO, xDataBranco, zDesconsiderar);
                            else
                                sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Asterisco_Desconsiderar(ghe, ExamesSomentePeriodoPcmso, xCliente.Exibir_Datas_Exames_ASO, clinico, xCliente.Dias_Desconsiderar);
                        }
                        else
                        {
                            if ( zDesconsiderar == false )
                                sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Asterisco(ghe, ExamesSomentePeriodoPcmso, xCliente.Exibir_Datas_Exames_ASO, xDataBranco, zDesconsiderar);
                            else
                                sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Asterisco_Desconsiderar(ghe, ExamesSomentePeriodoPcmso, xCliente.Exibir_Datas_Exames_ASO, clinico, xCliente.Dias_Desconsiderar);
                        }
                    }
                    else if (clinico.IdExameDicionario.ToString().Trim() == "Mudança de Função")
                    {
                        // procurar ghe_ant primeiro, na mesma classif.funcional
                        // se nao encontrar, procurar classif.funcional anterior e ghe

                        if (xCliente.GHEAnterior_MudancaFuncao == true)
                        {

                            Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

                            DataSet xdS = xGHE.Trazer_Laudos_GHEs_Colaborador(clinico.IdEmpregadoFuncao.nID_EMPREGADO.Id);

                            if (xdS.Tables[0].Rows.Count < 2)
                            {
                                throw new Exception("O empregado " + clinico.IdEmpregado.tNO_EMPG
                                    + " está associado a apenas um GHE/Classif.Funcional, logo, não é possível gerar o ASO de Mudança de Função. ");
                            }


                            int znAux = 0;
                            Int32 zGHE_Atual = 0;
                            Int32 zGHE_Ant = 0;


                            foreach (DataRow row in xdS.Tables[0].Rows)
                            {
                                znAux++;

                                if (znAux == 1) zGHE_Atual = System.Convert.ToInt32(row["nId_Func"].ToString());
                                else if (znAux == 2) zGHE_Ant = System.Convert.ToInt32(row["nId_Func"].ToString());
                                else break;

                            }


                            //ghe_ant
                            //ghe
                            Ghe zGhe1 = new Ghe();
                            zGhe1.Find(zGHE_Atual);
                            Ghe zGhe2 = new Ghe();
                            zGhe2.Find(zGHE_Ant);

                            if ( zDesconsiderar == false )
                                sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao(zGhe1, zGhe2, ExamesSomentePeriodoPcmso, xCliente.Exibir_Datas_Exames_ASO, xDataBranco, zDesconsiderar);
                            else
                                sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao_Desconsiderar(zGhe1, zGhe2, ExamesSomentePeriodoPcmso, xCliente.Exibir_Datas_Exames_ASO, clinico, xCliente.Dias_Desconsiderar);
                        }
                        else
                        {
                            if ( zDesconsiderar == false  )
                                sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado(ghe, ExamesSomentePeriodoPcmso, xCliente.Exibir_Datas_Exames_ASO, xDataBranco, zDesconsiderar);
                            else
                                sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Desconsiderar(ghe, ExamesSomentePeriodoPcmso, xCliente.Exibir_Datas_Exames_ASO, clinico, xCliente.Dias_Desconsiderar);
                        }


                    }
                    else
                    {

                        if (clinico.IdExameDicionario.ToString().Trim() == "Demissional")
                        {
                            if (zDesconsiderar == false)
                                sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado(ghe, ExamesSomentePeriodoPcmso, xCliente.Exibir_Datas_Exames_ASO, xDataBranco, zDesconsiderar);
                            else
                                sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Desconsiderar(ghe, ExamesSomentePeriodoPcmso, xCliente.Exibir_Datas_Exames_ASO, clinico, xCliente.Dias_Desconsiderar);
                        }
                        else
                        {
                            if (zDesconsiderar == false)
                                sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado(ghe, ExamesSomentePeriodoPcmso, xCliente.Exibir_Datas_Exames_ASO, xDataBranco, zDesconsiderar);
                            else
                                sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Desconsiderar(ghe, ExamesSomentePeriodoPcmso, xCliente.Exibir_Datas_Exames_ASO, clinico, xCliente.Dias_Desconsiderar);
                        }
                    }


                }



                //checar se têm exames de aptidão
                string sExamesOcupacionais_Aptidao = "";

                Empregado_Aptidao xAptidao = new Empregado_Aptidao();
                clinico.IdEmpregado.Find();
                xAptidao.Find(" nId_Empregado = " + clinico.IdEmpregado.Id.ToString());

                GHE_Aptidao zAptidao = new GHE_Aptidao();
                zAptidao.Find(" nId_Func = " + ghe_Apt.Id.ToString());


                if (xAptidao.Id != 0 || zAptidao.Id != 0)
                {
                    if ((xAptidao.apt_Alimento == true || xAptidao.apt_Aquaviario == true || xAptidao.apt_Eletricidade == true || xAptidao.apt_Espaco_Confinado == true ||
                         xAptidao.apt_Submerso == true || xAptidao.apt_Trabalho_Altura == true || xAptidao.apt_Transporte == true || xAptidao.apt_Brigadista == true || xAptidao.apt_Socorrista == true || xAptidao.apt_Respirador == true || xAptidao.apt_PPR == true || xAptidao.apt_Radiacao == true) ||
                         (zAptidao.apt_Alimento == true || zAptidao.apt_Aquaviario == true || zAptidao.apt_Eletricidade == true || zAptidao.apt_Espaco_Confinado == true ||
                        zAptidao.apt_Submerso == true || zAptidao.apt_Trabalho_Altura == true || zAptidao.apt_Transporte == true || zAptidao.apt_Brigadista == true || zAptidao.apt_Socorrista == true || zAptidao.apt_PPR || zAptidao.apt_Radiacao == true ))
                    {

                        Empregado_Aptidao nAptidao = new Empregado_Aptidao();

                        //juntando aptidao do empregado com do PPRA-GHE
                        if (xAptidao.Id != 0 && zAptidao.Id != 0)
                        {
                            nAptidao.apt_Alimento = xAptidao.apt_Alimento || zAptidao.apt_Alimento;
                            nAptidao.apt_Aquaviario = xAptidao.apt_Aquaviario || zAptidao.apt_Aquaviario;
                            nAptidao.apt_Brigadista = xAptidao.apt_Brigadista || zAptidao.apt_Brigadista;
                            nAptidao.apt_Eletricidade = xAptidao.apt_Eletricidade || zAptidao.apt_Eletricidade;
                            nAptidao.apt_Espaco_Confinado = xAptidao.apt_Espaco_Confinado || zAptidao.apt_Espaco_Confinado;
                            nAptidao.apt_Socorrista = xAptidao.apt_Socorrista || zAptidao.apt_Socorrista;
                            nAptidao.apt_Submerso = xAptidao.apt_Submerso || zAptidao.apt_Submerso;
                            nAptidao.apt_Trabalho_Altura = xAptidao.apt_Trabalho_Altura || zAptidao.apt_Trabalho_Altura;
                            nAptidao.apt_Transporte = xAptidao.apt_Transporte || zAptidao.apt_Transporte;
                            nAptidao.apt_PPR = xAptidao.apt_PPR || zAptidao.apt_PPR;
                            nAptidao.apt_Radiacao = xAptidao.apt_Radiacao || zAptidao.apt_Radiacao;
                            nAptidao.apt_Respirador = xAptidao.apt_Respirador;
                            nAptidao.nId_Empregado = clinico.IdEmpregado.Id;
                        }
                        else if (xAptidao.Id != 0)
                        {
                            nAptidao.apt_Alimento = xAptidao.apt_Alimento;
                            nAptidao.apt_Aquaviario = xAptidao.apt_Aquaviario;
                            nAptidao.apt_Brigadista = xAptidao.apt_Brigadista;
                            nAptidao.apt_Eletricidade = xAptidao.apt_Eletricidade;
                            nAptidao.apt_Espaco_Confinado = xAptidao.apt_Espaco_Confinado;
                            nAptidao.apt_Socorrista = xAptidao.apt_Socorrista;
                            nAptidao.apt_Submerso = xAptidao.apt_Submerso;
                            nAptidao.apt_Trabalho_Altura = xAptidao.apt_Trabalho_Altura;
                            nAptidao.apt_Transporte = xAptidao.apt_Transporte;
                            nAptidao.apt_PPR = xAptidao.apt_PPR;
                            nAptidao.apt_Radiacao = xAptidao.apt_Radiacao ;
                            nAptidao.apt_Respirador = xAptidao.apt_Respirador;
                            nAptidao.nId_Empregado = clinico.IdEmpregado.Id;
                        }
                        else if (zAptidao.Id != 0)
                        {
                            nAptidao.apt_Alimento = zAptidao.apt_Alimento;
                            nAptidao.apt_Aquaviario = zAptidao.apt_Aquaviario;
                            nAptidao.apt_Brigadista = zAptidao.apt_Brigadista;
                            nAptidao.apt_Eletricidade = zAptidao.apt_Eletricidade;
                            nAptidao.apt_Espaco_Confinado = zAptidao.apt_Espaco_Confinado;
                            nAptidao.apt_Socorrista = zAptidao.apt_Socorrista;
                            nAptidao.apt_Submerso = zAptidao.apt_Submerso;
                            nAptidao.apt_Trabalho_Altura = zAptidao.apt_Trabalho_Altura;
                            nAptidao.apt_Transporte = zAptidao.apt_Transporte;
                            nAptidao.apt_PPR = zAptidao.apt_PPR;
                            nAptidao.apt_Radiacao = zAptidao.apt_Radiacao;
                            nAptidao.nId_Empregado = clinico.IdEmpregado.Id;
                        }



                       
                        xCliente.Find(pcmso.IdCliente.Id);

                        if (zDesconsiderar == false)
                            sExamesOcupacionais_Aptidao = clinico.GetPlanejamentoExamesAso_Formatado_Aptidao(nAptidao, xCliente.Exibir_Datas_Exames_ASO, xDataBranco, sExamesOcupacionais,zDesconsiderar, clinico );
                        else
                            sExamesOcupacionais_Aptidao = clinico.GetPlanejamentoExamesAso_Formatado_Aptidao_Desconsiderar(nAptidao, xCliente.Exibir_Datas_Exames_ASO, sExamesOcupacionais, clinico, xCliente.Dias_Desconsiderar);
                    }
                }


                if (sExamesOcupacionais.Trim() != "")
                {
                    if (sExamesOcupacionais_Aptidao.Trim() != "" && ((sExamesOcupacionais_Aptidao.Substring(0, 1) == "\n" && sExamesOcupacionais.Substring(sExamesOcupacionais.Length - 1, 1) == "\n") || sExamesOcupacionais.Substring(sExamesOcupacionais.Length - 3, 3) == "\n\r\n"))
                        sExamesOcupacionais = sExamesOcupacionais.Substring(0, sExamesOcupacionais.Length - 1) + sExamesOcupacionais_Aptidao;
                    else
                        sExamesOcupacionais = sExamesOcupacionais + sExamesOcupacionais_Aptidao;
                }
                else
                    sExamesOcupacionais = sExamesOcupacionais_Aptidao;





                string sBusca = "";
                string sBuscaProx = "";
                string xLinha = "";

                string sExames = "";
                string sDatas = "";

                string sExames2 = "";
                string sDatas2 = "";


                //retirar avaliação clínica
                if (xCliente.Bloquear_Avaliacao_Clinica == true)
                {

                    if (sExamesOcupacionais.IndexOf("Avaliação Clínica") > 0)
                    {


                        for (int rCont = 1; rCont < 40; rCont++)
                        {
                            sBusca = rCont.ToString().Trim() + ".";
                            sBuscaProx = (rCont + 1).ToString().Trim() + ".";

                            int xPosit = sExamesOcupacionais.IndexOf(sBusca);
                            int xPosit2 = sExamesOcupacionais.IndexOf(sBuscaProx);

                            xLinha = "";

                            if (xPosit >= 0)
                            {
                                if (xPosit2 > 0)
                                {
                                    xLinha = sExamesOcupacionais.Substring(xPosit, xPosit2 - xPosit);
                                }
                                else
                                {
                                    xLinha = sExamesOcupacionais.Substring(xPosit);
                                }


                                //pegar número do exame da avaliação clínica, e passar a colocar nos demais
                                if (xLinha.IndexOf("Avaliação Clínica") > 0)
                                {
                                    string sExamesOcupacionais_Old = sExamesOcupacionais;
                                    //retirar a avaliação clínica de sExames Ocupacionais
                                    sExamesOcupacionais = sExamesOcupacionais.Replace(xLinha.Replace("\n", ""), "");

                                    if (sExamesOcupacionais == sExamesOcupacionais_Old)
                                    {
                                        sExamesOcupacionais = sExamesOcupacionais.Replace(xLinha, "");
                                    }

                                    for (int bCont = rCont; bCont < 30; bCont++)
                                    {
                                        sBusca = bCont.ToString().Trim() + ".";
                                        sBuscaProx = (bCont + 1).ToString().Trim() + ".";
                                        xPosit = sExamesOcupacionais.IndexOf(sBuscaProx);

                                        if (xPosit > 0)
                                        {
                                            //sExamesOcupacionais = sExamesOcupacionais.Replace(sBuscaProx, sBusca);
                                            sExamesOcupacionais = sExamesOcupacionais.Substring(0, xPosit) + sBusca + sExamesOcupacionais.Substring(xPosit + sBuscaProx.Length);
                                        }

                                    }

                                    break;
                                }

                            }

                        }


                    }

                }



                clinico.IdExameDicionario.Find();

                try
                {
                    //retirar data de audiometria
                    if (clinico.IdExameDicionario.ToString().ToUpper().Trim() == "DEMISSIONAL")
                    {

                        if (sExamesOcupacionais.IndexOf("Audiometria  ") > 0)
                        {

                            if (xCliente.Demissional_Audiometria == true)
                            {

                                clinico.IdEmpregado.Find();


                                //checar se último demissional foi a mais de 120 dias
                                int xPosit = sExamesOcupacionais.IndexOf("Audiometria  ");
                                int xPosit2 = sExamesOcupacionais.IndexOf("/", xPosit + 10);


                                DateTime zUltimaAux = TrazerDataUltimoExame(6, clinico.IdEmpregado.Id);

                                if (zUltimaAux.Year < 2050)
                                {
                                    int xDias = (clinico.DataExame - zUltimaAux).Days;

                                    if (xDias < 120)
                                    {
                                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                                        sExamesOcupacionais = sExamesOcupacionais.Substring(0, xPosit2 - 2) + zUltimaAux.ToString("dd/MM/yyyy", ptBr) + sExamesOcupacionais.Substring(xPosit2 + 10);
                                    }
                                    else
                                    {
                                        sExamesOcupacionais = sExamesOcupacionais.Substring(0, xPosit2 - 2) + "  /  /    " + sExamesOcupacionais.Substring(xPosit2 + 10);
                                    }

                                }

                                //string xDataAud = sExamesOcupacionais.Substring(xPosit2 - 2, 10);

                                //if (xDataAud != "  /  /    ")
                                //{
                                //    //checar se último demissional foi a mais de 120 dias
                                //    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                                //    DateTime zDataAud = System.Convert.ToDateTime(xDataAud, ptBr);

                                //    int xDias = (clinico.DataExame - zDataAud).Days;

                                //    if (xDias > 120)
                                //    {
                                //        sExamesOcupacionais = sExamesOcupacionais.Substring(0, xPosit2 - 2) + "  /  /    " + sExamesOcupacionais.Substring(xPosit2 + 10);
                                //    }
                                //}
                                //else
                                //{
                                //    DateTime zUltimaAux = TrazerDataUltimoExame(6, clinico.IdEmpregado.Id);
                                //}

                            }
                            else
                            {
                                //opção não - deixar sempre em branco
                                int xPosit = sExamesOcupacionais.IndexOf("Audiometria  ");
                                int xPosit2 = sExamesOcupacionais.IndexOf("/", xPosit + 10);

                                sExamesOcupacionais = sExamesOcupacionais.Substring(0, xPosit2 - 2) + "  /  /    " + sExamesOcupacionais.Substring(xPosit2 + 10);
                            }
                        }
                    }


                }
                catch (Exception ex)
                {

                }
                finally
                {

                }








                int xLimite = 640;

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                {
                    xLimite = 700;
                }


                if (sExamesOcupacionais.Length > xLimite)
                {
                    //contar quebras
                    var s2 = sExamesOcupacionais.Replace("\n", "");
                    int zQuebras = sExamesOcupacionais.Length - s2.Length;

                    string zExames = "";


                    if (zQuebras > 10)
                    {
                        int rPosit = 0;
                        rPosit = sExamesOcupacionais.IndexOf("\n", 0);

                        if (rPosit < 80)  //indicando que têm quebra entre primeiro e segundo exame
                        {

                            zQuebras = 1;

                            for (int kCont = 0; kCont < sExamesOcupacionais.Length; kCont++)
                            {
                                if (sExamesOcupacionais.Substring(kCont, 1) == "\n")
                                {
                                    if (zQuebras % 2 == 0)
                                    {
                                        zExames = zExames + sExamesOcupacionais.Substring(kCont, 1);
                                    }

                                    zQuebras = zQuebras + 1;
                                }
                                else
                                {
                                    zExames = zExames + sExamesOcupacionais.Substring(kCont, 1);
                                }

                            }


                            sExamesOcupacionais = zExames;


                        }
                    }

                }





                //verificar se FATOR RH e Tipo Sanguineo fazem parte da admissão e não foram incluídos em sExamesOcupacionais, se fizerem, devem entrar em todos os ASOs, com a data que foi realizado
                //em alguns franqueados eles vêm em exames separados, em outros juntos.

                if (xCliente.FatorRH == true && clinico.IdExameDicionario.Descricao.Trim() == "Periódico")
                {

                    Ilitera.Data.PPRA_EPI xExamesAdmissionais = new Ilitera.Data.PPRA_EPI();

                    DataSet xdS = xExamesAdmissionais.Trazer_Admissao_FatorRH_TipoSanguineo(pcmso.Id, ghe_Apt.Id, clinico.IdEmpregadoFuncao.nID_EMPREGADO.Id);


                    foreach (DataRow row in xdS.Tables[0].Rows)
                    {
                        //row["nId_Func"].ToString());

                        //checar se exame já está em sExamesOcupacionais, se estiver, pode sair, senão, inserí-lo com a data
                        string xExame = row["Exame"].ToString().Trim();
                        string fPosit = "1. ";
                        int fCont = 1;

                        if (sExamesOcupacionais.ToUpper().IndexOf(xExame.ToUpper()) < 0)
                        {
                            for (fCont = 24; fCont >= 1; fCont--)
                            {
                                fPosit = fCont.ToString().Trim() + ". ";

                                if (sExamesOcupacionais.IndexOf(fPosit) >= 0)
                                {
                                    fCont++;
                                    fPosit = (fCont).ToString().Trim() + ". ";
                                    break;
                                }
                            }


                            string xEspacos = "                                                ";

                            if (xExame.Length < xEspacos.Length)
                            {
                                xExame = xExame + xEspacos.Substring(1, xEspacos.Length - xExame.Length);
                            }

                            xExame = xExame.Substring(0, 38) + row["Data_Realizado"].ToString().Trim();

                            sExamesOcupacionais = sExamesOcupacionais + fPosit + xExame;

                            if (fCont % 2 == 1 || fCont <= 9) sExamesOcupacionais = sExamesOcupacionais + "\n";
                            else sExamesOcupacionais = sExamesOcupacionais + "   ";

                        }

                    }

                }





                if (sRiscosAmbientais == string.Empty)
                    sRiscosAmbientais = "Risco ocupacional inexistente"; // "Sem risco ocupacional específico";

                if (sRiscosOcupacionais == string.Empty)
                    sRiscosOcupacionais = "Risco ocupacional inexistente" ;  // "Sem risco ocupacional específico";


                clinico.IdEmpregadoFuncao.nID_EMPR.Find();
                clinico.IdEmpregadoFuncao.nID_EMPR.IdGrupoEmpresa.Find();

                if (clinico.IdEmpregadoFuncao.nID_EMPR.NomeAbreviado.ToUpper().IndexOf("DRIVEWAY") >= 0 || clinico.IdEmpregadoFuncao.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "UNION")
                {

                    if (sRiscosOcupacionais == "Risco ocupacional inexistente")
                    {
                        sRiscosOcupacionais = "Inexistente";
                    }

                    if (sRiscosOcupacionais.ToUpper().IndexOf("RUÍDO CONTÍNUO") >= 0)
                    {
                        sRiscosOcupacionais = sRiscosOcupacionais.Replace("Ruído Contínuo", "Ruído");
                    }

                }

                //verificar opção para retirar "**Dispensado de exames complementares"                
                if (xCliente.Bloquear_Dispensado == true)
                {
                    sRiscosOcupacionais = sRiscosOcupacionais.Replace("**Dispensado de exames complementares", " ").Replace("*Dispensado de exames complementares", " ").Replace("Dispensado de exames complementares", " ");
                }



                newRow["RiscosAmbientais"] = sRiscosAmbientais;
                newRow["RiscosOcupacionais"] = sRiscosOcupacionais;
                newRow["ExamesComplementares"] = sExamesOcupacionais;
            }
            #endregion

            #region Foto

            public void Foto(Clinico clinico, Cliente cliente, bool SemFoto)
            {
                if (!SemFoto)
                {
                    if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("EY") > 0)
                    {
                        newRow["iFOTO"] = "I:\\fotosDocsDigitais\\_Ilitera\\ASO.jpg";
                    }
                    else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                    {
                        newRow["iFOTO"] = "I:\\fotosDocsDigitais\\_Prajna\\Relatorio_Empresa.jpg"; 
                    }
                    else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("COTIA") > 0)
                    {
                        if (clinico.IdEmpregado.nNO_FOTO < 1 )//.FotoEmpregado(cliente).ToLower().Trim() == "")
                        {
                            newRow["iFOTO"] = "I:\\fotosDocsDigitais\\_Cotia\\Logo_Ilitera.jpg";
                        }
                        else
                            newRow["iFOTO"] = clinico.IdEmpregado.FotoEmpregado(cliente).ToLower();
                    }
                    else
                        newRow["iFOTO"] = clinico.IdEmpregado.FotoEmpregado(cliente).ToLower();
                }
                else
                {
                    if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("EY") > 0)
                    {
                        newRow["iFOTO"] = "I:\\fotosDocsDigitais\\_Ilitera\\ASO.jpg";
                    }
                    else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                    {
                        newRow["iFOTO"] = "I:\\fotosDocsDigitais\\_Prajna\\Relatorio_Empresa.jpg";
                    }
                    else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("COTIA") > 0)
                    {
                        newRow["iFOTO"] = "I:\\fotosDocsDigitais\\_Cotia\\Logo_Ilitera.jpg";
                    }
                }
            }

            #endregion

            #region DataNascimentoIdentidade

            public void DataNascimentoIdentidade(Clinico clinico)
            {
                string strDataNascimento = (clinico.IdEmpregado.hDT_NASC != new DateTime()) ? clinico.IdEmpregado.hDT_NASC.ToString("dd-MM-yyyy") + "        Idade " + clinico.IdEmpregado.IdadeEmpregado().ToString() + " anos" : string.Empty;

                string strIdentidade;

                if (clinico.IdEmpregado.tNO_IDENTIDADE == string.Empty)
                    strIdentidade = "RG                            Nascido em " + strDataNascimento;
                else
                    strIdentidade = "RG " + clinico.IdEmpregado.tNO_IDENTIDADE + "     Nascido em " + strDataNascimento;

                newRow["tNO_IDENTIDADE"] = strIdentidade;
            }
            #endregion

            #region ContatoMedico

            public void ContatoMedico(IExameBase iExameBase)
            {
                if (iExameBase.IdJuridica.mirrorOld == null)
                    iExameBase.IdJuridica.Find();

                //Contato Médico - Telefone da Clínica
                if (iExameBase.IdJuridica.Id != 0)
                {
                    string xFone = iExameBase.IdJuridica.GetContatoTelefonico().GetDDDTelefone();
                    string xMail = iExameBase.IdJuridica.Email.Trim();


                    //if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("EY") > 0)
                    //{
                    //    newRow["tCONTATO_MEDICO"] = "";
                    //    return;
                    //}



                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") > 0)
                    {
                        xMail = "   " + iExameBase.IdJuridica.GetMunicipio().NomeAbreviado.Trim() + " " + iExameBase.IdJuridica.GetEndereco().Uf.Trim();


                        if (xFone.Trim() != "" && xMail.Trim() != "")
                        {
                            if (xMail.Length > 40)
                            {
                                if (xMail.Length > 64) xMail = xMail.Substring(0, 64);

                                string xNome = iExameBase.IdJuridica.NomeAbreviado.Trim();
                                if (xNome.Length > 38) xNome = xNome.Substring(0, 38);

                                newRow["tCONTATO_MEDICO"] = xNome + "   Fone: " + iExameBase.IdJuridica.GetContatoTelefonico().GetDDDTelefone() + System.Environment.NewLine + xMail;
                            }
                            else
                            {
                                if (xMail.Length > 40) xMail = xMail.Substring(0, 40);
                                newRow["tCONTATO_MEDICO"] = iExameBase.IdJuridica.NomeAbreviado + System.Environment.NewLine + "Fone: " + iExameBase.IdJuridica.GetContatoTelefonico().GetDDDTelefone() + xMail;
                            }
                        }
                        else if (xFone.Trim() == "" && xMail.Trim() != "")
                        {
                            if (xMail.Length > 64) xMail = xMail.Substring(0, 64);
                            newRow["tCONTATO_MEDICO"] = iExameBase.IdJuridica.NomeAbreviado + System.Environment.NewLine + xMail;
                        }
                        else if (xFone.Trim() != "" && xMail.Trim() == "")
                            newRow["tCONTATO_MEDICO"] = iExameBase.IdJuridica.NomeAbreviado + System.Environment.NewLine + "Fone: " + iExameBase.IdJuridica.GetContatoTelefonico().GetDDDTelefone();
                        else
                            newRow["tCONTATO_MEDICO"] = "";
                    }
                    else
                    {

                        if (xFone.Trim() != "" && xMail.Trim() != "")
                        {
                            if (xMail.Length > 40)
                            {
                                if (xMail.Length > 64) xMail = xMail.Substring(0, 64);

                                string xNome = iExameBase.IdJuridica.NomeAbreviado.Trim();
                                if (xNome.Length > 38) xNome = xNome.Substring(0, 38);

                                newRow["tCONTATO_MEDICO"] = xNome + "   Fone: " + iExameBase.IdJuridica.GetContatoTelefonico().GetDDDTelefone() + System.Environment.NewLine + "e-mail: " + xMail;
                            }
                            else
                            {
                                if (xMail.Length > 40) xMail = xMail.Substring(0, 40);
                                newRow["tCONTATO_MEDICO"] = iExameBase.IdJuridica.NomeAbreviado + System.Environment.NewLine + "Fone: " + iExameBase.IdJuridica.GetContatoTelefonico().GetDDDTelefone() + "  e-mail: " + xMail;
                            }
                        }
                        else if (xFone.Trim() == "" && xMail.Trim() != "")
                        {
                            if (xMail.Length > 64) xMail = xMail.Substring(0, 64);
                            newRow["tCONTATO_MEDICO"] = iExameBase.IdJuridica.NomeAbreviado + System.Environment.NewLine + "e-mail: " + xMail;
                        }
                        else if (xFone.Trim() != "" && xMail.Trim() == "")
                            newRow["tCONTATO_MEDICO"] = iExameBase.IdJuridica.NomeAbreviado + System.Environment.NewLine + "Fone: " + iExameBase.IdJuridica.GetContatoTelefonico().GetDDDTelefone();
                        else
                            newRow["tCONTATO_MEDICO"] = "";

                    }
                }
                else
                {
                    newRow["tCONTATO_MEDICO"] = " ";
                }
            }

            #endregion

            #region Resultado

            public void Resultado(Clinico clinico)
            {
                if (clinico.IndResultado == (int)ResultadoExame.Normal)
                    newRow["tX_APTO"] = "X";
                else if (clinico.IndResultado == (int)ResultadoExame.Alterado)
                    newRow["tX_INAPTO"] = "X";
                else if (clinico.IndResultado == (int)ResultadoExame.EmEspera)
                    newRow["tX_Espera"] = "X";
            }
            #endregion

            #region Medico

            public void Medico(IExameBase iExameBase)
            {
                Medico medico = iExameBase.IdMedico;

                //Para empresas que eram "PCMSO não contratada" e que agora são.	
                if (medico.Id == 0 || medico.Id == (int)Medicos.PcmsoNaoContratada)
                    return;

                if (medico.mirrorOld == null)
                    medico.Find();

                if (medico.NomeCompleto.Trim().ToUpper() != "INDEFINIDO")
                {
                    newRow["tNOME_MEDICO"] = medico.NomeCompleto;
                    newRow["tTITULO_MEDICO"] = medico.Titulo + " " + medico.Numero;
                }
                else
                {
                    newRow["tNOME_MEDICO"] = "";
                    newRow["tTITULO_MEDICO"] = "";
                }
            }
            #endregion

            #region Juridica

            public void Juridica(ConvocacaoExame convocacao)
            {
                if (convocacao.IdCliente.mirrorOld == null)
                    convocacao.IdCliente.Find();

                if (convocacao.IdCliente.IdJuridicaPapel.Id == (int)IndJuridicaPapel.TerceiroAutonomo)
                    Juridica(convocacao.IdCliente.IdJuridicaPai, string.Empty, newRow);
                else
                    Juridica(convocacao.IdCliente, string.Empty, newRow);
            }

            public void Juridica(Clinico clinico)
            {
                if (clinico.IdEmpregado.nID_EMPR.mirrorOld == null)
                    clinico.IdEmpregado.nID_EMPR.Find();

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0 || Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("EY") > 0)
                {
                    if (clinico.IdEmpregado.nID_EMPR.IdJuridicaPapel.Id == (int)IndJuridicaPapel.TerceiroAutonomo)
                    {
                        Juridica(clinico.IdEmpregado.nID_EMPR.IdJuridicaPai, string.Empty, newRow);
                    }
                    else
                    {
                        if (clinico.IdEmpregado.nID_EMPR.Id != clinico.IdEmpregadoFuncao.nID_EMPR.Id)   //colaboradores em projetos
                        {
                            Juridica(clinico.IdEmpregadoFuncao.nID_EMPR, string.Empty, newRow);
                        }
                        else
                        {
                            Juridica(clinico.IdEmpregado.nID_EMPR, string.Empty, newRow);
                        }
                    }
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Global") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("VIA") > 0)
                {

                    if (clinico.IdEmpregado.nID_EMPR.Id != clinico.IdEmpregadoFuncao.nID_EMPR.Id)   //colaboradores em projetos
                    {
                        Juridica(clinico.IdEmpregadoFuncao.nID_EMPR, string.Empty, newRow);
                    }
                    else
                    {
                        Juridica(clinico.IdEmpregado.nID_EMPR, string.Empty, newRow);
                    }

                }
                else
                {

                    if (clinico.IdEmpregado.nID_EMPR.IdJuridicaPapel.Id == (int)IndJuridicaPapel.TerceiroAutonomo)
                    {
                        Juridica(clinico.IdEmpregado.nID_EMPR.IdJuridicaPai, string.Empty, newRow);
                    }
                    else
                        //if (clinico.IdEmpregado.nID_EMPR.Id != clinico.IdEmpregadoFuncao.nID_EMPR.Id)   //colaboradores em projetos
                        //{
                        //    Juridica(clinico.IdEmpregadoFuncao.nID_EMPR, string.Empty, newRow);
                        //}
                        //else
                        Juridica(clinico.IdEmpregado.nID_EMPR, string.Empty, newRow);
                }
            }

            private static void Juridica(Juridica juridica, string tomadora, DataRow newRow)
            {
                if (juridica.mirrorOld == null)
                    juridica.Find();
                

                //Prajna - TESTAR
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                {

                    if (juridica.IdJuridicaPai.Id != 0 && juridica.IdJuridicaPai.Id != juridica.Id)
                    {
                        //Juridica xjur = new Juridica();
                        //xjur = juridica.IdJuridicaPai;


                        //newRow["RazaoSocial"] = juridica.IdJuridicaPai.NomeCompleto.ToString() + "  Unidade: " + juridica.NomeAbreviado;

                        //if (juridica.IdJuridicaPai.ToString().Trim().IndexOf("KNMOX") > 0)
                        //{
                        //    newRow["RazaoSocial"] = juridica.IdJuridicaPai.ToString();
                        //}
                        //else
                        //{
                            newRow["RazaoSocial"] = juridica.IdJuridicaPai.ToString() + "  Unidade: " + juridica.NomeAbreviado;
                        //}

                            Cliente zCliente = new Cliente();
                            zCliente.Find(" Cliente.IdJuridica = " + juridica.Id);

                        if (zCliente.Id != 0 && zCliente.CNPJ_Matriz == false)
                        {

                            if (juridica.CEI != string.Empty)
                            {
                                if (tomadora != string.Empty)
                                    newRow["CGC"] = juridica.NomeCodigo + "    " + tomadora + "    CEI " + juridica.CEI;
                                else
                                    newRow["CGC"] = juridica.NomeCodigo + "    CEI " + juridica.CEI;
                            }
                            else
                            {
                                if (tomadora != string.Empty)
                                    newRow["CGC"] = juridica.NomeCodigo + "       " + tomadora;
                                else
                                    newRow["CGC"] = juridica.NomeCodigo;
                            }


                        }
                        else
                        {
                            juridica.IdJuridicaPai.Find();
                            if (tomadora != string.Empty)
                                newRow["CGC"] = juridica.IdJuridicaPai.NomeCodigo + "       " + tomadora;
                            else
                                newRow["CGC"] = juridica.IdJuridicaPai.NomeCodigo;
                        }


                            if (zCliente.Endereco_Matriz == true)
                            {
                                newRow["Endereco"] = juridica.IdJuridicaPai.GetEndereco().GetEndereco();
                                newRow["Cidade"] = juridica.IdJuridicaPai.GetEndereco().GetCidade();
                                newRow["Estado"] = juridica.IdJuridicaPai.GetEndereco().GetEstado();
                                newRow["CEP"] = juridica.IdJuridicaPai.GetEndereco().Cep;
                            }
                            else
                            {
                                newRow["Endereco"] = juridica.GetEndereco().GetEndereco();
                                newRow["Cidade"] = juridica.GetEndereco().GetCidade();
                                newRow["Estado"] = juridica.GetEndereco().GetEstado();
                                newRow["CEP"] = juridica.GetEndereco().Cep;
                            }

                        //if (tomadora != string.Empty)
                        //    newRow["CGC"] = juridica.IdJuridicaPai.NomeCodigo + "       " + tomadora;
                        //else
                        //    newRow["CGC"] = juridica.IdJuridicaPai.NomeCodigo;

                    }
                    else
                    {
                        newRow["RazaoSocial"] = juridica.NomeCompleto + System.Environment.NewLine + juridica.NomeAbreviado  ;

                        newRow["Endereco"] = juridica.GetEndereco().GetEndereco();
                        newRow["Cidade"] = juridica.GetEndereco().GetCidade();
                        newRow["Estado"] = juridica.GetEndereco().GetEstado();
                        newRow["CEP"] = juridica.GetEndereco().Cep;

                        if (juridica.CEI != string.Empty)
                        {
                            if (tomadora != string.Empty)
                                newRow["CGC"] = juridica.NomeCodigo + "    " + tomadora + "    CEI " + juridica.CEI;
                            else
                                newRow["CGC"] = juridica.NomeCodigo + "    CEI " + juridica.CEI;
                        }
                        else
                        {
                            if (tomadora != string.Empty)
                                newRow["CGC"] = juridica.NomeCodigo + "       " + tomadora;
                            else
                                newRow["CGC"] = juridica.NomeCodigo;
                        }
                    }
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("ROPHE") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("COTIA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("EY") > 0)
                {
                    if (juridica.IdJuridicaPai.Id != 0 && juridica.IdJuridicaPai.Id != juridica.Id)
                    {
                        newRow["RazaoSocial"] = juridica.IdJuridicaPai.ToString() + "  Unidade: " + juridica.NomeAbreviado;


                        Cliente zCliente = new Cliente();
                        zCliente.Find(" Cliente.IdJuridica = " + juridica.Id);

                        if (zCliente.Endereco_Matriz == true)
                        {
                            newRow["Endereco"] = juridica.IdJuridicaPai.GetEndereco().GetEndereco();
                            newRow["Cidade"] = juridica.IdJuridicaPai.GetEndereco().GetCidade();
                            newRow["Estado"] = juridica.IdJuridicaPai.GetEndereco().GetEstado();
                            newRow["CEP"] = juridica.IdJuridicaPai.GetEndereco().Cep;
                        }
                        else
                        {
                            newRow["Endereco"] = juridica.GetEndereco().GetEndereco();
                            newRow["Cidade"] = juridica.GetEndereco().GetCidade();
                            newRow["Estado"] = juridica.GetEndereco().GetEstado();
                            newRow["CEP"] = juridica.GetEndereco().Cep;

                        }


                        if (zCliente.Id != 0 && zCliente.CNPJ_Matriz == false)
                        {
                            if (juridica.CEI != string.Empty)
                            {
                                if (tomadora != string.Empty)
                                    newRow["CGC"] = juridica.NomeCodigo + "    " + tomadora + "    CEI " + juridica.CEI;
                                else
                                    newRow["CGC"] = juridica.NomeCodigo + "    CEI " + juridica.CEI;
                            }
                            else
                            {
                                if (tomadora != string.Empty)
                                    newRow["CGC"] = juridica.NomeCodigo + "       " + tomadora;
                                else
                                    newRow["CGC"] = juridica.NomeCodigo;
                            }

                        }
                        else
                        {
                            juridica.IdJuridicaPai.Find();
                            if (tomadora != string.Empty)
                                newRow["CGC"] = juridica.IdJuridicaPai.NomeCodigo + "       " + tomadora;
                            else
                                newRow["CGC"] = juridica.IdJuridicaPai.NomeCodigo;
                        }
                    }
                    else
                    {
                        newRow["RazaoSocial"] = juridica.NomeCompleto + System.Environment.NewLine + juridica.NomeAbreviado;

                        newRow["Endereco"] = juridica.GetEndereco().GetEndereco();
                        newRow["Cidade"] = juridica.GetEndereco().GetCidade();
                        newRow["Estado"] = juridica.GetEndereco().GetEstado();
                        newRow["CEP"] = juridica.GetEndereco().Cep;

                        if (juridica.CEI != string.Empty)
                        {
                            if (tomadora != string.Empty)
                                newRow["CGC"] = juridica.NomeCodigo + "    " + tomadora + "    CEI " + juridica.CEI;
                            else
                                newRow["CGC"] = juridica.NomeCodigo + "    CEI " + juridica.CEI;
                        }
                        else
                        {
                            if (tomadora != string.Empty)
                                newRow["CGC"] = juridica.NomeCodigo + "       " + tomadora;
                            else
                                newRow["CGC"] = juridica.NomeCodigo;
                        }
                    }


                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("VIA") > 0)
                {

                    if (juridica.IdJuridicaPai.Id != 0 && juridica.IdJuridicaPai.Id != juridica.Id)
                    {
                        newRow["RazaoSocial"] = juridica.IdJuridicaPai.ToString() + "  Unidade: " + juridica.NomeAbreviado;


                        Cliente zCliente = new Cliente();
                        zCliente.Find(" Cliente.IdJuridica = " + juridica.Id);

                        if (zCliente.Endereco_Matriz == true)
                        {
                            newRow["Endereco"] = juridica.IdJuridicaPai.GetEndereco().GetEndereco();
                            newRow["Cidade"] = juridica.IdJuridicaPai.GetEndereco().GetCidade();
                            newRow["Estado"] = juridica.IdJuridicaPai.GetEndereco().GetEstado();
                            newRow["CEP"] = juridica.IdJuridicaPai.GetEndereco().Cep;
                        }
                        else
                        {
                            newRow["Endereco"] = juridica.GetEndereco().GetEndereco();
                            newRow["Cidade"] = juridica.GetEndereco().GetCidade();
                            newRow["Estado"] = juridica.GetEndereco().GetEstado();
                            newRow["CEP"] = juridica.GetEndereco().Cep;

                        }


                        if (zCliente.Id != 0 && zCliente.CNPJ_Matriz == false)
                        {
                            if (juridica.CEI != string.Empty)
                            {
                                if (tomadora != string.Empty)
                                    newRow["CGC"] = juridica.NomeCodigo + "    " + tomadora + "    CEI " + juridica.CEI;
                                else
                                    newRow["CGC"] = juridica.NomeCodigo + "    CEI " + juridica.CEI;
                            }
                            else
                            {
                                if (tomadora != string.Empty)
                                    newRow["CGC"] = juridica.NomeCodigo + "       " + tomadora;
                                else
                                    newRow["CGC"] = juridica.NomeCodigo;
                            }

                        }
                        else
                        {
                            if (tomadora != string.Empty)
                                newRow["CGC"] = juridica.IdJuridicaPai.NomeCodigo + "       " + tomadora;
                            else
                                newRow["CGC"] = juridica.IdJuridicaPai.NomeCodigo;
                        }
                    }
                    else
                    {
                        newRow["RazaoSocial"] = juridica.NomeCompleto + System.Environment.NewLine + juridica.NomeAbreviado;

                        newRow["Endereco"] = juridica.GetEndereco().GetEndereco();
                        newRow["Cidade"] = juridica.GetEndereco().GetCidade();
                        newRow["Estado"] = juridica.GetEndereco().GetEstado();
                        newRow["CEP"] = juridica.GetEndereco().Cep;

                        if (tomadora != string.Empty)
                            newRow["CGC"] = juridica.NomeCodigo + "       " + tomadora;
                        else
                            newRow["CGC"] = juridica.NomeCodigo;
                                              
                    }
                }

                else
                {
                    newRow["RazaoSocial"] = System.Environment.NewLine + juridica.NomeCompleto;
                    newRow["Endereco"] = juridica.GetEndereco().GetEndereco();
                    newRow["Cidade"] = juridica.GetEndereco().GetCidade();
                    newRow["Estado"] = juridica.GetEndereco().GetEstado();
                    newRow["CEP"] = juridica.GetEndereco().Cep;

                    if (juridica.IdJuridicaPai.Id != 0 && juridica.IdJuridicaPai.Id != juridica.Id)
                    {
                        Cliente zCliente = new Cliente();
                        zCliente.Find(" Cliente.IdJuridica = " + juridica.Id);

                        if (zCliente.Id != 0 && zCliente.CNPJ_Matriz == false)
                        {
                            if (juridica.CEI != string.Empty)
                            {
                                if (tomadora != string.Empty)
                                    newRow["CGC"] = juridica.NomeCodigo + "    " + tomadora + "    CEI " + juridica.CEI;
                                else
                                    newRow["CGC"] = juridica.NomeCodigo + "    CEI " + juridica.CEI;
                            }
                            else
                            {
                                if (tomadora != string.Empty)
                                    newRow["CGC"] = juridica.NomeCodigo + "       " + tomadora;
                                else
                                    newRow["CGC"] = juridica.NomeCodigo;
                            }

                        }
                        else
                        {
                            juridica.IdJuridicaPai.Find();
                            if (tomadora != string.Empty)
                                newRow["CGC"] = juridica.IdJuridicaPai.NomeCodigo + "       " + tomadora;
                            else
                                newRow["CGC"] = juridica.IdJuridicaPai.NomeCodigo;
                        }

                    }
                    else
                    {
                        if (juridica.CEI != string.Empty)
                        {
                            if (tomadora != string.Empty)
                                newRow["CGC"] = juridica.NomeCodigo + "    " + tomadora + "    CEI " + juridica.CEI;
                            else
                                newRow["CGC"] = juridica.NomeCodigo + "    CEI " + juridica.CEI;
                        }
                        else
                        {
                            if (tomadora != string.Empty)
                                newRow["CGC"] = juridica.NomeCodigo + "       " + tomadora;
                            else
                                newRow["CGC"] = juridica.NomeCodigo;
                        }
                    }


                    //if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("EY") > 0)
                    //{
                    //    newRow["ClinicaCidade"] = juridica.NomeCompleto + System.Environment.NewLine + juridica.GetEndereco().GetEndereco() + System.Environment.NewLine + "CEP:" + juridica.GetEndereco().Cep + "   " + juridica.GetEndereco().GetCidade() + " / " + juridica.GetEndereco().GetEstado() + "  CNPJ: " + juridica.NomeCodigo;
                    //}
                    //else
                    //{

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                        newRow["ClinicaCidade"] = juridica.GetEndereco().GetCidade();
                    else
                        newRow["ClinicaCidade"] = "";


                    //}



                }
            }
            #endregion

            #region CoordenadorPcmso

            public void CoordenadorPcmso(string sNomeCoordenador)
            {
                newRow["tMEDICO_COORD_PCMSO"] = sNomeCoordenador;
            }

            #endregion
        }

        #endregion

        #endregion

        #region DataSourcePci

        #region DataSourcePci

        private void DataSourcePci(Clinico clinico)
        {
            DataRow newRow;

            newRow = dsPci.Tables[0].NewRow();

            int count = new AdendoExame().ExecuteCount("IdExameBase=" + clinico.Id);

            bool HasAdendo = count > 0;

            PopularRowCabecalho(clinico, newRow, HasAdendo);

            PopularRowAnamnese(clinico, newRow);

            PopularRowExameFisico(clinico, newRow);

            dsPci.Tables[0].Rows.Add(newRow);
        }
        #endregion

        #region PopularRowCabecalho

        private void PopularRowCabecalho(Clinico clinico, DataRow newRow, bool HasAdendo)
        {
            //Para aso e pci em branco
            if (clinico.Id == 0)
            {
                if (cliente.Id != 0)
                {
                    newRow["IdExame"] = cliente.Id.ToString();
                    newRow["Cidade"] = cliente.GetEndereco().GetCidade();
                    newRow["hasAdendo"] = false;
                }
            }
            else
            {
                newRow["IdExame"] = clinico.Id.ToString();
                newRow["hasAso"] = PciComAso;
                newRow["hasAdendo"] = HasAdendo;


                string zObs = "";


                switch (clinico.IdEmpregado.nIND_BENEFICIARIO)
                {
                    case (int)TipoBeneficiario.BeneficiarioReabilitado:
                        zObs = "( Beneficiário Reabilitado )      ";
                        break;
                    case (int)TipoBeneficiario.PortadorDeficiencia:
                        zObs = "( Pessoa com deficiência )     ";
                        break;
                    case (int)TipoBeneficiario.NaoAplicavel:
                        zObs = "";
                        break;
                    default:
                        zObs = "";
                        break;
                }

                newRow["Observacao"] = zObs + clinico.Prontuario;


                newRow["nRS_EXAME"] = clinico.IndResultado.ToString();
                newRow["dDT_EXAME"] = clinico.DataExame.ToString("dd-MM-yyyy") + ".";
                //newRow["Cidade"] = clinico.IdJuridica.GetEndereco().GetCidade();
                newRow["Cidade"] = cliente.GetEndereco().GetCidade();

                //Para empresas que eram "PCMSO não contratada" e que agora são.
                if (clinico.IdMedico.Id != 0 && clinico.IdMedico.Id != (int)Medicos.PcmsoNaoContratada)
                {
                    if (clinico.IdMedico.NomeCompleto.Trim().ToUpper() != "INDEFINIDO")
                    {
                        newRow["tNOME_MEDICO"] = clinico.IdMedico.NomeCompleto;
                        newRow["tTITULO_MEDICO"] = clinico.IdMedico.Titulo + " " + clinico.IdMedico.Numero;
                    }
                    else
                    {
                        newRow["tNOME_MEDICO"] = "";
                        newRow["tTITULO_MEDICO"] = "";
                    }
                }
            }
        }
        #endregion

        #region PopularRowExameFisico

        private void PopularRowExameFisico(Clinico clinico, DataRow newRow)
        {
            
            ExameFisico exameFisico;

            exameFisico = new ExameFisico();
            exameFisico.Find("IdExameBase=" + clinico.Id);

            if (exameFisico == null)
                return;
            
            //ExameFisico
            newRow["PressaoArterial"] = exameFisico.PressaoArterial.ToString();

            newRow["Coord_PCMSO"] = GetCoordenadorPCMSO(cliente, pcmso);


            //vou usar esse campo para inibir quadrado "Em espera"
            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                newRow["Franquia"] = "DAITI";
            else
                newRow["Franquia"] = "OUTRAS";


            if (exameFisico.Pulso != 0)
                newRow["Pulso"] = exameFisico.Pulso.ToString();

            if (exameFisico.Altura != 0)
                newRow["Altura"] = exameFisico.Altura.ToString("F");

            if (exameFisico.Peso != 0)
                newRow["Peso"] = exameFisico.Peso.ToString();



            if (exameFisico.DataUltimaMenstruacao != new DateTime() && exameFisico.DataUltimaMenstruacao != new DateTime(1753, 1, 1))
                newRow["DataUltimaMenstruacao"] = exameFisico.DataUltimaMenstruacao.ToString("dd-MM-yyyy");

            string xRespSim = "Sim";
            string xRespNao = "Não";




            if ( Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0  || Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("EY") > 0)
            {
                //exameFisico.IdExameBase.Find();
                //exameFisico.IdExameBase.IdEmpregado.Find();
                //exameFisico.IdExameBase.IdEmpregado.nID_EMPR.Find();
                //exameFisico.IdExameBase.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();
                //if (exameFisico.IdExameBase.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI")
                //{
                    xRespSim = "Alterado";
                    xRespNao = "Não Alterado";

                    //usar este campo para IMC
                    if (exameFisico.Peso > 0 && exameFisico.Altura > 0)
                    {
                        float zIMC = 0;

                        if (exameFisico.Altura < 100 )
                           zIMC = ( exameFisico.Peso  / (exameFisico.Altura * exameFisico.Altura));
                        else
                           zIMC = (exameFisico.Peso / ((exameFisico.Altura/100) * (exameFisico.Altura/100)));

                        newRow["nRS_Exame"] = zIMC.ToString("#.##"); 
                    }
                    else
                        newRow["nRS_Exame"] = "";

                //}
            }


            if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("EY") > 0)
            {

                if (exameFisico.Sistolica.ToString().Trim() != "" && exameFisico.Sistolica.ToString().Trim() != "0")
                    newRow["PressaoArterial"] = exameFisico.Sistolica.ToString().Trim() + " x " + exameFisico.Diastolica.ToString().Trim();
                else
                    newRow["PressaoArterial"] = "";


                string xOlhos = "";
                string xCabeca = "";
                string xPele = "";
                string xRespiratorio = "";
                string xCardio = "";
                string xAbdome = "";
                string xMI = "";
                string xMS = "";
                string xColuna = "";
                string xMaos = "";

                string xTinel = "";
                string xPreensao = "";
                string xFinkelstein = "";
                string xPhalen = "";
                string xNodulos = "";
                string xCrepitacao = "";
                string xEdema = "";

                newRow["hasPeleAlterado"] = exameFisico.PeleAnexosAlterado;
                newRow["CircunferenciaAbdominal"] = exameFisico.CircunferenciaAbdominal;




                if (exameFisico.hasOlhosNormal == (short)StatusAnamnese.Sim)
                    newRow["OlhosNormal"] = "X";
                //xOlhos = xOlhos + "Normal / ";

                if (exameFisico.hasOculosLentes == (short)StatusAnamnese.Sim)
                    newRow["OlhosOculos"] = "X";
                //xOlhos = xOlhos + "Uso de óculos/lentes / ";

                //if (exameFisico.OlhosAlterado.Trim() != "")
                //    xOlhos = xOlhos + exameFisico.OlhosAlterado.Trim();

                //if (xOlhos != "")
                //{
                //    if (xOlhos.Substring(xOlhos.Length - 2, 1) == "/")
                //        xOlhos = xOlhos.Substring(0, xOlhos.Length - 2);
                //}

                //newRow["hasOsteoAlterado"] = xOlhos; //exameFisico.OsteoAlterado;
                newRow["hasOsteoAlterado"] = exameFisico.OsteoAlterado.Trim();





                if (exameFisico.hasCabecaNormal == (short)StatusAnamnese.Sim)
                    newRow["CabecaNormal"] = "X";
                //xCabeca = xCabeca + "Normal / ";

                if (exameFisico.hasTireoide == (short)StatusAnamnese.Sim)
                    newRow["CabecaTireoide"] = "X";
                //xCabeca = xCabeca + "Tireóide alterada / ";

                if (exameFisico.hasDente == (short)StatusAnamnese.Sim)
                    newRow["CabecaDente"] = "X";
                //xCabeca = xCabeca + "Dente alterado / ";

                if (exameFisico.hasGarganta == (short)StatusAnamnese.Sim)
                    newRow["CabecaGarganta"] = "X";
                //xCabeca = xCabeca + "Garganta alterada / ";

                if (exameFisico.hasAdenomegalia == (short)StatusAnamnese.Sim)
                    newRow["CabecaAdenomegalia"] = "X";
                //xCabeca = xCabeca + "Adenomegalia / ";

                if (exameFisico.hasOuvido == (short)StatusAnamnese.Sim)
                    newRow["CabecaOuvido"] = "X";
                //xCabeca = xCabeca + "Ouvido Alterado / ";

                //if (exameFisico.CabecaAlterado.Trim() != "")
                //    xCabeca = xCabeca + exameFisico.CabecaAlterado.Trim();

                //if (xCabeca != "")
                //{
                //    if (xCabeca.Substring(xCabeca.Length - 2, 1) == "/")
                //        xCabeca = xCabeca.Substring(0, xCabeca.Length - 2);
                //}

                //newRow["hasCabecaAlterado"] = xCabeca; //exameFisico.CabecaAlterado;
                newRow["hasCabecaAlterado"] = exameFisico.CabecaAlterado.Trim();





                if (exameFisico.hasPeleNormal == (short)StatusAnamnese.Sim)
                    newRow["PeleNormal"] = "X";
                //xPele = xPele + "Normal / ";

                if (exameFisico.hasDescoradas == (short)StatusAnamnese.Sim)
                    newRow["PeleDescoradas"] = "X";
                //xPele = xPele + "Descoradas / ";

                if (exameFisico.hasIctericas == (short)StatusAnamnese.Sim)
                    newRow["PeleIctericas"] = "X";
                //xPele = xPele + "Ictéricas / ";

                if (exameFisico.hasCianoticas == (short)StatusAnamnese.Sim)
                    newRow["PeleCianoticas"] = "X";
                //xPele = xPele + "Cianóticas / ";

                if (exameFisico.hasDermatoses == (short)StatusAnamnese.Sim)
                    newRow["PeleDermatoses"] = "X";
                //xPele = xPele + "Dermatoses / ";

                if (exameFisico.PeleAnexosAlterado.Trim() != "")
                    newRow["MVsemRAAlterado"] = exameFisico.PeleAnexosAlterado.Trim();
                //xPele = xPele + exameFisico.PeleAnexosAlterado.Trim();


                //if (xPele != "")
                //{
                //    if (xPele.Substring(xPele.Length - 2, 1) == "/")
                //        xPele = xPele.Substring(0, xPele.Length - 2);
                //}

                //newRow["MVsemRAAlterado"] = xPele; // exameFisico.MVsemRAAlterado;




                if (exameFisico.hasMVsemRA == (short)StatusAnamnese.Sim)
                    newRow["RespiratorioMVsemRA"] = "X";
                //xRespiratorio = xRespiratorio + "MV+ sem RA / ";

                if (exameFisico.hasMVAlterado == (short)StatusAnamnese.Sim)
                    newRow["RespiratorioMVAlterado"] = "X";
                //xRespiratorio = xRespiratorio + "MV Alterado / ";

                if (exameFisico.hasAdventicios == (short)StatusAnamnese.Sim)
                    newRow["RespiratorioRuidos"] = "X";
                //xRespiratorio = xRespiratorio + "Presença de ruídos adventícios / ";

                //if (exameFisico.PulmaoAlterado.Trim() != "")
                //    xRespiratorio = xRespiratorio + exameFisico.PulmaoAlterado.Trim();

                //if (xRespiratorio != "")
                //{
                //    if (xRespiratorio.Substring(xRespiratorio.Length - 2, 1) == "/")
                //        xRespiratorio = xRespiratorio.Substring(0, xRespiratorio.Length - 2);
                //}

                //newRow["hasPulmaoAlterado"] = xRespiratorio; //exameFisico.PulmaoAlterado;
                newRow["hasPulmaoAlterado"] = exameFisico.PulmaoAlterado.Trim();




                if (exameFisico.hasBRNF == (short)StatusAnamnese.Sim)
                    newRow["CardioBRNF"] = "X";
                //xCardio = xCardio + "BRNF em 2t sem SA / ";

                if (exameFisico.hasArritmias == (short)StatusAnamnese.Sim)
                    newRow["CardioArritmias"] = "X";
                //xCardio = xCardio + "Arritmias / ";

                if (exameFisico.hasSopros == (short)StatusAnamnese.Sim)
                    newRow["CardioSopros"] = "X";
                //xCardio = xCardio + "Sopros / ";

                if (exameFisico.hasCardioOutros == (short)StatusAnamnese.Sim)
                    newRow["CardioOutros"] = "X";
                //xCardio = xCardio + "Outros / ";

                //if (exameFisico.CoracaoAlterado.Trim() != "")
                //    xCardio = xCardio + exameFisico.CoracaoAlterado.Trim();

                //if (xCardio != "")
                //{
                //    if (xCardio.Substring(xCardio.Length - 2, 1) == "/")
                //        xCardio = xCardio.Substring(0, xCardio.Length - 2);
                //}

                //newRow["hasCoracaoAlterado"] = xCardio;  //exameFisico.CoracaoAlterado;
                newRow["hasCoracaoAlterado"] = exameFisico.CoracaoAlterado.Trim();



                if (exameFisico.hasPlano == (short)StatusAnamnese.Sim)
                    newRow["AbdomePlano"] = "X";
                //xAbdome = xAbdome + "plano, DB-, RHA+, indolor / ";

                if (exameFisico.hasPalpacao == (short)StatusAnamnese.Sim)
                    newRow["AbdomePalpacao"] = "X";
                //xAbdome = xAbdome + "dor à palpação / ";

                if (exameFisico.hasHepatoesplenomegalia == (short)StatusAnamnese.Sim)
                    newRow["AbdomeHepato"] = "X";
                //xAbdome = xAbdome + "Hepatoesplenomegalia / ";

                if (exameFisico.hasMassas == (short)StatusAnamnese.Sim)
                    newRow["AbdomeMassas"] = "X";
                //xAbdome = xAbdome + "massas palpáveis / ";

                //if (exameFisico.AbdomemAlterado.Trim() != "")
                //    xAbdome = xAbdome + exameFisico.AbdomemAlterado.Trim();

                //if (xAbdome != "")
                //{
                //    if (xAbdome.Substring(xAbdome.Length - 2, 1) == "/")
                //        xAbdome = xAbdome.Substring(0, xAbdome.Length - 2);
                //}


                //newRow["hasAbdomemAlterado"] = xAbdome;  // exameFisico.AbdomemAlterado;
                newRow["hasAbdomemAlterado"] = exameFisico.AbdomemAlterado.Trim();








                if (exameFisico.hasMINormal == (short)StatusAnamnese.Sim)
                    newRow["MINormal"] = "X";
                //xMI = xMI + "normal / ";

                if (exameFisico.hasEdemas == (short)StatusAnamnese.Sim)
                    newRow["MIEdemas"] = "X";
                //xMI = xMI + "Edemas / ";

                if (exameFisico.hasPulsos == (short)StatusAnamnese.Sim)
                    newRow["MIPulsos"] = "X";
                //xMI = xMI + "pulsos periféricos ausentes / ";

                if (exameFisico.hasArticular == (short)StatusAnamnese.Sim)
                    newRow["MILimitacao"] = "X";
                //xMI = xMI + "limitação articular / ";

                if (exameFisico.hasVarizes == (short)StatusAnamnese.Sim)
                    newRow["MIVarizes"] = "X";
                //xMI = xMI + "varizes / ";

                //if (exameFisico.MIAlterado.Trim() != "")
                //    xMI = xMI + exameFisico.MIAlterado.Trim();

                //if (xMI != "")
                //{
                //    if (xMI.Substring(xMI.Length - 2, 1) == "/")
                //        xMI = xMI.Substring(0, xMI.Length - 2);
                //}

                //newRow["hasMIAlterado"] = xMI;  // exameFisico.MIAlterado;
                newRow["hasMIAlterado"] = exameFisico.MIAlterado.Trim();





                if (exameFisico.hasMSNormal == (short)StatusAnamnese.Sim)
                    newRow["OmbrosNormal"] = "X";
                //xMS = xMS + "normal / ";

                if (exameFisico.hasDorRotacao == (short)StatusAnamnese.Sim)
                    newRow["OmbrosRotacao"] = "X";
                //xMS = xMS + "dor a tortação (interna/externa) / ";

                if (exameFisico.hasPalpacaoBiceps == (short)StatusAnamnese.Sim)
                    newRow["OmbrosBiceps"] = "X";
                //xMS = xMS + "dor a palpação do bíceps / ";

                if (exameFisico.hasPalpacaoSubacromial == (short)StatusAnamnese.Sim)
                    newRow["OmbrosSubacromial"] = "X";
                //xMS = xMS + "dor a palpação subacromial / ";

                //if (exameFisico.MSAlterado.Trim() != "")
                //    xMS = xMS + exameFisico.MSAlterado.Trim();

                //if (xMS != "")
                //{
                //    if (xMS.Substring(xMS.Length - 2, 1) == "/")
                //        xMS = xMS.Substring(0, xMS.Length - 2);
                //}

                //newRow["hasMSAlterado"] = xMS; //exameFisico.MSAlterado;
                newRow["hasMSAlterado"] = exameFisico.MSAlterado;







                if (exameFisico.hasColunaNormal == (short)StatusAnamnese.Sim)
                    newRow["ColunaNormal"] = "X";
                //xColuna = xColuna + "normal / ";

                if (exameFisico.hasCifose == (short)StatusAnamnese.Sim)
                    newRow["ColunaCifose"] = "X";
                //xColuna = xColuna + "Cifose / ";

                if (exameFisico.hasLordose == (short)StatusAnamnese.Sim)
                    newRow["ColunaLordose"] = "X";
                //xColuna = xColuna + "Lordose / ";

                if (exameFisico.hasEscoliose == (short)StatusAnamnese.Sim)
                    newRow["ColunaEscoliose"] = "X";
                //xColuna = xColuna + "Escoliose / ";

                if (exameFisico.hasLasegue == (short)StatusAnamnese.Sim)
                    newRow["ColunaLasegue"] = "X";
                //xColuna = xColuna + "Laségue / ";

                if (exameFisico.hasLimitacaoFlexao == (short)StatusAnamnese.Sim)
                    newRow["ColunaFlexao"] = "X";
                //xColuna = xColuna + "Limitação de flexão / ";

                //if (exameFisico.ColunaVertebralAlterado.Trim() != "")
                //    xColuna = xColuna + exameFisico.MSAlterado.Trim();

                //if (xColuna != "")
                //{
                //    if (xColuna.Substring(xColuna.Length - 2, 1) == "/")
                //        xColuna = xColuna.Substring(0, xColuna.Length - 2);
                //}

                // newRow["ColunaVertebralAlterado"] = xColuna;  // exameFisico.ColunaVertebralAlterado;
                newRow["ColunaVertebralAlterado"] = exameFisico.ColunaVertebralAlterado;









                if (exameFisico.hasMaosNormal == (short)StatusAnamnese.Sim)
                    newRow["MaosNormal"] = "X";
                //xMaos = xMaos + "Normal / ";

                //if (exameFisico.MaosPunhosAlterado.Trim() != "")
                //    xMaos = xMaos + exameFisico.MaosPunhosAlterado.Trim();

                //if (xMaos != "")
                //{
                //    if (xMaos.Substring(xMaos.Length - 2, 1) == "/")
                //        xMaos = xMaos.Substring(0, xMaos.Length - 2);
                //}

                //newRow["BRNFAlterado"] = xMaos; //exameFisico.BRNFAlterado;
                newRow["BRNFAlterado"] = exameFisico.MaosPunhosAlterado;



                int zPosit = 0;






                if (exameFisico.hasTinelD == (short)StatusAnamnese.Sim)
                    newRow["TinelD"] = "X";
                //xTinel = xTinel + "Direito / ";

                if (exameFisico.hasTinelE == (short)StatusAnamnese.Sim)
                    newRow["TinelE"] = "X";
                //xTinel = xTinel + "Esquerdo / ";

                //if (xTinel != "")
                //{
                //    xTinel = "Teste de tinel: " + xTinel;
                //    zPosit++;

                //    if (xTinel.Substring(xTinel.Length - 2, 1) == "/")
                //        xTinel = xTinel.Substring(0, xTinel.Length - 2);
                //}


                if (exameFisico.hasPreensaoD == (short)StatusAnamnese.Sim)
                    newRow["PrensaoD"] = "X";
                //xPreensao = xPreensao + "Direito / ";

                if (exameFisico.hasPreensaoE == (short)StatusAnamnese.Sim)
                    newRow["PrensaoE"] = "X";
                //xPreensao = xPreensao + "Esquerdo / ";

                //if (xPreensao != "")
                //{
                //    xPreensao = "Perda da capacidade de preensão: " + xPreensao;
                //    zPosit++;

                //    if (xPreensao.Substring(xPreensao.Length - 2, 1) == "/")
                //        xPreensao = xPreensao.Substring(0, xPreensao.Length - 2);
                //}









                if (exameFisico.hasFinkelsteinD == (short)StatusAnamnese.Sim)
                    newRow["FinklsteinD"] = "X";
                //xFinkelstein = xFinkelstein + "Direito / ";

                if (exameFisico.hasFinkelsteinE == (short)StatusAnamnese.Sim)
                    newRow["FinklsteinE"] = "X";
                //xFinkelstein = xFinkelstein + "Esquerdo / ";

                //if (xFinkelstein != "")
                //{
                //    xFinkelstein = "Teste de Finkelstein: " + xFinkelstein;
                //    zPosit++;

                //    if (xFinkelstein.Substring(xFinkelstein.Length - 2, 1) == "/")
                //        xFinkelstein = xFinkelstein.Substring(0, xFinkelstein.Length - 2);
                //}



                if (exameFisico.hasPhalenD == (short)StatusAnamnese.Sim)
                    newRow["PhalenD"] = "X";
                //xPhalen = xPhalen + "Direito / ";

                if (exameFisico.hasPhalenE == (short)StatusAnamnese.Sim)
                    newRow["PhalenE"] = "X";
                //xPhalen = xPhalen + "Esquerdo / ";

                //if (xPhalen != "")
                //{
                //    xPhalen = "Manobra de Phalen: " + xPhalen;
                //    zPosit++;

                //    if (xPhalen.Substring(xPhalen.Length - 2, 1) == "/")
                //        xPhalen = xPhalen.Substring(0, xPhalen.Length - 2);
                //}




                if (exameFisico.hasNodulosD == (short)StatusAnamnese.Sim)
                    newRow["NodulosD"] = "X";
                // xNodulos = xNodulos + "Direito / ";


                if (exameFisico.hasNodulosE == (short)StatusAnamnese.Sim)
                    newRow["NodulosE"] = "X";
                //xNodulos = xNodulos + "Esquerdo / ";

                //if (xNodulos != "")
                //{
                //    xNodulos = "Nódulo/cistos: " + xNodulos;
                //    zPosit++;

                //    if (xNodulos.Substring(xNodulos.Length - 2, 1) == "/")
                //        xNodulos = xNodulos.Substring(0, xNodulos.Length - 2);
                //}






                if (exameFisico.hasCrepitacaoD == (short)StatusAnamnese.Sim)
                    newRow["CrepitacaoD"] = "X";
                //xCrepitacao = xCrepitacao + "Direito / ";

                if (exameFisico.hasCrepitacaoE == (short)StatusAnamnese.Sim)
                    newRow["CrepitacaoE"] = "X";
                //xCrepitacao = xCrepitacao + "Esquerdo / ";

                //if (xCrepitacao != "")
                //{
                //    xCrepitacao = "Crepitação: " + xCrepitacao;
                //    zPosit++;

                //    if (xCrepitacao.Substring(xCrepitacao.Length - 2, 1) == "/")
                //        xCrepitacao = xCrepitacao.Substring(0, xCrepitacao.Length - 2);
                //}



                if (exameFisico.hasEdemaD == (short)StatusAnamnese.Sim)
                    newRow["EdemaD"] = "X";
                //xEdema = xEdema + "Direito / ";

                if (exameFisico.hasEdemaE == (short)StatusAnamnese.Sim)
                    newRow["EdemaE"] = "X";
                //xEdema = xEdema + "Esquerdo / ";

                //if (xEdema != "")
                //{
                //    xEdema = "Edema: " + xEdema;
                //    zPosit++;

                //    if (xEdema.Substring(xEdema.Length - 2, 1) == "/")
                //        xEdema = xEdema.Substring(0, xEdema.Length - 2);
                //}



                //if (zPosit > 0)
                //{

                //    String[] sMaos = new String[zPosit];

                //    zPosit = 0;

                //    if (xTinel != "")
                //    {
                //        sMaos[zPosit] = xTinel;
                //        zPosit++;
                //    }

                //    if (xPreensao != "")
                //    {
                //        sMaos[zPosit] = xPreensao;
                //        zPosit++;
                //    }

                //    if (xFinkelstein != "")
                //    {
                //        sMaos[zPosit] = xFinkelstein;
                //        zPosit++;
                //    }

                //    if (xPhalen != "")
                //    {
                //        sMaos[zPosit] = xPhalen;
                //        zPosit++;
                //    }

                //    if (xNodulos != "")
                //    {
                //        sMaos[zPosit] = xNodulos;
                //        zPosit++;
                //    }

                //    if (xCrepitacao != "")
                //    {
                //        sMaos[zPosit] = xCrepitacao;
                //        zPosit++;
                //    }

                //    if (xEdema != "")
                //    {
                //        sMaos[zPosit] = xEdema;
                //        zPosit++;
                //    }

                //    if (zPosit > 0)
                //        newRow["PlanoAlterado"] = sMaos[0];
                //    else
                //        newRow["PlanoAlterado"] = "";

                //    if (zPosit > 1)
                //        newRow["DBAlterado"] = sMaos[1];
                //    else
                //        newRow["DBAlterado"] = "";

                //    if (zPosit > 2)
                //        newRow["RHAAlterado"] = sMaos[2];
                //    else
                //        newRow["RHAAlterado"] = "";

                //    if (zPosit > 3)
                //        newRow["IndolorAlterado"] = sMaos[3];
                //    else
                //        newRow["IndolorAlterado"] = "";

                //    if (zPosit > 4)
                //        newRow["AbaulamentosAlterado"] = sMaos[4];
                //    else
                //        newRow["AbaulamentosAlterado"] = "";


                //    if (zPosit > 5)
                //    {
                //        newRow["IndolorAlterado"] = sMaos[3] + "  |  " + sMaos[4];
                //        newRow["AbaulamentosAlterado"] = sMaos[5];
                //    }


                //    if (zPosit > 6)
                //    {
                //        newRow["IndolorAlterado"] = sMaos[3] + "  |  " + sMaos[4];
                //        newRow["AbaulamentosAlterado"] = sMaos[5] + "  |  " + sMaos[6];
                //    }


                //}
                //else
                //{

                //    newRow["PlanoAlterado"] = "";
                //    newRow["DBAlterado"] = "";
                //    newRow["RHAAlterado"] = "";
                //    newRow["IndolorAlterado"] = "";
                //    newRow["AbaulamentosAlterado"] = "";

                //}



            }
            else
            {
                if (exameFisico.hasCabecaAlterado == (short)StatusAnamnese.Sim)
                    newRow["hasCabecaAlterado"] = xRespSim;
                else if (exameFisico.hasCabecaAlterado == (short)StatusAnamnese.Nao)
                    newRow["hasCabecaAlterado"] = xRespNao;
                else if (exameFisico.hasCabecaAlterado == (short)StatusAnamnese.NaoPreenchido)
                    newRow["hasCabecaAlterado"] = "";


                if (exameFisico.hasCoracaoAlterado == (short)StatusAnamnese.Sim)
                    newRow["hasCoracaoAlterado"] = xRespSim;
                else if (exameFisico.hasCoracaoAlterado == (short)StatusAnamnese.Nao)
                    newRow["hasCoracaoAlterado"] = xRespNao;
                else if (exameFisico.hasCoracaoAlterado == (short)StatusAnamnese.NaoPreenchido)
                    newRow["hasCoracaoAlterado"] = "";


                if (exameFisico.hasPulmaoAlterado == (short)StatusAnamnese.Sim)
                    newRow["hasPulmaoAlterado"] = xRespSim;
                else if (exameFisico.hasPulmaoAlterado == (short)StatusAnamnese.Nao)
                    newRow["hasPulmaoAlterado"] = xRespNao;
                else if (exameFisico.hasPulmaoAlterado == (short)StatusAnamnese.NaoPreenchido)
                    newRow["hasPulmaoAlterado"] = "";

                if (exameFisico.hasMIAlterado == (short)StatusAnamnese.Sim)
                    newRow["hasMIAlterado"] = xRespSim;
                else if (exameFisico.hasMIAlterado == (short)StatusAnamnese.Nao)
                    newRow["hasMIAlterado"] = xRespNao;
                else if (exameFisico.hasMIAlterado == (short)StatusAnamnese.NaoPreenchido)
                    newRow["hasMIAlterado"] = "";


                if (exameFisico.hasPeleAnexosAlterado == (short)StatusAnamnese.Sim)
                    newRow["hasPeleAlterado"] = xRespSim;
                else if (exameFisico.hasPeleAnexosAlterado == (short)StatusAnamnese.Nao)
                    newRow["hasPeleAlterado"] = xRespNao;
                else if (exameFisico.hasPeleAnexosAlterado == (short)StatusAnamnese.NaoPreenchido)
                    newRow["hasPeleAlterado"] = "";


                if (exameFisico.hasMSAlterado == (short)StatusAnamnese.Sim)
                    newRow["hasMSAlterado"] = xRespSim;
                else if (exameFisico.hasMSAlterado == (short)StatusAnamnese.Nao)
                    newRow["hasMSAlterado"] = xRespNao;
                else if (exameFisico.hasMSAlterado == (short)StatusAnamnese.NaoPreenchido)
                    newRow["hasMSAlterado"] = "";


                if (exameFisico.hasAbdomemAlterado == (short)StatusAnamnese.Sim)
                    newRow["hasAbdomemAlterado"] = xRespSim;
                else if (exameFisico.hasAbdomemAlterado == (short)StatusAnamnese.Nao)
                    newRow["hasAbdomemAlterado"] = xRespNao;
                else if (exameFisico.hasAbdomemAlterado == (short)StatusAnamnese.NaoPreenchido)
                    newRow["hasAbdomemAlterado"] = "";




                if (exameFisico.hasOsteoAlterado == (short)StatusAnamnese.Sim)
                    newRow["hasOsteoAlterado"] = xRespSim;
                else if (exameFisico.hasOsteoAlterado == (short)StatusAnamnese.Nao)
                    newRow["hasOsteoAlterado"] = xRespNao;
                else if (exameFisico.hasOsteoAlterado == (short)StatusAnamnese.NaoPreenchido)
                    newRow["hasOsteoAlterado"] = "";
            }






            newRow["tMEDICO_COORD_PCMSO"] = GetCoordenadorPCMSO(cliente, pcmso);

            newRow["Coord_PCMSO"] = GetCoordenadorPCMSO(cliente, pcmso);

            //newRow["Observacao"] = clinico.ObservacaoResultado;
            newRow["Observacao"] = clinico.Prontuario;




            if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("EY") > 0 && clinico.IdJuridica.Id != 0)
            {
                string xFone = clinico.IdJuridica.GetContatoTelefonico().GetDDDTelefone();
                string xMail = clinico.IdJuridica.Email.Trim();

                //if (Ilitera.Data.SQLServer.EntitySQLServer.xLocalServer.ToUpper().IndexOf("EY") > 0)
                //{
                //    newRow["tCONTATO_MEDICO"] = "";
                //    return;
                //}


                if (xFone.Trim() != "" && xMail.Trim() != "")
                {
                    if (xMail.Length > 40)
                    {
                        if (xMail.Length > 64) xMail = xMail.Substring(0, 64);

                        string xNome = clinico.IdJuridica.NomeAbreviado.Trim();
                        if (xNome.Length > 38) xNome = xNome.Substring(0, 38);

                        newRow["tCONTATO_MEDICO"] = xNome + "   Fone: " + clinico.IdJuridica.GetContatoTelefonico().GetDDDTelefone() + System.Environment.NewLine + "e-mail: " + xMail;
                    }
                    else
                    {
                        if (xMail.Length > 40) xMail = xMail.Substring(0, 40);
                        newRow["tCONTATO_MEDICO"] = clinico.IdJuridica.NomeAbreviado + System.Environment.NewLine + "Fone: " + clinico.IdJuridica.GetContatoTelefonico().GetDDDTelefone() + "  e-mail: " + xMail;
                    }
                }
                else if (xFone.Trim() == "" && xMail.Trim() != "")
                {
                    if (xMail.Length > 64) xMail = xMail.Substring(0, 64);
                    newRow["tCONTATO_MEDICO"] = clinico.IdJuridica.NomeAbreviado + System.Environment.NewLine + "e-mail: " + xMail;
                }
                else if (xFone.Trim() != "" && xMail.Trim() == "")
                    newRow["tCONTATO_MEDICO"] = clinico.IdJuridica.NomeAbreviado + System.Environment.NewLine + "Fone: " + clinico.IdJuridica.GetContatoTelefonico().GetDDDTelefone();
                else
                    newRow["tCONTATO_MEDICO"] = "";

            }
            else
            {
                newRow["tCONTATO_MEDICO"] = " ";
            }





            newRow["AP_Penultima_Empresa"] = exameFisico.AP_Penultima_Empresa;
            newRow["AP_Penultima_Funcao"] = exameFisico.AP_Penultima_Funcao;
            newRow["AP_Penultima_Tempo"] = exameFisico.AP_Penultima_Tempo;
            newRow["AP_Ultima_Empresa"] = exameFisico.AP_Ultima_Empresa;
            newRow["AP_Ultima_Funcao"] = exameFisico.AP_Ultima_Funcao;
            newRow["AP_Ultima_Tempo"] = exameFisico.AP_Ultima_Tempo;


        }
        #endregion

        #region PopularRowAnamnese

        private void PopularRowAnamnese(Clinico clinico, DataRow newRow)
        {
            Anamnese anamnese;

            anamnese = new Anamnese();
            anamnese.Find("IdExameBase=" + clinico.Id);

            if (anamnese == null)
                return;

            // Anamnese
            if (anamnese.HasQueixasAtuais == (short)StatusAnamnese.Sim)
                newRow["HasQueixasAtuais"] = "SIM";
            else if (anamnese.HasQueixasAtuais == (short)StatusAnamnese.Nao)
                newRow["HasQueixasAtuais"] = "NÃO";

            if (anamnese.HasAfastamento == (short)StatusAnamnese.Sim)
                newRow["HasAfastamento"] = "SIM";
            else if (anamnese.HasAfastamento == (short)StatusAnamnese.Nao)
                newRow["HasAfastamento"] = "NÃO";

            if (anamnese.HasTraumatismos == (short)StatusAnamnese.Sim)
                newRow["HasTraumatismos"] = "SIM";
            else if (anamnese.HasTraumatismos == (short)StatusAnamnese.Nao)
                newRow["HasTraumatismos"] = "NÃO";

            if (anamnese.HasCirurgia == (short)StatusAnamnese.Sim)
                newRow["HasCirurgia"] = "SIM";
            else if (anamnese.HasCirurgia == (short)StatusAnamnese.Nao)
                newRow["HasCirurgia"] = "NÃO";

            if (anamnese.HasMedicacoes == (short)StatusAnamnese.Sim)
                newRow["HasMedicacoes"] = "SIM";
            else if (anamnese.HasMedicacoes == (short)StatusAnamnese.Nao)
                newRow["HasMedicacoes"] = "NÃO";

            if (anamnese.HasAntecedentes == (short)StatusAnamnese.Sim)
                newRow["HasAntecedentes"] = "SIM";
            else if (anamnese.HasAntecedentes == (short)StatusAnamnese.Nao)
                newRow["HasAntecedentes"] = "NÃO";

            if (anamnese.HasTabagismo == (short)StatusAnamnese.Sim)
                newRow["HasTabagismo"] = "SIM";
            else if (anamnese.HasTabagismo == (short)StatusAnamnese.Nao)
                newRow["HasTabagismo"] = "NÃO";

            if (anamnese.HasAlcoolismo == (short)StatusAnamnese.Sim)
                newRow["HasAlcoolismo"] = "SIM";
            else if (anamnese.HasAlcoolismo == (short)StatusAnamnese.Nao)
                newRow["HasAlcoolismo"] = "NÃO";

            if (anamnese.HasDeficienciaFisica == (short)StatusAnamnese.Sim)
                newRow["HasDeficienciaFisica"] = "SIM";
            else if (anamnese.HasDeficienciaFisica == (short)StatusAnamnese.Nao)
                newRow["HasDeficienciaFisica"] = "NÃO";

            if (anamnese.HasDoencaCronica == (short)StatusAnamnese.Sim)
                newRow["HasDoencaCronica"] = "SIM";
            else if (anamnese.HasDoencaCronica == (short)StatusAnamnese.Nao)
                newRow["HasDoencaCronica"] = "NÃO";


            if (anamnese.HasBronquite == (short)StatusAnamnese.Sim)
                newRow["HasBronquite"] = "SIM";
            else if (anamnese.HasBronquite == (short)StatusAnamnese.Nao)
                newRow["HasBronquite"] = "NÃO";

            if (anamnese.HasDigestiva == (short)StatusAnamnese.Sim)
                newRow["HasDigestiva"] = "SIM";
            else if (anamnese.HasDigestiva == (short)StatusAnamnese.Nao)
                newRow["HasDigestiva"] = "NÃO";

            if (anamnese.HasEstomago == (short)StatusAnamnese.Sim)
                newRow["HasEstomago"] = "SIM";
            else if (anamnese.HasEstomago == (short)StatusAnamnese.Nao)
                newRow["HasEstomago"] = "NÃO";

            if (anamnese.HasEnxerga == (short)StatusAnamnese.Sim)
                newRow["HasEnxerga"] = "SIM";
            else if (anamnese.HasEnxerga == (short)StatusAnamnese.Nao)
                newRow["HasEnxerga"] = "NÃO";

            if (anamnese.HasDorCabeca == (short)StatusAnamnese.Sim)
                newRow["HasDorCabeca"] = "SIM";
            else if (anamnese.HasDorCabeca == (short)StatusAnamnese.Nao)
                newRow["HasDorCabeca"] = "NÃO";

            if (anamnese.HasDesmaio == (short)StatusAnamnese.Sim)
                newRow["HasDesmaio"] = "SIM";
            else if (anamnese.HasDesmaio == (short)StatusAnamnese.Nao)
                newRow["HasDesmaio"] = "NÃO";

            if (anamnese.HasCoracao == (short)StatusAnamnese.Sim)
                newRow["HasCoracao"] = "SIM";
            else if (anamnese.HasCoracao == (short)StatusAnamnese.Nao)
                newRow["HasCoracao"] = "NÃO";

            if (anamnese.HasUrinaria == (short)StatusAnamnese.Sim)
                newRow["HasUrinaria"] = "SIM";
            else if (anamnese.HasUrinaria == (short)StatusAnamnese.Nao)
                newRow["HasUrinaria"] = "NÃO";

            if (anamnese.HasDiabetes == (short)StatusAnamnese.Sim)
                newRow["HasDiabetes"] = "SIM";
            else if (anamnese.HasDiabetes == (short)StatusAnamnese.Nao)
                newRow["HasDiabetes"] = "NÃO";

            if (anamnese.HasGripado == (short)StatusAnamnese.Sim)
                newRow["HasGripado"] = "SIM";
            else if (anamnese.HasGripado == (short)StatusAnamnese.Nao)
                newRow["HasGripado"] = "NÃO";

            if (anamnese.HasEscuta == (short)StatusAnamnese.Sim)
                newRow["HasEscuta"] = "SIM";
            else if (anamnese.HasEscuta == (short)StatusAnamnese.Nao)
                newRow["HasEscuta"] = "NÃO";

            if (anamnese.HasDoresCosta == (short)StatusAnamnese.Sim)
                newRow["HasDoresCosta"] = "SIM";
            else if (anamnese.HasDoresCosta == (short)StatusAnamnese.Nao)
                newRow["HasDoresCosta"] = "NÃO";

            if (anamnese.HasReumatismo == (short)StatusAnamnese.Sim)
                newRow["HasReumatismo"] = "SIM";
            else if (anamnese.HasReumatismo == (short)StatusAnamnese.Nao)
                newRow["HasReumatismo"] = "NÃO";

            if (anamnese.HasAlergia == (short)StatusAnamnese.Sim)
                newRow["HasAlergia"] = "SIM";
            else if (anamnese.HasAlergia == (short)StatusAnamnese.Nao)
                newRow["HasAlergia"] = "NÃO";

            if (anamnese.HasEsporte == (short)StatusAnamnese.Sim)
                newRow["HasEsporte"] = "SIM";
            else if (anamnese.HasEsporte == (short)StatusAnamnese.Nao)
                newRow["HasEsporte"] = "NÃO";

            if (anamnese.HasAcidentou == (short)StatusAnamnese.Sim)
                newRow["HasAcidentou"] = "SIM";
            else if (anamnese.HasAcidentou == (short)StatusAnamnese.Nao)
                newRow["HasAcidentou"] = "NÃO";

            if (anamnese.Has_AF_Hipertensao == (short)StatusAnamnese.Sim)
                newRow["Has_AF_Hipertensao"] = "SIM";
            else if (anamnese.Has_AF_Hipertensao == (short)StatusAnamnese.Nao)
                newRow["Has_AF_Hipertensao"] = "NÃO";

            if (anamnese.Has_AF_Diabetes == (short)StatusAnamnese.Sim)
                newRow["Has_AF_Diabetes"] = "SIM";
            else if (anamnese.Has_AF_Diabetes == (short)StatusAnamnese.Nao)
                newRow["Has_AF_Diabetes"] = "NÃO";

            if (anamnese.Has_AF_Coracao == (short)StatusAnamnese.Sim)
                newRow["Has_AF_Coracao"] = "SIM";
            else if (anamnese.Has_AF_Coracao == (short)StatusAnamnese.Nao)
                newRow["Has_AF_Coracao"] = "NÃO";

            if (anamnese.Has_AF_Derrames == (short)StatusAnamnese.Sim)
                newRow["Has_AF_Derrames"] = "SIM";
            else if (anamnese.Has_AF_Derrames == (short)StatusAnamnese.Nao)
                newRow["Has_AF_Derrames"] = "NÃO";

            if (anamnese.Has_AF_Obesidade == (short)StatusAnamnese.Sim)
                newRow["Has_AF_Obesidade"] = "SIM";
            else if (anamnese.Has_AF_Obesidade == (short)StatusAnamnese.Nao)
                newRow["Has_AF_Obesidade"] = "NÃO";

            if (anamnese.Has_AF_Cancer == (short)StatusAnamnese.Sim)
                newRow["Has_AF_Cancer"] = "SIM";
            else if (anamnese.Has_AF_Cancer == (short)StatusAnamnese.Nao)
                newRow["Has_AF_Cancer"] = "NÃO";

            if (anamnese.Has_AF_Colesterol == (short)StatusAnamnese.Sim)
                newRow["Has_AF_Colesterol"] = "SIM";
            else if (anamnese.Has_AF_Colesterol == (short)StatusAnamnese.Nao)
                newRow["Has_AF_Colesterol"] = "NÃO";

            if (anamnese.Has_AF_Psiquiatricos == (short)StatusAnamnese.Sim)
                newRow["Has_AF_Psiquiatricos"] = "SIM";
            else if (anamnese.Has_AF_Psiquiatricos == (short)StatusAnamnese.Nao)
                newRow["Has_AF_Psiquiatricos"] = "NÃO";

            if (anamnese.Has_Otologica_Obstrucao == (short)StatusAnamnese.Sim)
                newRow["Has_Otologica_Obstrucao"] = "SIM";
            else if (anamnese.Has_Otologica_Obstrucao == (short)StatusAnamnese.Nao)
                newRow["Has_Otologica_Obstrucao"] = "NÃO";

            if (anamnese.Has_Otologica_Cerumen == (short)StatusAnamnese.Sim)
                newRow["Has_Otologica_Cerumen"] = "SIM";
            else if (anamnese.Has_Otologica_Cerumen == (short)StatusAnamnese.Nao)
                newRow["Has_Otologica_Cerumen"] = "NÃO";

            if (anamnese.Has_Otologica_Alteracao == (short)StatusAnamnese.Sim)
                newRow["Has_Otologica_Alteracao"] = "SIM";
            else if (anamnese.Has_Otologica_Alteracao == (short)StatusAnamnese.Nao)
                newRow["Has_Otologica_Alteracao"] = "NÃO";


        }
        #endregion

        #endregion

        #region DataSourceAdendo

        private void DataSourceAdendo(Clinico clinico)
        {
            DataSet ds = new AdendoExame().ListaAdendoExames("IdExameBase=" + clinico.Id + " ORDER BY Data DESC");

            DataRow newRow;

            foreach (DataRow row in ds.Tables[0].Select())
            {
                newRow = dsAdendo.Tables[0].NewRow();

                newRow["IdExameBase"] = row["IdExameBase"];
                newRow["Data"] = row["Data"];
                newRow["Medico"] = row["Medico"];
                newRow["Descricao"] = row["Descricao"];

                dsAdendo.Tables[0].Rows.Add(newRow);
            }
        }
        #endregion

        #endregion

        #region GetCoordenadorPCMSO

        public static string GetCoordenadorPCMSO(Cliente cliente, Pcmso pcmso)
        {
            string sMedicoCoordenador = string.Empty;

            if (pcmso == null || pcmso.Id == 0)
                sMedicoCoordenador = "Médico Responsável pelo PCMSO: _________________________    CRM-SP nº ______________    Fone ________________";
            else
            {
                if (pcmso.IdCoordenador.mirrorOld == null)
                    pcmso.IdCoordenador.Find();

                if (pcmso.IdCoordenador.IdJuridica.mirrorOld == null)
                    pcmso.IdCoordenador.IdJuridica.Find();

            //    if (pcmso.IdCoordenador.Id == (int)Medicos.DraMarcela || pcmso.IdCoordenador.Id == (int)Medicos.DraRosana)
            //        sMedicoCoordenador = "Médica do Trabalho Coordenadora do PCMSO: " + pcmso.IdCoordenador.NomeCompleto + "    " + pcmso.IdCoordenador.Numero + "     Telefone " + pcmso.IdCoordenador.IdJuridica.GetContatoTelefonico().GetDDDTelefone();
            //    else
            //        sMedicoCoordenador = "Médico do Trabalho Coordenador do PCMSO: " + pcmso.IdCoordenador.NomeCompleto + "    " + pcmso.IdCoordenador.Numero + "     Telefone " + pcmso.IdCoordenador.IdJuridica.GetContatoTelefonico().GetDDDTelefone();
            //}

                //Medico Medico;
                string xFone;
                //ContatoTelefonico contatoTelefonico;

                Clientes_Clinicas xClientes_Clinicas = new Clientes_Clinicas();

                //wagner - ajuste para obter fone prestador
                xFone = xClientes_Clinicas.Retornar_Fone_Prestador(pcmso.IdCoordenador.Id);


                //if (pcmso.IdCoordenador.Id == (int)Medicos.DraMarcela || pcmso.IdCoordenador.Id == (int)Medicos.DraRosana)
                //    sMedicoCoordenador = "Médica do Trabalho Coordenadora do PCMSO: " + pcmso.IdCoordenador.NomeCompleto + "    " + pcmso.IdCoordenador.Numero + "     Telefone " + xFone;
                //else
                sMedicoCoordenador = "Médico Responsável pelo PCMSO: " + pcmso.IdCoordenador.NomeCompleto + "   " + pcmso.IdCoordenador.Numero + " " +  pcmso.IdCoordenador.UF +  "  Telefone " + xFone;
            }

            return sMedicoCoordenador;
        }
        #endregion


        private static DateTime TrazerDataUltimoExame(Int32 IdExameDicionario, Int32 IdEmpregado)
        {
            StringBuilder criteria = new StringBuilder();

            criteria.Append("IdEmpregado=" + IdEmpregado.ToString());

            criteria.Append(" AND IdExameDicionario IN (" + IdExameDicionario.ToString() + ")");

            criteria.Append(" AND IndResultado <>" + (int)ResultadoExame.NaoRealizado + " AND IndResultado <>" + (int)ResultadoExame.EmEspera);
            ExameBase ultimoExame = new ExameBase();

            System.Collections.ArrayList
                listExameBase = ultimoExame.FindMax("DataExame", criteria.ToString());

            DateTime dataUltima;

            if (listExameBase.Count > 1)
            {
                ultimoExame = (ExameBase)listExameBase[0];
            }


            if (listExameBase.Count >= 1)
                dataUltima = new DateTime(ultimoExame.DataExame.Year, ultimoExame.DataExame.Month, ultimoExame.DataExame.Day);
            else
                dataUltima = new DateTime(2050, 1, 1);

            return dataUltima;
        }



    }
}

