using System;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	public enum Controles : int
	{
		AguardandoAgendamento = 626794824,
		CipaEnvEmailAta = -1353584348,
		CipaAguarDadosAta = 1762999118,	
		EnviandoEmailOuImprDoc = 1096629624,
		EnviarEmailConferenciaCliente=478009144,
		EnviandoEMAILouImpressaoDocumento = 1096629624,
		EmitindoCertificado=1782184998,
		PcmsoNocoesGeraisAcao = -394241566,
        PcmsoConfiguracaoExames = 1449560538,
		PcmsoPlanejamentoExames=-1954682202,
		SaidaDocumento=-1330078966,
		ElaborandoEnviandoCliente=-1917993482,
        ImpressaoOuPublicacaoInternet = -1146714546,
	}

	[Database("opsa","Controle","IdControle")]
	public class Controle : Ilitera.Data.Table  
	{

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Controle()
		{

		}
		private int _IdControle;
		private string _Nome = string.Empty;

		public override int Id
		{
			get{return _IdControle;}
			set{_IdControle = value;}
		}
		[Obrigatorio(true, "Nome é campo obrigatório!")]
		public string Nome
		{
			get{return _Nome;}
			set{_Nome = value;}
		}
		public override string ToString()
		{
            if (this.mirrorOld == null)
                this.Find();

			return _Nome;
		}
	}
}
