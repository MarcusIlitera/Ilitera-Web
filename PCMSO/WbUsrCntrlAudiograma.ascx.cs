using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Ilitera.Opsa.Data;
using System.Text;


namespace Ilitera.Net.PCMSO
{
	/// <summary>
	///		Summary description for WbUsrCntrlAudiograma.
	/// </summary>
	public partial  class WbUsrCntrlAudiograma : System.Web.UI.UserControl
	{
		private int _IdEmpregado;
		private int _IndOrelha;

		protected void Page_Load(object sender, System.EventArgs e)
		{

		}

		public void PopulaDDLDiagnostico()
		{
			ddlDiagnostico.DataSource = new Diagnostico().GetAll("Ordem");
			ddlDiagnostico.DataTextField = "Nome";
			ddlDiagnostico.DataValueField = "IdDiagnostico";
			ddlDiagnostico.DataBind();
		}

		public void PopulaAudiograma(Audiometria audiometria, int IndOrelha)
		{
			AudiometriaAudiograma audiometriaAudiograma = new AudiometriaAudiograma();
			audiometriaAudiograma.Find("IdAudiometria=" + audiometria.Id + " AND IndOrelha=" + IndOrelha.ToString());
            
			rblRefSeq.ClearSelection();			
			if (audiometriaAudiograma.IsReferencial)
				rblRefSeq.Items.FindByValue("0").Selected = true;
			else
				rblRefSeq.Items.FindByValue("1").Selected = true;

			ckbMascAA025.Enabled = audiometriaAudiograma.IsAereoMascarado250;
			ckbMascAA025.Checked = audiometriaAudiograma.IsAereoMascarado250;
			ckbMascAA05.Enabled = audiometriaAudiograma.IsAereoMascarado500;
			ckbMascAA05.Checked = audiometriaAudiograma.IsAereoMascarado500;
			ckbMascAA1.Enabled = audiometriaAudiograma.IsAereoMascarado1000;
			ckbMascAA1.Checked = audiometriaAudiograma.IsAereoMascarado1000;
			ckbMascAA2.Enabled = audiometriaAudiograma.IsAereoMascarado2000;
			ckbMascAA2.Checked = audiometriaAudiograma.IsAereoMascarado2000;
			ckbMascAA3.Enabled = audiometriaAudiograma.IsAereoMascarado3000;
			ckbMascAA3.Checked = audiometriaAudiograma.IsAereoMascarado3000;
			ckbMascAA4.Enabled = audiometriaAudiograma.IsAereoMascarado4000;
			ckbMascAA4.Checked = audiometriaAudiograma.IsAereoMascarado4000;
			ckbMascAA6.Enabled = audiometriaAudiograma.IsAereoMascarado6000;
			ckbMascAA6.Checked = audiometriaAudiograma.IsAereoMascarado6000;
			ckbMascAA8.Enabled = audiometriaAudiograma.IsAereoMascarado8000;
			ckbMascAA8.Checked = audiometriaAudiograma.IsAereoMascarado8000;

            if (audiometriaAudiograma.Aereo250!="")			ddlAA025.Items.FindByValue(audiometriaAudiograma.Aereo250).Selected = true;
            if (audiometriaAudiograma.Aereo500 != "")       ddlAA05.Items.FindByValue(audiometriaAudiograma.Aereo500).Selected = true;
            if (audiometriaAudiograma.Aereo1000 != "")       ddlAA1.Items.FindByValue(audiometriaAudiograma.Aereo1000).Selected = true;
            if (audiometriaAudiograma.Aereo2000 != "")       ddlAA2.Items.FindByValue(audiometriaAudiograma.Aereo2000).Selected = true;
            if (audiometriaAudiograma.Aereo3000 != "")       ddlAA3.Items.FindByValue(audiometriaAudiograma.Aereo3000).Selected = true;
            if (audiometriaAudiograma.Aereo4000  != "")       ddlAA4.Items.FindByValue(audiometriaAudiograma.Aereo4000).Selected = true;
            if (audiometriaAudiograma.Aereo6000 != "")       ddlAA6.Items.FindByValue(audiometriaAudiograma.Aereo6000).Selected = true;
            if (audiometriaAudiograma.Aereo8000 != "")       ddlAA8.Items.FindByValue(audiometriaAudiograma.Aereo8000).Selected = true;

            if (audiometriaAudiograma.Osseo500 != "") ddlOA05.Items.FindByValue(audiometriaAudiograma.Osseo500).Selected = true;
            if (audiometriaAudiograma.Osseo1000 != "") ddlOA1.Items.FindByValue(audiometriaAudiograma.Osseo1000).Selected = true;
            if (audiometriaAudiograma.Osseo2000 != "") ddlOA2.Items.FindByValue(audiometriaAudiograma.Osseo2000).Selected = true;
            if (audiometriaAudiograma.Osseo3000 != "") ddlOA3.Items.FindByValue(audiometriaAudiograma.Osseo3000).Selected = true;
            if (audiometriaAudiograma.Osseo4000 != "") ddlOA4.Items.FindByValue(audiometriaAudiograma.Osseo4000).Selected = true;
            if (audiometriaAudiograma.Osseo6000 != "") ddlOA6.Items.FindByValue(audiometriaAudiograma.Osseo6000).Selected = true;

			ckbMascOA05.Enabled = audiometriaAudiograma.IsOsseoMascarado500;
			ckbMascOA05.Checked = audiometriaAudiograma.IsOsseoMascarado500;
			ckbMascOA1.Enabled = audiometriaAudiograma.IsOsseoMascarado1000;
			ckbMascOA1.Checked = audiometriaAudiograma.IsOsseoMascarado1000;
			ckbMascOA2.Enabled = audiometriaAudiograma.IsOsseoMascarado2000;
			ckbMascOA2.Checked = audiometriaAudiograma.IsOsseoMascarado2000;
			ckbMascOA3.Enabled = audiometriaAudiograma.IsOsseoMascarado3000;
			ckbMascOA3.Checked = audiometriaAudiograma.IsOsseoMascarado3000;
			ckbMascOA4.Enabled = audiometriaAudiograma.IsOsseoMascarado4000;
			ckbMascOA4.Checked = audiometriaAudiograma.IsOsseoMascarado4000;
			ckbMascOA6.Enabled = audiometriaAudiograma.IsOsseoMascarado6000;
			ckbMascOA6.Checked = audiometriaAudiograma.IsOsseoMascarado6000;

			txtIAtual.Text = audiometriaAudiograma.InterpretacaoPortaria19();

			if (!audiometriaAudiograma.IsReferencial)
			{
				audiometriaAudiograma.IdAudiogramaReferencial.Find();

				if (audiometriaAudiograma.IdAudiogramaReferencial.IsAereoMascarado250)
                    wteAR025.Text = "M " + audiometriaAudiograma.IdAudiogramaReferencial.Aereo250;
				else
					wteAR025.Text = audiometriaAudiograma.IdAudiogramaReferencial.Aereo250;
				if (audiometriaAudiograma.IdAudiogramaReferencial.IsAereoMascarado500)
                    wteAR05.Text = "M " + audiometriaAudiograma.IdAudiogramaReferencial.Aereo500;
				else
					wteAR05.Text = audiometriaAudiograma.IdAudiogramaReferencial.Aereo500;
				if (audiometriaAudiograma.IdAudiogramaReferencial.IsAereoMascarado1000)
                    wteAR1.Text = "M " + audiometriaAudiograma.IdAudiogramaReferencial.Aereo1000;
				else
					wteAR1.Text = audiometriaAudiograma.IdAudiogramaReferencial.Aereo1000;
				if (audiometriaAudiograma.IdAudiogramaReferencial.IsAereoMascarado2000)
                    wteAR2.Text = "M " + audiometriaAudiograma.IdAudiogramaReferencial.Aereo2000;
				else
					wteAR2.Text = audiometriaAudiograma.IdAudiogramaReferencial.Aereo2000;
				if (audiometriaAudiograma.IdAudiogramaReferencial.IsAereoMascarado3000)
                    wteAR3.Text = "M " + audiometriaAudiograma.IdAudiogramaReferencial.Aereo3000;
				else
					wteAR3.Text = audiometriaAudiograma.IdAudiogramaReferencial.Aereo3000;
				if (audiometriaAudiograma.IdAudiogramaReferencial.IsAereoMascarado4000)
                    wteAR4.Text = "M " + audiometriaAudiograma.IdAudiogramaReferencial.Aereo4000;
				else
					wteAR4.Text = audiometriaAudiograma.IdAudiogramaReferencial.Aereo4000;
				if (audiometriaAudiograma.IdAudiogramaReferencial.IsAereoMascarado6000)
                    wteAR6.Text = "M " + audiometriaAudiograma.IdAudiogramaReferencial.Aereo6000;
				else
					wteAR6.Text = audiometriaAudiograma.IdAudiogramaReferencial.Aereo6000;
				if (audiometriaAudiograma.IdAudiogramaReferencial.IsAereoMascarado8000)
                    wteAR8.Text = "M " + audiometriaAudiograma.IdAudiogramaReferencial.Aereo8000;
				else
					wteAR8.Text = audiometriaAudiograma.IdAudiogramaReferencial.Aereo8000;

				if (audiometriaAudiograma.IdAudiogramaReferencial.IsOsseoMascarado500)
                    wteOR05.Text = "M " + audiometriaAudiograma.IdAudiogramaReferencial.Osseo500;
				else
					wteOR05.Text = audiometriaAudiograma.IdAudiogramaReferencial.Osseo500;
				if (audiometriaAudiograma.IdAudiogramaReferencial.IsOsseoMascarado1000)
                    wteOR1.Text = "M " + audiometriaAudiograma.IdAudiogramaReferencial.Osseo1000;
				else
					wteOR1.Text = audiometriaAudiograma.IdAudiogramaReferencial.Osseo1000;
				if (audiometriaAudiograma.IdAudiogramaReferencial.IsOsseoMascarado2000)
                    wteOR2.Text = "M " + audiometriaAudiograma.IdAudiogramaReferencial.Osseo2000;
				else
					wteOR2.Text = audiometriaAudiograma.IdAudiogramaReferencial.Osseo2000;
				if (audiometriaAudiograma.IdAudiogramaReferencial.IsOsseoMascarado3000)
                    wteOR3.Text = "M " + audiometriaAudiograma.IdAudiogramaReferencial.Osseo3000;
				else
					wteOR3.Text = audiometriaAudiograma.IdAudiogramaReferencial.Osseo3000;
				if (audiometriaAudiograma.IdAudiogramaReferencial.IsOsseoMascarado4000)
					wteOR4.Text = "M " + audiometriaAudiograma.IdAudiogramaReferencial.Osseo4000;
				else
					wteOR4.Text = audiometriaAudiograma.IdAudiogramaReferencial.Osseo4000;
				if (audiometriaAudiograma.IdAudiogramaReferencial.IsOsseoMascarado6000)
                    wteOR6.Text = "M " + audiometriaAudiograma.IdAudiogramaReferencial.Osseo6000;
				else
					wteOR6.Text = audiometriaAudiograma.IdAudiogramaReferencial.Osseo6000;

				txtIReferencial.Text = audiometriaAudiograma.InterpretacaoPortaria19();
			}

			rblMeatoscopia.ClearSelection();
			if (audiometriaAudiograma.IsMeatoscopiaAlterada)
				rblMeatoscopia.Items.FindByValue("1").Selected = true;
			else
				rblMeatoscopia.Items.FindByValue("0").Selected = true;

			txtDescMeatoscopia.Text = audiometriaAudiograma.ObsMeatoscopia;
			
			ddlDiagnostico.ClearSelection();

            if (audiometriaAudiograma.IdDiagnostico.Id.ToString() != "0" )			ddlDiagnostico.Items.FindByValue(audiometriaAudiograma.IdDiagnostico.Id.ToString()).Selected = true;

			ckbIsOcupacional.Checked = audiometriaAudiograma.IsOcupacional;
			if (!ddlDiagnostico.SelectedValue.Equals("1"))
				ckbIsOcupacional.Enabled = true;

			txtObs.Text = audiometriaAudiograma.ObsDiagnostico;
		}

