using System;
using System.Data;
using System.Text;
using System.Collections;
using Ilitera.Opsa.Data;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceAudiometria : Ilitera.Common.DataSourceBase
    {
        private Audiometria audiometria;
        private AudiometriaAudiograma audiogramaOD;
        private AudiometriaAudiograma audiogramaOE;
        private Cliente cliente;
        private Boolean SemData;

        public DataSourceAudiometria(Audiometria audiometria)
        {
            this.audiometria = audiometria;
            this.SemData = false;

            audiogramaOD = this.audiometria.GetAudiograma(Orelha.Direita);

            audiogramaOE = this.audiometria.GetAudiograma(Orelha.Esquerda);

            if (audiometria.IdEmpregado.nID_EMPR == null)
                audiometria.IdEmpregado.Find();

            this.cliente = audiometria.IdEmpregado.nID_EMPR;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

        public DataSourceAudiometria(Audiometria audiometria,  Boolean xSemData)
        {
            this.audiometria = audiometria;
            this.SemData = xSemData;

            audiogramaOD = this.audiometria.GetAudiograma(Orelha.Direita);

            audiogramaOE = this.audiometria.GetAudiograma(Orelha.Esquerda);

            if (audiometria.IdEmpregado.nID_EMPR == null)
                audiometria.IdEmpregado.Find();

            this.cliente = audiometria.IdEmpregado.nID_EMPR;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

        #region GetReport

        public RptAudiometria GetReport(bool IsAudiograma)
        {
            RptAudiometria report = new RptAudiometria();

            report.Subreports["RptAudiometriaHeader.rpt"].SetDataSource(DataSourceHeader());
            report.Subreports["RptAudiometriaAnamnese.rpt"].SetDataSource(DataSourceAnamnese());
            report.Subreports["RptAudiometriaQuadro.rpt"].SetDataSource(DataSourceQuadro());
            report.Subreports["RptAudiometriaAudiograma.rpt"].SetDataSource(DataSourceAudiograma());
            report.Subreports["RptAudiometriaFooter.rpt"].SetDataSource(DataSourceFooter());

            report.SetDataSource(DataSourceBase(IsAudiograma));
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }
        #endregion

        #region Tables

        private DataTable GetDataTableAudiometria()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("IdAudiometria", Type.GetType("System.Int32"));
            table.Columns.Add("IsAudiograma", Type.GetType("System.Boolean"));

            return table;
        }

        private DataTable GetDataTableAnamnese()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("IdAudiometria", Type.GetType("System.Int32"));
            table.Columns.Add("HasSenteDificuldadeAuditiva", Type.GetType("System.String"));
            table.Columns.Add("HasTemPresencaAcufenos", Type.GetType("System.String"));
            table.Columns.Add("HasTemPresencaVertigens", Type.GetType("System.String"));
            table.Columns.Add("HasFamiliaresComProblemaAuditivo", Type.GetType("System.String"));
            table.Columns.Add("IsAntecedenteCaxumba", Type.GetType("System.String"));
            table.Columns.Add("IsAntecedenteSarampo", Type.GetType("System.String"));
            table.Columns.Add("IsAntecedenteMenigite", Type.GetType("System.String"));
            table.Columns.Add("HasFazUsoMedicacao", Type.GetType("System.String"));
            table.Columns.Add("FazUsoMedicacaoQual", Type.GetType("System.String"));
            table.Columns.Add("TempoExposicaoRuidoOcupacional", Type.GetType("System.String"));
            table.Columns.Add("TempoUsoProtetorAuricular", Type.GetType("System.String"));
            table.Columns.Add("HasExposicaoRuidoExtraLaboral", Type.GetType("System.String"));
            table.Columns.Add("HasExposicaoProdutosOtotoxicos", Type.GetType("System.String"));
            table.Columns.Add("ExposicaoProdutosOtotoxicosQual", Type.GetType("System.String"));
            table.Columns.Add("HasAlteracaoMeatosAcusticos", Type.GetType("System.String"));
            table.Columns.Add("AlteracaoMeatosAcusticosQual", Type.GetType("System.String"));
            table.Columns.Add("Observacao", Type.GetType("System.String"));

            return table;
        }

        private DataTable GetDataTableAudiograma()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("IdAudiometria", Type.GetType("System.Int32"));
            table.Columns.Add("RepousoAuditivo", Type.GetType("System.String"));
            table.Columns.Add("DataAudiometria", Type.GetType("System.String"));
            table.Columns.Add("Audiometro", Type.GetType("System.String"));
            table.Columns.Add("DataCalibracao", Type.GetType("System.String"));
            table.Columns.Add("ODParecer", Type.GetType("System.String"));
            table.Columns.Add("ODAudiograma", Type.GetType("System.String"));
            table.Columns.Add("OEParecer", Type.GetType("System.String"));
            table.Columns.Add("OEAudiograma", Type.GetType("System.String"));
            table.Columns.Add("Observacao", Type.GetType("System.String"));

            table.Columns.Add("ODAerea025", Type.GetType("System.String"));
            table.Columns.Add("ODAerea050", Type.GetType("System.String"));
            table.Columns.Add("ODAerea1", Type.GetType("System.String"));
            table.Columns.Add("ODAerea2", Type.GetType("System.String"));
            table.Columns.Add("ODAerea3", Type.GetType("System.String"));
            table.Columns.Add("ODAerea4", Type.GetType("System.String"));
            table.Columns.Add("ODAerea6", Type.GetType("System.String"));
            table.Columns.Add("ODAerea8", Type.GetType("System.String"));

            table.Columns.Add("ODOssea050", Type.GetType("System.String"));
            table.Columns.Add("ODOssea1", Type.GetType("System.String"));
            table.Columns.Add("ODOssea2", Type.GetType("System.String"));
            table.Columns.Add("ODOssea3", Type.GetType("System.String"));
            table.Columns.Add("ODOssea4", Type.GetType("System.String"));
            table.Columns.Add("ODOssea6", Type.GetType("System.String"));

            table.Columns.Add("OEAerea025", Type.GetType("System.String"));
            table.Columns.Add("OEAerea050", Type.GetType("System.String"));
            table.Columns.Add("OEAerea1", Type.GetType("System.String"));
            table.Columns.Add("OEAerea2", Type.GetType("System.String"));
            table.Columns.Add("OEAerea3", Type.GetType("System.String"));
            table.Columns.Add("OEAerea4", Type.GetType("System.String"));
            table.Columns.Add("OEAerea6", Type.GetType("System.String"));
            table.Columns.Add("OEAerea8", Type.GetType("System.String"));

            table.Columns.Add("OEOssea050", Type.GetType("System.String"));
            table.Columns.Add("OEOssea1", Type.GetType("System.String"));
            table.Columns.Add("OEOssea2", Type.GetType("System.String"));
            table.Columns.Add("OEOssea3", Type.GetType("System.String"));
            table.Columns.Add("OEOssea4", Type.GetType("System.String"));
            table.Columns.Add("OEOssea6", Type.GetType("System.String"));

            return table;
        }

        private DataTable GetDataTableFooter()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("IdAudiometria", Type.GetType("System.Int32"));
            table.Columns.Add("DataAssinatura", Type.GetType("System.String"));
            table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("NomeMedico", Type.GetType("System.String"));
            table.Columns.Add("CRM", Type.GetType("System.String"));
            table.Columns.Add("iAssinatura", Type.GetType("System.Byte[]"));

            return table;
        }

        private DataTable GetDataTableHeader()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("IdAudiometria", Type.GetType("System.Int32"));
            table.Columns.Add("TipoExame", Type.GetType("System.String"));
            table.Columns.Add("RazaoSocial", Type.GetType("System.String"));
            table.Columns.Add("Endereco", Type.GetType("System.String"));
            table.Columns.Add("CEP", Type.GetType("System.String"));
            table.Columns.Add("CidadeEstado", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));
            table.Columns.Add("Identidade", Type.GetType("System.String"));
            table.Columns.Add("DataNascimento", Type.GetType("System.String"));
            table.Columns.Add("Idade", Type.GetType("System.String"));
            table.Columns.Add("Sexo", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("FotoEmpregado", Type.GetType("System.String"));

            return table;
        }

        private DataTable GetDataTableQuadro()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("IdAudiometria", Type.GetType("System.Int32"));

            table.Columns.Add("DataAudiometria", Type.GetType("System.String"));

            table.Columns.Add("ODAerea025", Type.GetType("System.String"));
            table.Columns.Add("ODAerea050", Type.GetType("System.String"));
            table.Columns.Add("ODAerea1", Type.GetType("System.String"));
            table.Columns.Add("ODAerea2", Type.GetType("System.String"));
            table.Columns.Add("ODAerea3", Type.GetType("System.String"));
            table.Columns.Add("ODAerea4", Type.GetType("System.String"));
            table.Columns.Add("ODAerea6", Type.GetType("System.String"));
            table.Columns.Add("ODAerea8", Type.GetType("System.String"));

            table.Columns.Add("ODOssea050", Type.GetType("System.String"));
            table.Columns.Add("ODOssea1", Type.GetType("System.String"));
            table.Columns.Add("ODOssea2", Type.GetType("System.String"));
            table.Columns.Add("ODOssea3", Type.GetType("System.String"));
            table.Columns.Add("ODOssea4", Type.GetType("System.String"));
            table.Columns.Add("ODOssea6", Type.GetType("System.String"));

            table.Columns.Add("OEAerea025", Type.GetType("System.String"));
            table.Columns.Add("OEAerea050", Type.GetType("System.String"));
            table.Columns.Add("OEAerea1", Type.GetType("System.String"));
            table.Columns.Add("OEAerea2", Type.GetType("System.String"));
            table.Columns.Add("OEAerea3", Type.GetType("System.String"));
            table.Columns.Add("OEAerea4", Type.GetType("System.String"));
            table.Columns.Add("OEAerea6", Type.GetType("System.String"));
            table.Columns.Add("OEAerea8", Type.GetType("System.String"));

            table.Columns.Add("OEOssea050", Type.GetType("System.String"));
            table.Columns.Add("OEOssea1", Type.GetType("System.String"));
            table.Columns.Add("OEOssea2", Type.GetType("System.String"));
            table.Columns.Add("OEOssea3", Type.GetType("System.String"));
            table.Columns.Add("OEOssea4", Type.GetType("System.String"));
            table.Columns.Add("OEOssea6", Type.GetType("System.String"));

            return table;
        }

        #endregion

        #region DataSource

        private DataSet DataSourceBase(bool IsAudiograma)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(GetDataTableAudiometria());

            DataRow newRow = ds.Tables[0].NewRow();

            newRow["IdAudiometria"] = audiometria.Id;
            newRow["IsAudiograma"] = IsAudiograma;

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private DataSet DataSourceAnamnese()
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(GetDataTableAnamnese());

            DataRow newRow = ds.Tables[0].NewRow();

            AnamneseAudiologica ananmese
                = AnamneseAudiologica.GetAnamneseAudiologica(audiometria);

            newRow["IdAudiometria"] = audiometria.Id;
            newRow["HasSenteDificuldadeAuditiva"] = AnamneseAudiologica.GetRespostaAnamnese(ananmese.HasSenteDificuldadeAuditiva);
            newRow["HasTemPresencaAcufenos"] = AnamneseAudiologica.GetRespostaAnamnese(ananmese.HasTemPresencaAcufenos);
            newRow["HasTemPresencaVertigens"] = AnamneseAudiologica.GetRespostaAnamnese(ananmese.HasTemPresencaVertigens);
            newRow["HasFamiliaresComProblemaAuditivo"] = AnamneseAudiologica.GetRespostaAnamnese(ananmese.HasFamiliaresComProblemaAuditivo);
            newRow["IsAntecedenteCaxumba"] = ananmese.IsAntecedenteCaxumba ? "X" : string.Empty;
            newRow["IsAntecedenteSarampo"] = ananmese.IsAntecedenteSarampo ? "X" : string.Empty;
            newRow["IsAntecedenteMenigite"] = ananmese.IsAntecedenteMenigite ? "X" : string.Empty;
            newRow["HasFazUsoMedicacao"] = AnamneseAudiologica.GetRespostaAnamnese(ananmese.HasFazUsoMedicacao);
            newRow["FazUsoMedicacaoQual"] = ananmese.FazUsoMedicacaoQual;
            newRow["TempoExposicaoRuidoOcupacional"] = ananmese.TempoExposicaoRuidoOcupacional;
            newRow["TempoUsoProtetorAuricular"] = ananmese.TempoUsoProtetorAuricular;
            newRow["HasExposicaoRuidoExtraLaboral"] = AnamneseAudiologica.GetRespostaAnamnese(ananmese.HasExposicaoRuidoExtraLaboral);
            newRow["HasExposicaoProdutosOtotoxicos"] = AnamneseAudiologica.GetRespostaAnamnese(ananmese.HasExposicaoProdutosOtotoxicos);
            newRow["ExposicaoProdutosOtotoxicosQual"] = ananmese.ExposicaoProdutosOtotoxicosQual;
            newRow["HasAlteracaoMeatosAcusticos"] = AnamneseAudiologica.GetRespostaAnamnese(ananmese.HasAlteracaoMeatosAcusticos);
            newRow["AlteracaoMeatosAcusticosQual"] = ananmese.AlteracaoMeatosAcusticosQual;
            newRow["Observacao"] = ananmese.Observacao;

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private DataSet DataSourceAudiograma()
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(GetDataTableAudiograma());

            DataRow newRow = ds.Tables[0].NewRow();

            newRow["IdAudiometria"] = audiometria.Id;

            if ( this.SemData == true )
               newRow["DataAudiometria"] = "";
            else
               newRow["DataAudiometria"] = audiometria.DataExame.ToString("dd-MM-yyyy");

            newRow["RepousoAuditivo"] = audiometria.TempoRepouso != 0 ? audiometria.TempoRepouso.ToString() + " horas" : string.Empty;

            //if (audiometria.IdAudiometro.Id == 0)
            //    throw new Exception("O Audiometro é campo obrigatório!");

            newRow["Audiometro"] = audiometria.IdAudiometro.GetNomeAudiometro();
            newRow["DataCalibracao"] = audiometria.IdAudiometro.DataUltimaAfericao.ToString("dd-MM-yyyy"); //audiometria.DataUltimaAfericao.ToString("dd-MM-yyyy");

           // if (audiogramaOD.IdDiagnostico.Id == 0 && !audiogramaOD.IsAudiogramaEmBranco())
           //     throw new Exception("Diagnóstico é campo obrigatório!");

            //newRow["ODParecer"] = audiogramaOD.IdDiagnostico.ToString();
            newRow["ODAudiograma"] = audiogramaOD.GetArquivo(cliente);

            //if (audiogramaOE.IdDiagnostico.Id == 0 && !audiogramaOE.IsAudiogramaEmBranco())
            //    throw new Exception("Diagnóstico é campo obrigatório!");

            //newRow["OEParecer"] = audiogramaOE.IdDiagnostico.ToString();
            newRow["OEAudiograma"] = audiogramaOE.GetArquivo(cliente);

            string zAux = "";


            if ( audiogramaOE.Rebaixamento == "L" )
            {
                newRow["OEParecer"] = "Rebaixamento auditivo de grau leve.";
            }
            else if ( audiogramaOE.Rebaixamento == "M" )
            {
                newRow["OEParecer"] = "Rebaixamento auditivo de grau moderado.";
            }
            else if (audiogramaOE.Rebaixamento == "S")
            {
                newRow["OEParecer"] = "Rebaixamento auditivo de grau severo.";
            }
            else if (audiogramaOE.Rebaixamento == "P")
            {
                newRow["OEParecer"] = "Rebaixamento auditivo de grau profundo.";
            }
            else
            {

                if (audiogramaOE.Tipo_Perda == "C")
                {
                    zAux = "condutiva";
                }
                else if (audiogramaOE.Tipo_Perda == "M")
                {
                    zAux = "mista";
                }
                else if (audiogramaOE.Tipo_Perda == "N")
                {
                    zAux = "neurossensorial";
                }
                else
                {
                    zAux = "";
                }


                if (audiogramaOE.Grau_Perda == "L")
                {
                    if (zAux == "")
                        newRow["OEParecer"] = "Rebaixamento auditivo de grau leve.";
                    else
                        newRow["OEParecer"] = "Perda auditiva " + zAux + " de grau leve.";
                }
                else if (audiogramaOE.Grau_Perda == "M")
                {
                    if (zAux == "")
                        newRow["OEParecer"] = "Rebaixamento auditivo de grau moderado.";
                    else
                        newRow["OEParecer"] = "Perda auditiva " + zAux + " de grau moderado.";
                }
                else if (audiogramaOE.Grau_Perda == "S")
                {
                    if (zAux == "")
                        newRow["OEParecer"] = "Rebaixamento auditivo de grau severo.";
                    else
                        newRow["OEParecer"] = "Perda auditiva " + zAux + " de grau severo.";
                }
                else if (audiogramaOE.Grau_Perda == "P")
                {
                    if (zAux == "")
                        newRow["OEParecer"] = "Rebaixamento auditivo de grau profundo.";
                    else
                        newRow["OEParecer"] = "Perda auditiva " + zAux + " de grau profundo.";
                }
                else
                {
                    newRow["OEParecer"] = "Audição dentro dos padrões de normalidade";
                }


            }

            

            zAux = "";


            if ( audiogramaOD.Rebaixamento == "L" )
            {
                newRow["ODParecer"] = "Rebaixamento auditivo de grau leve.";
            }
            else if ( audiogramaOD.Rebaixamento == "M" )
            {
                newRow["ODParecer"] = "Rebaixamento auditivo de grau moderado.";
            }
            else if (audiogramaOD.Rebaixamento == "S")
            {
                newRow["ODParecer"] = "Rebaixamento auditivo de grau severo.";
            }
            else if (audiogramaOD.Rebaixamento == "P")
            {
                newRow["ODParecer"] = "Rebaixamento auditivo de grau profundo.";
            }
            else
            {

                if (audiogramaOD.Tipo_Perda == "C")
                {
                    zAux = "condutiva";
                }
                else if (audiogramaOD.Tipo_Perda == "M")
                {
                    zAux = "mista";
                }
                else if (audiogramaOD.Tipo_Perda == "N")
                {
                    zAux = "neurossensorial";
                }
                else
                {
                    zAux = "";
                }


                if (audiogramaOD.Grau_Perda == "L")
                {
                    if (zAux == "")
                        newRow["ODParecer"] = "Rebaixamento auditivo de grau leve.";
                    else
                        newRow["ODParecer"] = "Perda auditiva " + zAux + " de grau leve.";
                }
                else if (audiogramaOD.Grau_Perda == "M")
                {
                    if (zAux == "")
                        newRow["ODParecer"] = "Rebaixamento auditivo de grau moderado.";
                    else
                        newRow["ODParecer"] = "Perda auditiva " + zAux + " de grau moderado.";
                }
                else if (audiogramaOD.Grau_Perda == "S")
                {
                    if (zAux == "")
                        newRow["ODParecer"] = "Rebaixamento auditivo de grau severo.";
                    else
                        newRow["ODParecer"] = "Perda auditiva " + zAux + " de grau severo.";
                }
                else if (audiogramaOD.Grau_Perda == "P")
                {
                    if (zAux == "")
                        newRow["ODParecer"] = "Rebaixamento auditivo de grau profundo.";
                    else
                        newRow["ODParecer"] = "Perda auditiva " + zAux + " de grau profundo.";
                }
                else
                {
                    newRow["ODParecer"] = "Audição dentro dos padrões de normalidade";
                }

            }


            StringBuilder strObs = new StringBuilder();

            if (audiometria.ObservacaoResultado != string.Empty)
                strObs.Append(audiometria.ObservacaoResultado + "\r\n");

            if (audiogramaOE.ObsMeatoscopia != string.Empty)
                strObs.Append(audiogramaOE.ObsMeatoscopia + "\r\n");

            if (audiogramaOD.ObsMeatoscopia != string.Empty)
                strObs.Append(audiogramaOD.ObsMeatoscopia + "\r\n");

            if (audiogramaOE.ObsDiagnostico != string.Empty)
                strObs.Append(audiogramaOE.ObsDiagnostico + "\r\n");

            if (audiogramaOD.ObsDiagnostico != string.Empty)
                strObs.Append(audiogramaOD.ObsDiagnostico + "\r\n");

            newRow["Observacao"] = strObs.ToString();

            if (!(audiogramaOD.IsAudiogramaEmBranco()
                && audiogramaOE.IsAudiogramaEmBranco()))
            {
                newRow["ODAerea025"] = audiogramaOD.IsAereoMascarado250 ? "* " + audiogramaOD.Aereo250 : audiogramaOD.Aereo250;
                newRow["ODAerea050"] = audiogramaOD.IsAereoMascarado500 ? "* " + audiogramaOD.Aereo500 : audiogramaOD.Aereo500;
                newRow["ODAerea1"] = audiogramaOD.IsAereoMascarado1000 ? "* " + audiogramaOD.Aereo1000 : audiogramaOD.Aereo1000;
                newRow["ODAerea2"] = audiogramaOD.IsAereoMascarado2000 ? "* " + audiogramaOD.Aereo2000 : audiogramaOD.Aereo2000;
                newRow["ODAerea3"] = audiogramaOD.IsAereoMascarado3000 ? "* " + audiogramaOD.Aereo3000 : audiogramaOD.Aereo3000;
                newRow["ODAerea4"] = audiogramaOD.IsAereoMascarado4000 ? "* " + audiogramaOD.Aereo4000 : audiogramaOD.Aereo4000;
                newRow["ODAerea6"] = audiogramaOD.IsAereoMascarado6000 ? "* " + audiogramaOD.Aereo6000 : audiogramaOD.Aereo6000;
                newRow["ODAerea8"] = audiogramaOD.IsAereoMascarado8000 ? "* " + audiogramaOD.Aereo8000 : audiogramaOD.Aereo8000;

                newRow["ODOssea050"] = audiogramaOD.IsOsseoMascarado500 ? "* " + audiogramaOD.Osseo500 : audiogramaOD.Osseo500;
                newRow["ODOssea1"] = audiogramaOD.IsOsseoMascarado1000 ? "* " + audiogramaOD.Osseo1000 : audiogramaOD.Osseo1000;
                newRow["ODOssea2"] = audiogramaOD.IsOsseoMascarado2000 ? "* " + audiogramaOD.Osseo2000 : audiogramaOD.Osseo2000;
                newRow["ODOssea3"] = audiogramaOD.IsOsseoMascarado3000 ? "* " + audiogramaOD.Osseo3000 : audiogramaOD.Osseo3000;
                newRow["ODOssea4"] = audiogramaOD.IsOsseoMascarado4000 ? "* " + audiogramaOD.Osseo4000 : audiogramaOD.Osseo4000;
                newRow["ODOssea6"] = audiogramaOD.IsOsseoMascarado6000 ? "* " + audiogramaOD.Osseo6000 : audiogramaOD.Osseo6000;

                newRow["OEAerea025"] = audiogramaOE.IsAereoMascarado250 ? "* " + audiogramaOE.Aereo250 : audiogramaOE.Aereo250;
                newRow["OEAerea050"] = audiogramaOE.IsAereoMascarado500 ? "* " + audiogramaOE.Aereo500 : audiogramaOE.Aereo500;
                newRow["OEAerea1"] = audiogramaOE.IsAereoMascarado1000 ? "* " + audiogramaOE.Aereo1000 : audiogramaOE.Aereo1000;
                newRow["OEAerea2"] = audiogramaOE.IsAereoMascarado2000 ? "* " + audiogramaOE.Aereo2000 : audiogramaOE.Aereo2000;
                newRow["OEAerea3"] = audiogramaOE.IsAereoMascarado3000 ? "* " + audiogramaOE.Aereo3000 : audiogramaOE.Aereo3000;
                newRow["OEAerea4"] = audiogramaOE.IsAereoMascarado4000 ? "* " + audiogramaOE.Aereo4000 : audiogramaOE.Aereo4000;
                newRow["OEAerea6"] = audiogramaOE.IsAereoMascarado6000 ? "* " + audiogramaOE.Aereo6000 : audiogramaOE.Aereo6000;
                newRow["OEAerea8"] = audiogramaOE.IsAereoMascarado8000 ? "* " + audiogramaOE.Aereo8000 : audiogramaOE.Aereo8000;

                newRow["OEOssea050"] = audiogramaOE.IsOsseoMascarado500 ? "* " + audiogramaOE.Osseo500 : audiogramaOE.Osseo500;
                newRow["OEOssea1"] = audiogramaOE.IsOsseoMascarado1000 ? "* " + audiogramaOE.Osseo1000 : audiogramaOE.Osseo1000;
                newRow["OEOssea2"] = audiogramaOE.IsOsseoMascarado2000 ? "* " + audiogramaOE.Osseo2000 : audiogramaOE.Osseo2000;
                newRow["OEOssea3"] = audiogramaOE.IsOsseoMascarado3000 ? "* " + audiogramaOE.Osseo3000 : audiogramaOE.Osseo3000;
                newRow["OEOssea4"] = audiogramaOE.IsOsseoMascarado4000 ? "* " + audiogramaOE.Osseo4000 : audiogramaOE.Osseo4000;
                newRow["OEOssea6"] = audiogramaOE.IsOsseoMascarado6000 ? "* " + audiogramaOE.Osseo6000 : audiogramaOE.Osseo6000;
            }

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private DataSet DataSourceFooter()
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(GetDataTableFooter());

            DataRow newRow = ds.Tables[0].NewRow();

            EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(audiometria.IdEmpregado);

            if (audiometria.IdMedico.mirrorOld == null)
                audiometria.IdMedico.Find();

            newRow["IdAudiometria"] = audiometria.Id;
            newRow["DataAssinatura"] = audiometria.DataExame.ToString("d \"de\" MMMM \"de\" yyyy");
            newRow["NomeEmpregado"] = audiometria.IdEmpregado.ToString();
            newRow["Funcao"] = empregadoFuncao.GetNomeFuncao();
            newRow["NomeMedico"] = audiometria.IdMedico.NomeCompleto;
            newRow["CRM"] = audiometria.IdMedico.Numero;

            try
            {
                //newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto(laudoTecnico.nID_COMISSAO_2.FotoAss);
                newRow["iAssinatura"] = Ilitera.Common.Fotos.GetByteFoto( audiometria.IdMedico.FotoAss);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private DataSet DataSourceHeader()
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(GetDataTableHeader());

            DataRow newRow = ds.Tables[0].NewRow();

            newRow["IdAudiometria"] = audiometria.Id;
            newRow["TipoExame"] = audiometria.GetAudiometriaTipo();
            newRow["RazaoSocial"] = cliente.NomeCompleto;
            newRow["Endereco"] = cliente.GetEndereco().GetEndereco();
            newRow["CidadeEstado"] = cliente.GetEndereco().GetCidadeEstado();
            newRow["CEP"] = cliente.GetEndereco().Cep;
            newRow["CNPJ"] = cliente.NomeCodigo;

            if (audiometria.Id == 0)
            {
                newRow["NomeEmpregado"] = "Nome:";
                newRow["Identidade"] = "RG:";
                newRow["DataNascimento"] = "Nascido em: ";
                newRow["Sexo"] = "Sexo: ";
                newRow["Setor"] = "Setor: ";
                newRow["Funcao"] = "Função: ";
            }

            EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(audiometria.IdEmpregado);

            newRow["NomeEmpregado"] = audiometria.IdEmpregado.GetNomeEmpregadoComRE();
            newRow["Identidade"] = "RG " + audiometria.IdEmpregado.tNO_IDENTIDADE;
            newRow["DataNascimento"] = "Nascido em " + audiometria.IdEmpregado.GetDataNascimento();
            newRow["Idade"] = "Idade " + audiometria.IdEmpregado.IdadeEmpregado().ToString() + " anos";
            newRow["Sexo"] = audiometria.IdEmpregado.tSEXO;
            newRow["Setor"] = "Setor de " + empregadoFuncao.GetNomeSetor();
            newRow["Funcao"] = "Função de " + empregadoFuncao.GetNomeFuncao();
            newRow["FotoEmpregado"] = audiometria.IdEmpregado.FotoEmpregado();

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private DataSet DataSourceQuadro()
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(GetDataTableQuadro());

            DataRow newRow = ds.Tables[0].NewRow();

            newRow["IdAudiometria"] = audiometria.Id;

            if ( this.SemData == true )
               newRow["DataAudiometria"] = "";
            else
               newRow["DataAudiometria"] = audiometria.DataExame.ToString("dd-MM-yyyy");

            if (!(audiogramaOD.IsAudiogramaEmBranco()
                && audiogramaOE.IsAudiogramaEmBranco()))
            {
                newRow["ODAerea025"] = audiogramaOD.IsAereoMascarado250 ? "* " + audiogramaOD.Aereo250 : audiogramaOD.Aereo250;
                newRow["ODAerea050"] = audiogramaOD.IsAereoMascarado500 ? "* " + audiogramaOD.Aereo500 : audiogramaOD.Aereo500;
                newRow["ODAerea1"] = audiogramaOD.IsAereoMascarado1000 ? "* " + audiogramaOD.Aereo1000 : audiogramaOD.Aereo1000;
                newRow["ODAerea2"] = audiogramaOD.IsAereoMascarado2000 ? "* " + audiogramaOD.Aereo2000 : audiogramaOD.Aereo2000;
                newRow["ODAerea3"] = audiogramaOD.IsAereoMascarado3000 ? "* " + audiogramaOD.Aereo3000 : audiogramaOD.Aereo3000;
                newRow["ODAerea4"] = audiogramaOD.IsAereoMascarado4000 ? "* " + audiogramaOD.Aereo4000 : audiogramaOD.Aereo4000;
                newRow["ODAerea6"] = audiogramaOD.IsAereoMascarado6000 ? "* " + audiogramaOD.Aereo6000 : audiogramaOD.Aereo6000;
                newRow["ODAerea8"] = audiogramaOD.IsAereoMascarado8000 ? "* " + audiogramaOD.Aereo8000 : audiogramaOD.Aereo8000;

                newRow["ODOssea050"] = audiogramaOD.IsOsseoMascarado500 ? "* " + audiogramaOD.Osseo500 : audiogramaOD.Osseo500;
                newRow["ODOssea1"] = audiogramaOD.IsOsseoMascarado1000 ? "* " + audiogramaOD.Osseo1000 : audiogramaOD.Osseo1000;
                newRow["ODOssea2"] = audiogramaOD.IsOsseoMascarado2000 ? "* " + audiogramaOD.Osseo2000 : audiogramaOD.Osseo2000;
                newRow["ODOssea3"] = audiogramaOD.IsOsseoMascarado3000 ? "* " + audiogramaOD.Osseo3000 : audiogramaOD.Osseo3000;
                newRow["ODOssea4"] = audiogramaOD.IsOsseoMascarado4000 ? "* " + audiogramaOD.Osseo4000 : audiogramaOD.Osseo4000;
                newRow["ODOssea6"] = audiogramaOD.IsOsseoMascarado6000 ? "* " + audiogramaOD.Osseo6000 : audiogramaOD.Osseo6000;

                newRow["OEAerea025"] = audiogramaOE.IsAereoMascarado250 ? "* " + audiogramaOE.Aereo250 : audiogramaOE.Aereo250;
                newRow["OEAerea050"] = audiogramaOE.IsAereoMascarado500 ? "* " + audiogramaOE.Aereo500 : audiogramaOE.Aereo500;
                newRow["OEAerea1"] = audiogramaOE.IsAereoMascarado1000 ? "* " + audiogramaOE.Aereo1000 : audiogramaOE.Aereo1000;
                newRow["OEAerea2"] = audiogramaOE.IsAereoMascarado2000 ? "* " + audiogramaOE.Aereo2000 : audiogramaOE.Aereo2000;
                newRow["OEAerea3"] = audiogramaOE.IsAereoMascarado3000 ? "* " + audiogramaOE.Aereo3000 : audiogramaOE.Aereo3000;
                newRow["OEAerea4"] = audiogramaOE.IsAereoMascarado4000 ? "* " + audiogramaOE.Aereo4000 : audiogramaOE.Aereo4000;
                newRow["OEAerea6"] = audiogramaOE.IsAereoMascarado6000 ? "* " + audiogramaOE.Aereo6000 : audiogramaOE.Aereo6000;
                newRow["OEAerea8"] = audiogramaOE.IsAereoMascarado8000 ? "* " + audiogramaOE.Aereo8000 : audiogramaOE.Aereo8000;

                newRow["OEOssea050"] = audiogramaOE.IsOsseoMascarado500 ? "* " + audiogramaOE.Osseo500 : audiogramaOE.Osseo500;
                newRow["OEOssea1"] = audiogramaOE.IsOsseoMascarado1000 ? "* " + audiogramaOE.Osseo1000 : audiogramaOE.Osseo1000;
                newRow["OEOssea2"] = audiogramaOE.IsOsseoMascarado2000 ? "* " + audiogramaOE.Osseo2000 : audiogramaOE.Osseo2000;
                newRow["OEOssea3"] = audiogramaOE.IsOsseoMascarado3000 ? "* " + audiogramaOE.Osseo3000 : audiogramaOE.Osseo3000;
                newRow["OEOssea4"] = audiogramaOE.IsOsseoMascarado4000 ? "* " + audiogramaOE.Osseo4000 : audiogramaOE.Osseo4000;
                newRow["OEOssea6"] = audiogramaOE.IsOsseoMascarado6000 ? "* " + audiogramaOE.Osseo6000 : audiogramaOE.Osseo6000;
            }

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        #endregion
    }
}
