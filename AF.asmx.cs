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
    [WebService(Namespace = "https://www.ilitera.net.br/daiti")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService()]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AF : System.Web.Services.WebService
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
                                        string CTPS, string Serie, string UF_CTPS, string Admissao, string Apelido, string Demissao, string Email, string Inicio_Funcao,
                                        string Funcao, string Setor, string LocalTrabalho, string CentroCusto, string Cargo, string Jornada, string PJ)
        {


            string zStatus = "";
            string zAcao = "";

            //(1) primeira checagem básica de parâmetros
            //(2) checagem durante processamento

            if (Usuario.Trim().Length < 3 || Usuario.Trim().Length > 12)
            {
                zStatus = "Usuário fornecido inválido (1)";
            }

            if (Senha.Trim().Length < 3 || Senha.Trim().Length > 8)
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
                if (Sexo.Trim() != "M" && Sexo.Trim() != "F")
                {
                    zStatus = zStatus + " / " + "Sexo fornecido inválido (1)";
                }
            }

            if (RG.Trim() != "")  // não obrigatório
            {
                if (RG.Trim().Length < 4 || RG.Trim().Length > 16)
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
                if (PIS.Trim().Length < 2 || PIS.Trim().Length > 14)
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

            if (CPF.Trim().Length < 8 || CPF.Trim().Length > 11)
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
                if (Validar_Data(Nascimento.Trim()) == false)
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



            if (CentroCusto.Trim().Length < 2 || CentroCusto.Trim().Length > 20)
            {
                zStatus = zStatus + " / " + "Centro de Custo fornecido inválido (1)";
            }

            if (PJ.Trim().Length != 1)
            {
                zStatus = zStatus + " / " + "Campo de indicação de PJ fornecido inválida (1)";
            }
            else
            {                
                if (PJ.Trim()!="1" && PJ.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Campo de indicação de PJ fornecido inválida (2)";
                }
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
                string xCentroCusto = "";

                if (CentroCusto.ToUpper().IndexOf("OBRA")<0)
                {
                    //zStatus = zStatus + " / " + "Centro de Custo não localizado (2)";
                    xCentroCusto = CentroCusto;
                }
                else
                {
                    xCentroCusto = CentroCusto.Substring(CentroCusto.ToUpper().IndexOf("OBRA") + 5, 3).Trim();
                }



                if (zStatus == "")
                {
                    string rSelect = " NomeAbreviado like '%" + xCentroCusto + "%' and IsInativo = 0 and nomecodigo <> '' and idpessoa in ( select idpessoa from juridica where idgrupoempresa = -359564237 ) ";  //dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

                    rEmpresa.Find(rSelect);

                    if (rEmpresa.Id == 0)
                    {
                        zStatus = zStatus + " / " + "Centro de Custo fornecido não localizado (2)";
                    }
                }

            }


            //DUVIDA
            // por exemplo, atualização cadastral com uma nova classif.funcional - campo DataInicio preenchida
            // se CentroCusto indica CNPJ de onde será criada nova classif.funcional, será que estou pegando o colaborador correto,
            // será que não precisa CNPJ_Origem ou algo do tipo ?



            //começar tratamento do registro
            //verificar se é uma simples inserção, atualização cadastral, nova classif.funcional ou demissão e chamar funções para cada uma delas.

            Empregado rColaborador2 = new Empregado();

            if (zStatus == "")
            {
                string rAdm = Admissao.Substring(6, 2) + "/" + Admissao.Substring(4, 2) + "/" + Admissao.Substring(0, 4);
                string rPJ = "0";

                if (PJ.Trim() == "1") rPJ = "1";


                //primeira busca - todas condições
                ArrayList alColab = new Empregado().Find(" tNo_CPF = '" + CPF.Trim() + "' and hDt_Adm = convert( smalldatetime, '" + rAdm + "', 103 ) and hDt_Dem is null and gTerceiro = " + rPJ + " and nId_Empr in ( " + rEmpresa.Id.ToString() + " ) ");

                if (alColab.Count > 1)
                {
                    zStatus = zStatus + " / " + "Foi localizado mais de um registro desse Colaborador indicado no sistema. (2)";
                }
                else
                {
                    foreach (Empregado zColaborador in alColab)
                    {
                        rColaborador2.Find(zColaborador.Id);
                    }
                }


                //checar se CPF em centro de custo já está cadastrado e ativo, se não encontrar, procurar em toda AF                
                // rColaborador2.Find(" tNo_CPF = '" + CPF.Trim() + "' and hDt_Adm = convert( smalldatetime, '" + rAdm + "', 103 ) and gTerceiro = " + rPJ + " and nId_Empr in ( " + rEmpresa.Id.ToString() + " ) ");

                if ( zStatus=="" && rColaborador2.Id == 0 )
                {
                    alColab = new Empregado().Find(" tNo_CPF = '" + CPF.Trim() + "' and hDt_Adm = convert( smalldatetime, '" + rAdm + "', 103 ) and hDt_Dem is not null and gTerceiro = " + rPJ + " and nId_Empr in ( " + rEmpresa.Id.ToString() + " ) ");

                    foreach (Empregado zColaborador in alColab)
                    {
                        if ( zColaborador.hDT_DEM != new DateTime())
                        {
                            zStatus = zStatus + " / " + "Colaborador indicado está inativo no sistema. (2)";
                        }

                        break;                       

                    }
                }


                if (zStatus == "" && rColaborador2.Id == 0)
                {
                    alColab = new Empregado().Find(" tNo_CPF = '" + CPF.Trim() + "' and hDt_Adm = convert( smalldatetime, '" + rAdm + "', 103 ) and hDt_Dem is null  and nId_Empr in ( " + rEmpresa.Id.ToString() + " ) ");

                    foreach (Empregado zColaborador in alColab)
                    {
                        zStatus = zStatus + " / " + "Discrepância na indicação de PJ do colaborador. (2)";                       

                        break;

                    }
                }




                //tentar fazer mais uma busca pelo grupoempresa
                if (zStatus == ""  && rColaborador2.Id==0)
                {

                    alColab = new Empregado().Find(" tNo_CPF = '" + CPF.Trim() + "' and hDt_Dem is null and hDt_Adm = convert( smalldatetime, '" + rAdm + "', 103 )  and gTerceiro = " + rPJ + " and nId_Empr in ( select idpessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where IdGrupoEmpresa in ( select  IdGrupoEmpresa from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where idpessoa = " + rEmpresa.Id.ToString() + " ) and IdPessoa in ( select IdPessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa (nolock) where IsInativo = 0 ) ) ");

                    if (alColab.Count > 1)
                    {
                        zStatus = zStatus + " / " + "Foi localizado mais de um registro desse Colaborador indicado no sistema. (3)";
                    }
                    else
                    {
                        foreach (Empregado zColaborador in alColab)
                        {
                            rColaborador2.Find(zColaborador.Id);
                        }
                    }


                }

            }




            if (zStatus == "")
            {

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


                        if (zStatus == "" && ( Inicio_Funcao == "" || Funcao == "" || Setor == "" ))
                        {
                            zStatus = " / Inserção não pode ser feita sem os dados de uma nova classif.funcional (2)";
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

                            string zNascimento = Nascimento.Substring(6, 2) + "/" + Nascimento.Substring(4, 2) + "/" + Nascimento.Substring(0, 4);
                            rColaborador.hDT_NASC = System.Convert.ToDateTime(zNascimento, ptBr);

                            rColaborador.teMail = Email;
                            //rColaborador.teMail_Resp = xeMail_Responsavel;

                            string zAdmissao = Admissao.Substring(6, 2) + "/" + Admissao.Substring(4, 2) + "/" + Admissao.Substring(0, 4);
                            rColaborador.hDT_ADM = System.Convert.ToDateTime(zAdmissao, ptBr);
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

                                    xEmprFunc.hDT_INICIO = System.Convert.ToDateTime(zAdmissao, ptBr);

                                    xEmprFunc.nID_EMPR = rCliente;
                                    xEmprFunc.Save();

                                    if (xEmprFunc.Id == 0)
                                    {
                                        zStatus = zStatus + " / " + "Problema na salva da Classif.Funcional (2)";
                                        rColaborador.Delete();
                                    }

                                    xEmprFunc.nID_EMPR.Find();
                                    Int32 xEmprId = xEmprFunc.nID_EMPR.Id;

                                    xEmprFunc.nID_FUNCAO.Find();
                                    string xNomeFuncao = xEmprFunc.nID_FUNCAO.NomeFuncao;

                                    xEmprFunc.nID_EMPREGADO.Find();
                                    Int32 xEmprFuncEmpregado = xEmprFunc.nID_EMPREGADO.Id;

                                    Ilitera.Data.Empregado_Cadastral xFuncao_GHE = new Ilitera.Data.Empregado_Cadastral();
                                    bool xAlocacao = xFuncao_GHE.Criar_Alocacao_Funcao_GHE(xEmprId, xNomeFuncao, xEmprFuncEmpregado);                                   


                                }

                            }
                        

                        }

                        zAcao = "Inserção do colaborador no sistema.";
                    
                    }
                    catch (Exception ex)
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
                            string zDemissao = Demissao.Substring(6, 2) + "/" + Demissao.Substring(4, 2) + "/" + Demissao.Substring(0, 4);
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

                            string zNascimento = Nascimento.Substring(6, 2) + "/" + Nascimento.Substring(4, 2) + "/" + Nascimento.Substring(0, 4);
                            rColaborador2.hDT_NASC = System.Convert.ToDateTime(zNascimento, ptBr);

                            rColaborador2.teMail = Email;
                            //rColaborador.teMail_Resp = xeMail_Responsavel;

                            string zAdmissao = Admissao.Substring(6, 2) + "/" + Admissao.Substring(4, 2) + "/" + Admissao.Substring(0, 4);
                            rColaborador2.hDT_ADM = System.Convert.ToDateTime(zAdmissao, ptBr);
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


                        Cliente rCliente = new Cliente();

                        rCliente.Find(" IdCliente = " + rEmpresa.Id.ToString());

                        if (rCliente.Id == 0)
                        {
                            zStatus = zStatus + " / " + "Erro: Problema na localização de Cliente (2)";
                        }



                        if (zStatus == "")
                        {
                            //criar nova classif.funcional  
                            //ver se campos estão preenchidos 
                            if (Inicio_Funcao.Trim() != "")
                            {

                                string zInicio = Inicio_Funcao.Substring(6, 2) + "/" + Inicio_Funcao.Substring(4, 2) + "/" + Inicio_Funcao.Substring(0,4);


                                //primeiro preciso checar se já existe uma classif.funcional igual ou mais recente que DataInicio
                               // EmpregadoFuncao xEmprFunc_Loc = new EmpregadoFuncao();
                                //xEmprFunc_Loc.Find(" nId_Empregado = " + rColaborador2.Id.ToString() + " and hDt_Inicio >= convert( smalldatetime, '" + zInicio + "', 103 ) ");

                                ArrayList xEmprFunc_Loc = new EmpregadoFuncao().Find(" nId_Empregado = " + rColaborador2.Id.ToString() + " and hDt_Inicio >= convert( smalldatetime, '" + zInicio + "', 103 ) ");

                                if (xEmprFunc_Loc.Count > 1)
                                {
                                    zStatus = zStatus + " / " + "Erro: Existe classif.funcional mais recente que DataInicio, não foi possível criar uma nova.(2)";
                                }

                                                                
                                //if ( xEmprFunc_Loc.Id!=0)
                                //{
                                 //   zStatus = zStatus + " / " + "Erro: Existe classif.funcional mais recente que DataInicio, não foi possível criar uma nova.(2)";
                                //}



                                try
                                {
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
                                        xEmprFunc.nID_EMPREGADO = rColaborador2;
                                        
                                        xEmprFunc.hDT_INICIO = System.Convert.ToDateTime(zInicio, ptBr);

                                        xEmprFunc.nID_EMPR = rCliente;
                                        xEmprFunc.Save();

                                        
                                        if (xEmprFunc.Id == 0)
                                        {
                                            zStatus = zStatus + " / " + "Problema na salva da Classif.Funcional (2)";
                                        }


                                        //if (zStatus == "")
                                        //{
                                        //    //precisaria colocar data de término da classif.funcional anterior com zInicio - 1

                                        //    ArrayList xEmprFunc_Aj = new EmpregadoFuncao().Find(" nId_Empregado = " + rColaborador2.Id.ToString() + " and hDt_Inicio < convert( smalldatetime, '" + zInicio + "', 103 ) order by hDt_Inicio desc");

                                        //    if (xEmprFunc_Aj.Count > 1)
                                        //    {
                                        //        foreach (EmpregadoFuncao rAj in xEmprFunc_Aj)
                                        //        {
                                        //            rAj.hDT_TERMINO = System.Convert.ToDateTime(zInicio, ptBr).AddDays(-1);

                                        //            break;
                                        //        }
                                        //    }
                                        //}

                                    }


                                    zAcao = zAcao + " / Atualização cadastral do colaborador no sistema.";
                                }

                                catch (Exception ex)
                                {
                                    zStatus = zStatus + " / " + "Erro na atualização cadastral do colaborador - classif.funcional (2): " + ex.Message;
                                }

                            }

                        }


                    }


                }

            }


            if (zStatus == "")
            {
                zStatus = "Processamento Finalizado - " + zAcao;
            }

            return zStatus;

            
        }



        


        private XmlDocument GetXmlDocument(XDocument document)
        {
            using (XmlReader xmlReader = document.CreateReader())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);
                if (document.Declaration != null)
                {
                    XmlDeclaration dec = xmlDoc.CreateXmlDeclaration(document.Declaration.Version,
                        document.Declaration.Encoding, document.Declaration.Standalone);
                    xmlDoc.InsertBefore(dec, xmlDoc.FirstChild);
                }
                return xmlDoc;
            }
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

            Validar = zData.Substring(6, 2);
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


            Validar = zData.Substring(0, 4);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                return false;
            }


            //if (zData.Substring(2, 1) != "/" || zData.Substring(5, 1) != "/")
            //{
            //     return false;
            // }


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




    }
}
