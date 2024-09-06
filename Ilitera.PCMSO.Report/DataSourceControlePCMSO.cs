using System;
using System.Data;
using System.Collections.Generic;

using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceControlePCMSO : DataSourceBase
    {
        private ClinicaExameDicionario clinicaMestra;

        public DataSourceControlePCMSO()
        {
            clinicaMestra = new ClinicaExameDicionario();
            clinicaMestra.Find("IdClinica=310"
                + " AND IdExameDicionario =" + (int)IndExameClinico.Periodico);
        }

        public RptControlePCMSO GetReport()
        {
            RptControlePCMSO report = new RptControlePCMSO();
            report.SetDataSource(GetDataSource());
            report.Refresh();
            SetTempoProcessamento(report);
            return report;
        }

        #region GetDataSource

        public DataSet GetDataSource()
        {
            DataSet ds = new DataSet();
            DataTable table = GetTable();
            ds.Tables.Add(table);

            double totalEmpregados = 0;
            double totalReceitaMensal = 0;
            double totalReceitaAnual = 0;
            double totalDespesa_Exa_AdmDem_Qtd = 0;
            double totalDespesa_Exa_AdmDem_ValorUnit = 0;
            double totalDespesa_Exa_AdmDem_ValorMensal = 0;
            double totalDespesa_Exa_AdmDem_ValorAnual = 0;
            double totalDespesa_Exa_Periodico_ValorUnit = 0;
            double totalDespesa_Exa_Periodico_ValorMensal = 0;
            double totalDespesa_Exa_Periodico_ValorAnual = 0;
            double totalDespesa_MedicaMensal = 0;
            double totalDespesa_MedicaAnual = 0;
            double totalLucroBrutoMensal = 0;
            double totalDespesaADMMensal = 0;
            double totalLucroMensalLiquido = 0;
            double totalLucroBrutoAnual = 0;
            double totalDespesaADMAnual = 0;
            double totalLucroAnualLiquido = 0;

            double despesaPCMSO = 8000.0D;

            List<Cliente> clientes = new Cliente().Find<Cliente>("ContrataPCMSO=" + (int)TipoPcmsoContratada.Contratada
                            + " AND IdJuridicaPapel = " + (int)IndJuridicaPapel.Cliente
                            + " AND IdCliente IN (SELECT IdCliente FROM qryClienteAtivos)"
                            + " ORDER BY NomeAbreviado");

            foreach (Cliente cliente in clientes)
            {
                int qtdEmpregados = cliente.QtdEmpregados;

                totalEmpregados += qtdEmpregados;
            }

            DataRow newRow;

            foreach (Cliente cliente in clientes)
            {
                newRow = ds.Tables[0].NewRow();

                int qtdEmpregados = cliente.QtdEmpregados;

                double turnover = cliente.Turnover;

                double receitaPorEmpregado = Convert.ToDouble(cliente.GetValorPorEmpregadoPCMSO());
                double receitaTotalMensal = qtdEmpregados * receitaPorEmpregado;
                double receitaTotalAnual = receitaTotalMensal * 12;

                double qdeAdmDem;

                if (turnover != 0.0D)
                {
                    qdeAdmDem = ((qtdEmpregados * turnover) / 100) * 2 * 12;
                }
                else
                {
                    qdeAdmDem = cliente.GetQuantidadeExamesAdmDemAnual();

                    if (qdeAdmDem == 0.0D)
                        qdeAdmDem = ((qtdEmpregados * 2) / 100) * 2 * 12;
                }

                //Busca a Clinica Padrão
                ClinicaCliente clinicaPadrao = new ClinicaCliente();
                clinicaPadrao.Find("IdCliente=" + cliente.Id + " AND ClinicaPadrao=1");

                double valorAdmDem = GetValorExameAdmissionalDemissional(clinicaPadrao.IdClinica);
                double valorPeriodico = GetValorExamePeriodico(clinicaPadrao.IdClinica);
                double despesa_Exa_AdmDem_ValorAnual = valorAdmDem * Convert.ToInt32(qdeAdmDem);
                double despesa_Exa_AdmDem_ValorMensal = despesa_Exa_AdmDem_ValorAnual / 12;
                double despesa_Exa_Periodico_ValorAnual = valorPeriodico * qtdEmpregados;
                double despesa_Exa_Periodico_ValorMensal = despesa_Exa_Periodico_ValorAnual / 12;
                double despesa_MedicaMensal = despesa_Exa_AdmDem_ValorMensal + despesa_Exa_Periodico_ValorMensal;
                double despesa_MedicaAnual = despesa_Exa_AdmDem_ValorAnual + despesa_Exa_Periodico_ValorAnual;
                double lucroBrutoMensal = receitaTotalMensal - despesa_MedicaMensal;
                double lucroBrutoAnual = receitaTotalAnual - despesa_MedicaAnual;
                double despesaADMMensal = (despesaPCMSO / totalEmpregados) * qtdEmpregados;
                double despesaADMAnual = ((despesaPCMSO * 12) / totalEmpregados) * qtdEmpregados;
                double lucroMensalLiquido = lucroBrutoMensal - despesaADMMensal;
                double lucroAnualLiquido = lucroBrutoAnual - despesaADMAnual;

                totalReceitaMensal += receitaTotalMensal;
                totalReceitaAnual += receitaTotalAnual;
                totalDespesa_Exa_AdmDem_Qtd += Convert.ToInt32(qdeAdmDem);
                totalDespesa_Exa_AdmDem_ValorUnit += valorAdmDem;
                totalDespesa_Exa_AdmDem_ValorMensal += despesa_Exa_AdmDem_ValorMensal;
                totalDespesa_Exa_AdmDem_ValorAnual += despesa_Exa_AdmDem_ValorAnual;
                totalDespesa_Exa_Periodico_ValorUnit += valorPeriodico;
                totalDespesa_Exa_Periodico_ValorMensal += despesa_Exa_Periodico_ValorMensal;
                totalDespesa_Exa_Periodico_ValorAnual += despesa_Exa_Periodico_ValorAnual;
                totalDespesa_MedicaMensal += despesa_MedicaMensal;
                totalDespesa_MedicaAnual += despesa_MedicaAnual;
                totalLucroBrutoMensal += lucroBrutoMensal;
                totalDespesaADMMensal += despesaADMMensal;
                totalLucroMensalLiquido += lucroMensalLiquido;
                totalLucroBrutoAnual += lucroBrutoAnual;
                totalDespesaADMAnual += despesaADMAnual;
                totalLucroAnualLiquido += lucroAnualLiquido;

                newRow["NomeEmpresa"] = cliente.NomeAbreviado;
                //Configuracao
                newRow["Configuracao_QtdEmpregados"] = qtdEmpregados;
                newRow["Configuracao_Turnover"] = turnover;
                //Receita
                newRow["Receita_ValorEmpregado"] = receitaPorEmpregado;
                newRow["Receita_TotalMensal"] = receitaTotalMensal;
                newRow["Receita_TotalAnual"] = receitaTotalAnual;
                //Despesa Exame AdmDem
                newRow["Despesa_Exa_AdmDem_Qtd"] = Convert.ToInt32(qdeAdmDem);
                newRow["Despesa_Exa_AdmDem_ValorUnit"] = valorAdmDem;
                newRow["Despesa_Exa_AdmDem_ValorMensal"] = despesa_Exa_AdmDem_ValorMensal;
                newRow["Despesa_Exa_AdmDem_ValorAnual"] = despesa_Exa_AdmDem_ValorAnual;
                //Despesa Exame Periodico
                newRow["Despesa_Exa_Periodico_Qtd"] = qtdEmpregados;
                newRow["Despesa_Exa_Periodico_ValorUnit"] = valorPeriodico;
                newRow["Despesa_Exa_Periodico_ValorMensal"] = despesa_Exa_Periodico_ValorMensal;
                newRow["Despesa_Exa_Periodico_ValorAnual"] = despesa_Exa_Periodico_ValorAnual;
                //Despesa
                newRow["Despesa_MedicaMensal"] = despesa_MedicaMensal;
                newRow["Despesa_MedicaAnual"] = despesa_MedicaAnual;
                //Lucro Mensal
                newRow["LucoBrutoMensal"] = lucroBrutoMensal;
                newRow["DespesaADMMensal"] = despesaADMMensal;
                newRow["LucroMensalLiquido"] = lucroMensalLiquido;
                //Lucro Anual
                newRow["LucoBrutoAnual"] = lucroBrutoAnual;
                newRow["DespesaADMAnual"] = despesaADMAnual;
                newRow["LucroAnualLiquido"] = lucroAnualLiquido;
                //Totais
                newRow["MediaPonderadaValorUnitario"] = Convert.ToDouble((totalReceitaMensal / totalEmpregados));
                newRow["TotalReceitaMensal"] = totalReceitaMensal;
                newRow["TotalReceitaAnual"] = totalReceitaAnual;
                newRow["TotalDespesa_Exa_AdmDem_Qtd"] = totalDespesa_Exa_AdmDem_Qtd;
                newRow["TotalDespesa_Exa_AdmDem_ValorUnit"] = totalDespesa_Exa_AdmDem_ValorUnit;
                newRow["TotalDespesa_Exa_AdmDem_ValorMensal"] = totalDespesa_Exa_AdmDem_ValorMensal;
                newRow["TotalDespesa_Exa_AdmDem_ValorAnual"] = totalDespesa_Exa_AdmDem_ValorAnual;
                newRow["TotalDespesa_Exa_Periodico_Qtd"] = totalEmpregados;
                newRow["TotalDespesa_Exa_Periodico_ValorUnit"] = totalDespesa_Exa_Periodico_ValorUnit;
                newRow["TotalDespesa_Exa_Periodico_ValorMensal"] = totalDespesa_Exa_Periodico_ValorMensal;
                newRow["TotalDespesa_Exa_Periodico_ValorAnual"] = totalDespesa_Exa_Periodico_ValorAnual;
                newRow["TotalDespesa_MedicaMensal"] = totalDespesa_MedicaMensal;
                newRow["TotalDespesa_MedicaAnual"] = totalDespesa_MedicaAnual;
                newRow["TotalLucroBrutoMensal"] = totalLucroBrutoMensal;
                newRow["TotalDespesaADMMensal"] = totalDespesaADMMensal;
                newRow["TotalLucroMensalLiquido"] = totalLucroMensalLiquido;
                newRow["TotalLucroBrutoAnual"] = totalLucroBrutoAnual;
                newRow["TotalDespesaADMAnual"] = totalDespesaADMAnual;
                newRow["TotalLucroAnualLiquido"] = totalLucroAnualLiquido;

                ds.Tables[0].Rows.Add(newRow);
            }

            return ds;
        }

        private double GetValorExamePeriodico(Clinica clinica)
        {
            double valorPeriodico = 0D;

            string criteria = "IdClinica=" + clinica.Id
                    + " AND IdExameDicionario = " + (int)IndExameClinico.Periodico;

            List<ClinicaExameDicionario>
                periodico = new ClinicaExameDicionario().Find<ClinicaExameDicionario>(criteria);

            if (periodico.Count == 0)//Busca a Mestra
            {	
                valorPeriodico = clinicaMestra.ValorPadrao;
            }
            else
            {
                foreach (ClinicaExameDicionario valorPeriodicoClinica in periodico)
                    valorPeriodico = valorPeriodicoClinica.ValorPadrao;
            }
            return valorPeriodico;
        }

        private double GetValorExameAdmissionalDemissional(Clinica clinica)
        {
            string criteria = "IdClinica=" + clinica.Id
                            + " AND IdExameDicionario =" + (int)IndExameClinico.Admissional;
            
            List<ClinicaExameDicionario>
                exameAdmissional = new ClinicaExameDicionario().Find<ClinicaExameDicionario>(criteria);

            double valorAdmDem = 0D;

            if (exameAdmissional.Count == 0)//Busca a Mestra
            {
                valorAdmDem = clinicaMestra.ValorPadrao;
            }
            else
            {
                foreach (ClinicaExameDicionario valorAdmissionalClinica in exameAdmissional)
                    valorAdmDem = valorAdmissionalClinica.ValorPadrao;
            }

            return valorAdmDem;
        }
        #endregion

        #region GetTable

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
            //Configuracao
            table.Columns.Add("Configuracao_QtdEmpregados", Type.GetType("System.Int32"));
            table.Columns.Add("Configuracao_Turnover", Type.GetType("System.Double"));
            //Receita
            table.Columns.Add("Receita_ValorEmpregado", Type.GetType("System.Double"));
            table.Columns.Add("Receita_TotalMensal", Type.GetType("System.Double"));
            table.Columns.Add("Receita_TotalAnual", Type.GetType("System.Double"));
            //Despesa Exame AdmDem
            table.Columns.Add("Despesa_Exa_AdmDem_Qtd", Type.GetType("System.Int32"));
            table.Columns.Add("Despesa_Exa_AdmDem_ValorUnit", Type.GetType("System.Double"));
            table.Columns.Add("Despesa_Exa_AdmDem_ValorMensal", Type.GetType("System.Double"));
            table.Columns.Add("Despesa_Exa_AdmDem_ValorAnual", Type.GetType("System.Double"));
            //Despesa Exame Periodico
            table.Columns.Add("Despesa_Exa_Periodico_Qtd", Type.GetType("System.Int32"));
            table.Columns.Add("Despesa_Exa_Periodico_ValorUnit", Type.GetType("System.Double"));
            table.Columns.Add("Despesa_Exa_Periodico_ValorMensal", Type.GetType("System.Double"));
            table.Columns.Add("Despesa_Exa_Periodico_ValorAnual", Type.GetType("System.Double"));
            //Despesa
            table.Columns.Add("Despesa_MedicaMensal", Type.GetType("System.Double"));
            table.Columns.Add("Despesa_MedicaAnual", Type.GetType("System.Double"));
            //Lucro Mensal
            table.Columns.Add("LucoBrutoMensal", Type.GetType("System.Double"));
            table.Columns.Add("DespesaADMMensal", Type.GetType("System.Double"));
            table.Columns.Add("LucroMensalLiquido", Type.GetType("System.Double"));
            //Lucro Anual
            table.Columns.Add("LucoBrutoAnual", Type.GetType("System.Double"));
            table.Columns.Add("DespesaADMAnual", Type.GetType("System.Double"));
            table.Columns.Add("LucroAnualLiquido", Type.GetType("System.Double"));
            //Totais
            table.Columns.Add("MediaPonderadaValorUnitario", Type.GetType("System.Double"));
            table.Columns.Add("TotalReceitaMensal", Type.GetType("System.Double"));
            table.Columns.Add("TotalReceitaAnual", Type.GetType("System.Double"));
            table.Columns.Add("TotalDespesa_Exa_AdmDem_Qtd", Type.GetType("System.Double"));
            table.Columns.Add("TotalDespesa_Exa_AdmDem_ValorUnit", Type.GetType("System.Double"));
            table.Columns.Add("TotalDespesa_Exa_AdmDem_ValorMensal", Type.GetType("System.Double"));
            table.Columns.Add("TotalDespesa_Exa_AdmDem_ValorAnual", Type.GetType("System.Double"));
            table.Columns.Add("TotalDespesa_Exa_Periodico_Qtd", Type.GetType("System.Double"));
            table.Columns.Add("TotalDespesa_Exa_Periodico_ValorUnit", Type.GetType("System.Double"));
            table.Columns.Add("TotalDespesa_Exa_Periodico_ValorMensal", Type.GetType("System.Double"));
            table.Columns.Add("TotalDespesa_Exa_Periodico_ValorAnual", Type.GetType("System.Double"));
            table.Columns.Add("TotalDespesa_MedicaMensal", Type.GetType("System.Double"));
            table.Columns.Add("TotalDespesa_MedicaAnual", Type.GetType("System.Double"));
            table.Columns.Add("TotalLucroBrutoMensal", Type.GetType("System.Double"));
            table.Columns.Add("TotalDespesaADMMensal", Type.GetType("System.Double"));
            table.Columns.Add("TotalLucroMensalLiquido", Type.GetType("System.Double"));
            table.Columns.Add("TotalLucroBrutoAnual", Type.GetType("System.Double"));
            table.Columns.Add("TotalDespesaADMAnual", Type.GetType("System.Double"));
            table.Columns.Add("TotalLucroAnualLiquido", Type.GetType("System.Double"));
            return table;
        }
        #endregion
    }
}
