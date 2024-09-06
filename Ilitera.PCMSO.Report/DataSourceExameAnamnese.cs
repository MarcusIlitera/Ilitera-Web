using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;

using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;



namespace Ilitera.PCMSO.Report
{
    public class DataSourceExameAnamnese : DataSourceBase
    {
        #region Eventos

        //public override event EventProgress ProgressIniciar;
        //public override event EventProgress ProgressAtualizar;
        //public override event EventProgressFinalizar ProgressFinalizar;

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

        private bool xSemResposta=false;

        #endregion

        #region Construtor


        public DataSourceExameAnamnese(Int32 zIdExameBase, bool zSemResposta)
        {
            InicializarTables();

            this.xSemResposta = zSemResposta;

            ExameBase rExame = new ExameBase();
            rExame.Find(zIdExameBase);

            rExame.IdEmpregado.Find();

            this.clinico = new Clinico();
            this.clinico.Find(rExame.Id);

            //this.clinico = new Clinico();
            //this.clinico.IdEmpregado = rExame.IdEmpregado;

            //this.clinico.IdEmpregado.Find();
            //this.clinico.IdEmpregado.nID_EMPR.Find();

            //this.clinico.IndResultado = rExame.IndResultado;

            //this.clinico.IdMedico = rExame.IdMedico;

            //this.clinico.Id = rExame.Id;

            //this.cliente = this.clinico.IdEmpregado.nID_EMPR;

            //this.clinico.DataExame = rExame.DataExame;

            //this.clinico.IdExameDicionario = rExame.IdExameDicionario;
            //this.clinico.IdExameDicionario.Find();

            //ArrayList list = new Pcmso().Find( "IdCliente=" + cliente.Id.ToString() + " order by DataPCMSO desc ");

            this.clinico.IdEmpregadoFuncao.Find();
            this.clinico.IdEmpregadoFuncao.nID_EMPR.Find();
            ArrayList list = new Pcmso().Find("IdCliente=" + this.clinico.IdEmpregadoFuncao.nID_EMPR.Id.ToString() + " order by DataPCMSO desc ");

            if (list.Count >= 1)
                this.pcmso = (Pcmso)list[0];




            //EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(this.clinico.IdEmpregado, rExame.IdEmpregado.nID_EMPR.Id);

            //this.clinico.IdEmpregadoFuncao = empregadoFuncao;

            



        }


        public DataSourceExameAnamnese(Clinico clinico)
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

