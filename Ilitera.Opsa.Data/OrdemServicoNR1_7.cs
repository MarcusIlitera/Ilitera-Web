using System;
using System.Collections.Generic;
using System.Text;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "OrdemServicoNR1_7", "IdOrdemServicoNR1_7", "", "Ordens de Serviço NR 01 item 1.7")]
    public class OrdemServicoNR1_7 : Ilitera.Opsa.Data.Documento
    {
        private int _IdOrdemServicoNR1_7;
        private LaudoTecnico _IdLaudoTecnico;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public OrdemServicoNR1_7()
        {

        }

        public override int Id
        {
            get { return _IdOrdemServicoNR1_7; }
            set { _IdOrdemServicoNR1_7 = value; }
        }

        public LaudoTecnico IdLaudoTecnico
        {
            get { return _IdLaudoTecnico; }
            set { _IdLaudoTecnico = value; }
        }

        public override void Validate()
        {
            this.IdDocumentoBase.Id = (int)Documentos.OrdemServicoNr1_7;

            if (this.Id == 0 && this.IdPedido.Id == 0)
                throw new Exception("Pedido é campo obrigatório!");

            base.Validate();
        }

        public static OrdemServicoNR1_7 GetOrdemServicoNR1_7(Pedido pedido)
        {
            OrdemServicoNR1_7 os = new OrdemServicoNR1_7();
            os.Find("IdPedido=" + pedido.Id);

            if (os.Id == 0)
            {
                os.Inicialize();
                os.IdPedido = pedido;
                os.IdCliente = pedido.IdCliente;
                os.IdLaudoTecnico = LaudoTecnico.GetUltimoLaudo(pedido.IdCliente.Id);
                os.IdDocumentoBase.Id = (int)Documentos.OrdemServicoNr1_7;
                os.DataLevantamento = pedido.DataSolicitacao;
            }
            return os;
        }
    }
}
