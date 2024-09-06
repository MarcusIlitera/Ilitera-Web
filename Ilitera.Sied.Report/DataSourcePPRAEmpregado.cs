using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Ilitera.Common;
using Ilitera.Opsa.Data;

using CrystalDecisions.CrystalReports.Engine;

namespace Ilitera.Sied.Report
{
    public enum IndTipoPPRA : int
    {
        PPRA,
        LTCAT
    }

    public class DataSourcePPRAEmpregado : DataSourceBase
    {
        private int indTipoPPRA;
        private DataSet dsAgentesQuimicos = new DataSet();
        private DataSet dsVibracoes = new DataSet();
        private DataSet dsDataSource = new DataSet();
        private string _MarcaDaqua = string.Empty;
        private bool ComFoto = true;
        private bool IsPpraPrevidenciario = false;

        #region Propertie MarcaDaqua

        public string MarcaDaqua
        {
            get { return _MarcaDaqua; }
            set { _MarcaDaqua = value; }
        }
        #endregion

        #region Construtores

        public DataSourcePPRAEmpregado(Empregado empregado) 
            : this(empregado, (int)IndTipoPPRA.PPRA)
        {
            
        }

        public DataSourcePPRAEmpregado(Empregado empregado,  int indTipoPPRA)
            : this(empregado, EmpregadoFuncao.GetEmpregadoFuncao(empregado), indTipoPPRA)
        {

        }

        public DataSourcePPRAEmpregado(EmpregadoFuncao empregadoFuncao, int indTipoPPRA)
            : this(empregadoFuncao.nID_EMPREGADO, empregadoFuncao, indTipoPPRA)
        {

        }

        public DataSourcePPRAEmpregado(EmpregadoFuncao empregadoFuncao, LaudoTecnico laudoTecnico, int indTipoPPRA)
            :
            this(empregadoFuncao.nID_EMPREGADO, empregadoFuncao, laudoTecnico, indTipoPPRA, DateTime.Today)
        {

        }

        public DataSourcePPRAEmpregado(Empregado empregado, EmpregadoFuncao empregadoFuncao, int indTipoPPRA)
            :
            this(empregado, empregadoFuncao, empregadoFuncao.GetLaudoTecnico(), indTipoPPRA, DateTime.Today)
        {

        }

        public DataSourcePPRAEmpregado(Empregado empregado, LaudoTecnico laudoTecnico, int indTipoPPRA)
            :
            this(   empregado, 
                    EmpregadoFuncao.GetEmpregadoFuncao(laudoTecnico, empregado), 
                    laudoTecnico, 
                    indTipoPPRA,
                    DateTime.Today)
        {

        }

        public DataSourcePPRAEmpregado( Empregado empregado, 
                                        EmpregadoFuncao empregadoFuncao, 
                                        LaudoTecnico laudoTecnico, 
                                        int indTipoPPRA,
                                        DateTime dtAssinaturaLaudo)
        {
            this.indTipoPPRA = indTipoPPRA;

            if (empregado.mirrorOld == null)
                empregado.Find();

            if (empregadoFuncao.Id.Equals(0))
                throw new Exception("O empregado deve possuir pelo menos 1 Classificação Funcional!");

            dsDataSource.Tables.Add(GetTable());
            dsAgentesQuimicos.Tables.Add(GetTableAgentesQuimicos());
            dsVibracoes.Tables.Add(GetTableAgentesQuimicos());

            PopularDataSet(laudoTecnico, empregado, empregadoFuncao, dtAssinaturaLaudo, 1);
        }

        public DataSourcePPRAEmpregado(ArrayList listEmpregados, int indTipoPPRA)
        {
            this.indTipoPPRA = indTipoPPRA;

            if (listEmpregados.Count > 20)
                ComFoto = false;

            PopularDataSet(listEmpregados);
        }
        #endregion

        #region GetReport

        public ReportClass GetReport()
        {
            ReportClass report;

            //if (indTipoPPRA == (int)IndTipoPPRA.LTCAT)   //(!this.IsPpraPrevidenciario)
                report = new RptPPRAEmpregado();
            //else
            //    report = new RptPPRAEmpregadoPrevidenciario();

            report.OpenSubreport("AgentesQuimicos").SetDataSource(dsAgentesQuimicos);
            report.OpenSubreport("Vibracoes").SetDataSource(dsVibracoes);
            report.SetDataSource(dsDataSource);
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }
        #endregion

        #region PopularDataSet

        private void PopularDataSet(ArrayList listEmpregados)
        {
            dsDataSource.Tables.Add(GetTable());
            dsAgentesQuimicos.Tables.Add(GetTableAgentesQuimicos());
            dsVibracoes.Tables.Add(GetTableAgentesQuimicos());

            foreach (Empregado empregado in listEmpregados)
            {
                EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);

                if (empregadoFuncao.Id.Equals(0))
                    throw new Exception("O empregado deve possuir pelo menos 1 Classificação Funcional!");

                LaudoTecnico laudoTecnico = empregadoFuncao.GetLaudoTecnico_LTCAT();

                PopularDataSet(laudoTecnico, empregado, empregadoFuncao, DateTime.Today, listEmpregados.Count);
            }
        }
        #endregion

        #region PopularDataSet

