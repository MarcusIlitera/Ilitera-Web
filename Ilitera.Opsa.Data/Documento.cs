using System;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    #region enum Documentos

    public enum Documentos : int
    {
        PPRA = 1,
        Auditoria = 2,
        LaudoEletrico = 3,
        VasosInspecao = 4,
        VasosProjeto = 5,
        MapaRisco = 6,
        PCMSO = 7,
        ExamePeriodico = 8,
        Treinamentos = 9,
        CIPA = 10,
        AE = 11,
        CaldeiraInspecao = 12,
        CaldeiraProjeto = 13,
        OrdemServicoNr1_7 = 14,
        Pericia = 15,
        DocumentoAvulso = 16
    }
    #endregion

    #region Class DocumentoBase

    [Database("opsa","DocumentoBase","IdDocumentoBase")]
	public class DocumentoBase : Ilitera.Data.Table 
	{
		private int _IdDocumentoBase;
		private string _NomeDocumento;
		private string _FotoDiretorioPadrao;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public DocumentoBase()
		{

		}
		public override int Id
		{
			get{return _IdDocumentoBase;}
			set{_IdDocumentoBase = value;}
		}
		public string NomeDocumento
		{
			get{return _NomeDocumento;}
			set{_NomeDocumento = value;}
		}
		public string FotoDiretorioPadrao
		{
			get{return _FotoDiretorioPadrao;}
			set{_FotoDiretorioPadrao = value;}
		}
        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return NomeDocumento;
        }
    }
    #endregion

    #region class Documento

    [Database("opsa","Documento","IdDocumento")]
	public class Documento : Ilitera.Data.Table 
	{
		private int _IdDocumento;
		private DocumentoBase _IdDocumentoBase;
		private Cliente _IdCliente;
		private Pedido _IdPedido;
		private Prestador _IdPrestador;
		private DateTime _DataLevantamento;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Documento()
		{
			
		}
		public override int Id
		{
			get{return _IdDocumento;}
			set{_IdDocumento = value;}
		}
		[Obrigatorio(true, "Documento base é campo obrigatório!")]
		public DocumentoBase IdDocumentoBase
		{
			get{return _IdDocumentoBase;}
			set{_IdDocumentoBase = value;}
		}
		[Obrigatorio(true, "Cliente é campo obrigatório!")]
		public Cliente IdCliente
		{
			get{return _IdCliente;}
			set{_IdCliente = value;}
		}
		public Pedido IdPedido
		{
			get{return _IdPedido;}
			set{_IdPedido = value;}
		}
		[Obrigatorio(true, "Prestador é campo obrigatório!")]
		public Prestador IdPrestador
		{
			get{return _IdPrestador;}
			set{_IdPrestador = value;}
		}
		[Obrigatorio(true, "Data de Levantamento é campo obrigatório!")]
		public DateTime DataLevantamento
		{
			get{return _DataLevantamento;}
			set{_DataLevantamento = value;}
		}

        public override void Delete()
        {
            if (this.IdPedido.Id != 0)
            {
                if (this.IdPedido.mirrorOld == null)
                    this.IdPedido.Find();

                if (this.IdPedido.DataCancelamento == new DateTime()
                    && this.IdPedido.DataConclusao != new DateTime())
                    throw new Exception("O documento "+ this.IdDocumentoBase.ToString() 
                                        + " está com o pedido concluído.\n\nNão pode ser excluído!");

                FaturamentoPedido faturamentoPedido = new FaturamentoPedido();
                faturamentoPedido.Find("IdPedido=" + this.IdPedido.Id);

                if (faturamentoPedido.Id != 0)
                    throw new Exception("Pedido já faturado o documento.\n\nNão pode ser excluído!");
            }
            base.Delete();
        }
    }
    #endregion

    #region class DocumentoAlteracao

    [Database("opsa","DocumentoAlteracao","IdDocumentoAlteracao")]
	public class DocumentoAlteracao : Ilitera.Data.Table , IDataInicioTermino
    {
        #region Properties
        private int _IdDocumentoAlteracao;
		private Documento _IdDocumento;
        private LaudoTecnico _IdLaudoTecnico;
		private Pedido _IdPedido;
		private Prestador _IdPrestador;
		private DateTime _DataInicio;
		private DateTime _DataTermino;
		private string _Motivo=string.Empty;
        private bool _IsAlteracaoPCMSO;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public DocumentoAlteracao()
		{
			
		}
		public override int Id
		{
			get{return _IdDocumentoAlteracao;}
			set{_IdDocumentoAlteracao = value;}
		}
		public Documento IdDocumento
		{
			get{return _IdDocumento;}
			set{_IdDocumento = value;}
		}
        public LaudoTecnico IdLaudoTecnico
		{
            get { return _IdLaudoTecnico; }
            set { _IdLaudoTecnico = value; }
		}
        public Pedido IdPedido
        {
            get { return _IdPedido; }
            set { _IdPedido = value; }
        }
		[Obrigatorio(true, "Pestador é campo obrigatório!")]
		public Prestador IdPrestador
		{
			get{return _IdPrestador;}
			set{_IdPrestador = value;}
		}
		[Obrigatorio(true, "Data de alteração é campo obrigatório!")]
		public DateTime DataInicio
		{
			get{return _DataInicio;}
			set{_DataInicio = value;}
		}
		public DateTime DataTermino
		{
			get{return _DataTermino;}
			set{_DataTermino = value;}
		}
		public string Motivo
		{
			get{return _Motivo;}
			set{_Motivo = value;}
		}

        public bool IsAlteracaoPCMSO
        {
            get { return _IsAlteracaoPCMSO; }
            set { _IsAlteracaoPCMSO = value; }
        }
        #endregion

        #region Metodos

        #region Validate

        public override void Validate()
        {
            base.Validate();

            if (this.DataTermino != new DateTime() && this.Motivo == string.Empty)
                throw new Exception("O Motivo é campo obrigatório!");
        }
        #endregion

        #region Save

        public override int Save()
        {
            int ret = base.Save();

            ReabrirPedidoPcmso();

            ReabrirPedidoOrdemServico();

            return ret;
        }
        #endregion

        #region ReabrirPedidoPcmso

        private void ReabrirPedidoPcmso()
        {
            if (this.IsAlteracaoPCMSO
                && this.DataTermino != new DateTime()
                && this.IdLaudoTecnico.Id != 0)
            {
                Pcmso pcmso = new Pcmso();
                pcmso.Find("IdLaudoTecnico=" + this.IdLaudoTecnico.Id);

                if (pcmso.Id != 0)
                {
                    if (pcmso.IdPedido.mirrorOld == null)
                        pcmso.IdPedido.Find();

                    if (pcmso.IdPedido.IndStatus == (int)StatusPedidos.Executado)
                    {
                        pcmso.IdPedido.DataConclusao = new DateTime();
                        pcmso.IdPedido.Observacao = "Revisão (PPRA Alterado por " + this.IdPrestador.ToString() + ")"
                            + "- Alterações: " + this.Motivo;
                        pcmso.IdPedido.IdPrestador.Id = (int)Medicos.DraRosana;
                        pcmso.IdPedido.Save();

                        new ControlePedido().Delete("IdPedido=" + pcmso.IdPedido.Id);

                        ControlePedido.GerarDatario(pcmso.IdPedido);
                    }
                }
            }
        }
        #endregion

        #region ReabrirPedidoOrdemServico

        private void ReabrirPedidoOrdemServico()
        {
            if (this.IsAlteracaoPCMSO
                && this.DataTermino != new DateTime()
                && this.IdLaudoTecnico.Id != 0)
            {
                OrdemServicoNR1_7 ordemServico = new OrdemServicoNR1_7();
                ordemServico.Find("IdLaudoTecnico=" + this.IdLaudoTecnico.Id);

                if (ordemServico.Id != 0)
                {
                    Pedido pedido = ordemServico.IdPedido;

                    if (pedido.mirrorOld == null)
                        pedido.Find();

                    if (pedido.IndStatus == (int)StatusPedidos.Executado)
                    {
                        pedido.DataConclusao = new DateTime();
                        pedido.Observacao = "Revisão (PPRA Alterado por " + this.IdPrestador.ToString() + ")"
                                            + "- Alterações: " + this.Motivo;
                        pedido.Save();

                        new ControlePedido().Delete("IdPedido=" + pedido.Id);

                        ControlePedido.GerarDatario(pedido);
                    }
                }
            }
        }
        #endregion

        #endregion
    }
    #endregion

    #region Class DocumentoAvulso

    [Database("opsa", "DocumentoAvulso", "IdDocumentoAvulso")]
    public class DocumentoAvulso : Documento
    {
        private int _IdDocumentoAvulso;
        private string _Descricao = string.Empty;
        private string _NomeArquivo = string.Empty;
        private string _PathArquivoFonte = string.Empty;
        private string _PathArquivoPublicado = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public DocumentoAvulso()
        {

        }
        public override int Id
        {
            get { return _IdDocumentoAvulso; }
            set { _IdDocumentoAvulso = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public string NomeArquivo
        {
            get { return _NomeArquivo; }
            set { _NomeArquivo = value; }
        }
        public string PathArquivoFonte
        {
            get { return _PathArquivoFonte; }
            set { _PathArquivoFonte = value; }
        }
        public string PathArquivoPublicado
        {
            get { return _PathArquivoPublicado; }
            set { _PathArquivoPublicado = value; }
        }
        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return Descricao;
        }
    }
    #endregion
}
