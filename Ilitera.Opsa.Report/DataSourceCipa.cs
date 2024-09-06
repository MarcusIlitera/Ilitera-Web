using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Data.SqlClient;

namespace Ilitera.Opsa.Report
{
    public class DataSourceCipa : DataSourceBase
    {
        private Cipa cipa;
        private EventoCipa eventoCipa;
        private FrasePadrao frasePadrao;
        private string responsavelMestraCipa = "";

        public DataSourceCipa(EventoCipa eventoCipa)
        {
            this.eventoCipa = eventoCipa;
            this.cipa = eventoCipa.IdCipa;

            if (this.cipa.mirrorOld == null)
                this.cipa.Find();

            if (this.cipa.IdCliente.mirrorOld == null)
                this.cipa.IdCliente.Find();

            this.frasePadrao = new FrasePadrao();
            this.frasePadrao.Find("IdEventoBaseCipa=" + eventoCipa.IdEventoBaseCipa.Id);
        }

        private EleicaoCipa eleicaoCipa;
        private Endereco endereco;

        #region Reports

        #region GetReport

        public CrystalDecisions.CrystalReports.Engine.ReportClass GetReport()
        {
            CrystalDecisions.CrystalReports.Engine.ReportClass report = null;

            switch (eventoCipa.IdEventoBaseCipa.Id)
            {
                case (int)EventoBase.ComunicacaoSindicato:
                    report = new RptSindicato();
                    report.SetDataSource(DataSourceRptSindicato());
                    report.Refresh();
                    break;

                case (int)EventoBase.Edital:
                    report = new RptEdital();
                    report.SetDataSource(DataSourceEdital());
                    report.OpenSubreport("ComissaoEleitoral").SetDataSource(DataSourceRptComissaoEleitoral());
                    report.Refresh();
                    break;

                case (int)EventoBase.Publicacao:
                    report = new RptPublicacao();
                    report.SetDataSource(DataSourceRptPublicacao());
                    report.Refresh();
                    break;

                case (int)EventoBase.InicioInscricao:
                    report = new RptInicioInscricoes();
                    report.SetDataSource(DataSourceRptInicioInscricao());
                    report.Refresh();
                    break;

                case (int)EventoBase.TerminoInscricao:
                    report = new RptTerminoInscricao();
                    report.SetDataSource(DataSourceRptTerminoInscricao());
                    report.Refresh();
                    break;

                case (int)EventoBase.Posse:
                    report = new RptAtaDaPosse_New();
                    report.SetDataSource(DataSourceRptAtaDaPosse());
                    report.OpenSubreport("Representantes").SetDataSource(DataSourceRptRepresentantes());
                    report.OpenSubreport("AssinaturaRepresentantes").SetDataSource(DataSourceRptRepresentantes());
                    report.OpenSubreport("ComissaoEleitoral").SetDataSource(DataSourceRptComissaoEleitoral());
                    report.Refresh();
                    break;

                case (int)EventoBase.Calendario:
                    report = new RptCalendario();
                    report.SetDataSource(DataSourceRptCalendario());
                    report.OpenSubreport("Reunioes").SetDataSource(DataSourceRptReunioes());
                    report.Refresh();
                    break;

                case (int)EventoBase.RegistroDRT:
                    report = new RptRegistroDRT();
                    report.SetDataSource(DataSourceRptRegistroDRT());
                    report.Refresh();
                    break;
            }
            return report;
        }
        #endregion

        #region GerReportFolhaDeApuracao

        public RptFolhaDeApuracao_New GetReportFolhaDeApuracao(List<CandidatosApurados> listaDeCandidatos)
        {
            RptFolhaDeApuracao_New report = new RptFolhaDeApuracao_New();
            report.Database.Tables["Result"].SetDataSource(DataSourceFolhaApuracaoCompleto(listaDeCandidatos));
            report.OpenSubreport("Data").SetDataSource(DataEleicaoFolhaApuracaoCompleto());
            report.OpenSubreport("DadosEmpresa").SetDataSource(DadosEmpresaFolhaApuracaoCompleto());
            //report.Database.Tables["DataEleicao"].SetDataSource(DataEleicaoFolhaApuracaoCompleto());
            //report.Database.Tables["DadosEmpresa"].SetDataSource();
            //report.SetDataSource();
            //report.OpenSubreport("EmpregadorApuracao").SetDataSource(DataSourceRptEmpregadorApuracao());
            //report.OpenSubreport("SubRptDemaisVotadosNovo").SetDataSource(DataSourceRptDemaisVotadosNovo(listaDeCandidatos));
            //
            report.Refresh();
            return report;
        }
        #endregion

        #region GetReportCedulaDeVotacao
        public RptCedulaDeVotacao GetReportCedulaDeVotacao(int numCedulas, bool ComNumeroRegistro)
        {
            RptCedulaDeVotacao report = new RptCedulaDeVotacao();
            report.SetDataSource(DataSourceRptCedulaDeVotacao(numCedulas, ComNumeroRegistro));
            report.Refresh();
            return report;

        }
        #endregion

        #region GerReportRegistroDRT

        public RptRegistroDRT GerReportRegistroDRT()
        {
            RptRegistroDRT report = new RptRegistroDRT();
            report.SetDataSource(DataSourceRptRegistroDRT());
            report.Refresh();
            return report;
        }
        #endregion

        #region GetReportCalendario
        public RptCalendario GetReportCalendario()
        {
            RptCalendario report = new RptCalendario();
            report.SetDataSource(DataSourceRptCalendario());
            report.OpenSubreport("Reunioes").SetDataSource(DataSourceRptReunioes());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }
        #endregion

        #region GetReportElaboracaoDeAta

        public RptElaboracaoDeAta GetReportElaboracaoDeAta()
        {
            RptElaboracaoDeAta report = new RptElaboracaoDeAta();
            report.SetDataSource(DataSourceRptElaboracaoDeAta());
            report.Refresh();

            return report;
        }
        #endregion

        #region GetReportComprovanteInscricao
        public RptComprovanteDeInscricao GetReportComprovanteInscricao()
        {
            RptComprovanteDeInscricao report = new RptComprovanteDeInscricao();
            report.SetDataSource(DataSourceRptComprovanteDeInscricaoDadosEmpregados());
            //report.OpenSubreport("DadosDaEmpresa").SetDataSource(DataSourceRptComprovanteDeInscricaoDadosDaEmpresa());
            report.OpenSubreport("ComissaoEleitoral").SetDataSource(DataSourceRptComprovanteDeInscricaoComissaoEleitoral());
            report.Refresh();
            return report;
        }
        #endregion

        #region GetReportInscricaoDeCandidatos

        public RptInscricaoDeCandidatos GetReportInscricaoDeCandidatos()
        {
            RptInscricaoDeCandidatos report = new RptInscricaoDeCandidatos();
            report.SetDataSource(DataSourceRptInscricaoDosCandidatos());
            report.Refresh();
            return report;
        }
        #endregion

        #endregion

        #region DataSource

        #region Sindicato

        private DataSet DataSourceRptSindicato()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTableSindicato());

            DataRow newRow = ds.Tables[0].NewRow();

