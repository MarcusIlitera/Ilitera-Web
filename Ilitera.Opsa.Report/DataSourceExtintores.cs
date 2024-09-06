using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Report
{	
	public class DataSourceExtintores : DataSourceBase
	{
		private Extintores extintor;
		private Cliente cliente;

		public DataSourceExtintores(Extintores extintor)
		{
			this.extintor = extintor;
			this.extintor.IdCliente.Find();
			this.extintor.IdTipoExtintor.Find();
			this.extintor.IdFabricanteExtintor.Find();
			this.extintor.IdSetor.Find();
		}

		public DataSourceExtintores(Cliente cliente)
		{
			this.cliente = cliente;
		}

		public RptListagemExtintores GetReportListagemExtintores()
		{
			RptListagemExtintores report = new RptListagemExtintores();
			report.SetDataSource(GetListagemExtintores());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		public RptExtintorDetalhe GetReportExtintor()
		{
			RptExtintorDetalhe report = new RptExtintorDetalhe();
			report.OpenSubreport("HistoricoExtintor").SetDataSource(GetHistoricoExtintor());
			report.SetDataSource(GetDadosBasicos());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		private DataSet GetListagemExtintores()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			DataRow row;

			table.Columns.Add("NomeCliente", Type.GetType("System.String"));
			table.Columns.Add("AtivoFixo", Type.GetType("System.String"));
			table.Columns.Add("DataFabricacao", Type.GetType("System.String"));
            table.Columns.Add("VencimentoRecarga", Type.GetType("System.String"));
            table.Columns.Add("Garantia", Type.GetType("System.String"));
			table.Columns.Add("TipoExtintor", Type.GetType("System.String"));
			table.Columns.Add("CondicaoExtintor", Type.GetType("System.String"));
			table.Columns.Add("PesoCheio", Type.GetType("System.String"));
			table.Columns.Add("PesoVazio", Type.GetType("System.String"));
			table.Columns.Add("Fabricante", Type.GetType("System.String"));
			table.Columns.Add("Setor", Type.GetType("System.String"));
			ds.Tables.Add(table);

			ArrayList alExtintores = new Extintores().Find("IdCliente=" + cliente.Id + " ORDER BY VencimentoRecarga, AtivoFixo");

			foreach (Extintores extintores in alExtintores)
			{
				extintores.IdTipoExtintor.Find();
				extintores.IdFabricanteExtintor.Find();
				extintores.IdSetor.Find();
				
				row = ds.Tables[0].NewRow();
				row["NomeCliente"] = cliente.NomeAbreviado;
				row["AtivoFixo"] = extintores.AtivoFixo;
				row["DataFabricacao"] = extintores.DataFabricacao.ToString("dd-MM-yyyy");
                row["VencimentoRecarga"] = extintores.VencimentoRecarga.ToString("dd-MM-yyyy");
                row["Garantia"] = extintores.Garantia.ToString();
				row["TipoExtintor"] = extintores.IdTipoExtintor.ModeloExtintor;
				switch (extintores.IndCondicao)
				{
					case (int)IndCondicaoExtintor.Alocado:
					{
						row["CondicaoExtintor"] = "Alocado";
						break;
					}
					case (int)IndCondicaoExtintor.ForaDeUso:
					{
						row["CondicaoExtintor"] = "Fora de Uso";
						break;
					}
					case (int)IndCondicaoExtintor.Reserva:
					{
						row["CondicaoExtintor"] = "Reserva";
						break;
					}
				}
				row["PesoCheio"] = extintores.PesoCheio.ToString();
				row["PesoVazio"] = extintores.PesoVazio.ToString();
				row["Fabricante"] = extintores.IdFabricanteExtintor.NomeAbreviado;
				row["Setor"] = extintores.IdSetor.tNO_STR_EMPR;
				ds.Tables[0].Rows.Add(row);
			}

			return ds;
		}

		private DataSet GetHistoricoExtintor()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			DataRow row;

			table.Columns.Add("DataEvento", Type.GetType("System.String"));
			table.Columns.Add("TipoHistorico", Type.GetType("System.String"));
			table.Columns.Add("Reparacao", Type.GetType("System.String"));
			table.Columns.Add("PesoAtual", Type.GetType("System.String"));
			table.Columns.Add("Responsavel", Type.GetType("System.String"));
			table.Columns.Add("NumHistorico", Type.GetType("System.String"));
			ds.Tables.Add(table);

			ArrayList alHistorico = new HistoricoExtintor().Find("IdExtintores=" + extintor.Id + " ORDER BY DataEvento DESC");

			foreach (HistoricoExtintor historico in alHistorico)
			{
				historico.IdUsuarioResp.Find();

				row = ds.Tables[0].NewRow();
				row["DataEvento"] = historico.DataEvento.ToString("dd-MM-yyyy");
				row["TipoHistorico"] = historico.GetTipoHistorico();
				row["Reparacao"] = historico.GetReparacao();
				row["PesoAtual"] = historico.PesoAtual.ToString();
				row["Responsavel"] = historico.IdUsuarioResp.NomeCompleto;
				row["NumHistorico"] = alHistorico.Count.ToString();
				ds.Tables[0].Rows.Add(row);
			}
			return ds;
		}

		private DataSet GetDadosBasicos()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");

			table.Columns.Add("NomeCliente", Type.GetType("System.String"));
			table.Columns.Add("AtivoFixo", Type.GetType("System.String"));
			table.Columns.Add("DataFabricacao", Type.GetType("System.String"));
            table.Columns.Add("VencimentoRecarga", Type.GetType("System.String"));
            table.Columns.Add("Garantia", Type.GetType("System.String"));
			table.Columns.Add("TipoExtintor", Type.GetType("System.String"));
			table.Columns.Add("CondicaoExtintor", Type.GetType("System.String"));
			table.Columns.Add("PesoCheio", Type.GetType("System.String"));
			table.Columns.Add("PesoVazio", Type.GetType("System.String"));
			table.Columns.Add("Fabricante", Type.GetType("System.String"));
			table.Columns.Add("Setor", Type.GetType("System.String"));
			table.Columns.Add("Localizacao", Type.GetType("System.String"));
			table.Columns.Add("Observacoes", Type.GetType("System.String"));
			ds.Tables.Add(table);

			DataRow row = ds.Tables[0].NewRow();

			row["NomeCliente"] = extintor.IdCliente.NomeAbreviado;
			row["AtivoFixo"] = extintor.AtivoFixo;
			row["DataFabricacao"] = extintor.DataFabricacao.ToString("dd-MM-yyyy");
            row["VencimentoRecarga"] = extintor.VencimentoRecarga.ToString("dd-MM-yyyy");
            row["Garantia"] = extintor.Garantia.ToString();
			row["TipoExtintor"] = extintor.IdTipoExtintor.ModeloExtintor;
			switch (extintor.IndCondicao)
			{
				case (int)IndCondicaoExtintor.Alocado:
				{
					row["CondicaoExtintor"] = "Alocado";
					break;
				}
				case (int)IndCondicaoExtintor.ForaDeUso:
				{
					row["CondicaoExtintor"] = "Fora de Uso";
					break;
				}
				case (int)IndCondicaoExtintor.Reserva:
				{
					row["CondicaoExtintor"] = "Reserva";
					break;
				}
			}
			row["PesoCheio"] = extintor.PesoCheio.ToString();
			row["PesoVazio"] = extintor.PesoVazio.ToString();
			row["Fabricante"] = extintor.IdFabricanteExtintor.NomeCompleto;
			row["Setor"] = extintor.IdSetor.tNO_STR_EMPR;
			row["Localizacao"] = extintor.Localizacao;
			row["Observacoes"] = extintor.Observacao;

			ds.Tables[0].Rows.Add(row);

			return ds;
		}
	}
}
