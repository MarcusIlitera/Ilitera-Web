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
    public class DataSourceAbsenteismo : DataSourceBase
    {
        private Int32 xIdEmpregado;
        private string xData1;
        private string xData2;


        public DataSourceAbsenteismo(Int32 zIdEmpregado, string zData1, string zData2)
        {
            this.xIdEmpregado = zIdEmpregado;
            this.xData1 = zData1;
            this.xData2 = zData2;
        }

        public RptAbsenteismo GetReport()
        {
    
            DataSet dsAbsenteismo = new DataSet();

            //PopulaDSAbsenteismo(dsAbsenteismo);

            RptAbsenteismo report = new RptAbsenteismo();
            //report.OpenSubreport("Absenteismo").SetDataSource(dsAbsenteismo);
            report.SetDataSource(GetDadosBasicos());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;

        }
        #region DadosBasicos

        private DataSet GetDadosBasicos()
        {
           
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");

            table.Columns.Add("Observacoes", Type.GetType("System.String"));
            table.Columns.Add("Data", Type.GetType("System.String"));
            table.Columns.Add("Data_Abs", typeof(string));
            table.Columns.Add("Previsao_Retorno_Abs", typeof(string));
            table.Columns.Add("Data_Retorno_Abs", typeof(string));
            table.Columns.Add("CID", typeof(string));
            table.Columns.Add("Data_Acid", typeof(string));
            table.Columns.Add("Hora_Acid", typeof(string));
            table.Columns.Add("Tipo_Acid", typeof(string));
            table.Columns.Add("Local_Acid", typeof(string));
            table.Columns.Add("Numero_CAT", typeof(string));
            table.Columns.Add("Data_Emissao_CAT", typeof(string));
            table.Columns.Add("Hora_CAT", typeof(string));
            table.Columns.Add("Emitente_CAT", typeof(string));
            table.Columns.Add("Tipo_CAT", typeof(string));
            table.Columns.Add("Colaborador", typeof(string));
            table.Columns.Add("Data1", typeof(string));
            table.Columns.Add("Data2", typeof(string));
            table.Columns.Add("Tipo_Abs", typeof(string));

            table.Columns.Add("Total_Afastamentos", typeof(string));
            table.Columns.Add("Total_Horas_Af", typeof(string));
            table.Columns.Add("Total_Horas", typeof(string));
            table.Columns.Add("Total_Dias_Af", typeof(string));
            table.Columns.Add("Afast_Ocupacional", typeof(string));
            table.Columns.Add("Afast_Nao_Ocupacional", typeof(string));
            table.Columns.Add("Horas_Afast_Ocupacional", typeof(string));
            table.Columns.Add("Horas_Afast_Nao_Ocupacional", typeof(string));
            table.Columns.Add("Indice_Absenteismo", typeof(string));
            table.Columns.Add("Total_Dias", typeof(string));
            table.Columns.Add("Dias_Uteis", typeof(string));
            table.Columns.Add("Dias_Nao_Uteis", typeof(string));


            //table.Columns.Add("Cidade", Type.GetType("System.String"));
            //table.Columns.Add("Estado", Type.GetType("System.String"));
            //table.Columns.Add("CNPJ", Type.GetType("System.String"));
            ds.Tables.Add(table);

           // DataRow row = ds.Tables[0].NewRow();

            Empregado zEmpregado = new Empregado(xIdEmpregado);




            DataRow newrow;
            int xAfastamentos = 0;
            int xAfastamentos_Oc = 0;
            int xAfastamentos_NOc = 0;
            DateTime zData1;
            DateTime zData2;
            Int32 zHoras_Afastadas = 0;
            Int32 zHoras_Afastadas_Oc = 0;
            Int32 zHoras_Afastadas_NOc = 0;
            Int32 zDias_Afastados = 0;
            Int32 zTotal_Periodo = 0;
            Int32 zDias_Uteis = 0;
            Int32 zDias_Nao_Uteis = 0;
            Int32 zTotal_Dias = 0;

            DataTable tableAbs = GetTableAbsenteismo();

            //dsAbsenteismo.Tables.Add(tableAbs);


            string where = "IdEmpregado=" + xIdEmpregado.ToString() + " and DataInicial between convert( smalldatetime, '" + xData1 + "',103 ) and convert( smalldatetime, '" + xData2 + "', 103 )  ORDER BY DataInicial";
            ArrayList alAfastamentos = new Afastamento().Find(where);


            //zTotal_Periodo = Calcular_Horas_Afastadas(System.Convert.ToDateTime(xData1), System.Convert.ToDateTime(xData2));
            //ver se colaborador tem Jornada de Trabalho preenchida,  senão, pegar Cliente->Carga_Horaria,  se este nulo, pegar 220 horas / mês
            zTotal_Periodo = Calcular_Horas_Afastadas_Total(System.Convert.ToDateTime(xData1), System.Convert.ToDateTime(xData2), xIdEmpregado);



            zTotal_Dias = Calcular_Dias_Uteis_Afastados(System.Convert.ToDateTime(xData1), System.Convert.ToDateTime(xData2));

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            foreach (Afastamento afastamento in alAfastamentos)
            {


                newrow = ds.Tables[0].NewRow();

                newrow["Total_Horas"] = zTotal_Periodo.ToString().Trim();
                newrow["Data_Abs"] = afastamento.DataInicial.ToString("dd/MM/yyyy HH:mm");
                zData1 = afastamento.DataInicial;
                zData2 = System.Convert.ToDateTime("01/01/1900");

                //trocar local do acidente pela observação
                if (afastamento.Obs.Trim() == "")
                {
                    newrow["Local_Acid"] = "-";
                }
                else
                {
                    newrow["Local_Acid"] = afastamento.Obs.Trim();
                }


                if (afastamento.DataPrevista != new DateTime() && afastamento.DataPrevista != new DateTime(1753, 1, 1))
                {
                    newrow["Previsao_Retorno_Abs"] = afastamento.DataPrevista.ToString("dd/MM/yyyy");
                    zData2 = afastamento.DataPrevista;

                    if (zData2 > System.Convert.ToDateTime(xData2, ptBr)) zData2 = System.Convert.ToDateTime(xData2, ptBr);
                }
                else
                {
                    newrow["Previsao_Retorno_Abs"] = "";
                }


                if (afastamento.DataVolta != new DateTime() && afastamento.DataVolta != new DateTime(1753, 1, 1))
                {
                    newrow["Data_Retorno_Abs"] = afastamento.DataVolta.ToString("dd/MM/yyyy HH:mm");
                    zData2 = afastamento.DataVolta;

                    if (zData2 > System.Convert.ToDateTime(xData2, ptBr)) zData2 = System.Convert.ToDateTime(xData2, ptBr);
                }
                else
                {
                    newrow["Data_Retorno_Abs"] = "";
                }


                if (afastamento.IndTipoAfastamento == 1)
                {
                    newrow["Tipo_Abs"] = "Ocupacional";
                    xAfastamentos_Oc++;

                    if (zData2.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        zHoras_Afastadas_Oc = zHoras_Afastadas_Oc + Calcular_Horas_Afastadas_Jornada(zData1, zData2, xIdEmpregado);
                    }

                }
                else if (afastamento.IndTipoAfastamento == 2)
                {
                    newrow["Tipo_Abs"] = "Não-Ocupacional";
                    xAfastamentos_NOc++;

                    if (zData2.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        zHoras_Afastadas_NOc = zHoras_Afastadas_NOc + Calcular_Horas_Afastadas_Jornada(zData1, zData2, xIdEmpregado);
                    }

                }
                //else if (afastamento.IndTipoAfastamento == 3) newrow["Tipo_Abs"] = "Falta";


                if (afastamento.IdAcidente.Id == 0)
                {
                    newrow["Data_Acid"] = "-";
                    newrow["Hora_Acid"] = "-";
                    newrow["Tipo_Acid"] = "-";
                    //newrow["Local_Acid"] = "-";
                }
                else
                {
                    afastamento.IdAcidente.Find();
                    newrow["Data_Acid"] = afastamento.IdAcidente.DataAcidente.ToString("dd/MM/yyyy HH:mm");
                    newrow["Hora_Acid"] = ""; //afastamento.IdAcidente.DataAcidente.ToString("HH:mm");

                    switch (afastamento.IdAcidente.IndTipoAcidente)
                    {
                        case (int)TipoAcidente.Doenca:
                            newrow["Tipo_Acid"] = "Doença";
                            break;
                        case (int)TipoAcidente.Tipico:
                            newrow["Tipo_Acid"] = "Típico";
                            break;
                        case (int)TipoAcidente.Trajeto:
                            newrow["Tipo_Acid"] = "Trajeto";
                            break;
                    }

                    //afastamento.IdAcidente.IdLocalAcidente.Find();
                    //newrow["Local_Acid"] = afastamento.IdAcidente.EspecLocal;
                }


                string zCid = "";
                                

                if (afastamento.IdCID.Id != 0)
                {
                    afastamento.IdCID.Find();
                    //newrow["CID"] = afastamento.IdCID.Descricao.ToString();
                    zCid = afastamento.IdCID.Descricao.ToString();

                    if (afastamento.IdCID2 != 0)
                    {
                        CID xCid = new CID();
                        xCid.Find(afastamento.IdCID2);

                        zCid = zCid + "  |  " + xCid.Descricao.Trim();
                    }

                    if (afastamento.IdCID3 != 0)
                    {
                        CID xCid = new CID();
                        xCid.Find(afastamento.IdCID3);

                        zCid = zCid + "  |  " + xCid.Descricao.Trim();
                    }

                    if (afastamento.IdCID4 != 0)
                    {
                        CID xCid = new CID();
                        xCid.Find(afastamento.IdCID4);

                        zCid = zCid + "  |  " + xCid.Descricao.Trim();
                    }

                    newrow["CID"] = zCid;

                }
                else newrow["CID"] = "";


                if (afastamento.IdAcidente.IdCAT != null)
                {
                    afastamento.IdAcidente.IdCAT.Find();
                    newrow["Numero_CAT"] = afastamento.IdAcidente.IdCAT.NumeroCAT.ToString();
                    newrow["Data_Emissao_CAT"] = afastamento.IdAcidente.IdCAT.DataEmissao.ToString("dd/MM/yyyy HH:mm");
                    newrow["Hora_CAT"] = ""; // afastamento.IdAcidente.IdCAT.DataEmissao.ToString("HH:mm");

                    if (afastamento.IdAcidente.IdCAT.IndEmitente == 1)
                        newrow["Emitente_CAT"] = "Empregador";
                    else if (afastamento.IdAcidente.IdCAT.IndEmitente == 2)
                        newrow["Emitente_CAT"] = "Sindicato";
                    else if (afastamento.IdAcidente.IdCAT.IndEmitente == 3)
                        newrow["Emitente_CAT"] = "Médico";
                    else if (afastamento.IdAcidente.IdCAT.IndEmitente == 4)
                        newrow["Emitente_CAT"] = "Segurado ou Dependente";
                    else if (afastamento.IdAcidente.IdCAT.IndEmitente == 5)
                        newrow["Emitente_CAT"] = "Autoridade Pública";


                    if (afastamento.IdAcidente.IdCAT.IndTipoCAT == 1)
                        newrow["Tipo_CAT"] = "Inicial";
                    else if (afastamento.IdAcidente.IdCAT.IndTipoCAT == 2)
                        newrow["Tipo_CAT"] = "Reabertura";
                    else if (afastamento.IdAcidente.IdCAT.IndTipoCAT == 3)
                        newrow["Tipo_CAT"] = "Comunicação de Óbito";

                }
                else
                {
                    newrow["Numero_CAT"] = "-";
                    newrow["Data_Emissao_CAT"] = "-";
                    newrow["Hora_CAT"] = "-";
                    newrow["Emitente_CAT"] = "-";
                    newrow["Tipo_CAT"] = "-";
                }

                newrow["Colaborador"] = "";
                newrow["Data1"] = "";
                newrow["Data2"] = "";

                //calculo dos totais
                xAfastamentos = xAfastamentos_NOc + xAfastamentos_Oc;

                if (zData2.ToString("dd/MM/yyyy") != "01/01/1900")
                {
                    zHoras_Afastadas = zHoras_Afastadas_NOc + zHoras_Afastadas_Oc;
                    //zDias_Afastados = System.Convert.ToInt32(zHoras_Afastadas / 8) + 1;

                    if (zHoras_Afastadas > 8)
                    {
                        //if (zData1 != zData2)
                        //{                            
                            zDias_Uteis = zDias_Uteis + (Calcular_Dias_Uteis_Afastados(System.Convert.ToDateTime(zData1), System.Convert.ToDateTime(zData2))); //- 1);                            
                            zDias_Nao_Uteis = zDias_Nao_Uteis + Calcular_Dias_Nao_Uteis_Afastados(System.Convert.ToDateTime(zData1), System.Convert.ToDateTime(zData2));
                        //}
                        //else
                        //{
                        //    zDias_Uteis = zDias_Uteis + (Calcular_Dias_Uteis_Afastados(System.Convert.ToDateTime(zData1), System.Convert.ToDateTime(zData2)));
                        //    zDias_Nao_Uteis = zDias_Nao_Uteis + Calcular_Dias_Nao_Uteis_Afastados(System.Convert.ToDateTime(zData1), System.Convert.ToDateTime(zData2));
                        //}
                        

                    }
                    else
                    {
                        zDias_Uteis = 1;
                        zDias_Nao_Uteis = 0;
                    }

                    zDias_Afastados = zDias_Nao_Uteis + zDias_Uteis;

                }

                newrow["Total_Dias"] = zTotal_Dias;
                newrow["Dias_Uteis"] = zDias_Uteis;
                newrow["Dias_Nao_Uteis"] = zDias_Nao_Uteis;

                newrow["Total_Afastamentos"] = xAfastamentos.ToString();
                newrow["Total_Horas_Af"] = zHoras_Afastadas.ToString("##,#");
                newrow["Total_Dias_Af"] = zDias_Afastados;

                if (xAfastamentos_Oc > 0)
                {
                    newrow["Afast_Ocupacional"] = xAfastamentos_Oc.ToString() ;
                    newrow["Horas_Afast_Ocupacional"] = zHoras_Afastadas_Oc.ToString("##,#");
                }
                else
                {
                    newrow["Afast_Ocupacional"] = " ";
                    newrow["Horas_Afast_Ocupacional"] = " ";
                }

                if (xAfastamentos_NOc > 0)
                {
                    newrow["Afast_Nao_Ocupacional"] = xAfastamentos_NOc.ToString() ;
                    newrow["Horas_Afast_Nao_Ocupacional"] = zHoras_Afastadas_NOc.ToString("##,#");
                }
                else
                {
                    newrow["Afast_Nao_Ocupacional"] = " ";
                    newrow["Horas_Afast_Nao_Ocupacional"] = " ";
                }

                newrow["Indice_Absenteismo"] = (((zHoras_Afastadas * 1.0) / (zTotal_Periodo * 1.0)) * 100.0).ToString("0.00") + " %";

                newrow["Observacoes"] = zEmpregado.tNO_EMPG.ToString();
                newrow["Data"] = xData1 + " a " + xData2;


                ds.Tables[0].Rows.Add(newrow);

            }






            //row["Observacoes"] = zEmpregado.tNO_EMPG.ToString();
            //row["Data"] = xData1 + " a " + xData2;
            ////row["Cidade"] = municipio.NomeCompleto;
            ////row["Estado"] = municipio.IdUnidadeFederativa.NomeAbreviado;
            ////row["CNPJ"] = cliente.NomeCodigo;

            //ds.Tables[0].Rows.Add(row);

            return ds;
        }
        #endregion

        #region DataTables

       

        private DataTable GetTableAbsenteismo()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("Data_Abs", typeof(string));
            table.Columns.Add("Previsao_Retorno_Abs", typeof(string));
            table.Columns.Add("Data_Retorno_Abs", typeof(string));
            table.Columns.Add("CID", typeof(string));
            table.Columns.Add("Data_Acid", typeof(string));
            table.Columns.Add("Hora_Acid", typeof(string));
            table.Columns.Add("Tipo_Acid", typeof(string));
            table.Columns.Add("Local_Acid", typeof(string));
            table.Columns.Add("Numero_CAT", typeof(string));
            table.Columns.Add("Data_Emissao_CAT", typeof(string));
            table.Columns.Add("Hora_CAT", typeof(string));
            table.Columns.Add("Emitente_CAT", typeof(string));
            table.Columns.Add("Tipo_CAT", typeof(string));
            table.Columns.Add("Colaborador", typeof(string));
            table.Columns.Add("Data1", typeof(string));
            table.Columns.Add("Data2", typeof(string));
            table.Columns.Add("Tipo_Abs", typeof(string));

            table.Columns.Add("Total_Afastamentos", typeof(string));
            table.Columns.Add("Total_Horas_Af", typeof(string));
            table.Columns.Add("Total_Horas", typeof(string));
            table.Columns.Add("Total_Dias_Af", typeof(string));
            table.Columns.Add("Afast_Ocupacional", typeof(string));
            table.Columns.Add("Afast_Nao_Ocupacional", typeof(string));
            table.Columns.Add("Horas_Afast_Ocupacional", typeof(string));
            table.Columns.Add("Horas_Afast_Nao_Ocupacional", typeof(string));
            table.Columns.Add("Indice_Absenteismo", typeof(string));


            return table;
        }

       
        #endregion

        #region Quadros

        private void PopulaDSAbsenteismo(DataSet dsAbsenteismo)
        {
            DataRow newrow;
            int xAfastamentos = 0;
            int xAfastamentos_Oc = 0;
            int xAfastamentos_NOc = 0;
            DateTime zData1;
            DateTime zData2;
            Int32 zHoras_Afastadas = 0;
            Int32 zHoras_Afastadas_Oc = 0;
            Int32 zHoras_Afastadas_NOc = 0;
            Int32 zDias_Afastados = 0;
            Int32 zTotal_Periodo = 0;
            

            DataTable tableAbs = GetTableAbsenteismo();
                 
            dsAbsenteismo.Tables.Add(tableAbs);


            string where = "IdEmpregado=" + xIdEmpregado.ToString() + " and DataInicial between convert( smalldatetime, '" + xData1 + "',103 ) and convert( smalldatetime, '" + xData2 + "', 103 )  ORDER BY DataInicial";
            ArrayList alAfastamentos = new Afastamento().Find(where);


            zTotal_Periodo = Calcular_Horas_Afastadas_Total( System.Convert.ToDateTime( xData1), System.Convert.ToDateTime(xData2), xIdEmpregado);  


            foreach (Afastamento afastamento in alAfastamentos)
            {


                newrow = dsAbsenteismo.Tables[0].NewRow();

                newrow["Total_Horas"] = zTotal_Periodo.ToString().Trim();
                newrow["Data_Abs"] = afastamento.DataInicial.ToString("dd/MM/yyyy HH:mm");
                zData1 = afastamento.DataInicial;
                zData2 = System.Convert.ToDateTime("01/01/1900");


                //trocar local do acidente pela observação
                if (afastamento.Obs.Trim() == "")
                {
                    newrow["Local_Acid"] = "-";
                }
                else
                {
                    newrow["Local_Acid"] = afastamento.Obs.Trim();
                }


                if (afastamento.DataPrevista != new DateTime() && afastamento.DataPrevista != new DateTime(1753, 1, 1))
                {
                    newrow["Previsao_Retorno_Abs"] = afastamento.DataPrevista.ToString("dd/MM/yyyy");
                    zData2 = afastamento.DataPrevista;
                }
                else
                {
                    newrow["Previsao_Retorno_Abs"] = "";                 
                }


                if (afastamento.DataVolta != new DateTime() && afastamento.DataVolta != new DateTime(1753, 1, 1))
                {
                    newrow["Data_Retorno_Abs"] = afastamento.DataVolta.ToString("dd/MM/yyyy HH:mm");
                    zData2 = afastamento.DataVolta;
                }
                else
                {
                    newrow["Data_Retorno_Abs"] = "";
                }


                if (afastamento.IndTipoAfastamento == 1)
                {
                    newrow["Tipo_Abs"] = "Ocupacional";
                    xAfastamentos_Oc++;

                    if (zData2.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        zHoras_Afastadas_Oc = zHoras_Afastadas_Oc + Calcular_Horas_Afastadas_Jornada(zData1, zData2, xIdEmpregado);                        
                    }

                }
                else if (afastamento.IndTipoAfastamento == 2)
                {
                    newrow["Tipo_Abs"] = "Não-Ocupacional";
                    xAfastamentos_NOc++;

                    if (zData2.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        zHoras_Afastadas_NOc = zHoras_Afastadas_NOc + Calcular_Horas_Afastadas_Jornada(zData1, zData2, xIdEmpregado);
                    }

                }
                //else if (afastamento.IndTipoAfastamento == 3) newrow["Tipo_Abs"] = "Falta";


                if (afastamento.IdAcidente.Id == 0)
                {
                    newrow["Data_Acid"] = "-";
                    newrow["Hora_Acid"] = "-";
                    newrow["Tipo_Acid"] = "-";
                    //newrow["Local_Acid"] = "-";
                }
                else
                {
                    afastamento.IdAcidente.Find();
                    newrow["Data_Acid"] = afastamento.IdAcidente.DataAcidente.ToString("dd/MM/yyyy HH:mm");
                    newrow["Hora_Acid"] = ""; //afastamento.IdAcidente.DataAcidente.ToString("HH:mm");

                    switch (afastamento.IdAcidente.IndTipoAcidente)
                    {
                        case (int)TipoAcidente.Doenca:
                            newrow["Tipo_Acid"] = "Doença";
                            break;
                        case (int)TipoAcidente.Tipico:
                            newrow["Tipo_Acid"] = "Típico";
                            break;
                        case (int)TipoAcidente.Trajeto:
                            newrow["Tipo_Acid"] = "Trajeto";
                            break;
                    }

                    //afastamento.IdAcidente.IdLocalAcidente.Find();
                    //newrow["Local_Acid"] = afastamento.IdAcidente.EspecLocal;
                }


                if (afastamento.IdCID.Id != 0)
                {
                    afastamento.IdCID.Find();
                    newrow["CID"] = afastamento.IdCID.Descricao.ToString();
                }
                else newrow["CID"] = "";


                if (afastamento.IdAcidente.IdCAT != null)
                {
                    afastamento.IdAcidente.IdCAT.Find();
                    newrow["Numero_CAT"] = afastamento.IdAcidente.IdCAT.NumeroCAT.ToString();
                    newrow["Data_Emissao_CAT"] = afastamento.IdAcidente.IdCAT.DataEmissao.ToString("dd/MM/yyyy HH:mm");
                    newrow["Hora_CAT"] = ""; // afastamento.IdAcidente.IdCAT.DataEmissao.ToString("HH:mm");

                    if (afastamento.IdAcidente.IdCAT.IndEmitente == 1)
                        newrow["Emitente_CAT"] = "Empregador";
                    else if (afastamento.IdAcidente.IdCAT.IndEmitente == 2)
                        newrow["Emitente_CAT"] = "Sindicato";
                    else if (afastamento.IdAcidente.IdCAT.IndEmitente == 3)
                        newrow["Emitente_CAT"] = "Médico";
                    else if (afastamento.IdAcidente.IdCAT.IndEmitente == 4)
                        newrow["Emitente_CAT"] = "Segurado ou Dependente";
                    else if (afastamento.IdAcidente.IdCAT.IndEmitente == 5)
                        newrow["Emitente_CAT"] = "Autoridade Pública";


                    if (afastamento.IdAcidente.IdCAT.IndTipoCAT == 1)
                        newrow["Tipo_CAT"] = "Inicial";
                    else if (afastamento.IdAcidente.IdCAT.IndTipoCAT == 2)
                        newrow["Tipo_CAT"] = "Reabertura";
                    else if (afastamento.IdAcidente.IdCAT.IndTipoCAT == 3)
                        newrow["Tipo_CAT"] = "Comunicação de Óbito";

                }
                else
                {
                    newrow["Numero_CAT"] = "-";
                    newrow["Data_Emissao_CAT"] = "-";
                    newrow["Hora_CAT"] = "-";
                    newrow["Emitente_CAT"] = "-";
                    newrow["Tipo_CAT"] = "-";
                }

                newrow["Colaborador"] = "";
                newrow["Data1"] = "";
                newrow["Data2"] = "";

                //calculo dos totais
                xAfastamentos = xAfastamentos_NOc + xAfastamentos_Oc;

                if (zData2.ToString("dd/MM/yyyy") != "01/01/1900")
                {
                    zHoras_Afastadas = zHoras_Afastadas_NOc + zHoras_Afastadas_Oc;
                    zDias_Afastados = System.Convert.ToInt32(zHoras_Afastadas / 8) + 1;
                }

                newrow["Total_Afastamentos"] = xAfastamentos.ToString();
                newrow["Total_Horas_Af"] = zHoras_Afastadas.ToString("##,#");
                newrow["Total_Dias_Af"] = zDias_Afastados;

                if (xAfastamentos_Oc > 0)
                {
                    newrow["Afast_Ocupacional"] = "( " + xAfastamentos_Oc.ToString() + " )";
                    newrow["Horas_Afast_Ocupacional"] = zHoras_Afastadas_Oc.ToString("##,#");
                }
                else
                {
                    newrow["Afast_Ocupacional"] = " ";
                    newrow["Horas_Afast_Ocupacional"] = " ";
                }

                if (xAfastamentos_NOc > 0)
                {
                    newrow["Afast_Nao_Ocupacional"] = "( " + xAfastamentos_NOc.ToString() + " )";
                    newrow["Horas_Afast_Nao_Ocupacional"] = zHoras_Afastadas_NOc.ToString("##,#");
                }
                else
                {
                    newrow["Afast_Nao_Ocupacional"] = " ";
                    newrow["Horas_Afast_Nao_Ocupacional"] = " ";
                }

                newrow["Indice_Absenteismo"] = (((zHoras_Afastadas * 1.0) / (zTotal_Periodo * 1.0)) * 100.0).ToString("0.00") + " %";


                
                dsAbsenteismo.Tables[0].Rows.Add(newrow);

            }
            
        }
        
        
        private Int32 Calcular_Horas_Afastadas(DateTime zData1,  DateTime zData2)
        {
            Int32 zTotal = 0;
            

            if (zData1 > zData2) return 0;


            for (DateTime rData = zData1; rData <= zData2; )
            {

                if (rData.DayOfWeek != DayOfWeek.Saturday && rData.DayOfWeek != DayOfWeek.Sunday)
                {
                    if (rData.Hour >= 8 && rData.Hour < 17 && rData.Hour != 12)
                    {
                        zTotal++;
                    }
                    rData = rData.AddHours(1);
                }
                else
                {
                    rData = rData.AddDays(1);
                } 

            }

            return zTotal;
        }




        private Int32 Calcular_Horas_Afastadas_Jornada(DateTime zData1, DateTime zData2, Int32 zIdEmpregado)
        {
            Int32 zTotal = 0;


            if (zData1 > zData2) return 0;


            Empregado zEmpregado = new Empregado(zIdEmpregado);

            EmpregadoFuncao zEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(zEmpregado);


            //ver se colaborador tem jornada de trabalho definida  campo tbltempo_exp->svl_hor_sem  ( * 5 temos o valor mensal )
            zEmpregadoFuncao.nID_TEMPO_EXP.Find();

            if (zEmpregadoFuncao.nID_TEMPO_EXP.sVL_HOR_NUM == 0)
            {

                for (DateTime rData = zData1; rData < zData2; )
                {

                    if (rData.DayOfWeek != DayOfWeek.Saturday && rData.DayOfWeek != DayOfWeek.Sunday)
                    {
                        if (rData.Hour >= 8 && rData.Hour < 17 && rData.Hour != 12)
                        {
                            zTotal++;
                        }
                        rData = rData.AddHours(1);
                    }
                    else
                    {
                        rData = rData.AddDays(1);
                    }

                }
            }
            else
            {
                double zHora_Aux = 9.0 + zEmpregadoFuncao.nID_TEMPO_EXP.sVL_HOR_NUM;

                for (DateTime rData = zData1; rData < zData2; )
                {

                    if (zEmpregadoFuncao.nID_TEMPO_EXP.sVL_HOR_SEM / zEmpregadoFuncao.nID_TEMPO_EXP.sVL_HOR_NUM < 5.9)   // segunda a sexta
                    {

                        if (rData.DayOfWeek != DayOfWeek.Saturday && rData.DayOfWeek != DayOfWeek.Sunday)
                        {
                            if (rData.Hour >= 8 && rData.Hour < zHora_Aux && rData.Hour != 12)
                            {
                                zTotal++;
                            }
                            rData = rData.AddHours(1);
                        }
                        else
                        {
                            rData = rData.AddDays(1);
                        }

                    }
                    else  //segunda a sabado
                    {

                        if ( rData.DayOfWeek != DayOfWeek.Sunday)
                        {
                            if (rData.Hour >= 8 && rData.Hour < zHora_Aux && rData.Hour != 12)
                            {
                                zTotal++;
                            }
                            rData = rData.AddHours(1);
                        }
                        else
                        {
                            rData = rData.AddDays(1);
                        }
                    }
                }
            }


            return zTotal;
        }







        private Int32 Calcular_Horas_Afastadas_Total(DateTime zData1, DateTime zData2, Int32 zIdEmpregado)
        {
            Int32 zTotal = 0;
            float zHoras_Mes = 0;


            if (zData1 > zData2) return 0;

            Empregado zEmpregado = new Empregado(zIdEmpregado);

            EmpregadoFuncao zEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(zEmpregado);


            //ver se colaborador tem jornada de trabalho definida  campo tbltempo_exp->svl_hor_sem  ( * 5 temos o valor mensal )
            zEmpregadoFuncao.nID_TEMPO_EXP.Find();

            if (zEmpregadoFuncao.nID_TEMPO_EXP.sVL_HOR_SEM == 0)
            {
                //pegar horas mensais do cliente
                zEmpregadoFuncao.nID_EMPR.Find();

                if ( zEmpregadoFuncao.nID_EMPR.Carga_Horaria == 0 ) // || zEmpregadoFuncao.nID_EMPR.Carga_Horaria == null )
                    zHoras_Mes = 220;
                else
                   zHoras_Mes = zEmpregadoFuncao.nID_EMPR.Carga_Horaria;

            }
            else
            {

                zHoras_Mes = zEmpregadoFuncao.nID_TEMPO_EXP.sVL_HOR_SEM * 5;

            }


            //calcular quantos meses existem entre data1 e data2   1 dia = 0,033  1 mês = 1

            zTotal = 0;

            for (DateTime rData = zData1; rData <= zData2; )
            {
                 zTotal++;
                 rData = rData.AddDays(1);
            }


            zTotal = System.Convert.ToInt32( zHoras_Mes * (zTotal / 30.5) );
            

            return zTotal;
        }


        private Int32 Calcular_Dias_Uteis_Afastados(DateTime zData1, DateTime zData2)
        {
            Int32 zTotal = 0;


            if (zData1 > zData2) return 0;


            if (zData2.Hour == 0 && zData2.Minute == 0)
            {
                for (DateTime rData = zData1; rData < zData2; )
                {

                    if (rData.DayOfWeek != DayOfWeek.Saturday && rData.DayOfWeek != DayOfWeek.Sunday)
                    {
                        zTotal++;
                        rData = rData.AddDays(1);
                    }
                    else
                    {
                        rData = rData.AddDays(1);
                    }

                }
            }
            else
            {
                for (DateTime rData = zData1; rData <= zData2; )
                {

                    if (rData.DayOfWeek != DayOfWeek.Saturday && rData.DayOfWeek != DayOfWeek.Sunday)
                    {
                        zTotal++;
                        rData = rData.AddDays(1);
                    }
                    else
                    {
                        rData = rData.AddDays(1);
                    }

                }
            }
            return zTotal;
        }


        private Int32 Calcular_Dias_Nao_Uteis_Afastados(DateTime zData1, DateTime zData2)
        {
            Int32 zTotal = 0;


            if (zData1 > zData2) return 0;


            if (zData2.Hour == 0 && zData2.Minute == 0)
            {
                for (DateTime rData = zData1; rData < zData2; )
                {

                    if (rData.DayOfWeek == DayOfWeek.Saturday || rData.DayOfWeek == DayOfWeek.Sunday)
                    {
                        zTotal++;
                        rData = rData.AddDays(1);
                    }
                    else
                    {
                        rData = rData.AddDays(1);
                    }

                }
            }
            else
            {
                for (DateTime rData = zData1; rData <= zData2; )
                {

                    if (rData.DayOfWeek == DayOfWeek.Saturday || rData.DayOfWeek == DayOfWeek.Sunday)
                    {
                        zTotal++;
                        rData = rData.AddDays(1);
                    }
                    else
                    {
                        rData = rData.AddDays(1);
                    }

                }
            }
            return zTotal;
        }

        #endregion
    }
}
