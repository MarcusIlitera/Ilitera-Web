using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Ilitera.Opsa.Data;
using System.Data;
using System.IO;
using System.Net;
using System.Xml;
using Ilitera.Common;
using System.Net.Mail;
using Ilitera.Data;

using System.Text;
using System.Collections;
using System.Xml.Linq;


using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using Ilitera.Sied.Report;
using Ilitera.PCMSO.Report;


namespace Ilitera.Net
{
    /// <summary>
    /// Summary description for Comunicacao1
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [WebService(Namespace = "https://www.ilitera.net.br/essence_hom")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService()]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Comunicacao1 : System.Web.Services.WebService
    {

        //chamar via URL - http://localhost:46870/comunicacao.asmx/HelloWorld

        //  https://www.ilitera.net.br/essence_hom/comunicacao.asmx?op=HelloWorld




        //[WebMethod]
        //public string HelloWorld(string xTeste, string xTeste2)
        //{
        //    return "Hello World" + " "+ xTeste + " " + xTeste2;
        //}

        //exemplos

        //  https://stackoverflow.com/questions/31374512/post-parameters-during-asmx-web-service-call



        //outro exemplo
        //https://stackoverflow.com/questions/11450836/http-post-using-web-service


        //para retornar uM XML, por exemplo, com arquivos de atestados e CATs -  usar na chamada     public XMLDocument ....
        // https://stackoverflow.com/questions/2784818/how-do-i-return-pure-xml-from-asmx-web-service


        [WebMethod]
        public string Dados_Colaborador(string Usuario, string Senha, string Nome, string Sexo, string RG, string Matricula, string PIS, string CPF, string Nascimento, 
                                        string CTPS, string Serie, string UF_CTPS, string Admissao, string Apelido, string Demissao, string Email, 
                                        string Inicio_Funcao, string Funcao, string Setor, string LocalTrabalho, string Termino_Funcao, string CentroCusto, string Cargo, string Jornada )
        {
            string zStatus = "";
            string zAcao = "";

            //(1) primeira checagem básica de parâmetros
            //(2) checagem durante processamento
            
            if (Usuario.Trim().Length<3 || Usuario.Trim().Length>12)
            {
                zStatus = "Usuário fornecido inválido (1)";
            }

            if ( Senha.Trim().Length<3 || Senha.Trim().Length > 8)
            {
                zStatus = zStatus + " / " + "Senha fornecida inválida (1)";
            }

            if (Nome.Trim().Length < 3 || Nome.Trim().Length > 80)
            {
                zStatus = zStatus + " / " + "Nome fornecido inválido (1)";
            }

            if (Sexo.Trim().Length != 1)
            {
                zStatus = zStatus + " / " + "Sexo fornecido inválido (1)";
            }
            else
            {
                if ( Sexo.Trim()!="M" && Sexo.Trim()!="F" )
                {
                    zStatus = zStatus + " / " + "Sexo fornecido inválido (1)";
                }
            }

            if ( RG.Trim()!="")  // não obrigatório
            {
                if ( RG.Trim().Length<4 || RG.Trim().Length>16)
                {
                    zStatus = zStatus + " / " + "RG fornecido inválido (1)";
                }
            }

            if (Matricula.Trim() != "")  // não obrigatório
            {
                if (Matricula.Trim().Length < 2 || Matricula.Trim().Length > 50)
                {
                    zStatus = zStatus + " / " + "Matrícula fornecida inválida (1)";
                }
            }

            if (PIS.Trim() != "")  // não obrigatório
            {
                if (PIS.Trim().Length < 2 || PIS.Trim().Length>14)
                {
                    zStatus = zStatus + " / " + "PIS fornecido inválido (1)";
                }
                else
                {
                    //verificar se é numérico
                    Int64 number;
                    bool result = Int64.TryParse(PIS.Trim(), out number);
                    if (!result)
                    {
                        zStatus = zStatus + " / " + "PIS fornecido deve ser numérico (1)";
                    }
                }
            }

            if (CPF.Trim().Length < 8 || CPF.Trim().Length > 11 )
            {
                zStatus = zStatus + " / " + "CPF fornecido inválido (1)";
            }
            else
            {
                //verificar se é numérico
                Int64 number;
                bool result = Int64.TryParse(CPF.Trim(), out number);
                if (!result)
                {
                    zStatus = zStatus + " / " + "CPF fornecido deve ser numérico (1)";
                }
                else
                {
                    if (!Pessoa.VeriricaCPF(CPF.Trim()))
                    {
                        zStatus = zStatus + " / " + "CPF fornecido não-válido (1)";
                    }
                }
            }


            if (Nascimento.Trim().Length != 8)
            {
                zStatus = zStatus + " / " + "Data Nascimento fornecida inválida (1)";
            }
            else
            {
                //checar YYYYMMDD
                if ( Validar_Data(Nascimento.Trim())==false)
                {
                    zStatus = zStatus + " / " + "Data Nascimento em formato inválido ( deve ser YYYYMMDD ) (1)";
                }
            }

            if (CTPS.Trim() != "")  // não obrigatório
            {
                if (CTPS.Trim().Length < 2 || CTPS.Trim().Length > 15)
                {
                    zStatus = zStatus + " / " + "CTPS fornecido inválido (1)";
                }
            }

            if (Serie.Trim() != "")  // não obrigatório
            {
                if (Serie.Trim().Length < 2 || Serie.Trim().Length > 10)
                {
                    zStatus = zStatus + " / " + "Série do CTPS fornecida inválida (1)";
                }
            }


            if (UF_CTPS.Trim() != "")  // não obrigatório
            {
                if (UF_CTPS.Trim().Length != 2)
                {
                    zStatus = zStatus + " / " + "UF do CTPS fornecido inválido (1)";
                }
            }

            if (Admissao.Trim().Length != 8)
            {
                zStatus = zStatus + " / " + "Data de Admissão fornecida inválida (1)";
            }
            else
            {
                //checar YYYYMMDD
                if (Validar_Data(Admissao.Trim()) == false)
                {
                    zStatus = zStatus + " / " + "Data de Admissão em formato inválido ( deve ser YYYYMMDD ) (1)";
                }
            }            

            if (Apelido.Trim() != "")  // não obrigatório
            {
                if (Apelido.Trim().Length < 2 || Apelido.Trim().Length > 50)
                {
                    zStatus = zStatus + " / " + "Apelido fornecido inválido (1)";
                }
            }

            if (Demissao.Trim() != "")  // não obrigatório
            {
                if (Demissao.Trim().Length != 8)
                {
                    zStatus = zStatus + " / " + "Data de Demissão fornecida inválida (1)";
                }
                else
                {
                    //checar YYYYMMDD
                    if (Validar_Data(Demissao.Trim()) == false)
                    {
                        zStatus = zStatus + " / " + "Data de Demissão em formato inválido ( deve ser YYYYMMDD ) (1)";
                    }
                }
            }

            if (Email.Trim() != "")  // não obrigatório
            {
                if (Email.Trim().Length < 5 || Email.Trim().Length > 60)
                {
                    zStatus = zStatus + " / " + "E-mail fornecido inválido (1)";
                }
            }


            if (Inicio_Funcao.Trim() != "")  // não obrigatório
            {
                if (Inicio_Funcao.Trim().Length != 8)
                {
                    zStatus = zStatus + " / " + "Data de Início da classif.Funcional fornecida inválida (1)";
                }
                else
                {
                    //checar YYYYMMDD
                    if (Validar_Data(Inicio_Funcao.Trim()) == false)
                    {
                        zStatus = zStatus + " / " + "Data de Início da classif.Funcional em formato inválido ( deve ser YYYYMMDD ) (1)";
                    }
                }
            }

            if (Funcao.Trim() != "")  // não obrigatório
            {
                if (Funcao.Trim().Length < 2 || Funcao.Trim().Length > 100)
                {
                    zStatus = zStatus + " / " + "Função fornecida inválida (1)";
                }
            }

            if (Setor.Trim() != "")  // não obrigatório
            {
                if (Setor.Trim().Length < 2 || Setor.Trim().Length > 100)
                {
                    zStatus = zStatus + " / " + "Setor fornecido inválido (1)";
                }
            }

            if (LocalTrabalho.Trim() != "")  // não obrigatório
            {
                if (LocalTrabalho.Trim().Length < 2 || LocalTrabalho.Trim().Length > 200)
                {
                    zStatus = zStatus + " / " + "Local de trabalho fornecido inválido (1)";
                }
            }

            if (Termino_Funcao.Trim() != "")  // não obrigatório
            {
                if (Termino_Funcao.Trim().Length != 8)
                {
                    zStatus = zStatus + " / " + "Data de Término da classif.Funcional fornecida inválida (1)";
                }
                else
                {
                    //checar YYYYMMDD
                    if (Validar_Data(Termino_Funcao.Trim()) == false)
                    {
                        zStatus = zStatus + " / " + "Data de Término da classif.Funcional em formato inválido ( deve ser YYYYMMDD ) (1)";
                    }
                }
            }


            if (CentroCusto.Trim().Length < 2 || CentroCusto.Trim().Length > 20)
            {
                zStatus = zStatus + " / " + "Centro de Custo fornecido inválido (1)";
            }


            if (Cargo.Trim() != "")  // não obrigatório
            {
                if (Cargo.Trim().Length < 2 || Cargo.Trim().Length > 100)
                {
                    zStatus = zStatus + " / " + "Cargo fornecido inválido (1)";
                }
            }

            if (Jornada.Trim() != "")  // não obrigatório
            {
                if (Jornada.Trim().Length < 2 || Jornada.Trim().Length > 100)
                {
                    zStatus = zStatus + " / " + "Jornada fornecida inválido (1)";
                }
            }


            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            //validar Centro de Custo fornecido
            Pessoa rEmpresa = new Ilitera.Common.Pessoa();



            if (zStatus == "")
            {

                //validar Usuário e senha
                Ilitera.Data.eSocial xUser = new Ilitera.Data.eSocial();
                Int32 xCodUsuario = xUser.Retornar_Codigo_Usuario(Usuario, Senha);

                if (xCodUsuario == 0)
                {
                    zStatus = zStatus + " / " + "Usuário/Senha inválidos (2)";
                }
            }


            if (zStatus == "")
            {

                //qual condição utilizar ??
                string rSelect = " NomeAbreviado like '%" + CentroCusto + "%'";  //dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

                rEmpresa.Find(rSelect);

                if (rEmpresa.Id == 0)
                {
                    zStatus = zStatus + " / " + "Centro de Custo fornecido não localizado (2)";
                }

            }


            //começar tratamento do registro
            //verificar se é uma simples inserção, atualização cadastral, nova classif.funcional ou demissão e chamar funções para cada uma delas.

            if (zStatus == "")
            {
                //checar se CPF em centro de custo já está cadastrado e ativo 
                Empregado rColaborador2 = new Empregado();
                rColaborador2.Find(" tNo_CPF = '" + CPF.Trim() + "' and hDt_Dem is null  and nId_Empr in ( select idpessoa from Juridica (nolock) where IdGrupoEmpresa in ( select  IdGrupoEmpresa from Juridica (nolock) where idpessoa = " + rEmpresa.Id.ToString() + " ) and IdPessoa in ( select IdPessoa from Pessoa (nolock) where IsInativo = 0 ) ) ");



                if (rColaborador2.Id == 0)   //realizar inserção do colaborador
                {

                    if (Demissao.Trim() != "")
                    {
                        zStatus = zStatus + " / " + "Colaborador não foi localizado como ativo no sistema, não foi possível inativá-lo (2).";
                    }

                    try
                    {

                        Cliente rCliente = new Cliente();

                        if (zStatus == "")
                        {

                            rCliente.Find(" IdCliente = " + rEmpresa.Id.ToString());

                            if (rCliente.Id == 0)
                            {
                                zStatus = zStatus + " / " + "Erro: Problema na localização de Cliente (2)";
                            }
                        }



                        if (zStatus == "")
                        {

                            //criar registro de colaborador
                            Empregado rColaborador = new Empregado();
                            
                            rColaborador.tNO_EMPG = Nome;
                            rColaborador.tNO_CPF = CPF;

                            if (PIS != "")
                                rColaborador.nNO_PIS_PASEP = System.Convert.ToInt64(PIS);

                            rColaborador.tNUM_CTPS = CTPS;
                            rColaborador.tSER_CTPS = Serie;
                            rColaborador.tUF_CTPS = UF_CTPS;
                            rColaborador.tNO_IDENTIDADE = RG;
                            rColaborador.tSEXO = Sexo;

                            string zNascimento = Nascimento.Substring(6, 2) + "/" + Nascimento.Substring(4, 2) + "/" + Nascimento.Substring(0, 3);
                            rColaborador.hDT_NASC = System.Convert.ToDateTime(zNascimento, ptBr);

                            rColaborador.teMail = Email;
                            //rColaborador.teMail_Resp = xeMail_Responsavel;

                            string zAdmissao = Admissao.Substring(6, 2) + "/" + Admissao.Substring(4, 2) + "/" + Admissao.Substring(0, 3);
                            rColaborador.hDT_ADM = System.Convert.ToDateTime(Admissao, ptBr);
                            rColaborador.tCOD_EMPR = Matricula;

                            RegimeRevezamento xRegimeRevezamento = new RegimeRevezamento();
                            rColaborador.nID_REGIME_REVEZAMENTO = xRegimeRevezamento;


                            rColaborador.nID_EMPR = rCliente;
                            rColaborador.Save();

                            if (rColaborador.Id == 0)
                            {
                                zStatus = zStatus + " / " + "Problema na salva do colaborador (2)";
                            }
                            else
                            {
                                //criar classif.funcional

                                //ver se função existe
                                Funcao rFuncao = new Funcao();
                                rFuncao.Find(" IdCliente = " + rEmpresa.Id.ToString() + " and NomeFuncao = '" + Funcao + "' ");

                                if (rFuncao.Id == 0)
                                {
                                    rFuncao = new Funcao();
                                    rFuncao.NumeroCBO = "";
                                    rFuncao.NomeFuncao = Funcao;
                                    rFuncao.IdCliente = rCliente;
                                    rFuncao.DescricaoFuncao = "";
                                    rFuncao.Save();
                                }

                                if (rFuncao.Id == 0)
                                {
                                    zStatus = zStatus + " / " + "Problema na salva da Funcao (2)";
                                    rColaborador.Delete();
                                }


                                Cargo rCargo = new Cargo();

                                if (zStatus == "")
                                {

                                    //ver se cargo existe
                                    if (Cargo != "")
                                    {

                                        rCargo.Find(" nId_Empr = " + rEmpresa.Id.ToString() + " and tNo_Cargo = '" + Cargo + "' ");

                                        if (rCargo.Id == 0)
                                        {
                                            rCargo = new Cargo();
                                            rCargo.tNO_CARGO = Cargo;
                                            rCargo.nID_EMPR = rCliente;
                                            rCargo.Save();
                                        }


                                        if (rCargo.Id == 0)
                                        {
                                            zStatus = zStatus + " / " + "Problema na salva do Cargo (2)";
                                            rColaborador.Delete();
                                        }

                                    }

                                }



                                Setor rSetor = new Setor();

                                if (zStatus == "")
                                {
                                    //ver se setor existe
                                    rSetor.Find(" nId_Empr = " + rEmpresa.Id.ToString() + " and tNO_STR_EMPR = '" + Setor + "' ");

                                    if (rSetor.Id == 0)
                                    {
                                        rSetor = new Setor();
                                        rSetor.tNO_STR_EMPR = Setor;
                                        rSetor.nID_EMPR = rCliente;
                                        rSetor.Save();
                                    }

                                    if (rSetor.Id == 0)
                                    {
                                        zStatus = zStatus + " / " + "Problema na salva do Setor (2)";
                                        rColaborador.Delete();
                                    }


                                }


                                if (zStatus == "")
                                {
                                    EmpregadoFuncao xEmprFunc = new EmpregadoFuncao();


                                    TempoExposicao xTempo = new TempoExposicao();
                                    xTempo.Find(" tHora_Extenso_Semanal = '44 horas semanais' and thora_extenso = '08h48min' ");

                                    Ghe xGhe = new Ghe();
                                    xEmprFunc.nID_GHE_AE = xGhe;

                                    ImportacaoAutomatica xImp = new ImportacaoAutomatica();
                                    xEmprFunc.nID_IMPORTACAO_AUTOMATICA = xImp;

                                    xEmprFunc.nID_TEMPO_EXP = xTempo;
                                    xEmprFunc.nID_SETOR = rSetor;
                                    xEmprFunc.nID_CARGO = rCargo;
                                    xEmprFunc.nID_FUNCAO = rFuncao;
                                    xEmprFunc.nID_EMPREGADO = rColaborador;

                                    string zInicio = Inicio_Funcao.Substring(6, 2) + "/" + Inicio_Funcao.Substring(4, 2) + "/" + Inicio_Funcao.Substring(0, 3);
                                    xEmprFunc.hDT_INICIO = System.Convert.ToDateTime(zAdmissao, ptBr);

                                    xEmprFunc.nID_EMPR = rCliente;
                                    xEmprFunc.Save();

                                    if (xEmprFunc.Id == 0)
                                    {
                                        zStatus = zStatus + " / " + "Problema na salva da Classif.Funcional (2)";
                                        rColaborador.Delete();
                                    }



                                }

                            }

                        }

                        zAcao = "Inserção do colaborador no sistema.";
                    }
                    catch ( Exception ex)
                    {
                        zStatus = zStatus + " / " + "Erro na inserção do colaborador (2): " + ex.Message;
                    }
                }  // fim inserção
                else
                {
                    //demissão
                    if (Demissao.Trim() != "")
                    {
                        //fazer inativação
                        try
                        {                            
                            string zDemissao = Demissao.Substring(6, 2) + "/" + Demissao.Substring(4, 2) + "/" + Demissao.Substring(0, 3);
                            rColaborador2.hDT_DEM = System.Convert.ToDateTime(zDemissao, ptBr);

                            rColaborador2.Save();

                            zAcao = "Inativação do colaborador no sistema.";
                        }
                        catch (Exception ex)
                        {
                            zStatus = zStatus + " / " + "Erro na inativação do colaborador (2): " + ex.Message;
                        }
                    }
                    else
                    {

                        //atualização cadastral 
                        try
                        {
                            rColaborador2.tNO_EMPG = Nome;


                            if (PIS != "")
                                rColaborador2.nNO_PIS_PASEP = System.Convert.ToInt64(PIS);

                            rColaborador2.tNUM_CTPS = CTPS;
                            rColaborador2.tSER_CTPS = Serie;
                            rColaborador2.tUF_CTPS = UF_CTPS;
                            rColaborador2.tNO_IDENTIDADE = RG;
                            rColaborador2.tSEXO = Sexo;

                            string zNascimento = Nascimento.Substring(6, 2) + "/" + Nascimento.Substring(4, 2) + "/" + Nascimento.Substring(0, 3);
                            rColaborador2.hDT_NASC = System.Convert.ToDateTime(zNascimento, ptBr);

                            rColaborador2.teMail = Email;
                            //rColaborador.teMail_Resp = xeMail_Responsavel;

                            string zAdmissao = Admissao.Substring(6, 2) + "/" + Admissao.Substring(4, 2) + "/" + Admissao.Substring(0, 3);
                            rColaborador2.hDT_ADM = System.Convert.ToDateTime(Admissao, ptBr);
                            rColaborador2.tCOD_EMPR = Matricula;

                            RegimeRevezamento xRegimeRevezamento = new RegimeRevezamento();
                            rColaborador2.nID_REGIME_REVEZAMENTO = xRegimeRevezamento;

                            rColaborador2.Save();

                            zAcao = "Atualização cadastral do colaborador no sistema.";
                        }
                        catch (Exception ex)
                        {
                            zStatus = zStatus + " / " + "Erro na atualização cadastral do colaborador (2): " + ex.Message;
                        }


                        
                        //criar nova classif.funcional  
                        //ver se campos estão preenchidos 



                        zAcao = zAcao + " / Atualização cadastral do colaborador no sistema.";

                    }

                }






            }



            if (zStatus == "")
            {
                zStatus = "Processamento Finalizado - " + zAcao;
            }

            return zStatus;

        }


        protected Boolean Validar_Data(string zData)
        {
            int zDia = 0;
            int zMes = 0;
            int zAno = 0;

            string Validar;
            bool isNumerical;
            int myInt;


            if (zData.Length != 8)
            {
                return false;
            }

            Validar = zData.Substring(0, 4);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                return false;
            }


            Validar = zData.Substring(4, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                return false;
            }


            Validar = zData.Substring(6, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                return false;
            }



            zDia = System.Convert.ToInt32(zData.Substring(6, 2));
            zMes = System.Convert.ToInt32(zData.Substring(4, 2));
            zAno = System.Convert.ToInt32(zData.Substring(0, 4));

            if (zAno < 1900 || zAno > 2025)
            {
                return false;
            }

            if (zMes < 1 || zMes > 12)
            {
                return false;
            }

            if (zMes == 1 || zMes == 3 || zMes == 5 || zMes == 7 || zMes == 8 || zMes == 10 || zMes == 12)
            {
                if (zDia < 1 || zDia > 31)
                {
                    return false;
                }
            }
            else if (zMes == 4 || zMes == 6 || zMes == 9 || zMes == 11)
            {
                if (zDia < 1 || zDia > 30)
                {
                    return false;
                }
            }
            else
            {
                if (zDia < 1 || zDia > 29)
                {
                    return false;
                }
            }

            return true;

        }


        //[WebMethod]
        //public string Executar_Convocacao_Old(string CNPJ,string Empresa, string CodUsuario, string TipoExame, string Colaborador, string Data, string ID, string IdConvocacao )
        //{

        //    ClinicaCliente rClinica = new ClinicaCliente();
        //    Empregado rEmpregado = new Empregado();
        //    Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
        //    int zId = 0;


        //    string rSelect = "";

        //    string txt_Status = "";


        //    //arquivo texto com retorno do processamento - apenas corpo para poder enviar na chamada do dadosempregado_auto
        //    string xArq = System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim();


        //    try
        //    {               


        //        rSelect = " NomeAbreviado = '" + Empresa + "' and dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

        //        //pegar Id Empresa                    
        //        rEmpresa.Find(rSelect);

        //        //se não achar empresa,  emitir retorno avisando
        //        if (rEmpresa.Id == 0)
        //        {
        //            txt_Status = txt_Status + "Erro: Empresa não localizada ( " + Empresa + "  )" + System.Environment.NewLine;
        //        }

        //        //quando for convocação ( mailing ) no último irá o link para chamar comunicação
        //        //pode ir com um ID pronto




        //        //pegar Id Colaborador                       
        //        rEmpregado.Find(" tNo_Empg='" + Colaborador + "'  and nId_Empr = " + rEmpresa.Id.ToString());

        //        //se não achar empregado,  emitir retorno avisando
        //        if (rEmpregado.Id == 0)
        //        {
        //            txt_Status = txt_Status + "Erro: Empregado não localizado ( " + Colaborador + "  )" + System.Environment.NewLine;
        //        }


        //        rClinica.Find(" IdCliente = " + rEmpresa.Id.ToString() + " and convert( char(12), IdCliente ) + ' ' + convert( char(12), IdClinica ) in ( select top 1 convert( char(12), IdCliente ) + ' ' + convert( char(12), IdClinica ) from ClinicaCliente where IdCliente = " + rEmpresa.Id.ToString() + " ) ");


        //        //se não achar clinica,  emitir retorno avisando
        //        if (rClinica.Id == 0)
        //        {
        //            txt_Status = txt_Status + "Erro: Clínica não localizada " + System.Environment.NewLine;
        //        }


