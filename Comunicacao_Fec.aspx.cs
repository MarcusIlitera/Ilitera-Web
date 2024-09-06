using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using Ilitera.Sied.Report;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Ilitera.PCMSO.Report;
using System.Text;
using System.Net.Mail;


using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;

using System.Configuration;


namespace Ilitera.Net
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Comunicacao_Fec : System.Web.UI.Page
    {



        protected void Page_Load(object sender, System.EventArgs e)
        {
            // InicializaWebPageObjects();

            string xSolicitacao = "";

            Ilitera.Data.SQLServer.EntitySQLServer.xDB1 = ConfigurationManager.AppSettings["DB1"].ToString();
            Ilitera.Data.SQLServer.EntitySQLServer.xDB2 = ConfigurationManager.AppSettings["DB2"].ToString();
            Ilitera.Data.SQLServer.EntitySQLServer._LocalServer = ConfigurationManager.AppSettings["LocalServer"].ToString();

            
            try
            {
                txt_Status.Text = Request["Status"].ToString();
            }
            catch
            {
                txt_Status.Text = "";
            }

            if (txt_Status.Text.Trim() != "") return;




         
            Page.Response.ContentType = "text/xml";
            // Read XML posted via HTTP
            StreamReader reader = new StreamReader(Page.Request.InputStream);
            String xmlData = reader.ReadToEnd();

            if (xmlData == "")
            {
                xmlData = Session["XML"].ToString();
            }


            if (xmlData == "")
            {
                return;
            }

            XDocument doc = XDocument.Parse(xmlData);

            var points = doc.Descendants("Tipo");



            foreach (XElement current in points)
            {
                xSolicitacao = current.Element("Solicitacao").Value;
            }



            if (xSolicitacao == "Criar_Empresa")
            {
                Executar_Criar_Empresa(xmlData);
            }
            else if (xSolicitacao == "Editar_Empresa")
            {
                Executar_Editar_Empresa(xmlData);
            }
            else if (xSolicitacao == "Inativar_Empresa")
            {
                Executar_Inativar_Empresa(xmlData);
            }
            else if (xSolicitacao == "Inserir_Colaboradores")
            {
                Executar_Inserir_Colaboradores(xmlData);
            }
            else if (xSolicitacao == "Editar_Colaborador")
            {
                Executar_Editar_Colaborador(xmlData);
            }
            else if (xSolicitacao == "Inativar_Colaborador")
            {
                Executar_Inativar_Colaborador(xmlData);
            }
            //else if (xSolicitacao == "Checar_Laudo")
            //{
            //    Executar_Checar_Laudo(xmlData);
            //}
            else if (xSolicitacao == "Solicitar_Laudo")
            {
                Executar_Solicitar_Laudo(xmlData);
            }
            else if (xSolicitacao == "Convocacao_Exame")
            {
                Executar_Convocacao(xmlData);
            }

        }




        private void Executar_Criar_Empresa(string xmlData)
        {


            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

            //int zId = 0;

            string xID = "";
            string xNomeEmpresa = "";
            string xRazaoSocial = "";
            string xCNPJ = "";
            string xCodUsuario = "";
            string xTipoLogradouro = "";
            string xLogradouro = "";
            string xNumero = "";
            string xComplemento = "";
            string xBairro = "";
            string xCEP = "";
            string xMunicipio = "";
            string xUF = "";
            string xIE = "";
            string xCCM = "";
            string xSite = "";
            string xEmail = "";
            string xObservacao = "";
            string xDDDContato = "";
            string xTelefoneContato = "";
            string xNomeContato = "";
            string xDeptoContato = "";
            string xEmailContato = "";
            string xCNAE = "";
            string xOrigem = "";




            string rSelect = "";

            string xRetorno = "";


            try
            {


                XDocument doc = XDocument.Parse(xmlData);


                var points = doc.Descendants("Tipo");

                foreach (XElement current in points)
                {
                    xID = current.Element("ID").Value;
                    xNomeEmpresa = current.Element("NomeEmpresa").Value;
                    xRazaoSocial = current.Element("RazaoSocial").Value;
                    xCNPJ = current.Element("CNPJ").Value;
                    xCodUsuario = current.Element("CodUsuario").Value;
                    xTipoLogradouro = current.Element("TipoLogradouro").Value;
                    xLogradouro = current.Element("Logradouro").Value;
                    xNumero = current.Element("Numero").Value;
                    xComplemento = current.Element("Complemento").Value;
                    xBairro = current.Element("Bairro").Value;
                    xCEP = current.Element("CEP").Value;
                    xMunicipio = current.Element("Municipio").Value;
                    xUF = current.Element("UF").Value;
                    xIE = current.Element("IE").Value;
                    xCCM = current.Element("CCM").Value;
                    xSite = current.Element("Site").Value;
                    xEmail = current.Element("eMail").Value;
                    xObservacao = current.Element("Observacao").Value;
                    xDDDContato = current.Element("DDDContato").Value;
                    xTelefoneContato = current.Element("TelefoneContato").Value;
                    xNomeContato = current.Element("NomeContato").Value;
                    xDeptoContato = current.Element("DeptoContato").Value;
                    xEmailContato = current.Element("eMailContato").Value;
                    xCNAE = current.Element("CNAE").Value;
                    xOrigem = current.Element("Origem").Value;

                }

                xRetorno = "";

                //validar campos
                if (xID.Length < 20 || xID.Length > 22)
                {
                    txt_Status.Text = " Campo ID inválido | ";
                    xRetorno = " Campo ID Inválido |";
                }
                if (xNomeEmpresa.Length < 1 || xNomeEmpresa.Length > 80)
                {
                    txt_Status.Text = txt_Status.Text + " Campo NomeEmpresa inválido |";
                    xRetorno = xRetorno + " Campo NomeEmpresa Inválido |";
                }
                if (xRazaoSocial.Length < 1 || xRazaoSocial.Length > 80)
                {
                    txt_Status.Text = txt_Status.Text + " Campo RazaoSocial inválido |";
                    xRetorno = xRetorno + " Campo RazaoSocial Inválido |";
                }
                if (SomenteNumeros(xCNPJ) == false)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CNPJ deve ser somente números |";
                    xRetorno = xRetorno + " Campo CNPJ deve ser somente números |";
                }
                if (xCNPJ.Length < 10 || xCNPJ.Length > 14)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CNPJ inválido |";
                    xRetorno = xRetorno + " Campo CNPJ Inválido |";
                }
                if (xTipoLogradouro.Length > 15)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Tipo Logradouro inválido |";
                    xRetorno = xRetorno + " Campo Tipo Logradouro Inválido |";
                }
                if (xLogradouro.Length < 1 || xLogradouro.Length > 150)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Logradouro inválido |";
                    xRetorno = xRetorno + " Campo Logradouro Inválido |";
                }
                if (xNumero.Length < 1 || xNumero.Length > 30)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Número inválido |";
                    xRetorno = xRetorno + " Campo Número Inválido |";
                }
                if (xComplemento.Length > 200)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Complemento inválido |";
                    xRetorno = xRetorno + " Campo Complemento Inválido |";
                }
                if (xBairro.Length < 1 || xBairro.Length > 80)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Bairro inválido |";
                    xRetorno = xRetorno + " Campo Bairro Inválido |";
                }
                if (xCEP.Length < 1 || xCEP.Length > 9)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CEP inválido |";
                    xRetorno = xRetorno + " Campo CEP Inválido |";
                }
                if (xMunicipio.Length < 1 || xMunicipio.Length > 80)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Municipio inválido |";
                    xRetorno = xRetorno + " Campo Municipio Inválido |";
                }
                if (xUF.Length < 1 || xUF.Length > 2)
                {
                    txt_Status.Text = txt_Status.Text + " Campo UF inválido |";
                    xRetorno = xRetorno + " Campo UF Inválido |";
                }
                if (xIE.Length > 50)
                {
                    txt_Status.Text = txt_Status.Text + " Campo IE inválido |";
                    xRetorno = xRetorno + " Campo IE Inválido |";
                }
                if (xCCM.Length > 50)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CCM inválido |";
                    xRetorno = xRetorno + " Campo CCM Inválido |";
                }
                if (xSite.Length > 100)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Site inválido |";
                    xRetorno = xRetorno + " Campo Site Inválido |";
                }
                if (xEmail.Length > 100)
                {
                    txt_Status.Text = txt_Status.Text + " Campo eMail Principal inválido |";
                    xRetorno = xRetorno + " Campo eMail Principal Inválido |";
                }
                if (xObservacao.Length > 500)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Observação inválido |";
                    xRetorno = xRetorno + " Campo Observação Inválido |";
                }
                if ( xDDDContato.Length > 10)
                {
                    txt_Status.Text = txt_Status.Text + " Campo DDD Contato inválido |";
                    xRetorno = xRetorno + " Campo DDD Contato Inválido |";
                }
                if ( xTelefoneContato.Length > 20)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Telefone Contato inválido |";
                    xRetorno = xRetorno + " Campo Telefone Contato Inválido |";
                }
                if ( xDeptoContato.Length > 50)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Depto Contato inválido |";
                    xRetorno = xRetorno + " Campo Depto Contato Inválido |";
                }
                if ( xEmailContato.Length > 100)
                {
                    txt_Status.Text = txt_Status.Text + " Campo eMail Contato inválido |";
                    xRetorno = xRetorno + " Campo eMail Contato Inválido |";
                }
                if ( xNomeContato.Length > 100)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Nome Contato inválido |";
                    xRetorno = xRetorno + " Campo Nome Contato Inválido |";
                }
                if (xCNAE.Length > 10)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CNAE inválido |";
                    xRetorno = xRetorno + " Campo CNAE Inválido |";
                }


                if (xRetorno != "")
                    xRetorno = "50 ( Validação de campos ): " + xRetorno;






                if (txt_Status.Text == "")
                {

                    rSelect = " NomeAbreviado = '" + xNomeEmpresa + "' and dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

                    //pegar Id Empresa                    
                    rEmpresa.Find(rSelect);

                    //se não achar empresa,  emitir retorno avisando
                    if (rEmpresa.Id != 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: Empresa já existe ( " + xNomeEmpresa + "  )" + System.Environment.NewLine;
                        xRetorno = "02 (Empresa já existe(" + xNomeEmpresa + ")";
                    }

                }




                if (txt_Status.Text == "")
                {

                    rEmpresa = new Ilitera.Common.Pessoa();

                    rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

                    //pegar Id Empresa                    
                    rEmpresa.Find(rSelect);

                    //se não achar empresa,  emitir retorno avisando
                    if (rEmpresa.Id != 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: CNPJ já existe ( " + xCNPJ + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (CNPJ já existe(" + xCNPJ + ")";
                    }

                }



                GrupoEmpresa xGrupoEmpresa = new GrupoEmpresa();

                if (txt_Status.Text == "")
                {
                    xGrupoEmpresa.Descricao = xNomeEmpresa;
                    xGrupoEmpresa.Save();

                    if (xGrupoEmpresa.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: salva do grupo ( " + xNomeEmpresa + "  )" + System.Environment.NewLine;
                        xRetorno = "04 (Erro na salva do grupo(" + xNomeEmpresa + ")";
                    }
                }




                if (txt_Status.Text == "")
                {

                    CNAE zCNAE = new CNAE();
                    //zCNAE.Find(" Codigo = '0111-3/01' and IndCNAE = '1' ");
                    if( xCNAE != "")
                        zCNAE.Find(" Codigo = '" + xCNAE + "' and IndCNAE = '1' ");
                    else
                        zCNAE.Find(" Codigo = '0111-3/01' and IndCNAE = '1' ");

                    if ( zCNAE.Id == 0)
                        zCNAE.Find(" Codigo = '0111-3/01' and IndCNAE = '1' ");



                    //crio o registro em opsa->Pessoa e pego o ID

                    rEmpresa = new Ilitera.Common.Pessoa();

                    rEmpresa.NomeAbreviado = xNomeEmpresa;
                    rEmpresa.NomeCompleto = xRazaoSocial;
                    rEmpresa.NomeCodigo = xCNPJ;
                    rEmpresa.IndTipoPessoa = 1;
                    rEmpresa.IsInativo = false;
                    rEmpresa.Site = xSite;
                    rEmpresa.Email = xEmail;
                    rEmpresa.Origem = xOrigem;




                    rEmpresa.Save();


                    if (rEmpresa.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: salva da empresa(1) ( " + xNomeEmpresa + "  )" + System.Environment.NewLine;
                        xRetorno = "05 (Erro na salva da empresa (" + xNomeEmpresa + ")";
                    }


                    //como a classe Cliente está dando erros ao tentar pegar o objeto de IdJuridicaPapel, IdCNAE e IdGrupoEmpresa, criei um método de inserção

                    Ilitera.Data.Comunicacao xCriarEmpresa = new Ilitera.Data.Comunicacao();
                    xCriarEmpresa.Criar_Empresa(rEmpresa.Id, xIE, xCCM, xObservacao, zCNAE.Id, xGrupoEmpresa.Id, 1, xNomeEmpresa);



                    rSelect = " NomeAbreviado = '" + xNomeEmpresa + "' and dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

                    //pegar Id Empresa                    
                    rEmpresa.Find(rSelect);

                    //se não achar empresa,  emitir retorno avisando
                    if (rEmpresa.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: salva de empresa(2) ( " + xNomeEmpresa + "  )" + System.Environment.NewLine;
                        xRetorno = "06 (Erro na salva da empresa(2) (" + xNomeEmpresa + ")";
                    }



                }






                TipoLogradouro xTipoEndereco = new TipoLogradouro();
                
                if (txt_Status.Text == "")
                {

                    if (xTipoLogradouro != "")
                    {

                        xTipoEndereco.Find(" NomeAbreviado = '" + xTipoLogradouro + "'  ");

                        if (xTipoEndereco.Id == 0)
                        {
                            txt_Status.Text = txt_Status.Text + "Erro: tipo de logradouro não localizado ( " + xTipoLogradouro + "  )" + System.Environment.NewLine;
                            xRetorno = "07 (Tipo de Logradouro não localizado (" + xTipoLogradouro + ")";
                            rEmpresa.Delete();
                        }

                    }
                    else
                    {
                        xTipoEndereco.Find(0);
                    }

                }




                Municipio xCidade = new Municipio();

                if (txt_Status.Text == "")
                {

                    xCidade.Find(" NomeAbreviado = '" + xMunicipio + "' or NomeCompleto = '" + xMunicipio + "' ");

                    if (xCidade.Id == 0)
                    {

                        UnidadeFederativa xEstado = new UnidadeFederativa();
                        xEstado.Find("NomeAbreviado = '" + xUF + "' ");

                        if (xEstado.Id == 0)
                        {
                            xEstado = new UnidadeFederativa();
                            xEstado.Find(" NomeAbreviado = 'SP' ");
                        }


                        xCidade = new Municipio();
                        xCidade.NomeAbreviado = xMunicipio;
                        xCidade.NomeCompleto = xMunicipio;
                        xCidade.IndTipoLocalizacaoGeografica = (int)TipoLocalizacaoGeografica.Municipio;
                        xCidade.IdUnidadeFederativa = xEstado;
                        xCidade.Save();

                        if (xCidade.Id == 0)
                        {
                            txt_Status.Text = txt_Status.Text + "Erro: Municipio ( " + xMunicipio + "  )" + System.Environment.NewLine;
                            xRetorno = "08 (Erro na localização de município (" + xMunicipio + ")";
                            rEmpresa.Delete();
                        }


                    }

                }


                if (txt_Status.Text == "")
                {

                    Endereco xEndereco = new Endereco();

                    
                    xEndereco.IdTipoLogradouro = xTipoEndereco;


                    xEndereco.Logradouro = xLogradouro;
                    xEndereco.Numero = xNumero;
                    xEndereco.IdPessoa = rEmpresa;
                    xEndereco.Cep = xCEP;
                    xEndereco.Bairro = xBairro;
                    xEndereco.Municipio = xMunicipio;
                    xEndereco.IdMunicipio = xCidade;
                    xEndereco.Uf = xUF;
                    xEndereco.IndTipoEndereco = (int)TipoEndereco.Default;
                    xEndereco.Complemento = xComplemento;

                    xEndereco.IdMunicipio.Find();

                    xEndereco.Save();


                    if (xEndereco.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: salva de logradouro ( " + xLogradouro + "  )" + System.Environment.NewLine;
                        xRetorno = "09 (Erro na salva de logradouro (" + xLogradouro + ")";
                        rEmpresa.Delete();
                    }

                }


                if (txt_Status.Text == "")
                {
                    ContatoTelefonico xTelefone = new ContatoTelefonico();

                    xTelefone.DDD = xDDDContato;
                    xTelefone.Departamento = xDeptoContato;
                    xTelefone.IdPessoa = rEmpresa;
                    xTelefone.IndTipoTelefone = 0;
                    xTelefone.Numero = xTelefoneContato;
                    xTelefone.Nome = xNomeContato;
                    xTelefone.eMail = xEmailContato;

                    xTelefone.Save();

                    if (xTelefone.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: salva de telefone ( " + xTelefoneContato + "  )" + System.Environment.NewLine;
                        xRetorno = "10 (Erro na salva de telefone (" + xTelefone + ")";
                        rEmpresa.Delete();
                    }


                }


                xRetorno = "01 Processamento concluído sem erros";


            }
            catch (Exception ex)
            {
                txt_Status.Text = txt_Status.Text + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

                Session["zErro"] = txt_Status.Text;
                Response.Redirect("~/Comunicacao2.aspx");


            }
            finally
            {

                if (txt_Status.Text != "")
                {
                    Session["zErro"] = txt_Status.Text;
                    Response.Redirect("~/Comunicacao2.aspx");
                }
                else
                {


                    Session["zErro"] = txt_Status.Text + " " + "Processamento Finalizado";
                    Response.Redirect("~/Comunicacao2.aspx");
                }
            }


        }






        private void Executar_Editar_Empresa(string xmlData)
        {


            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

            //int zId = 0;

            string xID = "";
            string xCNPJ_Original = "";
            string xNomeEmpresa = "";
            string xRazaoSocial = "";
            string xCNPJ = "";
            string xCodUsuario = "";
            string xTipoLogradouro = "";
            string xLogradouro = "";
            string xNumero = "";
            string xComplemento = "";
            string xBairro = "";
            string xCEP = "";
            string xMunicipio = "";
            string xUF = "";
            string xIE = "";
            string xCCM = "";
            string xSite = "";
            string xEmail = "";
            string xObservacao = "";
            string xDDDContato = "";
            string xTelefoneContato = "";
            string xNomeContato = "";
            string xDeptoContato = "";
            string xEmailContato = "";
            string xCNAE = "";




            string rSelect = "";

            string xRetorno = "";


            try
            {


                XDocument doc = XDocument.Parse(xmlData);


                var points = doc.Descendants("Tipo");

                foreach (XElement current in points)
                {
                    xID = current.Element("ID").Value;
                    xNomeEmpresa = current.Element("NomeEmpresa").Value;
                    xRazaoSocial = current.Element("RazaoSocial").Value;
                    xCNPJ = current.Element("CNPJ").Value;
                    xCNPJ_Original = current.Element("CNPJ_Original").Value;
                    xCodUsuario = current.Element("CodUsuario").Value;
                    xTipoLogradouro = current.Element("TipoLogradouro").Value;
                    xLogradouro = current.Element("Logradouro").Value;
                    xNumero = current.Element("Numero").Value;
                    xComplemento = current.Element("Complemento").Value;
                    xBairro = current.Element("Bairro").Value;
                    xCEP = current.Element("CEP").Value;
                    xMunicipio = current.Element("Municipio").Value;
                    xUF = current.Element("UF").Value;
                    xIE = current.Element("IE").Value;
                    xCCM = current.Element("CCM").Value;
                    xSite = current.Element("Site").Value;
                    xEmail = current.Element("eMail").Value;
                    xObservacao = current.Element("Observacao").Value;
                    xDDDContato = current.Element("DDDContato").Value;
                    xTelefoneContato = current.Element("TelefoneContato").Value;
                    xNomeContato = current.Element("NomeContato").Value;
                    xDeptoContato = current.Element("DeptoContato").Value;
                    xEmailContato = current.Element("eMailContato").Value;
                    xCNAE = current.Element("CNAE").Value;

                }

                xRetorno = "";

                //validar campos
                if (xID.Length < 20 || xID.Length > 22)
                {
                    txt_Status.Text = " Campo ID inválido | ";
                    xRetorno = " Campo ID Inválido |";
                }
                if ( xNomeEmpresa.Length > 80)
                {
                    txt_Status.Text = txt_Status.Text + " Campo NomeEmpresa inválido |";
                    xRetorno = xRetorno + " Campo NomeEmpresa Inválido |";
                }
                if ( xRazaoSocial.Length > 80)
                {
                    txt_Status.Text = txt_Status.Text + " Campo RazaoSocial inválido |";
                    xRetorno = xRetorno + " Campo RazaoSocial Inválido |";
                }
                if (SomenteNumeros(xCNPJ_Original) == false)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CNPJ Original deve ser somente números |";
                    xRetorno = xRetorno + " Campo CNPJ Original deve ser somente números |";
                }
                if (xCNPJ_Original.Length < 10 || xCNPJ_Original.Length > 14)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CNPJ Original inválido |";
                    xRetorno = xRetorno + " Campo CNPJ Original Inválido |";
                }
                if ( xCNPJ.Length > 14)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CNPJ inválido |";
                    xRetorno = xRetorno + " Campo CNPJ Inválido |";
                }
                if (xTipoLogradouro.Length > 15)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Tipo Logradouro inválido |";
                    xRetorno = xRetorno + " Campo Tipo Logradouro Inválido |";
                }
                if (xLogradouro.Length > 150)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Logradouro inválido |";
                    xRetorno = xRetorno + " Campo Logradouro Inválido |";
                }
                if (xNumero.Length > 30)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Número inválido |";
                    xRetorno = xRetorno + " Campo Número Inválido |";
                }
                if (xComplemento.Length > 200)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Complemento inválido |";
                    xRetorno = xRetorno + " Campo Complemento Inválido |";
                }
                if (xBairro.Length > 80)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Bairro inválido |";
                    xRetorno = xRetorno + " Campo Bairro Inválido |";
                }
                if (xCEP.Length > 9)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CEP inválido |";
                    xRetorno = xRetorno + " Campo CEP Inválido |";
                }
                if ( xMunicipio.Length > 80)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Municipio inválido |";
                    xRetorno = xRetorno + " Campo Municipio Inválido |";
                }
                if (xUF.Length > 2)
                {
                    txt_Status.Text = txt_Status.Text + " Campo UF inválido |";
                    xRetorno = xRetorno + " Campo UF Inválido |";
                }
                if (xIE.Length > 50)
                {
                    txt_Status.Text = txt_Status.Text + " Campo IE inválido |";
                    xRetorno = xRetorno + " Campo IE Inválido |";
                }
                if (xCCM.Length > 50)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CCM inválido |";
                    xRetorno = xRetorno + " Campo CCM Inválido |";
                }
                if (xSite.Length > 100)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Site inválido |";
                    xRetorno = xRetorno + " Campo Site Inválido |";
                }
                if (xEmail.Length > 100)
                {
                    txt_Status.Text = txt_Status.Text + " Campo eMail Principal inválido |";
                    xRetorno = xRetorno + " Campo eMail Principal Inválido |";
                }
                if (xObservacao.Length > 500)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Observação inválido |";
                    xRetorno = xRetorno + " Campo Observação Inválido |";
                }
                if (xDDDContato.Length > 10)
                {
                    txt_Status.Text = txt_Status.Text + " Campo DDD Contato inválido |";
                    xRetorno = xRetorno + " Campo DDD Contato Inválido |";
                }
                if (xTelefoneContato.Length > 20)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Telefone Contato inválido |";
                    xRetorno = xRetorno + " Campo Telefone Contato Inválido |";
                }
                if (xDeptoContato.Length > 50)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Depto Contato inválido |";
                    xRetorno = xRetorno + " Campo Depto Contato Inválido |";
                }
                if (xEmailContato.Length > 100)
                {
                    txt_Status.Text = txt_Status.Text + " Campo eMail Contato inválido |";
                    xRetorno = xRetorno + " Campo eMail Contato Inválido |";
                }
                if (xNomeContato.Length > 100)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Nome Contato inválido |";
                    xRetorno = xRetorno + " Campo Nome Contato Inválido |";
                }
                if (xCNAE.Length > 10)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CNAE inválido |";
                    xRetorno = xRetorno + " Campo CNAE Inválido |";
                }


                if (xRetorno != "")
                    xRetorno = "50 ( Validação de campos ): " + xRetorno;






                if (txt_Status.Text == "")
                {

                    rEmpresa = new Ilitera.Common.Pessoa();

                    rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ_Original + "' ";

                    //pegar Id Empresa                    
                    rEmpresa.Find(rSelect);

                    //se não achar empresa,  emitir retorno avisando
                    if (rEmpresa.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: CNPJ Original não localizado ( " + xCNPJ_Original + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (CNPJ Original não localizado (" + xCNPJ_Original + ")";
                    }

                }




                if (txt_Status.Text == "")
                {
                    if ( xCNPJ!= "" && xCNPJ != xCNPJ_Original)
                    {
                        Ilitera.Common.Pessoa rEmpresa2 = new Ilitera.Common.Pessoa();

                        rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

                        //pegar Id Empresa                    
                        rEmpresa2.Find(rSelect);

                        //se não achar empresa,  emitir retorno avisando
                        if (rEmpresa2.Id != 0)
                        {
                            txt_Status.Text = txt_Status.Text + "Erro: CNPJ a ser alterado já existe no cadastro ( " + xCNPJ + "  )" + System.Environment.NewLine;
                            xRetorno = "03 (CNPJ a ser alterado já existe no cadastro (" + xCNPJ + ")";
                        }

                    }

                }


                if (txt_Status.Text == "")
                {

                    int zCNAEId = 0;

                    if (xCNAE != "")
                    {
                        CNAE zCNAE = new CNAE();
                        zCNAE.Find(" Codigo = '0111-3/01' and IndCNAE = '1' ");
                        zCNAEId = zCNAE.Id;
                    }

                    //crio o registro em opsa->Pessoa e pego o ID                                        
                    if (xNomeEmpresa != "")
                        rEmpresa.NomeAbreviado = xNomeEmpresa;

                    if (xRazaoSocial != "")
                        rEmpresa.NomeCompleto = xRazaoSocial;

                    if (xCNPJ != "")
                        rEmpresa.NomeCodigo = xCNPJ;

                    if (xSite != "")
                        rEmpresa.Site = xSite;

                    if (xEmail != "")
                        rEmpresa.Email = xEmail;


                    rEmpresa.Save();



                    if (xIE != "" || xCCM != "" || xObservacao != "" || zCNAEId != 0)
                    {
                        Ilitera.Data.Comunicacao xCriarEmpresa = new Ilitera.Data.Comunicacao();
                        //xCriarEmpresa.Criar_Empresa(rEmpresa.Id, xIE, xCCM, xObservacao, zCNAE.Id, xGrupoEmpresa.Id, 1, xNomeEmpresa);
                        xCriarEmpresa.Alterar_Empresa(rEmpresa.Id, xIE, xCCM, xObservacao, zCNAEId);
                    }


                }



                TipoLogradouro xTipoEndereco = new TipoLogradouro();

                if (xTipoLogradouro != "")
                {
                    if (txt_Status.Text == "")
                    {
                        xTipoEndereco.Find(" NomeAbreviado = '" + xTipoLogradouro + "'  ");

                        if (xTipoEndereco.Id == 0)
                        {
                            txt_Status.Text = txt_Status.Text + "Erro: tipo de logradouro não localizado ( " + xTipoLogradouro + "  )" + System.Environment.NewLine;
                            xRetorno = "07 (Tipo de Logradouro não localizado (" + xTipoLogradouro + ")";
                        }

                    }
                }



                Municipio xCidade = new Municipio();

                if (xMunicipio != "")
                {

                    if (txt_Status.Text == "")
                    {

                        xCidade.Find(" NomeAbreviado = '" + xMunicipio + "' or NomeCompleto = '" + xMunicipio + "' ");

                        if (xCidade.Id == 0)
                        {
                            txt_Status.Text = txt_Status.Text + "Erro: Municipio ( " + xMunicipio + "  )" + System.Environment.NewLine;
                            xRetorno = "08 (Erro na localização de município (" + xMunicipio + ")";
                        }

                    }
                }




                if (txt_Status.Text == "")
                {

                    Endereco xEndereco = new Endereco();
                    xEndereco.Find( " IdPessoa = " + rEmpresa.Id.ToString());

                    if (xEndereco.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: endereço de empresa não localizado ( " + xLogradouro + "  )" + System.Environment.NewLine;
                        xRetorno = "09 (Endereço da empresa não localizado (" + xLogradouro + ")";
                    }

                    if (txt_Status.Text == "")
                    {
                        if (xTipoLogradouro != "")
                            xEndereco.IdTipoLogradouro = xTipoEndereco;

                        if (xLogradouro != "")
                            xEndereco.Logradouro = xLogradouro;

                        if (xNumero != "")
                            xEndereco.Numero = xNumero;

                        if (xCEP != "")
                            xEndereco.Cep = xCEP;

                        if (xBairro != "")
                            xEndereco.Bairro = xBairro;

                        if (xMunicipio != "")
                        {
                            xEndereco.Municipio = xMunicipio;
                            xEndereco.IdMunicipio = xCidade;
                            xEndereco.IdMunicipio.Find();
                        }

                        if (xUF != "")
                            xEndereco.Uf = xUF;

                        if (xComplemento != "")
                            xEndereco.Complemento = xComplemento;


                        xEndereco.Save();

                    }


                }



                if (txt_Status.Text == "")
                {
                    ContatoTelefonico xTelefone = new ContatoTelefonico();
                    xTelefone.Find( " IdPessoa = " + rEmpresa.Id.ToString());

                    if (xTelefone.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: salva de telefone ( " + xTelefoneContato + "  )" + System.Environment.NewLine;
                        xRetorno = "10 (Erro na salva de telefone (" + xTelefone + ")";
                    }


                    if (xDDDContato != "")
                        xTelefone.DDD = xDDDContato;

                    if (xDeptoContato != "")
                        xTelefone.Departamento = xDeptoContato;

                    if (xTelefoneContato != "")
                        xTelefone.Numero = xTelefoneContato;

                    if (xNomeContato != "")
                        xTelefone.Nome = xNomeContato;

                    if (xEmailContato != "")
                        xTelefone.eMail = xEmailContato;

                    xTelefone.Save();



                }


                xRetorno = "01 Processamento concluído sem erros";


            }
            catch (Exception ex)
            {
                txt_Status.Text = txt_Status.Text + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

                Session["zErro"] = txt_Status.Text;
                Response.Redirect("~/Comunicacao2.aspx");


            }
            finally
            {

                if (txt_Status.Text != "")
                {
                    Session["zErro"] = txt_Status.Text;
                    Response.Redirect("~/Comunicacao2.aspx");
                }
                else
                {


                    Session["zErro"] = txt_Status.Text + " " + "Processamento Finalizado";
                    Response.Redirect("~/Comunicacao2.aspx");
                }
            }


        }



        private void Executar_Inativar_Empresa(string xmlData)
        {


            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
        
            //int zId = 0;

            string xID = "";
            string xCNPJ_Original = "";
      




            string rSelect = "";

            string xRetorno = "";


            try
            {


                XDocument doc = XDocument.Parse(xmlData);


                var points = doc.Descendants("Tipo");

                foreach (XElement current in points)
                {
                    xID = current.Element("ID").Value;                    
                    xCNPJ_Original = current.Element("CNPJ_Original").Value;
                }

                xRetorno = "";

                //validar campos
                if (xID.Length < 20 || xID.Length > 22)
                {
                    txt_Status.Text = " Campo ID inválido | ";
                    xRetorno = " Campo ID Inválido |";
                }              
                if (SomenteNumeros(xCNPJ_Original) == false)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CNPJ Original deve ser somente números |";
                    xRetorno = xRetorno + " Campo CNPJ Original deve ser somente números |";
                }
                if (xCNPJ_Original.Length < 10 || xCNPJ_Original.Length > 14)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CNPJ Original inválido |";
                    xRetorno = xRetorno + " Campo CNPJ Original Inválido |";
                }
              

                if (xRetorno != "")
                    xRetorno = "50 ( Validação de campos ): " + xRetorno;






                if (txt_Status.Text == "")
                {

                    rEmpresa = new Ilitera.Common.Pessoa();

                    rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ_Original + "' ";

                    //pegar Id Empresa                    
                    rEmpresa.Find(rSelect);

                    //se não achar empresa,  emitir retorno avisando
                    if (rEmpresa.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: CNPJ Original não localizado ( " + xCNPJ_Original + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (CNPJ Original não localizado (" + xCNPJ_Original + ")";
                    }

                }




                if (txt_Status.Text == "")
                {

                    rEmpresa.IsInativo = true;
                    rEmpresa.Save();


                }



                xRetorno = "01 Processamento concluído sem erros";


            }
            catch (Exception ex)
            {
                txt_Status.Text = txt_Status.Text + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

                Session["zErro"] = txt_Status.Text;
                Response.Redirect("~/Comunicacao2.aspx");


            }
            finally
            {

                if (txt_Status.Text != "")
                {
                    Session["zErro"] = txt_Status.Text;
                    Response.Redirect("~/Comunicacao2.aspx");
                }
                else
                {


                    Session["zErro"] = txt_Status.Text + " " + "Processamento Finalizado";
                    Response.Redirect("~/Comunicacao2.aspx");
                }
            }


        }







        private void Executar_Inserir_Colaboradores(string xmlData)
        {


            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

            //int zId = 0;

            string xID = "";
            string xCNPJ = "";
            string xNome = "";
            string xMatricula = "";
            string xNIT = "";
            string xCTPS_Numero = "";
            string xCTPS_Serie = "";
            string xCTPS_UF = "";
            string xRG = "";
            string xCPF = "";
            string xSexo = "";
            string xDataNascimento = "";
            string xDataAdmissao = "";
            string xFuncao = "";
            string xCargo = "";
            string xSetor = "";
            string xCBO = "";
            string xeMail = "";
            string xeMail_Responsavel = "";
            string xDescricao_Funcao = "";

            string rSelect = "";

            string xRetorno = "";


            try
            {


                XDocument doc = XDocument.Parse(xmlData);


                //preciso testar loop para vários registros no mesmo XML

                var points = doc.Descendants("Colaborador");

                foreach (XElement current in points)
                {
                    xID = current.Element("ID").Value;
                    xCNPJ = current.Element("CNPJ").Value;

                    xNome = current.Element("Nome").Value;
                    xMatricula = current.Element("Matricula").Value;
                    xNIT = current.Element("NIT").Value;
                    xCTPS_Numero = current.Element("CTPS_Numero").Value;
                    xCTPS_Serie = current.Element("CTPS_Serie").Value;
                    xCTPS_UF = current.Element("CTPS_UF").Value;
                    xRG = current.Element("RG").Value;
                    xCPF = current.Element("CPF").Value;
                    xSexo = current.Element("Sexo").Value;
                    xDataNascimento = current.Element("DataNascimento").Value;
                    xDataAdmissao = current.Element("DataAdmissao").Value;
                    xFuncao = current.Element("Funcao").Value;
                    xCargo = current.Element("Cargo").Value;
                    xSetor = current.Element("Setor").Value;
                    xCBO = current.Element("CBO").Value;
                    xeMail = current.Element("email").Value;
                    xeMail_Responsavel = current.Element("email_Responsavel").Value;
                    xDescricao_Funcao = current.Element("Descricao_Funcao").Value;

                    //email e email resp

                    xRetorno = "";

                    //validar campos
                    if (xID.Length < 20 || xID.Length > 22)
                    {
                        txt_Status.Text = " Campo ID inválido | ";
                        xRetorno = " Campo ID Inválido |";
                    }
                    if (SomenteNumeros(xCNPJ) == false)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo CNPJ deve ser somente números |";
                        xRetorno = xRetorno + " Campo CNPJ deve ser somente números |";
                    }
                    if (xCNPJ.Length < 10 || xCNPJ.Length > 14)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo CNPJ inválido |";
                        xRetorno = xRetorno + " Campo CNPJ Inválido |";
                    }


                    //confirmar tamanhos maximos
                    xNIT = xNIT.Trim();
                    if (xNIT != "")
                    {
                        if (SomenteNumeros(xNIT) == false)
                        {
                            txt_Status.Text = txt_Status.Text + " Campo NIT deve ser somente números |";
                            xRetorno = xRetorno + " Campo NIT deve ser somente números |";
                        }
                        if (xNIT.Length < 10 || xNIT.Length > 14)
                        {
                            txt_Status.Text = txt_Status.Text + " Campo NIT inválido |";
                            xRetorno = xRetorno + " Campo NIT Inválido |";
                        }
                    }
                    //else
                    //{
                    //    txt_Status.Text = txt_Status.Text + " Campo NIT inválido |";
                    //    xRetorno = xRetorno + " Campo NIT Inválido |";
                    //}
                    if ( xMatricula.Length > 10)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo Matricula inválido |";
                        xRetorno = xRetorno + " Campo Matricula Inválido |";
                    }
                    if ( xCTPS_Numero.Length > 10)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo CTPS Numero inválido |";
                        xRetorno = xRetorno + " Campo CTPS Numero Inválido |";
                    }
                    if ( xCTPS_Serie.Length > 8)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo CTPS Serie inválido |";
                        xRetorno = xRetorno + " Campo CTPS Serie Inválido |";
                    }
                    if ( xCTPS_UF.Length > 2)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo CTPS UF inválido |";
                        xRetorno = xRetorno + " Campo CTPS UF Inválido |";
                    }
                    if (xRG.Length < 6 || xRG.Length > 14)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo RG inválido |";
                        xRetorno = xRetorno + " Campo RG Inválido |";
                    }
                    if (SomenteNumeros(xCPF) == false)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo CPF deve ser somente números |";
                        xRetorno = xRetorno + " Campo CPF deve ser somente números |";
                    }
                    if (xCPF.Length < 6 || xCPF.Length > 11)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo CPF inválido |";
                        xRetorno = xRetorno + " Campo CPF Inválido |";
                    }
                    if (xSexo.Length > 1)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo Sexo inválido |";
                        xRetorno = xRetorno + " Campo Sexo Inválido |";
                    }
                    if (xDataNascimento.Length != 10)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo Data Nascimento inválido |";
                        xRetorno = xRetorno + " Campo Data Nascimento Inválido |";
                    }
                    if (Validar_Data(xDataNascimento) == false)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo Data Nascimento inválido |";
                        xRetorno = xRetorno + " Campo Data Nascimento Inválido |";
                    }
                    if (xDataAdmissao.Length != 10)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo Data Admissao inválido |";
                        xRetorno = xRetorno + " Campo Data Admissao Inválido |";
                    }
                    if (Validar_Data(xDataAdmissao) == false)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo Data Admissao inválido |";
                        xRetorno = xRetorno + " Campo Data Admissao Inválido |";
                    }
                    if (xFuncao.Length < 1 || xFuncao.Length > 100)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo Funcao inválido |";
                        xRetorno = xRetorno + " Campo Funcao Inválido |";
                    }
                    if (xSetor.Length < 1 || xSetor.Length > 50)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo Setor inválido |";
                        xRetorno = xRetorno + " Campo Setor Inválido |";
                    }
                    if (xCargo.Length > 100)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo Cargo inválido |";
                        xRetorno = xRetorno + " Campo Cargo Inválido |";
                    }
                    if (xCBO.Length > 10)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo CBO inválido |";
                        xRetorno = xRetorno + " Campo CBO Inválido |";
                    }
                    if ( xeMail.Length > 60)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo e-mail inválido |";
                        xRetorno = xRetorno + " Campo e-mail Inválido |";
                    }
                    if ( xeMail_Responsavel.Length > 60)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo e-mail Responsavel inválido |";
                        xRetorno = xRetorno + " Campo e-mail Responsavel Inválido |";
                    }
                    if ( xDescricao_Funcao.Length > 1000)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo Descrição Função inválido |";
                        xRetorno = xRetorno + " Campo Descrição Função Inválido |";
                    }


                    if (xRetorno != "")
                        xRetorno = "50 ( Validação de campos ): " + xRetorno;






                    if (txt_Status.Text == "")
                    {

                        rEmpresa = new Ilitera.Common.Pessoa();

                        rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

                        //pegar Id Empresa                    
                        rEmpresa.Find(rSelect);

                        //se não achar empresa,  emitir retorno avisando
                        if (rEmpresa.Id == 0)
                        {
                            txt_Status.Text = txt_Status.Text + "Erro: Empresa não localizada ( " + xCNPJ + "  )" + System.Environment.NewLine;
                            xRetorno = "03 (Empresa não localizada (" + xCNPJ + ")";
                        }

                    }



                    string xCPF_Completo = "";
                    xCPF = xCPF.Trim();

                    if (xCPF.Length < 11)
                        xCPF_Completo = new string('0', 11 - xCPF.Length) + xCPF;
                    else
                        xCPF_Completo = xCPF;


                    if (txt_Status.Text == "")
                    {
                        StringBuilder cpf = new StringBuilder();
                        cpf.Append(xCPF_Completo)
                            .Replace(".", string.Empty)
                            .Replace("-", string.Empty)
                            .Replace(" ", string.Empty);

                        if (!Pessoa.VeriricaCPF(cpf.ToString()))
                        {
                            txt_Status.Text = txt_Status.Text + "Erro: CPF Inválido ( " + xCPF + "  )" + System.Environment.NewLine;
                            xRetorno = "15 (CPF Inválido (" + xCPF + ")";
                        }
                    }




                    if ( txt_Status.Text == "")
                    {



                        Empregado rColaborador = new Empregado();
                        rColaborador.Find(" ( tNo_CPF = '" + xCPF + "' or  tNo_CPF = '" + xCPF_Completo + "' )  and nId_Empr = " + rEmpresa.Id.ToString());

                        if ( rColaborador.Id != 0)
                        {
                            txt_Status.Text = txt_Status.Text + "Erro: Colaborador já cadastrado nesta empresa ( " + xCPF + "  )" + System.Environment.NewLine;
                            xRetorno = "03 (Colaborador já cadastrado nesta empresa (" + xCPF + ")";
                        }


                    }


                    Cliente rCliente = new Cliente();

                    if (txt_Status.Text == "")
                    {
                        
                        rCliente.Find(" IdCliente = " + rEmpresa.Id.ToString());

                        if (rCliente.Id == 0)
                        {
                            txt_Status.Text = txt_Status.Text + "Erro: Problema na localização de Cliente ( " + xCNPJ + "  )" + System.Environment.NewLine;
                            xRetorno = "03 (Problema na localização de cliente (" + xCNPJ + ")";
                        }
                    }



                    if (txt_Status.Text == "")
                    {

                        //criar registro de colaborador
                        Empregado rColaborador = new Empregado();

                        System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                        rColaborador.tNO_EMPG = xNome;
                        rColaborador.tNO_CPF = xCPF_Completo;

                        if ( xNIT!="")
                           rColaborador.nNO_PIS_PASEP = System.Convert.ToInt64(xNIT);                        

                        rColaborador.tNUM_CTPS = xCTPS_Numero;
                        rColaborador.tSER_CTPS = xCTPS_Serie;
                        rColaborador.tUF_CTPS = xCTPS_UF;
                        rColaborador.tNO_IDENTIDADE = xRG;
                        rColaborador.tSEXO = xSexo;
                        rColaborador.hDT_NASC = System.Convert.ToDateTime(xDataNascimento, ptBr);
                        rColaborador.teMail = xeMail;
                        rColaborador.teMail_Resp = xeMail_Responsavel;
                        rColaborador.hDT_ADM = System.Convert.ToDateTime(xDataAdmissao, ptBr);
                        rColaborador.tCOD_EMPR = xMatricula;

                        RegimeRevezamento xRegimeRevezamento = new RegimeRevezamento();
                        rColaborador.nID_REGIME_REVEZAMENTO = xRegimeRevezamento;                      


                        rColaborador.nID_EMPR = rCliente;
                        rColaborador.Save();

                        if (rColaborador.Id == 0)
                        {
                            txt_Status.Text = txt_Status.Text + "Erro: Problema na salva do colaborador ( " + xNome + "  )" + System.Environment.NewLine;
                            xRetorno = "03 (Problema na salva do colaborador (" + xNome + ")";
                        }
                        else
                        {
                            //criar classif.funcional

                            //ver se função existe
                            Funcao rFuncao = new Funcao();
                            rFuncao.Find(" IdCliente = " + rEmpresa.Id.ToString() + " and NomeFuncao = '" + xFuncao + "' ");

                            if (rFuncao.Id == 0)
                            {
                                rFuncao = new Funcao();
                                rFuncao.NumeroCBO = xCBO;
                                rFuncao.NomeFuncao = xFuncao;
                                rFuncao.IdCliente = rCliente;
                                rFuncao.DescricaoFuncao = xDescricao_Funcao;                                                       
                                rFuncao.Save();
                            }

                            if (rFuncao.Id == 0)
                            {
                                txt_Status.Text = txt_Status.Text + "Erro: Problema na salva da Funcao ( " + xFuncao + "  )" + System.Environment.NewLine;
                                xRetorno = "03 (Problema na salva da Funcao (" + xFuncao + ")";
                                rColaborador.Delete();
                            }


                            Cargo rCargo = new Cargo();

                            if (txt_Status.Text == "")
                            {

                                //ver se cargo existe
                                if (xCargo != "")
                                {

                                    rCargo.Find(" nId_Empr = " + rEmpresa.Id.ToString() + " and tNo_Cargo = '" + xCargo + "' ");

                                    if (rCargo.Id == 0)
                                    {
                                        rCargo = new Cargo();
                                        rCargo.tNO_CARGO = xCargo;
                                        rCargo.nID_EMPR = rCliente;
                                        rCargo.Save();
                                    }

                                    
                                    if (rCargo.Id == 0)
                                    {
                                        txt_Status.Text = txt_Status.Text + "Erro: Problema na salva do Cargo ( " + xCargo + "  )" + System.Environment.NewLine;
                                        xRetorno = "03 (Problema na salva do Cargo (" + xCargo + ")";
                                        rColaborador.Delete();
                                    }

                                }

                            }

                            

                            Setor rSetor = new Setor();

                            if (txt_Status.Text == "")
                            {
                                //ver se setor existe
                                rSetor.Find(" nId_Empr = " + rEmpresa.Id.ToString() + " and tNO_STR_EMPR = '" + xSetor + "' ");

                                if (rSetor.Id == 0)
                                {
                                    rSetor = new Setor();
                                    rSetor.tNO_STR_EMPR = xSetor;
                                    rSetor.nID_EMPR = rCliente;
                                    rSetor.Save();
                                }

                                if (rSetor.Id == 0)
                                {
                                    txt_Status.Text = txt_Status.Text + "Erro: Problema na salva do Setor ( " + xSetor + "  )" + System.Environment.NewLine;
                                    xRetorno = "03 (Problema na salva do Setor (" + xSetor + ")";
                                    rColaborador.Delete();
                                }


                            }


                            if (txt_Status.Text == "")
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
                                xEmprFunc.hDT_INICIO = System.Convert.ToDateTime(xDataAdmissao, ptBr);
                                xEmprFunc.nID_EMPR = rCliente;
                                xEmprFunc.Save();

                                if (xEmprFunc.Id == 0)
                                {
                                    txt_Status.Text = txt_Status.Text + "Erro: Problema na salva da Classif.Funcional " + System.Environment.NewLine;
                                    xRetorno = "03 (Problema na salva da Classif.Funcional ";
                                    rColaborador.Delete();
                                }



                            }                          

                        }


                        
                    }




                    if (txt_Status.Text != "")
                    {

                        txt_Status.Text = txt_Status.Text + " (" + xNome + " )" + System.Environment.NewLine;
                        xRetorno = xRetorno + " (" + xNome + " )" + System.Environment.NewLine;

                    }
                    else
                    {

                        //para cada registro enviar o retorno
                        xRetorno = "01 Processamento concluído sem erros";
                    }



                    //após envio do retorno
                    //xRetorno = "";
                    //txt_Status.Text = "";


                }

            }
            catch (Exception ex)
            {
                txt_Status.Text = txt_Status.Text + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

                Session["zErro"] = txt_Status.Text;
                Response.Redirect("~/Comunicacao2.aspx");


            }
            finally
            {

                if (txt_Status.Text != "")
                {
                    Session["zErro"] = txt_Status.Text;
                    Response.Redirect("~/Comunicacao2.aspx");
                }
                else
                {


                    Session["zErro"] = txt_Status.Text + " " + "Processamento Finalizado";
                    Response.Redirect("~/Comunicacao2.aspx");
                }
            }


        }


        private void Executar_Editar_Colaborador(string xmlData)
        {


            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

            //int zId = 0;

            string xID = "";
            string xCNPJ = "";
            string xNome = "";
            string xMatricula = "";
            string xNIT = "";
            string xCPF_Original = "";
            string xCTPS_Numero = "";
            string xCTPS_Serie = "";
            string xCTPS_UF = "";
            string xRG = "";
            string xCPF = "";
            string xSexo = "";
            string xDataNascimento = "";
            string xDataAdmissao = "";
            string xFuncao = "";
            string xCargo = "";
            string xSetor = "";
            string xCBO = "";
            string xeMail = "";
            string xeMail_Responsavel = "";
            string xDescricao_Funcao = "";



            string rSelect = "";

            string xRetorno = "";


            try
            {


                XDocument doc = XDocument.Parse(xmlData);


                //preciso testar loop para vários registros no mesmo XML

                var points = doc.Descendants("Tipo");

                foreach (XElement current in points)
                {
                    xID = current.Element("ID").Value;
                    xCNPJ = current.Element("CNPJ").Value;

                    xNome = current.Element("Nome").Value;
                    xMatricula = current.Element("Matricula").Value;
                    xNIT = current.Element("NIT").Value;
                    xCPF_Original = current.Element("CPF_Original").Value;
                    xCTPS_Numero = current.Element("CTPS_Numero").Value;
                    xCTPS_Serie = current.Element("CTPS_Serie").Value;
                    xCTPS_UF = current.Element("CTPS_UF").Value;
                    xRG = current.Element("RG").Value;
                    xCPF = current.Element("CPF").Value;
                    xSexo = current.Element("Sexo").Value;
                    xDataNascimento = current.Element("DataNascimento").Value;
                    xDataAdmissao = current.Element("DataAdmissao").Value;
                    xFuncao = current.Element("Funcao").Value;
                    xCargo = current.Element("Cargo").Value;
                    xSetor = current.Element("Setor").Value;
                    xCBO = current.Element("CBO").Value;
                    xeMail = current.Element("email").Value;
                    xeMail_Responsavel = current.Element("email_Responsavel").Value;
                    xDescricao_Funcao = current.Element("Descricao_Funcao").Value;
                }


                xRetorno = "";

                //validar campos
                if (xID.Length < 20 || xID.Length > 22)
                {
                    txt_Status.Text = " Campo ID inválido | ";
                    xRetorno = " Campo ID Inválido |";
                }             
                if (xCNPJ.Length > 14)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CNPJ inválido |";
                    xRetorno = xRetorno + " Campo CNPJ Inválido |";
                }

                if (SomenteNumeros(xCPF_Original) == false)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CPF Original deve ser somente números |";
                    xRetorno = xRetorno + " Campo CPF Original deve ser somente números |";
                }
                if (xCPF_Original.Length < 8 || xCPF_Original.Length > 11)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CPF Original inválido |";
                    xRetorno = xRetorno + " Campo CPF Original Inválido |";
                }

                xNIT = xNIT.Trim();
                if (xNIT != "")
                {
                    if (xNIT.Length > 14)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo NIT inválido |";
                        xRetorno = xRetorno + " Campo NIT Inválido |";
                    }
                }
                if (xMatricula.Length > 10)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Matricula inválido |";
                    xRetorno = xRetorno + " Campo Matricula Inválido |";
                }
                if (xCTPS_Numero.Length > 8)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CTPS Numero inválido |";
                    xRetorno = xRetorno + " Campo CTPS Numero Inválido |";
                }
                if (xCTPS_Serie.Length > 6)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CTPS Serie inválido |";
                    xRetorno = xRetorno + " Campo CTPS Serie Inválido |";
                }
                if (xCTPS_UF.Length > 2)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CTPS UF inválido |";
                    xRetorno = xRetorno + " Campo CTPS UF Inválido |";
                }
                if ( xRG.Length > 14)
                {
                    txt_Status.Text = txt_Status.Text + " Campo RG inválido |";
                    xRetorno = xRetorno + " Campo RG Inválido |";
                }
                xCPF = xCPF.Trim();
                if (xCPF != "")
                {
                    if (xCPF.Length < 6 || xCPF.Length > 11)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo CPF inválido |";
                        xRetorno = xRetorno + " Campo CPF Inválido |";
                    }
                }
                if (xSexo.Length > 1)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Sexo inválido |";
                    xRetorno = xRetorno + " Campo Sexo Inválido |";
                }
                if (xDataNascimento != "")
                {
                    if (xDataNascimento.Length != 10)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo Data Nascimento inválido |";
                        xRetorno = xRetorno + " Campo Data Nascimento Inválido |";
                    }
                    if (Validar_Data(xDataNascimento) == false)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo Data Nascimento inválido |";
                        xRetorno = xRetorno + " Campo Data Nascimento Inválido |";
                    }
                }
                if (xDataAdmissao != "")
                {
                    if (xDataAdmissao.Length != 10)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo Data Admissao inválido |";
                        xRetorno = xRetorno + " Campo Data Admissao Inválido |";
                    }
                    if (Validar_Data(xDataAdmissao) == false)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo Data Admissao inválido |";
                        xRetorno = xRetorno + " Campo Data Admissao Inválido |";
                    }
                }
                if ( xFuncao.Length > 100)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Funcao inválido |";
                    xRetorno = xRetorno + " Campo Funcao Inválido |";
                }
                if ( xSetor.Length > 60)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Setor inválido |";
                    xRetorno = xRetorno + " Campo Setor Inválido |";
                }
                if (xCargo.Length >100)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Cargo inválido |";
                    xRetorno = xRetorno + " Campo Cargo Inválido |";
                }
                if (xCBO.Length > 10)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CBO inválido |";
                    xRetorno = xRetorno + " Campo CBO Inválido |";
                }
                if (xeMail.Length > 60)
                {
                    txt_Status.Text = txt_Status.Text + " Campo e-mail inválido |";
                    xRetorno = xRetorno + " Campo e-mail Inválido |";
                }
                if (xeMail_Responsavel.Length > 60)
                {
                    txt_Status.Text = txt_Status.Text + " Campo e-mail Responsavel inválido |";
                    xRetorno = xRetorno + " Campo e-mail Responsavel Inválido |";
                }
                if ( xDescricao_Funcao.Length > 1000)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Descrição Função inválido |";
                    xRetorno = xRetorno + " Campo Descrição Função Inválido |";
                }




                if (xRetorno != "")
                    xRetorno = "50 ( Validação de campos ): " + xRetorno;






                if (txt_Status.Text == "")
                {

                    rEmpresa = new Ilitera.Common.Pessoa();

                    rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

                    //pegar Id Empresa                    
                    rEmpresa.Find(rSelect);

                    //se não achar empresa,  emitir retorno avisando
                    if (rEmpresa.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: Empresa não localizada ( " + xCNPJ + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (Empresa não localizada (" + xCNPJ + ")";
                    }

                }


                string xCPF_Completo = "";
                xCPF_Original = xCPF_Original.Trim();

                if (xCPF_Original.Length < 11)
                    xCPF_Completo = new string('0', 11 - xCPF_Original.Length) + xCPF_Original;
                else
                    xCPF_Completo = xCPF_Original;


                if (txt_Status.Text == "")
                {
                    StringBuilder cpf = new StringBuilder();
                    cpf.Append(xCPF_Completo)
                        .Replace(".", string.Empty)
                        .Replace("-", string.Empty)
                        .Replace(" ", string.Empty);

                    if (!Pessoa.VeriricaCPF(cpf.ToString()))
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: CPF Inválido ( " + xCPF + "  )" + System.Environment.NewLine;
                        xRetorno = "15 (CPF Inválido (" + xCPF + ")";
                    }


                }




                Empregado rColaborador = new Empregado();

                if (txt_Status.Text == "")
                {

                    //procurar colaborador por NIT Original e CNPJ para edição                   

                  

                    rColaborador.Find(" ( tno_CPF = '" + xCPF_Original + "'  or tno_CPF = '" + xCPF_Completo + "' ) and nId_Empr = " + rEmpresa.Id.ToString());

                    if (rColaborador.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: Colaborador não localizado ( " + xCPF_Original + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (Colaborador não localizado (" + xCPF_Original + ")";
                    }


                }




                if (xCPF != "")
                {
                    if (xCPF.Length < 11)
                        xCPF_Completo = new string('0', 11 - xCPF.Length) + xCPF;
                    else
                        xCPF_Completo = xCPF;
                }



                Cliente rCliente = new Cliente();

                if (txt_Status.Text == "")
                {
                    rCliente.Find(" IdCliente = " + rEmpresa.Id.ToString());

                    if (rCliente.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: Problema na localização de Cliente ( " + xCNPJ + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (Problema na localização de cliente (" + xCNPJ + ")";
                    }

                }




                if (txt_Status.Text == "")
                {

                    //criar registro de colaborador
                   
                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                    if ( xNome != "" )
                       rColaborador.tNO_EMPG = xNome;

                    if ( xCPF != "" )
                       rColaborador.tNO_CPF = xCPF_Completo;

                    if ( xNIT != "")
                       rColaborador.nNO_PIS_PASEP = System.Convert.ToInt64(xNIT);

                    if ( xCTPS_Numero != "")
                       rColaborador.tNUM_CTPS = xCTPS_Numero;

                    if ( xCTPS_Serie != "")
                       rColaborador.tSER_CTPS = xCTPS_Serie;

                    if ( xCTPS_UF != "")
                       rColaborador.tUF_CTPS = xCTPS_UF;

                    if ( xRG != "")
                       rColaborador.tNO_IDENTIDADE = xRG;

                    if ( xSexo != "")
                       rColaborador.tSEXO = xSexo;

                    if ( xDataNascimento!= "")
                       rColaborador.hDT_NASC = System.Convert.ToDateTime(xDataNascimento, ptBr);

                    if ( xeMail != "")
                       rColaborador.teMail = xeMail;

                    if ( xeMail_Responsavel != "")
                       rColaborador.teMail_Resp = xeMail_Responsavel;

                    if ( xDataAdmissao != "")
                        rColaborador.hDT_ADM = System.Convert.ToDateTime(xDataAdmissao, ptBr);

                    if ( xMatricula != "")
                        rColaborador.tCOD_EMPR = xMatricula;

                   // RegimeRevezamento xRegimeRevezamento = new RegimeRevezamento();
                    //rColaborador.nID_REGIME_REVEZAMENTO = xRegimeRevezamento;


                   // rColaborador.nID_EMPR = rCliente;
                    rColaborador.Save();

                    if (rColaborador.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: Problema na salva do colaborador ( " + xNome + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (Problema na salva do colaborador (" + xNome + ")";
                    }
                    else
                    {
                        //ajustar classif.funcional




                        Funcao rFuncao = new Funcao();

                        //ver se função existe
                        if (xFuncao != "")
                        {
                            
                            rFuncao.Find(" IdCliente = " + rEmpresa.Id.ToString() + " and NomeFuncao = '" + xFuncao + "' ");

                            if (rFuncao.Id == 0)
                            {
                                rFuncao = new Funcao();
                                rFuncao.NumeroCBO = xCBO;
                                rFuncao.NomeFuncao = xFuncao;
                                rFuncao.IdCliente = rCliente;
                                rFuncao.DescricaoFuncao = xDescricao_Funcao;
                                rFuncao.Save();
                            }
                            else
                            {
                                if ( xDescricao_Funcao.Trim() != "")
                                {
                                    rFuncao.DescricaoFuncao = xDescricao_Funcao;
                                    rFuncao.Save();
                                }

                            }

                            if (rFuncao.Id == 0)
                            {
                                txt_Status.Text = txt_Status.Text + "Erro: Problema na salva da Funcao ( " + xFuncao + "  )" + System.Environment.NewLine;
                                xRetorno = "03 (Problema na salva da Funcao (" + xFuncao + ")";                                
                            }


                        }


                        Cargo rCargo = new Cargo();

                        if (xCargo != "")
                        {
                                                       

                            if (txt_Status.Text == "")
                            {



                                rCargo.Find(" nId_Empr = " + rEmpresa.Id.ToString() + " and tNo_Cargo = '" + xCargo + "' ");

                                if (rCargo.Id == 0)
                                {
                                    rCargo = new Cargo();
                                    rCargo.tNO_CARGO = xCargo;
                                    rCargo.nID_EMPR = rCliente;
                                    rCargo.Save();
                                }


                                if (rCargo.Id == 0)
                                {
                                    txt_Status.Text = txt_Status.Text + "Erro: Problema na salva do Cargo ( " + xCargo + "  )" + System.Environment.NewLine;
                                    xRetorno = "03 (Problema na salva do Cargo (" + xCargo + ")";                                    
                                }

                            }


                        }



                        Setor rSetor = new Setor();

                        if (xSetor != "")
                        {                            

                            if (txt_Status.Text == "")
                            {
                                //ver se setor existe
                                rSetor.Find(" nId_Empr = " + rEmpresa.Id.ToString() + " and tNO_STR_EMPR = '" + xSetor + "' ");

                                if (rSetor.Id == 0)
                                {
                                    rSetor = new Setor();
                                    rSetor.tNO_STR_EMPR = xSetor;
                                    rSetor.nID_EMPR = rCliente;
                                    rSetor.Save();
                                }

                                if (rSetor.Id == 0)
                                {
                                    txt_Status.Text = txt_Status.Text + "Erro: Problema na salva do Setor ( " + xSetor + "  )" + System.Environment.NewLine;
                                    xRetorno = "03 (Problema na salva do Setor (" + xSetor + ")";                                    
                                }


                            }

                        }


                        

                        if (txt_Status.Text == "")
                        {
                            if (xFuncao != "" || xSetor != "" || xCargo != "" || xDataAdmissao != "")
                            {

                                EmpregadoFuncao xEmprFunc = new EmpregadoFuncao();
                                xEmprFunc.Find(" nId_Empregado = " + rColaborador.Id.ToString());

                                if (xEmprFunc.Id == 0)
                                {
                                    txt_Status.Text = txt_Status.Text + "Erro: Problema na localização da Classif.Funcional " + System.Environment.NewLine;
                                    xRetorno = "03 (Problema na localização da Classif.Funcional ";                                    
                                }


                                if (txt_Status.Text == "")
                                {

                                    //TempoExposicao xTempo = new TempoExposicao();
                                    //xTempo.Find(" tHora_Extenso_Semanal = '44 horas semanais' and thora_extenso = '08h48min' ");

                                    //Ghe xGhe = new Ghe();
                                    //xEmprFunc.nID_GHE_AE = xGhe;

                                    //ImportacaoAutomatica xImp = new ImportacaoAutomatica();
                                    //xEmprFunc.nID_IMPORTACAO_AUTOMATICA = xImp;

                                    //xEmprFunc.nID_TEMPO_EXP = xTempo;

                                    if ( rSetor.Id!=0)
                                       xEmprFunc.nID_SETOR = rSetor;

                                    if ( rCargo.Id!=0)
                                        xEmprFunc.nID_CARGO = rCargo;

                                    if ( rFuncao.Id!=0)
                                        xEmprFunc.nID_FUNCAO = rFuncao;

                                    //xEmprFunc.nID_EMPREGADO = rColaborador;

                                    if ( xDataAdmissao!="")
                                        xEmprFunc.hDT_INICIO = System.Convert.ToDateTime(xDataAdmissao, ptBr);

                                    //xEmprFunc.nID_EMPR = rCliente;
                                    xEmprFunc.Save();

                                    if (xEmprFunc.Id == 0)
                                    {
                                        txt_Status.Text = txt_Status.Text + "Erro: Problema na salva da Classif.Funcional " + System.Environment.NewLine;
                                        xRetorno = "03 (Problema na salva da Classif.Funcional ";                                        
                                    }

                                }
                            }
                        }

                    }



                }




                xRetorno = "01 Processamento concluído sem erros";


            }
            catch (Exception ex)
            {
                txt_Status.Text = txt_Status.Text + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

                Session["zErro"] = txt_Status.Text;
                Response.Redirect("~/Comunicacao2.aspx");


            }
            finally
            {

                if (txt_Status.Text != "")
                {
                    Session["zErro"] = txt_Status.Text;
                    Response.Redirect("~/Comunicacao2.aspx");
                }
                else
                {


                    Session["zErro"] = txt_Status.Text + " " + "Processamento Finalizado";
                    Response.Redirect("~/Comunicacao2.aspx");
                }
            }


        }





        private void Executar_Inativar_Colaborador(string xmlData)
        {


            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

            //int zId = 0;

            string xID = "";
            string xCNPJ = "";
            string xCPF = "";
            string xDataDemissao = "";


            string rSelect = "";

            string xRetorno = "";


            try
            {


                XDocument doc = XDocument.Parse(xmlData);


                //preciso testar loop para vários registros no mesmo XML

                var points = doc.Descendants("Tipo");

                foreach (XElement current in points)
                {
                    xID = current.Element("ID").Value;
                    xCNPJ = current.Element("CNPJ").Value;
                    xCPF = current.Element("CPF").Value;
                    xDataDemissao = current.Element("DataDemissao").Value;
                }


                xRetorno = "";

                //validar campos
                if (xID.Length < 20 || xID.Length > 22)
                {
                    txt_Status.Text = " Campo ID inválido | ";
                    xRetorno = " Campo ID Inválido |";
                }
                if (xCNPJ.Length > 14)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CNPJ inválido |";
                    xRetorno = xRetorno + " Campo CNPJ Inválido |";
                }

                if (SomenteNumeros(xCPF) == false)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CPF deve ser somente números |";
                    xRetorno = xRetorno + " Campo CPf  deve ser somente números |";
                }
                if (xCPF.Length < 6 || xCPF.Length > 11)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CPF inválido |";
                    xRetorno = xRetorno + " Campo CPF Inválido |";
                }


                if (xDataDemissao != "")
                {
                    if (xDataDemissao.Length != 10)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo Data Demissao inválido |";
                        xRetorno = xRetorno + " Campo Data Demissao Inválido |";
                    }
                    if (Validar_Data(xDataDemissao) == false)
                    {
                        txt_Status.Text = txt_Status.Text + " Campo Data Demissao inválido |";
                        xRetorno = xRetorno + " Campo Data Demissao Inválido |";
                    }
                }



                if (xRetorno != "")
                    xRetorno = "50 ( Validação de campos ): " + xRetorno;






                if (txt_Status.Text == "")
                {

                    rEmpresa = new Ilitera.Common.Pessoa();

                    rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

                    //pegar Id Empresa                    
                    rEmpresa.Find(rSelect);

                    //se não achar empresa,  emitir retorno avisando
                    if (rEmpresa.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: Empresa não localizada ( " + xCNPJ + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (Empresa não localizada (" + xCNPJ + ")";
                    }

                }


                string xCPF_Completo = "";
                xCPF = xCPF.Trim();

                if (xCPF.Length < 11)
                    xCPF_Completo = new string('0', 11 - xCPF.Length) + xCPF;
                else
                    xCPF_Completo = xCPF;


                //procurar colaborador por NIT Original e CNPJ para edição
                Empregado rColaborador = new Empregado();

                if (txt_Status.Text == "")
                {

                    rColaborador.Find(" ( tNo_CPF = '" + xCPF + "' or  tNo_CPF = '" + xCPF_Completo + "' )  and nId_Empr = " + rEmpresa.Id.ToString());
                    

                    if (rColaborador.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: Colaborador não localizado ( " + xCPF + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (Colaborador não localizado (" + xCPF + ")";
                    }


                }


           


                if (txt_Status.Text == "")
                {

                    //inativar

                    System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                    rColaborador.hDT_DEM = System.Convert.ToDateTime(xDataDemissao, ptBr);

                    rColaborador.Save();
                    //trigger deve salvar na classif.funcional a data de demissao


                    if (rColaborador.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: Problema na salva do colaborador ( " + xCPF + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (Problema na salva do colaborador (" + xCPF + ")";
                    }


                }




                xRetorno = "01 Processamento concluído sem erros";


            }
            catch (Exception ex)
            {
                txt_Status.Text = txt_Status.Text + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

                Session["zErro"] = txt_Status.Text;
                Response.Redirect("~/Comunicacao2.aspx");


            }
            finally
            {

                if (txt_Status.Text != "")
                {
                    Session["zErro"] = txt_Status.Text;
                    Response.Redirect("~/Comunicacao2.aspx");
                }
                else
                {


                    Session["zErro"] = txt_Status.Text + " " + "Processamento Finalizado";
                    Response.Redirect("~/Comunicacao2.aspx");
                }
            }


        }



        //private XmlDocument Executar_Checar_Laudo(string xmlData)
        //{


        //    Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

        //    int zId = 0;

        //    string xID = "";
        //    string xCNPJ = "";


        //    string rSelect = "";

        //    string xRetorno = "";


        //    try
        //    {


        //        XDocument doc = XDocument.Parse(xmlData);


        //        //preciso testar loop para vários registros no mesmo XML

        //        var points = doc.Descendants("Tipo");

        //        foreach (XElement current in points)
        //        {
        //            xID = current.Element("ID").Value;
        //            xCNPJ = current.Element("CNPJ").Value;
        //        }


        //        xRetorno = "";

        //        //validar campos
        //        if (xID.Length < 20 || xID.Length > 22)
        //        {
        //            txt_Status.Text = " Campo ID inválido | ";
        //            xRetorno = " Campo ID Inválido |";
        //        }
        //        if (xCNPJ.Length > 14)
        //        {
        //            txt_Status.Text = txt_Status.Text + " Campo CNPJ inválido |";
        //            xRetorno = xRetorno + " Campo CNPJ Inválido |";
        //        }

       

        //        if (xRetorno != "")
        //            xRetorno = "50 ( Validação de campos ): " + xRetorno;






        //        if (txt_Status.Text == "")
        //        {

        //            rEmpresa = new Ilitera.Common.Pessoa();

        //            rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

        //            //pegar Id Empresa                    
        //            rEmpresa.Find(rSelect);

        //            //se não achar empresa,  emitir retorno avisando
        //            if (rEmpresa.Id == 0)
        //            {
        //                txt_Status.Text = txt_Status.Text + "Erro: Empresa não localizada ( " + xCNPJ + "  )" + System.Environment.NewLine;
        //                xRetorno = "03 (Empresa não localizada (" + xCNPJ + ")";
        //            }

        //        }




        //        XmlDocument xRet = new XmlDocument();




        //        if (txt_Status.Text == "")
        //        {

        //            //retornar XML com seguinte estrutura
        //            //CNPJ
        //            //LAUDO ( PPRA/PCMSO )
        //            //DataLaudo
               

        //            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

        //            Ilitera.Data.PPRA_EPI xLaudos = new Ilitera.Data.PPRA_EPI();

        //            DataSet zDs = xLaudos.Retornar_Laudos(rEmpresa.Id);

                      


        //             //montar XML de retorno com clínicas
        //             if (txt_Status.Text == "")
        //             {

        //                 xDs.DataSetName = "Retorno";
        //                 xDs.Tables[0].TableName = "Clinicas";

        //                 string xstrXML = xDs.GetXml();

        //                 xRet.LoadXml(xstrXML);


        //             }



        //                for ( int zCont=0; zCont<zDs.Tables[0].Rows.Count;zCont++)
        //            {



        //            }


        //        }




        //        xRetorno = "01 Processamento concluído sem erros";


        //    }
        //    catch (Exception ex)
        //    {
        //        txt_Status.Text = txt_Status.Text + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

        //        Session["zErro"] = txt_Status.Text;
        //        Response.Redirect("~/Comunicacao2.aspx");


        //    }
        //    finally
        //    {

        //        if (txt_Status.Text != "")
        //        {
        //            Session["zErro"] = txt_Status.Text;
        //            Response.Redirect("~/Comunicacao2.aspx");


        //        }
        //        else
        //        {


        //            Session["zErro"] = txt_Status.Text + " " + "Processamento Finalizado";
        //            Response.Redirect("~/Comunicacao2.aspx");
        //        }
        //    }


        //}



        private void Executar_Solicitar_Laudo(string xmlData)
        {


            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();

            //int zId = 0;

            string xID = "";
            string xCNPJ = "";
            string xDataLaudo = "";
            string xLaudo = "";
            string xTipoRetorno = "";

            string rSelect = "";

            string xRetorno = "";


            try
            {


                XDocument doc = XDocument.Parse(xmlData);


                //preciso testar loop para vários registros no mesmo XML

                var points = doc.Descendants("Tipo");

                foreach (XElement current in points)
                {
                    xID = current.Element("ID").Value;
                    xCNPJ = current.Element("CNPJ").Value;
                    xLaudo = current.Element("Laudo").Value;
                    xDataLaudo= current.Element("DataLaudo").Value;
                    xTipoRetorno = current.Element("TipoRetorno").Value;
                }


                xRetorno = "";

                //validar campos
                if (xID.Length < 20 || xID.Length > 22)
                {
                    txt_Status.Text = " Campo ID inválido | ";
                    xRetorno = " Campo ID Inválido |";
                }
                if (xCNPJ.Length > 14)
                {
                    txt_Status.Text = txt_Status.Text + " Campo CNPJ inválido |";
                    xRetorno = xRetorno + " Campo CNPJ Inválido |";
                }
                if (xLaudo.Length < 1 || xLaudo.Length > 6)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Laudo inválido |";
                    xRetorno = xRetorno + " Campo Laudo Inválido |";
                }
                if (xTipoRetorno.Length < 1 || xTipoRetorno.Length > 10)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Tipo Retorno inválido |";
                    xRetorno = xRetorno + " Campo Tipo Retorno Inválido |";
                }
                if (xDataLaudo.Length != 10)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Data Laudo inválido |";
                    xRetorno = xRetorno + " Campo Data Laudo Inválido |";
                }
                if (Validar_Data(xDataLaudo) == false)
                {
                    txt_Status.Text = txt_Status.Text + " Campo Data Laudo inválido |";
                    xRetorno = xRetorno + " Campo Data Laudo Inválido |";
                }

                if (xRetorno != "")
                    xRetorno = "50 ( Validação de campos ): " + xRetorno;






                if (txt_Status.Text == "")
                {

                    rEmpresa = new Ilitera.Common.Pessoa();

                    rSelect = " dbo.udf_getnumeric( nomecodigo ) = '" + xCNPJ + "' ";

                    //pegar Id Empresa                    
                    rEmpresa.Find(rSelect);

                    //se não achar empresa,  emitir retorno avisando
                    if (rEmpresa.Id == 0)
                    {
                        txt_Status.Text = txt_Status.Text + "Erro: Empresa não localizada ( " + xCNPJ + "  )" + System.Environment.NewLine;
                        xRetorno = "03 (Empresa não localizada (" + xCNPJ + ")";
                    }

                }









                if (txt_Status.Text == "")
                {


                    

                    if (xTipoRetorno.ToUpper().Trim() == "LINK")
                    {

                        tbl_Laudos zLaudos = new tbl_Laudos();
                        zLaudos.Find(" nId_Empr = " + rEmpresa.Id.ToString() + " and convert( char(10),Data_Laudo,103) = '" + xDataLaudo + "' and Laudo = '" + xLaudo + "' ");

                        if ( zLaudos.Id == 0 )
                        {
                            txt_Status.Text = txt_Status.Text + "Erro: Laudo não localizado ( " + xCNPJ + " Data: + " + xDataLaudo + " +  Tipo: " + xLaudo + "  )" + System.Environment.NewLine;
                            xRetorno = "04 (Laudo não localizado (" + xCNPJ + ")";
                        }

                        if (txt_Status.Text == "")
                        {
                            string xLink = zLaudos.Path.Trim();

                            // http://www.ilitera.net.br/driveI/laudos/

                            //xLink = xLink.ToUpper().Replace("I:\\LAUDOS\\", "http://www.ilitera.net.br/driveI/laudos/");
                            xLink = xLink.ToUpper().Replace("I:\\LAUDOS\\", "https://www.ilitera.net.br/laudos/");

                            //tirar driveI e criar Laudos




                            string xArq = "I:\\temp\\Retorno_Link_Laudos_" + System.DateTime.Now.Year.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Hour.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Second.ToString().Trim() + ".xml";

                            new XDocument(
                                 new XElement("Tipo",
                                    new XElement("ID", xID),
                                    new XElement("CNPJ", xCNPJ),
                                    new XElement("Laudo", xLaudo),
                                    new XElement("Link_Laudo", xLink),
                                    new XElement("Conteudo_Arquivo", "")
                                    )
                                  ).Save(xArq);

                            //enviar XML para endereço de retorno

                        }

                    }
                    else if(xTipoRetorno.ToUpper().Trim() == "ARQUIVO")
                    {

                        tbl_Laudos zLaudos = new tbl_Laudos();
                        zLaudos.Find(" nId_Empr = " + rEmpresa.Id.ToString() + " and convert( char(10),Data_Laudo,103) = '" + xDataLaudo + "' and Laudo = '" + xLaudo + "' ");

                        if (zLaudos.Id == 0)
                        {
                            txt_Status.Text = txt_Status.Text + "Erro: Laudo não localizado ( " + xCNPJ + " Data: + " + xDataLaudo + " +  Tipo: " + xLaudo + "  )" + System.Environment.NewLine;
                            xRetorno = "04 (Laudo não localizado (" + xCNPJ + ")";
                        }

                        if (txt_Status.Text == "")
                        {
                            string xLink = zLaudos.Path.Trim(); 

                            byte[] arrBytes = File.ReadAllBytes(xLink);
                            string strXml = Convert.ToBase64String(arrBytes);
                            //File.WriteAllText(@"i:\temp\teste_xml.txt", strXml);

                            string xArq = "I:\\temp\\Retorno_Link_Laudos_" + System.DateTime.Now.Year.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Hour.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Second.ToString().Trim() + ".xml";

                            new XDocument(
                                 new XElement("Tipo",
                                    new XElement("ID", xID),
                                    new XElement("CNPJ", xCNPJ),
                                    new XElement("Laudo", xLaudo),
                                    new XElement("Link_Laudo", ""),
                                    new XElement("Conteudo_Arquivo", strXml)
                                    )
                                  ).Save(xArq);

                            //enviar XML para endereço de retorno

                        }

                        


                    }



                    //retornar XML com seguinte estrutura
                    //CNPJ
                    //LAUDO ( PPRA/PCMSO )
                    //Laudo
                    //Link_Laudo
                    //Conteudo_Arquivo  ( PDF do laudo )      Posso mesmo deixar o PDF armazenado no servidor ( em Paradigmas ou diretório específico i:\Laudos ) 
                    //                                        e enviar Link, é mais rápido.  Vou deixar pronto as duas alternativas.
                    //                                        Posso criar processo noturno para criar os laudos nesta pasta,  assim fica mais fácil jogar no XML
                    //                                        Vou precisar de tabela de controle  
                    //                                        tbl_Laudos
                    //                                        Id_Laudo
                    //                                        nId_Chave ( nId_laud_Tec ou IdPCMSO )
                    //                                        Laudo  ( PPRA/PCMSO )
                    //                                        nId_Empr
                    //                                        Data_Laudo
                    //                                        Path  ( nome do arquivo PPRA_ + nId_Empr + _ + YYYYMMDD.pdf    

                    //if xTipo_Retorno == "Link"
                    //if xTipo_Retorno == "Arquivo"




                    //                    select* from
                    //                    (
                    //                       select nId_Laud_Tec as Chave, 'PPRA' as Laudo, nId_Empr, hDt_Laudo as Data_Laudo

                    //                       from tbllaudo_tec

                    //                       where nId_Laud_Tec not in

                    //                       (select nId_Chave from tbl_Laudos where Laudo = 'PPRA' )
                    //   and nId_Pedido in
                    //   (select IdPedido from opsa_prajna_Hom.dbo.pedido where IndStatus = 2 and DataConclusao is not null and DataCancelamento is null )
                    //   and nId_Empr in
                    //   (select IdPessoa from opsa_prajna_hom.dbo.Pessoa where isinativo = 0 )
                    //  union
                    //   select IdPCMSO as Chave, 'PCMSO' as Laudo, IdCliente as nId_Empr, DataPCMSO as Data_Laudo
                    //   from opsa_prajna_hom.dbo.PCMSO as a
                    //   left join opsa_prajna_Hom.dbo.Documento as b on(a.IdDocumento = b.IdDocumento)
                    //   where IdPCMSO not in
                    //   (select nId_Chave from tbl_Laudos where Laudo = 'PCMSO' )
                    //   and IdPedido in
                    //   (select IdPedido from opsa_prajna_Hom.dbo.pedido where IndStatus = 2 and DataConclusao is not null and DataCancelamento is null )
                    //   and IdCliente in
                    //   (select IdPessoa from opsa_prajna_hom.dbo.Pessoa where isinativo = 0 )
                    //) as tx90
                    //where datepart(yyyy, Data_Laudo) >= 2018


                }




                xRetorno = "01 Processamento concluído sem erros";


            }
            catch (Exception ex)
            {
                txt_Status.Text = txt_Status.Text + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;

                Session["zErro"] = txt_Status.Text;
                Response.Redirect("~/Comunicacao2.aspx");


            }
            finally
            {

                if (txt_Status.Text != "")
                {
                    Session["zErro"] = txt_Status.Text;
                    Response.Redirect("~/Comunicacao2.aspx");
                }
                else
                {


                    Session["zErro"] = txt_Status.Text + " " + "Processamento Finalizado";
                    Response.Redirect("~/Comunicacao2.aspx");
                }
            }


        }





        private static bool SomenteNumeros(string str)
        {
            str = str.Trim();
            return ( System.Text.RegularExpressions.Regex.IsMatch(str, @"^\+?\d+$"));
        }


        protected Boolean Validar_Data(string zData)
        {
            int zDia = 0;
            int zMes = 0;
            int zAno = 0;

            string Validar;
            bool isNumerical;
            int myInt;


            if (zData.Length != 10)
            {                
                return false;
            }

            Validar = zData.Substring(0, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {               
                return false;
            }


            Validar = zData.Substring(3, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {               
                return false;
            }


            Validar = zData.Substring(6, 4);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {               
                return false;
            }


            if (zData.Substring(2, 1) != "/" || zData.Substring(5, 1) != "/")
            {              
                return false;
            }


            zDia = System.Convert.ToInt32(zData.Substring(0, 2));
            zMes = System.Convert.ToInt32(zData.Substring(3, 2));
            zAno = System.Convert.ToInt32(zData.Substring(6, 4));

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




        public string Executar_Convocacao(string xmlData)
        {

            //ClinicaCliente rClinica = new ClinicaCliente();
            Clinica rClinica = new Clinica();
            Empregado rEmpregado = new Empregado();
            Ilitera.Common.Pessoa rEmpresa = new Ilitera.Common.Pessoa();
            int zId = 0;


            string rSelect = "";

            string txt_Status = "";

            


            //int xAux;
            string xExames = "";
            string xExames2 = "";
            string xExames3 = "";
            string xExames4 = "";
            string xTipo = "";
            string xBasico = "0";
            string xObs = "";
            //int xCont = 0;
            //string xEnvio_Email = "N";

            string xImpDt = "S";

            string CNPJ = "";
            string Empresa = "";
            string CodUsuario = "";
            string TipoExame = "";
            string Colaborador = "";
            string Data = "";
            string xId = "";
            string xIdClinica = "";   
            


            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            
            try
            {
                

                XDocument doc = XDocument.Parse(xmlData);


                //preciso testar loop para vários registros no mesmo XML

                var points = doc.Descendants("Tipo");

                foreach (XElement current in points)
                {
                    CNPJ = current.Element("CNPJ").Value;
                    Empresa = current.Element("Empresa").Value;
                    CodUsuario = current.Element("CodUsuario").Value;
                    TipoExame = current.Element("TipoExame").Value;
                    Colaborador = current.Element("Colaborador").Value;
                    Data = current.Element("Data").Value;
                    xId = current.Element("ID").Value;
                    xIdClinica = current.Element("IdClinica").Value;

                }



                rSelect = " NomeAbreviado = '" + Empresa + "' and dbo.udf_getnumeric( nomecodigo ) = '" + CNPJ + "' ";

                //pegar Id Empresa                    
                rEmpresa.Find(rSelect);

                //se não achar empresa,  emitir retorno avisando
                if (rEmpresa.Id == 0)
                {
                    txt_Status = txt_Status + "Erro: Empresa não localizada ( " + Empresa + "  )" + System.Environment.NewLine;
                }

                //quando for convocação ( mailing ) no último irá o link para chamar comunicação
                //pode ir com um ID pronto


                int xTipoExame = 4;

                if (txt_Status == "")
                {
                    //pegar Id Colaborador                       
                    rEmpregado.Find(" tNo_Empg='" + Colaborador + "'  and nId_Empr = " + rEmpresa.Id.ToString());

                    //se não achar empregado,  emitir retorno avisando
                    if (rEmpregado.Id == 0)
                    {
                        txt_Status = txt_Status + "Erro: Empregado não localizado ( " + Colaborador + "  )" + System.Environment.NewLine;
                    }


                    if (txt_Status == "")
                    {


                        //rClinica.Find(" IdCliente = " + rEmpresa.Id.ToString() + " and convert( char(12), IdCliente ) + ' ' + convert( char(12), IdClinica ) in ( select top 1 convert( char(12), IdCliente ) + ' ' + convert( char(12), IdClinica ) from ClinicaCliente where IdCliente = " + rEmpresa.Id.ToString() + " ) ");


                        //Clinica, procurar por proximidade de CEP                        

                        //Ilitera.Data.Comunicacao xBusca = new Ilitera.Data.Comunicacao();
                        //Int32 zIdClinica = xBusca.Trazer_Clinica_CEP(xCEP);

                        //if ( zIdClinica == 0 )
                        //{
                        //    txt_Status = txt_Status + "Erro: Clínica não localizada por CEP" + System.Environment.NewLine;
                        //}

                        //if (txt_Status == "")
                        //{
                        //    rClinica.Find(zIdClinica);

                        //    //se não achar clinica,  emitir retorno avisando
                        //    if (rClinica.Id == 0)
                        //    {
                        //        txt_Status = txt_Status + "Erro: Clínica não localizada " + System.Environment.NewLine;
                        //    }

                        rClinica.Find(System.Convert.ToInt32(xIdClinica));

                        //se não achar clinica,  emitir retorno avisando
                        if (rClinica.Id == 0)
                        {
                            txt_Status = txt_Status + "Erro: Clínica não localizada " + System.Environment.NewLine;
                        }


                    
                    }




                    if (TipoExame.ToUpper().Trim() == "PERIODICO" || TipoExame.ToUpper().Trim() == "PERIÓDICO")
                    {
                        TipoExame = "4";
                        xTipoExame = 4;
                    }
                    else if (TipoExame.ToUpper().Trim() == "DEMISSIONAL")
                    {
                        TipoExame = "2";
                        xTipoExame = 2;
                    }
                    else if (TipoExame.ToUpper().Trim().Substring(0, 5) == "MUDAN")
                    {
                        TipoExame = "3";
                        xTipoExame = 3;
                    }
                    else if (TipoExame.ToUpper().Trim().Substring(0, 7) == "RETORNO")
                    {
                        TipoExame = "5";
                        xTipoExame = 5;
                    }
                    else if (TipoExame.ToUpper().Trim().Substring(0, 6) == "ADMISS")
                    {
                        TipoExame = "1";
                        xTipoExame = 1;
                    }


                    zId = rEmpregado.Id;
                }


                //validar token - deve existir registro com dados do e-mail de convocação



                //código que estaria na outra página
                if (txt_Status.Trim() == "")
                {


                    //rClinica.IdClinica.Find();





                    string xEmail = rClinica.Email.Trim();
                    //xEnvio_Email = "S";






                    Guid strAux = Guid.NewGuid();



                    xObs = rClinica.Observacao;  //  txt_Obs.Text.Trim();


                    //chamar popula para pegar string com exames da guia
                    string lst_Exames = PopularValueListClinicaClienteExameDicionario(rClinica.Id.ToString(), xTipoExame.ToString().Trim(), rEmpresa.Id.ToString(), rEmpregado.Id.ToString(), CodUsuario, Data);


                    if (lst_Exames.Substring(0, 2) == "-1")  //retornou erro
                        txt_Status = lst_Exames;





                    if (txt_Status.Trim() == "")
                    {

                        xExames = lst_Exames;


                        ////exames na guia, carregar 
                        //for (xAux = 0; xAux < lst_Exames.Items.Count; xAux++)
                        //{
                        //    if (lst_Exames.Items[xAux].Selected == true)
                        //    {
                        //        xCont++;

                        //        if (xCont < 6)
                        //        {
                        //            xExames = xExames + " /n " + lst_Exames.Items[xAux].Text;
                        //        }
                        //        else if (xCont < 11)
                        //        {
                        //            xExames2 = xExames2 + " /n " + lst_Exames.Items[xAux].Text;
                        //        }
                        //        else if (xCont < 16)
                        //        {
                        //            xExames3 = xExames3 + " /n " + lst_Exames.Items[xAux].Text;
                        //        }
                        //        else
                        //        {
                        //            xExames4 = xExames4 + " /n " + lst_Exames.Items[xAux].Text;
                        //        }
                        //    }

                    }

                    //if (chk_Basico.Checked == true)
                    //{
                    //    xBasico = "1";
                    //}


                    xTipo = xTipoExame.ToString();




                    ExameBase rexame = new ExameBase();


                    rexame.Find(" IDEMPREGADO = " + rEmpregado.Id.ToString() + " and IdExameDicionario = " + xTipoExame.ToString() + " and convert( char(10),DataExame,103 ) = '" + Data + "' ");



                    if (rexame.Id != 0)
                    {
                        //MsgBox1.Show("Ilitera.Net", "ASO já foi criado para este tipo de exame e data.", null,
                        //new EO.Web.MsgBoxButton("OK"));

                        //TextWriter tw = new StreamWriter(lbl_Arq.Text);
                        //tw.WriteLine("ASO já foi criado para este exame e data");
                        //tw.Close();
                        txt_Status = "ASO já foi criado para este exame e data";

                    }

                }



                if (txt_Status.Trim() == "")
                {
                    Cliente cliente = new Cliente();
                    cliente.Find(System.Convert.ToInt32(rEmpresa.Id.ToString()));


                    string xAptidao = "";








                    //pegar data de planejamento + "|" + Data Ultimo exame
                    string rData = "";
                    Ilitera.Data.Clientes_Funcionarios xPlan2 = new Ilitera.Data.Clientes_Funcionarios();
                    rData = xPlan2.Buscar_Data_Planejamento_Exame_Colaborador(rEmpregado.Id, 4, Data);


                    Ilitera.Data.PPRA_EPI xGuia = new Ilitera.Data.PPRA_EPI();
                    xGuia.Salvar_Dados_Guia_Encaminhamento(rEmpresa.Id, rEmpregado.Id, xTipoExame.ToString(), xExames, Data, "", rClinica.NomeAbreviado, System.Convert.ToInt32(CodUsuario), "N", rData.Substring(0, 10), rData.Substring(11), "");



                    //se demissional da Prajna,  colocar data do ASO na data de demissão de colaborador
                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().Trim().ToUpper() == "OPSA")
                    {
                        if (xTipo.ToUpper().Trim() == "D")
                        {
                            //System.Convert.ToInt32(Request["IdEmpregado"].ToString())
                            //txt_Data.Text
                            Ilitera.Data.PPRA_EPI xDem = new Ilitera.Data.PPRA_EPI();
                            xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(rEmpregado.Id.ToString()), Data);

                        }
                    }
                    else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                    {
                        if (xTipo.ToUpper().Trim() == "D")
                        {
                            //System.Convert.ToInt32(Request["IdEmpregado"].ToString())
                            //txt_Data.Text
                            Ilitera.Data.PPRA_EPI xDem = new Ilitera.Data.PPRA_EPI();

                            //xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(Request["IdEmpregado"].ToString()), txt_Data.Text);                         
                            xDem.Guia_Demissao_Colaborador_Obras(System.Convert.ToInt32(rEmpregado.Id.ToString()), Data);


                        }

                    }





                    // Depois testar criação de complementares - talvez eu possa colocar isso junto com código que adiciona exames em lst_Exames






                    //// criar exames complementares do ASO, se estes não existirem ainda.  Colocar como em espera
                    //if (cliente.Gerar_Complementares_Guia == true)
                    //{


                    //    Ilitera.Opsa.Data.Empregado nEmpregado = new Ilitera.Opsa.Data.Empregado(System.Convert.ToInt32(rEmpregado.Id.ToString()));

                    //    Ilitera.Common.Juridica xClin = new Ilitera.Common.Juridica();
                    //    xClin.Find(" IdJuridica = " + rClinica.Id.ToString() );

                    //    for (int nCont = 0; nCont < lst_IdExames.Items.Count; nCont++)
                    //    {

                    //        //checar se exame já existe
                    //        Int32 xIdExameDicionario = System.Convert.ToInt32(lst_IdExames.Items[nCont].ToString());


                    //        Complementar xCompl = new Complementar();
                    //        xCompl.Find(" IdEmpregado = " + rEmpregado.Id.ToString() + " and IdExameDicionario = " + xIdExameDicionario.ToString() + " and convert( char(10), DataExame, 103 ) = '" + Data + "'");

                    //        if (xCompl.Id == 0)
                    //        {

                    //            ExameDicionario xED = new ExameDicionario();
                    //            xED.Find(" IdExameDicionario = " + xIdExameDicionario.ToString());

                    //            if (xED.Nome.ToUpper().Trim() == "AUDIOMETRIA")
                    //            {
                    //                Audiometria xAud = new Audiometria();
                    //                xAud.IdExameDicionario = xED;
                    //                xAud.IdEmpregado = nEmpregado;
                    //                xAud.DataExame = System.Convert.ToDateTime(Data, ptBr);
                    //                xAud.IndResultado = (int)Ilitera.Opsa.Data.ResultadoExame.EmEspera;
                    //                xAud.IdJuridica = xClin;

                    //                PagamentoClinica xPag = new PagamentoClinica();
                    //                xAud.IdPagamentoClinica = xPag;

                    //                xAud.IndAudiometriaTipo = 0;

                    //                Medico rMedico = new Medico();
                    //                xAud.IdMedico = rMedico;

                    //                ConvocacaoExame xConv = new ConvocacaoExame();
                    //                xAud.IdConvocacaoExame = xConv;

                    //                Audiometro xAudiometro = new Audiometro();
                    //                xAud.IdAudiometro = xAudiometro;

                    //                Ilitera.Common.Compromisso xcompr = new Ilitera.Common.Compromisso();
                    //                xAud.IdCompromisso = xcompr;

                    //                xAud.Save();
                    //            }
                    //            else
                    //            {
                    //                xCompl.IdExameDicionario = xED;
                    //                xCompl.IdEmpregado = nEmpregado;
                    //                xCompl.DataExame = System.Convert.ToDateTime(Data, ptBr);
                    //                xCompl.IndResultado = (int)Ilitera.Opsa.Data.ResultadoExame.EmEspera;
                    //                xCompl.IdJuridica = xClin;
                    //                xCompl.Save();
                    //            }

                    //        }



                    //    }


                    //}




                    //   Response.Redirect("~\\DadosEmpresa\\RelatorioGuiaASO_Auto.aspx?IliteraSystem=" + strAux.ToString().Substring(0, 5)
                    //   + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&E1=" + xExames + "&E2=" + xExames2 + "&E3=" + xExames3 + "&E4=" + xExames4 + "&IdClinica=" + cmb_Clinicas.SelectedValue.ToString() + "&H_E=" + txt_Hora.Text + "&D_E=" + txt_Data.Text + "&Tipo=" + xTipo + "&Basico=" + xBasico + "&Mail=" + xEnvio_Email + "&ImpDt=" + xImpDt + "&Apt=" + xAptidao + "&CodUsuario=" + Request["IdUsuario"].ToString().Trim() + "&xId=" + Request["xId"].ToString().Trim() + "&xIdExame=" + xIdExame + "&xArq=" + Request["xArq"].ToString().Trim());



                    //parte de geração das guias

                    string xId_Empregado;
                    string xId_Empresa;
                    string xId_Clinica;

                    string xData_Exame;
                    string xHora_Exame;

                    string xApt;
                    string xDtDemissao;
                    //string xID;
                    string xId_Exame;



                    //InicializaWebPageObjects();
                    //Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);





                    xId_Empregado = rEmpregado.Id.ToString();
                    xId_Empresa = rEmpresa.Id.ToString();
                    xId_Clinica = rClinica.Id.ToString();
                    xData_Exame = Data;
                    xHora_Exame = "";




                    xApt = xAptidao;

                    xId_Exame = TipoExame;


                    xDtDemissao = "";




                    Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();


                    //criando ASO
                    xTipoExame = System.Convert.ToInt16(xTipo);



                    Cliente zCliente = new Cliente();
                    zCliente.Find(System.Convert.ToInt32(xId_Empresa));



                    ExameClinicoFacade exame = new ExameClinicoFacade();

                    exame.Prontuario = "";
                    //exame.Observacao = "";

                    empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + rEmpregado.Id.ToString());

                    exame.IdEmpregado = empregado;

                    exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
                    exame.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();

                    exame.apt_Espaco_Confinado = "";
                    exame.apt_Trabalho_Altura = "";
                    exame.apt_Transporte = "";
                    exame.apt_Submerso = "";
                    exame.apt_Eletricidade = "";
                    exame.apt_Alimento = "";
                    exame.apt_Brigadista = "";
                    exame.apt_Socorrista = "";



                    ExameDicionario xExameDicionario = new ExameDicionario();

                    xExameDicionario.Find(xTipoExame);

                    Juridica xJuridica = new Juridica();
                    xJuridica.Find(Convert.ToInt32(rClinica.Id));

                    exame.IdExameDicionario = xExameDicionario;
                    exame.IdJuridica = xJuridica;

                    //exame.IdExameDicionario.Find( xTipoExame );

                    //exame.IdJuridica.Find(Convert.ToInt32(Request["IdClinica"].ToString()));


                    exame.DataExame = System.Convert.ToDateTime(xData_Exame, ptBr);

                    if (xTipoExame == 2 && xDtDemissao.Trim() != "") exame.DataDemissao = System.Convert.ToDateTime(xDtDemissao, ptBr);

                    exame.IndResultado = 3;


                    Medico xMedico = new Medico();
                    xMedico.Find(1111);  // -2133369037);

                    exame.IdMedico = xMedico;

                    //Usuario xusuario = new Usuario();
                    //xusuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

                    //exame.UsuarioId = 


                    //Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

                    Entities.Usuario usuario = new Entities.Usuario();
                    usuario.IdUsuario = System.Convert.ToInt32(System.Convert.ToInt32(CodUsuario));


                    int zStatus = 0;
                    try
                    {
                        zStatus = exame.Save(System.Convert.ToInt32(System.Convert.ToInt32(CodUsuario)));
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToUpper() != "MÉTODO NÃO-ESTÁTICO REQUER UM DESTINO." && ex.Message.ToUpper() != "NON-STATIC METHOD REQUIRES A TARGET.")
                            throw new Exception(ex.Message.ToString());


                    }

                    //exame.Save();

                    //if ( cliente.Gerar_Complementares_Guia==true)  // criar exames complementares do ASO, se estes não existirem ainda.  Colocar como em espera
                    //{






                    //}




                    Clinico exame2 = new Clinico();
                    exame2.Find(exame.Id);


                    exame2.apt_Trabalho_Altura2 = "0";
                    exame2.apt_Espaco_Confinado2 = "0";
                    exame2.apt_Transporte2 = "0";
                    exame2.apt_Submerso2 = "0";
                    exame2.apt_Eletricidade2 = "0";
                    exame2.apt_Aquaviario2 = "0";
                    exame2.apt_Alimento2 = "0";
                    exame2.apt_Brigadista2 = "0";
                    exame2.apt_Socorrista2 = "0";


                    if (xApt.IndexOf("A") >= 0) exame2.apt_Trabalho_Altura2 = "1";
                    if (xApt.IndexOf("C") >= 0) exame2.apt_Espaco_Confinado2 = "1";
                    if (xApt.IndexOf("T") >= 0) exame2.apt_Transporte2 = "1";
                    if (xApt.IndexOf("S") >= 0) exame2.apt_Submerso2 = "1";
                    if (xApt.IndexOf("E") >= 0) exame2.apt_Eletricidade2 = "1";
                    if (xApt.IndexOf("Q") >= 0) exame2.apt_Aquaviario2 = "1";
                    if (xApt.IndexOf("M") >= 0) exame2.apt_Alimento2 = "1";
                    if (xApt.IndexOf("B") >= 0) exame2.apt_Brigadista2 = "1";
                    if (xApt.IndexOf("R") >= 0) exame2.apt_Socorrista2 = "1";






                    Int16 zTamanho = 3;

                    exame2.IdEmpregado.Find();
                    exame2.IdEmpregado.nID_EMPR.Find();
                    exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();

                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)//exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI"  )  // && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
                    {
                        zTamanho = 4;
                    }


                    //string xTitulo = "Kit Guia/ASO/PCI - Convocação"; ;

                    //if (xId_Exame != "4")  //apenas guia de complementar
                    //{
                    //    zTamanho = 1;
                    //    xTitulo = "Kit Guia Complementar - Convocação";
                    //}


                    CrystalDecisions.CrystalReports.Engine.ReportClass[] reports = new ReportClass[zTamanho];

                    //Cliente zCliente = new Cliente();
                    //zCliente.Find(System.Convert.ToInt32(xId_Empresa));


                    exame2.IdEmpregadoFuncao.Find();


                    //se for apenas guia de encaminhamento - convocação exames complementares


                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                    {
                        //RptGuia_Prajna report0 = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt).GetReport();
                        RptGuia_Nova_Prajna report0 = new DataSourceGuia_Prajna(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, exame2.IdEmpregadoFuncao.Id).GetReport_Nova();
                        //CreatePDFDocument(report, this.Response);
                        reports[0] = report0;
                    }
                    else
                    {
                        //RptGuia report0 = new DataSourceGuia(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt).GetReport();
                        RptGuia_Nova report0 = new DataSourceGuia(cliente, System.Convert.ToInt32(xId_Empregado), System.Convert.ToInt32(xId_Empresa), System.Convert.ToInt32(xId_Clinica), xExames, xExames2, xExames3, xExames4, xData_Exame, xHora_Exame, xTipo, xBasico, xObs, xImpDt, exame2.IdEmpregadoFuncao.Id).GetReport_Nova();
                        //CreatePDFDocument(report, this.Response);
                        reports[0] = report0;
                    }







                    if (xId_Exame == "4")
                    {



                        RptAso report2 = new DataSourceExameAsoPci(exame2).GetReport();

                        //tenho o ID do ASO para colocar no registro da guia gerada ?
                        //dar um update ?




                        //Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Relatorio, report.GetType());
                        //ImpressaoDocUsuario.InsereRegistro(usuario, cliente, Convert.ToInt64(report.SummaryInfo.KeywordsInReport), true, funcionalidade);

                        //CreatePDFDocument(report, this.Response);


                        //Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();

                        //Clinico exame2 = new Clinico(Convert.ToInt32(Request["IdExame"]));

                        reports[1] = report2;

                        Juridica xClin = new Juridica();
                        xClin.Find(rClinica.Id);

                        string xClinNome = "";

                        if (xClin != null) xClinNome = xClin.NomeAbreviado.ToUpper().Trim();


                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Daiti") > 0 || Ilitera.Data.SQLServer.EntitySQLServer.xDB2.Trim() == "Sied_Novo" || xClinNome.IndexOf("DAITI") >= 0 || xClinNome.IndexOf("IPATINGA") >= 0)
                        {
                            RptPci report3 = new DataSourceExameAsoPci(exame2).GetReportPci_Antigo();
                            reports[2] = report3;
                        }
                        else
                        {

                            exame2.IdEmpregado.Find();
                            exame2.IdEmpregado.nID_EMPR.Find();
                            exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();

                            //if (exame.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI" && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini

                            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0) //  && exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI")  // && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
                            {
                                RptPci_Novo_Capgemini report3 = new DataSourceExameAsoPci(exame2).GetReportPciCapgemini();
                                reports[2] = report3;
                            }
                            else
                            {
                                RptPci_Novo report3 = new DataSourceExameAsoPci(exame2).GetReportPci();
                                reports[2] = report3;
                            }
                        }
                        //RptPci report3 = new DataSourceExameAsoPci(exame2).GetReportPci();


                        exame2.IdEmpregado.Find();
                        exame2.IdEmpregado.nID_EMPR.Find();
                        exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Find();


                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0) //exame2.IdEmpregado.nID_EMPR.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI"  ) // && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("_HOM") > 0)  //.Id == -905238295)  // Capgemini
                        {
                            Int32 xIdExame = exame2.Id;

                            Carregar_Dados_Anamnese_Exame(exame2.Id);

                            RptAnamnese report4 = new DataSourceExameAnamnese(xIdExame, true).GetReport();
                            reports[3] = report4;

                        }



                    }


                    //CreatePDFMerged(reports, this.Response, "", false, xID);

                    //HttpResponse response = this.Response;
                    string watermark = "";
                    bool RenumerarPaginas = false;



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


                    //CreatePDFMerged(streams, response, watermark, RenumerarPaginas, xId);





                    MemoryStream reportStream = PdfSharpUtility.MergeReports(streams, string.Empty, watermark, RenumerarPaginas);



                    string xPath = "I:\\temp\\guia_" + xId.Trim() + ".pdf";

                    //if (xEnvio_Email == "S")
                    //{

                    //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                    //{




                    FileStream fileStream = File.Create(xPath, (int)reportStream.Length);
                    // Initialize the bytes array with the stream length and then fill it with data
                    byte[] bytesInStream = new byte[reportStream.Length];
                    reportStream.Read(bytesInStream, 0, bytesInStream.Length);
                    // Use write method to write to the file specified above
                    fileStream.Write(bytesInStream, 0, bytesInStream.Length);

                    fileStream.Flush();

                    fileStream.Dispose();
                    fileStream = null;

                    Clinica xClinica = new Clinica(exame.IdJuridica.Id);

                    Cliente xCliente = new Cliente(exame.IdEmpregado.nID_EMPR.Id);
                    string xEmpresa = "";

                    if (xCliente.IdJuridicaPai.Id != 0 && xCliente.IdJuridicaPai.Id != xCliente.Id)
                    {
                        if (xCliente.IdJuridicaPai.ToString().ToUpper().IndexOf("KNOX") > 0)
                        {
                            xEmpresa = xCliente.IdJuridicaPai.ToString();
                        }
                        else
                        {
                            xEmpresa = xCliente.IdJuridicaPai.ToString() + "   Unidade: " + xCliente.NomeAbreviado.ToString();
                        }
                    }
                    else
                    {
                        xEmpresa = xCliente.GetNomeEmpresa();
                    }



                    string xCorpo = "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> </p>" +
                                     "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> DADOS DO AGENDAMENTO: </p><br>" +
                                     "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +
                                     "RG: " + exame.IdEmpregado.tNO_IDENTIDADE.ToString() + "<br>" +
                                     "Data de Nascimento: " + exame.IdEmpregado.hDT_NASC.ToString("dd/MM/yyyy") + "<br><br>" +
                                     "Empresa:  " + xEmpresa + "<br>" +
                                     "CNPJ: " + exame.IdEmpregado.nID_EMPR.GetCnpj() + "<br>" +
                                     "Cargo: " + exame.IdEmpregadoFuncao.nID_FUNCAO.ToString() + "<br>" +
                                      "Setor: " + exame.IdEmpregadoFuncao.nID_SETOR.ToString() + "<br>" +
                                      "Unidade de Trabalho: " + exame.IdEmpregadoFuncao.GetLocalDeTrabalho(exame.IdEmpregado.nID_EMPR) + "<br>" +
                                      "Clinica: " + exame.IdJuridica.NomeAbreviado + "<br>" +
                                      "Endereço da clinica: " + xClinica.GetEndereco().IdTipoLogradouro.NomeCompleto + " " + xClinica.GetEndereco().Logradouro + " " + xClinica.GetEndereco().Numero + " " + xClinica.GetEndereco().Bairro + " " + xClinica.GetEndereco().Municipio + "/" + xClinica.GetEndereco().Uf + "   Fone: " + exame.IdJuridica.GetContatoTelefonico() + "<br>" +
                                      "Tipo de Exame: " + exame.IdExameDicionario.Nome + " <br>" +
                                      "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";


                    //string xCorpo = "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> </p>" +
                    //                 "<p style='font-family:Tahoma;font-size:18px;text-align:center;text-decoration:underline;color:black> DADOS DO AGENDAMENTO: </p><br>" +
                    //                 "<p><font size='3' face='Tahoma'>Nome: " + exame.IdEmpregado.tNO_EMPG + "<br>" +                             
                    //                  "Data do Exame: " + exame.DataExame.ToString("dd/MM/yyyy") + " </font></p></body>";



                    if (xId.IndexOf("KIT") >= 0)
                    {
                        //procurar tblConvocacao e enviar e-mail com kit para colaborador

                        //attach arquivo - por exemplo i:\\temp\\guia_KIT_962020797.pdf   ( guia_ + ID + .pdf )



                        Convocacao rConvocacao = new Convocacao();
                        rConvocacao.Find(System.Convert.ToInt32(xId.Substring(4)));




                        string zEmail = "";

                        if (rConvocacao.eMail_Envio.IndexOf("|") > 0)
                            zEmail = rConvocacao.eMail_Envio.Substring(0, rConvocacao.eMail_Envio.IndexOf("|") - 1).Trim();
                        else
                            zEmail = rConvocacao.eMail_Envio;



                        rConvocacao.hDt_Convocacao = exame.DataExame;
                        rConvocacao.Save();








                    }


                }



            }
            catch (Exception ex)
            {
                if (txt_Status.Trim() == "")
                    txt_Status = "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;
                else
                    txt_Status = txt_Status + System.Environment.NewLine + "Erro: Carga do XML: " + ex.Message + System.Environment.NewLine;
                //Session["zErro"] = txt_Status;

                return txt_Status;

            }
            finally
            {

                if (txt_Status != "")
                {
                    //Session["zErro"] = txt_Status;


                }
                else
                {
                    // rClinica.IdClinica.Find();



                    //Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
                    //Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia_Auto.aspx?IdEmpregado=" + zId.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + xCodUsuario + "&TipoExame=" + xTipoExame + "&Data=" + xData + "&IdClinica=" + rClinica.IdClinica.Id.ToString().Trim() + "&CodUsuario=" + xCodUsuario + "&xId=" + xID);
                    // HttpContext.Current.Response.Redirect("~\\PCMSO\\DadosEmpregado_Guia_Auto.aspx?IdEmpregado=" + zId.ToString().Trim() + "&IdEmpresa=" + rEmpresa.Id.ToString().Trim() + "&IdUsuario=" + CodUsuario + "&TipoExame=" + TipoExame + "&Data=" + Data + "&IdClinica=" + rClinica.IdClinica.Id.ToString().Trim() + "&CodUsuario=" + CodUsuario + "&xId=" + ID + "&xIdExame=4&xArq=" + xArq);


                    //Session["zErro"] = txt_Status + " " + "Processamento Finalizado";

                    txt_Status = "01 - Processamento Finalizado";

                }
            }

            //string zErro = "";

            //xArq = "I:\\temp\\ws_" + xArq + ".txt";

            //if (File.Exists(xArq))
            //    zErro = File.ReadAllText(xArq);

            return txt_Status;

        }





        private void Carregar_Dados_Anamnese_Exame(Int32 zExame)
        {


            if (zExame == 0)
            {
                return;
            }



            List<Anamnese_Exame> AnExame = new Anamnese_Exame().Find<Anamnese_Exame>(" IdExameBase = " + zExame);


            Clinico vExame = new Clinico();
            vExame.Find(zExame);


            if (AnExame.Count == 0)
            {
                //trazer padrão para cliente
                vExame.IdEmpregado.Find();
                vExame.IdEmpregado.nID_EMPR.Find();
                List<Anamnese_Dinamica> anExamePadrao = new Anamnese_Dinamica().Find<Anamnese_Dinamica>(" IdPessoa = " + vExame.IdEmpregado.nID_EMPR.Id);


                if (anExamePadrao.Count == 0)
                {
                    return;
                }
                else
                {
                    Ilitera.Data.Clientes_Funcionarios xAnam = new Ilitera.Data.Clientes_Funcionarios();
                    xAnam.Carregar_Anamnese_Dinamica(vExame.IdEmpregado.nID_EMPR.Id, zExame);


                    //foreach (Anamnese_Dinamica zPadrao in anExamePadrao)
                    //{
                    //    Anamnese_Exame rTestes = new Anamnese_Exame();

                    //    rTestes.IdAnamneseDinamica = zPadrao.Id;
                    //    rTestes.IdExameBase = vExame.Id;
                    //    rTestes.Resultado = "N";
                    //    rTestes.Peso = zPadrao.Peso;
                    //    rTestes.Save();
                    //}

                }

            }


            return;


        }



        protected void Envio_Email_Prajna(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresaExameClinicoFacade, ExameClinicoFacade exame, string xCodUsuario, Int32 xIdEmpresa)
        {

            string xDestinatario = "";

            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email           
            objEmail.From = new MailAddress("agendamento@5aessence.com.br");


            //para
            string xEmail = "";

            xEmail = exame.IdJuridica.Email.ToString().Trim();

            if (xEmail == "")
            {
                throw new Exception("Clínica não possui e-mail cadastrado.");
            }

            if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

            objEmail.To.Add(xEmail);
            xDestinatario = xEmail + "; agendamento@5aessence.com.br ;";

            objEmail.CC.Add("agendamento@5aessence.com.br");


            //cópia para usuário logado
            //Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];
            Entities.Usuario usuario = new Entities.Usuario();
            usuario.IdUsuario = System.Convert.ToInt32(xCodUsuario);


            Prestador xPrestador = new Prestador(usuario.IdPrestador);


            if (xPrestador.Email != null)
            {
                xEmail = xPrestador.Email.ToString().Trim();
                if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

                if (xEmail.Trim() != "")
                {
                    objEmail.Bcc.Add(xEmail);
                    xDestinatario = xDestinatario + xEmail + ";";
                }
            }

            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            Attachment xItem = new Attachment(xAttach);
            objEmail.Attachments.Add(xItem);

            SmtpClient objSmtp = new SmtpClient();
            //objSmtp.Host = "mail.exchange.locaweb.com.br";
            //objSmtp.Port = 587;
            //objSmtp.Credentials = new NetworkCredential("agendamento@essencenet.com.br", "kr.prj1705");

            objSmtp.EnableSsl = true;

            //objSmtp.Host = "outlook.office.com";
            objSmtp.Host = "smtp.office365.com";
            objSmtp.Port = 587;
            objSmtp.Credentials = new NetworkCredential("agendamento@5aessence.com.br", "Agend_5060");

            objSmtp.Send(objEmail);

            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Kit Guia / ASO / PCI");

            return;

        }




        protected void Envio_Email_Ilitera(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            objEmail.From = new MailAddress("agendamento.sp.sto@ilitera.com.br");


            //caixa-postal de onde será enviado o e-mail
            //objEmail.ReplyTo = new MailAddress("email@seusite.com.br");

            //para
            //objEmail.To.Add("lasdowsky@gmail.com");
            //string xEmail = "";

            //xEmail = exame.IdJuridica.Email.ToString().Trim();

            //if (xEmail == "")
            //{
            //    throw new Exception("Clínica não possui e-mail cadastrado.");
            //}

            //if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

            //objEmail.To.Add(xEmail);

            objEmail.To.Add(xPara);
            //xDestinatario = xEmail + "; atendimento@ilitera.com.br ;";

            //objEmail.Bcc.Add("oculto@provedor.com.br");

            //objEmail.CC.Add("atendimento@ilitera.com.br");
            //objEmail.CC.Add("atendimento2@ilitera.com.br");

            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            Attachment xItem = new Attachment(xAttach);
            objEmail.Attachments.Add(xItem);

            SmtpClient objSmtp = new SmtpClient();

            //objSmtp.Host = "smtp.ilitera.com.br";
            objSmtp.Host = "smtp.office365.com";
            objSmtp.EnableSsl = true;
            objSmtp.Port = 587;
            objSmtp.Credentials = new NetworkCredential("agendamento.sp.sto@ilitera.com.br", "bibi6096");


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            //checar se não é e-mail duplicado

            //DataSet rDs = xEnvio.Checar_Envio_Email(xIdEmpregado, xIdEmpresa, xDestinatario, "Kit Guia / ASO / PCI");

            //if (rDs.Tables[0].Rows.Count == 0)
            //{
            objSmtp.Send(objEmail);
            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Kit Guia / ASO / PCI");
            //}


            return;

        }



        private string PopularValueListClinicaClienteExameDicionario(string xValue, string xIdExame, string xIdEmpresa, string xIdEmpregado, string xCodUsuario, string xData)
        {

            string lst_Exames = "";

            DataSet dsExames = new ExameDicionario().GetIdNome("Nome", " IdExameDicionario IN (SELECT IdExameDicionario FROM ClinicaExameDicionario WHERE IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + xIdEmpresa + " and IdClinica = " + xValue + " ))");
            DataSet ds = new ClinicaExameDicionario().Get("IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + xIdEmpresa + " " + " and IdClinica = " + xValue + " ) " + " AND IDCLINICAEXAMEDICIONARIO IN " +
             "( " +
             "   SELECT IdClinicaExameDicionario " +
             "   FROM ClinicaClienteExameDicionario  " +
             "    WHERE IdClinicaCliente IN ( " +
             "      SELECT IdClinicaCliente FROM ClinicaCliente " +
             "      WHERE IdCliente=" + xIdEmpresa + " " + " and IdClinica = " + xValue + " and IsAutorizado = 1 ) ) ");


            ////carregar dados da clinica
            Clinica xClinica = new Clinica(System.Convert.ToInt32(xValue));



            //pegar exames de PCMSO do funcionário
            Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();
            empregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + xIdEmpregado);

            Clinico exame = new Clinico();
            exame.IdEmpregado = empregado;
            exame.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
            exame.IdPcmso = exame.IdEmpregadoFuncao.nID_EMPR.GetUltimoPcmso();

            exame.UsuarioId = System.Convert.ToInt32(xCodUsuario);

            Pcmso pcmso = new Pcmso();
            pcmso = exame.IdPcmso;

            bool zClinico = false;


            if (pcmso.IdLaudoTecnico != null)
            {
                List<Ghe> ghes = new Ghe().Find<Ghe>("nID_LAUD_TEC=" + pcmso.IdLaudoTecnico.Id);

                Ghe ghe;

                if (ghes == null || ghes.Count == 0)
                    ghe = exame.IdEmpregadoFuncao.GetGheEmpregado(pcmso.IdLaudoTecnico);
                else
                {
                    int IdGhe = exame.IdEmpregadoFuncao.GetIdGheEmpregado(pcmso.IdLaudoTecnico);

                    ghe = ghes.Find(delegate (Ghe g) { return g.Id == IdGhe; });
                }



                if (ghe == null)
                {
                    return "-10   Colaborador não está alocado em GHE,  não é possível criar Guia de Encaminhamento/ASO.";
                }



                bool zDesconsiderar = false;
                string xDataBranco = "";

                string sExamesOcupacionais = "";

                Cliente cliente = new Cliente();
                cliente.Find(System.Convert.ToInt32(xIdEmpresa));


                //Wagner 04/07/2018 - ver exames complementares que estão com data ( ver se opção exibir data complementares está ativa )
                // esses exames com datas não precisam aparecer na guia, pois não precisam ser solicitados.

                //se for para desconsiderar data de complementares a partir de certa data, usar o esquema que já existe com xDataBranco
                if (xDataBranco == "")
                {
                    if (cliente.Ativar_DesconsiderarCompl == true)
                    {
                        if (cliente.Dias_Desconsiderar > 0)
                        {
                            zDesconsiderar = true;
                        }
                    }
                }
                else
                {
                    zDesconsiderar = false;
                }


                //Clinico clinico = new Clinico();

                //clinico.IdPcmso = pcmso;
                //clinico.IdEmpregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(pcmso.IdLaudoTecnico, empregado);


                //clinico.UsuarioId = System.Convert.ToInt32(Request["IdUsuario"].ToString());
                ExameDicionario rDicionario = new ExameDicionario();

                rDicionario.Find(System.Convert.ToInt32(xIdExame));

                exame.IdExameDicionario = rDicionario;

                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                exame.DataExame = System.Convert.ToDateTime(xData, ptBr);



                //pegar exames do ASO
                string sExamesASO = "";
                string sExamesASO_Aptidao = "";


                if (xIdExame == "3")   //mudança de função
                {
                    // procurar ghe_ant primeiro, na mesma classif.funcional
                    // se nao encontrar, procurar classif.funcional anterior e ghe


                    if (cliente.GHEAnterior_MudancaFuncao == true)
                    {
                        // procurar ghe_ant primeiro, na mesma classif.funcional
                        // se nao encontrar, procurar classif.funcional anterior e ghe

                        Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

                        DataSet xdS = xGHE.Trazer_Laudos_GHEs_Colaborador(empregado.Id);

                        if (xdS.Tables[0].Rows.Count < 2)
                        {
                            return "-14  O empregado " + empregado.tNO_EMPG + " está associado a apenas um GHE/Classif.Funcional, logo, não é possível gerar o ASO de Mudança de Função. ";
                        }


                        int znAux = 0;
                        Int32 zGHE_Atual = 0;
                        Int32 zGHE_Ant = 0;


                        foreach (DataRow row in xdS.Tables[0].Rows)
                        {
                            znAux++;

                            if (znAux == 1) zGHE_Atual = System.Convert.ToInt32(row["nId_Func"].ToString());
                            else if (znAux == 2) zGHE_Ant = System.Convert.ToInt32(row["nId_Func"].ToString());
                            else break;

                        }


                        //ghe_ant
                        //ghe
                        Ghe zGhe1 = new Ghe();
                        zGhe1.Find(zGHE_Atual);
                        Ghe zGhe2 = new Ghe();
                        zGhe2.Find(zGHE_Ant);

                        if (zDesconsiderar == false)
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao(zGhe1, zGhe2, false, cliente.Exibir_Datas_Exames_ASO, xDataBranco, zDesconsiderar);
                        else
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao_Desconsiderar(zGhe1, zGhe2, false, cliente.Exibir_Datas_Exames_ASO, exame, cliente.Dias_Desconsiderar);
                    }
                    else
                    {
                        if (zDesconsiderar == false)
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado(ghe, false, cliente.Exibir_Datas_Exames_ASO, xDataBranco, zDesconsiderar);
                        else
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Desconsiderar(ghe, false, cliente.Exibir_Datas_Exames_ASO, exame, cliente.Dias_Desconsiderar);
                    }


                }
                else
                {

                    if (xIdExame == "2")  //demissional
                    {
                        if (zDesconsiderar == false)
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado(ghe, false, false, xDataBranco, zDesconsiderar);
                        else
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Desconsiderar(ghe, false, false, exame, cliente.Dias_Desconsiderar);
                    }
                    else
                    {
                        if (zDesconsiderar == false)
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado(ghe, false, cliente.Exibir_Datas_Exames_ASO, xDataBranco, zDesconsiderar);
                        else
                            sExamesASO = exame.GetPlanejamentoExamesAso_Formatado_Desconsiderar(ghe, false, cliente.Exibir_Datas_Exames_ASO, exame, cliente.Dias_Desconsiderar);
                    }
                }





                //pegar exames para guia
                if (xIdExame == "3")   //mudança de função
                {

                    if (cliente.GHEAnterior_MudancaFuncao == true)
                    {
                        // procurar ghe_ant primeiro, na mesma classif.funcional
                        // se nao encontrar, procurar classif.funcional anterior e ghe

                        Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

                        DataSet xdS = xGHE.Trazer_Laudos_GHEs_Colaborador(empregado.Id);

                        if (xdS.Tables[0].Rows.Count < 2)
                        {
                            return "-13  O empregado " + empregado.tNO_EMPG + " está associado a apenas um GHE/Classif.Funcional, logo, não é possível gerar o ASO de Mudança de Função. ";
                        }


                        int znAux = 0;
                        Int32 zGHE_Atual = 0;
                        Int32 zGHE_Ant = 0;


                        foreach (DataRow row in xdS.Tables[0].Rows)
                        {
                            znAux++;

                            if (znAux == 1) zGHE_Atual = System.Convert.ToInt32(row["nId_Func"].ToString());
                            else if (znAux == 2) zGHE_Ant = System.Convert.ToInt32(row["nId_Func"].ToString());
                            else break;

                        }


                        //ghe_ant
                        //ghe
                        Ghe zGhe1 = new Ghe();
                        zGhe1.Find(zGHE_Atual);
                        Ghe zGhe2 = new Ghe();
                        zGhe2.Find(zGHE_Ant);


                        sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Formatado_Mudanca_Funcao_Guia(zGhe1, zGhe2, true);
                    }
                    else
                    {
                        sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "M");
                    }


                }
                else if (xIdExame == "1")  // admissao
                {
                    sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "A");
                }
                else if (xIdExame == "2")  // demissao
                {
                    sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "D");
                }
                else if (xIdExame == "5")  // retorno
                {
                    sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "R");
                }
                else if (xIdExame == "4")   //periódico
                {
                    sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true, "P");
                }
                else
                {
                    sExamesOcupacionais = exame.GetPlanejamentoExamesAso_Guia(ghe, true);
                }



                //checar se têm exames de aptidão
                string sExamesOcupacionais_Aptidao = "";

                Empregado_Aptidao xAptidao = new Empregado_Aptidao();
                xAptidao.Find(" nId_Empregado = " + empregado.Id.ToString());


                GHE_Aptidao zAptidao = new GHE_Aptidao();
                zAptidao.Find("nId_Func = " + ghe.Id.ToString());

                if (xAptidao.Id != 0 || zAptidao.Id != 0)
                {
                    if ((xAptidao.apt_Alimento == true || xAptidao.apt_Aquaviario == true || xAptidao.apt_Eletricidade == true || xAptidao.apt_Espaco_Confinado == true ||
                              xAptidao.apt_Submerso == true || xAptidao.apt_Trabalho_Altura == true || xAptidao.apt_Transporte == true || xAptidao.apt_Brigadista == true || xAptidao.apt_Socorrista == true) ||
                              (zAptidao.apt_Alimento == true || zAptidao.apt_Aquaviario == true || zAptidao.apt_Eletricidade == true || zAptidao.apt_Espaco_Confinado == true ||
                             zAptidao.apt_Submerso == true || zAptidao.apt_Trabalho_Altura == true || zAptidao.apt_Transporte == true || zAptidao.apt_Brigadista == true || zAptidao.apt_Socorrista == true))
                    {

                        Empregado_Aptidao nAptidao = new Empregado_Aptidao();


                        nAptidao.nId_Empregado = empregado.Id;

                        //juntando aptidao do empregado com do PPRA-GHE
                        if (xAptidao.Id != 0 && zAptidao.Id != 0)
                        {
                            nAptidao.apt_Alimento = xAptidao.apt_Alimento || zAptidao.apt_Alimento;
                            nAptidao.apt_Aquaviario = xAptidao.apt_Aquaviario || zAptidao.apt_Aquaviario;
                            nAptidao.apt_Brigadista = xAptidao.apt_Brigadista || zAptidao.apt_Brigadista;
                            nAptidao.apt_Eletricidade = xAptidao.apt_Eletricidade || zAptidao.apt_Eletricidade;
                            nAptidao.apt_Espaco_Confinado = xAptidao.apt_Espaco_Confinado || zAptidao.apt_Espaco_Confinado;
                            nAptidao.apt_Socorrista = xAptidao.apt_Socorrista || zAptidao.apt_Socorrista;
                            nAptidao.apt_Submerso = xAptidao.apt_Submerso || zAptidao.apt_Submerso;
                            nAptidao.apt_Trabalho_Altura = xAptidao.apt_Trabalho_Altura || zAptidao.apt_Trabalho_Altura;
                            nAptidao.apt_Transporte = xAptidao.apt_Transporte || zAptidao.apt_Transporte;
                        }
                        else if (xAptidao.Id != 0)
                        {
                            nAptidao.apt_Alimento = xAptidao.apt_Alimento;
                            nAptidao.apt_Aquaviario = xAptidao.apt_Aquaviario;
                            nAptidao.apt_Brigadista = xAptidao.apt_Brigadista;
                            nAptidao.apt_Eletricidade = xAptidao.apt_Eletricidade;
                            nAptidao.apt_Espaco_Confinado = xAptidao.apt_Espaco_Confinado;
                            nAptidao.apt_Socorrista = xAptidao.apt_Socorrista;
                            nAptidao.apt_Submerso = xAptidao.apt_Submerso;
                            nAptidao.apt_Trabalho_Altura = xAptidao.apt_Trabalho_Altura;
                            nAptidao.apt_Transporte = xAptidao.apt_Transporte;
                        }
                        else if (zAptidao.Id != 0)
                        {
                            nAptidao.apt_Alimento = zAptidao.apt_Alimento;
                            nAptidao.apt_Aquaviario = zAptidao.apt_Aquaviario;
                            nAptidao.apt_Brigadista = zAptidao.apt_Brigadista;
                            nAptidao.apt_Eletricidade = zAptidao.apt_Eletricidade;
                            nAptidao.apt_Espaco_Confinado = zAptidao.apt_Espaco_Confinado;
                            nAptidao.apt_Socorrista = zAptidao.apt_Socorrista;
                            nAptidao.apt_Submerso = zAptidao.apt_Submerso;
                            nAptidao.apt_Trabalho_Altura = zAptidao.apt_Trabalho_Altura;
                            nAptidao.apt_Transporte = zAptidao.apt_Transporte;
                        }

                        Cliente xCliente = new Cliente();
                        xCliente.Find(pcmso.IdCliente.Id);

                        ExameDicionario rDic = new ExameDicionario();
                        rDic.Find(System.Convert.ToInt32(xIdExame));


                        exame.IdExameDicionario = rDic;
                        exame.IdEmpregado = empregado;


                        sExamesOcupacionais_Aptidao = exame.GetPlanejamentoExamesAso_Guia_Aptidao(nAptidao, xCliente.Exibir_Datas_Exames_ASO, "F", sExamesOcupacionais);

                        sExamesASO_Aptidao = exame.GetPlanejamentoExamesAso_Formatado_Aptidao(nAptidao, xCliente.Exibir_Datas_Exames_ASO, xDataBranco, sExamesOcupacionais, zDesconsiderar, exame);

                    }
                }

                if (sExamesOcupacionais.Trim() != "")
                    sExamesOcupacionais = sExamesOcupacionais + sExamesOcupacionais_Aptidao;
                else
                    sExamesOcupacionais = sExamesOcupacionais_Aptidao;

                string txt_Exames = sExamesOcupacionais;



                if (sExamesASO.Trim() != "")
                    sExamesASO = sExamesASO + sExamesASO_Aptidao;
                else
                    sExamesASO = sExamesASO_Aptidao;


                //bool zSelecao = true;





                //Prajna quer que apareça clinico se ele realmente estiver no planejamento
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") < 0)
                {
                    lst_Exames = lst_Exames + "Exame Clínico" + "|";
                }


                //pegar apenas complementar da convocação - kit
                if (xIdExame != "4")
                {


                    int xPosit = 0;
                    DataRow[] rows = dsExames.Tables[0].Select("Id=" + xIdExame);

                    //valueListClinicaClienteExameDicionario.ValueListItems.Add(Convert.ToInt32(row[0]), Convert.ToString(rows[0]["Nome"]));
                    if (rows.Count() > 0)
                    {

                        // retirar admissional, periodico, retorno ao trab., complement. desta lista
                        if (rows[0]["Nome"].ToString().Trim().ToUpper() != "RETORNO AO TRABALHO" &&
                            rows[0]["Nome"].ToString().Trim().ToUpper() != "DEMISSIONAL" &&
                            rows[0]["Nome"].ToString().Trim().ToUpper() != "ADMISSIONAL" &&
                            rows[0]["Nome"].ToString().Trim().ToUpper() != "MUDANÇA DE FUNÇÃO" &&
                            rows[0]["Nome"].ToString().Trim().ToUpper() != "PERIÓDICO")
                        {

                            //  lst_Exames.Items.Add(Convert.ToString(rows[0]["Nome"]));

                            if (sExamesOcupacionais.ToUpper().IndexOf(Convert.ToString(rows[0]["Nome"]).ToUpper()) > 0)
                            {
                                //poderia procurar o exame, e ver se foi feito e está com resultado.  Se sim, não selecionar
                                //precisaria usar a data de planejamento deste exame

                                xPosit = sExamesASO.ToUpper().IndexOf(Convert.ToString(rows[0]["Nome"]).ToUpper());

                                if (xPosit > 0)
                                {
                                    if (sExamesASO.Substring(xPosit + 38, 12).Replace("/", " ").Trim() == "")
                                        xPosit = 0;

                                }

                                if (xPosit == 0)
                                {
                                    lst_Exames = lst_Exames + Convert.ToString(rows[0]["Nome"]) + "|";
                                }
                            }

                        }


                    }

                }
                else
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        int xPosit = 0;
                        DataRow[] rows = dsExames.Tables[0].Select("Id=" + Convert.ToInt32(row["IdExameDicionario"]));

                        //valueListClinicaClienteExameDicionario.ValueListItems.Add(Convert.ToInt32(row[0]), Convert.ToString(rows[0]["Nome"]));
                        if (rows.Count() > 0)
                        {

                            // retirar admissional, periodico, retorno ao trab., complement. desta lista
                            if (rows[0]["Nome"].ToString().Trim().ToUpper() != "RETORNO AO TRABALHO" &&
                                rows[0]["Nome"].ToString().Trim().ToUpper() != "DEMISSIONAL" &&
                                rows[0]["Nome"].ToString().Trim().ToUpper() != "ADMISSIONAL" &&
                                rows[0]["Nome"].ToString().Trim().ToUpper() != "MUDANÇA DE FUNÇÃO" &&
                                rows[0]["Nome"].ToString().Trim().ToUpper() != "PERIÓDICO")
                            {

                                //  lst_Exames.Items.Add(Convert.ToString(rows[0]["Nome"]));

                                if (sExamesOcupacionais.ToUpper().IndexOf(Convert.ToString(rows[0]["Nome"]).ToUpper()) > 0)
                                {
                                    //poderia procurar o exame, e ver se foi feito e está com resultado.  Se sim, não selecionar
                                    //precisaria usar a data de planejamento deste exame

                                    xPosit = sExamesASO.ToUpper().IndexOf(Convert.ToString(rows[0]["Nome"]).ToUpper());

                                    if (xPosit > 0)
                                    {
                                        if (sExamesASO.Substring(xPosit + 38, 12).Replace("/", " ").Trim() == "")
                                            xPosit = 0;

                                    }

                                    if (xPosit == 0)
                                    {
                                        lst_Exames = lst_Exames + Convert.ToString(rows[0]["Nome"]) + "|";

                                    }
                                }

                            }
                            else if (rows[0]["Nome"].ToString().Trim().ToUpper() == "PERIÓDICO")
                            {
                                zClinico = true;
                            }

                        }

                    }
                }


            }



            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") >= 0)
            {
                if (zClinico == true)
                {

                    lst_Exames = lst_Exames + "Exame Clínico" + "|";
                }
            }



            return lst_Exames;



        }








        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion
    }
}

