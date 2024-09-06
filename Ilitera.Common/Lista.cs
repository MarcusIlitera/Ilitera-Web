using System;

namespace Ilitera.Common
{
	/// <summary>
	/// Summary description for Lista.
	/// </summary>
	public class Lista 
	{
		private int _Id;		
		private string _descricao = string.Empty;
		public Lista()
		{
		}
		public int Id
		{														  
			get	{return _Id;}
			set {_Id = value;}
		}
		public string descricao
		{														  
			get	{return _descricao;}
			set {_descricao = value;}
		}
	}
}
