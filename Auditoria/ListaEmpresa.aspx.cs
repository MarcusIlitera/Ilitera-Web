using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Text;

using System.Drawing;

namespace Ilitera.Net
{
	/// <summary>
	///
	/// </summary>
    public partial class ListaEmpresa : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblEmpresa;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
			{
                Usuario usuario = new Usuario();
                usuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

				DataSet ds = new Cliente().Lista(usuario, string.Empty);
				
				if(ds.Tables[0].Rows.Count == 1 && !ds.Tables[0].Rows[0]["AtivarLocalDeTrabalho"].ToString().Equals("True"))
				{
					StringBuilder st = new StringBuilder();

					st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpresa').value = '" + ds.Tables[0].Rows[0]["Id"] + "';");
					st.Append("window.location.href='ListagemIrregularidades.aspx?IdEmpresa=" + ds.Tables[0].Rows[0]["Id"] + "&IdUsuario=" + usuario.Id + "';");

                    this.ClientScript.RegisterStartupScript(this.GetType(), "GoToIrregularidades", st.ToString(), true);
				}
				else
				{
					//PreencheLabels("lblEmp", "");
					//ResetIdValues();
					lisTodas.NavigateUrl = "ListaEmpresa.aspx?IdUsuario=" + usuario.Id;
					
					if (!usuario.Id.Equals(1))
					{
                        Prestador prestador = new Prestador();
                        prestador.Find("IdPessoa=" + usuario.IdPessoa.Id);

						prestador.IdJuridica.Find();
						ckbEnableLocais.Enabled = !(prestador.IdJuridica.IsLocalDeTrabalho());
					}

					if (ds.Tables[0].Rows.Count > 0)
						lblTotRegistros.Text = "Total de registros: <b>" + ds.Tables[0].Rows.Count.ToString() + "</b>";
					else
						lblTotRegistros.Text = "Nenhum registro encontrado";
					
					//UltraWebGridListaEmpresa.DataSource = ds;
					//UltraWebGridListaEmpresa.DataBind();
				}
			}
		}

		protected void btnLocalizar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			lblError.Text = string.Empty;
			if (Page.IsValid)
			{
                Usuario usuario = new Usuario();
                usuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

				DataSet dsSource = new Cliente().Lista(usuario, txtEmpresa.Text.Trim(), ckbEnableLocais.Checked, txtLocalTrabalho.Text.Trim(), Convert.ToInt32(rblFiltroLocaisTrabalho.SelectedValue));
				
                //UltraWebGridListaEmpresa.DisplayLayout.Pager.CurrentPageIndex = 1;

                //if (ckbEnableLocais.Checked)
                //{
                //    if (dsSource.Tables.Count.Equals(1))
                //    {
                //        SetUltraWebGridType(UltraWebGridListaEmpresa, false);
                //        //Aviso("Nenhum Local de Trabalho foi encontrado!");
                //    }
                //    else if (dsSource.Tables.Count.Equals(2))
                //        SetUltraWebGridType(UltraWebGridListaEmpresa, true);

                //    txtLocalTrabalho.BorderColor = Color.FromName("#7CC5A1");
                //    txtLocalTrabalho.BackColor = Color.FromName("#FCFEFD");
                //    lblBuscaLocalTrab.ForeColor = Color.FromName("#44926D");
                //}
                //else
                //    txtLocalTrabalho.BackColor = Color.FromName("#EBEBEB");

                //PopulaUltraWebGrid(UltraWebGridListaEmpresa, dsSource, lblTotRegistros);
			}
		}

		private DataSet GeraDataSet()
		{
            Usuario usuario = new Usuario();
            usuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

			DataSet ds = new Cliente().Lista(usuario, txtEmpresa.Text.Trim(), ckbEnableLocais.Checked, txtLocalTrabalho.Text.Trim(), Convert.ToInt32(rblFiltroLocaisTrabalho.SelectedValue));
			
			if (ds.Tables[0].Rows.Count > 0)
				lblTotRegistros.Text = "Total de registros: <b>" + ds.Tables[0].Rows.Count.ToString() + "</b>";
			else
				lblTotRegistros.Text = "Nenhum registro encontrado";
			
			return ds;
		}

		protected void cvdCaracteres_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
		{
			//VerificaCaracter(args, txtEmpresa, cvdCaracteres);
		}

        //protected void UltraWebGridListaEmpresa_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        //{
        //    lblError.Text = string.Empty;

        //    if (VerificaCaracter(txtEmpresa, lblError) && VerificaCaracter(txtLocalTrabalho, lblError))
        //    {
        //        InicializaWebPageObjects();

        //        DataSet dsSource = new Cliente().Lista(usuario, txtEmpresa.Text.Trim(), ckbEnableLocais.Checked, txtLocalTrabalho.Text.Trim(), Convert.ToInt32(rblFiltroLocaisTrabalho.SelectedValue));
				
        //        if (ckbEnableLocais.Checked)
        //        {
        //            if (dsSource.Tables.Count.Equals(1))
        //                SetUltraWebGridType(UltraWebGridListaEmpresa, false);
        //            else if (dsSource.Tables.Count.Equals(2))
        //                SetUltraWebGridType(UltraWebGridListaEmpresa, true);

        //            txtLocalTrabalho.BorderColor = Color.FromName("#7CC5A1");
        //            txtLocalTrabalho.BackColor = Color.FromName("#FCFEFD");
        //            lblBuscaLocalTrab.ForeColor = Color.FromName("#44926D");
        //        }
        //        else
        //            txtLocalTrabalho.BackColor = Color.FromName("#EBEBEB");
				
        //        PopulaUltraWebGrid(UltraWebGridListaEmpresa, dsSource, lblTotRegistros);

        //        if (UltraWebGridListaEmpresa.DisplayLayout.Pager.PageCount < e.NewPageIndex)
        //            UltraWebGridListaEmpresa.DisplayLayout.Pager.CurrentPageIndex = UltraWebGridListaEmpresa.DisplayLayout.Pager.PageCount;
        //        else
        //            UltraWebGridListaEmpresa.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
				
        //        UltraWebGridListaEmpresa.DataBind();
        //    }
        //}

		protected void ckbEnableLocais_CheckedChanged(object sender, System.EventArgs e)
		{
			lblError.Text = string.Empty;

            //if (VerificaCaracter(txtEmpresa, lblError) && VerificaCaracter(txtLocalTrabalho, lblError))
            //{
            //    DataSet dsSource = new Cliente().Lista(usuario, txtEmpresa.Text.Trim(), ckbEnableLocais.Checked, txtLocalTrabalho.Text.Trim(), Convert.ToInt32(rblFiltroLocaisTrabalho.SelectedValue));
				
            //    rblFiltroLocaisTrabalho.Enabled = ckbEnableLocais.Checked;
				
            //    if (ckbEnableLocais.Checked)
            //    {
            //        if (dsSource.Tables.Count.Equals(1))
            //            SetUltraWebGridType(UltraWebGridListaEmpresa, false);
            //        else if (dsSource.Tables.Count.Equals(2))
            //            SetUltraWebGridType(UltraWebGridListaEmpresa, true);
					
            //        txtLocalTrabalho.ReadOnly = false;
            //        txtLocalTrabalho.BorderColor = Color.FromName("#7CC5A1");
            //        txtLocalTrabalho.BackColor = Color.FromName("#FCFEFD");
            //        lblBuscaLocalTrab.ForeColor = Color.FromName("#44926D");
            //    }
            //    else
            //    {
            //        SetUltraWebGridType(UltraWebGridListaEmpresa, false);
					
            //        txtLocalTrabalho.Text = string.Empty;
            //        txtLocalTrabalho.ReadOnly = true;
            //        txtLocalTrabalho.BorderColor = Color.LightGray;
            //        txtLocalTrabalho.BackColor = Color.FromName("#EBEBEB");
            //        lblBuscaLocalTrab.ForeColor = Color.Gray;
            //    }

            //    PopulaUltraWebGrid(UltraWebGridListaEmpresa, dsSource, lblTotRegistros);

            //    if (UltraWebGridListaEmpresa.DisplayLayout.Pager.CurrentPageIndex > UltraWebGridListaEmpresa.DisplayLayout.Pager.PageCount)
            //    {
            //        UltraWebGridListaEmpresa.DisplayLayout.Pager.CurrentPageIndex = UltraWebGridListaEmpresa.DisplayLayout.Pager.PageCount;
            //        UltraWebGridListaEmpresa.DataBind();
            //    }
            //}
		}

		protected void cvdLocalTrabalho_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
		{
			//VerificaCaracter(args, txtLocalTrabalho, cvdLocalTrabalho);
		}

		protected void rblFiltroLocaisTrabalho_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            Usuario usuario = new Usuario();
            usuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

			DataSet dsSource = new Cliente().Lista(usuario, txtEmpresa.Text.Trim(), ckbEnableLocais.Checked, txtLocalTrabalho.Text.Trim(), Convert.ToInt32(rblFiltroLocaisTrabalho.SelectedValue));
			
            //if (dsSource.Tables.Count.Equals(1))
            //    SetUltraWebGridType(UltraWebGridListaEmpresa, false);
            //else if (dsSource.Tables.Count.Equals(2))
            //    SetUltraWebGridType(UltraWebGridListaEmpresa, true);

            //PopulaUltraWebGrid(UltraWebGridListaEmpresa, dsSource, lblTotRegistros);

            //if (UltraWebGridListaEmpresa.DisplayLayout.Pager.CurrentPageIndex > UltraWebGridListaEmpresa.DisplayLayout.Pager.PageCount)
            //{
            //    UltraWebGridListaEmpresa.DisplayLayout.Pager.CurrentPageIndex = UltraWebGridListaEmpresa.DisplayLayout.Pager.PageCount;
            //    UltraWebGridListaEmpresa.DataBind();
            //}

            txtLocalTrabalho.BorderColor = Color.FromName("#7CC5A1");
            txtLocalTrabalho.BackColor = Color.FromName("#FCFEFD");
            lblBuscaLocalTrab.ForeColor = Color.FromName("#44926D");
		}
	}
}
