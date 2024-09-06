using System;
using System.Data;
using System.Text;
using System.Collections;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using Ilitera.Opsa.Report;
using Ilitera.Data.SQLServer;
using Ilitera.Data;

namespace Ilitera.Opsa.Report
{
    public class DataSourceAbsenteismos : DataSourceBase
    {

        public DataSourceAbsenteismos()
        {

        }



        public rptAbsenteismo_Analitico GetReport_Bloqueio(int xId, string xD_Inicial, string xD_Final, string xCliente, Int16 xEmp, string xConsiderar, string xStatus, string xAtestados, bool zExibir_CID)
        {
            rptAbsenteismo_Analitico report = new rptAbsenteismo_Analitico();
            report.SetDataSource(GetGuiasAbsenteismo(xId, xD_Inicial, xD_Final, xCliente, xEmp, "A", xConsiderar, xStatus, xAtestados, zExibir_CID));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;

        }

        public rptAbsenteismo_Analitico_Empr GetReport_Bloqueio_Empr(int xId, string xD_Inicial, string xD_Final, string xCliente, Int16 xEmp, string xConsiderar, string xStatus, string xAtestados)
        {
            rptAbsenteismo_Analitico_Empr report = new rptAbsenteismo_Analitico_Empr();
            report.SetDataSource(GetGuiasAbsenteismo_Empr(xId, xD_Inicial, xD_Final, xCliente, xEmp, "A", xConsiderar, xStatus, xAtestados));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;

        }


