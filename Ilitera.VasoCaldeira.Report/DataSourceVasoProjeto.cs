using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.VasoCaldeira.Report
{
    public class DataSourceVasoProjeto : DataSourceBase
    {
        private ProjetoVasoCaldeira projetoVaso;

        public DataSourceVasoProjeto(ProjetoVasoCaldeira projetoVaso)
        {
            this.projetoVaso = new ProjetoVasoCaldeira();
            this.projetoVaso.Find(projetoVaso.Id);
        }

        public DataSourceVasoProjeto(int xprojetoVaso)
        {
            this.projetoVaso = new ProjetoVasoCaldeira();
            this.projetoVaso.Find(xprojetoVaso);
        }

        public RptVasoProjeto GetReport()
        {
            RptVasoProjeto report = new RptVasoProjeto();
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

            VasoPressao vasoPressao = new VasoPressao();
            vasoPressao.Find(projetoVaso.IdVasoCaldeiraBase.Id);

            if (projetoVaso.IdVasoCaldeiraBase.mirrorOld == null)
                projetoVaso.IdVasoCaldeiraBase.Find();
            
            newRow["CarimboCNPJ"] = projetoVaso.IdCliente.GetCarimboCnpjHtml(projetoVaso.DataLevantamento);
            newRow["DataLaudo"] = GetDateTimeToString(projetoVaso.DataLevantamento);
            newRow["Fabricante"] = projetoVaso.IdVasoCaldeiraBase.IdFabricanteVasoCaldeira.ToString();
            newRow["Local"] = vasoPressao.Localizacao;
            newRow["NumeroIdentificacao"] = vasoPressao.NumeroIdentificacao;
            newRow["AnoFabricacao"] = vasoPressao.AnoFabricacao;
            newRow["PMTA"] = VasoCaldeiraBase.GetUnidadeConvertida(vasoPressao.PressaoMaximaTrabalho, vasoPressao.IdUnidadePressao);
            newRow["PressaoOperacao"] = VasoCaldeiraBase.GetUnidadeConvertida(vasoPressao.GetPressaoOperacao(), vasoPressao.IdUnidadePressao);
            newRow["PressaoTesteHidrostatico"] = VasoCaldeiraBase.GetUnidadeConvertida(vasoPressao.PressaoTesteHidrostatico, vasoPressao.IdUnidadePressao);
            newRow["CodigoProjeto"] = vasoPressao.CodigoProjeto;
            newRow["AnoEdicao"] = vasoPressao.AnoEdicao == 0 ? "Desconhecido" : vasoPressao.AnoEdicao.ToString();
            newRow["VolumeInterno"] = vasoPressao.VolumeInterno.ToString("##0.###") + " m³";
            newRow["PMTA_MPA"] = vasoPressao.GetPMTA_MPa().ToString("n") + " Mpa";
            newRow["ClasseFluido"] = "Classe " + vasoPressao.IdClasseDeFluido.ToString();
            newRow["CapacidadeProdVapor"] = string.Empty;
            newRow["AreaSupAquecimento"] = string.Empty;
            newRow["TipoCaldeiraVaso"] = string.Empty;
            newRow["RelacaoPV"] = vasoPressao.RelacaoPV.ToString("n");
            newRow["Categoria"] = vasoPressao.IdCategoriaVasoPressao.ToString();
            newRow["TipoValvulaSeguranca"] = vasoPressao.IdValvulaSeguranca.ToString();
            newRow["PressaoAbertura"] = vasoPressao.PressaoAbertura == 0 ? "-" : vasoPressao.PressaoAbertura.ToString("g") + " " + vasoPressao.IdUnidadePressao.ToString();
            newRow["MarcaTipoManometro"] = vasoPressao.IdMarcaTipoManometro.ToString();
            newRow["Escala"] = vasoPressao.Escala == 0 ? "-" : vasoPressao.Escala.ToString("g") + " " + vasoPressao.IdUnidadePressao.ToString();

            string xArquivo;

            xArquivo = vasoPressao.GetFoto();

            if (xArquivo.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
            {
                xArquivo = xArquivo.Substring(0, xArquivo.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + xArquivo.Substring(xArquivo.IndexOf("\\") + 1);
            }
            
            newRow["Foto"] = Ilitera.Common.Fotos.PathFoto_Uri(xArquivo);

            xArquivo = projetoVaso.GetPlantaBaixa();

            if (xArquivo.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
            {
                xArquivo = xArquivo.Substring(0, xArquivo.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + xArquivo.Substring(xArquivo.IndexOf("\\") + 1);
            }

            newRow["PlantaBaixa"] = Ilitera.Common.Fotos.PathFoto_Uri(xArquivo);

            xArquivo = projetoVaso.GetSugestao();

            if (xArquivo.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
            {
                xArquivo = xArquivo.Substring(0, xArquivo.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + xArquivo.Substring(xArquivo.IndexOf("\\") + 1);
            }

            newRow["Sugestao"] = Ilitera.Common.Fotos.PathFoto_Uri(xArquivo);

            newRow["DataLocal"] = projetoVaso.IdCliente.GetCidade(projetoVaso.DataLevantamento) + ", " + projetoVaso.DataLevantamento.ToString("dd \"de\" MMMM \"de\" yyyy");
            newRow["TipoVaso"] = (int)vasoPressao.IndVasoTipo;

            DataSourceCaldeiraProjeto.PopularAutoria(newRow, projetoVaso.IdAutoria);

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
            table.Columns.Add("CapacidadeProdVapor", Type.GetType("System.String"));
            table.Columns.Add("AreaSupAquecimento", Type.GetType("System.String"));
            table.Columns.Add("TipoCaldeiraVaso", Type.GetType("System.String"));
            table.Columns.Add("RelacaoPV", Type.GetType("System.String"));
            table.Columns.Add("Categoria", Type.GetType("System.String"));
            table.Columns.Add("TipoValvulaSeguranca", Type.GetType("System.String"));
            table.Columns.Add("PressaoAbertura", Type.GetType("System.String"));
            table.Columns.Add("MarcaTipoManometro", Type.GetType("System.String"));
            table.Columns.Add("Escala", Type.GetType("System.String"));
            table.Columns.Add("Foto", Type.GetType("System.String"));
            table.Columns.Add("PlantaBaixa", Type.GetType("System.String"));
            table.Columns.Add("DataLocal", Type.GetType("System.String"));
            table.Columns.Add("AutoriaNome", Type.GetType("System.String"));
            table.Columns.Add("AutoriaTitulo", Type.GetType("System.String"));
            table.Columns.Add("AutoriaNumero", Type.GetType("System.String"));
            table.Columns.Add("AutoriaAss", Type.GetType("System.String"));
            table.Columns.Add("AutoriaDocumento", Type.GetType("System.String"));
            table.Columns.Add("TipoVaso", Type.GetType("System.Int32"));
            table.Columns.Add("Sugestao", Type.GetType("System.String"));
            return table;
        }
    }
}
