using System;
using Ilitera.Data;
using System.Data;

namespace Ilitera.Opsa.Data
{
	[Database("opsa","PcmsoDocumento","IdPcmsoDocumento")]
	public class PcmsoDocumento: Ilitera.Data.Table 
	{
		private int _IdPcmsoDocumento;
		private Pcmso _IdPcmso;		
		private string _Titulo=string.Empty;
		private string _Texto=string.Empty;
		private int _NumOrdem;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PcmsoDocumento()
		{

		}
		public override int Id
		{
			get{return _IdPcmsoDocumento;}
			set{_IdPcmsoDocumento = value;}
		}
		public Pcmso IdPcmso
		{
			get{return _IdPcmso;}
			set{_IdPcmso = value;}
		}		
		public string Titulo
		{
			get{return _Titulo;}
			set{_Titulo = value;}
		}
		public string Texto
		{
			get{return _Texto;}
			set{_Texto = value;}
		}
		public int NumOrdem
		{
			get{return _NumOrdem;}
			set{_NumOrdem = value;}
		}
		public override string ToString()
		{			
			if(Titulo!=string.Empty)
				return NumOrdem + " - " + Titulo;
			else
				return NumOrdem + " - " + Texto.Substring(0, 10);
		}

		public bool VerificaExisteDocumento(int IdPcmso)
		{
			DataSet ds = this.Get("IdPcmso=" + IdPcmso);

			if (ds.Tables[0].Rows.Count == 0)
				return false;
			else
				return true;
		}
	}
}
