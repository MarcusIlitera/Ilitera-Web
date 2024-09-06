using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

using Ilitera.Opsa.Data;

namespace Ilitera.VasoCaldeira.Report
{
    public class DataSourceCaldeiraInspecao : Ilitera.Common.DataSourceBase
    {
        private InspecaoVasoCaldeira inspecaoCaldeira;

        public DataSourceCaldeiraInspecao(InspecaoVasoCaldeira inspecaoCaldeira)
        {
            if (inspecaoCaldeira.IdVasoCaldeiraBase.mirrorOld == null)
                inspecaoCaldeira.IdVasoCaldeiraBase.Find();

            this.inspecaoCaldeira = inspecaoCaldeira;
        }

        public RptCaldeiraInspecao GetReport()
        {
            RptCaldeiraInspecao report = new RptCaldeiraInspecao();
            report.SetDataSource(GetDataSource());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

        private DataSet GetDataSource()
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(GetTable());

            DataRow newRow = ds.Tables[0].NewRow();

            Caldeira caldeira = new Caldeira();
            caldeira.Find(inspecaoCaldeira.IdVasoCaldeiraBase.Id);

            newRow["CarimboCNPJ"] = inspecaoCaldeira.IdCliente.GetCarimboCnpjHtml(inspecaoCaldeira.DataLevantamento);
            newRow["DataLaudo"] = inspecaoCaldeira.DataLevantamento.ToString("dd-MM-yyyy");
            newRow["Fabricante"] = inspecaoCaldeira.IdVasoCaldeiraBase.IdFabricanteVasoCaldeira.ToString();
            newRow["Local"] = caldeira.Localizacao;
            newRow["NumeroIdentificacao"] = caldeira.NumeroIdentificacao;
            newRow["AnoFabricacao"] = caldeira.AnoFabricacao;
            newRow["PMTA"] = VasoCaldeiraBase.GetUnidadeConvertida(caldeira.PressaoMaximaTrabalho, caldeira.IdUnidadePressao);
            newRow["PressaoOperacao"] = VasoCaldeiraBase.GetUnidadeConvertida(caldeira.GetPressaoOperacao(), caldeira.IdUnidadePressao);
            newRow["PressaoTesteHidrostatico"] = VasoCaldeiraBase.GetUnidadeConvertida(caldeira.PressaoTesteHidrostatico, caldeira.IdUnidadePressao);
            newRow["CodigoProjeto"] = caldeira.CodigoProjeto;
            newRow["AnoEdicao"] = caldeira.AnoEdicao == 0 ? "Desconhecido" : caldeira.AnoEdicao.ToString();
            newRow["VolumeInterno"] = caldeira.VolumeInterno.ToString("##0.###") + " m³";
            newRow["PMTA_MPA"] = caldeira.GetPMTA_MPa().ToString("n") + " Mpa";
            //newRow["ClasseFluido"] = "Classe " + caldeira.IdClasseDeFluido.ToString();
            //newRow["FluidoServico"] = caldeira.IdFluidoServico.ToString();
            //newRow["GrupoPotencialRisco"] = "Grupo " + caldeira.IdGrupoRisco.Id.ToString();
            newRow["TipoVasoPressao"] = caldeira.IdTipoVasoCaldeira.ToString();
            newRow["TipoInspecaoExecutada"] = inspecaoCaldeira.GetTipoInspecao();
            newRow["ExameExterno"] = inspecaoCaldeira.IndExameExterno;
            newRow["ExameInterno"] = inspecaoCaldeira.IndExameInterno;
            newRow["RelacaoPV"] = caldeira.RelacaoPV.ToString("n");
            newRow["Categoria"] = caldeira.IndCategoriaCaldeira.ToString();
            newRow["DescricaoExame"] = inspecaoCaldeira.DescricaoExames;
            newRow["Resultado"] = inspecaoCaldeira.Resultado;
            newRow["ProximoExameExterno"] = inspecaoCaldeira.ProximoExameExterno;
            newRow["ProximoExameInterno"] = inspecaoCaldeira.ProximoExameInterno;
            newRow["ProximoTesteHidrostatico"] = inspecaoCaldeira.ProximoTesteHidrostatico;
            newRow["Foto"] = Ilitera.Common.Fotos.PathFoto(caldeira.GetFoto());
            newRow["DataLocal"] = inspecaoCaldeira.IdCliente.GetCidade(inspecaoCaldeira.DataLevantamento) + ", " + inspecaoCaldeira.DataLevantamento.ToString("dd \"de\" MMMM \"de\" yyyy");

            DataSourceCaldeiraProjeto.PopularAutoria(newRow, inspecaoCaldeira.IdAutoria);

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("CarimboCNPJ", Type.GetType("System.String"));
            table.Columns.Add("DataLaudo", Type.GetType("System.String"));
            table.Columns.Add("Fabricante", Type.GetType("System.String"));
            table.Columns.Add("Local", Type.GetType("System.String"));
            table.Columns.Add("NumeroIdentificacao", Type.GetType("System.String"));
            table.Columns.Add("AnoFabricacao", Type.GetType("System.String"));
            table.Columns.Add("PMTA", Type.GetType("System.String"));
            table.Columns.Add("PressaoOperacao", Type.GetType("System.String"));
            table.Columns.Add("PressaoTesteHidrostatico", Type.GetType("System.String"));
            table.Columns.Add("CodigoProjeto", Type.GetType("System.String"));
            table.Columns.Add("AnoEdicao", Type.GetType("System.String"));
            table.Columns.Add("VolumeInterno", Type.GetType("System.String"));
            table.Columns.Add("PMTA_MPA", Type.GetType("System.String"));
            table.Columns.Add("ClasseFluido", Type.GetType("System.String"));
            table.Columns.Add("FluidoServico", Type.GetType("System.String"));
            table.Columns.Add("GrupoPotencialRisco", Type.GetType("System.String"));
            table.Columns.Add("TipoVasoPressao", Type.GetType("System.String"));
            table.Columns.Add("TipoInspecaoExecutada", Type.GetType("System.String"));
            table.Columns.Add("ExameExterno", Type.GetType("System.Int32"));
            table.Columns.Add("ExameInterno", Type.GetType("System.Int32"));
            table.Columns.Add("RelacaoPV", Type.GetType("System.String"));
            table.Columns.Add("Categoria", Type.GetType("System.String"));
            table.Columns.Add("DescricaoExame", Type.GetType("System.String"));
            table.Columns.Add("Resultado", Type.GetType("System.String"));
            table.Columns.Add("ProximoExameExterno", Type.GetType("System.DateTime"));
            table.Columns.Add("ProximoExameInterno", Type.GetType("System.DateTime"));
            table.Columns.Add("ProximoTesteHidrostatico", Type.GetType("System.DateTime"));
            table.Columns.Add("Foto", Type.GetType("System.String"));
            table.Columns.Add("DataLocal", Type.GetType("System.String"));
            table.Columns.Add("AutoriaNome", Type.GetType("System.String"));
            table.Columns.Add("AutoriaTitulo", Type.GetType("System.String"));
            table.Columns.Add("AutoriaNumero", Type.GetType("System.String"));
            table.Columns.Add("AutoriaAss", Type.GetType("System.String"));
            table.Columns.Add("AutoriaDocumento", Type.GetType("System.String"));
            return table;
        }
    }
}