        public DataSourceExameAnamnese(   ConvocacaoExame convocacao,   
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

   

        #region GetReportAso

        public RptAnamnese GetReport()
        {
            RptAnamnese report = new RptAnamnese();

          
                DataSourcePrincipal(clinico);
                DataSourceAso(clinico);

            

            report.Subreports[0].SetDataSource(dsExame_Vazio);

            //if (this.convocacao != null)
            //{                
            //    report.Subreports[1].SetDataSource(dsExame_Vazio);
            //    report.Subreports[2].SetDataSource(dsExame_Vazio);
            //    report.Subreports[3].SetDataSource(dsExame_Vazio);
            //}
            //else
            //{
            //    report.Subreports[1].SetDataSource(dsExame);
            //    report.Subreports[2].SetDataSource(dsExame);
            //    report.Subreports[3].SetDataSource(dsExame);
            //}

            report.SetDataSource(dsExame);
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }


        
        #endregion

     

      

      
        #region DataTables

        #region InicializarTables

        private void InicializarTables()
        {
            dsPrincipal.Tables.Add(GetDataTablePrincipal());
            dsExame.Tables.Add(GetDataTableReportAso());
            dsExame_Vazio.Tables.Add(GetDataTableReportQuestao());
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
            table.Columns.Add("Titulo", Type.GetType("System.String"));

            return table;
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



            table.Columns.Add("tMEDICO_COORD_PCMSO", Type.GetType("System.String"));
            table.Columns.Add("Observacao", Type.GetType("System.String"));

            //Exame
            table.Columns.Add("dDT_EXAME", Type.GetType("System.String"));
            table.Columns.Add("nRS_EXAME", Type.GetType("System.String"));
            table.Columns.Add("tNOME_MEDICO", Type.GetType("System.String"));
            table.Columns.Add("tTITULO_MEDICO", Type.GetType("System.String"));
            table.Columns.Add("Cidade", Type.GetType("System.String"));

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

      


     

        private void DataSourceAso(Clinico clinico)
        {

            clinico.IdEmpregadoFuncao.nID_EMPR.Find();
            //if (clinico.IdEmpregadoFuncao.nID_FUNCAO == null && this.clinico != null)
            //{
                clinico.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(this.clinico.IdEmpregado, clinico.IdEmpregadoFuncao.nID_EMPR.Id);
            //}

            //clinico.ValidarDadosAso();

            DataRow newRow = dsExame.Tables[0].NewRow();

            PopularRow pRow = new PopularRow(newRow);

            pRow.ExameEmpregado(clinico);
            pRow.AvaliacaoAmbiental(clinico, pcmso, null, ExamesSomentePeriodoPcmso, "");
            pRow.ExameTipo(clinico);

            clinico.IdEmpregadoFuncao.Find();
            clinico.IdEmpregadoFuncao.nID_EMPR.Find();
            pRow.Foto(clinico, clinico.IdEmpregadoFuncao.nID_EMPR, SemFoto);
            pRow.DataNascimentoIdentidade(clinico);
            pRow.Juridica(clinico);
            pRow.Medico(clinico);
            //pRow.ContatoMedico(clinico);
            pRow.Resultado(clinico);
            pRow.CoordenadorPcmso(GetCoordenadorPCMSO(cliente, pcmso));

            dsExame.Tables[0].Rows.Add(newRow);


            //dsExame_Vazio.Tables.Add(dsExame.Tables[0].DefaultView.ToTable());

            string zSistema = "";

            DataRow newRow2 = dsExame_Vazio.Tables[0].NewRow();

            Ilitera.Data.Clientes_Funcionarios zQuest = new Ilitera.Data.Clientes_Funcionarios();

            DataSet ds = zQuest.Trazer_Anamnese_Exame(clinico.Id);


            for ( int zCont=0; zCont<ds.Tables[0].Rows.Count;zCont++)
            {

                newRow2 = dsExame_Vazio.Tables[0].NewRow();
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
                    if (ds.Tables[0].Rows[zCont][6].ToString().Trim().Substring(0, 1) == "S")
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

                dsExame_Vazio.Tables[0].Rows.Add(newRow2);

            }

            ////carregar questoes
            //newRow2 = dsExame_Vazio.Tables[0].NewRow();
            //newRow2["IdExame"] = 1;
            //newRow2["Sistema"] = "Sistema01";
            //newRow2["Questao"] = "Teste 01";
            //newRow2["tX_APTO"] = "X";
            //newRow2["tX_INAPTO"] = "";
            //dsExame_Vazio.Tables[0].Rows.Add(newRow2);

            //newRow2 = dsExame_Vazio.Tables[0].NewRow();
            //newRow2["IdExame"] = 2;
            //newRow2["Sistema"] = "";
            //newRow2["Questao"] = "Teste 02";
            //newRow2["tX_APTO"] = "X";
            //newRow2["tX_INAPTO"] = "";
            //dsExame_Vazio.Tables[0].Rows.Add(newRow2);


            //newRow2 = dsExame_Vazio.Tables[0].NewRow();
            //newRow2["IdExame"] = 3;
            //newRow2["Sistema"] = "";
            //newRow2["Questao"] = "Teste 03";
            //newRow2["tX_APTO"] = " ";
            //newRow2["tX_INAPTO"] = "X";
            //dsExame_Vazio.Tables[0].Rows.Add(newRow2);



            //newRow2 = dsExame_Vazio.Tables[0].NewRow();
            //newRow2["IdExame"] = 1;
            //newRow2["Sistema"] = "Sistema Teste rrrr";
            //newRow2["Questao"] = "Teste 01";
            //newRow2["tX_APTO"] = "X";
            //newRow2["tX_INAPTO"] = "";
            //dsExame_Vazio.Tables[0].Rows.Add(newRow2);

            //newRow2 = dsExame_Vazio.Tables[0].NewRow();
            //newRow2["IdExame"] = 2;
            //newRow2["Sistema"] = "";
            //newRow2["Questao"] = "Teste 02";
            //newRow2["tX_APTO"] = "X";
            //newRow2["tX_INAPTO"] = "";
            //dsExame_Vazio.Tables[0].Rows.Add(newRow2);


            //newRow2 = dsExame_Vazio.Tables[0].NewRow();
            //newRow2["IdExame"] = 3;
            //newRow2["Sistema"] = "";
            //newRow2["Questao"] = "Teste 03";
            //newRow2["tX_APTO"] = " ";
            //newRow2["tX_INAPTO"] = "X";
            //dsExame_Vazio.Tables[0].Rows.Add(newRow2);


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
                //newRow["tNO_STR_EMPR"] = "Setor de " + clinico.IdEmpregadoFuncao.GetNomeSetor();
                newRow["tNO_STR_EMPR"] = "Setor de " + clinico.IdEmpregadoFuncao.GetNomeSetor() + "       CPF " + clinico.IdEmpregado.tNO_CPF;


                //newRow["tNO_FUNC_EMPR"] = "Função de " + clinico.IdEmpregadoFuncao.GetNomeFuncao();

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
                    newRow["dDT_EXAME"] = xCliente.GetMunicipio() + " , " +  clinico.DataExame.ToString("dd-MM-yyyy") + ".";
                }
                else
                {
                    newRow["dDT_EXAME"] = clinico.DataExame.ToString("dd-MM-yyyy") + ".";
                }


                newRow["nRS_Exame"] = clinico.IndResultado.ToString();

           



                //string zObs = "";

                //switch (clinico.IdEmpregado.nIND_BENEFICIARIO)
                //{
                //    case (int)TipoBeneficiario.BeneficiarioReabilitado:
                //        zObs = "( Beneficiário Reabilitado )     ";
                //        break;
                //    case (int)TipoBeneficiario.PortadorDeficiencia:
                //        zObs = "( Portador de Deficiência habilitada )     ";
                //        break;
                //    case (int)TipoBeneficiario.NaoAplicavel:
                //        zObs = "";
                //        break;
                //    default:
                //        zObs = "";
                //        break;
                //}

                //newRow["ObservacaoResultado"] = zObs + clinico.ObservacaoResultado + System.Environment.NewLine + xAptidao; 

                
            }


            public void ExameEmpregado(Clinico clinico, ConvocacaoExame convocacao)
            {
                newRow["IdExame"] = clinico.Id;
                newRow["tNO_EMPG"] = clinico.IdEmpregado.GetNomeEmpregadoComRE();
                newRow["nSEXO"] = "Sexo: " + clinico.IdEmpregado.tSEXO;
                newRow["tNO_STR_EMPR"] = "Setor de " + clinico.IdEmpregadoFuncao.GetNomeSetor();


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





                newRow["ObservacaoResultado"] = convocacao.Observacao + System.Environment.NewLine + xAptidao;
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
                                                     bool ExamesSomentePeriodoPcmso,
                                                     string xDataBranco)
            {
                string sNomeGhe = string.Empty;
                string sRiscosAmbientais = string.Empty;
                string sRiscosOcupacionais = string.Empty;
                string sExamesOcupacionais = string.Empty;

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

//                    if (ghe == null || ghe.Id == 0)
//                        throw new Exception("O empregado " + clinico.IdEmpregado.tNO_EMPG
//                            + " não está associado a nenhum GHE ou o PCMSO ainda não foi atualizado para o novo Laudo Técnico realizado!");

                    sNomeGhe = ghe.tNO_FUNC;
                    //sRiscosAmbientais = ghe.RiscosAmbientaisAso();


                    // Cliente xCliente = new Cliente();
                    // xCliente.Find(pcmso.IdCliente.Id);                                       

                    // sRiscosOcupacionais = ghe.RiscosOcupacionaisAso( xCliente.Exibir_Riscos_ASO);



                    //    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Daiti") > 0)
                    //    {

                    //        //if ( clinico.IdExameDicionario.Descricao.Trim() == "Demissional" )
                    //        //    sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Asterisco(ghe, ExamesSomentePeriodoPcmso, false, xDataBranco);
                    //        //else
                    //           sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Asterisco(ghe, ExamesSomentePeriodoPcmso, xCliente.Exibir_Datas_Exames_ASO, xDataBranco);

                    //    }
                    //    else if (clinico.IdExameDicionario.Descricao.Trim() == "Mudança de Função")
                    //    {
                    //        // procurar ghe_ant primeiro, na mesma classif.funcional
                    //        // se nao encontrar, procurar classif.funcional anterior e ghe                       

                    //        Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

                    //        DataSet xdS = xGHE.Trazer_Laudos_GHEs_Colaborador(clinico.IdEmpregadoFuncao.nID_EMPREGADO.Id);

                    //        if (xdS.Tables[0].Rows.Count < 2)
                    //        {
                    //            throw new Exception("O empregado " + clinico.IdEmpregado.tNO_EMPG
                    //                + " está associado a apenas um GHE/Classif.Funcional, logo, não é possível gerar o ASO de Mudança de Função. ");                            
                    //        }


                    //        int znAux = 0;
                    //        Int32 zGHE_Atual = 0;
                    //        Int32 zGHE_Ant = 0;


                    //        foreach (DataRow row in xdS.Tables[0].Rows)
                    //        {
                    //            znAux++;

                    //            ////quando tiver mais de dois GHEs associados em seu histórico
                    //            //if (znAux > xdS.Tables[0].Rows.Count - 2)
                    //            //{
                    //                if (znAux == 1) zGHE_Atual = System.Convert.ToInt32(row["nId_Func"].ToString());
                    //                else if (znAux == 2) zGHE_Ant = System.Convert.ToInt32(row["nId_Func"].ToString());
                    //                else break;

                    //                //if (zGHE_Atual == 0) zGHE_Atual = System.Convert.ToInt32(row["nId_Func"].ToString());
                    //                //else                 zGHE_Ant = System.Convert.ToInt32(row["nId_Func"].ToString());

                    //            //}

                    //        }


                    //        //ghe_ant
                    //        //ghe
                    //        Ghe zGhe1 = new Ghe();
                    //        zGhe1.Find(zGHE_Atual);
                    //        Ghe zGhe2 = new Ghe();
                    //        zGhe2.Find( zGHE_Ant );


                    //        sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao(zGhe1, zGhe2, ExamesSomentePeriodoPcmso, xCliente.Exibir_Datas_Exames_ASO, xDataBranco);



                    //    }
                    //    else
                    //    {
                    //        if (clinico.IdExameDicionario.Descricao.Trim() == "Demissional")
                    //            sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado(ghe, ExamesSomentePeriodoPcmso, false, xDataBranco);
                    //        else
                    //           sExamesOcupacionais = clinico.GetPlanejamentoExamesAso_Formatado(ghe, ExamesSomentePeriodoPcmso, xCliente.Exibir_Datas_Exames_ASO, xDataBranco);

                    //    }
                    //}

                    //if (sRiscosAmbientais == string.Empty)
                      //  sRiscosAmbientais = "Sem risco ocupacional específico";

                    //if (sRiscosOcupacionais == string.Empty)
                      //  sRiscosOcupacionais = "Sem risco ocupacional específico";

                    newRow["tNO_GHE"] = "GHE : " + sNomeGhe;
                    //newRow["RiscosAmbientais"] = sRiscosAmbientais;
                    //newRow["RiscosOcupacionais"] = sRiscosOcupacionais;
                    //newRow["ExamesComplementares"] = sExamesOcupacionais;
                }
            }
            #endregion

