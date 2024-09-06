using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "SaidaDocumento", "IdSaidaDocumento")]
    public class SaidaDocumento : Ilitera.Data.Table
    {
        #region enum TipoSaidaDocumento

        public enum TipoSaidaDocumento : int
        {
            Motorista,
            Fax,
            Email,
            RetiradoCliente,
            Correio,
            CorreioSedex,
        }
        #endregion

        #region properties

        private int _IdSaidaDocumento;
        private Cliente _IdCliente;
        private Obrigacao _IdObrigacao;
        private Pedido _IdPedido;
        private bool _IsClinicoSaidaDocumento;
        private int _Numero;
        private TipoSaidaDocumento _IndTipoSaidaDocumento = TipoSaidaDocumento.Motorista;
        private DateTime _DataSaida;
        private DateTime _DataEntrega;
        private string _Observacao = string.Empty;
        private Transporte _IdTransporte;
        private bool _VistoMotorista;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public SaidaDocumento()
        {

        }
        public override int Id
        {
            get { return _IdSaidaDocumento; }
            set { _IdSaidaDocumento = value; }
        }
        [Obrigatorio(true, "Cliente é campo obrigatório!")]
        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }
        public Obrigacao IdObrigacao
        {
            get { return _IdObrigacao; }
            set { _IdObrigacao = value; }
        }
        public Pedido IdPedido
        {
            get { return _IdPedido; }
            set { _IdPedido = value; }
        }
        public bool IsClinicoSaidaDocumento
        {
            get { return _IsClinicoSaidaDocumento; }
            set { _IsClinicoSaidaDocumento = value; }
        }
        [Obrigatorio(true, "Número é campo obrigatório!")]
        public int Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
        public TipoSaidaDocumento IndTipoSaidaDocumento
        {
            get { return _IndTipoSaidaDocumento; }
            set { _IndTipoSaidaDocumento = value; }
        }
        [Obrigatorio(true, "Data de Saída é campo obrigatório!")]
        public DateTime DataSaida
        {
            get { return _DataSaida; }
            set { _DataSaida = value; }
        }
        public DateTime DataEntrega
        {
            get { return _DataEntrega; }
            set { _DataEntrega = value; }
        }
        public Transporte IdTransporte
        {
            get { return _IdTransporte; }
            set { _IdTransporte = value; }
        }
        public bool VistoMotorista
        {
            get { return _VistoMotorista; }
            set { _VistoMotorista = value; }
        }
        public override string ToString()
        {
            if (this.IdObrigacao != null && this.IdObrigacao.Id != 0)
            {
                if (this.IdObrigacao.mirrorOld == null)
                    this.IdObrigacao.Find();

                return this.IdObrigacao.NomeReduzido;
            }
            else
                return this.Observacao;
        }
        #endregion

        #region Metodos

        #region GetDescricaoDocumento

        public string GetDescricaoDocumento()
        {
            if (this.IdObrigacao != null && this.IdObrigacao.Id != 0)
            {
                if (this.IdObrigacao.mirrorOld == null)
                    this.IdObrigacao.Find();

                if (this.Observacao != string.Empty)
                    return this.IdObrigacao.Nome + " (" + this.Observacao + ")";
                else
                    return this.IdObrigacao.Nome;
            }
            else
                return this.Observacao;
        }
        #endregion

        #region GetTipoSaidaDocumento

        public string GetTipoSaidaDocumento()
        {
            string ret;

            if (this.IndTipoSaidaDocumento == TipoSaidaDocumento.Motorista)
            {
                if (this.IdTransporte.Id == 0)
                    ret = "Motorista";
                else
                    ret = "Motorista " + this.IdTransporte.ToString();
            }
            else if (this.IndTipoSaidaDocumento == TipoSaidaDocumento.Fax)
                ret = "Fax";
            else if (this.IndTipoSaidaDocumento == TipoSaidaDocumento.Email)
                ret = "Email";
            else if (this.IndTipoSaidaDocumento == TipoSaidaDocumento.Correio)
                ret = "Correio";
            else if (this.IndTipoSaidaDocumento == TipoSaidaDocumento.CorreioSedex)
                ret = "Correio/Sedex";
            else if (this.IndTipoSaidaDocumento == TipoSaidaDocumento.RetiradoCliente)
                ret = "Retirado pelo Cliente";
            else
                ret = string.Empty;

            return ret;
        }
        #endregion

        #region GetNovoNumero

        public static int GetNovoNumero()
        {
            return new Random().Next(int.MinValue, int.MaxValue);
        }
        #endregion

        #region GetListDocumentosSaida

        public List<SaidaDocumento> GetListDocumentosSaida()
        {
            string where = "Numero=" + this.Numero
                        + " AND IdCliente=" + this.IdCliente.Id;

            return new SaidaDocumento().Find<SaidaDocumento>(where);
        }
        #endregion

        #region GetExames

        public List<ExameBase> GetExames()
        {
            if (!this.IsClinicoSaidaDocumento)
                throw new Exception("Disponível somente para saída de exames!");

            List<ExameBase> exames = new ExameBase().Find<ExameBase>("IdExameBase IN "
                                + "(SELECT IdExameBase FROM ExameBaseSaidaDocumento WHERE"
                                + " IdSaidaDocumento=" + this.Id + ")"
                                + " ORDER BY Convert(Datetime, Convert(varchar, DataExame, 103), 103),"
                                + " (SELECT Empregado FROM qryExames WHERE Id = ExameBase.IdExameBase)");
            return exames;
        }

        #endregion

        #region GetBodyAvisoSaidaDocumento

        public string GetBodyAvisoSaidaDocumento()
        {
            List<SaidaDocumento> saidas = GetListDocumentosSaida();

            string body = string.Empty;

            StringBuilder strDocs = new StringBuilder();

            if (saidas.Count == 1 && saidas[0].IsClinicoSaidaDocumento)
            {
                strDocs.Append(saidas[0].GetDescricaoDocumento() + "<br/>");

                strDocs.Append("<br/>");

                List<ExameBase> exames = saidas[0].GetExames();

                foreach (ExameBase exame in exames)
                    strDocs.Append(exame.GetDataEmpregadoTipoExame() + "<br/>");
            }
            else
            {
                foreach (SaidaDocumento saidaDoc in saidas)
                    strDocs.Append(saidaDoc.GetDescricaoDocumento() + "<br/>");
            }

            StringBuilder strBody = new StringBuilder();
            strBody.Append(body);
            strBody.Replace("DATA_SAIDA", this.DataSaida.ToString("dd-MM-yyyy"));
            strBody.Replace("MOTORISTA", this.GetTipoSaidaDocumento());
            strBody.Replace("SAIDA_DOCUMENTO", strDocs.ToString());

            return strBody.ToString();
        }
        #endregion

        #region GetSaidaDocumentoMotorista

        public DataSet GetSaidaDocumentoMotorista(string dateDe, string dateAte, int IdTransporte)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("Nome_Motorista", Type.GetType("System.String"));
            table.Columns.Add("Nome_Empresa", Type.GetType("System.String"));
            table.Columns.Add("Nome_Obrigacao", Type.GetType("System.String"));
            ds.Tables.Add(table);

            DataRow newRow;

            ArrayList list = new SaidaDocumento().Find("IdTransporte=" + IdTransporte 
                + " AND DataSaida between '" + dateDe + "' AND '" + dateAte + "'");

            for (int i = 0; i < list.Count; i++)
            {
                if (((SaidaDocumento)list[i]).IdCliente.mirrorOld == null)
                    ((SaidaDocumento)list[i]).IdCliente.Find();

                newRow = ds.Tables[0].NewRow();

                newRow["Nome_Motorista"] = ((SaidaDocumento)list[i]).IdTransporte.ToString();
                newRow["Nome_Empresa"] = ((SaidaDocumento)list[i]).IdCliente.NomeAbreviado;
                newRow["Nome_Obrigacao"] = ((SaidaDocumento)list[i]).ToString();

                ds.Tables[0].Rows.Add(newRow);
            }
            return ds;
        }
        #endregion

        #endregion
    }
}
