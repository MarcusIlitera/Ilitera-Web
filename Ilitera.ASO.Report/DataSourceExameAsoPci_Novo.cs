using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;

using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.ASO.Report
{

    public class DataSourceExameAsoPci_Novo : DataSourceBase
    {
        #region Eventos

        public override event EventProgress ProgressIniciar;
        public override event EventProgress ProgressAtualizar;
        public override event EventProgressFinalizar ProgressFinalizar;

        #endregion

        #region Variaveis


        private DataSet dsPrincipal = new DataSet();
        private DataSet dsExame = new DataSet();
        private DataSet dsExame_Vazio = new DataSet();
        private DataSet dsPci = new DataSet();
        private DataSet dsAdendo = new DataSet();

        private bool PciComAso = false;
        private bool ExamesSomentePeriodoPcmso = false;
        private bool SemFoto = false;

        private ConvocacaoExame convocacao;
        private Clinico clinico;
        private Cliente cliente;
        private Pcmso pcmso;

        private DataSet dsAnamnese = new DataSet();
        private bool xSemResposta = false;

        #endregion

        #region Construtor

        public DataSourceExameAsoPci_Novo()
        {
            InicializarTables();

        }

        public DataSourceExameAsoPci_Novo(Clinico clinico)
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

        public DataSourceExameAsoPci_Novo(ConvocacaoExame convocacao,
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

        public RptQuestionario_Respiratorio GetReport_Questionario()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("Arquivo", Type.GetType("System.String"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));

            DataSet dsQuest = new DataSet();
            dsQuest.Tables.Add(table);

            DataRow newRow2 = dsQuest.Tables[0].NewRow();
            newRow2["Arquivo"] = "1";
            newRow2["Descricao"] = "1";
            dsQuest.Tables[0].Rows.Add(newRow2);


            RptQuestionario_Respiratorio report = new RptQuestionario_Respiratorio();

            report.SetDataSource(dsQuest);
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }


        #region GetReportAso




        public Rpt_Novo_ASO GetReport()
        {
            //Fontes proporcionais
            //Lucida Console
            // Consolas

            Rpt_Novo_ASO report = new Rpt_Novo_ASO();

            if (clinico != null)
            {
                DataSourcePrincipal(clinico);
                DataSourceAso(clinico);
            }
            else
                DataSourceAso();


            report.Subreports[0].SetDataSource(dsExame);

            if (this.convocacao != null)
            {
                report.Subreports[1].SetDataSource(dsExame_Vazio);
                report.Subreports[2].SetDataSource(dsExame_Vazio);
                report.Subreports[3].SetDataSource(dsExame_Vazio);
            }
            else
            {
                report.Subreports[1].SetDataSource(dsExame);
                report.Subreports[2].SetDataSource(dsExame);
                report.Subreports[3].SetDataSource(dsExame);
            }

            report.SetDataSource(dsPrincipal);
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }



        public Rpt_Novo_ASO_Unico GetReport_Unico( string rCabecalho)
        {
            //Fontes proporcionais
            //Lucida Console
            // Consolas

            Rpt_Novo_ASO_Unico report = new Rpt_Novo_ASO_Unico();

            if (clinico != null)
            {
                DataSourcePrincipal(clinico, rCabecalho);
                DataSourceAso(clinico);
            }
            else
                DataSourceAso();


            report.Subreports[0].SetDataSource(dsExame);


            report.SetDataSource(dsPrincipal);
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }





        public Rpt_Novo_Pci_Novo_Unico_Essence GetReportPciUnico_Essence(bool PciComAso)
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
                DataSourceAso(true, "");

            Rpt_Novo_Pci_Novo_Unico_Essence reportPci = new Rpt_Novo_Pci_Novo_Unico_Essence();

            reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
            reportPci.SetDataSource(dsPci);
            reportPci.Refresh();


            SetTempoProcessamento(reportPci);

            return reportPci;
        }

        public static DataTable GetDataTableReportQuestao()
        {
            DataTable table = new DataTable("ResultASO");
            //Empregado
            table.Columns.Add("IdExame", Type.GetType("System.Int32"));
            table.Columns.Add("Sistema", Type.GetType("System.String"));
            table.Columns.Add("Questao", Type.GetType("System.String"));
            table.Columns.Add("tX_APTO", Type.GetType("System.String"));
            table.Columns.Add("tX_INAPTO", Type.GetType("System.String"));
            table.Columns.Add("Obs", Type.GetType("System.String"));

            return table;
        }



        public RptAnamnese_Novo GetReport_Anamnese(bool zSem_Resposta)
        {
            RptAnamnese_Novo report = new RptAnamnese_Novo();

            xSemResposta = zSem_Resposta;

            DataSourcePrincipal(clinico);
            DataSourceAso(clinico);


            dsAnamnese.Tables.Add(GetDataTableReportQuestao());
            string zSistema = "";

            DataRow newRow2 = dsAnamnese.Tables[0].NewRow();

            Ilitera.Data.Clientes_Funcionarios zQuest = new Ilitera.Data.Clientes_Funcionarios();

            DataSet ds = zQuest.Trazer_Anamnese_Exame(clinico.Id);


            for (int zCont = 0; zCont < ds.Tables[0].Rows.Count; zCont++)
            {

                newRow2 = dsAnamnese.Tables[0].NewRow();
                newRow2["IdExame"] = zCont;

                if (zSistema != ds.Tables[0].Rows[zCont][4].ToString().Trim())
                {
                    zSistema = ds.Tables[0].Rows[zCont][4].ToString().Trim();
                    newRow2["Sistema"] = ds.Tables[0].Rows[zCont][4].ToString().Trim();
                }
                else
                {
                    newRow2["Sistema"] = "";
                }

                newRow2["Questao"] = ds.Tables[0].Rows[zCont][5].ToString().Trim();
                newRow2["Obs"] = ds.Tables[0].Rows[zCont]["Obs"].ToString().Trim();


                if (this.xSemResposta == true)
                {
                    newRow2["tX_APTO"] = "";
                    newRow2["tX_INAPTO"] = "";
                }
                else
                {
                    if (ds.Tables[0].Rows[zCont][6].ToString().Trim() == "S")
                    {
                        newRow2["tX_APTO"] = "X";
                        newRow2["tX_INAPTO"] = "";
                    }
                    else
                    {
                        newRow2["tX_APTO"] = "";
                        newRow2["tX_INAPTO"] = "X";
                    }
                }

                dsAnamnese.Tables[0].Rows.Add(newRow2);

            }




            report.Subreports[0].SetDataSource(dsAnamnese);

            report.SetDataSource(dsExame);
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }







        public Rpt_Novo_Pci_Novo_Unico GetReportPciUnico(bool PciComAso)
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
                DataSourceAso(true, "");

            Rpt_Novo_Pci_Novo_Unico reportPci = new Rpt_Novo_Pci_Novo_Unico();


            //DataSet dsExame_Cop = new DataSet();

            //dsExame_Cop.Tables.Add(GetDataTableReportAso());


            //for (int zCont = 0; zCont < dsExame.Tables[0].Rows.Count; zCont++)
            //{
            //    for (int zAux = 0; zAux < 4; zAux++)
            //    {

            //        dsExame_Cop.Tables[0].ImportRow(dsExame.Tables[0].Rows[zCont]);
            //        if (zAux == 0)
            //        {
            //            dsExame_Cop.Tables[0].Rows[(zCont * 4) + zAux]["tx_Espera"] = "Via ( Empresa )";
            //            dsExame_Cop.Tables[0].Rows[(zCont * 4) + zAux]["tVia"] = "-";
            //        }
            //        else if (zAux == 1)
            //        {
            //            dsExame_Cop.Tables[0].Rows[(zCont * 4) + zAux]["tx_Espera"] = "Via ( Empregado )";
            //            dsExame_Cop.Tables[0].Rows[(zCont * 4) + zAux]["tVia"] = "";
            //        }
            //        else if (zAux == 2)
            //        {
            //            dsExame_Cop.Tables[0].Rows[(zCont * 4) + zAux]["tx_Espera"] = "Via ( Consultoria )";
            //            dsExame_Cop.Tables[0].Rows[(zCont * 4) + zAux]["tVia"] = "-";
            //        }
            //        else if (zAux == 3)
            //        {
            //            dsExame_Cop.Tables[0].Rows[(zCont * 4) + zAux]["tx_Espera"] = "Via ( Clínica )";
            //            dsExame_Cop.Tables[0].Rows[(zCont * 4) + zAux]["tVia"] = "";
            //        }
            //    }

            //}



            //reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsExame_Cop);
            //reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
            //reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);
            reportPci.SetDataSource(dsPci);
            reportPci.Refresh();


            SetTempoProcessamento(reportPci);

            return reportPci;
        }




        public Rpt_Novo_Pci_Novo_Unico_EY GetReportPciUnico_EY(bool PciComAso)
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
                DataSourceAso(true, "");

            Rpt_Novo_Pci_Novo_Unico_EY reportPci = new Rpt_Novo_Pci_Novo_Unico_EY();


          
            reportPci.SetDataSource(dsPci);
            reportPci.Refresh();


            SetTempoProcessamento(reportPci);

            return reportPci;
        }


        public Rpt_Novo_Pci_Novo GetReportPci3(bool PciComAso, int zCopias)
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
                DataSourceAso(true, "");

            Rpt_Novo_Pci_Novo reportPci = new Rpt_Novo_Pci_Novo();


            DataSet dsExame_Cop = new DataSet();

            dsExame_Cop.Tables.Add(GetDataTableReportAso());


            for (int zCont = 0; zCont < dsExame.Tables[0].Rows.Count; zCont++)
            {
                for (int zAux = 0; zAux < zCopias; zAux++)
                {

                    dsExame_Cop.Tables[0].ImportRow(dsExame.Tables[0].Rows[zCont]);
                    if (zAux == 0)
                    {
                        dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empresa )";
                        dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
                    }
                    else if (zAux == 1)
                    {
                        dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empregado )";
                        dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
                    }
                    else if (zAux == 2)
                    {
                        dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Consultoria )";
                        dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
                    }
                    else if (zAux == 3)
                    {
                        dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Clínica )";
                        dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
                    }
                }

            }


            reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsExame_Cop);

            reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
            reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);
            reportPci.SetDataSource(dsPci);
            reportPci.Refresh();


            SetTempoProcessamento(reportPci);

            return reportPci;
        }




        public Rpt_Novo_Pci_Novo GetReportPci3(bool PciComAso, Int32 xOrder, string xOrdem, string xDataBranco)
        {
            this.PciComAso = PciComAso;

            dsAdendo.Tables.Add(GetDataTableAdendo());

            if (clinico != null)
            {
                DataSourceAso(clinico, xDataBranco);
                DataSourcePci(clinico);
                DataSourceAdendo(clinico);
            }
            else
                DataSourceAso(true, xDataBranco);

            Rpt_Novo_Pci_Novo reportPci = new Rpt_Novo_Pci_Novo();


            DataSet dsExame_Cop = new DataSet();

            dsExame_Cop.Tables.Add(GetDataTableReportAso());


            for (int zCont = 0; zCont < dsExame.Tables[0].Rows.Count; zCont++)
            {
                for (int zAux = 0; zAux < 4; zAux++)
                {

                    dsExame_Cop.Tables[0].ImportRow(dsExame.Tables[0].Rows[zCont]);
                    if (zAux == 0)
                    {
                        dsExame_Cop.Tables[0].Rows[(zCont * 4) + zAux]["tx_Espera"] = "Via ( Empresa )";
                        dsExame_Cop.Tables[0].Rows[(zCont * 4) + zAux]["tVia"] = "-";
                    }
                    else if (zAux == 1)
                    {
                        dsExame_Cop.Tables[0].Rows[(zCont * 4) + zAux]["tx_Espera"] = "Via ( Empregado )";
                        dsExame_Cop.Tables[0].Rows[(zCont * 4) + zAux]["tVia"] = "";
                    }
                    else if (zAux == 2)
                    {
                        dsExame_Cop.Tables[0].Rows[(zCont * 4) + zAux]["tx_Espera"] = "Via ( Consultoria )";
                        dsExame_Cop.Tables[0].Rows[(zCont * 4) + zAux]["tVia"] = "-";
                    }
                    else if (zAux == 3)
                    {
                        dsExame_Cop.Tables[0].Rows[(zCont * 4) + zAux]["tx_Espera"] = "Via ( Clínica )";
                        dsExame_Cop.Tables[0].Rows[(zCont * 4) + zAux]["tVia"] = "";
                    }
                }

            }


            //for (int zCont = 19; zCont > 4; zCont--)
            //{
            //    dsExame_Cop.Tables[0].Rows[zCont].Delete();
            //}



            DataSet dsTransf = new DataSet(); //Declare a dataSet to be filled.

            if (xOrder == 1)
                dsExame.Tables[0].DefaultView.Sort = "tNO_GHE, tNO_EMPG";
            else
                dsExame.Tables[0].DefaultView.Sort = "tNO_EMPG";

            dsTransf.Tables.Add(dsExame.Tables[0].DefaultView.ToTable());


            DataSet dsTransf2 = new DataSet(); //Declare a dataSet to be filled.

            if (xOrder == 1)
                dsExame_Cop.Tables[0].DefaultView.Sort = "tNO_GHE, tNO_EMPG";
            else
                dsExame_Cop.Tables[0].DefaultView.Sort = "tNO_EMPG";

            dsTransf2.Tables.Add(dsExame_Cop.Tables[0].DefaultView.ToTable());

            if (xOrder == 1)
            {
                dsTransf2.Tables[0].DefaultView.Sort = "tNO_GHE, tNO_EMPG";
                dsTransf.Tables[0].DefaultView.Sort = "tNO_GHE, tNO_EMPG";
            }


            DataSet dsTransf4 = new DataSet(); //Declare a dataSet to be filled.

            if (xOrder == 1)
                dsPci.Tables[0].DefaultView.Sort = "tNO_GHE";  //, tNO_EMPG2";
            //else
            //    dsPci.Tables[0].DefaultView.Sort = "tNO_EMPG";

            dsTransf4.Tables.Add(dsPci.Tables[0].DefaultView.ToTable());

            //reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsExame_Cop);
            reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsTransf2);


            //reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
            reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsTransf);

            reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);
            //reportPci.SetDataSource(dsPci);
            reportPci.SetDataSource(dsTransf4);
            reportPci.Refresh();


            SetTempoProcessamento(reportPci);

            return reportPci;
        }



        public Rpt_Novo_Pci_Novo GetReportPci3(bool PciComAso, int zCopias, Int32 xOrder, string xDataBranco)
        {
            this.PciComAso = PciComAso;

            dsAdendo.Tables.Add(GetDataTableAdendo());

            if (clinico != null)
            {
                DataSourceAso(clinico, xDataBranco);
                DataSourcePci(clinico);
                DataSourceAdendo(clinico);
            }
            else
                DataSourceAso(true, xDataBranco);

            Rpt_Novo_Pci_Novo reportPci = new Rpt_Novo_Pci_Novo();


            DataSet dsExame_Cop = new DataSet();

            dsExame_Cop.Tables.Add(GetDataTableReportAso());


            for (int zCont = 0; zCont < dsExame.Tables[0].Rows.Count; zCont++)
            {
                for (int zAux = 0; zAux < zCopias; zAux++)
                {

                    dsExame_Cop.Tables[0].ImportRow(dsExame.Tables[0].Rows[zCont]);
                    if (zAux == 0)
                    {
                        dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empresa )";
                        dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
                    }
                    else if (zAux == 1)
                    {
                        dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empregado )";
                        dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
                    }
                    else if (zAux == 2)
                    {
                        dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Consultoria )";
                        dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
                    }
                    else if (zAux == 3)
                    {
                        dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Clínica )";
                        dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
                    }
                }

            }


            DataSet dsTransf = new DataSet(); //Declare a dataSet to be filled.

            if (xOrder == 1)
                dsExame.Tables[0].DefaultView.Sort = "tNO_GHE";
            else
                dsExame.Tables[0].DefaultView.Sort = "tNO_EMPG";

            dsTransf.Tables.Add(dsExame.Tables[0].DefaultView.ToTable());



            DataSet dsTransf2 = new DataSet(); //Declare a dataSet to be filled.

            if (xOrder == 1)
                dsExame_Cop.Tables[0].DefaultView.Sort = "tNO_GHE";
            else
                dsExame_Cop.Tables[0].DefaultView.Sort = "tNO_EMPG";

            dsTransf2.Tables.Add(dsExame_Cop.Tables[0].DefaultView.ToTable());


            DataSet dsTransf4 = new DataSet(); //Declare a dataSet to be filled.

            if (xOrder == 1)
                dsPci.Tables[0].DefaultView.Sort = "tNO_GHE";  //, tNO_EMPG2";
            //else
            //    dsPci.Tables[0].DefaultView.Sort = "tNO_EMPG";

            dsTransf4.Tables.Add(dsPci.Tables[0].DefaultView.ToTable());



            //reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsExame_Cop);
            reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsTransf2);

            //reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
            reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsTransf);
            reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);
            reportPci.SetDataSource(dsTransf4);  // dsPci
            reportPci.Refresh();


            SetTempoProcessamento(reportPci);

            return reportPci;
        }




        //public RptPci3_Antigo GetReportPci3_Antigo(bool PciComAso, int zCopias)
        //{
        //    this.PciComAso = PciComAso;

        //    dsAdendo.Tables.Add(GetDataTableAdendo());

        //    if (clinico != null)
        //    {
        //        DataSourceAso(clinico);
        //        DataSourcePci(clinico);
        //        DataSourceAdendo(clinico);
        //    }
        //    else
        //        DataSourceAso(true, "");

        //    RptPci3_Antigo reportPci = new RptPci3_Antigo();


        //    DataSet dsExame_Cop = new DataSet();

        //    dsExame_Cop.Tables.Add(GetDataTableReportAso());


        //    for (int zCont = 0; zCont < dsExame.Tables[0].Rows.Count; zCont++)
        //    {
        //        for (int zAux = 0; zAux < zCopias; zAux++)
        //        {

        //            dsExame_Cop.Tables[0].ImportRow(dsExame.Tables[0].Rows[zCont]);
        //            if (zAux == 0)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empresa )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
        //            }
        //            else if (zAux == 1)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empregado )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
        //            }
        //            else if (zAux == 2)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Consultoria )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
        //            }
        //            else if (zAux == 3)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Clínica )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
        //            }
        //        }

        //    }


        //    reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsExame_Cop);

        //    reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
        //    reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);
        //    reportPci.SetDataSource(dsPci);
        //    reportPci.Refresh();


        //    SetTempoProcessamento(reportPci);

        //    return reportPci;
        //}




        //public RptPci3_Antigo GetReportPci3_Antigo(bool PciComAso, int zCopias, Int32 xOrder, string xDataBranco)
        //{
        //    this.PciComAso = PciComAso;

        //    dsAdendo.Tables.Add(GetDataTableAdendo());

        //    if (clinico != null)
        //    {
        //        DataSourceAso(clinico, xDataBranco);
        //        DataSourcePci(clinico);
        //        DataSourceAdendo(clinico);
        //    }
        //    else
        //        DataSourceAso(true, xDataBranco);

        //    RptPci3_Antigo reportPci = new RptPci3_Antigo();


        //    DataSet dsExame_Cop = new DataSet();

        //    dsExame_Cop.Tables.Add(GetDataTableReportAso());


        //    for (int zCont = 0; zCont < dsExame.Tables[0].Rows.Count; zCont++)
        //    {
        //        for (int zAux = 0; zAux < zCopias; zAux++)
        //        {

        //            dsExame_Cop.Tables[0].ImportRow(dsExame.Tables[0].Rows[zCont]);
        //            if (zAux == 0)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empresa )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
        //            }
        //            else if (zAux == 1)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empregado )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
        //            }
        //            else if (zAux == 2)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Consultoria )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
        //            }
        //            else if (zAux == 3)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Clínica )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
        //            }
        //        }

        //    }



        //    DataSet dsTransf = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsExame.Tables[0].DefaultView.Sort = "tNO_GHE, tNO_EMPG";
        //    else
        //        dsExame.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf.Tables.Add(dsExame.Tables[0].DefaultView.ToTable());


        //    DataSet dsTransf2 = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsExame_Cop.Tables[0].DefaultView.Sort = "tNO_GHE, tNO_EMPG";
        //    else
        //        dsExame_Cop.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf2.Tables.Add(dsExame_Cop.Tables[0].DefaultView.ToTable());


        //    DataSet dsTransf4 = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsPci.Tables[0].DefaultView.Sort = "tNO_GHE";  //, tNO_EMPG2";
        //    //else
        //    //    dsPci.Tables[0].DefaultView.Sort = "tNO_EMPG2";

        //    dsTransf4.Tables.Add(dsPci.Tables[0].DefaultView.ToTable());




        //    //reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsExame_Cop);
        //    reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsTransf2);

        //    //reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
        //    reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsTransf);
        //    reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);
        //    reportPci.SetDataSource(dsTransf4); //dsPci);
        //    //reportPci.SetDataSource(dsTransf3);
        //    reportPci.Refresh();


        //    SetTempoProcessamento(reportPci);

        //    return reportPci;
        //}




        //public RptPci_Novo_Capgemini2 GetReportPci_Capgemini(bool PciComAso, int zCopias, Int32 xOrder, string xDataBranco)
        //{
        //    this.PciComAso = PciComAso;

        //    dsAdendo.Tables.Add(GetDataTableAdendo());

        //    if (clinico != null)
        //    {
        //        DataSourceAso(clinico, xDataBranco);
        //        DataSourcePci(clinico);
        //        DataSourceAdendo(clinico);
        //    }
        //    else
        //        DataSourceAso(true, xDataBranco);

        //    RptPci_Novo_Capgemini2 reportPci = new RptPci_Novo_Capgemini2();


        //    DataSet dsExame_Cop = new DataSet();

        //    dsExame_Cop.Tables.Add(GetDataTableReportAso());


        //    for (int zCont = 0; zCont < dsExame.Tables[0].Rows.Count; zCont++)
        //    {
        //        for (int zAux = 0; zAux < zCopias; zAux++)
        //        {

        //            dsExame_Cop.Tables[0].ImportRow(dsExame.Tables[0].Rows[zCont]);
        //            if (zAux == 0)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empresa )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
        //            }
        //            else if (zAux == 1)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empregado )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
        //            }
        //            else if (zAux == 2)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Consultoria )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
        //            }
        //            else if (zAux == 3)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Clínica )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
        //            }
        //        }

        //    }



        //    DataSet dsTransf = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsExame.Tables[0].DefaultView.Sort = "tNO_GHE, tNO_EMPG";
        //    else
        //        dsExame.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf.Tables.Add(dsExame.Tables[0].DefaultView.ToTable());


        //    DataSet dsTransf2 = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsExame_Cop.Tables[0].DefaultView.Sort = "tNO_GHE, tNO_EMPG";
        //    else
        //        dsExame_Cop.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf2.Tables.Add(dsExame_Cop.Tables[0].DefaultView.ToTable());


        //    DataSet dsTransf4 = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsPci.Tables[0].DefaultView.Sort = "tNO_GHE";  //, tNO_EMPG2";
        //    else
        //        dsPci.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf4.Tables.Add(dsPci.Tables[0].DefaultView.ToTable());




        //    //reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsExame_Cop);
        //    reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsTransf2);

        //    //reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
        //    reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsTransf);
        //    reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);
        //    reportPci.SetDataSource(dsTransf4); //dsPci);
        //    //reportPci.SetDataSource(dsTransf3);
        //    reportPci.Refresh();


        //    SetTempoProcessamento(reportPci);

        //    return reportPci;
        //}


        //public RptPci_Novo_EY2 GetReportPci_EY2(bool PciComAso, int zCopias, Int32 xOrder, string xDataBranco)
        //{
        //    this.PciComAso = PciComAso;

        //    dsAdendo.Tables.Add(GetDataTableAdendo());

        //    if (clinico != null)
        //    {
        //        DataSourceAso(clinico, xDataBranco);
        //        DataSourcePci(clinico);
        //        DataSourceAdendo(clinico);
        //    }
        //    else
        //        DataSourceAso(true, xDataBranco);

        //    RptPci_Novo_EY2 reportPci = new RptPci_Novo_EY2();


        //    DataSet dsExame_Cop = new DataSet();

        //    dsExame_Cop.Tables.Add(GetDataTableReportAso());


        //    for (int zCont = 0; zCont < dsExame.Tables[0].Rows.Count; zCont++)
        //    {
        //        for (int zAux = 0; zAux < zCopias; zAux++)
        //        {

        //            dsExame_Cop.Tables[0].ImportRow(dsExame.Tables[0].Rows[zCont]);
        //            if (zAux == 0)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empresa )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
        //            }
        //            else if (zAux == 1)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empregado )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
        //            }
        //            else if (zAux == 2)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Consultoria )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
        //            }
        //            else if (zAux == 3)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Clínica )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
        //            }
        //        }

        //    }



        //    DataSet dsTransf = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsExame.Tables[0].DefaultView.Sort = "tNO_GHE, tNO_EMPG";
        //    else
        //        dsExame.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf.Tables.Add(dsExame.Tables[0].DefaultView.ToTable());


        //    DataSet dsTransf2 = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsExame_Cop.Tables[0].DefaultView.Sort = "tNO_GHE, tNO_EMPG";
        //    else
        //        dsExame_Cop.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf2.Tables.Add(dsExame_Cop.Tables[0].DefaultView.ToTable());


        //    DataSet dsTransf4 = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsPci.Tables[0].DefaultView.Sort = "tNO_GHE";  //, tNO_EMPG2";
        //    else
        //        dsPci.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf4.Tables.Add(dsPci.Tables[0].DefaultView.ToTable());




        //    //reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsExame_Cop);
        //    reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsTransf2);

        //    //reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
        //    reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsTransf);
        //    reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);
        //    reportPci.SetDataSource(dsTransf4); //dsPci);
        //    //reportPci.SetDataSource(dsTransf3);
        //    reportPci.Refresh();


        //    SetTempoProcessamento(reportPci);

        //    return reportPci;
        //}





        //public RptPci_Novo_Capgemini3 GetReportPci_Capgemini3(bool PciComAso, int zCopias, Int32 xOrder, string xDataBranco, Int32 rOrder, bool rimpData)
        //{
        //    this.PciComAso = PciComAso;

        //    dsAdendo.Tables.Add(GetDataTableAdendo());

        //    if (clinico != null)
        //    {
        //        DataSourceAso(clinico, xDataBranco);
        //        DataSourcePci(clinico);
        //        DataSourceAdendo(clinico);
        //    }
        //    else
        //        DataSourceAso(true, xDataBranco);

        //    RptPci_Novo_Capgemini3 reportPci = new RptPci_Novo_Capgemini3();


        //    DataSet dsExame_Cop = new DataSet();

        //    dsExame_Cop.Tables.Add(GetDataTableReportAso());


        //    for (int zCont = 0; zCont < dsExame.Tables[0].Rows.Count; zCont++)
        //    {
        //        for (int zAux = 0; zAux < zCopias; zAux++)
        //        {

        //            dsExame_Cop.Tables[0].ImportRow(dsExame.Tables[0].Rows[zCont]);
        //            if (zAux == 0)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empresa )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
        //            }
        //            else if (zAux == 1)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empregado )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
        //            }
        //            else if (zAux == 2)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Consultoria )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
        //            }
        //            else if (zAux == 3)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Clínica )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
        //            }
        //        }

        //    }



        //    DataSet dsTransf = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsExame.Tables[0].DefaultView.Sort = "tNO_GHE, tNO_EMPG";
        //    else
        //        dsExame.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf.Tables.Add(dsExame.Tables[0].DefaultView.ToTable());


        //    DataSet dsTransf2 = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsExame_Cop.Tables[0].DefaultView.Sort = "tNO_GHE, tNO_EMPG";
        //    else
        //        dsExame_Cop.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf2.Tables.Add(dsExame_Cop.Tables[0].DefaultView.ToTable());


        //    DataSet dsTransf4 = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsPci.Tables[0].DefaultView.Sort = "tNO_GHE";  //, tNO_EMPG2";
        //    else
        //        dsPci.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf4.Tables.Add(dsPci.Tables[0].DefaultView.ToTable());




        //    //reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsExame_Cop);
        //    reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsTransf2);

        //    //reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
        //    reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsTransf);
        //    reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);



        //    DataSourceGuia_Prajna xPrajna = new DataSourceGuia_Prajna(convocacao, pcmso, rOrder, rimpData);
        //    reportPci.OpenSubreport("RptGuia_Prajna.rpt").SetDataSource(xPrajna.GetIntroducaoGuia());

        //    DataSourceExameAnamnese xAnam = new DataSourceExameAnamnese(convocacao, pcmso, true, true);
        //    reportPci.OpenSubreport("RptAnamnese.rpt").SetDataSource(xAnam.GetReportConvocacao());

        //    reportPci.SetDataSource(dsTransf4); //dsPci);
        //    //reportPci.SetDataSource(dsTransf3);
        //    reportPci.Refresh();


        //    SetTempoProcessamento(reportPci);

        //    return reportPci;
        //}



        //public RptPci_Novo_EY3 GetReportPci_EY3(bool PciComAso, int zCopias, Int32 xOrder, string xDataBranco, Int32 rOrder, bool rimpData)
        //{
        //    this.PciComAso = PciComAso;

        //    dsAdendo.Tables.Add(GetDataTableAdendo());

        //    if (clinico != null)
        //    {
        //        DataSourceAso(clinico, xDataBranco);
        //        DataSourcePci(clinico);
        //        DataSourceAdendo(clinico);
        //    }
        //    else
        //        DataSourceAso(true, xDataBranco);

        //    RptPci_Novo_EY3 reportPci = new RptPci_Novo_EY3();


        //    DataSet dsExame_Cop = new DataSet();

        //    dsExame_Cop.Tables.Add(GetDataTableReportAso());


        //    for (int zCont = 0; zCont < dsExame.Tables[0].Rows.Count; zCont++)
        //    {
        //        for (int zAux = 0; zAux < zCopias; zAux++)
        //        {

        //            dsExame_Cop.Tables[0].ImportRow(dsExame.Tables[0].Rows[zCont]);
        //            if (zAux == 0)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empresa )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
        //            }
        //            else if (zAux == 1)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empregado )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
        //            }
        //            else if (zAux == 2)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Consultoria )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
        //            }
        //            else if (zAux == 3)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Clínica )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
        //            }
        //        }

        //    }



        //    DataSet dsTransf = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsExame.Tables[0].DefaultView.Sort = "tNO_GHE, tNO_EMPG";
        //    else
        //        dsExame.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf.Tables.Add(dsExame.Tables[0].DefaultView.ToTable());


        //    DataSet dsTransf2 = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsExame_Cop.Tables[0].DefaultView.Sort = "tNO_GHE, tNO_EMPG";
        //    else
        //        dsExame_Cop.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf2.Tables.Add(dsExame_Cop.Tables[0].DefaultView.ToTable());


        //    DataSet dsTransf4 = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsPci.Tables[0].DefaultView.Sort = "tNO_GHE";  //, tNO_EMPG2";
        //    else
        //        dsPci.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf4.Tables.Add(dsPci.Tables[0].DefaultView.ToTable());




        //    //reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsExame_Cop);
        //    reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsTransf2);

        //    //reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
        //    reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsTransf);
        //    reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);



        //    DataSourceGuia_Prajna xPrajna = new DataSourceGuia_Prajna(convocacao, pcmso, rOrder, rimpData);
        //    reportPci.OpenSubreport("RptGuia_Prajna.rpt").SetDataSource(xPrajna.GetIntroducaoGuia());

        //    DataSourceExameAnamnese xAnam = new DataSourceExameAnamnese(convocacao, pcmso, true, true);
        //    reportPci.OpenSubreport("RptAnamnese.rpt").SetDataSource(xAnam.GetReportConvocacao());

        //    reportPci.SetDataSource(dsTransf4); //dsPci);
        //    //reportPci.SetDataSource(dsTransf3);
        //    reportPci.Refresh();


        //    SetTempoProcessamento(reportPci);

        //    return reportPci;
        //}





        //public RptPci_Novo_Capgemini4 GetReportPci_Capgemini4(bool PciComAso, int zCopias, Int32 xOrder, string xDataBranco, Int32 rOrder, bool rimpData)
        //{
        //    this.PciComAso = PciComAso;

        //    dsAdendo.Tables.Add(GetDataTableAdendo());

        //    if (clinico != null)
        //    {
        //        DataSourceAso(clinico, xDataBranco);
        //        DataSourcePci(clinico);
        //        DataSourceAdendo(clinico);
        //    }
        //    else
        //        DataSourceAso(true, xDataBranco);

        //    RptPci_Novo_Capgemini4 reportPci = new RptPci_Novo_Capgemini4();


        //    DataSet dsExame_Cop = new DataSet();

        //    dsExame_Cop.Tables.Add(GetDataTableReportAso());


        //    for (int zCont = 0; zCont < dsExame.Tables[0].Rows.Count; zCont++)
        //    {
        //        for (int zAux = 0; zAux < zCopias; zAux++)
        //        {

        //            dsExame_Cop.Tables[0].ImportRow(dsExame.Tables[0].Rows[zCont]);
        //            if (zAux == 0)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empresa )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
        //            }
        //            else if (zAux == 1)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empregado )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
        //            }
        //            else if (zAux == 2)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Consultoria )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
        //            }
        //            else if (zAux == 3)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Clínica )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
        //            }
        //        }

        //    }



        //    DataSet dsTransf = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsExame.Tables[0].DefaultView.Sort = "tNO_GHE, tNO_EMPG";
        //    else
        //        dsExame.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf.Tables.Add(dsExame.Tables[0].DefaultView.ToTable());


        //    DataSet dsTransf2 = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsExame_Cop.Tables[0].DefaultView.Sort = "tNO_GHE, tNO_EMPG";
        //    else
        //        dsExame_Cop.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf2.Tables.Add(dsExame_Cop.Tables[0].DefaultView.ToTable());


        //    DataSet dsTransf4 = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsPci.Tables[0].DefaultView.Sort = "tNO_GHE";  //, tNO_EMPG2";
        //    else
        //        dsPci.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf4.Tables.Add(dsPci.Tables[0].DefaultView.ToTable());




        //    //reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsExame_Cop);
        //    reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsTransf2);

        //    //reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
        //    reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsTransf);
        //    reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);



        //    DataSourceExameAnamnese xAnam = new DataSourceExameAnamnese(convocacao, pcmso, true, true);
        //    reportPci.OpenSubreport("RptAnamnese.rpt").SetDataSource(xAnam.GetReportConvocacao());

        //    reportPci.SetDataSource(dsTransf4); //dsPci);
        //    //reportPci.SetDataSource(dsTransf3);
        //    reportPci.Refresh();


        //    SetTempoProcessamento(reportPci);

        //    return reportPci;
        //}


        //public RptPci_Novo_EY4 GetReportPci_EY4(bool PciComAso, int zCopias, Int32 xOrder, string xDataBranco, Int32 rOrder, bool rimpData)
        //{
        //    this.PciComAso = PciComAso;

        //    dsAdendo.Tables.Add(GetDataTableAdendo());

        //    if (clinico != null)
        //    {
        //        DataSourceAso(clinico, xDataBranco);
        //        DataSourcePci(clinico);
        //        DataSourceAdendo(clinico);
        //    }
        //    else
        //        DataSourceAso(true, xDataBranco);

        //    RptPci_Novo_EY4 reportPci = new RptPci_Novo_EY4();


        //    DataSet dsExame_Cop = new DataSet();

        //    dsExame_Cop.Tables.Add(GetDataTableReportAso());


        //    for (int zCont = 0; zCont < dsExame.Tables[0].Rows.Count; zCont++)
        //    {
        //        for (int zAux = 0; zAux < zCopias; zAux++)
        //        {

        //            dsExame_Cop.Tables[0].ImportRow(dsExame.Tables[0].Rows[zCont]);
        //            if (zAux == 0)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empresa )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
        //            }
        //            else if (zAux == 1)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Empregado )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
        //            }
        //            else if (zAux == 2)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Consultoria )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "-";
        //            }
        //            else if (zAux == 3)
        //            {
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tx_Espera"] = "Via ( Clínica )";
        //                dsExame_Cop.Tables[0].Rows[(zCont * zCopias) + zAux]["tVia"] = "";
        //            }
        //        }

        //    }



        //    DataSet dsTransf = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsExame.Tables[0].DefaultView.Sort = "tNO_GHE, tNO_EMPG";
        //    else
        //        dsExame.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf.Tables.Add(dsExame.Tables[0].DefaultView.ToTable());


        //    DataSet dsTransf2 = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsExame_Cop.Tables[0].DefaultView.Sort = "tNO_GHE, tNO_EMPG";
        //    else
        //        dsExame_Cop.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf2.Tables.Add(dsExame_Cop.Tables[0].DefaultView.ToTable());


        //    DataSet dsTransf4 = new DataSet(); //Declare a dataSet to be filled.

        //    if (xOrder == 1)
        //        dsPci.Tables[0].DefaultView.Sort = "tNO_GHE";  //, tNO_EMPG2";
        //    else
        //        dsPci.Tables[0].DefaultView.Sort = "tNO_EMPG";

        //    dsTransf4.Tables.Add(dsPci.Tables[0].DefaultView.ToTable());




        //    //reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsExame_Cop);
        //    reportPci.OpenSubreport("RptAsoViaEmpregador.rpt").SetDataSource(dsTransf2);

        //    //reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsExame);
        //    reportPci.OpenSubreport("RptAsoViaPci.rpt").SetDataSource(dsTransf);
        //    reportPci.OpenSubreport("RptAdendo").SetDataSource(dsAdendo);



        //    DataSourceExameAnamnese xAnam = new DataSourceExameAnamnese(convocacao, pcmso, true, true);
        //    reportPci.OpenSubreport("RptAnamnese.rpt").SetDataSource(xAnam.GetReportConvocacao());

        //    reportPci.SetDataSource(dsTransf4); //dsPci);
        //    //reportPci.SetDataSource(dsTransf3);
        //    reportPci.Refresh();


        //    SetTempoProcessamento(reportPci);

        //    return reportPci;
        //}

        #endregion


        #endregion

        #region DataTables

        #region InicializarTables

        private void InicializarTables()
        {
            dsPrincipal.Tables.Add(GetDataTablePrincipal());
            dsExame.Tables.Add(GetDataTableReportAso());
            dsExame_Vazio.Tables.Add(GetDataTableReportAso());
            dsPci.Tables.Add(GetDataTableReportPci());
        }
        #endregion

        #region GetDataTablePrincipal

        private static DataTable GetDataTablePrincipal()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdExame", Type.GetType("System.Int32"));
            table.Columns.Add("Cabecalho", Type.GetType("System.String"));
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
            table.Columns.Add("ExamesComplementares2", Type.GetType("System.String"));
            table.Columns.Add("iFOTO", Type.GetType("System.String"));
            table.Columns.Add("DataComplementares", Type.GetType("System.String"));
            table.Columns.Add("DataComplementares2", Type.GetType("System.String"));

            table.Columns.Add("Sistema", Type.GetType("System.String"));

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
            table.Columns.Add("Titulo", Type.GetType("System.String"));


            table.Columns.Add("Admissao", Type.GetType("System.String"));

            table.Columns.Add("Faturar", Type.GetType("System.String"));

            table.Columns.Add("Contrato_Empresa", Type.GetType("System.String"));
            table.Columns.Add("Contrato_CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Contrato_Numero", Type.GetType("System.String"));
            table.Columns.Add("Contrato_Endereco", Type.GetType("System.String"));
            table.Columns.Add("Contrato_CEP", Type.GetType("System.String"));
            table.Columns.Add("Contrato_Cidade_UF", Type.GetType("System.String"));

            table.Columns.Add("Codigo_Barras", Type.GetType("System.String"));
            table.Columns.Add("Codigo_Barras_Numero", Type.GetType("System.String"));


            table.Columns.Add("AP_Trabalho_Altura", Type.GetType("System.String"));
            table.Columns.Add("AP_Trabalho_Altura_Apto", Type.GetType("System.String"));
            table.Columns.Add("AP_Trabalho_Altura_Inapto", Type.GetType("System.String"));

            table.Columns.Add("AP_Espacos_Confinados", Type.GetType("System.String"));
            table.Columns.Add("AP_Espacos_Confinados_Apto", Type.GetType("System.String"));
            table.Columns.Add("AP_Espacos_Confinados_Inapto", Type.GetType("System.String"));

            table.Columns.Add("AP_Eletricidade", Type.GetType("System.String"));
            table.Columns.Add("AP_Eletricidade_Apto", Type.GetType("System.String"));
            table.Columns.Add("AP_Eletricidade_Inapto", Type.GetType("System.String"));

            table.Columns.Add("AP_Brigadista", Type.GetType("System.String"));
            table.Columns.Add("AP_Brigadista_Apto", Type.GetType("System.String"));
            table.Columns.Add("AP_Brigadista_Inapto", Type.GetType("System.String"));

            table.Columns.Add("AP_Socorrista", Type.GetType("System.String"));
            table.Columns.Add("AP_Socorrista_Apto", Type.GetType("System.String"));
            table.Columns.Add("AP_Socorrista_Inapto", Type.GetType("System.String"));

            table.Columns.Add("AP_Motorizado", Type.GetType("System.String"));
            table.Columns.Add("AP_Motorizado_Apto", Type.GetType("System.String"));
            table.Columns.Add("AP_Motorizado_Inapto", Type.GetType("System.String"));

            table.Columns.Add("AP_Submersas", Type.GetType("System.String"));
            table.Columns.Add("AP_Submersas_Apto", Type.GetType("System.String"));
            table.Columns.Add("AP_Submersas_Inapto", Type.GetType("System.String"));

            table.Columns.Add("AP_Aquaviarias", Type.GetType("System.String"));
            table.Columns.Add("AP_Aquaviarias_Apto", Type.GetType("System.String"));
            table.Columns.Add("AP_Aquaviarias_Inapto", Type.GetType("System.String"));

            table.Columns.Add("AP_Respiradores", Type.GetType("System.String"));
            table.Columns.Add("AP_Respiradores_Apto", Type.GetType("System.String"));
            table.Columns.Add("AP_Respiradores_Inapto", Type.GetType("System.String"));

            table.Columns.Add("AP_Alimentos", Type.GetType("System.String"));
            table.Columns.Add("AP_Alimentos_Apto", Type.GetType("System.String"));
            table.Columns.Add("AP_Alimentos_Inapto", Type.GetType("System.String"));

            table.Columns.Add("AP_Trabalho_Bordo", Type.GetType("System.String"));
            table.Columns.Add("AP_Trabalho_Bordo_Apto", Type.GetType("System.String"));
            table.Columns.Add("AP_Trabalho_Bordo_Inapto", Type.GetType("System.String"));

            table.Columns.Add("AP_Outros", Type.GetType("System.String"));
            table.Columns.Add("AP_Outros_Apto", Type.GetType("System.String"));
            table.Columns.Add("AP_Outros_Inapto", Type.GetType("System.String"));

            return table;
        }
        #endregion

        #region GetDataTableReportPci

        public static DataTable GetDataTableReportPci()
        {
            DataTable table = new DataTable("ResultPci");

            //ExameFisico
            table.Columns.Add("IdExame", Type.GetType("System.Int32"));

            table.Columns.Add("tNO_EMPG", Type.GetType("System.String"));
            //table.Columns.Add("tNO_EMPG2", Type.GetType("System.String"));
            table.Columns.Add("tNO_GHE", Type.GetType("System.String"));

            table.Columns.Add("hasAso", Type.GetType("System.Boolean"));
            table.Columns.Add("hasAdendo", Type.GetType("System.Boolean"));
            table.Columns.Add("PressaoArterial", Type.GetType("System.String"));
            table.Columns.Add("Pulso", Type.GetType("System.String"));
            table.Columns.Add("Altura", Type.GetType("System.String"));
            table.Columns.Add("Peso", Type.GetType("System.String"));
            table.Columns.Add("DataUltimaMenstruacao", Type.GetType("System.String"));
            table.Columns.Add("hasCabecaAlterado", Type.GetType("System.String"));
            table.Columns.Add("hasCoracaoAlterado", Type.GetType("System.String"));
            table.Columns.Add("hasPulmaoAlterado", Type.GetType("System.String"));
            table.Columns.Add("hasMIAlterado", Type.GetType("System.String"));
            table.Columns.Add("hasPeleAlterado", Type.GetType("System.String"));
            table.Columns.Add("hasMSAlterado", Type.GetType("System.String"));
            table.Columns.Add("hasAbdomemAlterado", Type.GetType("System.String"));
            table.Columns.Add("hasOsteoAlterado", Type.GetType("System.String"));

            table.Columns.Add("hasPlano", Type.GetType("System.String"));
            table.Columns.Add("hasDB", Type.GetType("System.String"));

            table.Columns.Add("IMC", Type.GetType("System.String"));

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


            table.Columns.Add("tCONTATO_MEDICO", Type.GetType("System.String"));
            table.Columns.Add("Coord_PCMSO", Type.GetType("System.String"));
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

            table.Columns.Add("Sistema", Type.GetType("System.String"));

            table.Columns.Add("Faturar", Type.GetType("System.String"));
            table.Columns.Add("RazaoSocial", Type.GetType("System.String"));
            table.Columns.Add("CGC", Type.GetType("System.String"));
            table.Columns.Add("Endereco", Type.GetType("System.String"));
            table.Columns.Add("CEP", Type.GetType("System.String"));
            table.Columns.Add("Estado", Type.GetType("System.String"));
            
            table.Columns.Add("Admissao", Type.GetType("System.String"));
            table.Columns.Add("tNO_FUNC_EMPR", Type.GetType("System.String"));
            table.Columns.Add("tNO_STR_EMPR", Type.GetType("System.String"));
            table.Columns.Add("tNO_IDENTIDADE", Type.GetType("System.String"));
            table.Columns.Add("nSexo", Type.GetType("System.String"));
            
            table.Columns.Add("Codigo_Barras", Type.GetType("System.String"));
            table.Columns.Add("Codigo_Barras_Numero", Type.GetType("System.String"));
            table.Columns.Add("tDS_EXAME_TIPO", Type.GetType("System.String"));
            table.Columns.Add("iFOTO", Type.GetType("System.String"));
            table.Columns.Add("ClinicaCidade", Type.GetType("System.String"));


            table.Columns.Add("Contrato_Empresa", Type.GetType("System.String"));
            table.Columns.Add("Contrato_CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Contrato_Numero", Type.GetType("System.String"));
            table.Columns.Add("Contrato_Endereco", Type.GetType("System.String"));
            table.Columns.Add("Contrato_CEP", Type.GetType("System.String"));
            table.Columns.Add("Contrato_Cidade_UF", Type.GetType("System.String"));

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

        public DataSet DataSourcePrincipal(Clinico clinico, string xCabecalho)
        {
            DataRow newRow = dsPrincipal.Tables[0].NewRow();
            newRow["IdExame"] = clinico.Id;
            newRow["Cabecalho"] = xCabecalho;
            dsPrincipal.Tables[0].Rows.Add(newRow);
            return dsPrincipal;
        }
        #endregion

        #region DataSourceAso

        private void DataSourceAso(string xDataBranco)
        {
            DataSourceAso(false, xDataBranco);
        }


        private void DataSourceAso()
        {
            DataSourceAso(false, "");
        }

        private void DataSourceAso(bool ComPci, string xDataBranco)
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

                //DataSourcePrincipal(clinico); //teste

                if (clinico.IdEmpregadoFuncao.nID_FUNCAO == null && this.clinico != null)
                {
                    clinico.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(this.clinico.IdEmpregado);
                }

                clinico.ValidarDadosAso();


                //teste - se for convocacao,  carregar 3 vezes seguidas o mesmo registro no dataset
                //wagner

                if (ComPci)
                {

                    DataRow newRow = dsExame.Tables[0].NewRow();

                    PopularRow pRow = new PopularRow(newRow);

                    pRow.ExameEmpregado(clinico, convocacao);
                    pRow.AvaliacaoAmbiental(clinico, pcmso, ghes, ExamesSomentePeriodoPcmso, xDataBranco);
                    pRow.Foto(clinico, cliente, SemFoto);
                    pRow.DataNascimentoIdentidade(clinico);
                    pRow.Resultado(clinico);
                    pRow.ExameTipo(convocacao);
                    pRow.Juridica(convocacao);
                    pRow.Medico(convocacao);
                    pRow.ContatoMedico(convocacao);
                    pRow.CoordenadorPcmso(sNomeCoordenador);


                    DataSourcePci(clinico);
                    DataSourceAdendo(clinico);

                    dsExame.Tables[0].Rows.Add(newRow);

                }
                else
                {

                    //for (int zCont = 1; zCont <= convocacao.Copias; zCont++)
                    //{

                        DataRow newRow = dsExame.Tables[0].NewRow();

                        PopularRow pRow = new PopularRow(newRow);

                        pRow.ExameEmpregado(clinico, convocacao);
                        pRow.AvaliacaoAmbiental(clinico, pcmso, ghes, ExamesSomentePeriodoPcmso, xDataBranco);
                        pRow.Foto(clinico, cliente, SemFoto);
                        pRow.DataNascimentoIdentidade(clinico);
                        pRow.Resultado(clinico);
                        pRow.ExameTipo(convocacao);
                        pRow.Juridica(convocacao);
                        pRow.Medico(convocacao);
                        pRow.ContatoMedico(convocacao);
                        pRow.CoordenadorPcmso(sNomeCoordenador);

                        //if (zCont == 1)
                        //{
                            newRow["tx_Espera"] = "Via Empresa";
                            newRow["tVia"] = "-";
                        //}
                        //else if (zCont == 2)
                        //{
                        //    newRow["tx_Espera"] = "Via Empregado";
                        //    newRow["tVia"] = "";
                        //}
                        //else if (zCont == 3)
                        //{
                        //    newRow["tx_Espera"] = "Via Ilitera";
                        //    newRow["tVia"] = "-";
                        //}
                        //else if (zCont == 4)
                        //{
                        //    newRow["tx_Espera"] = "Via Clínica";
                        //    newRow["tVia"] = "";
                        //}

                        //if (ComPci && zCont == 1 )
                        //{
                        //    DataSourcePci(clinico);
                        //    DataSourceAdendo(clinico);
                        //}


                        dsExame.Tables[0].Rows.Add(newRow);

                    }
               // }
            }

            if (ProgressFinalizar != null)
                ProgressFinalizar();
        }

        private void DataSourceAso(Clinico clinico)
        {

            //if (clinico.IdEmpregadoFuncao.nID_FUNCAO == null && this.clinico != null)
            //{
            //clinico.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(this.clinico.IdEmpregado);
            //}

            this.clinico.IdPcmso.Find();
            this.clinico.IdPcmso.IdCliente.Find();
            this.clinico.IdPcmso.IdCliente.IdGrupoEmpresa.Find();

            clinico.IdEmpregadoFuncao.Find();

            if ( this.clinico.IdPcmso.IdCliente.IdGrupoEmpresa.Descricao.IndexOf("CAPGEMINI")>0)
            {
                if (clinico.IdEmpregadoFuncao.Id==0)
                   clinico.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao( this.clinico.IdPcmso.IdCliente, this.clinico.IdEmpregado);
            }
            else
            {
                if (clinico.IdEmpregadoFuncao.Id == 0)
                    clinico.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(this.clinico.IdEmpregado);
            }

            clinico.ValidarDadosAso();

            DataRow newRow = dsExame.Tables[0].NewRow();

            PopularRow pRow = new PopularRow(newRow);

            pRow.ExameEmpregado(clinico);
            pRow.AvaliacaoAmbiental(clinico, pcmso, null, ExamesSomentePeriodoPcmso, "");
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


        private void DataSourceAso(Clinico clinico, string xDataBranco)
        {

            //if (clinico.IdEmpregadoFuncao.nID_FUNCAO == null && this.clinico != null)
            //{
            clinico.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(this.clinico.IdEmpregado);
            //}

            clinico.ValidarDadosAso();

            DataRow newRow = dsExame.Tables[0].NewRow();

            PopularRow pRow = new PopularRow(newRow);

            pRow.ExameEmpregado(clinico);
            pRow.AvaliacaoAmbiental(clinico, pcmso, null, ExamesSomentePeriodoPcmso, xDataBranco);
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

        public class PopularRow
        {
            private DataRow newRow;

            public PopularRow(DataRow newRow)
            {
                this.newRow = newRow;
            }

            #region ExameEmpregado

            public void ExameEmpregado(Clinico clinico)
            {

                clinico.IdEmpregado.Find();
                newRow["IdExame"] = clinico.Id;
                newRow["tNO_EMPG"] = clinico.IdEmpregado.tNO_EMPG; //.GetNomeEmpregadoComRE();
                newRow["nSEXO"] = clinico.IdEmpregado.tSEXO;
                //newRow["tNO_STR_EMPR"] = "Setor de " + clinico.IdEmpregadoFuncao.GetNomeSetor();
                newRow["tNO_STR_EMPR"] = clinico.IdEmpregadoFuncao.GetNomeSetor();

                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                newRow["Admissao"] = clinico.IdEmpregado.hDT_ADM.ToString("dd/MM/yyyy", ptBr);

                //newRow["tNO_FUNC_EMPR"] = "Função de " + clinico.IdEmpregadoFuncao.GetNomeFuncao();




                //CODIGO DE BARRAS  clinico->CodBusca
                //ativar toda parte para salvar esse código - ativar FormPastaTrabalho_Codigo
                //se vier em branco, como nos exames antigos, o código de barras não aparecerá, não precisa ajustar nada

                if (clinico.CodBusca != null && clinico.CodBusca != 0)
                {
                    newRow["Codigo_Barras"] = "*" + clinico.CodBusca.ToString().Trim() + "*";
                    newRow["Codigo_Barras_Numero"] = clinico.CodBusca.ToString().Trim();
                }
                else
                {
                    newRow["Codigo_Barras"] = "";
                    newRow["Codigo_Barras_Numero"] = "";
                }



                //FATURAR

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("METODO") > 0) //Daiti
                {
                    newRow["Faturar"] = "Ilitera Método" + System.Environment.NewLine + "33.233.895/0001-63 - (11)98701-1223";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_PRO") > 0)
                {
                    newRow["Faturar"] = "Ilitera PRO Segurança Saúde e Qualidade de Vida Ltda" + System.Environment.NewLine + "40.017.926/0001-04";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                {
                    newRow["Faturar"] = "Essence: Prana Gestão Ocupacional Ltda" + System.Environment.NewLine + "17.898.088/0001-03 - (11) 2344-4585";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().Trim() == "OPSA" || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("VIA") > 0)
                {
                    newRow["Faturar"] = "Ilitera UNO - Segurança, Saúde e Qualidade de Vida Ltda" + System.Environment.NewLine + "CNPJ  36.534.756/0001-68  Telefone 11 4249-4949";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0) //Daiti
                {
                    newRow["Faturar"] = "Assessoria: Ilitera Daiiti Segurança, Saúde e Qualidade de Vida Ltda" + System.Environment.NewLine + "06.259.938/0001-07 - (11)2506-2054 / 2507-2054";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Mappas") > 0)
                {
                    newRow["Faturar"] = "Assessoria: Mappas-SST Segurança do Trabalho Ltda ME" + System.Environment.NewLine + "10.688.659/0001-36 - (12)3426-9186";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("QTECK") > 0)
                {
                    newRow["Faturar"] = "QTECK ILITERA" + System.Environment.NewLine + "22.164.410/0001-00 - (11)4941-2288";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                {
                    clinico.IdEmpregado.Find();
                    clinico.IdEmpregado.nID_EMPR.Find();

                    Juridica xJur = new Juridica();
                    xJur.Find(clinico.IdEmpregado.nID_EMPR.Id);

                    if (xJur.Id != 0)
                    {
                        if (xJur.Auxiliar == "RAMO" || xJur.Auxiliar == "GLOBAL2")
                        {
                            newRow["Faturar"] = "RAMO ASS.EMPRESARIAL - CNPJ 28.495.277/0001-51 - (11) 93266-8098  faturamento2@globalsegmed.com.br";
                        }
                        //else if (xJur.Auxiliar == "GLOBAL2")
                        //{
                        //    newRow["Faturar"] = "GLOBALSEGMED - CNPJ 43.640.229/0001-01 - (11) 96718-0199  faturamento2@globalsegmed.com.br";
                        //}
                        else
                        {
                            newRow["Faturar"] = "GLOBALSEG - CNPJ 22.159.703/0001-08 - (11) 93266-8098  faturamento@globalsegmed.com.br";
                        }
                    }
                    else
                    {
                        newRow["Faturar"] = "GLOBALSEG - CNPJ 22.159.703/0001-08 - (11) 93266-8098  faturamento@globalsegmed.com.br";
                    }

                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("FOCUS") < 0 && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SAFETY") > 0)
                {
                    newRow["Faturar"] = "ILITERA GRAFENO EIRELI – CNPJ 27.013.373/0001-53  ou" + System.Environment.NewLine + "ILITERA GRAFENO LTDA – CNPJ 41.109.740/0001-48" + System.Environment.NewLine + "Obs: Faturar no CNPJ previsto em contrato.";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("FOCUS") > 0 && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SAFETY") > 0)
                {
                    newRow["Faturar"] = "ILITERA SAFETY" + System.Environment.NewLine + "CNPJ 36.196.609/0001-25"; //  - (11)98913-4933 / (11)94504-4728";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SOMA") > 0)
                {
                    newRow["Faturar"] = "Soma Ilitera (Soma Segurança Ocupacional Ltda)" + System.Environment.NewLine + "CNPJ: 04.170.948/0001-46 - (13)3229-1770";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SDTOURIN") > 0)
                {
                    newRow["Faturar"] = "ILITERA TURIN   CNPJ: 33.606.042/0001-20" + System.Environment.NewLine + "(11)2363-2245 / (11)94743-1615";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("FOX") > 0)
                {
                    newRow["Faturar"] = "ILITERA FOX" + System.Environment.NewLine + "CNPJ: 25.400.875/0001-01 - (55)3422-4891";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRIME") > 0)
                {
                    newRow["Faturar"] = "Ilitera Prime Assessoria em Segurança e Saúde Ltda - 33.876.636/0001-50  (11)4801-6300";
                }
                else
                {
                    //newRow["Frase"] = "Ilitera Via - Segurança, Saúde e Qualidade de Vida Ltda" + System.Environment.NewLine + "CNPJ  28.353.986/0001-00  Telefone 11 2356-7660";
                    newRow["Faturar"] = "Ilitera UNO - Segurança, Saúde e Qualidade de Vida Ltda" + System.Environment.NewLine + "CNPJ  36.534.756/0001-68  Telefone 11 4249-4949";
                }





                clinico.IdEmpregadoFuncao.nID_EMPR.Find();

                if (clinico.IdEmpregadoFuncao.nID_EMPR.NomeAbreviado.ToUpper().IndexOf("DRIVEWAY") >= 0)
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

                    if (zCargo != "")
                    {
                        // newRow["tNO_FUNC_EMPR"] = "Função: " + clinico.IdEmpregadoFuncao.GetNomeFuncao() + "   Cargo: " + zCargo;
                        newRow["tNO_FUNC_EMPR"] = "Cargo: " + zCargo;
                    }
                    else
                    {
                        newRow["tNO_FUNC_EMPR"] = "Cargo: " + clinico.IdEmpregadoFuncao.GetNomeFuncao();
                    }
                }
                else
                {
                    newRow["tNO_FUNC_EMPR"] = clinico.IdEmpregadoFuncao.GetNomeFuncao();
                }





                Cliente xCliente2 = new Cliente();
                xCliente2.Find(clinico.IdEmpregadoFuncao.nID_EMPR.Id);



                //CONTRATO
                if (xCliente2.Contrato_Numero.Trim() != "")
                {
                    newRow["Contrato_Numero"] = "Contrato " + xCliente2.Contrato_Numero;
                }
                else
                {
                    newRow["Contrato_Numero"] = "";
                }



                Cliente xCliente = new Cliente();
                xCliente.Find(clinico.IdEmpregado.nID_EMPR.Id);



                if (xCliente.Municipio_Data_ASO == "N")
                {
                    newRow["dDT_EXAME"] = clinico.DataExame.ToString("dd-MM-yyyy") + ".";
                }
                else if (xCliente.Municipio_Data_ASO == "C")
                {
                    newRow["dDT_EXAME"] = clinico.IdJuridica.GetMunicipio() + " , " + clinico.DataExame.ToString("dd-MM-yyyy") + ".";
                }
                else if (xCliente.Municipio_Data_ASO == "E")
                {
                    newRow["dDT_EXAME"] = xCliente.GetMunicipio() + " , " + clinico.DataExame.ToString("dd-MM-yyyy") + ".";
                }
                else
                {
                    newRow["dDT_EXAME"] = clinico.DataExame.ToString("dd-MM-yyyy") + ".";
                }


                newRow["nRS_Exame"] = clinico.IndResultado.ToString();


                string xAptidao = "";

                Empregado_Aptidao rAptidao = new Empregado_Aptidao();
                clinico.IdEmpregado.Find();
                rAptidao.Find(" nId_Empregado = " + clinico.IdEmpregado.Id.ToString());

                clinico.IdPcmso.Find();
                clinico.IdPcmso.IdLaudoTecnico.Find();
                Int32 zIdGHE = clinico.IdEmpregadoFuncao.GetIdGheEmpregado(clinico.IdPcmso.IdLaudoTecnico);
                GHE_Aptidao zAptidao = new GHE_Aptidao();
                zAptidao.Find("nId_Func = " + zIdGHE.ToString());


                newRow["AP_Alimentos"] = "NA";
                newRow["AP_Aquaviarias"] = "NA";
                newRow["AP_Eletricidade"] = "NA";
                newRow["AP_Espacos_Confinados"] = "NA";
                newRow["AP_Submersas"] = "NA";
                newRow["AP_Trabalho_Altura"] = "NA";
                newRow["AP_Motorizado"] = "NA";
                newRow["AP_Brigadista"] = "NA";
                newRow["AP_Socorrista"] = "NA";
                newRow["AP_Respiradores"] = "NA";
                newRow["AP_Trabalho_Bordo"] = "NA";
                newRow["AP_Outros"] = "NA";


                newRow["AP_Alimentos_Apto"] = "-";
                newRow["AP_Aquaviarias_Apto"] = "-";
                newRow["AP_Eletricidade_Apto"] = "-";
                newRow["AP_Espacos_Confinados_Apto"] = "-";
                newRow["AP_Submersas_Apto"] = "-";
                newRow["AP_Trabalho_Altura_Apto"] = "-";
                newRow["AP_Motorizado_Apto"] = "-";
                newRow["AP_Brigadista_Apto"] = "-";
                newRow["AP_Socorrista_Apto"] = "-";
                newRow["AP_Respiradores_Apto"] = "-";
                newRow["AP_Trabalho_Bordo_Apto"] = "-";
                newRow["AP_Outros_Apto"] = "-";


                newRow["AP_Alimentos_Inapto"] = "-";
                newRow["AP_Aquaviarias_Inapto"] = "-";
                newRow["AP_Eletricidade_Inapto"] = "-";
                newRow["AP_Espacos_Confinados_Inapto"] = "-";
                newRow["AP_Submersas_Inapto"] = "-";
                newRow["AP_Trabalho_Altura_Inapto"] = "-";
                newRow["AP_Motorizado_Inapto"] = "-";
                newRow["AP_Brigadista_Inapto"] = "-";
                newRow["AP_Socorrista_Inapto"] = "-";
                newRow["AP_Respiradores_Inapto"] = "-";
                newRow["AP_Trabalho_Bordo_Inapto"] = "-";
                newRow["AP_Outros_Inapto"] = "-";

                clinico.IdExameDicionario.Find();




                bool vAptidoes = true;

                if (xCliente.Aptidoes_Demissional != null)
                {
                    if (clinico.IdExameDicionario.Id == 2 && xCliente.Aptidoes_Demissional != true)
                    {
                        vAptidoes = false;
                    }
                }
                else
                {
                    vAptidoes = false;
                }


                if (xCliente.Aptidoes_Retorno != null)
                {
                    if (clinico.IdExameDicionario.Id == 5 && xCliente.Aptidoes_Retorno != true)
                    {
                        vAptidoes = false;
                    }
                }
                else
                {
                    vAptidoes = false;
                }


                //if ((rAptidao.Id != 0 || zAptidao.Id != 0) && (clinico.IdExameDicionario.Id != 2) && (clinico.IdExameDicionario.Id != 5))
                if ((rAptidao.Id != 0 || zAptidao.Id != 0) && (vAptidoes == true))
                {

                    //clinico.IdPcmso.Find();
                    //clinico.IdPcmso.IdCliente.Find();
                    //Cliente zCliente = new Cliente();
                    //zCliente.Find(clinico.IdPcmso.IdCliente.Id);


                    //if (zCliente.Riscos_PPRA == false)
                    //{
                    //xAptidao = "Aptidão para: ";


                    if (rAptidao.apt_Alimento == true || zAptidao.apt_Alimento == true)
                    {
                        //xAptidao = xAptidao + " manipular alimentos (item 4.6.1 da Resolução RDC nº216/2004) /";
                        newRow["AP_Alimentos"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Alimentos_Apto"] = "X"; newRow["AP_Alimentos_Inapto"] = " "; }
                        else { newRow["AP_Alimentos_Apto"] = " "; newRow["AP_Alimentos_Inapto"] = " "; }
                    }

                    if (rAptidao.apt_Aquaviario == true || zAptidao.apt_Aquaviario == true)
                    {
                        //xAptidao = xAptidao + " serviços aquaviários (NR 30) /";
                        newRow["AP_Aquaviarias"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Aquaviarias_Apto"] = "X"; newRow["AP_Aquaviarias_Inapto"] = " "; }
                        else { newRow["AP_Aquaviarias_Apto"] = " "; newRow["AP_Aquaviarias_Inapto"] = " "; }
                    }

                    if (rAptidao.apt_Eletricidade == true || zAptidao.apt_Eletricidade == true)
                    {
                        //xAptidao = xAptidao + " serviços em eletricidade (NR 10) /";
                        newRow["AP_Eletricidade"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Eletricidade_Apto"] = "X"; newRow["AP_Eletricidade_Inapto"] = " "; }
                        else { newRow["AP_Eletricidade_Apto"] = " "; newRow["AP_Eletricidade_Inapto"] = " "; }
                    }

                    if (rAptidao.apt_Espaco_Confinado == true || zAptidao.apt_Espaco_Confinado == true)
                    {
                        //xAptidao = xAptidao + " trabalho em espaços confinados (NR 33) /";
                        newRow["AP_Espacos_Confinados"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Espacos_Confinados_Apto"] = "X"; newRow["AP_Espacos_Confinados_Inapto"] = " "; }
                        else { newRow["AP_Espacos_Confinados_Apto"] = " "; newRow["AP_Espacos_Confinados_Inapto"] = " "; }
                    }

                    if (rAptidao.apt_Submerso == true || zAptidao.apt_Submerso == true)
                    {
                        //xAptidao = xAptidao + " atividades submersas (NR 15) /";
                        newRow["AP_Submersas"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Submersas_Apto"] = "X"; newRow["AP_Submersas_Inapto"] = " "; }
                        else { newRow["AP_Submersas_Apto"] = " "; newRow["AP_Submersas_Inapto"] = " "; }
                    }

                    if (rAptidao.apt_Trabalho_Altura == true || zAptidao.apt_Trabalho_Altura == true)
                    {
                        //xAptidao = xAptidao + " trabalho em altura (NR 35) /";
                        newRow["AP_Trabalho_Altura"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Trabalho_Altura_Apto"] = "X"; newRow["AP_Trabalho_Altura_Inapto"] = " "; }
                        else { newRow["AP_Trabalho_Altura_Apto"] = " "; newRow["AP_Trabalho_Altura_Inapto"] = " "; }
                    }

                    if (rAptidao.apt_Trabalho_Bordo == true || zAptidao.apt_Trabalho_Bordo == true)
                    {
                        newRow["AP_Trabalho_Bordo"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Trabalho_Bordo_Apto"] = "X"; newRow["AP_Trabalho_Bordo_Inapto"] = " "; }
                        else { newRow["AP_Trabalho_Bordo_Apto"] = " "; newRow["AP_Trabalho_Bordo_Inapto"] = " "; }
                    }


                    if (rAptidao.apt_Transporte == true || zAptidao.apt_Transporte == true)
                    {
                        //xAptidao = xAptidao + " operar equipamentos de transporte motorizados (NR 11) /";
                        newRow["AP_Motorizado"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Motorizado_Apto"] = "X"; newRow["AP_Motorizado_Inapto"] = " "; }
                        else { newRow["AP_Motorizado_Apto"] = " "; newRow["AP_Motorizado_Inapto"] = " "; }
                    }

                    if (rAptidao.apt_Brigadista == true || zAptidao.apt_Brigadista == true)
                    {
                        //xAptidao = xAptidao + " trabalho como Brigadista /";
                        newRow["AP_Brigadista"] = "X";
                        newRow["AP_Socorrista"] = " ";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Brigadista_Apto"] = "X"; newRow["AP_Brigadista_Inapto"] = " "; }
                        else { newRow["AP_Brigadista_Apto"] = " "; newRow["AP_Brigadista_Inapto"] = " "; newRow["AP_Socorrista_Apto"] = " "; newRow["AP_Socorrista_Inapto"] = " "; }
                    }

                    if (rAptidao.apt_PPR == true || zAptidao.apt_PPR == true)
                    {
                        //xAptidao = xAptidao + " colaborador em PPR /";
                    }


                    if (rAptidao.apt_Socorrista == true || zAptidao.apt_Socorrista == true)
                    {
                        //xAptidao = xAptidao + " trabalho como Socorrista /";
                        newRow["AP_Socorrista"] = "X";
                        newRow["AP_Brigadista"] = " ";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Socorrista_Apto"] = "X"; newRow["AP_Socorrista_Inapto"] = " "; }
                        else { newRow["AP_Socorrista_Apto"] = " "; newRow["AP_Socorrista_Inapto"] = " "; newRow["AP_Brigadista_Apto"] = " "; newRow["AP_Brigadista_Inapto"] = " "; }
                    }

                    if (rAptidao.apt_Respirador == true && rAptidao.apt_Respirador == true)
                    {
                        //xAptidao = xAptidao + " uso de Respiradores /";
                        newRow["AP_Respiradores"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Respiradores_Apto"] = "X"; newRow["AP_Respiradores_Inapto"] = " "; }
                        else { newRow["AP_Respiradores_Apto"] = " "; newRow["AP_Respiradores_Inapto"] = " "; }
                    }

                    if (newRow["AP_Brigadista"].ToString().Trim() == "NA" || newRow["AP_Brigadista"].ToString().Trim() == "X")
                    {
                        newRow["AP_Socorrista"] = "";
                    }

                    if (newRow["AP_Socorrista"].ToString().Trim() == "NA" || newRow["AP_Socorrista"].ToString().Trim() == "X")
                    {
                        newRow["AP_Brigadista"] = "";
                    }


                    //if (xAptidao == "Aptidão para: ") xAptidao = "";
                    //else xAptidao = xAptidao.Substring(0, xAptidao.Length - 1);



                    //}
                    //else
                    //{
                    //    //Int16 zPar = 0;

                    //    if (rAptidao.apt_Alimento == true || zAptidao.apt_Alimento == true)
                    //    {
                    //        //zPar++;
                    //        // xAptidao = xAptidao + System.Environment.NewLine + " Manipular alimentos           ( )Apto ( )Inapto ";
                    //        newRow["AP_Alimentos"] = "X";
                    //    }

                    //    if (rAptidao.apt_Aquaviario == true || zAptidao.apt_Aquaviario == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Serviços aquaviários          ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Serviços aquaviários         ( )Apto ( )Inapto ";

                    //        newRow["AP_Aquaviarias"] = "X";
                    //    }


                    //    if (rAptidao.apt_Eletricidade == true || zAptidao.apt_Eletricidade == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Serviços em eletricidade      ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Serviços em eletricidade     ( )Apto ( )Inapto ";

                    //        newRow["AP_Eletricidade"] = "X";
                    //    }

                    //    if (rAptidao.apt_Espaco_Confinado == true || zAptidao.apt_Espaco_Confinado == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Espaços confinados            ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Espaços confinados           ( )Apto ( )Inapto ";

                    //        newRow["AP_Espacos_Confinados"] = "X";
                    //    }

                    //    if (rAptidao.apt_Submerso == true || zAptidao.apt_Submerso == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Atividades submersas          ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Atividades submersas          ( )Apto ( )Inapto ";

                    //        newRow["AP_Submersas"] = "X";
                    //    }

                    //    if (rAptidao.apt_Trabalho_Altura == true || zAptidao.apt_Trabalho_Altura == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Trabalho em Altura            ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Trabalho em Altura            ( )Apto ( )Inapto ";

                    //        newRow["AP_Trabalho_Altura"] = "X";
                    //    }

                    //    if (rAptidao.apt_Trabalho_Bordo == true || zAptidao.apt_Trabalho_Bordo == true)
                    //    {
                    //        newRow["AP_Trabalho_Bordo"] = "X";
                    //    }


                    //    if (rAptidao.apt_Transporte == true || zAptidao.apt_Transporte == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Operar Eq.Transp.Motorizados  ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Operar Eq.Transp.Motorizados  ( )Apto ( )Inapto ";

                    //        newRow["AP_Motorizado"] = "X";
                    //    }

                    //    if (rAptidao.apt_Brigadista == true || zAptidao.apt_Brigadista == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Trabalho como Brigadista      ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Trabalho como Brigadista      ( )Apto ( )Inapto ";

                    //        newRow["AP_Brigadista"] = "X";
                    //        newRow["AP_Socorrista"] = " ";
                    //    }

                    //    if (rAptidao.apt_PPR == true || zAptidao.apt_PPR == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Colaborador em PPR            ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Colaborador em PPR            ( )Apto ( )Inapto ";
                    //    }

                    //    if (rAptidao.apt_Socorrista == true || zAptidao.apt_Socorrista == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Trabalho como Socorrista      ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Trabalho como Socorrista      ( )Apto ( )Inapto ";

                    //        newRow["AP_Socorrista"] = "X";
                    //        newRow["AP_Brigadista"] = " ";

                    //    }

                    //    if (rAptidao.apt_Respirador == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Uso de Respiradores           ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Uso de Respiradores           ( )Apto ( )Inapto ";

                    //        newRow["AP_Respiradores"] = "X";
                    //    }

                    //    //if (xAptidao == "") xAptidao = "";
                    //    //else xAptidao = xAptidao.Substring(2);
                    //}
                }
                //else 

                if (xAptidao.Trim() == "")
                {

                    if (clinico.apt_Submerso2 != null && (clinico.apt_Submerso2.ToString().Trim() == "1" || clinico.apt_Submerso2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        //xAptidao = xAptidao + " Apto para atividades submersas.";
                        newRow["AP_Submersas"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Aquaviarias_Apto"] = "X"; newRow["AP_Aquaviarias_Inapto"] = " "; }
                        else { newRow["AP_Aquaviarias_Apto"] = " "; newRow["AP_Aquaviarias_Inapto"] = " "; }
                    }


                    if (clinico.apt_Espaco_Confinado2 != null && (clinico.apt_Espaco_Confinado2.ToString().Trim() == "1" || clinico.apt_Espaco_Confinado2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        //xAptidao = xAptidao + " Apto para trabalho em espaços confinados.";
                        newRow["AP_Espacos_Confinados"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Espacos_Confinados_Apto"] = "X"; newRow["AP_Espacos_Confinados_Inapto"] = " "; }
                        else { newRow["AP_Espacos_Confinados_Apto"] = " "; newRow["AP_Espacos_Confinados_Inapto"] = " "; }
                    }

                    if (clinico.apt_Transporte2 != null && (clinico.apt_Transporte2.ToString().Trim() == "1" || clinico.apt_Transporte2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        // xAptidao = xAptidao + " Apto operar máquinas e equipamentos de transporte motorizado.";
                        newRow["AP_Motorizado"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Motorizado_Apto"] = "X"; newRow["AP_Motorizado_Inapto"] = " "; }
                        else { newRow["AP_Motorizado_Apto"] = " "; newRow["AP_Motorizado_Inapto"] = " "; }
                    }

                    if (clinico.apt_Trabalho_Altura2 != null && (clinico.apt_Trabalho_Altura2.ToString().Trim() == "1" || clinico.apt_Trabalho_Altura2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        //xAptidao = xAptidao + " Apto para trabalho em altura.";
                        newRow["AP_Trabalho_Altura"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Trabalho_Altura_Apto"] = "X"; newRow["AP_Trabalho_Altura_Inapto"] = " "; }
                        else { newRow["AP_Trabalho_Altura_Apto"] = " "; newRow["AP_Trabalho_Altura_Inapto"] = " "; }
                    }

                    if (clinico.apt_Eletricidade2 != null && (clinico.apt_Eletricidade2.ToString().Trim() == "1" || clinico.apt_Eletricidade2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        //xAptidao = xAptidao + " Apto para serviço em eletricidade.";
                        newRow["AP_Eletricidade"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Eletricidade_Apto"] = "X"; newRow["AP_Eletricidade_Inapto"] = " "; }
                        else { newRow["AP_Eletricidade_Apto"] = " "; newRow["AP_Eletricidade_Inapto"] = " "; }
                    }


                    if (clinico.apt_Aquaviario2 != null && (clinico.apt_Aquaviario2.ToString().Trim() == "1" || clinico.apt_Aquaviario2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        //xAptidao = xAptidao + " Apto para serviços aquaviários.";
                        newRow["AP_Aquaviarias"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Aquaviarias_Apto"] = "X"; newRow["AP_Aquaviarias_Inapto"] = " "; }
                        else { newRow["AP_Aquaviarias_Apto"] = " "; newRow["AP_Aquaviarias_Inapto"] = " "; }
                    }

                    if (clinico.apt_Alimento2 != null && (clinico.apt_Alimento2.ToString().Trim() == "1" || clinico.apt_Alimento2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        //xAptidao = xAptidao + " Apto para manipular alimentos.";
                        newRow["AP_Alimentos"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Alimentos_Apto"] = "X"; newRow["AP_Alimentos_Inapto"] = " "; }
                        else { newRow["AP_Alimentos_Apto"] = " "; newRow["AP_Alimentos_Inapto"] = " "; }
                    }

                    if (clinico.apt_Brigadista2 != null && (clinico.apt_Brigadista2.ToString().Trim() == "1" || clinico.apt_Brigadista2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        //xAptidao = xAptidao + " Apto para Brigadista.";
                        newRow["AP_Brigadista"] = "X";
                        newRow["AP_Socorrista"] = " ";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Brigadista_Apto"] = "X"; newRow["AP_Brigadista_Inapto"] = " "; }
                        else { newRow["AP_Brigadista_Apto"] = " "; newRow["AP_Brigadista_Inapto"] = " "; }
                    }

                    if (clinico.apt_Socorrista2 != null && (clinico.apt_Socorrista2.ToString().Trim() == "1" || clinico.apt_Socorrista2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        //xAptidao = xAptidao + " Apto para Socorrista.";
                        newRow["AP_Socorrista"] = "X";
                        newRow["AP_Brigadista"] = " ";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Socorrista_Apto"] = "X"; newRow["AP_Socorrista_Inapto"] = " "; }
                        else { newRow["AP_Socorrista_Apto"] = " "; newRow["AP_Socorrista_Inapto"] = " "; }
                    }

                    if (clinico.apt_Respirador2 != null && (clinico.apt_Respirador2.ToString().Trim() == "1" || clinico.apt_Respirador2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        //xAptidao = xAptidao + " Apto para Uso de Respiradores.";
                        newRow["AP_Respiradores"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Respiradores_Apto"] = "X"; newRow["AP_Respiradores_Inapto"] = " "; }
                        else { newRow["AP_Respiradores_Apto"] = " "; newRow["AP_Respiradores_Inapto"] = " "; }
                    }



                    if (clinico.apt_Submerso2 != null && (clinico.apt_Submerso2.ToString().Trim() == "0" || clinico.apt_Submerso2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto para atividades submersas.";
                        newRow["AP_Submersas"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Aquaviarias_Inapto"] = "X"; newRow["AP_Aquaviarias_Apto"] = " "; }
                        else { newRow["AP_Aquaviarias_Apto"] = " "; newRow["AP_Aquaviarias_Inapto"] = ""; }
                    }


                    if (clinico.apt_Espaco_Confinado2 != null && (clinico.apt_Espaco_Confinado2.ToString().Trim() == "0" || clinico.apt_Espaco_Confinado2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto para trabalho em espaços confinados.";
                        newRow["AP_Espacos_Confinados"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Espacos_Confinados_Inapto"] = "X"; newRow["AP_Espacos_Confinados_Apto"] = " "; }
                        else { newRow["AP_Espacos_Confinados_Inapto"] = " "; newRow["AP_Espacos_Confinados_Apto"] = " "; }
                    }

                    if (clinico.apt_Transporte2 != null && (clinico.apt_Transporte2.ToString().Trim() == "0" || clinico.apt_Transporte2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto operar máquinas e equipamentos de transporte motorizado.";
                        newRow["AP_Motorizado"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Motorizado_Inapto"] = "X"; newRow["AP_Motorizado_Apto"] = " "; }
                        else { newRow["AP_Motorizado_Inapto"] = " "; newRow["AP_Motorizado_Apto"] = " "; }
                    }

                    if (clinico.apt_Trabalho_Altura2 != null && (clinico.apt_Trabalho_Altura2.ToString().Trim() == "0" || clinico.apt_Trabalho_Altura2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto para trabalho em altura.";
                        newRow["AP_Trabalho_Altura"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Trabalho_Altura_Inapto"] = "X"; newRow["AP_Trabalho_Altura_Apto"] = " "; }
                        else { newRow["AP_Trabalho_Altura_Inapto"] = " "; newRow["AP_Trabalho_Altura_Apto"] = " "; }
                    }

                    if (clinico.apt_Eletricidade2 != null && (clinico.apt_Eletricidade2.ToString().Trim() == "0" || clinico.apt_Eletricidade2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto para serviço em eletricidade.";
                        newRow["AP_Eletricidade"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Eletricidade_Inapto"] = "X"; newRow["AP_Eletricidade_Apto"] = " "; }
                        else { newRow["AP_Eletricidade_Inapto"] = " "; newRow["AP_Eletricidade_Apto"] = " "; }
                    }

                    if (clinico.apt_Aquaviario2 != null && (clinico.apt_Aquaviario2.ToString().Trim() == "0" || clinico.apt_Aquaviario2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto para serviços aquaviários.";
                        newRow["AP_Aquaviarias"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Aquaviarias_Inapto"] = "X"; newRow["AP_Aquaviarias_Apto"] = " "; }
                        else { newRow["AP_Aquaviarias_Inapto"] = " "; newRow["AP_Aquaviarias_Apto"] = " "; }
                    }

                    if (clinico.apt_Alimento2 != null && (clinico.apt_Alimento2.ToString().Trim() == "0" || clinico.apt_Alimento2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto para manipular alimentos.";
                        newRow["AP_Alimentos"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Alimentos_Inapto"] = "X"; newRow["AP_Alimentos_Apto"] = " "; }
                        else { newRow["AP_Alimentos_Inapto"] = " "; newRow["AP_Alimentos_Apto"] = " "; }
                    }

                    if (clinico.apt_Brigadista2 != null && (clinico.apt_Brigadista2.ToString().Trim() == "0" || clinico.apt_Brigadista2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto para Brigadista.";
                        newRow["AP_Brigadista"] = "X";
                        newRow["AP_Socorrista"] = " ";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Brigadista_Inapto"] = "X"; newRow["AP_Brigadista_Apto"] = " "; }
                        else { newRow["AP_Brigadista_Inapto"] = " "; newRow["AP_Brigadista_Apto"] = " "; }
                    }

                    if (clinico.apt_Socorrista2 != null && (clinico.apt_Socorrista2.ToString().Trim() == "0" || clinico.apt_Socorrista2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto para Socorrista.";
                        newRow["AP_Socorrista"] = "X";
                        newRow["AP_Brigadista"] = " ";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Socorrista_Inapto"] = "X"; newRow["AP_Socorrista_Apto"] = " "; }
                        else { newRow["AP_Socorrista_Inapto"] = " "; newRow["AP_Socorrista_Apto"] = " "; }
                    }

                    if (clinico.apt_Respirador2 != null && (clinico.apt_Respirador2.ToString().Trim() == "0" || clinico.apt_Respirador2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto para Uso de Respiradores.";
                        newRow["AP_Respiradores"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Respiradores_Inapto"] = "X"; newRow["AP_Respiradores_Apto"] = " "; }
                        else { newRow["AP_Respiradores_Inapto"] = " "; newRow["AP_Respiradores_Apto"] = " "; }
                    }


                    if (newRow["AP_Brigadista"].ToString().Trim() == "NA" || newRow["AP_Brigadista"].ToString().Trim() == "X")
                    {
                        newRow["AP_Socorrista"] = "";
                    }

                    if (newRow["AP_Socorrista"].ToString().Trim() == "NA" || newRow["AP_Socorrista"].ToString().Trim() == "X")
                    {
                        newRow["AP_Brigadista"] = "";
                    }


                }





                string zObs = "";

                switch (clinico.IdEmpregado.nIND_BENEFICIARIO)
                {
                    case (int)TipoBeneficiario.BeneficiarioReabilitado:
                        zObs = "( Beneficiário Reabilitado )";
                        break;
                    case (int)TipoBeneficiario.PortadorDeficiencia:
                        zObs = "( Pessoa com deficiência )";
                        break;
                    case (int)TipoBeneficiario.NaoAplicavel:
                        zObs = "";
                        break;
                    default:
                        zObs = "";
                        break;
                }


                //if (Ilitera.Data.SQLServer.EntitySQLServer.xLocalServer.ToString().ToUpper().IndexOf("EY") > 0)
                //    xAptidao = "";


                //if (zObs + clinico.ObservacaoResultado != "" )
                //   newRow["ObservacaoResultado"] = zObs + clinico.ObservacaoResultado + System.Environment.NewLine + xAptidao; 
                //else
                //   newRow["ObservacaoResultado"] = xAptidao;

                newRow["ObservacaoResultado"] = zObs;

            }


            public void ExameEmpregado(Clinico clinico, ConvocacaoExame convocacao)
            {
                newRow["IdExame"] = clinico.Id;
                newRow["tNO_EMPG"] = clinico.IdEmpregado.tNO_EMPG; //.GetNomeEmpregadoComRE();
                newRow["nSEXO"] = clinico.IdEmpregado.tSEXO;
                //newRow["tNO_STR_EMPR"] = "Setor de " + clinico.IdEmpregadoFuncao.GetNomeSetor();
                newRow["tNO_STR_EMPR"] = "Setor de " + clinico.IdEmpregadoFuncao.GetNomeSetor() + "     CPF " + clinico.IdEmpregado.tNO_CPF;





                clinico.IdEmpregadoFuncao.nID_EMPR.Find();

                if (clinico.IdEmpregadoFuncao.nID_EMPR.NomeAbreviado.ToUpper().IndexOf("DRIVEWAY") >= 0)
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

                    if (zCargo != "")
                    {
                        // newRow["tNO_FUNC_EMPR"] = "Função: " + clinico.IdEmpregadoFuncao.GetNomeFuncao() + "   Cargo: " + zCargo;
                        newRow["tNO_FUNC_EMPR"] = "Cargo: " + zCargo;
                    }
                    else
                    {
                        newRow["tNO_FUNC_EMPR"] = "Cargo: " + clinico.IdEmpregadoFuncao.GetNomeFuncao();
                    }
                }
                else
                {
                    newRow["tNO_FUNC_EMPR"] = clinico.IdEmpregadoFuncao.GetNomeFuncao();
                }



                //newRow["tNO_FUNC_EMPR"] = "Função de " + clinico.IdEmpregadoFuncao.GetNomeFuncao();


                Cliente xCliente = new Cliente();
                xCliente.Find(clinico.IdEmpregado.nID_EMPR.Id);

                if (xCliente.Municipio_Data_ASO == "N")
                {
                    newRow["dDT_EXAME"] = clinico.DataExame.ToString("dd-MM-yyyy") + ".";
                }
                else if (xCliente.Municipio_Data_ASO == "C")
                {
                    newRow["dDT_EXAME"] = clinico.IdJuridica.GetMunicipio() + " , " + clinico.DataExame.ToString("dd-MM-yyyy") + ".";
                }
                else if (xCliente.Municipio_Data_ASO == "E")
                {
                    newRow["dDT_EXAME"] = xCliente.GetMunicipio() + " , " + clinico.DataExame.ToString("dd-MM-yyyy") + ".";
                }
                else
                {
                    newRow["dDT_EXAME"] = clinico.DataExame.ToString("dd-MM-yyyy") + ".";
                }





                newRow["nRS_Exame"] = clinico.IndResultado.ToString();

                string xAptidao = "";


                Empregado_Aptidao rAptidao = new Empregado_Aptidao();
                clinico.IdEmpregado.Find();
                rAptidao.Find(" nId_Empregado = " + clinico.IdEmpregado.Id.ToString());


                clinico.IdPcmso.Find();
                clinico.IdPcmso.IdLaudoTecnico.Find();
                Int32 zIdGHE = clinico.IdEmpregadoFuncao.GetIdGheEmpregado(clinico.IdPcmso.IdLaudoTecnico);
                GHE_Aptidao zAptidao = new GHE_Aptidao();
                zAptidao.Find("nId_Func = " + zIdGHE.ToString());



                if (rAptidao.Id != 0 || zAptidao.Id != 0)
                {

                    //Cliente zCliente = new Cliente();
                    //zCliente.Find(clinico.IdPcmso.IdCliente.Id);


                    //if (zCliente.Riscos_PPRA == false)
                    //{
                    //xAptidao = "Aptidão para: ";


                    if (rAptidao.apt_Alimento == true || zAptidao.apt_Alimento == true)
                    {
                        //xAptidao = xAptidao + " manipular alimentos (item 4.6.1 da Resolução RDC nº216/2004) /";
                        newRow["AP_Alimentos"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Alimentos_Apto"] = "X"; newRow["AP_Alimentos_Inapto"] = " "; }
                        else { newRow["AP_Alimentos_Apto"] = " "; newRow["AP_Alimentos_Inapto"] = " "; }
                    }

                    if (rAptidao.apt_Aquaviario == true || zAptidao.apt_Aquaviario == true)
                    {
                        //xAptidao = xAptidao + " serviços aquaviários (NR 30) /";
                        newRow["AP_Aquaviarias"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Aquaviarias_Apto"] = "X"; newRow["AP_Aquaviarias_Inapto"] = " "; }
                        else { newRow["AP_Aquaviarias_Apto"] = " "; newRow["AP_Aquaviarias_Inapto"] = " "; }
                    }

                    if (rAptidao.apt_Eletricidade == true || zAptidao.apt_Eletricidade == true)
                    {
                        //xAptidao = xAptidao + " serviços em eletricidade (NR 10) /";
                        newRow["AP_Eletricidade"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Eletricidade_Apto"] = "X"; newRow["AP_Eletricidade_Inapto"] = " "; }
                        else { newRow["AP_Eletricidade_Apto"] = " "; newRow["AP_Eletricidade_Inapto"] = " "; }
                    }

                    if (rAptidao.apt_Espaco_Confinado == true || zAptidao.apt_Espaco_Confinado == true)
                    {
                        //xAptidao = xAptidao + " trabalho em espaços confinados (NR 33) /";
                        newRow["AP_Espacos_Confinados"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Espacos_Confinados_Apto"] = "X"; newRow["AP_Espacos_Confinados_Inapto"] = " "; }
                        else { newRow["AP_Espacos_Confinados_Apto"] = " "; newRow["AP_Espacos_Confinados_Inapto"] = " "; }
                    }

                    if (rAptidao.apt_Submerso == true || zAptidao.apt_Submerso == true)
                    {
                        //xAptidao = xAptidao + " atividades submersas (NR 15) /";
                        newRow["AP_Submersas"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Submersas_Apto"] = "X"; newRow["AP_Submersas_Inapto"] = " "; }
                        else { newRow["AP_Submersas_Apto"] = " "; newRow["AP_Submersas_Inapto"] = " "; }
                    }

                    if (rAptidao.apt_Trabalho_Altura == true || zAptidao.apt_Trabalho_Altura == true)
                    {
                        //xAptidao = xAptidao + " trabalho em altura (NR 35) /";
                        newRow["AP_Trabalho_Altura"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Trabalho_Altura_Apto"] = "X"; newRow["AP_Trabalho_Altura_Inapto"] = " "; }
                        else { newRow["AP_Trabalho_Altura_Apto"] = " "; newRow["AP_Trabalho_Altura_Inapto"] = " "; }
                    }

                    if (rAptidao.apt_Trabalho_Bordo == true || zAptidao.apt_Trabalho_Bordo == true)
                    {
                        newRow["AP_Trabalho_Bordo"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Trabalho_Bordo_Apto"] = "X"; newRow["AP_Trabalho_Bordo_Inapto"] = " "; }
                        else { newRow["AP_Trabalho_Bordo_Apto"] = " "; newRow["AP_Trabalho_Bordo_Inapto"] = " "; }
                    }


                    if (rAptidao.apt_Transporte == true || zAptidao.apt_Transporte == true)
                    {
                        //xAptidao = xAptidao + " operar equipamentos de transporte motorizados (NR 11) /";
                        newRow["AP_Motorizado"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Motorizado_Apto"] = "X"; newRow["AP_Motorizado_Inapto"] = " "; }
                        else { newRow["AP_Motorizado_Apto"] = " "; newRow["AP_Motorizado_Inapto"] = " "; }
                    }

                    if (rAptidao.apt_Brigadista == true || zAptidao.apt_Brigadista == true)
                    {
                        //xAptidao = xAptidao + " trabalho como Brigadista /";
                        newRow["AP_Brigadista"] = "X";
                        newRow["AP_Socorrista"] = " ";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Brigadista_Apto"] = "X"; newRow["AP_Brigadista_Inapto"] = " "; }
                        else { newRow["AP_Brigadista_Apto"] = " "; newRow["AP_Brigadista_Inapto"] = " "; newRow["AP_Socorrista_Apto"] = " "; newRow["AP_Socorrista_Inapto"] = " "; }
                    }

                    if (rAptidao.apt_PPR == true || zAptidao.apt_PPR == true)
                    {
                        //xAptidao = xAptidao + " colaborador em PPR /";
                    }


                    if (rAptidao.apt_Socorrista == true || zAptidao.apt_Socorrista == true)
                    {
                        //xAptidao = xAptidao + " trabalho como Socorrista /";
                        newRow["AP_Socorrista"] = "X";
                        newRow["AP_Brigadista"] = " ";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Socorrista_Apto"] = "X"; newRow["AP_Socorrista_Inapto"] = " "; }
                        else { newRow["AP_Socorrista_Apto"] = " "; newRow["AP_Socorrista_Inapto"] = " "; newRow["AP_Brigadista_Apto"] = " "; newRow["AP_Brigadista_Inapto"] = " "; }
                    }

                    if (rAptidao.apt_Respirador != null && rAptidao.apt_Respirador == true)
                    {
                        //xAptidao = xAptidao + " uso de Respiradores /";
                        newRow["AP_Respiradores"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Respiradores_Apto"] = "X"; newRow["AP_Respiradores_Inapto"] = "X"; }
                        else { newRow["AP_Respiradores_Apto"] = " "; newRow["AP_Respiradores_Inapto"] = " "; }
                    }


                    if (newRow["AP_Brigadista"].ToString().Trim() == "NA" || newRow["AP_Brigadista"].ToString().Trim() == "X")
                    {
                        newRow["AP_Socorrista"] = "";
                    }

                    if (newRow["AP_Socorrista"].ToString().Trim() == "NA" || newRow["AP_Socorrista"].ToString().Trim() == "X")
                    {
                        newRow["AP_Brigadista"] = "";
                    }


                    //if (xAptidao == "Aptidão para: ") xAptidao = "";
                    //else xAptidao = xAptidao.Substring(0, xAptidao.Length - 1);

                    //}
                    //else
                    //{
                    //    //Int16 zPar = 0;

                    //    if (rAptidao.apt_Alimento == true || zAptidao.apt_Alimento == true)
                    //    {
                    //        //zPar++;
                    //        // xAptidao = xAptidao + System.Environment.NewLine + " Manipular alimentos           ( )Apto ( )Inapto ";
                    //        newRow["AP_Alimentos"] = "X";
                    //    }

                    //    if (rAptidao.apt_Aquaviario == true || zAptidao.apt_Aquaviario == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Serviços aquaviários          ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Serviços aquaviários         ( )Apto ( )Inapto ";

                    //        newRow["AP_Aquaviarias"] = "X";
                    //    }


                    //    if (rAptidao.apt_Eletricidade == true || zAptidao.apt_Eletricidade == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Serviços em eletricidade      ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Serviços em eletricidade     ( )Apto ( )Inapto ";

                    //        newRow["AP_Eletricidade"] = "X";
                    //    }

                    //    if (rAptidao.apt_Espaco_Confinado == true || zAptidao.apt_Espaco_Confinado == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Espaços confinados            ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Espaços confinados           ( )Apto ( )Inapto ";

                    //        newRow["AP_Espacos_Confinados"] = "X";
                    //    }

                    //    if (rAptidao.apt_Submerso == true || zAptidao.apt_Submerso == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Atividades submersas          ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Atividades submersas          ( )Apto ( )Inapto ";

                    //        newRow["AP_Submersas"] = "X";
                    //    }

                    //    if (rAptidao.apt_Trabalho_Altura == true || zAptidao.apt_Trabalho_Altura == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Trabalho em Altura            ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Trabalho em Altura            ( )Apto ( )Inapto ";

                    //        newRow["AP_Trabalho_Altura"] = "X";
                    //    }

                    //    if (rAptidao.apt_Trabalho_Bordo == true || zAptidao.apt_Trabalho_Bordo == true)
                    //    {                            
                    //        newRow["AP_Trabalho_Bordo"] = "X";
                    //    }

                    //    if (rAptidao.apt_Transporte == true || zAptidao.apt_Transporte == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Operar Eq.Transp.Motorizados  ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Operar Eq.Transp.Motorizados  ( )Apto ( )Inapto ";

                    //        newRow["AP_Motorizado"] = "X";
                    //    }

                    //    if (rAptidao.apt_Brigadista == true || zAptidao.apt_Brigadista == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Trabalho como Brigadista      ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Trabalho como Brigadista      ( )Apto ( )Inapto ";

                    //        newRow["AP_Brigadista"] = "X";
                    //        newRow["AP_Socorrista"] = " ";
                    //    }

                    //    if (rAptidao.apt_PPR == true || zAptidao.apt_PPR == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Colaborador em PPR            ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Colaborador em PPR            ( )Apto ( )Inapto ";
                    //    }

                    //    if (rAptidao.apt_Socorrista == true || zAptidao.apt_Socorrista == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Trabalho como Socorrista      ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Trabalho como Socorrista      ( )Apto ( )Inapto ";

                    //        newRow["AP_Socorrista"] = "X";
                    //        newRow["AP_Brigadista"] = " ";

                    //    }

                    //    if (rAptidao.apt_Respirador != null && rAptidao.apt_Respirador == true)
                    //    {
                    //        //zPar++;
                    //        //if (zPar != 0 && zPar % 2 == 1)
                    //        //    xAptidao = xAptidao + System.Environment.NewLine + " Uso de Respiradores           ( )Apto ( )Inapto ";
                    //        //else
                    //        //    xAptidao = xAptidao + " Uso de Respiradores           ( )Apto ( )Inapto ";

                    //        newRow["AP_Respiradores"] = "X";
                    //    }

                    //    //if (xAptidao == "") xAptidao = "";
                    //    //else xAptidao = xAptidao.Substring(2);
                    //}
                }
                else
                {

                    if (clinico.apt_Submerso2 != null && (clinico.apt_Submerso2.ToString().Trim() == "1" || clinico.apt_Submerso2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        //xAptidao = xAptidao + " Apto para atividades submersas.";
                        newRow["AP_Submersas"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Aquaviarias_Apto"] = "X"; newRow["AP_Aquaviarias_Inapto"] = " "; }
                        else { newRow["AP_Aquaviarias_Apto"] = " "; newRow["AP_Aquaviarias_Inapto"] = " "; }
                    }


                    if (clinico.apt_Espaco_Confinado2 != null && (clinico.apt_Espaco_Confinado2.ToString().Trim() == "1" || clinico.apt_Espaco_Confinado2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        //xAptidao = xAptidao + " Apto para trabalho em espaços confinados.";
                        newRow["AP_Espacos_Confinados"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Espacos_Confinados_Apto"] = "X"; newRow["AP_Espacos_Confinados_Inapto"] = " "; }
                        else { newRow["AP_Espacos_Confinados_Apto"] = " "; newRow["AP_Espacos_Confinados_Inapto"] = " "; }
                    }

                    if (clinico.apt_Transporte2 != null && (clinico.apt_Transporte2.ToString().Trim() == "1" || clinico.apt_Transporte2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        // xAptidao = xAptidao + " Apto operar máquinas e equipamentos de transporte motorizado.";
                        newRow["AP_Motorizado"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Motorizado_Apto"] = "X"; newRow["AP_Motorizado_Inapto"] = " "; }
                        else { newRow["AP_Motorizado_Apto"] = " "; newRow["AP_Motorizado_Inapto"] = " "; }
                    }

                    if (clinico.apt_Trabalho_Altura2 != null && (clinico.apt_Trabalho_Altura2.ToString().Trim() == "1" || clinico.apt_Trabalho_Altura2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        //xAptidao = xAptidao + " Apto para trabalho em altura.";
                        newRow["AP_Trabalho_Altura"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Trabalho_Altura_Apto"] = "X"; newRow["AP_Trabalho_Altura_Inapto"] = " "; }
                        else { newRow["AP_Trabalho_Altura_Apto"] = " "; newRow["AP_Trabalho_Altura_Inapto"] = " "; }
                    }

                    if (clinico.apt_Eletricidade2 != null && (clinico.apt_Eletricidade2.ToString().Trim() == "1" || clinico.apt_Eletricidade2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        //xAptidao = xAptidao + " Apto para serviço em eletricidade.";
                        newRow["AP_Eletricidade"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Eletricidade_Apto"] = "X"; newRow["AP_Eletricidade_Inapto"] = " "; }
                        else { newRow["AP_Eletricidade_Apto"] = " "; newRow["AP_Eletricidade_Inapto"] = " "; }
                    }


                    if (clinico.apt_Aquaviario2 != null && (clinico.apt_Aquaviario2.ToString().Trim() == "1" || clinico.apt_Aquaviario2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        //xAptidao = xAptidao + " Apto para serviços aquaviários.";
                        newRow["AP_Aquaviarias"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Aquaviarias_Apto"] = "X"; newRow["AP_Aquaviarias_Inapto"] = " "; }
                        else { newRow["AP_Aquaviarias_Apto"] = " "; newRow["AP_Aquaviarias_Inapto"] = " "; }
                    }

                    if (clinico.apt_Alimento2 != null && (clinico.apt_Alimento2.ToString().Trim() == "1" || clinico.apt_Alimento2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        //xAptidao = xAptidao + " Apto para manipular alimentos.";
                        newRow["AP_Alimentos"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Alimentos_Apto"] = "X"; newRow["AP_Alimentos_Inapto"] = " "; }
                        else { newRow["AP_Alimentos_Apto"] = " "; newRow["AP_Alimentos_Inapto"] = " "; }
                    }

                    if (clinico.apt_Brigadista2 != null && (clinico.apt_Brigadista2.ToString().Trim() == "1" || clinico.apt_Brigadista2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        //xAptidao = xAptidao + " Apto para Brigadista.";
                        newRow["AP_Brigadista"] = "X";
                        newRow["AP_Socorrista"] = " ";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Brigadista_Apto"] = "X"; newRow["AP_Brigadista_Inapto"] = " "; }
                        else { newRow["AP_Brigadista_Apto"] = " "; newRow["AP_Brigadista_Inapto"] = " "; }
                    }

                    if (clinico.apt_Socorrista2 != null && (clinico.apt_Socorrista2.ToString().Trim() == "1" || clinico.apt_Socorrista2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        //xAptidao = xAptidao + " Apto para Socorrista.";
                        newRow["AP_Socorrista"] = "X";
                        newRow["AP_Brigadista"] = " ";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Socorrista_Apto"] = "X"; newRow["AP_Socorrista_Inapto"] = " "; }
                        else { newRow["AP_Socorrista_Apto"] = " "; newRow["AP_Socorrista_Inapto"] = " "; }
                    }

                    if (clinico.apt_Respirador2 != null && (clinico.apt_Respirador2.ToString().Trim() == "1" || clinico.apt_Respirador2.ToString().Trim().ToUpper() == "TRUE"))
                    {
                        //xAptidao = xAptidao + " Apto para Uso de Respiradores.";
                        newRow["AP_Respiradores"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Respiradores_Apto"] = "X"; newRow["AP_Respiradores_Inapto"] = " "; }
                        else { newRow["AP_Respiradores_Apto"] = " "; newRow["AP_Respiradores_Inapto"] = " "; }
                    }



                    if (clinico.apt_Submerso2 != null && (clinico.apt_Submerso2.ToString().Trim() == "0" || clinico.apt_Submerso2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto para atividades submersas.";
                        newRow["AP_Submersas"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Aquaviarias_Inapto"] = "X"; newRow["AP_Aquaviarias_Apto"] = " "; }
                        else { newRow["AP_Aquaviarias_Inapto"] = " "; newRow["AP_Aquaviarias_Apto"] = " "; }
                    }


                    if (clinico.apt_Espaco_Confinado2 != null && (clinico.apt_Espaco_Confinado2.ToString().Trim() == "0" || clinico.apt_Espaco_Confinado2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto para trabalho em espaços confinados.";
                        newRow["AP_Espacos_Confinados"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Espacos_Confinados_Inapto"] = "X"; newRow["AP_Espacos_Confinados_Apto"] = " "; }
                        else { newRow["AP_Espacos_Confinados_Inapto"] = " "; newRow["AP_Espacos_Confinados_Apto"] = " "; }
                    }

                    if (clinico.apt_Transporte2 != null && (clinico.apt_Transporte2.ToString().Trim() == "0" || clinico.apt_Transporte2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto operar máquinas e equipamentos de transporte motorizado.";
                        newRow["AP_Motorizado"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Motorizado_Inapto"] = "X"; newRow["AP_Motorizado_Apto"] = " "; }
                        else { newRow["AP_Motorizado_Inapto"] = " "; newRow["AP_Motorizado_Apto"] = " "; }
                    }

                    if (clinico.apt_Trabalho_Altura2 != null && (clinico.apt_Trabalho_Altura2.ToString().Trim() == "0" || clinico.apt_Trabalho_Altura2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto para trabalho em altura.";
                        newRow["AP_Trabalho_Altura"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Trabalho_Altura_Inapto"] = "X"; newRow["AP_Trabalho_Altura_Apto"] = " "; }
                        else { newRow["AP_Trabalho_Altura_Inapto"] = " "; newRow["AP_Trabalho_Altura_Apto"] = " "; }
                    }

                    if (clinico.apt_Eletricidade2 != null && (clinico.apt_Eletricidade2.ToString().Trim() == "0" || clinico.apt_Eletricidade2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto para serviço em eletricidade.";
                        newRow["AP_Eletricidade"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) newRow["AP_Eletricidade_Inapto"] = "X";
                    }

                    if (clinico.apt_Aquaviario2 != null && (clinico.apt_Aquaviario2.ToString().Trim() == "0" || clinico.apt_Aquaviario2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto para serviços aquaviários.";
                        newRow["AP_Aquaviarias"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Aquaviarias_Inapto"] = "X"; newRow["AP_Aquaviarias_Apto"] = " "; }
                        else { newRow["AP_Aquaviarias_Inapto"] = " "; newRow["AP_Aquaviarias_Apto"] = " "; }
                    }

                    if (clinico.apt_Alimento2 != null && (clinico.apt_Alimento2.ToString().Trim() == "0" || clinico.apt_Alimento2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto para manipular alimentos.";
                        newRow["AP_Alimentos"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Alimentos_Inapto"] = "X"; newRow["AP_Alimentos_Apto"] = " "; }
                        else { newRow["AP_Alimentos_Inapto"] = " "; newRow["AP_Alimentos_Apto"] = " "; }
                    }

                    if (clinico.apt_Brigadista2 != null && (clinico.apt_Brigadista2.ToString().Trim() == "0" || clinico.apt_Brigadista2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto para Brigadista.";
                        newRow["AP_Brigadista"] = "X";
                        newRow["AP_Socorrista"] = " ";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Brigadista_Inapto"] = "X"; newRow["AP_Brigadista_Apto"] = " "; }
                        else { newRow["AP_Brigadista_Inapto"] = " "; newRow["AP_Brigadista_Apto"] = " "; }
                    }

                    if (clinico.apt_Socorrista2 != null && (clinico.apt_Socorrista2.ToString().Trim() == "0" || clinico.apt_Socorrista2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto para Socorrista.";
                        newRow["AP_Socorrista"] = "X";
                        newRow["AP_Brigadista"] = " ";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Socorrista_Inapto"] = "X"; newRow["AP_Socorrista_Apto"] = " "; }
                        else { newRow["AP_Socorrista_Inapto"] = " "; newRow["AP_Socorrista_Apto"] = " "; }
                    }

                    if (clinico.apt_Respirador2 != null && (clinico.apt_Respirador2.ToString().Trim() == "0" || clinico.apt_Respirador2.ToString().Trim().ToUpper() == "FALSE"))
                    {
                        //xAptidao = xAptidao + " Inapto para Uso de Respiradores.";
                        newRow["AP_Respiradores"] = "X";
                        if (clinico.IndResultado == (int)ResultadoExame.Normal || clinico.IndResultado == (int)ResultadoExame.Alterado) { newRow["AP_Respiradores_Inapto"] = "X"; newRow["AP_Respiradores_Inapto"] = " "; }
                        else { newRow["AP_Respiradores_Inapto"] = " "; newRow["AP_Respiradores_Inapto"] = " "; }
                    }


                    if (newRow["AP_Brigadista"].ToString().Trim() == "NA" || newRow["AP_Brigadista"].ToString().Trim() == "X")
                    {
                        newRow["AP_Socorrista"] = "";
                    }

                    if (newRow["AP_Socorrista"].ToString().Trim() == "NA" || newRow["AP_Socorrista"].ToString().Trim() == "X")
                    {
                        newRow["AP_Brigadista"] = "";
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






                if (convocacao.Observacao.Trim() != "")
                {
                    if (zObs != "")
                        newRow["ObservacaoResultado"] = zObs + "    " + convocacao.Observacao + System.Environment.NewLine + xAptidao;
                    else
                        newRow["ObservacaoResultado"] = convocacao.Observacao + System.Environment.NewLine + xAptidao;
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

                if (iExameBase.IdExameDicionario.Descricao.ToUpper().Trim()=="MUDANÇA DE FUNÇÃO" )
                   newRow["tDS_EXAME_TIPO"] = "Exame de Mudança de Riscos Ocupacionais";
                else
                   newRow["tDS_EXAME_TIPO"] = iExameBase.IdExameDicionario.Descricao;
            }

            #endregion

            #region AvaliacaoAmbiental

            public void AvaliacaoAmbiental(Clinico clinico,
                                                     Pcmso pcmso,
                                                     List<Ghe> ghes,
                                                     bool ExamesSomentePeriodoPcmso,
                                                     string xDataBranco)
            {
                string sNomeGhe = string.Empty;
                string sRiscosAmbientais = string.Empty;
                string sRiscosOcupacionais = string.Empty;
                string sExamesOcupacionais = string.Empty;

                Cliente xCliente = new Cliente();


                bool zDesconsiderar = false;

                bool xExibirDatas = true;

                Ghe ghe_Apt = new Ghe();

                Int16 zDias_Desconsiderar = 0;


                if (pcmso != null || pcmso.Id != 0)
                {
                    Ghe ghe;

                    if (ghes == null || ghes.Count == 0)
                        ghe = clinico.IdEmpregadoFuncao.GetGheEmpregado(pcmso.IdLaudoTecnico);
                    else
                    {
                        int IdGhe = clinico.IdEmpregadoFuncao.GetIdGheEmpregado(pcmso.IdLaudoTecnico);

                        ghe = ghes.Find(delegate (Ghe g) { return g.Id == IdGhe; });
                    }

                    if (ghe == null || ghe.Id == 0)
                        throw new Exception("O empregado " + clinico.IdEmpregado.tNO_EMPG
                            + " não está associado a nenhum GHE ou o PCMSO ainda não foi atualizado para o novo Laudo Técnico realizado!");



                    PcmsoGhe pcmsoGhe = new PcmsoGhe();
                    pcmsoGhe.Find("IdPcmso=" + pcmso.Id + " AND IdGHE=" + ghe.Id);



                    ghe_Apt.Find(ghe.Id);

                    sNomeGhe = ghe.tNO_FUNC;
                    sRiscosAmbientais = ghe.RiscosAmbientaisAso();


                    xCliente.Find(pcmso.IdCliente.Id);

                    //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("PRAJNA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("QTECK") > 0  || Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0)
                    //    sRiscosOcupacionais = ghe.RiscosOcupacionaisAso(xCliente.Exibir_Riscos_ASO, false, xCliente.Riscos_PPRA);
                    //else
                        sRiscosOcupacionais = ghe.RiscosOcupacionaisAso(xCliente.Exibir_Riscos_ASO, xCliente.Bloquear_Dispensado, xCliente.Riscos_PPRA);





                    if (xCliente.InibirGHE == false)
                        newRow["tNO_GHE"] = sNomeGhe;
                    else
                        newRow["tNO_GHE"] = "";




                    //configuração de PCMSO para cada GHE, para desconsiderar dias do vencimento
                    if (clinico.IdExameDicionario.Id == 4 || clinico.IdExameDicionario.Id == 2)
                    {
                        if (pcmsoGhe.Desconsiderar_Dias_ASO > 0)
                        {
                            zDias_Desconsiderar = pcmsoGhe.Desconsiderar_Dias_ASO;
                            zDesconsiderar = true;
                            xDataBranco = "  /  /    ";
                        }
                    }



                    //configuração do PCMSO e GHE têm prioridade
                    if (zDias_Desconsiderar == 0)
                    {
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
                                    xDataBranco = "  /  /    ";
                                    //    else
                                    //        xDataBranco = xPlanej.DataVencimento.AddDays(xCliente.Dias_Desconsiderar * (-1)).ToString("dd/MM/yyyy", ptBr);
                                    //}
                                    zDesconsiderar = true;
                                    zDias_Desconsiderar = xCliente.Dias_Desconsiderar;
                                }
                            }
                        }
                        else
                        {
                            zDesconsiderar = false;
                        }

                    }




                    //as horas do clinico e complementar são carregadas, e na comparação para exibir datas
                    //essas horas podem gerar problemas.  Por isso jogo 23 horas na data do exame - Wagner - 20/07/2018
                    clinico.DataExame = clinico.DataExame.AddHours(23 - clinico.DataExame.Hour);

                    clinico.IdExameDicionario.Find();


                    if (  xCliente.Bloquear_Data_Demissionais==true &&  clinico.IdExameDicionario.Descricao.Trim() == "Demissional")
                    {
                        xDataBranco = "31/12/2050";
                        zDesconsiderar = false;
                    }
                    

                    //Global quer que desconsiderar seja apenas para periódicos - 13/10/2023                    
                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") > 0)
                    {
                        if (clinico.IdExameDicionario.Id == 1 || clinico.IdExameDicionario.Id == 2)
                        {
                            zDesconsiderar = false;
                        }                       

                    }


                    xExibirDatas = xCliente.Exibir_Datas_Exames_ASO;


                    //mudança de função e demissional da Global para alguns grupos não exibir data complementares
                    //if (clinico.IdExameDicionario.Id == 3 || (clinico.IdExameDicionario.Id == 2))
                    //eles também querem que Afonso França, Mudança de função e retorno ao trabalho com complementares sempre sem data
                    //if (clinico.IdExameDicionario.Id == 2 || clinico.IdExameDicionario.Id == 3 || clinico.IdExameDicionario.Id == 5 )
                    //{
                    //    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") > 0)
                    //    {
                    //        clinico.IdEmpregado.Find();
                    //        clinico.IdEmpregado.nID_EMPR.Find();
                    //        clinico.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();
                    //        Int32 zIdGrupo = clinico.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Id;

                    //        if (zIdGrupo == -359564237 || zIdGrupo == 518994214 || zIdGrupo == 1635302153 || zIdGrupo == -987166118 || zIdGrupo == -1551657879)
                    //        {

                    //            xExibirDatas = false;

                    //        }
                    //    }
                    //}


                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Daiti") > 0)
                    {

                        //if ( clinico.IdExameDicionario.Descricao.Trim() == "Demissional" )
                        //    sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Asterisco(ghe, ExamesSomentePeriodoPcmso, false, xDataBranco);
                        //else
                        if (zDesconsiderar == false)
                            sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Asterisco(ghe, ExamesSomentePeriodoPcmso, xExibirDatas, xDataBranco, zDesconsiderar);
                        else
                            sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Asterisco_Desconsiderar(ghe, ExamesSomentePeriodoPcmso, xExibirDatas, clinico, zDias_Desconsiderar);

                    }
                    else if (clinico.IdExameDicionario.Id==3)
                    {

                        if (xCliente.GHEAnterior_MudancaFuncao == true)
                        {
                            // procurar ghe_ant primeiro, na mesma classif.funcional
                            // se nao encontrar, procurar classif.funcional anterior e ghe                       

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

                                ////quando tiver mais de dois GHEs associados em seu histórico
                                //if (znAux > xdS.Tables[0].Rows.Count - 2)
                                //{
                                if (znAux == 1) zGHE_Atual = System.Convert.ToInt32(row["nId_Func"].ToString());
                                else if (znAux == 2) zGHE_Ant = System.Convert.ToInt32(row["nId_Func"].ToString());
                                else break;

                                //if (zGHE_Atual == 0) zGHE_Atual = System.Convert.ToInt32(row["nId_Func"].ToString());
                                //else                 zGHE_Ant = System.Convert.ToInt32(row["nId_Func"].ToString());

                                //}

                            }

                            //ghe_ant
                            //ghe
                            Ghe zGhe1 = new Ghe();
                            zGhe1.Find(zGHE_Atual);
                            Ghe zGhe2 = new Ghe();
                            zGhe2.Find(zGHE_Ant);

                            if (zDesconsiderar == false)
                                sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao(zGhe1, zGhe2, ExamesSomentePeriodoPcmso, xExibirDatas, xDataBranco, zDesconsiderar);
                            else
                                sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao_Desconsiderar(zGhe1, zGhe2, ExamesSomentePeriodoPcmso, xExibirDatas, clinico, zDias_Desconsiderar);

                        }
                        else
                        {
                            if (zDesconsiderar == false)
                                sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado(ghe, ExamesSomentePeriodoPcmso, xExibirDatas, xDataBranco, zDesconsiderar);
                            else
                                sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Desconsiderar(ghe, ExamesSomentePeriodoPcmso, xExibirDatas, clinico, zDias_Desconsiderar);

                        }


                    }
                    else
                    {
                        if (clinico.IdExameDicionario.Id == 2)
                        {
                            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") > 0)  //demissionais globais têm regras próprias
                            {
                                if (zDesconsiderar == false)
                                    sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Global(ghe, ExamesSomentePeriodoPcmso, xExibirDatas, xDataBranco, zDesconsiderar);
                                else
                                    sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Desconsiderar_Global(ghe, ExamesSomentePeriodoPcmso, xExibirDatas, clinico, zDias_Desconsiderar);
                            }
                            else
                            {
                                if (zDesconsiderar == false)
                                    sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado(ghe, ExamesSomentePeriodoPcmso, xExibirDatas, xDataBranco, zDesconsiderar);
                                else
                                    sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Desconsiderar(ghe, ExamesSomentePeriodoPcmso, xExibirDatas, clinico, zDias_Desconsiderar);
                            }
                        }
                        else
                        {
                            if (zDesconsiderar == false)
                                sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado(ghe, ExamesSomentePeriodoPcmso, xExibirDatas, xDataBranco, zDesconsiderar);
                            else
                                sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Desconsiderar(ghe, ExamesSomentePeriodoPcmso, xExibirDatas, clinico, zDias_Desconsiderar);
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
                         xAptidao.apt_Submerso == true || xAptidao.apt_Trabalho_Altura == true || xAptidao.apt_Transporte == true || xAptidao.apt_Brigadista == true || xAptidao.apt_Socorrista == true || xAptidao.apt_PPR == true || xAptidao.apt_Radiacao == true || xAptidao.apt_Trabalho_Bordo==true) ||
                         (zAptidao.apt_Alimento == true || zAptidao.apt_Aquaviario == true || zAptidao.apt_Eletricidade == true || zAptidao.apt_Espaco_Confinado == true ||
                        zAptidao.apt_Submerso == true || zAptidao.apt_Trabalho_Altura == true || zAptidao.apt_Transporte == true || zAptidao.apt_Brigadista == true || zAptidao.apt_Socorrista == true || zAptidao.apt_PPR == true || zAptidao.apt_Radiacao == true || zAptidao.apt_Trabalho_Bordo==true ))
                    {

                        Empregado_Aptidao nAptidao = new Empregado_Aptidao();
                        nAptidao.nId_Empregado = clinico.IdEmpregado.Id;

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
                            nAptidao.apt_Trabalho_Bordo = xAptidao.apt_Trabalho_Bordo || zAptidao.apt_Trabalho_Bordo;
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
                            nAptidao.apt_Radiacao = xAptidao.apt_Radiacao;
                            nAptidao.apt_Trabalho_Bordo = xAptidao.apt_Trabalho_Bordo;
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
                            nAptidao.apt_Trabalho_Bordo = zAptidao.apt_Trabalho_Bordo;
                        }




                        xCliente.Find(pcmso.IdCliente.Id);

                        if (zDesconsiderar == false)
                            sExamesOcupacionais_Aptidao = clinico.GetPlanejamentoExamesAso_Formatado_Aptidao(nAptidao, xExibirDatas, xDataBranco, sExamesOcupacionais, zDesconsiderar, clinico);
                        else
                            sExamesOcupacionais_Aptidao = clinico.GetPlanejamentoExamesAso_Formatado_Aptidao_Desconsiderar(nAptidao, xExibirDatas, sExamesOcupacionais, clinico, zDias_Desconsiderar);

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


                sExamesOcupacionais = sExamesOcupacionais.Replace("\r", "");

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
                                        sBusca = (bCont - 1).ToString().Trim() + ".";
                                        sBuscaProx = " " + (bCont).ToString().Trim() + ".";
                                        xPosit = sExamesOcupacionais.IndexOf(sBuscaProx);

                                        if (xPosit < 0)
                                        {
                                            sBuscaProx = "\n" + (bCont).ToString().Trim() + ".";
                                            xPosit = sExamesOcupacionais.IndexOf(sBuscaProx);
                                        }


                                        if (xPosit >= 0)
                                        {
                                            if (sBusca == "9.")
                                            {
                                                sBusca = "9. ";
                                            }

                                            //sExamesOcupacionais = sExamesOcupacionais.Replace(sBuscaProx, sBusca);
                                            sExamesOcupacionais = sExamesOcupacionais.Substring(0, xPosit) + sBusca + sExamesOcupacionais.Substring(xPosit + sBuscaProx.Length);
                                        }
                                        else
                                        {
                                            // se for primeiro caracter da string
                                            sBuscaProx = (bCont).ToString().Trim() + ".";
                                            xPosit = sExamesOcupacionais.IndexOf(sBuscaProx);

                                            if (xPosit == 0)
                                            {
                                                if (sBusca == "9.")
                                                {
                                                    sBusca = "9. ";
                                                }

                                                //sExamesOcupacionais = sExamesOcupacionais.Replace(sBuscaProx, sBusca);
                                                sExamesOcupacionais = sExamesOcupacionais.Substring(0, xPosit) + sBusca + sExamesOcupacionais.Substring(xPosit + sBuscaProx.Length);

                                            }

                                        }

                                    }

                                    break;
                                }

                            }

                        }


                    }

                }
                else
                {
                    if (sExamesOcupacionais.IndexOf("Avaliação Clínica") > 0)
                    {
                        int xPosit = sExamesOcupacionais.IndexOf("Avaliação Clínica");

                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                        if (clinico.IndResultado == 1 || clinico.IndResultado == 2)
                        {
                            sExamesOcupacionais = sExamesOcupacionais.Substring(0, xPosit + 38) + clinico.DataExame.ToString("dd/MM/yyyy", ptBr) + sExamesOcupacionais.Substring(xPosit + 49);
                        }
                        else
                        {
                            sExamesOcupacionais = sExamesOcupacionais.Substring(0, xPosit + 38) + "          " + sExamesOcupacionais.Substring(xPosit + 49);
                        }

                        
                        //xExame = xExame.Substring(0, 40) + "/" + xExame.Substring(41, 2) + "/    "
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





                if ( sExamesOcupacionais.Length > 640)
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
                    sRiscosOcupacionais = "Risco ocupacional inexistente"; // "Sem risco ocupacional específico";



                //////para driveway e UNION
                clinico.IdEmpregadoFuncao.Find();
                clinico.IdEmpregadoFuncao.nID_EMPR.Find();
                clinico.IdEmpregadoFuncao.nID_EMPR.IdGrupoEmpresa.Find();

                if (clinico.IdEmpregadoFuncao.nID_EMPR.NomeAbreviado.ToUpper().IndexOf("DRIVEWAY") >= 0 || clinico.IdEmpregadoFuncao.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim()== "UNION")
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




                //quebrar sExamesOcupacionais em duas várias
                //uma com o nome do exame e outro com a data
                //criar campo DataComplementares dentro do ASO

                sBusca = "";
                sBuscaProx = "";
                xLinha = "";

                sExames = "";
                sDatas = "";

                sExames2 = "";
                sDatas2 = "";


                for ( int rCont=1; rCont<40;rCont++)
                {
                    sBusca = rCont.ToString().Trim() + ".";
                    sBuscaProx = (rCont+1).ToString().Trim() + ".";

                    int xPosit = sExamesOcupacionais.IndexOf(sBusca);
                    int xPosit2 = sExamesOcupacionais.IndexOf(sBuscaProx);

                    xLinha = "";

                    if ( xPosit >= 0)
                    {
                        if ( xPosit2 > 0 )
                        {
                            xLinha = sExamesOcupacionais.Substring(xPosit, xPosit2 - xPosit);
                        }
                        else
                        {
                            xLinha = sExamesOcupacionais.Substring(xPosit);
                        }

                        xLinha = xLinha.Replace("\n", "");
                        xLinha = xLinha.Replace( System.Environment.NewLine , "");

                        if (rCont < 15)
                        {
                            sExames = sExames + xLinha.Substring(0, 40) + System.Environment.NewLine;
                            sDatas = sDatas + xLinha.Substring(41) + System.Environment.NewLine;
                        }
                        else
                        {
                            sExames2 = sExames2 + xLinha.Substring(0, 40) + System.Environment.NewLine;
                            sDatas2 = sDatas2 + xLinha.Substring(41) + System.Environment.NewLine;
                        }
                    }

                }

                sDatas = sDatas.Replace("  /  /  ", "        ");
                sDatas2 = sDatas2.Replace("  /  /  ", "        ");

                if (clinico.IdEmpregadoFuncao.nID_EMPR.NomeAbreviado.ToUpper().IndexOf("BSV ") < 0)
                {
                    if (sExames.Trim() == "") sExames = "Exame Complementar não indicado";
                }
                else
                {
                    if (sExames.Trim() == "") sExames = "Exame Clínico";
                }


                //verificar opção para retirar "**Dispensado de exames complementares"                
                if (xCliente.Bloquear_Dispensado == true)
                {
                    sRiscosOcupacionais = sRiscosOcupacionais.Replace("**Dispensado de exames complementares", " ").Replace("*Dispensado de exames complementares", " ").Replace("Dispensado de exames complementares", " ");
                }
                


                newRow["RiscosAmbientais"] = sRiscosAmbientais;
                newRow["RiscosOcupacionais"] = sRiscosOcupacionais.Replace("\n", "   ").Replace("\r", "");

                newRow["ExamesComplementares"] = sExames; //sExamesOcupacionais;
                newRow["DataComplementares"] = sDatas;  // sExamesOcupacionais;

                newRow["ExamesComplementares2"] = sExames2; //sExamesOcupacionais;
                newRow["DataComplementares2"] = sDatas2;  // sExamesOcupacionais;

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
                    else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                    {
                        if (clinico.IdEmpregado.nNO_FOTO < 1)//.FotoEmpregado(cliente).ToLower().Trim() == "")
                        {
                            newRow["iFOTO"] = "I:\\fotosDocsDigitais\\_Global\\Logo_Global.jpg";
                        }
                        else
                            newRow["iFOTO"] = clinico.IdEmpregado.FotoEmpregado(cliente).ToLower();
                    }
                    else
                        newRow["iFOTO"] = clinico.IdEmpregado.FotoEmpregado(cliente).ToLower();
                }
                else
                {
                    if ( Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("EY") > 0)
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
                string strDataNascimento = (clinico.IdEmpregado.hDT_NASC != new DateTime()) ? clinico.IdEmpregado.hDT_NASC.ToString("dd-MM-yyyy") : string.Empty;  // + "        Idade " + clinico.IdEmpregado.IdadeEmpregado().ToString() + " anos" : string.Empty;

                //string strIdentidade;

                //if (clinico.IdEmpregado.tNO_IDENTIDADE == string.Empty)
                //    strIdentidade = "RG                            Nascido em " + strDataNascimento;
                //else
                //    strIdentidade = "RG " + clinico.IdEmpregado.tNO_IDENTIDADE + "     Nascido em " + strDataNascimento;

                //newRow["tNO_IDENTIDADE"] = strIdentidade;
                newRow["tNO_IDENTIDADE"] = clinico.IdEmpregado.tNO_CPF + "   Data de Nascimento " + strDataNascimento; ;

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

                    //if (Ilitera.Data.SQLServer.EntitySQLServer.xLocalServer.ToUpper().IndexOf("EY") > 0)
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

            public void Resultado(
                Clinico clinico)
            {
                if (clinico.IndResultado == (int)ResultadoExame.Normal)
                    newRow["tX_APTO"] = "X";
                else if (clinico.IndResultado == (int)ResultadoExame.Alterado)
                    newRow["tX_INAPTO"] = "X";
                //else if (clinico.IndResultado == (int)ResultadoExame.EmEspera)
                //    newRow["tX_Espera"] = "X";

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

                newRow["tNOME_MEDICO"] = medico.NomeCompleto;
                newRow["tTITULO_MEDICO"] = medico.Titulo + " " + medico.Numero;
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

                if (clinico.IdEmpregado.nID_EMPR.IdJuridicaPapel.Id == (int)IndJuridicaPapel.TerceiroAutonomo)
                {                   
                    Juridica(clinico.IdEmpregado.nID_EMPR.IdJuridicaPai, string.Empty, newRow);
                }
                else
                    if (clinico.IdEmpregado.nID_EMPR.Id != clinico.IdEmpregadoFuncao.nID_EMPR.Id)   //colaboradores em projetos
                    {
                        Juridica(clinico.IdEmpregadoFuncao.nID_EMPR, string.Empty, newRow);
                    }
                    else
                        Juridica(clinico.IdEmpregado.nID_EMPR, string.Empty, newRow);
            }

            public void Juridica(Juridica juridica, string tomadora, DataRow newRow)
            {
                if (juridica.mirrorOld == null)
                    juridica.Find();

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("GLOBAL") > 0)
                {
                    if (juridica.Auxiliar == "RAMO" || juridica.Auxiliar == "GLOBAL2")
                    {
                        newRow["Sistema"] = "GLOBAL2";
                    }
                    else
                    {
                        newRow["Sistema"] = "GLOBAL";
                    }
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("PRAJNA") > 0)
                {
                    newRow["Sistema"] = "ESSENCE";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.Server.ToUpper().IndexOf("140") > 0)
                {
                    newRow["Sistema"] = "SCJ";
                }
                else if (juridica.NomeAbreviado.ToUpper().IndexOf("CALOI") >= 0)
                {
                    newRow["Sistema"] = "CALOI";
                }
                else
                {
                    newRow["Sistema"] = "OUTROS";
                }


                //Prajna - TESTAR

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                {

                    newRow["RazaoSocial"] = juridica.NomeCompleto.ToString();
                    newRow["Endereco"] = juridica.GetEndereco().GetEndereco();
                    newRow["Cidade"] = juridica.GetEndereco().GetCidade();
                    newRow["Estado"] = juridica.GetEndereco().GetEstado();
                    newRow["CEP"] = juridica.GetEndereco().Cep;

                    juridica.IdJuridicaPai.Find();

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

                    //newRow["ClinicaCidade"] = juridica.GetEndereco().GetCidade();
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)  //deixar igual a web para Prajna
                {

                    juridica.IdJuridicaPai.Find();

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
                        newRow["RazaoSocial"] = juridica.NomeCompleto + "  -  " + juridica.NomeAbreviado;
                        

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
                else
                //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                {
                    juridica.IdJuridicaPai.Find();

                    if (juridica.IdJuridicaPai.Id != 0 && juridica.IdJuridicaPai.Id != juridica.Id)
                    {
                        //newRow["RazaoSocial"] = juridica.IdJuridicaPai.ToString() + "  Unidade: " + juridica.NomeAbreviado;
                        newRow["RazaoSocial"] = juridica.IdJuridicaPai.NomeCompleto.Trim();


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


                        if ( zCliente.Id != 0 && zCliente.CNPJ_Matriz == false)
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
                        newRow["RazaoSocial"] = juridica.NomeCompleto; //+ "  -  " + juridica.NomeAbreviado;

                        newRow["Endereco"] = juridica.GetEndereco().GetEndereco();
                        newRow["Cidade"] = juridica.GetEndereco().GetCidade();
                        newRow["Estado"] = juridica.GetEndereco().GetEstado();
                        newRow["CEP"] = juridica.GetEndereco().Cep;                       

                        if (tomadora != string.Empty)
                            newRow["CGC"] = juridica.NomeCodigo + "       " + tomadora;
                        else
                            newRow["CGC"] = juridica.NomeCodigo;


                        //if (Ilitera.Data.SQLServer.EntitySQLServer.xLocalServer.ToUpper().IndexOf("EY") > 0)
                        //{
                            //newRow["ClinicaCidade"] = juridica.GetEndereco().GetEndereco() + System.Environment.NewLine + "CEP:" + juridica.GetEndereco().Cep + "   " + juridica.GetEndereco().GetCidade() + " / " + juridica.GetEndereco().GetEstado() + "  CNPJ: " + juridica.NomeCodigo;
                        //}
                        //else
                        //{
                        //    newRow["ClinicaCidade"] = "";
                       // }
                    }
                    

                    Cliente zCliente2 = new Cliente();
                    zCliente2.Find(" Cliente.IdJuridica = " + juridica.Id);


                    if (zCliente2.Contrato_Numero.Trim() != "")
                    {

                        juridica.IdJuridicaPai.Find();

                        if (juridica.IdJuridicaPai.Id != 0 && juridica.IdJuridicaPai.Id != juridica.Id)
                        {
                            newRow["Contrato_Empresa"] = juridica.NomeCompleto.Trim();
                            newRow["Contrato_CNPJ"] = "CNPJ " + juridica.NomeCodigo.Trim();
                        }
                        else
                        {
                            newRow["Contrato_Empresa"] = newRow["RazaoSocial"].ToString();
                            newRow["Contrato_CNPJ"] = "CNPJ " + newRow["CGC"].ToString();
                        }

                           

                        //newRow["Contrato_Endereco"] = newRow["Endereco"].ToString();
                        //newRow["Contrato_Cidade_UF"] = newRow["Cidade"].ToString() + " / " + newRow["Estado"].ToString(); ;
                        //newRow["Contrato_CEP"] = newRow["CEP"].ToString();


                        newRow["Contrato_Endereco"] = juridica.GetEndereco().GetEndereco();
                        newRow["Contrato_Cidade_UF"] = juridica.GetEndereco().GetCidade() + " / " + juridica.GetEndereco().GetEstado();
                        newRow["Contrato_CEP"] = juridica.GetEndereco().Cep;
                    }
                    else
                    {
                        newRow["Contrato_Empresa"] = "";
                        newRow["Contrato_CNPJ"] = "";

                        newRow["Contrato_Endereco"] = "";
                        newRow["Contrato_Cidade_UF"] = "";
                        newRow["Contrato_CEP"] = "";
                    }

                }

                //else
                //{
                //    newRow["RazaoSocial"] = System.Environment.NewLine + juridica.NomeCompleto;
                //    newRow["Endereco"] = juridica.GetEndereco().GetEndereco();
                //    newRow["Cidade"] = juridica.GetEndereco().GetCidade();
                //    newRow["Estado"] = juridica.GetEndereco().GetEstado();
                //    newRow["CEP"] = juridica.GetEndereco().Cep;

                //    if (tomadora != string.Empty)
                //        newRow["CGC"] = juridica.NomeCodigo + "       " + tomadora;
                //    else
                //        newRow["CGC"] = juridica.NomeCodigo;

                //    newRow["ClinicaCidade"] = juridica.GetEndereco().GetCidade();
                //}


                newRow["ClinicaCidade"] = juridica.GetEndereco().GetEndereco() + System.Environment.NewLine + "CEP:" + juridica.GetEndereco().Cep + "   " + juridica.GetEndereco().GetCidade() + " / " + juridica.GetEndereco().GetEstado();

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


                string sNomeGhe = string.Empty;
                Ghe ghe;
                ghe = clinico.IdEmpregadoFuncao.GetGheEmpregado(pcmso.IdLaudoTecnico);
                sNomeGhe = ghe.tNO_FUNC;

                newRow["tNO_EMPG"] = clinico.IdEmpregado.tNO_EMPG; //.GetNomeEmpregadoComRE();
                //newRow["tNO_EMPG2"] = clinico.IdEmpregado.tNO_EMPG;

                if (cliente.InibirGHE == false)
                    newRow["tNO_GHE"] = sNomeGhe;
                else
                    newRow["tNO_GHE"] = "";


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



                Cliente xCliente = new Cliente();
                xCliente.Find(clinico.IdEmpregado.nID_EMPR.Id);

                if (xCliente.Municipio_Data_ASO == "N")
                {
                    newRow["dDT_EXAME"] = clinico.DataExame.ToString("dd-MM-yyyy") + ".";
                }
                else if (xCliente.Municipio_Data_ASO == "C")
                {
                    newRow["dDT_EXAME"] = clinico.IdJuridica.GetMunicipio() + " , " + clinico.DataExame.ToString("dd-MM-yyyy") + ".";
                }
                else if (xCliente.Municipio_Data_ASO == "E")
                {
                    newRow["dDT_EXAME"] = xCliente.GetMunicipio() + " , " + clinico.DataExame.ToString("dd-MM-yyyy") + ".";
                }
                else
                {
                    newRow["dDT_EXAME"] = clinico.DataExame.ToString("dd-MM-yyyy") + ".";
                }


                //Novos campos
                if (clinico.CodBusca != null && clinico.CodBusca != 0)
                {
                    newRow["Codigo_Barras"] = "*" + clinico.CodBusca.ToString().Trim() + "*";
                    newRow["Codigo_Barras_Numero"] = clinico.CodBusca.ToString().Trim();
                }
                else
                {
                    newRow["Codigo_Barras"] = "";
                    newRow["Codigo_Barras_Numero"] = "";
                }

                //FATURAR
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("METODO") > 0) //Daiti
                {
                    newRow["Faturar"] = "Ilitera Método" + System.Environment.NewLine + "33.233.895/0001-63 - (11)98701-1223";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_PRO") > 0)
                {
                    newRow["Faturar"] = "Ilitera PRO Segurança Saúde e Qualidade de Vida Ltda" + System.Environment.NewLine + "40.017.926/0001-04";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                {
                    newRow["Faturar"] = "Essence: Prana Gestão Ocupacional Ltda" + System.Environment.NewLine + "17.898.088/0001-03 - (11) 2344-4585";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().Trim() == "OPSA" || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("VIA") > 0)
                {
                    newRow["Faturar"] = "Ilitera UNO - Segurança, Saúde e Qualidade de Vida Ltda" + System.Environment.NewLine + "CNPJ  36.534.756/0001-68  Telefone 11 4249-4949";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0) //Daiti
                {
                    newRow["Faturar"] = "Assessoria: Ilitera Daiiti Segurança, Saúde e Qualidade de Vida Ltda" + System.Environment.NewLine + "06.259.938/0001-07 - (11)2506-2054 / 2507-2054";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Mappas") > 0)
                {
                    newRow["Faturar"] = "Assessoria: Mappas-SST Segurança do Trabalho Ltda ME" + System.Environment.NewLine + "10.688.659/0001-36 - (12)3426-9186";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("QTECK") > 0)
                {
                    newRow["Faturar"] = "QTECK ILITERA" + System.Environment.NewLine + "22.164.410/0001-00 - (11)4941-2288";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
                {
                    Juridica xJur = new Juridica();
                    xJur.Find(xCliente.Id);

                    if (xJur.Id != 0)
                    {
                        if (xJur.Auxiliar == "RAMO" || xJur.Auxiliar == "GLOBAL2")
                        {
                            newRow["Faturar"] = "RAMO ASS.EMPRESARIAL - CNPJ 28.495.277/0001-51 - (11) 93266-8098  faturamento2@globalsegmed.com.br";
                        }
                        //else if (xJur.Auxiliar == "GLOBAL2")
                        //{
                        //    newRow["Faturar"] = "GLOBALSEGMED - CNPJ 43.640.229/0001-01 - (11) 96718-0199  faturamento2@globalsegmed.com.br";
                        //}
                        else
                        {
                            newRow["Faturar"] = "GLOBALSEG - CNPJ 22.159.703/0001-08 - (11) 93266-8098  faturamento@globalsegmed.com.br";
                        }
                    }
                    else
                    {
                        newRow["Faturar"] = "GLOBALSEG - CNPJ 22.159.703/0001-08 - (11) 93266-8098  faturamento@globalsegmed.com.br";
                    }

                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("FOCUS") < 0 && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SAFETY") > 0)
                {
                    newRow["Faturar"] = "ILITERA GRAFENO EIRELI – CNPJ 27.013.373/0001-53  ou" + System.Environment.NewLine + "ILITERA GRAFENO LTDA – CNPJ 41.109.740/0001-48" + System.Environment.NewLine + "Obs: Faturar no CNPJ previsto em contrato.";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("FOCUS") > 0 && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SAFETY") > 0)
                {
                    newRow["Faturar"] = "ILITERA SAFETY" + System.Environment.NewLine + "CNPJ 36.196.609/0001-25"; //  - (11)98913-4933 / (11)94504-4728";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SOMA") > 0)
                {
                    newRow["Faturar"] = "Soma Ilitera (Soma Segurança Ocupacional Ltda)" + System.Environment.NewLine + "CNPJ: 04.170.948/0001-46 - (13)3229-1770";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SDTOURIN") > 0)
                {
                    newRow["Faturar"] = "ILITERA TURIN   CNPJ: 33.606.042/0001-20" + System.Environment.NewLine + "(11)2363-2245 / (11)94743-1615";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("FOX") > 0)
                {
                    newRow["Faturar"] = "ILITERA FOX" + System.Environment.NewLine + "CNPJ: 25.400.875/0001-01 - (55)3422-4891";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRIME") > 0)
                {
                    newRow["Faturar"] = "Ilitera Prime Assessoria em Segurança e Saúde Ltda - 33.876.636/0001-50  (11)4801-6300";
                }
                else
                {
                    //newRow["Frase"] = "Ilitera Via - Segurança, Saúde e Qualidade de Vida Ltda" + System.Environment.NewLine + "CNPJ  28.353.986/0001-00  Telefone 11 2356-7660";
                    newRow["Faturar"] = "Ilitera UNO - Segurança, Saúde e Qualidade de Vida Ltda" + System.Environment.NewLine + "CNPJ  36.534.756/0001-68  Telefone 11 4249-4949";
                }



                newRow["nSEXO"] = "Sexo " + clinico.IdEmpregado.tSEXO;
                newRow["tNO_STR_EMPR"] = clinico.IdEmpregadoFuncao.GetNomeSetor();  // + "     CPF " + clinico.IdEmpregado.tNO_CPF;

                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                newRow["Admissao"] = clinico.IdEmpregado.hDT_ADM.ToString("dd/MM/yyyy", ptBr);


                PopularRow pRow = new PopularRow(newRow);
                pRow.Juridica(clinico);
                pRow.DataNascimentoIdentidade(clinico);
                pRow.Foto(clinico, cliente, false);

                string sNomeCoordenador = GetCoordenadorPCMSO(cliente, pcmso);
                pRow.CoordenadorPcmso(sNomeCoordenador);

                newRow["tNO_FUNC_EMPR"] = clinico.IdEmpregadoFuncao.GetNomeFuncao();


                newRow["tDS_EXAME_TIPO"] = clinico.IdExameDicionario.Nome;

                


                //newRow["Cidade"] = clinico.IdJuridica.GetEndereco().GetCidade();
                newRow["Cidade"] = cliente.GetEndereco().GetCidade();

                //Para empresas que eram "PCMSO não contratada" e que agora são.
                if (clinico.IdMedico.Id != 0 && clinico.IdMedico.Id != (int)Medicos.PcmsoNaoContratada)
                {
                    newRow["tNOME_MEDICO"] = clinico.IdMedico.NomeCompleto;
                    newRow["tTITULO_MEDICO"] = clinico.IdMedico.Titulo + " " + clinico.IdMedico.Numero;
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

            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0 || Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("EY") > 0)
            {
               
             
                //usar este campo para IMC
                if (exameFisico.Peso > 0 && exameFisico.Altura > 0)
                {
                    float zIMC = 0;

                    if (exameFisico.Altura < 100)
                        zIMC = (exameFisico.Peso / (exameFisico.Altura * exameFisico.Altura));
                    else
                        zIMC = (exameFisico.Peso / ((exameFisico.Altura / 100) * (exameFisico.Altura / 100)));

                    newRow["IMC"] = zIMC.ToString("#.##");
                }
                else
                    newRow["IMC"] = "";

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


                //hasplano - Colesterol
                //hasDB  - hipertensao


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

            if (anamnese.HasHipertensao == (short)StatusAnamnese.Sim)
                newRow["HasDB"] = "SIM";
            else if (anamnese.HasHipertensao == (short)StatusAnamnese.Nao)
                newRow["HasDB"] = "NÃO";


            if (anamnese.HasColesterol == (short)StatusAnamnese.Sim)
                newRow["HasPlano"] = "SIM";
            else if (anamnese.HasColesterol == (short)StatusAnamnese.Nao)
                newRow["HasPlano"] = "NÃO";


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

                //Medico Medico;
                string xFone;
                //ContatoTelefonico contatoTelefonico;

                Clientes_Clinicas xClientes_Clinicas = new Clientes_Clinicas();

                //wagner - ajuste para obter fone prestador
                xFone = xClientes_Clinicas.Retornar_Fone_Prestador(pcmso.IdCoordenador.Id);


                //if (pcmso.IdCoordenador.Id == (int)Medicos.DraMarcela || pcmso.IdCoordenador.Id == (int)Medicos.DraRosana)
                //    sMedicoCoordenador = "Médica do Trabalho Coordenadora do PCMSO: " + pcmso.IdCoordenador.NomeCompleto + "    " + pcmso.IdCoordenador.Numero + "     Telefone " + xFone;
               // else
                    sMedicoCoordenador = "Médico Responsável pelo PCMSO: " + pcmso.IdCoordenador.NomeCompleto + "   " + pcmso.IdCoordenador.Numero + " " + pcmso.IdCoordenador.UF + "  Telefone " + xFone;
            }

            return sMedicoCoordenador;
        }
        #endregion



        private static DateTime TrazerDataUltimoExame(Int32 IdExameDicionario,Int32 IdEmpregado)
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
                dataUltima = new DateTime(2050,1,1);

            return dataUltima;
        }



    }
}

