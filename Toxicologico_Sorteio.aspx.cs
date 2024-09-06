using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Ilitera.Opsa.Data;
using System.Text;
using Entities;
using System.IO;
using Ilitera.Common;
using CrystalDecisions.CrystalReports.Engine;

namespace Ilitera.Net
{
    public partial class Toxicologico_Sorteio : System.Web.UI.Page
    {


        protected  Entities.Usuario usuario = new Entities.Usuario();
        protected Cliente cliente = new Cliente();

       


        protected void Page_Load(object sender, EventArgs e)
        {

            cmd_Novo_Sorteio.Attributes.Add("onClick", "javascript:return(confirm('Deseja criar novo sorteio ?'))");
            cmd_Realizar_Sorteio.Attributes.Add("onClick", "javascript:return(confirm('Confirma realização do Sorteio ?'))");

            try
            {
                if (Session["Empresa"] != null && Session["Empresa"].ToString() != String.Empty)
                {
                    //InicializaWebPageObjects(true);

                    if (!IsPostBack)
                    {

                        try
                        {
                            string FormKey = "Toxicologico_Sorteio_aspx";

                            Ilitera.Common.Funcionalidade funcionalidade = new Ilitera.Common.Funcionalidade();
                            funcionalidade.Find("ClassName='" + FormKey + "'");

                            Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];
                            Ilitera.Common.Usuario.Permissao_Web(xUser.IdUsuario, funcionalidade.Id);

                        }
                        catch (Exception ex)
                        {
                            Session["Message"] = ex.Message;
                            Server.Transfer("~/Tratar_Excecao.aspx");
                            return;

                        }

                        //try
                        //{
                        //    string FormKey = this.Page.ToString().Substring(4);

                        //    Ilitera.Common.Funcionalidade funcionalidade = new Ilitera.Common.Funcionalidade();
                        //    funcionalidade.Find("ClassName='" + FormKey + "'");

                        //    if (funcionalidade.Id == 0)
                        //        throw new Exception("Formulário não cadastrado - " + FormKey);

                        //    Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];
                        //    Ilitera.Common.Usuario.Permissao_Web(xUser.IdUsuario, funcionalidade.Id);
                        //}

                        //catch (Exception ex)
                        //{
                        //    Session["Message"] = ex.Message;
                        //    Server.Transfer("~/Tratar_Excecao.aspx");
                        //    return;
                        //}

                        cliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));


                        PopulaSorteios();
                        PopulaDDLClinicas();

                        Limpar_Campos();

                        Carregar_Grid_Colaboradores_Ativos_Com_Email(0);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }



        private void Carregar_Grid_Colaboradores_Ativos_Com_Email(Int32 xIdSorteio)
        {
            Ilitera.Data.PPRA_EPI xEmpregados = new Ilitera.Data.PPRA_EPI();


            grd_Colaboradores_Ativos.DataSource = xEmpregados.Trazer_Colaboradores_Toxicologico( System.Convert.ToInt32( Request["IdEmpresa"].ToString()), xIdSorteio); //new Ilitera.Opsa.Data.Empregado().Get(" nId_Empr=" + Request["IdEmpresa"].ToString() + " and tEmail<>''  ");
            grd_Colaboradores_Ativos.DataBind();
        }


        private void Carregar_Grid_Colaboradores_Participantes_Sorteio(Int32 xIdSorteio)
        {
            Ilitera.Data.PPRA_EPI xEmpregados = new Ilitera.Data.PPRA_EPI();


            grd_Lista_Sorteio.DataSource = xEmpregados.Trazer_Colaboradores_Toxicologico_Participantes(System.Convert.ToInt32(Request["IdEmpresa"].ToString()), xIdSorteio);
            grd_Lista_Sorteio.DataBind();
        }

        private void Carregar_Grid_Colaboradores_Participantes_Sorteados( Int32 xIdSorteio)
        {
            Ilitera.Data.PPRA_EPI xEmpregados = new Ilitera.Data.PPRA_EPI();


            grd_Sorteados.DataSource = xEmpregados.Trazer_Colaboradores_Toxicologico_Sorteados(System.Convert.ToInt32(Request["IdEmpresa"].ToString()), xIdSorteio);
            grd_Sorteados.DataBind();
        }