            #region Foto

            public void Foto(Clinico clinico, Cliente cliente, bool SemFoto)
            {
                if (!SemFoto)
                    newRow["iFOTO"] = clinico.IdEmpregado.FotoEmpregado(cliente).ToLower();
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
                //if (iExameBase.IdJuridica.Id != 0)
                  //  newRow["tCONTATO_MEDICO"] = "Telefone " + iExameBase.IdJuridica.GetContatoTelefonico().GetDDDTelefone();

                //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Daiti") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0)
                //{
                    newRow["tCONTATO_MEDICO"] = " ";
                //}
                //else
                //{
                //    newRow["tCONTATO_MEDICO"] = "Contato com médico examinador :  11 4437 1643";
                //}
            }

            #endregion

            #region Resultado

            public void Resultado(Clinico clinico)
            {
                if (clinico.IndResultado == (int)ResultadoExame.Normal)
                    newRow["RiscosOcupacionais"] = "X";
                else if (clinico.IndResultado == (int)ResultadoExame.Alterado)
                    newRow["RiscosAmbientais"] = "X";
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

            private static void Juridica(Juridica juridica, string tomadora, DataRow newRow)
            {
                if (juridica.mirrorOld == null)
                    juridica.Find();


                //Prajna - TESTAR

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0)
                {
                    newRow["RazaoSocial"] = juridica.NomeCompleto.ToString();
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
                            if (tomadora != string.Empty)
                                newRow["CGC"] = juridica.NomeCodigo + "       " + tomadora;
                            else
                                newRow["CGC"] = juridica.NomeCodigo;

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
                        if (tomadora != string.Empty)
                            newRow["CGC"] = juridica.NomeCodigo + "       " + tomadora;
                        else
                            newRow["CGC"] = juridica.NomeCodigo;
                    }

                    newRow["ClinicaCidade"] = juridica.GetEndereco().GetCidade();
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)  //deixar igual a web para Prajna
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
                            if (tomadora != string.Empty)
                                newRow["CGC"] = juridica.NomeCodigo + "       " + tomadora;
                            else
                                newRow["CGC"] = juridica.NomeCodigo;

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
                //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
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


