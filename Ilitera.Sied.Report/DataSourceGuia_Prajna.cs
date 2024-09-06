using System;
using Ilitera.Opsa.Data;
using System.Data;
using Ilitera.Common;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;





namespace Ilitera.Sied.Report
{
    public class DataSourceGuia_Prajna : DataSourceBase
    {
        private Cliente cliente;
        private int xIdEmpregado;
        private int xIdEmpresa;
        private int xIdClinica;
        private string xExames;
        private string xExames2;
        private string xExames3;
        private string xExames4;
        private string xData_Exame;
        private string xHora_Exame;
        private string xTipo;
        //private string xObs;
        private string xBasico;
        private string xObs2;
        private string xImpDt;
        private int xIdEmpregadoFuncao;

        public DataSourceGuia_Prajna(Cliente cliente, int xIdEmpregado, int xIdEmpresa, int xIdClinica, string xExames, string xExames2, string xExames3, string xExames4, string xData_Exame, string xHora_Exame, string xTipo, string xBasico, string xObs2, String xImpDt, int xIdEmpregadoFuncao )
        {
            this.cliente = cliente;
            this.xIdEmpregado = xIdEmpregado;
            this.xIdClinica = xIdClinica;
            this.xIdEmpresa = xIdEmpresa;
            this.xExames = xExames;
            this.xExames2 = xExames2;
            this.xExames3 = xExames3;
            this.xExames4 = xExames4;
            this.xData_Exame = xData_Exame;
            this.xHora_Exame = xHora_Exame;
            this.xTipo = xTipo;
            this.xImpDt = xImpDt;
            //this.xObs = xObs;
            this.xBasico = xBasico;
            this.xObs2 = xObs2;
            this.xIdEmpregadoFuncao = xIdEmpregadoFuncao;

            if (this.cliente.mirrorOld == null)
                this.cliente.Find();

         }


