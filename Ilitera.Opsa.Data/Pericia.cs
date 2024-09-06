using System;
using System.Collections.Generic;
using System.Text;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "Pericia", "IdPericia", "", "Perícias")]
    public class Pericia : Ilitera.Opsa.Data.Documento
    {
        private int _IdPericia;
        private bool _IsInsalubridade;
        private bool _IsMedica;
        private Periculosidade _IndPericulosidade;
        private string _AutorOuReclamante = string.Empty;
        private string _Processo = string.Empty;
        private Vara _IdVara;
        private bool _IsLocalEmpresa = true;
        private string _LocalDaPericia = string.Empty;
        private DateTime _DataHoraPericia;
        private Perito _IdPerito;
        private Prestador _IdAssTecnicoSubstituto;
        private Advogado _IdAdvogadoReclamada;
        private bool _IsCancelarAviso;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Pericia()
        {

        }

        public override int Id
        {
            get { return _IdPericia; }
            set { _IdPericia = value; }
        }

        public DateTime DataHoraPericia
        {
            get { return this._DataHoraPericia; }
            set { this._DataHoraPericia = value; }
        }

        [Persist(false)]
        public Prestador IdAssistenteTecnicoIndicado
        {
            get{ return this.IdPrestador;}
            set { this.IdPrestador = value; }
        }

        public bool IsInsalubridade
        {
            get { return _IsInsalubridade; }
            set { _IsInsalubridade = value; }
        }

        public bool IsMedica
        {
            get { return _IsMedica; }
            set { _IsMedica = value; }
        }

        public Periculosidade IndPericulosidade
        {
            get { return _IndPericulosidade; }
            set { _IndPericulosidade = value; }
        }

        public string AutorOuReclamante
        {
            get { return _AutorOuReclamante; }
            set { _AutorOuReclamante = value; }
        }

        public Advogado IdAdvogadoReclamada
        {
            get { return _IdAdvogadoReclamada; }
            set { _IdAdvogadoReclamada = value; }
        }

        public string Processo
        {
            get { return _Processo; }
            set { _Processo = value; }
        }

        public Vara IdVara
        {
            get { return _IdVara; }
            set { _IdVara = value; }
        }

        public bool IsLocalEmpresa
        {
            get { return _IsLocalEmpresa; }
            set { _IsLocalEmpresa = value; }
        }

        public string LocalDaPericia
        {
            get { return _LocalDaPericia; }
            set { _LocalDaPericia = value; }
        }

        public Perito IdPerito
        {
            get { return _IdPerito; }
            set { _IdPerito = value; }
        }

        public Prestador IdAssTecnicoSubstituto
        {
            get { return _IdAssTecnicoSubstituto; }
            set { _IdAssTecnicoSubstituto = value; }
        }

        public bool IsCancelarAviso
        {
            get { return _IsCancelarAviso; }
            set { _IsCancelarAviso = value; }
        }

        public override string ToString()
        {
            return    "Perícia " + this.GetTipoPericia() + "\r\n"
                    + "Data hora: " + this.DataHoraPericia.ToString("ddd, dd-MMM-yyyy HH:mm") + "\r\n"
                    + "Ass.Téc.Indicado: " + this.IdAssistenteTecnicoIndicado.ToString() + "\r\n"
                    + "Perito: " + this.IdPerito.ToString() + "\r\n"
                    + "\r\n"
                    + "Reclamada: " + this.IdCliente.ToString() + "\r\n"
                    + "Reclamante: " + this.AutorOuReclamante.ToString() + "\r\n"
                    + "Vara: " + this.IdVara.ToString() + "\r\n"
                    + "Processo: " + this.Processo.ToString() + "\r\n"
                    + "\r\n"
                    + "Local:" + this.GetLocalPericia();
        }

        public override void Validate()
        {
            this.IdDocumentoBase.Id = (int)Documentos.Pericia;

            if (this.IdPedido.Id == 0)
                throw new Exception("Pedido é campo obrigatório!");

            base.Validate();
        }

        public enum Periculosidade : int
        {
            NA,
            EnergiaEletrica,
            Inflamaveis
        }

        public void AddQuesito(TextoPadrao textoPadrao, int numOrdem)
        {
            if (textoPadrao.mirrorOld == null)
                textoPadrao.Find();

            Quesito quesito = new Quesito();
            quesito.Inicialize();
            quesito.IdPericia = this;
            quesito.Texto = textoPadrao.Texto;
            quesito.Ordem = numOrdem;
            quesito.Save();
        }

        public string GetLocalPericia()
        {
            StringBuilder str = new StringBuilder();

            if (this.IsLocalEmpresa)
                str.Append(this.IdCliente.GetEndereco().GetEnderecoCompletoPorLinhaSemCep());
            else
                str.Append(this.LocalDaPericia);

            return str.ToString();

        }

        public static Pericia GetPericia(Pedido pedido)
        {
            Pericia pericia = new Pericia();
            pericia.Find("IdPedido=" + pedido.Id);

            if (pericia.Id == 0)
            {
                pericia.Inicialize();
                pericia.IdPedido = pedido;
                pericia.IdCliente = pedido.IdCliente;
                pericia.DataLevantamento = pedido.DataSolicitacao;
                pericia.IdPrestador = pedido.IdPrestador;
                pericia.IdDocumentoBase.Id = (int)Documentos.Pericia;
            }

            return pericia;
        }

        public string GetTipoPericia()
        {
            return GetTipoPericia(this.IsInsalubridade,
                                  this.IsMedica,
                                  this.IndPericulosidade,
                                  ", ");

        }

        public static string GetTipoPericia(bool IsInsalubridade, bool IsMedica, int IndPericulosidade, string concat)
        {
            return GetTipoPericia(IsInsalubridade, 
                                  IsMedica, 
                                  (Periculosidade)Enum.ToObject(typeof(Periculosidade), IndPericulosidade), 
                                  concat);

        }

        public static string GetTipoPericia(bool IsInsalubridade, bool IsMedica, Periculosidade IndPericulosidade, string concat)
        {
            StringBuilder ret = new StringBuilder();

            if (IsInsalubridade)
                ret.Append("Insalubridade" + concat);

            if(IsMedica)
                ret.Append("Médica" + concat);

            if(IndPericulosidade == Periculosidade.EnergiaEletrica)
                ret.Append("Periculosidade - E" + concat);
            else if(IndPericulosidade == Periculosidade.Inflamaveis)
                ret.Append("Periculosidade - I" + concat);

            if (ret.Length > 0)
                ret.Remove(ret.ToString().Length - concat.Length, concat.Length);

            return ret.ToString();
        }
    }

    [Database("opsa", "Vara", "IdVara")]
    public class Vara : Ilitera.Data.Table
    {
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Vara()
        {

        }
        private int _IdVara;
        private string _Descricao = string.Empty;

        public override int Id
        {
            get { return _IdVara; }
            set { _IdVara = value; }
        }
        [Obrigatorio(true, "Nome é campo obrigatório!")]
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return Descricao;
        }
    }


    [Database("opsa", "Quesito", "IdQuesito")]
    public class Quesito : Ilitera.Data.Table
    {
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Quesito()
        {

        }
        private int _IdQuesito;
        private Pericia _IdPericia;
        private string _Texto = string.Empty;
        private int _Ordem;

        public override int Id
        {
            get { return _IdQuesito; }
            set { _IdQuesito = value; }
        }
        [Obrigatorio(true, "Perícia é campo obrigatório!")]
        public Pericia IdPericia
        {
            get { return _IdPericia; }
            set { _IdPericia = value; }
        }
        public string Texto
        {
            get { return _Texto; }
            set { _Texto = value; }
        }
        public int Ordem
        {
            get { return _Ordem; }
            set { _Ordem = value; }
        }
        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return Texto;
        }
    }
}