        //        //pegar IdExame
        //        if (TipoExame.ToUpper().Trim() == "PERIODICO" || TipoExame.ToUpper().Trim() == "PERIÓDICO")
        //            TipoExame = "4";
        //        else if (TipoExame.ToUpper().Trim() == "DEMISSIONAL")
        //            TipoExame = "2";
        //        else if (TipoExame.ToUpper().Trim().Substring(0, 5) == "MUDAN")
        //            TipoExame = "3";
        //        else if (TipoExame.ToUpper().Trim().Substring(0, 7) == "RETORNO")
        //            TipoExame = "5";


        //        zId = rEmpregado.Id;


        //        //validar token - deve existir registro com dados do e-mail de convocação




        //    }
        //    catch (Exception ex)
        //    {
        //        txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

        //        //Session["zErro"] = txt_Status;

        //        return txt_Status;


        //    }
        //    finally
        //    {

        //        if (txt_Status != "")
        //        {
        //            //Session["zErro"] = txt_Status;


        //        }
        //        else
        //        {
        //            rClinica.IdClinica.Find();



        //            //Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
        //            //Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia_Auto.aspx?IdEmpregado=" + zId.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + xCodUsuario + "&TipoExame=" + xTipoExame + "&Data=" + xData + "&IdClinica=" + rClinica.IdClinica.Id.ToString().Trim() + "&CodUsuario=" + xCodUsuario + "&xId=" + xID);
        //            HttpContext.Current.Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia_Auto.aspx?IdEmpregado=" + zId.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + CodUsuario + "&TipoExame=" + TipoExame + "&Data=" + Data + "&IdClinica=" + rClinica.IdClinica.Id.ToString().Trim() + "&CodUsuario=" + CodUsuario + "&xId=" + ID + "&xIdExame=4&xArq=" + xArq);


        //            //Session["zErro"] = txt_Status + " " + "Processamento Finalizado";

        //        }
        //    }

        //    string zErro = "";

        //    xArq = "I:\\temp\\ws_" + xArq + ".txt";

        //    if ( File.Exists( xArq))
        //        zErro = File.ReadAllText(xArq);

        //    if (zErro != "")
        //        return zErro; 
        //    else
        //        return txt_Status;

        //}








        //[WebMethod]
        //public string Executar_Afastamento(string ID, string Empresa, string CNPJ, string CodUsuario, string Colaborador, string DataInicio, string HoraInicio, string PrevisaoRetorno,
        //                                  string DataRetorno, string HoraRetorno, string Observacao, string TipoAfastamento, string Emitente_Atestado, string Responsavel_Atestado,
        //                                  string NrConselho_Atestado, string UFConselho_Atestado, string CID1, string CID2, string CID3, string CID4, string Arquivo, string Conteudo_Arquivo )
        //{

        //    string txt_Status = "";
        //    Empregado rEmpregado = new Empregado();
        //    Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
        //    //int zId = 0;


        //    string rSelect = "";


        //    try
        //    {



        //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");



        //        rSelect = " NomeAbreviado = '" + Empresa + "' and dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

        //        //pegar Id Empresa                    
        //        rEmpresa.Find(rSelect);

        //        //se não achar empresa,  emitir retorno avisando
        //        if (rEmpresa.Id == 0)
        //        {
        //            txt_Status = txt_Status + "Erro: Empresa não localizada ( " + Empresa + "  )" + System.Environment.NewLine;
        //        }


        //        if (txt_Status == "")
        //        {
        //            rEmpregado.Find(" tNo_Empg='" + Colaborador + "'  and nId_Empr = " + rEmpresa.Id.ToString());


        //            if (txt_Status == "")
        //            {
        //                //se não achar empregado,  emitir retorno avisando
        //                if (rEmpregado.Id == 0)
        //                {
        //                    txt_Status = txt_Status + "Erro: Empregado não localizado ( " + Colaborador + "  )" + System.Environment.NewLine;
        //                }
        //            }

        //        }


        //        //verificar se já existe registro de afastamento - pode ser apenas atualização da Data e Hora Retorno
        //        //do absenteísmo - nesse caso apenas atualizar o registro

        //        //  UPDATE Data e Hora Retorno
        //        if (txt_Status == "")
        //        {

        //            Afastamento rAfastamento = new Afastamento();
        //            rAfastamento.Find(" IdEmpregado = " + rEmpregado.Id.ToString() + " and convert( char(10), DataInicial, 103 ) = '" + DataInicio + "' ");

        //            if (rAfastamento.Id != 0)
        //            {
        //                //se tiver dataretorno,  dar update e finalizar processamento
        //                if (rAfastamento.DataVolta == new DateTime() && DataRetorno != "")
        //                {
        //                    rAfastamento.DataVolta = System.Convert.ToDateTime(DataRetorno + " " + HoraRetorno, ptBr);
        //                    rAfastamento.Save();

        //                    txt_Status = "Data de Retorno deste Afastamento salva para o colaborador " + Colaborador + " .";
        //                }
        //                else
        //                {
        //                    txt_Status = "Afastamento já existe para o colaborador " + Colaborador + " .";
        //                }

        //            }

        //        }


        //        if (txt_Status == "")
        //        {

        //            Afastamento afastamento = new Afastamento();

        //            afastamento.IdEmpregado = rEmpregado;
        //            afastamento.DataInicial = System.Convert.ToDateTime(DataInicio + ' ' + HoraInicio, ptBr);

        //            if (PrevisaoRetorno != "")
        //                afastamento.DataPrevista = System.Convert.ToDateTime(PrevisaoRetorno, ptBr);
        //            else
        //                afastamento.DataPrevista = new DateTime();


        //            if (DataRetorno != "")
        //                afastamento.DataVolta = System.Convert.ToDateTime(DataRetorno + ' ' + HoraRetorno, ptBr);
        //            else
        //                afastamento.DataVolta = new DateTime();


        //            Acidente zAcidente = new Acidente();
        //            afastamento.IdAcidente = zAcidente;


        //            //validação
        //            bool xEnvio_Alerta = false;

        //            if (afastamento.DataVolta == null || afastamento.DataVolta.Year == 1)
        //            {
        //                if (afastamento.DataInicial.AddDays(15) < afastamento.DataPrevista)
        //                {
        //                    xEnvio_Alerta = true;
        //                }
        //            }
        //            else
        //            {
        //                if (afastamento.DataInicial.AddDays(15) < afastamento.DataVolta)
        //                {
        //                    xEnvio_Alerta = true;
        //                }
        //            }


        //            if (xEnvio_Alerta == false)
        //            //checar se há atetados com mais de 15 dias afastados nos ultimos 60 dias
        //            {

        //                Ilitera.Data.Clientes_Funcionarios xAbs = new Ilitera.Data.Clientes_Funcionarios();
        //                DataSet rDs = xAbs.Checar_Absenteismo_Colaborador(rEmpregado.Id, afastamento.DataInicial.ToString("dd/MM/yyyy", ptBr));

        //                if (rDs.Tables[0].Rows.Count > 0)
        //                {
        //                    if (rDs.Tables[0].Rows[0][0].ToString().Trim() != "")
        //                    {
        //                        if (System.Convert.ToInt16(rDs.Tables[0].Rows[0][0].ToString()) > 15)
        //                        {
        //                            xEnvio_Alerta = true;
        //                        }
        //                    }
        //                }
        //            }


        //            if (xEnvio_Alerta == true)
        //            {
        //                txt_Status = "O colaborador " + Colaborador + " possui afastamentos anteriores com pelo menos 15 dias corridos ou com total de dias afastados de pelo menos 15 dias nos últimos 60 dias. Favor verificar se os atestados possuem a mesma patologia para possível encaminhamento ao INSS.";
        //            }




        //            if (txt_Status == "" && CID1 != "")
        //            {
        //                CID rCID = new CID();
        //                rCID.Find(" CodigoCID = '" + CID1 + "'  ");

        //                if (rCID.Id == 0)
        //                {
        //                    txt_Status = "Código CID fornecido Inválido: " + CID1;
        //                }
        //                else
        //                {
        //                    afastamento.IdCID = rCID;
        //                }
        //            }

        //            if (txt_Status == "" && CID2 != "")
        //            {
        //                CID rCID = new CID();
        //                rCID.Find(" CodigoCID = '" + CID2 + "'  ");

        //                if (rCID.Id == 0)
        //                {
        //                    txt_Status = "Código CID fornecido Inválido: " + CID2;
        //                }
        //                else
        //                {
        //                    afastamento.IdCID2 = rCID.Id;
        //                }
        //            }

        //            if (txt_Status == "" && CID3 != "")
        //            {
        //                CID rCID = new CID();
        //                rCID.Find(" CodigoCID = '" + CID3 + "'  ");

        //                if (rCID.Id == 0)
        //                {
        //                    txt_Status = "Código CID fornecido Inválido: " + CID3;
        //                }
        //                else
        //                {
        //                    afastamento.IdCID3 = rCID.Id;
        //                }
        //            }

        //            if (txt_Status == "" && CID4 != "")
        //            {
        //                CID rCID = new CID();
        //                rCID.Find(" CodigoCID = '" + CID4 + "'  ");

        //                if (rCID.Id == 0)
        //                {
        //                    txt_Status = "Código CID fornecido Inválido: " + CID4;
        //                }
        //                else
        //                {
        //                    afastamento.IdCID4 = rCID.Id;
        //                }
        //            }



        //            if (txt_Status == "")
        //            {

        //                afastamento.Obs = Observacao.Trim();

        //                //xEmitente_Atestado = current.Element("Emitente_Atestado").Value;
        //                //xResponsavel_Atestado = current.Element("Responsavel_Atestado").Value;
        //                //xNrConselho_Atestado = current.Element("NrConselho_Atestado").Value;
        //                //xUFConselho_Atestado = current.Element("UFConselho_Atestado").Value;


        //                if (TipoAfastamento.ToUpper() == "OCUPACIONAL")
        //                {
        //                    afastamento.IndTipoAfastamento = 1;
        //                }
        //                else
        //                {
        //                    afastamento.IndTipoAfastamento = 2;
        //                }


        //                AfastamentoTipo xAT = new AfastamentoTipo();
        //                xAT.Find(101);
        //                afastamento.IdAfastamentoTipo = xAT;

        //                afastamento.UsuarioId = System.Convert.ToInt32(CodUsuario);

        //                afastamento.Save();

        //            }


        //            if (txt_Status == "" && Conteudo_Arquivo != "")
        //            {
        //                //xArquivo terá a extensão

        //                byte[] arrBytes = Convert.FromBase64String(Conteudo_Arquivo);

        //                //montar com diretório padrão da empresa, nome do colaborador, data e extensão - chave Arquivo
        //                Cliente xCliente = new Cliente();
        //                xCliente.Find(System.Convert.ToInt32(rEmpresa.Id));

        //                string xArq = "";

        //                //if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
        //                xArq = "Atestado_" + Colaborador.Replace(" ", "_") + "_" + DataInicio.Replace("/", "") + "." + Arquivo;


        //                string uri = "ftp://54.94.157.244:21/" + xCliente.DiretorioPadrao.ToString().Trim().Replace(" ", "%20") + "/Prontuario/" + xArq;

        //                //string uri = "ftp://54.94.157.244:21/5A%20GT/Prontuario/teste_xml.pdf";



        //                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(uri));
        //                request.Method = WebRequestMethods.Ftp.UploadFile;

        //                request.Credentials = new NetworkCredential("lmm", "Lm160521");

        //                request.UseBinary = true;
        //                request.UsePassive = true;
        //                request.ContentLength = arrBytes.Length;
        //                Stream stream = request.GetRequestStream();
        //                stream.Write(arrBytes, 0, arrBytes.Length);
        //                stream.Close();
        //                FtpWebResponse res = (FtpWebResponse)request.GetResponse();

        //                afastamento.Atestado = @"I:\FotosDocsDigitais\" + xCliente.DiretorioPadrao.ToString().Trim() + "/Prontuario/" + xArq;

        //                //salvar dados do atestado nos campos do afastamento
        //                afastamento.Atestado_Emitente = Responsavel_Atestado;

        //                if (Emitente_Atestado.Trim().ToUpper() == "CRM")
        //                    afastamento.Atestado_ideOC = "1";
        //                else
        //                    afastamento.Atestado_ideOC = "2";

        //                afastamento.Atestado_nrOC = NrConselho_Atestado;
        //                afastamento.Atestado_ufOC = UFConselho_Atestado;

        //                afastamento.Save();
        //            }


        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

        //        //Session["zErro"] = txt_Status;
        //        return txt_Status;


        //    }
        //    finally
        //    {

        //        if (txt_Status != "")
        //        {
        //            //Session["zErro"] = txt_Status;                    
        //        }
        //        else
        //        {

        //            // Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
        //            // Response.Redirect("~\\PCMSO\\CadAbsentismo_Auto.aspx?IdEmpregado=" + zId.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + xCodUsuario + "&Data=" + xData + "&CodUsuario=" + xCodUsuario + "&xId=" + xID);


        //            // 'CadAbsentismo.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + xUser.IdUsuario.ToString() + "&IdAcidente=0'

        //            //Session["zErro"] = txt_Status + " " + "Processamento Finalizado";
        //            txt_Status = "Processamento OK";

        //        }
        //    }

        //    return txt_Status;


        //}



        //[WebMethod]
        //public XmlDocument Retornar_Clinicas(string CEP, string Numero_Clinicas, string Valor_Maximo)
        //{


        //    ClinicaCliente rClinica = new ClinicaCliente();
        //    Empregado rEmpregado = new Empregado();
        //    Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
        //    //int zId = 0;


        //    //string rSelect = "";
        //    string txt_Status = "";

        //    XmlDocument xRet = new XmlDocument();


        //    try
        //    {

        //        Ilitera.Data.Comunicacao xBusca = new Ilitera.Data.Comunicacao();
        //        DataSet xDs = new DataSet();

        //        xDs = xBusca.Trazer_Clinicas_CEP(CEP, Numero_Clinicas, Valor_Maximo);


        //        if (xDs.Tables[0].Rows.Count == 0)
        //        {
        //            txt_Status = txt_Status + "Erro: Clínicas não localizadas por CEP" + System.Environment.NewLine;
        //        }



        //        //montar XML de retorno com clínicas
        //        if ( txt_Status=="")
        //        {

        //            xDs.DataSetName = "Retorno";
        //            xDs.Tables[0].TableName = "Clinicas";

        //            string xstrXML = xDs.GetXml();

        //            xRet.LoadXml(xstrXML);


        //        }




        //    }
        //    catch (Exception ex)
        //    {
        //        txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

        //        //Session["zErro"] = txt_Status.Text;
        //        //Response.Redirect("~/Comunicacao2.aspx");

        //    }
        //    finally
        //    {

        //        if (txt_Status != "")
        //        {

        //            //montar XMLDocument com erro
        //            string xRetorno = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

        //            xRet.LoadXml(xRetorno);


        //        }

        //    }


        //    return xRet;

        //}



        //[WebMethod]
        //public XmlDocument Retornar_Laudos(string xCNPJ)
        //{


        //    Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
        //    //int zId = 0;




        //    string rSelect = "";
        //    string txt_Status = "";

        //    XmlDocument xRet = new XmlDocument();


        //    try
        //    {


        //        rEmpresa = new Ilitera.Common.Pessoa();

        //        rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

        //        //pegar Id Empresa                    
        //        rEmpresa.Find(rSelect);

        //        //se não achar empresa,  emitir retorno avisando
        //        if (rEmpresa.Id == 0)
        //        {
        //            txt_Status = txt_Status + "Erro: Empresa não localizada ( " + xCNPJ + "  )" + System.Environment.NewLine;                    
        //        }



        //        if (txt_Status == "")
        //        {

        //            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

        //            Ilitera.Data.PPRA_EPI xLaudos = new Ilitera.Data.PPRA_EPI();

        //            DataSet xDs = xLaudos.Retornar_Laudos(rEmpresa.Id);


        //            //if (xDs.Tables[0].Rows.Count == 0)
        //            //{
        //            //    txt_Status = txt_Status + "Empresa sem laudos concluídos." + System.Environment.NewLine;
        //            //}



        //            //montar XML de retorno com clínicas
        //            if (txt_Status == "")
        //            {

        //                xDs.DataSetName = "Retorno";
        //                xDs.Tables[0].TableName = "Laudos";

        //                string xstrXML = xDs.GetXml();

        //                xRet.LoadXml(xstrXML);


        //            }


        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine + ex.InnerException + System.Environment.NewLine;

        //        //Session["zErro"] = txt_Status.Text;
        //        //Response.Redirect("~/Comunicacao2.aspx");

        //    }
        //    finally
        //    {

        //        if (txt_Status != "")
        //        {

        //            //montar XMLDocument com erro
        //            string xRetorno = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

        //            xRet.LoadXml(xRetorno);


        //        }

        //    }


        //    return xRet;

        //}





        //[WebMethod]
        //public string Executar_Exame(string ID, string Empresa, string CNPJ, string CodUsuario, string TipoExame, string Colaborador, string Data, string NIT, string RG, string CPF,
        //                            string DtAdmissao, string DtNascimento, string CTPS_Numero, string CTPS_Serie, string CTPS_UF, string Email, string Matricula,
        //                            string Sexo, string Funcao, string Cargo, string Setor)
        //{


        //    ClinicaCliente rClinica = new ClinicaCliente();
        //    Empregado rEmpregado = new Empregado();
        //    Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
        //    int zId = 0;




        //    string rSelect = "";
        //    string txt_Status = "";


        //    try
        //    {



        //        rSelect = " NomeAbreviado = '" + Empresa + "' and dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

        //        //pegar Id Empresa                    
        //        rEmpresa.Find(rSelect);

        //        //se não achar empresa,  emitir retorno avisando
        //        if (rEmpresa.Id == 0)
        //        {
        //            txt_Status = txt_Status + "Erro: Empresa não localizada ( " + Empresa + "  )" + System.Environment.NewLine;
        //        }

        //        //quando for convocação ( mailing ) no último irá o link para chamar comunicação
        //        //pode ir com um ID pronto


        //        if (txt_Status == "")
        //        {
        //            if (TipoExame.ToUpper().Trim() != "ADMISSIONAL")
        //            {


        //                //pegar Id Colaborador                       
        //                rEmpregado.Find(" tNo_Empg='" + Colaborador + "'  and nId_Empr = " + rEmpresa.Id.ToString());

        //                //se não achar empregado,  emitir retorno avisando
        //                if (rEmpregado.Id == 0)
        //                {
        //                    txt_Status = txt_Status + "Erro: Empregado não localizado ( " + Colaborador + "  )" + System.Environment.NewLine;
        //                }


        //                rClinica.Find(" IdCliente = " + rEmpresa.Id.ToString() + " and convert( char(12), IdCliente ) + ' ' + convert( char(12), IdClinica ) in ( select top 1 convert( char(12), IdCliente ) + ' ' + convert( char(12), IdClinica ) from ClinicaCliente where IdCliente = " + rEmpresa.Id.ToString() + " ) ");


        //                //se não achar clinica,  emitir retorno avisando
        //                if (rClinica.Id == 0)
        //                {
        //                    txt_Status = txt_Status + "Erro: Clínica não localizada " + System.Environment.NewLine;
        //                }


        //                //pegar IdExame
        //                if (TipoExame.ToUpper().Trim() == "PERIODICO" || TipoExame.ToUpper().Trim() == "PERIÓDICO")
        //                    TipoExame = "4";
        //                else if (TipoExame.ToUpper().Trim() == "DEMISSIONAL")
        //                    TipoExame = "2";
        //                else if (TipoExame.ToUpper().Trim().Substring(0, 5) == "MUDAN")
        //                    TipoExame = "3";
        //                else if (TipoExame.ToUpper().Trim().Substring(0, 7) == "RETORNO")
        //                    TipoExame = "5";


        //                zId = rEmpregado.Id;

        //                //if (txt_Status.Text == "")
        //                //{
        //                //    rClinica.IdClinica.Find();

        //                //    Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
        //                //    Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia_Auto.aspx?IdEmpregado=" + rEmpregado.Id.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + xCodUsuario + "&TipoExame=" + xTipoExame + "&Data=" + xData + "&IdClinica=" + rClinica.IdClinica.Id.ToString().Trim() + "&CodUsuario=" + xCodUsuario + "&xId=" + xID);
        //                //}
        //                //else
        //                //{
        //                //    Session["zErro"] = txt_Status.Text;
        //                //    Response.Redirect("~/Comunicacao2.aspx");
        //                //}

        //            }


        //            else  //Admissional
        //            {

        //                if (txt_Status == "")
        //                {




        //                    //Se colaborador já existir, cancelar                        
        //                    rEmpregado.Find(" tno_CPF='" + CPF + "'  and nId_Empr = " + rEmpresa.Id.ToString());


        //                    if (rEmpregado.Id != 0)
        //                    {
        //                        txt_Status = txt_Status + "Erro: Empregado já cadastrado ( " + Colaborador + "  )" + System.Environment.NewLine;
        //                    }


        //                    if (txt_Status == "")
        //                    {

        //                        //criar registro do colaborador
        //                        Ilitera.Data.Empregado_Cadastral xEmpregado = new Ilitera.Data.Empregado_Cadastral();
        //                        zId = xEmpregado.Inserir_Dados_Empregado(rEmpresa.Id.ToString().Trim(), Colaborador, Sexo.Substring(0, 1), CTPS_Numero, CTPS_Serie, CTPS_UF, Matricula, RG, DtAdmissao, "", DtNascimento, NIT, CPF, "", System.Convert.ToInt32(CodUsuario), "", Email);

        //                        //criar classif.funcional
        //                        xEmpregado.Inserir_Classificacao_Funcional(zId, rEmpresa.Id, DtAdmissao, "", Cargo, Setor, Funcao, System.Convert.ToInt32(CodUsuario), "");


        //                        //alocar GHE
        //                        EmpregadoFuncao xEmprFunc = new EmpregadoFuncao();
        //                        xEmprFunc.Find(" nId_Empregado = " + zId.ToString());

        //                        int colId = xEmprFunc.Id;

        //                        LaudoTecnico nLaudo = new LaudoTecnico();
        //                        nLaudo.Find(" nId_Empr = " + rEmpresa.Id.ToString() + " and convert( char(12), nId_Empr ) + ' ' + convert( char(10), hDt_Laudo, 103 ) in ( select convert( char(12), nId_Empr ) + ' ' + convert( char(10), max(hDt_Laudo), 103 ) from tblLaudo_Tec group by nId_Empr ) ");

        //                        Ghe nGHE = new Ghe();
        //                        nGHE.Find(" nId_Laud_Tec = " + nLaudo.Id.ToString() + " and tNO_FUNC = '" + Setor + "' ");


        //                        Int32 zLaudo = nLaudo.Id;
        //                        Int32 zGHE = nGHE.Id;


        //                        if (colId != 0 && zLaudo != 0 && zGHE != 0)
        //                        {
        //                            GheEmpregado gheEmpregado = new GheEmpregado();
        //                            gheEmpregado.Find("nID_FUNC=" + zGHE + " AND nID_EMPREGADO_FUNCAO=" + colId);
        //                            if (gheEmpregado.Id == 0)
        //                            {
        //                                gheEmpregado.Inicialize();
        //                                gheEmpregado.nID_LAUD_TEC.Id = zLaudo;
        //                                gheEmpregado.nID_EMPREGADO_FUNCAO.Id = colId;
        //                            }
        //                            gheEmpregado.nID_FUNC.Id = zGHE;
        //                            gheEmpregado.Save();



        //                            //criar exame
        //                            TipoExame = "1";