        public rptAbsenteismo_Sumarizado GetReport_Bloqueio_Sum(int xId, string xD_Inicial, string xD_Final, string xCliente, Int16 xEmp, string xConsiderar, string xStatus, string xAtestados)
        {
            rptAbsenteismo_Sumarizado report = new rptAbsenteismo_Sumarizado();
            report.SetDataSource(GetGuiasAbsenteismo_Sum(xId, xD_Inicial, xD_Final, xCliente, xEmp, "S", xConsiderar, xStatus, xAtestados));
            report.Refresh();
            //SetTempoProcessamento(report);
            return report;

        }


  
        public DataSet GetGuiasAbsenteismo(int zId, string zD_Inicial, string zD_Final, string zCliente, Int16 zEmp, string xTipoRel, string zConsiderar, string xStatus, string xAtestados, bool xExibir_CID)
        {

            DataSet ds;
            Int32 zDias_Uteis = 0;
            Int32 zDias_Nao_Uteis = 0;
            Int32 zDias_Afastados = 0;
            Int32 zHoras_Afastadas = 0;
            Int32 zHoras_Afastadas_Oc = 0;
            Int32 zHoras_Afastadas_NOc = 0;
            DateTime zData1;
            DateTime zData2;


            Clientes_Funcionarios xGuias = new Clientes_Funcionarios();

            ds = xGuias.Gerar_DS_Relatorio_Absenteismo(zId, zD_Inicial, zD_Final, xTipoRel, zConsiderar, zEmp, xStatus, xAtestados);

            if (xTipoRel == "A")
            {
                Int16 xReg = (Int16)ds.Tables[0].Rows.Count;
                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                for (Int16 zAux = 0; zAux < ds.Tables[0].Rows.Count; zAux++)
                {                              
                    //calcular dias uteis de afastamento
                    zDias_Uteis = 0;
                    zDias_Nao_Uteis = 0;
                    zHoras_Afastadas = 0;
                    zHoras_Afastadas_Oc = 0;
                    zHoras_Afastadas_NOc = 0;
                    zDias_Afastados = 0;

                    if ( xExibir_CID == false )
                    {
                        ds.Tables[0].Rows[zAux]["CID1"] = "";
                        ds.Tables[0].Rows[zAux]["CID2"] = "";
                        ds.Tables[0].Rows[zAux]["CID3"] = "";
                        ds.Tables[0].Rows[zAux]["CID4"] = "";
                    }

                    
                    zData1 =  System.Convert.ToDateTime(ds.Tables[0].Rows[zAux]["Afastamento"].ToString().Substring(13).Trim(), ptBr);

                    if (ds.Tables[0].Rows[zAux]["Retorno"].ToString().Trim() == "")
                    {
                        zData2 = System.DateTime.Now;

                        if (zData2 > System.Convert.ToDateTime(zD_Final, ptBr)) zData2 = System.Convert.ToDateTime(zD_Final, ptBr);
                    }
                    else
                    {
                        zData2 = System.Convert.ToDateTime(ds.Tables[0].Rows[zAux]["Retorno"].ToString().Substring(9).Trim(), ptBr);

                        if (zData2 > System.Convert.ToDateTime(zD_Final, ptBr)) zData2 = System.Convert.ToDateTime(zD_Final, ptBr);
                    }

                    if (ds.Tables[0].Rows[zAux]["Tipo_Afastamento"].ToString().Trim() == "Ocupacional")
                    {
                        if (zData2.ToString("dd/MM/yyyy") != "01/01/1900")
                        {
                            zHoras_Afastadas_Oc = zHoras_Afastadas_Oc + Calcular_Horas_Afastadas_Jornada(zData1, zData2, System.Convert.ToInt32(ds.Tables[0].Rows[zAux]["nId_Empregado"].ToString()));
                        }
                    }
                    else 
                    {
                        if (zData2.ToString("dd/MM/yyyy") != "01/01/1900")
                        {
                            zHoras_Afastadas_NOc = zHoras_Afastadas_NOc + Calcular_Horas_Afastadas_Jornada(zData1, zData2, System.Convert.ToInt32(ds.Tables[0].Rows[zAux]["nId_Empregado"].ToString()));
                        }
                    }
                                      
                    if (zData2.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        zHoras_Afastadas = zHoras_Afastadas_NOc + zHoras_Afastadas_Oc;
                      
                        if (zHoras_Afastadas > 8)
                        {
                            zDias_Uteis = zDias_Uteis + (Calcular_Dias_Uteis_Afastados(System.Convert.ToDateTime(zData1), System.Convert.ToDateTime(zData2))); //- 1);                            
                            zDias_Nao_Uteis = zDias_Nao_Uteis + Calcular_Dias_Nao_Uteis_Afastados(System.Convert.ToDateTime(zData1), System.Convert.ToDateTime(zData2));
                        }
                        else
                        {
                            zDias_Uteis = zDias_Uteis + 1;
                            //zDias_Nao_Uteis = zDias_Nao_Uteis + 0;
                        }

                        zDias_Afastados = zDias_Nao_Uteis + zDias_Uteis;

                    }

                    ds.Tables[0].Rows[zAux]["Exame"] = "Dias Afastado: " + zDias_Afastados.ToString().Trim();
                    ds.Tables[0].Rows[zAux]["Registros"] = xReg;
                }

            }
            
            return ds;

        }