		protected void ckbMascarado_CheckedChanged(object sender, EventArgs e)
		{
			if (!ckbMascAA025.Checked)
				ckbMascAA025.Enabled = ckbMascarado.Checked;
			if (!ckbMascAA05.Checked)
				ckbMascAA05.Enabled = ckbMascarado.Checked;
			if (!ckbMascAA1.Checked)
				ckbMascAA1.Enabled = ckbMascarado.Checked;
			if (!ckbMascAA2.Checked)
				ckbMascAA2.Enabled = ckbMascarado.Checked;
			if (!ckbMascAA3.Checked)
				ckbMascAA3.Enabled = ckbMascarado.Checked;
			if (!ckbMascAA4.Checked)
				ckbMascAA4.Enabled = ckbMascarado.Checked;
			if (!ckbMascAA6.Checked)
				ckbMascAA6.Enabled = ckbMascarado.Checked;
			if (!ckbMascAA8.Checked)
				ckbMascAA8.Enabled = ckbMascarado.Checked;

			if (!ckbMascOA05.Checked)
				ckbMascOA05.Enabled = ckbMascarado.Checked;
			if (!ckbMascOA1.Checked)
				ckbMascOA1.Enabled = ckbMascarado.Checked;
			if (!ckbMascOA2.Checked)
				ckbMascOA2.Enabled = ckbMascarado.Checked;
			if (!ckbMascOA3.Checked)
				ckbMascOA3.Enabled = ckbMascarado.Checked;
			if (!ckbMascOA4.Checked)
				ckbMascOA4.Enabled = ckbMascarado.Checked;
			if (!ckbMascOA6.Checked)
				ckbMascOA6.Enabled = ckbMascarado.Checked;
		}