        //                            rClinica.Find(" IdCliente = " + rEmpresa.Id.ToString() + " and convert( char(12), IdCliente ) + ' ' + convert( char(12), IdClinica ) in ( select top 1 convert( char(12), IdCliente ) + ' ' + convert( char(12), IdClinica ) from ClinicaCliente where IdCliente = " + rEmpresa.Id.ToString() + " ) ");

        //                        }
        //                    }

        //                }

        //            }

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

        //        //Session["zErro"] = txt_Status.Text;
        //        //Response.Redirect("~/Comunicacao2.aspx");


        //    }
        //    finally
        //    {

        //        if (txt_Status != "")
        //        {
        //            //Session["zErro"] = txt_Status.Text;
        //            //Response.Redirect("~/Comunicacao2.aspx");
        //        }
        //        else
        //        {
        //            rClinica.IdClinica.Find();

        //            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
        //            //Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia_Auto.aspx?IdEmpregado=" + zId.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + xCodUsuario + "&TipoExame=" + xTipoExame + "&Data=" + xData + "&IdClinica=" + rClinica.IdClinica.Id.ToString().Trim() + "&CodUsuario=" + xCodUsuario + "&xId=" + xID);


        //            //Session["zErro"] = txt_Status.Text + " " + "Processamento Finalizado";
        //            txt_Status = "Processamento OK";

        //        }
        //    }


        //    return txt_Status;

        //}





        //[WebMethod]
        //public string Executar_Acidente(string ID, string Empresa ,  string CodUsuario ,   string CNPJ ,  string Colaborador ,   string DataAcidente ,  string HoraAcidente , string TipoAcidente ,
        //                                string SituacaoGeradora , string ParteCorpoAtingida ,  string Lateralidade ,  string AgenteCausador ,   string DescricaoLesao ,  string DescricaoCompl ,
        //                                string LocalAcidente ,  string EnderecoLocal ,   string NumeroLocal ,   string MunicipioLocal ,  string UFLocal ,  string UnidadeLocal ,  string EspecificacaoLocal ,
        //                                string SetorAcidente ,  string Internacao ,  string DataAtendimento ,  string HoraAtendimento ,  string DuracaoTratamentoDias ,  string CodigoCNES ,
        //                                string Reponsavel_Atestado , string NrConselho_Atestado , string UFConselho_Atestado ,  string DiagnosticoProvavel , string CID1 , string CID2 ,
        //                                string CID3 , string CID4 ,  string TransferidoOutroSetor ,  string AposentadoInvalidez ,  string PerdaMaterial ,  string EmitenteCAT ,  string DataEmissaoCAT ,
        //                                string HoraEmissaoCAT , string TipoCAT ,  string NumeroCAT , string Iniciativa , string BOPolicial , string DataObito ,  string DataInicioAbsenteismo ,
        //                                string HoraInicioAbsenteismo ,  string PrevisaoRetornoAbsenteismo ,  string DataRetornoAbsenteismo , string HoraRetornoAbsenteismo ,  string ObservacaoAbsenteismo )
        //{


        //    ClinicaCliente rClinica = new ClinicaCliente();
        //    Empregado rEmpregado = new Empregado();
        //    Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
        //    //int zId = 0;



        //    string rSelect = "";
        //    string txt_Status = "";

        //    try
        //    {


        //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


        //        rSelect = " NomeAbreviado = '" + Empresa + "' and dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

        //        //pegar Id Empresa                    
        //        rEmpresa.Find(rSelect);

        //        //se não achar empresa,  emitir retorno avisando
        //        if (rEmpresa.Id == 0)
        //        {
        //            txt_Status = txt_Status + "Erro: Empresa não localizada ( " + Empresa + "  )" + System.Environment.NewLine;
        //        }



        //        rEmpregado.Find(" tNo_Empg='" + Colaborador + "'  and nId_Empr = " + rEmpresa.Id.ToString());


        //        if (txt_Status == "")
        //        {
        //            //se não achar empregado,  emitir retorno avisando
        //            if (rEmpregado.Id == 0)
        //            {
        //                txt_Status = txt_Status + "Erro: Empregado não localizado ( " + Colaborador + "  )" + System.Environment.NewLine;
        //            }
        //        }


        //        Acidente rAcidente = new Acidente();
        //        rAcidente.Find(" IdEmpregado = " + rEmpregado.Id.ToString() + " and convert( char(10), DataAcidente, 103 ) = '" + DataAcidente + "' ");

        //        if (rAcidente.Id != 0)
        //        {
        //            txt_Status = "Acidente já existe para o colaborador " + Colaborador + " .";
        //        }


        //        if (txt_Status == "")
        //        {
        //            Acidente acidente = new Acidente();

        //            acidente.IdEmpregado = rEmpregado;

        //            acidente.DataAcidente = System.Convert.ToDateTime(DataAcidente + " " + HoraAcidente, ptBr);

        //            TipoAcidente = TipoAcidente.Trim().ToUpper();
        //            if (TipoAcidente == "TIPICO" || TipoAcidente == "TÍPICO") acidente.IndTipoAcidente = 1;
        //            else if (TipoAcidente == "DOENCA" || TipoAcidente == "DOENÇA") acidente.IndTipoAcidente = 2;
        //            else if (TipoAcidente == "TRAJETO") acidente.IndTipoAcidente = 3;
        //            else acidente.IndTipoAcidente = 1;

        //            if (SituacaoGeradora != "")
        //                acidente.Codigo_Situacao_Geradora = System.Convert.ToInt32(SituacaoGeradora);

        //            if (ParteCorpoAtingida != "")
        //                acidente.Codigo_Parte_Corpo_Atingida = System.Convert.ToInt32(ParteCorpoAtingida);

        //            Lateralidade = Lateralidade.Trim().ToUpper();
        //            if (Lateralidade == "ESQUERDA")
        //                acidente.IdLateralidade = 1;
        //            else if (Lateralidade == "DIREITA")
        //                acidente.IdLateralidade = 2;
        //            else if (Lateralidade == "AMBAS")
        //                acidente.IdLateralidade = 3;
        //            else
        //                acidente.IdLateralidade = 0;

        //            if (AgenteCausador != "")
        //                acidente.AgenteCausador = AgenteCausador;

        //            if (DescricaoLesao != "")
        //                acidente.Codigo_Descricao_Lesao = System.Convert.ToInt32(DescricaoLesao);

        //            acidente.Descricao = DescricaoCompl;

        //            LocalAcidente zLocalAcidente = new LocalAcidente();
        //            zLocalAcidente.Find(System.Convert.ToInt32(LocalAcidente));

        //            acidente.IdLocalAcidente = zLocalAcidente;


        //            acidente.Logradouro = EnderecoLocal;
        //            acidente.Nr_Logradouro = NumeroLocal;
        //            acidente.Municipio = MunicipioLocal;
        //            acidente.UF = UFLocal;

        //            acidente.EspecLocal = EspecificacaoLocal;

        //            acidente.indTipoSetor = 1;

        //            //xUnidadeLocal
        //            //xSetorAcidente




        //            if (Internacao == "S")
        //            {
        //                acidente.DataInternacao = System.Convert.ToDateTime(DataAtendimento + " " + HoraAtendimento, ptBr);
        //                acidente.HasInternacao = true;
        //                acidente.CNES = CodigoCNES;
        //            }
        //            else
        //            {
        //                acidente.HasInternacao = false;
        //                acidente.CNES = "";
        //            }


        //            if (DuracaoTratamentoDias != "")
        //                acidente.DuracaoInternacao = System.Convert.ToInt16(DuracaoTratamentoDias);


        //            if (Reponsavel_Atestado != "")
        //            {
        //                acidente.MedicoInternacao = Reponsavel_Atestado;
        //                acidente.CRMInternacao = NrConselho_Atestado;
        //                acidente.UFInternacao = UFConselho_Atestado;
        //                acidente.DiagnosticoProvavel = DiagnosticoProvavel;
        //            }





        //            if (txt_Status == "" && CID1 != "")
        //            {
        //                CID rCID = new CID();
        //                rCID.Find(" CodigoCID = '" + CID1 + "'  ");

        //                if (rCID.Id == 0)
        //                {
        //                    txt_Status = "Código CID fornecido Inválido: " + CID1;
        //                }
        //                else
        //                {
        //                    acidente.IdCID = rCID;
        //                }
        //            }

        //            if (txt_Status == "" && CID2 != "")
        //            {
        //                CID rCID = new CID();
        //                rCID.Find(" CodigoCID = '" + CID2 + "'  ");

        //                if (rCID.Id == 0)
        //                {
        //                    txt_Status = "Código CID fornecido Inválido: " + CID2;
        //                }
        //                else
        //                {
        //                    acidente.IdCID2 = rCID.Id;
        //                }
        //            }

        //            if (txt_Status == "" && CID3 != "")
        //            {
        //                CID rCID = new CID();
        //                rCID.Find(" CodigoCID = '" + CID3 + "'  ");

        //                if (rCID.Id == 0)
        //                {
        //                    txt_Status = "Código CID fornecido Inválido: " + CID3;
        //                }
        //                else
        //                {
        //                    acidente.IdCID3 = rCID.Id;
        //                }
        //            }

        //            if (txt_Status == "" && CID4 != "")
        //            {
        //                CID rCID = new CID();
        //                rCID.Find(" CodigoCID = '" + CID4 + "'  ");

        //                if (rCID.Id == 0)
        //                {
        //                    txt_Status = "Código CID fornecido Inválido: " + CID4;
        //                }
        //                else
        //                {
        //                    acidente.IdCID4 = rCID.Id;
        //                }
        //            }


        //            if (TransferidoOutroSetor == "S") acidente.isTransfSetor = true;
        //            else acidente.isTransfSetor = false;

        //            if (AposentadoInvalidez == "S") acidente.isAposInval = true;
        //            else acidente.isAposInval = false;

        //            if (PerdaMaterial != "")
        //            {
        //                try
        //                {
        //                    acidente.PerdaMaterial = System.Convert.ToSingle(PerdaMaterial);
        //                }
        //                catch  //Exception Ex)
        //                {
        //                    acidente.PerdaMaterial = 0;
        //                }
        //            }
        //            else
        //            {
        //                acidente.PerdaMaterial = 0;
        //            }



        //            if (NumeroCAT != "")
        //            {
        //                CAT cat = new CAT();

        //                cat.Inicialize();
        //                cat.IdEmpregado = rEmpregado;


        //                cat.NumeroCAT = NumeroCAT;

        //                cat.DataEmissao = System.Convert.ToDateTime(DataEmissaoCAT + " " + HoraEmissaoCAT, ptBr);
        //                cat.IndEmitente = Convert.ToInt32(EmitenteCAT);
        //                cat.IndTipoCAT = Convert.ToInt32(TipoCAT);

        //                if (BOPolicial != "")
        //                {
        //                    cat.hasRegPolicial = true;
        //                    cat.BO = BOPolicial;
        //                }
        //                else
        //                {
        //                    cat.hasRegPolicial = false;
        //                }

        //                if (DataObito != "")
        //                {
        //                    cat.hasMorte = true;
        //                    cat.DataObito = System.Convert.ToDateTime(DataObito, ptBr);
        //                }
        //                else
        //                {
        //                    cat.hasMorte = false;
        //                }

        //                cat.UsuarioId = System.Convert.ToInt32(CodUsuario);

        //                cat.Save();

        //                acidente.IdCAT = cat;



        //            }


        //            Ilitera.Common.Juridica xJuridica = new Ilitera.Common.Juridica();
        //            xJuridica.Find(rEmpresa.Id);

        //            acidente.IdJuridica = xJuridica;


        //            System.Collections.ArrayList emprFuncao = new EmpregadoFuncao().Find("nID_EMPREGADO=" + rEmpregado.Id
        //            + " AND hDT_INICIO<='" + acidente.DataAcidente.ToString("yyyy-MM-dd")
        //            + "' AND (hDT_TERMINO IS NULL OR hDT_TERMINO>='" + acidente.DataAcidente.ToString("yyyy-MM-dd") + "')");

        //            acidente.IdSetor = ((EmpregadoFuncao)emprFuncao[0]).nID_SETOR;


        //            acidente.Save();



        //            if (DataInicioAbsenteismo != "")
        //            {
        //                acidente.hasAfastamento = true;

        //                //salvar absenteismo
        //                Afastamento absentismo = new Afastamento();

        //                absentismo.Inicialize();
        //                absentismo.IdEmpregado = rEmpregado;



        //                absentismo.DataInicial = System.Convert.ToDateTime(DataInicioAbsenteismo + " " + HoraInicioAbsenteismo, ptBr);

        //                if (PrevisaoRetornoAbsenteismo != "")
        //                    absentismo.DataPrevista = System.Convert.ToDateTime(PrevisaoRetornoAbsenteismo, ptBr);
        //                else
        //                    absentismo.DataPrevista = new DateTime();


        //                if (DataRetornoAbsenteismo != "")
        //                    absentismo.DataVolta = System.Convert.ToDateTime(DataRetornoAbsenteismo + " " + HoraRetornoAbsenteismo, ptBr);
        //                else
        //                    absentismo.DataVolta = new DateTime();

        //                absentismo.IndTipoAfastamento = (int)TipoAfastamento.Ocupacional;
        //                absentismo.IdAcidente = acidente;


        //                if (txt_Status == "" && CID1 != "")
        //                {
        //                    CID rCID = new CID();
        //                    rCID.Find(" CodigoCID = '" + CID1 + "'  ");

        //                    if (rCID.Id == 0)
        //                    {
        //                        txt_Status = "Código CID fornecido Inválido: " + CID1;
        //                    }
        //                    else
        //                    {
        //                        absentismo.IdCID = rCID;
        //                    }
        //                }

        //                if (txt_Status == "" && CID2 != "")
        //                {
        //                    CID rCID = new CID();
        //                    rCID.Find(" CodigoCID = '" + CID2 + "'  ");

        //                    if (rCID.Id == 0)
        //                    {
        //                        txt_Status = "Código CID fornecido Inválido: " + CID2;
        //                    }
        //                    else
        //                    {
        //                        absentismo.IdCID2 = rCID.Id;
        //                    }
        //                }

        //                if (txt_Status == "" && CID3 != "")
        //                {
        //                    CID rCID = new CID();
        //                    rCID.Find(" CodigoCID = '" + CID3 + "'  ");

        //                    if (rCID.Id == 0)
        //                    {
        //                        txt_Status = "Código CID fornecido Inválido: " + CID3;
        //                    }
        //                    else
        //                    {
        //                        absentismo.IdCID3 = rCID.Id;
        //                    }
        //                }

        //                if (txt_Status == "" && CID4 != "")
        //                {
        //                    CID rCID = new CID();
        //                    rCID.Find(" CodigoCID = '" + CID4 + "'  ");

        //                    if (rCID.Id == 0)
        //                    {
        //                        txt_Status = "Código CID fornecido Inválido: " + CID4;
        //                    }
        //                    else
        //                    {
        //                        absentismo.IdCID4 = rCID.Id;
        //                    }
        //                }


        //                absentismo.INSS = false;

        //                absentismo.UsuarioId = Convert.ToInt32(CodUsuario);

        //                absentismo.Save();

        //            }
        //            else
        //                acidente.hasAfastamento = false;




        //            acidente.Save();

        //        }







        //    }
        //    catch (Exception ex)
        //    {
        //        txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

        //      //  Session["zErro"] = txt_Status;
        //      //  Response.Redirect("~/Comunicacao2.aspx");


        //    }
        //    finally
        //    {

        //        if (txt_Status != "")
        //        {

        //        }
        //        else
        //        {
        //            //rClinica.IdClinica.Find();

        //            //Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
        //            //Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia_Auto.aspx?IdEmpregado=" + zId.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + xCodUsuario + "&TipoExame=" + xTipoExame + "&Data=" + xData + "&IdClinica=" + rClinica.IdClinica.Id.ToString().Trim() + "&CodUsuario=" + xCodUsuario + "&xId=" + xID);


        //            // Session["zErro"] = txt_Status + " " + "Processamento Finalizado";
        //            txt_Status = "";

        //        }
        //    }

        //    return txt_Status;


        //}



        //[WebMethod]
        //public string Executar_CIPA(string ID, string Empresa,  string CodUsuario,  string CNPJ,  string Colaborador, string DataInicioCIPA, string Papel, string Indicado)
        //{


        //    Empregado rEmpregado = new Empregado();
        //    Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

        //    //int zId = 0;

        //    string rSelect = "";

        //    string txt_Status = "";

        //    try
        //    {


        //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


        //        rSelect = " NomeAbreviado = '" + Empresa + "' and dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

        //        //pegar Id Empresa                    
        //        rEmpresa.Find(rSelect);

        //        //se não achar empresa,  emitir retorno avisando
        //        if (rEmpresa.Id == 0)
        //        {
        //            txt_Status = txt_Status + "Erro: Empresa não localizada ( " + Empresa + "  )" + System.Environment.NewLine;
        //        }




        //        rEmpregado.Find(" tNo_Empg='" + Colaborador + "'  and nId_Empr = " + rEmpresa.Id.ToString());

        //        //se não achar empregado,  emitir retorno avisando
        //        if (rEmpregado.Id == 0)
        //        {
        //            txt_Status = txt_Status + "Erro: Empregado não localizado ( " + Colaborador + "  )" + System.Environment.NewLine;
        //        }



        //        if (txt_Status == "")
        //        {

        //            //ver se CIPA com a data Inicial já existe, senão criar ( preciso ver como criar o calendário, quantos dias para cada evento )
        //            //Poderia ver possibilidade de no repositório CIPA web,  poder anexar arquivo a cada evento,  não só à reuniões, acho que isso não complicado.
        //            Cipa rCIPA = new Cipa();

        //            rCIPA.Find("  IdCliente =" + rEmpresa.Id.ToString() + " and convert( char(10), Edital, 103 ) = '" + DataInicioCIPA + "' ");

        //            if (rCIPA.Id == 0)
        //            {
        //                Cliente rCliente = new Cliente();
        //                rCliente.Find(rEmpresa.Id);

        //                Ilitera.Common.Prestador rPrestador = new Ilitera.Common.Prestador();
        //                rPrestador.Find(" prestador.idjuridicapessoa in (select idjuridicapessoa from juridicapessoa where idpessoa in   (select idpessoa from usuario where idusuario = " + CodUsuario + " ))");

        //                if (rPrestador.Id == 0)
        //                {
        //                    txt_Status = txt_Status + "Erro: erro na salva da CIPA (1)" + System.Environment.NewLine;
        //                }


        //                Obrigacao rObrigacao = new Obrigacao();

        //                if (txt_Status == "")
        //                {
        //                    rObrigacao.Find(" nome = 'CIPA - Criação e Elaboração' ");

        //                    if (rObrigacao.Id == 0)
        //                    {
        //                        txt_Status = txt_Status + "Erro: erro na salva da CIPA (2)" + System.Environment.NewLine;
        //                    }
        //                }


        //                Ilitera.Common.Compromisso rCompromisso = new Ilitera.Common.Compromisso();
        //                rCompromisso.Find(0);

        //                PedidoGrupo rPedidoGrupo = new PedidoGrupo();

        //                if (txt_Status == "")
        //                {
        //                    rPedidoGrupo.DataSolicitacao = System.DateTime.Now;
        //                    rPedidoGrupo.IdCliente = rCliente;
        //                    rPedidoGrupo.Solicitante = "";
        //                    rPedidoGrupo.IdCompromisso = rCompromisso;
        //                    rPedidoGrupo.IdPrestador = rPrestador;
        //                    rPedidoGrupo.Numero = PedidoGrupo.GetNumeroPedido();
        //                    rPedidoGrupo.Save();

        //                    if (rPedidoGrupo.Id == 0)
        //                    {
        //                        txt_Status = txt_Status + "Erro: erro na salva da CIPA (3)" + System.Environment.NewLine;
        //                    }

        //                }


        //                EquipamentoBase rEqpto = new EquipamentoBase();
        //                rEqpto.Find(0);

        //                Pedido rPedido = new Pedido();


        //                if (txt_Status == "")
        //                {

        //                    rPedido.IdCliente = rCliente;
        //                    rPedido.IdPrestador = rPrestador;
        //                    rPedido.DataSolicitacao = System.DateTime.Now;
        //                    rPedido.DataSugestao = System.DateTime.Now;
        //                    rPedido.IdEquipamentoBase = rEqpto;
        //                    rPedido.IdObrigacao = rObrigacao;
        //                    rPedido.IdPedidoGrupo = rPedidoGrupo;
        //                    rPedido.Save();


        //                    if (rPedido.Id == 0)
        //                    {
        //                        txt_Status = txt_Status + "Erro: erro na salva da CIPA (4)" + System.Environment.NewLine;
        //                    }

        //                }



        //                DocumentoBase rDoc = new DocumentoBase();

        //                if (txt_Status == "")
        //                {
        //                    rDoc.Find("NomeDocumento = 'CIPA'");

        //                    if (rDoc.Id == 0)
        //                    {
        //                        txt_Status = txt_Status + "Erro: erro na salva da CIPA (5)" + System.Environment.NewLine;
        //                    }
        //                }




        //                //criar CIPA
        //                rCIPA = new Cipa();

        //                if (txt_Status == "")
        //                {
        //                    rCIPA.Edital = System.Convert.ToDateTime(DataInicioCIPA, ptBr);
        //                    rCIPA.IdCliente = rCliente;
        //                    rCIPA.IdPedido = rPedido;
        //                    rCIPA.IdPrestador = rPrestador;
        //                    rCIPA.Edital = System.Convert.ToDateTime(DataInicioCIPA, ptBr);
        //                    rCIPA.IdDocumentoBase = rDoc;
        //                    rCIPA.DataLevantamento = System.Convert.ToDateTime(DataInicioCIPA, ptBr);
        //                    rCIPA.ComissaoEleitoral = System.Convert.ToDateTime(DataInicioCIPA, ptBr);
        //                    rCIPA.Save();


        //                    if (rCIPA.Id == 0)
        //                    {
        //                        txt_Status = txt_Status + "Erro: erro na salva da CIPA (6)" + System.Environment.NewLine;
        //                    }

        //                }

        //            }




        //            //membrocipa-> GetNomeCargo()
        //            //indstatus  1 - ativo   2 - afastado   3 - renunciou
        //            //indgrupomembro  0 - empregado    1 - empregador    2 - secretario
        //            //indtitularsuplente      1 - titular    2 - suplente
        //            //estabilidade    data estabilidade
        //            //numero

        //            // Presidente         ativo, empregador, num=0, titular
        //            // Vice-Presidente    ativo, empregado, num=0, titular
        //            // numero membro      ativo, empregador/empregado, num>0, titular
        //            // numero suplente    ativo, empregador/empregado, num>0, suplente
        //            // secretario         ativo, secretario,titular 
        //            // substituto secretario   ativo, secretario,suplente

        //            MembroCipa rMembro = new MembroCipa();
        //            rMembro.IdCipa = rCIPA;
        //            rMembro.IdEmpregado = rEmpregado;
        //            rMembro.IndStatus = 1;

        //            if (Papel.ToUpper().IndexOf("PRESIDENTE") >= 0 && Papel.ToUpper().IndexOf("VICE") >= 0)
        //            {
        //                rMembro.Numero = 0;
        //                rMembro.IndGrupoMembro = 0;
        //                rMembro.IndTitularSuplente = 1;
        //            }
        //            else if (Papel.ToUpper().IndexOf("PRESIDENTE") >= 0)
        //            {
        //                rMembro.Numero = 0;
        //                rMembro.IndGrupoMembro = 1;
        //                rMembro.IndTitularSuplente = 1;
        //            }
        //            else if (Papel.ToUpper().IndexOf("SECRETÁRIO") >= 0 || Papel.ToUpper().IndexOf("SECRETARIO") >= 0)
        //            {
        //                rMembro.Numero = 0;
        //                rMembro.IndGrupoMembro = 2;

