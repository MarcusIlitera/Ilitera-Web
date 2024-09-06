using System;
using System.Data;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;


namespace Ilitera.Opsa.Data
{
	/// <summary>
	/// Summary description for Audiometro.
	/// </summary>
	[Database("opsa", "FAQJuridico", "IdFAQJuridico")]
	public class FAQJuridico: Ilitera.Data.Table
	{
		private int _IdFAQJuridico;
		private string _PerguntaJuridico = string.Empty;
		private string _RespostaJuridico = string.Empty;
		private DateTime _Data;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FAQJuridico()
		{

		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FAQJuridico(int Id)
		{
			this.Find(Id);
		}		
		public override int Id
		{
			get{return _IdFAQJuridico;}
			set{_IdFAQJuridico = value;}
		}
		[Obrigatorio(true, "A Pergunta deve ser preenchida!")]
		public string PerguntaJuridico
		{
			get{return _PerguntaJuridico;}
			set{_PerguntaJuridico = value;}
		}
		[Obrigatorio(true, "A Resposta deve ser preenchida!")]
		public string RespostaJuridico
		{
			get{return _RespostaJuridico;}
			set{_RespostaJuridico = value;}
		}
		public DateTime Data
		{
			get{return _Data;}
			set{_Data = value;}
		}
		public DataSet GetFAQ(string palavrachave)
		{
			return this.Get("UPPER(PerguntaJuridico) COLLATE SQL_Latin1_General_Cp1251_CS_AS LIKE '%" + palavrachave.ToUpper() + "%' ORDER BY Data");
		}
	}
}
