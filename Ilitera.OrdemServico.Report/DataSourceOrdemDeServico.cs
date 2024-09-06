using System;
using System.Data;
using System.Collections;
using System.Text;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections.Generic;

namespace Ilitera.OrdemServico.Report
{
	/// <summary>
	/// 
	/// </summary>
	public class DataSourceOrdemDeServico : DataSourceBase
	{
		private Cliente cliente;
		private Empregado empregado;
        private EmpregadoFuncao empregadoFuncao;
		private DateTime dataOS;
		private bool hasEspecifico;
		private ArrayList alProcedimentos;
		private ArrayList alProcedimentosGenericos;

        public DataSourceOrdemDeServico()
        {

        }

        public DataSourceOrdemDeServico(Empregado empregado): this(empregado, DateTime.Today)
		{

		}

		public DataSourceOrdemDeServico(Empregado empregado, DateTime dataOS)
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");

			this.empregado = empregado;
            this.empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
            this.empregadoFuncao.nID_EMPR.Find();
			this.dataOS = dataOS;
            cliente = this.empregadoFuncao.nID_EMPR;
		}

		public ReportOrdemDeServico2 GetReportOrdemDeServico(string xCabecalho)
		{
            ProcessaProcedimentos();

            ReportOrdemDeServico2 report = new ReportOrdemDeServico2();
			report.OpenSubreport("ProcedimentosEspecificos").SetDataSource(GetProcedimentoEmpregado());
			report.OpenSubreport("ProcedimentosGenericos").SetDataSource(GetProcedimentoGenericoEmpregado());
			report.OpenSubreport("ListaProcedimentos").SetDataSource(GetListaDeProcedimentos());
			report.OpenSubreport("ReportIntroducaoOS").SetDataSource(GetIntroducaoOS(xCabecalho));
			report.SetDataSource(GetProceCheck());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

        public ReportOrdemDeServico2_Vertical GetReportOrdemDeServico_Vertical( string xCabecalho)
        {
            ProcessaProcedimentos();

            ReportOrdemDeServico2_Vertical report = new ReportOrdemDeServico2_Vertical();
            report.OpenSubreport("ProcedimentosEspecificos").SetDataSource(GetProcedimentoEmpregado());
            report.OpenSubreport("ProcedimentosGenericos").SetDataSource(GetProcedimentoGenericoEmpregado());
            report.OpenSubreport("ListaProcedimentos").SetDataSource(GetListaDeProcedimentos());
            report.OpenSubreport("ReportIntroducaoOS").SetDataSource(GetIntroducaoOS(xCabecalho));
            report.SetDataSource(GetProceCheck());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }


		public ReportIntroducaoOS GetReportIntroducaoOS( string xCabecalho)
		{
			ReportIntroducaoOS report = new ReportIntroducaoOS();
			report.SetDataSource(GetIntroducaoOS(xCabecalho));
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		public ReportListaProcOS GetReportListaProcOS()
		{
            ProcessaProcedimentos();
            
            ReportListaProcOS report = new ReportListaProcOS();
			report.SetDataSource(GetListaDeProcedimentos());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		public ReportClass GetReportProcedimento(Procedimento procedimento)
		{
			if (procedimento.IndTipoProcedimento == TipoProcedimento.Instrutivo)
				return GetReportPOPGenerico(procedimento);
			else
				return GetReportPOP(procedimento);
		}

        public ReportClass GetReportProcedimento_Vertical(Procedimento procedimento)
        {
             return GetReportPOP_Vertical(procedimento);
        }

		public ReportPOPGenerico GetReportPOPGenerico(Procedimento procedimento)
		{
			ReportPOPGenerico report = new ReportPOPGenerico();
			DataSet ds = new DataSet();
			ds.Tables.Add(GetTableProcedimento());
			report.SetDataSource(GetProcedimentoGenerico(ds, procedimento));
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		public ReportPOP4 GetReportPOP(Procedimento procedimento)
		{
			ReportPOP4 report = new ReportPOP4();
			DataSet ds = new DataSet();
			ds.Tables.Add(GetTableProcedimento());
			report.SetDataSource(GetProcedimento(ds, procedimento, true));
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}


        public ReportPOP4_Vertical GetReportPOP_Vertical(Procedimento procedimento)
        {
            ReportPOP4_Vertical report = new ReportPOP4_Vertical();
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTableProcedimento());
            report.SetDataSource(GetProcedimento(ds, procedimento, true));
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

		public ReportPOP4 GetReportPOP()
		{
            ProcessaProcedimentos();

            ReportPOP4 report = new ReportPOP4();
			report.SetDataSource(GetProcedimentoEmpregado());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		public ReportPOPGenerico GetReportPOPGenerico()
		{
            ProcessaProcedimentos();

            ReportPOPGenerico report = new ReportPOPGenerico();
			report.SetDataSource(GetProcedimentoGenericoEmpregado());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
        }

        private void ProcessaProcedimentos()
        {
            alProcedimentos = new Procedimento().Find("(IdProcedimento IN (SELECT IdProcedimento FROM ConjuntoProcedimento WHERE IdConjunto"
                + " IN (SELECT IdConjunto FROM ConjuntoEmpregado WHERE IdEmpregado=" + empregado.Id
                + " AND (DataInicio IS NULL OR (DataTermino IS NULL AND DataInicio<='" + dataOS.ToString("yyyy-MM-dd") + "')"
                + " OR (DataInicio<='" + dataOS.ToString("yyyy-MM-dd") + "' AND DataTermino>='" + dataOS.ToString("yyyy-MM-dd") + "'))))"
                + " OR IdProcedimento IN (SELECT IdProcedimento FROM ProcedimentoEmpregado WHERE IdEmpregado=" + empregado.Id
                + " AND (DataInicio IS NULL OR (DataTermino IS NULL AND DataInicio<='" + dataOS.ToString("yyyy-MM-dd") + "')"
                + " OR (DataInicio<='" + dataOS.ToString("yyyy-MM-dd") + "' AND DataTermino>='" + dataOS.ToString("yyyy-MM-dd") + "'))))"
                + " AND IndTipoProcedimento<>" + (int)TipoProcedimento.Instrutivo
                + " ORDER BY Numero");

            alProcedimentosGenericos = new Procedimento().Find("(IdProcedimento IN (SELECT IdProcedimento FROM ConjuntoProcedimento WHERE IdConjunto"
                + " IN (SELECT IdConjunto FROM ConjuntoEmpregado WHERE IdEmpregado=" + empregado.Id
                + " AND (DataInicio IS NULL OR (DataTermino IS NULL AND DataInicio<='" + dataOS.ToString("yyyy-MM-dd") + "')"
                + " OR (DataInicio<='" + dataOS.ToString("yyyy-MM-dd") + "' AND DataTermino>='" + dataOS.ToString("yyyy-MM-dd") + "'))))"
                + " OR IdProcedimento IN (SELECT IdProcedimento FROM ProcedimentoEmpregado WHERE IdEmpregado=" + empregado.Id
                + " AND (DataInicio IS NULL OR (DataTermino IS NULL AND DataInicio<='" + dataOS.ToString("yyyy-MM-dd") + "')"
                + " OR (DataInicio<='" + dataOS.ToString("yyyy-MM-dd") + "' AND DataTermino>='" + dataOS.ToString("yyyy-MM-dd") + "'))))"
                + " AND IndTipoProcedimento=" + (int)TipoProcedimento.Instrutivo
                + " ORDER BY IndTipoProcedimento desc, Numero");

            if (alProcedimentos.Count > 0)
                hasEspecifico = true;
            else
                hasEspecifico = false;
        }

        #region GetProcedimento

        private DataSet GetProcedimento(DataSet ds, Procedimento procedimento, bool ComFoto)
		{
			Procedimento procedAtual = procedimento;

            string zOldPerigo = "";
            string zOldPrecaucoes = "";

	
			procedAtual.IdCliente.Find();

			if (procedimento.IndTipoProcedimento == TipoProcedimento.Especifico)
			{
				procedimento = procedimento.IdProcedimentoResumo;
				procedimento.Find();
			}

            
			string EPI = procedimento.strEpi();
			string Foto = string.Empty;
			string nomeEmpregado = string.Empty;

            if (ComFoto)
            {
                //Foto = procedAtual.FotoProcedimento();
                
                //Ilitera.Opsa.Data.Cliente xCliente = new Ilitera.Opsa.Data.Cliente();
                //xCliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));

                //string xArq = "C:\\DRIVE_I\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\OrdemServico\\" + procedimento.Foto_Procedimento;                
                string xArq = "I:\\fotosdocsdigitais\\" +  procedAtual.IdCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\OrdemServico\\" + procedAtual.Foto_Procedimento;


                Foto = Ilitera.Common.Fotos.PathFoto_Uri(xArq);
            }

            try
            {
                //wagner - localizar GHE
                Clinico exame = new Clinico();
                exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                exame.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();

                //exame.UsuarioId = System.Convert.ToInt32(Request["IdUsuario"].ToString());

                Pcmso pcmso = new Pcmso();
                pcmso = exame.IdPcmso;



                if (pcmso.IdLaudoTecnico != null)
                {
                    List<Ghe> ghes = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + pcmso.IdLaudoTecnico.Id);

                    Ghe ghe;

                    if (ghes == null || ghes.Count == 0)
                        ghe = exame.IdEmpregadoFuncao.GetGheEmpregado(pcmso.IdLaudoTecnico);
                    else
                    {
                        int IdGhe = exame.IdEmpregadoFuncao.GetIdGheEmpregado(pcmso.IdLaudoTecnico);

                        ghe = ghes.Find(delegate(Ghe g) { return g.Id == IdGhe; });
                    }

                    nomeEmpregado = empregado.tNO_EMPG + "   ( " + ghe.ToString() + " ) ";
                }
                else
                {
                    nomeEmpregado = empregado.tNO_EMPG ;
                }
             }
            catch
            {
                nomeEmpregado = "Nenhum Empregado vinculado";
            }

			ArrayList alOperacao = new Operacao().Find("IdProcedimento=" + procedimento.Id +" ORDER BY Sequencia");

			DataRow newRow;

            newRow = ds.Tables[0].NewRow();

            int zImpPrecaucao = 0;

			foreach(Operacao operacao in alOperacao)
			{

                //Cabeçalho
                procedAtual.IdCliente.IdJuridicaPai.Find();

                if (procedAtual.IdCliente.IdJuridicaPai.Id!=0)
                    newRow["RazaoSocial"] = procedAtual.IdCliente.IdJuridicaPai.NomeCompleto + " - " +  procedAtual.IdCliente.NomeCompleto;
                else
                    newRow["RazaoSocial"] = procedAtual.IdCliente.NomeCompleto;


				newRow["NomeEmpregado"]	= nomeEmpregado;
				newRow["nPOPS"] = procedAtual.Numero.ToString("0000");
				newRow["NomeProcedimento"] = procedAtual.Nome;
				newRow["EPI"] = EPI;
				newRow["EPC"] = procedimento.Epc;
				newRow["MedidasAdm"] = procedimento.MedidasAdm;
				newRow["MedidasEdu"] = procedimento.MedidasEdu;
				newRow["ComFoto"] = (Foto!=string.Empty);
				newRow["Foto"] = Foto;
				//Detalhe
				newRow["Sequencia"]	= operacao.Sequencia.ToString("00");
				newRow["Operacao"] = operacao.Descricao.ToString();

                zOldPerigo = "";
                zOldPrecaucoes = "";

                string xSeveridade = "";
                zImpPrecaucao = 0;
                

                ArrayList alOperacaoPerigo = new OperacaoPerigo().Find("IdOperacao=" + operacao.Id);
                string zPrecaucao = "";

                foreach (OperacaoPerigo operacaoPerigo in alOperacaoPerigo)
                {

                    zPrecaucao = operacaoPerigo.Precaucoes;

                    //OperacaoPerigoRisco
                    ArrayList alOperacaoPerigoRisco = new OperacaoPerigoRiscoAcidente().Find("IdOperacaoPerigo=" + operacaoPerigo.Id);
    
                    Perigo zPerigo = new Perigo();
                    zPerigo.Find(operacaoPerigo.IdPerigo.Id);



                    foreach (OperacaoPerigoRiscoAcidente operacaoPerigoRisco in alOperacaoPerigoRisco)
                    {

                        string rPerigo = "";
                        string rProbabilidade = "";
                        string rRisco = "";

                        operacaoPerigoRisco.IdRiscoAcidente.Find();
                        rPerigo = zPerigo.Nome.Trim();
                        rRisco = operacaoPerigoRisco.IdRiscoAcidente.Nome.Trim();

                        rProbabilidade = operacaoPerigoRisco.IndProbabilidadeRisco.ToString().Trim();


                        if (rProbabilidade.Trim() == "0")
                            rProbabilidade = "-";

                        //xProbabilidade = xProbabilidade + rProbabilidade + System.Environment.NewLine; ;
                        //xPerigo = xPerigo + rPerigo + System.Environment.NewLine;
                        //xRisco = xRisco + rRisco + System.Environment.NewLine;

                        if (operacaoPerigoRisco.IndGrauSeveridadeRisco.ToString().Trim() == "0")
                            xSeveridade = "-" ;
                        else
                            xSeveridade = operacaoPerigoRisco.IndGrauSeveridadeRisco.ToString();

                        if (xSeveridade.Trim().ToUpper() == "LEVEMENTEPREJUDICIAL")
                            xSeveridade = "Levemente Prejudicial";

                        newRow["Severidade"] = xSeveridade;
                        newRow["Probabilidade"] = rProbabilidade;


                        if (zOldPerigo != rPerigo)
                        {
                            newRow["Perigos"] = rPerigo;
                            zOldPerigo = rPerigo;
                        }
                        else
                        {
                            newRow["Perigos"] = "";
                        }



                        newRow["RiscosAcidente"] = rRisco;

                        //if (zImpPrecaucao == 0)
                        //{
                        //    if (operacao.Precaucoes.Trim() == "")
                        //        newRow["Precaucoes"] = "( NA )";
                        //    else
                        //        newRow["Precaucoes"] = operacao.Precaucoes;

                        //    zImpPrecaucao = 1;
                        //}
                        //else
                        //{
                        //    newRow["Precaucoes"] = "";
                        //}


                        if (zOldPrecaucoes != zPrecaucao)
                        {
                            newRow["Precaucoes"] = zPrecaucao;
                            zOldPrecaucoes = zPrecaucao;
                        }
                        else
                        {
                            newRow["Precaucoes"] = "";
                        }

                        ////Cabechalho
                        //newRow["RazaoSocial"] = procedAtual.IdCliente.NomeCompleto;
                        procedAtual.IdCliente.IdJuridicaPai.Find();

                        if (procedAtual.IdCliente.IdJuridicaPai.Id != 0)
                            newRow["RazaoSocial"] = procedAtual.IdCliente.IdJuridicaPai.NomeCompleto + " - " + procedAtual.IdCliente.NomeCompleto;
                        else
                            newRow["RazaoSocial"] = procedAtual.IdCliente.NomeCompleto;


                        newRow["NomeEmpregado"] = nomeEmpregado;
                        newRow["nPOPS"] = procedAtual.Numero.ToString("0000");
                        newRow["NomeProcedimento"] = procedAtual.Nome;
                        newRow["EPI"] = EPI;
                        newRow["EPC"] = procedimento.Epc;
                        newRow["MedidasAdm"] = procedimento.MedidasAdm;
                        newRow["MedidasEdu"] = procedimento.MedidasEdu;
                        newRow["ComFoto"] = (Foto != string.Empty);
                        newRow["Foto"] = Foto;
                        //Detalhe
                        newRow["Sequencia"] = operacao.Sequencia.ToString("00");

                        if (zImpPrecaucao == 0)
                        {
                            newRow["Operacao"] = operacao.Descricao.ToString();
                            zImpPrecaucao = 1;
                        }
                        else
                        {
                            newRow["Operacao"] = "";
                        }
                        

                        ds.Tables[0].Rows.Add(newRow);
                        newRow = ds.Tables[0].NewRow();
                    }

                }


                //ds.Tables[0].Rows.Add(newRow);


				
                ////newRow["RiscosAcidente"] = operacao.strAcidentes();

                ////string xSeveridade = "";
                ////string xProbabilidade = "";
                ////string xPerigo = "";

                ////ArrayList alOperacaoPerigo = new OperacaoPerigo().Find("IdOperacao=" + operacao.Id);

                ////foreach (OperacaoPerigo operacaoPerigo in alOperacaoPerigo)
                ////{

                


                ////    //OperacaoPerigoRisco
                ////    ArrayList alOperacaoPerigoRisco = new OperacaoPerigoRiscoAcidente().Find("IdOperacaoPerigo=" + operacaoPerigo.Id);

                ////    Perigo zPerigo = new Perigo();
                ////    zPerigo.Find(operacaoPerigo.IdPerigo.Id);

                    

                ////    foreach (OperacaoPerigoRiscoAcidente operacaoPerigoRisco in alOperacaoPerigoRisco)
                ////    {

                ////        string rPerigo = "";
                ////        string rProbabilidade = "";

                ////        operacaoPerigoRisco.IdRiscoAcidente.Find();
                ////        rPerigo = zPerigo.Nome.Trim() + " / " + operacaoPerigoRisco.IdRiscoAcidente.Nome.Trim();   


                ////        rProbabilidade = operacaoPerigoRisco.IndProbabilidadeRisco.ToString().Trim();


                ////        if (rProbabilidade.Trim() == "0")
                ////            rProbabilidade = "-";

                ////        xProbabilidade = xProbabilidade + rProbabilidade + System.Environment.NewLine; ;
                ////        xPerigo = xPerigo + rPerigo + System.Environment.NewLine; ;

                ////        if ( operacaoPerigoRisco.IndGrauSeveridadeRisco.ToString().Trim() == "0" )
                ////            xSeveridade = xSeveridade + "-" + System.Environment.NewLine;
                ////        else
                ////            xSeveridade = xSeveridade + operacaoPerigoRisco.IndGrauSeveridadeRisco.ToString() + System.Environment.NewLine;

                ////    }

                ////}


                ////newRow["Severidade"] = xSeveridade;
                ////newRow["Probabilidade"] = xProbabilidade;
                ////newRow["Perigos"] = xPerigo;


                ////if (operacao.Precaucoes.Trim() == "")
                ////    newRow["Precaucoes"] = "( NA )";
                ////else
                ////    newRow["Precaucoes"] = operacao.Precaucoes;

                //string xSeveridade = "";
                //string xProbabilidade = "";

                // ArrayList alOperacaoPerigo = new OperacaoPerigo().Find("IdOperacao=" + operacao.Id);

                // foreach (OperacaoPerigo operacaoPerigo in alOperacaoPerigo)
                // {

                //     //OperacaoPerigoRisco
                //     ArrayList alOperacaoPerigoRisco = new OperacaoPerigoRiscoAcidente().Find("IdOperacaoPerigo=" + operacaoPerigo.Id);

                //     Perigo zPerigo = new Perigo();
                //     zPerigo.Find(operacaoPerigo.IdPerigo.Id);

                //     foreach (OperacaoPerigoRiscoAcidente operacaoPerigoRisco in alOperacaoPerigoRisco)
                //     {

                //         string rPerigo = "";
                //         string rProbabilidade = "";

                //         if (zPerigo.Nome.Trim().Length < 38)
                //         {
                //             rPerigo = zPerigo.Nome.Trim();

                //             for (int mCont = zPerigo.Nome.Trim().Length; mCont < 38; mCont++)
                //                 rPerigo = rPerigo + " ";
                //         }
                //         else
                //             rPerigo = zPerigo.Nome.Trim().Substring(0, 38);


                //         if (operacaoPerigoRisco.IndProbabilidadeRisco.ToString().Trim().Length < 15)
                //         {
                //             rProbabilidade = operacaoPerigoRisco.IndProbabilidadeRisco.ToString().Trim();

                //             for (int mCont = operacaoPerigoRisco.IndProbabilidadeRisco.ToString().Trim().Length; mCont < 15; mCont++)
                //                 rProbabilidade = rProbabilidade + " ";
                //         }
                //         else
                //             rProbabilidade = operacaoPerigoRisco.IndProbabilidadeRisco.ToString().Trim().Substring(0, 15);


                //         if ( rProbabilidade.Trim() == "0" )
                //             xSeveridade = xSeveridade + "Perigo: " + rPerigo + System.Environment.NewLine;
                //         else
                //             xSeveridade = xSeveridade + "Perigo: " + rPerigo + "   Probabilidade: " + rProbabilidade + "   Severidade: " + operacaoPerigoRisco.IndGrauSeveridadeRisco.ToString() + System.Environment.NewLine;
                //         //xProbabilidade = xProbabilidade + operacaoPerigoRisco.IndProbabilidadeRisco.ToString() + System.Environment.NewLine;

                //     }

                // }


                // newRow["Severidade"] = xSeveridade;
                // //newRow["Probabilidade"] = xProbabilidade;

                
                //if ( operacao.Precaucoes.Trim() == "" )
                //   newRow["Precaucoes"] = "( NA )";
                //else
                //   newRow["Precaucoes"] = operacao.Precaucoes;
                

				//ds.Tables[0].Rows.Add(newRow);
			}

			if (ds.Tables[0].Rows.Count.Equals(0))
			{
				newRow = ds.Tables[0].NewRow();
                //newRow["RazaoSocial"] = procedAtual.IdCliente.NomeCompleto;

                procedAtual.IdCliente.IdJuridicaPai.Find();

                if (procedAtual.IdCliente.IdJuridicaPai.Id != 0)
                    newRow["RazaoSocial"] = procedAtual.IdCliente.IdJuridicaPai.NomeCompleto + " - " + procedAtual.IdCliente.NomeCompleto;
                else
                    newRow["RazaoSocial"] = procedAtual.IdCliente.NomeCompleto;


                newRow["NomeEmpregado"]	= nomeEmpregado;
				newRow["nPOPS"] = procedAtual.Numero.ToString("0000");
				newRow["NomeProcedimento"] = procedAtual.Nome;
				newRow["EPI"] = EPI;
				newRow["EPC"] = procedimento.Epc;
				newRow["MedidasAdm"] = procedimento.MedidasAdm;
				newRow["MedidasEdu"] = procedimento.MedidasEdu;
				newRow["ComFoto"] = (Foto!=string.Empty);
				newRow["Foto"] = Foto;
				newRow["Sequencia"]	= string.Empty;
				newRow["Operacao"]	= string.Empty;
				newRow["RiscosAcidente"]	= string.Empty;

                //newRow["Probabilidade"] = "teste 00000101";
                //newRow["Severidade"] = "teste 00000102";
                //newRow["Precaucoes"] = "teste 0000103";
                
				ds.Tables[0].Rows.Add(newRow);
			}

			return ds;
        }
        #endregion

        #region GetProcedimentoGenerico

        private DataSet GetProcedimentoGenerico(DataSet ds, Procedimento procedimento)
		{
			DataRow newRow;
			procedimento.IdCliente.Find();

			string nomeEmpregado = string.Empty;

			try
			{
				nomeEmpregado = empregado.tNO_EMPG;
			}
			catch
			{
				nomeEmpregado = "Nenhum Empregado vinculado";
			}

			ArrayList alOperacao = new Operacao().Find("IdProcedimento=" + procedimento.Id + " ORDER BY Sequencia");

			foreach(Operacao operacao in alOperacao)
			{
				newRow = ds.Tables[0].NewRow();
                //Cabecalho
                //newRow["RazaoSocial"] = procedimento.IdCliente.NomeCompleto;
                procedimento.IdCliente.IdJuridicaPai.Find();

                if (procedimento.IdCliente.IdJuridicaPai.Id != 0)
                    newRow["RazaoSocial"] = procedimento.IdCliente.IdJuridicaPai.NomeCompleto + " - " + procedimento.IdCliente.NomeCompleto;
                else
                    newRow["RazaoSocial"] = procedimento.IdCliente.NomeCompleto;


                newRow["NomeEmpregado"] = nomeEmpregado;
				newRow["nPOPS"] = procedimento.Numero.ToString("0000");
				newRow["NomeProcedimento"] = procedimento.Nome;
				//Detalhe
				newRow["Sequencia"]	= operacao.Sequencia.ToString("00");
				newRow["Operacao"] = operacao.Descricao.ToString();
				ds.Tables[0].Rows.Add(newRow);
			}

			if (ds.Tables[0].Rows.Count.Equals(0))
			{
				newRow = ds.Tables[0].NewRow();
                //newRow["RazaoSocial"] = procedimento.IdCliente.NomeCompleto;
                procedimento.IdCliente.IdJuridicaPai.Find();

                if (procedimento.IdCliente.IdJuridicaPai.Id != 0)
                    newRow["RazaoSocial"] = procedimento.IdCliente.IdJuridicaPai.NomeCompleto + " - " + procedimento.IdCliente.NomeCompleto;
                else
                    newRow["RazaoSocial"] = procedimento.IdCliente.NomeCompleto;

                newRow["NomeEmpregado"]	= nomeEmpregado;
				newRow["nPOPS"] = procedimento.Numero.ToString("0000");
				newRow["NomeProcedimento"] = procedimento.Nome;
				newRow["Sequencia"]	= string.Empty;
				newRow["Operacao"] = string.Empty;
				ds.Tables[0].Rows.Add(newRow);
			}

			return ds;
        }
        #endregion

        #region GetProcedimentoEmpregado

        private DataSet GetProcedimentoEmpregado()
		{
			ArrayList listProcedimento = new ArrayList();

			DataSet ds = new DataSet();	
			ds.Tables.Add(GetTableProcedimento());

			foreach (Procedimento procedimento in alProcedimentos)
				GetProcedimento(ds, procedimento, false);

			return ds;
        }
        #endregion

        #region GetProcedimentoGenericoEmpregado

        private DataSet GetProcedimentoGenericoEmpregado()
		{
			ArrayList listProcedimento = new ArrayList();
			DataSet ds = new DataSet();
			ds.Tables.Add(GetTableProcedimento());
		
			foreach (Procedimento procedimentoGenerico in alProcedimentosGenericos)
				GetProcedimentoGenerico(ds, procedimentoGenerico);

			return ds;
        }
        #endregion

        #region GetListaDeProcedimentos

        private DataSet GetListaDeProcedimentos()
		{
			ArrayList listProcedimento = new ArrayList();
			DataSet ds = new DataSet();
			DataRow newRow;

			ds.Tables.Add(GetTableListagemProcedimento());
		
			int TotalDeProc = alProcedimentos.Count + alProcedimentosGenericos.Count;
			
			foreach(Procedimento procedimentoGenerico in alProcedimentosGenericos)
			{				
				newRow = ds.Tables[0].NewRow();
				newRow["NomeEmpregado"] = empregado.tNO_EMPG;
				newRow["nPOPS"]	= procedimentoGenerico.Numero.ToString("0000");
				newRow["NumeroProcedimentos"] = TotalDeProc;
				newRow["NomeProcedimento"]	= procedimentoGenerico.Nome;
				
				ds.Tables[0].Rows.Add(newRow);
			}

			foreach(Procedimento procedimento in alProcedimentos)
			{				
				newRow = ds.Tables[0].NewRow();
				newRow["NomeEmpregado"] = empregado.tNO_EMPG;
				newRow["nPOPS"]	= procedimento.Numero.ToString("0000");	
				newRow["NumeroProcedimentos"] = TotalDeProc;
				newRow["NomeProcedimento"]	= procedimento.Nome;
				
				ds.Tables[0].Rows.Add(newRow);
			}

			return ds;
        }
        #endregion

        #region OS Resumo

        #region GetReportOSResumo

        public RptOSResumo GetReportOSResumo()
        {
            LaudoTecnico laudoTecnico = LaudoTecnico.GetUltimoLaudo(cliente.Id);

            return GetReportOSResumo(laudoTecnico, empregadoFuncao);
        }

        public RptOSResumo GetReportOSResumo(LaudoTecnico laudo, EmpregadoFuncao empregadoFuncao)
        {
            if (Ilitera.Data.Table.IsWeb)
            {
                DataSet dsOS17 = new OrdemServicoNR1_7().Get("IdCliente=" + cliente.Id
                    + " AND IdPedido IN (SELECT IdPedido FROM Pedido WHERE DataConclusao IS NOT NULL AND DataCancelamento IS NULL"
                    + " AND IdCliente=" + cliente.Id + " AND IdObrigacao=" + (int)Obrigacoes.OrdemServico + ")"
                    + " AND IdLaudoTecnico=" + laudo.Id);

                if (dsOS17.Tables[0].Rows.Count.Equals(0))
                    throw new Exception("A OS NR 1.7 ainda não está concluída para o respectivo Laudo Técnico. Qualquer dúvida entre em contato com a Ilitera.");
            }

            RptOSResumo report = new RptOSResumo();
            report.SetDataSource(GetOSResumoEmpregado(laudo, empregadoFuncao));
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }
        #endregion

        #region GetOSResumoEmpregado

        private DataSet GetOSResumoEmpregado(LaudoTecnico laudo, EmpregadoFuncao empregadoFuncao)
        {
            DataSet ds = new DataSet();
            DataRow newRow;

            ds.Tables.Add(GetTableOSResumo());

            newRow = ds.Tables[0].NewRow();

            empregadoFuncao.nID_FUNCAO.Find();
            empregadoFuncao.nID_SETOR.Find();
            empregadoFuncao.nID_TEMPO_EXP.Find();

            Municipio municipio;

            //EGS - Cada localiadade deve indicar seu estado de origem
            //if (cliente.IdJuridicaPai.Id == 0)
                municipio = cliente.GetMunicipio();
            //else
            //    municipio = cliente.IdJuridicaPai.GetMunicipio();

            Ghe ghe = empregadoFuncao.GetGheEmpregado(laudo);

            newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudo.hDT_LAUDO);
            newRow["NomeEmpregado"] = empregado.tNO_EMPG;
            newRow["CTPS"] = empregado.GetCTPS();
            newRow["NomeSetor"] = empregadoFuncao.nID_SETOR.tNO_STR_EMPR;
            newRow["NomeFuncao"] = empregadoFuncao.nID_FUNCAO.NomeFuncao;
            newRow["DescricaoFuncao"] = empregadoFuncao.GetDescricaoFuncao();
            newRow["NomeRiscos"] = ghe.GetRiscoAcidente(", ");
            
            string pathFoto = empregado.FotoEmpregado();

            if (!pathFoto.Equals(string.Empty) && System.IO.File.Exists(pathFoto))
            {
                newRow["FotoEmpregado"] = pathFoto;
                newRow["ComFoto"] = true;
            }
            else
                newRow["ComFoto"] = false;
            
            newRow["strEPI"] = ghe.Epi(", ");
            newRow["strEPC"] = Utility.SubstituirQuebraLinhaPor(ghe.Epc(), ", ");
            newRow["strMedAdm"] = Utility.SubstituirQuebraLinhaPor(ghe.GetMedidasControleAdministrativa(), ", ");
            newRow["strMedEdu"] = Utility.SubstituirQuebraLinhaPor(ghe.GetMedidasControleEducacional(), ", ");
            newRow["strObrigProib"] = ghe.GetProcedimentoInstrutivoHtml();
            
            newRow["Cidade"] = municipio.NomeCompleto;
            newRow["DataAssinatura"] = DateTime.Today;
            newRow["Jornada"] = empregadoFuncao.nID_TEMPO_EXP.tHORA_EXTENSO_SEMANAL;
            newRow["DataInicioAtividade"] = empregadoFuncao.hDT_INICIO.ToString("dd-MM-yyyy");

            if (empregadoFuncao.hDT_TERMINO != new DateTime())
                newRow["DataTerminoAtividade"] = empregadoFuncao.hDT_TERMINO.ToString("dd-MM-yyyy");
            else
                newRow["DataTerminoAtividade"] = "__-__-____";

            ds.Tables[0].Rows.Add(newRow);
            
            return ds;
        }
        #endregion

        #region GetTableOSResumo

        private DataTable GetTableOSResumo()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("CarimboCNPJ", typeof(string));
            table.Columns.Add("NomeEmpregado", typeof(string));
            table.Columns.Add("CTPS", typeof(string));
            table.Columns.Add("NomeSetor", typeof(string));
            table.Columns.Add("NomeFuncao", typeof(string));
            table.Columns.Add("DescricaoFuncao", typeof(string));
            table.Columns.Add("NomeRiscos", typeof(string));
            table.Columns.Add("strEPI", typeof(string));
            table.Columns.Add("FotoEmpregado", typeof(string));
            table.Columns.Add("ComFoto", typeof(bool));
            table.Columns.Add("strEPC", typeof(string));
            table.Columns.Add("strMedAdm", typeof(string));
            table.Columns.Add("strMedEdu", typeof(string));
            table.Columns.Add("strObrigProib", typeof(string));
            table.Columns.Add("Cidade", typeof(string));
            table.Columns.Add("Jornada", typeof(string));
            table.Columns.Add("DataInicioAtividade", typeof(string));
            table.Columns.Add("DataTerminoAtividade", typeof(string));
            table.Columns.Add("DataAssinatura", typeof(DateTime));

            return table;
        }
        #endregion

        #endregion

        #region GetIntroducaoOS

        private DataSet GetIntroducaoOS(string xCabecalho)
        {
            DataSet ds = new DataSet();
            DataRow newRow;
            string sexo = string.Empty;

            DataTable table = new DataTable("Result");
            table.Columns.Add("IdCliente", typeof(int));
            table.Columns.Add("RazaoSocial", typeof(string));
            table.Columns.Add("Endereco", typeof(string));
            table.Columns.Add("CEP", typeof(string));
            table.Columns.Add("Cidade", typeof(string));
            table.Columns.Add("Estado", typeof(string));
            table.Columns.Add("CNPJ", typeof(string));
            table.Columns.Add("CNAE", typeof(string));
            table.Columns.Add("NomeEmpregado", typeof(string));
            table.Columns.Add("PisPasep", typeof(string));
            table.Columns.Add("DataNascimento", typeof(string));
            table.Columns.Add("Sexo", typeof(string));
            table.Columns.Add("Admissao", typeof(string));
            table.Columns.Add("CBO", typeof(string));
            table.Columns.Add("Funcao", typeof(string));
            table.Columns.Add("Setor", typeof(string));
            table.Columns.Add("Cargo", typeof(string));
            table.Columns.Add("CTPS", typeof(string));
            table.Columns.Add("Demissao", typeof(string));
            table.Columns.Add("Foto", typeof(string));
            table.Columns.Add("ComFoto", typeof(bool));
            table.Columns.Add("DataOS", typeof(DateTime));
            table.Columns.Add("Cabecalho", typeof(string));

            ds.Tables.Add(table);

            Endereco endereco = cliente.GetEndereco();
            Municipio municipio = cliente.GetMunicipio();
            municipio.IdUnidadeFederativa.Find();

            empregadoFuncao.nID_FUNCAO.Find();

            cliente.IdCNAE.Find();

            if (empregado.tSEXO.Equals("M"))
                sexo = "Masculino";
            else if (empregado.tSEXO.Equals("F"))
                sexo = "Feminino";

            newRow = ds.Tables[0].NewRow();
            newRow["IdCliente"] = cliente.Id;
            //newRow["RazaoSocial"] = cliente.NomeCompleto;
            cliente.IdJuridicaPai.Find();

            if (cliente.IdJuridicaPai.Id != 0)
                newRow["RazaoSocial"] = cliente.IdJuridicaPai.NomeCompleto + " - " + cliente.NomeCompleto;
            else
                newRow["RazaoSocial"] = cliente.NomeCompleto;

            if (xCabecalho != "")
                newRow["Cabecalho"] = xCabecalho;
            else
                newRow["Cabecalho"] = "ORDEM DE SERVIÇO";


            newRow["Endereco"] = endereco.GetEndereco();
            newRow["CEP"] = endereco.Cep;
            newRow["Cidade"] = municipio.NomeCompleto;
            municipio.IdUnidadeFederativa.Find();
            newRow["Estado"] = municipio.IdUnidadeFederativa.NomeAbreviado;
            newRow["CNPJ"] = cliente.NomeCodigo;
            newRow["CNAE"] = cliente.IdCNAE.Codigo + " - " + cliente.IdCNAE.Descricao;
            newRow["NomeEmpregado"] = empregado.tNO_EMPG;
            newRow["PisPasep"] = empregado.nNO_PIS_PASEP.ToString();
            if (empregado.hDT_NASC != new DateTime() && empregado.hDT_NASC != new DateTime(1753, 1, 1))
                newRow["DataNascimento"] = empregado.hDT_NASC.ToString("dd-MM-yyyy");
            newRow["Sexo"] = sexo;
            if (empregado.hDT_ADM != new DateTime() && empregado.hDT_ADM != new DateTime(1753, 1, 1))
                newRow["Admissao"] = empregado.hDT_ADM.ToString("dd-MM-yyyy");
            newRow["CBO"] = empregadoFuncao.nID_FUNCAO.NumeroCBO;
            newRow["Funcao"] = empregadoFuncao.GetNomeFuncao();
            newRow["Setor"] = empregadoFuncao.GetNomeSetor();
            newRow["Cargo"] = empregadoFuncao.GetCargoEmpregado();
            newRow["CTPS"] = empregado.GetCTPS();
            if (empregado.hDT_DEM != new DateTime() && empregado.hDT_DEM != new DateTime(1753, 1, 1))
                newRow["Demissao"] = empregado.hDT_DEM.ToString("dd-MM-yyyy");

            string pathFoto = empregado.FotoEmpregado();

            if (pathFoto != string.Empty && System.IO.File.Exists(pathFoto))
            {
                newRow["Foto"] = pathFoto;
                newRow["ComFoto"] = true;
            }
            else
                newRow["ComFoto"] = false;

            newRow["DataOS"] = this.dataOS;

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }
        #endregion

        #region GetProceCheck

        private DataSet GetProceCheck()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("hasEspecifico", Type.GetType("System.Boolean"));
			ds.Tables.Add(table);

			DataRow newRow = ds.Tables[0].NewRow();			
			newRow["hasEspecifico"] = hasEspecifico;
			ds.Tables[0].Rows.Add(newRow);

			return ds;
        }
        #endregion

        #region GetTableProcedimento

        public static DataTable GetTableProcedimento()
		{
			DataTable table = new DataTable("Result");
			
			table.Columns.Add("RazaoSocial", typeof(string));
			table.Columns.Add("nPOPS", typeof(string));
			table.Columns.Add("NomeEmpregado", typeof(string));
			table.Columns.Add("NomeProcedimento", typeof(string));
			table.Columns.Add("DataInicio", typeof(string));
			table.Columns.Add("DataTermino", typeof(string));
			table.Columns.Add("EPI", typeof(string));
			table.Columns.Add("EPC", typeof(string));
			table.Columns.Add("MedidasAdm", typeof(string));
			table.Columns.Add("MedidasEdu", typeof(string));
			table.Columns.Add("Foto", typeof(string));
			table.Columns.Add("ComFoto", typeof(bool));
			table.Columns.Add("Sequencia", typeof(string));
			table.Columns.Add("Operacao", typeof(string));
			table.Columns.Add("RiscosAcidente", typeof(string));
            table.Columns.Add("Probabilidade", typeof(string));
            table.Columns.Add("Severidade", typeof(string));
            table.Columns.Add("Precaucoes", typeof(string));
            table.Columns.Add("Perigos", typeof(string));

			return table;
        }
        #endregion

        #region GetTableListagemProcedimento

        private DataTable GetTableListagemProcedimento()
		{
			DataTable table = new DataTable("Result");

			table.Columns.Add("NomeEmpregado", typeof(string));
			table.Columns.Add("nPOPS", typeof(string));
			table.Columns.Add("NomeProcedimento", typeof(string));
			table.Columns.Add("NumeroProcedimentos", typeof(string));
            table.Columns.Add("TipoProcedimento", typeof(int));

			return table;
        }
        #endregion

        #region IsProcedimentoJaListado

        private bool IsProcedimentoJaListado(Procedimento procedimento, ArrayList listProcedimento)
		{
			foreach (Procedimento procedimentoExistente in listProcedimento)
				if (procedimento.Id.Equals(procedimentoExistente.Id))
					return true;

			return false;
        }
        #endregion
    }
}
