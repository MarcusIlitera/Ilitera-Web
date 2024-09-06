using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using Ilitera.Data;
using Ilitera.Opsa.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Report
{
	public class DataSourceBoletoRecibo
	{
		public DataSourceBoletoRecibo()
		{			

		}

		private DataTable GetDataTable()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("IdFaturamento", Type.GetType("System.Boolean"));
			table.Columns.Add("TipoRecibo", Type.GetType("System.Boolean"));
			table.Columns.Add("Emissao", Type.GetType("System.String"));
			table.Columns.Add("NomeCompleto", Type.GetType("System.String"));
			table.Columns.Add("Endereco", Type.GetType("System.String"));
			table.Columns.Add("Cidade", Type.GetType("System.String"));
			table.Columns.Add("UF", Type.GetType("System.String"));
			table.Columns.Add("Cep", Type.GetType("System.String"));
			table.Columns.Add("CNPJ", Type.GetType("System.String"));
			table.Columns.Add("InscricaoEstadual", Type.GetType("System.String"));
			table.Columns.Add("InscricaoCCM", Type.GetType("System.String"));
			table.Columns.Add("CondicaoPagamento", Type.GetType("System.String"));
			table.Columns.Add("Vencimento", Type.GetType("System.String"));
			table.Columns.Add("Valor_1", Type.GetType("System.String"));
			table.Columns.Add("Valor_5", Type.GetType("System.String"));
			table.Columns.Add("Valor_6", Type.GetType("System.String"));
			table.Columns.Add("Valor_IR", Type.GetType("System.String"));
			table.Columns.Add("Valor_PIS", Type.GetType("System.String"));
			table.Columns.Add("Valor_CONFINS", Type.GetType("System.String"));
			table.Columns.Add("Valor_CSLL", Type.GetType("System.String"));
			table.Columns.Add("Valor_TotalImpostos", Type.GetType("System.String"));
			table.Columns.Add("Valor_TotalSemImpostos", Type.GetType("System.String"));
			table.Columns.Add("Valor_Total", Type.GetType("System.String"));
			table.Columns.Add("Porcentagem_Impostos", Type.GetType("System.String"));
			table.Columns.Add("Descricao_5", Type.GetType("System.String"));
			table.Columns.Add("Descricao_6", Type.GetType("System.String"));
			table.Columns.Add("EntregaEndereco", Type.GetType("System.String"));
			table.Columns.Add("EntregaTelefone", Type.GetType("System.String"));
			table.Columns.Add("Cedente", Type.GetType("System.String"));
			table.Columns.Add("CnpjCedente", Type.GetType("System.String"));
			table.Columns.Add("Agencia_CodigoCedente", Type.GetType("System.String"));
			table.Columns.Add("DataDocumento", Type.GetType("System.String"));
			table.Columns.Add("NumeroDocumento", Type.GetType("System.String"));
			table.Columns.Add("EspecieDocumento", Type.GetType("System.String"));
			table.Columns.Add("Aceite", Type.GetType("System.String"));
			table.Columns.Add("DataProcessamento", Type.GetType("System.String"));
			table.Columns.Add("NossoNumero", Type.GetType("System.String"));
			table.Columns.Add("UsoBanco", Type.GetType("System.String"));
			table.Columns.Add("Carteira", Type.GetType("System.String"));
			table.Columns.Add("EspecieMoeda", Type.GetType("System.String"));
			table.Columns.Add("Quantidade", Type.GetType("System.String"));
			table.Columns.Add("Valor", Type.GetType("System.String"));
			table.Columns.Add("ValorDocumento", Type.GetType("System.String"));
			table.Columns.Add("Instrucoes", Type.GetType("System.String"));
			table.Columns.Add("Desconto_Abatimentos", Type.GetType("System.String"));
			table.Columns.Add("OutrasDeducoes", Type.GetType("System.String"));
			table.Columns.Add("Mora_Multa", Type.GetType("System.String"));
			table.Columns.Add("OutrosAcressimos", Type.GetType("System.String"));
			table.Columns.Add("ValorCobrado", Type.GetType("System.String"));
			table.Columns.Add("LinhaDigitavel", Type.GetType("System.String"));
			table.Columns.Add("CodBarras", Type.GetType("System.String"));
			table.Columns.Add("Motorista", Type.GetType("System.String"));
			return table;
		}

		public DataTable GetDataTableServicos()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("IdFaturamento", Type.GetType("System.Int32"));
			table.Columns.Add("Quantidade", Type.GetType("System.String"));
			table.Columns.Add("Unidade", Type.GetType("System.String"));
			table.Columns.Add("Descricao", Type.GetType("System.String"));
			table.Columns.Add("ValorUnitario", Type.GetType("System.String"));
			table.Columns.Add("ValorTotal", Type.GetType("System.String"));
			return table;
		}

		public DataSet GetBoletoReciboTeste()
		{
			ArrayList list = new ArrayList();
			Recibos recibo = new Recibos();
			list = recibo.Find("NossoNumero IN (39, 47, 53, 60, 103, 110, 252, 260, 261, 262, 263, 264, 265, 266, 267, 268, 269)");
			return GetBoletoRecibo(list);
		}

		public DataSet GetBoletoRecibo(Recibos recibo)
		{
			ArrayList list = new ArrayList();
			list.Add(recibo);
			return GetBoletoRecibo(list);
		}

		public DataSet GetBoletoRecibo(string DataProcessamento)
		{
			ArrayList list;
			list = new Recibos().Find("Dt_processamento='"+DataProcessamento+"'");
			return GetBoletoRecibo(list);
		}

		public DataSet GetBoletoRecibo(ArrayList listRecibos)
		{
			const double IR = 0.0150F;
			const double PIS_COFINS_CSLL =  0.0465F;

			DataSet ds = new DataSet();
			ds.Tables.Add(this.GetDataTable());
			DataRow newRow;	
			StringBuilder instrucoes = new StringBuilder();
			instrucoes.Append("JUROS DE MORA DE 0,1% POR DIA\n");
			instrucoes.Append("SUJEITO A PROTESTO APÓS 10 DIAS DO VENCIMENTO\n");
			foreach(Recibos recibos in listRecibos)
			{
				Faturamento faturamento = new Faturamento();
				Cliente cliente = new Cliente();
				cliente.Find("CodigoAntigo="+recibos.CODIGO);
				double ValorIR		= 0.0F;
				double PctImpostos	= 0.0F;
				if(recibos.NaoReterIRAbaixo10)
				{
					if((recibos.Vl_total_recibo * IR)>10)
					{
						ValorIR = recibos.Vl_total_recibo * IR;
						if(cliente.IsOptanteSimples||(recibos.Vl_total_recibo<5000F))							
							PctImpostos = IR;
						else
							PctImpostos = IR + PIS_COFINS_CSLL;
					}
					else
					{
						if(cliente.IsOptanteSimples ||(recibos.Vl_total_recibo<5000F))
							PctImpostos = 0.0F;
						else
							PctImpostos = PIS_COFINS_CSLL;
					}
				}
				else
				{
					ValorIR	= recibos.Vl_total_recibo * IR;
					if(cliente.IsOptanteSimples ||(recibos.Vl_total_recibo<5000F))
						PctImpostos = IR;
					else
						PctImpostos = IR + PIS_COFINS_CSLL;
				}
				
				double ValorTotalImposto = System.Math.Round((recibos.Vl_total_recibo * PctImpostos), 2);
				
				newRow = ds.Tables[0].NewRow();
				double valorDocumento;
				string codigoCedente;
				if(recibos.RECIBO_NF=="R")
				{
					valorDocumento			= recibos.Vl_total_recibo - ValorTotalImposto;
					newRow["TipoRecibo"]	= true;
					codigoCedente			= "";
					newRow["Cedente"]		= "";
					newRow["CnpjCedente"]	= "";
				}
				else
				{
					valorDocumento			= recibos.Vl_total_recibo;
					newRow["TipoRecibo"]	= false;
					codigoCedente			= "";
					newRow["Cedente"]		= "";
					newRow["CnpjCedente"]	= "";
				}
				string strCodigoBarras				= faturamento.CodigoBarras("033", "9", valorDocumento, recibos.Dt_vencimento , codigoCedente, recibos.NossoNumero.ToString(), "00");
				string strLinhaDigitavel			= faturamento.LinhaDigitavel(recibos.Dt_vencimento, codigoCedente, recibos.NossoNumero.ToString(), "00", "033",  valorDocumento, strCodigoBarras);
				newRow["Emissao"]					= recibos.Dt_emissao.ToString("dd-MM-yyyy");
				newRow["NomeCompleto"]				= recibos.Nm_razao_social;
				newRow["Endereco"]					= recibos.Ds_endereco;
				newRow["Cidade"]					= recibos.Nm_cidade;
				newRow["UF"]						= recibos.Sg_estado;
				newRow["Cep"]						= recibos.Cd_cep;
				newRow["CNPJ"]						= recibos.Cd_cnpj;
				newRow["InscricaoEstadual"]			= recibos.Cd_inscr_estadual;
				newRow["InscricaoCCM"]				= "";
				newRow["CondicaoPagamento"]			= "À VISTA - MÊS "+recibos.Dt_processamento;
				newRow["Vencimento"]				= recibos.Dt_vencimento.ToString("dd-MM-yyyy");
				newRow["Valor_1"]					= recibos.VALOR_NF_1.ToString("n");
				if(recibos.VALOR_NF_5!=0)
					newRow["Valor_5"]				= recibos.VALOR_NF_5.ToString("n");
				if(recibos.VALOR_NF_6!=0)
					newRow["Valor_6"]				= recibos.VALOR_NF_6.ToString("n");
				if(ValorIR!=0)
					newRow["Valor_IR"]				= ValorIR.ToString("n");
				else
					newRow["Valor_IR"]				= "-";
				if(cliente.IsOptanteSimples || (recibos.Vl_total_recibo<5000F))
				{
					newRow["Valor_PIS"]					= "-";
					newRow["Valor_CONFINS"]				= "-";
					newRow["Valor_CSLL"]				= "-";
				}
				else
				{
					newRow["Valor_PIS"]					= Convert.ToSingle(recibos.Vl_total_recibo * 0.0065F).ToString("n");
					newRow["Valor_CONFINS"]				= Convert.ToSingle(recibos.Vl_total_recibo * 0.0300F).ToString("n");
					newRow["Valor_CSLL"]				= Convert.ToSingle(recibos.Vl_total_recibo * 0.0100F).ToString("n");
				}
				newRow["Porcentagem_Impostos"]		= PctImpostos.ToString("p");
				newRow["Valor_TotalImpostos"]		= ValorTotalImposto.ToString("n");
				newRow["Valor_TotalSemImpostos"]	= valorDocumento.ToString("n");
				newRow["Valor_Total"]				= recibos.Vl_total_recibo.ToString("n");
				newRow["Descricao_5"]				= recibos.DESCR_NF_5;
				newRow["Descricao_6"]				= recibos.DESCR_NF_6;
				newRow["Agencia_CodigoCedente"]		= codigoCedente;
				newRow["DataDocumento"]				= DateTime.Today.ToString("dd-MM-yyyy");
				newRow["NumeroDocumento"]			= recibos.NossoNumero.ToString("0000000");
				newRow["EspecieDocumento"]			= "DS-CI";
				newRow["Aceite"]					= "N";
				newRow["DataProcessamento"]			= recibos.Dt_emissao.ToString("dd-MM-yyyy");
				newRow["NossoNumero"]				= faturamento.FormataNossoNumero("105", recibos.NossoNumero.ToString());
				newRow["UsoBanco"]					= "AVENIDAS";
				newRow["Carteira"]					= "COB.";
				newRow["EspecieMoeda"]				= "R$";
				newRow["Quantidade"]				= "";
				newRow["Valor"]						= "";
				newRow["ValorDocumento"]			= valorDocumento.ToString("n");
				newRow["Instrucoes"]				= instrucoes.ToString().Replace("0,1%", "R$ "+Convert.ToSingle(valorDocumento*0.001F).ToString("n"));
				newRow["Desconto_Abatimentos"]		= "";
				newRow["OutrasDeducoes"]			= "";
				newRow["Mora_Multa"]				= "";
				newRow["OutrosAcressimos"]			= "";
				newRow["ValorCobrado"]				= "";
				newRow["LinhaDigitavel"]			= strLinhaDigitavel;
				newRow["CodBarras"]					= strCodigoBarras;
				newRow["Motorista"]					= recibos.MOTORISTA.ToString();		
				ds.Tables[0].Rows.Add(newRow);
			}
			return ds;
		}

		private DataTable GetDataTableControleFinanceiroMensal()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("MicroEmpresa", Type.GetType("System.String"));
			table.Columns.Add("NomeEmpresa", Type.GetType("System.String"));
			table.Columns.Add("Inicio", Type.GetType("System.String"));
			table.Columns.Add("Documento", Type.GetType("System.String"));
			table.Columns.Add("SM", Type.GetType("System.String"));
			table.Columns.Add("Seguranca", Type.GetType("System.Single"));
			table.Columns.Add("PCMSO", Type.GetType("System.Single"));
			table.Columns.Add("Juridico", Type.GetType("System.Single"));
			table.Columns.Add("Total", Type.GetType("System.Single"));
			table.Columns.Add("IRF", Type.GetType("System.Single"));
			table.Columns.Add("TotalReceber", Type.GetType("System.Single"));
			table.Columns.Add("Vencimento", Type.GetType("System.String"));
			table.Columns.Add("Entrega", Type.GetType("System.String"));
			table.Columns.Add("Acessado", Type.GetType("System.String"));
			table.Columns.Add("Transporte", Type.GetType("System.String"));
			table.Columns.Add("Contato", Type.GetType("System.String"));
			table.Columns.Add("Telefone", Type.GetType("System.String"));
			table.Columns.Add("Email", Type.GetType("System.String"));
			table.Columns.Add("Atualizado", Type.GetType("System.Boolean"));
			return table;
		}

        public DataSet GetControleFinanceiroMensal(ArrayList listRecibos)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.GetDataTableControleFinanceiroMensal());
            DataRow newRow;
            int i = 1;
            foreach (Recibos recibos in listRecibos)
            {
                newRow = ds.Tables[0].NewRow();
                Cliente cliente = new Cliente();
                cliente.Find("CodigoAntigo=" + recibos.CODIGO);
                newRow["MicroEmpresa"] = (recibos.RECIBO_NF == "R") ? "OPSA" : "MESTRA";
                newRow["NomeEmpresa"] = cliente.NomeAbreviado;
                newRow["Inicio"] = cliente.DataCadastro.ToString("dd-MM-yyyy");
                newRow["Documento"] = recibos.NossoNumero;
                if (recibos.QT_ASSES != recibos.VALOR_NF_1)
                    newRow["SM"] = recibos.QT_ASSES;
                newRow["Seguranca"] = recibos.VALOR_NF_1;
                if (recibos.DESCR_NF_5 != string.Empty && recibos.VALOR_NF_5 != 0)
                {
                    if (recibos.DESCR_NF_5.IndexOf("PCMSO") != -1)
                        newRow["PCMSO"] = recibos.VALOR_NF_5;
                    else
                        newRow["Juridico"] = recibos.VALOR_NF_5;
                }
                if (recibos.DESCR_NF_6 != string.Empty && recibos.VALOR_NF_6 != 0)
                {
                    if (recibos.DESCR_NF_6.IndexOf("PCMSO") != -1)
                        newRow["PCMSO"] = recibos.VALOR_NF_6;
                    else
                        newRow["Juridico"] = recibos.VALOR_NF_6;
                }
                newRow["Total"] = recibos.Vl_total_recibo.ToString("n");
                newRow["IRF"] = Convert.ToSingle(recibos.Vl_total_recibo * 0.0650F);
                newRow["TotalReceber"] = Convert.ToSingle(recibos.Vl_total_recibo - (recibos.Vl_total_recibo * 0.0650F));
                newRow["Vencimento"] = recibos.Dt_vencimento.ToString("dd-MM-yyyy");
                ContatoTelefonico contatoTelefonico;
                ArrayList list = new Contato().Find("IdJuridica=" + cliente.Id);

                if (list.Count != 0)
                {
                    foreach (Contato contato in list)
                    {
                        contato.IdPessoa.Find();
                        if (contato.IdPessoa.Email != string.Empty)
                        {
                            contatoTelefonico = contato.IdPessoa.GetContatoTelefonico();
                            newRow["Contato"] = contato.IdPessoa.NomeAbreviado;
                            newRow["Telefone"] = contatoTelefonico.Numero;
                            newRow["Email"] = contato.IdPessoa.Email;
                            newRow["Atualizado"] = true;
                        }
                        else
                        {
                            contatoTelefonico = cliente.GetContatoTelefonico();
                            newRow["Contato"] = contatoTelefonico.Nome;
                            newRow["Telefone"] = contatoTelefonico.Numero;
                            newRow["Email"] = cliente.Email;
                            newRow["Atualizado"] = false;
                        }
                        break;
                    }
                }
                else
                {
                    contatoTelefonico = cliente.GetContatoTelefonico();

                    newRow["Contato"] = contatoTelefonico.Nome;
                    newRow["Telefone"] = contatoTelefonico.Numero;
                    newRow["Email"] = cliente.Email;
                    newRow["Atualizado"] = false;
                }
                if (recibos.DataEntrega != new DateTime())
                    newRow["Entrega"] = recibos.DataEntrega.ToString("dd-MM-yyyy");
                else
                    newRow["Entrega"] = "-";
                if (recibos.IndEntregaBoleto == (int)Faturamento.EntregaBoleto.NaoEntregue)
                    newRow["Transporte"] = "-";
                else if (recibos.IndEntregaBoleto == (int)Faturamento.EntregaBoleto.Motorista)
                    newRow["Transporte"] = "M";
                else if (recibos.IndEntregaBoleto == (int)Faturamento.EntregaBoleto.Correio)
                    newRow["Transporte"] = "C";
                else if (recibos.IndEntregaBoleto == (int)Faturamento.EntregaBoleto.Email)
                    newRow["Transporte"] = "E";
                else if (recibos.IndEntregaBoleto == (int)Faturamento.EntregaBoleto.Internet)
                    newRow["Transporte"] = "I";
                if (recibos.DataAcesso != new DateTime())
                    newRow["Acessado"] = recibos.DataAcesso.ToString("dd-MM-yyyy");
                else
                    newRow["Acessado"] = "-";
                i++;
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
	}
}
