using System;
using System.Data;
using System.Data.SqlClient;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Collections;
using System.Drawing;
using Ilitera.Data;
using System.Collections.Generic;

namespace Ilitera.Sied.Report
{	
	public class DataSourcePPRAAnexo : DataSourceBase
	{
		private LaudoTecnico laudoTecnico;
		private Cliente cliente;
        private string xErg;

        public DataSourcePPRAAnexo()
        {
            this.xErg = "";
        }

        public DataSourcePPRAAnexo(Cliente cliente)
		{
			this.cliente = cliente;
            this.xErg = "";
            this.laudoTecnico = LaudoTecnico.GetUltimoLaudo(cliente.Id);
		}

		public DataSourcePPRAAnexo(LaudoTecnico laudoTecnico)
		{
			this.laudoTecnico = laudoTecnico;
            this.cliente = laudoTecnico.nID_EMPR;
            this.xErg = "";

            if (this.cliente.mirrorOld==null)
                this.cliente.Find();
		}


        public DataSourcePPRAAnexo(int idlaudoTecnico)
        {
            LaudoTecnico xLaudo = new LaudoTecnico(idlaudoTecnico);
            
            this.laudoTecnico = xLaudo;
            this.cliente = laudoTecnico.nID_EMPR;
            this.xErg = "";

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }


        public DataSourcePPRAAnexo(int idlaudoTecnico, string zErg)
        {
            LaudoTecnico xLaudo = new LaudoTecnico(idlaudoTecnico);

            this.laudoTecnico = xLaudo;
            this.cliente = laudoTecnico.nID_EMPR;
            this.xErg = zErg;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }


        public DataSourcePPRAAnexo(Cliente cliente, LaudoTecnico laudoTecnico)
        {
            this.laudoTecnico = laudoTecnico;
            this.cliente = cliente;
            this.xErg = "";

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();
        }



        public RptPPRAAnexo2 GetReport_LaudoEletrico(Int32 zIdLaudo)
        {
            RptPPRAAnexo2 report = new RptPPRAAnexo2();

            report.Load();

            report.SetDataSource(GetDataSource_Laudo(zIdLaudo));


            report.Refresh();

            //report.Subreports[0].SetDataSource(GetDataSource2());

            SetTempoProcessamento(report);

            return report;
        }



        public RptPPRAAnexo2 GetReport_LaudoSPDA(Int32 zIdLaudo)
        {
            RptPPRAAnexo2 report = new RptPPRAAnexo2();

            report.Load();

            report.SetDataSource(GetDataSource_LaudoSPDA(zIdLaudo));


            report.Refresh();

            //report.Subreports[0].SetDataSource(GetDataSource2());

            SetTempoProcessamento(report);

            return report;
        }


        public RptPPRATextoAnexo GetReport_Texto_Erg()
        {
            RptPPRATextoAnexo report = new RptPPRATextoAnexo();

            report.Load();

            report.SetDataSource(GetDataSourceTexto_Erg(this.laudoTecnico.Id));

            report.Refresh();

            //report.Subreports[0].SetDataSource(GetDataSource2());

            SetTempoProcessamento(report);

            return report;
        }



        public RptPPRAAnexo2 GetReport()
		{
			RptPPRAAnexo2 report = new RptPPRAAnexo2();	

			report.Load();

            if (this.xErg == "") report.SetDataSource(GetDataSource());
            else report.SetDataSource(GetDataSource_Erg());


            report.Refresh();

            //report.Subreports[0].SetDataSource(GetDataSource2());

            SetTempoProcessamento(report);
            
			return report;
		}


        public RptPPRAAnexo2 GetReportPCMSO(Int32 rIdPCMSO)
        {
            RptPPRAAnexo2 report = new RptPPRAAnexo2();

            report.Load();

            report.SetDataSource(GetDataSource_PCMSO(rIdPCMSO));

            report.Refresh();

            //report.Subreports[0].SetDataSource(GetDataSource2());

            SetTempoProcessamento(report);

            return report;
        }



        private DataSet GetDataSource_PCMSO(Int32 zIdPcmso)
        {
            DataSet zdS = new DataSet();
            DataTable table = GetTable();
            DataRow newRow;


            zdS.Tables.Add(table);


            //table.Columns.Add("Arquivo", Type.GetType("System.String"));


            //carregar lista de anexos
            List<PCMSO_Anexo> rAnexos = new PCMSO_Anexo().Find<PCMSO_Anexo>(" IdPcmso = " + zIdPcmso.ToString() + " order by Arquivo ");

            foreach (PCMSO_Anexo zAnexo in rAnexos)
            {
                newRow = zdS.Tables[0].NewRow();

                newRow["Arquivo"] = Ilitera.Common.Fotos.GetByteFoto_Uri(zAnexo.Arquivo);
                newRow["Descricao"] = zAnexo.Descricao;

                zdS.Tables[0].Rows.Add(newRow);


            }

            return zdS;


        }


