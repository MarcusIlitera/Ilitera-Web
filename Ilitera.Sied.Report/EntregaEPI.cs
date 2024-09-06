using System;
using System.Data;
using System.Collections;
using System.Text;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Sied.Report
{	
	public class ListaEntregaEPI : DataSourceBase
	{
		private Empregado empregado;
		private Cliente cliente;
		private EmpregadoFuncao empregadoFuncao;
		private ArrayList alEPIEntregue;
		private EPICAEntrega epiCAEntrega;

		public ListaEntregaEPI(Empregado empregado, int IdEPICAEntrega)
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
			this.empregado = empregado;
			cliente = new Cliente();
			cliente.Find(empregado.nID_EMPR.Id);
			empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
			empregadoFuncao.nID_SETOR.Find();
			empregadoFuncao.nID_FUNCAO.Find();
			
			alEPIEntregue = new EPICAEntregaDetalhe().Find("IdEPICAEntrega=" + IdEPICAEntrega);
			epiCAEntrega = new EPICAEntrega(IdEPICAEntrega);
		}

        public ListaEntregaEPI( int xIdEmpresa, string xD1, string xD2)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");

            cliente = new Cliente();
            cliente.Find(xIdEmpresa);
            
        }

		public ReportEntregaEPI GetReportEntregaEPI()
		{
			
            ReportEntregaEPI report = new ReportEntregaEPI();
            
        
			report.OpenSubreport("EPI").SetDataSource(ListaEPI());


			report.SetDataSource(DataSourceEntregaEPI());
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}


        public RptReciboEPI2 GetReportEntregaEPI( int xIdEmpresa, string xD1, string xD2, int zIdEPICAEntrega, int zIdEmpregado)
        {

            RptReciboEPI2 report = new RptReciboEPI2();
            

            //report.OpenSubreport("EPI").SetDataSource(ListaEPI());


            report.SetDataSource(DataSourceEntregaEPI_Completo(xIdEmpresa, xD1, xD2, zIdEPICAEntrega, zIdEmpregado));
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }


        public RptReciboEPI2_Biometria GetReportEntregaEPI_Biometria(int xIdEmpresa, string xD1, string xD2, int zIdEPICAEntrega, int zIdEmpregado)
        {

            RptReciboEPI2_Biometria report = new RptReciboEPI2_Biometria();


            //report.OpenSubreport("EPI").SetDataSource(ListaEPI());


            report.SetDataSource(DataSourceEntregaEPI_Completo(xIdEmpresa, xD1, xD2, zIdEPICAEntrega, zIdEmpregado));
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }



        public RptReciboMateriais GetReportEntregaMateriais(string zMaterial)
        {

            RptReciboMateriais report = new RptReciboMateriais();


            //report.OpenSubreport("EPI").SetDataSource(ListaEPI());


            report.SetDataSource(DataSourceEntregaMateriais(zMaterial));
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

        
        private DataSet DataSourceEntregaMateriais(string rMaterial)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("RAZAO_SOCIAL", Type.GetType("System.String"));
            table.Columns.Add("NOME_EMPREGADO", Type.GetType("System.String"));
            table.Columns.Add("SEXO", Type.GetType("System.String"));
            table.Columns.Add("DATA_NASCIMENTO", Type.GetType("System.String"));
            table.Columns.Add("IDADE", Type.GetType("System.String"));
            table.Columns.Add("RG", Type.GetType("System.String"));
            table.Columns.Add("FUNCAO", Type.GetType("System.String"));
            table.Columns.Add("SETOR", Type.GetType("System.String"));
            table.Columns.Add("DATA_ENTREGA", Type.GetType("System.String"));
            table.Columns.Add("iFOTO", Type.GetType("System.Byte[]"));
            table.Columns.Add("ComFoto", Type.GetType("System.String"));
            table.Columns.Add("ComFotoLogo", Type.GetType("System.String"));
            table.Columns.Add("DATA_ADMISSAO", Type.GetType("System.String"));
            table.Columns.Add("iFOTOLogo", Type.GetType("System.Byte[]"));
            table.Columns.Add("RE", Type.GetType("System.String"));
            table.Columns.Add("NOME_EPI", Type.GetType("System.String"));
            table.Columns.Add("DATA_FORNECIMENTO", Type.GetType("System.String"));
            table.Columns.Add("QTD_ENTREGUE", Type.GetType("System.String"));
            table.Columns.Add("TOTAL", Type.GetType("System.String"));

            int zTotal = 0;

            ds.Tables.Add(table);
            string[] stringSeparators5 = new string[] { ";" };
            string[] result5;

            result5 = rMaterial.Split(stringSeparators5, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result5)
            {

                DataRow newRow;
                newRow = ds.Tables[0].NewRow();

                cliente.IdJuridicaPai.Find();

                if (cliente.IdJuridicaPai.Id != 0 && cliente.IdJuridicaPai.Id != cliente.Id)
                {
                    newRow["RAZAO_SOCIAL"] = cliente.IdJuridicaPai.ToString() + " : " + cliente.NomeAbreviado.ToString();
                }
                else
                {
                    newRow["RAZAO_SOCIAL"] = cliente.GetNomeEmpresa();
                }


                //newRow["RAZAO_SOCIAL"] = cliente.NomeCompleto;
                newRow["NOME_EMPREGADO"] = empregado.tNO_EMPG;
                newRow["DATA_NASCIMENTO"] = Ilitera.Common.Utility.TratarData(empregado.hDT_NASC);
                newRow["SEXO"] = empregado.tSEXO;
                newRow["IDADE"] = empregado.IdadeEmpregado().ToString();
                newRow["RG"] = empregado.tNO_IDENTIDADE;
                newRow["DATA_ADMISSAO"] = Ilitera.Common.Utility.TratarData(empregado.hDT_ADM);
                newRow["FUNCAO"] = empregadoFuncao.nID_FUNCAO.NomeFuncao;
                newRow["SETOR"] = empregadoFuncao.nID_SETOR.tNO_STR_EMPR;
                newRow["DATA_ENTREGA"] = Ilitera.Common.Utility.TratarData(epiCAEntrega.DataRecebimento);
                newRow["RE"] = empregado.tCOD_EMPR;


                newRow["DATA_FORNECIMENTO"] = s.Substring(0, 10);
                //newRow["NOME_EPI"] = s.Substring(17);
                newRow["QTD_ENTREGUE"] = s.Substring(12, 4);
                //newRow["TOTAL"] = s.Substring(12, 4);

                zTotal = zTotal + System.Convert.ToInt16(s.Substring(12, 4));
                newRow["TOTAL"] = zTotal.ToString().Trim();

                Ilitera.Data.PPRA_EPI xUnif = new Ilitera.Data.PPRA_EPI();

                string zUniforme = xUnif.Retornar_Uniforme(System.Convert.ToInt32(s.Substring(16)));

                newRow["NOME_EPI"] = zUniforme;
                


                
                if (cliente.Logotipo != "")
                {

                                        
                    newRow["iFotoLogo"] = GetByteFoto_Uri_Material(cliente.Logotipo.Trim());
                    newRow["ComFotoLogo"] = "true";
                }
                else
                {
                    newRow["ComFotoLogo"] = "false";
                }


                ds.Tables[0].Rows.Add(newRow);

            }




            return ds;
        }



        //private static byte[] GetByteFoto_Uri(string sURIFoto)
        //{
        //    string xLoc = "";
        //    string xLoc2 = "";
        //    System.Net.WebClient webclient = new  System.Net.WebClient();
        //    byte[] bytesArray = null;

        //    xLoc = sURIFoto.Replace("\\", "/");
        //    //xLoc2 = xLoc.Replace("I:", "http://54.94.157.244/driveI");
        //    xLoc2 = xLoc.Replace("I:", "https://www.ilitera.net.br/driveI");
        //    xLoc2 = xLoc2.Replace("/", "\\");

        //    //xLoc2 = xLoc.Replace("I:", "http://ilitera.dyndns.ws:8888/driveI");


        //    string xArq = "";


        //    xArq = xLoc2.Replace("http://www.ilitera.net.br/driveI", "I:");
        //    xArq = xArq.Replace("/", "\\");


        //    //bytesArray = webclient.DownloadData(xLoc2);
        //    bytesArray = webclient.DownloadData(xArq);


        //    return bytesArray;

        //}



        private static byte[] GetByteFoto_Uri_Material(string sURIFoto)
        {
            string xLoc = "";
            string xLoc2 = "";
            System.Net.WebClient webclient = new System.Net.WebClient();
            byte[] bytesArray = null;

            xLoc = sURIFoto.Replace("\\", "/");
            //xLoc2 = xLoc.Replace("I:", "http://54.94.157.244/driveI");
            //xLoc2 = xLoc.Replace("I:", "http://www.ilitera.net.br/driveI");
            xLoc2 = xLoc.Replace("http://www.ilitera.net.br/driveI", "I:");

            
            xLoc2 = xLoc2.Replace("/", "\\");

            //xLoc2 = xLoc.Replace("I:", "http://ilitera.dyndns.ws:8888/driveI");


            bytesArray = webclient.DownloadData(xLoc2);

            

            return bytesArray;

        }


        private DataSet DataSourceEntregaEPI()
		{	
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("RAZAO_SOCIAL", Type.GetType("System.String"));
			table.Columns.Add("NOME_EMPREGADO", Type.GetType("System.String"));
			table.Columns.Add("SEXO", Type.GetType("System.String"));
			table.Columns.Add("DATA_NASCIMENTO", Type.GetType("System.String"));
			table.Columns.Add("IDADE", Type.GetType("System.String"));
			table.Columns.Add("RG", Type.GetType("System.String"));
			table.Columns.Add("FUNCAO", Type.GetType("System.String"));
			table.Columns.Add("SETOR", Type.GetType("System.String"));
			table.Columns.Add("DATA_ENTREGA", Type.GetType("System.String"));
			table.Columns.Add("iFOTO", Type.GetType("System.Byte[]"));
			table.Columns.Add("ComFoto", Type.GetType("System.Boolean"));
			ds.Tables.Add(table);
			DataRow newRow;
			newRow = ds.Tables[0].NewRow();

            cliente.IdJuridicaPai.Find();

            if (cliente.IdJuridicaPai.Id != 0 && cliente.IdJuridicaPai.Id != cliente.Id)
            {
                newRow["RAZAO_SOCIAL"] = cliente.IdJuridicaPai.ToString() + " : " + cliente.NomeAbreviado.ToString();
            }
            else
            {
                newRow["RAZAO_SOCIAL"] = cliente.GetNomeEmpresa();
            }
            //newRow["RAZAO_SOCIAL"]		= cliente.NomeCompleto;
            newRow["NOME_EMPREGADO"]	= empregado.tNO_EMPG;
			newRow["DATA_NASCIMENTO"]	= Ilitera.Common.Utility.TratarData(empregado.hDT_NASC);
			newRow["SEXO"]				= empregado.tSEXO;
			newRow["IDADE"]				= empregado.IdadeEmpregado().ToString();
			newRow["RG"]				= empregado.tNO_IDENTIDADE;
			newRow["FUNCAO"]			= empregadoFuncao.nID_FUNCAO.NomeFuncao;
			newRow["SETOR"]				= empregadoFuncao.nID_SETOR.tNO_STR_EMPR;
			newRow["DATA_ENTREGA"]		= Ilitera.Common.Utility.TratarData(epiCAEntrega.DataRecebimento);
            
            try
            {
                string pathFoto = empregado.FotoEmpregado();

                if (pathFoto != string.Empty && System.IO.File.Exists(pathFoto))
                {
                    newRow["iFOTO"] = Ilitera.Common.Fotos.GetByteFoto_Uri(pathFoto);
                    newRow["ComFoto"] = true;
                }
                else
                    newRow["ComFoto"] = false;
            }
            catch (Exception ex)
            {
                newRow["ComFoto"] = false;
                System.Diagnostics.Trace.WriteLine(ex.Message.ToString());
            }
		
			ds.Tables[0].Rows.Add(newRow);
			return ds;
		}

		private DataSet ListaEPI()
		{
			DataSet ds = new DataSet();
			DataSet dsOrdered = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("NOME_EPI", Type.GetType("System.String"));
			table.Columns.Add("CA", Type.GetType("System.String"));
			table.Columns.Add("QTD_ENTREGUE", Type.GetType("System.String"));
			table.Columns.Add("PERIODICIDADE", Type.GetType("System.String"));
			DataTable tableordered = new DataTable("Result");
			tableordered.Columns.Add("NOME_EPI", Type.GetType("System.String"));
			tableordered.Columns.Add("CA", Type.GetType("System.String"));
			tableordered.Columns.Add("QTD_ENTREGUE", Type.GetType("System.String"));
			tableordered.Columns.Add("PERIODICIDADE", Type.GetType("System.String"));
			ds.Tables.Add(table);
			dsOrdered.Tables.Add(tableordered);

			DataRow newRow, newRowOrdered;

			foreach (EPICAEntregaDetalhe epientregue in alEPIEntregue)
			{
				epientregue.IdEPIClienteCA.Find();
				epientregue.IdEPIClienteCA.IdEPI.Find();
				epientregue.IdEPIClienteCA.IdCA.Find();

				string periodicidade = string.Empty;
				
				switch (epientregue.IdEPIClienteCA.Periodicidade)
				{
					case 0:
						if (epientregue.IdEPIClienteCA.NumPeriodicidade == 1)
							periodicidade = "dia";
						else
							periodicidade = "dias";
						break;
					case 1:
						if (epientregue.IdEPIClienteCA.NumPeriodicidade == 1)
							periodicidade = "mês";
						else
							periodicidade = "meses";
						break;
					case 2:
						if (epientregue.IdEPIClienteCA.NumPeriodicidade == 1)
							periodicidade = "ano";
						else
							periodicidade = "anos";
						break;
				}
				
				newRow = ds.Tables[0].NewRow();

				newRow["NOME_EPI"] = epientregue.IdEPIClienteCA.IdEPI.ToString();
				newRow["CA"] = epientregue.IdEPIClienteCA.IdCA.NumeroCA.ToString("00000");
				newRow["QTD_ENTREGUE"] = epientregue.QtdEntregue.ToString();
				newRow["PERIODICIDADE"] = epientregue.IdEPIClienteCA.NumPeriodicidade.ToString() + " " + periodicidade;

				ds.Tables[0].Rows.Add(newRow);
			}
			
			DataRow[] rows = ds.Tables[0].Select("", "NOME_EPI");

			foreach (DataRow row in rows)
			{
				newRowOrdered = dsOrdered.Tables[0].NewRow();
				
				newRowOrdered["NOME_EPI"] = row["NOME_EPI"].ToString();
				newRowOrdered["CA"] = row["CA"].ToString();
				newRowOrdered["QTD_ENTREGUE"] = row["QTD_ENTREGUE"].ToString();
				newRowOrdered["PERIODICIDADE"] = row["PERIODICIDADE"].ToString();
				
				dsOrdered.Tables[0].Rows.Add(newRowOrdered);
			}

			return dsOrdered;
		}








        private DataSet DataSourceEntregaEPI_Completo( int xIdEmpresa, string xD1, string xD2, int zIdEPICAEntrega, int zEmpr)
        {
            //juntar parte de Lista EPI, tudo em um Result apenas,  com todos os empregados, entregas vindos de um Select

            DataSet ds = new DataSet();



            //DataTable table = new DataTable("Result");
            //table.Columns.Add("RAZAO_SOCIAL", Type.GetType("System.String"));
            //table.Columns.Add("NOME_EMPREGADO", Type.GetType("System.String"));
            //table.Columns.Add("SEXO", Type.GetType("System.String"));
            //table.Columns.Add("DATA_NASCIMENTO", Type.GetType("System.String"));
            //table.Columns.Add("DATA_ADMISSAO", Type.GetType("System.String"));
            //table.Columns.Add("IDADE", Type.GetType("System.String"));
            //table.Columns.Add("RG", Type.GetType("System.String"));
            //table.Columns.Add("RE", Type.GetType("System.String"));
            //table.Columns.Add("FUNCAO", Type.GetType("System.String"));
            //table.Columns.Add("SETOR", Type.GetType("System.String"));
            //table.Columns.Add("DATA_ENTREGA", Type.GetType("System.String"));
            //table.Columns.Add("iFOTO", Type.GetType("System.Byte[]"));
            //table.Columns.Add("ComFoto", Type.GetType("System.Boolean"));

            //table.Columns.Add("NOME_EPI", Type.GetType("System.String"));
            //table.Columns.Add("CA", Type.GetType("System.String"));
            //table.Columns.Add("QTD_ENTREGUE", Type.GetType("System.String"));
            //table.Columns.Add("DATA_FORNECIMENTO", Type.GetType("System.String"));
            
            //ds.Tables.Add(table);
            //DataRow newRow;
            //newRow = ds.Tables[0].NewRow();

            //CHAMAR MÉTODO PARA CARGA DESTES DADOS

                //use opsa
                //select *
                //from epicaentrega as a left join epicaentregadetalhe as b
                //on a.IdEPICAEntrega = b.IdEPICAEntrega
                //where a.IdEmpregado in
                //(
                //   select distinct e.nID_Empregado
                //   from sied_novo.dbo.tblEmpregado e 
                //   left join sied_novo.dbo.tblEmpregado_Funcao ef on e.nID_Empregado = ef.nID_Empregado 
                //   where e.nID_EMPR = 1656616546
                //   and ( ef.nID_EMPR = 1656616546  or ef.nID_EMPR is null )
                //)
                //and DataRecebimento between convert( smalldatetime, '01/02/2012', 103 ) and convert( smalldatetime, '18/04/2012', 103 )



            //newRow["RAZAO_SOCIAL"] = cliente.NomeCompleto;
            //newRow["NOME_EMPREGADO"] = empregado.tNO_EMPG;
            //newRow["DATA_NASCIMENTO"] = Ilitera.Common.Utility.TratarData(empregado.hDT_NASC);
            //newRow["SEXO"] = empregado.tSEXO;
            //newRow["IDADE"] = empregado.IdadeEmpregado().ToString();
            //newRow["RG"] = empregado.tNO_IDENTIDADE;
            //newRow["FUNCAO"] = empregadoFuncao.nID_FUNCAO.NomeFuncao;
            //newRow["SETOR"] = empregadoFuncao.nID_SETOR.tNO_STR_EMPR;
            //newRow["DATA_ENTREGA"] = Ilitera.Common.Utility.TratarData(epiCAEntrega.DataRecebimento);

            //try
            //{
            //    string pathFoto = empregado.FotoEmpregado();

            //    if (pathFoto != string.Empty && System.IO.File.Exists(pathFoto))
            //    {
            //        newRow["iFOTO"] = Ilitera.Common.Fotos.GetByteFoto_Uri(pathFoto);
            //        newRow["ComFoto"] = true;
            //    }
            //    else
            //        newRow["ComFoto"] = false;
            //}
            //catch (Exception ex)
            //{
            //    newRow["ComFoto"] = false;
            //    System.Diagnostics.Trace.WriteLine(ex.Message.ToString());
            //}

            //ds.Tables[0].Rows.Add(newRow);

            cliente = new Cliente();
            cliente.Find(xIdEmpresa);
            
            
            Ilitera.Data.PPRA_EPI xEPI = new Ilitera.Data.PPRA_EPI();

            byte[] data;
            //System.Drawing.Bitmap bmp = cliente.LogoEmpresa();

            //if (bmp != null)
            //{
            //    System.IO.MemoryStream stream = new System.IO.MemoryStream();
            //    bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            //    //bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            //    stream.Position = 0;
            //    data = new byte[stream.Length];
            //    stream.Read(data, 0, System.Convert.ToInt32(stream.Length));
            //}
            //else
            //{
                data = null;
            //}

            string xEmpresa = "";

            cliente.IdJuridicaPai.Find();

            if (cliente.IdJuridicaPai.Id != 0 && cliente.IdJuridicaPai.Id != cliente.Id)
            {
                xEmpresa = cliente.IdJuridicaPai.ToString() + " : " + cliente.NomeAbreviado.ToString();
            }
            else
            {
                xEmpresa = cliente.GetNomeEmpresa();
            }

            ds = xEPI.Recibo_EPIs(xIdEmpresa, xD1, xD2, xEmpresa, cliente.ArqFotoEmpregInicio, cliente.ArqFotoEmpregTermino, cliente.ArqFotoEmrpegQteDigitos, cliente.ArqFotoEmpregExtensao, cliente.GetFotoDiretorioPadrao(), data, zIdEPICAEntrega, zEmpr, cliente.Logotipo.ToString().Trim());
            
            return ds;
        }

	}
}
