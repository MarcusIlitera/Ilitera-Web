using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.IO;

using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.VasoCaldeira.Report
{
    public class DataSourceCaldeiraProjeto : Ilitera.Common.DataSourceBase
    {
        private ProjetoVasoCaldeira projetoCaldeira;

        public DataSourceCaldeiraProjeto(ProjetoVasoCaldeira projetoCaldeira)
        {
            this.projetoCaldeira = projetoCaldeira;
        }

        public RptCaldeiraProjeto GetReport()
        {
            RptCaldeiraProjeto report = new RptCaldeiraProjeto();
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
            caldeira.Find(projetoCaldeira.IdVasoCaldeiraBase.Id);

            if (projetoCaldeira.IdVasoCaldeiraBase.mirrorOld == null)
                projetoCaldeira.IdVasoCaldeiraBase.Find();
            
            newRow["CarimboCNPJ"] = projetoCaldeira.IdCliente.GetCarimboCnpjHtml(projetoCaldeira.DataLevantamento);
            newRow["DataLaudo"] = projetoCaldeira.DataLevantamento.ToString("dd-MM-yyyy");
            newRow["Fabricante"] = projetoCaldeira.IdVasoCaldeiraBase.IdFabricanteVasoCaldeira.ToString();
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
            newRow["CapacidadeProdVapor"] = caldeira.CapacidadeProducaoVapor + " Kg/h";
            newRow["AreaSupAquecimento"] = caldeira.AreaSuperficieAquecimento.ToString("##0.###") + " m²";
            newRow["TipoCaldeiraVaso"] = caldeira.IdTipoVasoCaldeira.ToString();
            newRow["RelacaoPV"] = caldeira.RelacaoPV.ToString("n");
            newRow["Categoria"] = caldeira.IndCategoriaCaldeira.ToString();
            newRow["TipoValvulaSeguranca"] = caldeira.IdValvulaSeguranca.ToString();
            newRow["PressaoAbertura"] = caldeira.PressaoAbertura == 0 ? "-" : caldeira.PressaoAbertura.ToString("g") + " " + caldeira.IdUnidadePressao.ToString();
            newRow["MarcaTipoManometro"] = caldeira.IdMarcaTipoManometro.ToString();
            newRow["Escala"] = caldeira.Escala == 0 ? "-" : caldeira.Escala.ToString("g") + " " + caldeira.IdUnidadePressao.ToString();
            newRow["Foto"] = Fotos.PathFoto(caldeira.GetFoto());
            newRow["PlantaBaixa"] = Fotos.PathFoto(projetoCaldeira.GetPlantaBaixa());
            //newRow["Sugestao"] = Fotos.PathFoto(projetoCaldeira.GetSugestao());
            newRow["DataLocal"] = projetoCaldeira.IdCliente.GetCidade(projetoCaldeira.DataLevantamento) + ", " + projetoCaldeira.DataLevantamento.ToString("dd \"de\" MMMM \"de\" yyyy");

            PopularAutoria(newRow, projetoCaldeira.IdAutoria);

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        public static void PopularAutoria(DataRow newRow, Prestador prestador)
        {
            if (prestador.mirrorOld == null)
                prestador.Find();

            newRow["AutoriaNome"] = prestador.NomeCompleto;
            newRow["AutoriaTitulo"] = prestador.Titulo;
            newRow["AutoriaNumero"] = prestador.Numero;

            string emptyPath = EnvironmentUtitity.FileAssinaturaEmBranco;

            //wagner
            if (Fotos.PathFoto_Uri(prestador.FotoAss) != "")
            {
                //FileInfo fileAss = new FileInfo(Fotos.PathFoto_Uri(prestador.FotoAss));

                newRow["AutoriaAss"] = Fotos.PathFoto_Uri(prestador.FotoAss);
                //if (fileAss.Exists)
                //    newRow["AutoriaAss"] = fileAss.FullName;
                //else
                //    newRow["AutoriaAss"] = fileEmpty.FullName;
                

            }

            if (Fotos.PathFoto_Uri(prestador.FotoRG) != "")
            {
                //FileInfo fileDoc = new FileInfo(Fotos.PathFoto_Uri(prestador.FotoRG));

                newRow["AutoriaDocumento"] = Fotos.PathFoto_Uri(prestador.FotoRG);
                //if (fileDoc.Exists)
                //    newRow["AutoriaDocumento"] = fileDoc.FullName;
                //else
                //    newRow["AutoriaDocumento"] = fileEmpty.FullName;


            }

            if (Fotos.PathFoto(emptyPath) != "")
            {
                FileInfo fileEmpty = new FileInfo(Fotos.PathFoto(emptyPath));
            }


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
            table.Columns.Add("Sugestao", Type.GetType("System.String"));
            return table;
        }
    }
}