        public RptGuia_Prajna GetReport()
        {
            RptGuia_Prajna report = new RptGuia_Prajna();
            report.SetDataSource(GetIntroducaoGuia());
            //report.OpenSubreport("FotoFachada").SetDataSource(DataSourceFotoFachada());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }


        public RptGuia_Nova_Prajna GetReport_Nova()
        {
            RptGuia_Nova_Prajna report = new RptGuia_Nova_Prajna();
            report.SetDataSource(GetIntroducaoGuia());
            //report.OpenSubreport("FotoFachada").SetDataSource(DataSourceFotoFachada());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }



        private DataSet GetIntroducaoGuia()
        {

            
            DataTable table = new DataTable("DataTable1");

            //colaborador
            table.Columns.Add("Nome", Type.GetType("System.String"));
            table.Columns.Add("Setor", Type.GetType("System.String"));
            table.Columns.Add("Funcao", Type.GetType("System.String"));
            table.Columns.Add("CTPS", Type.GetType("System.String"));
            table.Columns.Add("RG", Type.GetType("System.String"));
            table.Columns.Add("Nascimento", Type.GetType("System.String"));
            table.Columns.Add("Sexo", Type.GetType("System.String"));
            table.Columns.Add("Admissao", Type.GetType("System.String"));
            table.Columns.Add("GHE", Type.GetType("System.String"));

            //empregador
            table.Columns.Add("Empregador", Type.GetType("System.String"));
            table.Columns.Add("Endereco", Type.GetType("System.String"));
            table.Columns.Add("Cidade", Type.GetType("System.String"));
            table.Columns.Add("UF", Type.GetType("System.String"));
            table.Columns.Add("CEP", Type.GetType("System.String"));
            table.Columns.Add("CNPJ", Type.GetType("System.String"));
            table.Columns.Add("Inscricao_Estadual", Type.GetType("System.String"));

            //clinica
            table.Columns.Add("Clinica", Type.GetType("System.String"));
            table.Columns.Add("End_Clinica", Type.GetType("System.String"));
            table.Columns.Add("Cid_Clinica", Type.GetType("System.String"));
            table.Columns.Add("UF_Clinica", Type.GetType("System.String"));
            table.Columns.Add("CEP_Clinica", Type.GetType("System.String"));
            
            table.Columns.Add("Exames", Type.GetType("System.String"));
            table.Columns.Add("Exames2", Type.GetType("System.String"));
            table.Columns.Add("Exames3", Type.GetType("System.String"));
            table.Columns.Add("Exames4", Type.GetType("System.String"));
            table.Columns.Add("Data_Exame", Type.GetType("System.String"));
            table.Columns.Add("Hora_Exame", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Admissional", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Demissional", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Mudanca", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Retorno", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Periodico", Type.GetType("System.String"));
            table.Columns.Add("Tipo_Outro", Type.GetType("System.String"));
            table.Columns.Add("Mapa", Type.GetType("System.String"));
            table.Columns.Add("Obs", Type.GetType("System.String"));
            table.Columns.Add("Obs2", Type.GetType("System.String"));
            table.Columns.Add("Preparo", Type.GetType("System.String"));

            table.Columns.Add("Frase", Type.GetType("System.String"));

            DataSet ds = new DataSet(); 

            ds.Tables.Add(table);

            DataRow newRow = ds.Tables[0].NewRow();


            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
            {
                newRow["Frase"] = "Essence: Prana Gestão Ocupacional Ltda - 17.898.088/0001-03 - (11) 5504-9600";
            }

            //carregar dados da empresa

            Cliente xCliente = new Cliente(xIdEmpresa);



            //ver configuração do ASO, se deve sair endereço da matriz



            

            if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
            {


             
                if (xCliente.IdJuridicaPai.ToString().ToUpper().IndexOf("KNOX") > 0)
                {
                    //Cliente xCliente2 = new Cliente(xCliente.IdJuridicaPai.Id);

                    newRow["Empregador"] = xCliente.IdJuridicaPai.ToString();
                    newRow["Endereco"] = xCliente.IdJuridicaPai.GetEndereco().IdTipoLogradouro + " " + xCliente.IdJuridicaPai.GetEndereco().Logradouro + " " + xCliente.IdJuridicaPai.GetEndereco().Numero.ToString() + " " + xCliente.IdJuridicaPai.GetEndereco().Complemento;
                    newRow["Cidade"] = xCliente.IdJuridicaPai.GetEndereco().GetCidade();
                    newRow["UF"] = xCliente.IdJuridicaPai.GetEndereco().Uf;
                    newRow["CEP"] = xCliente.IdJuridicaPai.GetEndereco().Cep;

                }
                else
                {

                    newRow["Empregador"] = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();

                    if (xCliente.Endereco_Matriz == true)
                    {
                        newRow["Endereco"] = xCliente.IdJuridicaPai.GetEndereco().IdTipoLogradouro + " " + xCliente.IdJuridicaPai.GetEndereco().Logradouro + " " + xCliente.IdJuridicaPai.GetEndereco().Numero.ToString() + " " + xCliente.IdJuridicaPai.GetEndereco().Complemento;
                        newRow["Cidade"] = xCliente.IdJuridicaPai.GetEndereco().GetCidade();
                        newRow["UF"] = xCliente.IdJuridicaPai.GetEndereco().Uf;
                        newRow["CEP"] = xCliente.IdJuridicaPai.GetEndereco().Cep;

                    }
                    else
                    {
                        newRow["Endereco"] = xCliente.GetEndereco().IdTipoLogradouro + " " + xCliente.GetEndereco().Logradouro + " " + xCliente.GetEndereco().Numero.ToString() + " " + xCliente.GetEndereco().Complemento;
                        newRow["Cidade"] = xCliente.GetEndereco().GetCidade();
                        newRow["UF"] = xCliente.GetEndereco().Uf;
                        newRow["CEP"] = xCliente.GetEndereco().Cep;

                    }



                }
            }
            else
            {
                newRow["Empregador"] = xCliente.GetNomeEmpresa();
                newRow["Endereco"] = xCliente.GetEndereco().IdTipoLogradouro + " " + xCliente.GetEndereco().Logradouro + " " + xCliente.GetEndereco().Numero.ToString() + " " + xCliente.GetEndereco().Complemento;
                newRow["Cidade"] = xCliente.GetEndereco().GetCidade();
                newRow["UF"] = xCliente.GetEndereco().Uf;
                newRow["CEP"] = xCliente.GetEndereco().Cep;

            }



            
            //newRow["CNPJ"] = xCliente.GetCnpj();
            //newRow["Inscricao_Estadual"] = xCliente.IE;

            


            //carregar dados do funcionario
            Empregado xEmpregado = new Empregado(xIdEmpregado);

            string xPCD = "";

            if (xEmpregado.nIND_BENEFICIARIO != null)
            {
                if (xEmpregado.nIND_BENEFICIARIO == 1 || xEmpregado.nIND_BENEFICIARIO == 2) xPCD = " (PCD) ";
            }

            string xCNH = "";

            if (xEmpregado.CNH.Trim() != "")
            {
                if (xExames.ToUpper().IndexOf("TOXICOLÓGICO") >= 0 || xExames2.ToUpper().IndexOf("TOXICOLÓGICO") >= 0 ||
                    xExames3.ToUpper().IndexOf("TOXICOLÓGICO") >= 0 || xExames4.ToUpper().IndexOf("TOXICOLÓGICO") >= 0)
                {
                    xCNH = "CNH  " + xEmpregado.CNH.Trim();
                }
            }



            if (xEmpregado.tNO_CPF.Trim() != "")
                newRow["Nome"] = xEmpregado.tNO_EMPG + xPCD + "   CPF: " + xEmpregado.tNO_CPF;
            else
                newRow["Nome"] = xEmpregado.tNO_EMPG + xPCD;  

            if (this.xBasico == "1")
            {
                newRow["CTPS"] = ""; 
                newRow["RG"] = ""; 
                newRow["Nascimento"] = ""; 
                newRow["Sexo"] = ""; 
                newRow["Admissao"] = ""; 
            }
            else
            {
                newRow["CTPS"] = xEmpregado.tNUM_CTPS.ToString(); 
                newRow["RG"] = xEmpregado.tNO_IDENTIDADE.ToString();
                newRow["Nascimento"] = xEmpregado.hDT_NASC.Day.ToString() + "/" + xEmpregado.hDT_NASC.Month.ToString() + "/" + xEmpregado.hDT_NASC.Year.ToString();
                newRow["Sexo"] = xEmpregado.tSEXO;
                newRow["Admissao"] = xEmpregado.hDT_ADM.Day.ToString() + "/" + xEmpregado.hDT_ADM.Month.ToString() + "/" + xEmpregado.hDT_ADM.Year.ToString() + "     " + xCNH ;
            }



            //EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(xEmpregado);
            EmpregadoFuncao empregadoFuncao = new EmpregadoFuncao(xIdEmpregadoFuncao);


            newRow["Funcao"] = empregadoFuncao.GetNomeFuncao();
            newRow["Setor"] = empregadoFuncao.GetNomeSetor();


            
            if (xCliente.InibirGHE == false)
                newRow["GHE"] = empregadoFuncao.GetGheEmpregado();
            else
                newRow["GHE"] = "" ;

            //pegar cnpj da classif.funcional atual
            empregadoFuncao.nID_EMPR.Find();
            newRow["CNPJ"] = empregadoFuncao.nID_EMPR.GetCnpj();
            newRow["Inscricao_Estadual"] = empregadoFuncao.nID_EMPR.IE;





            //carregar dados da clinica
            Clinica xClinica = new Clinica(xIdClinica);

        

            newRow["Clinica"] = xClinica.GetNomeEmpresa();

            newRow["End_Clinica"] = xClinica.GetEndereco().IdTipoLogradouro + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero.ToString() + " " + xClinica.GetEndereco().Complemento + " " + xClinica.GetEndereco().Bairro; ;
            newRow["Cid_Clinica"] = xClinica.GetEndereco().IdMunicipio;
            newRow["UF_Clinica"] = xClinica.GetEndereco().Uf;
            newRow["CEP_Clinica"] = xClinica.GetEndereco().Cep;

            string zObs = "";

            switch (xEmpregado.nIND_BENEFICIARIO)
            {
                case (int)TipoBeneficiario.BeneficiarioReabilitado:
                    zObs = "( Beneficiário Reabilitado )    ";
                    break;
                case (int)TipoBeneficiario.PortadorDeficiencia:
                    zObs = "( Portador de Deficiência habilitada )    ";
                    break;
                case (int)TipoBeneficiario.NaoAplicavel:
                    zObs = "";
                    break;
                default:
                    zObs = "";
                    break;
            }


            //if (xClinica.HorarioAtendimento.ToString().Trim() != "")
            //{
            //    if ( zObs.Trim()!= "" )
            //       newRow["Obs"] = zObs + "|   Horário de Atendimento: " + xClinica.HorarioAtendimento.ToString();
            //    else
            //        newRow["Obs"] = "Horário de Atendimento: " + xClinica.HorarioAtendimento.ToString();
            //}
            //else
            //{
            //    newRow["Obs"] = zObs + " ";
            //}


            if (xClinica.HorarioAtendimento.ToString().Trim() != "")
            {
                if (zObs.Trim() != "")
                    newRow["Obs"] = zObs + "|   Horário de Atendimento: " + xClinica.HorarioAtendimento.ToString() + System.Environment.NewLine + this.xObs2;
                else
                    newRow["Obs"] = "Horário de Atendimento: " + xClinica.HorarioAtendimento.ToString() + System.Environment.NewLine +  this.xObs2;
            }
            else
            {
                newRow["Obs"] = zObs + " " + System.Environment.NewLine + this.xObs2;
            }






            string xFone_Clinica = "";

            xFone_Clinica = xClinica.GetContatoTelefonico().DDD + " " + xClinica.GetContatoTelefonico().Numero;
            if (xFone_Clinica.ToString().Trim() != "")
                xFone_Clinica = xFone_Clinica + "    ";

            xFone_Clinica = xFone_Clinica + xClinica.GetContatoTelefonico2().DDD + " " + xClinica.GetContatoTelefonico2().Numero;
            if (xFone_Clinica.ToString().Trim() != "")
                xFone_Clinica = xFone_Clinica + "    ";

            xFone_Clinica = xFone_Clinica + xClinica.GetContatoTelefonico3().DDD + " " + xClinica.GetContatoTelefonico3().Numero;
            if (xFone_Clinica.ToString().Trim() != "")
                xFone_Clinica = xFone_Clinica + "    ";

            xFone_Clinica = xFone_Clinica + xClinica.GetContatoTelefonico4().DDD + " " + xClinica.GetContatoTelefonico4().Numero;
            if (xFone_Clinica.ToString().Trim() != "")
                xFone_Clinica = xFone_Clinica + "    ";

            xFone_Clinica = xFone_Clinica + xClinica.GetContatoTelefonico5().DDD + " " + xClinica.GetContatoTelefonico5().Numero;
            if (xFone_Clinica.ToString().Trim() != "")
                xFone_Clinica = xFone_Clinica + "    ";


            xFone_Clinica = "Tel.: " + xFone_Clinica;

            newRow["Obs2"] = xFone_Clinica + System.Environment.NewLine + this.xObs2;


            newRow["Mapa"] = Fotos.PathFoto_Uri(xClinica.Foto);


            //if ( xCliente.Bloquear_Avaliacao_Clinica == true)
            //{
            //    xExames = xExames.Replace("Avaliação Clínica", "");
            //    xExames2 = xExames2.Replace("Avaliação Clínica", "");
            //    xExames3 = xExames3.Replace("Avaliação Clínica", "");
            //    xExames4 = xExames4.Replace("Avaliação Clínica", "");
            //}

            //ESSENCE faz questão de Exame Clínico
            xExames = xExames.Replace("Avaliação Clínica", "Exame Clínico");
            xExames2 = xExames2.Replace("Avaliação Clínica", "Exame Clínico");
            xExames3 = xExames3.Replace("Avaliação Clínica", "Exame Clínico");
            xExames4 = xExames4.Replace("Avaliação Clínica", "Exame Clínico");

            if (xExames.Length > 2)
            {
                xExames = xExames.Substring(3);
            }

            if (xExames2.Length > 2)
            {
                xExames2 = xExames2.Substring(3);
            }
            
            if (xExames3.Length > 2)
            {
                xExames3 = xExames3.Substring(3);
            }

            if (xExames4.Length > 2)
            {
                xExames4 = xExames4.Substring(3);
            }

            xExames = xExames.Replace("/n", "\n");
            xExames2 = xExames2.Replace("/n", "\n");
            xExames3 = xExames3.Replace("/n", "\n");
            xExames4 = xExames4.Replace("/n", "\n");

 
            newRow["Exames"] = xExames;
            newRow["Exames2"] = xExames2;
            newRow["Exames3"] = xExames3;
            newRow["Exames4"] = xExames4;

            
            if (xTipo == "A")
            {
                newRow["Tipo_Admissional"] = "X";                
            }
            else if (xTipo == "D")
            {
                newRow["Tipo_Demissional"] = "X";             
            }
            else if (xTipo == "M")
            {
                newRow["Tipo_Mudanca"] = "X";                
            }
            else if (xTipo == "R")
            {
                newRow["Tipo_Retorno"] = "X";                
            }
            else if (xTipo == "P")
            {
                newRow["Tipo_Periodico"] = "X";                
            }
            else if (xTipo == "O")
            {
                newRow["Tipo_Outro"] = "X";
            }

            if (xImpDt == "N")
            {
                newRow["Data_Exame"] = "";
            }
            else
            {
                newRow["Data_Exame"] = xData_Exame;
            }

            newRow["Hora_Exame"] = xHora_Exame;


            string sPreparo = GetExamesComplementares_Preparo(xTipo, xEmpregado, xClinica);
            newRow["Preparo"] = sPreparo;



            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }



        private static string GetExamesComplementares_Preparo(string zTipo, Empregado empregado, Clinica zClinica)
        {
            List<Empregado.ExameComplementar> examesComplementares = new List<Empregado.ExameComplementar>();

            switch (zTipo)
            {
                case "A":
                    examesComplementares = empregado.ListExamesComplementaresIndicadosNaAdmissao();
                    break;
                case "D":
                    examesComplementares = empregado.ListExamesComplementaresIndicadosNaDemissao();
                    break;
                case "M":
                    examesComplementares = empregado.ListExamesComplementaresIndicadosNaMudancaFuncao();
                    break;
                case "P":
                    examesComplementares = empregado.ListExamesComplementaresIndicadosNoPeriodico();
                    break;
                case "R":
                    examesComplementares = empregado.ListExamesComplementaresIndicadosNoRetornoTrabalho();
                    break;
            }

            StringBuilder str = new StringBuilder();

            foreach (Empregado.ExameComplementar exameComplementar in examesComplementares)
            {

                ClinicaExameDicionario zClinicaExameDic = new ClinicaExameDicionario();
                zClinicaExameDic.Find(" IdClinica = " + zClinica.Id + " and IdExameDicionario = " + exameComplementar.IdExameDicionario);

                if (zClinicaExameDic.Preparo != null)
                {
                    if (zClinicaExameDic.Preparo.ToString().Trim() != "")
                    {
                        str.Append(System.Environment.NewLine + exameComplementar.NomeExame + ": " + System.Environment.NewLine + zClinicaExameDic.Preparo + System.Environment.NewLine);

                    }
                }
            }

            if (str.Length > 0)
                str.Remove(str.Length - 2, 2);

            //Clinico exameClinico = new Clinico(exame.Id);

            //if (exameClinico.DescExameCompl)
            //    str.Append(" (Todos os exames complementares indicados foram desconsiderados pela empresa)");

            return str.ToString();
        }

    }
}
