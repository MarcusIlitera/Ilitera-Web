using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;

using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Collections;

namespace Ilitera.Opsa.Report
{
    public class DataSourceEleicaoCIPA
    {
        #region Inicializar

        private Cipa cipa;
        private EleicaoCipa eleicaoCipa;
        private Endereco endereco;

        public DataSourceEleicaoCIPA(EleicaoCipa eleicaoCipa)
        {
            this.eleicaoCipa = eleicaoCipa;
            this.cipa = eleicaoCipa.IdCipa;

            if (this.cipa.mirrorOld == null)
                this.cipa.Find();

            if (this.cipa.IdCliente.mirrorOld == null)
                this.cipa.IdCliente.Find();

            this.endereco = this.cipa.IdCliente.GetEndereco();
        }
        #endregion

        #region GetReport

        public RptEleicao_New GetReport()
        {
            RptEleicao_New report = new RptEleicao_New();

            report.SetDataSource(DataSourceAtaDeEleicao());
            report.OpenSubreport("CandidatosEleitos").SetDataSource(DataSourceCandidatosEleitos());
            report.OpenSubreport("CandidatosNaoEleitos").SetDataSource(DataSourceCandidatosNaoEleitos());
            //report.OpenSubreport("Secretarios").SetDataSource(DataSourceSecretarios());
            report.OpenSubreport("RepresentantesDoEmpregador").SetDataSource(DataSourceRepresentantesDoEmpregador());
            report.OpenSubreport("AssinaturaRepresentantes").SetDataSource(DataSourceRepresentantes());
            report.OpenSubreport("ComissaoEleitoral").SetDataSource(DataSourceComissaoEleitoral());
            report.Refresh();

            return report;
        }
        #endregion

        #region DataSource

        #region DataSourceAtaDeEleicao

        private DataSet DataSourceAtaDeEleicao()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("tDadosEmpresa", Type.GetType("System.String"));
            table.Columns.Add("tDataDaEleicao", Type.GetType("System.String"));
            table.Columns.Add("tHORARIO", Type.GetType("System.String"));
            table.Columns.Add("tLocal", Type.GetType("System.String"));
            table.Columns.Add("tFraseInicial", Type.GetType("System.String"));
            table.Columns.Add("tFraseFinal", Type.GetType("System.String"));
            table.Columns.Add("tVicePresidente", Type.GetType("System.String"));
            table.Columns.Add("tFraseAdicional", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);

            DataRow newRow = ds.Tables[0].NewRow();

            newRow["tDadosEmpresa"] = cipa.IdCliente.GetDadosEmpresaCipa_Sem_GrupoCipa(eleicaoCipa.DataSolicitacao);
            newRow["tDataDaEleicao"] = cipa.Eleicao.ToString("dd-MM-yyyy");
            newRow["tHORARIO"] = eleicaoCipa.GetHorario();
            newRow["tLocal"] = eleicaoCipa.Local;
            newRow["tFraseInicial"] = eleicaoCipa.GetFrasePadraoAbertura();
            newRow["tFraseFinal"] = eleicaoCipa.GetFrasePadraoEncerramento();
            newRow["tFraseAdicional"] = eleicaoCipa.FraseAdicional;

            MembroCipa vicePresidente = new MembroCipa();
            vicePresidente.Find("IdCipa=" + this.cipa.Id
                            + " AND IndGrupoMembro=" + (int)GrupoMembro.Empregado
                            + " AND Numero = 0 ");

            if (vicePresidente.Id != 0)
                newRow["tVicePresidente"] = vicePresidente.ToString();

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }
        #endregion

        #region DataSourceCandidatosEleitos

        private DataSet DataSourceCandidatosEleitos()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("tCandidatosEleitos", Type.GetType("System.String"));
            table.Columns.Add("tVotos", Type.GetType("System.String"));
            table.Columns.Add("tCargo", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);

            string criteria = "IdCipa=" + this.cipa.Id
                + " AND IdEmpregado IN (SELECT IdEmpregado FROM MembroCipa WHERE IdCipa=" + this.cipa.Id
                + " AND IndStatus=" + (int)MembroCipa.Status.Ativo
                + " AND IndGrupoMembro=" + (int)GrupoMembro.Empregado + ")"
                + " ORDER BY Votos DESC";

            List<ParticipantesEleicaoCipa> 
                eleitos = new ParticipantesEleicaoCipa().Find<ParticipantesEleicaoCipa>(criteria);

            DataRow newRow;

