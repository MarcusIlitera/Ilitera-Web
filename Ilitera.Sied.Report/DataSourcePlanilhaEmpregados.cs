using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Sied.Report
{
	public class DataSourcePlanilhaEmpregados
	{
		private Cliente cliente;
		private LaudoTecnico laudoTecnico;

        public DataSourcePlanilhaEmpregados(Cliente cliente, LaudoTecnico laudoTecnico)
        {
            this.laudoTecnico = laudoTecnico;

            this.cliente = cliente;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }

        public DataSet GetDataSource()
        {
            DataSet ds = new DataSet("DataSet");
            ds.Tables.Add(GetTable());
            DataRow newRow;

            List<Ghe> listGhe = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + laudoTecnico.Id + " ORDER BY tNO_FUNC");

            foreach (Ghe ghe in listGhe)
            {
                List<Empregado> empregados = ghe.GetEmpregadosExpostos(false, false);

                List<PPRA> listPPRA = new PPRA().Find<PPRA>("nID_FUNC=" + ghe.Id + " ORDER BY nID_RSC");

                //Campo EPC
                StringBuilder sEPC = new StringBuilder();
                string strEpc = ghe.Epc();
                string strEpiAcientente = ghe.EpiAcidentes();

                if (strEpc.IndexOf("Inexistente") == -1)
                    sEPC.Append("EPC: \n" + strEpc + "\n");

                if (strEpiAcientente.IndexOf("Inexistente") == -1)
                    sEPC.Append("EPI (Riscos de Acidentes):\n" + strEpiAcientente + "\n");

                if (strEpc.IndexOf("Inexistente") != -1 && strEpiAcientente.IndexOf("Inexistente") != -1)
                    sEPC.Append("Inexistente");

                string strConclusao = ghe.Conclusao(cliente);

                foreach (Empregado empregado in empregados)
                {
                    EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(ghe, empregado);

                    string strFuncao = empregadoFuncao.GetNomeFuncao();
                    string strSetor = empregadoFuncao.GetNomeSetor();

                    foreach (PPRA ppra in listPPRA)
                    {
                        newRow = ds.Tables[0].NewRow();

                        newRow["Cliente"] = cliente.NomeAbreviado;
                        newRow["Nome do Empregado"] = empregado.tNO_EMPG;
                        newRow["Idade"] = empregado.IdadeEmpregado().ToString();
                        newRow["Sexo"] = empregado.tSEXO;
                        newRow["Raz�o Social"] = cliente.NomeCompleto;
                        newRow["Endere�o"] = cliente.GetEndereco().GetEndereco();
                        newRow["Cidade"] = cliente.GetEndereco().GetCidade();
                        newRow["Estado"] = cliente.GetEndereco().GetEstado();
                        newRow["Cep"] = cliente.GetEndereco().Cep;
                        newRow["Cnpj"] = cliente.GetCnpj();
                        newRow["Fun��o"] = strFuncao;
                        newRow["Tipo de Ghe"] = ghe.GetTipoAtividade();
                        newRow["Setor"] = strSetor;
                        newRow["Data de Admiss�o"] = empregado.hDT_ADM;
                        newRow["CodigoGfip"] = Ghe.GetGFIP(Convert.ToInt32((empregadoFuncao.nIND_GFIP)));
                        newRow["Insalubridade (S/N)"] = empregado.nIND_ADICIONAL == TipoAdicional.Insalubridade ? "S" : "N";
                        newRow["Periculosidade (S/N)"] = empregado.nIND_ADICIONAL == TipoAdicional.Periculosidade ? "S" : "N";
                        newRow["Percentual"] = empregado.nAD_INSALUBRIDADE.ToString();
                        newRow["Ghe"] = ghe.tNO_FUNC;
                        newRow["Ghe Periculosidade (S/N)"] = ghe.bPERICULOSIDADE ? "S" : "N";
                        newRow["Agente Nocivo"] = ppra.GetNomeAgenteResumido();
                        newRow["Itensidade/Concentra��o"] = ppra.GetStrValorMedido();
                        newRow["Unidade de Avalia��o"] = ppra.GetStrUnidade();
                        newRow["Agentes_LimiteTol"] = ppra.GetLimiteToleranciaSemUnidade();
                        newRow["Limite foi Ultrapassado?"] = ppra.GetLimiteFoiUltrapassado();
                        newRow["Fonte Geradora"] = ppra.tDS_FTE_GER;
                        newRow["Tempo de Exposi��o"] = ghe.nID_TEMPO_EXP.ToString();
                        newRow["Situa��o Ru�do"] = string.Empty;
                        newRow["Situa��o Calor"] = string.Empty;
                        newRow["Situa��o Agentes Qu�micos"] = string.Empty;
                        newRow["Poss�veis Comprometimentos � Sa�de"] = ppra.tDS_DANO_REL_SAU;
                        newRow["EPI"] = ppra.GetEpi();
                        newRow["EPC"] = sEPC.ToString();
                        newRow["Conclus�o (Tipo de Exposi��o)"] = ghe.GetStrTipoExposicaoAmbiental();
                        newRow["Ilumin�ncia Lux"] = ghe.nLUX + " Lux";
                        newRow["Ilumin�ncia Recomendada"] = ghe.GetIluminanciaRecomendada();
                        newRow["Velocidade do Ar"] = ghe.nVELOC + " m/s";
                        newRow["Unidade Relativa do Ar"] = ghe.nUMID + " %";
                        newRow["Temperatura em �C"] = ghe.nTEMP + " �C";

                        ds.Tables[0].Rows.Add(newRow);

                        //break;
                    }
                    //break;
                }
            }
            return ds;
        }

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("Cliente", Type.GetType("System.String"));
            table.Columns.Add("Nome do Empregado", Type.GetType("System.String"));
            table.Columns.Add("Idade", Type.GetType("System.String"));
            table.Columns.Add("Sexo", Type.GetType("System.String"));
            table.Columns.Add("Raz�o Social", Type.GetType("System.String"));
            table.Columns.Add("Endere�o", Type.GetType("System.String"));
            table.Columns.Add("Cidade", Type.GetType("System.String"));
            table.Columns.Add("Estado", Type.GetType("System.String"));
            table.Columns.Add("Cep", Type.GetType("System.String"));
            table.Columns.Add("Cnpj", Type.GetType("System.String"));
            table.Columns.Add("Fun��o", Type.GetType("System.String"));
            table.Columns.Add("Tipo de Ghe", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Data de Admiss�o", Type.GetType("System.DateTime"));
            table.Columns.Add("CodigoGfip", Type.GetType("System.String"));
            table.Columns.Add("Insalubridade (S/N)", Type.GetType("System.String"));
            table.Columns.Add("Periculosidade (S/N)", Type.GetType("System.String"));
            table.Columns.Add("Percentual", Type.GetType("System.String"));
            table.Columns.Add("Ghe", Type.GetType("System.String"));
            table.Columns.Add("Ghe Periculosidade (S/N)", Type.GetType("System.String"));
            table.Columns.Add("Agente Nocivo", Type.GetType("System.String"));
            table.Columns.Add("Itensidade/Concentra��o", Type.GetType("System.String"));
            table.Columns.Add("Unidade de Avalia��o", Type.GetType("System.String"));
            table.Columns.Add("Tipo de Avalia��o", Type.GetType("System.String"));
            table.Columns.Add("Agentes_LimiteTol", Type.GetType("System.String"));
            table.Columns.Add("Limite foi Ultrapassado?", Type.GetType("System.String"));
            table.Columns.Add("Fonte Geradora", Type.GetType("System.String"));
            table.Columns.Add("Tempo de Exposi��o", Type.GetType("System.String"));
            table.Columns.Add("Situa��o Ru�do", Type.GetType("System.String"));
            table.Columns.Add("Situa��o Calor", Type.GetType("System.String"));
            table.Columns.Add("Situa��o Agentes Qu�micos", Type.GetType("System.String"));
            table.Columns.Add("Poss�veis Comprometimentos � Sa�de", Type.GetType("System.String"));
            table.Columns.Add("EPI", Type.GetType("System.String"));
            table.Columns.Add("EPC", Type.GetType("System.String"));
            table.Columns.Add("Conclus�o (Tipo de Exposi��o)", Type.GetType("System.String"));
            table.Columns.Add("Ilumin�ncia Lux", Type.GetType("System.String"));
            table.Columns.Add("Ilumin�ncia Recomendada", Type.GetType("System.String"));
            table.Columns.Add("Velocidade do Ar", Type.GetType("System.String"));
            table.Columns.Add("Unidade Relativa do Ar", Type.GetType("System.String"));
            table.Columns.Add("Temperatura em �C", Type.GetType("System.String"));

            return table;
        }
	}
}