        private void PopulaDDLClinicas()
        {
           
            ddlClinica.DataSource = new Clinica().Get("((IdClinica IN (SELECT IdClinica FROM ClinicaCliente WHERE IdCliente=" + Request["IdEmpresa"].ToString() + " )"
                 + " AND IdClinica IN (SELECT distinct IdClinica FROM ClinicaExameDicionario (nolock) )"
                 + " AND IdJuridicaPapel=" + (int)IndJuridicaPapel.Clinica + ")"
                 + " OR (IdClinica IN (SELECT IdClinica FROM ClinicaCliente (nolock) WHERE IdCliente=" + Request["IdEmpresa"].ToString() + ")"
                 + " AND IdJuridicaPapel=" + (int)IndJuridicaPapel.ClinicaOutras + "))"
                 + " AND IsInativo=0 ORDER BY NomeAbreviado");
            ddlClinica.DataValueField = "IdClinica";
            ddlClinica.DataTextField = "NomeAbreviado";
            ddlClinica.DataBind();

            ddlClinica.Items.Insert(0, new ListItem("Selecione...", "0"));
            
        }


        private void PopulaSorteios()
        {

            ddlSorteios.DataSource = new ToxicologicoSorteio().Get(" IdCliente =" + Request["IdEmpresa"].ToString() + " ORDER BY Data_Criacao desc");
            ddlSorteios.DataValueField = "Id_Toxicologico_Sorteio";
            ddlSorteios.DataTextField = "Data_Criacao";
            ddlSorteios.DataBind();

            ddlSorteios.Items.Insert(0, new ListItem("Selecione...", "0"));
            
        }


        private void AbrePCMSO(string page)
        {

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

        }

        protected void cmd_Realizar_Sorteio_Click(object sender, EventArgs e)
        {
            //pela posição no grid numerar de 1 a 20 por exemplo,  sorteio será pelo resultado de randomico(1,20), se repetir um já selecionado, executa novamente.

            // salvar registros em Toxicologico_Sorteio_Participantes com todos participantes e indicando selecionados
            // criar exames no sistema
            // enviar e-mails


        }

        protected void grd_Lista_Sorteio_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {

        }

        protected void grd_Colaboradores_Ativos_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {

            
            
        }

        protected void grd_Sorteados_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {

        }

        protected void cmd_Salvar_Click(object sender, EventArgs e)
        {
            // sorteio 
            // pela posição do grid, exemplo 1 a 20, chamar rand( 1,20) quantas vezes for necessária para selecionar pela posição do grid.  
            // Se repetir, apenas ignorar e chamar de novo até número de colaboradores for atingido.
            // 1.criar exames complementares
            // 2.criar guia encaminhamento
            // 3.enviar e-mails
            // ToxicologicoSorteio.finalizado = 1
            // Recarregar tela

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];
            
            if ( ddlClinica.SelectedIndex < 1)
            {
                MsgBox1.Show("Ilitera.Net", "Selecione clínica.", null,
                new EO.Web.MsgBoxButton("OK"));

                return;
            }

            try
            {
                ToxicologicoSorteio xSorteio = new ToxicologicoSorteio();
                xSorteio.IdJuridica = System.Convert.ToInt32( ddlClinica.SelectedValue );
                xSorteio.Codigo_Sorteio = txt_Codigo.Text;
                xSorteio.Data_Criacao = DateTime.Now;
                xSorteio.Colaboradores_Sorteados = System.Convert.ToInt16(txt_Colaboradores.Text);
                xSorteio.IdUsuario = usuario.IdUsuario;
                xSorteio.IdCliente = System.Convert.ToInt32(Request["IdEmpresa"].ToString());
                xSorteio.Finalizado = false;                
                xSorteio.Save();


                PopulaSorteios();
                ddlSorteios.Enabled = true;
                cmd_Novo_Sorteio.Enabled = true;
                
                Limpar_Campos();

                cmd_Carregar.Enabled = true;

            }
            catch ( Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Erro(1):" + ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));
            }


        }


