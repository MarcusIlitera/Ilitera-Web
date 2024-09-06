using System;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{

	[Database("opsa", "CaracterizacaoCliente", "IdCaracterizacaoCliente")]
	public class CaracterizacaoCliente: Ilitera.Data.Table
	{
		private int _IdCaracterizacaoCliente;
		private Cliente _IdCliente;
		private Caracterizacao _IdCaracterizacaoDocumento;
		private Caracterizacao _IdCaracterizacaoCIPA;
		private Caracterizacao _IdCaracterizacaoReuniao;
		private Caracterizacao _IdCaracterizacaoTreinamento;
		private Caracterizacao _IdCaracterizacaoCertificado;
		private Caracterizacao _IdCaracterizacaoContato;
		private Caracterizacao _IdCaracterizacaoQuantRelacionamento;
		private Caracterizacao _IdCaracterizacaoFiscalizacao;
		private Caracterizacao _IdCaracterizacaoSESMT;
		private string _CaracterizacaoObs = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CaracterizacaoCliente()
		{

		}

		public override int Id
		{														  
			get{return _IdCaracterizacaoCliente;}
			set	{_IdCaracterizacaoCliente = value;}
		}

		public Cliente IdCliente
		{														  
			get{return _IdCliente;}
			set	{_IdCliente = value;}
		}

		public Caracterizacao IdCaracterizacaoDocumento
		{														  
			get{return _IdCaracterizacaoDocumento;}
			set	{_IdCaracterizacaoDocumento = value;}
		}
		public Caracterizacao IdCaracterizacaoCIPA
		{														  
			get{return _IdCaracterizacaoCIPA;}
			set	{_IdCaracterizacaoCIPA = value;}
		}
		public Caracterizacao IdCaracterizacaoTreinamento
		{														  
			get{return _IdCaracterizacaoTreinamento;}
			set	{_IdCaracterizacaoTreinamento = value;}
		}
		public Caracterizacao IdCaracterizacaoCertificado
		{														  
			get{return _IdCaracterizacaoCertificado;}
			set	{_IdCaracterizacaoCertificado = value;}
		}
		public Caracterizacao IdCaracterizacaoContato
		{														  
			get{return _IdCaracterizacaoContato;}
			set	{_IdCaracterizacaoContato = value;}
		}
		public Caracterizacao IdCaracterizacaoQuantRelacionamento
		{														  
			get{return _IdCaracterizacaoQuantRelacionamento;}
			set	{_IdCaracterizacaoQuantRelacionamento = value;}
		}
		public Caracterizacao IdCaracterizacaoFiscalizacao
		{														  
			get{return _IdCaracterizacaoFiscalizacao;}
			set	{_IdCaracterizacaoFiscalizacao = value;}
		}
		public Caracterizacao IdCaracterizacaoSESMT
		{														  
			get{return _IdCaracterizacaoSESMT;}
			set	{_IdCaracterizacaoSESMT = value;}
		}
		public Caracterizacao IdCaracterizacaoReuniao
		{														  
			get{return _IdCaracterizacaoReuniao;}
			set	{_IdCaracterizacaoReuniao = value;}
		}
		public string CaracterizacaoObs
		{														  
			get{return _CaracterizacaoObs;}
			set	{_CaracterizacaoObs = value;}
		}
	}
}
