using System;
using Ilitera.Data;

namespace Ilitera.Common
{
	[Database("opsa","DimensionamentoCipa","IdDimensionamentoCipa")]
	public class DimensionamentoCipa : Ilitera.Data.Table 
	{

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public DimensionamentoCipa()
		{

		}
		private	int _IdDimensionamentoCipa;
		private	int _MinEmpregado;
		private	int _MaxEmpregado;
		private	int _Efetivo;	
		private	int _Suplente;
		private	GrupoCipa _IdGrupoCipa;	
		
		public override int Id
		{
			get{return _IdDimensionamentoCipa;}
			set{_IdDimensionamentoCipa = value;}
		}
		public int MinEmpregado
		{
			get{return _MinEmpregado;}
			set{_MinEmpregado = value;}
		}
		public int MaxEmpregado
		{
			get{return _MaxEmpregado;}
			set{_MaxEmpregado = value;}
		}
		public int Efetivo
		{
			get{return _Efetivo;}
			set{_Efetivo = value;}
		}
		public int Suplente
		{
			get{return _Suplente;}
			set{_Suplente = value;}
		}
		public GrupoCipa IdGrupoCipa
		{
			get{return _IdGrupoCipa;}
			set{_IdGrupoCipa = value;}
		}
	}
}