        private DataSet GetDataSource_Laudo(Int32 rIdLaudo)
        {
            DataSet zdS = new DataSet();
            DataTable table = GetTable();
            DataRow newRow;


            zdS.Tables.Add(table);


            //table.Columns.Add("Arquivo", Type.GetType("System.String"));


            LaudoEletrico zLaudo = new LaudoEletrico();
            zLaudo.Find(rIdLaudo);

            if (zLaudo.Anexo1.Trim() != "")
            {
                newRow = zdS.Tables[0].NewRow();

                newRow["Arquivo"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zLaudo.Anexo1);
                newRow["Descricao"] = zLaudo.Descr_Anexo1;

                zdS.Tables[0].Rows.Add(newRow);
            }

            if (zLaudo.Anexo2.Trim() != "")
            {
                newRow = zdS.Tables[0].NewRow();

                newRow["Arquivo"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zLaudo.Anexo2);
                newRow["Descricao"] = zLaudo.Descr_Anexo2;

                zdS.Tables[0].Rows.Add(newRow);
            }

            if (zLaudo.Anexo3.Trim() != "")
            {
                newRow = zdS.Tables[0].NewRow();

                newRow["Arquivo"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zLaudo.Anexo3);
                newRow["Descricao"] = zLaudo.Descr_Anexo3;

                zdS.Tables[0].Rows.Add(newRow);
            }

            if (zLaudo.Anexo4.Trim() != "")
            {
                newRow = zdS.Tables[0].NewRow();

                newRow["Arquivo"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zLaudo.Anexo4);
                newRow["Descricao"] = zLaudo.Descr_Anexo4;

                zdS.Tables[0].Rows.Add(newRow);
            }


            return zdS;

        }


        private DataSet GetDataSource_LaudoSPDA(Int32 rIdLaudo)
        {
            DataSet zdS = new DataSet();
            DataTable table = GetTable();
            DataRow newRow;


            zdS.Tables.Add(table);


            //table.Columns.Add("Arquivo", Type.GetType("System.String"));


            LaudoSPDA zLaudo = new LaudoSPDA();
            zLaudo.Find(rIdLaudo);

            if (zLaudo.Anexo1.Trim() != "")
            {
                newRow = zdS.Tables[0].NewRow();

                newRow["Arquivo"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zLaudo.Anexo1);                
                newRow["Descricao"] = zLaudo.Descr_Anexo1;

                zdS.Tables[0].Rows.Add(newRow);
            }

            if (zLaudo.Anexo2.Trim() != "")
            {
                newRow = zdS.Tables[0].NewRow();
                                
                newRow["Arquivo"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zLaudo.Anexo2);
                newRow["Descricao"] = zLaudo.Descr_Anexo2;

                zdS.Tables[0].Rows.Add(newRow);
            }

            if (zLaudo.Anexo3.Trim() != "")
            {
                newRow = zdS.Tables[0].NewRow();

                newRow["Arquivo"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zLaudo.Anexo3);                
                newRow["Descricao"] = zLaudo.Descr_Anexo3;

                zdS.Tables[0].Rows.Add(newRow);
            }

            if (zLaudo.Anexo4.Trim() != "")
            {
                newRow = zdS.Tables[0].NewRow();

                newRow["Arquivo"] = Ilitera.Common.Fotos.GetByteFoto_Uri("I:\\fotosdocsdigitais\\" + cliente.DiretorioPadrao + "\\vasopressao\\" + zLaudo.Anexo4);                
                newRow["Descricao"] = zLaudo.Descr_Anexo4;

                zdS.Tables[0].Rows.Add(newRow);
            }




            return zdS;


        }



        private DataSet GetDataSourceTexto_Erg(Int32 zIdLaudo)
        {
            DataSet zdS = new DataSet();
            DataTable table = GetTable();
            DataRow newRow;


            zdS.Tables.Add(table);



            LaudoErgonomicoDocumento zAnexo = new LaudoErgonomicoDocumento();
            zAnexo.Find(" nId_Laud_Tec = " + zIdLaudo.ToString());

            if (zAnexo.Texto.Trim() != "" && zAnexo.Titulo.Trim() != "")
            {
                newRow = zdS.Tables[0].NewRow();

                newRow["Arquivo"] = zAnexo.Texto;
                newRow["Descricao"] = zAnexo.Titulo;

                zdS.Tables[0].Rows.Add(newRow);
            }


            return zdS;


        }






