using System;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
    public enum TipoValor : int
    {
        ValoresSM,
        Percentuais
    }

    public enum Indices : int
    {
        Real,
        IGPM,
        SM
    }

	[Database("opsa","Indice","IdIndice")]

    

    public class Indice : Ilitera.Data.Table
	{
		private int _IdIndice;
		private string _Sigla = string.Empty;
		private string _Descricao = string.Empty;
		private int _IndTipoValor;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public Indice()
        {

        }

		public override int Id
		{
			get{return _IdIndice;}
			set{_IdIndice = value;}
		}
		public string Sigla
		{
			get{return _Sigla;}
			set{_Sigla = value;}
		}
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
		public int IndTipoValor
		{
			get{return _IndTipoValor;}
			set{_IndTipoValor = value;}
		}
		public override string ToString()
		{
			return this.Sigla;
		}
	}
}