using System;
using Ilitera.Data;

namespace Ilitera.Common
{
	[Database("opsa","SESMT","Codigo")]
	public class Sesmt : Ilitera.Data.Table
	{
		private int _Codigo;
		private int _MinEmpreg;
		private int _MaxEmpreg;
		private int _GrauRisco;
		private int _Tecnico;
		private int _Engenheiro;
		private int _AuxiliarEnfermagem;
		private int _Enfermeiro;
		private int _Medico;
		private bool _TecnicoTempoParcial;
		private bool _EngenheiroTempoParcial;
		private bool _AuxiliarEnfermagemTempoParcial;
		private bool _EnfermeiroTempoParcial;
		private bool _MedicoTempoParcial;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Sesmt()
		{

		}
		public override int Id
		{
			get{return _Codigo;}
			set{_Codigo = value;}
		}
		public int MinEmpreg
		{
			get{return _MinEmpreg;}
			set{_MinEmpreg = value;}
		}
		public int MaxEmpreg
		{
			get{return _MaxEmpreg;}
			set{_MaxEmpreg = value;}
		}
		public int GrauRisco
		{
			get{return _GrauRisco;}
			set{_GrauRisco = value;}
		}
		public int Tecnico
		{
			get{return _Tecnico;}
			set{_Tecnico = value;}
		}
		public int Engenheiro
		{
			get{return _Engenheiro;}
			set{_Engenheiro = value;}
		}
		public int AuxiliarEnfermagem
		{
			get{return _AuxiliarEnfermagem;}
			set{_AuxiliarEnfermagem = value;}
		}
		public int Enfermeiro
		{
			get{return _Enfermeiro;}
			set{_Enfermeiro = value;}
		}
		public int Medico
		{
			get{return _Medico;}
			set{_Medico = value;}
		}
		public bool TecnicoTempoParcial
		{
			get{return _TecnicoTempoParcial;}
			set{_TecnicoTempoParcial = value;}
		}
		public bool EngenheiroTempoParcial
		{
			get{return _EngenheiroTempoParcial;}
			set{_EngenheiroTempoParcial = value;}
		}
		public bool AuxiliarEnfermagemTempoParcial
		{
			get{return _AuxiliarEnfermagemTempoParcial;}
			set{_AuxiliarEnfermagemTempoParcial = value;}
		}
		public bool EnfermeiroTempoParcial
		{
			get{return _EnfermeiroTempoParcial;}
			set{_EnfermeiroTempoParcial = value;}
		}
		public bool MedicoTempoParcial
		{
			get{return _MedicoTempoParcial;}
			set{_MedicoTempoParcial = value;}
		}
	}
}
