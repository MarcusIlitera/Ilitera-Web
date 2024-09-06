using System;
using System.Collections;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	[Database("opsa","Cotacao","IdCotacao")]
	public class Cotacao: Ilitera.Data.Table
	{
		private int _IdCotacao;
		private Indice _IdIndice;
		private DateTime _DataCotacao = DateTime.Today;
		private float _ValorCotacao;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        
        public Cotacao()
		{
		}

		public override int Id
		{
			get{return _IdCotacao;}
			set{_IdCotacao = value;}
		}
		public Indice IdIndice
		{
			get{return _IdIndice;}
			set{_IdIndice = value;}
		}
		public DateTime DataCotacao
		{
			get{return _DataCotacao;}
			set{_DataCotacao = value;}
		}
		public float ValorCotacao
		{
			get{return _ValorCotacao;}
			set{_ValorCotacao = value;}
		}

        
        public override string ToString()
		{
			if(this.Id==0)
				return string.Empty;

            if (this.mirrorOld == null)
                this.Find();

            if (this.IdIndice.mirrorOld == null)
                this.IdIndice.Find();
			
			if(this.IdIndice.IndTipoValor==(int)TipoValor.ValoresSM)
                return this.IdIndice.Sigla + " " + this.DataCotacao.ToString("MM-yyyy") + " " + this.ValorCotacao.ToString("c");
			else
                return this.IdIndice.Sigla + " " + this.DataCotacao.ToString("MM-yyyy") + " " + this.ValorCotacao.ToString("#,##0.0000%;(#,##0.0000%);-");
		}

        public override void Delete()
        {
            ArrayList list = new ContratoServicoReajuste().Find("IdCotacao=" + this.Id);

            foreach (ContratoServicoReajuste contratoServReajuste in list)
                contratoServReajuste.Delete();

            base.Delete();
        }
	}
}
