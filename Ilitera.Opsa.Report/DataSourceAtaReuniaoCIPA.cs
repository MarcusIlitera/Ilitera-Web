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
    public class DataSourceAtaReuniaoCIPA : DataSourceBase
    {
        private ReuniaoCipa reuniaoCipa;

        public DataSourceAtaReuniaoCIPA(ReuniaoCipa reuniaoCipa)
        {
            this.reuniaoCipa = reuniaoCipa;

            if (new ReuniaoPresencaCipa().ExecuteCount("IdReuniaoCipa=" + this.reuniaoCipa.Id) == 0)
                throw new Exception("Primeiro Indique os Presentes na Reunião!");

            this.reuniaoCipa.IdCipa.Find();
            this.reuniaoCipa.IdCipa.IdCliente.Find();
        }

        public RptAtaReuniaoCipa GetReport()
        {
            RptAtaReuniaoCipa report = new RptAtaReuniaoCipa();
            report.SetDataSource(DataSourceRptAtaReuniaoCipa());
            report.OpenSubreport("Participantes").SetDataSource(DataSourceRptAtaReuniaoCipaParticipantes());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

        public DataSet DataSourceRptAtaReuniaoCipa()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("tDadosEmpresa", Type.GetType("System.String"));
            table.Columns.Add("tTituloDoRelatorio", Type.GetType("System.String"));
            table.Columns.Add("dDT_REUNIAO", Type.GetType("System.DateTime"));
            table.Columns.Add("tHORARIO", Type.GetType("System.String"));
            table.Columns.Add("tLOCAL", Type.GetType("System.String"));
            table.Columns.Add("tTEXTO", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);

            DataRow newRow = ds.Tables[0].NewRow();

            newRow["tDadosEmpresa"] = reuniaoCipa.IdCliente.GetDadosEmpresaCipa(reuniaoCipa.DataSolicitacao);
            newRow["tTituloDoRelatorio"] = reuniaoCipa.GetTituloReuniao();
            newRow["dDT_REUNIAO"] = reuniaoCipa.DataSolicitacao;
            newRow["tHORARIO"] = reuniaoCipa.GetHorario();
            newRow["tLOCAL"] = reuniaoCipa.Local;
            newRow["tTEXTO"] = reuniaoCipa.GetTextoAtaHtml();

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        public DataSet DataSourceRptAtaReuniaoCipaParticipantes()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("tINDICADO", Type.GetType("System.String"));
            table.Columns.Add("tCARGO", Type.GetType("System.String"));
            table.Columns.Add("nNUMERO", Type.GetType("System.Int32"));
            table.Columns.Add("tNOME", Type.GetType("System.String"));
            table.Columns.Add("tTEXTO", Type.GetType("System.String"));
            ds.Tables.Add(table);

            DataRow newRow;

            ArrayList list = new ReuniaoPresencaCipa().Find("IdReuniaoCipa=" + reuniaoCipa.Id);

            foreach (ReuniaoPresencaCipa membroPresente in list)
            {
                ArrayList listMembroCipa = new MembroCipa().Find("IdMembroCipa=" + membroPresente.IdMembroCipa.Id
                                                                  + " AND IdCipa = " + reuniaoCipa.IdCipa.Id);

                foreach (MembroCipa membroCipa in listMembroCipa)
                {
                    newRow = ds.Tables[0].NewRow();

                    newRow["tINDICADO"] = membroCipa.GetGrupoMembroParaReuniao();
                    newRow["nNUMERO"] = membroCipa.Numero;
                    newRow["tCARGO"] = membroCipa.GetNomeCargo();

                    if (membroPresente.IndPresenca == ReuniaoPresencaCipa.Presenca.Presente)
                        newRow["tNOME"] = membroPresente.ToString();
                    else
                        newRow["tNOME"] = membroPresente.ToString() + " (" + ReuniaoPresencaCipa.GetPresenca((int)membroPresente.IndPresenca) + ")";

                    ds.Tables[0].Rows.Add(newRow);
                }
            }
            return ds;
        }
    }
}
