using System;
using System.Text;
using System.Data;
using System.Collections;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Opsa.Report
{

    public class DataSourceListaDeClientesTenente
    {
        public DataSourceListaDeClientesTenente()
        {

        }

        public RptListaDeClientesTenente GetReport()
        {
            RptListaDeClientesTenente report = new RptListaDeClientesTenente();
            report.SetDataSource(GetDataSource());
            report.Refresh();
            return report;
        }

        private DataSet GetDataSource()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("NOME", Type.GetType("System.String"));
            table.Columns.Add("SM", Type.GetType("System.String"));
            table.Columns.Add("PCMSO", Type.GetType("System.String"));
            table.Columns.Add("INICIO", Type.GetType("System.String"));
            table.Columns.Add("EMPREGADOS", Type.GetType("System.String"));
            table.Columns.Add("CONTATO", Type.GetType("System.String"));
            table.Columns.Add("TELEFONE", Type.GetType("System.String"));
            table.Columns.Add("FAX", Type.GetType("System.String"));
            table.Columns.Add("CIPA", Type.GetType("System.String"));
            table.Columns.Add("RECARGA", Type.GetType("System.String"));
            ds.Tables.Add(table);

            ArrayList list = new Cliente().Find("IdCliente IN (SELECT IdCliente FROM qryClienteAtivos) ORDER BY NomeAbreviado");
            
            DataRow newRow;
            
            foreach (Cliente cliente in list)
            {
                ContatoTelefonico contatoTelefonico = cliente.GetContatoTelefonico();
                newRow = ds.Tables[0].NewRow();
                newRow["NOME"] = cliente.NomeAbreviado;
                newRow["SM"] = cliente.GetSalarioMinimo();
                newRow["PCMSO"] = cliente.GetValorPorEmpregadoPCMSO().ToString("n");
                newRow["INICIO"] = cliente.DataCadastro.ToString("yyyy");
                newRow["EMPREGADOS"] = cliente.QtdEmpregados.ToString();
                newRow["CONTATO"] = contatoTelefonico.Nome;
                newRow["TELEFONE"] = contatoTelefonico.GetDDDTelefone();
                newRow["FAX"] = cliente.GetFax().GetDDDTelefone();
                newRow["CIPA"] = cliente.ContrataCipa == (int)TipoCipa.Contratada ? "SIM" : "";
                newRow["RECARGA"] = cliente.RecargaExtintor ? "SIM" : "";
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
    }
}