        protected void Limpar_Campos()
        {

            ddlClinica.SelectedIndex = 0;
            ddlClinica.Enabled = false;

            txt_Codigo.Text = "";
            txt_Codigo.Enabled = false;

            txt_Colaboradores.Text = "0";
            txt_Colaboradores.Enabled = false;

            txt_Data.Text = "";
            txt_Data.Enabled = false;

            cmd_Add.Enabled = false;
            cmd_Remove.Enabled = false;

            cmd_Ultimo_Sorteio.Enabled = false;
            cmd_Certificado_Participacao.Enabled = false;
            cmd_Realizar_Sorteio.Enabled = false;

            grd_Colaboradores_Ativos.DataSource = null;
            grd_Lista_Sorteio.DataSource = null;
            grd_Sorteados.DataSource = null;

            return;   

        }

        protected void cmd_Novo_Sorteio_Click(object sender, EventArgs e)
        {

            // checar se tem Sorteio criado mas não finalizado - não permitir criar se tiver sorteio aberto

            ArrayList xSorteios = new ToxicologicoSorteio().Find(" IdCliente = " + Request["IdEmpresa"].ToString() + " and Finalizado = 0 ");

            if (xSorteios.Count > 0)
            {
                MsgBox1.Show("Ilitera.Net", "Existem sorteios não finalizados desse cliente", null,
                new EO.Web.MsgBoxButton("OK"));

                return;
            }


            // carregar apenas colaboradores com e-mails na lista de disponíveis
            // habilitar campos

            try
            {


                cmd_Novo_Sorteio.Enabled = false;

                cmd_Carregar.Enabled = false;

                ddlSorteios.Enabled = false;

                PopulaDDLClinicas();
                ddlClinica.SelectedIndex = 0;
                ddlClinica.Enabled = true;

                txt_Codigo.Text = "IL_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00");
                txt_Codigo.Enabled = false;

                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                txt_Data.Text = DateTime.Now.Date.ToString("dd/MM/yyyy", ptBr);
                txt_Data.Enabled = true;

                cmd_Salvar.Enabled = true;

                txt_Colaboradores.Text = "0";
                txt_Colaboradores.Enabled = true;



                Carregar_Grid_Colaboradores_Ativos_Com_Email(0);

                Carregar_Grid_Colaboradores_Participantes_Sorteio(0);

                Carregar_Grid_Colaboradores_Participantes_Sorteados(0);

            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Erro(2):" + ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));
            }

