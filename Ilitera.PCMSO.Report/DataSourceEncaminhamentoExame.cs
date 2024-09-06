using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceEncaminhamentoExame : DataSourceBase
    {
        private ConvocacaoExame convocacaoExame;
        private ExameBase exame;
        private DataSet ds;

        public DataSourceEncaminhamentoExame()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture 
                = new System.Globalization.CultureInfo("pt-BR");
        }

        public DataSourceEncaminhamentoExame(ConvocacaoExame convocacaoExame, 
                                            Pcmso pcmso)
            : this()
        {
            this.convocacaoExame = convocacaoExame;
        }

        public DataSourceEncaminhamentoExame(ExameBase exame)
            : this()
        {
            this.exame = exame;
        }

        public RptEncaminhamentoExame GetReport()
        {
            ds = new DataSet();
            ds.Tables.Add(GetTable());

            RptEncaminhamentoExame report = new RptEncaminhamentoExame();
            
            if (exame != null)
                report.SetDataSource(GetDataSourceExame());
            else if (convocacaoExame != null)
                report.SetDataSource(GetDataSourceConvocacao());

            report.Refresh();
            SetTempoProcessamento(report);
            return report;
        }

        public RptEncaminhamentoExame GetReportEmBranco()
        {
            RptEncaminhamentoExame report = new RptEncaminhamentoExame();
            report.SetDataSource(GetDataSourceEmBranco());
            report.Refresh();
            SetTempoProcessamento(report);
            return report;
        }

        public DataSet GetDataSourceConvocacao()
        {
            Clinica clinica = new Clinica(convocacaoExame.IdJuridica.Id);
            Cliente cliente = new Cliente(convocacaoExame.IdCliente.Id);
            Endereco endereco = cliente.GetEndereco();
            Endereco enderecoclinica = clinica.GetEndereco();

            IExameBase iExameBase = convocacaoExame;
         
            if (iExameBase.IdMedico.mirrorOld == null)
                iExameBase.IdMedico.Find();

            if (iExameBase.IdExameDicionario.mirrorOld == null)
                iExameBase.IdExameDicionario.Find();

            StringBuilder str = new StringBuilder();

            str.Append("IdConvocacaoExame=" + convocacaoExame.Id);
            str.Append(" AND IndResultado=" + (int)ResultadoExame.NaoRealizado);
            
            ArrayList listExames = new ExameBase().Find(str.ToString());

            foreach (ExameBase exame in listExames)
            {
                Empregado empregado = exame.IdEmpregado;

                if (empregado.mirrorOld == null)
                    empregado.Find();

                EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);

                DataRow newRow = GetDataSource(ds.Tables[0].NewRow(), exame, clinica, iExameBase, cliente, endereco, enderecoclinica, empregado, empregadoFuncao);

                ds.Tables[0].Rows.Add(newRow);
            }

            return ds;
        }

        public DataSet GetDataSourceExame()
        {
            Empregado empregado = exame.IdEmpregado;

            if (empregado.mirrorOld == null)
                empregado.Find();

            Clinica clinica = new Clinica(exame.IdJuridica.Id);
            Cliente cliente = new Cliente(empregado.nID_EMPR.Id);
            Endereco endereco = cliente.GetEndereco();
            Endereco enderecoclinica = clinica.GetEndereco();

            IExameBase iExameBase = exame;
            if (iExameBase.IdMedico.mirrorOld == null)
                iExameBase.IdMedico.Find();

            if (iExameBase.IdExameDicionario.mirrorOld == null)
                iExameBase.IdExameDicionario.Find();
                        
            EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);

            DataRow newRow = GetDataSource(ds.Tables[0].NewRow(), exame, clinica, iExameBase, cliente, endereco, enderecoclinica, empregado, empregadoFuncao);

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        public static DataRow GetDataSource(DataRow newRow, ExameBase exame, Clinica clinica, IExameBase iExameBase, Cliente cliente, Endereco endereco, Endereco enderecoclinica, Empregado empregado, EmpregadoFuncao empregadoFuncao)
        {
            string PathMapaClinica = enderecoclinica.GetPathMapa();
            //			clinica
            newRow["RAZAO_SOCIAL"] = clinica.NomeCompleto;
            newRow["ENDERECO"] = enderecoclinica.GetEndereco();
            newRow["CEP"] = enderecoclinica.Cep;
            newRow["CIDADE"] = enderecoclinica.GetCidade();
            newRow["ESTADO"] = enderecoclinica.GetEstado();
            newRow["HORARIOATENDIMENTO"] = clinica.HorarioAtendimento;
            newRow["TELEFONE"] = clinica.GetContatoTelefonico();
            newRow["BAIRRO"] = clinica.GetEndereco().Bairro;
            newRow["OBSENDCLINICA"] = enderecoclinica.Observacao;
            newRow["ComMapa"] = !PathMapaClinica.Equals(string.Empty);
            newRow["MapaClinica"] = PathMapaClinica;

            if (exame.HoraAgendamento != "")
                newRow["HORAMARCADA"] = exame.HoraAgendamento;
            else
            {
                newRow["HORAMARCADA"] = "*";
                newRow["OBSCLINICA"] = "* Ligar para a Clínica e verificar os horários disponíveis";
            }

            if (iExameBase.IdMedico.Id != 0)
                newRow["MEDICO"] = iExameBase.IdMedico.NomeCompleto;
            else
                newRow["MEDICO"] = "-----";

            //			empresa
            newRow["EMPRESA_RAZAO_SOCIAL"] = cliente.NomeCompleto;
            newRow["EMPRESA_ENDERECO"] = endereco.GetEndereco();
            newRow["EMPRESA_CEP"] = endereco.Cep;
            newRow["EMPRESA_CIDADE"] = endereco.GetCidade();
            newRow["EMPRESA_ESTADO"] = endereco.GetEstado();

            //          empregado
            newRow["NOME_EMPREGADO"] = empregado.tNO_EMPG;
            newRow["DATA_NASCIMENTO"] = Ilitera.Common.Utility.TratarData(empregado.hDT_NASC);
            newRow["SEXO"] = empregado.tSEXO;

            if (empregado.IdadeEmpregado() == 0)
                newRow["IDADE"] = "";
            else
                newRow["IDADE"] = empregado.IdadeEmpregado();

            newRow["RG"] = empregado.tNO_IDENTIDADE;
            newRow["FUNCAO"] = empregadoFuncao.nID_FUNCAO.ToString();
            newRow["SETOR"] = empregadoFuncao.nID_SETOR.ToString();

            //			exame
            newRow["DATAEXAME"] = Ilitera.Common.Utility.TratarData(exame.DataExame);
            newRow["TIPOEXAME"] = iExameBase.IdExameDicionario.Nome;

            newRow["Complementares"] = GetExamesComplementares(exame, empregado);

            string fileName = Ilitera.Common.Fotos.PathFoto_Uri(empregado.FotoEmpregado());

            System.IO.FileInfo file = null;

            if (fileName != string.Empty)
                file = new System.IO.FileInfo(fileName);

            if (file != null && file.Exists)
            {
                newRow["iFOTO"] = file.FullName;
                newRow["ComFoto"] = true;
            }
            else
            {
                newRow["iFOTO"] = string.Empty;
                newRow["ComFoto"] = false;
            }

            return newRow;
        }

        private static string GetExamesComplementares(ExameBase exame, Empregado empregado)
        {
            List<Empregado.ExameComplementar> examesComplementares = new List<Empregado.ExameComplementar>();

            switch (exame.IdExameDicionario.Id)
            {
                case (int)IndExameClinico.Admissional:
                    examesComplementares = empregado.ListExamesComplementaresIndicadosNaAdmissao();
                    break;
                case (int)IndExameClinico.Demissional:
                    examesComplementares = empregado.ListExamesComplementaresIndicadosNaDemissao();
                    break;
                case (int)IndExameClinico.MudancaDeFuncao:
                    examesComplementares = empregado.ListExamesComplementaresIndicadosNaMudancaFuncao();
                    break;
                case (int)IndExameClinico.Periodico:
                    examesComplementares = empregado.ListExamesComplementaresIndicadosNoPeriodico();
                    break;
                case (int)IndExameClinico.RetornoAoTrabalho:
                    examesComplementares = empregado.ListExamesComplementaresIndicadosNoRetornoTrabalho();
                    break;
            }

            StringBuilder str = new StringBuilder();

            foreach (Empregado.ExameComplementar exameComplementar in examesComplementares)
            {
                str.Append(exameComplementar.NomeExame);
                str.Append(", ");
            }

            if (str.Length > 0)
                str.Remove(str.Length - 2, 2);

            Clinico exameClinico = new Clinico(exame.Id);

            if (exameClinico.DescExameCompl)
                str.Append(" (Todos os exames complementares indicados foram desconsiderados pela empresa)");

            return str.ToString();
        }

        private DataSet GetDataSourceEmBranco()
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(GetTable());

            DataRow newRow = ds.Tables[0].NewRow();

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
            table.Columns.Add("HORAMARCADA", Type.GetType("System.String"));
            table.Columns.Add("MEDICO", Type.GetType("System.String"));
            table.Columns.Add("OBSCLINICA", Type.GetType("System.String"));
            table.Columns.Add("OBSENDCLINICA", Type.GetType("System.String"));
            table.Columns.Add("ComMapa", typeof(bool));
            table.Columns.Add("MapaClinica", typeof(string));
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
            table.Columns.Add("DATAEXAME", Type.GetType("System.String"));
            table.Columns.Add("TIPOEXAME", Type.GetType("System.String"));
            table.Columns.Add("Complementares", Type.GetType("System.String"));
            table.Columns.Add("iFOTO", Type.GetType("System.String"));
            table.Columns.Add("ComFoto", Type.GetType("System.Boolean"));
            return table;
        }
    }
}
