using System;
using Ilitera.Data;
using System.Collections;

namespace Ilitera.Opsa.Data
{
	[Database("opsa","DRT","IdDRT")]
	public class DRT : Ilitera.Common.Juridica
	{
		private int _IdDRT;
		private static ArrayList listDRT;
		
		public DRT()
		{

		}

		public override int Id
		{
			get{return _IdDRT;}
			set{_IdDRT = value;}
		}

		public override string ToString()
		{
			if(this.IdCNAE==null)
				this.Find();

			return this.NomeAbreviado;
		}

		public static ArrayList GetFindAll()
		{
			if(listDRT == null)
				RefleshFindAll();

			return listDRT;
		}

		public static void RefleshFindAll()
		{
			listDRT = new DRT().FindAll();
			listDRT.Sort();
		}

		public override int Save()
		{
			listDRT = null;
			return base.Save();
		}
	}
}
