using System;
using System.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Collections;
using System.Drawing;
using System.Text;

namespace Ilitera.OrdemServico.Report
{	
	public class DataSourceAPR : DataSourceBase
	{
		private Procedimento procedimento;

        public DataSourceAPR(Procedimento procedimento)
		{
			this.procedimento = procedimento;
		}

        public ReportAPR GetReportAPR()
		{
            ReportAPR report = new ReportAPR();
            report.SetDataSource(GetDataSourceAPR());
            report.Refresh();

            SetTempoProcessamento(report);

			return report;
        }

        public ReportAPI GetReportAPI()
        {
            ReportAPI report = new ReportAPI();
            report.SetDataSource(GetDataSourceAPI());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

        #region DataSourceAPR
        private DataSet GetDataSourceAPR()
        {
            DataSet ds = new DataSet();
            DataTable tableHeader = GetTableHearder();
            DataTable table = GetTable();

            PopulaHeader(tableHeader);
            PopulaDetalhes(table);

            ds.Tables.Add(tableHeader);
            ds.Tables.Add(table);

            return ds;
        }

        private void PopulaDetalhes(DataTable table)
        {
            DataRow newRow;

            ArrayList alOperacao = new Operacao().Find("IdProcedimento=" + procedimento.Id
                + " ORDER BY Sequencia");

            if (alOperacao.Count.Equals(0))
            {
                newRow = table.NewRow();
                table.Rows.Add(newRow);
            }

            foreach (Operacao operacao in alOperacao)
            {
                ArrayList alPerigo = new Perigo().Find("IdPerigo IN (SELECT IdPerigo FROM OperacaoPerigo WHERE IdOperacao=" + operacao.Id
                    + ") ORDER BY Nome");

                if (alPerigo.Count.Equals(0))
                {
                    newRow = table.NewRow();
                    newRow["IdOperacao"] = operacao.Id;
                    newRow["NumOperacao"] = operacao.Sequencia.ToString("00");
                    newRow["NomeOperacao"] = operacao.Descricao;
                    table.Rows.Add(newRow);
                }

                foreach (Perigo perigo in alPerigo)
                {
                    ArrayList alRiscoAcidente = new RiscoAcidente().Find("IdRiscoAcidente IN (SELECT IdRiscoAcidente FROM OperacaoPerigoRiscoAcidente WHERE"
                        + " IdOperacaoPerigo IN (SELECT IdOperacaoPerigo FROM OperacaoPerigo WHERE"
                        + " IdOperacao=" + operacao.Id + " AND IdPerigo=" + perigo.Id + "))"
                        + " ORDER BY Nome");

                    if (alRiscoAcidente.Count.Equals(0))
                    {
                        newRow = table.NewRow();
                        newRow["IdOperacao"] = operacao.Id;
                        newRow["NumOperacao"] = operacao.Sequencia.ToString("00");
                        newRow["NomeOperacao"] = operacao.Descricao;
                        newRow["IdPerigo"] = perigo.Id;
                        newRow["NomePerigo"] = perigo.Nome;
                        table.Rows.Add(newRow);
                    }

                    foreach (RiscoAcidente riscoAcidente in alRiscoAcidente)
                    {
                        StringBuilder sbDanos = new StringBuilder();
                        StringBuilder sbEPI = new StringBuilder();

                        OperacaoPerigoRiscoAcidente operacaoPerigoRiscoAcidente = new OperacaoPerigoRiscoAcidente();
                        operacaoPerigoRiscoAcidente.Find("IdOperacaoPerigo IN (SELECT IdOperacaoPerigo FROM OperacaoPerigo WHERE"
                            + " IdOperacao=" + operacao.Id + " AND IdPerigo=" + perigo.Id
                            + ") AND IdRiscoAcidente=" + riscoAcidente.Id);

                        ArrayList alDano = new Dano().Find("IdDano IN (SELECT IdDano FROM OperacaoPerigoRiscoDano WHERE"
                            + " IdOperacaoPerigoRiscoAcidente=" + operacaoPerigoRiscoAcidente.Id
                            + ") ORDER BY Nome");

                        foreach (Dano dano in alDano)
                        {
                            if (!sbDanos.ToString().Equals(string.Empty))
                                sbDanos.Append(", ");

                            sbDanos.Append(dano.Nome);
                        }

                        ArrayList alEPI = new Epi().Find("IdEPI IN (SELECT IdEpi FROM OperacaoPerigoRiscoEPI WHERE"
                            + " IdOperacaoPerigoRiscoAcidente=" + operacaoPerigoRiscoAcidente.Id
                            + ") ORDER BY Descricao");

                        foreach (Epi epi in alEPI)
                        {
                            if (!sbEPI.ToString().Equals(string.Empty))
                                sbEPI.Append(", ");

                            sbEPI.Append(epi.ToString());
                        }

                        newRow = table.NewRow();
                        newRow["IdOperacao"] = operacao.Id;
                        newRow["NumOperacao"] = operacao.Sequencia.ToString("00");
                        newRow["NomeOperacao"] = operacao.Descricao;
                        newRow["IdPerigo"] = perigo.Id;
                        newRow["NomePerigo"] = perigo.Nome;
                        newRow["Riscos"] = riscoAcidente.Nome;
                        newRow["DanoPerda"] = sbDanos.ToString();
                        newRow["EPI"] = sbEPI.ToString();
                        newRow["EPC"] = operacaoPerigoRiscoAcidente.Epc;
                        newRow["MedAdm"] = operacaoPerigoRiscoAcidente.MedidasAdm;
                        newRow["MedEdu"] = operacaoPerigoRiscoAcidente.MedidasEdu;
                        newRow["Probabilidade"] = operacaoPerigoRiscoAcidente.GetIndiceProbabilidade();
                        newRow["Severidade"] = operacaoPerigoRiscoAcidente.GetIndiceSeveridade();
                        newRow["GrauRisco"] = operacaoPerigoRiscoAcidente.GetIndiceGrauRisco();
                        table.Rows.Add(newRow);
                    }
                }
            }
        }
        #endregion

        #region DataSourceAPI
        private DataSet GetDataSourceAPI()
        {
            DataSet ds = new DataSet();
            DataTable tableHeader = GetTableHearder();
            DataTable tableImpacto = GetTableImpacto();

            PopulaHeader(tableHeader);
            PopulaDetalhesAPI(tableImpacto);

            ds.Tables.Add(tableHeader);
            ds.Tables.Add(tableImpacto);

            return ds;
        }

        private void PopulaDetalhesAPI(DataTable table)
        {
            DataRow newRow;

            ArrayList alOperacao = new Operacao().Find("IdProcedimento=" + procedimento.Id
                + " ORDER BY Sequencia");

            if (alOperacao.Count.Equals(0))
            {
                newRow = table.NewRow();
                table.Rows.Add(newRow);
            }

            foreach (Operacao operacao in alOperacao)
            {
                ArrayList alAspecto = new AspectoAmbiental().Find("IdAspectoAmbiental IN (SELECT IdAspectoAmbiental FROM OperacaoAspectoAmbiental WHERE IdOperacao=" + operacao.Id
                    + ") ORDER BY Nome");

                if (alAspecto.Count.Equals(0))
                {
                    newRow = table.NewRow();
                    newRow["IdOperacao"] = operacao.Id;
                    newRow["NumOperacao"] = operacao.Sequencia.ToString("00");
                    newRow["NomeOperacao"] = operacao.Descricao;
                    table.Rows.Add(newRow);
                }

                foreach (AspectoAmbiental aspecto in alAspecto)
                {
                    ArrayList alImpacto = new ImpactoAmbiental().Find("IdImpactoAmbiental IN (SELECT IdImpactoAmbiental FROM OperacaoAspectoAmbientalImpacto WHERE"
                        + " IdOperacaoAspectoAmbiental IN (SELECT IdOperacaoAspectoAmbiental FROM OperacaoAspectoAmbiental WHERE"
                        + " IdOperacao=" + operacao.Id + " AND IdAspectoAmbiental=" + aspecto.Id + "))"
                        + " ORDER BY Nome");

                    if (alImpacto.Count.Equals(0))
                    {
                        newRow = table.NewRow();
                        newRow["IdOperacao"] = operacao.Id;
                        newRow["NumOperacao"] = operacao.Sequencia.ToString("00");
                        newRow["NomeOperacao"] = operacao.Descricao;
                        newRow["IdAspecto"] = aspecto.Id;
                        newRow["NomeAspecto"] = aspecto.Nome;
                        table.Rows.Add(newRow);
                    }

                    foreach (ImpactoAmbiental impacto in alImpacto)
                    {
                        OperacaoAspectoAmbientalImpacto operacaoAspectoAmbientalImpacto = new OperacaoAspectoAmbientalImpacto();
                        operacaoAspectoAmbientalImpacto.Find("IdOperacaoAspectoAmbiental IN (SELECT IdOperacaoAspectoAmbiental FROM OperacaoAspectoAmbiental WHERE"
                            + " IdOperacao=" + operacao.Id + " AND IdAspectoAmbiental=" + aspecto.Id
                            + ") AND IdImpactoAmbiental=" + impacto.Id);

                        newRow = table.NewRow();
                        newRow["IdOperacao"] = operacao.Id;
                        newRow["NumOperacao"] = operacao.Sequencia.ToString("00");
                        newRow["NomeOperacao"] = operacao.Descricao;
                        newRow["IdAspecto"] = aspecto.Id;
                        newRow["NomeAspecto"] = aspecto.Nome;
                        newRow["Impactos"] = impacto.Nome;
                        newRow["MedOpe"] = operacaoAspectoAmbientalImpacto.MedidasOpe;
                        newRow["MedEdu"] = operacaoAspectoAmbientalImpacto.MedidasEdu;
                        newRow["Probabilidade"] = operacaoAspectoAmbientalImpacto.GetIndiceProbabilidade();
                        newRow["Severidade"] = operacaoAspectoAmbientalImpacto.GetIndiceSeveridade();
                        newRow["GrauImpacto"] = operacaoAspectoAmbientalImpacto.GetIndiceGrauImpacto();
                        table.Rows.Add(newRow);
                    }
                }
            }
        }
        #endregion

        #region CommonMethods
        private void PopulaHeader(DataTable table)
        {
            DataRow newRow;
            string tipoSituacao = string.Empty;

            switch (procedimento.IndTipoSituacao)
            {
                case (short)IndTipoSituacao.Anormal:
                    tipoSituacao = "Anormal";
                    break;
                case (short)IndTipoSituacao.Emergencial:
                    tipoSituacao = "Emergencial";
                    break;
                case (short)IndTipoSituacao.Normal:
                    tipoSituacao = "Normal";
                    break;
            }

            if (procedimento.IdElaboradorAPP.mirrorOld == null)
                procedimento.IdElaboradorAPP.Find();

            if (procedimento.IdRevisorAPP.mirrorOld == null)
                procedimento.IdRevisorAPP.Find();

            newRow = table.NewRow();
            newRow["CarimboCNPJ"] = procedimento.IdCliente.GetCarimboCnpjHtml(DateTime.Now);
            newRow["DataEmissao"] = DateTime.Now.ToString("dd-MM-yyyy");
            newRow["NomeProcedimento"] = procedimento.Numero.ToString("0000") + " - " + procedimento.Nome;
            newRow["Situacao"] = tipoSituacao;
            newRow["Setor"] = procedimento.GetSetores();
            newRow["PostoTrabalho"] = procedimento.GetEquipamentos();
            newRow["Elaborador"] = procedimento.IdElaboradorAPP.NomeCompleto;
            newRow["FuncaoElaborador"] = procedimento.IdElaboradorAPP.Titulo;
            
            if (!procedimento.DataElaboracaoAPP.Equals(new DateTime()))
                newRow["DataElaboracao"] = procedimento.DataElaboracaoAPP.ToString("dd-MM-yyyy");

            newRow["Revisor"] = procedimento.IdRevisorAPP.NomeCompleto;
            newRow["FuncaoRevisor"] = procedimento.IdRevisorAPP.Titulo;

            if (!procedimento.DataRevisaoAPP.Equals(new DateTime()))
                newRow["DataRevisao"] = procedimento.DataRevisaoAPP.ToString("dd-MM-yyyy");

            table.Rows.Add(newRow);
        }
        #endregion

        #region Tables
        private DataTable GetTableHearder()
        {
            DataTable table = new DataTable("ResultHeader");

            table.Columns.Add("CarimboCNPJ", typeof(string));
            table.Columns.Add("DataEmissao", typeof(string));
            table.Columns.Add("NomeProcedimento", typeof(string));
            table.Columns.Add("Situacao", typeof(string));
            table.Columns.Add("Setor", typeof(string));
            table.Columns.Add("PostoTrabalho", typeof(string));
            table.Columns.Add("Elaborador", typeof(string));
            table.Columns.Add("FuncaoElaborador", typeof(string));
            table.Columns.Add("DataElaboracao", typeof(string));
            table.Columns.Add("Revisor", typeof(string));
            table.Columns.Add("FuncaoRevisor", typeof(string));
            table.Columns.Add("DataRevisao", typeof(string));

            return table;
        }

        private DataTable GetTable()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("IdOperacao", typeof(int));
            table.Columns.Add("NumOperacao", typeof(string));
            table.Columns.Add("NomeOperacao", typeof(string));
            table.Columns.Add("IdPerigo", typeof(int));
            table.Columns.Add("NomePerigo", typeof(string));
            table.Columns.Add("Riscos", typeof(string));
            table.Columns.Add("DanoPerda", typeof(string));
            table.Columns.Add("EPI", typeof(string));
            table.Columns.Add("EPC", typeof(string));
            table.Columns.Add("MedAdm", typeof(string));
            table.Columns.Add("MedEdu", typeof(string));
            table.Columns.Add("Probabilidade", typeof(string));
            table.Columns.Add("Severidade", typeof(string));
            table.Columns.Add("GrauRisco", typeof(string));

            return table;
        }

        private DataTable GetTableImpacto()
        {
            DataTable table = new DataTable("ResultImpacto");

            table.Columns.Add("IdOperacao", typeof(int));
            table.Columns.Add("NumOperacao", typeof(string));
            table.Columns.Add("NomeOperacao", typeof(string));
            table.Columns.Add("IdAspeccto", typeof(int));
            table.Columns.Add("NomeAspecto", typeof(string));
            table.Columns.Add("Impactos", typeof(string));
            table.Columns.Add("MedOpe", typeof(string));
            table.Columns.Add("MedEdu", typeof(string));
            table.Columns.Add("Probabilidade", typeof(string));
            table.Columns.Add("Severidade", typeof(string));
            table.Columns.Add("GrauImpacto", typeof(string));

            return table;
        }
        #endregion
    }
}
