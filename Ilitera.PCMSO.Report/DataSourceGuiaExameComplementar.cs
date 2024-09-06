using System;
using System.Data;
using System.Collections;
using System.Text;

using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceGuiaExameComplementar : DataSourceBase
    {
        private SolicitacaoComplementar solicitacaoCompl;
        private Empregado empregado;
        private Cliente cliente;
        private Clinica clinica;
        private Endereco endereco;
        private Endereco enderecoclinica;
        private Municipio municipio;
        private Municipio municipioclinica;
        private EmpregadoFuncao empregadoFuncao;

        public DataSourceGuiaExameComplementar(SolicitacaoComplementar solicitacaoCompl)
        {
            this.solicitacaoCompl = solicitacaoCompl;
            this.solicitacaoCompl.IdMedicoResponsavel.Find();
            this.empregado = solicitacaoCompl.IdEmpregado;
            this.empregado.Find();
            this.clinica = solicitacaoCompl.IdClinica;
            this.clinica.Find();
            this.cliente = empregado.nID_EMPR;
            this.cliente.Find();
            this.endereco = cliente.GetEndereco();
            this.enderecoclinica = clinica.GetEndereco();
            this.municipio = cliente.GetMunicipio();
            this.municipio.IdUnidadeFederativa.Find();
            this.municipioclinica = clinica.GetMunicipio();
            this.municipioclinica.IdUnidadeFederativa.Find();
            this.empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
            this.empregadoFuncao.nID_SETOR.Find();
            this.empregadoFuncao.nID_FUNCAO.Find();
        }

        public RptGuiaExameComplementar GetRptGuiaExameComplementar()
        {
            RptGuiaExameComplementar report = new RptGuiaExameComplementar();
            report.SetDataSource(GetDataSource());
            report.Refresh();
            SetTempoProcessamento(report);
            return report;
        }

        private DataSet GetDataSource()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTable());

            DataRow newRow = ds.Tables[0].NewRow();

            //clinica
            newRow["RAZAO_SOCIAL"] = clinica.NomeCompleto;
            newRow["ENDERECO"] = enderecoclinica.GetEndereco();
            newRow["CEP"] = enderecoclinica.Cep;
            newRow["BAIRRO"] = enderecoclinica.Bairro;
            newRow["CIDADE"] = municipioclinica.NomeCompleto;
            newRow["ESTADO"] = municipioclinica.IdUnidadeFederativa.NomeAbreviado;
            newRow["HORARIOATENDIMENTO"] = clinica.HorarioAtendimento;
            newRow["TELEFONE"] = clinica.GetContatoTelefonico();

            //Médico Responsável
            newRow["MEDICO"] = solicitacaoCompl.IdMedicoResponsavel.NomeCompleto;
            newRow["CRM"] = solicitacaoCompl.IdMedicoResponsavel.Numero;

            //empresa
            newRow["EMPRESA_RAZAO_SOCIAL"] = cliente.NomeCompleto;
            newRow["EMPRESA_ENDERECO"] = endereco.GetEndereco();
            newRow["EMPRESA_CEP"] = endereco.Cep;
            newRow["EMPRESA_CIDADE"] = municipio.NomeCompleto;
            newRow["EMPRESA_ESTADO"] = municipio.IdUnidadeFederativa.NomeAbreviado;

            //empregado
            newRow["NOME_EMPREGADO"] = empregado.tNO_EMPG;
            newRow["DATA_NASCIMENTO"] = empregado.hDT_NASC.ToString("dd-MM-yyyy");
            newRow["SEXO"] = empregado.tSEXO;
            if (empregado.IdadeEmpregado().Equals(0))
                newRow["IDADE"] = string.Empty;
            else
                newRow["IDADE"] = empregado.IdadeEmpregado();
            newRow["RG"] = empregado.tNO_IDENTIDADE;
            newRow["FUNCAO"] = empregadoFuncao.nID_FUNCAO.NomeFuncao;
            newRow["SETOR"] = empregadoFuncao.nID_SETOR.tNO_STR_EMPR;

            //exame
            StringBuilder exames = new StringBuilder();

            ArrayList alComplSolicitados = new ComplementaresSolicitados().FindExames(solicitacaoCompl);

            foreach (ComplementaresSolicitados examesCompl in alComplSolicitados)
            {
                examesCompl.IdExameBase.Find();
                examesCompl.IdExameBase.IdExameDicionario.Find();

                if (exames.ToString().Equals(string.Empty))
                    exames.Append(examesCompl.IdExameBase.IdExameDicionario.Nome);
                else
                    exames.Append(", " + examesCompl.IdExameBase.IdExameDicionario.Nome);
            }

            newRow["EXAMES"] = exames.ToString();

            switch (solicitacaoCompl.IndExameClinico)
            {
                case (int)IndExameClinico.Admissional:
                    newRow["TIPOEXAME"] = "Admissional";
                    break;
                case (int)IndExameClinico.Demissional:
                    newRow["TIPOEXAME"] = "Demissional";
                    break;
                case (int)IndExameClinico.MudancaDeFuncao:
                    newRow["TIPOEXAME"] = "Mudança de Função";
                    break;
                case (int)IndExameClinico.Periodico:
                    newRow["TIPOEXAME"] = "Periódico";
                    break;
                case (int)IndExameClinico.RetornoAoTrabalho:
                    newRow["TIPOEXAME"] = "Retorno ao Trabalho";
                    break;
            }

            //foto
            string path = Ilitera.Common.Fotos.PathFoto_Uri(empregado.FotoEmpregado());

            if (path != string.Empty && System.IO.File.Exists(path))
            {
                newRow["iFOTO"] = path;
                newRow["ComFoto"] = true;
            }
            else
                newRow["ComFoto"] = false;

            //Observações Endereço Clínica
            newRow["OBSENDCLINICA"] = enderecoclinica.Observacao;

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("RAZAO_SOCIAL", Type.GetType("System.String"));
            table.Columns.Add("ENDERECO", Type.GetType("System.String"));
            table.Columns.Add("CEP", Type.GetType("System.String"));
            table.Columns.Add("CIDADE", Type.GetType("System.String"));
            table.Columns.Add("ESTADO", Type.GetType("System.String"));
            table.Columns.Add("HORARIOATENDIMENTO", Type.GetType("System.String"));
            table.Columns.Add("TELEFONE", Type.GetType("System.String"));
            table.Columns.Add("BAIRRO", Type.GetType("System.String"));
            table.Columns.Add("MEDICO", Type.GetType("System.String"));
            table.Columns.Add("CRM", typeof(string));
            table.Columns.Add("OBSENDCLINICA", Type.GetType("System.String"));
            table.Columns.Add("EMPRESA_RAZAO_SOCIAL", Type.GetType("System.String"));
            table.Columns.Add("EMPRESA_ENDERECO", Type.GetType("System.String"));
            table.Columns.Add("EMPRESA_CEP", Type.GetType("System.String"));
            table.Columns.Add("EMPRESA_CIDADE", Type.GetType("System.String"));
            table.Columns.Add("EMPRESA_ESTADO", Type.GetType("System.String"));
            table.Columns.Add("NOME_EMPREGADO", Type.GetType("System.String"));
            table.Columns.Add("DATA_NASCIMENTO", Type.GetType("System.String"));
            table.Columns.Add("SEXO", Type.GetType("System.String"));
            table.Columns.Add("IDADE", Type.GetType("System.String"));
            table.Columns.Add("RG", Type.GetType("System.String"));
            table.Columns.Add("FUNCAO", Type.GetType("System.String"));
            table.Columns.Add("SETOR", Type.GetType("System.String"));
            table.Columns.Add("EXAMES", Type.GetType("System.String"));
            table.Columns.Add("TIPOEXAME", Type.GetType("System.String"));
            table.Columns.Add("iFOTO", Type.GetType("System.String"));
            table.Columns.Add("ComFoto", Type.GetType("System.Boolean"));
            return table;
        }
    }
}
