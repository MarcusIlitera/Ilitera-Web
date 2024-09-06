using System;
using System.Data;
using System.Collections;
using Ilitera.Opsa.Data;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using System.Text;
using Ilitera.Common;

namespace Ilitera.Opsa.Report
{
	public class DataSourceBioSegurancaIndividual
	{
		private static Template template = new Template();
		private float usedHeight;
		private Page page;
		private ArrayList alTopicos;
		private Document document;
		//private TextArea style;

		static DataSourceBioSegurancaIndividual()
		{
			PageNumberingLabel pageNumLabel = new PageNumberingLabel("Página %%CP%% de %%TP%%", 0F, 780F, 515F, 10F, Font.Helvetica, 7, TextAlign.Right);
			template.Elements.Add(pageNumLabel);
		}
		
		public DataSourceBioSegurancaIndividual(int IdEmpregado)
		{
            Empregado empregado = new Empregado();
            empregado.Find(Convert.ToInt32(IdEmpregado));

            EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);

            Ghe ghe = empregadoFuncao.GetGheEmpregado(); 

			if (ghe.Id.Equals(0))
				throw new Exception("O empregado selecionado não se encontra cadastrado em nenhum GHE do último Laudo Técnico realizado! Por gentileza, entre em contato com a Ilitera.");
			
			alTopicos = new TopicoBioSeguranca().Find("IdGHE=" + ghe.Id + " ORDER BY Sequencia");

			if (alTopicos.Count.Equals(0))
				throw new Exception("O Manual de BioSegurança para o Empregado selecionado ainda não está disponível!");
			
			// Create a document and set it's properties
			document = new Document();
			document.Creator = "Ilitera.NET";
			document.Author = "Ilitera.NET";
			document.Title = "Manual Individual de BioSegurança";
				
			// Add the template to the document
			document.Template = template;

			// Create an HTMLTextAreaStyle and set the initial properties for the HTMLTextArea
            //style = new TextArea(FontFamily.Helvetica, 10, false);
            //style.Paragraph.SpacingAfter = 6;
            //style.Paragraph.AllowOrphanLines = false;
		}
        
		public Document GetReport()
		{
			GetCapa();
			
			page = new Page(PageSize.A4, PageOrientation.Portrait, 40F);
			usedHeight = 0F;
			int numTopicos = 0;
				
			foreach (TopicoBioSeguranca topico in alTopicos)
			{
				numTopicos += 1;
					
				DataSet dsImgSupTopico = new TopicoBioSegurancaImage().Get("IdTopicoBioSeguranca=" + topico.Id
					+" AND IndPosicao=" + (int)IndPosicaoImage.Superior);

				DataSet dsImgInfTopico = new TopicoBioSegurancaImage().Get("IdTopicoBioSeguranca=" + topico.Id
					+" AND IndPosicao=" + (int)IndPosicaoImage.Inferior);

				if (dsImgSupTopico.Tables[0].Rows.Count.Equals(1))
					GetImageTopDown(dsImgSupTopico.Tables[0].Rows[0]["PathImage"].ToString());

                //TextArea htmlTextArea = null;
					
                //if (!topico.TextoHTML.Trim().Equals(string.Empty))
                //    htmlTextArea = new TextArea(topico.TextoHTML, 0F, usedHeight, 515F, 770F - usedHeight, style);
			
				// Loop untill all of the text is displayed
                //while(htmlTextArea != null)
                //    htmlTextArea = AddTextToDocument(document, htmlTextArea);

				if (dsImgInfTopico.Tables[0].Rows.Count.Equals(1))
					GetImageTopDown(dsImgInfTopico.Tables[0].Rows[0]["PathImage"].ToString());

				usedHeight += 15F;

				if (numTopicos.Equals(alTopicos.Count) && page.Elements.Count > 0)
					document.Pages.Add(page);
			}

			return document;
		}

        //private TextArea AddTextToDocument(Document document, TextArea htmlTextArea)
        //{
        //    if (!htmlTextArea.HasOverflowText())
        //    {
        //        htmlTextArea.Height = htmlTextArea.GetRequiredHeight();
        //        usedHeight += htmlTextArea.GetRequiredHeight() + 8F;

        //        page.Elements.Add(htmlTextArea);

        //        return null;
        //    }
        //    else
        //    {
        //        page.Elements.Add(htmlTextArea);
        //        document.Pages.Add(page);
        //        usedHeight = 0F;
        //        page = new Page(PageSize.A4, PageOrientation.Portrait, 40F);

        //        //return htmlTextArea.GetOverflowHtmlTextArea(0F, 0F, 770F);
        //    }
        //}

		private void GetImageTopDown(string PathImage)
		{
			Image image = new Image(Fotos.PathFoto_Uri(PathImage), 0F, usedHeight, 1F);
						
			if (image.Width > 515F)// Se maior que a largura da página, redimenciona a altura proporcionalmente
			{
				image.Height = ((51500F/image.Width)*image.Height)/100F;
				image.Width = 515F;
			}

			if ((770F - usedHeight) > image.Height)
				page.Elements.Add(image);
			else
			{
				document.Pages.Add(page);
				usedHeight = 0F;
				image.Y = 0F;

				page = new Page(PageSize.A4, PageOrientation.Portrait, 40F);
				page.Elements.Add(image);
			}
						
			usedHeight += image.Height + 8F;
		}

		private void GetCapa()
		{
			page = new Page(PageSize.A4, PageOrientation.Portrait, 40F);

            //Label titulo = new Label("Manual\n\nde\n\nBioSegurança", 0F, 100F, 515F, 400F, Font.HelveticaBold, 56F, TextAlign.Center, Color.DarkGreen);
            //Label tipoManual = new Label("Individual", 0F, 550F, 515F, 200F, Font.TimesBold, 44F, TextAlign.Center, Color.DarkRed);
            //tipoManual.Underline = true;

            //page.Elements.Add(titulo);
            //page.Elements.Add(tipoManual);
			page.ApplyDocumentTemplate = false;

			document.Pages.Add(page);
		}
	}
}
