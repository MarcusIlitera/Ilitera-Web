using System;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	public enum IndTitulo : int
	{
		Titulo = 1,
		SubTitulo,
		SubTitulo2
	}

	[Database("opsa","PcmsoFrasePadrao","IdPcmsoFrasePadrao")]
	public class PcmsoFrasePadrao: Ilitera.Data.Table 
	{
		private int _IdPcmsoFrasePadrao;
		private string _Nome=string.Empty; 
		private string _Texto=string.Empty; 
		private string _Titulo = string.Empty;
		private bool _Automatico;
		private int _IndTitulo;
		private int _NumOrdem;
		private PcmsoFrasePadraoDicionario _IdPcmsoFrasePadraoDicionario;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PcmsoFrasePadrao()
		{
		}
		public override int Id
		{
			get{return _IdPcmsoFrasePadrao;}
			set{_IdPcmsoFrasePadrao = value;}
		}
		[Obrigatorio(true, "Nome da Frase é campo obrigatório!")]
		public string Nome
		{
			get{return _Nome;}
			set{_Nome = value;}
		}
		public string Texto
		{
			get{return _Texto;}
			set{_Texto = value;}
		}
		public string Titulo
		{
			get{return _Titulo;}
			set{_Titulo = value;}
		}
		public bool Automatico
		{
			get{return _Automatico;}
			set{_Automatico = value;}
		}
		public int IndTitulo
		{
			get{return _IndTitulo;}
			set{_IndTitulo = value;}
		}
		public int NumOrdem
		{
			get{return _NumOrdem;}
			set{_NumOrdem = value;}
		}
		public PcmsoFrasePadraoDicionario IdPcmsoFrasePadraoDicionario
		{
			get{return _IdPcmsoFrasePadraoDicionario;}
			set{_IdPcmsoFrasePadraoDicionario = value;}
		}
		public override string ToString()
		{
			return this.NumOrdem + " - " + _Nome;
		}	
	}
}
