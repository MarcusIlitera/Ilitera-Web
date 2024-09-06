using System;
using Ilitera.Data;

namespace Ilitera.Common
{

	[Database("opsa","RepetirDatas","IdRepetirDatas")]
	public class RepetirDatas: Ilitera.Data.Table
	{
		private int _IdRepetirDatas;
		private Repetir _IdRepetir;
		private Pessoa _IdPessoa;
		private DateTime _Inicio;
		private DateTime _Termino;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public RepetirDatas()
		{

		}
		public override int Id
		{
			get{return _IdRepetirDatas;}
			set{_IdRepetirDatas = value;}
		}
		public Repetir IdRepetir
		{
			get{return _IdRepetir;}
			set{_IdRepetir = value;}
		}
		public Pessoa IdPessoa
		{
			get{return _IdPessoa;}
			set{_IdPessoa = value;}
		}
		public DateTime Inicio
		{
			get{return _Inicio;}
			set{_Inicio = value;}
		}
		public DateTime Termino
		{
			get{return _Termino;}
			set{_Termino = value;}
		}
		public override string ToString()
		{
			string strData = this.IdPessoa.ToString() + " - " +  this.Inicio.ToString("dd-MM-yyyy")
				+" "+ this.Inicio.ToString("t")
				+" às "+ this.Termino.ToString("t");

			return strData;
		}
	}
}
