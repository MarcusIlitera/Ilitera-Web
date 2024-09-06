using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Text;

namespace Ilitera.PCMSO.Report
{	
	public class DataSourceSolicitacaoExamesLaboratoriais : DataSourceBase
	{
        private ConvocacaoExame convocacaExame;
        private ArrayList listExames;
        private ExameDicionario exameDicionario;
        private Empregado empregado;
        private Medico medicoExame;
        private IndExameClinico indExameClinico;

        #region Construtores

        public DataSourceSolicitacaoExamesLaboratoriais(Empregado empregado, IndExameClinico indExameClinico)
            :
            this(empregado, indExameClinico, new Medico())
        {
        }

        public DataSourceSolicitacaoExamesLaboratoriais(Empregado empregado, IndExameClinico indExameClinico, Medico medicoExame)
            :
            this(empregado, new ExameDicionario(), medicoExame, indExameClinico)
        {
        }

        public DataSourceSolicitacaoExamesLaboratoriais(Empregado empregado, ExameDicionario exameDicionario)
            :
            this(empregado, exameDicionario, new Medico())
        {
        }

        public DataSourceSolicitacaoExamesLaboratoriais(Empregado empregado, ExameDicionario exameDicionario, Medico medicoExame)
            :
            this(empregado, exameDicionario, medicoExame, 0)
        {
        }

        public DataSourceSolicitacaoExamesLaboratoriais(Empregado empregado, ExameDicionario exameDicionario, Medico medicoExame, IndExameClinico indExameClinico)
        {
            this.exameDicionario = exameDicionario;
            this.empregado = empregado;
            this.medicoExame = medicoExame;
            this.indExameClinico = indExameClinico;
        }
        
        public DataSourceSolicitacaoExamesLaboratoriais(Clinico clinico)
        {
            this.listExames = new ArrayList();
            this.listExames.Add(clinico);
        }

        public DataSourceSolicitacaoExamesLaboratoriais(ConvocacaoExame convocacaExame, ArrayList listExames)
		{
            this.convocacaExame = convocacaExame;
            this.listExames = listExames;
        }

        #endregion

        #region GetReport

        public RptSolicitacaoExamesLaboratoriais GetReport()
		{
            RptSolicitacaoExamesLaboratoriais report = new RptSolicitacaoExamesLaboratoriais();

            if (listExames != null)
                report.SetDataSource(GetDataSetByExamesClinicos());
            else if (!exameDicionario.Id.Equals(0))
                report.SetDataSource(GetDataSetByExamesDicionario());
            else
                report.SetDataSource(GetDataSetByEmpregado());

			report.Refresh();

            SetTempoProcessamento(report);

			return report;
        }

        public RptSolicitacaoExamesLista GetReportLista()
        {
            RptSolicitacaoExamesLista report = new RptSolicitacaoExamesLista();

            if (listExames != null)
                report.SetDataSource(GetDataSetByExamesClinicos());
            else if (!exameDicionario.Id.Equals(0))
                report.SetDataSource(GetDataSetByExamesDicionario());
            else
                report.SetDataSource(GetDataSetByEmpregado());

            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

        #endregion

        #region DataSets

        private DataSet GetDataSetByEmpregado()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTable());
            DataRow newRow;

            List<Empregado.ExameComplementar> listExameComplementar = new List<Empregado.ExameComplementar>();

            switch (indExameClinico)
            {
                case IndExameClinico.Admissional:
                    listExameComplementar = empregado.ListExamesComplementaresIndicadosNaAdmissao();
                    break;
                case IndExameClinico.Demissional:
                    listExameComplementar = empregado.ListExamesComplementaresIndicadosNaDemissao();
                    break;
                case IndExameClinico.MudancaDeFuncao:
                    listExameComplementar = empregado.ListExamesComplementaresIndicadosNaMudancaFuncao();
                    break;
                case IndExameClinico.Periodico:
                    listExameComplementar = empregado.ListExamesComplementaresIndicadosNoPeriodico();
                    break;
                case IndExameClinico.RetornoAoTrabalho:
                    listExameComplementar = empregado.ListExamesComplementaresIndicadosNoRetornoTrabalho();
                    break;
            }

            StringBuilder strExamesDicionario = new StringBuilder();
            