		protected void ddlDiagnostico_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddlDiagnostico.SelectedValue.Equals("1"))
			{
				ckbIsOcupacional.Checked = false;
				ckbIsOcupacional.Enabled = false;
			}
			else
				ckbIsOcupacional.Enabled = true;
		}

		public AudiometriaAudiograma GetAudiograma(Audiometria audiometria, int IndOrelha)
		{
			AudiometriaAudiograma audiometriaAudiograma = new AudiometriaAudiograma();
			audiometriaAudiograma.Find("IdAudiometria=" + audiometria.Id + " AND IndOrelha=" + IndOrelha);

			if (audiometriaAudiograma.Id.Equals(0))
			{
				audiometriaAudiograma.Inicialize();
				audiometriaAudiograma.IndOrelha = IndOrelha;
			}

            audiometriaAudiograma.IsReferencial = rblRefSeq.Items.FindByValue("0").Selected;
            
			audiometriaAudiograma.IsAereoMascarado250 = ckbMascAA025.Checked;
			audiometriaAudiograma.IsAereoMascarado500 = ckbMascAA05.Checked;
			audiometriaAudiograma.IsAereoMascarado1000 = ckbMascAA1.Checked;
			audiometriaAudiograma.IsAereoMascarado2000 = ckbMascAA2.Checked;
			audiometriaAudiograma.IsAereoMascarado3000 = ckbMascAA3.Checked;
			audiometriaAudiograma.IsAereoMascarado4000 = ckbMascAA4.Checked;
			audiometriaAudiograma.IsAereoMascarado6000 = ckbMascAA6.Checked;
			audiometriaAudiograma.IsAereoMascarado8000 = ckbMascAA8.Checked;

			audiometriaAudiograma.Aereo250 = ddlAA025.SelectedValue;
			audiometriaAudiograma.Aereo500 = ddlAA05.SelectedValue;
			audiometriaAudiograma.Aereo1000 = ddlAA1.SelectedValue;
			audiometriaAudiograma.Aereo2000 = ddlAA2.SelectedValue;
			audiometriaAudiograma.Aereo3000 = ddlAA3.SelectedValue;
			audiometriaAudiograma.Aereo4000 = ddlAA4.SelectedValue;
			audiometriaAudiograma.Aereo6000 = ddlAA6.SelectedValue;
			audiometriaAudiograma.Aereo8000 = ddlAA8.SelectedValue;

			audiometriaAudiograma.Osseo500 = ddlOA05.SelectedValue;
			audiometriaAudiograma.Osseo1000 = ddlOA1.SelectedValue;
			audiometriaAudiograma.Osseo2000 = ddlOA2.SelectedValue;
			audiometriaAudiograma.Osseo3000 = ddlOA3.SelectedValue;
			audiometriaAudiograma.Osseo4000 = ddlOA4.SelectedValue;
			audiometriaAudiograma.Osseo6000 = ddlOA6.SelectedValue;

			audiometriaAudiograma.IsOsseoMascarado500 = ckbMascOA05.Checked;
			audiometriaAudiograma.IsOsseoMascarado1000 = ckbMascOA1.Checked;
			audiometriaAudiograma.IsOsseoMascarado2000 = ckbMascOA2.Checked;
			audiometriaAudiograma.IsOsseoMascarado3000 = ckbMascOA3.Checked;
			audiometriaAudiograma.IsOsseoMascarado4000 = ckbMascOA4.Checked;
			audiometriaAudiograma.IsOsseoMascarado6000 = ckbMascOA6.Checked;

			if (!audiometriaAudiograma.IsReferencial)
				audiometriaAudiograma.IdAudiogramaReferencial = audiometriaAudiograma.GetAudiogramaReferencial(audiometria.IdEmpregado.Id);
			
			audiometriaAudiograma.IsMeatoscopiaAlterada = rblMeatoscopia.Items.FindByValue("1").Selected;
			audiometriaAudiograma.ObsMeatoscopia = txtDescMeatoscopia.Text.Trim();
			
			audiometriaAudiograma.IdDiagnostico.Id = Convert.ToInt32(ddlDiagnostico.SelectedValue);
			audiometriaAudiograma.IsOcupacional = ckbIsOcupacional.Checked;
			audiometriaAudiograma.ObsDiagnostico = txtObs.Text.Trim();

			return audiometriaAudiograma;
		}

		public string InterpretacaoAtual
		{
			set
			{
				txtIAtual.Text = value;
			}
		}
		
		public int IdEmpregado
		{
			set{_IdEmpregado = value;}
			get{return _IdEmpregado;}
		}
		
		public int IndOrelha
		{
			set{_IndOrelha = value;}
			get{return _IndOrelha;}
		}

		protected void rblRefSeq_SelectedIndexChanged(object sender, EventArgs e)
		{
			AudiometriaAudiograma audiometriaReferencial = new AudiometriaAudiograma();
			audiometriaReferencial.IndOrelha = this.IndOrelha;
			audiometriaReferencial = audiometriaReferencial.GetAudiogramaReferencial(this.IdEmpregado);

			if (rblRefSeq.SelectedValue.Equals("0"))
			{
				wteAR025.Text = string.Empty;
				wteAR05.Text = string.Empty;
				wteAR1.Text = string.Empty;
				wteAR2.Text = string.Empty;
				wteAR3.Text = string.Empty;
				wteAR4.Text = string.Empty;
				wteAR6.Text = string.Empty;
				wteAR8.Text = string.Empty;

				wteOR05.Text = string.Empty;
				wteOR1.Text = string.Empty;
				wteOR2.Text = string.Empty;
				wteOR3.Text = string.Empty;
				wteOR4.Text = string.Empty;
				wteOR6.Text = string.Empty;

				txtIReferencial.Text = string.Empty;
			}
			//else if (rblRefSeq.SelectedValue.Equals("1") && audiometriaReferencial.Id.Equals(0))
			//{
			//	rblRefSeq.ClearSelection();
			//	rblRefSeq.Items.FindByValue("0").Selected = true;

			//	StringBuilder st = new StringBuilder();

			//	st.Append("window.alert('Este Audiograma não pode ser Sequencial! Não existe nenhum exame Referencial já realizado para esta Orelha');");
				
			//	this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Aviso", st.ToString(), true);
			//}
			else 
			{
				if (audiometriaReferencial.IsAereoMascarado250)
					wteAR025.Text = "M " + audiometriaReferencial.Aereo250;
				else
					wteAR025.Text = audiometriaReferencial.Aereo250;
				if (audiometriaReferencial.IsAereoMascarado500)
					wteAR05.Text = "M " + audiometriaReferencial.Aereo500;
				else
					wteAR05.Text = audiometriaReferencial.Aereo500;
				if (audiometriaReferencial.IsAereoMascarado1000)
					wteAR1.Text = "M " + audiometriaReferencial.Aereo1000;
				else
					wteAR1.Text = audiometriaReferencial.Aereo1000;
				if (audiometriaReferencial.IsAereoMascarado2000)
					wteAR2.Text = "M " + audiometriaReferencial.Aereo2000;
				else
					wteAR2.Text = audiometriaReferencial.Aereo2000;
				if (audiometriaReferencial.IsAereoMascarado3000)
					wteAR3.Text = "M " + audiometriaReferencial.Aereo3000;
				else
					wteAR3.Text = audiometriaReferencial.Aereo3000;
				if (audiometriaReferencial.IsAereoMascarado4000)
					wteAR4.Text = "M " + audiometriaReferencial.Aereo4000;
				else
					wteAR4.Text = audiometriaReferencial.Aereo4000;
				if (audiometriaReferencial.IsAereoMascarado6000)
					wteAR6.Text = "M " + audiometriaReferencial.Aereo6000;
				else
					wteAR6.Text = audiometriaReferencial.Aereo6000;
				if (audiometriaReferencial.IsAereoMascarado8000)
					wteAR8.Text = "M " + audiometriaReferencial.Aereo8000;
				else
					wteAR8.Text = audiometriaReferencial.Aereo8000;

				if (audiometriaReferencial.IsOsseoMascarado500)
					wteOR05.Text = "M " + audiometriaReferencial.Osseo500;
				else
					wteOR05.Text = audiometriaReferencial.Osseo500;
				if (audiometriaReferencial.IsOsseoMascarado1000)
					wteOR1.Text = "M " + audiometriaReferencial.Osseo1000;
				else
					wteOR1.Text = audiometriaReferencial.Osseo1000;
				if (audiometriaReferencial.IsOsseoMascarado2000)
					wteOR2.Text = "M " + audiometriaReferencial.Osseo2000;
				else
					wteOR2.Text = audiometriaReferencial.Osseo2000;
				if (audiometriaReferencial.IsOsseoMascarado3000)
					wteOR3.Text = "M " + audiometriaReferencial.Osseo3000;
				else
					wteOR3.Text = audiometriaReferencial.Osseo3000;
				if (audiometriaReferencial.IsOsseoMascarado4000)
					wteOR4.Text = "M " + audiometriaReferencial.Osseo4000;
				else
					wteOR4.Text = audiometriaReferencial.Osseo4000;
				if (audiometriaReferencial.IsOsseoMascarado6000)
					wteOR6.Text = "M " + audiometriaReferencial.Osseo6000;
				else
					wteOR6.Text = audiometriaReferencial.Osseo6000;

				txtIReferencial.Text = audiometriaReferencial.InterpretacaoPortaria19();
			}
		}
	}
}