        private void PopularDataSet(LaudoTecnico laudoTecnico, 
                                    Empregado empregado, 
                                    EmpregadoFuncao empregadoFuncao,
                                    DateTime dtAssinaturaLaudo,
                                    int CountEmpregados)
        {
            if (laudoTecnico.nID_EMPR.mirrorOld == null)
                laudoTecnico.nID_EMPR.Find();

            Ghe ghe = empregadoFuncao.GetGheEmpregado(laudoTecnico);

            if (ghe.Id.Equals(0))
            {
                if (CountEmpregados == 1)
                    throw new Exception("É necessário que o empregado pertença a um GHE em cada um dos Laudos Técnicos realizados que se enquadram nos períodos de sua Classificação Funcional!");
                else
                    return;
            }

            if (Ilitera.Data.Table.IsWeb && ghe.IsAgentesQuimicosSemAvaliacaoQuantitativa())
            {
                if (CountEmpregados == 1)
                    throw new Exception("Análises Quantitativas de Agentes Químicos - Os agentes químicos descritos na coluna 'Anexos 11 e 12 - NR 15' necessitam de avaliação quantitativa conforme a NR 9, item 9.3.4, realizada através de coleta e análise laboratorial, para verificação dos níveis de concentração, e posterior comparação com os limites de tolerância estabelecidos pela norma legal, objetivando comprovar o controle da exposição, dimensionar a exposição dos trabalhadores e subsidiar o equacionamento das medidas de controle.");
                else
                    return;
            }

            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" nID_LAUD_TEC IN (SELECT nID_LAUD_TEC FROM tblFUNC_EMPREGADO WHERE nID_EMPREGADO_FUNCAO=" + empregadoFuncao.Id + ")");
            
            if(Ilitera.Data.Table.IsWeb)
                strWhere.Append(" AND (nID_PEDIDO IN (SELECT IdPedido FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido WHERE DataConclusao IS NOT NULL) OR bAE =1)");

            strWhere.Append(" ORDER BY hDT_LAUDO");

            List<LaudoTecnico> alSortedLaudos = new LaudoTecnico().Find<LaudoTecnico>(strWhere.ToString());

            DataRow newRow = dsDataSource.Tables[0].NewRow();

            PopularRowEmpregado(newRow, empregado, empregadoFuncao, laudoTecnico, ghe, alSortedLaudos, dtAssinaturaLaudo);

