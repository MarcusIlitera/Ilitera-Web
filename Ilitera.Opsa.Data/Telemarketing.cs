using System;
using System.Data;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;


namespace Ilitera.Opsa.Data
{
	
	public enum TipoTelemarketing: int
	{
		Inicial=1,
		Telemarketing1,
		MalaDireta,
		Telemarketing2,
		NewsLetter
	}
	
	[DisplayInForm(), DisplayFormCaption("Descricao")]
	[Database("opsa", "Telemarketing", "IdTelemarketing")]
	public class Telemarketing : Ilitera.Data.Table
	{
		private int	_IdTelemarketing;
		private Prospeccao _IdProspeccao;
		private TelemarketingAcao _IdTelemarketingAcao;
		private DateTime _DataTelemarketing = DateTime.Now;
		private Prestador _IdPrestador;
		private TelemarketingStatus _IdTelemarketingStatus;
		private string _Descricao = string.Empty;
		private Compromisso _IdCompromisso;
		private Tarefa _IdTarefa;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Telemarketing()
		{

		}

		[DisplayField(0, true)]
		public override int Id
		{														  
			get{return _IdTelemarketing;}
			set	{_IdTelemarketing = value;}
		}

		public Prospeccao IdProspeccao
		{														  
			get{return _IdProspeccao;}
			set	{_IdProspeccao = value;}
		}

		[DisplayField(30,false, "Telemarketing")]
		public TelemarketingAcao IdTelemarketingAcao
		{														  
			get{return _IdTelemarketingAcao;}
			set	{_IdTelemarketingAcao = value;}
		}

		[DisplayField(30,false, "Data")]
		public DateTime DataTelemarketing
		{														  
			get{return _DataTelemarketing;}
			set	{_DataTelemarketing = value;}
		}

		[DisplayCombo("GetListaOperadorTelemarketing")]
		[DisplayField(30)]
		public Prestador IdPrestador
		{														  
			get{return _IdPrestador;}
			set	{_IdPrestador = value;}
		}

		[DisplayField(30,false, "Telemarketing")]
		public TelemarketingStatus IdTelemarketingStatus
		{														  
			get{return _IdTelemarketingStatus;}
			set	{_IdTelemarketingStatus = value;}
		}

		[DisplayField(255,false, "Descrição")]
		public string Descricao
		{														  
			get{return _Descricao;}
			set	{_Descricao = value;}
		}	

		public Compromisso IdCompromisso
		{														  
			get{return _IdCompromisso;}
			set	{_IdCompromisso = value;}
		}	

		public Tarefa IdTarefa
		{														  
			get{return _IdTarefa;}
			set	{_IdTarefa = value;}
		}
	}
}