            foreach (ParticipantesEleicaoCipa eleito in eleitos)
            {
                string membroWhere = "IdCipa=" + cipa.Id
                                    + " AND IdEmpregado=" + eleito.IdEmpregado.Id
                                    + " AND IndStatus=" + (int)MembroCipa.Status.Ativo
                                    + " AND IndGrupoMembro=" + 0
                                    + " ORDER BY IndTitularSuplente, IndGrupoMembro, Numero";
                List<MembroCipa> list = new MembroCipa().Find<MembroCipa>(membroWhere);
                newRow = ds.Tables[0].NewRow();

                newRow["tCandidatosEleitos"] = eleito.ToString();
                newRow["tVotos"] = eleito.Votos.ToString();
                newRow["tCargo"] = list[0].GetNomeCargo();

                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
        #endregion

        #region DataSourceCandidatosNaoEleitos

        private DataSet DataSourceCandidatosNaoEleitos()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("tCandidatosNaoEleitos", Type.GetType("System.String"));
            table.Columns.Add("tVotos", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);

            string criteria = "IdCipa=" + this.cipa.Id
                + " AND IdEmpregado NOT IN (SELECT IdEmpregado FROM MembroCipa WHERE IdCipa=" + this.cipa.Id
                + " AND IdEmpregado IS NOT NULL"
                + ")"
                + " ORDER BY Votos DESC";

            List<ParticipantesEleicaoCipa> 
                candidatosNaoEleitos = new ParticipantesEleicaoCipa().Find<ParticipantesEleicaoCipa>(criteria);

            DataRow newRow;

            foreach (ParticipantesEleicaoCipa candidato in candidatosNaoEleitos)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["tCandidatosNaoEleitos"] = candidato.ToString();
                newRow["tVotos"] = candidato.Votos.ToString();
                ds.Tables[0].Rows.Add(newRow);
            }

            if (eleicaoCipa.VotosNulos != 0)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["tCandidatosNaoEleitos"] = "VOTOS NULOS";
                newRow["tVotos"] = eleicaoCipa.VotosNulos.ToString();
                ds.Tables[0].Rows.Add(newRow);
            }

            if (eleicaoCipa.VotosBrancos != 0)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["tCandidatosNaoEleitos"] = "VOTOS EM BRANCO";
                newRow["tVotos"] = eleicaoCipa.VotosBrancos.ToString();
                ds.Tables[0].Rows.Add(newRow);
            }

            return ds;
        }
        #endregion

        #region DataSourceSecretarios

        private DataSet DataSourceSecretarios()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("tNome", Type.GetType("System.String"));
            table.Columns.Add("tCargo", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);

            List<MembroCipa> list = new MembroCipa().Find<MembroCipa>("IdCipa=" + cipa.Id
                + " AND IndGrupoMembro=" + (int)GrupoMembro.Secretario
                + " ORDER BY IndTitularSuplente,  Numero");

            DataRow newRow;

            foreach (MembroCipa membroCipa in list)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["tNome"] = membroCipa.ToString();
                newRow["tCargo"] = membroCipa.GetNomeCargo();
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
        #endregion

        #region DataSourceRepresentantesDoEmpregador

        private DataSet DataSourceRepresentantesDoEmpregador()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("tNome", Type.GetType("System.String"));
            table.Columns.Add("tCargo", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);

            string criteria = "IdCipa=" + cipa.Id
                            + " AND IndStatus=" + (int)MembroCipa.Status.Ativo
                            + " AND IndGrupoMembro=" + (int)GrupoMembro.Empregador
                            + " ORDER BY IndTitularSuplente, IndGrupoMembro, Numero";

            List<MembroCipa> membros = new MembroCipa().Find<MembroCipa>(criteria);

            DataRow newRow;

            foreach (MembroCipa membroCipa in membros)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["tNome"] = membroCipa.ToString();
                newRow["tCargo"] = membroCipa.GetNomeCargo();
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
        #endregion

        #region DataSourceRepresentantes

        private DataSet DataSourceRepresentantes()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("tINDICADO", Type.GetType("System.String"));
            table.Columns.Add("tCARGO", Type.GetType("System.String"));
            table.Columns.Add("nNUMERO", Type.GetType("System.Int32"));
            table.Columns.Add("tNOME", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);

            string criteria = "IdCipa=" + cipa.Id
                        + " AND IndStatus=" + (int)MembroCipa.Status.Ativo
                        + " ORDER BY IndTitularSuplente, Numero";

            List<MembroCipa> membros = new MembroCipa().Find<MembroCipa>(criteria);

            DataRow newRow;

            foreach (MembroCipa membroCipa in membros)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["tINDICADO"] = membroCipa.GetGrupoMembro();
                newRow["tCARGO"] = membroCipa.GetNomeCargo();
                newRow["nNUMERO"] = membroCipa.Numero;
                newRow["tNOME"] = membroCipa.ToString();
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
        #endregion

        #region DataSourceComissaoEleitoral

        private DataSet DataSourceComissaoEleitoral()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("tINDICADO", Type.GetType("System.String"));
            table.Columns.Add("tCARGO", Type.GetType("System.String"));
            table.Columns.Add("nNUMERO", Type.GetType("System.Int32"));
            table.Columns.Add("tNOME", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);

            List<MembroComissaoEleitoral> 
                comissao = new MembroComissaoEleitoral().Find<MembroComissaoEleitoral>("IdCipa=" + cipa.Id);

            DataRow newRow;

            foreach (MembroComissaoEleitoral membro in comissao)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["tCARGO"] = string.Empty;
                newRow["tNOME"] = membro.ToString();
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
        #endregion

        #endregion
    }
}
