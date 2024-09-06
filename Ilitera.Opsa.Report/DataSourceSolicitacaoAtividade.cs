using System;
using System.Collections;
using System.Data;
using System.Drawing;
using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Report
{
    public class DataSourceSolicitacaoAtividade
    {

        private PedidoGrupo pedidoGrupo;

        public DataSourceSolicitacaoAtividade(PedidoGrupo pedidoGrupo)
        {
            this.pedidoGrupo = pedidoGrupo;
        }

        public RptSolicitacaoDeAtividades GetReport()
        {
            RptSolicitacaoDeAtividades report = new RptSolicitacaoDeAtividades();
            report.OpenSubreport("PeriodoRealizacaoAtividades").SetDataSource(GetGridAgendados());
            report.OpenSubreport("ListaDePedidos").SetDataSource(DataSourceRptListaDePedidos());
            report.SetDataSource(DataSourceRptSolicitacaoDeAtividades());
            report.Refresh();
            return report;
        }

        public DataSet DataSourceRptSolicitacaoDeAtividades()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("dDataSolicitacao", Type.GetType("System.DateTime"));
            table.Columns.Add("tCliente", Type.GetType("System.String"));
            table.Columns.Add("tSolicitante", Type.GetType("System.String"));
            table.Columns.Add("tPrestador", Type.GetType("System.String"));
            table.Columns.Add("tRazaoSocial", Type.GetType("System.String"));
            table.Columns.Add("tEndereco", Type.GetType("System.String"));
            table.Columns.Add("tBairro", Type.GetType("System.String"));
            table.Columns.Add("tCidade", Type.GetType("System.String"));
            table.Columns.Add("tUF", Type.GetType("System.String"));
            table.Columns.Add("tCEP", Type.GetType("System.String"));
            table.Columns.Add("tIE", Type.GetType("System.String"));
            table.Columns.Add("tCGC", Type.GetType("System.String"));
            table.Columns.Add("tTelefone", Type.GetType("System.String"));
            table.Columns.Add("tFax", Type.GetType("System.String"));
            table.Columns.Add("tDepartamento", Type.GetType("System.String"));
            table.Columns.Add("tContato", Type.GetType("System.String"));
            table.Columns.Add("tRamoAtividade", Type.GetType("System.String"));
            table.Columns.Add("tCaldeira", Type.GetType("System.String"));
            table.Columns.Add("tCompressor", Type.GetType("System.String"));
            table.Columns.Add("tEmpilhadeira", Type.GetType("System.String"));
            table.Columns.Add("tPonteRolante", Type.GetType("System.String"));
            table.Columns.Add("tCNAE", Type.GetType("System.String"));
            table.Columns.Add("tGrupo", Type.GetType("System.String"));
            table.Columns.Add("tGrauDeRisco", Type.GetType("System.String"));
            table.Columns.Add("tNumeroDeEmpregados", Type.GetType("System.String"));
            table.Columns.Add("tObservacoes", Type.GetType("System.String"));
            table.Columns.Add("tNumeroPedido", Type.GetType("System.String"));
            table.Columns.Add("iFOTO", Type.GetType("System.Byte[]"));
            ds.Tables.Add(table);
            DataRow newRow;
            newRow = ds.Tables[0].NewRow();

            pedidoGrupo.Find();

            newRow["dDataSolicitacao"] = pedidoGrupo.DataSolicitacao;

            pedidoGrupo.IdCliente.Find();
            newRow["tCliente"] = pedidoGrupo.IdCliente.NomeAbreviado;
            newRow["tSolicitante"] = pedidoGrupo.Solicitante;

            if (pedidoGrupo.IdCompromisso.Id != 0)
                newRow["tPrestador"] = pedidoGrupo.GetPrestadoresAgendadosPedido();
            else
                newRow["tPrestador"] = pedidoGrupo.IdPrestador.ToString();

            newRow["tRazaoSocial"] = pedidoGrupo.IdCliente.NomeCompleto;

            Endereco endereco = pedidoGrupo.IdCliente.GetEndereco();

            newRow["tEndereco"] = endereco.GetEndereco();
            newRow["tBairro"] = endereco.Bairro;
            newRow["tCidade"] = endereco.GetCidade();
            newRow["tUF"] = endereco.GetEstado();
            newRow["tCEP"] = endereco.Cep;

            newRow["tIE"] = pedidoGrupo.IdCliente.IE;
            newRow["tCGC"] = pedidoGrupo.IdCliente.NomeCodigo;
            ContatoTelefonico contatoTelefonico = new ContatoTelefonico();
            contatoTelefonico = pedidoGrupo.IdCliente.GetContatoTelefonico();
            newRow["tTelefone"] = contatoTelefonico.Numero;
            ContatoTelefonico contatoFax = new ContatoTelefonico();
            contatoFax = pedidoGrupo.IdCliente.GetFax();
            newRow["tFax"] = contatoFax.Numero;
            newRow["tDepartamento"] = contatoTelefonico.Departamento;
            newRow["tContato"] = contatoTelefonico.Nome;
            newRow["tRamoAtividade"] = pedidoGrupo.IdCliente.Atividade;
            newRow["tCaldeira"] = pedidoGrupo.IdCliente.QtdCaldeiras.ToString();
            newRow["tCompressor"] = pedidoGrupo.IdCliente.QtdVasoPressao.ToString();
            newRow["tEmpilhadeira"] = pedidoGrupo.IdCliente.QtdEmpilhadeiras.ToString();
            newRow["tPonteRolante"] = pedidoGrupo.IdCliente.QtdPontes.ToString();
            pedidoGrupo.IdCliente.IdCNAE.Find();
            newRow["tCNAE"] = pedidoGrupo.IdCliente.IdCNAE.Codigo + " " + pedidoGrupo.IdCliente.IdCNAE.Descricao;
            pedidoGrupo.IdCliente.IdCNAE.IdGrupoCipa.Find();
            newRow["tGrupo"] = pedidoGrupo.IdCliente.IdCNAE.IdGrupoCipa.Descricao;
            newRow["tGrauDeRisco"] = pedidoGrupo.IdCliente.IdCNAE.GrauRisco.ToString();


            newRow["tNumeroDeEmpregados"] = pedidoGrupo.IdCliente.QtdEmpregados.ToString();
            newRow["tObservacoes"] = pedidoGrupo.Observacao;
            newRow["tNumeroPedido"] = pedidoGrupo.Numero.ToString("###.###");
            try
            {
                pedidoGrupo.IdPrestador.IdPessoa.Find();
                newRow["iFOTO"] = Ilitera.Common.Fotos.GetByteFoto_Uri((pedidoGrupo.IdPrestador.IdPessoa.Foto));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message.ToString());
            }
            ds.Tables[0].Rows.Add(newRow);
            return ds;
        }

        private DataSet GetGridAgendados()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdCompromisso", Type.GetType("System.Int32"));
            table.Columns.Add("Data", Type.GetType("System.String"));
            table.Columns.Add("Observação", Type.GetType("System.String"));
            table.Columns.Add("Descrição", Type.GetType("System.String"));
            ds.Tables.Add(table);
            DataRow newRow;

            if (pedidoGrupo.IdCompromisso.Id == 0)
                return ds;

            pedidoGrupo.IdCompromisso.Find();

            ArrayList list = new ArrayList();

            if (pedidoGrupo.IdCompromisso.IdRepetir.Id != 0)
                list = new Compromisso().Find("IdRepetir=" + pedidoGrupo.IdCompromisso.IdRepetir.Id + " ORDER BY DataInicio");
            else
                list.Add(pedidoGrupo.IdCompromisso);

            for (int i = 0; i < list.Count; i++)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["IdCompromisso"] = ((Compromisso)list[i]).Id;
                newRow["Data"] = ((Compromisso)list[i]).GetDataComprimisso() + " (" + ((Compromisso)list[i]).IdPessoa.ToString() + ")";
                newRow["Observação"] = ((Compromisso)list[i]).Descricao;
                newRow["Descrição"] = ((Compromisso)list[i]).Assunto;
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }

        private DataSet DataSourceRptListaDePedidos()
        {
            DataSet ds = new DataSet();

            DataTable table = new DataTable("Result");
            table.Columns.Add("tPedidos", Type.GetType("System.String"));

            ds.Tables.Add(table);

            DataRow newRow;

            ArrayList list = new Pedido().Find("IdPedidoGrupo=" + pedidoGrupo.Id);

            for (int i = 0; i < list.Count; i++)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["tPedidos"] = ((Pedido)list[i]).ToString();
                ds.Tables[0].Rows.Add(newRow);
            }

            return ds;
        }
    }
}
