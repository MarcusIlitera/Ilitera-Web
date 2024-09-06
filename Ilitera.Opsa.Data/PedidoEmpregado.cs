using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "PedidoEmpregado", "IdPedidoEmpregado")]
    public class PedidoEmpregado : Ilitera.Data.Table
    {
        private int _IdPedidoEmpregado;
        private Pedido _IdPedido;
        private Empregado _IdEmpregado;
        private DateTime _DataVencimento;
        private string _Observacao = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PedidoEmpregado()
        {

        }
        public override int Id
        {
            get { return _IdPedidoEmpregado; }
            set { _IdPedidoEmpregado = value; }
        }
        public Pedido IdPedido
        {
            get { return _IdPedido; }
            set { _IdPedido = value; }
        }
        public Empregado IdEmpregado
        {
            get { return _IdEmpregado; }
            set { _IdEmpregado = value; }
        }
        public DateTime DataVencimento
        {
            get { return _DataVencimento; }
            set { _DataVencimento = value; }
        }
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }

        public static void AvisoSugestaoPedidoEmpregado(Pedido pedido, List<Prestador> prestadores, Usuario usuario)
        {

        }

        public static string GetBodyAvisoSugestaoPedidoEmpregado(Pedido pedido)
        {
            return string.Empty;
        }

        private static string GetListaPedidoEmpregado(Pedido pedido)
        {
            ArrayList listPedidoEmpregado = new PedidoEmpregado().Find("IdPedido=" + pedido.Id
                                + "ORDER BY DataVencimento,"
                                + " (SELECT tNO_EMPG FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPREGADO = IdEmpregado)");

            if (listPedidoEmpregado.Count == 0)
                throw new Exception("Não há empregados associados ao pedido!");

            string sLine = @"<tr>"
                            + @"<td align=""center"" width=""102""><strong style=""font-weight: 400"">"
                            + @" <font size=""1"" color=""#003300"">DataVencimento</font></strong></td>"
                            + @"<td align=""center"" width=""348"">"
                            + @"<font color=""#003300"" size=""1"" face=""Verdana, Arial, Helvetica, sans-serif"">"
                            + @"<strong style=""font-weight: 400"">NomeEmpregado</strong></font></td>"
                            + @"</tr>";

            StringBuilder sbLine = new StringBuilder();

            foreach (PedidoEmpregado pedidoEmpregado in listPedidoEmpregado)
            {
                if (pedidoEmpregado.IdEmpregado.mirrorOld == null)
                    pedidoEmpregado.IdEmpregado.Find();

                sbLine.Append(sLine);
                sbLine.Replace("NomeEmpregado", pedidoEmpregado.Observacao != string.Empty ? pedidoEmpregado.IdEmpregado.tNO_EMPG + " (" + pedidoEmpregado.Observacao + ")" : pedidoEmpregado.IdEmpregado.tNO_EMPG);
                sbLine.Replace("DataVencimento", pedidoEmpregado.DataVencimento == new DateTime() ? "-" : pedidoEmpregado.DataVencimento.ToString("dd-MM-yyyy"));
            }
            return sbLine.ToString();
        }
    }
}
