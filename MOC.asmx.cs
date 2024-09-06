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

using Ilitera.ASO.Report;


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
    public class MOC : System.Web.Services.WebService
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
        public String Atualizar_Anamnese(string Usuario, string Senha, string Data_Exame, string Tipo_Exame, string CPF, string CNPJ, string Altura, string Peso,
                                         string PA, string BPM, string DUM, string Pele_Anexos, string Osteosmuscular, string Cabeca_Pescoco, string Coracao,
                                         string Pulmoes, string Abdominal, string Membros_Superiores, string Membros_Inferiores, string Esta_Bem_Saude,
                                         string Ficou_Afastado, string Teve_Fratura, string Esteve_Hospitalizado, string Medicacao_Continua, string Fuma,
                                         string Hipertensao, string Diabetes, string Doencas_Coracao, string Derrame_Cerebral, string Bebe, string Bronquite_Asma_Rinite,
                                         string Doenca_Nao_Mencionada, string Doenca_Digestiva, string Doenca_Estomago, string Enxerga_Bem, string Obesidade,
                                         string Cancer, string Colesterol_Alto, string Tratamento_Psiquiatrico, string Dor_Cabeca, string Desmaiou, string Doencas_Urinarias,
                                         string Gripado_com_Frequencia, string Escuta_Bem, string Dores_Costa, string Reumatismo, string Alergia, string Pratica_Esporte,
                                         string Acidentou_Trabalho, string Canal_Auditivo_Obstrucao, string Canal_Auditivo_Acum_Cerumen, string Canal_Auditivo_Alteracao,
                                         string AF_Diabetes, string AF_Doencas_Coracao, string AF_Hipertensao, string AF_Colesterol_Alto, String IdEmpresa)
        {

            XmlDocument zRet = new XmlDocument();

            string zStatus = "";
            string zAcao = "";

            //(1) primeira checagem básica de parâmetros
            //(2) checagem durante processamento

            if (Usuario.Trim().Length < 3 || Usuario.Trim().Length > 12)
            {
                zStatus = "Usuário fornecido inválido (1)";
            }

            if (CNPJ.Trim().Length != 14)
            {
                zStatus = zStatus + " / CNPJ fornecido inválido (1)";
            }

            if (Senha.Trim().Length < 3 || Senha.Trim().Length > 8)
            {
                zStatus = zStatus + " / " + "Senha fornecida inválida (1)";
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



            if (Data_Exame.Trim().Length != 8)
            {
                zStatus = zStatus + " / " + "Data Exame fornecida inválida (1)";
            }
            else
            {
                //checar YYYYMMDD
                if (Validar_Data(Data_Exame.Trim()) == false)
                {
                    zStatus = zStatus + " / " + "Data Exame em formato inválido ( deve ser YYYYMMDD ) (1)";
                }
            }


            if (Tipo_Exame != "1" && Tipo_Exame != "2" && Tipo_Exame != "3" && Tipo_Exame != "4" && Tipo_Exame != "5")
            {
                zStatus = "Tipo Exame fornecido inválido (1)";
            }

            if (Altura.Trim().Length > 6)
            {
                zStatus = zStatus + " / " + "Altura inválida (1)";
            }

            if (Peso.Trim().Length > 6)
            {
                zStatus = zStatus + " / " + "Peso inválido (1)";
            }

            if (PA.Trim().Length > 6)
            {
                zStatus = zStatus + " / " + "PA inválido (1)";
            }

            if (BPM.Trim().Length > 6)
            {
                zStatus = zStatus + " / " + "BPM inválido (1)";
            }


            if (DUM.Trim() != "")
            {
                if (DUM.Trim().Length != 8)
                {
                    zStatus = zStatus + " / " + "DUM fornecida inválida (1)";
                }
                else
                {
                    //checar YYYYMMDD
                    if (Validar_Data(DUM.Trim()) == false)
                    {
                        zStatus = zStatus + " / " + "DUM em formato inválido ( deve ser YYYYMMDD ) (1)";
                    }
                }
            }


            if (Pele_Anexos.Trim() != "")
            {
                if (Pele_Anexos.Trim() != "1" && Pele_Anexos.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Pele_Anexos fornecido inválido (1)";
                }
            }

            if (Osteosmuscular.Trim() != "")
            {
                if (Osteosmuscular.Trim() != "1" && Osteosmuscular.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Osteosmuscular fornecido inválido (1)";
                }
            }


            if (Cabeca_Pescoco.Trim() != "")
            {
                if (Cabeca_Pescoco.Trim() != "1" && Cabeca_Pescoco.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Cabeca_Pescoco fornecido inválido (1)";
                }
            }

            if (Coracao.Trim() != "")
            {
                if (Coracao.Trim() != "1" && Coracao.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Coracao fornecido inválido (1)";
                }
            }

            if (AF_Doencas_Coracao.Trim() != "")
            {
                if (AF_Doencas_Coracao.Trim() != "1" && AF_Doencas_Coracao.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "AF Doenças Coracao fornecido inválido (1)";
                }
            }


            if (Pulmoes.Trim() != "")
            {
                if (Pulmoes.Trim() != "1" && Pulmoes.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Pulmoes fornecido inválido (1)";
                }
            }

            if (Abdominal.Trim() != "")
            {
                if (Abdominal.Trim() != "1" && Abdominal.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Abdominal fornecido inválido (1)";
                }
            }

            if (Membros_Superiores.Trim() != "")
            {
                if (Membros_Superiores.Trim() != "1" && Membros_Superiores.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Membros_Superiores fornecido inválido (1)";
                }
            }

            if (Membros_Inferiores.Trim() != "")
            {
                if (Membros_Inferiores.Trim() != "1" && Membros_Inferiores.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Membros_Inferiores fornecido inválido (1)";
                }
            }

            if (Esta_Bem_Saude.Trim() != "")
            {
                if (Esta_Bem_Saude.Trim() != "1" && Esta_Bem_Saude.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Esta_Bem_Saude fornecido inválido (1)";
                }
            }

            if (Ficou_Afastado.Trim() != "")
            {
                if (Ficou_Afastado.Trim() != "1" && Ficou_Afastado.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Ficou_Afastado fornecido inválido (1)";
                }
            }

            if (Teve_Fratura.Trim() != "")
            {
                if (Teve_Fratura.Trim() != "1" && Teve_Fratura.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Teve_Fratura fornecido inválido (1)";
                }
            }

            if (Esteve_Hospitalizado.Trim() != "")
            {
                if (Esteve_Hospitalizado.Trim() != "1" && Esteve_Hospitalizado.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Esteve_Hospitalizado fornecido inválido (1)";
                }
            }

            if (Medicacao_Continua.Trim() != "")
            {
                if (Medicacao_Continua.Trim() != "1" && Medicacao_Continua.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Medicacao_Continua fornecido inválido (1)";
                }
            }

            if (Fuma.Trim() != "")
            {
                if (Fuma.Trim() != "1" && Fuma.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Fuma fornecido inválido (1)";
                }
            }

            if (Hipertensao.Trim() != "")
            {
                if (Hipertensao.Trim() != "1" && Hipertensao.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Hipertenso fornecido inválido (1)";
                }
            }

            if (AF_Hipertensao.Trim() != "")
            {
                if (AF_Hipertensao.Trim() != "1" && AF_Hipertensao.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "AF Hipertenso fornecido inválido (1)";
                }
            }


            if (Diabetes.Trim() != "")
            {
                if (Diabetes.Trim() != "1" && Diabetes.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Diabetes fornecido inválido (1)";
                }
            }

            if (AF_Diabetes.Trim() != "")
            {
                if (AF_Diabetes.Trim() != "1" && AF_Diabetes.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "AF Diabetes fornecido inválido (1)";
                }
            }


            if (Doencas_Coracao.Trim() != "")
            {
                if (Doencas_Coracao.Trim() != "1" && Doencas_Coracao.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Doencas_Coracao fornecido inválido (1)";
                }
            }

            if (Enxerga_Bem.Trim() != "")
            {
                if (Enxerga_Bem.Trim() != "1" && Enxerga_Bem.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Enxerga_Bem fornecido inválido (1)";
                }
            }

            if (Obesidade.Trim() != "")
            {
                if (Obesidade.Trim() != "1" && Obesidade.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Obesidade fornecido inválido (1)";
                }
            }

            if (Cancer.Trim() != "")
            {
                if (Cancer.Trim() != "1" && Cancer.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Cancer fornecido inválido (1)";
                }
            }

            if (Colesterol_Alto.Trim() != "")
            {
                if (Colesterol_Alto.Trim() != "1" && Colesterol_Alto.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Colesterol_Alto fornecido inválido (1)";
                }
            }

            if (AF_Colesterol_Alto.Trim() != "")
            {
                if (AF_Colesterol_Alto.Trim() != "1" && AF_Colesterol_Alto.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "AF Colesterol_Alto fornecido inválido (1)";
                }
            }


            if (Tratamento_Psiquiatrico.Trim() != "")
            {
                if (Tratamento_Psiquiatrico.Trim() != "1" && Tratamento_Psiquiatrico.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Tratamento_Psiquiatrico fornecido inválido (1)";
                }
            }

            if (Dor_Cabeca.Trim() != "")
            {
                if (Dor_Cabeca.Trim() != "1" && Dor_Cabeca.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Dor_Cabeca fornecido inválido (1)";
                }
            }

            if (Desmaiou.Trim() != "")
            {
                if (Desmaiou.Trim() != "1" && Desmaiou.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Desmaiou fornecido inválido (1)";
                }
            }

            if (Doencas_Urinarias.Trim() != "")
            {
                if (Doencas_Urinarias.Trim() != "1" && Doencas_Urinarias.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Doencas_Urinarias fornecido inválido (1)";
                }
            }

            if (Gripado_com_Frequencia.Trim() != "")
            {
                if (Gripado_com_Frequencia.Trim() != "1" && Gripado_com_Frequencia.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Gripado_com_Frequencia fornecido inválido (1)";
                }
            }

            if (Escuta_Bem.Trim() != "")
            {
                if (Escuta_Bem.Trim() != "1" && Escuta_Bem.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Escuta_Bem fornecido inválido (1)";
                }
            }

            if (Dores_Costa.Trim() != "")
            {
                if (Dores_Costa.Trim() != "1" && Dores_Costa.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Dores_Costas fornecido inválido (1)";
                }
            }

            if (Reumatismo.Trim() != "")
            {
                if (Reumatismo.Trim() != "1" && Reumatismo.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Reumatismo fornecido inválido (1)";
                }
            }

            if (Alergia.Trim() != "")
            {
                if (Alergia.Trim() != "1" && Alergia.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Alergia fornecido inválido (1)";
                }
            }

            if (Pratica_Esporte.Trim() != "")
            {
                if (Pratica_Esporte.Trim() != "1" && Pratica_Esporte.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Pratica_Esporte fornecido inválido (1)";
                }
            }

            if (Acidentou_Trabalho.Trim() != "")
            {
                if (Acidentou_Trabalho.Trim() != "1" && Acidentou_Trabalho.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Acidentou_Trabalho fornecido inválido (1)";
                }
            }

            if (Canal_Auditivo_Obstrucao.Trim() != "")
            {
                if (Canal_Auditivo_Obstrucao.Trim() != "1" && Canal_Auditivo_Obstrucao.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Canal_Auditivo_Obstrucao fornecido inválido (1)";
                }
            }

            if (Canal_Auditivo_Acum_Cerumen.Trim() != "")
            {
                if (Canal_Auditivo_Acum_Cerumen.Trim() != "1" && Canal_Auditivo_Acum_Cerumen.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Canal_Auditivo_Acum_Cerumen fornecido inválido (1)";
                }
            }

            if (Canal_Auditivo_Alteracao.Trim() != "")
            {
                if (Canal_Auditivo_Alteracao.Trim() != "1" && Canal_Auditivo_Alteracao.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Canal_Auditivo_Alteracao fornecido inválido (1)";
                }
            }

            if (Derrame_Cerebral.Trim() != "")
            {
                if (Derrame_Cerebral.Trim() != "1" && Derrame_Cerebral.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Derrame_Cerebral fornecido inválido (1)";
                }
            }

            if (Bebe.Trim() != "")
            {
                if (Bebe.Trim() != "1" && Bebe.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Bebe fornecido inválido (1)";
                }
            }

            if (Bronquite_Asma_Rinite.Trim() != "")
            {
                if (Bronquite_Asma_Rinite.Trim() != "1" && Bronquite_Asma_Rinite.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Bronquite_Asma_Rinite fornecido inválido (1)";
                }
            }

            if (Doenca_Nao_Mencionada.Trim() != "")
            {
                if (Doenca_Nao_Mencionada.Trim() != "1" && Doenca_Nao_Mencionada.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Doenca_Nao_Mencionada fornecido inválido (1)";
                }
            }

            if (Doenca_Digestiva.Trim() != "")
            {
                if (Doenca_Digestiva.Trim() != "1" && Doenca_Digestiva.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Doenca_Digestiva fornecido inválido (1)";
                }
            }

            if (Doenca_Estomago.Trim() != "")
            {
                if (Doenca_Estomago.Trim() != "1" && Doenca_Estomago.Trim() != "2")
                {
                    zStatus = zStatus + " / " + "Doenca_Estomago fornecido inválido (1)";
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

                if (IdEmpresa != "")
                {
                    rEmpresa.Find(System.Convert.ToInt32(IdEmpresa));

                    if (rEmpresa.Id == 0)
                    {
                        zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                    }

                }
                else
                {
                    //qual condição utilizar ??
                    string rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    rEmpresa.Find(rSelect);

                    if (rEmpresa.Id == 0)
                    {
                        //pode ser duplicidade, procurar então CNPJ e CPF juntos
                        rEmpresa.Find(" dbo.udf_getnumeric(nomecodigo) = '" + CNPJ + "' and idpessoa in ( select nid_empr from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado where tno_CPF = '" + CPF + "' and hdt_dem is null ) ");

                        if (rEmpresa.Id == 0)
                        {
                            zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                        }

                    }
                }

            }

            //começar tratamento do registro
            //verificar se é uma simples inserção, atualização cadastral, nova classif.funcional ou demissão e chamar funções para cada uma delas.

            if (zStatus == "")
            {
                //checar se CPF em centro de custo já está cadastrado e ativo 
                Empregado rColaborador2 = new Empregado();
                rColaborador2.Find(" tNo_CPF = '" + CPF.Trim() + "' and hDt_Dem is null  and nId_Empr in ( " + rEmpresa.Id.ToString() + " ) ");  // select idpessoa from " +  Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where IdGrupoEmpresa in ( select  IdGrupoEmpresa from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where idpessoa = " + rEmpresa.Id.ToString() + " ) and IdPessoa in ( select IdPessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa (nolock) where IsInativo = 0 ) ) ");


                if (rColaborador2.Id == 0)   //realizar inserção do colaborador
                {
                    zStatus = zStatus + " / " + " CPF fornecido não localizado (2)";
                }

                string xData = Data_Exame.Substring(6, 2) + "/" + Data_Exame.Substring(4, 2) + "/" + Data_Exame.Substring(0, 4);
                Clinico rClinico = new Clinico();

                if (zStatus == "")
                {
                    rClinico.Find(" IdEmpregado = " + rColaborador2.Id.ToString() + " and IdExameDicionario = " + Tipo_Exame + " and convert( char(10),DataExame,103 ) = '" + xData + "' ");

                    if (rClinico.Id == 0)
                    {
                        zStatus = zStatus + " / " + " Exame Clínico da Anamnese não localizado (2)";
                    }

                }


                Anamnese xAnamnese = new Anamnese();
                ExameFisico xExameFisico = new ExameFisico();

                if (zStatus == "")
                {
                    xAnamnese.Find(" IdExameBase = " + rClinico.Id.ToString());

                    if (xAnamnese.Id == 0)
                    {
                        zStatus = zStatus + " / " + " Anamnese não localizado (2)";
                    }

                }

                if (zStatus == "")
                {
                    xExameFisico.Find(" IdExameBase = " + rClinico.Id.ToString());

                    if (xExameFisico.Id == 0)
                    {
                        zStatus = zStatus + " / " + " Exame Físico não localizado (2)";
                    }

                }



                if (zStatus == "")
                {
                    try
                    {
                        if (Altura.Trim() != "") xExameFisico.Altura = System.Convert.ToSingle(Altura);
                        if (Peso.Trim() != "") xExameFisico.Peso = System.Convert.ToSingle(Peso);
                        if (PA.Trim() != "") xExameFisico.PressaoArterial = PA;
                        if (BPM.Trim() != "") xExameFisico.Pulso = System.Convert.ToInt16(BPM);
                        if (DUM.Trim() != "") xExameFisico.DataUltimaMenstruacao = System.Convert.ToDateTime(DUM.Substring(6, 2) + "/" + DUM.Substring(4, 2) + "/" + DUM.Substring(0, 4), ptBr);
                        if (Pele_Anexos.Trim() != "") xExameFisico.hasPeleAnexosAlterado = System.Convert.ToInt16(Pele_Anexos);
                        if (Osteosmuscular.Trim() != "") xExameFisico.hasOsteoAlterado = System.Convert.ToInt16(Osteosmuscular);
                        if (Cabeca_Pescoco.Trim() != "") xExameFisico.hasCabecaAlterado = System.Convert.ToInt16(Cabeca_Pescoco);
                        if (Coracao.Trim() != "") xExameFisico.hasCoracaoAlterado = System.Convert.ToInt16(Coracao);
                        if (Pulmoes.Trim() != "") xExameFisico.hasPulmaoAlterado = System.Convert.ToInt16(Pulmoes);
                        if (Abdominal.Trim() != "") xExameFisico.hasAbdomemAlterado = System.Convert.ToInt16(Abdominal);

                        if (Membros_Superiores.Trim() != "") xExameFisico.hasMSAlterado = System.Convert.ToInt16(Membros_Superiores);
                        if (Membros_Inferiores.Trim() != "") xExameFisico.hasMIAlterado = System.Convert.ToInt16(Membros_Inferiores);

                        if (Esta_Bem_Saude.Trim() != "") xAnamnese.HasQueixasAtuais = System.Convert.ToInt16(Esta_Bem_Saude);
                        if (Ficou_Afastado.Trim() != "") xAnamnese.HasAfastamento = System.Convert.ToInt16(Ficou_Afastado);
                        if (Teve_Fratura.Trim() != "") xAnamnese.HasTraumatismos = System.Convert.ToInt16(Teve_Fratura);

                        if (Esteve_Hospitalizado.Trim() != "") xAnamnese.HasCirurgia = System.Convert.ToInt16(Esteve_Hospitalizado);
                        if (Medicacao_Continua.Trim() != "") xAnamnese.HasMedicacoes = System.Convert.ToInt16(Medicacao_Continua);

                        if (Fuma.Trim() != "") xAnamnese.HasTabagismo = System.Convert.ToInt16(Fuma);

                        if (AF_Hipertensao.Trim() != "") xAnamnese.Has_AF_Hipertensao = System.Convert.ToInt16(AF_Hipertensao);
                        if (Hipertensao.Trim() != "") xAnamnese.HasHipertensao = System.Convert.ToInt16(Hipertensao);

                        if (Diabetes.Trim() != "") xAnamnese.HasDiabetes = System.Convert.ToInt16(Diabetes);
                        if (AF_Diabetes.Trim() != "") xAnamnese.Has_AF_Diabetes = System.Convert.ToInt16(AF_Diabetes);

                        if (Bebe.Trim() != "") xAnamnese.HasAlcoolismo = System.Convert.ToInt16(Bebe);
                        if (Obesidade.Trim() != "") xAnamnese.Has_AF_Obesidade = System.Convert.ToInt16(Obesidade);
                        if (Cancer.Trim() != "") xAnamnese.Has_AF_Cancer = System.Convert.ToInt16(Cancer);

                        if (Doencas_Coracao.Trim() != "") xAnamnese.HasCoracao = System.Convert.ToInt16(Doencas_Coracao);
                        if (AF_Doencas_Coracao.Trim() != "") xAnamnese.Has_AF_Coracao = System.Convert.ToInt16(AF_Doencas_Coracao);

                        if (Derrame_Cerebral.Trim() != "") xAnamnese.Has_AF_Derrames = System.Convert.ToInt16(Derrame_Cerebral);

                        if (Bronquite_Asma_Rinite.Trim() != "") xAnamnese.HasBronquite = System.Convert.ToInt16(Bronquite_Asma_Rinite);
                        if (Doenca_Nao_Mencionada.Trim() != "") xAnamnese.HasDoencaCronica = System.Convert.ToInt16(Doenca_Nao_Mencionada);

                        if (Doenca_Digestiva.Trim() != "") xAnamnese.HasDigestiva = System.Convert.ToInt16(Doenca_Digestiva);
                        if (Doenca_Estomago.Trim() != "") xAnamnese.HasEstomago = System.Convert.ToInt16(Doenca_Estomago);
                        if (Enxerga_Bem.Trim() != "") xAnamnese.HasEnxerga = System.Convert.ToInt16(Enxerga_Bem);

                        if (Colesterol_Alto.Trim() != "") xAnamnese.HasColesterol = System.Convert.ToInt16(Colesterol_Alto);
                        if (AF_Colesterol_Alto.Trim() != "") xAnamnese.Has_AF_Colesterol = System.Convert.ToInt16(AF_Colesterol_Alto);


                        if (Dor_Cabeca.Trim() != "") xAnamnese.HasDorCabeca = System.Convert.ToInt16(Dor_Cabeca);
                        if (Desmaiou.Trim() != "") xAnamnese.HasDesmaio = System.Convert.ToInt16(Desmaiou);

                        if (Tratamento_Psiquiatrico.Trim() != "") xAnamnese.Has_AF_Psiquiatricos = System.Convert.ToInt16(Tratamento_Psiquiatrico);
                        if (Doencas_Urinarias.Trim() != "") xAnamnese.HasUrinaria = System.Convert.ToInt16(Doencas_Urinarias);
                        if (Gripado_com_Frequencia.Trim() != "") xAnamnese.HasGripado = System.Convert.ToInt16(Gripado_com_Frequencia);

                        if (Escuta_Bem.Trim() != "") xAnamnese.HasEscuta = System.Convert.ToInt16(Escuta_Bem);
                        if (Dores_Costa.Trim() != "") xAnamnese.HasDoresCosta = System.Convert.ToInt16(Dores_Costa);
                        if (Reumatismo.Trim() != "") xAnamnese.HasReumatismo = System.Convert.ToInt16(Reumatismo);
                        if (Alergia.Trim() != "") xAnamnese.HasAlergia = System.Convert.ToInt16(Alergia);

                        if (Pratica_Esporte.Trim() != "") xAnamnese.HasEsporte = System.Convert.ToInt16(Pratica_Esporte);
                        if (Acidentou_Trabalho.Trim() != "") xAnamnese.HasAcidentou = System.Convert.ToInt16(Acidentou_Trabalho);

                        if (Canal_Auditivo_Obstrucao.Trim() != "") xAnamnese.Has_Otologica_Obstrucao = System.Convert.ToInt16(Canal_Auditivo_Obstrucao);
                        if (Canal_Auditivo_Acum_Cerumen.Trim() != "") xAnamnese.Has_Otologica_Cerumen = System.Convert.ToInt16(Canal_Auditivo_Acum_Cerumen);
                        if (Canal_Auditivo_Alteracao.Trim() != "") xAnamnese.Has_Otologica_Alteracao = System.Convert.ToInt16(Canal_Auditivo_Alteracao);



                        xExameFisico.Save();
                        xAnamnese.Save();


                    }
                    catch (Exception ex)
                    {
                        zStatus = zStatus + " / " + "Erro na atualização cadastral do colaborador (2): " + ex.Message;
                    }

                }



            }



            //if (zStatus != "")
            //{
            //    //montar XMLDocument com erro
            //    string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

            //    zRet.LoadXml(vRet);
            //}
            //else
            //{
            //    string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Status>Processamento Finalizado</Status>";

            //    zRet.LoadXml(vRet);
            //}

            //return zRet;

            if (zStatus == "")
            {
                zStatus = "Processamento Finalizado";

            }


            return zStatus;

        }






        [WebMethod]
        public String Atualizar_Dados_Colaborador(string Usuario, string Senha, string Nome, string Sexo, string RG, string Matricula, string CPF, string Nascimento,
                                                  string Admissao, string Email, string CNPJ, string Telefone, String IdEmpresa)
        {

            string zStatus = "";
            string zAcao = "";

            XmlDocument zRet = new XmlDocument();

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

            if (CNPJ.Trim().Length != 14)
            {
                zStatus = zStatus + " / CNPJ fornecido inválido (1)";
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


            if (Email.Trim() != "")  // não obrigatório
            {
                if (Email.Trim().Length < 5 || Email.Trim().Length > 60)
                {
                    zStatus = zStatus + " / " + "E-mail fornecido inválido (1)";
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

                if (IdEmpresa != "")
                {
                    rEmpresa.Find(System.Convert.ToInt32(IdEmpresa));

                    if (rEmpresa.Id == 0)
                    {
                        zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                    }

                }
                else
                {
                    //qual condição utilizar ??
                    string rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    rEmpresa.Find(rSelect);

                    if (rEmpresa.Id == 0)
                    {
                        //pode ser duplicidade, procurar então CNPJ e CPF juntos
                        rEmpresa.Find(" dbo.udf_getnumeric(nomecodigo) = '" + CNPJ + "' and idpessoa in ( select nid_empr from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado where tno_CPF = '" + CPF + "' and hdt_dem is null ) ");

                        if (rEmpresa.Id == 0)
                        {
                            zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                        }

                    }
                }

            }

            //começar tratamento do registro
            //verificar se é uma simples inserção, atualização cadastral, nova classif.funcional ou demissão e chamar funções para cada uma delas.

            if (zStatus == "")
            {
                //checar se CPF em centro de custo já está cadastrado e ativo 
                Empregado rColaborador2 = new Empregado();
                rColaborador2.Find(" tNo_CPF = '" + CPF.Trim() + "' and hDt_Dem is null  and nId_Empr in ( " + rEmpresa.Id.ToString() + " ) ");  // select idpessoa from " +  Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where IdGrupoEmpresa in ( select  IdGrupoEmpresa from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where idpessoa = " + rEmpresa.Id.ToString() + " ) and IdPessoa in ( select IdPessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa (nolock) where IsInativo = 0 ) ) ");



                if (rColaborador2.Id == 0)   //realizar inserção do colaborador
                {
                    zStatus = zStatus + " / " + " CPF fornecido não localizado (2)";
                }

                if (zStatus == "")
                {

                    try
                    {




                        //atualização cadastral 
                        try
                        {
                            rColaborador2.tNO_EMPG = Nome;


                            rColaborador2.tNO_IDENTIDADE = RG;
                            rColaborador2.tSEXO = Sexo;

                            string zNascimento = Nascimento.Substring(6, 2) + "/" + Nascimento.Substring(4, 2) + "/" + Nascimento.Substring(0, 4);
                            rColaborador2.hDT_NASC = System.Convert.ToDateTime(zNascimento, ptBr);

                            rColaborador2.teMail = Email;
                            //rColaborador.teMail_Resp = xeMail_Responsavel;

                            string zAdmissao = Admissao.Substring(6, 2) + "/" + Admissao.Substring(4, 2) + "/" + Admissao.Substring(0, 4);
                            rColaborador2.hDT_ADM = System.Convert.ToDateTime(zAdmissao, ptBr);

                            if (Matricula.Trim() != "")
                            {
                                rColaborador2.tCOD_EMPR = Matricula;
                            }

                            //RegimeRevezamento xRegimeRevezamento = new RegimeRevezamento();
                            //rColaborador2.nID_REGIME_REVEZAMENTO = xRegimeRevezamento;

                            rColaborador2.Save();

                            zAcao = "Atualização cadastral do colaborador no sistema.";
                        }
                        catch (Exception ex)
                        {
                            zStatus = zStatus + " / " + "Erro na atualização cadastral do colaborador (2): " + ex.Message;
                        }


                    }
                    catch (Exception ex)
                    {
                        zStatus = zStatus + " / " + "Erro na atualização cadastral do colaborador (2): " + ex.Message;
                    }


                }
            }



            //if (zStatus != "")
            //{
            //    //montar XMLDocument com erro
            //    string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

            //    zRet.LoadXml(vRet);
            //}
            //else
            //{
            //    string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Status>Processamento Finalizado</Status>";

            //    zRet.LoadXml(vRet);
            //}

            //return zRet;

            if (zStatus == "")
            {
                zStatus = "Processamento Finalizado";
            }

            return zStatus;


        }




        [WebMethod]
        public String Criar_Exame_Complementar(string Usuario, string Senha, string Data_Exame, string Tipo_Exame, string Resultado, string CNPJ_Clinica, string CPF,
                                               string Medico, string CNPJ, String IdEmpresa)
        {



            string zStatus = "";
            string zAcao = "";

            XmlDocument zRet = new XmlDocument();


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

            if (Tipo_Exame.Trim() == "")
            {
                zStatus = zStatus + " / Tipo Exame fornecido inválido (1)";
            }

            if (Resultado != "1" && Resultado != "2" && Resultado != "3")
            {
                zStatus = zStatus + " / Resultado fornecido inválido (1)";
            }

            if (CNPJ.Trim().Length != 14)
            {
                zStatus = zStatus + " / CNPJ fornecido inválido (1)";
            }

            if (CNPJ_Clinica.Trim().Length != 14)
            {
                zStatus = zStatus + " / CNPJ Clínica fornecido inválido (1)";
            }


            if (Medico.Trim().Length < 4 || Medico.Trim().Length > 10)
            {
                zStatus = zStatus + " / " + "CRM do Médico fornecido inválido (1)";
            }
            else
            {
                //verificar se é numérico
                Int64 number;
                bool result = Int64.TryParse(Medico.Trim(), out number);
                if (!result)
                {
                    zStatus = zStatus + " / " + "CRM fornecido do Médico deve ser numérico (1)";
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


            if (Data_Exame.Trim().Length != 8)
            {
                zStatus = zStatus + " / " + "Data Exame fornecida inválida (1)";
            }
            else
            {
                //checar YYYYMMDD
                if (Validar_Data(Data_Exame.Trim()) == false)
                {
                    zStatus = zStatus + " / " + "Data Exame em formato inválido ( deve ser YYYYMMDD ) (1)";
                }
            }




            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            //validar Centro de Custo fornecido
            Pessoa rEmpresa = new Ilitera.Common.Pessoa();


            Int32 xCodUsuario = 0;

            if (zStatus == "")
            {

                //validar Usuário e senha
                Ilitera.Data.eSocial xUser = new Ilitera.Data.eSocial();
                xCodUsuario = xUser.Retornar_Codigo_Usuario(Usuario, Senha);

                if (xCodUsuario == 0)
                {
                    zStatus = zStatus + " / " + "Usuário/Senha inválidos (2)";
                }
            }


            if (zStatus == "")
            {

                if (IdEmpresa != "")
                {
                    rEmpresa.Find(System.Convert.ToInt32(IdEmpresa));

                    if (rEmpresa.Id == 0)
                    {
                        zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                    }

                }
                else
                {
                    //qual condição utilizar ??
                    string rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    rEmpresa.Find(rSelect);

                    if (rEmpresa.Id == 0)
                    {
                        //pode ser duplicidade, procurar então CNPJ e CPF juntos
                        rEmpresa.Find(" dbo.udf_getnumeric(nomecodigo) = '" + CNPJ + "' and idpessoa in ( select nid_empr from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado where tno_CPF = '" + CPF + "' and hdt_dem is null ) ");

                        if (rEmpresa.Id == 0)
                        {
                            zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                        }

                    }
                }

            }

            //começar tratamento do registro
            //verificar se é uma simples inserção, atualização cadastral, nova classif.funcional ou demissão e chamar funções para cada uma delas.
            Empregado rColaborador2 = new Empregado();

            if (zStatus == "")
            {
                //checar se CPF em centro de custo já está cadastrado e ativo             
                rColaborador2.Find(" tNo_CPF = '" + CPF.Trim() + "' and hDt_Dem is null  and nId_Empr in ( " + rEmpresa.Id.ToString() + " ) ");  // select idpessoa from " +  Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where IdGrupoEmpresa in ( select  IdGrupoEmpresa from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where idpessoa = " + rEmpresa.Id.ToString() + " ) and IdPessoa in ( select IdPessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa (nolock) where IsInativo = 0 ) ) ");



                if (rColaborador2.Id == 0)   //realizar inserção do colaborador
                {
                    zStatus = zStatus + " / " + " CPF fornecido não localizado (2)";
                }

            }



            ExameDicionario rTipoExame = new ExameDicionario();

            if (zStatus == "")
            {
                rTipoExame.Find("Nome = '" + Tipo_Exame.Trim() + "' ");

                if (rTipoExame.Id == 0)
                {
                    zStatus = zStatus + " / " + "Tipo de Exame complementar fornecido não localizado (2)";
                }
                else
                {
                    if (rTipoExame.Id >= 1 && rTipoExame.Id <= 5)
                    {
                        zStatus = zStatus + " / " + "Tipo de Exame complementar fornecido inválido (2)";
                    }
                }
            }




            string xData = Data_Exame.Substring(6, 2) + "/" + Data_Exame.Substring(4, 2) + "/" + Data_Exame.Substring(0, 4);

            Complementar exame2 = new Complementar();

            if (zStatus == "")
            {
                //localizar exame
                exame2.Find(" IdEmpregado = " + rColaborador2.Id.ToString() + " and IdExameDicionario = " + rTipoExame.Id.ToString() + " and convert(char(10),DataExame,103 ) = '" + xData + "' ");

                if (exame2.Id != 0)
                {
                    zStatus = zStatus + " / " + " Exame já existe (2)";
                }

            }


            Clinica rClinica = new Clinica();

            if (zStatus == "")
            {
                //localizar exame
                rClinica.Find(" IdJuridicaPapel=8 and IsInativo = 0 and  dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ_Clinica + "' ");

                if (rClinica.Id == 0)
                {
                    zStatus = zStatus + " / " + " Clínica não localizada";
                }

            }


            Medico rMedico = new Medico();

            if (zStatus == "")
            {
                //localizar exame
                //rMedico.Find(" NomeAbreviado = '" + Medico + "' or NomeCompleto = '" + Medico + "' ");

                //ArrayList alMedicos = new Medico().Find(" idpessoa in ( select idpessoa from pessoa where NomeAbreviado = '" + Medico + "' or NomeCompleto = '" + Medico + "' ) ");
                ArrayList alMedicos = new Medico().Find(" dbo.extractinteger(Numero) = '" + Medico + "' ");

                foreach (Medico vMedico in alMedicos)
                {
                    rMedico.Find(vMedico.Id);
                    break;
                }

                if (rMedico.Id == 0)
                {
                    zStatus = zStatus + " / " + " Médico não localizado";
                }

            }


            if (zStatus == "")
            {

                try
                {

                    if (rTipoExame.Id == 6)  //Audiometria
                    {
                        Audiometria xAud = new Audiometria();
                        xAud.IdExameDicionario = rTipoExame;
                        xAud.IdEmpregado = rColaborador2;
                        xAud.DataExame = System.Convert.ToDateTime(xData, ptBr);
                        xAud.IndResultado = System.Convert.ToInt16(Resultado); // (int)Ilitera.Opsa.Data.ResultadoExame.EmEspera;
                        xAud.IdJuridica = rClinica;

                        PagamentoClinica xPag = new PagamentoClinica();
                        xAud.IdPagamentoClinica = xPag;

                        xAud.IndAudiometriaTipo = 0;

                        Medico xMedico = new Medico();
                        xAud.IdMedico = rMedico;

                        ConvocacaoExame xConv = new ConvocacaoExame();
                        xAud.IdConvocacaoExame = xConv;

                        Audiometro xAudiometro = new Audiometro();
                        xAud.IdAudiometro = xAudiometro;

                        Ilitera.Common.Compromisso xcompr = new Ilitera.Common.Compromisso();
                        xAud.IdCompromisso = xcompr;

                        xAud.Save();

                    }
                    else
                    {
                        Complementar xCompl = new Complementar();
                        xCompl.IdExameDicionario = rTipoExame;
                        xCompl.IdEmpregado = rColaborador2;
                        xCompl.IdMedico = rMedico;

                        ConvocacaoExame xConv = new ConvocacaoExame();
                        xCompl.IdConvocacaoExame = xConv;

                        xCompl.DataExame = System.Convert.ToDateTime(xData, ptBr);
                        xCompl.IndResultado = System.Convert.ToInt16(Resultado);  //(int)Ilitera.Opsa.Data.ResultadoExame.EmEspera;
                        xCompl.IdJuridica = rClinica;
                        xCompl.Save();
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToUpper() != "MÉTODO NÃO-ESTÁTICO REQUER UM DESTINO." && ex.Message.ToUpper() != "NON-STATIC METHOD REQUIRES A TARGET.")
                    {
                        zStatus = zStatus + " / " + " Erro na salva do exame: " + ex.Message;
                    }
                }

            }



            //if (zStatus != "")
            //{
            //    //montar XMLDocument com erro
            //    string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

            //    zRet.LoadXml(vRet);
            //}
            //else
            //{
            //    string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Status>Processamento Finalizado</Status>";

            //    zRet.LoadXml(vRet);
            //}

            //return zRet;

            if (zStatus == "")
            {
                zStatus = "Processamento Finalizado";
            }

            return zStatus;


        }





        [WebMethod]
        public String Criar_Exame_Clinico(string Usuario, string Senha, string Data_Exame, string Tipo_Exame, string Resultado, string CNPJ_Clinica, string CPF,
                                  string Medico, string CNPJ, string Hora_Agendamento, String IdEmpresa)
        {



            string zStatus = "";
            string zAcao = "";

            XmlDocument zRet = new XmlDocument();

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

            if (Tipo_Exame != "1" && Tipo_Exame != "2" && Tipo_Exame != "3" && Tipo_Exame != "4" && Tipo_Exame != "5")
            {
                zStatus = zStatus + " / Tipo Exame fornecido inválido (1)";
            }

            if (Resultado != "1" && Resultado != "2" && Resultado != "3")
            {
                zStatus = zStatus + " / Resultado fornecido inválido (1)";
            }

            if (CNPJ.Trim().Length != 14)
            {
                zStatus = zStatus + " / CNPJ fornecido inválido (1)";
            }

            if (CNPJ_Clinica.Trim().Length != 14)
            {
                zStatus = zStatus + " / CNPJ Clínica fornecido inválido (1)";
            }

            if (Medico.Trim().Length < 4 || Medico.Trim().Length > 10)
            {
                zStatus = zStatus + " / " + "CRM do Médico fornecido inválido (1)";
            }
            else
            {
                //verificar se é numérico
                Int64 number;
                bool result = Int64.TryParse(Medico.Trim(), out number);
                if (!result)
                {
                    zStatus = zStatus + " / " + "CRM fornecido do Médico deve ser numérico (1)";
                }
            }

            if (Hora_Agendamento.Trim().Length != 5)
            {
                zStatus = zStatus + " / " + "Horário Agendamento fornecido inválido (1)";
            }
            else
            {
                if (Hora_Agendamento.Substring(2, 1) != ":")
                {
                    zStatus = zStatus + " / " + "Horário Agendamento fornecido inválido (2)";
                }
                else
                {
                    string Horas = Hora_Agendamento.Substring(0, 2);
                    string Minutos = Hora_Agendamento.Substring(3, 2);

                    Int16 number;
                    bool result = Int16.TryParse(Horas, out number);
                    if (!result)
                    {
                        zStatus = zStatus + " / " + "Hora fornecida deve ser numérico (3)";
                    }
                    else
                    {
                        Int16 number2;
                        bool result2 = Int16.TryParse(Minutos, out number2);
                        if (!result2)
                        {
                            zStatus = zStatus + " / " + "Minutos fornecido deve ser numérico (3)";
                        }
                        else
                        {
                            if (System.Convert.ToInt16(Horas) < 0 || System.Convert.ToInt16(Horas) > 23 || System.Convert.ToInt16(Minutos) < 0 || System.Convert.ToInt16(Minutos) > 59)
                            {
                                zStatus = zStatus + " / " + "Horário fornecido inválido (4)";
                            }
                        }
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


            if (Data_Exame.Trim().Length != 8)
            {
                zStatus = zStatus + " / " + "Data Exame fornecida inválida (1)";
            }
            else
            {
                //checar YYYYMMDD
                if (Validar_Data(Data_Exame.Trim()) == false)
                {
                    zStatus = zStatus + " / " + "Data Exame em formato inválido ( deve ser YYYYMMDD ) (1)";
                }
            }




            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            //validar Centro de Custo fornecido
            Pessoa rEmpresa = new Ilitera.Common.Pessoa();


            Int32 xCodUsuario = 0;

            if (zStatus == "")
            {

                //validar Usuário e senha
                Ilitera.Data.eSocial xUser = new Ilitera.Data.eSocial();
                xCodUsuario = xUser.Retornar_Codigo_Usuario(Usuario, Senha);

                if (xCodUsuario == 0)
                {
                    zStatus = zStatus + " / " + "Usuário/Senha inválidos (2)";
                }
            }


            if (zStatus == "")
            {

                if (IdEmpresa != "")
                {
                    rEmpresa.Find(System.Convert.ToInt32(IdEmpresa));

                    if (rEmpresa.Id == 0)
                    {
                        zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                    }

                }
                else
                {
                    //qual condição utilizar ??
                    string rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    rEmpresa.Find(rSelect);

                    if (rEmpresa.Id == 0)
                    {
                        //pode ser duplicidade, procurar então CNPJ e CPF juntos
                        rEmpresa.Find(" dbo.udf_getnumeric(nomecodigo) = '" + CNPJ + "' and idpessoa in ( select nid_empr from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado where tno_CPF = '" + CPF + "' and hdt_dem is null ) ");

                        if (rEmpresa.Id == 0)
                        {
                            zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                        }

                    }
                }

            }

            //começar tratamento do registro
            //verificar se é uma simples inserção, atualização cadastral, nova classif.funcional ou demissão e chamar funções para cada uma delas.
            Empregado rColaborador2 = new Empregado();

            if (zStatus == "")
            {
                //checar se CPF em centro de custo já está cadastrado e ativo             
                rColaborador2.Find(" tNo_CPF = '" + CPF.Trim() + "' and hDt_Dem is null  and nId_Empr in ( " + rEmpresa.Id.ToString() + " ) ");  // select idpessoa from " +  Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where IdGrupoEmpresa in ( select  IdGrupoEmpresa from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where idpessoa = " + rEmpresa.Id.ToString() + " ) and IdPessoa in ( select IdPessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa (nolock) where IsInativo = 0 ) ) ");



                if (rColaborador2.Id == 0)   //realizar inserção do colaborador
                {
                    zStatus = zStatus + " / " + " CPF fornecido não localizado (2)";
                }

            }



            string xData = Data_Exame.Substring(6, 2) + "/" + Data_Exame.Substring(4, 2) + "/" + Data_Exame.Substring(0, 4);

            Clinico exame2 = new Clinico();

            if (zStatus == "")
            {
                //localizar exame
                exame2.Find(" IdEmpregado = " + rColaborador2.Id.ToString() + " and IdExameDicionario = " + Tipo_Exame + " and convert(char(10),DataExame,103 ) = '" + xData + "' ");

                if (exame2.Id != 0)
                {
                    zStatus = zStatus + " / " + " Exame já existe (2)";
                }

            }


            Clinica rClinica = new Clinica();

            if (zStatus == "")
            {
                //localizar exame
                rClinica.Find(" IdJuridicaPapel=8 and IsInativo = 0 and  dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ_Clinica + "' ");

                if (rClinica.Id == 0)
                {
                    zStatus = zStatus + " / " + " Clínica não localizada";
                }

            }


            Medico rMedico = new Medico();

            if (zStatus == "")
            {
                //localizar exame
                //rMedico.Find(" NomeAbreviado = '" + Medico + "' or NomeCompleto = '" + Medico + "' ");

                //ArrayList alMedicos = new Medico().Find(" idpessoa in ( select idpessoa from pessoa where NomeAbreviado = '" + Medico + "' or NomeCompleto = '" + Medico + "' ) ");
                ArrayList alMedicos = new Medico().Find("  dbo.extractinteger(Numero) = '" + Medico + "' ");

                foreach (Medico vMedico in alMedicos)
                {
                    rMedico.Find(vMedico.Id);
                    break;
                }

                if (rMedico.Id == 0)
                {
                    zStatus = zStatus + " / " + " Médico não localizado";
                }

            }





            if (zStatus == "")
            {


                string xDataExame = Data_Exame.Substring(6, 2) + "/" + Data_Exame.Substring(4, 2) + "/" + Data_Exame.Substring(0, 4);

                //salvar exame
                ExameClinicoFacade exame = new ExameClinicoFacade();

                exame.Prontuario = "";
                //exame.Observacao = "";                      

                exame.IdEmpregado = rColaborador2;

                //exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(rColaborador2, rEmpresa.Id);
                exame.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();

                exame.apt_Espaco_Confinado = "";
                exame.apt_Trabalho_Altura = "";
                exame.apt_Transporte = "";
                exame.apt_Submerso = "";
                exame.apt_Eletricidade = "";
                exame.apt_Alimento = "";
                exame.apt_Brigadista = "";
                exame.apt_Socorrista = "";
                exame.apt_Respirador = "";

                ExameDicionario xExameDicionario = new ExameDicionario();

                xExameDicionario.Find(System.Convert.ToInt32(Tipo_Exame));


                exame.IdExameDicionario = xExameDicionario;
                exame.IdJuridica = rClinica;

                exame.DataExame = System.Convert.ToDateTime(xDataExame, ptBr);

                //if (Tipo_Exame == "2" && xDtDemissao.Trim() != "") exame.DataDemissao = System.Convert.ToDateTime(xDtDemissao, ptBr);

                exame.IndResultado = System.Convert.ToInt16(Resultado);

                exame.IdMedico = rMedico;

                if (exame.Id == 0)
                {
                    //  exame.CodBusca = Gerar_CodBusca();
                }

                exame.Tirar_eSocial = false;

                int vStatus = 0;
                try
                {
                    vStatus = exame.Save(xCodUsuario);
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToUpper() != "MÉTODO NÃO-ESTÁTICO REQUER UM DESTINO." && ex.Message.ToUpper() != "NON-STATIC METHOD REQUIRES A TARGET.")
                        throw new Exception(ex.Message.ToString());
                }



                try
                {

                    ExameBase rExame = new ExameBase();
                    rExame.Find(exame.Id);

                    if (rExame.Id != 0)
                    {
                        Anamnese rAnamnese = new Anamnese();
                        rAnamnese.IdExameBase = rExame;
                        rAnamnese.Save();

                        ExameFisico rFisico = new ExameFisico();
                        rFisico.IdExameBase = rExame;
                        rFisico.Save();

                    }

                }
                catch (Exception ex)
                {
                    if (ex.Message.ToUpper() != "MÉTODO NÃO-ESTÁTICO REQUER UM DESTINO." && ex.Message.ToUpper() != "NON-STATIC METHOD REQUIRES A TARGET.")
                        throw new Exception(ex.Message.ToString());
                }






                //pegar exames da guia                
                string rExames = "";
                string rDatas = "";
                string rExames2 = "";
                string rDatas2 = "";

                string xExames_Guia = "";

                Clinico xClinico = new Clinico();
                xClinico.Find(exame.Id);


                if (zStatus == "")
                {
                    try
                    {
                        //acho que a idéia é chamar como na geração do ASO, e no newRow pegar a string dos campos e quebrar para dentro do XML
                        DataSet dsExame = new DataSet();

                        dsExame.Tables.Add(Ilitera.ASO.Report.DataSourceExameAsoPci_Novo.GetDataTableReportAso());

                        DataRow newRow = dsExame.Tables[0].NewRow();

                        Ilitera.ASO.Report.DataSourceExameAsoPci_Novo.PopularRow pRow = new Ilitera.ASO.Report.DataSourceExameAsoPci_Novo.PopularRow(newRow);



                        xClinico.IdPcmso.Find();

                        List<Ghe> ghes = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + xClinico.IdPcmso.IdLaudoTecnico.Id);

                        pRow.AvaliacaoAmbiental(xClinico, xClinico.IdPcmso, ghes, false, "");

                        rExames = newRow["ExamesComplementares"].ToString();
                        rDatas = newRow["DataComplementares"].ToString();
                        rExames2 = newRow["ExamesComplementares2"].ToString();
                        rDatas2 = newRow["DataComplementares2"].ToString();
                    }
                    catch (Exception ex)
                    {
                        zStatus = zStatus + " / " + "Erro na obtenção de exames da guia (2): " + ex.Message;
                    }
                }


                string xExames = "";
                string xExames2 = "";
                string xExames3 = "";
                string xExames4 = "";




                if (zStatus == "")
                {

                    int TotExames = 0;

                    string[] stringSeparators4 = new string[] { "\r\n" };
                    string[] result4;
                    string[] result5;

                    result4 = rExames.Split(stringSeparators4, StringSplitOptions.None);
                    result5 = rDatas.Split(stringSeparators4, StringSplitOptions.None);


                    int zCont = 0;
                    string pExame = "";
                    string pData = "";


                    foreach (string s in result4)
                    {
                        pExame = "";
                        pData = "";

                        if (s.Trim() != "" && s.Trim() != "Exame Complementar não indicado")
                        {
                            pExame = s.Trim();

                            int vCont = 0;

                            foreach (string r in result5)
                            {
                                if (r.Trim() != "")
                                {
                                    if (zCont == vCont)
                                    {
                                        pData = r.Trim();
                                    }
                                }

                                vCont = vCont + 1;

                            }

                            zCont = zCont + 1;

                            if (pExame != "")
                            {
                                if (pData == "")
                                {
                                    if (TotExames < 8)
                                    {
                                        xExames = xExames + " /n " + pExame;
                                    }
                                    else if (TotExames < 15)
                                    {
                                        xExames2 = xExames2 + " /n " + pExame;
                                    }
                                    else if (TotExames < 22)
                                    {
                                        xExames3 = xExames3 + " /n " + pExame;
                                    }
                                    else
                                    {
                                        xExames4 = xExames4 + " /n " + pExame;
                                    }

                                    xExames_Guia = xExames_Guia + pExame + " ,";
                                    TotExames = TotExames + 1;
                                }
                            }



                        }


                    }



                    result4 = rExames2.Split(stringSeparators4, StringSplitOptions.None);
                    result5 = rDatas2.Split(stringSeparators4, StringSplitOptions.None);


                    zCont = 0;
                    pExame = "";
                    pData = "";


                    foreach (string s in result4)
                    {
                        pExame = "";
                        pData = "";

                        if (s.Trim() != "" && s.Trim() != "Exame Complementar não indicado")
                        {
                            pExame = s.Trim();

                            int vCont = 0;

                            foreach (string r in result5)
                            {
                                if (r.Trim() != "")
                                {
                                    if (zCont == vCont)
                                    {
                                        pData = r.Trim();
                                    }
                                }

                                vCont = vCont + 1;

                            }

                            zCont = zCont + 1;

                            if (pExame != "")
                            {
                                if (pData == "")
                                {
                                    if (TotExames < 8)
                                    {
                                        xExames = xExames + " /n " + pExame;
                                    }
                                    else if (TotExames < 15)
                                    {
                                        xExames2 = xExames2 + " /n " + pExame;
                                    }
                                    else if (TotExames < 22)
                                    {
                                        xExames3 = xExames3 + " /n " + pExame;
                                    }
                                    else
                                    {
                                        xExames4 = xExames4 + " /n " + pExame;
                                    }


                                    xExames_Guia = xExames_Guia + pExame + " ,";
                                    TotExames = TotExames + 1;
                                }
                            }



                        }


                    }


                    if (xExames_Guia.Trim() == "")
                    {
                        xExames = "Avaliação Clínica";
                    }
                    else
                    {
                        if (xExames_Guia.ToUpper().IndexOf("AVALIAÇÃO CL") < 0 && xExames_Guia.ToUpper().IndexOf("EXAME CL") < 0)
                        {
                            if (TotExames < 8)
                            {
                                xExames = xExames + " /n " + "Avaliação Clínica";
                            }
                            else if (TotExames < 15)
                            {
                                xExames2 = xExames2 + " /n " + "Avaliação Clínica";
                            }
                            else if (TotExames < 22)
                            {
                                xExames3 = xExames3 + " /n " + "Avaliação Clínica";
                            }
                            else
                            {
                                xExames4 = xExames4 + " /n " + "Avaliação Clínica";
                            }

                        }

                    }




                }



                //tirar numeração dos exames
                for (int jCont = 20; jCont > 0; jCont--)
                {
                    string xBusca = "";

                    xBusca = jCont.ToString().Trim() + ".";

                    xExames = xExames.Replace(xBusca, "");
                    xExames2 = xExames2.Replace(xBusca, "");
                    xExames3 = xExames3.Replace(xBusca, "");
                    xExames4 = xExames4.Replace(xBusca, "");
                }






                //gerar guia/aso/pci
                try
                {
                    exame2 = new Clinico();
                    exame2.Find(exame.Id);


                    exame2.IdExameDicionario.Find();

                    //criar PDF
                    string xTipo = "";

                    if (exame2.IdExameDicionario.Id == 1)
                    {
                        xTipo = "A";
                    }
                    else if (exame2.IdExameDicionario.Id == 2)
                    {
                        xTipo = "D";
                    }
                    else if (exame2.IdExameDicionario.Id == 3)
                    {
                        xTipo = "M";
                    }
                    else if (exame2.IdExameDicionario.Id == 4)
                    {
                        xTipo = "P";
                    }
                    else if (exame2.IdExameDicionario.Id == 5)
                    {
                        xTipo = "R";
                    }

                    CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[6];

                    exame2.IdEmpregado.Find();
                    exame2.IdEmpregado.nID_EMPR.Find();




                    reports[0] = new DataSourceGuia2(exame2.IdEmpregado.nID_EMPR, rColaborador2.Id, rEmpresa.Id, rClinica.Id, xExames, xExames2, xExames3, xExames4, Data_Exame, Hora_Agendamento, xTipo, "0", "", "S", exame2.IdEmpregadoFuncao.Id).GetReport_Nova();

                    reports[1] = new DataSourceExameAsoPci_Novo(exame2).GetReport_Unico("Via Empresa");
                    reports[2] = new DataSourceExameAsoPci_Novo(exame2).GetReport_Unico("Via Empregado");
                    reports[3] = new DataSourceExameAsoPci_Novo(exame2).GetReport_Unico("Via Clínica");
                    reports[4] = new DataSourceExameAsoPci_Novo(exame2).GetReport_Unico("Via Ilitera");

                    //PCI
                    reports[5] = new DataSourceExameAsoPci_Novo(exame2).GetReportPciUnico(false);




                    zStatus = CreatePDFMerged(reports, "", false, exame2);

                    zStatus = zStatus.Replace("I:\\websites\\Novo_Daiti\\Guias\\", "https://www.ilitera.net.br/daiti/guias/");

                    //string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Status>" + zStatus + "</Status>";

                    //zRet.LoadXml(vRet);

                    reports = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                catch (Exception ex)
                {
                    zStatus = "Erro na criação/salva do exame:" + ex.Message;

                    //string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                    //zRet.LoadXml(vRet);

                }


            }


         

            

            //return zRet;

            return zStatus;


        }





        [WebMethod]
        public XmlDocument Dados_Colaborador(string Usuario, string Senha, string CPF, string CNPJ, String IdEmpresa)
        {
            XmlDocument zRet = new XmlDocument();
            string xRet = @"<?xml version=""1.0"" encoding=""UTF-8""?>";


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

            if (CNPJ.Trim().Length != 14)
            {
                zStatus = zStatus + " / CNPJ fornecido inválido (1)";
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

                if (IdEmpresa != "")
                {
                    rEmpresa.Find(System.Convert.ToInt32(IdEmpresa));

                    if (rEmpresa.Id == 0)
                    {
                        zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                    }

                }
                else
                {
                    //qual condição utilizar ??
                    string rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    rEmpresa.Find(rSelect);

                    if (rEmpresa.Id == 0)
                    {
                        //pode ser duplicidade, procurar então CNPJ e CPF juntos
                        rEmpresa.Find(" dbo.udf_getnumeric(nomecodigo) = '" + CNPJ + "' and idpessoa in ( select nid_empr from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado where tno_CPF = '" + CPF + "' and hdt_dem is null ) ");

                        if (rEmpresa.Id == 0)
                        {
                            zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                        }

                    }
                }

            }

            //começar tratamento do registro
            //verificar se é uma simples inserção, atualização cadastral, nova classif.funcional ou demissão e chamar funções para cada uma delas.

            Empregado rColaborador2 = new Empregado();

            if (zStatus == "")
            {
                //problema se tiver dois registros do colaborador no mesmo grupo            
                rColaborador2.Find(" tNo_CPF = '" + CPF.Trim() + "' and hDt_Dem is null  and nId_Empr in ( " + rEmpresa.Id.ToString() + " ) ");  // select idpessoa from " +  Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where IdGrupoEmpresa in ( select  IdGrupoEmpresa from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where idpessoa = " + rEmpresa.Id.ToString() + " ) and IdPessoa in ( select IdPessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa (nolock) where IsInativo = 0 ) ) ");



                if (rColaborador2.Id == 0)   //realizar inserção do colaborador
                {
                    zStatus = zStatus + " / " + " CPF fornecido não localizado (2)";
                }


            }

            if (zStatus == "")
            {

                //montar XML

                try
                {
                    string xDataNasc = rColaborador2.hDT_NASC.ToString("dd/MM/yyyy", ptBr);
                    string xDataAdm = rColaborador2.hDT_ADM.ToString("dd/MM/yyyy", ptBr);

                    EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(rColaborador2);
                    string xFuncao = empregadoFuncao.GetNomeFuncao();
                    string xSetor = empregadoFuncao.GetNomeSetor();

                    empregadoFuncao.nID_SETOR.Find();
                    Int32 xIdSetor = empregadoFuncao.nID_SETOR.Id;

                    empregadoFuncao.nID_FUNCAO.Find();
                    Int32 xIdFuncao = empregadoFuncao.nID_FUNCAO.Id;


                    string xRetXML = "<Dados_Cadastrais>";

                    xRetXML = xRetXML + "<Nome>" + rColaborador2.tNO_EMPG.Trim() + "</Nome>";
                    xRetXML = xRetXML + "<Matricula>" + rColaborador2.tCOD_EMPR.Trim() + "</Matricula>";
                    xRetXML = xRetXML + "<Data_Nascimento>" + xDataNasc + "</Data_Nascimento>";
                    xRetXML = xRetXML + "<Data_Admissao>" + xDataAdm + "</Data_Admissao>";
                    xRetXML = xRetXML + "<RG>" + rColaborador2.tNO_IDENTIDADE.Trim() + "</RG>";
                    xRetXML = xRetXML + "<Sexo>" + rColaborador2.tSEXO.Trim() + "</Sexo>";
                    xRetXML = xRetXML + "<email>" + rColaborador2.teMail.Trim() + "</email>";
                    xRetXML = xRetXML + "<Setor_Atual>" + xSetor + "</Setor_Atual>";
                    xRetXML = xRetXML + "<Id_Setor_Atual>" + xIdSetor.ToString().Trim() + "</Id_Setor_Atual>";
                    xRetXML = xRetXML + "<Cargo_Atual>" + xFuncao + "</Cargo_Atual>";
                    xRetXML = xRetXML + "<Id_Cargo_Atual>" + xIdFuncao.ToString().Trim() + "</Id_Cargo_Atual>";
                    xRetXML = xRetXML + "<Telefone>" + rColaborador2.tTELEFONE.Trim() + "</Telefone>";

                    xRetXML = xRetXML + "<CEP>" + rColaborador2.tEND_CEP.Trim() + "</CEP>";
                    xRetXML = xRetXML + "<Endereco>" + rColaborador2.tEND_NOME.Trim() + "</Endereco>";
                    xRetXML = xRetXML + "<Numero>" + rColaborador2.tEND_NUMERO.Trim() + "</Numero>";
                    xRetXML = xRetXML + "<UF>" + rColaborador2.tEND_UF.Trim() + "</UF>";
                    xRetXML = xRetXML + "<Cidade>" + rColaborador2.tEND_MUNICIPIO.Trim() + "</Cidade>";
                    xRetXML = xRetXML + "<Bairro>" + rColaborador2.tEND_BAIRRO.Trim() + "</Bairro>";

                    xRetXML = xRetXML + "</Dados_Cadastrais>";

                    byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

                    MemoryStream ms2 = new MemoryStream(encodedString2);
                    ms2.Flush();
                    ms2.Position = 0;

                    XmlDocument zLote = new XmlDocument();
                    zLote.Load(ms2);

                    zRet = zLote;

                }
                catch (Exception ex)
                {
                    zStatus = zStatus + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;
                }
                finally
                {

                    if (zStatus != "")
                    {
                        //montar XMLDocument com erro
                        string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                        zRet.LoadXml(vRet);
                    }

                }

            }
            else
            {
                string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                zRet.LoadXml(vRet);
            }

            return zRet;

        }




        [WebMethod]
        public XmlDocument Retorna_Clinicas(string Usuario, string Senha, string Data_Inicial, string Data_Final, string CNPJ)
        {

            string zStatus = "";
            string zAcao = "";



            XmlDocument zRet = new XmlDocument();

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

            if (CNPJ.Trim() != "")
            {
                if (CNPJ.Trim().Length != 14)
                {
                    zStatus = zStatus + " / CNPJ fornecido inválido (1)";
                }
            }

            string xData_Inicial = "";
            string xData_Final = "";


            if (Data_Inicial.Trim().Length != 8)
            {
                zStatus = zStatus + " / " + "Data Inicial fornecida inválida (1)";
            }
            else
            {
                xData_Inicial = Data_Inicial.Substring(6, 2) + "/" + Data_Inicial.Substring(4, 2) + "/" + Data_Inicial.Substring(0, 4);

                //checar YYYYMMDD
                if (Validar_Data(Data_Inicial.Trim()) == false)
                {
                    zStatus = zStatus + " / " + "Data Inicial em formato inválido ( deve ser YYYYMMDD ) (1)";
                }
            }

            if (Data_Final.Trim().Length != 8)
            {
                zStatus = zStatus + " / " + "Data Final fornecida inválida (1)";
            }
            else
            {
                xData_Final = Data_Final.Substring(6, 2) + "/" + Data_Final.Substring(4, 2) + "/" + Data_Final.Substring(0, 4);
                //checar YYYYMMDD
                if (Validar_Data(Data_Final.Trim()) == false)
                {
                    zStatus = zStatus + " / " + "Data Final em formato inválido ( deve ser YYYYMMDD ) (1)";
                }
            }




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


            DataSet rDs = new DataSet();

            if (zStatus == "")
            {


                try
                {
                    Ilitera.Data.Clientes_Clinicas xClinicas = new Ilitera.Data.Clientes_Clinicas();

                    rDs = xClinicas.Retornar_Dados_Clinicas(xData_Inicial, xData_Final, CNPJ.Trim());
                }
                catch (Exception ex)
                {
                    zStatus = "Erro na obtenção de dados das clínicas: " + ex.Message;
                }

            }


            if (zStatus == "")
            {

                //montar XML

                try
                {

                    //string xClinica = "";
                    string xRetXML = "<RETORNO>";

                    for (int zLinha = 0; zLinha < rDs.Tables[0].Rows.Count; zLinha++)
                    {


                        xRetXML = xRetXML + "<Clinica>";

                        xRetXML = xRetXML + "<NomeClinica>" + rDs.Tables[0].Rows[zLinha]["NomeClinica"].ToString().Trim().Replace("/", " ").Replace("&", " ") + "</NomeClinica>";
                        xRetXML = xRetXML + "<CNPJ>" + rDs.Tables[0].Rows[zLinha]["CNPJ"].ToString().Trim().Replace("/", "") + "</CNPJ>";
                        xRetXML = xRetXML + "<RazaoSocial>" + rDs.Tables[0].Rows[zLinha]["RazaoSocial"].ToString().Trim().Replace("/", " ").Replace("&", " ") + "</RazaoSocial>";
                        xRetXML = xRetXML + "<TipoLogradouro>" + rDs.Tables[0].Rows[zLinha]["TipoLogradouro"].ToString().Trim().Replace("/", " ") + "</TipoLogradouro>";
                        xRetXML = xRetXML + "<Logradouro>" + rDs.Tables[0].Rows[zLinha]["Logradouro"].ToString().Trim().Replace("/", " ") + "</Logradouro>";
                        xRetXML = xRetXML + "<Numero>" + rDs.Tables[0].Rows[zLinha]["Numero"].ToString().Trim().Replace("/", " ") + "</Numero>";
                        xRetXML = xRetXML + "<Bairro>" + rDs.Tables[0].Rows[zLinha]["Bairro"].ToString().Trim().Replace("/", " ") + "</Bairro>";
                        xRetXML = xRetXML + "<CEP>" + rDs.Tables[0].Rows[zLinha]["CEP"].ToString().Trim() + "</CEP>";
                        xRetXML = xRetXML + "<Cidade>" + rDs.Tables[0].Rows[zLinha]["Cidade"].ToString().Trim().Replace("/", " ") + "</Cidade>";
                        xRetXML = xRetXML + "<UF>" + rDs.Tables[0].Rows[zLinha]["UF"].ToString().Trim() + "</UF>";
                        xRetXML = xRetXML + "<email>" + rDs.Tables[0].Rows[zLinha]["email"].ToString().Trim().Replace("/", " ") + "</email>";
                        xRetXML = xRetXML + "<Horario>" + rDs.Tables[0].Rows[zLinha]["Horario"].ToString().Trim().Replace("/", " ").Replace("<", "").Replace(">", "") + "</Horario>";
                        xRetXML = xRetXML + "<Contato>" + rDs.Tables[0].Rows[zLinha]["Contato"].ToString().Trim().Replace("/", " ") + "</Contato>";
                        xRetXML = xRetXML + "<Fone>" + rDs.Tables[0].Rows[zLinha]["Fone"].ToString().Trim().Replace("/", " ") + "</Fone>";
                        xRetXML = xRetXML + "<Data_Cadastro>" + rDs.Tables[0].Rows[zLinha]["Data_Cadastro"].ToString().Trim().Replace("/", " ") + "</Data_Cadastro>";


                        xRetXML = xRetXML + "</Clinica>";


                    }

                    xRetXML = xRetXML + "</RETORNO>";

                    byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

                    MemoryStream ms2 = new MemoryStream(encodedString2);
                    ms2.Flush();
                    ms2.Position = 0;

                    XmlDocument zLote = new XmlDocument();
                    zLote.Load(ms2);

                    zRet = zLote;

                }
                catch (Exception ex)
                {
                    zStatus = zStatus + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;
                }
                finally
                {

                    if (zStatus != "")
                    {
                        //montar XMLDocument com erro
                        string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                        zRet.LoadXml(vRet);
                    }

                }

            }
            else
            {
                string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                zRet.LoadXml(vRet);
            }

            return zRet;

        }




        [WebMethod]
        public XmlDocument Retorna_Clinicas_Exames(string Usuario, string Senha)
        {

            string zStatus = "";
            string zAcao = "";

            XmlDocument zRet = new XmlDocument();

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


            DataSet rDs = new DataSet();

            if (zStatus == "")
            {

                try
                {
                    Ilitera.Data.Clientes_Clinicas xClinicas = new Ilitera.Data.Clientes_Clinicas();

                    rDs = xClinicas.Retornar_Clinicas_Valores("01/01/1980", "31/12/2050");
                }
                catch (Exception ex)
                {
                    zStatus = "Erro na obtenção de dados das clínicas: " + ex.Message;
                }

            }


            if (zStatus == "")
            {

                //montar XML

                try
                {

                    string xClinica = "";
                    string xRetXML = "<RETORNO>";

                    for (int zLinha = 0; zLinha < rDs.Tables[0].Rows.Count; zLinha++)
                    {
                        xRetXML = xRetXML + "<Exame>";
                        xRetXML = xRetXML + "<CNPJ>" + rDs.Tables[0].Rows[zLinha]["CNPJ"].ToString().Trim().Replace("/", "") + "</CNPJ>";
                        xRetXML = xRetXML + "<Nome_Exame>" + rDs.Tables[0].Rows[zLinha]["Exame"].ToString().Trim().Replace("/", "") + "</Nome_Exame>";
                        xRetXML = xRetXML + "<Valor_Exame>" + rDs.Tables[0].Rows[zLinha]["Valor"].ToString().Trim() + "</Valor_Exame>";
                        xRetXML = xRetXML + "<IdExame>" + rDs.Tables[0].Rows[zLinha]["IdExame"].ToString().Trim() + "</IdExame>";
                        xRetXML = xRetXML + "</Exame>";
                    }

                    xRetXML = xRetXML + "</RETORNO>";

                    byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

                    MemoryStream ms2 = new MemoryStream(encodedString2);
                    ms2.Flush();
                    ms2.Position = 0;

                    XmlDocument zLote = new XmlDocument();
                    zLote.Load(ms2);

                    zRet = zLote;

                }
                catch (Exception ex)
                {
                    zStatus = zStatus + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;
                }
                finally
                {

                    if (zStatus != "")
                    {
                        //montar XMLDocument com erro
                        string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                        zRet.LoadXml(vRet);
                    }

                }

            }
            else
            {
                string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                zRet.LoadXml(vRet);
            }

            return zRet;

        }



        [WebMethod]
        public String Atualizar_Resultado_Exame(string Usuario, string Senha, string Tipo_Exame, string CPF, string Data_ASO, string CNPJ, string Resultado, String IdEmpresa)
        {
            string zStatus = "";

            XmlDocument zRet = new XmlDocument();


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

            if (Tipo_Exame != "1" && Tipo_Exame != "2" && Tipo_Exame != "3" && Tipo_Exame != "4" && Tipo_Exame != "5")
            {
                zStatus = zStatus + " / Tipo Exame fornecido inválido (1)";
            }

            if (CNPJ.Trim().Length != 14)
            {
                zStatus = zStatus + " / CNPJ fornecido inválido (1)";
            }


            if (Data_ASO.Trim().Length != 8)
            {
                zStatus = zStatus + " / " + "Data Exame fornecida inválida (1)";
            }
            else
            {
                //checar YYYYMMDD
                if (Validar_Data(Data_ASO.Trim()) == false)
                {
                    zStatus = zStatus + " / " + "Data ASO em formato inválido ( deve ser YYYYMMDD ) (1)";
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

            if (Resultado != "1" && Resultado != "2")
            {
                zStatus = zStatus + " / " + "Resultado fornecido inválido (1)";
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

                if (IdEmpresa != "")
                {
                    rEmpresa.Find(System.Convert.ToInt32(IdEmpresa));

                    if (rEmpresa.Id == 0)
                    {
                        zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                    }

                }
                else
                {
                    //qual condição utilizar ??
                    string rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    rEmpresa.Find(rSelect);

                    if (rEmpresa.Id == 0)
                    {
                        //pode ser duplicidade, procurar então CNPJ e CPF juntos
                        rEmpresa.Find(" dbo.udf_getnumeric(nomecodigo) = '" + CNPJ + "' and idpessoa in ( select nid_empr from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado where tno_CPF = '" + CPF + "' and hdt_dem is null ) ");

                        if (rEmpresa.Id == 0)
                        {
                            zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                        }

                    }
                }

            }

            //começar tratamento do registro
            //verificar se é uma simples inserção, atualização cadastral, nova classif.funcional ou demissão e chamar funções para cada uma delas.

            Empregado rColaborador2 = new Empregado();

            if (zStatus == "")
            {
                //problema se tiver dois registros do colaborador no mesmo grupo            
                rColaborador2.Find(" tNo_CPF = '" + CPF.Trim() + "' and hDt_Dem is null  and nId_Empr in ( " + rEmpresa.Id.ToString() + " ) ");  // select idpessoa from " +  Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where IdGrupoEmpresa in ( select  IdGrupoEmpresa from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where idpessoa = " + rEmpresa.Id.ToString() + " ) and IdPessoa in ( select IdPessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa (nolock) where IsInativo = 0 ) ) ");



                if (rColaborador2.Id == 0)   //realizar inserção do colaborador
                {
                    zStatus = zStatus + " / " + " CPF fornecido não localizado (2)";
                }


            }


            string xData = Data_ASO.Substring(6, 2) + "/" + Data_ASO.Substring(4, 2) + "/" + Data_ASO.Substring(0, 4);

            Clinico xClinico = new Clinico();

            if (zStatus == "")
            {
                //localizar exame
                xClinico.Find(" IdEmpregado = " + rColaborador2.Id.ToString() + " and IdExameDicionario = " + Tipo_Exame + " and convert(char(10),DataExame,103 ) = '" + xData + "' ");

                if (xClinico.Id == 0)
                {
                    zStatus = zStatus + " / " + " Exame não localizado (2)";
                }

            }

            if (zStatus == "")
            {
                try
                {
                    xClinico.IndResultado = System.Convert.ToInt16(Resultado);
                    xClinico.Save();
                }
                catch (Exception ex)
                {
                    zStatus = zStatus + " / " + "Erro na atualização cadastral do colaborador (2): " + ex.Message;
                }
            }

            //if (zStatus != "")
            //{
            //    //montar XMLDocument com erro
            //    string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

            //    zRet.LoadXml(vRet);
            //}
            //else
            //{
            //    string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Status>Processamento Finalizado</Status>";

            //    zRet.LoadXml(vRet);
            //}

            //return zRet;

            if (zStatus == "") zStatus = "Processamento Finalizado.";

            return zStatus;

        }



        [WebMethod]
        private XmlDocument Retorna_Exames_ASO_OLD(string Usuario, string Senha, string Tipo_Exame, string CPF, string Data_ASO, string CNPJ, String IdEmpresa)
        {

            //validar campos e obter Clínico pelo CPF + Data_Exame + Tipo_Exame


            string zStatus = "";
            string zAcao = "";

            XmlDocument zRet = new XmlDocument();


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

            if (Tipo_Exame != "1" && Tipo_Exame != "2" && Tipo_Exame != "3" && Tipo_Exame != "4" && Tipo_Exame != "5")
            {
                zStatus = zStatus + " / Tipo Exame fornecido inválido (1)";
            }

            if (CNPJ.Trim().Length != 14)
            {
                zStatus = zStatus + " / CNPJ fornecido inválido (1)";
            }


            if (Data_ASO.Trim().Length != 8)
            {
                zStatus = zStatus + " / " + "Data Exame fornecida inválida (1)";
            }
            else
            {
                //checar YYYYMMDD
                if (Validar_Data(Data_ASO.Trim()) == false)
                {
                    zStatus = zStatus + " / " + "Data ASO em formato inválido ( deve ser YYYYMMDD ) (1)";
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

                if (IdEmpresa != "")
                {
                    rEmpresa.Find(System.Convert.ToInt32(IdEmpresa));

                    if (rEmpresa.Id == 0)
                    {
                        zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                    }

                }
                else
                {
                    //qual condição utilizar ??
                    string rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    rEmpresa.Find(rSelect);

                    if (rEmpresa.Id == 0)
                    {
                        //pode ser duplicidade, procurar então CNPJ e CPF juntos
                        rEmpresa.Find(" dbo.udf_getnumeric(nomecodigo) = '" + CNPJ + "' and idpessoa in ( select nid_empr from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado where tno_CPF = '" + CPF + "' and hdt_dem is null ) ");

                        if (rEmpresa.Id == 0)
                        {
                            zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                        }

                    }
                }

            }

            //começar tratamento do registro
            //verificar se é uma simples inserção, atualização cadastral, nova classif.funcional ou demissão e chamar funções para cada uma delas.

            Empregado rColaborador2 = new Empregado();

            if (zStatus == "")
            {
                //problema se tiver dois registros do colaborador no mesmo grupo            
                rColaborador2.Find(" tNo_CPF = '" + CPF.Trim() + "' and hDt_Dem is null  and nId_Empr in ( " + rEmpresa.Id.ToString() + " ) ");  // select idpessoa from " +  Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where IdGrupoEmpresa in ( select  IdGrupoEmpresa from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where idpessoa = " + rEmpresa.Id.ToString() + " ) and IdPessoa in ( select IdPessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa (nolock) where IsInativo = 0 ) ) ");



                if (rColaborador2.Id == 0)   //realizar inserção do colaborador
                {
                    zStatus = zStatus + " / " + " CPF fornecido não localizado (2)";
                }


            }


            string xData = Data_ASO.Substring(6, 2) + "/" + Data_ASO.Substring(4, 2) + "/" + Data_ASO.Substring(0, 4);

            Clinico xClinico = new Clinico();

            if (zStatus == "")
            {
                //localizar exame
                xClinico.Find(" IdEmpregado = " + rColaborador2.Id.ToString() + " and IdExameDicionario = " + Tipo_Exame + " and convert(char(10),DataExame,103 ) = '" + xData + "' ");

                if (xClinico.Id == 0)
                {
                    zStatus = zStatus + " / " + " Exame não localizado (2)";
                }

            }


            string xExames = "";
            string xDatas = "";
            string xExames2 = "";
            string xDatas2 = "";


            if (zStatus == "")
            {
                try
                {
                    //acho que a idéia é chamar como na geração do ASO, e no newRow pegar a string dos campos e quebrar para dentro do XML
                    DataSet dsExame = new DataSet();

                    dsExame.Tables.Add(Ilitera.ASO.Report.DataSourceExameAsoPci_Novo.GetDataTableReportAso());

                    DataRow newRow = dsExame.Tables[0].NewRow();

                    Ilitera.ASO.Report.DataSourceExameAsoPci_Novo.PopularRow pRow = new Ilitera.ASO.Report.DataSourceExameAsoPci_Novo.PopularRow(newRow);



                    xClinico.IdPcmso.Find();

                    List<Ghe> ghes = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + xClinico.IdPcmso.IdLaudoTecnico.Id);

                    pRow.AvaliacaoAmbiental(xClinico, xClinico.IdPcmso, ghes, false, "");

                    xExames = newRow["ExamesComplementares"].ToString();
                    xDatas = newRow["DataComplementares"].ToString();
                    xExames2 = newRow["ExamesComplementares2"].ToString();
                    xDatas2 = newRow["DataComplementares2"].ToString();
                }
                catch (Exception ex)
                {
                    zStatus = zStatus + " / " + "Erro na obtenção de exames (2): " + ex.Message;
                }
            }


            string xRetXML = "<RETORNO>";


            if (zStatus == "")
            {

                try
                {
                    string[] stringSeparators4 = new string[] { "\r\n" };
                    string[] result4;
                    string[] result5;

                    result4 = xExames.Split(stringSeparators4, StringSplitOptions.None);
                    result5 = xDatas.Split(stringSeparators4, StringSplitOptions.None);


                    int zCont = 0;
                    string pExame = "";
                    string pData = "";


                    foreach (string s in result4)
                    {
                        pExame = "";
                        pData = "";

                        if (s.Trim() != "" && s.Trim() != "Exame Complementar não indicado")
                        {
                            pExame = s.Trim();

                            int vCont = 0;

                            foreach (string r in result5)
                            {
                                if (r.Trim() != "")
                                {
                                    if (zCont == vCont)
                                    {
                                        pData = r.Trim();
                                    }
                                }

                                vCont = vCont + 1;

                            }

                            zCont = zCont + 1;

                            if (pExame != "")
                            {
                                xRetXML = xRetXML + "<Exame>";

                                xRetXML = xRetXML + "<NomeExame>" + pExame + "</NomeExame>";

                                if (pData != "" && pData != "*")
                                {
                                    xRetXML = xRetXML + "<DataExameValido>" + pData + "</DataExameValido>";
                                }
                                else
                                {
                                    xRetXML = xRetXML + "<DataExameValido></DataExameValido>";
                                }

                                xRetXML = xRetXML + "</Exame>";


                            }

                        }


                    }




                    xRetXML = xRetXML + "</RETORNO>";

                    byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

                    MemoryStream ms2 = new MemoryStream(encodedString2);
                    ms2.Flush();
                    ms2.Position = 0;

                    XmlDocument zLote = new XmlDocument();
                    zLote.Load(ms2);

                    zRet = zLote;

                }
                catch (Exception ex)
                {
                    zStatus = zStatus + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;
                }
                finally
                {

                    if (zStatus != "")
                    {
                        //montar XMLDocument com erro
                        string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                        zRet.LoadXml(vRet);
                    }

                }

            }
            else
            {
                string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                zRet.LoadXml(vRet);
            }

            return zRet;
        }








        [WebMethod]
        public XmlDocument Retorna_Exames_ASO(string Usuario, string Senha, string Tipo_Exame, string CPF, string CNPJ, String IdEmpresa)
        {

            //validar campos e obter Clínico pelo CPF + Data_Exame + Tipo_Exame
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            string Data_ASO = System.DateTime.Now.ToString("dd/MM/yyyy", ptBr);
            string zStatus = "";
            string zAcao = "";


            Ghe ghe = new Ghe();


            XmlDocument zRet = new XmlDocument();


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

            if (Tipo_Exame != "1" && Tipo_Exame != "2" && Tipo_Exame != "3" && Tipo_Exame != "4" && Tipo_Exame != "5")
            {
                zStatus = zStatus + " / Tipo Exame fornecido inválido (1)";
            }

            if (CNPJ.Trim().Length != 14)
            {
                zStatus = zStatus + " / CNPJ fornecido inválido (1)";
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

                if (IdEmpresa != "")
                {
                    rEmpresa.Find(System.Convert.ToInt32(IdEmpresa));

                    if (rEmpresa.Id == 0)
                    {
                        zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                    }

                }
                else
                {
                    //qual condição utilizar ??
                    string rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    rEmpresa.Find(rSelect);

                    if (rEmpresa.Id == 0)
                    {
                        //pode ser duplicidade, procurar então CNPJ e CPF juntos
                        rEmpresa.Find(" dbo.udf_getnumeric(nomecodigo) = '" + CNPJ + "' and idpessoa in ( select nid_empr from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado where tno_CPF = '" + CPF + "' and hdt_dem is null ) ");

                        if (rEmpresa.Id == 0)
                        {
                            zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                        }

                    }
                }

            }

            //começar tratamento do registro
            //verificar se é uma simples inserção, atualização cadastral, nova classif.funcional ou demissão e chamar funções para cada uma delas.

            Empregado rColaborador2 = new Empregado();

            if (zStatus == "")
            {
                //problema se tiver dois registros do colaborador no mesmo grupo            
                rColaborador2.Find(" tNo_CPF = '" + CPF.Trim() + "' and hDt_Dem is null  and nId_Empr in ( " + rEmpresa.Id.ToString() + " ) ");  // select idpessoa from " +  Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where IdGrupoEmpresa in ( select  IdGrupoEmpresa from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where idpessoa = " + rEmpresa.Id.ToString() + " ) and IdPessoa in ( select IdPessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa (nolock) where IsInativo = 0 ) ) ");



                if (rColaborador2.Id == 0)   //realizar inserção do colaborador
                {
                    zStatus = zStatus + " / " + " CPF fornecido não localizado (2)";
                }


            }


            string xData = Data_ASO; // Data_ASO.Substring(6, 2) + "/" + Data_ASO.Substring(4, 2) + "/" + Data_ASO.Substring(0, 4);

            Clinico xClinico = new Clinico();

            if (zStatus == "")
            {
                //localizar exame
                xClinico.Find(" IdEmpregado = " + rColaborador2.Id.ToString() + " and IdExameDicionario = " + Tipo_Exame + " and convert(char(10),DataExame,103 ) = '" + xData + "' ");

                //se não localizar exame, posso criar sem salvar
                if (xClinico.Id == 0)
                {
                    //   zStatus = zStatus + " / " + " Exame não localizado (2)";
                    //salvar exame

                    xClinico.IdEmpregado = rColaborador2;

                    //exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                    xClinico.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(rColaborador2, rEmpresa.Id);
                    xClinico.IdPcmso = xClinico.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();


                    ExameDicionario xExameDicionario = new ExameDicionario();

                    xExameDicionario.Find(System.Convert.ToInt32(Tipo_Exame));


                    xClinico.IdExameDicionario = xExameDicionario;
                    //xClinico.IdJuridica = rClinica;

                    xClinico.DataExame = System.Convert.ToDateTime(xData, ptBr);

                    //if (Tipo_Exame == "2" && xDtDemissao.Trim() != "") exame.DataDemissao = System.Convert.ToDateTime(xDtDemissao, ptBr);

                    xClinico.IndResultado = 3;


                }

            }


            //string xExames = "";
            //string xDatas = "";
            //string xExames2 = "";
            //string xDatas2 = "";





            //if (zStatus == "")
            //{
            //    try
            //    {
            //        //acho que a idéia é chamar como na geração do ASO, e no newRow pegar a string dos campos e quebrar para dentro do XML
            //        DataSet dsExame = new DataSet();

            //        dsExame.Tables.Add(Ilitera.ASO.Report.DataSourceExameAsoPci_Novo.GetDataTableReportAso());

            //        DataRow newRow = dsExame.Tables[0].NewRow();

            //        Ilitera.ASO.Report.DataSourceExameAsoPci_Novo.PopularRow pRow = new Ilitera.ASO.Report.DataSourceExameAsoPci_Novo.PopularRow(newRow);


            //        pRow.AvaliacaoAmbiental(xClinico, xClinico.IdPcmso, ghes, false, "");

            //        //tenho que trazer periodicidade dos exames

            //        xExames = newRow["ExamesComplementares"].ToString();
            //        xDatas = ""; //newRow["DataComplementares"].ToString();
            //        xExames2 = newRow["ExamesComplementares2"].ToString();
            //        xDatas2 = ""; // newRow["DataComplementares2"].ToString();
            //    }
            //    catch (Exception ex)
            //    {
            //        zStatus = zStatus + " / " + "Erro na obtenção de exames (2): " + ex.Message;
            //    }
            //}


            string xRetXML = "<RETORNO>";


            if (zStatus == "")
            {


                try
                {

                    xClinico.IdPcmso.Find();
                    xClinico.IdPcmso.IdLaudoTecnico.Find();

                    List<Ghe> ghes = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + xClinico.IdPcmso.IdLaudoTecnico.Id);


                    //string[] stringSeparators4 = new string[] { "\r\n" };
                    //string[] result4;


                    //result4 = xExames.Split(stringSeparators4, StringSplitOptions.None);



                    //int zCont = 0;
                    //string pExame = "";

                    //foreach (string s in result4)
                    //{
                    //    pExame = "";                        

                    //    if (s.Trim() != "" && s.Trim() != "Exame Complementar não indicado")
                    //    {
                    //        pExame = s.Trim();

                    //        zCont = zCont + 1;

                    //        if (pExame != "")
                    //        {
                    //            xRetXML = xRetXML + "<Exame>";

                    //            xRetXML = xRetXML + "<NomeExame>" + pExame + "</NomeExame>";


                    //buscar periodicidade no PCMSO desse exame
                    xClinico.IdPcmso.Find();
                    int IdGhe = xClinico.IdEmpregadoFuncao.GetIdGheEmpregado(xClinico.IdPcmso.IdLaudoTecnico);

                    ghe = ghes.Find(delegate (Ghe g) { return g.Id == IdGhe; });


                    string wherePcmsoPlan = xClinico.GetWhereExamesComplementares(ghe);

                    List<PcmsoPlanejamento> listPcmsoPlan = new PcmsoPlanejamento().Find<PcmsoPlanejamento>(wherePcmsoPlan);

                    List<ExameComplementar> ret = new List<ExameComplementar>();

                    foreach (PcmsoPlanejamento pcmsoPlan in listPcmsoPlan)
                    {
                        xRetXML = xRetXML + "<Exame>";

                        pcmsoPlan.IdExameDicionario.Find();
                        xRetXML = xRetXML + "<NomeExame>" + pcmsoPlan.IdExameDicionario.Nome.Trim() + "</NomeExame>";
                        xRetXML = xRetXML + "<IdExame>" + pcmsoPlan.IdExameDicionario.Id.ToString().Trim() + "</IdExame>";

                        string rPeriodo = ExameDicionario.GetPeriodicidadeExame(pcmsoPlan);
                        if (rPeriodo.Trim() != "")
                        {
                            xRetXML = xRetXML + "<Periodicidade>" + rPeriodo + "</Periodicidade>";
                        }
                        else
                        {
                            xRetXML = xRetXML + "<Periodicidade></Periodicidade>";
                        }

                        xRetXML = xRetXML + "</Exame>";
                    }







                    //        }

                    //    }


                    //}




                    xRetXML = xRetXML + "</RETORNO>";

                    byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

                    MemoryStream ms2 = new MemoryStream(encodedString2);
                    ms2.Flush();
                    ms2.Position = 0;

                    XmlDocument zLote = new XmlDocument();
                    zLote.Load(ms2);

                    zRet = zLote;

                }
                catch (Exception ex)
                {
                    zStatus = zStatus + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;
                }
                finally
                {

                    if (zStatus != "")
                    {
                        //montar XMLDocument com erro
                        string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                        zRet.LoadXml(vRet);
                    }

                }

            }
            else
            {
                string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                zRet.LoadXml(vRet);
            }

            return zRet;
        }





        [WebMethod]
        public XmlDocument Retorna_Riscos(string Usuario, string Senha, string Tipo_Exame, string CPF, string CNPJ, String IdEmpresa)
        {

            //validar campos e obter Clínico pelo CPF + Data_Exame + Tipo_Exame
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            string Data_ASO = System.DateTime.Now.ToString("dd/MM/yyyy", ptBr);
            string zStatus = "";
            string zAcao = "";
            string zNomeGHE = "";


            Ghe ghe = new Ghe();


            XmlDocument zRet = new XmlDocument();


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

            if (Tipo_Exame != "1" && Tipo_Exame != "2" && Tipo_Exame != "3" && Tipo_Exame != "4" && Tipo_Exame != "5")
            {
                zStatus = zStatus + " / Tipo Exame fornecido inválido (1)";
            }

            if (CNPJ.Trim().Length != 14)
            {
                zStatus = zStatus + " / CNPJ fornecido inválido (1)";
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

                if (IdEmpresa != "")
                {
                    rEmpresa.Find(System.Convert.ToInt32(IdEmpresa));

                    if (rEmpresa.Id == 0)
                    {
                        zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                    }

                }
                else
                {
                    //qual condição utilizar ??
                    string rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    rEmpresa.Find(rSelect);

                    if (rEmpresa.Id == 0)
                    {
                        //pode ser duplicidade, procurar então CNPJ e CPF juntos
                        rEmpresa.Find(" dbo.udf_getnumeric(nomecodigo) = '" + CNPJ + "' and idpessoa in ( select nid_empr from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado where tno_CPF = '" + CPF + "' and hdt_dem is null ) ");

                        if (rEmpresa.Id == 0)
                        {
                            zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                        }

                    }
                }

            }


            //começar tratamento do registro
            //verificar se é uma simples inserção, atualização cadastral, nova classif.funcional ou demissão e chamar funções para cada uma delas.

            Empregado rColaborador2 = new Empregado();

            if (zStatus == "")
            {
                //problema se tiver dois registros do colaborador no mesmo grupo            
                rColaborador2.Find(" tNo_CPF = '" + CPF.Trim() + "' and hDt_Dem is null  and nId_Empr in ( " + rEmpresa.Id.ToString() + " ) ");  // select idpessoa from " +  Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where IdGrupoEmpresa in ( select  IdGrupoEmpresa from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where idpessoa = " + rEmpresa.Id.ToString() + " ) and IdPessoa in ( select IdPessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa (nolock) where IsInativo = 0 ) ) ");



                if (rColaborador2.Id == 0)   //realizar inserção do colaborador
                {
                    zStatus = zStatus + " / " + " CPF fornecido não localizado (2)";
                }


            }


            string xData = Data_ASO; // Data_ASO.Substring(6, 2) + "/" + Data_ASO.Substring(4, 2) + "/" + Data_ASO.Substring(0, 4);

            Clinico xClinico = new Clinico();

            if (zStatus == "")
            {
                //localizar exame
                xClinico.Find(" IdEmpregado = " + rColaborador2.Id.ToString() + " and IdExameDicionario = " + Tipo_Exame + " and convert(char(10),DataExame,103 ) = '" + xData + "' ");

                //se não localizar exame, posso criar sem salvar
                if (xClinico.Id == 0)
                {
                    //   zStatus = zStatus + " / " + " Exame não localizado (2)";
                    //salvar exame

                    xClinico.IdEmpregado = rColaborador2;

                    //exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                    xClinico.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(rColaborador2, rEmpresa.Id);
                    xClinico.IdPcmso = xClinico.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();


                    ExameDicionario xExameDicionario = new ExameDicionario();

                    xExameDicionario.Find(System.Convert.ToInt32(Tipo_Exame));


                    xClinico.IdExameDicionario = xExameDicionario;
                    //xClinico.IdJuridica = rClinica;

                    xClinico.DataExame = System.Convert.ToDateTime(xData, ptBr);

                    //if (Tipo_Exame == "2" && xDtDemissao.Trim() != "") exame.DataDemissao = System.Convert.ToDateTime(xDtDemissao, ptBr);

                    xClinico.IndResultado = 3;


                }

            }


            //string xExames = "";
            //string xDatas = "";
            //string xExames2 = "";
            //string xDatas2 = "";



            try
            {
                if (zStatus == "")
                {

                    xClinico.IdPcmso.Find();
                    xClinico.IdPcmso.IdLaudoTecnico.Find();


                    List<Ghe> ghes = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + xClinico.IdPcmso.IdLaudoTecnico.Id);



                    if (ghes == null || ghes.Count == 0)
                        ghe = xClinico.IdEmpregadoFuncao.GetGheEmpregado(xClinico.IdPcmso.IdLaudoTecnico);
                    else
                    {
                        int IdGhe = xClinico.IdEmpregadoFuncao.GetIdGheEmpregado(xClinico.IdPcmso.IdLaudoTecnico);

                        ghe = ghes.Find(delegate (Ghe g) { return g.Id == IdGhe; });
                    }

                    if (ghe == null || ghe.Id == 0)
                    {
                        zStatus = "O empregado " + xClinico.IdEmpregado.tNO_EMPG
                            + " não está associado a nenhum GHE ou o PCMSO ainda não foi atualizado para o novo Laudo Técnico realizado!";
                    }
                    else
                    {
                        zNomeGHE = ghe.tNO_FUNC;
                    }


                }


                if (zStatus == "")
                {

                    //string sRiscosAmbientais = ghe.RiscosAmbientaisAso();



                    xClinico.IdPcmso.Find();
                    xClinico.IdPcmso.IdCliente.Find();

                    //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("PRAJNA") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("QTECK") > 0  || Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToString().ToUpper().IndexOf("EY") > 0)
                    //    sRiscosOcupacionais = ghe.RiscosOcupacionaisAso(xCliente.Exibir_Riscos_ASO, false, xCliente.Riscos_PPRA);
                    //else
                    string sRiscosOcupacionais = ghe.RiscosOcupacionaisAso(xClinico.IdPcmso.IdCliente.Exibir_Riscos_ASO, xClinico.IdPcmso.IdCliente.Bloquear_Dispensado, xClinico.IdPcmso.IdCliente.Riscos_PPRA);

                    sRiscosOcupacionais = sRiscosOcupacionais.Replace("**", "");
                    sRiscosOcupacionais = sRiscosOcupacionais.Replace("*", "");
                    sRiscosOcupacionais = sRiscosOcupacionais.Replace("Dispensado de exames complementares", "");
                    sRiscosOcupacionais = sRiscosOcupacionais.Replace("\r\n", "");
                    sRiscosOcupacionais = sRiscosOcupacionais.Replace("\n", "");

                    //

                    string xRetXML = "<RETORNO>";


                    xRetXML = xRetXML + "<Riscos>";

                    xRetXML = xRetXML + sRiscosOcupacionais;

                    xRetXML = xRetXML + "</Riscos>";

                    xRetXML = xRetXML + "<MedicoCoordenador>";

                    xClinico.IdPcmso.Find();
                    xClinico.IdPcmso.IdCoordenador.Find();
                    xRetXML = xRetXML + xClinico.IdPcmso.IdCoordenador.NomeCompleto;

                    xRetXML = xRetXML + "</MedicoCoordenador>";

                    xRetXML = xRetXML + "<CRMMedicoCoordenador>";

                    xClinico.IdPcmso.Find();
                    xClinico.IdPcmso.IdCoordenador.Find();
                    xRetXML = xRetXML + (xClinico.IdPcmso.IdCoordenador.Numero + " " + xClinico.IdPcmso.IdCoordenador.UF).Trim();
                    //xClinico.IdPcmso.IdCoordenador.Contato;
                    //xClinico.IdPcmso.IdCoordenador.IdPessoa.GetContatoTelefonico();

                    xRetXML = xRetXML + "</CRMMedicoCoordenador>";

                    xRetXML = xRetXML + "<ContatoMedicoCoordenador>";

                    xClinico.IdPcmso.Find();
                    xClinico.IdPcmso.IdCoordenador.Find();
                    xClinico.IdPcmso.IdCoordenador.IdPessoa.Find();
                    xRetXML = xRetXML + (xClinico.IdPcmso.IdCoordenador.IdPessoa.GetContatoTelefonico().DDD + " " + xClinico.IdPcmso.IdCoordenador.IdPessoa.GetContatoTelefonico().Numero).Trim();

                    xRetXML = xRetXML + "</ContatoMedicoCoordenador>";



                    xRetXML = xRetXML + "<Contrato>";

                    xClinico.IdEmpregado.Find();
                    xClinico.IdEmpregado.nID_EMPR.Find();
                    xRetXML = xRetXML + xClinico.IdEmpregado.nID_EMPR.Contrato_Numero;

                    xRetXML = xRetXML + "</Contrato>";


                    xRetXML = xRetXML + "<GHE>";
                    xRetXML = xRetXML + zNomeGHE;
                    xRetXML = xRetXML + "</GHE>";


                    xRetXML = xRetXML + "</RETORNO>";

                    byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

                    MemoryStream ms2 = new MemoryStream(encodedString2);
                    ms2.Flush();
                    ms2.Position = 0;

                    XmlDocument zLote = new XmlDocument();
                    zLote.Load(ms2);

                    zRet = zLote;
                }

            }
            catch (Exception ex)
            {
                zStatus = zStatus + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;
            }
            finally
            {

                if (zStatus != "")
                {
                    //montar XMLDocument com erro
                    string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                    zRet.LoadXml(vRet);
                }

            }



            return zRet;
        }





        [WebMethod]
        public XmlDocument Retorna_Dados_Empresa(string Usuario, string Senha, string CNPJ, String IdEmpresa)
        {

            //validar campos e obter Clínico pelo CPF + Data_Exame + Tipo_Exame
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            string Data_ASO = System.DateTime.Now.ToString("dd/MM/yyyy", ptBr);
            string zStatus = "";
            string zAcao = "";


            Ghe ghe = new Ghe();


            XmlDocument zRet = new XmlDocument();


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


            if (CNPJ.Trim().Length != 14)
            {
                zStatus = zStatus + " / CNPJ fornecido inválido (1)";
            }








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

                if (IdEmpresa != "")
                {
                    rEmpresa.Find(System.Convert.ToInt32(IdEmpresa));

                    if (rEmpresa.Id == 0)
                    {
                        zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                    }

                }
                else
                {


                    //qual condição utilizar ??
                    string rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    rEmpresa.Find(rSelect);

                    if (rEmpresa.Id == 0)
                    {
                        zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                    }
                }
            }



            try
            {



                if (zStatus == "")
                {


                    string xRetXML = "<RETORNO>";

                    xRetXML = xRetXML + "<RazaoSocial>" + rEmpresa.NomeCompleto + "</RazaoSocial>";
                    xRetXML = xRetXML + "<NomeFantasia>" + rEmpresa.NomeAbreviado + "</NomeFantasia>";
                    xRetXML = xRetXML + "<CNPJ>" + rEmpresa.NomeCodigo + "</CNPJ>";

                    TipoLogradouro rTipo = new TipoLogradouro();
                    rTipo.Find(rEmpresa.GetEndereco().IdTipoLogradouro.Id);

                    xRetXML = xRetXML + "<TipoLogradouro>" + rTipo.NomeAbreviado + "</TipoLogradouro>";
                    xRetXML = xRetXML + "<Logradouro>" + rEmpresa.GetEndereco().Logradouro + "</Logradouro>";
                    xRetXML = xRetXML + "<Numero>" + rEmpresa.GetEndereco().Numero + "</Numero>";
                    xRetXML = xRetXML + "<Complemento>" + rEmpresa.GetEndereco().Complemento + "</Complemento>";
                    xRetXML = xRetXML + "<Bairro>" + rEmpresa.GetEndereco().Bairro + "</Bairro>";
                    xRetXML = xRetXML + "<Municipio>" + rEmpresa.GetEndereco().Municipio + "</Municipio>";
                    xRetXML = xRetXML + "<UF>" + rEmpresa.GetEndereco().Uf + "</UF>";

                    xRetXML = xRetXML + "<email>" + rEmpresa.Email + "</email>";

                    CNAE rCNAE = new CNAE();
                    rCNAE.Find(rEmpresa.GetJuridica().IdCNAE.Id);

                    xRetXML = xRetXML + "<CNAE>" + rCNAE.Codigo + "</CNAE>";


                    xRetXML = xRetXML + "</RETORNO>";

                    xRetXML = xRetXML.Replace("&", " ");

                    byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

                    MemoryStream ms2 = new MemoryStream(encodedString2);
                    ms2.Flush();
                    ms2.Position = 0;

                    XmlDocument zLote = new XmlDocument();
                    zLote.Load(ms2);

                    zRet = zLote;
                }

            }
            catch (Exception ex)
            {
                zStatus = zStatus + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;
            }
            finally
            {

                if (zStatus != "")
                {
                    //montar XMLDocument com erro
                    string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                    zRet.LoadXml(vRet);
                }

            }



            return zRet;
        }





        [WebMethod]
        public XmlDocument Retornar_Exames(string Usuario, string Senha)
        {

            //validar campos e obter Clínico pelo CPF + Data_Exame + Tipo_Exame
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            string Data_ASO = System.DateTime.Now.ToString("dd/MM/yyyy", ptBr);
            string zStatus = "";
            string zAcao = "";


            Ghe ghe = new Ghe();


            XmlDocument zRet = new XmlDocument();


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



            string xRetXML = "<RETORNO>";

            if (zStatus == "")
            {

                try
                {

                    ArrayList alExames = new ExameDicionario().Find(" nome is not null order by nome ");

                    foreach (ExameDicionario exame in alExames)
                    {
                        xRetXML = xRetXML + "<Exame>";
                        xRetXML = xRetXML + "<NomeExame>" + exame.Nome.Trim() + "</NomeExame>";
                        xRetXML = xRetXML + "<IdExame>" + exame.Id.ToString().Trim() + "</IdExame>";
                        xRetXML = xRetXML + "</Exame>";
                    }

                    xRetXML = xRetXML + "</RETORNO>";

                    byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

                    MemoryStream ms2 = new MemoryStream(encodedString2);
                    ms2.Flush();
                    ms2.Position = 0;

                    XmlDocument zLote = new XmlDocument();
                    zLote.Load(ms2);

                    zRet = zLote;
                }
                catch (Exception ex)
                {
                    zStatus = zStatus + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;
                }
                finally
                {

                    if (zStatus != "")
                    {
                        //montar XMLDocument com erro
                        string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                        zRet.LoadXml(vRet);
                    }

                }


            }


            return zRet;

        }




        [WebMethod]
        public XmlDocument Retorna_Exames_Complementares(string Usuario, string Senha, string CPF, string CNPJ, String IdEmpresa)
        {

            //validar campos e obter Clínico pelo CPF + Data_Exame + Tipo_Exame
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            string zStatus = "";
            string zAcao = "";


            XmlDocument zRet = new XmlDocument();


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


            if (CNPJ.Trim().Length != 14)
            {
                zStatus = zStatus + " / CNPJ fornecido inválido (1)";
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

                if (IdEmpresa != "")
                {
                    rEmpresa.Find(System.Convert.ToInt32(IdEmpresa));

                    if (rEmpresa.Id == 0)
                    {
                        zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                    }

                }
                else
                {
                    //qual condição utilizar ??
                    string rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    rEmpresa.Find(rSelect);

                    if (rEmpresa.Id == 0)
                    {
                        //pode ser duplicidade, procurar então CNPJ e CPF juntos
                        rEmpresa.Find(" dbo.udf_getnumeric(nomecodigo) = '" + CNPJ + "' and idpessoa in ( select nid_empr from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado where tno_CPF = '" + CPF + "' and hdt_dem is null ) ");

                        if (rEmpresa.Id == 0)
                        {
                            zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                        }

                    }
                }

            }

            //começar tratamento do registro
            //verificar se é uma simples inserção, atualização cadastral, nova classif.funcional ou demissão e chamar funções para cada uma delas.

            Empregado rColaborador2 = new Empregado();

            if (zStatus == "")
            {
                //problema se tiver dois registros do colaborador no mesmo grupo            
                rColaborador2.Find(" tNo_CPF = '" + CPF.Trim() + "' and hDt_Dem is null  and nId_Empr in ( " + rEmpresa.Id.ToString() + " ) ");  // select idpessoa from " +  Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where IdGrupoEmpresa in ( select  IdGrupoEmpresa from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where idpessoa = " + rEmpresa.Id.ToString() + " ) and IdPessoa in ( select IdPessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa (nolock) where IsInativo = 0 ) ) ");



                if (rColaborador2.Id == 0)   //realizar inserção do colaborador
                {
                    zStatus = zStatus + " / " + " CPF fornecido não localizado (2)";
                }


            }



            string xRetXML = "<RETORNO>";


            if (zStatus == "")
            {

                try
                {

                    ArrayList alExames = new Complementar().Find("IdEmpregado=" + rColaborador2.Id.ToString() + " and IndResultado in (1,2) ORDER BY DataExame DESC");

                    foreach (Complementar exameNO in alExames)
                    {


                        xRetXML = xRetXML + "<Exame>";

                        exameNO.IdExameDicionario.Find();
                        xRetXML = xRetXML + "<NomeExame>" + exameNO.IdExameDicionario.Nome.Trim() + "</NomeExame>";
                        xRetXML = xRetXML + "<IdExame>" + exameNO.IdExameDicionario.Id.ToString().Trim() + "</IdExame>";

                        xRetXML = xRetXML + "<DataExame>" + exameNO.DataExame.ToString("dd/MM/yyyy", ptBr) + "</DataExame>";

                        xRetXML = xRetXML + "</Exame>";
                    }


                    xRetXML = xRetXML + "</RETORNO>";

                    byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

                    MemoryStream ms2 = new MemoryStream(encodedString2);
                    ms2.Flush();
                    ms2.Position = 0;

                    XmlDocument zLote = new XmlDocument();
                    zLote.Load(ms2);

                    zRet = zLote;

                }
                catch (Exception ex)
                {
                    zStatus = zStatus + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;
                }
                finally
                {

                    if (zStatus != "")
                    {
                        //montar XMLDocument com erro
                        string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                        zRet.LoadXml(vRet);
                    }

                }

            }
            else
            {
                string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                zRet.LoadXml(vRet);
            }

            return zRet;
        }




        [WebMethod]
        public XmlDocument Retorna_Aptidoes(string Usuario, string Senha, string CPF, string CNPJ, String IdEmpresa)
        {

            //validar campos e obter Clínico pelo CPF + Data_Exame + Tipo_Exame
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            string Data_ASO = System.DateTime.Now.ToString("dd/MM/yyyy", ptBr);
            string zStatus = "";
            string zAcao = "";


            Ghe ghe = new Ghe();


            XmlDocument zRet = new XmlDocument();


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


            if (CNPJ.Trim().Length != 14)
            {
                zStatus = zStatus + " / CNPJ fornecido inválido (1)";
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

                if (IdEmpresa != "")
                {
                    rEmpresa.Find(System.Convert.ToInt32(IdEmpresa));

                    if (rEmpresa.Id == 0)
                    {
                        zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                    }

                }
                else
                {
                    //qual condição utilizar ??
                    string rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    rEmpresa.Find(rSelect);

                    if (rEmpresa.Id == 0)
                    {
                        //pode ser duplicidade, procurar então CNPJ e CPF juntos
                        rEmpresa.Find(" dbo.udf_getnumeric(nomecodigo) = '" + CNPJ + "' and idpessoa in ( select nid_empr from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado where tno_CPF = '" + CPF + "' and hdt_dem is null ) ");

                        if (rEmpresa.Id == 0)
                        {
                            zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                        }

                    }
                }

            }

            //começar tratamento do registro
            //verificar se é uma simples inserção, atualização cadastral, nova classif.funcional ou demissão e chamar funções para cada uma delas.

            Empregado rColaborador2 = new Empregado();

            if (zStatus == "")
            {
                //problema se tiver dois registros do colaborador no mesmo grupo            
                rColaborador2.Find(" tNo_CPF = '" + CPF.Trim() + "' and hDt_Dem is null  and nId_Empr in ( " + rEmpresa.Id.ToString() + " ) ");  // select idpessoa from " +  Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where IdGrupoEmpresa in ( select  IdGrupoEmpresa from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where idpessoa = " + rEmpresa.Id.ToString() + " ) and IdPessoa in ( select IdPessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa (nolock) where IsInativo = 0 ) ) ");



                if (rColaborador2.Id == 0)   //realizar inserção do colaborador
                {
                    zStatus = zStatus + " / " + " CPF fornecido não localizado (2)";
                }


            }


            DataSet vDs = new DataSet();

            if (zStatus == "")
            {
                try
                {
                    Ilitera.Data.PPRA_EPI xApt = new Ilitera.Data.PPRA_EPI();
                    vDs = xApt.Retornar_Aptidoes_Empregado(rColaborador2.Id);
                }
                catch (Exception ex)
                {
                    zStatus = zStatus + " / " + ex.Message;
                }

            }







            string xRetXML = "<RETORNO>";


            if (zStatus == "")
            {

                try
                {


                    for (int rCont = 0; rCont < vDs.Tables[0].Rows.Count; rCont++)
                    {



                        if (vDs.Tables[0].Rows[rCont]["x1"].ToString().Trim() != "")
                        {
                            xRetXML = xRetXML + "<Aptidao>" + vDs.Tables[0].Rows[rCont]["x1"].ToString().Trim() + "</Aptidao>";
                        }
                        if (vDs.Tables[0].Rows[rCont]["x2"].ToString().Trim() != "")
                        {
                            xRetXML = xRetXML + "<Aptidao>" + vDs.Tables[0].Rows[rCont]["x2"].ToString().Trim() + "</Aptidao>";
                        }
                        if (vDs.Tables[0].Rows[rCont]["x3"].ToString().Trim() != "")
                        {
                            xRetXML = xRetXML + "<Aptidao>" + vDs.Tables[0].Rows[rCont]["x3"].ToString().Trim() + "</Aptidao>";
                        }
                        if (vDs.Tables[0].Rows[rCont]["x4"].ToString().Trim() != "")
                        {
                            xRetXML = xRetXML + "<Aptidao>" + vDs.Tables[0].Rows[rCont]["x4"].ToString().Trim() + "</Aptidao>";
                        }
                        if (vDs.Tables[0].Rows[rCont]["x5"].ToString().Trim() != "")
                        {
                            xRetXML = xRetXML + "<Aptidao>" + vDs.Tables[0].Rows[rCont]["x5"].ToString().Trim() + "</Aptidao>";
                        }
                        if (vDs.Tables[0].Rows[rCont]["x6"].ToString().Trim() != "")
                        {
                            xRetXML = xRetXML + "<Aptidao>" + vDs.Tables[0].Rows[rCont]["x6"].ToString().Trim() + "</Aptidao>";
                        }
                        if (vDs.Tables[0].Rows[rCont]["x7"].ToString().Trim() != "")
                        {
                            xRetXML = xRetXML + "<Aptidao>" + vDs.Tables[0].Rows[rCont]["x7"].ToString().Trim() + "</Aptidao>";
                        }
                        if (vDs.Tables[0].Rows[rCont]["x8"].ToString().Trim() != "")
                        {
                            xRetXML = xRetXML + "<Aptidao>" + vDs.Tables[0].Rows[rCont]["x8"].ToString().Trim() + "</Aptidao>";
                        }
                        if (vDs.Tables[0].Rows[rCont]["x9"].ToString().Trim() != "")
                        {
                            xRetXML = xRetXML + "<Aptidao>" + vDs.Tables[0].Rows[rCont]["x9"].ToString().Trim() + "</Aptidao>";
                        }
                        if (vDs.Tables[0].Rows[rCont]["x10"].ToString().Trim() != "")
                        {
                            xRetXML = xRetXML + "<Aptidao>" + vDs.Tables[0].Rows[rCont]["x10"].ToString().Trim() + "</Aptidao>";
                        }
                        if (vDs.Tables[0].Rows[rCont]["x11"].ToString().Trim() != "")
                        {
                            xRetXML = xRetXML + "<Aptidao>" + vDs.Tables[0].Rows[rCont]["x11"].ToString().Trim() + "</Aptidao>";
                        }


                    }


                    xRetXML = xRetXML + "</RETORNO>";

                    byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

                    MemoryStream ms2 = new MemoryStream(encodedString2);
                    ms2.Flush();
                    ms2.Position = 0;

                    XmlDocument zLote = new XmlDocument();
                    zLote.Load(ms2);

                    zRet = zLote;

                }
                catch (Exception ex)
                {
                    zStatus = zStatus + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;
                }
                finally
                {

                    if (zStatus != "")
                    {
                        //montar XMLDocument com erro
                        string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                        zRet.LoadXml(vRet);
                    }

                }

            }
            else
            {
                string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                zRet.LoadXml(vRet);
            }

            return zRet;
        }


        



        [WebMethod]
        public XmlDocument Retorna_Ocorrencias_CNPJ_CPF(string Usuario, string Senha, string CPF, string CNPJ)
        {

            //validar campos e obter Clínico pelo CPF + Data_Exame + Tipo_Exame
            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            string Data_ASO = System.DateTime.Now.ToString("dd/MM/yyyy", ptBr);
            string zStatus = "";
            string zAcao = "";


            Ghe ghe = new Ghe();


            XmlDocument zRet = new XmlDocument();


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



            if (CNPJ.Trim().Length != 14)
            {
                zStatus = zStatus + " / CNPJ fornecido inválido (1)";
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






            //validar Centro de Custo fornecido
            // Pessoa rEmpresa = new Ilitera.Common.Pessoa();


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


            string xRetXML = "<RETORNO>";

            if (zStatus == "")
            {

                try
                {

                    ArrayList alEmpresas = new Ilitera.Common.Pessoa().Find(" dbo.udf_getnumeric(nomecodigo) = '" + CNPJ + "' and idpessoa in ( select nid_empr from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado where tno_CPF = '" + CPF + "' and hdt_dem is null ) ");

                    foreach (Ilitera.Common.Pessoa vPessoa in alEmpresas)
                    {
                        xRetXML = xRetXML + "<Empresa>";
                        xRetXML = xRetXML + "<IdEmpresa>" + vPessoa.Id.ToString().Trim() + "</IdEmpresa>";
                        xRetXML = xRetXML + "<NomeEmpresa>" + vPessoa.NomeAbreviado.Trim() + "</NomeEmpresa>";
                        xRetXML = xRetXML + "</Empresa>";
                    }


                    xRetXML = xRetXML + "</RETORNO>";

                    byte[] encodedString2 = Encoding.UTF8.GetBytes(xRetXML);

                    MemoryStream ms2 = new MemoryStream(encodedString2);
                    ms2.Flush();
                    ms2.Position = 0;

                    XmlDocument zLote = new XmlDocument();
                    zLote.Load(ms2);

                    zRet = zLote;

                }
                catch (Exception ex)
                {
                    zStatus = zStatus + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;
                }
                finally
                {

                    if (zStatus != "")
                    {
                        //montar XMLDocument com erro
                        string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                        zRet.LoadXml(vRet);
                    }

                }

            }
            else
            {
                string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

                zRet.LoadXml(vRet);
            }

            return zRet;
        }





        [WebMethod]
        public String Atualizar_Aptidoes_Exame(string Usuario, string Senha, string Tipo_Exame, string CPF, string Data_ASO, string CNPJ, string Trabalho_Altura, string Espacos_Confinados,
                                               string Transporte_Motorizado, string Atividades_Submersas, string Eletricidade, string Aquaviarios, string Manipulacao_Alimentos,
                                               string Brigadista, string Socorrista, string Trabalho_Bordo, string Radiacao, string PPR, string Respiradores, String IdEmpresa)
        {
            string zStatus = "";

            XmlDocument zRet = new XmlDocument();

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

            if (Tipo_Exame != "1" && Tipo_Exame != "2" && Tipo_Exame != "3" && Tipo_Exame != "4" && Tipo_Exame != "5")
            {
                zStatus = zStatus + " / Tipo Exame fornecido inválido (1)";
            }

            if (CNPJ.Trim().Length != 14)
            {
                zStatus = zStatus + " / CNPJ fornecido inválido (1)";
            }


            if (Data_ASO.Trim().Length != 8)
            {
                zStatus = zStatus + " / " + "Data Exame fornecida inválida (1)";
            }
            else
            {
                //checar YYYYMMDD
                if (Validar_Data(Data_ASO.Trim()) == false)
                {
                    zStatus = zStatus + " / " + "Data ASO em formato inválido ( deve ser YYYYMMDD ) (1)";
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

            if (Trabalho_Altura != "1" && Trabalho_Altura != "2")
            {
                zStatus = zStatus + " / " + "Trabalho Altura fornecido inválido (1)";
            }

            if (Espacos_Confinados != "1" && Espacos_Confinados != "2")
            {
                zStatus = zStatus + " / " + "Espaços confinados fornecido inválido (1)";
            }

            if (Transporte_Motorizado != "1" && Transporte_Motorizado != "2")
            {
                zStatus = zStatus + " / " + "Transporte Motorizado fornecido inválido (1)";
            }

            if (Atividades_Submersas != "1" && Atividades_Submersas != "2")
            {
                zStatus = zStatus + " / " + "Atividades Submersas fornecido inválido (1)";
            }

            if (Eletricidade != "1" && Eletricidade != "2")
            {
                zStatus = zStatus + " / " + "Eletricidade fornecido inválido (1)";
            }

            if (Aquaviarios != "1" && Aquaviarios != "2")
            {
                zStatus = zStatus + " / " + "Aquaviarios fornecido inválido (1)";
            }

            if (Manipulacao_Alimentos != "1" && Manipulacao_Alimentos != "2")
            {
                zStatus = zStatus + " / " + "Manipulacao Alimentos fornecido inválido (1)";
            }

            if (Brigadista != "1" && Brigadista != "2")
            {
                zStatus = zStatus + " / " + "Brigadista fornecido inválido (1)";
            }

            if (Socorrista != "1" && Socorrista != "2")
            {
                zStatus = zStatus + " / " + "Socorrista fornecido inválido (1)";
            }

            if (Trabalho_Bordo != "1" && Trabalho_Bordo != "2")
            {
                zStatus = zStatus + " / " + "Trabalho Bordo fornecido inválido (1)";
            }

            if (Radiacao != "1" && Radiacao != "2")
            {
                zStatus = zStatus + " / " + "Radiacao fornecido inválido (1)";
            }

            if (PPR != "1" && PPR != "2")
            {
                zStatus = zStatus + " / " + "PPR fornecido inválido (1)";
            }

            if (Respiradores != "1" && Respiradores != "2")
            {
                zStatus = zStatus + " / " + "Respiradores fornecido inválido (1)";
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

                if (IdEmpresa != "")
                {
                    rEmpresa.Find(System.Convert.ToInt32(IdEmpresa));

                    if (rEmpresa.Id == 0)
                    {
                        zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                    }

                }
                else
                {
                    //qual condição utilizar ??
                    string rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                    rEmpresa.Find(rSelect);

                    if (rEmpresa.Id == 0)
                    {
                        //pode ser duplicidade, procurar então CNPJ e CPF juntos
                        rEmpresa.Find(" dbo.udf_getnumeric(nomecodigo) = '" + CNPJ + "' and idpessoa in ( select nid_empr from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblempregado where tno_CPF = '" + CPF + "' and hdt_dem is null ) ");

                        if (rEmpresa.Id == 0)
                        {
                            zStatus = zStatus + " / " + " CNPJ fornecido não localizado (2)";
                        }

                    }
                }

            }

            //começar tratamento do registro
            //verificar se é uma simples inserção, atualização cadastral, nova classif.funcional ou demissão e chamar funções para cada uma delas.

            Empregado rColaborador2 = new Empregado();

            if (zStatus == "")
            {
                //problema se tiver dois registros do colaborador no mesmo grupo            
                rColaborador2.Find(" tNo_CPF = '" + CPF.Trim() + "' and hDt_Dem is null  and nId_Empr in ( " + rEmpresa.Id.ToString() + " ) ");  // select idpessoa from " +  Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where IdGrupoEmpresa in ( select  IdGrupoEmpresa from  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica (nolock) where idpessoa = " + rEmpresa.Id.ToString() + " ) and IdPessoa in ( select IdPessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pessoa (nolock) where IsInativo = 0 ) ) ");



                if (rColaborador2.Id == 0)   //realizar inserção do colaborador
                {
                    zStatus = zStatus + " / " + " CPF fornecido não localizado (2)";
                }


            }


            string xData = Data_ASO.Substring(6, 2) + "/" + Data_ASO.Substring(4, 2) + "/" + Data_ASO.Substring(0, 4);

            Clinico xClinico = new Clinico();

            if (zStatus == "")
            {
                //localizar exame
                xClinico.Find(" IdEmpregado = " + rColaborador2.Id.ToString() + " and IdExameDicionario = " + Tipo_Exame + " and convert(char(10),DataExame,103 ) = '" + xData + "' ");

                if (xClinico.Id == 0)
                {
                    zStatus = zStatus + " / " + " Exame não localizado (2)";
                }

            }

            if (zStatus == "")
            {
                try
                {
                    xClinico.apt_Trabalho_Altura2 = (System.Convert.ToInt16(Trabalho_Altura) - 1).ToString();
                    xClinico.apt_Espaco_Confinado2 = (System.Convert.ToInt16(Espacos_Confinados) - 1).ToString();
                    xClinico.apt_Transporte2 = (System.Convert.ToInt16(Transporte_Motorizado) - 1).ToString();
                    xClinico.apt_Submerso2 = (System.Convert.ToInt16(Atividades_Submersas) - 1).ToString();
                    xClinico.apt_Eletricidade2 = (System.Convert.ToInt16(Eletricidade) - 1).ToString();
                    xClinico.apt_Aquaviario2 = (System.Convert.ToInt16(Aquaviarios) - 1).ToString();
                    xClinico.apt_Alimento2 = (System.Convert.ToInt16(Manipulacao_Alimentos) - 1).ToString();
                    xClinico.apt_Brigadista2 = (System.Convert.ToInt16(Brigadista) - 1).ToString();
                    xClinico.apt_Socorrista2 = (System.Convert.ToInt16(Socorrista) - 1).ToString();
                    xClinico.apt_Radiacao2 = (System.Convert.ToInt16(Radiacao) - 1).ToString();
                    xClinico.apt_Respirador2 = (System.Convert.ToInt16(Respiradores) - 1).ToString();                    
                    xClinico.Save();
                }
                catch (Exception ex)
                {
                    zStatus = zStatus + " / " + "Erro na atualização cadastral do colaborador (2): " + ex.Message;
                }
            }


            //if (zStatus != "")
            //{
            //    //montar XMLDocument com erro
            //    string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Erro>" + zStatus + "</Erro>";

            //    zRet.LoadXml(vRet);
            //}
            //else
            //{
            //    string vRet = @"<?xml version=""1.0"" encoding=""UTF-8""?><Status>Processamento Finalizado</Status>";

            //    zRet.LoadXml(vRet);
            //}

            //return zRet;


            if (zStatus == "") zStatus = "Processamento Finalizado.";

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


        protected string CreatePDFMerged(CrystalDecisions.CrystalReports.Engine.ReportClass[] reports, string watermark, bool RenumerarPaginas, Clinico xExame)
        {
            Stream[] streams = new Stream[reports.Length];

            int i = 0;

            foreach (CrystalDecisions.CrystalReports.Engine.ReportClass report in reports)
            {
                if (RenumerarPaginas)
                    report.ReportDefinition.ReportObjects["PaginaNdeN1"].ObjectFormat.EnableSuppress = true;

                streams[i] = report.ExportToStream(ExportFormatType.PortableDocFormat);

                report.Close();

                i++;
            }

            string xPath = CreatePDFMerged(streams, watermark, RenumerarPaginas, xExame);

            return xPath;
        }



        protected string CreatePDFMerged(Stream[] streams, string watermark, bool RenumerarPaginas, Clinico exame)
        {
            MemoryStream reportStream = PdfSharpUtility.MergeReports(streams, string.Empty, watermark, RenumerarPaginas);

            exame.IdEmpregado.Find();
            exame.IdEmpregado.nID_EMPR.Find();
            Cliente xCliente = new Cliente(exame.IdEmpregado.nID_EMPR.Id);

            exame.IdJuridica.Find();
            Clinica xClinica = new Clinica(exame.IdJuridica.Id);

            string zDia = exame.DataExame.Day.ToString().Trim();
            if (exame.DataExame.Day < 10) zDia = "0" + zDia;

            string zMes = exame.DataExame.Month.ToString().Trim();
            if (exame.DataExame.Month < 10) zMes = "0" + zMes;

            string xPath = "I:\\websites\\Novo_Daiti\\Guias\\guia_" + exame.IdEmpregado.tNO_EMPG.Replace(" ", "-") + "_" + exame.IdExameDicionario.Nome + "_" + exame.DataExame.Year.ToString().Trim() + zMes + zDia + ".pdf";    //System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".pdf";

            FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
            // Initialize the bytes array with the stream length and then fill it with data
            byte[] bytesInStream = new byte[reportStream.Length];
            reportStream.Read(bytesInStream, 0, bytesInStream.Length);
            // Use write method to write to the file specified above
            fileStream.Write(bytesInStream, 0, bytesInStream.Length);

            fileStream.Flush();

            fileStream.Dispose();
            fileStream = null;


            return xPath;
            //System.IO.File.Delete(xPath);
            //esse i:\\temp\\ ele considera do webserver ou da máquina local ??

            //ShowPdfDocument(response, reportStream);
        }


        protected static void ShowPdfDocument(HttpResponse response, MemoryStream reportStream)
        {
            response.Clear();
            response.ContentType = "application/pdf";
            response.AddHeader("content-length", reportStream.Length.ToString());
            response.BinaryWrite(reportStream.ToArray()); ;
            response.Flush();
            reportStream.Close();
            response.End();
        }


        protected static void CreatePDFDocument(ReportClass report, HttpResponse response, Clinico exame)
        {
            CreatePDFDocument(report, response, false, string.Empty);
        }

        protected static void CreatePDFDocument(ReportClass report, HttpResponse response, bool forceDownload, string DownloadfileName)
        {
            report.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, response, forceDownload, DownloadfileName);
        }



    }
}