            dsDataSource.Tables[0].Rows.Add(newRow);
        }
        #endregion

        #region PopularRowEmpregado

        private void PopularRowEmpregado(DataRow newRow, 
                                         Empregado empregado, 
                                         EmpregadoFuncao empregadoFuncao, 
                                         LaudoTecnico laudoTecnico, 
                                         Ghe ghe, 
                                         List<LaudoTecnico> alSortedLaudos,
                                         DateTime dtAssinaturaLaudo)
        {
            Cliente cliente = empregadoFuncao.nID_EMPR;

            if (cliente.mirrorOld == null)
                cliente.Find();

            //Marca daqua para Elektro
            this._MarcaDaqua = ghe.GetMarcaDaquaPPRAEmpregado(cliente);

            newRow["IdEmpregado"] = empregado.Id;
            newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);
            newRow["Cidade"] = cliente.GetEndereco().GetCidade();
            newRow["TIPO_LTCAT"] = indTipoPPRA;

            if (ComFoto)
            {
                string xPath = empregado.FotoEmpregado();

                if (xPath.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 0) xPath=xPath.Replace("I:\\", "I:\\FOTOSDOCSDIGITAIS\\");

                string pathFoto = Ilitera.Common.Fotos.PathFoto_Uri(xPath);

                if (pathFoto != string.Empty ) //&& System.IO.File.Exists(pathFoto))
                {
                    newRow["FOTO_EMPREGADO"] = pathFoto;
                    newRow["ComFoto"] = true;
                }
                else
                    newRow["ComFoto"] = false;
            }
            else
                newRow["ComFoto"] = false;

            newRow["NOME_EMPREGADO"] = empregado.tNO_EMPG;
            newRow["PIS_PASEP"] = empregado.nNO_PIS_PASEP == 0 ? "-" : empregado.nNO_PIS_PASEP.ToString();

            if (empregado.hDT_NASC != new DateTime())
                newRow["DATA_NASCIMENTO"] = empregado.hDT_NASC.ToString("dd-MM-yyyy");
            else
                newRow["DATA_NASCIMENTO"] = "__-__-____";

            newRow["SEXO"] = empregado.tSEXO;
            newRow["CTPS"] = empregado.GetCTPS();
            newRow["RG"] = empregado.tNO_IDENTIDADE;

            if (empregado.nID_REGIME_REVEZAMENTO.Id == 0)
                newRow["REGIME_REVEZAMENTO"] = "Inexistente";
            else
                newRow["REGIME_REVEZAMENTO"] = empregado.nID_REGIME_REVEZAMENTO.ToString();

            empregadoFuncao.nID_TEMPO_EXP.Find();
            empregadoFuncao.nID_SETOR.Find();
            empregadoFuncao.nID_FUNCAO.Find();
            empregadoFuncao.nID_EMPR.Find();
            empregadoFuncao.nID_CARGO.Find();

            if (empregadoFuncao.nIND_GFIP == (int)CodigoGFIP.PPRA)
                newRow["GFIP"] = ghe.GetGFIP();
            else
                newRow["GFIP"] = Ghe.GetGFIP(empregadoFuncao.nIND_GFIP);

            newRow["DataInicio"] = empregadoFuncao.hDT_INICIO.ToString("dd-MM-yyyy");

            if (empregadoFuncao.hDT_TERMINO != new DateTime())
                newRow["DataTermino"] = empregadoFuncao.hDT_TERMINO.ToString("dd-MM-yyyy");
            else
                newRow["DataTermino"] = "__-__-____";

            newRow["CNPJLocal"] = empregadoFuncao.nID_EMPR.NomeCodigo;
            
            if (empregadoFuncao.nID_TEMPO_EXP.Id == 0)
                newRow["Jornada"] = "até 44 horas semanais";
            else
                newRow["Jornada"] = empregadoFuncao.nID_TEMPO_EXP.tHORA_EXTENSO_SEMANAL;

            newRow["CBO"] = empregadoFuncao.nID_FUNCAO.NumeroCBO;
            newRow["Setor"] = empregadoFuncao.nID_SETOR.tNO_STR_EMPR;
            newRow["Funcao"] = empregadoFuncao.nID_FUNCAO.NomeFuncao;

            if (empregadoFuncao.nID_CARGO.tNO_CARGO.Equals(string.Empty))
                newRow["Cargo"] = "não aplicável";
            else
                newRow["Cargo"] = empregadoFuncao.nID_CARGO.tNO_CARGO;

            newRow["DescricaoSetor"] = empregadoFuncao.GetDescricaoLocalTrabalho();
            newRow["DescricaoFuncao"] = empregadoFuncao.GetDescricaoFuncao();

            //ArrayList alPPRA = new PPRA().Find("nID_FUNC=" + ghe.Id + " ORDER BY nID_RSC");


            
            //DateTime dataInicioLaudo;

            //dataInicioLaudo = alSortedLaudos[alSortedLaudos.Count - 1].hDT_LAUDO;
            ////newRow["DataTermino"] = alSortedLaudos[alSortedLaudos.Count - 1].hDT_LAUDO.AddYears(1);

            ////if (this.IsPpraPrevidenciario)
            ////    dataInicioLaudo = Convert.ToDateTime(newRow["DataInicio"]);
            ////else
            ////    dataInicioLaudo = laudoTecnico.hDT_LAUDO;

            ///*Define a data de término do Laudo com base na data do próximo Laudo associado à classificação funcional em questão.
            //Se não houver próximo Laudo associado, a data de término será definida adicionando-se 1 ano.*/
            //DateTime dataTerminoLaudo = laudoTecnico.GetDataTermino(alSortedLaudos);

            ///*Se caso a data de término do Laudo for maior que a data de término da classificação funcional, a data de término do Laudo será
            //igual a data de término da classificação funcional*/
            //if (dataTerminoLaudo > empregadoFuncao.hDT_TERMINO && empregadoFuncao.hDT_TERMINO != new DateTime())
            //    dataTerminoLaudo = empregadoFuncao.hDT_TERMINO;

            ///*Se caso a data de término do Laudo for maior que a data de hoje e a classificação funcional não possuir data de término, 
            //a data de término do Laudo será igual a data atual*/
            //if (dataTerminoLaudo >= DateTime.Today && empregadoFuncao.hDT_TERMINO == new DateTime())
            //    dataTerminoLaudo = new DateTime();

            //if (dataTerminoLaudo == new DateTime() && this.IsPpraPrevidenciario)
            //    dataTerminoLaudo = new DateTime(2007, 3, 31);

            ///*Se caso o laudo analisado for o primeiro laudo técnico executado para o conjunto de laudos técnicos associados para a
            //classificação funcional em questão, a data de início do laudo será igual à data de início da classificação funcional*/
            //if (alSortedLaudos.Count > 0 && laudoTecnico.Id == ((LaudoTecnico)alSortedLaudos[0]).Id)
            //{
            //    //dataInicioLaudo = empregadoFuncao.hDT_INICIO;

            //    /* No caso de laudos muito antigos, pode acontecer da data de início ser maior que a data de término, devido à anualidade
            //    no período dos laudos. Como geralmente laudos antigos acabam não sendo cadastrados ano a ano e o sistema calcula as datas
            //    de início e término com base no período anual, é necessário checar algumas consistências. Se caso a data de início do laudo
            //    acabar sendo maior que a data de término, sendo ela diferente de nula, a data de término adotada será a data de início
            //    acrescido de 1 ano*/
            //    if (dataInicioLaudo >= dataTerminoLaudo && dataTerminoLaudo != new DateTime())
            //    {
            //        dataTerminoLaudo = dataInicioLaudo.AddYears(1).AddDays(-1);

            //        /*Após a adoção da nova data de término, as mesmas verificações anteriores com relação a data de término da classificação
            //        funcional precisam ser efetuadas*/
            //        if (dataTerminoLaudo > empregadoFuncao.hDT_TERMINO && empregadoFuncao.hDT_TERMINO != new DateTime())
            //            dataTerminoLaudo = empregadoFuncao.hDT_TERMINO;

            //        if (dataTerminoLaudo > DateTime.Today && empregadoFuncao.hDT_TERMINO == new DateTime())
            //            dataTerminoLaudo = new DateTime();
            //    }
            //}

            //DateTime dataCriacaoPPRA = new DateTime(1995, 1, 1);//25-2-1995

            //if (dataInicioLaudo < dataCriacaoPPRA && dataTerminoLaudo < dataCriacaoPPRA && dataTerminoLaudo != new DateTime())
            //    throw new Exception("O PPRA só pode ser impresso para periodos posteriores a "+ dataCriacaoPPRA.ToString("dd-MM-yyyy"));

            ///* A vigência do PPRA não pode ser inferior a criação do mesmo*/
            ////if (dataInicioLaudo < dataCriacaoPPRA)
            ////    dataInicioLaudo = dataCriacaoPPRA;

            //newRow["DataInicioLaudo"] = dataInicioLaudo;
            
            //if (dataTerminoLaudo != new DateTime())
            //{
            //    /* Se ainda houver problemas com relação às datas de início e término do Laudo, uma exceção é lançada para analizar o cadastro
            //    específico de um funcionário*/
            //    if (dataInicioLaudo >= dataTerminoLaudo)
            //        throw new Exception("Atenção, o(a) funcionário(a) " + empregado.tNO_EMPG
            //            + " está com um problema na associação de sua Classificação Funcional com os levantamentos executados!"
            //            + " Qualquer dúvida entre em contato com a Ilitera.");

            //    newRow["DataTerminoLaudo"] = dataTerminoLaudo;
            //}





            DateTime dataInicioLaudo;
            //if (this.IsPpraPrevidenciario)
            //dataInicioLaudo = Convert.ToDateTime(newRow["DataInicio"]);


            dataInicioLaudo = laudoTecnico.hDT_LAUDO;
            DateTime dataTerminoLaudo = new DateTime();


            IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("pt-Br", true);

            ArrayList zList = new LaudoTecnico().Find("nID_EMPR=" + laudoTecnico.nID_EMPR.Id.ToString().Trim()
                + " AND (nID_PEDIDO IN (SELECT IdPedido FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido WHERE DataConclusao IS NOT NULL) OR bAE =1)"
                + " AND hDT_Laudo > convert( smalldatetime, '" + laudoTecnico.hDT_LAUDO.ToString("dd/MM/yyyy").Trim().Substring(0, 10) + "' , 103 ) "
                + " ORDER BY hDT_LAUDO");

            string zData = "";

            foreach (LaudoTecnico rLaudo in zList)
            {
                if (zData == "")
                {
                    zData = rLaudo.hDT_LAUDO.AddDays(-1).ToString("dd/MM/yyyy").Trim().Substring(0, 10).Replace("/", "-");
                    dataTerminoLaudo = rLaudo.hDT_LAUDO.AddDays(-1);
                }
            }

            if (zData == "")
            {
                dataTerminoLaudo = laudoTecnico.hDT_LAUDO.AddDays(364);
            }


            newRow["DataInicioLaudo"] = dataInicioLaudo;

            if (dataTerminoLaudo != new DateTime())
            {
                /* Se ainda houver problemas com relação às datas de início e término do Laudo, uma exceção é lançada para analizar o cadastro
                específico de um funcionário*/
                if (dataInicioLaudo >= dataTerminoLaudo)
                    throw new Exception("Atenção, o(a) funcionário(a) " + empregado.tNO_EMPG
                        + " está com um problema na associação de sua Classificação Funcional com os levantamentos executados!"
                        + " Qualquer dúvida entre em contato com o suporte ao sistema."); //Ilitera através do telefone 11 4249-4949 ou pelo e-mail wagner.sp.sto@ilitera.com.br");

                //throw new Exception("Atenção, o(a) funcionário(a) " + empregado.tNO_EMPG
                //    + " está com um problema na associação de sua Classificação Funcional com os levantamentos executados!"
                //    + " Qualquer dúvida entre em contato com a Ilitera através do telefone 11 3521-3000 ou pelo e-mail suporte@mestra.net");

                newRow["DataTerminoLaudo"] = dataTerminoLaudo;
            }
            else
                newRow["DataTerminoLaudo"] = dataTerminoLaudo;







            if (this.indTipoPPRA == (int)IndTipoPPRA.PPRA)//Somente para PPRA
                newRow["isAdministrativo"] = ghe.nIND_ATIVIDADES == Ghe.AtividadeTipo.ExclusivamenteAdministrativa;
            else
                newRow["isAdministrativo"] = false; ;

            PPRA ruido = new PPRA();
            ruido.Find("nID_FUNC=" + ghe.Id + " AND nID_RSC=" + (int)Riscos.RuidoContinuo);

            newRow["NivelRuido"] = ruido.tVL_MED_Ruido_LTCAT + " dB(A)";

            if (IsPpraPrevidenciario)
                newRow["TempoExposicao"] = "Permanente";
            else
            {
                empregadoFuncao.nID_TEMPO_EXP.Find();
                if (empregadoFuncao.nID_TEMPO_EXP.Id != 0)
                    newRow["TempoExposicao"] = empregadoFuncao.nID_TEMPO_EXP.tHORA;
                else
                {
                    if (ghe.nID_TEMPO_EXP.mirrorOld == null)
                        ghe.nID_TEMPO_EXP.Find();

                    newRow["TempoExposicao"] = ghe.nID_TEMPO_EXP.tHORA;
                }
            }

            if (IsPpraPrevidenciario)
                newRow["ExposicaoMax"] = "até 44 h semanais";
            else
                newRow["ExposicaoMax"] = ruido.tVL_LIM_TOL;

            if (IsPpraPrevidenciario)
            {
                if (empregadoFuncao.IsPertenceAoPeriodo85())
                    newRow["AcimaLimite85"] = (ruido.tVL_MED_Ruido_LTCAT > 85) ? "SIM" : "NÃO";
                else
                    newRow["AcimaLimite85"] = "-";

                if (empregadoFuncao.IsPertenceAoPeriodo90())
                    newRow["AcimaLimite90"] = (ruido.tVL_MED_Ruido_LTCAT > 90) ? "SIM" : "NÃO";
                else
                    newRow["AcimaLimite90"] = "-";

                if (empregadoFuncao.IsPertenceAoPeriodo80())
                    newRow["AcimaLimite80"] = (ruido.tVL_MED_Ruido_LTCAT > 80) ? "SIM" : "NÃO";
                else
                    newRow["AcimaLimite80"] = "-";
            }
            else
            {
                //if (ruido.gINSALUBRE)
                //    newRow["AcimaLimite85"] = "SIM";
                //else
                //    newRow["AcimaLimite85"] = "NÃO";
                newRow["AcimaLimite85"] = (ruido.tVL_MED_Ruido_LTCAT > 85) ? "SIM" : "NÃO";
            }

            StringBuilder reconhecimentoRuido = new StringBuilder();
            reconhecimentoRuido.Append(ruido.GetReconhecimentoPPRA(false));
            reconhecimentoRuido.Replace(ruido.GetNomeAgente() + " - ", string.Empty);

            //Ilitera: 11/10/2010 - Estava dando erro pois esse campo estava vindo vazio
            if (reconhecimentoRuido != null && reconhecimentoRuido.ToString().Trim() != String.Empty)
            {
                newRow["ReconhecimentoRuido"] = Utility.OnlyFirstLetterUpper(reconhecimentoRuido.ToString());
            }
            else
            {
                newRow["ReconhecimentoRuido"] = "";
            }
            

            PPRA calor = new PPRA();
            calor.Find("nID_FUNC=" + ghe.Id + " AND nID_RSC=" + (int)Riscos.Calor);

            if (calor.Id != 0)
            {
                newRow["isCalorDescansoLocalDiverso"] = calor.bDESC;

                if (calor.gINSALUBRE)
                    newRow["CalorUltrapassado"] = "SIM";
                else
                    newRow["CalorUltrapassado"] = "NÃO";

                if (calor.bDESC)
                {
                    newRow["CalorValor"] = calor.IBUTG;
                    newRow["CalorLimite"] = calor.tVL_LIM_TOL + " IBUTG";
                    newRow["TaxaMetabolismo"] = calor.M + " Kcal/h";
                    newRow["TempoTrabalho"] = calor.Tt;
                    newRow["TempoDescanso"] = calor.Td;
                    newRow["IBUTGTrabalho"] = calor.IBUTGt;
                    newRow["IBUTGDescanso"] = calor.IBUTGd;
                    newRow["MetabolismoTrabalho"] = calor.Mt + " Kcal/h";
                    newRow["MetabolismoDescanso"] = calor.Md + " Kcal/h";
                }
                else
                {
                    newRow["CalorValor"] = calor.GetAvaliacaoQuantitativa();
                    newRow["CalorMaximoPermissivel"] = calor.tVL_LIM_TOL;
                    newRow["TipoAtividade"] = ghe.TipoAtividade();
                    newRow["CalorLimite"] = ghe.CalorLimite();
                }

                StringBuilder reconhecimentoCalor = new StringBuilder();
                
                if(IsPpraPrevidenciario)
                    reconhecimentoCalor.Append(calor.GetReconhecimentoPPRA(false).Replace("irradiação solar indireta.", "irradiação solar indireta (Inexistência fontes artificiais)."));
                else
                    reconhecimentoCalor.Append(calor.GetReconhecimentoPPRA(false));

                reconhecimentoCalor.Replace("Calor - ", string.Empty);

                //Ilitera: 11/10/2010 - Estava dando erro pois esse campo estava vindo vazio
                if (reconhecimentoCalor != null && reconhecimentoCalor.ToString().Trim() != String.Empty)
                {
                    newRow["ReconhecimentoCalor"] = Utility.OnlyFirstLetterUpper(reconhecimentoCalor.ToString());
                }
                else
                {
                    newRow["ReconhecimentoCalor"] = String.Empty;
                }
                
            }
            else
            {
                newRow["CalorUltrapassado"] = "-";
                newRow["isCalorDescansoLocalDiverso"] = false;
                newRow["CalorValor"] = "Inexistente";
                newRow["CalorMaximoPermissivel"] = "Inexistente";
                newRow["TipoAtividade"] = "Inexistente";
                newRow["CalorLimite"] = "Inexistente";
            }

            PopularRowAgentesQuimicos(empregado, ghe);


            newRow["Vibracoes"] = "0";

            //carregar dsVibracoes

            PopularRowVibracoes(empregado, ghe);

            if (dsVibracoes.Tables[0].Rows.Count > 0)
            {
                //procurar "IdEmpregado" = ghe.Id
                DataRow[] Regs2 = dsVibracoes.Tables[0].Select("IdEmpregado = " + ghe.Id.ToString());

                if (Regs2.Length > 0)
                {
                    newRow["Vibracoes"] = "1";
                }
                
            }


            newRow["DemaisAgentes"] = ghe.GetDemaisAgentesReconhecimento(true);

            newRow["DescricaoEPI"] = ghe.Epi(", ");

            if (IsPpraPrevidenciario)
                newRow["DescricaoEPC"] = ghe.Epc_LTCAT().Replace(Ghe.strInexistente, @"A utilização de EPI é admitida no âmbito do programa de gerenciamento dos riscos ambientais em situações de trabalho que existem inviabilidade técnica insuficiência ou interinidade para implementação de EPC, conforme autorizam as normas legais – art. 171, V, “a”, da IN/INSS PR n° 11/06 c/c art. 383, § 2º, IN/SRP n° 03/05.");
            else
                newRow["DescricaoEPC"] = ghe.Epc_LTCAT();

            if (IsPpraPrevidenciario && PossueAgentesQuimicosPrevidenciario(ghe))
            {
                StringBuilder strEquipHtml = new StringBuilder();

                strEquipHtml.Append(ghe.EquipamentosHTML(laudoTecnico.hDT_LAUDO,calor.Fontes_Artificiais));
                strEquipHtml.Append("<p align='justify'><Font color='navy'>");
                strEquipHtml.Append("<b>");
                strEquipHtml.Append("Agentes Químicos");
                strEquipHtml.Append("</b> - ");
                strEquipHtml.Append("Avaliações Quantitativas - observância metodologia NHO Fundacentro. Certificação laboratorial anexa.");
                strEquipHtml.Append("</Font></p><br>");
                newRow["Equipamentos"] = strEquipHtml.ToString();
            }
            else
                newRow["Equipamentos"] = ghe.EquipamentosHTML(laudoTecnico.hDT_LAUDO, calor.Fontes_Artificiais);

            if (IsPpraPrevidenciario)
                newRow["Conclusao"] = empregadoFuncao.GetConclusaoPrevidenciario();
            else
                newRow["Conclusao"] = ghe.Conclusao_LTCAT(cliente);

            if (laudoTecnico.mirrorOld == null)
                laudoTecnico.Find();

            if (laudoTecnico.PrestadorID.mirrorOld == null)
                laudoTecnico.PrestadorID.Find();

            newRow["FotoDocumento"] = Fotos.PathFoto_Uri(laudoTecnico.PrestadorID.FotoRG);
            newRow["PrestadorNome"] = laudoTecnico.PrestadorID.NomeCompleto;
            newRow["PrestadorTitulo"] = laudoTecnico.PrestadorID.Titulo;
            newRow["PrestadorNumero"] = laudoTecnico.PrestadorID.Numero;
            newRow["AssinaturaPrestador"] = Fotos.PathFoto_Uri(laudoTecnico.PrestadorID.FotoAss);
            newRow["AssinaturaDataLaudo"] = dataInicioLaudo;//.AddMonths(1);  //dtAssinaturaLaudo;
        }
        #endregion

        #region PossueAgentesQuimicosPrevidenciario

        private bool PossueAgentesQuimicosPrevidenciario(Ghe ghe)
        {
            string where = "nID_FUNC=" + ghe.Id
                        + " AND nID_AG_NCV IN (6, 355)";

            int count  = new PPRA().ExecuteCount(where);

            return count > 0;
        }
        #endregion

        #region PopularRowAgentesQuimicos

        private void PopularRowAgentesQuimicos(Empregado empregado, Ghe ghe)
        {
            string where = "nID_FUNC=" + ghe.Id
                            + " AND nID_RSC IN(" + (int)Riscos.AgentesQuimicos
                                        + ", " + (int)Riscos.AgentesQuimicosB
                                        + ", " + (int)Riscos.AgentesQuimicosC
                                        + ", " + (int)Riscos.AgentesQuimicosD
                                        + ", " + (int)Riscos.AsbestosPoeirasMinerais
                                        + ", " + (int)Riscos.ACGIH + ")"
                            + " ORDER BY nID_FUNC, nID_RSC, nID_AG_NCV";

            ArrayList alAgentesQuimicos = new PPRA().Find(where);

            StringBuilder reconhecimentoAgentes = new StringBuilder();

            foreach (PPRA agenteQuimico in alAgentesQuimicos)
            {
                agenteQuimico.nID_AG_NCV.Find();

                if (IsPpraPrevidenciario
                 && !(agenteQuimico.nID_AG_NCV.Id == 6
                     || agenteQuimico.nID_AG_NCV.Id == 355))
                    continue;

                if (agenteQuimico.nID_AG_NCV.Nome.ToUpper().Trim() == "AUSÊNCIA DE FATOR DE RISCO") // .nID_AG_NCV.Codigo_eSocial == "09.01.001")
                    continue;


                if (IsPpraPrevidenciario && agenteQuimico.nID_AG_NCV.Id == 355)//Benzeno
                    reconhecimentoAgentes.Append(agenteQuimico.GetReconhecimentoPPRA(true).Replace("cuja concentração", "cuja pequena concentração"));
                else if (IsPpraPrevidenciario && agenteQuimico.nID_AG_NCV.Id == 6)//Manganês
                    reconhecimentoAgentes.Append(agenteQuimico.GetReconhecimentoPPRA(true).Replace("Manganês e seus compostos", "Manganês - Fumos de Solda"));
                else
                    reconhecimentoAgentes.Append(agenteQuimico.GetReconhecimentoPPRA(true));

                reconhecimentoAgentes.Append("<br>");
            }

            DataRow newRowAgentesQuimicos;

            foreach (PPRA agenteQuimico in alAgentesQuimicos)
            {
                agenteQuimico.nID_AG_NCV.Find();

                if (IsPpraPrevidenciario
                    && !(agenteQuimico.nID_AG_NCV.Id == 6
                        || agenteQuimico.nID_AG_NCV.Id == 355))
                    continue;

                if (agenteQuimico.nID_AG_NCV.Nome.ToUpper().Trim() == "AUSÊNCIA DE FATOR DE RISCO") // .nID_AG_NCV.Codigo_eSocial == "09.01.001")
                    continue;


                newRowAgentesQuimicos = dsAgentesQuimicos.Tables[0].NewRow();

                newRowAgentesQuimicos["IdEmpregado"] = empregado.Id;
                newRowAgentesQuimicos["IdLaudoTecnico"] = ghe.nID_LAUD_TEC.Id;

                if (IsPpraPrevidenciario && agenteQuimico.nID_AG_NCV.Id == 6)//Manganês
                    newRowAgentesQuimicos["NomeAgente"] = agenteQuimico.GetNomeAgente().Replace("Manganês e seus compostos", "Manganês - Fumos de Solda");
                else if (IsPpraPrevidenciario && agenteQuimico.nID_AG_NCV.Id == 355)//Benzeno
                {
                    if (agenteQuimico.GetNomeAgente().IndexOf("(Tintas - Pinturas)") == -1)
                        newRowAgentesQuimicos["NomeAgente"] = agenteQuimico.GetNomeAgente().Replace("benzeno", "benzeno (Tintas - Pinturas)");
                }
                else
                    newRowAgentesQuimicos["NomeAgente"] = agenteQuimico.GetNomeAgente();
                
                newRowAgentesQuimicos["AvaliacaoQuantitativa"] = agenteQuimico.GetAvaliacaoQuantitativa_LTCAT();
                newRowAgentesQuimicos["LimiteTolerancia"] = agenteQuimico.GetLimiteTolerancia();
                newRowAgentesQuimicos["TempoExposicao"] = agenteQuimico.GetModoExposicao();
                newRowAgentesQuimicos["FoiUltrapassado"] = agenteQuimico.GetLimiteFoiUltrapassado();

                agenteQuimico.nID_RSC.Find();
                if (!agenteQuimico.nID_RSC.Qualitativo && agenteQuimico.Risco_Insignificante == true)
                {
                    newRowAgentesQuimicos["ReconhecimentoAgentes"] = "*** Risco Insignificante" + System.Environment.NewLine + reconhecimentoAgentes.ToString();
                }
                else
                {
                    newRowAgentesQuimicos["ReconhecimentoAgentes"] = reconhecimentoAgentes.ToString();
                }

                dsAgentesQuimicos.Tables[0].Rows.Add(newRowAgentesQuimicos);
           }

            if (alAgentesQuimicos.Count.Equals(0))
            {
                newRowAgentesQuimicos = dsAgentesQuimicos.Tables[0].NewRow();

                newRowAgentesQuimicos["IdEmpregado"] = empregado.Id;
                newRowAgentesQuimicos["IdLaudoTecnico"] = ghe.nID_LAUD_TEC.Id;
                newRowAgentesQuimicos["NomeAgente"] = "Inexistente";
                newRowAgentesQuimicos["AvaliacaoQuantitativa"] = "-";
                newRowAgentesQuimicos["LimiteTolerancia"] = "-";
                newRowAgentesQuimicos["TempoExposicao"] = "-";
                newRowAgentesQuimicos["FoiUltrapassado"] = "-";

                dsAgentesQuimicos.Tables[0].Rows.Add(newRowAgentesQuimicos);
            }
        }



        private void PopularRowVibracoes(Empregado empregado, Ghe ghe)
        {
            string where = "nID_FUNC=" + ghe.Id
                            + " AND nID_RSC IN(" + (int)Riscos.Vibracoes
                                        + ", " + (int)Riscos.VibracoesVCI + ")"
                            + " ORDER BY nID_FUNC, nID_RSC, nID_AG_NCV";

            ArrayList alAgentesQuimicos = new PPRA().Find(where);

            StringBuilder reconhecimentoAgentes = new StringBuilder();

            foreach (PPRA agenteQuimico in alAgentesQuimicos)
            {                

                reconhecimentoAgentes.Append(agenteQuimico.GetReconhecimentoPPRA(true));

                reconhecimentoAgentes.Append("<br>");
            }

            DataRow newRowAgentesQuimicos;

            foreach (PPRA agenteQuimico in alAgentesQuimicos)
            {
               

                newRowAgentesQuimicos = dsVibracoes.Tables[0].NewRow();

                newRowAgentesQuimicos["IdEmpregado"] = empregado.Id;
                newRowAgentesQuimicos["IdLaudoTecnico"] = ghe.nID_LAUD_TEC.Id;

                newRowAgentesQuimicos["NomeAgente"] = agenteQuimico.GetNomeAgente();

                newRowAgentesQuimicos["AvaliacaoQuantitativa"] = agenteQuimico.GetAvaliacaoQuantitativa_LTCAT();
                newRowAgentesQuimicos["LimiteTolerancia"] = agenteQuimico.GetLimiteTolerancia();
                newRowAgentesQuimicos["TempoExposicao"] = agenteQuimico.GetModoExposicao();
                newRowAgentesQuimicos["FoiUltrapassado"] = agenteQuimico.GetLimiteFoiUltrapassado();

                agenteQuimico.nID_RSC.Find();
                if (!agenteQuimico.nID_RSC.Qualitativo && agenteQuimico.Risco_Insignificante == true)
                {
                    newRowAgentesQuimicos["ReconhecimentoAgentes"] = "*** Risco Insignificante" + System.Environment.NewLine + reconhecimentoAgentes.ToString();
                }
                else
                {
                    newRowAgentesQuimicos["ReconhecimentoAgentes"] = reconhecimentoAgentes.ToString();
                }


                dsVibracoes.Tables[0].Rows.Add(newRowAgentesQuimicos);
            }

            //if (alAgentesQuimicos.Count.Equals(0))
            //{
            //    newRowAgentesQuimicos = dsAgentesQuimicos.Tables[0].NewRow();

            //    newRowAgentesQuimicos["IdEmpregado"] = empregado.Id;
            //    newRowAgentesQuimicos["IdLaudoTecnico"] = ghe.nID_LAUD_TEC.Id;
            //    newRowAgentesQuimicos["NomeAgente"] = "Inexistente";
            //    newRowAgentesQuimicos["AvaliacaoQuantitativa"] = "-";
            //    newRowAgentesQuimicos["LimiteTolerancia"] = "-";
            //    newRowAgentesQuimicos["TempoExposicao"] = "-";
            //    newRowAgentesQuimicos["FoiUltrapassado"] = "-";

            //    dsAgentesQuimicos.Tables[0].Rows.Add(newRowAgentesQuimicos);
            //}
        }

        #endregion

        #region GetTableAgentesQuimicos

        private DataTable GetTableAgentesQuimicos()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdEmpregado", typeof(int));
            table.Columns.Add("IdLaudoTecnico", typeof(int));
            table.Columns.Add("NomeAgente", typeof(string));
            table.Columns.Add("AvaliacaoQuantitativa", typeof(string));
            table.Columns.Add("LimiteTolerancia", typeof(string));
            table.Columns.Add("TempoExposicao", typeof(string));
            table.Columns.Add("FoiUltrapassado", typeof(string));
            table.Columns.Add("ReconhecimentoAgentes", typeof(string));
            return table;
        }
        #endregion

        #region GetTable

        private DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("CarimboCNPJ", typeof(string));
            table.Columns.Add("IdEmpregado", typeof(int));
            table.Columns.Add("NOME_EMPREGADO", typeof(string));
            table.Columns.Add("CTPS", typeof(string));
            table.Columns.Add("PIS_PASEP", typeof(string));
            table.Columns.Add("DATA_NASCIMENTO", typeof(string));
            table.Columns.Add("SEXO", typeof(string));
            table.Columns.Add("ComFoto", typeof(bool));
            table.Columns.Add("FOTO_EMPREGADO", typeof(string));
            table.Columns.Add("TIPO_LTCAT", typeof(int));
            table.Columns.Add("RG", typeof(string));
            table.Columns.Add("REGIME_REVEZAMENTO", typeof(string));
            table.Columns.Add("DataInicioLaudo", typeof(DateTime));
            table.Columns.Add("DataTerminoLaudo", typeof(DateTime));
            table.Columns.Add("DataInicio", typeof(string));
            table.Columns.Add("DataTermino", typeof(string));
            table.Columns.Add("CNPJLocal", typeof(string));
            table.Columns.Add("Cidade", typeof(string));
            table.Columns.Add("Jornada", typeof(string));
            table.Columns.Add("Setor", typeof(string));
            table.Columns.Add("Funcao", typeof(string));
            table.Columns.Add("Cargo", typeof(string));
            table.Columns.Add("CBO", typeof(string));
            table.Columns.Add("GFIP", typeof(string));
            table.Columns.Add("DescricaoSetor", typeof(string));
            table.Columns.Add("DescricaoFuncao", typeof(string));
            table.Columns.Add("NivelRuido", typeof(string));
            table.Columns.Add("isAdministrativo", typeof(bool));
            table.Columns.Add("TempoExposicao", typeof(string));
            table.Columns.Add("ExposicaoMax", typeof(string));
            table.Columns.Add("AcimaLimite80", typeof(string));
            table.Columns.Add("AcimaLimite90", typeof(string));
            table.Columns.Add("AcimaLimite85", typeof(string));
            table.Columns.Add("ReconhecimentoRuido", typeof(string));
            table.Columns.Add("isCalorDescansoLocalDiverso", typeof(bool));
            table.Columns.Add("CalorValor", typeof(string));
            table.Columns.Add("CalorLimite", typeof(string));
            table.Columns.Add("TipoAtividade", typeof(string));
            table.Columns.Add("TaxaMetabolismo", typeof(string));
            table.Columns.Add("TempoTrabalho", typeof(string));
            table.Columns.Add("TempoDescanso", typeof(string));
            table.Columns.Add("IBUTGTrabalho", typeof(string));
            table.Columns.Add("IBUTGDescanso", typeof(string));
            table.Columns.Add("MetabolismoTrabalho", typeof(string));
            table.Columns.Add("MetabolismoDescanso", typeof(string));
            table.Columns.Add("CalorUltrapassado", typeof(string));
            table.Columns.Add("CalorMaximoPermissivel", typeof(string));
            table.Columns.Add("ReconhecimentoCalor", typeof(string));
            table.Columns.Add("DemaisAgentes", typeof(string));
            table.Columns.Add("DescricaoEPI", typeof(string));
            table.Columns.Add("DescricaoEPC", typeof(string));
            table.Columns.Add("Equipamentos", typeof(string));
            table.Columns.Add("Conclusao", typeof(string));
            table.Columns.Add("FotoDocumento", typeof(string));
            table.Columns.Add("PrestadorTitulo", typeof(string));
            table.Columns.Add("PrestadorNumero", typeof(string));
            table.Columns.Add("PrestadorNome", typeof(string));
            table.Columns.Add("AssinaturaPrestador", typeof(string));
            table.Columns.Add("AssinaturaDataLaudo", typeof(DateTime));
            table.Columns.Add("Vibracoes", typeof(string));
            return table;
        }
        #endregion
    }
}