                        if ( zCliente.Id != 0 && zCliente.CNPJ_Matriz == false)
                        {
                            if (tomadora != string.Empty)
                                newRow["CGC"] = juridica.NomeCodigo + "       " + tomadora;
                            else
                                newRow["CGC"] = juridica.NomeCodigo;

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

                newRow["tNO_EMPG"] = clinico.IdEmpregado.GetNomeEmpregadoComRE();
                //newRow["tNO_EMPG2"] = clinico.IdEmpregado.tNO_EMPG;
                newRow["tNO_GHE"] = "GHE : " + sNomeGhe;

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

            if (exameFisico.hasCabecaAlterado == (short)StatusAnamnese.Sim)
                newRow["hasCabecaAlterado"] = "Sim";
            else if (exameFisico.hasCabecaAlterado == (short)StatusAnamnese.Nao)
                newRow["hasCabecaAlterado"] = "Não";
            else if (exameFisico.hasCabecaAlterado == (short)StatusAnamnese.NaoPreenchido)
                newRow["hasCabecaAlterado"] = "";


            if (exameFisico.hasCoracaoAlterado == (short)StatusAnamnese.Sim)
                newRow["hasCoracaoAlterado"] = "Sim";
            else if (exameFisico.hasCoracaoAlterado == (short)StatusAnamnese.Nao)
                newRow["hasCoracaoAlterado"] = "Não";
            else if (exameFisico.hasCoracaoAlterado == (short)StatusAnamnese.NaoPreenchido)
                newRow["hasCoracaoAlterado"] = "";


            if (exameFisico.hasPulmaoAlterado == (short)StatusAnamnese.Sim)
                newRow["hasPulmaoAlterado"] = "Sim";
            else if (exameFisico.hasPulmaoAlterado == (short)StatusAnamnese.Nao)
                newRow["hasPulmaoAlterado"] = "Não";
            else if (exameFisico.hasPulmaoAlterado == (short)StatusAnamnese.NaoPreenchido)
                newRow["hasPulmaoAlterado"] = "";

            if (exameFisico.hasMIAlterado == (short)StatusAnamnese.Sim)
                newRow["hasMIAlterado"] = "Sim";
            else if (exameFisico.hasMIAlterado == (short)StatusAnamnese.Nao)
                newRow["hasMIAlterado"] = "Não";
            else if (exameFisico.hasMIAlterado == (short)StatusAnamnese.NaoPreenchido)
                newRow["hasMIAlterado"] = "";


            if (exameFisico.hasPeleAnexosAlterado == (short)StatusAnamnese.Sim)
                newRow["hasPeleAlterado"] = "Sim";
            else if (exameFisico.hasPeleAnexosAlterado == (short)StatusAnamnese.Nao)
                newRow["hasPeleAlterado"] = "Não";
            else if (exameFisico.hasPeleAnexosAlterado == (short)StatusAnamnese.NaoPreenchido)
                newRow["hasPeleAlterado"] = "";


            if (exameFisico.hasMSAlterado == (short)StatusAnamnese.Sim)
                newRow["hasMSAlterado"] = "Sim";
            else if (exameFisico.hasMSAlterado == (short)StatusAnamnese.Nao)
                newRow["hasMSAlterado"] = "Não";
            else if (exameFisico.hasMSAlterado == (short)StatusAnamnese.NaoPreenchido)
                newRow["hasMSAlterado"] = "";


            if (exameFisico.hasAbdomemAlterado == (short)StatusAnamnese.Sim)
                newRow["hasAbdomemAlterado"] = "Sim";
            else if (exameFisico.hasAbdomemAlterado == (short)StatusAnamnese.Nao)
                newRow["hasAbdomemAlterado"] = "Não";
            else if (exameFisico.hasAbdomemAlterado == (short)StatusAnamnese.NaoPreenchido)
                newRow["hasAbdomemAlterado"] = "";


            if (exameFisico.hasOsteoAlterado == (short)StatusAnamnese.Sim)
                newRow["hasOsteoAlterado"] = "Sim";
            else if (exameFisico.hasOsteoAlterado == (short)StatusAnamnese.Nao)
                newRow["hasOsteoAlterado"] = "Não";
            else if (exameFisico.hasOsteoAlterado == (short)StatusAnamnese.NaoPreenchido)
                newRow["hasOsteoAlterado"] = "";



            newRow["tMEDICO_COORD_PCMSO"] = GetCoordenadorPCMSO(cliente, pcmso);
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
                sMedicoCoordenador = "Médico do Trabalho Coordenador do PCMSO: _________________________    CRM-SP nº ______________    Fone ________________";
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


                if (pcmso.IdCoordenador.Id == (int)Medicos.DraMarcela || pcmso.IdCoordenador.Id == (int)Medicos.DraRosana)
                    sMedicoCoordenador = "Médica do Trabalho Coordenadora do PCMSO: " + pcmso.IdCoordenador.NomeCompleto + "    " + pcmso.IdCoordenador.Numero + "     Telefone " + xFone;
                else
                    sMedicoCoordenador = "Médico do Trabalho Coordenador do PCMSO: " + pcmso.IdCoordenador.NomeCompleto + "    " + pcmso.IdCoordenador.Numero + "     Telefone " + xFone;
            }

            return sMedicoCoordenador;
        }
        #endregion
    }
}