        private DataSet GetDataSource()
        {

            //string xLoc;
            //string xLoc2;

            DataSet zdS = new DataSet();
            DataTable table = GetTable();
            DataRow newRow;


            zdS.Tables.Add(table);
            

            //table.Columns.Add("Arquivo", Type.GetType("System.String"));


            //carregar lista de anexos
            Ilitera.Data.PPRA_EPI xAnexos = new Ilitera.Data.PPRA_EPI();
            DataSet xdS = xAnexos.Retornar_Anexos_PPRA(this.laudoTecnico.Id, "-PDF");

            foreach (DataRow row in xdS.Tables[0].Rows)
            {
                newRow = zdS.Tables[0].NewRow();

                //xLoc = row["Arquivo"].ToString().Replace("\\", "/");
                //xLoc2 = xLoc.Replace("I:", "http://187.45.232.35/driveI");


                newRow["Arquivo"] = Ilitera.Common.Fotos.GetByteFoto_Uri(row["Arquivo"].ToString());

                //newRow["Arquivo"] = row["Arquivo"].ToString();
                newRow["Descricao"] = row["Descricao"].ToString();

                zdS.Tables[0].Rows.Add(newRow);
            

            }

            return zdS;


        }

        private static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            //table.Columns.Add("Arquivo", Type.GetType("System.String"));
            table.Columns.Add("Arquivo", Type.GetType("System.Byte[]"));
            table.Columns.Add("Descricao", Type.GetType("System.String"));
            return table;
        }


        private DataSet GetDataSource2()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("CarimboCNPJ", Type.GetType("System.String"));
            table.Columns.Add("PlanejamentoAnual", Type.GetType("System.String"));
            table.Columns.Add("Jan", Type.GetType("System.String"));
            table.Columns.Add("Fev", Type.GetType("System.String"));
            table.Columns.Add("Mar", Type.GetType("System.String"));
            table.Columns.Add("Abr", Type.GetType("System.String"));
            table.Columns.Add("Mai", Type.GetType("System.String"));
            table.Columns.Add("Jun", Type.GetType("System.String"));
            table.Columns.Add("Jul", Type.GetType("System.String"));
            table.Columns.Add("Ago", Type.GetType("System.String"));
            table.Columns.Add("Set", Type.GetType("System.String"));
            table.Columns.Add("Out", Type.GetType("System.String"));
            table.Columns.Add("Nov", Type.GetType("System.String"));
            table.Columns.Add("Dez", Type.GetType("System.String"));
            table.Columns.Add("Ano", Type.GetType("System.String"));
            table.Columns.Add("EstrategiaMetAcao", Type.GetType("System.String"));
            table.Columns.Add("FormaRegistro", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);

            DataRow newRow;

            ArrayList list = CronogramaPPRA.GetCronograma(laudoTecnico);

            foreach (CronogramaPPRA cronogramaPPRA in list)
            {
                if (cronogramaPPRA.PlanejamentoAnual.Equals(string.Empty))
                    continue;

                newRow = ds.Tables["Result"].NewRow();

                newRow["CarimboCNPJ"] = cliente.GetCarimboCnpjHtml(laudoTecnico.hDT_LAUDO);

                ds.Tables["Result"].Rows.Add(newRow);
                break;
            }

            return ds;
        }


        private DataSet GetDataSource_Erg()
        {
            DataSet zdS = new DataSet();
            DataTable table = GetTable();
            DataRow newRow;

            //carregar lista de anexos
            Ilitera.Data.PPRA_EPI xAnexos = new Ilitera.Data.PPRA_EPI();
            DataSet xdS = xAnexos.Retornar_Anexos_Erg(this.laudoTecnico.Id, "-PDF");

            zdS.Tables.Add(table);

            foreach (DataRow row in xdS.Tables[0].Rows)
            {
                newRow = zdS.Tables[0].NewRow();

                newRow["Arquivo"]  = Ilitera.Common.Fotos.GetByteFoto_Uri(row["Arquivo"].ToString());
                newRow["Descricao"] = row["Descricao"].ToString();

                zdS.Tables[0].Rows.Add(newRow);


            }

            return zdS;


        }



    }



}
