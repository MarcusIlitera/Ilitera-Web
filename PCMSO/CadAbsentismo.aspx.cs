using System;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Net.Mail;

using System.Drawing;
using System.Data;
using Ilitera.Data;

namespace Ilitera.Net
{
	/// <summary>
	/// Summary description for CadCAT.
	/// </summary>
    public partial class CadAbsentismo : System.Web.UI.Page
	{
		private Afastamento afastamento;
        private Empregado empregado;
        private Cliente cliente;


		protected System.Web.UI.WebControls.Label Label1;
		protected string tipo, ProcessoRealizado;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        protected void Page_Load(object sender, System.EventArgs e)
        {
            string xUsuario = Session["usuarioLogado"].ToString();
            InicializaWebPageObjects();
            RegisterClientCode();


            if (!Page.IsPostBack)
            {
                for (int i = 0; i < 24; i++)
                {
                    ddlHoraIni.Items.Insert(i, new ListItem(i.ToString("00"), i.ToString("00")));
                    ddlHoraRet.Items.Insert(i, new ListItem(i.ToString("00"), i.ToString("00")));
                }
                for (int i = 0; i < 60; i++)
                {
                    ddlMinutoIni.Items.Insert(i, new ListItem(i.ToString("00"), i.ToString("00")));
                    ddlMinutoRet.Items.Insert(i, new ListItem(i.ToString("00"), i.ToString("00")));
                }

                PopulaDDLAcidente();

                PopulaDDLOutros();

                if (afastamento.Id != 0)
                    PopulaTela();
                else
                {
                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                    {
                        rblAbsentismo.SelectedIndex = 1;
                    }
                    else
                    {
                        rblAbsentismo.SelectedIndex = 0;
                    }
                }


                Cliente cliente;
                cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                cliente.IdGrupoEmpresa.Find();

                if (cliente.IdGrupoEmpresa.Descricao.ToUpper().Trim() == "CAPGEMINI" && Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)  //.Id == -905238295)  // Capgemini
                {
                    chk_INSS.Visible = true;
                }


            }
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
			this.ID = "CadAbsentismo";

		}
		#endregion

		private void PopulaDDLAcidente()
		{
			DataSet ds = new DataSet();
			DataRow rowds;

			DataTable table = new DataTable("Default");
			table.Columns.Add("IdAcidente", Type.GetType("System.String"));
			table.Columns.Add("Acidente", Type.GetType("System.String"));
			ds.Tables.Add(table);

            DataSet dsAcidente = new Acidente().Get("IdEmpregado=" + Session["Empregado"].ToString()
				+" AND IdAcidente NOT IN (SELECT IdAcidente FROM Afastamento WHERE IdAfastamento<>" + afastamento.Id
				+" AND IdAcidente IS NOT NULL)"
				+" ORDER BY DataAcidente DESC");

			foreach (DataRow row in dsAcidente.Tables[0].Select())
			{
				rowds = ds.Tables[0].NewRow();
			
				rowds["IdAcidente"] = row["IdAcidente"];
				
				string tipoAcidente = string.Empty;

				switch (Convert.ToInt32(row["IndTipoAcidente"]))
				{
					case (int)TipoAcidente.Doenca:
						tipoAcidente = "Doença";
						break;
					case (int)TipoAcidente.Tipico:
						tipoAcidente = "Típico";
						break;
					case (int)TipoAcidente.Trajeto:
						tipoAcidente = "Trajeto";
						break;
				}

				rowds["Acidente"] = ((DateTime)row["DataAcidente"]).ToString("dd-MM-yyyy") + " " + tipoAcidente;

				ds.Tables[0].Rows.Add(rowds);
			}

			ddlAcidente.DataSource = ds;
			ddlAcidente.DataValueField = "IdAcidente";
			ddlAcidente.DataTextField = "Acidente";
			ddlAcidente.DataBind();

			ddlAcidente.Items.Insert(0, new ListItem("Absentismo sem Acidentes", "0"));
		}


        private void PopulaDDLOutros()
        {
            DataSet ds = new DataSet();
        
            DataSet dsAcidente = new AfastamentoTipo().Get(" Descricao IS NOT NULL ORDER BY Descricao ");

            ddlOutros.DataSource = dsAcidente;
            ddlOutros.DataValueField = "IdAfastamentoTipo";
            ddlOutros.DataTextField = "Descricao";
            ddlOutros.DataBind();

            ddlOutros.Items.Insert(0, new ListItem("-", "0"));
        }

        
        private void RegisterClientCode()
		{
			btnExcluir.Attributes.Add("onClick" ,"javascript:if(confirm('Deseja realmente excluir este Afastamento?')) return true; else return false;");
			btnCancelar.Attributes.Add("onClick" ,"javascript:window.close();");
			btnOK.Attributes.Add("onClick", "javascript: return VerificaData();");
		}

        protected void InicializaWebPageObjects()
        {
            //base.InicializaWebPageObjects();

            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));
            empregado = new Empregado(System.Convert.ToInt32(Session["Empregado"].ToString()));
            

