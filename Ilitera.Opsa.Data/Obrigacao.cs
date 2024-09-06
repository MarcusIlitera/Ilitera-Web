using System;
using System.Data;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	public enum ObrigacaoTipo: int
	{
		Geral,
		Cipa,
		Documento,
		Treinamento,
		Tarefa
	}

	public enum Incidencia: int
	{
		Todas,
		PossuirCaldeira,
		PossuirVasosPressao,
		PossuirEmpilhadeira,
		PossuirPonteRolante,
		PossuirCipa,
		NaoPossuirCipa,
		PossuirSESMT,
		SolicitarElaboracao,
		PossuirObraCivil,
		HouverPericulosidade,
		UsarEPI,
		AvaliacaoQuantitativa,
		Mineradora,
		Prensas,
		Galvanoplastia,
		ContrataPCMSO,
		PossuirAutoclave,
        ContrataOrdemServico
	}

	public enum Obrigacoes: int
	{
        Nenhuma,
		PPRA=1,
        PPRAaDistancia = -64739478,
		Auditoria=2,
        MapaRisco = 4,
		Quadros_III_IV_V=31,
		PastaCIPA=2138557634,
		PCMSO=32, 
        Pericia = 36,
        TreinamentoSistemaMestra = 272310556,
		Posse=1626938786,
		ExamesVencidos = 1907157096, 
		ExamesPeriodicos = 1341213194,
		EleicaoCipa=119884952,
		CIPA=65,
        CaldeiraInspecao = 6,
        CaldeiraProjeto = 18,
        VasosInspecao = 5,
        VasosProjeto = 21,
        SIPAT = 9,
        AE = 80,
        OrdemServico = 48,
        ASOEnvioAoCliente = -2076564978,
        PcmsoRetiradaProntuario = 375232383,
	}

	[Database("opsa","Obrigacao","IdObrigacao")]
	public class Obrigacao : Ilitera.Data.Table 
	{
		private int _IdObrigacao;
		private string _Nome = string.Empty;
		private string _NomeReduzido = string.Empty;
		private bool _IsInativo;
		private int _Aviso;
		private Prestador _IdPrestador;
        private double _ValorEmReais;
		private short _IndObrigacaoTipo;
		private EventoBaseCipa _IdEventoBaseCipa;
		private DocumentoBase _IdDocumentoBase;
		private bool _Agendar;
		private short _IndIncidencia;
		private bool _SugestaoParaPendente;
		private bool _DatadorFinalizadoParaConcluido;
        private bool _PrestadorPodeCriarPedido;
        private bool _PedidoPorEmpregado;


		public static bool Atualiza = true;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Obrigacao()
		{

		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Obrigacao(int Id)
		{
			this.Id = Id;
			this.Find();
		}
		public override int Id
		{
			get{return _IdObrigacao;}
			set{_IdObrigacao = value;}
		}
		[Obrigatorio(true, "O nome da obrigação é campo obrigatório!")]
		public string Nome
		{
			get{return _Nome;}
			set{_Nome = value;}
		}
		[Obrigatorio(true, "Apelido da obrigação é campo obrigatório!")]
		public string NomeReduzido
		{
			get{return _NomeReduzido;}
			set{_NomeReduzido = value;}
		}
		public int Aviso
		{
			get{return _Aviso;}
			set{_Aviso = value;}
		}
		public Prestador IdPrestador
		{
			get{return _IdPrestador;}
			set{_IdPrestador = value;}
		}
        public double ValorEmReais
        {
            get { return _ValorEmReais; }
            set { _ValorEmReais = value; }
        }
        public short IndObrigacaoTipo
        {
            get { return _IndObrigacaoTipo; }
            set
            {
                _IndObrigacaoTipo = value;

                if (value != (short)ObrigacaoTipo.Cipa && _IdEventoBaseCipa != null)
                    _IdEventoBaseCipa.Id = 0;

                if (value != (short)ObrigacaoTipo.Documento && _IdDocumentoBase != null)
                    _IdDocumentoBase.Id = 0;
            }
        }
		public EventoBaseCipa IdEventoBaseCipa
		{
			get{return _IdEventoBaseCipa;}
			set{_IdEventoBaseCipa = value;}
		}
		public DocumentoBase IdDocumentoBase
		{
			get{return _IdDocumentoBase;}
			set{_IdDocumentoBase = value;}
		}
		public bool Agendar
		{
			get{return _Agendar;}
			set{_Agendar = value;}
		}
		public short IndIncidencia
		{
			get{return _IndIncidencia;}
			set{_IndIncidencia = value;}
		}
		public bool SugestaoParaPendente
		{
			get{return _SugestaoParaPendente;}
			set{_SugestaoParaPendente = value;}
		}
		public bool DatadorFinalizadoParaConcluido
		{
			get{return _DatadorFinalizadoParaConcluido;}
			set{_DatadorFinalizadoParaConcluido = value;}
		}
        public bool PrestadorPodeCriarPedido
        {
            get { return _PrestadorPodeCriarPedido; }
            set { _PrestadorPodeCriarPedido = value; }
        }
        public bool PedidoPorEmpregado
        {
            get { return _PedidoPorEmpregado; }
            set { _PedidoPorEmpregado = value; }
        }
		public bool IsInativo
		{
			get{return _IsInativo;}
			set{_IsInativo = value;}
		}
		public override string ToString()
		{
            if (this.mirrorOld == null)
                this.Find();

            return _NomeReduzido;
		}

		public override int Save()
		{
            if (this.IndObrigacaoTipo == (int)ObrigacaoTipo.Cipa  && this.IdEventoBaseCipa.Id == 0)
                throw new Exception("É obrigatório o evento base da CIPA!");
            else if (this.IndObrigacaoTipo == (int)ObrigacaoTipo.Documento && this.IdDocumentoBase.Id == 0)
                throw new Exception("É obrigatório o documento base!");

            if (this.IndObrigacaoTipo == (int)ObrigacaoTipo.Treinamento)
                this.IdDocumentoBase.Id = (int)Documentos.Treinamentos;

            return base.Save();
		}

        public override void Delete()
        {
            if (new RegraLei().Find("IdObrigacao=" + this.Id).Count == 0)
                if (new RegraSindicato().Find("IdObrigacao=" + this.Id).Count == 0)
                    if (new RegraCliente().Find("IdObrigacao=" + this.Id).Count == 0)
                        base.Delete();
                    else
                        throw new Exception("Exclusão cancelada!");
        }

        public bool IsEquipamento()
        {
            return IsEquipamento(this);
        }

        public bool IsPPRA()
        {
            return this.Id == (int)Obrigacoes.PPRA
                || this.Id == (int)Obrigacoes.PPRAaDistancia;
        }

        public static bool IsEquipamento(Obrigacao obrigacao)
        {
            return obrigacao.Id == (int)Obrigacoes.VasosInspecao
                        || obrigacao.Id == (int)Obrigacoes.VasosProjeto
                        || obrigacao.Id == (int)Obrigacoes.CaldeiraInspecao
                        || obrigacao.Id == (int)Obrigacoes.CaldeiraProjeto;
        }

        public static bool IsRegraEmBranco(IRegra regra )
        {
           return (regra.Dia == 0
                && regra.DiasExecutar == 0
                && regra.DiasExecutarPrimeira == 0
                && regra.DataExecutar == new DateTime()
                && regra.IdObrigacaoBase.Id == 0
                && regra.IdObrigacaoBasePrimeira.Id == 0
                && regra.IndFeriado == 0
                && regra.IndFeriadoPeriodico == 0
                && regra.IndFeriadoPrimeira == 0
                && regra.IndPeriodicidade == 0
                && regra.IndPeriodoExecutar == 0
                && regra.IndPeriodoExecutarPrimeira == 0
                && regra.IndTipoPeriodicidade == (int)IndTipoPeriodicidades.NaoRealizar
                && regra.Intervalo == 0
                && regra.Mes == 0
                && regra.MinEmpregado == 0) ;
        }

		public string GetIncidencia()
		{
			string ret = string.Empty;

			switch (this.IndIncidencia)
			{
				case (int)Incidencia.Todas:
					ret = "Todas as Empresas";
					break;
				case (int)Incidencia.SolicitarElaboracao:
					ret = "Solicitar Elaboração";
					break;
				case (int)Incidencia.AvaliacaoQuantitativa:
					ret = "Avaliação Quantitativa";
					break;
				case (int)Incidencia.ContrataPCMSO:
					ret = "PCMSO contratada";
					break;
				case (int)Incidencia.Galvanoplastia:
					ret = "Galvanoplastia";
					break;
				case (int)Incidencia.HouverPericulosidade:
					ret = "Houver Periculosidade";
					break;
				case (int)Incidencia.Mineradora:
					ret = "Mineradora";
					break;
				case (int)Incidencia.NaoPossuirCipa:
					ret = "Não Possuir CIPA";
					break;
				case (int)Incidencia.PossuirCaldeira:
					ret = "Possuir Caldeira";
					break;
				case (int)Incidencia.PossuirCipa:
					ret = "Possuir CIPA";
					break;
				case (int)Incidencia.PossuirEmpilhadeira:
					ret = "Possuir Empilhadeira";
					break;
				case (int)Incidencia.PossuirObraCivil:
					ret = "Possuir ObraCivil";
					break;
				case (int)Incidencia.PossuirPonteRolante:
					ret = "Possuir Ponte Rolante";
					break;
				case (int)Incidencia.PossuirSESMT:
					ret = "Possuir SESMT";
					break;
				case (int)Incidencia.PossuirVasosPressao:
					ret = "Possuir Vasos Pressão";
					break;
				case (int)Incidencia.Prensas:
					ret = "Possuir Prensas";
					break;
				case (int)Incidencia.UsarEPI:
					ret = "Usar EPI";
					break;
			}
			return ret;
		}

		public static Obrigacao GetObrigacaoCipa(EventoBase eventoBase)
		{
			return GetObrigacaoCipa((int)eventoBase);
		}

		public static Obrigacao GetObrigacaoCipa(int eventoBase)
		{
			Obrigacao obrigacao = new Obrigacao();
			obrigacao.Find("IdEventoBaseCipa="+eventoBase.ToString());
			return obrigacao;
		}
	}
}