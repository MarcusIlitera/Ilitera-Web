using System;
using System.Collections;
using System.Data;
using System.Text;
using Ilitera.Data;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Opsa.Data
{
	public enum IndCondicaoExtintor:int
	{
		Alocado = 1,
		ForaDeUso,
		Reserva
	}

	[Database("opsa", "Extintores", "IdExtintores", "", "Extintor de Incêndio")]
	public class Extintores: Ilitera.Data.Table
	{
		private int	_IdExtintores;
		private Cliente _IdCliente;
		private string  _AtivoFixo;
		private TipoExtintor _IdTipoExtintor;
		private Setor _IdSetor;
		private string _Localizacao;
		private DateTime _DataFabricacao = new DateTime();
		private FabricanteExtintor _IdFabricanteExtintor;
		private int _Garantia;
		private int _IndCondicao;
		private float _PesoVazio;
		private float _PesoCheio;
		private string _Observacao;
        private DateTime _VencimentoRecarga = new DateTime();


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Extintores()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Extintores(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{														  
			get{return _IdExtintores;}
			set	{_IdExtintores = value;}
		}
		public Cliente IdCliente
		{														  
			get{return _IdCliente;}
			set	{_IdCliente = value;}
		}
		[Obrigatorio(true, "O Ativo Fixo do Extintor é obrigatório!")]
		public string AtivoFixo
		{														  
			get{return _AtivoFixo;}
			set	{_AtivoFixo = value;}
		}
		public TipoExtintor IdTipoExtintor
		{														  
			get{return _IdTipoExtintor;}
			set	{_IdTipoExtintor = value;}
		}
		public Setor IdSetor
		{														  
			get{return _IdSetor;}
			set	{_IdSetor = value;}
		}
		public string Localizacao
		{														  
			get{return _Localizacao;}
			set	{_Localizacao = value;}
		}
		public DateTime DataFabricacao
		{														  
			get{return _DataFabricacao;}
			set	{_DataFabricacao = value;}
		}
		public FabricanteExtintor IdFabricanteExtintor
		{														  
			get{return _IdFabricanteExtintor;}
			set	{_IdFabricanteExtintor = value;}
		}
		public int Garantia
		{														  
			get{return _Garantia;}
			set	{_Garantia = value;}
		}
		public int IndCondicao
		{														  
			get{return _IndCondicao;}
			set	{_IndCondicao = value;}
		}
		public float PesoVazio
		{														  
			get{return _PesoVazio;}
			set	{_PesoVazio = value;}
		}
		public float PesoCheio
		{														  
			get{return _PesoCheio;}
			set	{_PesoCheio = value;}
		}
		public string Observacao
		{														  
			get{return _Observacao;}
			set	{_Observacao = value;}
		}
        public DateTime VencimentoRecarga
        {
            get { return _VencimentoRecarga; }
            set { _VencimentoRecarga = value; }
        }

    }


    [Database("opsa", "AvisoExtintores", "IdAvisoExtintores", "", "Aviso de vencimento de Extintor")]
    public class AvisoExtintor : Ilitera.Data.Table
    {
        private int _IdAvisoExtintores;
        private int _IdPessoa;
        private string _eMail;
        private Int16 _Dias;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AvisoExtintor()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AvisoExtintor(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdAvisoExtintores; }
            set { _IdAvisoExtintores = value; }
        }
        public int IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }
        public string eMail
        {
            get { return _eMail; }
            set { _eMail = value; }
        }
        public Int16 Dias
        {
            get { return _Dias; }
            set { _Dias = value; }
        }
    }


    [Database("opsa", "TipoExtintor", "IdTipoExtintor", "", "Tipo de Extintor de Incêndio")]
	public class TipoExtintor: Ilitera.Data.Table
	{
		private int	_IdTipoExtintor;
		private string _ModeloExtintor;
		private string _AgenteExtintor;
		private Cliente _IdCliente;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TipoExtintor()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TipoExtintor(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{														  
			get{return _IdTipoExtintor;}
			set	{_IdTipoExtintor = value;}
		}
		[Obrigatorio(true, "O Modelo do Extintor é obrigatório!")]
		public string ModeloExtintor
		{														  
			get{return _ModeloExtintor;}
			set	{_ModeloExtintor = value;}
		}
		public string AgenteExtintor
		{														  
			get{return _AgenteExtintor;}
			set	{_AgenteExtintor = value;}
		}
		public Cliente IdCliente
		{														  
			get{return _IdCliente;}
			set	{_IdCliente = value;}
		}
	}
	
	[Database("opsa", "HistoricoExtintor", "IdHistoricoExtintor", "", "Histórico de Extintor de Incêndio")]
	public class HistoricoExtintor : Ilitera.Data.Table
	{
		private int	_IdHistoricoExtintor;
		private int _IndTipoHistorico;
		private int _IndReparacao;
		private Extintores _IdExtintores;
		private DateTime _DataEvento = new DateTime();
		private float _PesoAtual;
		private string _Descricao;
		private Prestador _IdUsuarioResp;
		private DateTime _Vencimento;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public HistoricoExtintor()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public HistoricoExtintor(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{														  
			get{return _IdHistoricoExtintor;}
			set	{_IdHistoricoExtintor = value;}
		}
		public int IndTipoHistorico
		{														  
			get{return _IndTipoHistorico;}
			set	{_IndTipoHistorico = value;}
		}
		public int IndReparacao
		{														  
			get{return _IndReparacao;}
			set	{_IndReparacao = value;}
		}
		public Extintores IdExtintores
		{														  
			get{return _IdExtintores;}
			set	{_IdExtintores = value;}
		}
		public DateTime DataEvento
		{														  
			get{return _DataEvento;}
			set	{_DataEvento = value;}
		}
		public float PesoAtual
		{														  
			get{return _PesoAtual;}
			set	{_PesoAtual = value;}
		}
		public string Descricao
		{														  
			get{return _Descricao;}
			set	{_Descricao = value;}
		}
		public Prestador IdUsuarioResp
		{														  
			get{return _IdUsuarioResp;}
			set	{_IdUsuarioResp = value;}
		}
		public DateTime Vencimento
		{														  
			get{return _Vencimento;}
			set	{_Vencimento = value;}
		}
		
		public string GetTipoHistorico()
		{
			string tipoHistorico = string.Empty;

			switch (this.IndTipoHistorico)
			{
				case (int)IndTipoHistoricoExtintor.Inspecao:
				{
					tipoHistorico = "Inspeção";
					break;
				}
				case (int)IndTipoHistoricoExtintor.Recebimento:
				{
					tipoHistorico = "Recebimento";
					break;
				}
				case (int)IndTipoHistoricoExtintor.Reparacao:
				{
					tipoHistorico = "Reparação";
					break;
				}
				case (int)IndTipoHistoricoExtintor.UsoIncendio:
				{
					tipoHistorico = "Uso em Incêndio";
					break;
				}
				case (int)IndTipoHistoricoExtintor.UsoInstrucao:
				{
					tipoHistorico = "Uso em Instrução";
					break;
				}
			}

			return tipoHistorico;
		}

		public string GetReparacao()
		{
			string reparacao = string.Empty;

			switch (this.IndReparacao)
			{
				case (int)IndReparacaoExtintor.Diversos:
				{
					reparacao = "Diversos";
					break;
				}
				case (int)IndReparacaoExtintor.Mangote:
				{
					reparacao = "Mangote";
					break;
				}
				case (int)IndReparacaoExtintor.Manometro:
				{
					reparacao = "Manômetro";
					break;
				}
				case (int)IndReparacaoExtintor.Pintura:
				{
					reparacao = "Pintura";
					break;
				}
				case (int)IndReparacaoExtintor.Recarregado:
				{
					reparacao = "Recarregado";
					break;
				}
				case (int)IndReparacaoExtintor.SubstDifusor:
				{
					reparacao = "Substituição do Difusor";
					break;
				}
				case (int)IndReparacaoExtintor.SubstGatilho:
				{
					reparacao = "Substituição do Gatilho";
					break;
				}
				case (int)IndReparacaoExtintor.TesteHidrostatico:
				{
					reparacao = "Teste Hidrostático";
					break;
				}
				case (int)IndReparacaoExtintor.ValvulaCilindroAdicional:
				{
					reparacao = "Válvula de Cilindro Adicional";
					break;
				}
				case (int)IndReparacaoExtintor.ValvulaCompleta:
				{
					reparacao = "Válvula Completa";
					break;
				}
				case (int)IndReparacaoExtintor.ValvulaSeguranca:
				{
					reparacao = "Válvula de Segurança";
					break;
				}
			}

			return reparacao;
		}
		
		public override string ToString()
		{
			StringBuilder st = new StringBuilder();

			st.Append(this.DataEvento.ToString("dd-MM-yyyy"));
			st.Append(" - ");
			st.Append(this.GetTipoHistorico());
			
			if (this.IndTipoHistorico == (int)IndTipoHistoricoExtintor.Inspecao || this.IndTipoHistorico == (int)IndTipoHistoricoExtintor.Reparacao)
			{
				st.Append(" - ");
				st.Append(this.GetReparacao());
			}

			return st.ToString();
		}
	}

	[Database("opsa", "FabricanteExtintor", "IdFabricanteExtintor", "", "Fabricante de Extintor de Incêndio")]
	public class FabricanteExtintor : Ilitera.Common.Juridica
	{
		private int	_IdFabricanteExtintor;
		private Cliente _IdCliente;
	
		public FabricanteExtintor()
		{
		}
		public FabricanteExtintor(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{														  
			get{return _IdFabricanteExtintor;}
			set	{_IdFabricanteExtintor = value;}
		}
		public Cliente IdCliente
		{														  
			get{return _IdCliente;}
			set	{_IdCliente = value;}
		}
	}
	
	public enum IndTipoHistoricoExtintor: int
	{
		Recebimento = 1,
		Inspecao,
		Reparacao,
		UsoInstrucao,
		UsoIncendio
	}

	public enum IndReparacaoExtintor: int
	{
		SubstGatilho = 1,
		SubstDifusor,
		Mangote,
		ValvulaSeguranca,
		ValvulaCompleta,
		ValvulaCilindroAdicional,
		Pintura,
		Manometro,
		TesteHidrostatico,
		Recarregado,
		Diversos
	}
}