        //                if (Papel.ToUpper().IndexOf("SUBST") >= 0)
        //                    rMembro.IndTitularSuplente = 2;
        //                else
        //                    rMembro.IndTitularSuplente = 1;
        //            }
        //            else
        //            {
        //                if (Indicado.ToUpper().Trim() == "EMPREGADOR")
        //                    rMembro.IndGrupoMembro = 1;
        //                else
        //                    rMembro.IndGrupoMembro = 0;


        //                if (Papel.ToUpper().IndexOf("SUPLENTE") >= 0)
        //                    rMembro.IndTitularSuplente = 2;
        //                else
        //                    rMembro.IndTitularSuplente = 1;

        //                string xNum = System.Text.RegularExpressions.Regex.Match(Papel, @"\d+").Value;

        //                if (xNum.Trim() == "")
        //                    xNum = "1";


        //                rMembro.Numero = System.Convert.ToInt16(xNum);

        //            }

        //            rMembro.NomeMembro = Colaborador;


        //            //antes de salvar procurar para ver se já existe esse colaborador como membro
        //            MembroCipa zMembroBusca = new MembroCipa();
        //            zMembroBusca.Find(" IdCipa = " + rCIPA.Id.ToString() + " and IdEmpregado = " + rMembro.IdEmpregado.Id.ToString());

        //            if (zMembroBusca.Id != 0)
        //            {
        //                txt_Status = txt_Status + "Erro: Colaborador já é membro da CIPA" + System.Environment.NewLine;
        //            }
        //            else
        //            {
        //                rMembro.Save();
        //            }



        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;




        //    }
        //    finally
        //    {

        //        if (txt_Status != "")
        //        {


        //        }
        //        else
        //        {

        //            txt_Status = "Processamento OK";

        //        }
        //    }

        //    return txt_Status;

        //}


        //[WebMethod]
        //public string Executar_CIPA_Evento(string Id, string Empresa, string CodUsuario, string CNPJ ,  string DataInicioCIPA , string DataEvento , string Evento , string Descricao ,
        //                                   string TipoReuniao , string Arquivo , string Conteudo_Arquivo  )
        //{

        //    Empregado rEmpregado = new Empregado();
        //    Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

        //    //int zId = 0;




        //    string rSelect = "";

        //    string txt_Status = "";

        //    try
        //    {



        //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


        //        rSelect = " NomeAbreviado = '" + Empresa + "' and dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

        //        //pegar Id Empresa                    
        //        rEmpresa.Find(rSelect);

        //        //se não achar empresa,  emitir retorno avisando
        //        if (rEmpresa.Id == 0)
        //        {
        //            txt_Status = txt_Status + "Erro: Empresa não localizada ( " + Empresa + "  )" + System.Environment.NewLine;
        //        }






        //        if (txt_Status == "")
        //        {



        //            //ver se CIPA com a data Inicial já existe, senão criar ( preciso ver como criar o calendário, quantos dias para cada evento )
        //            //Poderia ver possibilidade de no repositório CIPA web,  poder anexar arquivo a cada evento,  não só à reuniões, acho que isso não complicado.
        //            Cipa rCIPA = new Cipa();

        //            rCIPA.Find("  IdCliente =" + rEmpresa.Id.ToString() + " and convert( char(10), Edital, 103 ) = '" + DataInicioCIPA + "' ");

        //            if (rCIPA.Id == 0)
        //            {
        //                Cliente rCliente = new Cliente();
        //                rCliente.Find(rEmpresa.Id);

        //                Ilitera.Common.Prestador rPrestador = new Ilitera.Common.Prestador();
        //                rPrestador.Find(" prestador.idjuridicapessoa in (select idjuridicapessoa from juridicapessoa where idpessoa in   (select idpessoa from usuario where idusuario = " + CodUsuario + " ))");

        //                if (rPrestador.Id == 0)
        //                {
        //                    txt_Status = txt_Status + "Erro: erro na salva da CIPA (1)" + System.Environment.NewLine;
        //                }


        //                Obrigacao rObrigacao = new Obrigacao();

        //                if (txt_Status == "")
        //                {
        //                    rObrigacao.Find(" nome = 'CIPA - Criação e Elaboração' ");

        //                    if (rObrigacao.Id == 0)
        //                    {
        //                        txt_Status = txt_Status + "Erro: erro na salva da CIPA (2)" + System.Environment.NewLine;
        //                    }
        //                }


        //                Ilitera.Common.Compromisso rCompromisso = new Ilitera.Common.Compromisso();
        //                rCompromisso.Find(0);

        //                PedidoGrupo rPedidoGrupo = new PedidoGrupo();

        //                if (txt_Status == "")
        //                {
        //                    rPedidoGrupo.DataSolicitacao = System.DateTime.Now;
        //                    rPedidoGrupo.IdCliente = rCliente;
        //                    rPedidoGrupo.Solicitante = "";
        //                    rPedidoGrupo.IdCompromisso = rCompromisso;
        //                    rPedidoGrupo.IdPrestador = rPrestador;
        //                    rPedidoGrupo.Numero = PedidoGrupo.GetNumeroPedido();
        //                    rPedidoGrupo.Save();

        //                    if (rPedidoGrupo.Id == 0)
        //                    {
        //                        txt_Status = txt_Status + "Erro: erro na salva da CIPA (3)" + System.Environment.NewLine;
        //                    }

        //                }


        //                EquipamentoBase rEqpto = new EquipamentoBase();
        //                rEqpto.Find(0);

        //                Pedido rPedido = new Pedido();


        //                if (txt_Status == "")
        //                {

        //                    rPedido.IdCliente = rCliente;
        //                    rPedido.IdPrestador = rPrestador;
        //                    rPedido.DataSolicitacao = System.DateTime.Now;
        //                    rPedido.DataSugestao = System.DateTime.Now;
        //                    rPedido.IdEquipamentoBase = rEqpto;
        //                    rPedido.IdObrigacao = rObrigacao;
        //                    rPedido.IdPedidoGrupo = rPedidoGrupo;
        //                    rPedido.Save();


        //                    if (rPedido.Id == 0)
        //                    {
        //                        txt_Status = txt_Status + "Erro: erro na salva da CIPA (4)" + System.Environment.NewLine;
        //                    }

        //                }



        //                DocumentoBase rDoc = new DocumentoBase();

        //                if (txt_Status == "")
        //                {
        //                    rDoc.Find("NomeDocumento = 'CIPA'");

        //                    if (rDoc.Id == 0)
        //                    {
        //                        txt_Status = txt_Status + "Erro: erro na salva da CIPA (5)" + System.Environment.NewLine;
        //                    }
        //                }




        //                //criar CIPA
        //                rCIPA = new Cipa();

        //                if (txt_Status == "")
        //                {
        //                    rCIPA.Edital = System.Convert.ToDateTime(DataInicioCIPA, ptBr);
        //                    rCIPA.IdCliente = rCliente;
        //                    rCIPA.IdPedido = rPedido;
        //                    rCIPA.IdPrestador = rPrestador;
        //                    rCIPA.Edital = System.Convert.ToDateTime(DataInicioCIPA, ptBr);
        //                    rCIPA.IdDocumentoBase = rDoc;
        //                    rCIPA.DataLevantamento = System.Convert.ToDateTime(DataInicioCIPA, ptBr);
        //                    rCIPA.ComissaoEleitoral = System.Convert.ToDateTime(DataInicioCIPA, ptBr);
        //                    rCIPA.Save();


        //                    if (rCIPA.Id == 0)
        //                    {
        //                        txt_Status = txt_Status + "Erro: erro na salva da CIPA (6)" + System.Environment.NewLine;
        //                    }

        //                }

        //            }


        //            if (txt_Status == "")  //criar evento
        //            {

        //                CipaRepositorio xRepositorio = new CipaRepositorio();

        //                xRepositorio.IdCipa = rCIPA.Id;

        //                xRepositorio.Descricao = Descricao;

        //                xRepositorio.DataHora = System.Convert.ToDateTime(DataEvento, ptBr);

        //                xRepositorio.Evento = Evento;

        //                if (TipoReuniao.Trim().ToUpper() == "ORDINÁRIA" || TipoReuniao.Trim().ToUpper() == "ORDINARIA")
        //                    xRepositorio.TipoReuniao = "O";
        //                else
        //                    xRepositorio.TipoReuniao = "E";

        //                xRepositorio.Save();





        //                if (txt_Status == "" && Conteudo_Arquivo != "")
        //                {
        //                    //xArquivo terá a extensão

        //                    byte[] arrBytes = Convert.FromBase64String(Conteudo_Arquivo);

        //                    //montar com diretório padrão da empresa, nome do colaborador, data e extensão - chave Arquivo
        //                    Cliente xCliente = new Cliente();
        //                    xCliente.Find(System.Convert.ToInt32(rEmpresa.Id));

        //                    string xArq = "";
        //                    Random xRand = new Random();


        //                    //if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
        //                    xArq = "CIPA_" + DataEvento.Replace("/", "") + "_" + xRand.Next().ToString() + "." + Arquivo;


        //                    string uri = "ftp://54.94.157.244:21/" + xCliente.DiretorioPadrao.ToString().Trim().Replace(" ", "%20") + "/Prontuario/" + xArq;

        //                    //string uri = "ftp://54.94.157.244:21/5A%20GT/Prontuario/teste_xml.pdf";



        //                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(uri));
        //                    request.Method = WebRequestMethods.Ftp.UploadFile;

        //                    request.Credentials = new NetworkCredential("lmm", "Lm160521");

        //                    request.UseBinary = true;
        //                    request.UsePassive = true;
        //                    request.ContentLength = arrBytes.Length;
        //                    Stream stream = request.GetRequestStream();
        //                    stream.Write(arrBytes, 0, arrBytes.Length);
        //                    stream.Close();
        //                    FtpWebResponse res = (FtpWebResponse)request.GetResponse();

        //                    xRepositorio.Anexo = @"I:\FotosDocsDigitais\" + xCliente.DiretorioPadrao.ToString().Trim() + "/Prontuario/" + xArq;

        //                    //salvar dados do atestado nos campos do afastamento

        //                    xRepositorio.Save();
        //                }


        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;


        //    }
        //    finally
        //    {

        //        if (txt_Status != "")
        //        {

        //        }
        //        else
        //        {

        //            txt_Status = "Processamento Finalizado";

        //        }
        //    }

        //    return txt_Status;


        //}









        //[WebMethod]
        //public string Executar_Convocacao(string CNPJ, string Empresa, string CodUsuario, string TipoExame, string Colaborador, string Data, string ID, string IdConvocacao)
        //{

        //    ClinicaCliente rClinica = new ClinicaCliente();
        //    Empregado rEmpregado = new Empregado();
        //    Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
        //    int zId = 0;


        //    string rSelect = "";

        //    string txt_Status = "";

        //    string xId = ID;


        //    //int xAux;
        //    string xExames = "";
        //    string xExames2 = "";
        //    string xExames3 = "";
        //    string xExames4 = "";
        //    string xTipo = "";
        //    string xBasico = "0";
        //    string xObs = "";
        //    //int xCont = 0;
        //    //string xEnvio_Email = "N";

        //    string xImpDt = "S";


        //    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");



        //    try
        //    {


        //        rSelect = " NomeAbreviado = '" + Empresa + "' and dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

        //        //pegar Id Empresa                    
        //        rEmpresa.Find(rSelect);

        //        //se não achar empresa,  emitir retorno avisando
        //        if (rEmpresa.Id == 0)
        //        {
        //            txt_Status = txt_Status + "Erro: Empresa não localizada ( " + Empresa + "  )" + System.Environment.NewLine;
        //        }

        //        //quando for convocação ( mailing ) no último irá o link para chamar comunicação
        //        //pode ir com um ID pronto


        //        int xTipoExame = 4;

        //        if (txt_Status == "")
        //        {
        //            //pegar Id Colaborador                       
        //            rEmpregado.Find(" tNo_Empg='" + Colaborador + "'  and nId_Empr = " + rEmpresa.Id.ToString());

        //            //se não achar empregado,  emitir retorno avisando
        //            if (rEmpregado.Id == 0)
        //            {
        //                txt_Status = txt_Status + "Erro: Empregado não localizado ( " + Colaborador + "  )" + System.Environment.NewLine;
        //            }


        //            if (txt_Status == "")
        //            {
        //                rClinica.Find(" IdCliente = " + rEmpresa.Id.ToString() + " and convert( char(12), IdCliente ) + ' ' + convert( char(12), IdClinica ) in ( select top 1 convert( char(12), IdCliente ) + ' ' + convert( char(12), IdClinica ) from ClinicaCliente where IdCliente = " + rEmpresa.Id.ToString() + " ) ");


        //                //se não achar clinica,  emitir retorno avisando
        //                if (rClinica.Id == 0)
        //                {
        //                    txt_Status = txt_Status + "Erro: Clínica não localizada " + System.Environment.NewLine;
        //                }
        //            }




        //            if (TipoExame.ToUpper().Trim() == "PERIODICO" || TipoExame.ToUpper().Trim() == "PERIÓDICO")
        //            {
        //                TipoExame = "4";
        //                xTipoExame = 4;
        //            }
        //            else if (TipoExame.ToUpper().Trim() == "DEMISSIONAL")
        //            {
        //                TipoExame = "2";
        //                xTipoExame = 2;
        //            }
        //            else if (TipoExame.ToUpper().Trim().Substring(0, 5) == "MUDAN")
        //            {
        //                TipoExame = "3";
        //                xTipoExame = 3;
        //            }
        //            else if (TipoExame.ToUpper().Trim().Substring(0, 7) == "RETORNO")
        //            {
        //                TipoExame = "5";
        //                xTipoExame = 5;
        //            }
        //            else if (TipoExame.ToUpper().Trim().Substring(0, 6) == "ADMISS")
        //            {
        //                TipoExame = "1";
        //                xTipoExame = 1;
        //            }


        //            zId = rEmpregado.Id;
        //        }


        //        //validar token - deve existir registro com dados do e-mail de convocação



        //        //código que estaria na outra página
        //        if (txt_Status.Trim() == "")
        //        {


        //            rClinica.IdClinica.Find();





        //            string xEmail = rClinica.IdClinica.Email.Trim();
        //            //xEnvio_Email = "S";






        //            Guid strAux = Guid.NewGuid();



        //            xObs = rClinica.IdClinica.Observacao;  //  txt_Obs.Text.Trim();


        //            //chamar popula para pegar string com exames da guia
        //            string lst_Exames = PopularValueListClinicaClienteExameDicionario(rClinica.IdClinica.Id.ToString(), xTipoExame.ToString().Trim(), rEmpresa.Id.ToString(), rEmpregado.Id.ToString(), CodUsuario, Data);


        //            if (lst_Exames.Substring(0, 2) == "-1")  //retornou erro
        //                txt_Status = lst_Exames;





        //            if (txt_Status.Trim() == "")
        //            {

        //                xExames = lst_Exames;


        //                ////exames na guia, carregar 
        //                //for (xAux = 0; xAux < lst_Exames.Items.Count; xAux++)
        //                //{
        //                //    if (lst_Exames.Items[xAux].Selected == true)
        //                //    {
        //                //        xCont++;

        //                //        if (xCont < 6)
        //                //        {
        //                //            xExames = xExames + " /n " + lst_Exames.Items[xAux].Text;
        //                //        }
        //                //        else if (xCont < 11)
        //                //        {
        //                //            xExames2 = xExames2 + " /n " + lst_Exames.Items[xAux].Text;
        //                //        }
        //                //        else if (xCont < 16)
        //                //        {
        //                //            xExames3 = xExames3 + " /n " + lst_Exames.Items[xAux].Text;
        //                //        }
        //                //        else
        //                //        {
        //                //            xExames4 = xExames4 + " /n " + lst_Exames.Items[xAux].Text;
        //                //        }
        //                //    }

        //            }

        //            //if (chk_Basico.Checked == true)
        //            //{
        //            //    xBasico = "1";
        //            //}


        //            xTipo = xTipoExame.ToString();




        //            ExameBase rexame = new ExameBase();


        //            rexame.Find(" IDEMPREGADO = " + rEmpregado.Id.ToString() + " and IdExameDicionario = " + xTipoExame.ToString() + " and convert( char(10),DataExame,103 ) = '" + Data + "' ");



        //            if (rexame.Id != 0)
        //            {
        //                //MsgBox1.Show("Ilitera.Net", "ASO já foi criado para este tipo de exame e data.", null,
        //                //new EO.Web.MsgBoxButton("OK"));

        //                //TextWriter tw = new StreamWriter(lbl_Arq.Text);
        //                //tw.WriteLine("ASO já foi criado para este exame e data");
        //                //tw.Close();
        //                txt_Status = "ASO já foi criado para este exame e data";

        //            }

        //        }



        //        if (txt_Status.Trim() == "")
        //        {
        //            Cliente cliente = new Cliente();
        //            cliente.Find(System.Convert.ToInt32(rEmpresa.Id.ToString()));


        //            string xAptidao = "";








        //            //pegar data de planejamento + "|" + Data Ultimo exame
        //            string rData = "";
        //            Ilitera.Data.Clientes_Funcionarios xPlan2 = new Ilitera.Data.Clientes_Funcionarios();
        //            rData = xPlan2.Buscar_Data_Planejamento_Exame_Colaborador(rEmpregado.Id, 4, Data);


        //            Ilitera.Data.PPRA_EPI xGuia = new Ilitera.Data.PPRA_EPI();
        //            xGuia.Salvar_Dados_Guia_Encaminhamento(rEmpresa.Id, rEmpregado.Id, xTipoExame.ToString(), xExames, Data, "", rClinica.IdClinica.NomeAbreviado, System.Convert.ToInt32(CodUsuario), "N", rData.Substring(0, 10), rData.Substring(11), "");



        //            //se demissional da Prajna,  colocar data do ASO na data de demissão de colaborador
        //            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA")
        //            {
        //                if (xTipo.ToUpper().Trim() == "D")
        //                {
        //                    //System.Convert.ToInt32(Request["IdEmpregado"].ToString())
        //                    //txt_Data.Text
        //                    Ilitera.Data.PPRA_EPI xDem = new Ilitera.Data.PPRA_EPI();
        //                    xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(rEmpregado.Id.ToString()), Data);

        //                }
        //            }
        //            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
        //            {
        //                if (xTipo.ToUpper().Trim() == "D")
        //                {
        //                    //System.Convert.ToInt32(Request["IdEmpregado"].ToString())
        //                    //txt_Data.Text
        //                    Ilitera.Data.PPRA_EPI xDem = new Ilitera.Data.PPRA_EPI();

        //                    //xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), txt_Data.Text);                         
        //                    xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(rEmpregado.Id.ToString()), Data);


        //                }

        //            }





        //            // Depois testar criação de complementares - talvez eu possa colocar isso junto com código que adiciona exames em lst_Exames






        //            //// criar exames complementares do ASO, se estes não existirem ainda.  Colocar como em espera
        //            //if (cliente.Gerar_Complementares_Guia == true)
        //            //{


        //            //    Ilitera.Opsa.Data.Empregado nEmpregado = new Ilitera.Opsa.Data.Empregado(System.Convert.ToInt32(rEmpregado.Id.ToString()));

        //            //    Ilitera.Common.Juridica xClin = new Ilitera.Common.Juridica();
        //            //    xClin.Find(" IdJuridica = " + rClinica.Id.ToString() );

        //            //    for (int nCont = 0; nCont < lst_IdExames.Items.Count; nCont++)
        //            //    {

        //            //        //checar se exame já existe
        //            //        Int32 xIdExameDicionario = System.Convert.ToInt32(lst_IdExames.Items[nCont].ToString());


        //            //        Complementar xCompl = new Complementar();
        //            //        xCompl.Find(" IdEmpregado = " + rEmpregado.Id.ToString() + " and IdExameDicionario = " + xIdExameDicionario.ToString() + " and convert( char(10), DataExame, 103 ) = '" + Data + "'");

        //            //        if (xCompl.Id == 0)
        //            //        {

        //            //            ExameDicionario xED = new ExameDicionario();
        //            //            xED.Find(" IdExameDicionario = " + xIdExameDicionario.ToString());

        //            //            if (xED.Nome.ToUpper().Trim() == "AUDIOMETRIA")
        //            //            {
        //            //                Audiometria xAud = new Audiometria();
        //            //                xAud.IdExameDicionario = xED;
        //            //                xAud.IdEmpregado = nEmpregado;
        //            //                xAud.DataExame = System.Convert.ToDateTime(Data, ptBr);
        //            //                xAud.IndResultado = (int)Ilitera.Opsa.Data.ResultadoExame.EmEspera;
        //            //                xAud.IdJuridica = xClin;

        //            //                PagamentoClinica xPag = new PagamentoClinica();
        //            //                xAud.IdPagamentoClinica = xPag;

        //            //                xAud.IndAudiometriaTipo = 0;

        //            //                Medico rMedico = new Medico();
        //            //                xAud.IdMedico = rMedico;

        //            //                ConvocacaoExame xConv = new ConvocacaoExame();
        //            //                xAud.IdConvocacaoExame = xConv;

        //            //                Audiometro xAudiometro = new Audiometro();
        //            //                xAud.IdAudiometro = xAudiometro;

        //            //                Ilitera.Common.Compromisso xcompr = new Ilitera.Common.Compromisso();
        //            //                xAud.IdCompromisso = xcompr;

        //            //                xAud.Save();
        //            //            }
        //            //            else
        //            //            {
        //            //                xCompl.IdExameDicionario = xED;
        //            //                xCompl.IdEmpregado = nEmpregado;
        //            //                xCompl.DataExame = System.Convert.ToDateTime(Data, ptBr);
        //            //                xCompl.IndResultado = (int)Ilitera.Opsa.Data.ResultadoExame.EmEspera;
        //            //                xCompl.IdJuridica = xClin;
        //            //                xCompl.Save();
        //            //            }

        //            //        }



        //            //    }


        //            //}




        //            //   Response.Redirect("~\\DadosEmpresa\\RelatorioGuiaASO_Auto.aspx?IliteraSystem=" + strAux.ToString().Substring(0, 5)
        //            //   + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&E1=" + xExames + "&E2=" + xExames2 + "&E3=" + xExames3 + "&E4=" + xExames4 + "&IdClinica=" + cmb_Clinicas.SelectedValue.ToString() + "&H_E=" + txt_Hora.Text + "&D_E=" + txt_Data.Text + "&Tipo=" + xTipo + "&Basico=" + xBasico + "&Mail=" + xEnvio_Email + "&ImpDt=" + xImpDt + "&Apt=" + xAptidao + "&CodUsuario=" + Request["IdUsuario"].ToString().Trim() + "&xId=" + Request["xId"].ToString().Trim() + "&xIdExame=" + xIdExame + "&xArq=" + Request["xArq"].ToString().Trim());



        //            //parte de geração das guias

        //            string xId_Empregado;
        //            string xId_Empresa;
        //            string xId_Clinica;

        //            string xData_Exame;
        //            string xHora_Exame;

        //            string xApt;
        //            string xDtDemissao;
        //            //string xID;
        //            string xId_Exame;



        //            //InicializaWebPageObjects();
        //            //Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);





        //            xId_Empregado = rEmpregado.Id.ToString();
        //            xId_Empresa = rEmpresa.Id.ToString();
        //            xId_Clinica = rClinica.IdClinica.Id.ToString();
        //            xData_Exame = Data;
        //            xHora_Exame = "";




        //            xApt = xAptidao;

        //            xId_Exame = TipoExame;


        //            xDtDemissao = "";




        //            Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();


