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
    public class DataSourceQuadrosNR4 : DataSourceBase
    {
        private Cliente cliente;
        private int ano, numBlankRows;
        private float JorTrabEmpresa, MediaAnualEmpreg;
        private bool Alocados = false, insalubridadeFromPPRA = false;

        public DataSourceQuadrosNR4(Cliente cliente, int ano)
            : this(cliente, ano, 7.33F, 0F)
        {

        }

        public DataSourceQuadrosNR4(Cliente cliente, int ano, float JorTrabEmpresa)
            : this(cliente, ano, JorTrabEmpresa, 0F)
        {

        }

        public DataSourceQuadrosNR4(Cliente cliente, int ano, bool insalubridadeFromPPRA)
            : this(cliente, ano, 7.33F, 0F, false, insalubridadeFromPPRA, 5)
        {

        }

        public DataSourceQuadrosNR4(Cliente cliente, int ano, bool insalubridadeFromPPRA, int numBlankRows)
            : this(cliente, ano, 7.33F, 0F, false, insalubridadeFromPPRA, numBlankRows)
        {

        }

        public DataSourceQuadrosNR4(Cliente cliente, int ano, float JorTrabEmpresa, float MediaAnualEmpreg)
            : this(cliente, ano, JorTrabEmpresa, MediaAnualEmpreg, false, false, 5)
        {

        }

        public DataSourceQuadrosNR4(Cliente cliente, int ano, float JorTrabEmpresa, float MediaAnualEmpreg, bool Alocados, bool insalubridadeFromPPRA, int numBlankRows)
        {
            this.cliente = cliente;
            this.ano = ano;
            this.JorTrabEmpresa = JorTrabEmpresa;
            this.Alocados = Alocados;
            this.insalubridadeFromPPRA = insalubridadeFromPPRA;
            this.numBlankRows = numBlankRows;

            if (MediaAnualEmpreg != 0F)
                this.MediaAnualEmpreg = MediaAnualEmpreg;
            else
                this.MediaAnualEmpreg = cliente.GetMediaAnualEmpregados(ano, Alocados);
        }

        public RptQuadrosNR4 GetReport()
        {

           
            DataSet dsQuadroIII = new DataSet();
            DataSet dsQuadroVI = new DataSet();

            PopulaDSQuadroIIIVI(dsQuadroIII, dsQuadroVI);
            
            RptQuadrosNR4 report = new RptQuadrosNR4();
            report.OpenSubreport("QuadroIII").SetDataSource(dsQuadroIII);
            report.OpenSubreport("QuadroIV").SetDataSource(GetDSQuadroIV());
            report.OpenSubreport("QuadroV").SetDataSource(GetDSQuadroV());
            report.OpenSubreport("QuadroVI").SetDataSource(dsQuadroVI);
            report.SetDataSource(GetDadosBasicos());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

        #region DadosBasicos

        private DataSet GetDadosBasicos()
        {
            Endereco endereco = cliente.GetEndereco();
            Municipio municipio = cliente.GetMunicipio();
            municipio.IdUnidadeFederativa.Find();

            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");

            table.Columns.Add("RazaoSocial", Type.GetType("System.String"));
            table.Columns.Add("Endereco", Type.GetType("System.String"));
            table.Columns.Add("Cidade", Type.GetType("System.String"));
            table.Columns.Add("Estado", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            ds.Tables.Add(table);

            DataRow row = ds.Tables[0].NewRow();

            row["RazaoSocial"] = cliente.NomeCompleto;
            row["Endereco"] = endereco.GetEndereco();
            row["Cidade"] = municipio.NomeCompleto;
            row["Estado"] = municipio.IdUnidadeFederativa.NomeAbreviado;
            row["CNPJ"] = cliente.NomeCodigo;

            ds.Tables[0].Rows.Add(row);

            return ds;
        }
        #endregion

        #region DataTables

        private DataTable GetTableQuadroIII()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("NomeSetor", typeof(string));
            table.Columns.Add("NAbsoluto", typeof(int));
            table.Columns.Add("NAbsCAfastMenor15", typeof(int));
            table.Columns.Add("NAbsCAfastMaior15", typeof(int));
            table.Columns.Add("NAbsSAfast", typeof(int));
            table.Columns.Add("IndiceRelativo", typeof(float));
            table.Columns.Add("DiasHomemPerdidos", typeof(float));
            table.Columns.Add("TaxaFrequencia", typeof(float));
            table.Columns.Add("Obitos", typeof(int));
            table.Columns.Add("IndiceAvaliacao", typeof(float));
            table.Columns.Add("TotalEstabelecimento", typeof(string));
            table.Columns.Add("Observacoes", typeof(string));

            return table;
        }

        private DataTable GetTableQuadroIV()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("NomeDoenca", typeof(string));
            table.Columns.Add("NAbsolutoCasos", typeof(int));
            table.Columns.Add("SetoresAtividades", typeof(string));
            table.Columns.Add("NRelativoCasos", typeof(float));
            table.Columns.Add("Obitos", typeof(int));
            table.Columns.Add("NTrabTransOutroSetor", typeof(int));
            table.Columns.Add("NTrabDefinIncapacitados", typeof(int));
            table.Columns.Add("Observacoes", typeof(string));

            return table;
        }

        private DataTable GetTableQuadroV()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("NomeSetor", typeof(string));
            table.Columns.Add("AgentesIdentificados", typeof(string));
            table.Columns.Add("Intensidade", typeof(string));
            table.Columns.Add("NumTrabalhadores", typeof(string));
            table.Columns.Add("Observacoes", typeof(string));

            return table;
        }

        private DataTable GetTableQuadroVI()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("NomeSetor", typeof(string));
            table.Columns.Add("NumAcidentes", typeof(int));
            table.Columns.Add("PerdaMaterial", typeof(float));
            table.Columns.Add("AcidCSVitima", typeof(string));
            table.Columns.Add("TotalEstabelecimento", typeof(string));
            table.Columns.Add("Observacoes", typeof(string));
            table.Columns.Add("ObservacoesRodape", typeof(string));

            return table;
        }

        #endregion

        #region Quadros

        private void PopulaDSQuadroIIIVI(DataSet dsQuadroIII, DataSet dsQuadroVI)
        {
            DataRow rowQuadroIII, rowQuadroVI;
            DataTable tableQuadroIII = GetTableQuadroIII();
            DataTable tableQuadroVI = GetTableQuadroVI();
            int diasUteis = 0;

            dsQuadroIII.Tables.Add(tableQuadroIII);
            dsQuadroVI.Tables.Add(tableQuadroVI);

            //setores que possuem acidentes no ano selecionado verificando se possuem CAT cadastrada e ainda aqueles que tem afastamento sem volta
            StringBuilder strSetorAcidente = new StringBuilder();
            strSetorAcidente.Append("nID_SETOR IN (SELECT IdSetor FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Acidente WHERE (YEAR(DataAcidente)=" + ano);
            strSetorAcidente.Append(" OR IdAcidente IN (SELECT IdAcidente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Afastamento WHERE DataVolta IS NULL OR YEAR(DataVolta)=" + ano + "))");
            if (Alocados)
                strSetorAcidente.Append(" AND IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPR=" + cliente.Id + "))");
            else
                strSetorAcidente.Append(" AND IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPR=" + cliente.Id + ")");
            strSetorAcidente.Append(" AND IdCAT IS NOT NULL");
            strSetorAcidente.Append(" AND IndTipoAcidente=" + (int)TipoAcidente.Tipico + ")");
            strSetorAcidente.Append(" ORDER BY tNO_STR_EMPR");
            DataSet dsSetorAcidente = new Setor().Get(strSetorAcidente.ToString());

            StringBuilder strAcidenteTrajeto = new StringBuilder();
            if (Alocados)
                strAcidenteTrajeto.Append(" IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPR=" + cliente.Id + "))");
            else
                strAcidenteTrajeto.Append(" IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPR=" + cliente.Id + ")");
            strAcidenteTrajeto.Append(" AND (YEAR(DataAcidente)=" + ano);
            strAcidenteTrajeto.Append(" OR IdAcidente IN (SELECT IdAcidente FROM Afastamento WHERE DataVolta IS NULL OR YEAR(DataVolta)=" + ano + "))");
            strAcidenteTrajeto.Append(" AND IdCAT IS NOT NULL");
            strAcidenteTrajeto.Append(" AND IndTipoAcidente=" + (int)TipoAcidente.Trajeto);

            ArrayList alAcidenteTrajeto = new Acidente().Find(strAcidenteTrajeto.ToString());

            if (dsSetorAcidente.Tables[0].Rows.Count == 0 && alAcidenteTrajeto.Count == 0)
            {
                rowQuadroIII = dsQuadroIII.Tables[0].NewRow();
                rowQuadroIII["Observacoes"] = "Não ocorreram Acidentes com Vítimas no período analisado";
                dsQuadroIII.Tables[0].Rows.Add(rowQuadroIII);

                rowQuadroVI = dsQuadroVI.Tables[0].NewRow();
                rowQuadroVI["ObservacoesRodape"] = "Não ocorreram Acidentes sem Vítimas no período analisado";
                dsQuadroVI.Tables[0].Rows.Add(rowQuadroVI);
            }
            else
            {
                int AfasMenor15 = 0, AfasMaior15 = 0, SemAfas = 0, numObito = 0;
                double horasAfastado = 0, diasAfastado = 0;
                float perdaMaterial = 0F;
                StringBuilder SBobservacaoAcidente = new StringBuilder();

                for (DateTime dataInicio = new DateTime(ano, 1, 1); dataInicio <= new DateTime(ano, 12, 31); dataInicio = dataInicio.AddDays(1))
                    if (Feriado.IsFeriado(dataInicio, cliente.GetMunicipio()))
                        continue;
                    else if (Feriado.IsDomingo(dataInicio))
                        continue;
                    else
                        diasUteis += 1;

                foreach (DataRow rowSetor in dsSetorAcidente.Tables[0].Select())
                {
                    AfasMenor15 = 0;
                    AfasMaior15 = 0;
                    SemAfas = 0;
                    numObito = 0;

                    horasAfastado = 0;
                    diasAfastado = 0;

                    perdaMaterial = 0F;
                    SBobservacaoAcidente = new StringBuilder();

                    //Seleciona os Acidentes do Setor em questão
                    StringBuilder strAcidente = new StringBuilder();
                    if (Alocados)
                        strAcidente.Append("IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPR=" + cliente.Id + "))");
                    else
                        strAcidente.Append("IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPR=" + cliente.Id + ")");
                    strAcidente.Append(" AND IdSetor=" + rowSetor["nID_SETOR"]);
                    strAcidente.Append(" AND IdCAT IS NOT NULL");
                    strAcidente.Append(" AND IndTipoAcidente=" + (int)TipoAcidente.Tipico);
                    strAcidente.Append(" AND YEAR(DataAcidente)=" + ano);

                    ArrayList alAcidente = new Acidente().Find(strAcidente.ToString());

                    StringBuilder strAfastamentoSemVolta = new StringBuilder();
                    strAfastamentoSemVolta.Append("(DataVolta IS NULL OR YEAR(DataVolta)=" + ano + ")");
                    strAfastamentoSemVolta.Append(" AND IdAcidente IN (SELECT IdAcidente FROM Acidente WHERE ");
                    if (Alocados)
                        strAfastamentoSemVolta.Append(" IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPR=" + cliente.Id + "))");
                    else
                        strAfastamentoSemVolta.Append(" IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPR=" + cliente.Id + ")");
                    strAfastamentoSemVolta.Append(" AND YEAR(DataAcidente)<" + ano);
                    strAfastamentoSemVolta.Append(" AND IdSetor=" + rowSetor["nID_SETOR"]);
                    strAfastamentoSemVolta.Append(" AND IdCAT IS NOT NULL");
                    strAfastamentoSemVolta.Append(" AND IndTipoAcidente=" + (int)TipoAcidente.Tipico + ")");

                    ArrayList alAfastamentoSemVolta = new Afastamento().Find(strAfastamentoSemVolta.ToString());
                    
                    foreach (Acidente acidente in alAcidente)
                    {
                        acidente.IdCAT.Find();
                        if (acidente.IdCAT.hasMorte)
                            numObito += 1;

                        perdaMaterial += acidente.PerdaMaterial;

                        if (!SBobservacaoAcidente.ToString().Equals(string.Empty))
                            SBobservacaoAcidente.Append(", ");

                        SBobservacaoAcidente.Append(acidente.Observacoes);

                        ArrayList alAfastamento = new Afastamento().Find("IdAcidente=" + acidente.Id);

                        if (alAfastamento.Count.Equals(0))
                            SemAfas += 1;

                        foreach (Afastamento afastamentoAcidente in alAfastamento)
                        {
                            TimeSpan timespan = new TimeSpan();

                            if (afastamentoAcidente.DataVolta != new DateTime() && afastamentoAcidente.DataVolta != new DateTime(1753, 1, 1))
                                timespan = afastamentoAcidente.DataVolta.Subtract(afastamentoAcidente.DataInicial);
                            else
                                timespan = new DateTime(ano + 1, 01, 01).Subtract(afastamentoAcidente.DataInicial);

                            if (timespan.TotalDays <= 15)
                                AfasMenor15 += 1;
                            else if (timespan.TotalDays > 15)
                                AfasMaior15 += 1;

                            if (Convert.ToSingle(timespan.TotalHours) < JorTrabEmpresa)
                                horasAfastado += timespan.TotalHours;
                            else
                                diasAfastado += timespan.TotalDays;
                        }
                    }

                    foreach (Afastamento afastamento in alAfastamentoSemVolta)
                    {
                        TimeSpan timespan = new TimeSpan();

                        if (afastamento.DataVolta != new DateTime() && afastamento.DataVolta != new DateTime(1753, 1, 1))
                            timespan = afastamento.DataVolta.Subtract(new DateTime(ano - 1, 12, 31));
                        else
                            timespan = new DateTime(ano, 12, 31).Subtract(new DateTime(ano - 1, 12, 31));

                        diasAfastado += timespan.TotalDays;
                    }
                    
                    rowQuadroIII = dsQuadroIII.Tables[0].NewRow();
                    rowQuadroIII["NomeSetor"] = rowSetor["tNO_STR_EMPR"];
                    rowQuadroIII["NAbsoluto"] = alAcidente.Count;
                    rowQuadroIII["NAbsCAfastMenor15"] = AfasMenor15;
                    rowQuadroIII["NAbsCAfastMaior15"] = AfasMaior15;
                    rowQuadroIII["NAbsSAfast"] = SemAfas;
                    rowQuadroIII["IndiceRelativo"] = (Convert.ToSingle(alAcidente.Count) / MediaAnualEmpreg) * 100F;
                    rowQuadroIII["DiasHomemPerdidos"] = (Convert.ToSingle(horasAfastado) + (Convert.ToSingle(diasAfastado) * JorTrabEmpresa)) / JorTrabEmpresa;
                    rowQuadroIII["TaxaFrequencia"] = (Convert.ToSingle(alAcidente.Count) * 1000000F) / (MediaAnualEmpreg * JorTrabEmpresa * Convert.ToSingle(diasUteis));
                    rowQuadroIII["Obitos"] = numObito;
                    if (alAcidente.Count > 0)
                        rowQuadroIII["IndiceAvaliacao"] = ((Convert.ToSingle(horasAfastado) + (Convert.ToSingle(diasAfastado) * JorTrabEmpresa)) / JorTrabEmpresa) / Convert.ToSingle(alAcidente.Count);
                    else
                        rowQuadroIII["IndiceAvaliacao"] = 0F;
                    rowQuadroIII["TotalEstabelecimento"] = "(" + MediaAnualEmpreg.ToString("00.0") + ")";
                    dsQuadroIII.Tables[0].Rows.Add(rowQuadroIII);

                    if (alAcidente.Count > 0)
                    {
                        rowQuadroVI = dsQuadroVI.Tables[0].NewRow();
                        rowQuadroVI["NomeSetor"] = rowSetor["tNO_STR_EMPR"];
                        rowQuadroVI["NumAcidentes"] = alAcidente.Count;
                        rowQuadroVI["PerdaMaterial"] = perdaMaterial / 1000F;
                        rowQuadroVI["AcidCSVitima"] = SemAfas + "/" + (AfasMenor15 + AfasMaior15);
                        rowQuadroVI["TotalEstabelecimento"] = "(" + MediaAnualEmpreg.ToString("00.0") + ")";
                        rowQuadroVI["Observacoes"] = SBobservacaoAcidente.ToString();
                        dsQuadroVI.Tables[0].Rows.Add(rowQuadroVI);
                    }
                }

                int NumAbsAcidenteTrajeto = 0;
                AfasMenor15 = 0;
                AfasMaior15 = 0;
                SemAfas = 0;
                numObito = 0;

                horasAfastado = 0;
                diasAfastado = 0;

                perdaMaterial = 0F;
                SBobservacaoAcidente = new StringBuilder();

                foreach (Acidente acidenteTrajeto in alAcidenteTrajeto)
                    if (acidenteTrajeto.DataAcidente.Year == ano)
                    {
                        NumAbsAcidenteTrajeto += 1;

                        perdaMaterial += acidenteTrajeto.PerdaMaterial;

                        if (!SBobservacaoAcidente.ToString().Equals(string.Empty))
                            SBobservacaoAcidente.Append(", ");

                        SBobservacaoAcidente.Append(acidenteTrajeto.Observacoes);

                        acidenteTrajeto.IdCAT.Find();
                        if (acidenteTrajeto.IdCAT.hasMorte)
                            numObito += 1;

                        ArrayList alAfastamento = new Afastamento().Find("IdAcidente=" + acidenteTrajeto.Id);

                        if (alAfastamento.Count.Equals(0))
                            SemAfas += 1;

                        foreach (Afastamento afastamentoAcidenteTrajeto in alAfastamento)
                        {
                            TimeSpan timespan = new TimeSpan();

                            if (afastamentoAcidenteTrajeto.DataVolta != new DateTime() && afastamentoAcidenteTrajeto.DataVolta != new DateTime(1753, 1, 1))
                                timespan = afastamentoAcidenteTrajeto.DataVolta.Subtract(afastamentoAcidenteTrajeto.DataInicial);
                            else
                                timespan = new DateTime(ano + 1, 01, 01).Subtract(afastamentoAcidenteTrajeto.DataInicial);

                            if (timespan.TotalDays <= 15)
                                AfasMenor15 += 1;
                            else if (timespan.TotalDays > 15)
                                AfasMaior15 += 1;

                            if (Convert.ToSingle(timespan.TotalHours) < JorTrabEmpresa)
                                horasAfastado += timespan.TotalHours;
                            else
                                diasAfastado += timespan.TotalDays;
                        }
                    }
                    else if (acidenteTrajeto.DataAcidente.Year < ano)
                    {
                        ArrayList alAfastamento = new Afastamento().Find("IdAcidente=" + acidenteTrajeto.Id);

                        foreach (Afastamento afastamento in alAfastamento)
                        {
                            TimeSpan timespan = new TimeSpan();

                            if (afastamento.DataVolta != new DateTime() && afastamento.DataVolta != new DateTime(1753, 1, 1))
                                timespan = afastamento.DataVolta.Subtract(new DateTime(ano - 1, 12, 31));
                            else
                                timespan = new DateTime(ano, 12, 31).Subtract(new DateTime(ano - 1, 12, 31));

                            diasAfastado += timespan.TotalDays;
                        }
                    }

                if (alAcidenteTrajeto.Count > 0)
                {
                    rowQuadroIII = dsQuadroIII.Tables[0].NewRow();
                    rowQuadroIII["NomeSetor"] = "Acidente de Trajeto";
                    rowQuadroIII["NAbsoluto"] = NumAbsAcidenteTrajeto;
                    rowQuadroIII["NAbsCAfastMenor15"] = AfasMenor15;
                    rowQuadroIII["NAbsCAfastMaior15"] = AfasMaior15;
                    rowQuadroIII["NAbsSAfast"] = SemAfas;
                    rowQuadroIII["IndiceRelativo"] = (Convert.ToSingle(NumAbsAcidenteTrajeto) / MediaAnualEmpreg) * 100F;
                    rowQuadroIII["DiasHomemPerdidos"] = (Convert.ToSingle(horasAfastado) + (Convert.ToSingle(diasAfastado) * JorTrabEmpresa)) / JorTrabEmpresa;
                    rowQuadroIII["TaxaFrequencia"] = (Convert.ToSingle(NumAbsAcidenteTrajeto) * 1000000F) / (MediaAnualEmpreg * JorTrabEmpresa * Convert.ToSingle(diasUteis));
                    rowQuadroIII["Obitos"] = numObito;
                    if (NumAbsAcidenteTrajeto > 0)
                        rowQuadroIII["IndiceAvaliacao"] = ((Convert.ToSingle(horasAfastado) + (Convert.ToSingle(diasAfastado) * JorTrabEmpresa)) / JorTrabEmpresa) / Convert.ToSingle(NumAbsAcidenteTrajeto);
                    else
                        rowQuadroIII["IndiceAvaliacao"] = 0F;
                    rowQuadroIII["TotalEstabelecimento"] = "(" + MediaAnualEmpreg.ToString("00.0") + ")";
                    dsQuadroIII.Tables[0].Rows.Add(rowQuadroIII);

                    if (NumAbsAcidenteTrajeto > 0)
                    {
                        rowQuadroVI = dsQuadroVI.Tables[0].NewRow();
                        rowQuadroVI["NomeSetor"] = "Acidente de Trajeto";
                        rowQuadroVI["NumAcidentes"] = NumAbsAcidenteTrajeto;
                        rowQuadroVI["PerdaMaterial"] = perdaMaterial / 1000F;
                        rowQuadroVI["AcidCSVitima"] = SemAfas + "/" + (AfasMenor15 + AfasMaior15);
                        rowQuadroVI["TotalEstabelecimento"] = "(" + MediaAnualEmpreg.ToString("00.0") + ")";
                        rowQuadroVI["Observacoes"] = SBobservacaoAcidente.ToString();
                        dsQuadroVI.Tables[0].Rows.Add(rowQuadroVI);
                    }
                }

                if (dsQuadroVI.Tables[0].Rows.Count.Equals(0))
                {
                    rowQuadroVI = dsQuadroVI.Tables[0].NewRow();
                    rowQuadroVI["ObservacoesRodape"] = "Não ocorreram Acidentes sem Vítimas no período analisado";
                    dsQuadroVI.Tables[0].Rows.Add(rowQuadroVI);
                }
            }
        }
        
        private DataSet GetDSQuadroV()
        {   
            DataRow row;
            DataTable table = GetTableQuadroV();
            DataSet ds = new DataSet();

            ds.Tables.Add(table);

            if (insalubridadeFromPPRA)
            {
                LaudoTecnico laudo = LaudoTecnico.GetUltimoLaudo(cliente.Id);

                ArrayList alGHE = new Ghe().Find("nID_LAUD_TEC=" + laudo.Id
                    + " AND nID_FUNC IN (SELECT nID_FUNC FROM tblPPRA1 WHERE gINSALUBRE=1 AND gNET=0)"
                    + " ORDER BY tNO_FUNC");

                if (alGHE.Count.Equals(0))
                {
                    row = ds.Tables[0].NewRow();
                    row["Observacoes"] = "Insalubridade Inexistente";
                    ds.Tables[0].Rows.Add(row);
                }
                else
                    foreach (Ghe ghe in alGHE)
                    {
                        ArrayList alRisco = new PPRA().Find("nID_FUNC=" + ghe.Id
                            + " AND gINSALUBRE=1 AND gNET=0"
                            + " ORDER BY nID_RSC, (SELECT Nome FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.AgenteQuimico WHERE AgenteQuimico.IdAgenteQuimico=tblPPRA1.nID_AG_NCV)");

                        foreach (PPRA risco in alRisco)
                        {
                            risco.nID_AG_NCV.Find();

                            row = ds.Tables[0].NewRow();
                            row["NomeSetor"] = ghe.tNO_FUNC;
                            row["AgentesIdentificados"] = risco.GetNomeAgente();
                            row["Intensidade"] = risco.GetGrauInsalubridade();
                            row["NumTrabalhadores"] = ghe.GetNumeroEmpregadosExpostos(true).ToString();
                            ds.Tables[0].Rows.Add(row);
                        }
                    }
            }
            else
                for (int i = 0; i < numBlankRows; i++)
                {
                    row = ds.Tables[0].NewRow();
                    row["NomeSetor"] = " ";
                    ds.Tables[0].Rows.Add(row);
                }
            
            #region GalvaniPaulinia
            //ArrayList alSetores = new ArrayList();
            //alSetores.Add("Ácido Sulfúrico#2");
            //alSetores.Add("Civil#23");
            //alSetores.Add("Civil - Obras Novas#13");
            //alSetores.Add("Civil - Tratorista#1");
            //alSetores.Add("Comunicação e Marketing#3");
            //alSetores.Add("Controle de Qualidade#3");
            //alSetores.Add("Desenvolvimento de Produtos#3");
            //alSetores.Add("Engenharia de Manutenção#4");
            //alSetores.Add("Estocagem e Movimentação de Produtos#49");
            //alSetores.Add("ETA/DESMI#10");
            //alSetores.Add("ETEL#2");
            //alSetores.Add("Fábrica 1#4");
            //alSetores.Add("Fábrica 2#3");
            //alSetores.Add("Geração de Energia Elétrica#10");
            //alSetores.Add("Gestão#2");
            //alSetores.Add("Granulação 1 (GI)#3");
            //alSetores.Add("Granulação 2 (GII)#6");
            //alSetores.Add("Granulação 3 (GIII)#16");
            //alSetores.Add("Inativos - Afastados INSS#6");
            //alSetores.Add("Laboratório#14");
            //alSetores.Add("Limpeza e Conservação#17");
            //alSetores.Add("Logística Interna#1");
            //alSetores.Add("Manutenção#1");
            //alSetores.Add("Manutenção Mecânica#1");
            //alSetores.Add("Meio Ambiente#5");
            //alSetores.Add("Mistura de Grânulos e Ensaque#6");
            //alSetores.Add("Oficina#13");
            //alSetores.Add("Pintura/Laminação/Vulcanização#6");
            //alSetores.Add("Processos/Qualidade#1");
            //alSetores.Add("Produção de Ácido Sulfúrico#28");
            //alSetores.Add("Produção de Fertilizantes#17");
            //alSetores.Add("Projetos#7");
            //alSetores.Add("RH#3");
            //alSetores.Add("Segurança Industrial#4");
            //alSetores.Add("Superfosfato 1#5");
            //alSetores.Add("Superfosfato 2#5");
            //alSetores.Add("Tubulações#235");

            //foreach (string setor in alSetores)
            //{
            //    row = ds.Tables[0].NewRow();
            //    row["NomeSetor"] = setor.Substring(0, setor.IndexOf("#"));
            //    row["AgentesIdentificados"] = "Ácido Sulfúrico, Enxofre, Ruído";
            //    row["Intensidade"] = "Grau Médio";
            //    row["NumTrabalhadores"] = setor.Substring(setor.IndexOf("#") + 1);
            //    ds.Tables[0].Rows.Add(row);
            //}
            #endregion

            #region EstrePaulinia
            //ArrayList alSetores = new ArrayList();
            //string[] strSetor; 

            //strSetor = new string[] {"Controladoria", "Ruído e Calor", "2"};
            //alSetores.Add(strSetor);
            //strSetor = new string[] { "Balança", "Ruído e Calor", "4" };
            //alSetores.Add(strSetor);
            //strSetor = new string[] { "Lubrificação", "Ruído, Calor, Agentes Químicos e Biológicos", "2" };
            //alSetores.Add(strSetor);
            //strSetor = new string[] { "Mecânica", "Ruído, Calor, Agentes Químicos e Biológicos", "5" };
            //alSetores.Add(strSetor);
            //strSetor = new string[] { "Operacional", "Ruído, Calor, Biológicos e Particulados", "5" };
            //alSetores.Add(strSetor);
            //strSetor = new string[] { "Operacional Máquina", "Ruído, Calor, Biológicos e Particulados", "9" };
            //alSetores.Add(strSetor);
            //strSetor = new string[] { "Transporte", "Ruído, Calor e Agentes Biológicos", "5" };
            //alSetores.Add(strSetor);
            //strSetor = new string[] { "Segurança do Trabalho", "Ruído, Calor e Agentes Biológicos", "1" };
            //alSetores.Add(strSetor);
            //strSetor = new string[] { "Manutenção Civil", "Ruído, Calor, Tintas e Solventes, Cimento e Cal e Particulados", "4" };
            //alSetores.Add(strSetor);
            //strSetor = new string[] { "Britagem", "Ruído, Calor e Particulado", "1" };
            //alSetores.Add(strSetor);

            //foreach (string[] setor in alSetores)
            //{
            //    row = ds.Tables[0].NewRow();
            //    row["NomeSetor"] = setor[0];
            //    row["AgentesIdentificados"] = setor[1];
            //    row["Intensidade"] = "Grau Médio";
            //    row["NumTrabalhadores"] = setor[2];
            //    ds.Tables[0].Rows.Add(row);
            //}
            #endregion

            return ds;
        }             

        private DataSet GetDSQuadroIV()
        {
            DataRow row;
            DataTable table = GetTableQuadroIV();
            DataSet ds = new DataSet();

            ds.Tables.Add(table);

            StringBuilder strDoencas = new StringBuilder();

            strDoencas.Append("IdCID IN (SELECT IdCID FROM Acidente WHERE IndTipoAcidente=" + (int)TipoAcidente.Doenca);

            if (Alocados)
                strDoencas.Append(" AND IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPR=" + cliente.Id + "))");
            else
                strDoencas.Append(" AND IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPR=" + cliente.Id + ")");

            strDoencas.Append(" AND IdCAT IS NOT NULL");
            strDoencas.Append(" AND YEAR(DataAcidente)=" + ano + ")");
            strDoencas.Append(" ORDER BY Descricao");

            DataSet dsDoencas = new CID().Get(strDoencas.ToString());

            if (dsDoencas.Tables[0].Rows.Count == 0)
            {
                row = ds.Tables[0].NewRow();
                row["Observacoes"] = "Não ocorreram Doenças Ocupacionais no período analisado";
                ds.Tables[0].Rows.Add(row);
            }
            else
            {
                foreach (DataRow rowDoenca in dsDoencas.Tables[0].Select())
                {
                    int numCasos = 0, numObitos = 0, numTrabTransf = 0, numTrabIncapac = 0;
                    string setoresPort = string.Empty;

                    StringBuilder strAcidente = new StringBuilder();

                    if (Alocados)
                        strAcidente.Append("IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPR=" + cliente.Id + "))");
                    else
                        strAcidente.Append("IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPR=" + cliente.Id + ")");

                    strAcidente.Append(" AND YEAR(DataAcidente)=" + ano);
                    strAcidente.Append(" AND IdCAT IS NOT NULL");
                    strAcidente.Append(" AND IdCID=" + rowDoenca["IdCID"]);

                    ArrayList alAcidente = new Acidente().Find(strAcidente.ToString());

                    foreach (Acidente acidDoenca in alAcidente)
                    {
                        numCasos += 1;

                        acidDoenca.IdSetor.Find();
                        if (setoresPort == string.Empty)
                            setoresPort = acidDoenca.IdSetor.tNO_STR_EMPR;
                        else
                            setoresPort += ", " + acidDoenca.IdSetor.tNO_STR_EMPR;

                        acidDoenca.IdCAT.Find();
                        if (acidDoenca.IdCAT.hasMorte)
                            numObitos += 1;

                        if (acidDoenca.isTransfSetor)
                            numTrabTransf += 1;
                        if (acidDoenca.isAposInval)
                            numTrabIncapac += 1;
                    }

                    row = ds.Tables[0].NewRow();
                    row["NomeDoenca"] = rowDoenca["Descricao"];
                    row["NAbsolutoCasos"] = numCasos;
                    row["SetoresAtividades"] = setoresPort;
                    row["NRelativoCasos"] = (Convert.ToSingle(numCasos) / MediaAnualEmpreg) * 100F;
                    row["Obitos"] = numObitos;
                    row["NTrabTransOutroSetor"] = numTrabTransf;
                    row["NTrabDefinIncapacitados"] = numTrabIncapac;

                    ds.Tables[0].Rows.Add(row);
                }
            }
            return ds;
        }
        
        #endregion
    }
}
