using System;
using Ilitera.Data;
using System.Data;
using System.Drawing;
using System.Text;
using Ilitera.Common;
using System.Collections;

namespace Ilitera.Opsa.Data
{

	public enum IndTipoContato : int
	{
		Geral,
		Financeiro,
		RH,
		Contratos,
		Diretoria
	}

	[Database("opsa", "Contato", "IdContato")]
	public class Contato : Ilitera.Common.JuridicaPessoa
	{
		private int	_IdContato;		
		private int _IndTipoContato;
		private string _Titulo = string.Empty;
		private string _Cargo= string.Empty;
		private string _Departamento = string.Empty;
		private Cliente cliente;

		public Contato()
		{		
			this.Inicialize();

			this.IndPessoaPapel = (int)PessoaPapeis.Contato;
		}	
		public Contato(int Id)
		{
			this.Find(Id);
		}

		public Contato(string NomePessoa)
		{
			this.IdPessoa = new Pessoa();
			this.IdPessoa.NomeAbreviado = NomePessoa;
			this.IndPessoaPapel = (int)PessoaPapeis.Contato;
		}
		public override int Id
		{														  
			get{return _IdContato;}
			set	{_IdContato = value;}
		}				
		public int IndTipoContato
		{														  
			get{return _IndTipoContato;}
			set	{_IndTipoContato = value;}
		}
		public string Titulo
		{														  
			get{return _Titulo;}
			set	{_Titulo = value;}
		}
		public string Cargo
		{														  
			get{return _Cargo;}
			set	{_Cargo = value;}
		}
		public string Departamento
		{														  
			get{return _Departamento;}
			set	{_Departamento = value;}
		}		

		public void SetCliente(Cliente cliente)
		{
			this.cliente = cliente;
		}

		public Cliente GetCliente()
		{
			if(cliente==null)
			{
				cliente = new Cliente();
				cliente.Find(this.IdJuridica.Id);
			}
			return cliente;
		}

//		[Persist(false)]
//		public bool IsContatoBoleto
//		{
//			get
//			{
//				return (this.Id!=0 && GetCliente().IdContatoBoleto.Id==this.Id);
//			}
//			set	
//			{
//				if(value)
//					GetCliente().IdContatoBoleto = this;
//				else
//					GetCliente().IdContatoBoleto = new Contato();
//			}
//		}

//		[Persist(false)]
//		public bool IsContatoCIPA
//		{
//			get
//			{
//				return (this.Id!=0 && GetCliente().IdContatoCIPA.Id==this.Id);
//			}
//			set	
//			{
//				if(value)
//					GetCliente().IdContatoCIPA = this;
//				else
//					GetCliente().IdContatoCIPA = new Contato();
//			}
//		}

		public void FindPessoa(Pessoa pessoa)
		{
			JuridicaPessoa juridicaPessoa = new JuridicaPessoa();
			juridicaPessoa.Find("IdPessoa =" + pessoa.Id); 
			this.Find("IdJuridicaPessoa=" + juridicaPessoa.Id);
		}		
	}

	[Database("opsa", "qryContato", "IdContato")]
	public class qryContato: Ilitera.Data.Table
	{
		private int _IdContato;
		private Juridica _IdJuridica;
		private string _NomeAbreviado=string.Empty;
		private string _NomeCompleto=string.Empty;
		private string _Email=string.Empty;
		private string _Telefone=string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public qryContato()
		{
		}
		public override int Id
		{														  
			get{return _IdContato;}
			set	{_IdContato = value;}
		}
		public  Juridica IdJuridica
		{														  
			get{return _IdJuridica;}
			set	{_IdJuridica = value;}
		}
		public  string NomeAbreviado
		{														  
			get{return _NomeAbreviado;}
			set	{_NomeAbreviado = value;}
		}
		public string NomeCompleto
		{														  
			get{return _NomeCompleto;}
			set	{_NomeCompleto = value;}
		}
		public  string Telefone
		{														  
			get{return _Telefone;}
			set	{_Telefone = value;}
		}
		public  string Email
		{														  
			get{return _Email;}
			set	{_Email = value;}
		}
	}
}