        //            //criando ASO
        //            xTipoExame = System.Convert.ToInt16(xTipo);



        //            Cliente zCliente = new Cliente();
        //            zCliente.Find(System.Convert.ToInt32(xId_Empresa));



        //            ExameClinicoFacade exame = new ExameClinicoFacade();

        //            exame.Prontuario = "";
        //            //exame.Observacao = "";

        //            empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + rEmpregado.Id.ToString());

        //            exame.IdEmpregado = empregado;

        //            exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
        //            exame.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();

        //            exame.apt_Espaco_Confinado = "";
        //            exame.apt_Trabalho_Altura = "";
        //            exame.apt_Transporte = "";
        //            exame.apt_Submerso = "";
        //            exame.apt_Eletricidade = "";
        //            exame.apt_Alimento = "";
        //            exame.apt_Brigadista = "";
        //            exame.apt_Socorrista = "";



        //            ExameDicionario xExameDicionario = new ExameDicionario();

        //            xExameDicionario.Find(xTipoExame);

        //            Juridica xJuridica = new Juridica();
        //            xJuridica.Find(Convert.ToInt32(rClinica.IdClinica.Id));

        //            exame.IdExameDicionario = xExameDicionario;
        //            exame.IdJuridica = xJuridica;

        //            //exame.IdExameDicionario.Find( xTipoExame );

        //            //exame.IdJuridica.Find(Convert.ToInt32(Request["IdClinica"].ToString()));


        //            exame.DataExame = System.Convert.ToDateTime(xData_Exame, ptBr);

        //            if (xTipoExame == 2 && xDtDemissao.Trim() != "") exame.DataDemissao = System.Convert.ToDateTime(xDtDemissao, ptBr);

        //            exame.IndResultado = 3;


        //            Medico xMedico = new Medico();
        //            xMedico.Find(1111);  // -2133369037);

        //            exame.IdMedico = xMedico;

        //            //Usuario xusuario = new Usuario();
        //            //xusuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

        //            //exame.UsuarioId = 


        //            //Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

        //            Entities.Usuario usuario = new Entities.Usuario();
        //            usuario.IdUsuario = System.Convert.ToInt32( System.Convert.ToInt32( CodUsuario) );


        //            int zStatus = 0;
        //            try
        //            {
        //                zStatus = exame.Save(System.Convert.ToInt32(System.Convert.ToInt32(CodUsuario)));
        //            }
        //            catch (Exception ex)
        //            {
        //                if (ex.Message.ToUpper() != "MÉTODO NÃO-ESTÁTICO REQUER UM DESTINO." && ex.Message.ToUpper() != "NON-STATIC METHOD REQUIRES A TARGET.")
        //                    throw new Exception(ex.Message.ToString());


        //            }

        //            //exame.Save();

        //            //if ( cliente.Gerar_Complementares_Guia==true)  // criar exames complementares do ASO, se estes não existirem ainda.  Colocar como em espera
        //            //{






        //            //}




        //            Clinico exame2 = new Clinico();
        //            exame2.Find(exame.Id);


        //            exame2.apt_Trabalho_Altura2 = "0";
        //            exame2.apt_Espaco_Confinado2 = "0";
        //            exame2.apt_Transporte2 = "0";
        //            exame2.apt_Submerso2 = "0";
        //            exame2.apt_Eletricidade2 = "0";
        //            exame2.apt_Aquaviario2 = "0";
        //            exame2.apt_Alimento2 = "0";
        //            exame2.apt_Brigadista2 = "0";
        //            exame2.apt_Socorrista2 = "0";


        //            if (xApt.IndexOf("A") >= 0) exame2.apt_Trabalho_Altura2 = "1";
        //            if (xApt.IndexOf("C") >= 0) exame2.apt_Espaco_Confinado2 = "1";
        //            if (xApt.IndexOf("T") >= 0) exame2.apt_Transporte2 = "1";
        //            if (xApt.IndexOf("S") >= 0) exame2.apt_Submerso2 = "1";
        //            if (xApt.IndexOf("E") >= 0) exame2.apt_Eletricidade2 = "1";
        //            if (xApt.IndexOf("Q") >= 0) exame2.apt_Aquaviario2 = "1";
        //            if (xApt.IndexOf("M") >= 0) exame2.apt_Alimento2 = "1";
        //            if (xApt.IndexOf("B") >= 0) exame2.apt_Brigadista2 = "1";
        //            if (xApt.IndexOf("R") >= 0) exame2.apt_Socorrista2 = "1";






        //            Int16 zTamanho = 3;

        //            exame2.IdEmpregado.Find();
        //            exame2.IdEmpregado.nID_EMPR.Find();
        //            exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();

        //            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)//exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI"  )  // && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
        //            {
        //                zTamanho = 4;
        //            }


        //            //string xTitulo;

        //            //if (xId_Exame != "4")  //apenas guia de complementar
        //            //{
        //            //    zTamanho = 1;
        //            //    xTitulo = "Kit Guia Complementar - Convocação";
        //            //}
        //            //else
        //            //{
        //            //    xTitulo = "Kit Guia/ASO/PCI - Convocação"; ;
        //            //}

        //            CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[zTamanho];

        //            //Cliente zCliente = new Cliente();
        //            //zCliente.Find(System.Convert.ToInt32(xId_Empresa));





        //            //se for apenas guia de encaminhamento - convocação exames complementares


        //            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
        //            {
        //                //RptGuia_Prajna report0 = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt).GetReport();
        //                RptGuia_Nova_Prajna report0 = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, exame2.IdEmpregadoFuncao.Id).GetReport_Nova();
        //                //CreatePDFDocument(report, this.Response);
        //                reports[0] = report0;
        //            }
        //            else
        //            {
        //                //RptGuia report0 = new DataSourceGuia(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt).GetReport();
        //                RptGuia_Nova report0 = new DataSourceGuia(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, exame2.IdEmpregadoFuncao.Id).GetReport_Nova();
        //                //CreatePDFDocument(report, this.Response);
        //                reports[0] = report0;
        //            }







        //            if (xId_Exame == "4")
        //            {



        //                RptAso report2 = new DataSourceExameAsoPci(exame2).GetReport();

        //                //tenho o ID do ASO para colocar no registro da guia gerada ?
        //                //dar um update ?




        //                //Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
        //                //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

        //                //CreatePDFDocument(report, this.Response);


        //                //Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();

        //                //Clinico exame2 = new Clinico(Convert.ToInt32(Request["IdExame"]));

        //                reports[1] = report2;

        //                Juridica xClin = new Juridica();
        //                xClin.Find(rClinica.Id);

        //                string xClinNome = "";

        //                if (xClin != null) xClinNome = xClin.NomeAbreviado.ToUpper().Trim();


        //                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.Trim() == "Sied_Novo" || xClinNome.IndexOf("DAITI") >= 0 || xClinNome.IndexOf("IPATINGA") >= 0)
        //                {
        //                    RptPci report3 = new DataSourceExameAsoPci(exame2).GetReportPci_Antigo();
        //                    reports[2] = report3;
        //                }
        //                else
        //                {

        //                    exame2.IdEmpregado.Find();
        //                    exame2.IdEmpregado.nID_EMPR.Find();
        //                    exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();

        //                    //if (exame.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI" && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini

        //                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0) //  && exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI")  // && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
        //                    {
        //                        RptPci_Novo_Capgemini report3 = new DataSourceExameAsoPci(exame2).GetReportPciCapgemini();
        //                        reports[2] = report3;
        //                    }
        //                    else
        //                    {
        //                        RptPci_Novo report3 = new DataSourceExameAsoPci(exame2).GetReportPci();
        //                        reports[2] = report3;
        //                    }
        //                }
        //                //RptPci report3 = new DataSourceExameAsoPci(exame2).GetReportPci();


        //                exame2.IdEmpregado.Find();
        //                exame2.IdEmpregado.nID_EMPR.Find();
        //                exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();


        //                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0) //exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI"  ) // && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
        //                {
        //                    Int32 xIdExame = exame2.Id;

        //                    Carregar_Dados_Anamnese_Exame(exame2.Id);

        //                    RptAnamnese report4 = new DataSourceExameAnamnese(xIdExame, true).GetReport();
        //                    reports[3] = report4;

        //                }



        //            }


        //            //CreatePDFMerged(reports, this.Response, "", false, xID);

        //            //HttpResponse response = this.Response;
        //            string watermark = "";
        //            bool RenumerarPaginas = false;



        //            Stream[] streams = new Stream[reports.Length];

        //            int i = 0;



        //            foreach (CrystalDecisions.CrystalReports.Engine.ReportClass report in reports)
        //            {
        //                if (RenumerarPaginas)
        //                    report.ReportDefinition.ReportObjects["PaginaNdeN1"].ObjectFormat.EnableSuppress = true;

        //                streams[i] = report.ExportToStream(ExportFormatType.PortableDocFormat);

        //                report.Close();

        //                i++;
        //            }


        //            //CreatePDFMerged(streams, response, watermark, RenumerarPaginas, xId);





        //            MemoryStream reportStream = PdfSharpUtility.MergeReports(streams, string.Empty, watermark, RenumerarPaginas);



        //            string xPath = "I:\\temp\\guia_" + xId.Trim() + ".pdf";

        //            //if (xEnvio_Email == "S")
        //            //{

        //            //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
        //            //{




        //            FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
        //            // Initialize the bytes array with the stream length and then fill it with data
        //            byte[] bytesInStream = new byte[reportStream.Length];
        //            reportStream.Read(bytesInStream, 0, bytesInStream.Length);
        //            // Use write method to write to the file specified above
        //            fileStream.Write(bytesInStream, 0, bytesInStream.Length);

        //            fileStream.Flush();

        //            fileStream.Dispose();
        //            fileStream = null;

        //            Clinica xClinica = new Clinica(exame.IdJuridica.Id);

        //            Cliente xCliente = new Cliente(exame.IdEmpregado.nID_EMPR.Id);
        //            string xEmpresa = "";

        //            if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
        //            {
        //                if (xCliente.IdJuridicaPai.ToString().ToUpper().IndexOf("KNOX") > 0)
        //                {
        //                    xEmpresa = xCliente.IdJuridicaPai.ToString();
        //                }
        //                else
        //                {
        //                    xEmpresa = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();
        //                }
        //            }
        //            else
        //            {
        //                xEmpresa = xCliente.GetNomeEmpresa();
        //            }



        //            string xCorpo = "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> </p>" +
        //                             "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> DADOS DO AGENDAMENTO: </p><br>" +
        //                             "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +
        //                             "RG: " + exame.IdEmpregado.tNO_IDENTIDADE.ToString() + "<br>" +
        //                             "Data de Nascimento: " + exame.IdEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
        //                             "Empresa:  " + xEmpresa + "<br>" +
        //                             "CNPJ: " + exame.IdEmpregado.nID_EMPR.GetCnpj() + "<br>" +
        //                             "Cargo: " + exame.IdEmpregadoFuncao.nID_FUNCAO.ToString() + "<br>" +
        //                              "Setor: " + exame.IdEmpregadoFuncao.nID_SETOR.ToString() + "<br>" +
        //                              "Unidade de Trabalho: " + exame.IdEmpregadoFuncao.GetLocalDeTrabalho(exame.IdEmpregado.nID_EMPR) + "<br>" +
        //                              "Clinica: " + exame.IdJuridica.NomeAbreviado + "<br>" +
        //                              "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + exame.IdJuridica.GetContatoTelefonico() + "<br>" +
        //                              "Tipo de Exame: " + exame.IdExameDicionario.Nome + " <br>" +
        //                              "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";


        //            //string xCorpo = "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> </p>" +
        //            //                 "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> DADOS DO AGENDAMENTO: </p><br>" +
        //            //                 "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +                             
        //            //                  "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";



        //            if (xId.IndexOf("KIT") >= 0)
        //            {
        //                //procurar tblConvocacao e enviar e-mail com kit para colaborador

        //                //attach arquivo - por exemplo i:\\temp\\guia_KIT_962020797.pdf   ( guia_ + ID + .pdf )



        //                Convocacao rConvocacao = new Convocacao();
        //                rConvocacao.Find(System.Convert.ToInt32(xId.Substring(4)));




        //                string zEmail = "";

        //                if (rConvocacao.eMail_Envio.IndexOf("|") > 0)
        //                    zEmail = rConvocacao.eMail_Envio.Substring(0, rConvocacao.eMail_Envio.IndexOf("|") - 1).Trim();
        //                else
        //                    zEmail = rConvocacao.eMail_Envio;



        //                rConvocacao.hDt_Convocacao = exame.DataExame;
        //                rConvocacao.Save();








        //            }


        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        if (txt_Status.Trim() == "")
        //            txt_Status = "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;
        //        else
        //            txt_Status = txt_Status + System.Environment.NewLine + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;
        //        //Session["zErro"] = txt_Status;

        //        return txt_Status;

        //    }
        //    finally
        //    {

        //        if (txt_Status != "")
        //        {
        //            //Session["zErro"] = txt_Status;


        //        }
        //        else
        //        {
        //            // rClinica.IdClinica.Find();



        //            //Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
        //            //Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia_Auto.aspx?IdEmpregado=" + zId.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + xCodUsuario + "&TipoExame=" + xTipoExame + "&Data=" + xData + "&IdClinica=" + rClinica.IdClinica.Id.ToString().Trim() + "&CodUsuario=" + xCodUsuario + "&xId=" + xID);
        //            // HttpContext.Current.Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia_Auto.aspx?IdEmpregado=" + zId.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + CodUsuario + "&TipoExame=" + TipoExame + "&Data=" + Data + "&IdClinica=" + rClinica.IdClinica.Id.ToString().Trim() + "&CodUsuario=" + CodUsuario + "&xId=" + ID + "&xIdExame=4&xArq=" + xArq);


        //            //Session["zErro"] = txt_Status + " " + "Processamento Finalizado";

        //            txt_Status = "01 - Processamento Finalizado";

        //        }
        //    }

        //    //string zErro = "";

        //    //xArq = "I:\\temp\\ws_" + xArq + ".txt";

        //    //if (File.Exists(xArq))
        //    //    zErro = File.ReadAllText(xArq);

        //    return txt_Status;

        //}





        //private void Carregar_Dados_Anamnese_Exame(Int32 zExame)
        //{


        //    if (zExame == 0)
        //    {
        //        return;
        //    }



        //    List<Anamnese_Exame> AnExame = new Anamnese_Exame().Find<Anamnese_Exame>(" IdExameBase = " + zExame);


        //    Clinico vExame = new Clinico();
        //    vExame.Find(zExame);


        //    if (AnExame.Count == 0)
        //    {
        //        //trazer padrão para cliente
        //        vExame.IdEmpregado.Find();
        //        vExame.IdEmpregado.nID_EMPR.Find();
        //        List<Anamnese_Dinamica> anExamePadrao = new Anamnese_Dinamica().Find<Anamnese_Dinamica>(" IdPessoa = " + vExame.IdEmpregado.nID_EMPR.Id);


        //        if (anExamePadrao.Count == 0)
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            Ilitera.Data.Clientes_Funcionarios xAnam = new Ilitera.Data.Clientes_Funcionarios();
        //            xAnam.Carregar_Anamnese_Dinamica(vExame.IdEmpregado.nID_EMPR.Id, zExame);


        //            //foreach (Anamnese_Dinamica zPadrao in anExamePadrao)
        //            //{
        //            //    Anamnese_Exame rTestes = new Anamnese_Exame();

        //            //    rTestes.IdAnamneseDinamica = zPadrao.Id;
        //            //    rTestes.IdExameBase = vExame.Id;
        //            //    rTestes.Resultado = "N";
        //            //    rTestes.Peso = zPadrao.Peso;
        //            //    rTestes.Save();
        //            //}

        //        }

        //    }


        //    return;


        //}



        //protected void Envio_Email_Prajna(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresaExameClinicoFacade, ExameClinicoFacade exame, string xCodUsuario, Int32 xIdEmpresa)
        //{

        //    string xDestinatario = "";

        //    //enviar e-mail para clinica e prajna

        //    MailMessage objEmail = new MailMessage();

        //    //rementente do email           
        //    objEmail.From = new MailAddress("agendamento@5aessence.com.br");


        //    //para
        //    string xEmail = "";

        //    xEmail = exame.IdJuridica.Email.ToString().Trim();

        //    if (xEmail == "")
        //    {
        //        throw new Exception("Clínica não possui e-mail cadastrado.");
        //    }

        //    if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

        //    objEmail.To.Add(xEmail);
        //    xDestinatario = xEmail + "; agendamento@5aessence.com.br ;";

        //    objEmail.CC.Add("agendamento@5aessence.com.br");


        //    //cópia para usuário logado
        //    //Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];
        //    Entities.Usuario usuario = new Entities.Usuario();
        //    usuario.IdUsuario = System.Convert.ToInt32(xCodUsuario);


        //    Prestador xPrestador = new Prestador(usuario.IdPrestador);


        //    if (xPrestador.Email != null)
        //    {
        //        xEmail = xPrestador.Email.ToString().Trim();
        //        if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

        //        if (xEmail.Trim() != "")
        //        {
        //            objEmail.Bcc.Add(xEmail);
        //            xDestinatario = xDestinatario + xEmail + ";";
        //        }
        //    }

        //    objEmail.Priority = MailPriority.Normal;
        //    objEmail.IsBodyHtml = true;

        //    objEmail.Subject = xSubject;
        //    objEmail.Body = xBody;
        //    objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
        //    objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

        //    Attachment xItem = new Attachment(xAttach);
        //    objEmail.Attachments.Add(xItem);

        //    SmtpClient objSmtp = new SmtpClient();
        //    //objSmtp.Host = "mail.exchange.locaweb.com.br";
        //    //objSmtp.Port = 587;
        //    //objSmtp.Credentials = new NetworkCredential("agendamento@essencenet.com.br", "kr.prj1705");

        //    objSmtp.EnableSsl = true;

        //    //objSmtp.Host = "outlook.office.com";
        //    objSmtp.Host = "smtp.office365.com";
        //    objSmtp.Port = 587;
        //    objSmtp.Credentials = new NetworkCredential("agendamento@5aessence.com.br", "Agend_5060");

        //    objSmtp.Send(objEmail);

        //    Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
        //    xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Kit Guia / ASO / PCI");

        //    return;

        //}




        //protected void Envio_Email_Ilitera(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        //{
        //    string xDestinatario = "";
        //    //enviar e-mail para clinica e prajna

        //    MailMessage objEmail = new MailMessage();

        //    //rementente do email
        //    objEmail.From = new MailAddress("agendamento.sp.sto@ilitera.com.br");


        //    //caixa-postal de onde será enviado o e-mail
        //    //objEmail.ReplyTo = new MailAddress("email@seusite.com.br");

        //    //para
        //    //objEmail.To.Add("lasdowsky@gmail.com");
        //    //string xEmail = "";

        //    //xEmail = exame.IdJuridica.Email.ToString().Trim();

        //    //if (xEmail == "")
        //    //{
        //    //    throw new Exception("Clínica não possui e-mail cadastrado.");
        //    //}

        //    //if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

        //    //objEmail.To.Add(xEmail);

        //    objEmail.To.Add(xPara);
        //    //xDestinatario = xEmail + "; atendimento@ilitera.com.br ;";

        //    //objEmail.Bcc.Add("oculto@provedor.com.br");

        //    //objEmail.CC.Add("atendimento@ilitera.com.br");
        //    //objEmail.CC.Add("atendimento2@ilitera.com.br");

        //    objEmail.Priority = MailPriority.Normal;
        //    objEmail.IsBodyHtml = true;

        //    objEmail.Subject = xSubject;
        //    objEmail.Body = xBody;
        //    objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
        //    objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

        //    Attachment xItem = new Attachment(xAttach);
        //    objEmail.Attachments.Add(xItem);

        //    SmtpClient objSmtp = new SmtpClient();
        //    objSmtp.Host = "smtp.ilitera.com.br";
        //    objSmtp.Port = 587;
        //    objSmtp.Credentials = new NetworkCredential("agendamento.sp.sto@ilitera.com.br", "bibi6096");


        //    Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
        //    //checar se não é e-mail duplicado

        //    //DataSet rDs = xEnvio.Checar_Envio_Email(xIdEmpregado, xIdEmpresa, xDestinatario, "Kit Guia / ASO / PCI");

        //    //if (rDs.Tables[0].Rows.Count == 0)
        //    //{
        //    objSmtp.Send(objEmail);
        //    xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Kit Guia / ASO / PCI");
        //    //}


        //    return;

        //}



        //private string PopularValueListClinicaClienteExameDicionario(string xValue, string xIdExame, string xIdEmpresa, string xIdEmpregado, string xCodUsuario, string xData)
        //{

        //    string lst_Exames = "";

        //    DataSet dsExames = new ExameDicionario().GetIdNome("Nome", " IdExameDicionario IN (SELECT IdExameDicionario FROM ClinicaExameDicionario WHERE IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + xIdEmpresa + " and IdClinica = " + xValue + " ))");
        //    DataSet ds = new ClinicaExameDicionario().Get("IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + xIdEmpresa + " " + " and IdClinica = " + xValue + " ) " + " AND IDCLINICAEXAMEDICIONARIO IN " +
        //     "( " +
        //     "   SELECT IdClinicaExameDicionario " +
        //     "   FROM ClinicaClienteExameDicionario  " +
        //     "    WHERE IdClinicaCliente IN ( " +
        //     "      SELECT IdClinicaCliente FROM ClinicaCliente " +
        //     "      WHERE IdCliente=" + xIdEmpresa + " " + " and IdClinica = " + xValue + " and IsAutorizado = 1 ) ) ");


        //    ////carregar dados da clinica
        //    Clinica xClinica = new Clinica(System.Convert.ToInt32(xValue));



        //    //pegar exames de PCMSO do funcionário
        //    Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();
        //    empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + xIdEmpregado);

        //    Clinico exame = new Clinico();
        //    exame.IdEmpregado = empregado;
        //    exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
        //    exame.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();

        //    exame.UsuarioId = System.Convert.ToInt32(xCodUsuario);

        //    Pcmso pcmso = new Pcmso();
        //    pcmso = exame.IdPcmso;

        //    bool zClinico = false;


        //    if (pcmso.IdLaudoTecnico != null)
        //    {
        //        List<Ghe> ghes = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + pcmso.IdLaudoTecnico.Id);

        //        Ghe ghe;

        //        if (ghes == null || ghes.Count == 0)
        //            ghe = exame.IdEmpregadoFuncao.GetGheEmpregado(pcmso.IdLaudoTecnico);
        //        else
        //        {
        //            int IdGhe = exame.IdEmpregadoFuncao.GetIdGheEmpregado(pcmso.IdLaudoTecnico);

        //            ghe = ghes.Find(delegate (Ghe g) { return g.Id == IdGhe; });
        //        }



        //        if (ghe == null)
        //        {
        //            return "-10   Colaborador não está alocado em GHE,  não é possível criar Guia de Encaminhamento/ASO.";
        //        }



        //        bool zDesconsiderar = false;
        //        string xDataBranco = "";

        //        string sExamesOcupacionais = "";

        //        Cliente cliente = new Cliente();
        //        cliente.Find( System.Convert.ToInt32( xIdEmpresa) );


        //        //Wagner 04/07/2018 - ver exames complementares que estão com data ( ver se opção exibir data complementares está ativa )
        //        // esses exames com datas não precisam aparecer na guia, pois não precisam ser solicitados.

