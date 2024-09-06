using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

using Ilitera.Opsa.Data;

namespace Ilitera.VasoCaldeira.Report
{
    public class DataSourceVasoInspecao : Ilitera.Common.DataSourceBase
    {
        private InspecaoVasoCaldeira inspecaoVaso;

        public DataSourceVasoInspecao(InspecaoVasoCaldeira inspecaoVaso)
        {
            this.inspecaoVaso = new InspecaoVasoCaldeira();
            this.inspecaoVaso.Find(inspecaoVaso.Id);

            if (this.inspecaoVaso.IdVasoCaldeiraBase.mirrorOld == null)
                this.inspecaoVaso.IdVasoCaldeiraBase.Find();
        }

        public DataSourceVasoInspecao( int xInspecaoVaso)
        {
            this.inspecaoVaso = new InspecaoVasoCaldeira();
            this.inspecaoVaso.Find( xInspecaoVaso);

            if (this.inspecaoVaso.IdVasoCaldeiraBase.mirrorOld == null)
                this.inspecaoVaso.IdVasoCaldeiraBase.Find();
        }

        public RptVasoInspecao GetReport()
        {
            RptVasoInspecao report = new RptVasoInspecao();
            report.SetDataSource(GetDataSource());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

        public RptVasoInspecao_Novo GetReport_Novo()
        {
            RptVasoInspecao_Novo report = new RptVasoInspecao_Novo();
            report.SetDataSource(GetDataSource_Novo());
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
            vasoPressao.Find(inspecaoVaso.IdVasoCaldeiraBase.Id);

            newRow["CarimboCNPJ"] = inspecaoVaso.IdCliente.GetCarimboCnpjHtml(inspecaoVaso.DataLevantamento);
            newRow["DataLaudo"] = GetDateTimeToString(inspecaoVaso.DataLevantamento);
            newRow["Fabricante"] = inspecaoVaso.IdVasoCaldeiraBase.IdFabricanteVasoCaldeira.ToString();
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
            newRow["FluidoServico"] = vasoPressao.IdFluidoServico.ToString();
            newRow["GrupoPotencialRisco"] = "Grupo " + vasoPressao.IdGrupoRisco.Id.ToString();
            newRow["TipoCaldeiraVaso"] = vasoPressao.IdTipoVasoCaldeira.ToString();
            newRow["TipoInspecaoExecutada"] = inspecaoVaso.GetTipoInspecao();
            newRow["ExameExterno"] = inspecaoVaso.IndExameExterno;
            newRow["ExameInterno"] = inspecaoVaso.IndExameInterno;
            newRow["RelacaoPV"] = vasoPressao.RelacaoPV.ToString("n");
            newRow["Categoria"] = vasoPressao.IdCategoriaVasoPressao.ToString();
            newRow["DescricaoExame"] = inspecaoVaso.DescricaoExames;
            newRow["Resultado"] = inspecaoVaso.Resultado;
            newRow["ProximoExameExterno"] = inspecaoVaso.ProximoExameExterno;
            newRow["ProximoExameInterno"] = inspecaoVaso.ProximoExameInterno;
            newRow["ProximoTesteHidrostatico"] = inspecaoVaso.ProximoTesteHidrostatico;

            newRow["Recomendacao"] = inspecaoVaso.Recomendacao;
            newRow["Conclusao"] = inspecaoVaso.Conclusao;

            string xArquivo = vasoPressao.GetFoto();

            if (xArquivo.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
            {
                xArquivo = xArquivo.Substring(0, xArquivo.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + xArquivo.Substring(xArquivo.IndexOf("\\") + 1);
            }

            newRow["Foto"] = Ilitera.Common.Fotos.PathFoto_Uri(xArquivo);
            //newRow["Foto"] = Ilitera.Common.Fotos.PathFoto(vasoPressao.GetFoto());

            newRow["DataLocal"] = inspecaoVaso.IdCliente.GetCidade(inspecaoVaso.DataLevantamento) + ", " + inspecaoVaso.DataLevantamento.ToString("dd \"de\" MMMM \"de\" yyyy");

            DataSourceCaldeiraProjeto.PopularAutoria(newRow, inspecaoVaso.IdAutoria);

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
            table.Columns.Add("TipoCaldeiraVaso", Type.GetType("System.String"));
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
            table.Columns.Add("Recomendacao", Type.GetType("System.String"));
            table.Columns.Add("Conclusao", Type.GetType("System.String"));
            return table;
        }


        private DataSet GetDataSource_Novo()
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(GetTable_Novo());

            DataRow newRow = ds.Tables[0].NewRow();

            VasoPressao vasoPressao = new VasoPressao();
            vasoPressao.Find(inspecaoVaso.IdVasoCaldeiraBase.Id);

            newRow["CarimboCNPJ"] = inspecaoVaso.IdCliente.GetCarimboCnpjHtml(inspecaoVaso.DataLevantamento);
            newRow["DataLaudo"] = GetDateTimeToString(inspecaoVaso.DataLevantamento);
            newRow["Fabricante"] = inspecaoVaso.IdVasoCaldeiraBase.IdFabricanteVasoCaldeira.ToString();
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
            newRow["FluidoServico"] = vasoPressao.IdFluidoServico.ToString();
            newRow["GrupoPotencialRisco"] = "Grupo " + vasoPressao.IdGrupoRisco.Id.ToString();
            newRow["TipoCaldeiraVaso"] = vasoPressao.IdTipoVasoCaldeira.ToString();
            newRow["TipoInspecaoExecutada"] = inspecaoVaso.GetTipoInspecao();
            newRow["ExameExterno"] = inspecaoVaso.IndExameExterno;
            newRow["ExameInterno"] = inspecaoVaso.IndExameInterno;
            newRow["RelacaoPV"] = vasoPressao.RelacaoPV.ToString("n");
            newRow["Categoria"] = vasoPressao.IdCategoriaVasoPressao.ToString();
            newRow["DescricaoExame"] = inspecaoVaso.DescricaoExames;
            newRow["Resultado"] = inspecaoVaso.Resultado;
            newRow["ProximoExameExterno"] = inspecaoVaso.ProximoExameExterno.ToString().Substring(3, 7);
            newRow["ProximoExameInterno"] = inspecaoVaso.ProximoExameInterno.ToString().Substring(3, 7);
            newRow["ProximoTesteHidrostatico"] = inspecaoVaso.ProximoTesteHidrostatico.ToString().Substring(3, 7);
            //newRow["Foto"] = Ilitera.Common.Fotos.PathFoto(vasoPressao.GetFoto());
            newRow["DataLocal"] = inspecaoVaso.IdCliente.GetCidade(inspecaoVaso.DataLevantamento) + ", " + inspecaoVaso.DataLevantamento.ToString("dd \"de\" MMMM \"de\" yyyy");


            string xArquivo = vasoPressao.GetFoto();

            if (xArquivo.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
            {
                xArquivo = xArquivo.Substring(0, xArquivo.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + xArquivo.Substring(xArquivo.IndexOf("\\") + 1);
            }

            newRow["Foto"] = Ilitera.Common.Fotos.PathFoto_Uri(xArquivo);

            if (inspecaoVaso.Inspecao_Visual == true)
                newRow["Inspecao_Visual"] = "1";
            else
                newRow["Inspecao_Visual"] = "0";

            if (inspecaoVaso.Teste_Hidrostatico == true)
                newRow["Teste_Hidrostatico"] = "1";
            else
                newRow["Teste_Hidrostatico"] = "0";

            if (inspecaoVaso.Liquido_Penetante == true)
                newRow["Liquido_Penetante"] = "1";
            else
                newRow["Liquido_Penetante"] = "0";

            if (inspecaoVaso.Particulas_Magnetica_Fluorescente == true)
                newRow["Particulas_Magnetica_Fluorescente"] = "1";
            else
                newRow["Particulas_Magnetica_Fluorescente"] = "0";

            if (inspecaoVaso.UltraSom_Medicao_Espessura == true)
                newRow["UltraSom_Medicao_Espessura"] = "1";
            else
                newRow["UltraSom_Medicao_Espessura"] = "0";

            if (inspecaoVaso.UltraSom_Integridade_Solda == true)
                newRow["UltraSom_Integridade_Solda"] = "1";
            else
                newRow["UltraSom_Integridade_Solda"] = "0";

            if (inspecaoVaso.Analise_Metaografico_Replica == true)
                newRow["Analise_Metaografico_Replica"] = "1";
            else
                newRow["Analise_Metaografico_Replica"] = "0";

            if (inspecaoVaso.Ensaio_Mecanico_Amostra == true)
                newRow["Ensaio_Mecanico_Amostra"] = "1";
            else
                newRow["Ensaio_Mecanico_Amostra"] = "0";

            if (inspecaoVaso.Endoscopia_Industrial == true)
                newRow["Endoscopia_Industrial"] = "1";
            else
                newRow["Endoscopia_Industrial"] = "0";

            if (inspecaoVaso.Correntes_Parasitas == true)
                newRow["Correntes_Parasitas"] = "1";
            else
                newRow["Correntes_Parasitas"] = "0";

            if (inspecaoVaso.Ensaio_Iris == true)
                newRow["Ensaio_Iris"] = "1";
            else
                newRow["Ensaio_Iris"] = "0";

            if (inspecaoVaso.Emissao_Acustica == true)
                newRow["Emissao_Acustica"] = "1";
            else
                newRow["Emissao_Acustica"] = "0";

            newRow["Equipamentos_Utilizados"] = "Equipamentos Utilizados: " + inspecaoVaso.Equipamentos_Utilizados;


            if (inspecaoVaso.Manometro == true)
                newRow["Manometro"] = "1";
            else
                newRow["Manometro"] = "0";

            if (inspecaoVaso.Valvula_Pressostatica == true)
                newRow["Valvula_Pressostatica"] = "1";
            else
                newRow["Valvula_Pressostatica"] = "0";

            if (inspecaoVaso.Valvula_Seguranca == true)
                newRow["Valvula_Seguranca"] = "1";
            else
                newRow["Valvula_Seguranca"] = "0";

            if (inspecaoVaso.Controle_Nivel_Agua == true)
                newRow["Controle_Nivel_Agua"] = "1";
            else
                newRow["Controle_Nivel_Agua"] = "0";

            newRow["Especificacao"] = "Especificação: " + inspecaoVaso.Especificacao;

            newRow["Pressao_Teste"] = inspecaoVaso.Pressao_Teste;
            newRow["Fluido_Utilizado"] = inspecaoVaso.Fluido_Utilizado;
            newRow["Temperatura_Fluido"] = inspecaoVaso.Temperatura_Fluido;
            newRow["Duracao_Teste_Minutos"] = inspecaoVaso.Duracao_Teste_Minutos;

            newRow["G0_Tampo_Esquerdo_1"] = inspecaoVaso.G0_Tampo_Esquerdo_1;
            newRow["G0_Costado_2"] = inspecaoVaso.G0_Costado_2;
            newRow["G0_Costado_3"] = inspecaoVaso.G0_Costado_3;
            newRow["G0_Costado_4"] = inspecaoVaso.G0_Costado_4;
            newRow["G0_Tampo_Direito"] = inspecaoVaso.G0_Tampo_Direito;
            newRow["G90_Tampo_Esquerdo_1"] = inspecaoVaso.G90_Tampo_Esquerdo_1;
            newRow["G90_Costado_2"] = inspecaoVaso.G90_Costado_2;
            newRow["G90_Costado_3"] = inspecaoVaso.G90_Costado_3;
            newRow["G90_Costado_4"] = inspecaoVaso.G90_Costado_4;
            newRow["G90_Tampo_Direito"] = inspecaoVaso.G90_Tampo_Direito;
            newRow["G180_Tampo_Esquerdo_1"] = inspecaoVaso.G180_Tampo_Esquerdo_1;
            newRow["G180_Costado_2"] = inspecaoVaso.G180_Costado_2;
            newRow["G180_Costado_3"] = inspecaoVaso.G180_Costado_3;
            newRow["G180_Costado_4"] = inspecaoVaso.G180_Costado_4;
            newRow["G180_Tampo_Direito"] = inspecaoVaso.G180_Tampo_Direito;
            newRow["G270_Tampo_Esquerdo_1"] = inspecaoVaso.G270_Tampo_Esquerdo_1;
            newRow["G270_Costado_2"] = inspecaoVaso.G270_Costado_2;
            newRow["G270_Costado_3"] = inspecaoVaso.G270_Costado_3;
            newRow["G270_Costado_4"] = inspecaoVaso.G270_Costado_4;
            newRow["G270_Tampo_Direito"] = inspecaoVaso.G270_Tampo_Direito;

            newRow["Material_Adotado_Espessura_Vaso"] = inspecaoVaso.Material_Adotado_Espessura_Vaso;
            newRow["G0_Espessura"] = inspecaoVaso.G0_Espessura;
            newRow["G90_Espessura"] = inspecaoVaso.G90_Espessura;
            newRow["G180_Espessura"] = inspecaoVaso.G180_Espessura;
            newRow["G270_Espessura"] = inspecaoVaso.G270_Espessura;
            newRow["Material_Adotado_Espessura_Tubulacao"] = inspecaoVaso.Material_Adotado_Espessura_Tubulacao;
            newRow["Tensao_Maxima_Admissivel"] = inspecaoVaso.Tensao_Maxima_Admissivel;
            newRow["Eficiencia_Junta_Solda"] = inspecaoVaso.Eficiencia_Junta_Solda;
            newRow["Espessura_Minima_Encontrada"] = inspecaoVaso.Espessura_Minima_Encontrada;
            newRow["Raio_Interno_Corpo"] = inspecaoVaso.Raio_Interno_Corpo;
            newRow["PMTA2"] = inspecaoVaso.PMTA;

            newRow["Recomendacao"] = inspecaoVaso.Recomendacao;
            newRow["Conclusao"] = inspecaoVaso.Conclusao;


            newRow["Producao_Vapor"] = vasoPressao.Producao_Vapor;
            newRow["Material"] = vasoPressao.Material;
            newRow["Modelo"] = vasoPressao.Modelo;
            newRow["Superficie_Aquecimento"] = vasoPressao.Superficie_Aquecimento;


            DataSourceCaldeiraProjeto.PopularAutoria(newRow, inspecaoVaso.IdAutoria);

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private static DataTable GetTable_Novo()
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
            table.Columns.Add("TipoCaldeiraVaso", Type.GetType("System.String"));
            table.Columns.Add("TipoInspecaoExecutada", Type.GetType("System.String"));
            table.Columns.Add("ExameExterno", Type.GetType("System.Int32"));
            table.Columns.Add("ExameInterno", Type.GetType("System.Int32"));
            table.Columns.Add("RelacaoPV", Type.GetType("System.String"));
            table.Columns.Add("Categoria", Type.GetType("System.String"));
            table.Columns.Add("DescricaoExame", Type.GetType("System.String"));
            table.Columns.Add("Resultado", Type.GetType("System.String"));
            table.Columns.Add("ProximoExameExterno", Type.GetType("System.String"));
            table.Columns.Add("ProximoExameInterno", Type.GetType("System.String"));
            table.Columns.Add("ProximoTesteHidrostatico", Type.GetType("System.String"));
            table.Columns.Add("Foto", Type.GetType("System.String"));
            table.Columns.Add("DataLocal", Type.GetType("System.String"));
            table.Columns.Add("AutoriaNome", Type.GetType("System.String"));
            table.Columns.Add("AutoriaTitulo", Type.GetType("System.String"));
            table.Columns.Add("AutoriaNumero", Type.GetType("System.String"));
            table.Columns.Add("AutoriaAss", Type.GetType("System.String"));
            table.Columns.Add("AutoriaDocumento", Type.GetType("System.String"));
            table.Columns.Add("Inspecao_Visual", Type.GetType("System.String"));
            table.Columns.Add("Teste_Hidrostatico", Type.GetType("System.String"));
            table.Columns.Add("Liquido_Penetante", Type.GetType("System.String"));
            table.Columns.Add("Particulas_Magnetica_Fluorescente", Type.GetType("System.String"));
            table.Columns.Add("UltraSom_Medicao_Espessura", Type.GetType("System.String"));
            table.Columns.Add("UltraSom_Integridade_Solda", Type.GetType("System.String"));
            table.Columns.Add("Analise_Metaografico_Replica", Type.GetType("System.String"));
            table.Columns.Add("Ensaio_Mecanico_Amostra", Type.GetType("System.String"));
            table.Columns.Add("Endoscopia_Industrial", Type.GetType("System.String"));
            table.Columns.Add("Correntes_Parasitas", Type.GetType("System.String"));
            table.Columns.Add("Ensaio_Iris", Type.GetType("System.String"));
            table.Columns.Add("Emissao_Acustica", Type.GetType("System.String"));
            table.Columns.Add("Equipamentos_Utilizados", Type.GetType("System.String"));
            table.Columns.Add("Manometro", Type.GetType("System.String"));
            table.Columns.Add("Valvula_Pressostatica", Type.GetType("System.String"));
            table.Columns.Add("Valvula_Seguranca", Type.GetType("System.String"));
            table.Columns.Add("Controle_Nivel_Agua", Type.GetType("System.String"));
            table.Columns.Add("Especificacao", Type.GetType("System.String"));
            table.Columns.Add("Pressao_Teste", Type.GetType("System.Double"));
            table.Columns.Add("Fluido_Utilizado", Type.GetType("System.String"));
            table.Columns.Add("Temperatura_Fluido", Type.GetType("System.String"));
            table.Columns.Add("Duracao_Teste_Minutos", Type.GetType("System.Int16"));
            table.Columns.Add("G0_Tampo_Esquerdo_1", Type.GetType("System.Double"));
            table.Columns.Add("G0_Costado_2", Type.GetType("System.Double"));
            table.Columns.Add("G0_Costado_3", Type.GetType("System.Double"));
            table.Columns.Add("G0_Costado_4", Type.GetType("System.Double"));
            table.Columns.Add("G0_Tampo_Direito", Type.GetType("System.Double"));
            table.Columns.Add("G90_Tampo_Esquerdo_1", Type.GetType("System.Double"));
            table.Columns.Add("G90_Costado_2", Type.GetType("System.Double"));
            table.Columns.Add("G90_Costado_3", Type.GetType("System.Double"));
            table.Columns.Add("G90_Costado_4", Type.GetType("System.Double"));
            table.Columns.Add("G90_Tampo_Direito", Type.GetType("System.Double"));
            table.Columns.Add("G180_Tampo_Esquerdo_1", Type.GetType("System.Double"));
            table.Columns.Add("G180_Costado_2", Type.GetType("System.Double"));
            table.Columns.Add("G180_Costado_3", Type.GetType("System.Double"));
            table.Columns.Add("G180_Costado_4", Type.GetType("System.Double"));
            table.Columns.Add("G180_Tampo_Direito", Type.GetType("System.Double"));
            table.Columns.Add("G270_Tampo_Esquerdo_1", Type.GetType("System.Double"));
            table.Columns.Add("G270_Costado_2", Type.GetType("System.Double"));
            table.Columns.Add("G270_Costado_3", Type.GetType("System.Double"));
            table.Columns.Add("G270_Costado_4", Type.GetType("System.Double"));
            table.Columns.Add("G270_Tampo_Direito", Type.GetType("System.Double"));
            table.Columns.Add("Material_Adotado_Espessura_Vaso", Type.GetType("System.String"));
            table.Columns.Add("G0_Espessura", Type.GetType("System.Double"));
            table.Columns.Add("G90_Espessura", Type.GetType("System.Double"));
            table.Columns.Add("G180_Espessura", Type.GetType("System.Double"));
            table.Columns.Add("G270_Espessura", Type.GetType("System.Double"));
            table.Columns.Add("Material_Adotado_Espessura_Tubulacao", Type.GetType("System.String"));
            table.Columns.Add("Tensao_Maxima_Admissivel", Type.GetType("System.Double"));
            table.Columns.Add("Eficiencia_Junta_Solda", Type.GetType("System.Double"));
            table.Columns.Add("Espessura_Minima_Encontrada", Type.GetType("System.Double"));
            table.Columns.Add("Raio_Interno_Corpo", Type.GetType("System.Double"));
            table.Columns.Add("PMTA2", Type.GetType("System.Double"));
            table.Columns.Add("Recomendacao", Type.GetType("System.String"));
            table.Columns.Add("Conclusao", Type.GetType("System.String"));

            table.Columns.Add("Producao_Vapor", Type.GetType("System.String"));
            table.Columns.Add("Material", Type.GetType("System.String"));
            table.Columns.Add("Modelo", Type.GetType("System.String"));
            table.Columns.Add("Superficie_Aquecimento", Type.GetType("System.String"));

            return table;
        }

    }
}
