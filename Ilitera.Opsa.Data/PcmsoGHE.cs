using System;
using System.Data;
using System.Collections;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{

	[Database("opsa","PcmsoGhe","IdPcmsoGhe")]
	public class PcmsoGhe: Ilitera.Data.Table 
	{
		private int _IdPcmsoGhe;
		private Pcmso _IdPcmso;
		private Ghe _IdGhe;
		private bool _IsHouveAtualizacaoManual;
		private string _RiscosOcupacionais=string.Empty;
        private Int16 _Desconsiderar_Dias_ASO;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PcmsoGhe()
		{
		}
		public override int Id
		{
			get{return _IdPcmsoGhe;}
			set{_IdPcmsoGhe = value;}
		}
		public Pcmso IdPcmso
		{
			get{return _IdPcmso;}
			set{_IdPcmso = value;}
		}
		public Ghe IdGhe
		{
			get{return _IdGhe;}
			set{_IdGhe = value;}
		}
		public bool IsHouveAtualizacaoManual
		{
			get{return _IsHouveAtualizacaoManual;}
			set{_IsHouveAtualizacaoManual=value;}
		}
		public string RiscosOcupacionais
		{
			get{return _RiscosOcupacionais;}
			set{_RiscosOcupacionais = value;}
		}

        public Int16 Desconsiderar_Dias_ASO
        {
            get { return _Desconsiderar_Dias_ASO; }
            set { _Desconsiderar_Dias_ASO = value; }
        }

    }
}
