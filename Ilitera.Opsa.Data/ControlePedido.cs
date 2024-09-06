using System;
using System.Data;
using Ilitera.Data;
using System.Collections;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "ControlePedido", "IdControlePedido")]
    public class ControlePedido : Ilitera.Data.Table
    {
        private int _IdControlePedido;
        private Controle _IdControle;
        private Pedido _IdPedido;
        private int _Ordem;
        private Prestador _IdPrestador;
        private DateTime _Inicio;
        private DateTime _Termino;
        private string _Observacao = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ControlePedido()
        {

        }

        public override int Id
        {
            get { return _IdControlePedido; }
            set { _IdControlePedido = value; }
        }
        public Controle IdControle
        {
            get { return _IdControle; }
            set { _IdControle = value; }
        }
        public Pedido IdPedido
        {
            get { return _IdPedido; }
            set { _IdPedido = value; }
        }
        [Obrigatorio(true, "Ordem é campo obrigatório!")]
        public int Ordem
        {
            get { return _Ordem; }
            set { _Ordem = value; }
        }
        public Prestador IdPrestador
        {
            get { return _IdPrestador; }
            set { _IdPrestador = value; }
        }
        public DateTime Inicio
        {
            get { return _Inicio; }
            set { _Inicio = value; }
        }
        public DateTime Termino
        {
            get { return _Termino; }
            set { _Termino = value; }
        }
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        public override string ToString()
        {
            this.IdControle.Find();
            if (_Termino == new DateTime() || _Termino == new DateTime(1753, 1, 1))
                return "Pendente - " + this.IdControle.ToString();
            else
                return "Concluído em " + this.Termino.ToString("dd-MM-yyyy") + " - " + this.IdControle.ToString();
        }

        public string GetDescricao()
        {
            string ret;

            if (this.IdControle.Nome == string.Empty)
                this.IdControle.Find();

            ret = this.IdControle.Nome;

            return ret;
        }

        public static void GerarDatario(Pedido pedido)
        {
            GerarDatario(pedido, null);
        }

        public static void GerarDatario(Pedido pedido, IDbTransaction trans)
        {
            string where = "IdObrigacao=" + pedido.IdObrigacao.Id
                            + " AND IsInativo=0"
                            + " ORDER BY Ordem";

            ArrayList list = new ControleObrigacao().Find(where);

            foreach (ControleObrigacao controleObrigacao in list)
            {
                ControlePedido controlePedido = new ControlePedido();
                controlePedido.Find("IdPedido=" + pedido.Id + " AND IdControle=" + controleObrigacao.IdControle.Id);

                if (controlePedido.Id == 0)
                {
                    controlePedido.Inicialize();

                    controlePedido.IdPedido = pedido;
                    controlePedido.IdControle = controleObrigacao.IdControle;
                    controlePedido.Ordem = controleObrigacao.Ordem;

                    controlePedido.Transaction = trans;

                    controlePedido.Save();
                }
            }
        }

        public override void Validate()
        {
            if (this.Termino != new DateTime() && this.Inicio >= this.Termino)
                throw new Exception("A data de início deve ser maior que a data de término!");

            base.Validate();
        }

        public override int Save()
        {
            int ret = base.Save();

            if (this.IdPedido.mirrorOld == null)
            {
                this.IdPedido.Find();
                this.IdPedido.IdObrigacao.Find();
            }

            if (this.IdPedido.IdObrigacao.DatadorFinalizadoParaConcluido)
            {
                ArrayList listControlePedido
                    = new ControlePedido().Find("IdPedido=" + this.IdPedido.Id
                    + " AND Termino IS NULL");

                if (listControlePedido.Count == 0)
                {
                    DateTime dataConclusao = this.IdPedido.GetDataDocumento();

                    this.IdPedido.DataConclusao = dataConclusao;
                    this.IdPedido.Save();

                    ArrayList listPedidos = new Pedido().Find("IdPedidoGrupo="
                        + this.IdPedido.IdPedidoGrupo.Id
                        + " AND DataConclusao IS NULL"
                        + " AND IdObrigacao IN (SELECT IdObrigacao FROM Obrigacao WHERE IdDocumentoBase IS NULL)");

                    foreach (Pedido pedido in listPedidos)
                    {
                        pedido.DataConclusao = dataConclusao;
                        pedido.Save();
                    }

                    if (this.IdPedido.IdObrigacao.IsPPRA())
                    {
                        Obrigacao obrigacao = new Obrigacao();
                        obrigacao.Find((int)Obrigacoes.PCMSO);

                        Cliente cliente = new Cliente();
                        cliente.Find(this.IdPedido.IdCliente.Id);

                        ObrigacaoCliente obrigacaoCliente = ObrigacaoCliente.GetObrigacaoCliente(cliente, obrigacao);
                        obrigacaoCliente.Atualizar();
                    }
                }
            }

            /*
             * 
            if (this.IdControle.Id == (int)Controles.EnviarEmailConferenciaCliente
                && this.Termino == new DateTime()
                && this.IdPedido.IdPrestador.Id == (int)Prestadores.AtualizacaoPPRA_LTCA)
            {
                this.Inicio = DateTime.Now;
                this.IdPrestador.Id = (int)Prestadores.AtualizacaoPPRA_LTCA;

                EnviarEmailRetiradaPasta(this.IdPedido.IdCliente);
            }
            //Baixa os que não precisam mandar email
            else if (this.IdControle.Id == (int)Controles.EnviarEmailConferenciaCliente
                    && this.Termino == new DateTime()
                    && this.IdPedido.IdPrestador.Id != (int)AtualizacaoPPRA_LTCA)
            {
                this.Termino = DateTime.Now;
            }
            */

            return ret;
        }

        public static void EnviarEmailRetiradaPasta(Usuario usuario, Cliente cliente)
        {

        }
    }
}





