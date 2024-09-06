using System;
using System.Text;
using System.Data;
using System.Collections;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Opsa.Report
{

    public class DataSourceFichaCadastralCliente
    {
        private Cliente cliente;
        private Endereco endereco;


        public DataSourceFichaCadastralCliente(Cliente cliente)
        {
            this.cliente = cliente;
            this.endereco = this.cliente.GetEndereco();
        }

        public RptFichaCadastralCliente GetReport()
        {
            RptFichaCadastralCliente report = new RptFichaCadastralCliente();
            report.SetDataSource(DataSourceRptFichaCadastral());
            report.OpenSubreport("Contatos").SetDataSource(DataSourceRptContatos());
            report.OpenSubreport("IntegrantesSESMET").SetDataSource(IntegrantesSESMT.GetGridIntegrantesSESMT(cliente));
            report.Refresh();
            return report;
        }

        private DataSet DataSourceRptFichaCadastral()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("tNomeCompleto", Type.GetType("System.String"));
            table.Columns.Add("tCod", Type.GetType("System.String"));
            table.Columns.Add("tEndereco", Type.GetType("System.String"));
            table.Columns.Add("tBairro", Type.GetType("System.String"));
            table.Columns.Add("tCidade", Type.GetType("System.String"));
            table.Columns.Add("tUF", Type.GetType("System.String"));
            table.Columns.Add("tCEP", Type.GetType("System.String"));
            table.Columns.Add("tIE", Type.GetType("System.String"));
            table.Columns.Add("tCGC", Type.GetType("System.String"));
            table.Columns.Add("tRamoAtividade", Type.GetType("System.String"));
            table.Columns.Add("tCaldeira", Type.GetType("System.String"));
            table.Columns.Add("tCompressor", Type.GetType("System.String"));
            table.Columns.Add("tEmpilhadeira", Type.GetType("System.String"));
            table.Columns.Add("tPontesRolantes", Type.GetType("System.String"));
            table.Columns.Add("tPrensas", Type.GetType("System.String"));
            table.Columns.Add("tTanques", Type.GetType("System.String"));
            table.Columns.Add("tObservacoes", Type.GetType("System.String"));
            table.Columns.Add("tOrigem", Type.GetType("System.String"));
            table.Columns.Add("tEleicaoResponsavel", Type.GetType("System.String"));
            table.Columns.Add("tSindicato", Type.GetType("System.String"));
            table.Columns.Add("tNumeroCNAE", Type.GetType("System.String"));
            table.Columns.Add("tDescricaoCNAE", Type.GetType("System.String"));
            table.Columns.Add("tGrupoCipa", Type.GetType("System.String"));
            table.Columns.Add("tGrupoRisco", Type.GetType("System.String"));
            table.Columns.Add("tSuplentes", Type.GetType("System.String"));
            table.Columns.Add("tTitular", Type.GetType("System.String"));
            table.Columns.Add("tNumeroDeEmpregados", Type.GetType("System.String"));
            table.Columns.Add("tLocalAtividade", Type.GetType("System.String"));
            table.Columns.Add("tMotorista", Type.GetType("System.String"));
            table.Columns.Add("tSite", Type.GetType("System.String"));
            table.Columns.Add("tEmail", Type.GetType("System.String"));
            table.Columns.Add("tObrigacoesCipaContratada", Type.GetType("System.String"));
            table.Columns.Add("tObrigacoesPCMSOContratada", Type.GetType("System.String"));
            table.Columns.Add("tObrigacoesRecargaExtintor", Type.GetType("System.String"));
            table.Columns.Add("tObrigacoesObraConsCivil", Type.GetType("System.String"));
            table.Columns.Add("tObrigacoesMineradora", Type.GetType("System.String"));
            table.Columns.Add("tDRT", Type.GetType("System.String"));
            table.Columns.Add("DataDRT", Type.GetType("System.DateTime"));
            table.Columns.Add("tNumeroDRT", Type.GetType("System.String"));
            table.Columns.Add("tOrigemCliente", Type.GetType("System.String"));
            table.Columns.Add("tOrigemOutros", Type.GetType("System.String"));
            ds.Tables.Add(table);
            DataRow newRow;

            newRow = ds.Tables[0].NewRow();

            newRow["tNomeCompleto"] = cliente.NomeCompleto;
            newRow["tCod"] = cliente.NomeCodigo;
            newRow["tEndereco"] = endereco.GetEndereco();
            newRow["tBairro"] = endereco.Bairro;
            newRow["tCidade"] = endereco.GetCidade();
            newRow["tUF"] = endereco.GetEstado();
            newRow["tCEP"] = endereco.Cep;
            newRow["tIE"] = cliente.IE;
            newRow["tCGC"] = cliente.NomeCodigo;
            newRow["tRamoAtividade"] = cliente.Atividade;

            newRow["tCaldeira"] = GetValue(cliente.QtdCaldeiras);
            newRow["tCompressor"] = GetValue(cliente.QtdVasoPressao);
            newRow["tEmpilhadeira"] = GetValue(cliente.QtdEmpilhadeiras);

            newRow["tObservacoes"] = cliente.Observacao;
            newRow["tOrigem"] = cliente.IdOrigem.ToString();
            newRow["tEleicaoResponsavel"] = cliente.IdRespEleicaoCIPA.ToString();
            newRow["tSindicato"] = cliente.IdSindicato.ToString();

            cliente.IdCNAE.Find();
            cliente.IdCNAE.IdGrupoCipa.Find();

            newRow["tNumeroCNAE"] = cliente.IdCNAE.Codigo;
            newRow["tDescricaoCNAE"] = cliente.IdCNAE.Descricao;
            newRow["tGrupoRisco"] = cliente.IdCNAE.GrauRisco.ToString();
            newRow["tGrupoCipa"] = cliente.IdCNAE.IdGrupoCipa.Descricao;

            DimensionamentoCipa dim = cliente.GetDimensionamentoCipa();

            newRow["tSuplentes"] = dim.Suplente.ToString();
            newRow["tTitular"] = dim.Efetivo.ToString();

            newRow["tNumeroDeEmpregados"] = cliente.QtdEmpregados.ToString();
            newRow["tLocalAtividade"] = cliente.LocalAtividades;
            newRow["tMotorista"] = cliente.IdTransporte.ToString();
            newRow["tSite"] = cliente.Site;
            newRow["tEmail"] = cliente.Email;

            newRow["tPontesRolantes"] = GetValue(cliente.QtdPontes);
            newRow["tPrensas"] = GetValue(cliente.QtdPrensas);
            newRow["tTanques"] = GetValue(cliente.QtdTanques);

            newRow["tDRT"] = cliente.IdDRT.ToString();
            newRow["DataDRT"] = cliente.DataRegistroDRT;
            newRow["tNumeroDRT"] = cliente.NumeroRegistroDRT;
            newRow["tOrigemCliente"] = cliente.IdOrigem.ToString();
            newRow["tOrigemOutros"] = cliente.OrigemOutros;

            newRow["tObrigacoesCipaContratada"] = GetBoleanValue(cliente.ContrataCipa != (int)TipoCipa.NaoContratada);
            newRow["tObrigacoesPCMSOContratada"] = GetBoleanValue(cliente.ContrataPCMSO != (int)TipoPcmsoContratada.NaoContratada);
            newRow["tObrigacoesRecargaExtintor"] = GetBoleanValue(cliente.RecargaExtintor);
            newRow["tObrigacoesObraConsCivil"] = GetBoleanValue(cliente.HasObraCivil);
            newRow["tObrigacoesMineradora"] = GetBoleanValue(cliente.IsMineradora);

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private string GetValue(int iVal)
        {
            if (iVal != 0)
                return iVal.ToString();
            else
                return "-";
        }

        private string GetBoleanValue(bool bVal)
        {
            if (bVal)
                return "Sim";
            else
                return "Não";
        }

        public DataSet DataSourceRptContatos()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("tNomeContato", Type.GetType("System.String"));
            table.Columns.Add("tDDD", Type.GetType("System.String"));
            table.Columns.Add("tNumero", Type.GetType("System.String"));
            table.Columns.Add("tDepartamento", Type.GetType("System.String"));
            table.Columns.Add("tTipo", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);

            DataRow newRow;

            ArrayList listContatos = new ArrayList();

            ContatoTelefonico contatoTelefonicoPadrao = new ContatoTelefonico();
            contatoTelefonicoPadrao = cliente.GetContatoTelefonico();

            if (contatoTelefonicoPadrao.Id != 0)
                listContatos.Add(contatoTelefonicoPadrao);

            ContatoTelefonico contatoTelefonicoSegundo = new ContatoTelefonico();
            contatoTelefonicoSegundo = cliente.GetContatoTelefonico2();

            if (contatoTelefonicoSegundo.Id != 0)
                listContatos.Add(contatoTelefonicoSegundo);

            ContatoTelefonico contatoFax = new ContatoTelefonico();
            contatoFax = cliente.GetFax();

            if (contatoFax.Id != 0)
                listContatos.Add(contatoFax);

            for (int i = 0; i < listContatos.Count; i++)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["tNomeContato"] = ((ContatoTelefonico)listContatos[i]).Nome;
                newRow["tDDD"] = ((ContatoTelefonico)listContatos[i]).DDD;
                newRow["tNumero"] = ((ContatoTelefonico)listContatos[i]).Numero;
                newRow["tDepartamento"] = ((ContatoTelefonico)listContatos[i]).Departamento;

                switch (((ContatoTelefonico)listContatos[i]).IndTipoTelefone)
                {
                    case 0: newRow["tTipo"] = "Padrão";
                        break;
                    case 1: newRow["tTipo"] = "Segundo";
                        break;
                    case 2: newRow["tTipo"] = "Fax";
                        break;
                }
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
    }
}