            return;

        }

        protected void ddlSorteios_SelectedIndexChanged(object sender, EventArgs e)
        {

            // se selecionar sorteio finalizado, apenas exibir campos e deixar botão para certificados aberto
            // se selecionado sorteio aberto, habilitar grids e botão de Sorteio

        }

        protected void cmd_Carregar_Sorteio_Click(object sender, EventArgs e)
        {
            //verificar se está finalizado ou não o sorteio.
            Int32 xIdSorteio = 0;

            try
            {

                if (ddlSorteios.SelectedIndex < 0)
                {
                    MsgBox1.Show("Ilitera.Net", "Selecione Sorteio salvo.", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }

                xIdSorteio = System.Convert.ToInt32(ddlSorteios.SelectedValue);

                ToxicologicoSorteio xSorteio = new ToxicologicoSorteio();

                xSorteio.Find(xIdSorteio);

                if (xSorteio.Id == 0)
                {
                    MsgBox1.Show("Ilitera.Net", "Erro na carga do Sorteio.", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }


                ddlSorteios.Enabled = false;

                //carregar campos e grids, bloquear campos que não podem ser alterados
                txt_Codigo.Text = xSorteio.Codigo_Sorteio;
                txt_Colaboradores.Text = xSorteio.Colaboradores_Sorteados.ToString();

                
                ddlClinica.SelectedValue = xSorteio.IdJuridica.ToString();


                if (xSorteio.Finalizado == true)
                {
                    //bloquear campos
                    cmd_Add.Enabled = false;
                    cmd_Remove.Enabled = false;
                    cmd_Realizar_Sorteio.Enabled = false;
                    cmd_Certificado_Participacao.Enabled = true;
                    cmd_Guias.Enabled = true;
                    cmd_Imprimir_Guias.Enabled = true;
                    cmd_Imprimir_Sorteado.Enabled = true;
                    txt_Data.Enabled = false;
                    txt_Colaboradores.Enabled = false;
                }
                else
                {
                    cmd_Add.Enabled = true;
                    cmd_Remove.Enabled = true;
                    cmd_Realizar_Sorteio.Enabled = true;
                    cmd_Certificado_Participacao.Enabled = false;
                    cmd_Guias.Enabled = false;
                    cmd_Imprimir_Guias.Enabled = false;
                    cmd_Imprimir_Sorteado.Enabled = false;
                    txt_Data.Enabled = true;
                    txt_Colaboradores.Enabled = true;
                }


                cmd_Salvar.Enabled = false;

                Carregar_Grid_Colaboradores_Ativos_Com_Email(xIdSorteio);

                Carregar_Grid_Colaboradores_Participantes_Sorteio(xIdSorteio);

                Carregar_Grid_Colaboradores_Participantes_Sorteados(xIdSorteio);

            }
            catch( Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Erro(3):" + ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));
            }

            return;

        }

        protected void cmd_Realizar_Sorteio_Click1(object sender, EventArgs e)
        {
            //checar campos e grids se têm colaboradores selecionados
            if (txt_Colaboradores.Text.Trim() == "")
            {
                MsgBox1.Show("Ilitera.Net", "Preencha número de colaboradores.", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }

            if (Validar_Data(txt_Data.Text.Trim()) == false)
            {
                MsgBox1.Show("Ilitera.Net", "Preencha corretamente Data do Exame. ( dd/MM/yyyy )", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            if (System.Convert.ToDateTime(txt_Data.Text.Trim(), ptBr) > System.DateTime.Now.AddDays(20) || System.Convert.ToDateTime(txt_Data.Text.Trim(), ptBr) < System.DateTime.Now)
            {
                MsgBox1.Show("Ilitera.Net", "Data fornecida inválida.", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }


            string Validar;
            bool isNumerical;
            int myInt;

            Validar = txt_Colaboradores.Text.Trim();
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Número de colaboradores inválido.", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }


            if (System.Convert.ToInt16(txt_Colaboradores.Text) < 1)
            {
                MsgBox1.Show("Ilitera.Net", "Número de colaboradores inválido.", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }

            if (System.Convert.ToInt16(txt_Colaboradores.Text) > grd_Lista_Sorteio.Items.Count - 1)
            {
                MsgBox1.Show("Ilitera.Net", "Número de colaboradores inválido.", null,
                new EO.Web.MsgBoxButton("OK"));
                return;
            }


            try
            {

                //realizar sorteio
                Int32 xIdSorteio = System.Convert.ToInt32(ddlSorteios.SelectedValue);

                int zCandidatos = grd_Lista_Sorteio.Items.Count;
                int zSorteados = System.Convert.ToInt16(txt_Colaboradores.Text);


                System.Random rnd = new System.Random();

                for (int zCont = 1; zCont <= zSorteados; zCont++)
                {
                    int zEscolha = rnd.Next(1, zCandidatos);

                    int zId = System.Convert.ToInt32(grd_Lista_Sorteio.Items[zEscolha].Key);


                    ToxicologicoSorteio_Colaborador xColab_Busca2 = new ToxicologicoSorteio_Colaborador();
                    xColab_Busca2.Find(" Id_Toxicologico_Sorteio_Colaborador = " + zId.ToString() + " and Sorteado = 1 ");

                    if (xColab_Busca2.Id != 0)
                    {
                        zCont = zCont - 1;
                        continue;
                    }

                    ToxicologicoSorteio_Colaborador xColab_Busca = new ToxicologicoSorteio_Colaborador();
                    xColab_Busca.Find(" Id_Toxicologico_Sorteio_Colaborador = " + zId.ToString());

                    if (xColab_Busca.Id != 0)
                    {
                        xColab_Busca.Sorteado = true;
                        xColab_Busca.Data_Exame = System.Convert.ToDateTime(txt_Data.Text.Trim(), ptBr);
                        xColab_Busca.Data_Sorteio = System.DateTime.Now;
                        xColab_Busca.Save();

                        //checar se exame já existe
                        Int32 xIdExameDicionario = 100;



                        Complementar xCompl = new Complementar();
                        xCompl.Find(" IdEmpregado = " + xColab_Busca.IdEmpregado.ToString() + " and IdExameDicionario = " + xIdExameDicionario.ToString() + " and convert( char(10), DataExame, 103 ) = '" + txt_Data.Text + "'");

                        //criar exames
                        if (xCompl.Id == 0)
                        {

                            Ilitera.Opsa.Data.Empregado nEmpregado = new Opsa.Data.Empregado();
                            nEmpregado.Find(xColab_Busca.IdEmpregado);

                            Ilitera.Common.Juridica xClin = new Ilitera.Common.Juridica();
                            xClin.Find(" IdJuridica = " + System.Convert.ToInt32(ddlClinica.SelectedValue.ToString()));


                            ExameDicionario xED = new ExameDicionario();
                            xED.Find(" IdExameDicionario = " + xIdExameDicionario.ToString());

                            xCompl.IdExameDicionario = xED;
                            xCompl.IdEmpregado = nEmpregado;
                            xCompl.DataExame = System.Convert.ToDateTime(txt_Data.Text, ptBr);
                            xCompl.IndResultado = (int)Ilitera.Opsa.Data.ResultadoExame.EmEspera;
                            xCompl.IdJuridica = xClin;
                            xCompl.CodBusca = Gerar_Sequencial_Toxicologico();
                            xCompl.Save();

                        }


                        xColab_Busca.IdExameBase = xCompl.Id;
                        xColab_Busca.Save();


                    }

                }






                // colocar Finalizado = 1 e salvar numero colaboradores
                ToxicologicoSorteio zSorteio = new ToxicologicoSorteio();
                zSorteio.Find(xIdSorteio);

                zSorteio.Finalizado = true;
                zSorteio.Colaboradores_Sorteados = System.Convert.ToInt16(txt_Colaboradores.Text);
                zSorteio.Save();



                //colocar data do sorteio em todos registros
                for (int zCont = 0; zCont < grd_Lista_Sorteio.Items.Count; zCont++)
                {
                    Int32 zId = System.Convert.ToInt32(grd_Lista_Sorteio.Items[zCont].Key);

                    ToxicologicoSorteio_Colaborador rColab = new ToxicologicoSorteio_Colaborador();
                    rColab.Find(zId);

                    if (rColab.Id != 0)
                    {
                        rColab.Data_Sorteio = System.DateTime.Now;
                        rColab.Save();
                    }
                }


                Carregar_Grid_Colaboradores_Ativos_Com_Email(xIdSorteio);

                Carregar_Grid_Colaboradores_Participantes_Sorteio(xIdSorteio);

                Carregar_Grid_Colaboradores_Participantes_Sorteados(xIdSorteio);


            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Erro(4):" + ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));
            }


            return;
        }




        protected void cmd_Add_Click(object sender, EventArgs e)
        {
            Int32 zId = 0;

            if (grd_Colaboradores_Ativos.SelectedItemIndex < 0) return;

            try
            {

                zId = System.Convert.ToInt32(grd_Colaboradores_Ativos.Items[grd_Colaboradores_Ativos.SelectedItemIndex].Key);

                Int32 xIdSorteio = System.Convert.ToInt32(ddlSorteios.SelectedValue);


                //checar se já foi sorteado
                ToxicologicoSorteio_Colaborador xColab_Sort = new ToxicologicoSorteio_Colaborador();
                xColab_Sort.Find("  IdEmpregado = " + zId.ToString() + " and Sorteado = 1 and Data_Liberacao_Novo_Sorteio is null ");

                if (xColab_Sort.Id != 0)
                {
                    MsgBox1.Show("Ilitera.Net", "Este colaborador já foi escolhido em sorteio anterior.  Deseja disponibilizar esse colaborador para novo sorteio ?", null,
                    new EO.Web.MsgBoxButton("Sim", null, "Sim"),
                    new EO.Web.MsgBoxButton("Não", null, "Nao"));

                    return;
                }



                //checar se já foi inserido
                ToxicologicoSorteio_Colaborador xColab_Busca = new ToxicologicoSorteio_Colaborador();
                xColab_Busca.Find(" Id_Toxicologico_Sorteio = " + xIdSorteio.ToString() + " and IdEmpregado = " + zId.ToString());

                if (xColab_Busca.Id != 0)
                {
                    return;
                }


                //inserir
                ToxicologicoSorteio_Colaborador xColab = new ToxicologicoSorteio_Colaborador();
                xColab.IdEmpregado = zId;
                xColab.Id_Toxicologico_Sorteio = xIdSorteio;
                xColab.Sorteado = false;

                xColab.Save();


                Carregar_Grid_Colaboradores_Ativos_Com_Email(xIdSorteio);

                Carregar_Grid_Colaboradores_Participantes_Sorteio(xIdSorteio);


            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Erro(5):" + ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));
            }


            return;


        }




        protected void cmd_Remove_Click(object sender, EventArgs e)
        {

            Int32 zId = 0;

            if (grd_Lista_Sorteio.SelectedItemIndex < 0) return;



            try
            {

                zId = System.Convert.ToInt32(grd_Lista_Sorteio.Items[grd_Lista_Sorteio.SelectedItemIndex].Key);



                //localizar
                ToxicologicoSorteio_Colaborador xColab_Busca = new ToxicologicoSorteio_Colaborador();
                xColab_Busca.Find(" Id_Toxicologico_Sorteio_Colaborador = " + zId.ToString());

                if (xColab_Busca.Id == 0)
                {
                    return;
                }

                xColab_Busca.Delete();

                Int32 xIdSorteio = System.Convert.ToInt32(ddlSorteios.SelectedValue);

                Carregar_Grid_Colaboradores_Ativos_Com_Email(xIdSorteio);

                Carregar_Grid_Colaboradores_Participantes_Sorteio(xIdSorteio);


            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Erro(6):" + ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));
            }


            return;

        }



        protected void MsgBox1_ButtonClick(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            //Use the command name to determine which
            //button was clicked
            if (e.CommandName == "Sim")
            {

                Int32 zId = 0;

                if (grd_Colaboradores_Ativos.SelectedItemIndex < 0) return;


                try
                {
                    zId = System.Convert.ToInt32(grd_Colaboradores_Ativos.Items[grd_Colaboradores_Ativos.SelectedItemIndex].Key);

                    Int32 xIdSorteio = System.Convert.ToInt32(ddlSorteios.SelectedValue);


                    //localizar registro
                    ToxicologicoSorteio_Colaborador xColab_Sort = new ToxicologicoSorteio_Colaborador();
                    xColab_Sort.Find("  IdEmpregado = " + zId.ToString() + " and Sorteado = 1 and Data_Liberacao_Novo_Sorteio is null ");

                    if (xColab_Sort.Id != 0)
                    {
                        Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

                        xColab_Sort.Data_Liberacao_Novo_Sorteio = System.DateTime.Now;
                        xColab_Sort.IdUsuario_Liberacao_Novo_Sorteio = usuario.IdUsuario;
                        xColab_Sort.Save();

                        Carregar_Grid_Colaboradores_Ativos_Com_Email(xIdSorteio);

                        Carregar_Grid_Colaboradores_Participantes_Sorteio(xIdSorteio);

                        MsgBox1.Show("Ilitera.Net", "Colaborador liberado para novo sorteio!", null,
                        new EO.Web.MsgBoxButton("OK"));

                        return;
                    }


                }
                catch (Exception ex)
                {
                    MsgBox1.Show("Ilitera.Net", "Erro(7):" + ex.Message, null,
                    new EO.Web.MsgBoxButton("OK"));
                }



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


            if (zData.Length != 10)
            {
                MsgBox1.Show("Ilitera.Net", "Data Inválida.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            Validar = zData.Substring(0, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            Validar = zData.Substring(3, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Mês Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            Validar = zData.Substring(6, 4);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Ano Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            if (zData.Substring(2, 1) != "/" || zData.Substring(5, 1) != "/")
            {
                MsgBox1.Show("Ilitera.Net", "Data Inválida.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            zDia = System.Convert.ToInt32(zData.Substring(0, 2));
            zMes = System.Convert.ToInt32(zData.Substring(3, 2));
            zAno = System.Convert.ToInt32(zData.Substring(6, 4));

            if (zAno < 1900 || zAno > 2025)
            {
                MsgBox1.Show("Ilitera.Net", "Ano Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            if (zMes < 1 || zMes > 12)
            {
                MsgBox1.Show("Ilitera.Net", "Mês Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            if (zMes == 1 || zMes == 3 || zMes == 5 || zMes == 7 || zMes == 8 || zMes == 10 || zMes == 12)
            {
                if (zDia < 1 || zDia > 31)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }
            else if (zMes == 4 || zMes == 6 || zMes == 9 || zMes == 11)
            {
                if (zDia < 1 || zDia > 30)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }
            else
            {
                if (zDia < 1 || zDia > 29)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }

            return true;

        }





        protected void cmd_Imprimir_Guias_Click(object sender, EventArgs e)
        {
            Imprimir_Guia("N");
        }

        protected void Imprimir_Guia( string xEnvio_Email)
        { 

            int xAux;
            string xExames = "";
            string xExames2 = "";
            string xExames3 = "";
            string xExames4 = "";
            string xTipo = "";
            string xBasico = "0";
            string xObs = "";
            int xCont = 0;
            

            try
            {

            

                string xImpDt = "S";



                Guid strAux = Guid.NewGuid();



                xObs = "";

                xExames = "...Toxicológico";


                xTipo = "O";


                Cliente xCliente = new Cliente();
                xCliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));



                if (grd_Sorteados.SelectedItemIndex < 0) return;

                Int32 zId = System.Convert.ToInt32(grd_Sorteados.Items[grd_Sorteados.SelectedItemIndex].Key);


                ToxicologicoSorteio_Colaborador xColab_Busca2 = new ToxicologicoSorteio_Colaborador();
                xColab_Busca2.Find(" Id_Toxicologico_Sorteio_Colaborador = " + zId.ToString());

                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                if (xColab_Busca2.Id != 0)
                {
                    zId = xColab_Busca2.IdEmpregado;
                    txt_Data.Text = xColab_Busca2.Data_Exame.ToString("dd/MM/yyyy", ptBr);
                    txt_Data.Enabled = false;
                }
                else
                {
                    MsgBox1.Show("Ilitera.Net", "Erro na obtenção do Colaborador.", null,
                               new EO.Web.MsgBoxButton("OK"));
                    return;
                }


                Ilitera.Opsa.Data.Empregado nEmpregado = new Ilitera.Opsa.Data.Empregado(zId);


                Ilitera.Common.Juridica xClin = new Ilitera.Common.Juridica();
                xClin.Find(" IdJuridica = " + System.Convert.ToInt32(ddlClinica.SelectedValue.ToString()));


                //checar se exame já existe
                Int32 xIdExameDicionario = 100;



                Complementar xCompl = new Complementar();
                xCompl.Find(" IdEmpregado = " + nEmpregado.Id.ToString() + " and IdExameDicionario = " + xIdExameDicionario.ToString() + " and convert( char(10), DataExame, 103 ) = '" + txt_Data.Text + "'");


                if (xCompl.Id == 0)
                {

                    ExameDicionario xED = new ExameDicionario();
                    xED.Find(" IdExameDicionario = " + xIdExameDicionario.ToString());

                    xCompl.IdExameDicionario = xED;
                    xCompl.IdEmpregado = nEmpregado;
                    xCompl.DataExame = System.Convert.ToDateTime(txt_Data.Text, ptBr);
                    xCompl.IndResultado = (int)Ilitera.Opsa.Data.ResultadoExame.EmEspera;
                    xCompl.IdJuridica = xClin;
                    xCompl.CodBusca = Gerar_Sequencial_Toxicologico();
                    xCompl.Save();

                }



                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                string rData = "          |          ";

                Ilitera.Data.PPRA_EPI xGuia = new Ilitera.Data.PPRA_EPI();
                xGuia.Salvar_Dados_Guia_Encaminhamento(System.Convert.ToInt32(Request["IdEmpresa"].ToString()), nEmpregado.Id, xTipo, xExames + "|" + xExames2 + "|" + xExames3 + "|" + xExames4, txt_Data.Text, "", ddlClinica.Items[ddlClinica.SelectedIndex].ToString(), user.IdUsuario, "N", rData.Substring(0, 10), rData.Substring(11), "");



                Session["Obs_Guia"] = xObs;

                cliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));



                //pegar exames de PCMSO do funcionário
                //nEmpregado.Find(" tblEMPREGADO.nID_EMPREGADO = " + Request["IdEmpregado"].ToString());

                EmpregadoFuncao rEmprFuncao = new EmpregadoFuncao();
                rEmprFuncao = EmpregadoFuncao.GetEmpregadoFuncao(nEmpregado, cliente.Id);


                OpenReport("DadosEmpresa", "RelatorioGuiaComp.aspx?IliteraSystem=" + strAux.ToString()
        + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + nEmpregado.Id.ToString() + "&E1=" + xExames + "&E2=" + xExames2 + "&E3=" + xExames3 + "&E4=" + xExames4 + "&IdClinica=" + ddlClinica.SelectedValue.ToString() + "&H_E=" + "  " + "&D_E=" + txt_Data.Text + "&Tipo=" + xTipo + "&Basico=" + xBasico + "&Mail=" + xEnvio_Email + "&ImpDt=" + xImpDt + "&IdEmprFunc=" + rEmprFuncao.Id.ToString().Trim(), "RelatorioGuia",  true);



            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Erro(8):" + ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));
            }



        }




        protected void OpenReport(string directory, string fileAndQuery, string ReportName)
        {
            this.OpenReport(directory, fileAndQuery, ReportName, false);
        }

        protected void OpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        {
            StringBuilder st = new StringBuilder();

            Guid strAux = Guid.NewGuid();

            string valueProcess = "Local";

            if (valueProcess.Equals("Remote"))
                st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
                    + "&Key=" + strAux.ToString() + "MestraNet" + strAux.ToString()
                    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
            else if (valueProcess.Equals("Local"))
            {
                if (useDirectoryForLocalProcess)
                {
                    //st.Append("void(window.open('../" + directory + "/" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('{0}/{1}', '{2}','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", directory, fileAndQuery, ReportName);
                }
                else
                {
                    st.Append("void(window.open('" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('{0}','DadosEmpresa/{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", fileAndQuery, ReportName);
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), String.Format("OpenReport{0}", ReportName), st.ToString(), true);
        }




        protected void cmd_Certificado_Participacao_Click(object sender, EventArgs e)
        {

            //pegar ID e passar 
            if (grd_Lista_Sorteio.SelectedItemIndex < 0) return;

            Int32 zId = System.Convert.ToInt32(grd_Lista_Sorteio.Items[grd_Lista_Sorteio.SelectedItemIndex].Key);


            OpenReport("", "Toxicologico.aspx?Id=" + zId.ToString() + "&Tipo=2", "Toxicologico");



        }



        protected void cmd_Imprimir_Sorteado_Click(object sender, EventArgs e)
        {
            //pegar ID e passar 
            if (grd_Sorteados.SelectedItemIndex < 0) return;

            Int32 zId = System.Convert.ToInt32(grd_Sorteados.Items[grd_Sorteados.SelectedItemIndex].Key);


            OpenReport("", "Toxicologico.aspx?Id=" + zId.ToString() + "&Tipo=1", "Toxicologico");

                     
        }


        private Int32 Gerar_Sequencial_Toxicologico()
        {
            Int32 zCodMax = 1;


            Ilitera.Data.Clientes_Funcionarios xBusca = new Data.Clientes_Funcionarios();
            zCodMax = xBusca.Retornar_CodBusca_Toxicologico(0, 10000000);


            zCodMax = zCodMax + 1;


            return zCodMax;

        }

        protected void cmd_Guias_Click(object sender, EventArgs e)
        {

            //checar se colaborador têm email

            if (grd_Sorteados.SelectedItemIndex < 0) return;

            try
            {

                Int32 zId = System.Convert.ToInt32(grd_Sorteados.Items[grd_Sorteados.SelectedItemIndex].Key);


                ToxicologicoSorteio_Colaborador xColab_Busca2 = new ToxicologicoSorteio_Colaborador();
                xColab_Busca2.Find(" Id_Toxicologico_Sorteio_Colaborador = " + zId.ToString());

                //enviar e-mail
                Ilitera.Opsa.Data.Empregado rEmpregado = new Ilitera.Opsa.Data.Empregado();
                rEmpregado.Find(xColab_Busca2.IdEmpregado);

                if (rEmpregado.teMail.Trim() == "")
                {
                    MsgBox1.Show("Ilitera.Net", "Colaborador não tem e-mail cadastrado para envio.", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return;
                }


                Imprimir_Guia("T");


            }
            catch (Exception ex)
            {
                MsgBox1.Show("Ilitera.Net", "Erro(8):" + ex.Message, null,
                new EO.Web.MsgBoxButton("OK"));
            }

            return;
        }




    }
}