			if(Request["IdAfastamento"] != null && Request["IdAfastamento"] != "")
			{
				afastamento = new Afastamento(Convert.ToInt32(Request["IdAfastamento"]));
				tipo = "atualizado";
				ProcessoRealizado = "Edição do cadastro de Absentismo para o Empregado " + empregado.tNO_EMPG;
			}
			else
			{
				afastamento = new Afastamento();
				afastamento.Inicialize();
				afastamento.IdEmpregado = empregado;

				btnExcluir.Visible = false;
				tipo = "cadastrado";
				ProcessoRealizado = "Cadastro de Absentismo para o Empregado " + empregado.tNO_EMPG;
			}
		}

		private void PopulaAbsentismo(IDbTransaction transaction)
		{            
            afastamento.DataInicial = new DateTime(Convert.ToInt32(txtaai.Text), Convert.ToInt32(txtmmi.Text), Convert.ToInt32(txtddi.Text), 
				Convert.ToInt32(ddlHoraIni.SelectedValue), Convert.ToInt32(ddlMinutoIni.SelectedValue), 0, 0);
			if (txtmmp.Text != string.Empty)
				afastamento.DataPrevista = new DateTime(Convert.ToInt32(txtaap.Text), Convert.ToInt32(txtmmp.Text), Convert.ToInt32(txtddp.Text));
			else
				afastamento.DataPrevista = new DateTime();
			if (txtmmr.Text != string.Empty)
				afastamento.DataVolta = new DateTime(Convert.ToInt32(txtaar.Text), Convert.ToInt32(txtmmr.Text), Convert.ToInt32(txtddr.Text), 
					Convert.ToInt32(ddlHoraRet.SelectedValue), Convert.ToInt32(ddlMinutoRet.SelectedValue), 0, 0);
			else
				afastamento.DataVolta = new DateTime();

			afastamento.IndTipoAfastamento = Convert.ToInt32(rblAbsentismo.SelectedValue);

            afastamento.IdAfastamentoTipo.Id = Convert.ToInt32(ddlOutros.SelectedValue);
            afastamento.IdAfastamentoTipo.Find();


            Acidente acidente = new Acidente();

            if (afastamento.Id.Equals(0) && !ddlAcidente.SelectedValue.Equals("0"))
            {
                acidente = new Acidente(Convert.ToInt32(ddlAcidente.SelectedValue));
                acidente.hasAfastamento = true;

                if (txtCID.Text.Trim() != "" && lbl_Id1.Text.Trim() != "")
                {
                    acidente.IdCID.Id = System.Convert.ToInt32(lbl_Id1.Text);                 //Convert.ToInt32(ddlCID.SelectedValue);
                    if (txtCID2.Text.Trim() != "" && lbl_Id2.Text.Trim() != "")
                        acidente.IdCID2 = System.Convert.ToInt32(lbl_Id2.Text);
                    if (txtCID3.Text.Trim() != "" && lbl_Id2.Text.Trim() != "")
                        acidente.IdCID3 = System.Convert.ToInt32(lbl_Id3.Text);
                    if (txtCID4.Text.Trim() != "" && lbl_Id4.Text.Trim() != "")
                        acidente.IdCID4 = System.Convert.ToInt32(lbl_Id4.Text);
                }
                acidente.Transaction = transaction;
                acidente.Save();
            }
            else if (!afastamento.Id.Equals(0) && ddlAcidente.SelectedValue.Equals("0") && !afastamento.IdAcidente.Id.Equals(0))
            {
                afastamento.IdAcidente.Find();

                acidente = afastamento.IdAcidente;
                acidente.hasAfastamento = false;
                acidente.Transaction = transaction;
                acidente.Save();
            }
            else if (!afastamento.Id.Equals(0) && !ddlAcidente.SelectedValue.Equals("0") && afastamento.IdAcidente.Id.Equals(0))
            {
                acidente = new Acidente(Convert.ToInt32(ddlAcidente.SelectedValue));
                acidente.hasAfastamento = true;
                //acidente.IdCID.Id = Convert.ToInt32(ddlCID.SelectedValue);
                if (txtCID.Text.Trim() != "" && lbl_Id1.Text.Trim() != "")
                {
                    acidente.IdCID.Id = System.Convert.ToInt32(lbl_Id1.Text);                 //Convert.ToInt32(ddlCID.SelectedValue);
                    if (txtCID2.Text.Trim() != "" && lbl_Id2.Text.Trim() != "")
                        acidente.IdCID2 = System.Convert.ToInt32(lbl_Id2.Text);
                    if (txtCID3.Text.Trim() != "" && lbl_Id2.Text.Trim() != "")
                        acidente.IdCID3 = System.Convert.ToInt32(lbl_Id3.Text);
                    if (txtCID4.Text.Trim() != "" && lbl_Id4.Text.Trim() != "")
                        acidente.IdCID4 = System.Convert.ToInt32(lbl_Id4.Text);
                }

                acidente.Transaction = transaction;
                acidente.Save();
            }
            else if (!afastamento.Id.Equals(0) && !ddlAcidente.SelectedValue.Equals("0") && !afastamento.IdAcidente.Id.Equals(0))
            {
                if (!afastamento.IdAcidente.Id.Equals(Convert.ToInt32(ddlAcidente.SelectedValue)))
                {
                    afastamento.IdAcidente.Find();

                    acidente = afastamento.IdAcidente;
                    acidente.hasAfastamento = false;
                    acidente.Save();
                }

                acidente = new Acidente(Convert.ToInt32(ddlAcidente.SelectedValue));
                acidente.hasAfastamento = true;
                //acidente.IdCID.Id = Convert.ToInt32(ddlCID.SelectedValue);
                if (txtCID.Text.Trim() != "" && lbl_Id1.Text.Trim() != "")
                {
                    acidente.IdCID.Id = System.Convert.ToInt32(lbl_Id1.Text);                 //Convert.ToInt32(ddlCID.SelectedValue);
                    if (txtCID2.Text.Trim() != "" && lbl_Id2.Text.Trim() != "")
                        acidente.IdCID2 = System.Convert.ToInt32(lbl_Id2.Text);
                    if (txtCID3.Text.Trim() != "" && lbl_Id2.Text.Trim() != "")
                        acidente.IdCID3 = System.Convert.ToInt32(lbl_Id3.Text);
                    if (txtCID4.Text.Trim() != "" && lbl_Id4.Text.Trim() != "")
                        acidente.IdCID4 = System.Convert.ToInt32(lbl_Id4.Text);
                }

                acidente.Transaction = transaction;
                acidente.Save();
            }               
            
            afastamento.IdAcidente.Id = Convert.ToInt32(ddlAcidente.SelectedValue);
            //afastamento.IdCID.Id = System.Convert.ToInt32( lbl_Id1.Text.Trim() ); //Convert.ToInt32(ddlCID.SelectedValue);
            if (txtCID.Text.Trim() != "" && lbl_Id1.Text.Trim() != "")
            {
                afastamento.IdCID.Id = System.Convert.ToInt32(lbl_Id1.Text);                 //Convert.ToInt32(ddlCID.SelectedValue);
                if (txtCID2.Text.Trim() != "" && lbl_Id2.Text.Trim() != "")
                    afastamento.IdCID2 = System.Convert.ToInt32(lbl_Id2.Text);
                if (txtCID3.Text.Trim() != "" && lbl_Id2.Text.Trim() != "")
                    afastamento.IdCID3 = System.Convert.ToInt32(lbl_Id3.Text);
                if (txtCID4.Text.Trim() != "" && lbl_Id4.Text.Trim() != "")
                    afastamento.IdCID4 = System.Convert.ToInt32(lbl_Id4.Text);
            }

		}

		private void PopulaTela()
		{            
			txtddi.Text = afastamento.DataInicial.ToString("dd");
			txtmmi.Text = afastamento.DataInicial.ToString("MM");
			txtaai.Text = afastamento.DataInicial.ToString("yyyy");
			ddlHoraIni.Items.FindByValue(afastamento.DataInicial.ToString("HH")).Selected = true;
			ddlMinutoIni.Items.FindByValue(afastamento.DataInicial.ToString("mm")).Selected = true;


            if (afastamento.INSS == true) chk_INSS.Checked = true;
            else chk_INSS.Checked = false;


            txtObs.Text = afastamento.Obs.Trim();

            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
            {
                txt_Arq.Text = "";
                
                if (afastamento.Atestado != null)
                {
                    if (afastamento.Atestado.Trim() != "")
                    {
                        txt_Arq.Text = afastamento.Atestado.Trim();                
                    }
                }

                txt_Arq.ReadOnly = true;

                txtNomeOC.Text = afastamento.Atestado_Emitente.Trim();
                txtNumeroOC.Text = afastamento.Atestado_nrOC.Trim();
                txtUFOC.Text = afastamento.Atestado_ufOC.Trim();

                rblOrgaoOC.ClearSelection();
                if (afastamento.Atestado_ideOC.ToString().Trim() != "")
                    rblOrgaoOC.Items.FindByValue(afastamento.Atestado_ideOC.ToString()).Selected = true;

            }
            else
            {

                if (afastamento.Atestado != null)
                {
                    if (afastamento.Atestado.Trim() != "")
                    {
                        txt_Arq.Text = afastamento.Atestado.Trim();
                        txt_Arq.ReadOnly = true;

                        txtNomeOC.Text = afastamento.Atestado_Emitente.Trim();
                        txtNumeroOC.Text = afastamento.Atestado_nrOC.Trim();
                        txtUFOC.Text = afastamento.Atestado_ufOC.Trim();

                        rblOrgaoOC.ClearSelection();
                        if (afastamento.Atestado_ideOC.ToString().Trim() != "")
                            rblOrgaoOC.Items.FindByValue(afastamento.Atestado_ideOC.ToString()).Selected = true;


                    }
                    else
                    {
                        txt_Arq.Text = "";
                        txt_Arq.ReadOnly = true;

                        txtNomeOC.Text = "";
                        txtNumeroOC.Text = "";
                        txtUFOC.Text = "";
                        rblOrgaoOC.Items.FindByValue("1").Selected = true;
                    }



                }
                else
                {
                    txt_Arq.Text = "";
                    txt_Arq.ReadOnly = true;

                    txtNomeOC.Text = "";
                    txtNumeroOC.Text = "";
                    txtUFOC.Text = "";
                    rblOrgaoOC.Items.FindByValue("1").Selected = true;
                }
            }


			if (afastamento.DataPrevista != new DateTime() && afastamento.DataPrevista != new DateTime(1753, 1, 1))
			{
				txtddp.Text = afastamento.DataPrevista.ToString("dd");
				txtmmp.Text = afastamento.DataPrevista.ToString("MM");
				txtaap.Text = afastamento.DataPrevista.ToString("yyyy");
			}

			if (afastamento.DataVolta != new DateTime() && afastamento.DataVolta != new DateTime(1753, 1, 1))
			{
				txtddr.Text = afastamento.DataVolta.ToString("dd");
				txtmmr.Text = afastamento.DataVolta.ToString("MM");
				txtaar.Text = afastamento.DataVolta.ToString("yyyy");
				ddlHoraRet.Items.FindByValue(afastamento.DataVolta.ToString("HH")).Selected = true;
				ddlMinutoRet.Items.FindByValue(afastamento.DataVolta.ToString("mm")).Selected = true;
			}

			rblAbsentismo.ClearSelection();
			rblAbsentismo.Items.FindByValue(afastamento.IndTipoAfastamento.ToString()).Selected  = true;

			ddlAcidente.ClearSelection();
			ddlAcidente.Items.FindByValue(afastamento.IdAcidente.Id.ToString()).Selected = true;

            ddlOutros.ClearSelection();
            ddlOutros.Items.FindByValue(afastamento.IdAfastamentoTipo.Id.ToString()).Selected = true;


			if (afastamento.IdCID.Id != 0)
			{
				afastamento.IdCID.Find();
				ddlCID.Items.Insert(0, new ListItem(afastamento.IdCID.Descricao, afastamento.IdCID.Id.ToString()));

                txtCID.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                lbl_Id1.Text = ddlCID.SelectedValue.ToString().Trim();                
			}

            if (afastamento.IdCID2 != 0)
            {
                CID xCid = new CID();
                xCid.Find(afastamento.IdCID2);

                ddlCID.Items.Insert(0, new ListItem(xCid.Descricao, xCid.Id.ToString()));
                //ddlCID.Items.FindByValue(xCid.Id.ToString());
                //ddlCID.Items.FindByText(xCid.Descricao);

                txtCID2.Text = xCid.Descricao;
                lbl_Id2.Text = xCid.Id.ToString();
            }

            if (afastamento.IdCID3 != 0)
            {
                CID xCid = new CID();
                xCid.Find(afastamento.IdCID3);

                ddlCID.Items.Insert(0, new ListItem(xCid.Descricao, xCid.Id.ToString()));
               // ddlCID.Items.FindByValue(xCid.Id.ToString());

                txtCID3.Text = xCid.Descricao;
                lbl_Id3.Text = xCid.Id.ToString();
            }

            if (afastamento.IdCID4 != 0)
            {
                CID xCid = new CID();
                xCid.Find(afastamento.IdCID4);

                ddlCID.Items.Insert(0, new ListItem(xCid.Descricao, xCid.Id.ToString()));
                //ddlCID.Items.FindByValue(xCid.Id.ToString());

                txtCID4.Text = xCid.Descricao;
                lbl_Id4.Text = xCid.Id.ToString();
            }


		}

		protected void btnOK_Click(object sender, System.EventArgs e)
		{

            bool xNovo = false;
            bool xEnvio_Alerta = false;
            string xNomeArq = File1.FileName.Trim();   //só aceita arquivo atestado se campos do emitente preenchido

            if (xNomeArq == string.Empty)
            {
                xNomeArq = txt_Arq.Text.Trim();
            }

            if (xNomeArq != string.Empty)
            {
                if (rblOrgaoOC.SelectedValue.ToString().Trim() != "4")
                {
                    if (txtUFOC.Text.Trim() == "" || txtNomeOC.Text.Trim() == "" || txtNumeroOC.Text.Trim() == "")
                    {
                        MsgBox1.Show("Absenteísmo", "Todo atestado deve ter identificado de forma completa o emissor.", null,
                                         new EO.Web.MsgBoxButton("OK"));
                        return;
                    }
                }
            }

            //no update ele só deve pegar se estiver selecionado um arquivo
            xNomeArq = File1.FileName.Trim();



            //if ( txtddr.Text.Trim()=="" && txtddp.Text.Trim()=="")
            //{
            //    MsgBox1.Show("Absenteísmo", "Todo afastamento deve ter a data de retorno ou previsão de retorno preenchida.", null,
            //                     new EO.Web.MsgBoxButton("OK"));
            //    return;
            //}




            Cliente xCliente = new Cliente();
            xCliente.Find(System.Convert.ToInt32(Request["IdEmpresa"].ToString()));

            if (afastamento.Id == 0)
            {
                xNovo = true;
            }
            else
            {
                if (afastamento.DataVolta == null || afastamento.DataVolta.Year == 1 || afastamento.DataVolta == new DateTime())
                {
                    if ( txtddr.Text != "")
                    {
                        xNovo = true;
                    }
                }
                else
                {
                    string zDia = "";
                    string zMes = "";

                    string zDia2 = "";
                    string zMes2 = "";

                    if (afastamento.DataVolta.Month < 10)
                        zMes = "0" + afastamento.DataVolta.Month.ToString().Trim();
                    else
                        zMes = afastamento.DataVolta.Month.ToString().Trim();

                    if (afastamento.DataVolta.Day < 10)
                        zDia = "0" + afastamento.DataVolta.Day.ToString().Trim();
                    else
                        zDia = afastamento.DataVolta.Day.ToString().Trim();


                    if (txtmmr.Text.Trim().Length==1)
                        zMes2 = "0" + txtmmr.Text.Trim();
                    else
                        zMes2 = txtmmr.Text.Trim();

                    if (txtddr.Text.Trim().Length == 1)
                        zDia2 = "0" + txtddr.Text.Trim();
                    else
                        zDia2 = txtddr.Text.Trim();




                    if ( zDia != zDia2  || zMes!=zMes2 || afastamento.DataVolta.Year.ToString().Trim() !=  txtaar.Text.Trim() )
                    {
                        xNovo = true;
                    }
                }
            }



            IDbTransaction transaction = afastamento.GetTransaction();

            using (transaction)
            {
                try
                {
                    //xNomeArq = File1.FileName.Trim();

                    if (xNomeArq != string.Empty)
                    {

                        string xExtension = xNomeArq.Substring(xNomeArq.Length - 3, 3).ToUpper().Trim();

                        if (xExtension == "PDF" || xExtension == "JPG")
                        {

                            string xArq = "";

                            //if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
                            xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;
                            // else
                            //    xArq = "C:\\DRIVE_I\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;

                            File1.SaveAs(xArq);

                            xArq = "I:\\fotosdocsdigitais\\" + xCliente.DiretorioPadrao.ToString().ToUpper().Trim() + "\\Prontuario\\" + xNomeArq;

                            afastamento.Atestado = xArq;

                            afastamento.Atestado_Emitente = txtNomeOC.Text.Trim();
                            afastamento.Atestado_nrOC = txtNumeroOC.Text.Trim();
                            afastamento.Atestado_ufOC = txtUFOC.Text.Trim();

                            afastamento.Atestado_ideOC = rblOrgaoOC.SelectedValue.ToString().Trim();

                        }

                    }
                    else
                    {
                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                        {
                            afastamento.Atestado_Emitente = txtNomeOC.Text.Trim();
                            afastamento.Atestado_nrOC = txtNumeroOC.Text.Trim();
                            afastamento.Atestado_ufOC = txtUFOC.Text.Trim();

                            afastamento.Atestado_ideOC = rblOrgaoOC.SelectedValue.ToString().Trim();
                        }
                    }


                    if (chk_INSS.Visible == true)
                    {
                        if (chk_INSS.Checked == true) afastamento.INSS = true;
                        else afastamento.INSS = false;
                    }
                    else
                    {
                        afastamento.INSS = false;
                    }

                    PopulaAbsentismo(transaction);


                    afastamento.Obs = txtObs.Text.Trim();

                    Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                    afastamento.UsuarioId = user.IdUsuario;
                    afastamento.UsuarioProcessoRealizado = ProcessoRealizado;
                    afastamento.Transaction = transaction;
                    afastamento.Save();

                    transaction.Commit();


                    if (xNovo == true)
                    {

                        if (afastamento.DataVolta == null || afastamento.DataVolta.Year==1)
                        {
                            if (afastamento.DataInicial.AddDays(15) <= afastamento.DataPrevista)
                            {
                                xEnvio_Alerta = true;
                            }
                        }
                        else
                        {
                            if (afastamento.DataInicial.AddDays(15) <= afastamento.DataVolta)
                            {
                                xEnvio_Alerta = true;
                            }
                        }


                        if (xEnvio_Alerta == false)
                        //checar se há atetados com mais de 15 dias afastados nos ultimos 60 dias
                        {
                            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

                            Ilitera.Data.Clientes_Funcionarios xAbs = new Ilitera.Data.Clientes_Funcionarios();
                            DataSet rDs = xAbs.Checar_Absenteismo_Colaborador(Convert.ToInt32(Session["Empregado"].ToString()), afastamento.DataInicial.ToString("dd/MM/yyyy", ptBr));

                            if (rDs.Tables[0].Rows.Count > 0)
                            {
                                if (rDs.Tables[0].Rows[0][0].ToString().Trim() != "")
                                {
                                    if (System.Convert.ToInt16(rDs.Tables[0].Rows[0][0].ToString()) >= 15)
                                    {
                                        xEnvio_Alerta = true;
                                    }
                                }
                            }
                        }
                    }


                    if (xEnvio_Alerta == true)
                    {
                        
                        if (xCliente.Mail_Alerta_Absenteismo != null && xCliente.Mail_Alerta_Absenteismo.ToString().Trim() != "")
                        {
                            string xBody = ""; 

                            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                            {
                                xBody = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Alerta de Absenteísmo</H1></font></p> <br></br>" +
                                          "<p><font size='3' face='Tahoma'>O colaborador " + Session["NomeEmpregado"] + " possui afastamentos anteriores com pelo menos 15 dias corridos ou com total de dias afastados de pelo menos 15 dias nos últimos 60 dias. Favor verificar se os atestados possuem a mesma patologia para possível encaminhamento ao INSS. <br><br> Dúvidas contatar a central de atendimento Essence: centraldeatendimento@essencenet.com.br<br><br><br>Central de Atendimento<br>5A Essence<br>www.5aessence.com.br<br>Tel.: (11) 2344 - 4585</font></p></body>";


                                Envio_Email_Prajna(xCliente.Mail_Alerta_Absenteismo.Trim(), "", "Alerta de Absenteísmo", xBody, "", "Absenteísmo", Convert.ToInt32(Session["Empregado"].ToString()), System.Convert.ToInt32(Request["IdEmpresa"].ToString()));
                            }
                            else
                            {
                                xBody = "<body><p><font size='2' face='Tahoma'><H1 align='center'>Alerta de Absenteísmo</H1></font></p> <br></br>" +
                                          "<p><font size='3' face='Tahoma'>O colaborador " + Session["NomeEmpregado"] + " possui afastamentos anteriores com pelo menos 15 dias corridos ou com total de dias afastados de pelo menos 15 dias nos últimos 60 dias. Favor verificar se os atestados possuem a mesma patologia para possível encaminhamento ao INSS.<br><br> Dúvidas contatar a central de atendimento : atendimento@ilitera.com.br</font></p></body>"; 

                                Envio_Email_Ilitera(xCliente.Mail_Alerta_Absenteismo.Trim(), "", "Alerta de Absenteísmo", xBody, "", "Absenteísmo", Convert.ToInt32(Session["Empregado"].ToString()), System.Convert.ToInt32(Request["IdEmpresa"].ToString()));
                            }
                        }

                        //MsgBox1.Show("Absenteísmo", "Absenteísmo salvo !  Existem absenteísmos anteriores com pelo menos 15 dias corridos ou com total de dias afastados de pelo 15 dias nos últimos 2 meses. ", null,
                        // new EO.Web.MsgBoxButton("OK"));

                        StringBuilder st2 = new StringBuilder();

                        st2.Append("window.opener.document.forms[0].submit(); window.alert('Absenteísmo salvo !  Existem absenteísmos anteriores com pelo menos 15 dias corridos ou com total de dias afastados de pelo 15 dias nos últimos 2 meses. '); window.close();");

                        this.ClientScript.RegisterStartupScript(this.GetType(), "AbsentismoEmpregado", st2.ToString(), true);
                        return;
                    }


                    StringBuilder st = new StringBuilder();

                    st.Append("window.opener.document.forms[0].submit(); window.alert('O Absentismo foi " + tipo + " com sucesso!'); window.close();");

                    this.ClientScript.RegisterStartupScript(this.GetType(), "AbsentismoEmpregado", st.ToString(), true);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MsgBox1.Show("Acidente", ex.Message, null,
                                   new EO.Web.MsgBoxButton("OK"));
                    //Aviso(ex.Message);


                }
            }
		}

		protected void btnExcluir_Click(object sender, System.EventArgs e)
		{
			IDbTransaction transaction = afastamento.GetTransaction();

            using (transaction)
            {
                try
                {
                    if (!afastamento.IdAcidente.Id.Equals(0))
                    {
                        afastamento.IdAcidente.Find();

                        Acidente acidente = afastamento.IdAcidente;
                        acidente.hasAfastamento = false;
                        acidente.Transaction = transaction;
                        acidente.Save();
                    }

                    Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                    afastamento.UsuarioId = user.IdUsuario;
                    afastamento.UsuarioProcessoRealizado = "Exclusão de Absentismo do Empregado " + empregado.tNO_EMPG;
                    afastamento.Transaction = transaction;
                    afastamento.Delete();

                    transaction.Commit();

                    StringBuilder st = new StringBuilder("");

                    st.Append("window.opener.document.forms[0].submit(); window.alert('O Absentismo foi deletado com sucesso!'); window.close();");

                    this.ClientScript.RegisterStartupScript(this.GetType(), "DeletaAbsentismo", st.ToString(), true);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MsgBox1.Show("Acidente", ex.Message, null,
                                   new EO.Web.MsgBoxButton("OK"));
                    //Aviso(ex.Message);
                }
            }
		}

		protected void btnProcurar_Click(object sender, System.EventArgs e)
		{

            lbl_Procura.Text = "1";

			if (txtCID.Text.Trim() != string.Empty)
			{
                ddlCID.ClearSelection();
                
                while (ddlCID.Items.Count > 1)
					ddlCID.Items.RemoveAt(0);
				
                DataSet ds = new CID().Get("CodigoCID='" + txtCID.Text.Trim() + "' OR Descricao LIKE '%" + txtCID.Text.Trim() + "%' ORDER BY Descricao DESC");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Select())
                        ddlCID.Items.Insert(0, new ListItem(row["Descricao"].ToString(), row["IdCID"].ToString()));
                    txtCID.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                    lbl_Id1.Text = ddlCID.SelectedValue.ToString().Trim();

                }
                else
                    MsgBox1.Show("Acidente", "O código '" + txtCID.Text + "' não foi encontrado! Por favor, tente novamente!", null,
                                   new EO.Web.MsgBoxButton("OK"));


					//Aviso("O código '" + txtCID.Text + "' não foi encontrado! Por favor, tente novamente!");
			}
			else

                MsgBox1.Show("Acidente", "É necessário colocar o Código CID para executar a busca!", null,
                               new EO.Web.MsgBoxButton("OK"));
                
				//Aviso("É necessário colocar o Código CID para executar a busca!");
		}


        protected void btnProcurar2_Click(object sender, System.EventArgs e)
        {

            lbl_Procura.Text = "2";

            if (txtCID2.Text.Trim() != string.Empty)
            {
                ddlCID.ClearSelection();

                while (ddlCID.Items.Count > 1)
                    ddlCID.Items.RemoveAt(0);

                DataSet ds = new CID().Get("CodigoCID='" + txtCID2.Text.Trim() + "' OR Descricao LIKE '%" + txtCID2.Text.Trim() + "%' ORDER BY Descricao DESC");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Select())
                        ddlCID.Items.Insert(0, new ListItem(row["Descricao"].ToString(), row["IdCID"].ToString()));
                    txtCID2.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                    lbl_Id2.Text = ddlCID.SelectedValue.ToString().Trim();
                }
                else
                    MsgBox1.Show("Acidente", "O código '" + txtCID2.Text + "' não foi encontrado! Por favor, tente novamente!", null,
                                   new EO.Web.MsgBoxButton("OK"));


                //Aviso("O código '" + txtCID.Text + "' não foi encontrado! Por favor, tente novamente!");
            }
            else

                MsgBox1.Show("Acidente", "É necessário colocar o Código CID para executar a busca!", null,
                               new EO.Web.MsgBoxButton("OK"));

            //Aviso("É necessário colocar o Código CID para executar a busca!");
        }

        protected void btnProcurar3_Click(object sender, System.EventArgs e)
        {

            lbl_Procura.Text = "3";

            if (txtCID3.Text.Trim() != string.Empty)
            {
                ddlCID.ClearSelection();

                while (ddlCID.Items.Count > 1)
                    ddlCID.Items.RemoveAt(0);

                DataSet ds = new CID().Get("CodigoCID='" + txtCID3.Text.Trim() + "' OR Descricao LIKE '%" + txtCID3.Text.Trim() + "%' ORDER BY Descricao DESC");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Select())
                        ddlCID.Items.Insert(0, new ListItem(row["Descricao"].ToString(), row["IdCID"].ToString()));
                    txtCID3.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                    lbl_Id3.Text = ddlCID.SelectedValue.ToString().Trim();
                }
                else
                    MsgBox1.Show("Acidente", "O código '" + txtCID3.Text + "' não foi encontrado! Por favor, tente novamente!", null,
                                   new EO.Web.MsgBoxButton("OK"));


                //Aviso("O código '" + txtCID.Text + "' não foi encontrado! Por favor, tente novamente!");
            }
            else

                MsgBox1.Show("Acidente", "É necessário colocar o Código CID para executar a busca!", null,
                               new EO.Web.MsgBoxButton("OK"));

            //Aviso("É necessário colocar o Código CID para executar a busca!");
        }

        protected void btnProcurar4_Click(object sender, System.EventArgs e)
        {

            lbl_Procura.Text = "4";

            if (txtCID4.Text.Trim() != string.Empty)
            {
                ddlCID.ClearSelection();

                while (ddlCID.Items.Count > 1)
                    ddlCID.Items.RemoveAt(0);

                DataSet ds = new CID().Get("CodigoCID='" + txtCID4.Text.Trim() + "' OR Descricao LIKE '%" + txtCID4.Text.Trim() + "%' ORDER BY Descricao DESC");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Select())
                        ddlCID.Items.Insert(0, new ListItem(row["Descricao"].ToString(), row["IdCID"].ToString()));
                    txtCID4.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                    lbl_Id4.Text = ddlCID.SelectedValue.ToString().Trim();
                }
                else
                    MsgBox1.Show("Acidente", "O código '" + txtCID4.Text + "' não foi encontrado! Por favor, tente novamente!", null,
                                   new EO.Web.MsgBoxButton("OK"));


                //Aviso("O código '" + txtCID.Text + "' não foi encontrado! Por favor, tente novamente!");
            }
            else

                MsgBox1.Show("Acidente", "É necessário colocar o Código CID para executar a busca!", null,
                               new EO.Web.MsgBoxButton("OK"));

            //Aviso("É necessário colocar o Código CID para executar a busca!");
        }


		protected void ddlAcidente_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            if (ddlAcidente.SelectedValue != "0")
            {
                Acidente acidente = new Acidente(Convert.ToInt32(ddlAcidente.SelectedValue));

                if (acidente.IdCID.Id != 0)
                {
                    acidente.IdCID.Find();

                    ddlCID.ClearSelection();

                    while (ddlCID.Items.Count > 1)
                        ddlCID.Items.RemoveAt(0);

                    ddlCID.Items.Insert(0, new ListItem(acidente.IdCID.Descricao, acidente.IdCID.Id.ToString()));
                }
            }
		}

        protected void ddlCID_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlCID.SelectedIndex < 0) return;

            if (lbl_Procura.Text == "1")
            {
                txtCID.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                lbl_Id1.Text = ddlCID.SelectedValue.ToString().Trim();
            }
            else if (lbl_Procura.Text == "2")
            {
                txtCID2.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                lbl_Id2.Text = ddlCID.SelectedValue.ToString().Trim();
            }
            else if (lbl_Procura.Text == "3")
            {
                txtCID3.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                lbl_Id3.Text = ddlCID.SelectedValue.ToString().Trim();
            }
            else if (lbl_Procura.Text == "4")
            {
                txtCID4.Text = ddlCID.Items[ddlCID.SelectedIndex].ToString().Trim();
                lbl_Id4.Text = ddlCID.SelectedValue.ToString().Trim();
            }


        }

        protected void btnProjeto_Click(object sender, EventArgs e)
        {

            if (txt_Arq.Text.Trim()=="")
            {
                return;
            }

            try
            {

                //if (Session["Servidor_Web"].ToString().Trim().ToUpper() == "ILITERA")
                //{
                //    if (txt_Arq.Text.Trim().IndexOf("C:\\DRIVE_I") >= 0)
                //    {
                //        Response.Write("<script>");
                //        Response.Write("window.open('" + txt_Arq.Text.Trim().ToUpper().Replace("C:\\DRIVE_I", "http://ilitera.dyndns.ws:8888/driveI").Replace("\\", "/") + "','mywindow','width=450,height=700,left=100,top=100,screenX=0,screenY=100');");
                //        Response.Write("</script>");

                //    }
                //    else
                //    {
                //        Response.Write("<script>");
                //        Response.Write("window.open('" + txt_Arq.Text.Trim().ToUpper().Replace("I:\\FOTOSDOCSDIGITAIS", "http://ilitera.dyndns.ws:8888/driveI/fotosdocsdigitais").Replace("\\", "/") + "','mywindow','width=450,height=700,left=100,top=100,screenX=0,screenY=100');");
                //        Response.Write("</script>");

                //    }
                //}
                //else
                //    if (txt_Arq.Text.ToString().Trim().IndexOf("C:\\DRIVE_I") >= 0)
                //{
                //    Response.Write("<script>");
                //    //    Response.Write("window.open('" + txt_Arq.Text.Trim().ToUpper().Replace("C:\\DRIVE_I", "http://54.94.157.244/driveI").Replace("\\", "/") + "','mywindow','width=450,height=700,left=100,top=100,screenX=0,screenY=100');");
                //    Response.Write("window.open('" + txt_Arq.Text.Trim().ToUpper().Replace("C:\\DRIVE_I", "https://www.ilitera.net.br/driveI").Replace("\\", "/") + "','mywindow','width=450,height=700,left=100,top=100,screenX=0,screenY=100');");

                //    Response.Write("</script>");
                //}
                //else
                //{
                //    Response.Write("<script>");
                //    //Response.Write("window.open('" + txt_Arq.Text.Trim().ToUpper().Replace("I:\\FOTOSDOCSDIGITAIS", "http://54.94.157.244/driveI/fotosdocsdigitais").Replace("\\", "/") + "','mywindow','width=450,height=700,left=100,top=100,screenX=0,screenY=100');");
                //    Response.Write("window.open('" + txt_Arq.Text.Trim().ToUpper().Replace("I:\\FOTOSDOCSDIGITAIS", "https://www.ilitera.net.br/driveI/fotosdocsdigitais").Replace("\\", "/") + "','mywindow','width=450,height=700,left=100,top=100,screenX=0,screenY=100');");
                //    Response.Write("</script>");
                //}


                string xArq = txt_Arq.Text.ToString().Trim().Replace("C:", "I:");


                System.Net.WebClient client = new System.Net.WebClient();
                Byte[] buffer = client.DownloadData(xArq);
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + xArq);
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
                Response.End();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }       


        }



        protected void Envio_Email_Prajna(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {

            string xDestinatario = "";

            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email           
            objEmail.From = new MailAddress("agendamento@5aessencenet.com.br");


            //para
            string xEmail = xPara;

            if (xEmail.Trim() != string.Empty) //objEmail.CC.Add(new MailAddress(xCC));
            {
                string[] stringSeparators4 = new string[] { ";" };
                string[] result4;

                result4 = xEmail.Split(stringSeparators4, StringSplitOptions.RemoveEmptyEntries);

                foreach (string s in result4)
                {
                    if (s.Trim() != "") objEmail.To.Add(s);
                }
            }


            //if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

            //objEmail.To.Add(xEmail);
            xDestinatario = xEmail + "; agendamento@5aessence.com.br ;";

            objEmail.CC.Add("agendamento@5aessence.com.br");


            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            //Attachment xItem = new Attachment(xAttach);
            //objEmail.Attachments.Add(xItem);

            SmtpClient objSmtp = new SmtpClient();
            //objSmtp.Host = "mail.exchange.locaweb.com.br";
            //objSmtp.Port = 587;
            //objSmtp.Credentials = new NetworkCredential("agendamento@essencenet.com.br", "kr.prj1705");

            //objSmtp.EnableSsl = true;

            //objSmtp.Host = "outlook.office.com";
            //objSmtp.Host = "smtp.office365.com";
            //objSmtp.Port = 587;
            //objSmtp.Credentials = new System.Net.NetworkCredential("agendamento@5aessence.com.br", "Agend_5060");

            objSmtp.EnableSsl = false;

            //objSmtp.Host = "outlook.office.com";


            //objSmtp.Host = "smtp.office365.com";
            //objSmtp.Port = 587;                       
            //objSmtp.Credentials = new NetworkCredential("agendamento@5aessence.com.br", "Agend_5060");

            objSmtp.Host = "smtp.5aessencenet.com.br";
            objSmtp.Port = 587;
            objSmtp.Credentials = new System.Net.NetworkCredential("agendamento@5aessencenet.com.br", "Prana@2022!@");


            objSmtp.Send(objEmail);

            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Absenteísmo");

            return;

        }




        protected void Envio_Email_Ilitera(string xPara, string xCopia, string xSubject, string xBody, string xAttach, string xModulo, Int32 xIdEmpregado, Int32 xIdEmpresa)
        {
            string xDestinatario = "";
            //enviar e-mail para clinica e prajna

            MailMessage objEmail = new MailMessage();

            //rementente do email
            objEmail.From = new MailAddress("agendamento1@ilitera.com.br");



            //para
            string xEmail = xPara;

            if (xEmail.Trim() != string.Empty) //objEmail.CC.Add(new MailAddress(xCC));
            {
                string[] stringSeparators4 = new string[] { ";" };
                string[] result4;

                result4 = xEmail.Split(stringSeparators4, StringSplitOptions.RemoveEmptyEntries);

                foreach (string s in result4)
                {
                    if (s.Trim() != "") objEmail.To.Add(s);
                }
            }

            //if (xEmail.IndexOf(";") > 0) xEmail = xEmail.Substring(0, xEmail.IndexOf(";"));

            //objEmail.To.Add(xEmail);
            xDestinatario = xEmail + "; atendimento@ilitera.com.br ;";

            //objEmail.Bcc.Add("oculto@provedor.com.br");

            objEmail.CC.Add("atendimento@ilitera.com.br");


            //if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("EY") > 0)
            //    objEmail.CC.Add("alberto.pereira@br.ey.com");
            //else
            //    objEmail.CC.Add("lucas.sp.sto@ilitera.com.br");


            objEmail.Priority = MailPriority.Normal;
            objEmail.IsBodyHtml = true;

            objEmail.Subject = xSubject;
            objEmail.Body = xBody;
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            //Attachment xItem = new Attachment(xAttach);
            //objEmail.Attachments.Add(xItem);

            SmtpClient objSmtp = new SmtpClient();
            //objSmtp.Host = "smtp.ilitera.com.br";
            objSmtp.Host = "smtp.office365.com";
            objSmtp.EnableSsl = true;
            objSmtp.Port = 587;
            objSmtp.Credentials = new System.Net.NetworkCredential("agendamento1@ilitera.com.br", "Ilitera_3624");


            Ilitera.Data.Clientes_Clinicas xEnvio = new Ilitera.Data.Clientes_Clinicas();
            objSmtp.Send(objEmail);

            xEnvio.Envio_eMail(xIdEmpregado, xIdEmpresa, xDestinatario, "Absenteísmo");
            


            return;

        }



    }
}
