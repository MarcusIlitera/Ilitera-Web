using System;
using Ilitera.Common;
using Ilitera.Data;


namespace Ilitera.Opsa.Data
{	
	[Database("opsa", "PrestadorCliente", "IdPrestadorCliente")]
	public class PrestadorCliente : Ilitera.Data.Table
	{
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PrestadorCliente()
		{			
		}
		private int	_IdPrestadorCliente;
		private Prestador _IdPrestador;
		private Cliente _IdCliente;	

		public override int Id
		{														  
			get{return _IdPrestadorCliente;}
			set	{_IdPrestadorCliente = value;}
		}
		public Prestador IdPrestador
		{														  
			get{return _IdPrestador;}
			set	{_IdPrestador = value;}
		}
		public Cliente IdCliente
		{														  
			get{return _IdCliente;}
			set	{_IdCliente = value;}
		}
        public override string ToString()
        {
            if (IdCliente.mirrorOld == null)
                IdCliente.Find();

            return IdCliente.NomeAbreviado;
        }

        public override int Save()
        {
            if (this.IdCliente.mirrorOld == null)
                this.IdCliente.Find();

            if (this.IdCliente.IdJuridicaPai.Id != 0)
            {
                PrestadorCliente prestCliente = new PrestadorCliente();
                prestCliente.Find("IdPrestador=" + this.IdPrestador.Id
                                 + " AND IdCliente=" + this.IdCliente.IdJuridicaPai.Id);

                if (prestCliente.Id == 0)
                    throw new Exception("O Contato só pode visualizar esse local trabalho se a empresa "
                                        + this.IdCliente.IdJuridicaPai.ToString()
                                        + " também estiver disponível para esse Contato!");

            }
            return base.Save();
        }
	}
}
