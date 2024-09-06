using System;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{

	public enum CaracterizacaoTipo: int
	{
		Documento,
		EleicaoCIPA,
		Treinamento,
		Certificados,
		Contato,
		QuantRelacionamento,
		Fiscalizacao,
		SESMT,
		ReuniaoCIPA
	}

	[Database("opsa", "Caracterizacao", "IdCaracterizacao")]
	public class Caracterizacao : Ilitera.Data.Table
	{
		private int	_IdCaracterizacao;
		private int _IndCaracterizacao;
		private string _Descricao=string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Caracterizacao()
		{

		}

		public override int Id
		{														  
			get{return _IdCaracterizacao;}
			set	{_IdCaracterizacao = value;}
		}

		public int IndCaracterizacao
		{														  
			get{return _IndCaracterizacao;}
			set	{_IndCaracterizacao = value;}
		}
		[Obrigatorio(true, "Descricao é campo obrigatório!")]
		public string Descricao
		{														  
			get{return _Descricao;}
			set	{_Descricao = value;}
		}
	}
}
