using System;
using System.Collections;
using System.Data;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Opsa.Report
{
    public class DataSourceListaDeClientes
    {
        public DataSourceListaDeClientes()
        {

        }

        public RptListaDeClientes GetReport(DataSet dsEmpresas)
        {
            RptListaDeClientes report = new RptListaDeClientes();
            report.SetDataSource(GetListaEmpresa(dsEmpresas));
            report.Refresh();
            return report;
        }

        private DataSet GetListaEmpresa(DataSet dsEmpresas)
        {
            Juridica juridica;
            DataSet ds = new DataSet();
            DataRow newRow;

            DataTable table = new DataTable("Result");
            table.Columns.Add("Titulo", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Contato", Type.GetType("System.String"));
            table.Columns.Add("Telefone", Type.GetType("System.String"));
            table.Columns.Add("Cep", Type.GetType("System.String"));
            table.Columns.Add("Motorista", Type.GetType("System.String"));
            ds.Tables.Add(table);

            foreach (DataRow row in dsEmpresas.Tables[0].Rows)
            {
                juridica = new Juridica();
                juridica.Find(Convert.ToInt32(row["Id"]));

                ArrayList ContatosTelefonicos = new ContatoTelefonico().Find("IdPessoa=" + juridica.Id 
                               + " AND ((Nome<>'' AND Nome IS NOT NULL) OR (Numero<>'' AND Numero IS NOT NULL))");

                if (ContatosTelefonicos.Count == 0)
                {
                    newRow = ds.Tables[0].NewRow();
                    newRow["Titulo"] = "Contatos Telefônicos";
                    newRow["Empresa"] = juridica.NomeAbreviado;
                    ds.Tables[0].Rows.Add(newRow);
                }
                else
                {
                    foreach (ContatoTelefonico contatoTelefonico in ContatosTelefonicos)
                    {
                        newRow = ds.Tables[0].NewRow();
                        newRow["Titulo"] = "Contatos Telefônicos";
                        newRow["Empresa"] = juridica.NomeAbreviado;
                        newRow["Contato"] = contatoTelefonico.Nome;
                        newRow["Telefone"] = contatoTelefonico.GetDDDTelefone();
                        ds.Tables[0].Rows.Add(newRow);
                    }
                }
            }
            return ds;
        }
    }
}
