using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using Ilitera.Data;

using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.PCMSO.Report
{
    public class DataSourceExposicaoAso : DataSourceBase
	{
		private DataSet ds;
		private LaudoTecnico laudoTecnico;

		public DataSourceExposicaoAso()
		{
			ds = new DataSet();
			ds.Tables.Add(GetDataTable());
		}

		private DataTable GetDataTable()
		{
			DataTable table = new DataTable("Result");
			table.Columns.Add("ID", Type.GetType("System.Int32"));
			table.Columns.Add("nID_EMPREGADO", Type.GetType("System.Int32"));
			table.Columns.Add("tPERIODO", Type.GetType("System.Int32"));
			table.Columns.Add("tAGENTE", Type.GetType("System.String"));
			table.Columns.Add("tINTENSIDADE", Type.GetType("System.String"));
			table.Columns.Add("tEPI_EPC_TEC_UTIL", Type.GetType("System.String"));
			table.Columns.Add("tEPI_EPC_EFICAZ", Type.GetType("System.String"));
			table.Columns.Add("tGFIP", Type.GetType("System.String"));
			return table;
		}

        private void Exposicao(Empregado empregado)
        {
            DataRow newRow;

            if (empregado.mirrorOld == null)
                empregado.Find();

            laudoTecnico = new LaudoTecnico();
            laudoTecnico.FindMax("hDT_LAUDO", "nID_EMPR=" + empregado.nID_EMPR.Id);
          
            ArrayList listEmpregFuncao = new EmpregadoFuncao().Find("nID_EMPREGADO=" + empregado.Id);
            
            bool bFlag = false;
            
            foreach(EmpregadoFuncao empregadoFuncao in listEmpregFuncao)
            {
                GheEmpregado gheEmpregado = new GheEmpregado();
                gheEmpregado.Find("nID_LAUD_TEC=" + laudoTecnico.Id + " AND nID_EMPREGADO_FUNCAO=" + empregadoFuncao.Id);
                
                ArrayList listPPRA = new PPRA().Find("nID_FUNC=" + gheEmpregado.nID_FUNC.Id + " ORDER BY nID_RSC");
                
                foreach(PPRA ppra in listPPRA)
                {
                    //Só aparace os que estão acima do limite de tolerância
                    if ((ppra.gINSALUBRE || ppra.nID_RSC.Id == (int)Riscos.Ergonomicos || ppra.nID_RSC.Id == (int)Riscos.Acidentes)
                        && ppra.nID_RSC.Id != (int)Riscos.Frio
                        && ppra.nID_RSC.Id != (int)Riscos.Umidade)
                    {
                        bFlag = true;

                        if (ppra.nID_RSC.mirrorOld == null)
                            ppra.nID_RSC.Find();

                        newRow = ds.Tables[0].NewRow();

                        newRow["ID"] = empregado.nID_EMPR.Id;
                        newRow["nID_EMPREGADO"] = empregado.Id;
                        newRow["tAGENTE"] = ppra.GetNomeAgente();
                        newRow["tINTENSIDADE"] = "";
                        newRow["tEPI_EPC_TEC_UTIL"] = ppra.nID_EQP_MED.Id;
                        newRow["tEPI_EPC_EFICAZ"] = ppra.GetEficaz() ? "SIM" : "NÃO";

                        ds.Tables[0].Rows.Add(newRow);
                    }
                }
                Pcmso pcmso = new Pcmso();
                pcmso.Find("IdLaudoTecnico=" + laudoTecnico.Id);

                PcmsoGhe pcmsoGhe = new PcmsoGhe();
                pcmsoGhe.Find("IdPcmso=" + pcmso.Id + " AND IdGHE=" + gheEmpregado.nID_FUNC.Id);

                if (pcmsoGhe.Id != 0)
                {
                    newRow = ds.Tables[0].NewRow();

                    newRow["ID"] = empregado.nID_EMPR.Id;
                    newRow["nID_EMPREGADO"] = empregado.Id;
                    newRow["tAGENTE"] = pcmsoGhe.RiscosOcupacionais;
                    newRow["tINTENSIDADE"] = "";
                    newRow["tEPI_EPC_TEC_UTIL"] = "";
                    newRow["tEPI_EPC_EFICAZ"] = "";
                    newRow["tGFIP"] = "";

                    ds.Tables[0].Rows.Add(newRow);

                    bFlag = true;
                }
            }
            if (bFlag == false || listEmpregFuncao.Count == 0)
            {
                newRow = ds.Tables[0].NewRow();

                newRow["ID"] = empregado.nID_EMPR.Id;
                newRow["nID_EMPREGADO"] = empregado.Id;
                newRow["tAGENTE"] = "Risco ocupacional inexistente"; //"Sem Risco Ocupacional Específico";
                newRow["tINTENSIDADE"] = "";
                newRow["tEPI_EPC_TEC_UTIL"] = "";
                newRow["tEPI_EPC_EFICAZ"] = "";
                newRow["tGFIP"] = "";

                ds.Tables[0].Rows.Add(newRow);
            }
        }

		public DataSet GetAsoExposicao(Empregado empregado)
		{
			Exposicao(empregado);
			return ds;
		}

        public DataSet GetAsoExposicao(Clinico clinico)
        {
            if (clinico.IdEmpregado != null)
            {
                Empregado empregado = clinico.IdEmpregado;

                if (empregado.mirrorOld == null)
                    empregado.Find();

                Exposicao(empregado);
            }
            else
            {
                DataRow newRow = ds.Tables[0].NewRow();
                newRow["ID"] = 0;
                newRow["nID_EMPREGADO"] = 0;
                newRow["tAGENTE"] = "";
                newRow["tINTENSIDADE"] = "";
                newRow["tEPI_EPC_TEC_UTIL"] = "";
                newRow["tEPI_EPC_EFICAZ"] = "";
                newRow["tGFIP"] = "";
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }

		public DataSet GetAsoExposicao(ConvocacaoExame convocacaoExame)
		{
            string where = "nID_EMPREGADO IN (SELECT IdEmpregado FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.ExameBase WHERE"
                            +" IdConvocacaoExame=" + convocacaoExame.Id + ")";
            
            ArrayList listEmpregado = new Empregado().Find(where);

            foreach (Empregado empregado in listEmpregado)
                Exposicao(empregado);

			return ds;
		}
	}
}
