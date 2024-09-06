using System;
using Ilitera.Data;
using System.Data;

namespace Ilitera.Common
{
	[Database("opsa", "JuridicaPessoa", "IdJuridicaPessoa")]
	public class JuridicaPessoa : Ilitera.Data.Table
	{
		private int	_IdJuridicaPessoa;
		private Juridica _IdJuridica;
		private Pessoa	 _IdPessoa;
		private int _IndPessoaPapel;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public JuridicaPessoa()
		{

		}

		public override int Id
		{														  
			get{return _IdJuridicaPessoa;}
			set	{_IdJuridicaPessoa = value;}
		}
		public Juridica IdJuridica
		{														  
			get{return _IdJuridica;}
			set	{_IdJuridica = value;}
		}
		public Pessoa IdPessoa
		{														  
			get{return _IdPessoa;}
			set	{_IdPessoa = value;}
		}
		public int IndPessoaPapel
		{														  
			get{return _IndPessoaPapel;}
			set	{_IndPessoaPapel = value;}
		}
		
		[Persist(false)]
		public string NomeCodigo
		{
			get
			{
                if (this.IdPessoa.NomeCodigo == string.Empty)
                    this.IdPessoa.Find();

				return this.IdPessoa.NomeCodigo;
			}
		}

        [Persist(false)]
        public string NomeAbreviado
        {
            get
            {
                string ret = string.Empty;

                if (this.IdPessoa != null && this.IdPessoa.NomeAbreviado == string.Empty)
                {
                    this.IdPessoa.Find();
                    ret = this.IdPessoa.NomeAbreviado;
                }
                else if (this.IdPessoa != null && this.IdPessoa.NomeAbreviado != string.Empty)
                {
                    ret = this.IdPessoa.NomeAbreviado;
                }
                return ret;
            }
        }

		[Persist(false)]
		public string NomeCompleto
		{
            get
            {
                if (this.mirrorOld == null)
                    this.Find();

                if (this.IdPessoa.NomeCompleto == string.Empty)
                    this.IdPessoa.Find();

                return this.IdPessoa.NomeCompleto;
            }
		}

		[Persist(false)]
		public string Email
		{
			get
			{
                if (this.IdPessoa.mirrorOld == null)
                    this.IdPessoa.Find();
				
				return this.IdPessoa.Email;
			}
		}

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return NomeAbreviado;
        }

		public override int Save()
		{
            this.IdPessoa.IndTipoPessoa = (int)Pessoa.TipoPessoa.JuridicaPessoa;

			this.IdPessoa.Save();

			return base.Save();
		}

		public override void Delete()
		{
			this.IdPessoa.Delete();
			base.Delete();
		}
	}

    [Database("opsa", "qryJuridicaPessoa", "IdJuridicaPessoa")]
	public class qryJuridicaPessoa: Ilitera.Data.Table
	{
		private int _IdJuridicaPessoa;
		private Juridica _IdJuridica;
		private Pessoa _IdPessoa;
		private string _NomeEmpresa;
		private string _NomeAbreviado;
		private string _NomeCompleto;
		private string _Email;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public qryJuridicaPessoa()
		{

		}
		public override int Id
		{														  
			get{return _IdJuridicaPessoa;}
			set	{_IdJuridicaPessoa = value;}
		}
		public  Pessoa IdPessoa
		{														  
			get{return _IdPessoa;}
			set	{_IdPessoa = value;}
		}
		public  Juridica IdJuridica
		{														  
			get{return _IdJuridica;}
			set	{_IdJuridica = value;}
		}
		public  string NomeEmpresa
		{
			get{return _NomeEmpresa;}
			set	{_NomeEmpresa = value;}
		}
		public  string NomeAbreviado
		{
			get{return _NomeAbreviado;}
			set	{_NomeAbreviado = value;}
		}
		public  string NomeCompleto
		{
			get{return _NomeCompleto;}
			set	{_NomeCompleto = value;}
		}
		public  string Email
		{
			get{return _Email;}
			set	{_Email = value;}
		}
	}

	[Database("opsa", "qryPrestador", "IdPrestador")]
	public class qryPrestador: Ilitera.Data.Table
	{
		private int _IdPrestador;
		private Juridica _IdJuridica;
		private Pessoa _IdPessoa;
        private string _NomeEmpresa = string.Empty;
        private string _NomeAbreviado = string.Empty;
        private string _NomeCompleto = string.Empty;
        private string _Email = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public qryPrestador()
		{

		}       
        public override int Id
		{														  
			get{return _IdPrestador;}
			set	{_IdPrestador = value;}
		}
		public  Pessoa IdPessoa
		{														  
			get{return _IdPessoa;}
			set	{_IdPessoa = value;}
		}
		public  Juridica IdJuridica
		{														  
			get{return _IdJuridica;}
			set	{_IdJuridica = value;}
		}
		public  string NomeEmpresa
		{
			get{return _NomeEmpresa;}
			set	{_NomeEmpresa = value;}
		}
		public  string NomeAbreviado
		{
			get{return _NomeAbreviado;}
			set	{_NomeAbreviado = value;}
		}
		public  string NomeCompleto
		{
			get{return _NomeCompleto;}
			set	{_NomeCompleto = value;}
		}
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
	}
}