        //        //se for para desconsiderar data de complementares a partir de certa data, usar o esquema que já existe com xDataBranco
        //        if (xDataBranco == "")
        //        {
        //            if (cliente.Ativar_DesconsiderarCompl == true)
        //            {
        //                if (cliente.Dias_Desconsiderar > 0)
        //                {
        //                    zDesconsiderar = true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            zDesconsiderar = false;
        //        }


        //        //Clinico clinico = new Clinico();

        //        //clinico.IdPcmso = pcmso;
        //        //clinico.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(pcmso.IdLaudoTecnico, empregado);


        //        //clinico.UsuarioId = System.Convert.ToInt32(Request["IdUsuario"].ToString());
        //        ExameDicionario rDicionario = new ExameDicionario();

        //        rDicionario.Find( System.Convert.ToInt32( xIdExame) );

        //        exame.IdExameDicionario = rDicionario;

        //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
        //        exame.DataExame = System.Convert.ToDateTime(xData, ptBr);



        //        //pegar exames do ASO
        //        string sExamesASO = "";
        //        string sExamesASO_Aptidao = "";


        //        if (xIdExame == "3")   //mudança de função
        //        {
        //            // procurar ghe_ant primeiro, na mesma classif.funcional
        //            // se nao encontrar, procurar classif.funcional anterior e ghe


        //            if (cliente.GHEAnterior_MudancaFuncao == true)
        //            {
        //                // procurar ghe_ant primeiro, na mesma classif.funcional
        //                // se nao encontrar, procurar classif.funcional anterior e ghe

        //                Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

        //                DataSet xdS = xGHE.Trazer_Laudos_GHEs_Colaborador(empregado.Id);

        //                if (xdS.Tables[0].Rows.Count < 2)
        //                {                            
        //                    return  "-14  O empregado " + empregado.tNO_EMPG  + " está associado a apenas um GHE/Classif.Funcional, logo, não é possível gerar o ASO de Mudança de Função. ";                            
        //                }


        //                int znAux = 0;
        //                Int32 zGHE_Atual = 0;
        //                Int32 zGHE_Ant = 0;


        //                foreach (DataRow row in xdS.Tables[0].Rows)
        //                {
        //                    znAux++;

        //                    if (znAux == 1) zGHE_Atual = System.Convert.ToInt32(row["nId_Func"].ToString());
        //                    else if (znAux == 2) zGHE_Ant = System.Convert.ToInt32(row["nId_Func"].ToString());
        //                    else break;

        //                }


        //                //ghe_ant
        //                //ghe
        //                Ghe zGhe1 = new Ghe();
        //                zGhe1.Find(zGHE_Atual);
        //                Ghe zGhe2 = new Ghe();
        //                zGhe2.Find(zGHE_Ant);

        //                if (zDesconsiderar == false)
        //                    sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao(zGhe1, zGhe2, false, cliente.Exibir_Datas_Exames_ASO, xDataBranco, zDesconsiderar);
        //                else
        //                    sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao_Desconsiderar(zGhe1, zGhe2, false, cliente.Exibir_Datas_Exames_ASO, exame, cliente.Dias_Desconsiderar);
        //            }
        //            else
        //            {
        //                if (zDesconsiderar == false)
        //                    sExamesASO = exame.GetPlanejamentoExamesAso_Formatado(ghe, false, cliente.Exibir_Datas_Exames_ASO, xDataBranco, zDesconsiderar);
        //                else
        //                    sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Desconsiderar(ghe, false, cliente.Exibir_Datas_Exames_ASO, exame, cliente.Dias_Desconsiderar);
        //            }


        //        }
        //        else
        //        {

        //            if (xIdExame == "2")  //demissional
        //            {
        //                if (zDesconsiderar == false)
        //                    sExamesASO = exame.GetPlanejamentoExamesAso_Formatado(ghe, false, false, xDataBranco, zDesconsiderar);
        //                else
        //                    sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Desconsiderar(ghe, false, false, exame, cliente.Dias_Desconsiderar);
        //            }
        //            else
        //            {
        //                if (zDesconsiderar == false)
        //                    sExamesASO = exame.GetPlanejamentoExamesAso_Formatado(ghe, false, cliente.Exibir_Datas_Exames_ASO, xDataBranco, zDesconsiderar);
        //                else
        //                    sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Desconsiderar(ghe, false, cliente.Exibir_Datas_Exames_ASO, exame, cliente.Dias_Desconsiderar);
        //            }
        //        }





        //        //pegar exames para guia
        //        if (xIdExame == "3")   //mudança de função
        //        {

        //            if (cliente.GHEAnterior_MudancaFuncao == true)
        //            {
        //                // procurar ghe_ant primeiro, na mesma classif.funcional
        //                // se nao encontrar, procurar classif.funcional anterior e ghe

        //                Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

        //                DataSet xdS = xGHE.Trazer_Laudos_GHEs_Colaborador(empregado.Id);

        //                if (xdS.Tables[0].Rows.Count < 2)
        //                {
        //                    return "-13  O empregado " + empregado.tNO_EMPG + " está associado a apenas um GHE/Classif.Funcional, logo, não é possível gerar o ASO de Mudança de Função. ";
        //                }


        //                int znAux = 0;
        //                Int32 zGHE_Atual = 0;
        //                Int32 zGHE_Ant = 0;


        //                foreach (DataRow row in xdS.Tables[0].Rows)
        //                {
        //                    znAux++;

        //                    if (znAux == 1) zGHE_Atual = System.Convert.ToInt32(row["nId_Func"].ToString());
        //                    else if (znAux == 2) zGHE_Ant = System.Convert.ToInt32(row["nId_Func"].ToString());
        //                    else break;

        //                }


        //                //ghe_ant
        //                //ghe
        //                Ghe zGhe1 = new Ghe();
        //                zGhe1.Find(zGHE_Atual);
        //                Ghe zGhe2 = new Ghe();
        //                zGhe2.Find(zGHE_Ant);


        //                sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao_Guia(zGhe1, zGhe2, true);
        //            }
        //            else
        //            {
        //                sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "M");
        //            }


        //        }
        //        else if (xIdExame == "1")  // admissao
        //        {
        //            sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "A");
        //        }
        //        else if (xIdExame == "2")  // demissao
        //        {
        //            sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "D");
        //        }
        //        else if (xIdExame == "5")  // retorno
        //        {
        //            sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "R");
        //        }
        //        else if (xIdExame == "4")   //periódico
        //        {
        //            sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "P");
        //        }
        //        else
        //        {
        //            sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true);
        //        }



        //        //checar se têm exames de aptidão
        //        string sExamesOcupacionais_Aptidao = "";

        //        Empregado_Aptidao xAptidao = new Empregado_Aptidao();
        //        xAptidao.Find(" nId_Empregado = " + empregado.Id.ToString());


        //        GHE_Aptidao zAptidao = new GHE_Aptidao();
        //        zAptidao.Find("nId_Func = " + ghe.Id.ToString());

        //        if (xAptidao.Id != 0 || zAptidao.Id != 0)
        //        {
        //            if ((xAptidao.apt_Alimento == true || xAptidao.apt_Aquaviario == true || xAptidao.apt_Eletricidade == true || xAptidao.apt_Espaco_Confinado == true ||
        //                      xAptidao.apt_Submerso == true || xAptidao.apt_Trabalho_Altura == true || xAptidao.apt_Transporte == true || xAptidao.apt_Brigadista == true || xAptidao.apt_Socorrista == true) ||
        //                      (zAptidao.apt_Alimento == true || zAptidao.apt_Aquaviario == true || zAptidao.apt_Eletricidade == true || zAptidao.apt_Espaco_Confinado == true ||
        //                     zAptidao.apt_Submerso == true || zAptidao.apt_Trabalho_Altura == true || zAptidao.apt_Transporte == true || zAptidao.apt_Brigadista == true || zAptidao.apt_Socorrista == true))
        //            {

        //                Empregado_Aptidao nAptidao = new Empregado_Aptidao();


        //                nAptidao.nId_Empregado = empregado.Id;

        //                //juntando aptidao do empregado com do PPRA-GHE
        //                if (xAptidao.Id != 0 && zAptidao.Id != 0)
        //                {
        //                    nAptidao.apt_Alimento = xAptidao.apt_Alimento || zAptidao.apt_Alimento;
        //                    nAptidao.apt_Aquaviario = xAptidao.apt_Aquaviario || zAptidao.apt_Aquaviario;
        //                    nAptidao.apt_Brigadista = xAptidao.apt_Brigadista || zAptidao.apt_Brigadista;
        //                    nAptidao.apt_Eletricidade = xAptidao.apt_Eletricidade || zAptidao.apt_Eletricidade;
        //                    nAptidao.apt_Espaco_Confinado = xAptidao.apt_Espaco_Confinado || zAptidao.apt_Espaco_Confinado;
        //                    nAptidao.apt_Socorrista = xAptidao.apt_Socorrista || zAptidao.apt_Socorrista;
        //                    nAptidao.apt_Submerso = xAptidao.apt_Submerso || zAptidao.apt_Submerso;
        //                    nAptidao.apt_Trabalho_Altura = xAptidao.apt_Trabalho_Altura || zAptidao.apt_Trabalho_Altura;
        //                    nAptidao.apt_Transporte = xAptidao.apt_Transporte || zAptidao.apt_Transporte;
        //                }
        //                else if (xAptidao.Id != 0)
        //                {
        //                    nAptidao.apt_Alimento = xAptidao.apt_Alimento;
        //                    nAptidao.apt_Aquaviario = xAptidao.apt_Aquaviario;
        //                    nAptidao.apt_Brigadista = xAptidao.apt_Brigadista;
        //                    nAptidao.apt_Eletricidade = xAptidao.apt_Eletricidade;
        //                    nAptidao.apt_Espaco_Confinado = xAptidao.apt_Espaco_Confinado;
        //                    nAptidao.apt_Socorrista = xAptidao.apt_Socorrista;
        //                    nAptidao.apt_Submerso = xAptidao.apt_Submerso;
        //                    nAptidao.apt_Trabalho_Altura = xAptidao.apt_Trabalho_Altura;
        //                    nAptidao.apt_Transporte = xAptidao.apt_Transporte;
        //                }
        //                else if (zAptidao.Id != 0)
        //                {
        //                    nAptidao.apt_Alimento = zAptidao.apt_Alimento;
        //                    nAptidao.apt_Aquaviario = zAptidao.apt_Aquaviario;
        //                    nAptidao.apt_Brigadista = zAptidao.apt_Brigadista;
        //                    nAptidao.apt_Eletricidade = zAptidao.apt_Eletricidade;
        //                    nAptidao.apt_Espaco_Confinado = zAptidao.apt_Espaco_Confinado;
        //                    nAptidao.apt_Socorrista = zAptidao.apt_Socorrista;
        //                    nAptidao.apt_Submerso = zAptidao.apt_Submerso;
        //                    nAptidao.apt_Trabalho_Altura = zAptidao.apt_Trabalho_Altura;
        //                    nAptidao.apt_Transporte = zAptidao.apt_Transporte;
        //                }

        //                Cliente xCliente = new Cliente();
        //                xCliente.Find(pcmso.IdCliente.Id);

        //                ExameDicionario rDic = new ExameDicionario();
        //                rDic.Find(System.Convert.ToInt32(xIdExame));


        //                exame.IdExameDicionario = rDic;
        //                exame.IdEmpregado = empregado;


        //                sExamesOcupacionais_Aptidao = exame.GetPlanejamentoExamesAso_Guia_Aptidao(nAptidao, xCliente.Exibir_Datas_Exames_ASO, "F", sExamesOcupacionais);

        //                sExamesASO_Aptidao = exame.GetPlanejamentoExamesAso_Formatado_Aptidao(nAptidao, xCliente.Exibir_Datas_Exames_ASO, xDataBranco, sExamesOcupacionais, zDesconsiderar, exame);

        //            }
        //        }

        //        if (sExamesOcupacionais.Trim() != "")
        //            sExamesOcupacionais = sExamesOcupacionais + sExamesOcupacionais_Aptidao;
        //        else
        //            sExamesOcupacionais = sExamesOcupacionais_Aptidao;

        //        string txt_Exames = sExamesOcupacionais;



        //        if (sExamesASO.Trim() != "")
        //            sExamesASO = sExamesASO + sExamesASO_Aptidao;
        //        else
        //            sExamesASO = sExamesASO_Aptidao;


        //        //bool zSelecao = true;





        //        //Prajna quer que apareça clinico se ele realmente estiver no planejamento
        //        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") < 0)
        //        {
        //            lst_Exames = lst_Exames + "Exame Clínico" + "|";
        //        }


        //        //pegar apenas complementar da convocação - kit
        //        if (xIdExame != "4")
        //        {


        //            int xPosit = 0;
        //            DataRow[] rows = dsExames.Tables[0].Select("Id=" + xIdExame);

        //            //valueListClinicaClienteExameDicionario.ValueListItems.Add(Convert.ToInt32(row[0]), Convert.ToString(rows[0]["Nome"]));
        //            if (rows.Count() > 0)
        //            {

        //                // retirar admissional, periodico, retorno ao trab., complement. desta lista
        //                if (rows[0]["Nome"].ToString().Trim().ToUpper() != "RETORNO AO TRABALHO" &&
        //                    rows[0]["Nome"].ToString().Trim().ToUpper() != "DEMISSIONAL" &&
        //                    rows[0]["Nome"].ToString().Trim().ToUpper() != "ADMISSIONAL" &&
        //                    rows[0]["Nome"].ToString().Trim().ToUpper() != "MUDANÇA DE FUNÇÃO" &&
        //                    rows[0]["Nome"].ToString().Trim().ToUpper() != "PERIÓDICO")
        //                {

        //                    //  lst_Exames.Items.Add(Convert.ToString(rows[0]["Nome"]));

        //                    if (sExamesOcupacionais.ToUpper().IndexOf(Convert.ToString(rows[0]["Nome"]).ToUpper()) > 0)
        //                    {
        //                        //poderia procurar o exame, e ver se foi feito e está com resultado.  Se sim, não selecionar
        //                        //precisaria usar a data de planejamento deste exame

        //                        xPosit = sExamesASO.ToUpper().IndexOf(Convert.ToString(rows[0]["Nome"]).ToUpper());

        //                        if (xPosit > 0)
        //                        {
        //                            if (sExamesASO.Substring(xPosit + 38, 12).Replace("/", " ").Trim() == "")
        //                                xPosit = 0;

        //                        }

        //                        if (xPosit == 0)
        //                        {
        //                            lst_Exames = lst_Exames + Convert.ToString(rows[0]["Nome"]) +"|";                                      
        //                        }
        //                    }

        //                }


        //            }

        //        }
        //        else
        //        {
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                int xPosit = 0;
        //                DataRow[] rows = dsExames.Tables[0].Select("Id=" + Convert.ToInt32(row["IdExameDicionario"]));

        //                //valueListClinicaClienteExameDicionario.ValueListItems.Add(Convert.ToInt32(row[0]), Convert.ToString(rows[0]["Nome"]));
        //                if (rows.Count() > 0)
        //                {

        //                    // retirar admissional, periodico, retorno ao trab., complement. desta lista
        //                    if (rows[0]["Nome"].ToString().Trim().ToUpper() != "RETORNO AO TRABALHO" &&
        //                        rows[0]["Nome"].ToString().Trim().ToUpper() != "DEMISSIONAL" &&
        //                        rows[0]["Nome"].ToString().Trim().ToUpper() != "ADMISSIONAL" &&
        //                        rows[0]["Nome"].ToString().Trim().ToUpper() != "MUDANÇA DE FUNÇÃO" &&
        //                        rows[0]["Nome"].ToString().Trim().ToUpper() != "PERIÓDICO")
        //                    {

        //                        //  lst_Exames.Items.Add(Convert.ToString(rows[0]["Nome"]));

        //                        if (sExamesOcupacionais.ToUpper().IndexOf(Convert.ToString(rows[0]["Nome"]).ToUpper()) > 0)
        //                        {
        //                            //poderia procurar o exame, e ver se foi feito e está com resultado.  Se sim, não selecionar
        //                            //precisaria usar a data de planejamento deste exame

        //                            xPosit = sExamesASO.ToUpper().IndexOf(Convert.ToString(rows[0]["Nome"]).ToUpper());

        //                            if (xPosit > 0)
        //                            {
        //                                if (sExamesASO.Substring(xPosit + 38, 12).Replace("/", " ").Trim() == "")
        //                                    xPosit = 0;

        //                            }

        //                            if (xPosit == 0)
        //                            {
        //                                lst_Exames = lst_Exames + Convert.ToString(rows[0]["Nome"]) + "|";

        //                            }
        //                        }

        //                    }
        //                    else if (rows[0]["Nome"].ToString().Trim().ToUpper() == "PERIÓDICO")
        //                    {
        //                        zClinico = true;
        //                    }

        //                }

        //            }
        //        }


        //    }



        //    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") >= 0)
        //    {
        //        if (zClinico == true)
        //        {

        //            lst_Exames = lst_Exames + "Exame Clínico" + "|";
        //        }
        //    }



        //    return lst_Exames;



        //}




        //[WebMethod]
        //public string Resultado_Exame(string Id,  string CodUsuario, string CNPJ, string TipoExame, string Colaborador, string Data, string Resultado,
        //                                   string Observacao, string Arquivo, string Conteudo_Arquivo)
        //{

        //    Empregado rEmpregado = new Empregado();
        //    Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

        //    //int zId = 0;


        //    int xTipoExame = 4;
        //    string rSelect = "";

        //    string txt_Status = "";

        //    try
        //    {



        //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


        //        rSelect = "  dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

        //        //pegar Id Empresa                    
        //        rEmpresa.Find(rSelect);

        //        //se não achar empresa,  emitir retorno avisando
        //        if (rEmpresa.Id == 0)
        //        {
        //            txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
        //        }




        //        if (txt_Status == "")
        //        {
        //            rEmpregado.Find(" tNo_Empg='" + Colaborador + "'  and nId_Empr = " + rEmpresa.Id.ToString());


        //            if (txt_Status == "")
        //            {
        //                //se não achar empregado,  emitir retorno avisando
        //                if (rEmpregado.Id == 0)
        //                {
        //                    txt_Status = txt_Status + "Erro: Empregado não localizado ( " + Colaborador + "  )" + System.Environment.NewLine;
        //                }
        //            }

        //        }



        //        if (TipoExame.ToUpper().Trim() == "PERIODICO" || TipoExame.ToUpper().Trim() == "PERIÓDICO")
        //        {
        //            xTipoExame = 4;
        //        }
        //        else if (TipoExame.ToUpper().Trim() == "DEMISSIONAL")
        //        {
        //            xTipoExame = 2;
        //        }
        //        else if (TipoExame.ToUpper().Trim().Substring(0, 5) == "MUDAN")
        //        {
        //            xTipoExame = 3;
        //        }
        //        else if (TipoExame.ToUpper().Trim().Substring(0, 7) == "RETORNO")
        //        {
        //            xTipoExame = 5;
        //        }
        //        else if (TipoExame.ToUpper().Trim().Substring(0, 6) == "ADMISS")
        //        {
        //            xTipoExame = 1;
        //        }


        //        ExameBase exame = new ExameBase();

        //        if (txt_Status == "")
        //        {

        //            exame.Find("IdEmpregado = " + rEmpregado.Id.ToString() + " and IdExameDicionario = " + xTipoExame.ToString() + " and convert( char(10), DataExame, 103 ) = '" + Data + "' ");

        //            if (exame.Id == 0)
        //            {
        //                txt_Status = "Erro: Exame não localizado.";
        //            }

        //        }



        //        if (txt_Status == "")
        //        {
        //            //atualizar resultado e observacao
        //            exame.IndResultado = System.Convert.ToInt32( Resultado );
        //            exame.ObservacaoResultado = Observacao;
        //            exame.Save();

        //        }


        //        if (txt_Status == "")  //criar prontuário
        //        {


        //            if (txt_Status == "" && Conteudo_Arquivo != "")
        //            {
        //                //xArquivo terá a extensão

        //                byte[] arrBytes = Convert.FromBase64String(Conteudo_Arquivo);

        //                //montar com diretório padrão da empresa, nome do colaborador, data e extensão - chave Arquivo
        //                Cliente xCliente = new Cliente();
        //                xCliente.Find(System.Convert.ToInt32(rEmpresa.Id));

        //                string xArq = "";

        //                xArq = rEmpregado.tNO_EMPG.ToUpper().Trim() + "_" + exame.DataExame.ToString("ddMMyyyy") + "_" + "ASO" + Arquivo;


        //                string uri = "ftp://54.94.157.244:21/" + xCliente.DiretorioPadrao.ToString().Trim().Replace(" ", "%20") + "/Prontuario/" + xArq;

        //                //string uri = "ftp://54.94.157.244:21/5A%20GT/Prontuario/teste_xml.pdf";



        //                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(uri));
        //                request.Method = WebRequestMethods.Ftp.UploadFile;

        //                request.Credentials = new NetworkCredential("lmm", "Lm160521");

        //                request.UseBinary = true;
        //                request.UsePassive = true;
        //                request.ContentLength = arrBytes.Length;
        //                Stream stream = request.GetRequestStream();
        //                stream.Write(arrBytes, 0, arrBytes.Length);
        //                stream.Close();
        //                FtpWebResponse res = (FtpWebResponse)request.GetResponse();


        //                ProntuarioDigital xProntuario = new ProntuarioDigital();

        //                xProntuario.Find(" IdExameBase = " + exame.Id.ToString());

        //                if (xProntuario.Id != 0)
        //                {
        //                    xProntuario.Arquivo = @"I:\fotosdocsdigitais\" + xCliente.DiretorioPadrao.ToString().Trim() + "/Prontuario/" + xArq;
        //                    xProntuario.Save();
        //                }
        //                else
        //                {
        //                    Ilitera.Common.Prestador rPrestador = new Ilitera.Common.Prestador();
        //                    rPrestador.Find(" prestador.idjuridicapessoa in (select idjuridicapessoa from juridicapessoa where idpessoa in   (select idpessoa from usuario where idusuario = " + CodUsuario + " ))");

        //                    if (rPrestador.Id == 0)
        //                    {
        //                        txt_Status = txt_Status + "Erro: erro na salva do prontuário ( Cód.Usuário inválido )" + System.Environment.NewLine;
        //                    }

        //                    if (txt_Status == "")
        //                    {
        //                        ProntuarioDigital xProntuario2 = new ProntuarioDigital();
        //                        xProntuario2.IdExameBase = exame;
        //                        xProntuario2.IdEmpregado = rEmpregado;
        //                        xProntuario2.IndTipoDocumento = 1;
        //                        xProntuario2.DataDigitalizacao = System.DateTime.Now;
        //                        xProntuario2.DataProntuario = exame.DataExame;
        //                        xProntuario2.Arquivo = @"I:\fotosdocsdigitais\" + xCliente.DiretorioPadrao.ToString().Trim() + "/Prontuario/" + xArq;
        //                        xProntuario2.Descricao = "";
        //                        xProntuario2.IdDigitalizador = rPrestador;
        //                        xProntuario2.Save();
        //                    }

        //                }


        //            }


        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;


        //    }
        //    finally
        //    {

        //        if (txt_Status != "")
        //        {

        //        }
        //        else
        //        {

        //            txt_Status = "Processamento Finalizado";

        //        }
        //    }

        //    return txt_Status;


        //}




        //[WebMethod]
        //public string Editar_Afastamento(string ID, string CNPJ, string CodUsuario, string Colaborador, string DataInicio_Original, string DataInicio, string HoraInicio, string PrevisaoRetorno,
        //                                  string DataRetorno, string HoraRetorno, string Observacao, string TipoAfastamento, string Emitente_Atestado, string Responsavel_Atestado,
        //                                  string NrConselho_Atestado, string UFConselho_Atestado, string CID1, string CID2, string CID3, string CID4, string Arquivo, string Conteudo_Arquivo)
        //{