            foreach (Empregado.ExameComplementar exameComplementar in listExameComplementar)
            {
                if (!strExamesDicionario.ToString().Equals(string.Empty))
                    strExamesDicionario.Append(", ");

                strExamesDicionario.Append(exameComplementar.NomeExame);
            }

            newRow = ds.Tables[0].NewRow();

            newRow["NomeEmpregado"] = empregado.ToString();
            newRow["ExamesSolicitados"] = strExamesDicionario.ToString();
            newRow["DataSolicitacao"] = DateTime.Now.ToString("dd-MM-yyyy");

            if (!medicoExame.Id.Equals(0))
            {
                newRow["NomeMedico"] = medicoExame.NomeCompleto;
                newRow["TituloMedico"] = medicoExame.Titulo;
                newRow["NumeroMedico"] = medicoExame.Numero;
                newRow["ContatoMedico"] = medicoExame.Contato;
            }

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }
        
        private DataSet GetDataSetByExamesDicionario()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTable());
            DataRow newRow;

            newRow = ds.Tables[0].NewRow();

            newRow["NomeEmpregado"] = empregado.ToString();
            newRow["ExamesSolicitados"] = exameDicionario.ToString();
            newRow["DataSolicitacao"] = DateTime.Now.ToString("dd-MM-yyyy");

            if (!medicoExame.Id.Equals(0))
            {
                newRow["NomeMedico"] = medicoExame.NomeCompleto;
                newRow["TituloMedico"] = medicoExame.Titulo;
                newRow["NumeroMedico"] = medicoExame.Numero;
                newRow["ContatoMedico"] = medicoExame.Contato;
            }

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private DataSet GetDataSetByExamesClinicos()
		{
			DataSet ds = new DataSet();
			ds.Tables.Add(GetTable());
            DataRow newRow;
            
            Pcmso pcmso;
            Medico medico;

            if (convocacaExame != null)
            {
                medico = convocacaExame.IdMedico;
                pcmso = convocacaExame.IdCliente.GetUltimoPcmso();
            }
            else
            {
                medico = ((Clinico)listExames[0]).IdMedico;
                pcmso = ((Clinico)listExames[0]).IdPcmso;
            }

            if (medico.mirrorOld == null)
                medico.Find();

            if (pcmso.mirrorOld == null)
                pcmso.Find();

            foreach (Clinico exame in listExames)
			{
                Ghe ghe = exame.IdEmpregadoFuncao.GetGheEmpregado(pcmso.IdLaudoTecnico);

                //if (ghe == null || ghe.Id == 0)
                //    throw new Exception("O empregado " + exame.IdEmpregado.ToString() + " não está associado a nenhum GHE ou o PCMSO ainda não foi atualizado para o novo Laudo Técnico realizado!");

                List<ExameDicionario> listExamesDic = ghe.GetExamesComplementaresParaSolicitacao(pcmso);

                if (listExamesDic.Count.Equals(0))
                    continue;

                StringBuilder strExamesDicionario = new StringBuilder();

                foreach (ExameDicionario exameDic in listExamesDic)
                {
                    if (!strExamesDicionario.ToString().Equals(string.Empty))
                        strExamesDicionario.Append(", ");

                    strExamesDicionario.Append(exameDic.Descricao);
                }
                
                newRow = ds.Tables[0].NewRow();

                newRow["NomeEmpregado"] = exame.IdEmpregado.ToString();
                newRow["ExamesSolicitados"] = strExamesDicionario.ToString();
                newRow["DataSolicitacao"] = exame.DataExame.ToString("dd-MM-yyyy");
                newRow["NomeMedico"] = medico.NomeCompleto;
                newRow["TituloMedico"] = medico.Titulo;
                newRow["NumeroMedico"] = medico.Numero;
                newRow["ContatoMedico"] = medico.Contato;

                ds.Tables[0].Rows.Add(newRow);
			}

			return ds;
        }

        #endregion

        #region Tables

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("NomeEmpregado", Type.GetType("System.String"));
            table.Columns.Add("ExamesSolicitados", Type.GetType("System.String"));
            table.Columns.Add("DataSolicitacao", Type.GetType("System.String"));
            table.Columns.Add("NomeMedico", Type.GetType("System.String"));
            table.Columns.Add("TituloMedico", Type.GetType("System.String"));
            table.Columns.Add("NumeroMedico", Type.GetType("System.String"));
            table.Columns.Add("ContatoMedico", Type.GetType("System.String"));

            return table;
        }

        #endregion
    }
}