        public DataSet GetGuiasAbsenteismo_Acidente(int zId, string zD_Inicial, string zD_Final, string zCliente, Int16 zEmp, string xTipoRel, string zConsiderar, string xStatus, string xAtestados, bool xExibir_CID)
        {

            DataSet ds;
            Int32 zDias_Uteis = 0;
            Int32 zDias_Nao_Uteis = 0;
            Int32 zDias_Afastados = 0;
            Int32 zHoras_Afastadas = 0;
            Int32 zHoras_Afastadas_Oc = 0;
            Int32 zHoras_Afastadas_NOc = 0;
            DateTime zData1;
            DateTime zData2;


            Clientes_Funcionarios xGuias = new Clientes_Funcionarios();

            ds = xGuias.Gerar_DS_Relatorio_Absenteismo_Acidente(zId, zD_Inicial, zD_Final, xTipoRel, zConsiderar, zEmp, xStatus, xAtestados);

            if (xTipoRel == "A")
            {
                Int16 xReg = (Int16)ds.Tables[0].Rows.Count;
                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                for (Int16 zAux = 0; zAux < ds.Tables[0].Rows.Count; zAux++)
                {
                    //calcular dias uteis de afastamento
                    zDias_Uteis = 0;
                    zDias_Nao_Uteis = 0;
                    zHoras_Afastadas = 0;
                    zHoras_Afastadas_Oc = 0;
                    zHoras_Afastadas_NOc = 0;
                    zDias_Afastados = 0;

                    if (xExibir_CID == false)
                    {
                        ds.Tables[0].Rows[zAux]["CID1"] = "";
                        ds.Tables[0].Rows[zAux]["CID2"] = "";
                        ds.Tables[0].Rows[zAux]["CID3"] = "";
                        ds.Tables[0].Rows[zAux]["CID4"] = "";
                    }



                    zData1 = System.Convert.ToDateTime(ds.Tables[0].Rows[zAux]["Afastamento"].ToString().Trim(), ptBr);

                    if (ds.Tables[0].Rows[zAux]["Retorno"].ToString().Trim() == "")
                    {
                        zData2 = System.DateTime.Now;

                        if (zData2 > System.Convert.ToDateTime(zD_Final, ptBr)) zData2 = System.Convert.ToDateTime(zD_Final, ptBr);
                    }
                    else
                    {
                        zData2 = System.Convert.ToDateTime(ds.Tables[0].Rows[zAux]["Retorno"].ToString().Trim(), ptBr);

                        if (zData2 > System.Convert.ToDateTime(zD_Final, ptBr)) zData2 = System.Convert.ToDateTime(zD_Final, ptBr);
                    }

                    if (ds.Tables[0].Rows[zAux]["Tipo_Afastamento"].ToString().Trim() == "Ocupacional")
                    {
                        if (zData2.ToString("dd/MM/yyyy") != "01/01/1900")
                        {
                            zHoras_Afastadas_Oc = zHoras_Afastadas_Oc + Calcular_Horas_Afastadas_Jornada(zData1, zData2, System.Convert.ToInt32(ds.Tables[0].Rows[zAux]["nId_Empregado"].ToString()));
                        }
                    }
                    else
                    {
                        if (zData2.ToString("dd/MM/yyyy") != "01/01/1900")
                        {
                            zHoras_Afastadas_NOc = zHoras_Afastadas_NOc + Calcular_Horas_Afastadas_Jornada(zData1, zData2, System.Convert.ToInt32(ds.Tables[0].Rows[zAux]["nId_Empregado"].ToString()));
                        }
                    }

                    if (zData2.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        zHoras_Afastadas = zHoras_Afastadas_NOc + zHoras_Afastadas_Oc;

                        if (zHoras_Afastadas > 8)
                        {
                            zDias_Uteis = zDias_Uteis + (Calcular_Dias_Uteis_Afastados(System.Convert.ToDateTime(zData1), System.Convert.ToDateTime(zData2))); //- 1);                            
                            zDias_Nao_Uteis = zDias_Nao_Uteis + Calcular_Dias_Nao_Uteis_Afastados(System.Convert.ToDateTime(zData1), System.Convert.ToDateTime(zData2));
                        }
                        else
                        {
                            zDias_Uteis = zDias_Uteis + 1;
                            //zDias_Nao_Uteis = zDias_Nao_Uteis + 0;
                        }

                        zDias_Afastados = zDias_Nao_Uteis + zDias_Uteis;

                    }

                    ds.Tables[0].Rows[zAux]["Exame"] = zDias_Afastados.ToString().Trim();
                    ds.Tables[0].Rows[zAux]["Registros"] = xReg;
                }

            }

            return ds;

        }



