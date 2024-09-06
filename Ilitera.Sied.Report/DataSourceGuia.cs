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
    public class DataSourceGuia : DataSourceBase
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

        public DataSourceGuia(Cliente cliente, int xIdEmpregado, int xIdEmpresa, int xIdClinica, string xExames, string xExames2, string xExames3, string xExames4, string xData_Exame, string xHora_Exame, string xTipo, string xBasico, string xObs2, string xImpDt, int xIdEmpregadoFuncao)
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
            //this.xObs = xObs;
            this.xBasico = xBasico;
            this.xObs2 = xObs2;
            this.xImpDt = xImpDt;
            this.xIdEmpregadoFuncao = xIdEmpregadoFuncao;


            if (this.cliente.mirrorOld == null)
                this.cliente.Find();

         }


        public RptGuia_Nova GetReport_Nova()
        {
            RptGuia_Nova report = new RptGuia_Nova();
            report.SetDataSource(GetIntroducaoGuia_Nova());
            //report.OpenSubreport("FotoFachada").SetDataSource(DataSourceFotoFachada());
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }


        //public RptGuia GetReport()
        //{
        //    RptGuia report = new RptGuia();
        //    report.SetDataSource(GetIntroducaoGuia());
        //    //report.OpenSubreport("FotoFachada").SetDataSource(DataSourceFotoFachada());
        //    report.Refresh();

        //    SetTempoProcessamento(report);

        //    return report;
        //}

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

            //Essence: Prana Gestão Ocupacional Ltda. CNPJ 17.898.088 / 0001 - 03.Contato: (11) 4508 - 2332

            Cliente xCliente = new Cliente(xIdEmpresa);



            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("METODO") > 0) //Daiti
            {
                newRow["Frase"] = "Ilitera Método - 33.233.895/0001-63 - (11)98701-1223";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA")>0)
            {
                newRow["Frase"] = "Essence: Prana Gestão Ocupacional Ltda - 17.898.088/0001-03 - (11) 5504-9600";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().Trim() == "OPSA" || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("VIA") > 0)
            {
                //xCliente.IdGrupoEmpresa.Find();

                //if (xCliente.IdGrupoEmpresa.Descricao.ToUpper().IndexOf("NCR") >= 0)
                //{
                    newRow["Frase"] = "Ilitera UNO - Segurança, Saúde e Qualidade de Vida Ltda" + System.Environment.NewLine + "CNPJ  36.534.756/0001-68  Telefone 11 4249-4949";
                //}
                //else
                //{
                //    newRow["Frase"] = "Ilitera Via - Segurança, Saúde e Qualidade de Vida Ltda" + System.Environment.NewLine + "CNPJ  28.353.986/0001-00  Telefone 11 2356-7660";
                //}
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0) //Daiti
            {
                newRow["Frase"] = "Assessoria: Ilitera Daiiti Segurança, Saúde e Qualidade de Vida Ltda - 06.259.938/0001-07 - (11)2506-2054 / 2507-2054";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Mappas") > 0)
            {
                newRow["Frase"] = "Assessoria: Mappas-SST Segurança do Trabalho Ltda ME - 10.688.659/0001-36 - (12)3426-9186";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRIME") > 0)
            {
                newRow["Frase"] = "Ilitera Prime Assessoria em Segurança e Saúde Ltda - 33.876.636/0001-50  (11)4801-6300";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("QTECK") > 0)
            {
                newRow["Frase"] = "QTECK ILITERA. - 22.164.410/0001-00 - (11)4941-2288";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("COTIA") > 0)
            {
                newRow["Frase"] = "ILITERA COTIA E OESTE DA GRANDE SÃO PAULO -  CNPJ 25.371.101/0001-08 - (11)4551-5014";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
            {
                Juridica xJur = new Juridica();
                xJur.Find(xCliente.Id);

                if (xJur.Id != 0)
                {
                    if (xJur.Auxiliar == "RAMO" || xJur.Auxiliar == "GLOBAL2")
                    {
                        newRow["Frase"] = "RAMO ASS.EMPRESARIAL - CNPJ 28.495.277/0001-51 - (11) 93266-8098  faturamento2@globalsegmed.com.br";
                    }
                    //else if (xJur.Auxiliar == "GLOBAL2")
                    //{
                    //    newRow["Frase"] = "GLOBALSEGMED - CNPJ 43.640.229/0001-01 - (11) 96718-0199  faturamento2@globalsegmed.com.br";
                    //}
                    else
                    {
                        newRow["Frase"] = "GLOBALSEG - CNPJ 22.159.703/0001-08 - (11) 93266-8098  faturamento@globalsegmed.com.br";
                    }
                }
                else
                {
                    newRow["Frase"] = "GLOBALSEG - CNPJ 22.159.703/0001-08 - (11) 93266-8098  faturamento@globalsegmed.com.br";
                }

            }
            else if ( Ilitera.Data.SQLServer.EntitySQLServer.Server.ToUpper().IndexOf("FOCUS") < 0 && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SAFETY") > 0)
            {
                newRow["Frase"] = "ILITERA GRAFENO"; //  - (11)98913-4933 / (11)94504-4728";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.Server.ToUpper().IndexOf("FOCUS") > 0 && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SAFETY") > 0)
            {
                newRow["Frase"] = "ILITERA SAFETY - CNPJ 36.196.609/0001-25"; //  - (11)98913-4933 / (11)94504-4728";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SOMA") > 0)
            {
                newRow["Frase"] = "Soma Ilitera (Soma Segurança Ocupacional Ltda) - CNPJ: 04.170.948/0001-46 - (13)3229-1770";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SDTOURIN") > 0)
            {
                newRow["Frase"] = "ILITERA TURIN - CNPJ: 33.606.042/0001-20 - (11)2363-2245 / (11)94743-1615";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("FOX") > 0)
            {
                newRow["Frase"] = "ILITERA FOX - CNPJ: 25.400.875/0001-01 - (55)3422-4891";
            }
            else
            {
                //newRow["Frase"] = "Ilitera Via - Segurança, Saúde e Qualidade de Vida Ltda" + System.Environment.NewLine + "CNPJ  28.353.986/0001-00  Telefone 11 2356-7660";
                newRow["Frase"] = "Ilitera UNO - Segurança, Saúde e Qualidade de Vida Ltda" + System.Environment.NewLine + "CNPJ  36.534.756/0001-68  Telefone 11 4249-4949";
            }

            //carregar dados da empresa

            //Cliente xCliente = new Cliente(xIdEmpresa);

            xCliente.IdJuridicaPai.Find();

            if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != cliente.Id)
            {
                newRow["Empregador"] = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();
            }
            else
            {
                newRow["Empregador"] = xCliente.GetNomeEmpresa();
            }

             
            newRow["Endereco"] = xCliente.GetEndereco().IdTipoLogradouro + " " + xCliente.GetEndereco().Logradouro + " " + xCliente.GetEndereco().Numero.ToString() + " " + xCliente.GetEndereco().Complemento;
            newRow["Cidade"] = xCliente.GetEndereco().GetCidade();
            newRow["UF"] = xCliente.GetEndereco().Uf;
            newRow["CEP"] = xCliente.GetEndereco().Cep;
            newRow["CNPJ"] = xCliente.GetCnpj();
            newRow["Inscricao_Estadual"] = xCliente.IE;

            


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
                newRow["Admissao"] = xEmpregado.hDT_ADM.Day.ToString() + "/" + xEmpregado.hDT_ADM.Month.ToString() + "/" + xEmpregado.hDT_ADM.Year.ToString() + "       " + xCNH; 
            }



            //EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(xEmpregado);            
            EmpregadoFuncao empregadoFuncao = new EmpregadoFuncao(xIdEmpregadoFuncao);

            newRow["Funcao"] = empregadoFuncao.GetNomeFuncao();
            newRow["Setor"] = empregadoFuncao.GetNomeSetor();


            if (xCliente.InibirGHE == false)
                newRow["GHE"] = empregadoFuncao.GetGheEmpregado();
            else
                newRow["GHE"] = "";


            //carregar dados da clinica
            Clinica xClinica = new Clinica(xIdClinica);

        

            newRow["Clinica"] = xClinica.GetNomeEmpresa();

            newRow["End_Clinica"] = xClinica.GetEndereco().IdTipoLogradouro + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero.ToString() + " " + xClinica.GetEndereco().Complemento + " " + xClinica.GetEndereco().Bairro;
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


            if (xClinica.HorarioAtendimento.ToString().Trim() != "")
            {
                if (zObs.Trim() != "")
                    newRow["Obs"] = zObs + "|   Horário de Atendimento: " + xClinica.HorarioAtendimento.ToString();
                else
                    newRow["Obs"] = "Horário de Atendimento: " + xClinica.HorarioAtendimento.ToString();
            }
            else
            {
                newRow["Obs"] = zObs + " ";
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



            if (xCliente.Bloquear_Avaliacao_Clinica == true)
            {
                xExames = xExames.Replace("Avaliação Clínica", "");
                xExames2 = xExames2.Replace("Avaliação Clínica", "");
                xExames3 = xExames3.Replace("Avaliação Clínica", "");
                xExames4 = xExames4.Replace("Avaliação Clínica", "");
            }



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


            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0) //Daiti
            {

                xCliente.IdGrupoEmpresa.Find();

                if (xCliente.IdGrupoEmpresa.Descricao.ToUpper().IndexOf("UNION") >= 0 )
                {
                    newRow["Exames"] = "Exame Clínico";
                    newRow["Exames2"] = "Os exames complementares serão fornececidos pela empresa." + System.Environment.NewLine + "** Não liberar o ASO **";
                    newRow["Exames3"] = "";
                    newRow["Exames4"] = "";
                }
                else
                {
                    newRow["Exames"] = xExames;
                    newRow["Exames2"] = xExames2;
                    newRow["Exames3"] = xExames3;
                    newRow["Exames4"] = xExames4;
                }

            }
            else
            {
                newRow["Exames"] = xExames;
                newRow["Exames2"] = xExames2;
                newRow["Exames3"] = xExames3;
                newRow["Exames4"] = xExames4;
            }


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



            string sPreparo = GetExamesComplementares_Preparo(xTipo, xEmpregado, xClinica, xExames + " " + xExames2 + " " + xExames3 + " " + xExames4);
            newRow["Preparo"] = sPreparo;



            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }



        private DataSet GetIntroducaoGuia_Nova()
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

            //Essence: Prana Gestão Ocupacional Ltda. CNPJ 17.898.088 / 0001 - 03.Contato: (11) 4508 - 2332

            Cliente xCliente = new Cliente(xIdEmpresa);


            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("METODO") > 0) //Daiti
            {
                newRow["Frase"] = "Ilitera Método - 33.233.895/0001-63 - (11)98701-1223";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_PRO") > 0)
            {
                newRow["Frase"] = "Ilitera PRO Segurança Saúde e Qualidade de Vida Ltda - 40.017.926/0001-04";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
            {
                newRow["Frase"] = "Essence: Prana Gestão Ocupacional Ltda - 17.898.088/0001-03 - (11) 2344-4585";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().Trim() == "OPSA" || Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("VIA") > 0)
            {
                //newRow["Frase"] = "Assessoria: Ilitera Via - 28.353.986/0001-00 - (11) 2356-7660";
                //xCliente.IdGrupoEmpresa.Find();

                //if (xCliente.IdGrupoEmpresa.Descricao.ToUpper().IndexOf("NCR") >= 0)
                //{
                    newRow["Frase"] = "Ilitera UNO - Segurança, Saúde e Qualidade de Vida Ltda" + System.Environment.NewLine + "CNPJ  36.534.756/0001-68  Telefone 11 4249-4949";
                //}
                //else
                //{
                //    newRow["Frase"] = "Ilitera Via - Segurança, Saúde e Qualidade de Vida Ltda" + System.Environment.NewLine + "CNPJ  28.353.986/0001-00  Telefone 11 2356-7660";
                //}

            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0) //Daiti
            {
                newRow["Frase"] = "Assessoria: Ilitera Daiiti Segurança, Saúde e Qualidade de Vida Ltda - 06.259.938/0001-07 - (11)2506-2054 / 2507-2054";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Mappas") > 0)
            {
                newRow["Frase"] = "Assessoria: Mappas-SST Segurança do Trabalho Ltda ME - 10.688.659/0001-36 - (12)3426-9186";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRIME") > 0)
            {
                newRow["Frase"] = "Ilitera Prime Assessoria em Segurança e Saúde Ltda - 33.876.636/0001-50  (11)4801-6300";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("QTECK") > 0)
            {
                newRow["Frase"] = "QTECK ILITERA. - 22.164.410/0001-00 - (11)4941-2288";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("COTIA") > 0)
            {
                newRow["Frase"] = "ILITERA COTIA E OESTE DA GRANDE SÃO PAULO -  CNPJ 25.371.101/0001-08 - (11)4551-5014";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("GLOBAL") > 0)
            {
                Juridica xJur = new Juridica();
                xJur.Find(xCliente.Id);

                if (xJur.Id != 0)
                {
                    if (xJur.Auxiliar == "RAMO" || xJur.Auxiliar == "GLOBAL2")
                    {
                        newRow["Frase"] = "RAMO ASS.EMPRESARIAL - CNPJ 28.495.277/0001-51 - (11) 93266-8098  faturamento2@globalsegmed.com.br";
                    }
                    //else if (xJur.Auxiliar == "GLOBAL2")
                    //{
                    //    newRow["Frase"] = "GLOBALSEGMED - CNPJ 43.640.229/0001-01 - (11) 96718-0199  faturamento2@globalsegmed.com.br";
                    //}
                    else
                    {
                        newRow["Frase"] = "GLOBALSEG - CNPJ 22.159.703/0001-08 - (11) 93266-8098  faturamento@globalsegmed.com.br";
                    }
                }
                else
                {
                    newRow["Frase"] = "GLOBALSEG - CNPJ 22.159.703/0001-08 - (11) 93266-8098  faturamento@globalsegmed.com.br";
                }

            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("FOCUS") < 0 && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SAFETY") > 0)
            {
                newRow["Frase"] = "ILITERA GRAFENO"; //  - (11)98913-4933 / (11)94504-4728";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("FOCUS") > 0 && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SAFETY") > 0)
            {
                newRow["Frase"] = "ILITERA SAFETY - CNPJ 36.196.609/0001-25"; //  - (11)98913-4933 / (11)94504-4728";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SOMA") > 0)
            {
                newRow["Frase"] = "Soma Ilitera (Soma Segurança Ocupacional Ltda) - CNPJ: 04.170.948/0001-46 - (13)3229-1770";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("SDTOURIN") > 0)
            {
                newRow["Frase"] = "ILITERA TURIN - CNPJ: 33.606.042/0001-20 - (11)2363-2245 / (11)94743-1615";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("FOX") > 0)
            {
                newRow["Frase"] = "ILITERA FOX - CNPJ: 25.400.875/0001-01 - (55)3422-4891";
            }
            else
            {
                //newRow["Frase"] = "Ilitera Via - Segurança, Saúde e Qualidade de Vida Ltda" + System.Environment.NewLine + "CNPJ  28.353.986/0001-00  Telefone 11 2356-7660";
                newRow["Frase"] = "Ilitera UNO - Segurança, Saúde e Qualidade de Vida Ltda" + System.Environment.NewLine + "CNPJ  36.534.756/0001-68  Telefone 11 4249-4949";
            }

            //carregar dados da empresa

            //Cliente xCliente = new Cliente(xIdEmpresa);

            xCliente.IdJuridicaPai.Find();

            if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != cliente.Id)
            {
                newRow["Empregador"] = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();
            }
            else
            {
                newRow["Empregador"] = xCliente.GetNomeEmpresa();
            }


            newRow["Endereco"] = xCliente.GetEndereco().IdTipoLogradouro + " " + xCliente.GetEndereco().Logradouro + " " + xCliente.GetEndereco().Numero.ToString() + " " + xCliente.GetEndereco().Complemento;
            newRow["Cidade"] = xCliente.GetEndereco().GetCidade().Trim() + "-" + xCliente.GetEndereco().Uf.Trim() + "     " + xCliente.GetEndereco().Cep;
            newRow["UF"] = "";  // xCliente.GetEndereco().Uf;
            newRow["CEP"] = "";  // xCliente.GetEndereco().Cep;            
            newRow["CNPJ"] = "CNPJ " + xCliente.GetCnpj();

            if (xCliente.IE.Trim() != "")
                newRow["Inscricao_Estadual"] = "I.E. " + xCliente.IE;
            else
                newRow["Inscricao_Estadual"] = "";



            //carregar dados do funcionario
            Empregado xEmpregado = new Empregado(xIdEmpregado);

            string xPCD = "";

            if ( xEmpregado.nIND_BENEFICIARIO != null)
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
                newRow["CTPS"] = "CTPS " + xEmpregado.tNUM_CTPS.ToString();
                newRow["RG"] = "RG " + xEmpregado.tNO_IDENTIDADE.ToString();
                newRow["Nascimento"] = "Nascimento " + xEmpregado.hDT_NASC.Day.ToString() + "/" + xEmpregado.hDT_NASC.Month.ToString() + "/" + xEmpregado.hDT_NASC.Year.ToString();
                newRow["Sexo"] = "Sexo " + xEmpregado.tSEXO;
                newRow["Admissao"] = "Admissão " + xEmpregado.hDT_ADM.Day.ToString() + "/" + xEmpregado.hDT_ADM.Month.ToString() + "/" + xEmpregado.hDT_ADM.Year.ToString() + "    " + xCNH; 
            }



            //EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(xEmpregado);
            EmpregadoFuncao empregadoFuncao = new EmpregadoFuncao(xIdEmpregadoFuncao);

            newRow["Funcao"] = "Função " + empregadoFuncao.GetNomeFuncao();
            newRow["Setor"] = "Setor " + empregadoFuncao.GetNomeSetor();


            if (xCliente.InibirGHE == false)
                newRow["GHE"] = empregadoFuncao.GetGheEmpregado();
            else
                newRow["GHE"] = "";


            //carregar dados da clinica
            Clinica xClinica = new Clinica(xIdClinica);



            newRow["Clinica"] = xClinica.GetNomeEmpresa();

            newRow["End_Clinica"] = xClinica.GetEndereco().IdTipoLogradouro + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero.ToString() + " " + xClinica.GetEndereco().Complemento + " " + xClinica.GetEndereco().Bairro;
            newRow["Cid_Clinica"] = xClinica.GetEndereco().IdMunicipio + "-" + xClinica.GetEndereco().Uf + "      " + xClinica.GetEndereco().Cep;
            newRow["UF_Clinica"] = ""; // xClinica.GetEndereco().Uf;
            newRow["CEP_Clinica"] = (xClinica.GetContatoTelefonico().DDD + " " + xClinica.GetContatoTelefonico().Numero).Trim();  // xClinica.GetEndereco().Cep;




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

           // newRow["Obs2"] = xFone_Clinica + System.Environment.NewLine + this.xObs2;


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


            if (xClinica.HorarioAtendimento.ToString().Trim() != "")
            {
                if (zObs.Trim() != "")
                    newRow["Obs"] = zObs + "|   Horário de Atendimento: " + xClinica.HorarioAtendimento.ToString() + System.Environment.NewLine + xFone_Clinica + System.Environment.NewLine + this.xObs2; 
                else
                    newRow["Obs"] = "Horário de Atendimento: " + xClinica.HorarioAtendimento.ToString() + System.Environment.NewLine + xFone_Clinica + System.Environment.NewLine + this.xObs2;
            }
            else
            {
                newRow["Obs"] = zObs + " " + System.Environment.NewLine + xFone_Clinica + System.Environment.NewLine + this.xObs2;
            }




           


            newRow["Mapa"] = Fotos.PathFoto_Uri(xClinica.Foto);

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



            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0) //Daiti
            {

                xCliente.IdGrupoEmpresa.Find();

                if (xCliente.IdGrupoEmpresa.Descricao.ToUpper().IndexOf("UNION") >= 0)
                {
                    newRow["Exames"] = "Exame Clínico";
                    newRow["Exames3"] = "Os exames complementares serão fornececidos pela empresa." + System.Environment.NewLine + "** Não liberar o ASO **";
                    newRow["Exames2"] = "";
                    newRow["Exames4"] = "";
                }
                else
                {
                    newRow["Exames"] = xExames;
                    newRow["Exames2"] = xExames2;
                    newRow["Exames3"] = xExames3;
                    newRow["Exames4"] = xExames4;
                }

            }
            else
            {
                newRow["Exames"] = xExames;
                newRow["Exames2"] = xExames2;
                newRow["Exames3"] = xExames3;
                newRow["Exames4"] = xExames4;
            }




        


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



            string sPreparo = GetExamesComplementares_Preparo(xTipo, xEmpregado, xClinica, xExames + " " + xExames2 + " " + xExames3 + " " + xExames4);
            newRow["Preparo"] = sPreparo;



            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }





        private static string GetExamesComplementares_Preparo(string zTipo, Empregado empregado, Clinica zClinica, string zExames_Guia)
        {

            StringBuilder str = new StringBuilder();

            string[] stringSeparators4 = new string[] { "\n" };
            string[] result4;

            result4 = zExames_Guia.Split(stringSeparators4, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in result4)
            {
                if (s.Trim() != "")
                {
                    //procurar Id do Exame
                    ExameDicionario xDic = new ExameDicionario();
                    xDic.Find("Nome = '" + s.Trim() + "' ");

                    if (xDic.Id != 0)
                    {

                        ClinicaExameDicionario zClinicaExameDic = new ClinicaExameDicionario();
                        zClinicaExameDic.Find(" IdClinica = " + zClinica.Id + " and IdExameDicionario = " + xDic.Id);

                        if (zClinicaExameDic.Preparo != null)
                        {
                            if (zClinicaExameDic.Preparo.ToString().Trim() != "")
                            {
                                str.Append(System.Environment.NewLine + xDic.Nome + ": " + System.Environment.NewLine + zClinicaExameDic.Preparo + System.Environment.NewLine);

                            }
                        }


                    }

                }
            }

            //List<Empregado.ExameComplementar> examesComplementares = new List<Empregado.ExameComplementar>();

            //switch (zTipo)
            //{
            //    case "A":
            //        examesComplementares = empregado.ListExamesComplementaresIndicadosNaAdmissao();
            //        break;
            //    case "D":
            //        examesComplementares = empregado.ListExamesComplementaresIndicadosNaDemissao();
            //        break;
            //    case "M":
            //        examesComplementares = empregado.ListExamesComplementaresIndicadosNaMudancaFuncao();
            //        break;
            //    case "P":
            //        examesComplementares = empregado.ListExamesComplementaresIndicadosNoPeriodico();
            //        break;
            //    case "R":
            //        examesComplementares = empregado.ListExamesComplementaresIndicadosNoRetornoTrabalho();
            //        break;
            //}

            //StringBuilder str = new StringBuilder();

            //foreach (Empregado.ExameComplementar exameComplementar in examesComplementares)
            //{

            //    ClinicaExameDicionario zClinicaExameDic = new ClinicaExameDicionario();
            //    zClinicaExameDic.Find(" IdClinica = " + zClinica.Id + " and IdExameDicionario = " + exameComplementar.IdExameDicionario);

            //    if (zExames_Guia.ToUpper().IndexOf(zClinicaExameDic.ToString().ToUpper().Trim()) >= 0)
            //    {
            //        if (zClinicaExameDic.Preparo != null)
            //        {

            //            if (zClinicaExameDic.Preparo.ToString().Trim() != "")
            //            {
            //                str.Append(System.Environment.NewLine + exameComplementar.NomeExame + ": " + System.Environment.NewLine + zClinicaExameDic.Preparo + System.Environment.NewLine);

            //            }
            //        }
            //    }
            //}

            if (str.Length > 0)
                str.Remove(str.Length - 2, 2);

            //Clinico exameClinico = new Clinico(exame.Id);

            //if (exameClinico.DescExameCompl)
            //    str.Append(" (Todos os exames complementares indicados foram desconsiderados pela empresa)");

            return str.ToString();
        }


    }
}
