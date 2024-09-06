using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    #region enums
    
    public enum TipoDeContrato: int
	{
		Mensalista,
		Pedido,
        Misto
    }

    public enum TipoNota : int
    {
        Recibo = 1,
        NotaFiscal = 2
    }

    #endregion

    #region class Contrato
    [Database("opsa","Contrato","IdContrato")]
	public class Contrato: Ilitera.Data.Table
    {
        #region Properties

        private int _IdContrato;
		private Empresa _IdEmpresa;
		private Cliente _IdCliente;
		private int _IndTipoContrato;
		private string _Descricao = string.Empty;
		private DateTime _Inicio = DateTime.Today;
		private DateTime _Termino;
		private Juridica _IdSacado;
		private DateTime _DataRenegociar;
		private int _IndPeriodicidadeRenegociar;
		private int _IntervaloRenegociar;
        private Endereco _IdEnderecoEntrega;
		private Cedente _IdCedente;
		private int _DiaVencimento = 5;
		private DateTime _DataRecisao;
		private MotivoRecisao _IdMotivoRecisao;	
		private string _Observacao = string.Empty;
		private bool _Renegociar;
		private int _IndPeriodicidadeVencimento;
		private int _DiaCarencia;
		private int _IntervaloVencimento;
        private int _PrazoEmMeses = 1;
		private bool _AgruparServico;
		private Servico _IdServico;
		private string _PathArquivoContrato = string.Empty;
		private Cobranca _IdCobranca;
        private Indice _IdIndice;
        private bool _BoletoComDemostrativo;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Contrato()
		{

		}
		public override int Id
		{
			get{return _IdContrato;}
			set{_IdContrato = value;}
		}
		[Obrigatorio(true, "A Empresa é obrigatório!")]
		public Empresa IdEmpresa
		{
			get{return _IdEmpresa;}
			set{_IdEmpresa = value;}
		}
		public Cliente IdCliente
		{
			get{return _IdCliente;}
			set{_IdCliente = value;}
		}
		public int IndTipoContrato
		{
			get{return _IndTipoContrato;}
			set{_IndTipoContrato = value;}
		}
		[Obrigatorio(true, "Descrição do Contrato é obrigatório!")]
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
		[Obrigatorio(true, "A Data de Inicio é obrigatório!")]
		public DateTime Inicio
		{
			get{return _Inicio;}
			set{_Inicio = value;}
		}
		public DateTime Termino
		{
			get{return _Termino;}
			set{_Termino = value;}
		}
		public Juridica IdSacado
		{
			get{return _IdSacado;}
			set{_IdSacado = value;}
		}

        public Endereco IdEnderecoEntrega
        {
            get { return _IdEnderecoEntrega; }
            set { _IdEnderecoEntrega = value; }
        }
		public DateTime DataRenegociar
		{
			get{return _DataRenegociar;}
			set{_DataRenegociar = value;}
		}
		public int IndPeriodicidadeRenegociar
		{
			get{return _IndPeriodicidadeRenegociar;}
			set{_IndPeriodicidadeRenegociar = value;}
		}
		public int IntervaloRenegociar
		{
			get{return _IntervaloRenegociar;}
			set{_IntervaloRenegociar = value;}
		}
        public int PrazoEmMeses
        {
            get { return _PrazoEmMeses; }
            set { _PrazoEmMeses = value; }
        }
		[Obrigatorio(true, "Cedente é campo obrigatório!")]
		public Cedente IdCedente
		{
			get{return _IdCedente;}
			set{_IdCedente = value;}
		}
		[Obrigatorio(true, "A Dia do Vencimento é obrigatório!")]
		public int DiaVencimento
		{
			get{return _DiaVencimento;}
			set{_DiaVencimento = value;}
		}
		public DateTime DataRecisao
		{
			get{return _DataRecisao;}
			set{_DataRecisao = value;}
		}
		public MotivoRecisao IdMotivoRecisao
		{
			get{return _IdMotivoRecisao;}
			set{_IdMotivoRecisao = value;}
		}		
		public string Observacao
		{
			get{return _Observacao;}
			set{_Observacao = value;}
		}		
		public bool Renegociar
		{
			get{return _Renegociar;}
			set{_Renegociar = value;}
		}		
		public MotivoRecisao GetMotivoRecisao()
		{
			this.IdMotivoRecisao.Find();
			return this.IdMotivoRecisao;
		}
		public int IndPeriodicidadeVencimento
		{
			get{return _IndPeriodicidadeVencimento;}
			set{_IndPeriodicidadeVencimento = value;}
		}
		public int DiaCarencia
		{
			get{return _DiaCarencia;}
			set{_DiaCarencia = value;}
		}
		public int IntervaloVencimento
		{
			get{return _IntervaloVencimento;}
			set{_IntervaloVencimento = value;}
		}
		public bool AgruparServico
		{
			get{return _AgruparServico;}
			set{_AgruparServico = value;}
		}
		public Servico IdServico
		{
			get{return _IdServico;}
			set{_IdServico = value;}
		}
		
		public string PathArquivoContrato
		{
			get{return _PathArquivoContrato;}
			set{_PathArquivoContrato = value;}
		}
		[Obrigatorio(true, "Cobrança é campo obrigatório!")]
		public Cobranca IdCobranca
		{
			get{return _IdCobranca;}
			set{_IdCobranca = value;}
		}
        public Indice IdIndice
        {
            get { return _IdIndice; }
            set { _IdIndice = value; }
        }

        public bool BoletoComDemostrativo
        {
            get { return _BoletoComDemostrativo; }
            set { _BoletoComDemostrativo = value; }
        }
        #endregion

        #region override ToString

        public override string ToString()
        {
            string ret;

            if (this.Descricao == string.Empty && this.Id != 0)
            {
                if (this._DataRecisao != new DateTime())
                    ret = "Rescindindo em " + this._DataRecisao.ToString("dd-MM-yyyy");
                else if (this._Termino == new DateTime())
                    ret = "Em vigor desde " + this._Inicio.ToString("dd-MM-yyyy");
                else
                    ret = "Em vigor desde " + this._Inicio.ToString("dd-MM-yyyy") + " até " + this._Termino.ToString("dd-MM-yyyy");
            }
            else
            {
                ret = this.Descricao;
            }
            return ret;
        }
        #endregion

        #region override Validate

        public override void Validate()
        {
            if (this.IdCedente.Id != 0)
            {
                this.IdCedente.Find();
                this.IdEmpresa.Id = this.IdCedente.IdEmpresa.Id;
            }

            if (this.Id == 0)
            {
                if (this.IdCliente.IdJuridicaPapel == null)
                    this.IdCliente.Find();

                if (this.IdCliente.IsLocalDeTrabalho())
                    throw new Exception("Empresa do tipo tomadora de serviços!");
            }

            base.Validate();
        }

        #endregion

        #region Metodos

        #region PrimeiroVencimento

        public DateTime PrimeiroVencimento()
		{
			DateTime ret = this.Inicio.AddMonths(1);
			
            return new DateTime(ret.Year, ret.Month, this.DiaVencimento).AddDays(this.DiaCarencia);
        }
        #endregion

        #region TotalServicos

        public double TotalServicos()
        {
            List<ContratoServico> list = new ContratoServico().Find<ContratoServico>("IdContrato=" + this.Id);

            double val = 0.0d;

            foreach (ContratoServico cs in list)
                val = val + cs.ValorTotal();

            return val;
        }

        public static double TotalServicos(int IdContrato)
        {
            List<ContratoServico> list = new ContratoServico().Find<ContratoServico>("IdContrato=" + IdContrato);

            double val = 0.0d;

            foreach (ContratoServico cs in list)
                val = val + cs.ValorTotal();

            return val;
        }
        #endregion

        #region GetContratosNotaTipoNF

        public static List<Contrato> GetContratosNotaTipoNF(int mes, int ano)
        {
            DateTime dataEmissao = new DateTime(ano, mes, 1).AddMonths(1).AddDays(-1);

            StringBuilder str = new StringBuilder();
            str.Append("DataRecisao IS NULL");
            str.Append(" AND (Inicio <='" + dataEmissao.ToString("yyyy-MM-dd") + "' OR Inicio IS NULL)");
            str.Append(" AND (Termino >='" + dataEmissao.ToString("yyyy-MM-dd") + "' OR Termino IS NULL)");
            str.Append(" AND IdCliente IN (SELECT IdCliente FROM qryClienteAtivos)");
            str.Append(" AND IdCedente IN (SELECT IdCedente FROM Cedente WHERE IndTipoNota=" + (int)TipoNota.NotaFiscal + ")");
            str.Append(" AND IdContrato NOT IN (SELECT IdContrato FROM Faturamento WHERE"
                            + " Referencia='" + dataEmissao.ToString("MM/yyyy")
                            + "' AND IndOrigemFaturamento=" + (int)Faturamento.OrigemFaturamento.Contrato + " )");
            str.Append(" ORDER BY (SELECT NomeCompleto FROM Pessoa WHERE IdPessoa = IdSacado)");

            List<Contrato> contratos = new Contrato().Find<Contrato>(str.ToString());

            return contratos;
        }
        #endregion

        #region GetContratosNotaTipoRecibo

        public static List<Contrato> GetContratosNotaTipoRecibo(int mes, int ano, Cliente cliente)
        {
            DateTime dataEmissao = new DateTime(ano, mes, 1).AddMonths(1).AddDays(-1);

            StringBuilder str = new StringBuilder();
            str.Append(" (Inicio <='" + dataEmissao.ToString("yyyy-MM-dd") + "' OR Inicio IS NULL)");
            str.Append(" AND (Termino >='" + dataEmissao.ToString("yyyy-MM-dd") + "' OR Termino IS NULL)");
            str.Append(" AND DataRecisao IS NULL");
            str.Append(" AND IdCliente IN (SELECT IdCliente FROM qryClienteAtivos)");
            str.Append(" AND IdCedente IN (SELECT IdCedente	FROM Cedente WHERE IndTipoNota=" + (int)TipoNota.Recibo + ")");

            if (cliente.Id != 0)
                str.Append(" AND IdCliente =" + cliente.Id);

            str.Append(" ORDER BY (SELECT NomeAbreviado FROM Pessoa WHERE IdPessoa = IdCliente)");

            List<Contrato> contratos = new Contrato().Find<Contrato>(str.ToString());

            return contratos;
        }
        #endregion

        //public class GerarFaturamentoRecibo
        //{
        //    public static void GerarFaturamentoClientesNotaTipoRecibo(int mes, int ano)
        //    {
        //        GerarFaturamentoClientesNotaTipoRecibo(mes, ano, new Cliente());
        //    }

        //    public static void GerarFaturamentoClientesNotaTipoRecibo(int mes, int ano, Cliente cliente)
        //    {
        //        List<Contrato> contratos = GetContratosNotaTipoRecibo(mes, ano, cliente);

        //        GerarFaturamentos(mes, ano, contratos);
        //    }

        //    public static void GerarFaturamentos(int mes,
        //                                        int ano,
        //                                        List<Contrato> contratos)
        //    {
        //        foreach (Contrato contrato in contratos)
        //        {
        //            try
        //            {
        //                contrato.GerarFaturamento(mes, ano, 0);
        //            }
        //            catch (Exception ex)
        //            {
        //                System.Diagnostics.Debug.WriteLine(ex.Message);
        //            }
        //        }
        //    }
        //}

        #region GerarFaturamento

        public Faturamento GerarFaturamento(int mes,
                                            int ano,
                                            int ProximoNumero)
        {
            return GerarFaturamento(mes, ano, ProximoNumero, false);
        }

        public Faturamento GerarFaturamento(int mes,
                                            int ano,
                                            int ProximoNumero,
                                            bool IsAvulso)
        {
            Contrato contrato = this;

            Faturamento faturamento = new Faturamento();

            if (contrato.DiaVencimento == 0)
                throw new Exception(contrato.Descricao + " - O dia de vencimento não foi informado!");

            DateTime dataEmissao = DateTime.Today;
            DateTime dataReferencia = new DateTime(ano, mes, 1).AddMonths(1).AddDays(-1);
            DateTime dataVencimento = new DateTime(ano, mes, contrato.DiaVencimento).AddMonths(contrato.PrazoEmMeses);
            DateTime dataMaxima = new DateTime(ano, mes, 1).AddMonths(1).AddDays(-1);

            if (dataVencimento < contrato.PrimeiroVencimento())
                throw new Exception("Só pode ser faturado a partir de " + contrato.PrimeiroVencimento().ToString("dd-MM-yyyy") + "!");

            if (contrato.Termino != new DateTime() && dataMaxima > contrato.Termino)
                throw new Exception("Só pode ser faturado antes de " + contrato.Termino.ToString("dd-MM-yyyy") + "!");

            if (!IsAvulso)
            {
                faturamento.Find("IdContrato=" + contrato.Id
                                + " AND Referencia='" + mes.ToString("00") + "/" + ano.ToString() + "'"
                                + " AND IndOrigemFaturamento=" + (int)Faturamento.OrigemFaturamento.Contrato);
            }

            if (faturamento.Id != 0 && faturamento.IndEntregaBoleto != (int)Faturamento.EntregaBoleto.NaoEntregue)
                throw new Exception("Boleto já enviado ao cliente não pode ser recalculado!");

            if (faturamento.Id == 0)
            {
                faturamento.Inicialize();
                faturamento.IdContrato = contrato;
                faturamento.IdSacado.Id = contrato.IdSacado.Id;
                faturamento.IdCedente.Id = contrato.IdCedente.Id;
                faturamento.IdCobranca.Id = contrato.IdCobranca.Id;
                faturamento.IndOrigemFaturamento = IsAvulso ? (int)Faturamento.OrigemFaturamento.Avulso : (int)Faturamento.OrigemFaturamento.Contrato;
                faturamento.Referencia = mes.ToString("00") + "/" + ano;
                faturamento.Emissao = dataEmissao;
                faturamento.Vencimento = dataVencimento;
                faturamento.Numero = ProximoNumero;
                faturamento.Save();
            }
            else
            {
                faturamento.IdContrato = contrato;
                faturamento.IdSacado.Id = contrato.IdSacado.Id;
                faturamento.IdCedente.Id = contrato.IdCedente.Id;
                faturamento.IdCobranca.Id = contrato.IdCobranca.Id;
                faturamento.Emissao = dataEmissao;
                faturamento.Vencimento = dataVencimento;

                new FaturamentoServico().Delete("IdFaturamento=" + faturamento.Id);
                new FaturamentoPedido().Delete("IdFaturamento=" + faturamento.Id);
                new FaturamentoExameBase().Delete("IdFaturamento=" + faturamento.Id);
                new FaturamentoServicoAnalitico().Delete("IdFaturamento=" + faturamento.Id);
            }

            faturamento.Faturar(dataReferencia, false);

            return faturamento;
        }
        #endregion

        #region ReajustarContratoDoTipoPedido

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public void ReajustarContratoDoTipoPedido(DateTime dataReajuste, Cotacao cotacao)
        {
            if (this.IndTipoContrato != (int)TipoDeContrato.Pedido)
                throw new Exception("Somente para contratos do tipo pedido!");

            if(cotacao.Id==0)
                throw new Exception("Selecione uma cotação!");

            if (cotacao.IdIndice.mirrorOld == null)
                cotacao.IdIndice.Find();

            if (cotacao.IdIndice.IndTipoValor != (int)TipoValor.Percentuais)
                throw new Exception("Somente para indice com percentuais!");

            ContratoReajuste contratoReajuste = new ContratoReajuste();
            contratoReajuste.Find("IdContrato=" + this.Id + " AND IdCotacao=" + cotacao.Id);

            if(contratoReajuste.Id!=0)
                throw new Exception("Contrato já reajustado por esse indice!");

            contratoReajuste.Inicialize();
            contratoReajuste.IdContrato = this;
            contratoReajuste.DataReajuste = dataReajuste;
            contratoReajuste.IdCotacao = cotacao;

            IDbTransaction trans = contratoReajuste.GetTransaction();

            try
            {
                contratoReajuste.Save();

                ReajustarContratoServicoObrigacao(contratoReajuste);

                ReajustarExames(contratoReajuste);

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();

                throw ex;
            }
        }
        #endregion

        #region ReajustarContratoServicoObrigacao

        private void ReajustarContratoServicoObrigacao(ContratoReajuste contratoReajuste)
        {
            string where = "IdContratoServico IN (SELECT IdContratoServico FROM ContratoServico WHERE IdContrato=" + this.Id + ")";

            List<ContratoServicoObrigacao>
                list = new ContratoServicoObrigacao().Find<ContratoServicoObrigacao>(where);

            foreach (ContratoServicoObrigacao contratoServObrigacao in list)
                contratoServObrigacao.Reajustar(contratoReajuste);
        }
        #endregion

        #region ReajustarExames

        private void ReajustarExames(ContratoReajuste contratoReajuste)
        {
            string where = "IdContratoServico IN (SELECT IdContratoServico"
                                                + " FROM ContratoServico"
                                                + " WHERE IdContrato=" + this.Id + ")";

            List<ContratoServicoExameDicionario>
                list = new ContratoServicoExameDicionario().Find<ContratoServicoExameDicionario>(where);

            foreach (ContratoServicoExameDicionario contratoServicoExameDicionario in list)
                contratoServicoExameDicionario.Reajustar(contratoReajuste);
        }
        #endregion

        #endregion
    }
    #endregion

    #region class MotivoRecisao

    [Database("opsa","MotivoRecisao","IdMotivoRecisao")]
	public class MotivoRecisao: Ilitera.Data.Table 
	{
		private int _IdMotivoRecisao;
		private string _Descricao = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public MotivoRecisao()
		{

		}
		public override int Id
		{
			get{return _IdMotivoRecisao;}
			set{_IdMotivoRecisao = value;}
		}
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
    }
    #endregion

    #region class Agencia

    [Database("opsa","Agencia","IdAgencia")]
	public class Agencia: Ilitera.Common.Juridica
	{
		private int _IdAgencia;		
		private Banco  _IdBanco;
		private string _Numero = string.Empty;
		
		public Agencia()
		{			
			this.IdJuridicaPapel = new JuridicaPapel();
			this.IdJuridicaPapel.Id = (int)IndJuridicaPapel.Agencia;
			this.IndTipoPessoa = (short)TipoPessoa.Juridica;
		}
		public override int Id
		{
			get{return _IdAgencia;}
			set{_IdAgencia = value;}
		}	
		public Banco IdBanco
		{
			get{return _IdBanco;}
			set{_IdBanco = value;}
		}
		public string Numero
		{
			get{return _Numero;}
			set{_Numero = value;}
		}	
		public override string ToString()
		{
			return this.Numero;
		}
    }
    #endregion

    #region class Banco

    [Database("opsa","Banco","IdBanco")]
	public class Banco: Ilitera.Data.Table
	{
		private int _IdBanco;		
		private string _Nome=string.Empty ;
		private string _Numero=string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Banco()
		{

		}
		public override int Id
		{
			get{return _IdBanco;}
			set{_IdBanco = value;}
		}	
		[Obrigatorio(true, "Numero banco é obrigatório!")]
		public string Numero
		{
			get{return _Numero;}
			set{_Numero = value;}
		}	
		[Obrigatorio(true, "Nome banco é obrigatório!")]
		public string Nome
		{
			get{return _Nome;}
			set{_Nome = value;}
		}
		public override string ToString()
		{
			return this.Numero;
		}
    }
    #endregion

    #region class ContaCorrente

    [Database("opsa","ContaCorrente","IdContaCorrente")]
	public class ContaCorrente: Ilitera.Data.Table 
	{
		private int _IdContaCorrente;
		private Pessoa _IdPessoa;
		private Agencia _IdAgencia;	
		private Banco _IdBanco;		
		private string _Numero = string.Empty;	
		private string _Titular = string.Empty;
		private string _NumeroAgencia = string.Empty;
		private string _Responsavel = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ContaCorrente()
		{
		}
		public override int Id
		{
			get{return _IdContaCorrente;}
			set{_IdContaCorrente = value;}
		}
		[Obrigatorio(true, "Pessoa é obrigatório!")]
		public Pessoa IdPessoa
		{
			get{return _IdPessoa;}
			set{_IdPessoa = value;}
		}		
		public Agencia IdAgencia
		{
			get{return _IdAgencia;}
			set{_IdAgencia = value;}
		}		
		[Obrigatorio(true, "Numero da conta corrente é obrigatório!")]
		public string Numero
		{
			get{return _Numero;}
			set{_Numero = value;}
		}		
		public string Titular
		{
			get{return _Titular;}
			set{_Titular = value;}
		}		
		[Obrigatorio(true, "Banco é obrigatório!")]
		public Banco IdBanco
		{
			get{return _IdBanco;}
			set{_IdBanco = value;}
		}
		public string NumeroAgencia
		{
			get{return _NumeroAgencia;}
			set{_NumeroAgencia = value;}
		}
		public string Responsavel
		{
			get{return _Responsavel;}
			set{_Responsavel = value;}
		}
		public override string ToString()
		{
			if(this.mirrorOld==null)
				this.Find();

			return this.IdAgencia.ToString() + " "+ this.Numero;
		}

		public override int Save()
		{
			if(this.IdAgencia.Id!=0)
			{
				this.IdAgencia.Find();
				this.IdBanco = this.IdAgencia.IdBanco;
				this.NumeroAgencia = this.IdAgencia.Numero;
			}
			return base.Save ();
		}
    }
    #endregion

    #region class Cedente

    public enum Cedentes : int
    {
        BanespaOpsa = -4411682,
        BanespaMestra = 1
    }

    [Database("opsa", "Cedente","IdCedente")]
    public class Cedente : Ilitera.Data.Table
    {
        private int _IdCedente;
        private Empresa _IdEmpresa;
        private ContaCorrente _IdContaCorrente;
        private Cobranca _IdCobranca;
        private string _NomeCedente = string.Empty;
        private string _RazaoSocial = string.Empty;
        private string _CNPJ = string.Empty;
        private string _CodigoCedente = string.Empty;
        private string _UsoBanco = string.Empty;
        private string _Aceite = string.Empty;
        private string _Especie = string.Empty;
        private string _Carteira = string.Empty;
        private int _ProximoNumero;
        private int _IndTipoNota;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Cedente()
        {

        }
        public override int Id
        {
            get { return _IdCedente; }
            set { _IdCedente = value; }
        }
        [Obrigatorio(true, "A Empresa é obrigatório!")]
        public Empresa IdEmpresa
        {
            get { return _IdEmpresa; }
            set { _IdEmpresa = value; }
        }
        [Obrigatorio(true, "A Conta Corrente é obrigatório!")]
        public ContaCorrente IdContaCorrente
        {
            get { return _IdContaCorrente; }
            set { _IdContaCorrente = value; }
        }
        [Obrigatorio(true, "A Cobraça é obrigatório!")]
        public Cobranca IdCobranca
        {
            get { return _IdCobranca; }
            set { _IdCobranca = value; }
        }
        [Obrigatorio(true, "Nome Cedente é obrigatório!")]
        public string NomeCedente
        {
            get { return _NomeCedente; }
            set { _NomeCedente = value; }
        }
        public string RazaoSocial
        {
            get { return _RazaoSocial; }
            set { _RazaoSocial = value; }
        }
        public string CNPJ
        {
            get { return _CNPJ; }
            set { _CNPJ = value; }
        }
        public string CodigoCedente
        {
            get { return _CodigoCedente; }
            set { _CodigoCedente = value; }
        }
        public string UsoBanco
        {
            get { return _UsoBanco; }
            set { _UsoBanco = value; }
        }
        public string Aceite
        {
            get { return _Aceite; }
            set { _Aceite = value; }
        }
        public string Especie
        {
            get { return _Especie; }
            set { _Especie = value; }
        }
        public string Carteira
        {
            get { return _Carteira; }
            set { _Carteira = value; }
        }
        public int ProximoNumero
        {
            get { return _ProximoNumero; }
            set { _ProximoNumero = value; }
        }
        public int IndTipoNota
        {
            get { return _IndTipoNota; }
            set { _IndTipoNota = value; }
        }
        public static Cedente GetCedente(string numCedente)
        {
            List<Cedente> list = new Cedente().Find<Cedente>("REPLACE(CodigoCedente, '-', '') = '" + numCedente.ToString() + "'");

            if (list.Count == 1)
                return list[0];
            else
                return new Cedente();
        }
    }
    #endregion

    #region class Tributo

    [Database("opsa","Tributo","IdTributo")]
	public class Tributo: Ilitera.Data.Table 
	{
        public enum Copetencia : int
        {
            Federal,
            Estadual,
            Municipal
        }

		private int _IdTributo;
		private string _NomeCodigo = string.Empty;
		private string _NomeAbreviado = string.Empty;
		private string _NomeCompleto = string.Empty;
        private Copetencia _IndCopetencia;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Tributo()
		{
		}
		public override int Id
		{
			get{return _IdTributo;}
			set{_IdTributo = value;}
		}		
		public string NomeCodigo
		{
			get{return _NomeCodigo;}
			set{_NomeCodigo = value;}
		}		
		[Obrigatorio(true, "Nome Abreviado é campo obrigatório.")]
		public string NomeAbreviado
		{
			get{return _NomeAbreviado;}
			set{_NomeAbreviado = value;}
		}		
		public string NomeCompleto
		{
			get{return _NomeCompleto;}
			set{_NomeCompleto = value;}
		}
        public Copetencia IndCopetencia
		{
			get{return _IndCopetencia;}
			set{_IndCopetencia = value;}
		}
    }
    #endregion

    #region class TributoRegra

    [Database("opsa","TributoRegra","IdTributoRegra")]
	public class TributoRegra: Ilitera.Data.Table
	{
        public enum FatoGerador : int
        {
            Faturamento,
            Pagamento
        }

		private int _IdTributoRegra;
		private Tributo _IdTributo;
		private string _Nome = string.Empty;
		private double _ValorBaseMin;
		private double _ValorBaseMax;
		private double _ValorRecolhimentoMin;
		private double _ValorRecolhimentoMax;
		private double _ValorDeducao;
		private double _PorcentagemDeducao;
		private DateTime _DataInicial = DateTime.Today;
		private DateTime _DataFinal = DateTime.Today;
        private FatoGerador _IndFatoGerador;
		private LocalizacaoGeografica _IdLocalizacaoGeografica;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TributoRegra()
		{
		}
		public override int Id
		{
			get{return _IdTributoRegra;}
			set{_IdTributoRegra = value;}
		}		
		public Tributo IdTributo
		{
			get{return _IdTributo;}
			set{_IdTributo = value;}
		}		
		public string Nome
		{
			get{return _Nome;}
			set{_Nome = value;}
		}		
		public double ValorBaseMin
		{
			get{return _ValorBaseMin;}
			set{_ValorBaseMin = value;}
		}		
		public double ValorBaseMax
		{
			get{return _ValorBaseMax;}
			set{_ValorBaseMax = value;}
		}			
		public double ValorRecolhimentoMin
		{
			get{return _ValorRecolhimentoMin;}
			set{_ValorRecolhimentoMin = value;}
		}			
		public double ValorRecolhimentoMax
		{
			get{return _ValorRecolhimentoMax;}
			set{_ValorRecolhimentoMax = value;}
		}			
		public double ValorDeducao
		{
			get{return _ValorDeducao;}
			set{_ValorDeducao = value;}
		}	
		public double PorcentagemDeducao
		{
			get{return _PorcentagemDeducao;}
			set{_PorcentagemDeducao = value;}
		}	
		
		public DateTime DataInicial
		{
			get{return _DataInicial;}
			set{_DataInicial = value;}
		}			
		public DateTime DataFinal
		{
			get{return _DataFinal;}
			set{_DataFinal = value;}
		}
        public FatoGerador IndFatoGerador
		{
			get{return _IndFatoGerador;}
			set{_IndFatoGerador = value;}
		}			
		public LocalizacaoGeografica IdLocalizacaoGeografica
		{
			get{return _IdLocalizacaoGeografica;}
			set{_IdLocalizacaoGeografica = value;}
		}				

		public static DataSet ListaTributoRegra(Tributo tributo)
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("Id", Type.GetType("System.String"));
			table.Columns.Add("Nome", Type.GetType("System.String"));
			ds.Tables.Add(table);
			DataRow newRow;

            List<TributoRegra> 
                list = new TributoRegra().Find<TributoRegra>("IdTributo= " + tributo.Id 
                                                            + " ORDER BY Nome");		
			
            foreach(TributoRegra tributoRegra in list)
			{
				newRow = ds.Tables[0].NewRow();
                newRow["Id"] = tributoRegra.Id;
                newRow["Nome"] = tributoRegra.Nome;
				ds.Tables[0].Rows.Add(newRow);
			}
			return ds;
		}
    }
    #endregion

    #region class TributoAliquota

    [Database("opsa","TributoAliquota","IdTributoAliquota")]
	public class TributoAliquota: Ilitera.Data.Table 
	{
		private int _IdTributoAliquota;
		private TributoRegra _IdTributoRegra;
		private DateTime _Data = DateTime.Today;
		private double _Percentual;	
		private double _PctRetencaoNaFonte;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TributoAliquota()
		{
		}
		public override int Id
		{
			get{return _IdTributoAliquota;}
			set{_IdTributoAliquota = value;}
		}		
		public TributoRegra IdTributoRegra
		{
			get{return _IdTributoRegra;}
			set{_IdTributoRegra = value;}
		}		
		public DateTime Data
		{
			get{return _Data;}
			set{_Data = value;}
		}		
		public double Percentual
		{
			get{return _Percentual;}
			set{_Percentual = value;}
		}				
		public double PctRetencaoNaFonte
		{
			get{return _PctRetencaoNaFonte;}
			set{_PctRetencaoNaFonte = value;}
		}
        public static DataSet ListaTributoAliquota(TributoRegra tributoRegra)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Id", Type.GetType("System.String"));
            table.Columns.Add("Data", Type.GetType("System.String"));
            table.Columns.Add("Percentual", Type.GetType("System.String"));
            ds.Tables.Add(table);
            DataRow newRow;

            List<TributoAliquota>
                list = new TributoAliquota().Find<TributoAliquota>("IdTributoRegra=" + tributoRegra.Id);

            foreach (TributoAliquota tributoAliquota in list)
            {
                newRow = ds.Tables[0].NewRow();
                newRow["Id"] = tributoAliquota.Id;
                newRow["Data"] = tributoAliquota.Data.ToString("dd-MM-yyyy");
                newRow["Percentual"] = tributoAliquota.Percentual.ToString();
                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
    }
    #endregion
}