        public DataSet GetGuiasAbsenteismo_Empr(int zId, string zD_Inicial, string zD_Final, string zCliente, Int16 zEmp, string xTipoRel, string zConsiderar, string xStatus, string xAtestados)
        {

            DataSet ds;
            Int32 zDias_Uteis = 0;
            Int32 zDias_Nao_Uteis = 0;
            Int32 zDias_Afastados = 0;
            Int32 zHoras_Afastadas = 0;
            Int32 zHoras_Afastadas_Oc = 0;
            Int32 zHoras_Afastadas_NOc = 0;
            Int32 zTotal_Periodo = 0;
            DateTime zData1;
            DateTime zData2;
            int zAfastamentos = 0;


            string zEmpregado_Old = "";


            Clientes_Funcionarios xGuias = new Clientes_Funcionarios();


            //criar novo dataset com dados agrupados por empregado, com os campos adicionais e a % de absenteismo
            DataSet zDs = new DataSet();
            DataTable table = GetTableAbsenteismo();
            zDs.Tables.Add(table);
            DataRow newRow;



            //precisa fazer join com tblempregado_Funcao e tblTempo_Exp ( Jornada ) - ver campo tHora ( ou sVL_HOR_NUM ) ( Hora Diária ), ver se "Sab" está em tHora_Extenso
            ds = xGuias.Gerar_DS_Relatorio_Absenteismo(zId, zD_Inicial, zD_Final, xTipoRel, zConsiderar, zEmp, xStatus, xAtestados);

            // if (xTipoRel == "E")
            //{
            Int16 xReg = (Int16)ds.Tables[0].Rows.Count;
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            newRow = zDs.Tables["Result"].NewRow();



            //carga primeiro registro
            if (xReg > 0)
            {
                zEmpregado_Old = ds.Tables[0].Rows[0]["Empregado"].ToString();

                zTotal_Periodo = Calcular_Horas_Afastadas_Total(System.Convert.ToDateTime(zD_Inicial, ptBr), System.Convert.ToDateTime(zD_Final, ptBr), System.Convert.ToInt32(ds.Tables[0].Rows[0]["nId_Empregado"].ToString()));

                Empregado zEmpregado = new Empregado(System.Convert.ToInt32(ds.Tables[0].Rows[0]["nId_Empregado"].ToString()));
                EmpregadoFuncao zEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(zEmpregado);
                zEmpregadoFuncao.nID_TEMPO_EXP.Find();

                if (zEmpregadoFuncao.nID_TEMPO_EXP.sVL_HOR_SEM == 0)
                {
                    zEmpregado.nID_EMPR.Find();

                    if (zEmpregado.nID_EMPR.Carga_Horaria == 0)
                        newRow["Retorno"] = "220 horas/mês ( padrão )";
                    else
                        newRow["Retorno"] = zEmpregado.nID_EMPR.Carga_Horaria.ToString().Trim() + " horas/mês";

                }
                else
                {
                    newRow["Retorno"] = zEmpregadoFuncao.nID_TEMPO_EXP.tHORA_EXTENSO_SEMANAL;
                }

            }


            for (Int16 zAux = 0; zAux < ds.Tables[0].Rows.Count; zAux++)
            {


                if (zEmpregado_Old != ds.Tables[0].Rows[zAux]["Empregado"].ToString())
                {

                    newRow["Afastamento"] = zAfastamentos.ToString().Trim();


                    newRow["Tipo_Afastamento"] = zDias_Afastados.ToString().Trim() + " dias";
                    newRow["Cid2"] = zHoras_Afastadas.ToString().Trim() + " hs.";


                    newRow["Cid1"] = zTotal_Periodo.ToString().Trim() + " hs.";

                    newRow["Registros"] = ((zHoras_Afastadas * 1.0) / (zTotal_Periodo * 1.0) * 100).ToString("#,###.00").Trim() + " %";


                    zDs.Tables["Result"].Rows.Add(newRow);

                    zEmpregado_Old = ds.Tables[0].Rows[zAux]["Empregado"].ToString();

                    newRow = zDs.Tables["Result"].NewRow();


                    zTotal_Periodo = Calcular_Horas_Afastadas_Total(System.Convert.ToDateTime(zD_Inicial, ptBr), System.Convert.ToDateTime(zD_Final, ptBr), System.Convert.ToInt32(ds.Tables[0].Rows[zAux]["nId_Empregado"].ToString()));

                    Empregado zEmpregado = new Empregado(System.Convert.ToInt32(ds.Tables[0].Rows[zAux]["nId_Empregado"].ToString()));
                    EmpregadoFuncao zEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(zEmpregado);
                    zEmpregadoFuncao.nID_TEMPO_EXP.Find();

                    if (zEmpregadoFuncao.nID_TEMPO_EXP.sVL_HOR_SEM == 0)
                    {
                        zEmpregado.nID_EMPR.Find();

                        if (zEmpregado.nID_EMPR.Carga_Horaria == 0)
                            newRow["Retorno"] = "220 horas/mês ( padrão )";
                        else
                            newRow["Retorno"] = zEmpregado.nID_EMPR.Carga_Horaria.ToString().Trim() + " horas/mês";
                    }
                    else
                    {
                        newRow["Retorno"] = zEmpregadoFuncao.nID_TEMPO_EXP.tHORA_EXTENSO_SEMANAL;
                    }

                    zDias_Uteis = 0;
                    zDias_Nao_Uteis = 0;
                    zHoras_Afastadas = 0;
                    zHoras_Afastadas_Oc = 0;
                    zHoras_Afastadas_NOc = 0;
                    zDias_Afastados = 0;
                    zAfastamentos = 0;


                }




                newRow["CNPJ"] = ds.Tables[0].Rows[zAux]["CNPJ"].ToString();
                newRow["Empregado"] = ds.Tables[0].Rows[zAux]["Empregado"].ToString();
                newRow["Nascimento"] = ds.Tables[0].Rows[zAux]["Nascimento"].ToString();
                newRow["Admissao"] = ds.Tables[0].Rows[zAux]["Admissao"].ToString();
                newRow["Setor"] = ds.Tables[0].Rows[zAux]["Setor"].ToString();
                newRow["Funcao"] = ds.Tables[0].Rows[zAux]["Funcao"].ToString();
                newRow["Data1"] = ds.Tables[0].Rows[zAux]["Data1"].ToString();
                newRow["Data2"] = ds.Tables[0].Rows[zAux]["Data2"].ToString();

                zAfastamentos++;

                zData1 = System.Convert.ToDateTime(ds.Tables[0].Rows[zAux]["Afastamento"].ToString().Substring(13).Trim(), ptBr);

                if (ds.Tables[0].Rows[zAux]["Retorno"].ToString().Trim() == "")
                {
                    zData2 = System.DateTime.Now;
                }
                else
                {
                    zData2 = System.Convert.ToDateTime(ds.Tables[0].Rows[zAux]["Retorno"].ToString().Substring(9).Trim(), ptBr);

                    if (zData2 > System.Convert.ToDateTime(zD_Final, ptBr)) zData2 = System.Convert.ToDateTime(zD_Final, ptBr);
                }

                if (ds.Tables[0].Rows[zAux]["Tipo_Afastamento"].ToString().Trim() == "Ocupacional")
                {
                    if (zData2.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        //zHoras_Afastadas_Oc = zHoras_Afastadas_Oc + Calcular_Horas_Afastadas(zData1, zData2, System.Convert.ToDouble(ds.Tables[0].Rows[zAux]["Horas_Diarias"].ToString()), ds.Tables[0].Rows[zAux]["Trab_Sabado"].ToString().Trim());
                        zHoras_Afastadas_Oc = Calcular_Horas_Afastadas_Jornada(zData1, zData2, System.Convert.ToInt32(ds.Tables[0].Rows[zAux]["nId_Empregado"].ToString()));
                    }
                }
                else
                {
                    if (zData2.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        zHoras_Afastadas_NOc = Calcular_Horas_Afastadas_Jornada(zData1, zData2, System.Convert.ToInt32(ds.Tables[0].Rows[zAux]["nId_Empregado"].ToString()));
                    }
                }

                if (zData2.ToString("dd/MM/yyyy") != "01/01/1900")
                {
                    zHoras_Afastadas = zHoras_Afastadas + zHoras_Afastadas_NOc + zHoras_Afastadas_Oc;

                    if (zHoras_Afastadas > 8)
                    {
                        zDias_Uteis = zDias_Uteis + (Calcular_Dias_Uteis_Afastados(System.Convert.ToDateTime(zData1), System.Convert.ToDateTime(zData2))); //- 1);                            
                        zDias_Nao_Uteis = zDias_Nao_Uteis + Calcular_Dias_Nao_Uteis_Afastados(System.Convert.ToDateTime(zData1), System.Convert.ToDateTime(zData2));
                    }
                    else
                    {
                        zDias_Uteis = zDias_Uteis+ 1;
                        //zDias_Nao_Uteis = zDias_Nao_Uteis +   0;
                    }

                    //zDias_Afastados = zDias_Afastados + zDias_Nao_Uteis + zDias_Uteis;

                }

                zDias_Afastados = zDias_Nao_Uteis + zDias_Uteis;

                // Wagner
                // absenteismos no mesmo dia aparecem com horas zeradas, e dia =1, checar
                //ds.Tables[0].Rows[zAux]["Exame"] = "Dias Afastado: " + zDias_Afastados.ToString().Trim() + "  Horas Afastado: " + zHoras_Afastadas.ToString().Trim();
                //ds.Tables[0].Rows[zAux]["Registros"] = xReg;


            }

            //}

            return zDs;

        }



