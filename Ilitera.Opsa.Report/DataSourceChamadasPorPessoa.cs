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
    public class DataSourceChamadasPorPessoa
    {
        private Pessoa pessoa;
        private DateTime de;
        private DateTime ate;

        public DataSourceChamadasPorPessoa(Pessoa pessoa, DateTime de, DateTime ate)
        {
            this.pessoa = pessoa;
            this.de = de;
            this.ate = ate;
        }

        public RptChamadasPorPessoa GetReport()
        {
            RptChamadasPorPessoa report = new RptChamadasPorPessoa();
            report.SetDataSource(GetDataSource());
            report.Refresh();
            return report;
        }

        public DataSet GetDataSource()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTable());

            DataRow newRow;

            StringBuilder where = new StringBuilder();
            where.Append("IdPessoa=" + pessoa.Id);

            if (de != new DateTime() && ate != new DateTime())
                where.Append(" AND DataInicio BETWEEN '" + de.ToString("yyyy-MM-dd") + " 00:00:00.000'"
                    + " AND '" + ate.ToString("yyyy-MM-dd") + " 23:59:59.999'");

            ArrayList list = new Chamada().Find(where.ToString());

            foreach (Chamada chamada in list)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["Prestador"] = chamada.IdPessoa.GetNomePrestador();
                newRow["Empresa"] = chamada.IdPessoaContato.GetNomeEmpresa();
                newRow["Tipo"] = chamada.IndChamadaTipo.ToString();
                newRow["Solicitante"] = chamada.IdPessoaContato.GetNomePrestador() + "\n" + chamada.IdPessoaContato.GetTituloPrestador();
                newRow["Assunto"] = chamada.Descricao;
                newRow["Inicio"] = chamada.DataInicio;
                newRow["Termino"] = chamada.DataTermino;
                newRow["Descr_Solucao"] = chamada.Solucao;
                newRow["Solucao"] = (chamada.IndStatusChamada == (int)ChamadaStatus.Solucionado);

                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("Prestador", Type.GetType("System.String"));
            table.Columns.Add("Empresa", Type.GetType("System.String"));
            table.Columns.Add("Tipo", Type.GetType("System.String"));
            table.Columns.Add("Solicitante", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("Assunto", Type.GetType("System.String"));
            table.Columns.Add("Inicio", Type.GetType("System.DateTime"));
            table.Columns.Add("Termino", Type.GetType("System.DateTime"));
            table.Columns.Add("Solucao", Type.GetType("System.Boolean"));
            table.Columns.Add("Descr_Solucao", Type.GetType("System.String"));

            return table;
        }
    }
}
