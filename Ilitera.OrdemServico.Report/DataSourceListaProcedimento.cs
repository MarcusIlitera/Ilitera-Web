using System;
using System.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Collections;
using System.Drawing;

namespace Ilitera.OrdemServico.Report
{	
	public class DataSourceListaProcedimento : DataSourceBase
	{
		private Cliente cliente;
		private bool orderByNumProc;

		public DataSourceListaProcedimento(Cliente cliente, bool orderByNumProc)
		{
			this.cliente = cliente;
			this.orderByNumProc = orderByNumProc;
		}

		public RptListaProcedimentos GetReportListaProcedimento()
		{
			RptListaProcedimentos report = new RptListaProcedimentos();
			report.SetDataSource(GetListaProcedimento());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

        public ReportPOP4 GetReportPOPSColetivo(bool ComFoto)
        {
            ReportPOP4 report = new ReportPOP4();
            report.SetDataSource(GetDataSourceProcedimento(ComFoto));
            report.Refresh();
            return report;
        }

        private DataSet GetDataSourceProcedimento(bool ComFoto)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(DataSourceOrdemDeServico.GetTableProcedimento());

            ArrayList listProcedimento = GetProcedimentos();

            foreach (Procedimento procedimento in listProcedimento)
                GetProcedimento(ds, procedimento, ComFoto);

            return ds;
        }

        private DataSet GetProcedimento(DataSet ds, Procedimento procedimento, bool ComFoto)
        {
            Procedimento procedAtual = procedimento;

            procedAtual.IdCliente.Find();

            if (procedimento.IndTipoProcedimento == TipoProcedimento.Especifico)
            {
                procedimento = procedimento.IdProcedimentoResumo;
                procedimento.Find();
            }

            string sEPI = procedimento.strEpi();
            string sFoto = string.Empty;
            string sNomeEmpregado = "Nenhum Empregado vinculado";
            string sDataInicio = "__-__-____";
            string sDataTermino = "__-__-____";

            if (ComFoto)
                sFoto = procedAtual.FotoProcedimento();

            ArrayList alOperacao = new Operacao().Find("IdProcedimento=" + procedimento.Id + " ORDER BY Sequencia");

            DataRow newRow;

            foreach (Operacao operacao in alOperacao)
            {
                newRow = ds.Tables[0].NewRow();
                //Cabechalho
                newRow["RazaoSocial"] = procedAtual.IdCliente.NomeCompleto;
                newRow["NomeEmpregado"] = sNomeEmpregado;
                newRow["nPOPS"] = procedAtual.Numero.ToString("0000");
                newRow["NomeProcedimento"] = procedAtual.Nome;
                newRow["EPI"] = sEPI;
                newRow["EPC"] = procedimento.Epc;
                newRow["MedidasAdm"] = procedimento.MedidasAdm;
                newRow["MedidasEdu"] = procedimento.MedidasEdu;
                newRow["ComFoto"] = (sFoto != string.Empty);
                newRow["Foto"] = sFoto;
                newRow["DataInicio"] = sDataInicio;
                newRow["DataTermino"] = sDataTermino;
                //Detalhe
                newRow["Sequencia"] = operacao.Sequencia.ToString("00");
                newRow["Operacao"] = operacao.Descricao.ToString();
                newRow["RiscosAcidente"] = operacao.strAcidentes();
                ds.Tables[0].Rows.Add(newRow);
            }

            if (ds.Tables[0].Rows.Count.Equals(0))
            {
                newRow = ds.Tables[0].NewRow();
                newRow["RazaoSocial"] = procedAtual.IdCliente.NomeCompleto;
                newRow["NomeEmpregado"] = sNomeEmpregado;
                newRow["nPOPS"] = procedAtual.Numero.ToString("0000");
                newRow["NomeProcedimento"] = procedAtual.Nome;
                newRow["EPI"] = sEPI;
                newRow["EPC"] = procedimento.Epc;
                newRow["MedidasAdm"] = procedimento.MedidasAdm;
                newRow["MedidasEdu"] = procedimento.MedidasEdu;
                newRow["ComFoto"] = (sFoto != string.Empty);
                newRow["Foto"] = sFoto;
                newRow["DataInicio"] = sDataInicio;
                newRow["DataTermino"] = sDataTermino;
                newRow["Sequencia"] = string.Empty;
                newRow["Operacao"] = string.Empty;
                newRow["RiscosAcidente"] = string.Empty;
                ds.Tables[0].Rows.Add(newRow);
            }

            return ds;
        }

		private DataSet GetListaProcedimento()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
            table.Columns.Add("nPOPS", Type.GetType("System.String"));
			table.Columns.Add("NomeProcedimento", Type.GetType("System.String"));
            table.Columns.Add("TipoProcedimento", Type.GetType("System.Int32"));	
			ds.Tables.Add(table);

            ArrayList listProcedimento = GetProcedimentos();

			DataRow newRow;	
			
			foreach(Procedimento procedimento in listProcedimento)
			{
				newRow = ds.Tables[0].NewRow();							
				newRow["NomeEmpresa"] = this.cliente.NomeCompleto;
                newRow["nPOPS"] = procedimento.Numero.ToString("0000");
				newRow["NomeProcedimento"] = procedimento.Nome;
                newRow["TipoProcedimento"] = procedimento.IndTipoProcedimento;
				ds.Tables[0].Rows.Add(newRow);
			}
			return ds;
		}

        private ArrayList GetProcedimentos()
        {
            ArrayList listProcedimento;

            if (orderByNumProc)
                listProcedimento = new Procedimento().Find("IdCliente=" + this.cliente.Id + " ORDER BY Numero");
            else
                listProcedimento = new Procedimento().Find("IdCliente=" + this.cliente.Id + " ORDER BY Nome");

            return listProcedimento;
        }
	}
}