        private static DataTable GetTableAbsenteismo()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Empregado", Type.GetType("System.String"));
            table.Columns.Add("Data1", Type.GetType("System.String"));
            table.Columns.Add("Data2", Type.GetType("System.String"));
            table.Columns.Add("Nascimento", Type.GetType("System.String"));
            table.Columns.Add("Admissao", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Retorno", Type.GetType("System.String"));
            table.Columns.Add("Cid1", Type.GetType("System.String"));
            table.Columns.Add("Cid2", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Afastamento", Type.GetType("System.String"));
            table.Columns.Add("Registros", Type.GetType("System.String"));
            return table;
        }





        public DataSet GetGuiasAbsenteismo_Sum(int zId, string zD_Inicial, string zD_Final, string zCliente, Int16 zEmp, string xTipoRel, string zConsiderar, string xStatus, string xAtestados)
        {

            DataSet ds;
            Int32 zDias_Uteis = 0;
            Int32 zDias_Nao_Uteis = 0;
            Int32 zDias_Afastados = 0;
            Int32 zHoras_Afastadas = 0;
            Int32 zHoras_Afastadas_Oc = 0;
            Int32 zHoras_Afastadas_NOc = 0;
            DateTime zData1;
            DateTime zData2;
            Int32 zRegs = 0;

            Clientes_Funcionarios xGuias = new Clientes_Funcionarios();
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            ds = xGuias.Gerar_DS_Relatorio_Absenteismo(zId, zD_Inicial, zD_Final, xTipoRel, zConsiderar, zEmp, xStatus, xAtestados);

            zDias_Uteis = 0;
            zDias_Nao_Uteis = 0;


            for (Int16 zAux = 0; zAux < ds.Tables[0].Rows.Count; zAux++)
            {
                //calcular dias uteis de afastamento
                //zDias_Uteis = 0;
                //zDias_Nao_Uteis = 0;
                zHoras_Afastadas = 0;
                zHoras_Afastadas_Oc = 0;
                zHoras_Afastadas_NOc = 0;

                zData1 = System.Convert.ToDateTime(ds.Tables[0].Rows[zAux]["Afastamento"].ToString().Substring(13).Trim(), ptBr);

                if (ds.Tables[0].Rows[zAux]["Retorno"].ToString().Trim() == "")
                {
                    zData2 = System.DateTime.Now;
                }
                else
                {
                    zData2 = System.Convert.ToDateTime(ds.Tables[0].Rows[zAux]["Retorno"].ToString().Substring(9).Trim(), ptBr);
                }

                if (ds.Tables[0].Rows[zAux]["Tipo_Afastamento"].ToString().Trim() == "Ocupacional")
                {
                    if (zData2.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        zHoras_Afastadas_Oc = zHoras_Afastadas_Oc + Calcular_Horas_Afastadas_Jornada(zData1, zData2, System.Convert.ToInt32(ds.Tables[0].Rows[zAux]["nId_Empregado"].ToString()));
                    }
                }
                else
                {
                    if (zData2.ToString("dd/MM/yyyy") != "01/01/1900")
                    {
                        zHoras_Afastadas_NOc = zHoras_Afastadas_NOc + Calcular_Horas_Afastadas_Jornada(zData1, zData2, System.Convert.ToInt32(ds.Tables[0].Rows[zAux]["nId_Empregado"].ToString()));
                    }
                }

                if (zData2.ToString("dd/MM/yyyy") != "01/01/1900")
                {
                    zHoras_Afastadas = zHoras_Afastadas_NOc + zHoras_Afastadas_Oc;

                    if (zHoras_Afastadas > 8)
                    {
                        zDias_Uteis = zDias_Uteis + (Calcular_Dias_Uteis_Afastados(System.Convert.ToDateTime(zData1), System.Convert.ToDateTime(zData2))); //- 1);                            
                        zDias_Nao_Uteis = zDias_Nao_Uteis + Calcular_Dias_Nao_Uteis_Afastados(System.Convert.ToDateTime(zData1), System.Convert.ToDateTime(zData2));
                    }
                    else
                    {
                        zDias_Uteis = zDias_Uteis + 1;
                        //zDias_Nao_Uteis =  0;
                    }

                    //zDias_Afastados = zDias_Afastados + ( zDias_Nao_Uteis + zDias_Uteis );

                }

                zDias_Afastados = zDias_Nao_Uteis + zDias_Uteis;


            }



            ds = xGuias.Gerar_DS_Relatorio_Absenteismo_Sumarizado(zId, zD_Inicial, zD_Final, xTipoRel, zConsiderar, zEmp, xStatus);

            for (Int16 zAux = 0; zAux < ds.Tables[0].Rows.Count; zAux++)
            {
                zRegs = zRegs + System.Convert.ToInt32(ds.Tables[0].Rows[zAux]["Registros"].ToString());
            }

            for (Int16 zAux = 0; zAux < ds.Tables[0].Rows.Count; zAux++)
            {                
                ds.Tables[0].Rows[zAux]["Exame"] = "Total de Ocorrências: " + zRegs.ToString().Trim() + "           Total de Dias Afastado: " + zDias_Afastados.ToString().Trim();
            }

            return ds;

        }




        private Int32 Calcular_Horas_Afastadas(DateTime zData1, DateTime zData2)
        {
            Int32 zTotal = 0;


            if (zData1 > zData2) return 0;

            //descobrir jornada diária de trabalho,  qtde horas diária, e se trabalha de sábado 
            //se não tiver definido, usar regra abaixo    


            if (zData1.Day == zData2.Day && zData1.Month == zData2.Month && zData1.Year == zData2.Year)
            {
                for (DateTime rData = zData1; rData < zData2; )
                {
                    if (rData.Hour >= 8 && rData.Hour < 17)
                    {
                        zTotal++;
                    }
                    rData = rData.AddHours(1);
                }

                if (zData1.Hour <= 12 && zData2.Hour >= 12 && zTotal > 0)
                {
                    if (zData2.Hour == 12 && zData2.Minute > 0) zTotal--;
                    else if (zData2.Hour != 12) zTotal--;
                }

            }
            else
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

            return zTotal;
        }




        private Int32 Calcular_Horas_Afastadas(DateTime zData1, DateTime zData2, double Horas_Diarias, string Trab_Sabado)
        {
            Int32 zTotal = 0;
            Int32 zHoras_Dia = 0;
            Int32 zDia = 0;


            if (zData1 > zData2) return 0;

            //descobrir jornada diária de trabalho,  qtde horas diária, e se trabalha de sábado 
            //se não tiver definido, usar regra abaixo    


            //if (zData1.Day == zData2.Day && zData1.Month == zData2.Month && zData1.Year == zData2.Year)
            //{
            //    for (DateTime rData = zData1; rData < zData2; )
            //    {
            //        if (rData.Hour >= 8 && rData.Hour < 17)
            //        {
            //            zTotal++;
            //        }
            //        rData = rData.AddHours(1);
            //    }

            //    if (zData1.Hour <= 12 && zData2.Hour >= 12 && zTotal > 0)
            //    {
            //        if (zData2.Hour == 12 && zData2.Minute > 0) zTotal--;
            //        else if (zData2.Hour != 12) zTotal--;
            //    }

            //}
            //else
            //{
            //    for (DateTime rData = zData1; rData < zData2; )
            //    {

            //        if (rData.DayOfWeek != DayOfWeek.Saturday && rData.DayOfWeek != DayOfWeek.Sunday)
            //        {
            //            if (rData.Hour >= 8 && rData.Hour < 17 && rData.Hour != 12)
            //            {
            //                zTotal++;
            //            }
            //            rData = rData.AddHours(1);
            //        }
            //        else
            //        {
            //            rData = rData.AddDays(1);
            //        }

            //    }
            //}


            if (zData1.Day == zData2.Day && zData1.Month == zData2.Month && zData1.Year == zData2.Year)
            {
                for (DateTime rData = zData1; rData < zData2; )
                {
                    //if (rData.Hour >= 8 && rData.Hour < 17)
                    //{
                    zTotal++;
                    //}
                    rData = rData.AddHours(1);
                }

                //if (zData1.Hour <= 12 && zData2.Hour >= 12 && zTotal > 0)
                //{
                //    if (zData2.Hour == 12 && zData2.Minute > 0) zTotal--;
                //    else if (zData2.Hour != 12) zTotal--;
                //}
                if (zTotal > Horas_Diarias) zTotal = (Int16)Horas_Diarias;

            }
            else
            {

                zDia = zData1.Day;

                for (DateTime rData = zData1; rData < zData2; )
                {
                    if (Trab_Sabado == "S")
                    {
                        if (rData.DayOfWeek != DayOfWeek.Sunday)
                        {
                            //if (rData.Hour >= 8 && rData.Hour < 17 && rData.Hour != 12)
                            //{
                            //    zTotal++;
                            //}
                            if (rData.Day != zDia)
                            {
                                zHoras_Dia = 0;
                                zDia = rData.Day;
                            }

                            zHoras_Dia++;

                            if (zHoras_Dia <= Horas_Diarias) zTotal++;

                            rData = rData.AddHours(1);
                        }
                        else
                        {
                            rData = rData.AddDays(1);
                            zHoras_Dia = 0;
                            zDia = rData.Day;

                        }

                    }
                    else
                    {
                        if (rData.DayOfWeek != DayOfWeek.Saturday && rData.DayOfWeek != DayOfWeek.Sunday)
                        {
                            // if (rData.Hour >= 8 && rData.Hour < 17 && rData.Hour != 12)
                            // {
                            //     zTotal++;
                            // }
                            if (rData.Day != zDia)
                            {
                                zHoras_Dia = 0;
                                zDia = rData.Day;
                            }

                            zHoras_Dia++;

                            if (zHoras_Dia <= Horas_Diarias) zTotal++;

                            rData = rData.AddHours(1);

                        }
                        else
                        {
                            rData = rData.AddDays(1);
                            zHoras_Dia = 0;
                            zDia = rData.Day;

                        }
                    }
                }
            }

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

                if (zEmpregadoFuncao.nID_EMPR.Carga_Horaria == 0 ) //|| zEmpregadoFuncao.nID_EMPR.Carga_Horaria == null)
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


            zTotal = System.Convert.ToInt32(zHoras_Mes * (zTotal / 30.5));


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

                        if (rData.DayOfWeek != DayOfWeek.Sunday)
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



    }
}