        //    string txt_Status = "";
        //    Empregado rEmpregado = new Empregado();
        //    Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
        //    //int zId = 0;


        //    Afastamento afastamento = new Afastamento();

        //    string rSelect = "";


        //    try
        //    {



        //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");



        //        rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

        //        //pegar Id Empresa                    
        //        rEmpresa.Find(rSelect);

        //        //se não achar empresa,  emitir retorno avisando
        //        if (rEmpresa.Id == 0)
        //        {
        //            txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
        //        }


        //        if (txt_Status == "")
        //        {
        //            rEmpregado.Find(" tNo_Empg='" + Colaborador + "'  and nId_Empr = " + rEmpresa.Id.ToString());


        //            if (txt_Status == "")
        //            {
        //                //se não achar empregado,  emitir retorno avisando
        //                if (rEmpregado.Id == 0)
        //                {
        //                    txt_Status = txt_Status + "Erro: Empregado não localizado ( " + Colaborador + "  )" + System.Environment.NewLine;
        //                }
        //            }

        //        }


        //        //verificar se já existe registro de afastamento - pode ser apenas atualização da Data e Hora Retorno
        //        //do absenteísmo - nesse caso apenas atualizar o registro




        //        //  UPDATE Data e Hora Retorno
        //        if (txt_Status == "")
        //        {

        //            afastamento.Find(" IdEmpregado = " + rEmpregado.Id.ToString() + " and convert( char(10), DataInicial, 103 ) = '" + DataInicio + "' ");

        //            if (afastamento.Id != 0)
        //            {
        //                //se tiver dataretorno,  dar update e finalizar processamento
        //                if (afastamento.DataVolta == new DateTime() && DataRetorno != "")
        //                {
        //                    afastamento.DataVolta = System.Convert.ToDateTime(DataRetorno + " " + HoraRetorno, ptBr);
        //                    //rAfastamento.Save();

        //                 //   txt_Status = "Data de Retorno deste Afastamento salva para o colaborador " + Colaborador + " .";
        //                }


        //            }
        //            else
        //            {
        //                txt_Status = "Erro: Afastamento não localizado.  Edição não foi realizada.";
        //            }

        //        }


        //        if (txt_Status == "")
        //        {


        //            afastamento.IdEmpregado = rEmpregado;
        //            afastamento.DataInicial = System.Convert.ToDateTime(DataInicio + ' ' + HoraInicio, ptBr);

        //            if (PrevisaoRetorno != "")
        //                afastamento.DataPrevista = System.Convert.ToDateTime(PrevisaoRetorno, ptBr);
        //            else
        //                afastamento.DataPrevista = new DateTime();


        //            if (DataRetorno != "")
        //                afastamento.DataVolta = System.Convert.ToDateTime(DataRetorno + ' ' + HoraRetorno, ptBr);
        //            else
        //                afastamento.DataVolta = new DateTime();


        //            Acidente zAcidente = new Acidente();
        //            afastamento.IdAcidente = zAcidente;


        //            //validação
        //            bool xEnvio_Alerta = false;

        //            if (afastamento.DataVolta == null || afastamento.DataVolta.Year == 1)
        //            {
        //                if (afastamento.DataInicial.AddDays(15) < afastamento.DataPrevista)
        //                {
        //                    xEnvio_Alerta = true;
        //                }
        //            }
        //            else
        //            {
        //                if (afastamento.DataInicial.AddDays(15) < afastamento.DataVolta)
        //                {
        //                    xEnvio_Alerta = true;
        //                }
        //            }


        //            if (xEnvio_Alerta == false)
        //            //checar se há atetados com mais de 15 dias afastados nos ultimos 60 dias
        //            {

        //                Ilitera.Data.Clientes_Funcionarios xAbs = new Ilitera.Data.Clientes_Funcionarios();
        //                DataSet rDs = xAbs.Checar_Absenteismo_Colaborador(rEmpregado.Id, afastamento.DataInicial.ToString("dd/MM/yyyy", ptBr));

        //                if (rDs.Tables[0].Rows.Count > 0)
        //                {
        //                    if (rDs.Tables[0].Rows[0][0].ToString().Trim() != "")
        //                    {
        //                        if (System.Convert.ToInt16(rDs.Tables[0].Rows[0][0].ToString()) > 15)
        //                        {
        //                            xEnvio_Alerta = true;
        //                        }
        //                    }
        //                }
        //            }


        //            if (xEnvio_Alerta == true)
        //            {
        //                txt_Status = "O colaborador " + Colaborador + " possui afastamentos anteriores com pelo menos 15 dias corridos ou com total de dias afastados de pelo menos 15 dias nos últimos 60 dias. Favor verificar se os atestados possuem a mesma patologia para possível encaminhamento ao INSS.";
        //            }




        //            if (txt_Status == "" && CID1 != "")
        //            {
        //                CID rCID = new CID();
        //                rCID.Find(" CodigoCID = '" + CID1 + "'  ");

        //                if (rCID.Id == 0)
        //                {
        //                    txt_Status = "Código CID fornecido Inválido: " + CID1;
        //                }
        //                else
        //                {
        //                    afastamento.IdCID = rCID;
        //                }
        //            }

        //            if (txt_Status == "" && CID2 != "")
        //            {
        //                CID rCID = new CID();
        //                rCID.Find(" CodigoCID = '" + CID2 + "'  ");

        //                if (rCID.Id == 0)
        //                {
        //                    txt_Status = "Código CID fornecido Inválido: " + CID2;
        //                }
        //                else
        //                {
        //                    afastamento.IdCID2 = rCID.Id;
        //                }
        //            }

        //            if (txt_Status == "" && CID3 != "")
        //            {
        //                CID rCID = new CID();
        //                rCID.Find(" CodigoCID = '" + CID3 + "'  ");

        //                if (rCID.Id == 0)
        //                {
        //                    txt_Status = "Código CID fornecido Inválido: " + CID3;
        //                }
        //                else
        //                {
        //                    afastamento.IdCID3 = rCID.Id;
        //                }
        //            }

        //            if (txt_Status == "" && CID4 != "")
        //            {
        //                CID rCID = new CID();
        //                rCID.Find(" CodigoCID = '" + CID4 + "'  ");

        //                if (rCID.Id == 0)
        //                {
        //                    txt_Status = "Código CID fornecido Inválido: " + CID4;
        //                }
        //                else
        //                {
        //                    afastamento.IdCID4 = rCID.Id;
        //                }
        //            }



        //            if (txt_Status == "")
        //            {

        //                afastamento.Obs = Observacao.Trim();

        //                //xEmitente_Atestado = current.Element("Emitente_Atestado").Value;
        //                //xResponsavel_Atestado = current.Element("Responsavel_Atestado").Value;
        //                //xNrConselho_Atestado = current.Element("NrConselho_Atestado").Value;
        //                //xUFConselho_Atestado = current.Element("UFConselho_Atestado").Value;


        //                if (TipoAfastamento.ToUpper() == "OCUPACIONAL")
        //                {
        //                    afastamento.IndTipoAfastamento = 1;
        //                }
        //                else
        //                {
        //                    afastamento.IndTipoAfastamento = 2;
        //                }


        //                AfastamentoTipo xAT = new AfastamentoTipo();
        //                xAT.Find(101);
        //                afastamento.IdAfastamentoTipo = xAT;

        //                afastamento.UsuarioId = System.Convert.ToInt32(CodUsuario);

        //                afastamento.Save();

        //            }


        //            if (txt_Status == "" && Conteudo_Arquivo != "")
        //            {
        //                //xArquivo terá a extensão

        //                byte[] arrBytes = Convert.FromBase64String(Conteudo_Arquivo);

        //                //montar com diretório padrão da empresa, nome do colaborador, data e extensão - chave Arquivo
        //                Cliente xCliente = new Cliente();
        //                xCliente.Find(System.Convert.ToInt32(rEmpresa.Id));

        //                string xArq = "";

        //                //if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
        //                xArq = "Atestado_" + Colaborador.Replace(" ", "_") + "_" + DataInicio.Replace("/", "") + "." + Arquivo;


        //                string uri = "ftp://54.94.157.244:21/" + xCliente.DiretorioPadrao.ToString().Trim().Replace(" ", "%20") + "/Prontuario/" + xArq;

        //                //string uri = "ftp://54.94.157.244:21/5A%20GT/Prontuario/teste_xml.pdf";



        //                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(uri));
        //                request.Method = WebRequestMethods.Ftp.UploadFile;

        //                request.Credentials = new NetworkCredential("lmm", "Lm160521");

        //                request.UseBinary = true;
        //                request.UsePassive = true;
        //                request.ContentLength = arrBytes.Length;
        //                Stream stream = request.GetRequestStream();
        //                stream.Write(arrBytes, 0, arrBytes.Length);
        //                stream.Close();
        //                FtpWebResponse res = (FtpWebResponse)request.GetResponse();

        //                afastamento.Atestado = @"I:\FotosDocsDigitais\" + xCliente.DiretorioPadrao.ToString().Trim() + "/Prontuario/" + xArq;

        //                //salvar dados do atestado nos campos do afastamento
        //                afastamento.Atestado_Emitente = Responsavel_Atestado;

        //                if (Emitente_Atestado.Trim().ToUpper() == "CRM")
        //                    afastamento.Atestado_ideOC = "1";
        //                else
        //                    afastamento.Atestado_ideOC = "2";

        //                afastamento.Atestado_nrOC = NrConselho_Atestado;
        //                afastamento.Atestado_ufOC = UFConselho_Atestado;

        //                afastamento.Save();
        //            }


        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

        //        //Session["zErro"] = txt_Status;
        //        return txt_Status;


        //    }
        //    finally
        //    {

        //        if (txt_Status != "")
        //        {
        //            //Session["zErro"] = txt_Status;                    
        //        }
        //        else
        //        {

        //            // Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
        //            // Response.Redirect("~\\PCMSO\\CadAbsentismo_Auto.aspx?IdEmpregado=" + zId.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + xCodUsuario + "&Data=" + xData + "&CodUsuario=" + xCodUsuario + "&xId=" + xID);


        //            // 'CadAbsentismo.aspx?IdEmpregado=" + Session["Empregado"].ToString() + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + xUser.IdUsuario.ToString() + "&IdAcidente=0'

        //            //Session["zErro"] = txt_Status + " " + "Processamento Finalizado";
        //            txt_Status = "Processamento OK";

        //        }
        //    }

        //    return txt_Status;


        //}




        //[WebMethod]
        //public string Criar_Cargo(string ID, string CodUsuario, string CNPJ, string Cargo, string DescricaoCargo, string CBO)
        //{


        //    Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

        //    //int zId = 0;

        //    string rSelect = "";

        //    string txt_Status = "";

        //    try
        //    {


        //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


        //        rSelect = "  dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

        //        //pegar Id Empresa                    
        //        rEmpresa.Find(rSelect);

        //        //se não achar empresa,  emitir retorno avisando
        //        if (rEmpresa.Id == 0)
        //        {
        //            txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
        //        }






        //        if (txt_Status == "")
        //        {
        //            Funcao rFuncao = new Funcao();


        //            rFuncao.Find("  IdCliente =" + rEmpresa.Id.ToString() + " and NomeFuncao = '" + Cargo.Trim() + "' ");

        //            if (rFuncao.Id == 0)
        //            {
        //                rFuncao.NomeFuncao = Cargo.Trim();

        //                Cliente xCliente = new Cliente();
        //                xCliente.Find(rEmpresa.Id);
        //                rFuncao.IdCliente = xCliente;                        
        //            }

        //            rFuncao.NumeroCBO = CBO.Trim();
        //            rFuncao.DescricaoFuncao = DescricaoCargo.Trim();

        //            rFuncao.Save();


        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;




        //    }
        //    finally
        //    {

        //        if (txt_Status != "")
        //        {


        //        }
        //        else
        //        {

        //            txt_Status = "Processamento OK";

        //        }
        //    }

        //    return txt_Status;

        //}


        //[WebMethod]
        //public string Cadastrar_Unidade(string xID,  string xNomeEmpresa,  string xRazaoSocial, string xCNPJ, string xCodUsuario, string xTipoLogradouro, string xLogradouro, string xNumero,
        //                                    string xComplemento, string xBairro, string xCEP, string xMunicipio, string xUF, string xIE, string xCCM, string xSite, string xEmail, string xObservacao,
        //                                    string xDDDContato, string xTelefoneContato, string xNomeContato, string xDeptoContato, string xEmailContato, string xCNAE )
        //{


        //    Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();


        //    string txt_Status = "";

        //    string rSelect = "";
        //    string xRetorno = "";


        //    try
        //    {


        //        xRetorno = "";



        //        //validar campos
        //        if (xID.Length < 20 || xID.Length > 22)
        //        {
        //            txt_Status = " Campo ID inválido | ";
        //            xRetorno = " Campo ID Inválido |";
        //        }
        //        if (xNomeEmpresa.Length < 1 || xNomeEmpresa.Length > 80)
        //        {
        //            txt_Status = txt_Status + " Campo NomeEmpresa inválido |";
        //            xRetorno = xRetorno + " Campo NomeEmpresa Inválido |";
        //        }
        //        if (xRazaoSocial.Length < 1 || xRazaoSocial.Length > 80)
        //        {
        //            txt_Status = txt_Status + " Campo RazaoSocial inválido |";
        //            xRetorno = xRetorno + " Campo RazaoSocial Inválido |";
        //        }
        //        if (SomenteNumeros(xCNPJ) == false)
        //        {
        //            txt_Status = txt_Status + " Campo CNPJ deve ser somente números |";
        //            xRetorno = xRetorno + " Campo CNPJ deve ser somente números |";
        //        }
        //        if (xCNPJ.Length < 10 || xCNPJ.Length > 14)
        //        {
        //            txt_Status = txt_Status + " Campo CNPJ inválido |";
        //            xRetorno = xRetorno + " Campo CNPJ Inválido |";
        //        }
        //        if (xTipoLogradouro.Length > 15)
        //        {
        //            txt_Status = txt_Status + " Campo Tipo Logradouro inválido |";
        //            xRetorno = xRetorno + " Campo Tipo Logradouro Inválido |";
        //        }
        //        if (xLogradouro.Length < 1 || xLogradouro.Length > 150)
        //        {
        //            txt_Status = txt_Status + " Campo Logradouro inválido |";
        //            xRetorno = xRetorno + " Campo Logradouro Inválido |";
        //        }
        //        if (xNumero.Length < 1 || xNumero.Length > 30)
        //        {
        //            txt_Status = txt_Status + " Campo Número inválido |";
        //            xRetorno = xRetorno + " Campo Número Inválido |";
        //        }
        //        if (xComplemento.Length > 200)
        //        {
        //            txt_Status = txt_Status + " Campo Complemento inválido |";
        //            xRetorno = xRetorno + " Campo Complemento Inválido |";
        //        }
        //        if (xBairro.Length < 1 || xBairro.Length > 80)
        //        {
        //            txt_Status = txt_Status + " Campo Bairro inválido |";
        //            xRetorno = xRetorno + " Campo Bairro Inválido |";
        //        }
        //        if (xCEP.Length < 1 || xCEP.Length > 9)
        //        {
        //            txt_Status = txt_Status + " Campo CEP inválido |";
        //            xRetorno = xRetorno + " Campo CEP Inválido |";
        //        }
        //        if (xMunicipio.Length < 1 || xMunicipio.Length > 80)
        //        {
        //            txt_Status = txt_Status + " Campo Municipio inválido |";
        //            xRetorno = xRetorno + " Campo Municipio Inválido |";
        //        }
        //        if (xUF.Length < 1 || xUF.Length > 2)
        //        {
        //            txt_Status = txt_Status + " Campo UF inválido |";
        //            xRetorno = xRetorno + " Campo UF Inválido |";
        //        }
        //        if (xIE.Length > 50)
        //        {
        //            txt_Status = txt_Status + " Campo IE inválido |";
        //            xRetorno = xRetorno + " Campo IE Inválido |";
        //        }
        //        if (xCCM.Length > 50)
        //        {
        //            txt_Status = txt_Status + " Campo CCM inválido |";
        //            xRetorno = xRetorno + " Campo CCM Inválido |";
        //        }
        //        if (xSite.Length > 100)
        //        {
        //            txt_Status = txt_Status + " Campo Site inválido |";
        //            xRetorno = xRetorno + " Campo Site Inválido |";
        //        }
        //        if (xEmail.Length > 100)
        //        {
        //            txt_Status = txt_Status + " Campo eMail Principal inválido |";
        //            xRetorno = xRetorno + " Campo eMail Principal Inválido |";
        //        }
        //        if (xObservacao.Length > 500)
        //        {
        //            txt_Status = txt_Status + " Campo Observação inválido |";
        //            xRetorno = xRetorno + " Campo Observação Inválido |";
        //        }
        //        if (xDDDContato.Length > 10)
        //        {
        //            txt_Status = txt_Status + " Campo DDD Contato inválido |";
        //            xRetorno = xRetorno + " Campo DDD Contato Inválido |";
        //        }
        //        if (xTelefoneContato.Length > 20)
        //        {
        //            txt_Status = txt_Status + " Campo Telefone Contato inválido |";
        //            xRetorno = xRetorno + " Campo Telefone Contato Inválido |";
        //        }
        //        if (xDeptoContato.Length > 50)
        //        {
        //            txt_Status = txt_Status + " Campo Depto Contato inválido |";
        //            xRetorno = xRetorno + " Campo Depto Contato Inválido |";
        //        }
        //        if (xEmailContato.Length > 100)
        //        {
        //            txt_Status = txt_Status + " Campo eMail Contato inválido |";
        //            xRetorno = xRetorno + " Campo eMail Contato Inválido |";
        //        }
        //        if (xNomeContato.Length > 100)
        //        {
        //            txt_Status = txt_Status + " Campo Nome Contato inválido |";
        //            xRetorno = xRetorno + " Campo Nome Contato Inválido |";
        //        }
        //        if (xCNAE.Length > 10)
        //        {
        //            txt_Status = txt_Status + " Campo CNAE inválido |";
        //            xRetorno = xRetorno + " Campo CNAE Inválido |";
        //        }


        //        if (xRetorno != "")
        //            xRetorno = "50 ( Validação de campos ): " + xRetorno;






        //        if (txt_Status == "")
        //        {

        //            rSelect = " NomeAbreviado = '" + xNomeEmpresa + "' and dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

        //            //pegar Id Empresa                    
        //            rEmpresa.Find(rSelect);

        //            //se não achar empresa,  emitir retorno avisando
        //            if (rEmpresa.Id != 0)
        //            {
        //                txt_Status = txt_Status + "Erro: Empresa já existe ( " + xNomeEmpresa + "  )" + System.Environment.NewLine;
        //                xRetorno = "02 (Empresa já existe(" + xNomeEmpresa + ")";
        //            }

        //        }




        //        if (txt_Status == "")
        //        {

        //            rEmpresa = new Ilitera.Common.Pessoa();

        //            rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

        //            //pegar Id Empresa                    
        //            rEmpresa.Find(rSelect);

        //            //se não achar empresa,  emitir retorno avisando
        //            if (rEmpresa.Id != 0)
        //            {
        //                txt_Status = txt_Status + "Erro: CNPJ já existe ( " + xCNPJ + "  )" + System.Environment.NewLine;
        //                xRetorno = "03 (CNPJ já existe(" + xCNPJ + ")";
        //            }

        //        }



        //        GrupoEmpresa xGrupoEmpresa = new GrupoEmpresa();

        //        if (txt_Status == "")
        //        {
        //            xGrupoEmpresa.Descricao = xNomeEmpresa;
        //            xGrupoEmpresa.Save();

        //            if (xGrupoEmpresa.Id == 0)
        //            {
        //                txt_Status = txt_Status + "Erro: salva do grupo ( " + xNomeEmpresa + "  )" + System.Environment.NewLine;
        //                xRetorno = "04 (Erro na salva do grupo(" + xNomeEmpresa + ")";
        //            }
        //        }




        //        if (txt_Status == "")
        //        {

        //            CNAE zCNAE = new CNAE();
        //            //zCNAE.Find(" Codigo = '0111-3/01' and IndCNAE = '1' ");
        //            if (xCNAE != "")
        //                zCNAE.Find(" Codigo = '" + xCNAE + "' and IndCNAE = '1' ");
        //            else
        //                zCNAE.Find(" Codigo = '0111-3/01' and IndCNAE = '1' ");

        //            if (zCNAE.Id == 0)
        //                zCNAE.Find(" Codigo = '0111-3/01' and IndCNAE = '1' ");



        //            //crio o registro em opsa->Pessoa e pego o ID

        //            rEmpresa = new Ilitera.Common.Pessoa();

        //            rEmpresa.NomeAbreviado = xNomeEmpresa;
        //            rEmpresa.NomeCompleto = xRazaoSocial;
        //            rEmpresa.NomeCodigo = xCNPJ;
        //            rEmpresa.IndTipoPessoa = 1;
        //            rEmpresa.IsInativo = false;
        //            rEmpresa.Site = xSite;
        //            rEmpresa.Email = xEmail;
        //            rEmpresa.Origem = "Decathlon";




        //            rEmpresa.Save();


        //            if (rEmpresa.Id == 0)
        //            {
        //                txt_Status = txt_Status + "Erro: salva da empresa(1) ( " + xNomeEmpresa + "  )" + System.Environment.NewLine;
        //                xRetorno = "05 (Erro na salva da empresa (" + xNomeEmpresa + ")";
        //            }


        //            //como a classe Cliente está dando erros ao tentar pegar o objeto de IdJuridicaPapel, IdCNAE e IdGrupoEmpresa, criei um método de inserção

        //            Ilitera.Data.Comunicacao xCriarEmpresa = new Ilitera.Data.Comunicacao();
        //            xCriarEmpresa.Criar_Empresa(rEmpresa.Id, xIE, xCCM, xObservacao, zCNAE.Id, xGrupoEmpresa.Id, 1, xNomeEmpresa);



        //            rSelect = " NomeAbreviado = '" + xNomeEmpresa + "' and dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

        //            //pegar Id Empresa                    
        //            rEmpresa.Find(rSelect);

        //            //se não achar empresa,  emitir retorno avisando
        //            if (rEmpresa.Id == 0)
        //            {
        //                txt_Status = txt_Status + "Erro: salva de empresa(2) ( " + xNomeEmpresa + "  )" + System.Environment.NewLine;
        //                xRetorno = "06 (Erro na salva da empresa(2) (" + xNomeEmpresa + ")";
        //            }



        //        }






        //        TipoLogradouro xTipoEndereco = new TipoLogradouro();

        //        if (txt_Status == "")
        //        {

        //            if (xTipoLogradouro != "")
        //            {

        //                xTipoEndereco.Find(" NomeAbreviado = '" + xTipoLogradouro + "'  ");

        //                if (xTipoEndereco.Id == 0)
        //                {
        //                    txt_Status = txt_Status + "Erro: tipo de logradouro não localizado ( " + xTipoLogradouro + "  )" + System.Environment.NewLine;
        //                    xRetorno = "07 (Tipo de Logradouro não localizado (" + xTipoLogradouro + ")";
        //                    rEmpresa.Delete();
        //                }

        //            }
        //            else
        //            {
        //                xTipoEndereco.Find(0);
        //            }

        //        }




        //        Municipio xCidade = new Municipio();

        //        if (txt_Status == "")
        //        {

        //            xCidade.Find(" NomeAbreviado = '" + xMunicipio + "' or NomeCompleto = '" + xMunicipio + "' ");

        //            if (xCidade.Id == 0)
        //            {