            newRow["tDadosEmpresa"] = cipa.IdCliente.GetDadosEmpresaCipa(eventoCipa.DataSolicitacao);
            newRow["dDataEdital"] = cipa.Edital.ToString("dd-MM-yyyy");
            newRow["dDataComissaoEleitoral"] = cipa.ComissaoEleitoral.ToString("dd-MM-yyyy");
            newRow["dDataPublicaoEdital"] = cipa.Publicacao.ToString("dd-MM-yyyy");
            newRow["dDataInicioInscricao"] = cipa.InicioInscricao.ToString("dd-MM-yyyy");
            newRow["dDataTerminoInscricao"] = cipa.TerminoInscricao.ToString("dd-MM-yyyy");
            newRow["dDataEleicao"] = cipa.Eleicao.ToString("dd-MM-yyyy");
            newRow["dDataPosse"] = cipa.Posse.ToString("dd-MM-yyyy");
            newRow["tHORARIO"] = eventoCipa.GetHorario();
            newRow["tPeriodoInscricao"] = "de " + cipa.InicioInscricao.ToString("dd-MM-yyyy") + " a " + cipa.InicioInscricao.AddDays(15).ToString("dd-MM-yyyy");
            newRow["tLOCAL"] = eventoCipa.Local;
            newRow["tPrestador"] = responsavelMestraCipa;

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private static DataTable GetTableSindicato()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("tDadosEmpresa", Type.GetType("System.String"));
            table.Columns.Add("dDataEdital", Type.GetType("System.String"));
            table.Columns.Add("dDataComissaoEleitoral", Type.GetType("System.String"));
            table.Columns.Add("dDataPublicaoEdital", Type.GetType("System.String"));
            table.Columns.Add("dDataInicioInscricao", Type.GetType("System.String"));
            table.Columns.Add("dDataTerminoInscricao", Type.GetType("System.String"));
            table.Columns.Add("dDataEleicao", Type.GetType("System.String"));
            table.Columns.Add("dDataPosse", Type.GetType("System.String"));
            table.Columns.Add("tHORARIO", Type.GetType("System.String"));
            table.Columns.Add("tPeriodoInscricao", Type.GetType("System.String"));
            table.Columns.Add("tLOCAL", Type.GetType("System.String"));
            table.Columns.Add("tFraseInicial", Type.GetType("System.String"));
            table.Columns.Add("tFraseFinal", Type.GetType("System.String"));
            table.Columns.Add("tPrestador", Type.GetType("System.String"));
            return table;
        }
        #endregion

        #region Edital

        private DataSet DataSourceEdital()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTableEdital());

            DataRow newRow = ds.Tables[0].NewRow();

            newRow["tDadosEmpresa"] = cipa.IdCliente.GetDadosEmpresaCipa(cipa.Eleicao);            
            newRow["dDataDaEleicao"] = cipa.Eleicao.ToString("dd-MM-yyyy");
            newRow["tHORARIO"] = eventoCipa.GetHorario();
            newRow["tPeriodoInscricao"] = "de " + cipa.InicioInscricao.ToString("dd-MM-yyyy") + " a " + cipa.TerminoInscricao.ToString("dd-MM-yyyy");
            newRow["tLOCAL"] = eventoCipa.Local;
            newRow["tPublicacaoDivulgacao"] = cipa.Publicacao.ToString("dd-MM-yyyy");
            newRow["tDataEdital"] = cipa.IdCliente.GetEndereco().GetCidade() + ", " + cipa.Edital.ToString("d \"de\" MMMM \"de\" yyyy");
            newRow["tFraseInicial"] = frasePadrao.Abertura;
            newRow["tFraseFinal"] = frasePadrao.Encerramento;

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private static DataTable GetTableEdital()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("tDadosEmpresa", Type.GetType("System.String"));
            table.Columns.Add("dDataDaEleicao", Type.GetType("System.String"));
            table.Columns.Add("tHORARIO", Type.GetType("System.String"));
            table.Columns.Add("tPeriodoInscricao", Type.GetType("System.String"));
            table.Columns.Add("tLOCAL", Type.GetType("System.String"));
            table.Columns.Add("tPublicacaoDivulgacao", Type.GetType("System.String"));
            table.Columns.Add("tDataEdital", Type.GetType("System.String"));
            table.Columns.Add("tFraseInicial", Type.GetType("System.String"));
            table.Columns.Add("tFraseFinal", Type.GetType("System.String"));
            return table;
        }
        #endregion

        #region Publicacao

        private DataSet DataSourceRptPublicacao()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTablePublicacao());

            DataRow newRow = ds.Tables[0].NewRow();

            newRow["tDadosEmpresa"] = cipa.IdCliente.GetDadosEmpresaCipa(eventoCipa.DataSolicitacao);
            newRow["dDataDaPublicacao"] = cipa.Publicacao.ToString("dd-MM-yyyy");
            newRow["tHORARIO"] = eventoCipa.GetHorario();
            newRow["tPeriodoInscricao"] = "de " + cipa.InicioInscricao.ToString("dd-MM-yyyy") + " a " + cipa.TerminoInscricao.ToString("dd-MM-yyyy");
            newRow["tLOCAL"] = eventoCipa.Local;
            newRow["tFraseInicial"] = frasePadrao.Abertura;
            newRow["tFraseFinal"] = frasePadrao.Encerramento;
            newRow["tPrestador"] = responsavelMestraCipa;

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private static DataTable GetTablePublicacao()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("tDadosEmpresa", Type.GetType("System.String"));
            table.Columns.Add("dDataDaPublicacao", Type.GetType("System.String"));
            table.Columns.Add("tHORARIO", Type.GetType("System.String"));
            table.Columns.Add("tPeriodoInscricao", Type.GetType("System.String"));
            table.Columns.Add("tLOCAL", Type.GetType("System.String"));
            table.Columns.Add("tFraseInicial", Type.GetType("System.String"));
            table.Columns.Add("tFraseFinal", Type.GetType("System.String"));
            table.Columns.Add("tPrestador", Type.GetType("System.String"));
            return table;
        }
        #endregion

        #region InicioInscricao

        private DataSet DataSourceRptInicioInscricao()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTableInicioInscricao());

            DataRow newRow = ds.Tables[0].NewRow();

            newRow["tDadosEmpresa"] = cipa.IdCliente.GetDadosEmpresaCipa(eventoCipa.DataSolicitacao);
            newRow["dDataInicioInscricao"] = cipa.InicioInscricao.ToString("dd-MM-yyyy");
            newRow["dHoraInicioInscricao"] = eventoCipa.HoraInicio;
            newRow["tPrestador"] = responsavelMestraCipa;

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private static DataTable GetTableInicioInscricao()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("tDadosEmpresa", Type.GetType("System.String"));
            table.Columns.Add("dDataInicioInscricao", Type.GetType("System.String"));
            table.Columns.Add("dHoraInicioInscricao", Type.GetType("System.String"));
            table.Columns.Add("tPrestador", Type.GetType("System.String"));
            return table;
        }
        #endregion

        #region TerminoInscricao

        private DataSet DataSourceRptTerminoInscricao()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTableTerminoInscricao());

            DataRow newRow = ds.Tables[0].NewRow();

            newRow["tDadosEmpresa"] = cipa.IdCliente.GetDadosEmpresaCipa(eventoCipa.DataSolicitacao);
            newRow["dDataTerminoInscricao"] = cipa.TerminoInscricao.ToString("dd-MM-yyyy");
            newRow["tPrestador"] = responsavelMestraCipa;

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private static DataTable GetTableTerminoInscricao()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("tDadosEmpresa", Type.GetType("System.String"));
            table.Columns.Add("dDataTerminoInscricao", Type.GetType("System.String"));
            table.Columns.Add("tPrestador", Type.GetType("System.String"));
            return table;
        }
        #endregion

        #region CedulaDeVotacao

        private DataSet DataSourceRptCedulaDeVotacao(int qtdCedulas, bool ComNumeroRegisto)
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("NumeroCedula", Type.GetType("System.Int32"));
            table.Columns.Add("NomeAbreviado", Type.GetType("System.String"));
            table.Columns.Add("DataEleicao", Type.GetType("System.String"));
            table.Columns.Add("NomeCancidato", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);

            if (cipa.IdCliente.mirrorOld == null)
                cipa.IdCliente.Find();

            ArrayList listParticipante = new ParticipantesEleicaoCipa().Find("IdCipa=" + cipa.Id);
            listParticipante.Sort();

            ArrayList listCandidado = new ArrayList();

            foreach (ParticipantesEleicaoCipa participante in listParticipante)
            {
                string sCandidato = participante.GetNomeCedula(ComNumeroRegisto);
                listCandidado.Add(sCandidato);
            }

            DataRow newRow;

            for (int i = 0; i < qtdCedulas; i++)
            {
                foreach (string strNomeCandidato in listCandidado)
                {
                    newRow = ds.Tables[0].NewRow();

                    newRow["NumeroCedula"] = i.ToString();
                    newRow["NomeAbreviado"] = cipa.IdCliente.NomeCompleto;
                    newRow["DataEleicao"] = cipa.Eleicao.ToString("dd-MM-yyyy");
                    newRow["NomeCancidato"] = strNomeCandidato;

                    ds.Tables[0].Rows.Add(newRow);
                }
            }
            return ds;
        }

        #endregion

        #region AtaDaPosse

        private DataSet DataSourceRptAtaDaPosse()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTableAtaDaPosse());

            DataRow newRow = ds.Tables[0].NewRow();

            newRow["tDadosEmpresa"] = cipa.IdCliente.GetDadosEmpresaCipa(eventoCipa.DataSolicitacao);
            newRow["dData"] = eventoCipa.DataSolicitacao;
            newRow["tHORARIO"] = eventoCipa.GetHorario();
            newRow["tLOCAL"] = eventoCipa.Local;
            newRow["tFraseInicial"] = frasePadrao.Abertura;
            newRow["tFraseFinal"] = frasePadrao.Encerramento;
            newRow["tFraseAdicional"] = eventoCipa.FraseAdicional;

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private static DataTable GetTableAtaDaPosse()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("tDadosEmpresa", Type.GetType("System.String"));
            table.Columns.Add("dData", Type.GetType("System.DateTime"));
            table.Columns.Add("tHORARIO", Type.GetType("System.String"));
            table.Columns.Add("tLOCAL", Type.GetType("System.String"));
            table.Columns.Add("tFraseInicial", Type.GetType("System.String"));
            table.Columns.Add("tFraseFinal", Type.GetType("System.String"));
            table.Columns.Add("tFraseAdicional", Type.GetType("System.String"));
            return table;
        }
        #endregion

        #region RegistroDRT

        private DataSet DataSourceRptRegistroDRT()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTableRegistroDRT());

            DataRow newRow = ds.Tables[0].NewRow();

            newRow["tDadosEmpresa"] = cipa.IdCliente.GetDadosEmpresaCipa(eventoCipa.DataSolicitacao);
            newRow["tCarimboCnpj"] = cipa.IdCliente.GetCarimboCnpjHtml(eventoCipa.DataSolicitacao, true);
            newRow["tData"] = cipa.IdCliente.GetEndereco().GetCidade() + ", " + cipa.Posse.ToString("d \"de\" MMMM \"de\" yyyy");

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private static DataTable GetTableRegistroDRT()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("tCarimboCnpj", Type.GetType("System.String"));
            table.Columns.Add("tData", Type.GetType("System.String"));
            table.Columns.Add("tDadosEmpresa", Type.GetType("System.String"));
            return table;
        }
        #endregion

        #region ComissaoEleitoral

        private DataSet DataSourceRptComissaoEleitoral()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("tCARGO", Type.GetType("System.String"));
            table.Columns.Add("tNOME", Type.GetType("System.String"));
            ds.Tables.Add(table);
            DataRow newRow;
           
            ArrayList list = new MembroComissaoEleitoral().Find("IdCipa=" + cipa.Id);

            if (list.Count == 0)
                list.Add(new MembroComissaoEleitoral());

            if (list.Count == 1)
                list.Add(new MembroComissaoEleitoral());
            
            for (int i = 0; i < list.Count; i++)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["tCARGO"] = ((MembroComissaoEleitoral)list[i]).NomeCargo;
                newRow["tNOME"] = ((MembroComissaoEleitoral)list[i]).ToString();

                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
        #endregion

        #region Representantes

        private DataSet DataSourceRptRepresentantes()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTableRepresententes());

            string where = "IdCipa=" + cipa.Id
                        + " AND IndStatus=" + (int)MembroCipa.Status.Ativo
                        + " ORDER BY IndTitularSuplente, Numero";

            ArrayList list = new MembroCipa().Find(where);

            DataRow newRow;

            foreach (MembroCipa membroCipa in list)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["tINDICADO"] = membroCipa.GetGrupoMembro();
                newRow["tCARGO"] = membroCipa.GetNomeCargo();
                newRow["nNUMERO"] = membroCipa.Numero;
                newRow["tNOME"] = membroCipa.ToString();

                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }

        private static DataTable GetTableRepresententes()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("tINDICADO", Type.GetType("System.String"));
            table.Columns.Add("tCARGO", Type.GetType("System.String"));
            table.Columns.Add("nNUMERO", Type.GetType("System.Int32"));
            table.Columns.Add("tNOME", Type.GetType("System.String"));
            return table;
        }
        #endregion

        #region Calendario

        private DataSet DataSourceRptCalendario()
        {
            DataSet ds = new DataSet();
            
            DataTable table = new DataTable("Result");
            table.Columns.Add("tDadosEmpresa", Type.GetType("System.String"));
            table.Columns.Add("tData", Type.GetType("System.String"));
            ds.Tables.Add(table);

            DataRow newRow = ds.Tables[0].NewRow();

            newRow["tDadosEmpresa"] = cipa.IdCliente.GetCarimboCnpjHtml(eventoCipa.DataSolicitacao, true);
            newRow["tData"] = cipa.IdCliente.GetEndereco().GetCidade() + ", " + cipa.Posse.ToString("d \"de\" MMMM \"de\" yyyy");

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }
        #endregion

        #region Reunioes

        private DataSet DataSourceRptReunioes()
        {
            DataSet ds = new DataSet();

            DataTable table = new DataTable("Result");
            table.Columns.Add("tTituloReuniao", Type.GetType("System.String"));
            table.Columns.Add("dDataReuniao", Type.GetType("System.DateTime"));
            table.Columns.Add("tHorario", Type.GetType("System.String"));
            ds.Tables.Add(table);

            DataRow newRow;

            ArrayList listReunioes = new ArrayList();
            ArrayList listTitulos = new ArrayList();
            ArrayList listHorarios = new ArrayList();

            int j = 1;

            for (int i = (int)EventoBase.Reuniao1;
                i <= (int)EventoBase.Reuniao12;
                i++)
            {
                listReunioes.Add(cipa.GetDataEventoCipa(i));
                listHorarios.Add(cipa.GetHorarioEventoCipa(i));
                listTitulos.Add(j.ToString() + "º" + " Reunião");
                j++;
            }

            //Termino do Mandato Anterior
            Cipa gestaoAnterior = cipa.GetGestaoAnterior();
            if (gestaoAnterior.Id != 0)
            {
                listReunioes.Add(gestaoAnterior.GetTerminoMandato());
                listTitulos.Add(j.ToString("Término do Mandato Anterior"));
                listHorarios.Add(string.Empty);
            }

            //Posse			
            listReunioes.Add(cipa.Posse);
            listTitulos.Add(j.ToString("Posse"));
            listHorarios.Add(string.Empty);

            Cipa gestaoProxima = cipa.GetGestaoProxima();
            if (gestaoProxima.Id != 0)
            {
                //Posse dos Representantes Eleitos
                listReunioes.Add(gestaoProxima.Posse);
                listTitulos.Add(j.ToString("Posse dos Representantes Eleitos"));
                listHorarios.Add(string.Empty);


                //Eleição
                listReunioes.Add(gestaoProxima.Eleicao);
                listTitulos.Add(j.ToString("Eleição"));
                listHorarios.Add(gestaoProxima.GetHorarioEventoCipa((int)EventoBase.Eleicao));
            }

            //Termino do Mandato
            listReunioes.Add(cipa.GetTerminoMandato());
            listTitulos.Add(j.ToString("Término do Mandato Atual"));
            listHorarios.Add(string.Empty);

            if (listReunioes.Count > 0)
            {
                for (int i = 0; i < listReunioes.Count; i++)
                {
                    newRow = ds.Tables[0].NewRow();
                    newRow["tTituloReuniao"] = listTitulos[i];
                    newRow["dDataReuniao"] = listReunioes[i];
                    newRow["tHorario"] = listHorarios[i];
                    ds.Tables[0].Rows.Add(newRow);
                }
            }
            return ds;
        }
        #endregion

        #region ComprovanteDeInscricao

        private DataSet DataSourceRptComprovanteDeInscricaoDadosEmpregados()
        {
            DataSet ds = new DataSet();

            DataTable table = new DataTable("Result");
            table.Columns.Add("tDataEleicao", Type.GetType("System.String"));
            table.Columns.Add("tNomeEmpregado", Type.GetType("System.String"));
            table.Columns.Add("tApelido", Type.GetType("System.String"));
            table.Columns.Add("tFuncao", Type.GetType("System.String"));
            table.Columns.Add("tSetor", Type.GetType("System.String"));
            table.Columns.Add("tDadosEmpresa", Type.GetType("System.String"));
            ds.Tables.Add(table);

            DataRow newRow;

            ArrayList list = new ParticipantesEleicaoCipa().Find("IdCipa=" + this.cipa.Id);
            list.Sort();

            for (int i = 0; i < list.Count; i++)
            {
                ((ParticipantesEleicaoCipa)list[i]).IdEmpregado.Find();

                EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(((ParticipantesEleicaoCipa)list[i]).IdEmpregado);

                newRow = ds.Tables[0].NewRow();

                newRow["tDataEleicao"] = cipa.GetDataEventoCipa((int)EventoBase.Eleicao).ToString("dd-MM-yyyy");
                newRow["tNomeEmpregado"] = ((ParticipantesEleicaoCipa)list[i]).IdEmpregado.ToString();
                newRow["tApelido"] = ((ParticipantesEleicaoCipa)list[i]).IdEmpregado.tNO_APELIDO;
                newRow["tFuncao"] = empregadoFuncao.GetNomeFuncao();
                newRow["tSetor"] = empregadoFuncao.GetNomeSetor();
                newRow["tDadosEmpresa"] = cipa.IdCliente.GetDadosEmpresaCipa(eventoCipa.DataSolicitacao);

                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }

        private DataSet DataSourceRptComprovanteDeInscricaoComissaoEleitoral()
        {
            DataSet ds = new DataSet();

            DataTable table = new DataTable("Result");
            table.Columns.Add("CidadeData", Type.GetType("System.String"));
            table.Columns.Add("NomeMembro", Type.GetType("System.String"));
            table.Columns.Add("Cargo", Type.GetType("System.String"));
            
            ds.Tables.Add(table);

            DataRow newRow; ;

            if (cipa.IdCliente.mirrorOld == null)
                cipa.IdCliente.Find();

            if (cipa.IdCliente.IdCNAE.mirrorOld == null)
                cipa.IdCliente.IdCNAE.Find();

            Endereco endereco = cipa.IdCliente.GetEndereco();

            ArrayList listMembroComissaoEleitoral = new MembroComissaoEleitoral().Find("IdCipa=" + this.cipa.Id);

            foreach (MembroComissaoEleitoral membroComissaoEleitoral in listMembroComissaoEleitoral)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["CidadeData"] = endereco.GetCidade() + ", " + DateTime.Today.ToString("d \"de\" MMMM \"de\" yyyy");
                newRow["NomeMembro"] = membroComissaoEleitoral.ToString();
                newRow["Cargo"] = membroComissaoEleitoral.NomeCargo;
                

                ds.Tables[0].Rows.Add(newRow);
            }

            return ds;
        }

        private DataSet DataSourceRptComprovanteDeInscricaoDadosDaEmpresa()
        {
            DataSet ds = new DataSet();

            DataTable table = new DataTable("Result");
            table.Columns.Add("CarimboCNPJ", Type.GetType("System.String"));
            ds.Tables.Add(table);

            DataRow newRow = ds.Tables[0].NewRow();

            if (cipa.IdCliente.mirrorOld == null)
                cipa.IdCliente.Find();

            newRow["CarimboCNPJ"] = cipa.IdCliente.GetCarimboCnpjHtml(cipa.InicioInscricao, true);
            
            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }
        #endregion

        #region InscricaoDosCandidatos

        private DataSet DataSourceRptInscricaoDosCandidatos()
        {
            DataSet ds = new DataSet();

            DataTable table = new DataTable("Result");
            table.Columns.Add("DataInicio", Type.GetType("System.DateTime"));
            table.Columns.Add("DataTermino", Type.GetType("System.DateTime"));
            table.Columns.Add("CarimboCNPJ", Type.GetType("System.String"));
            table.Columns.Add("Numero", Type.GetType("System.String"));
            
            ds.Tables.Add(table);
            DataRow newRow;

            if (cipa.IdCliente.mirrorOld == null)
                cipa.IdCliente.Find();

            for (int i = 1; i <= 20; i++)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["DataInicio"] = cipa.InicioInscricao;
                newRow["DataTermino"] = cipa.TerminoInscricao;
                newRow["CarimboCNPJ"] = cipa.IdCliente.GetCarimboCnpjHtml(cipa.InicioInscricao, true);
                newRow["Numero"] = i.ToString("0");
            

                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
        #endregion

        #region ElaboracaoDeAta

        private DataSet DataSourceRptElaboracaoDeAta()
        {
            DataSet ds = new DataSet();
            DataTable table = GetTableElaboracaoDeAta();

            ds.Tables.Add(table);

            DataRow newRow;
            newRow = ds.Tables[0].NewRow();

            if (cipa.IdCliente.mirrorOld == null)
                cipa.IdCliente.Find();

            Endereco endereco = cipa.IdCliente.GetEndereco();

            ContatoTelefonico contatoTelefonico = cipa.IdCliente.GetContatoTelefonico();
            ContatoTelefonico contatoFax = cipa.IdCliente.GetFax();

            newRow["tData"] = eventoCipa.DataSolicitacao.ToString("dd-MM-yyyy");
            newRow["tNomeCompleto"] = cipa.IdCliente.NomeCompleto;
            newRow["tLogradouro"] = endereco.GetEndereco();
            newRow["tTelefone"] = contatoTelefonico.Numero;
            newRow["tFax"] = contatoFax.Numero;
            newRow["tContato"] = contatoTelefonico.Nome;
            newRow["tDepartamento"] = contatoTelefonico.Departamento;
            newRow["tNumeroDeEmpregados"] = cipa.IdCliente.QtdEmpregados.ToString();

            PopularPresidente(newRow);
            PopularVicePresidente(newRow);
            PopularSecretarios(newRow);
            PopularEmpregador(newRow);
            PopularEmpregado(newRow);
            PopularSuplenteEmpregador(newRow);
            PopularSuplentesEmpregados(newRow);
            PopularCalendario(newRow);

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        #region Popular

        private void PopularCalendario(DataRow newRow)
        {
            //Cipa proximaGestao = cipa.IdCliente.GetProximaGestao();
            //DateTime dataEleicao = proximaGestao.GetDataEventoCipa(EventoBase.Eleicao);
            //DateTime dataPosse = proximaGestao.GetDataEventoCipa(EventoBase.Posse);

            newRow["tDataPeriodo"] = GetDataPeriodo(eventoCipa.IdEventoBaseCipa.Id);

            if (eventoCipa.IdEventoBaseCipa.Id == (int)EventoBase.Reuniao1)
                newRow["d1Reuniao"] = cipa.GetDataEventoCipa(EventoBase.Reuniao1);

            if (eventoCipa.IdEventoBaseCipa.Id == (int)EventoBase.Reuniao2)
                newRow["d2Reuniao"] = cipa.GetDataEventoCipa(EventoBase.Reuniao2);

            if (eventoCipa.IdEventoBaseCipa.Id == (int)EventoBase.Reuniao3)
                newRow["d3Reuniao"] = cipa.GetDataEventoCipa(EventoBase.Reuniao3);

            if (eventoCipa.IdEventoBaseCipa.Id == (int)EventoBase.Reuniao4)
                newRow["d4Reuniao"] = cipa.GetDataEventoCipa(EventoBase.Reuniao4);

            if (eventoCipa.IdEventoBaseCipa.Id == (int)EventoBase.Reuniao5)
                newRow["d5Reuniao"] = cipa.GetDataEventoCipa(EventoBase.Reuniao5);

            if (eventoCipa.IdEventoBaseCipa.Id == (int)EventoBase.Reuniao6)
                newRow["d6Reuniao"] = cipa.GetDataEventoCipa(EventoBase.Reuniao6);

            if (eventoCipa.IdEventoBaseCipa.Id == (int)EventoBase.Reuniao7)
                newRow["d7Reuniao"] = cipa.GetDataEventoCipa(EventoBase.Reuniao7);
            
            if (eventoCipa.IdEventoBaseCipa.Id == (int)EventoBase.Reuniao8)
                newRow["d8Reuniao"] = cipa.GetDataEventoCipa(EventoBase.Reuniao8);

            if (eventoCipa.IdEventoBaseCipa.Id == (int)EventoBase.Reuniao9)
                newRow["d9Reuniao"] = cipa.GetDataEventoCipa(EventoBase.Reuniao9);

            if (eventoCipa.IdEventoBaseCipa.Id == (int)EventoBase.Reuniao10)
                newRow["d10Reuniao"] = cipa.GetDataEventoCipa(EventoBase.Reuniao10);

            if (eventoCipa.IdEventoBaseCipa.Id == (int)EventoBase.Reuniao11)
                newRow["d11Reuniao"] = cipa.GetDataEventoCipa(EventoBase.Reuniao11);

            //if (eventoCipa.IdEventoBaseCipa.Id >= (int)EventoBase.Reuniao11)
            //    newRow["tEleicao"] = dataEleicao;

            if (eventoCipa.IdEventoBaseCipa.Id == (int)EventoBase.Reuniao12)
                newRow["d12Reuniao"] = cipa.GetDataEventoCipa(EventoBase.Reuniao12);

            //if (eventoCipa.IdEventoBaseCipa.Id >= (int)EventoBase.Reuniao11)
            //    newRow["tPosse"] = dataPosse;
        }

        private void PopularPresidente(DataRow newRow)
        {
            MembroCipa presidente = new MembroCipa();
            presidente.Find("IdCipa=" + cipa.Id
                            + " AND IndTitularSuplente=" + (int)TitularSuplente.Titular
                            + " AND IndGrupoMembro=" + (int)GrupoMembro.Empregador
                            + " AND Numero=0");

            newRow["tPresidente"] = presidente.ToString();
        }

        private void PopularVicePresidente(DataRow newRow)
        {
            MembroCipa vicePresidente = new MembroCipa();
            vicePresidente.Find("IdCipa=" + cipa.Id
                            + " AND IndTitularSuplente=" + (short)TitularSuplente.Titular
                            + " AND IndGrupoMembro=" + (int)GrupoMembro.Empregado
                            + " AND Numero=0");

            newRow["tVicePresidente"] = vicePresidente.ToString();
        }

        private void PopularSuplentesEmpregados(DataRow newRow)
        {
            ArrayList listSuplEmpregados = new MembroCipa().Find("IdCipa=" + cipa.Id
                                                                + " AND IndStatus=" + (int)MembroCipa.Status.Ativo
                                                                + " AND IndTitularSuplente=" + (short)TitularSuplente.Suplente
                                                                + " AND IndGrupoMembro=" + (short)GrupoMembro.Empregado
                                                                + " ORDER BY Numero");

            int qtdSuplEmpregados = listSuplEmpregados.Count - 1;

            while (qtdSuplEmpregados != -1)
            {
                switch (qtdSuplEmpregados)
                {
                    case 0:
                        newRow["t1SuplEmpregado"] = ((MembroCipa)listSuplEmpregados[qtdSuplEmpregados]).ToString();
                        break;
                    case 1:
                        newRow["t2SuplEmpregado"] = ((MembroCipa)listSuplEmpregados[qtdSuplEmpregados]).ToString();
                        break;
                    case 2:
                        newRow["t3SuplEmpregado"] = ((MembroCipa)listSuplEmpregados[qtdSuplEmpregados]).ToString();
                        break;
                    case 3:
                        newRow["t4SuplEmpregado"] = ((MembroCipa)listSuplEmpregados[qtdSuplEmpregados]).ToString();
                        break;
                    case 4:
                        newRow["t5SuplEmpregado"] = ((MembroCipa)listSuplEmpregados[qtdSuplEmpregados]).ToString();
                        break;
                    case 5:
                        newRow["t6SuplEmpregado"] = ((MembroCipa)listSuplEmpregados[qtdSuplEmpregados]).ToString();
                        break;
                }
                qtdSuplEmpregados--;
            }
        }

        private void PopularSuplenteEmpregador(DataRow newRow)
        {
            ArrayList listSuplEmpregador = new MembroCipa().Find("IdCipa=" + cipa.Id
                                                                + " AND IndStatus=" + (int)MembroCipa.Status.Ativo
                                                                + " AND IndTitularSuplente=" + (int)TitularSuplente.Suplente
                                                                + " AND IndGrupoMembro=" + (int)GrupoMembro.Empregador
                                                                + " ORDER BY Numero");

            int qtdSuplEmpregador = listSuplEmpregador.Count - 1;

            while (qtdSuplEmpregador != -1)
            {
                switch (qtdSuplEmpregador)
                {
                    case 0:
                        newRow["t1SuplEmpregador"] = ((MembroCipa)listSuplEmpregador[qtdSuplEmpregador]).ToString();
                        break;
                    case 1:
                        newRow["t2SuplEmpregador"] = ((MembroCipa)listSuplEmpregador[qtdSuplEmpregador]).ToString();
                        break;
                    case 2:
                        newRow["t3SuplEmpregador"] = ((MembroCipa)listSuplEmpregador[qtdSuplEmpregador]).ToString();
                        break;
                    case 3:
                        newRow["t4SuplEmpregador"] = ((MembroCipa)listSuplEmpregador[qtdSuplEmpregador]).ToString();
                        break;
                    case 4:
                        newRow["t5SuplEmpregador"] = ((MembroCipa)listSuplEmpregador[qtdSuplEmpregador]).ToString();
                        break;
                    case 5:
                        newRow["t6SuplEmpregador"] = ((MembroCipa)listSuplEmpregador[qtdSuplEmpregador]).ToString();
                        break;
                }
                qtdSuplEmpregador--;
            }
        }

        private void PopularEmpregado(DataRow newRow)
        {
            ArrayList listMembroEmpregado = new MembroCipa().Find("IdCipa=" + cipa.Id
                                                                + " AND IndTitularSuplente=" + (short)TitularSuplente.Titular
                                                                + " AND IndStatus=" + (int)MembroCipa.Status.Ativo
                                                                + " AND IndGrupoMembro=" + (short)GrupoMembro.Empregado
                                                                + " AND Numero<>0 ORDER BY Numero");

            int qtdMembrosEmpregados = listMembroEmpregado.Count - 1;

            while (qtdMembrosEmpregados != -1)
            {
                switch (qtdMembrosEmpregados)
                {
                    case 0:
                        newRow["t1MemEmpregado"] = ((MembroCipa)listMembroEmpregado[qtdMembrosEmpregados]).ToString();
                        break;
                    case 1:
                        newRow["t2MemEmpregado"] = ((MembroCipa)listMembroEmpregado[qtdMembrosEmpregados]).ToString();
                        break;
                    case 2:
                        newRow["t3MemEmpregado"] = ((MembroCipa)listMembroEmpregado[qtdMembrosEmpregados]).ToString();
                        break;
                    case 3:
                        newRow["t4MemEmpregado"] = ((MembroCipa)listMembroEmpregado[qtdMembrosEmpregados]).ToString();
                        break;
                    case 4:
                        newRow["t5MemEmpregado"] = ((MembroCipa)listMembroEmpregado[qtdMembrosEmpregados]).ToString();
                        break;
                }
                qtdMembrosEmpregados--;
            }
        }

        private void PopularEmpregador(DataRow newRow)
        {
            ArrayList listMembroEmpregador = new MembroCipa().Find("IdCipa=" + cipa.Id
                                                                + " AND IndTitularSuplente=" + (int)TitularSuplente.Titular
                                                                + " AND IndStatus=" + (int)MembroCipa.Status.Ativo
                                                                + " AND IndGrupoMembro=" + (int)GrupoMembro.Empregador
                                                                + " AND Numero <> 0"
                                                                + " ORDER BY Numero");


            int qtdMembrosEmpregador = listMembroEmpregador.Count - 1;

            while (qtdMembrosEmpregador != -1)
            {
                switch (qtdMembrosEmpregador)
                {
                    case 0:
                        newRow["t1MemEmpregador"] = ((MembroCipa)listMembroEmpregador[qtdMembrosEmpregador]).ToString();
                        break;
                    case 1:
                        newRow["t2MemEmpregador"] = ((MembroCipa)listMembroEmpregador[qtdMembrosEmpregador]).ToString();
                        break;
                    case 2:
                        newRow["t3MemEmpregador"] = ((MembroCipa)listMembroEmpregador[qtdMembrosEmpregador]).ToString();
                        break;
                    case 3:
                        newRow["t4MemEmpregador"] = ((MembroCipa)listMembroEmpregador[qtdMembrosEmpregador]).ToString();
                        break;
                    case 4:
                        newRow["t5MemEmpregador"] = ((MembroCipa)listMembroEmpregador[qtdMembrosEmpregador]).ToString();
                        break;
                }
                qtdMembrosEmpregador--;
            }
        }

        private void PopularSecretarios(DataRow newRow)
        {
            ArrayList listSecretarios = new MembroCipa().Find("IdCipa=" + cipa.Id
                                                            + " AND IndGrupoMembro=" + (int)GrupoMembro.Secretario);

            for (int i = 0; i < listSecretarios.Count; i++)
            {
                if (((MembroCipa)listSecretarios[i]).IndTitularSuplente == (int)TitularSuplente.Titular)
                    newRow["tSecretario"] = ((MembroCipa)listSecretarios[i]).ToString();
                else
                    newRow["tSecretarioSubst"] = ((MembroCipa)listSecretarios[i]).ToString();
            }
        }
        #endregion

        #region GetTable

        private static DataTable GetTableElaboracaoDeAta()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("tData", Type.GetType("System.String"));
            table.Columns.Add("tNomeCompleto", Type.GetType("System.String"));
            table.Columns.Add("tLogradouro", Type.GetType("System.String"));
            table.Columns.Add("tTelefone", Type.GetType("System.String"));
            table.Columns.Add("tFax", Type.GetType("System.String"));
            table.Columns.Add("tContato", Type.GetType("System.String"));
            table.Columns.Add("tDepartamento", Type.GetType("System.String"));
            table.Columns.Add("tNumeroDeEmpregados", Type.GetType("System.String"));
            table.Columns.Add("d1Reuniao", Type.GetType("System.DateTime"));
            table.Columns.Add("d2Reuniao", Type.GetType("System.DateTime"));
            table.Columns.Add("d3Reuniao", Type.GetType("System.DateTime"));
            table.Columns.Add("d4Reuniao", Type.GetType("System.DateTime"));
            table.Columns.Add("d5Reuniao", Type.GetType("System.DateTime"));
            table.Columns.Add("d6Reuniao", Type.GetType("System.DateTime"));
            table.Columns.Add("d7Reuniao", Type.GetType("System.DateTime"));
            table.Columns.Add("d8Reuniao", Type.GetType("System.DateTime"));
            table.Columns.Add("d9Reuniao", Type.GetType("System.DateTime"));
            table.Columns.Add("d10Reuniao", Type.GetType("System.DateTime"));
            table.Columns.Add("d11Reuniao", Type.GetType("System.DateTime"));
            table.Columns.Add("d12Reuniao", Type.GetType("System.DateTime"));
            table.Columns.Add("tEleicao", Type.GetType("System.DateTime"));
            table.Columns.Add("tPosse", Type.GetType("System.DateTime"));
            table.Columns.Add("tPresidente", Type.GetType("System.String"));
            table.Columns.Add("t1MemEmpregador", Type.GetType("System.String"));
            table.Columns.Add("t2MemEmpregador", Type.GetType("System.String"));
            table.Columns.Add("t3MemEmpregador", Type.GetType("System.String"));
            table.Columns.Add("t4MemEmpregador", Type.GetType("System.String"));
            table.Columns.Add("t5MemEmpregador", Type.GetType("System.String"));
            table.Columns.Add("tVicePresidente", Type.GetType("System.String"));
            table.Columns.Add("t1MemEmpregado", Type.GetType("System.String"));
            table.Columns.Add("t2MemEmpregado", Type.GetType("System.String"));
            table.Columns.Add("t3MemEmpregado", Type.GetType("System.String"));
            table.Columns.Add("t4MemEmpregado", Type.GetType("System.String"));
            table.Columns.Add("t5MemEmpregado", Type.GetType("System.String"));
            table.Columns.Add("t1SuplEmpregador", Type.GetType("System.String"));
            table.Columns.Add("t2SuplEmpregador", Type.GetType("System.String"));
            table.Columns.Add("t3SuplEmpregador", Type.GetType("System.String"));
            table.Columns.Add("t4SuplEmpregador", Type.GetType("System.String"));
            table.Columns.Add("t5SuplEmpregador", Type.GetType("System.String"));
            table.Columns.Add("t6SuplEmpregador", Type.GetType("System.String"));
            table.Columns.Add("t1SuplEmpregado", Type.GetType("System.String"));
            table.Columns.Add("t2SuplEmpregado", Type.GetType("System.String"));
            table.Columns.Add("t3SuplEmpregado", Type.GetType("System.String"));
            table.Columns.Add("t4SuplEmpregado", Type.GetType("System.String"));
            table.Columns.Add("t5SuplEmpregado", Type.GetType("System.String"));
            table.Columns.Add("t6SuplEmpregado", Type.GetType("System.String"));
            table.Columns.Add("tSecretario", Type.GetType("System.String"));
            table.Columns.Add("tSecretarioSubst", Type.GetType("System.String"));
            table.Columns.Add("tDataPeriodo", Type.GetType("System.String"));
            return table;
        }
        #endregion

        #region GetDataPeriodo

        private string GetDataPeriodo(int IdEventoBaseCipa)
        {
            string ret = string.Empty;

            switch (IdEventoBaseCipa)
            {
                case (int)EventoBase.Eleicao:
                    ret = cipa.Reuniao11.ToString("dd-MM-yyyy") + " a " + cipa.Eleicao.ToString("dd-MM-yyyy");
                    break;

                case (int)EventoBase.Posse:
                    ret = cipa.Reuniao12.ToString("dd-MM-yyyy") + " a " + cipa.Posse.ToString("dd-MM-yyyy");
                    break;

                case (int)EventoBase.Reuniao1:

                    Cipa ultimaCipa = cipa.GetGestaoAnterior();

                    if (ultimaCipa != null && ultimaCipa.Id != 0)
                        ret = ultimaCipa.GetTerminoMandato().ToString("dd-MM-yyyy") + " a " + cipa.Reuniao1.ToString("dd-MM-yyyy");
                    else
                        ret = cipa.Reuniao1.AddMonths(-1).ToString("dd-MM-yyyy") + " a " + cipa.Reuniao1.ToString("dd-MM-yyyy");
                    break;

                case (int)EventoBase.Reuniao2:
                    ret = cipa.Reuniao1.ToString("dd-MM-yyyy") + " a " + cipa.Reuniao2.ToString("dd-MM-yyyy");
                    break;

                case (int)EventoBase.Reuniao3:
                    ret = cipa.Reuniao2.ToString("dd-MM-yyyy") + " a " + cipa.Reuniao3.ToString("dd-MM-yyyy");
                    break;

                case (int)EventoBase.Reuniao4:
                    ret = cipa.Reuniao3.ToString("dd-MM-yyyy") + " a " + cipa.Reuniao4.ToString("dd-MM-yyyy");
                    break;

                case (int)EventoBase.Reuniao5:
                    ret = cipa.Reuniao4.ToString("dd-MM-yyyy") + " a " + cipa.Reuniao5.ToString("dd-MM-yyyy");
                    break;

                case (int)EventoBase.Reuniao6:
                    ret = cipa.Reuniao5.ToString("dd-MM-yyyy") + " a " + cipa.Reuniao6.ToString("dd-MM-yyyy");
                    break;

                case (int)EventoBase.Reuniao7:
                    ret = cipa.Reuniao6.ToString("dd-MM-yyyy") + " a " + cipa.Reuniao7.ToString("dd-MM-yyyy");
                    break;

                case (int)EventoBase.Reuniao8:
                    ret = cipa.Reuniao7.ToString("dd-MM-yyyy") + " a " + cipa.Reuniao8.ToString("dd-MM-yyyy");
                    break;

                case (int)EventoBase.Reuniao9:
                    ret = cipa.Reuniao8.ToString("dd-MM-yyyy") + " a " + cipa.Reuniao9.ToString("dd-MM-yyyy");
                    break;

                case (int)EventoBase.Reuniao10:
                    ret = cipa.Reuniao9.ToString("dd-MM-yyyy") + " a " + cipa.Reuniao10.ToString("dd-MM-yyyy");
                    break;

                case (int)EventoBase.Reuniao11:
                    ret = cipa.Reuniao10.ToString("dd-MM-yyyy") + " a " + cipa.Reuniao11.ToString("dd-MM-yyyy");
                    break;

                case (int)EventoBase.Reuniao12:
                    ret = cipa.Eleicao.ToString("dd-MM-yyyy") + " a " + cipa.Reuniao12.ToString("dd-MM-yyyy");
                    break;
            }
            return ret;
        }
        #endregion

        #endregion

        #region FolhaDeApuracao

        private DataSet DataSourceFolhaDeApuracao()
        {
            DataSet ds = new DataSet();

            DataTable table = new DataTable("Result");
            table.Columns.Add("tDadosEmpresa", Type.GetType("System.String"));
            table.Columns.Add("dDataDaEleicao", Type.GetType("System.String"));
            table.Columns.Add("tHORARIO", Type.GetType("System.String"));
            table.Columns.Add("tLOCAL", Type.GetType("System.String"));
            ds.Tables.Add(table);

            DataRow newRow = ds.Tables[0].NewRow();

            newRow["tDadosEmpresa"] = eventoCipa.IdCliente.GetCarimboCnpjHtml(eventoCipa.DataSolicitacao, true);
            newRow["dDataDaEleicao"] = eventoCipa.DataSolicitacao.ToString("dd-MM-yyyy");
            newRow["tHORARIO"] = eventoCipa.HoraInicio;
            newRow["tLOCAL"] = eventoCipa.Local;

            ds.Tables[0].Rows.Add(newRow);
            return ds;
        }

        #endregion

        #region ClassificacaoApuracao

        private DataSet DataSourceRptClassificacaoApuracao()
        {
            DataSet ds = new DataSet();
            int classificados;
            DataTable table = new DataTable("Result");
            table.Columns.Add("tClassificacao", Type.GetType("System.String"));
            ds.Tables.Add(table);
            DimensionamentoCipa dim = cipa.IdCliente.GetDimensionamentoCipa();
            classificados = dim.Efetivo + dim.Suplente;
            DataRow newRow;
            for (int i = 0; i < classificados; i++)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["tClassificacao"] = i + 1 + "º Lugar";
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }

        private DataSet DataSourceRptClassificacaoApuracao(List<CandidatosApurados> listaCandidatos, string tipoResultado)
        {
            DataSet ds = new DataSet();
            int classificados;
            DataTable table = new DataTable("Result");
            table.Columns.Add("tClassificacao", Type.GetType("System.String"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("Votos", Type.GetType("System.Int32"));
            ds.Tables.Add(table);
            DimensionamentoCipa dim = cipa.IdCliente.GetDimensionamentoCipa();
            classificados = dim.Efetivo + dim.Suplente;
            DataRow newRow;
            if (tipoResultado == "somenteEleitos")
            {
                for (int i = 0; i < classificados; i++)
                {
                    newRow = ds.Tables[0].NewRow();
                    newRow["tClassificacao"] = i + 1 + "º Lugar";
                    newRow["Nome"] = i < listaCandidatos.Count ? listaCandidatos[i].NomeEmpregado : "";
                    newRow["Votos"] = i < listaCandidatos.Count ? listaCandidatos[i].QtdeVotosRecebidoPeloCandidato : 0;
                    ds.Tables[0].Rows.Add(newRow);
                }
            }
            else if (tipoResultado == "todos")
            {
                for (int i = 0; i < listaCandidatos.Count; i++)
                {
                    newRow = ds.Tables[0].NewRow();
                    newRow["tClassificacao"] = i + 1 + "º Lugar";
                    newRow["Nome"] = i < listaCandidatos.Count ? listaCandidatos[i].NomeEmpregado : "";
                    newRow["Votos"] = i < listaCandidatos.Count ? listaCandidatos[i].QtdeVotosRecebidoPeloCandidato : 0;
                    ds.Tables[0].Rows.Add(newRow);
                }
            }
            return ds;
        }

        private DataSet DataSourceFolhaApuracaoCompleto(List<CandidatosApurados> listaCandidatos)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Cargo", Type.GetType("System.String"));
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("Votos", Type.GetType("System.Int32"));
            ds.Tables.Add(table);

            DimensionamentoCipa dim = cipa.IdCliente.GetDimensionamentoCipa();
            //Representantes do Empregador
            string query = $@"
                        SELECT Titulo = 'Representantes do Empregador', Cargo = '', tNO_EMPG as Nome, Votos = 0
                        FROM (
                            SELECT *, 0 AS order_column
                            FROM MembroCipa a 
                            JOIN [{Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper()}].dbo.tblEMPREGADO b ON a.IdEmpregado = b.nID_EMPREGADO
                            WHERE a.IdCipa = {this.cipa.Id} AND IndGrupoMembro = 1 AND Numero = 0 
    
                            UNION ALL
    
                            SELECT *, 1 AS order_column
                            FROM (
                                    SELECT *
                                    FROM MembroCipa a 
                                    JOIN [{Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper()}].dbo.tblEMPREGADO b ON a.IdEmpregado = b.nID_EMPREGADO
                                    WHERE a.IdCipa = {this.cipa.Id} AND IndGrupoMembro = 1 AND Numero <> 0 AND a.IndTitularSuplente = 1
                            ) AS subquery2
    
                            UNION ALL
    
                            SELECT *, 2 AS order_column
                            FROM (
                                    SELECT *
                                    FROM MembroCipa a 
                                    JOIN [{Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper()}].dbo.tblEMPREGADO b ON a.IdEmpregado = b.nID_EMPREGADO
                                    WHERE a.IdCipa = {this.cipa.Id} AND IndGrupoMembro = 1 AND a.IndTitularSuplente = 2
                            ) AS subquery3
                        ) AS resultado
                        ORDER BY order_column, Numero ASC;";

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();

                da = new SqlDataAdapter(query, cnn);
                da.Fill(ds, "Result");

                cnn.Close();
                da.Dispose();
            }
            ds.Tables["Result"].Rows[0][1] = "Presidente";

            for (int i = 2, j = 1; i <= dim.Efetivo; i++, j++)
            {
                ds.Tables["Result"].Rows[i - 1][1] = $"{j}º Membro";
            }

            for (int i = dim.Efetivo + 1, j = 1; i <= (dim.Efetivo + dim.Suplente); i++, j++)
            {
                ds.Tables["Result"].Rows[i - 1][1] = $"{j}º Suplente";
            }

            //Classificação
            int classificados;
            classificados = dim.Efetivo + dim.Suplente;
            DataRow newRow;
            for (int i = 0; i < listaCandidatos.Count; i++)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["Titulo"] = "Participantes";
                newRow["Cargo"] = i + 1 + "º Lugar";
                newRow["Nome"] = i < listaCandidatos.Count ? listaCandidatos[i].NomeEmpregado : "";
                newRow["Votos"] = i < listaCandidatos.Count ? listaCandidatos[i].QtdeVotosRecebidoPeloCandidato : 0;
                ds.Tables[0].Rows.Add(newRow);
            }


            //Votos
            query = $@"SELECT
                        'Votos' AS Titulo,
                        Cargo,
                        Votos
                    FROM (
                        SELECT 
                            VotosNulos, 
                            VotosBrancos, 
                            TotalDeVotos, 
                            (SELECT COUNT(*) 
                             FROM {Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper()}.dbo.tblEMPREGADO 
                             WHERE hDT_DEM IS NULL AND nID_EMPR = {this.cipa.IdCliente.Id}) AS TotalFuncionarios
                        FROM EleicaoCipa 
                        WHERE IdEleicaoCipa = {this.eventoCipa.Id}
                    ) AS SourceTable
                    UNPIVOT
                    (
                        Votos FOR Cargo IN (VotosNulos, VotosBrancos, TotalDeVotos, TotalFuncionarios)
                    ) AS UnpivotedTable;";

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();

                da = new SqlDataAdapter(query, cnn);
                da.Fill(ds, "Result");

                cnn.Close();
                da.Dispose();
            }

            //CandidatosEleitos
            for (int i = 0; i < classificados; i++)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["Titulo"] = "Eleitos";
                newRow["Cargo"] = i + 1 + "º Lugar";
                newRow["Nome"] = i < listaCandidatos.Count ? listaCandidatos[i].NomeEmpregado : "";
                newRow["Votos"] = i < listaCandidatos.Count ? listaCandidatos[i].QtdeVotosRecebidoPeloCandidato : 0;
                ds.Tables[0].Rows.Add(newRow);
            }

            //Representantes dos Empregados
            for (int i = 0; i < listaCandidatos.Count; i++)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["Titulo"] = "Representantes dos Empregados";
                if (i <= dim.Efetivo - 1)
                {
                    if (i == 0)
                    {
                        newRow["Cargo"] = "Vice-Presidente : ";
                        newRow["Nome"] = listaCandidatos[i].NomeEmpregado;
                    }
                    else
                    {
                        newRow["Cargo"] = i + "º Membro : ";
                        newRow["Nome"] = listaCandidatos[i].NomeEmpregado;
                    }
                }
                else
                {
                    newRow["Cargo"] = (i - dim.Efetivo + 1) + "º Suplente : ";
                    newRow["Nome"] = listaCandidatos[i].NomeEmpregado;
                }
                newRow["Votos"] = 0;
                ds.Tables[0].Rows.Add(newRow);
            }


            return ds;
        }

        private DataSet DataEleicaoFolhaApuracaoCompleto()
        {
            DataSet ds = new DataSet();

            DataTable table = new DataTable("Data");
            table.Columns.Add("Data", Type.GetType("System.String"));
            ds.Tables.Add(table);

            DataRow newRow = ds.Tables[0].NewRow();
            newRow["Data"] = eventoCipa.DataSolicitacao.ToString("dd/MM/yyyy");

            ds.Tables[0].Rows.Add(newRow);
            return ds;
        }

        private DataSet DadosEmpresaFolhaApuracaoCompleto()
        {
            DataSet ds = new DataSet();

            DataTable table = new DataTable("DadosEmpresa");
            table.Columns.Add("DadosEmpresa", Type.GetType("System.String"));
            ds.Tables.Add(table);

            DataRow newRow = ds.Tables[0].NewRow();
            newRow["DadosEmpresa"] = cipa.IdCliente.GetDadosEmpresaCipa(eventoCipa.DataSolicitacao);

            ds.Tables[0].Rows.Add(newRow);
            return ds;
        }
        #endregion

        #region DemaisVotadosApuracao
        private DataSet DataSourceRptDemaisVotadosApuracao()
        {
            DataSet ds = new DataSet();
            int classificados;
            int demaisVotados;
            DataTable table = new DataTable("Result");
            table.Columns.Add("tDemaisVotados", Type.GetType("System.String"));
            ds.Tables.Add(table);
            DimensionamentoCipa dim = cipa.IdCliente.GetDimensionamentoCipa();
            ArrayList listCandidatos = new MembroCipa().Find("IdCipa=" + cipa.Id
                + " AND IndGrupoMembro=" + (int)GrupoMembro.Empregado);
            classificados = dim.Efetivo + dim.Suplente;
            demaisVotados = listCandidatos.Count - classificados;
            DataRow newRow;
            for (int i = 0; i < demaisVotados; i++)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["tDemaisVotados"] = "";
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
        #endregion

        #region EmpregadosApuracao
        private DataSet DataSourceRptEmpregadosApuracao()
        {
            DataSet ds = new DataSet();

            DataTable table = new DataTable("Result");
            table.Columns.Add("tEmpregados", Type.GetType("System.String"));
            ds.Tables.Add(table);

            DimensionamentoCipa dim = cipa.IdCliente.GetDimensionamentoCipa();
            int classificados = dim.Efetivo + dim.Suplente;

            DataRow newRow;

            for (int i = 0; i < classificados; i++)
            {
                newRow = ds.Tables[0].NewRow();
                if (i <= dim.Efetivo - 1)
                {
                    if (i == 0)
                        newRow["tEmpregados"] = "Vice-Presidente :";
                    else
                        newRow["tEmpregados"] = i + "º Membro :";
                }
                else
                    newRow["tEmpregados"] = (i - dim.Efetivo + 1) + "º Suplente :";
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }

        private DataSet DataSourceRptEmpregadosApuracao(List<CandidatosApurados> listaCandidatos)
        {
            DataSet ds = new DataSet();

            DataTable table = new DataTable("Result");
            table.Columns.Add("tEmpregados", Type.GetType("System.String"));
            ds.Tables.Add(table);

            DimensionamentoCipa dim = cipa.IdCliente.GetDimensionamentoCipa();
            int classificados = dim.Efetivo + dim.Suplente;

            DataRow newRow;
            for (int i = 0; i < listaCandidatos.Count; i++)
            {
                newRow = ds.Tables[0].NewRow();
                if (i <= dim.Efetivo - 1)
                {
                    if (i == 0)
                        newRow["tEmpregados"] = $"Vice-Presidente :         {listaCandidatos[i].NomeEmpregado}";
                    else
                        newRow["tEmpregados"] = i + $"º Membro :                {listaCandidatos[i].NomeEmpregado}";
                }
                else
                    newRow["tEmpregados"] = (i - dim.Efetivo + 1) + $"º Suplente :              {listaCandidatos[i].NomeEmpregado}";
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }

        private DataSet DataSourceRptEmpregadorApuracaoNovo()
        {
            DataSet ds = new DataSet();

            DataTable table = new DataTable("ResultEmpregador");
            table.Columns.Add("nomeEmpregado", Type.GetType("System.String"));
            table.Columns.Add("cargoCipa", Type.GetType("System.String"));
            ds.Tables.Add(table);

            DimensionamentoCipa dim = cipa.IdCliente.GetDimensionamentoCipa();

            int classificados = dim.Efetivo + dim.Suplente;

            string query = $@"
                        SELECT tNO_EMPG as nomeEmpregado
                        FROM (
                            SELECT *, 0 AS order_column
                            FROM MembroCipa a 
                            JOIN [{Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper()}].dbo.tblEMPREGADO b ON a.IdEmpregado = b.nID_EMPREGADO
                            WHERE a.IdCipa = {this.cipa.Id} AND IndGrupoMembro = 1 AND Numero = 0 
    
                            UNION ALL
    
                            SELECT *, 1 AS order_column
                            FROM (
                                    SELECT *
                                    FROM MembroCipa a 
                                    JOIN [{Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper()}].dbo.tblEMPREGADO b ON a.IdEmpregado = b.nID_EMPREGADO
                                    WHERE a.IdCipa = {this.cipa.Id} AND IndGrupoMembro = 1 AND Numero <> 0 AND a.IndTitularSuplente = 1
                            ) AS subquery2
    
                            UNION ALL
    
                            SELECT *, 2 AS order_column
                            FROM (
                                    SELECT *
                                    FROM MembroCipa a 
                                    JOIN [{Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper()}].dbo.tblEMPREGADO b ON a.IdEmpregado = b.nID_EMPREGADO
                                    WHERE a.IdCipa = {this.cipa.Id} AND IndGrupoMembro = 1 AND a.IndTitularSuplente = 2
                            ) AS subquery3
                        ) AS resultado
                        ORDER BY order_column, Numero ASC;";

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();

                da = new SqlDataAdapter(query, cnn);
                da.Fill(ds, "ResultEmpregador");

                cnn.Close();
                da.Dispose();
            }
            ds.Tables["ResultEmpregador"].Rows[0][1] = "Presidente";

            for (int i = 2, j = 1; i <= dim.Efetivo; i++, j++)
            {
                ds.Tables["ResultEmpregador"].Rows[i - 1][1] = $"{j}º Membro";
            }

            for (int i = dim.Efetivo + 1, j = 1; i <= (dim.Efetivo + dim.Suplente); i++, j++)
            {
                ds.Tables["ResultEmpregador"].Rows[i - 1][1] = $"{j}º Suplente";
            }
            return ds;
        }

        private DataSet DataSourceVotos()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("ResultVotos");
            table.Columns.Add("VotosNulos", Type.GetType("System.Int32"));
            table.Columns.Add("VotosBrancos", Type.GetType("System.Int32"));
            table.Columns.Add("TotalDeVotos", Type.GetType("System.Int32"));
            table.Columns.Add("TotalFuncionarios", Type.GetType("System.Int32"));

            ds.Tables.Add(table);

            string query = $@"SELECT 
                                    VotosNulos, 
                                    VotosBrancos, 
                                    TotalDeVotos, 
                                    (SELECT COUNT(*) 
                                        FROM {Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper()}
                                        .dbo.tblEMPREGADO WHERE hDT_DEM IS NULL AND nID_EMPR = {this.cipa.IdCliente.Id}) 
                                    AS TotalFuncionarios
                              FROM EleicaoCipa 
                              WHERE IdEleicaoCipa = {this.eventoCipa.Id}";

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();

                da = new SqlDataAdapter(query, cnn);
                da.Fill(ds, "ResultVotos");

                cnn.Close();
                da.Dispose();
            }
            return ds;
        }
        #endregion

        #region EmpregadorApuracao
        private DataSet DataSourceRptEmpregadorApuracao()
        {
            DataSet ds = new DataSet();

            DataTable table = new DataTable("Result");
            table.Columns.Add("tEmpregador", Type.GetType("System.String"));
            ds.Tables.Add(table);

            DimensionamentoCipa dim = cipa.IdCliente.GetDimensionamentoCipa();

            int classificados = dim.Efetivo + dim.Suplente;

            DataRow newRow;

            for (int i = 0; i < classificados; i++)
            {
                newRow = ds.Tables[0].NewRow();
                if (i <= dim.Efetivo - 1)
                {
                    if (i == 0)
                        newRow["tEmpregador"] = "Presidente :";
                    else
                        newRow["tEmpregador"] = i + "º Membro :";
                }
                else
                    newRow["tEmpregador"] = (i - dim.Efetivo + 1) + "º Suplente :";

                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
        #endregion
        
        #endregion
    }

    public class CandidatosApurados
    {
        public int IdEmpregado { get; set; }
        public string NomeEmpregado { get; set; }
        public DateTime? DataAdmissao { get; set; }
        public int IdParticipantesEleicaoCipa { get; set; }
        public int IdCipa { get; set; }
        public int QtdeVotosRecebidoPeloCandidato { get; set; }
        public bool? IsVicePresidente { get; set; }
    }
}
