using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Drawing;

using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Auditoria.Report
{
	public class DataSourceAuditoria : DataSourceBase
	{
		private Ilitera.Opsa.Data.Auditoria auditoria;
		private Cliente cliente;
		private ArrayList list;
		private bool onlyHeader;

		public DataSourceAuditoria(Cliente cliente)
		{			
			this.cliente = cliente;
			this.cliente.Find();
			
			ArrayList list = new Ilitera.Opsa.Data.Auditoria().FindMax("DataLevantamento", "IdCliente=" + cliente.Id);
			
			if(list.Count == 0)
				throw new Exception("Esta empresa não possui Auditoria!");
			else
				auditoria = (Ilitera.Opsa.Data.Auditoria)list[0];

			list = new Irregularidade().FindIrregularidades(auditoria.Id.ToString());
		}

		public DataSourceAuditoria(Cliente cliente, int IdAuditoria) : this(cliente, IdAuditoria, false)
		{

		}

		public DataSourceAuditoria(Cliente cliente,  int IdAuditoria, bool onlyHeader)
		{
			this.cliente = cliente;
			this.cliente.Find();
			this.onlyHeader = onlyHeader;
			
			auditoria = new Ilitera.Opsa.Data.Auditoria(IdAuditoria);

			list = new Irregularidade().FindIrregularidades(auditoria.Id.ToString());
		}

		public DataSourceAuditoria(Cliente cliente, int IdAuditoria, int IdIrregularidade)
		{			
			this.cliente = cliente;
			this.cliente.Find();
			
			auditoria = new Ilitera.Opsa.Data.Auditoria(IdAuditoria);

			list = new Irregularidade().Find("IdAuditoria=" + auditoria.Id
				+" AND IdIrregularidade=" + IdIrregularidade);
		}

		public RptAuditoria2 GetReport()
		{
			DataSet ds = new DataSet();
			SetDetailAuditoria(ds);
			RptAuditoria2 report = new RptAuditoria2();
			report.SetDataSource(ds);
			report.Refresh();

            SetTempoProcessamento(report);
            
            return report;
		}

		public RptAuditoriaComCaracterizacao GetReportComCaracterizacao()
		{
			DataSet ds = new DataSet();
			SetDetailAuditoriaComCaracterizacao(ds);
			RptAuditoriaComCaracterizacao report = new RptAuditoriaComCaracterizacao();
			report.SetDataSource(ds);
			report.Refresh();

            SetTempoProcessamento(report);

			return report;
		}

		private void SetDetailAuditoria(DataSet ds)
		{
            DataTable table = GetTableAuditoria();
			ds.Tables.Add(table);
			DataRow newRow;

			Prestador prestador = new Prestador();
            prestador.Find(auditoria.IdPrestador.Id);	

			Endereco endereco = cliente.GetEndereco();
	
			if (onlyHeader)
			{
				newRow = ds.Tables["Result"].NewRow();
				newRow["HasHeader"]				= true;
				newRow["HasDetails"]			= false;
				newRow["IdCliente"]				= cliente.Id;
				try
				{
					newRow["FotoFachada"] = Ilitera.Common.Fotos.PathFoto_Uri(cliente.GetFotoFachada());
				}
				catch (Exception ex)
				{
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    //System.Diagnostics.Trace.WriteLine(ex.Message);
				}
				newRow["NomeCliente"]			= cliente.NomeCompleto;
				newRow["LogradouroNumero"]		= endereco.GetEndereco();
				newRow["BairroCidadeEstadoCEP"] = endereco.Bairro + "  " + endereco.GetCidade() + "  " +endereco.GetEstado()  + "  " +endereco.Cep;			
				newRow["CNPJ"]					= "CNPJ: " + cliente.NomeCodigo;	
				newRow["DataAssinatura"]		= cliente.GetEndereco().GetCidade() + ", "+ auditoria.DataLevantamento.ToString("d \"de\" MMMM \"de\" yyyy")+"."; 
				newRow["NomeAuditor"]			= prestador.NomeCompleto;	
				newRow["TituloAuditor"]			= prestador.Titulo;	
				newRow["AssinaturaAuditor"]		= Fotos.PathFoto_Uri(prestador.FotoAss);
				ds.Tables["Result"].Rows.Add(newRow);
			}
			else
				foreach(Irregularidade irr in list)
				{
					irr.IdNorma.Find();
					irr.IdNorma.IdNr.Find();

					newRow = ds.Tables["Result"].NewRow();
					newRow["IdCliente"]  = cliente.Id;
					newRow["HasHeader"]	 = (list.Count != 1);
					newRow["HasDetails"] = true;
					//header
					try
					{
						newRow["FotoFachada"] = Ilitera.Common.Fotos.PathFoto_Uri(cliente.GetFotoFachada());
					}
					catch (Exception ex)
					{
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        //System.Diagnostics.Trace.WriteLine(ex.Message);
					}
					newRow["NomeCliente"]			= cliente.NomeCompleto;
					newRow["LogradouroNumero"]		= endereco.GetEndereco();
					newRow["BairroCidadeEstadoCEP"] = endereco.Bairro + "  " + endereco.GetCidade() + "  " +endereco.GetEstado()  + "  " +endereco.Cep;			
					newRow["CNPJ"]					= "CNPJ: " + cliente.NomeCodigo;	
					newRow["DataAssinatura"]		= cliente.GetEndereco().GetCidade() + ", "+ auditoria.DataLevantamento.ToString("d \"de\" MMMM \"de\" yyyy")+"."; 
					newRow["NomeAuditor"]			= prestador.NomeCompleto;	
					newRow["TituloAuditor"]			= prestador.Titulo;	
					newRow["AssinaturaAuditor"]		= Fotos.PathFoto_Uri(prestador.FotoAss);

                    IrregularidadeFotoLocal fotoIrr = new IrregularidadeFotoLocal();
                    fotoIrr.Find("IdIrregularidade=" + irr.Id + " AND IsFotoPadrao=1");
					
                    //detail
                    if (fotoIrr.NumeroFoto != string.Empty)
                        newRow["NumeroFoto"] = Convert.ToInt32(fotoIrr.NumeroFoto);
                    try
                    {
                        //if (Ilitera.Data.Table.IsWeb)
                        //string xArquivo = Ilitera.Common.Fotos.PathSmallFoto(auditoria, Convert.ToInt32(fotoIrr.NumeroFoto));
                        string xArquivo = Ilitera.Common.Fotos.PathNormalFoto(auditoria, Convert.ToInt32(fotoIrr.NumeroFoto));

                        if (xArquivo.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
                        {
                            xArquivo = xArquivo.Substring(0, xArquivo.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + xArquivo.Substring(xArquivo.IndexOf("\\") + 1);
                        }

                        newRow["Foto"] = Ilitera.Common.Fotos.PathFoto_Uri(xArquivo);
                        //else
                        //    newRow["Foto"] = Ilitera.Common.Fotos.PathFoto(auditoria, Convert.ToInt32(fotoIrr.NumeroFoto));
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
					newRow["NomeEmpresa"] = cliente.NomeCompleto;
					newRow["NumeroEmpregado"] = cliente.QtdEmpregados.ToString();
					newRow["Data"] = auditoria.DataLevantamento.ToString("dd-MM-yyyy");
					newRow["LocalTrabalho"] = irr.strLocalIrregularidade();
                    newRow["NR"] = "NR " + irr.IdNorma.IdNr.ToString();

                    // procurar /r/n e substituir por chr(13)

                    string Regex = irr.IdNorma.GetEnquadramentoLegal();


                    string myNewTest = Regex.Replace("\r\n", " ");

                    myNewTest = myNewTest.Replace("a)", "\r\na)");
                    myNewTest = myNewTest.Replace("b)", "\r\nb)");
                    myNewTest = myNewTest.Replace("c)", "\r\nc)");
                    myNewTest = myNewTest.Replace("d)", "\r\nd)");
                    myNewTest = myNewTest.Replace("e)", "\r\ne)");
                    myNewTest = myNewTest.Replace("f)", "\r\nf)");


                    newRow["NormaLegal"] = myNewTest;  //irr.IdNorma.GetEnquadramentoLegal();

                    //newRow["NormaLegal"] = irr.IdNorma.GetEnquadramentoLegal();
					
					Infracao infracao = new Infracao(irr.GetIdInfracaoValorMulta(cliente.QtdEmpregados, irr.IdNorma.CodigoInfracao));
					
					newRow["ValorMultaUMin"] = infracao.ValorMin.ToString("n");
					newRow["ValorMultaUMax"] = infracao.ValorMax.ToString("n");
                    newRow["ValorMultaRMin"] = ((float)(Convert.ToSingle(infracao.ValorMin) * ValoresUfir.GetValorUfir())).ToString("n");
                    newRow["ValorMultaRMax"] = ((float)(Convert.ToSingle(infracao.ValorMax) * ValoresUfir.GetValorUfir())).ToString("n");

					newRow["DescricaoIrregularidade"] = irr.strAcoesExecutar();
					ds.Tables["Result"].Rows.Add(newRow);
				}
		}

        private static DataTable GetTableAuditoria()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdCliente", Type.GetType("System.Int32"));
            table.Columns.Add("NomeCliente", Type.GetType("System.String"));
            table.Columns.Add("LogradouroNumero", Type.GetType("System.String"));
            table.Columns.Add("BairroCidadeEstadoCEP", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("DataAssinatura", Type.GetType("System.String"));
            table.Columns.Add("NomeAuditor", Type.GetType("System.String"));
            table.Columns.Add("TituloAuditor", Type.GetType("System.String"));
            table.Columns.Add("AssinaturaAuditor", Type.GetType("System.String"));
            table.Columns.Add("HasHeader", Type.GetType("System.Boolean"));
            table.Columns.Add("NumeroFoto", Type.GetType("System.Int32"));
            table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
            table.Columns.Add("NumeroEmpregado", Type.GetType("System.String"));
            table.Columns.Add("Data", Type.GetType("System.String"));
            table.Columns.Add("LocalTrabalho", Type.GetType("System.String"));
            table.Columns.Add("NR", Type.GetType("System.String"));
            table.Columns.Add("NormaLegal", Type.GetType("System.String"));
            table.Columns.Add("ParecerTecnicoJuridico", Type.GetType("System.String"));
            table.Columns.Add("ValorMultaCodigo", Type.GetType("System.Int32"));
            table.Columns.Add("HasDetails", Type.GetType("System.Boolean"));
            table.Columns.Add("Foto", Type.GetType("System.String"));
            table.Columns.Add("FotoFachada", Type.GetType("System.String"));
            table.Columns.Add("DescricaoIrregularidade", Type.GetType("System.String"));
            table.Columns.Add("ValorMultaUMax", Type.GetType("System.String"));
            table.Columns.Add("ValorMultaUMin", Type.GetType("System.String"));
            table.Columns.Add("ValorMultaRMax", Type.GetType("System.String"));
            table.Columns.Add("ValorMultaRMin", Type.GetType("System.String"));
            return table;
        }

		private void SetDetailAuditoriaComCaracterizacao(DataSet ds)
		{
            DataTable table = GetTableAuditoriaComCaracterizacao();
			ds.Tables.Add(table);
			DataRow newRow;
			int numeroEmpregado = cliente.QtdEmpregados;	

			Prestador prestador = new Prestador();
            prestador.Find(auditoria.IdPrestador.Id);	
			Endereco endereco = cliente.GetEndereco();
	
			if (onlyHeader)
			{
				newRow = ds.Tables["Result"].NewRow();
				newRow["HasHeader"]				= true;
				newRow["HasDetails"]			= false;
				newRow["IdCliente"]				= cliente.Id;
				try
				{
					newRow["FotoFachada"] = Ilitera.Common.Fotos.PathFoto_Uri(cliente.GetFotoFachada());
				}
				catch (Exception ex)
				{
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    //System.Diagnostics.Trace.WriteLine(ex.Message);
				}
				newRow["NomeCliente"]			= cliente.NomeCompleto;
				newRow["LogradouroNumero"]		= endereco.GetEndereco();
				newRow["BairroCidadeEstadoCEP"] = endereco.Bairro + "  " + endereco.GetCidade() + "  " +endereco.GetEstado()  + "  " +endereco.Cep;			
				newRow["CNPJ"]					= "CNPJ: " + cliente.NomeCodigo;	
				newRow["DataAssinatura"]		= cliente.GetEndereco().GetCidade() + ", "+ auditoria.DataLevantamento.ToString("d \"de\" MMMM \"de\" yyyy")+"."; 
				newRow["NomeAuditor"]			= prestador.NomeCompleto;	
				newRow["TituloAuditor"]			= prestador.Titulo;	
				newRow["AssinaturaAuditor"]		= Fotos.PathFoto_Uri(prestador.FotoAss);
				ds.Tables["Result"].Rows.Add(newRow);
			}
			else
			{
				foreach(Irregularidade irr in list)
				{
					irr.IdNorma.Find();
					irr.IdNorma.IdNr.Find();
					newRow = ds.Tables["Result"].NewRow();
					newRow["IdCliente"]  = cliente.Id;
					newRow["HasHeader"]	 = (list.Count != 1);
					newRow["HasDetails"] = true;
					try
					{
						newRow["FotoFachada"] = Ilitera.Common.Fotos.PathFoto_Uri(cliente.GetFotoFachada());
					}
					catch (Exception ex)
					{
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        //System.Diagnostics.Trace.WriteLine(ex.Message);
					}
					newRow["NomeCliente"]			= cliente.NomeCompleto;
					newRow["LogradouroNumero"]		= endereco.GetEndereco();
					newRow["BairroCidadeEstadoCEP"] = endereco.Bairro + "  " + endereco.GetCidade() + "  " +endereco.GetEstado()  + "  " +endereco.Cep;			
					newRow["CNPJ"]					= "CNPJ: " + cliente.NomeCodigo;	
					newRow["DataAssinatura"]		= cliente.GetEndereco().GetCidade() + ", "+ auditoria.DataLevantamento.ToString("d \"de\" MMMM \"de\" yyyy")+"."; 
					newRow["NomeAuditor"]			= prestador.NomeCompleto;	
					newRow["TituloAuditor"]			= prestador.Titulo;	
					newRow["AssinaturaAuditor"]		= Fotos.PathFoto_Uri(prestador.FotoAss);

                    IrregularidadeFotoLocal fotoIrr = new IrregularidadeFotoLocal();
                    fotoIrr.Find("IdIrregularidade=" + irr.Id + " AND IsFotoPadrao=1");

					//detail
                    if (fotoIrr.NumeroFoto != string.Empty)
                        newRow["NumeroFoto"] = Convert.ToInt32(fotoIrr.NumeroFoto);
					try
					{
                        if (Ilitera.Data.Table.IsWeb)
                        {
                            //string xArquivo = Ilitera.Common.Fotos.PathSmallFoto(auditoria, Convert.ToInt32(fotoIrr.NumeroFoto));
                            string xArquivo = Ilitera.Common.Fotos.PathNormalFoto(auditoria, Convert.ToInt32(fotoIrr.NumeroFoto));

                            if (xArquivo.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
                            {
                                xArquivo = xArquivo.Substring(0, xArquivo.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + xArquivo.Substring(xArquivo.IndexOf("\\") + 1);
                            }

                            newRow["Foto"] = Ilitera.Common.Fotos.PathFoto_Uri(xArquivo);
                        }
                        else
                        {
                            string xArquivo = Ilitera.Common.Fotos.PathFoto_Uri(auditoria, Convert.ToInt32(fotoIrr.NumeroFoto));

                            if (xArquivo.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
                            {
                                xArquivo = xArquivo.Substring(0, xArquivo.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + xArquivo.Substring(xArquivo.IndexOf("\\") + 1);
                            }

                            newRow["Foto"] = Ilitera.Common.Fotos.PathFoto_Uri(xArquivo);
                            
                        }
					}
					catch(Exception ex)
					{
                        System.Diagnostics.Debug.WriteLine(ex.Message);
					}
					newRow["NomeEmpresa"] = cliente.NomeCompleto;
                    newRow["NumeroEmpregado"] = cliente.QtdEmpregados.ToString();
					newRow["Data"] = auditoria.DataLevantamento.ToString("dd-MM-yyyy");
					newRow["LocalTrabalho"] = irr.strLocalIrregularidade();
                    newRow["NR"] = "NR " + irr.IdNorma.IdNr.ToString();
                    

                    //newRow["NormaLegal"] = irr.IdNorma.GetEnquadramentoLegal();
                    // procurar /r/n e substituir por chr(13)

                    string Regex = irr.IdNorma.GetEnquadramentoLegal();


                    string myNewTest = Regex.Replace("\r\n", " ");

                    myNewTest = myNewTest.Replace("a)", "\r\na)");
                    myNewTest = myNewTest.Replace("b)", "\r\nb)");
                    myNewTest = myNewTest.Replace("c)", "\r\nc)");
                    myNewTest = myNewTest.Replace("d)", "\r\nd)");
                    myNewTest = myNewTest.Replace("e)", "\r\ne)");
                    myNewTest = myNewTest.Replace("f)", "\r\nf)");


                    newRow["NormaLegal"] = myNewTest;  //irr.IdNorma.GetEnquadramentoLegal();



					newRow["ValorMultaCodigo"] = irr.GetIdInfracaoValorMulta(numeroEmpregado,irr.IdNorma.CodigoInfracao);
					
					StringBuilder strParecer = new StringBuilder();
					strParecer.Append(@"{\rtf1\ansi\ansicpg1252\uc1\deff0\stshfdbch0\stshfloch0\stshfhich0\stshfbi0\deflang1046\deflangfe1046{\fonttbl{\f0\froman\fcharset0\fprq2{\*\panose 02020603050405020304}Times New Roman;}{\f1\fswiss\fcharset0\fprq2{\*\panose 020b0604020202020204}Arial;}");
					strParecer.Append(@"{\f36\froman\fcharset238\fprq2 Times New Roman CE;}{\f37\froman\fcharset204\fprq2 Times New Roman Cyr;}{\f39\froman\fcharset161\fprq2 Times New Roman Greek;}{\f40\froman\fcharset162\fprq2 Times New Roman Tur;}");
					strParecer.Append(@"{\f41\froman\fcharset177\fprq2 Times New Roman (Hebrew);}{\f42\froman\fcharset178\fprq2 Times New Roman (Arabic);}{\f43\froman\fcharset186\fprq2 Times New Roman Baltic;}{\f44\froman\fcharset163\fprq2 Times New Roman (Vietnamese);}");
					strParecer.Append(@"{\f46\fswiss\fcharset238\fprq2 Arial CE;}{\f47\fswiss\fcharset204\fprq2 Arial Cyr;}{\f49\fswiss\fcharset161\fprq2 Arial Greek;}{\f50\fswiss\fcharset162\fprq2 Arial Tur;}{\f51\fswiss\fcharset177\fprq2 Arial (Hebrew);}");
					strParecer.Append(@"{\f52\fswiss\fcharset178\fprq2 Arial (Arabic);}{\f53\fswiss\fcharset186\fprq2 Arial Baltic;}{\f54\fswiss\fcharset163\fprq2 Arial (Vietnamese);}}{\colortbl;\red0\green0\blue0;\red0\green0\blue255;\red0\green255\blue255;\red0\green255\blue0;");
					strParecer.Append(@"\red255\green0\blue255;\red255\green0\blue0;\red255\green255\blue0;\red255\green255\blue255;\red0\green0\blue128;\red0\green128\blue128;\red0\green128\blue0;\red128\green0\blue128;\red128\green0\blue0;\red128\green128\blue0;\red128\green128\blue128;");
					strParecer.Append(@"\red192\green192\blue192;}{\stylesheet{\ql \li0\ri0\widctlpar\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \fs24\lang1046\langfe1046\cgrid\langnp1046\langfenp1046 \snext0 Normal;}{\*\cs10 \additive \ssemihidden Default Paragraph Font;}{\*");
					strParecer.Append(@"\ts11\tsrowd\trftsWidthB3\trpaddl108\trpaddr108\trpaddfl3\trpaddft3\trpaddfb3\trpaddfr3\trcbpat1\trcfpat1\tscellwidthfts0\tsvertalt\tsbrdrt\tsbrdrl\tsbrdrb\tsbrdrr\tsbrdrdgl\tsbrdrdgr\tsbrdrh\tsbrdrv ");
					strParecer.Append(@"\ql \li0\ri0\widctlpar\aspalpha\aspnum\faauto\adjustright\rin0\lin0\itap0 \fs20\lang1024\langfe1024\cgrid\langnp1024\langfenp1024 \snext11 \ssemihidden Normal Table;}}{\*\rsidtbl \rsid865023\rsid3281332\rsid4221156\rsid5142496\rsid5252849\rsid5713250");
					strParecer.Append(@"\rsid5849218\rsid7020526\rsid7690079\rsid8027950}{\*\generator Microsoft Word 10.0.2627;}{\info{\title FATO CARACTERIZADOR DA INFRA\'c7\'c3O (N\'c3O-CONFORMIDADE LEGAL)}{\author Henrique}{\operator }{\creatim\yr2004\mo3\dy26\hr14\min38}");
					strParecer.Append(@"{\revtim\yr2005\mo4\dy12\hr14\min32}{\version3}{\edmins3}{\nofpages1}{\nofwords36}{\nofchars196}{\*\company Mestra}{\nofcharsws231}{\vern16437}}\margl1701\margr1701\margt1417\margb1417 ");
					strParecer.Append(@"\widowctrl\ftnbj\aenddoc\hyphhotz425\noxlattoyen\expshrtn\noultrlspc\dntblnsbdb\nospaceforul\hyphcaps0\horzdoc\dghspace120\dgvspace120\dghorigin1701\dgvorigin1984\dghshow0\dgvshow3\jcompress\viewkind4\viewscale100\nolnhtadjtbl\rsidroot7690079 \fet0\sectd ");
					strParecer.Append(@"\linex0\sectdefaultcl\sftnbj {\*\pnseclvl1\pnucrm\pnstart1\pnindent720\pnhang {\pntxta .}}{\*\pnseclvl2\pnucltr\pnstart1\pnindent720\pnhang {\pntxta .}}{\*\pnseclvl3\pndec\pnstart1\pnindent720\pnhang {\pntxta .}}{\*\pnseclvl4");
					strParecer.Append(@"\pnlcltr\pnstart1\pnindent720\pnhang {\pntxta )}}{\*\pnseclvl5\pndec\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl6\pnlcltr\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl7\pnlcrm\pnstart1\pnindent720\pnhang {\pntxtb (}");
					strParecer.Append(@"{\pntxta )}}{\*\pnseclvl8\pnlcltr\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}{\*\pnseclvl9\pnlcrm\pnstart1\pnindent720\pnhang {\pntxtb (}{\pntxta )}}\pard\plain \qc \li0\ri616\nowidctlpar\faauto\rin616\lin0\itap0\pararsid865023 ");
					strParecer.Append(@"\fs24\lang1046\langfe1046\cgrid\langnp1046\langfenp1046 {\b\f1\fs16\cf11\insrsid5252849 FATO CARACTERIZADOR DA INFRA\'c7\'c3O (N\'c3O-CONFORMIDADE LEGAL)}{\b\f1\fs16\insrsid8027950\charrsid7020526 ");
					strParecer.Append(@"\par }\pard \qj \li0\ri616\nowidctlpar\faauto\rin616\lin0\itap0\pararsid3281332 {\f1\fs12\insrsid5252849 DADOS_FATO}{\f1\fs12\insrsid8027950\charrsid7020526 ");
					strParecer.Append(@"\par }\pard \ql \li0\ri616\nowidctlpar\faauto\rin616\lin0\itap0\pararsid865023 {\b\f1\fs16\cf11\insrsid5252849 RESPONSABILIDADE CIVIL:}{\b\f1\fs16\insrsid7020526\charrsid7020526 ");
					strParecer.Append(@"\par }\pard \qj \li0\ri616\nowidctlpar\faauto\rin616\lin0\itap0\pararsid3281332 {\f1\fs12\insrsid5252849 DADOS_CIVIL}{\f1\fs12\insrsid7020526\charrsid7020526 ");
					strParecer.Append(@"\par }\pard \ql \li0\ri616\nowidctlpar\faauto\rin616\lin0\itap0\pararsid865023 {\b\f1\fs16\cf11\insrsid5252849 RESPONSABILIDADE PENAL:}{\b\f1\fs16\insrsid7020526\charrsid7020526 ");
					strParecer.Append(@"\par }\pard \qj \li0\ri616\nowidctlpar\faauto\rin616\lin0\itap0\pararsid3281332 {\f1\fs12\insrsid5252849 DADOS_PENAL}{\f1\fs12\insrsid7020526\charrsid7020526 ");
					strParecer.Append(@"\par }\pard \ql \li0\ri616\nowidctlpar\faauto\rin616\lin0\itap0\pararsid865023 {\b\f1\fs16\cf11\insrsid5252849 RESPONSABILIDADE TRABALHISTA:}{\b\f1\fs16\insrsid7020526\charrsid7020526 ");
					strParecer.Append(@"\par }\pard \qj \li0\ri616\nowidctlpar\faauto\rin616\lin0\itap0\pararsid3281332 {\f1\fs12\insrsid5252849 DADOS_TRABALHISTA}{\f1\fs12\insrsid7020526\charrsid7020526 ");
					strParecer.Append(@"\par }\pard \ql \li0\ri616\nowidctlpar\faauto\rin616\lin0\itap0\pararsid865023 {\b\f1\fs16\cf11\insrsid5252849 RESPONSABILIDADE NORMATIVA:}{\b\f1\fs16\insrsid7020526\charrsid7020526 ");
					strParecer.Append(@"\par }\pard \qj \li0\ri616\nowidctlpar\faauto\rin616\lin0\itap0\pararsid3281332 {\f1\fs12\insrsid5252849 DADOS_NORMATIVA}{\insrsid7690079 ");
					strParecer.Append(@"\par }}");
					strParecer.Replace("DADOS_FATO", irr.strAcoesExecutar());
					strParecer.Replace("DADOS_CIVIL", irr.IdNorma.RespCivil);
					strParecer.Replace("DADOS_PENAL", irr.IdNorma.RespPenal);
					strParecer.Replace("DADOS_TRABALHISTA", irr.IdNorma.RespTrabalhista);
					strParecer.Replace("DADOS_NORMATIVA", irr.IdNorma.RespNormativa);
					newRow["ParecerTecnicoJuridico"] = strParecer.ToString();
					ds.Tables["Result"].Rows.Add(newRow);
				}
			}
		}

        private static DataTable GetTableAuditoriaComCaracterizacao()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("IdCliente", Type.GetType("System.Int32"));
            table.Columns.Add("NomeCliente", Type.GetType("System.String"));
            table.Columns.Add("LogradouroNumero", Type.GetType("System.String"));
            table.Columns.Add("BairroCidadeEstadoCEP", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("DataAssinatura", Type.GetType("System.String"));
            table.Columns.Add("NomeAuditor", Type.GetType("System.String"));
            table.Columns.Add("TituloAuditor", Type.GetType("System.String"));
            table.Columns.Add("AssinaturaAuditor", Type.GetType("System.String"));
            table.Columns.Add("HasHeader", Type.GetType("System.Boolean"));
            table.Columns.Add("NumeroFoto", Type.GetType("System.Int32"));
            table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
            table.Columns.Add("NumeroEmpregado", Type.GetType("System.String"));
            table.Columns.Add("Data", Type.GetType("System.String"));
            table.Columns.Add("LocalTrabalho", Type.GetType("System.String"));
            table.Columns.Add("NR", Type.GetType("System.String"));
            table.Columns.Add("NormaLegal", Type.GetType("System.String"));
            table.Columns.Add("ParecerTecnicoJuridico", Type.GetType("System.String"));
            table.Columns.Add("ValorMultaCodigo", Type.GetType("System.Int32"));
            table.Columns.Add("HasDetails", Type.GetType("System.Boolean"));
            table.Columns.Add("Foto", Type.GetType("System.String"));
            table.Columns.Add("FotoFachada", Type.GetType("System.String"));
            return table;
        }
	}
}