        //                UnidadeFederativa xEstado = new UnidadeFederativa();
        //                xEstado.Find("NomeAbreviado = '" + xUF + "' ");

        //                if (xEstado.Id == 0)
        //                {
        //                    xEstado = new UnidadeFederativa();
        //                    xEstado.Find(" NomeAbreviado = 'SP' ");
        //                }


        //                xCidade = new Municipio();
        //                xCidade.NomeAbreviado = xMunicipio;
        //                xCidade.NomeCompleto = xMunicipio;
        //                xCidade.IndTipoLocalizacaoGeografica = (int)TipoLocalizacaoGeografica.Municipio;
        //                xCidade.IdUnidadeFederativa = xEstado;
        //                xCidade.Save();

        //                if (xCidade.Id == 0)
        //                {
        //                    txt_Status = txt_Status + "Erro: Municipio ( " + xMunicipio + "  )" + System.Environment.NewLine;
        //                    xRetorno = "08 (Erro na localização de município (" + xMunicipio + ")";
        //                    rEmpresa.Delete();
        //                }


        //            }

        //        }


        //        if (txt_Status == "")
        //        {

        //            Endereco xEndereco = new Endereco();


        //            xEndereco.IdTipoLogradouro = xTipoEndereco;


        //            xEndereco.Logradouro = xLogradouro;
        //            xEndereco.Numero = xNumero;
        //            xEndereco.IdPessoa = rEmpresa;
        //            xEndereco.Cep = xCEP;
        //            xEndereco.Bairro = xBairro;
        //            xEndereco.Municipio = xMunicipio;
        //            xEndereco.IdMunicipio = xCidade;
        //            xEndereco.Uf = xUF;
        //            xEndereco.IndTipoEndereco = (int)TipoEndereco.Default;
        //            xEndereco.Complemento = xComplemento;

        //            xEndereco.IdMunicipio.Find();

        //            xEndereco.Save();


        //            if (xEndereco.Id == 0)
        //            {
        //                txt_Status = txt_Status + "Erro: salva de logradouro ( " + xLogradouro + "  )" + System.Environment.NewLine;
        //                xRetorno = "09 (Erro na salva de logradouro (" + xLogradouro + ")";
        //                rEmpresa.Delete();
        //            }

        //        }


        //        if (txt_Status == "")
        //        {
        //            ContatoTelefonico xTelefone = new ContatoTelefonico();

        //            xTelefone.DDD = xDDDContato;
        //            xTelefone.Departamento = xDeptoContato;
        //            xTelefone.IdPessoa = rEmpresa;
        //            xTelefone.IndTipoTelefone = 0;
        //            xTelefone.Numero = xTelefoneContato;
        //            xTelefone.Nome = xNomeContato;
        //            xTelefone.eMail = xEmailContato;

        //            xTelefone.Save();

        //            if (xTelefone.Id == 0)
        //            {
        //                txt_Status = txt_Status + "Erro: salva de telefone ( " + xTelefoneContato + "  )" + System.Environment.NewLine;
        //                xRetorno = "10 (Erro na salva de telefone (" + xTelefone + ")";
        //                rEmpresa.Delete();
        //            }


        //        }


        //        xRetorno = "01 Processamento concluído sem erros";



        //    }
        //    catch (Exception ex)
        //    {
        //        txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;




        //    }
        //    finally
        //    {

        //        if (txt_Status != "")
        //        {


        //        }
        //        else
        //        {

        //            txt_Status = "Processamento OK";

        //        }
        //    }

        //    return txt_Status;



        //}




        //private static bool SomenteNumeros(string str)
        //{
        //    str = str.Trim();
        //    return (System.Text.RegularExpressions.Regex.IsMatch(str, @"^\+?\d+$"));
        //}







        //[WebMethod]
        //public string Editar_Acidente(string ID, string CodUsuario, string CNPJ, string Colaborador, string DataAcidente_Original, string DataAcidente, string HoraAcidente, string TipoAcidente,
        //                        string SituacaoGeradora, string ParteCorpoAtingida, string Lateralidade, string AgenteCausador, string DescricaoLesao, string DescricaoCompl,
        //                        string LocalAcidente, string EnderecoLocal, string NumeroLocal, string MunicipioLocal, string UFLocal, string UnidadeLocal, string EspecificacaoLocal,
        //                        string SetorAcidente, string Internacao, string DataAtendimento, string HoraAtendimento, string DuracaoTratamentoDias, string CodigoCNES,
        //                        string Reponsavel_Atestado, string NrConselho_Atestado, string UFConselho_Atestado, string DiagnosticoProvavel, string CID1, string CID2,
        //                        string CID3, string CID4, string TransferidoOutroSetor, string AposentadoInvalidez, string PerdaMaterial, string EmitenteCAT, string DataEmissaoCAT,
        //                        string HoraEmissaoCAT, string TipoCAT, string NumeroCAT, string Iniciativa, string BOPolicial, string DataObito, string Arquivo, string Conteudo_Arquivo)
        //{


        //    ClinicaCliente rClinica = new ClinicaCliente();
        //    Empregado rEmpregado = new Empregado();
        //    Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
        //    //int zId = 0;



        //    string rSelect = "";
        //    string txt_Status = "";

        //    try
        //    {


        //        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");


        //        rSelect = "  dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

        //        //pegar Id Empresa                    
        //        rEmpresa.Find(rSelect);

        //        //se não achar empresa,  emitir retorno avisando
        //        if (rEmpresa.Id == 0)
        //        {
        //            txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
        //        }



        //        rEmpregado.Find(" tNo_Empg='" + Colaborador + "'  and nId_Empr = " + rEmpresa.Id.ToString());


        //        if (txt_Status == "")
        //        {
        //            //se não achar empregado,  emitir retorno avisando
        //            if (rEmpregado.Id == 0)
        //            {
        //                txt_Status = txt_Status + "Erro: Empregado não localizado ( " + Colaborador + "  )" + System.Environment.NewLine;
        //            }
        //        }


        //        Acidente acidente = new Acidente();
        //        acidente.Find(" IdEmpregado = " + rEmpregado.Id.ToString() + " and convert( char(10), DataAcidente, 103 ) = '" + DataAcidente + "' ");

        //        if (acidente.Id == 0)
        //        {
        //            txt_Status = "Acidente nã localizado para o colaborador " + Colaborador + " .";
        //        }


        //        if (txt_Status == "")
        //        {


        //            acidente.DataAcidente = System.Convert.ToDateTime(DataAcidente + " " + HoraAcidente, ptBr);

        //            TipoAcidente = TipoAcidente.Trim().ToUpper();
        //            if (TipoAcidente == "TIPICO" || TipoAcidente == "TÍPICO") acidente.IndTipoAcidente = 1;
        //            else if (TipoAcidente == "DOENCA" || TipoAcidente == "DOENÇA") acidente.IndTipoAcidente = 2;
        //            else if (TipoAcidente == "TRAJETO") acidente.IndTipoAcidente = 3;
        //            else acidente.IndTipoAcidente = 1;

        //            if (SituacaoGeradora != "")
        //                acidente.Codigo_Situacao_Geradora = System.Convert.ToInt32(SituacaoGeradora);

        //            if (ParteCorpoAtingida != "")
        //                acidente.Codigo_Parte_Corpo_Atingida = System.Convert.ToInt32(ParteCorpoAtingida);

        //            Lateralidade = Lateralidade.Trim().ToUpper();
        //            if (Lateralidade == "ESQUERDA")
        //                acidente.IdLateralidade = 1;
        //            else if (Lateralidade == "DIREITA")
        //                acidente.IdLateralidade = 2;
        //            else if (Lateralidade == "AMBAS")
        //                acidente.IdLateralidade = 3;
        //            else
        //                acidente.IdLateralidade = 0;

        //            if (AgenteCausador != "")
        //                acidente.AgenteCausador = AgenteCausador;

        //            if (DescricaoLesao != "")
        //                acidente.Codigo_Descricao_Lesao = System.Convert.ToInt32(DescricaoLesao);

        //            acidente.Descricao = DescricaoCompl;

        //            LocalAcidente zLocalAcidente = new LocalAcidente();
        //            zLocalAcidente.Find(System.Convert.ToInt32(LocalAcidente));

        //            acidente.IdLocalAcidente = zLocalAcidente;


        //            acidente.Logradouro = EnderecoLocal;
        //            acidente.Nr_Logradouro = NumeroLocal;
        //            acidente.Municipio = MunicipioLocal;
        //            acidente.UF = UFLocal;

        //            acidente.EspecLocal = EspecificacaoLocal;

        //            acidente.indTipoSetor = 1;

        //            //xUnidadeLocal
        //            //xSetorAcidente




        //            if (Internacao == "S")
        //            {
        //                acidente.DataInternacao = System.Convert.ToDateTime(DataAtendimento + " " + HoraAtendimento, ptBr);
        //                acidente.HasInternacao = true;
        //                acidente.CNES = CodigoCNES;
        //            }
        //            else
        //            {
        //                acidente.HasInternacao = false;
        //                acidente.CNES = "";
        //            }


        //            if (DuracaoTratamentoDias != "")
        //                acidente.DuracaoInternacao = System.Convert.ToInt16(DuracaoTratamentoDias);


        //            if (Reponsavel_Atestado != "")
        //            {
        //                acidente.MedicoInternacao = Reponsavel_Atestado;
        //                acidente.CRMInternacao = NrConselho_Atestado;
        //                acidente.UFInternacao = UFConselho_Atestado;
        //                acidente.DiagnosticoProvavel = DiagnosticoProvavel;
        //            }





        //            if (txt_Status == "" && CID1 != "")
        //            {
        //                CID rCID = new CID();
        //                rCID.Find(" CodigoCID = '" + CID1 + "'  ");

        //                if (rCID.Id == 0)
        //                {
        //                    txt_Status = "Código CID fornecido Inválido: " + CID1;
        //                }
        //                else
        //                {
        //                    acidente.IdCID = rCID;
        //                }
        //            }

        //            if (txt_Status == "" && CID2 != "")
        //            {
        //                CID rCID = new CID();
        //                rCID.Find(" CodigoCID = '" + CID2 + "'  ");

        //                if (rCID.Id == 0)
        //                {
        //                    txt_Status = "Código CID fornecido Inválido: " + CID2;
        //                }
        //                else
        //                {
        //                    acidente.IdCID2 = rCID.Id;
        //                }
        //            }

        //            if (txt_Status == "" && CID3 != "")
        //            {
        //                CID rCID = new CID();
        //                rCID.Find(" CodigoCID = '" + CID3 + "'  ");

        //                if (rCID.Id == 0)
        //                {
        //                    txt_Status = "Código CID fornecido Inválido: " + CID3;
        //                }
        //                else
        //                {
        //                    acidente.IdCID3 = rCID.Id;
        //                }
        //            }

        //            if (txt_Status == "" && CID4 != "")
        //            {
        //                CID rCID = new CID();
        //                rCID.Find(" CodigoCID = '" + CID4 + "'  ");

        //                if (rCID.Id == 0)
        //                {
        //                    txt_Status = "Código CID fornecido Inválido: " + CID4;
        //                }
        //                else
        //                {
        //                    acidente.IdCID4 = rCID.Id;
        //                }
        //            }


        //            if (TransferidoOutroSetor == "S") acidente.isTransfSetor = true;
        //            else acidente.isTransfSetor = false;

        //            if (AposentadoInvalidez == "S") acidente.isAposInval = true;
        //            else acidente.isAposInval = false;

        //            if (PerdaMaterial != "")
        //            {
        //                try
        //                {
        //                    acidente.PerdaMaterial = System.Convert.ToSingle(PerdaMaterial);
        //                }
        //                catch  //Exception Ex)
        //                {
        //                    acidente.PerdaMaterial = 0;
        //                }
        //            }
        //            else
        //            {
        //                acidente.PerdaMaterial = 0;
        //            }



        //            if (NumeroCAT != "")
        //            {
        //                CAT cat = new CAT();

        //                cat.Find("IdEmpregado = " + rEmpregado.Id.ToString() + " and NumeroCat = " + NumeroCAT);

        //                if (cat.Id == 0)
        //                {
        //                    cat = new CAT();
        //                    cat.Inicialize();
        //                }

        //                cat.IdEmpregado = rEmpregado;


        //                cat.NumeroCAT = NumeroCAT;

        //                cat.DataEmissao = System.Convert.ToDateTime(DataEmissaoCAT + " " + HoraEmissaoCAT, ptBr);
        //                cat.IndEmitente = Convert.ToInt32(EmitenteCAT);
        //                cat.IndTipoCAT = Convert.ToInt32(TipoCAT);

        //                if (BOPolicial != "")
        //                {
        //                    cat.hasRegPolicial = true;
        //                    cat.BO = BOPolicial;
        //                }
        //                else
        //                {
        //                    cat.hasRegPolicial = false;
        //                }

        //                if (DataObito != "")
        //                {
        //                    cat.hasMorte = true;
        //                    cat.DataObito = System.Convert.ToDateTime(DataObito, ptBr);
        //                }
        //                else
        //                {
        //                    cat.hasMorte = false;
        //                }

        //                cat.UsuarioId = System.Convert.ToInt32(CodUsuario);


        //                cat.Save();



        //                if (txt_Status == "" && Conteudo_Arquivo != "")
        //                {
        //                    //xArquivo terá a extensão

        //                    byte[] arrBytes = Convert.FromBase64String(Conteudo_Arquivo);

        //                    //montar com diretório padrão da empresa, nome do colaborador, data e extensão - chave Arquivo
        //                    Cliente xCliente = new Cliente();
        //                    xCliente.Find(System.Convert.ToInt32(rEmpresa.Id));

        //                    string xArq = "";

        //                    //if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
        //                    xArq = "CAT_" + Colaborador.Replace(" ", "_") + "_" + NumeroCAT + "." + Arquivo;


        //                    string uri = "ftp://54.94.157.244:21/" + xCliente.DiretorioPadrao.ToString().Trim().Replace(" ", "%20") + "/Prontuario/" + xArq;

        //                    //string uri = "ftp://54.94.157.244:21/5A%20GT/Prontuario/teste_xml.pdf";



        //                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(uri));
        //                    request.Method = WebRequestMethods.Ftp.UploadFile;

        //                    request.Credentials = new NetworkCredential("lmm", "Lm160521");

        //                    request.UseBinary = true;
        //                    request.UsePassive = true;
        //                    request.ContentLength = arrBytes.Length;
        //                    Stream stream = request.GetRequestStream();
        //                    stream.Write(arrBytes, 0, arrBytes.Length);
        //                    stream.Close();
        //                    FtpWebResponse res = (FtpWebResponse)request.GetResponse();

        //                    cat.Arquivo_Cat = @"I:\FotosDocsDigitais\" + xCliente.DiretorioPadrao.ToString().Trim() + "/Prontuario/" + xArq;


        //                    cat.Save();
        //                }




        //                acidente.IdCAT = cat;



        //            }


        //            Ilitera.Common.Juridica xJuridica = new Ilitera.Common.Juridica();
        //            xJuridica.Find(rEmpresa.Id);

        //            acidente.IdJuridica = xJuridica;


        //            System.Collections.ArrayList emprFuncao = new EmpregadoFuncao().Find("nID_EMPREGADO=" + rEmpregado.Id
        //            + " AND hDT_INICIO<='" + acidente.DataAcidente.ToString("yyyy-MM-dd")
        //            + "' AND (hDT_TERMINO IS NULL OR hDT_TERMINO>='" + acidente.DataAcidente.ToString("yyyy-MM-dd") + "')");

        //            acidente.IdSetor = ((EmpregadoFuncao)emprFuncao[0]).nID_SETOR;


        //            acidente.Save();


        //            if ( acidente.hasAfastamento==true)                    
        //            {

        //                //localizar afastamento

        //                //salvar absenteismo
        //                Afastamento absentismo = new Afastamento();

        //                absentismo.Find("IdAcidente = " + acidente.Id.ToString());

        //                if (absentismo.Id == 0)
        //                {
        //                    absentismo = new Afastamento(); 
        //                    absentismo.Inicialize();
        //                }


        //                absentismo.IdEmpregado = rEmpregado;




        //                absentismo.IndTipoAfastamento = (int)TipoAfastamento.Ocupacional;
        //                absentismo.IdAcidente = acidente;


        //                if (txt_Status == "" && CID1 != "")
        //                {
        //                    CID rCID = new CID();
        //                    rCID.Find(" CodigoCID = '" + CID1 + "'  ");

        //                    if (rCID.Id == 0)
        //                    {
        //                        txt_Status = "Código CID fornecido Inválido: " + CID1;
        //                    }
        //                    else
        //                    {
        //                        absentismo.IdCID = rCID;
        //                    }
        //                }

        //                if (txt_Status == "" && CID2 != "")
        //                {
        //                    CID rCID = new CID();
        //                    rCID.Find(" CodigoCID = '" + CID2 + "'  ");

        //                    if (rCID.Id == 0)
        //                    {
        //                        txt_Status = "Código CID fornecido Inválido: " + CID2;
        //                    }
        //                    else
        //                    {
        //                        absentismo.IdCID2 = rCID.Id;
        //                    }
        //                }

        //                if (txt_Status == "" && CID3 != "")
        //                {
        //                    CID rCID = new CID();
        //                    rCID.Find(" CodigoCID = '" + CID3 + "'  ");

        //                    if (rCID.Id == 0)
        //                    {
        //                        txt_Status = "Código CID fornecido Inválido: " + CID3;
        //                    }
        //                    else
        //                    {
        //                        absentismo.IdCID3 = rCID.Id;
        //                    }
        //                }

        //                if (txt_Status == "" && CID4 != "")
        //                {
        //                    CID rCID = new CID();
        //                    rCID.Find(" CodigoCID = '" + CID4 + "'  ");

        //                    if (rCID.Id == 0)
        //                    {
        //                        txt_Status = "Código CID fornecido Inválido: " + CID4;
        //                    }
        //                    else
        //                    {
        //                        absentismo.IdCID4 = rCID.Id;
        //                    }
        //                }


        //                absentismo.INSS = false;

        //                absentismo.UsuarioId = Convert.ToInt32(CodUsuario);

        //                absentismo.Save();




        //            }
        //            else
        //                acidente.hasAfastamento = false;




        //            acidente.Save();

        //        }







        //    }
        //    catch (Exception ex)
        //    {
        //        txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

        //        //  Session["zErro"] = txt_Status;
        //        //  Response.Redirect("~/Comunicacao2.aspx");


        //    }
        //    finally
        //    {

        //        if (txt_Status != "")
        //        {

        //        }
        //        else
        //        {
        //            //rClinica.IdClinica.Find();

        //            //Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
        //            //Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia_Auto.aspx?IdEmpregado=" + zId.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + xCodUsuario + "&TipoExame=" + xTipoExame + "&Data=" + xData + "&IdClinica=" + rClinica.IdClinica.Id.ToString().Trim() + "&CodUsuario=" + xCodUsuario + "&xId=" + xID);


        //            // Session["zErro"] = txt_Status + " " + "Processamento Finalizado";
        //            txt_Status = "Processamento Finalizado OK";

        //        }
        //    }

        //    return txt_Status;


        //}




        //[WebMethod]
        //public XmlDocument solicitar_CAT(string ID, string CNPJ, string CodUsuario, string Colaborador, string Data_Inicial, string Data_Final)
        //{

        //    Clinica rClinica = new Clinica();
        //    Empregado rEmpregado = new Empregado();
        //    Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
        //    int zId = 0;

        //    XmlDocument zRetAll = new XmlDocument();
        //    XmlDocument zRet = new XmlDocument();

        //    string rSelect = "";

        //    string txt_Status = "";

        //    string xId = ID;




        //    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");



        //    try
        //    {


        //        rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

        //        //pegar Id Empresa                    
        //        rEmpresa.Find(rSelect);

        //        //se não achar empresa,  emitir retorno avisando
        //        if (rEmpresa.Id == 0)
        //        {
        //            txt_Status = txt_Status + "Erro: Empresa não localizada ( " + CNPJ + "  )" + System.Environment.NewLine;
        //        }

        //        //quando for convocação ( mailing ) no último irá o link para chamar comunicação
        //        //pode ir com um ID pronto





        //        if (txt_Status == "")
        //        {
        //            rEmpregado.Find(" tNo_Empg='" + Colaborador + "'  and nId_Empr = " + rEmpresa.Id.ToString());


        //            if (txt_Status == "")
        //            {
        //                //se não achar empregado,  emitir retorno avisando
        //                if (rEmpregado.Id == 0)
        //                {
        //                    txt_Status = txt_Status + "Erro: Empregado não localizado ( " + Colaborador + "  )" + System.Environment.NewLine;
        //                }
        //            }

        //        }

        //        //ver se tem CAT e arquivo associados dentro do período
        //        int xArqs = 0;                

        //        ArrayList nCat = new CAT().Find(" IDEMPREGADO = " + rEmpregado.Id.ToString() + " and DataEmissao between convert( smalldatetime,'" + Data_Inicial + "', 103 ) and convert( smalldatetime,'" + Data_Final + "', 103 )");


        //        if ( nCat.Count == 0 )
        //        {
        //            txt_Status = "Não há arquivo de CAT a ser baixado.";
        //        }


        //        if (txt_Status == "")
        //        {

        //            foreach (CAT rCat in nCat)
        //            {

        //                if ( rCat.Arquivo_Cat != "")
        //                {

        //                    string xPath = rCat.Arquivo_Cat.Trim();


        //                    string xExtensao = xPath.Substring(xPath.Length - 3, 3);


        //                    string xLink = xPath;
        //                    string strXml = "";

        //                    try
        //                    {

        //                        byte[] arrBytes = File.ReadAllBytes(xLink);
        //                        strXml = Convert.ToBase64String(arrBytes);

        //                    }
        //                    catch (Exception Ex)
        //                    {

        //                        txt_Status = "Erro ao carregar Arquivo de CAT: " + Ex.Message;

        //                    }


        //                    if (txt_Status == "")
        //                    {

        //                        if (xArqs == 0)
        //                        {
        //                            zRetAll = GetXmlDocument(
        //                            new XDocument(
        //                                new XElement("Arquivo",
        //                                    new XElement("Conteudo_Arquivo", strXml),
        //                                    new XElement("Extensao", xExtensao)
        //                                  )));
        //                        }
        //                        else
        //                        {
        //                            zRet = GetXmlDocument(
        //                            new XDocument(
        //                                new XElement("Arquivo",
        //                                    new XElement("Conteudo_Arquivo", strXml),
        //                                    new XElement("Extensao", xExtensao)
        //                                  )));

        //                            foreach ( XmlNode childNode in zRet.DocumentElement.ChildNodes)
        //                                zRetAll.DocumentElement.AppendChild(childNode);

        //                        }

        //                        xArqs++;
        //                    }

        //                }



        //            }





        //        }







        //    }
        //    catch (Exception ex)
        //    {
        //        txt_Status = txt_Status + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;


        //    }
        //    finally
        //    {

        //        if (txt_Status != "")
        //        {
        //            //montar XMLDocument com erro
        //            string xRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + txt_Status + "</Erro>";

        //            zRet.LoadXml(xRet);


        //        }

        //    }


        //    return zRetAll;

        //}



        //private XmlDocument GetXmlDocument(XDocument document)
        //{
        //    using (XmlReader xmlReader = document.CreateReader())
        //    {
        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.Load(xmlReader);
        //        if (document.Declaration != null)
        //        {
        //            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration(document.Declaration.Version,
        //                document.Declaration.Encoding, document.Declaration.Standalone);
        //            xmlDoc.InsertBefore(dec, xmlDoc.FirstChild);
        //        }
        //        return xmlDoc;
        //    }
        //}



    }
}
