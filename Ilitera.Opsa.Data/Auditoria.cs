using System;
using System.Collections;
using System.Collections.Generic;
using Ilitera.Data;
using Ilitera.Common;
using System.Data;
using System.Text;
using System.Xml;
using System.IO;

namespace Ilitera.Opsa.Data
{
    #region enum IndIrregularidade

    public enum IndIrregularidade : int
    {
        Todas,
        Regularizadas,
        NaoRegularizadas
    }
    #endregion

    #region class Auditoria

    [Database("opsa", "Auditoria", "IdAuditoria", "", "Auditoria de Segurança")]
    public class Auditoria : Documento, IFoto
    {
        #region Propriedades

        private int _IdAuditoria;
        private string _FotoInicio = string.Empty;
        private string _FotoTermino = string.Empty;
        private string _FotoExtensao = string.Empty;
        private int _FotoNumCasas;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Auditoria()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Auditoria(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdAuditoria; }
            set { _IdAuditoria = value; }
        }
        public string FotoInicio
        {
            get { return _FotoInicio; }
            set { _FotoInicio = value; }
        }
        public string FotoTermino
        {
            get { return _FotoTermino; }
            set { _FotoTermino = value; }
        }
        public string FotoExtensao
        {
            get { return _FotoExtensao; }
            set { _FotoExtensao = value; }
        }
        public int FotoNumCasas
        {
            get { return _FotoNumCasas; }
            set { _FotoNumCasas = value; }
        }
        [Persist(false)]
        public string FotoDiretorio
        {
            get
            {
                return Path.Combine(Path.Combine(this.IdCliente.GetFotoDiretorioPadrao(), 
                                                "Auditoria"), this.DataLevantamento.Year.ToString());
            }
        }
        [Persist(false)]
        public byte FotoTamanho
        {
            get { return Convert.ToByte(_FotoNumCasas); }
            set {_FotoNumCasas = Convert.ToInt32(value); }
        }
        public override string ToString()
        {
            if (this.IdCliente.mirrorOld == null)
                this.IdCliente.Find();

            return this.IdCliente.NomeAbreviado + " (" + this.DataLevantamento.ToString("dd-MM-yyyy") + ")";
        }
        #endregion

        #region Metodos

        #region GetAuditoria

        public static Auditoria GetAuditoria(Pedido pedido)
        {
            Auditoria auditoria = new Auditoria();

            ArrayList list = auditoria.Find("IdPedido=" + pedido.Id);

            if (list.Count > 1)
                throw new Exception("Este pedido pertence a mais de uma auditoria!");
            else if (auditoria.Id == 0 && pedido.DataConclusao != new DateTime())
                throw new Exception("Auditoria sem Pedido!");

            if (auditoria.Id == 0)
            {
                auditoria = new Ilitera.Opsa.Data.Auditoria();
                auditoria.Inicialize();
                auditoria.IdDocumentoBase.Id = (int)Documentos.Auditoria;
                auditoria.IdCliente.Id = pedido.IdCliente.Id;
                auditoria.IdPedido.Id = pedido.Id;
                auditoria.IdPrestador.Id = pedido.IdPrestador.Id;
                auditoria.DataLevantamento = pedido.DataSolicitacao;
                auditoria.Save();
            }

            return auditoria;
        }
        #endregion

        #region PopulaXMLFilesToFlash

        public void PopulaXMLFilesToFlash(string FlashRootPath)
        {
            string XmlClienteSufixoFileName = "clientInfo.xml";
            string XmlIrregularidadeSufixoFileName = "Irregularidades.xml";
            string XmlFotoSufixoFileName = "FotoIrreg.xml";

            StringBuilder XmlClienteFileName = new StringBuilder();
            StringBuilder XmlIrregularidadeFileName = new StringBuilder();
            StringBuilder XmlFotoFileName = new StringBuilder();

            XmlClienteFileName.Append(@"XML\");
            XmlIrregularidadeFileName.Append(@"XML\");
            XmlFotoFileName.Append(@"XML\");

            if (IsWeb)
            {
                XmlClienteFileName.Append(this.IdCliente.Id);
                XmlIrregularidadeFileName.Append(this.IdCliente.Id);
                XmlFotoFileName.Append(this.IdCliente.Id);
            }

            XmlClienteFileName.Append(XmlClienteSufixoFileName);
            XmlIrregularidadeFileName.Append(XmlIrregularidadeSufixoFileName);
            XmlFotoFileName.Append(XmlFotoSufixoFileName);

            this.IdCliente.Find();
            Endereco endereco = this.IdCliente.GetEndereco();

            XmlTextWriter writerCliente = new XmlTextWriter(Path.Combine(FlashRootPath, XmlClienteFileName.ToString()), Encoding.GetEncoding("iso-8859-1"));
            writerCliente.WriteStartDocument();
            writerCliente.WriteStartElement("clientInfo");

            writerCliente.WriteElementString("NomeCompleto", this.IdCliente.NomeCompleto);
            writerCliente.WriteElementString("Endereco", endereco.GetEndereco());
            writerCliente.WriteElementString("Complemento", endereco.GetCidadeEstado() + " - " + endereco.Cep);
            writerCliente.WriteElementString("DataAuditoria", this.DataLevantamento.ToString("dd-MM-yyyy"));
            writerCliente.WriteElementString("NormasInfrigidas", this.GetNormasInfrigidas());
            writerCliente.WriteElementString("LocaisAutuados", this.GetLocaisAutuados());

            writerCliente.WriteEndElement();
            writerCliente.WriteEndDocument();
            writerCliente.Close();

            ArrayList alIrregularidade = new Irregularidade().FindIrregularidades(this.Id.ToString());

            StringBuilder imageUrl = new StringBuilder();

            XmlTextWriter writerIrregularidade = new XmlTextWriter(Path.Combine(FlashRootPath, XmlIrregularidadeFileName.ToString()), Encoding.GetEncoding("iso-8859-1"));
            XmlTextWriter writerFotoIrreg = new XmlTextWriter(Path.Combine(FlashRootPath, XmlFotoFileName.ToString()), Encoding.GetEncoding("iso-8859-1"));

            writerIrregularidade.WriteStartDocument();
            writerIrregularidade.WriteStartElement("Irregularidades");

            writerFotoIrreg.WriteStartDocument();
            writerFotoIrreg.WriteStartElement("FotosIrregularidades");

            foreach (Irregularidade irregularidade in alIrregularidade)
            {
                irregularidade.IdNorma.Find();
                irregularidade.IdRespAdm.Find();

                Infracao infracao = new Infracao(irregularidade.GetIdInfracaoValorMulta(this.IdCliente.GetEmpregadosAtivos(),
                    irregularidade.IdNorma.CodigoInfracao));

                writerIrregularidade.WriteStartElement("Irregularidade");

                writerIrregularidade.WriteElementString("IdIrregularidade", irregularidade.Id.ToString());
                writerIrregularidade.WriteElementString("ResponsabilidadeLegal", irregularidade.strResponsabilidadeLegal());
                writerIrregularidade.WriteElementString("NaoConformidadeLegal", irregularidade.IdNorma.NaoConformidadeLegal.Trim());
                writerIrregularidade.WriteElementString("EnquadramentoLegal", irregularidade.IdNorma.GetEnquadramentoLegal());
                writerIrregularidade.WriteElementString("AcoesExecutar", irregularidade.strAcoesExecutar());
                writerIrregularidade.WriteElementString("DataCorrecao", !irregularidade.DataFinalRegul.Equals(new DateTime()) ? irregularidade.DataFinalRegul.ToString("dd-MM-yyyy") : string.Empty);
                writerIrregularidade.WriteElementString("ResponsavelCorrecao", irregularidade.IdRespAdm.NomeCompleto);
                writerIrregularidade.WriteElementString("LocalIrregularidade", irregularidade.strLocalIrregularidade());
                writerIrregularidade.WriteElementString("ParacerJuridico", irregularidade.IdNorma.ParecerJuridico.Trim());
                writerIrregularidade.WriteElementString("UFIRMinimo", infracao.ValorMin.ToString("n"));
                writerIrregularidade.WriteElementString("UFIRMaximo", infracao.ValorMax.ToString("n"));
                writerIrregularidade.WriteElementString("ReaisMinimo", ((float)(Convert.ToSingle(infracao.ValorMin) * ValoresUfir.GetValorUfir())).ToString("n"));
                writerIrregularidade.WriteElementString("ReaisMaximo", ((float)(Convert.ToSingle(infracao.ValorMax) * ValoresUfir.GetValorUfir())).ToString("n"));

                writerIrregularidade.WriteEndElement();

                DataSet dsFotos = new IrregularidadeFotoLocal().Get("IdIrregularidade=" + irregularidade.Id
                    + " ORDER BY (SELECT NomeLocal FROM LocalIrregularidade WHERE LocalIrregularidade.IdLocalIrregularidade=IrregularidadeFotoLocal.IdLocalIrregularidade)");

                foreach (DataRow rowFoto in dsFotos.Tables[0].Select())
                {
                    imageUrl.Append(Fotos.PathFoto_Uri(this, (rowFoto["NumeroFoto"].ToString() != string.Empty) ? Convert.ToInt32(rowFoto["NumeroFoto"]) : 0));
                    imageUrl.Replace(EnvironmentUtitity.GetShareFolder(EnvironmentUtitity.ShareFolders.DocsDigitais), "DocsDigitais");
                    imageUrl.Replace(@"\", "/");

                    writerFotoIrreg.WriteStartElement("FotoIrregularidade");

                    writerFotoIrreg.WriteElementString("IdIrregularidade", irregularidade.Id.ToString());
                    writerFotoIrreg.WriteElementString("PathFoto", imageUrl.ToString());
                    writerFotoIrreg.WriteElementString("IsPadrao", rowFoto["IsFotoPadrao"].ToString());

                    writerFotoIrreg.WriteEndElement();

                    imageUrl.Remove(0, imageUrl.Length);
                }
            }

            writerFotoIrreg.WriteEndElement();
            writerFotoIrreg.WriteEndDocument();
            writerFotoIrreg.Close();

            writerIrregularidade.WriteEndElement();
            writerIrregularidade.WriteEndDocument();
            writerIrregularidade.Close();
        }
        #endregion

        #region GetNormasInfrigidas

        public string GetNormasInfrigidas()
        {
            StringBuilder normas = new StringBuilder();

            DataSet dsNormas = new Nr().Get("IdNr IN (SELECT IdNr FROM Norma WHERE IdNorma IN (SELECT IdNorma FROM Irregularidade WHERE"
                + " IdAuditoria=" + this.Id + ")) ORDER BY Numero");

            foreach (DataRow rowNorma in dsNormas.Tables[0].Select())
            {
                normas.Append("NR ");
                normas.Append(rowNorma["Numero"].ToString());
                normas.Append(" - ");
                normas.Append(rowNorma["Descricao"]);
                normas.Append("\n");
            }

            return normas.ToString();
        }
        #endregion

        #region GetLocaisAutuados

        public string GetLocaisAutuados()
        {
            StringBuilder locais = new StringBuilder();

            DataSet dsLocais = new LocalIrregularidade().Get("IdLocalIrregularidade IN (SELECT IdLocalIrregularidade FROM IrregularidadeFotoLocal WHERE"
                + " IdIrregularidade IN (SELECT IdIrregularidade FROM Irregularidade WHERE IdAuditoria=" + this.Id + "))"
                + " ORDER BY NomeLocal");

            foreach (DataRow rowLocal in dsLocais.Tables[0].Select())
            {
                if (!locais.ToString().Equals(string.Empty))
                    locais.Append(", ");

                locais.Append(rowLocal["NomeLocal"]);
            }

            return locais.ToString();
        }
        #endregion

        #region ImportaAuditoria

        public void ImportaAuditoria(Auditoria auditoriaDe)
        {
            List<Irregularidade> irregularidades = new Irregularidade().Find<Irregularidade>("IdAuditoria=" + auditoriaDe.Id);

            foreach (Irregularidade irr in irregularidades)
            {
                Irregularidade newIrr = new Irregularidade();
                newIrr = (Irregularidade)irr.Clone();
                newIrr.Id = 0;
                newIrr.IdAuditoria.Id = this.Id;
                newIrr.Save();

                List<IrregularidadeAcoesExecutar>
                    acoes = new IrregularidadeAcoesExecutar().Find<IrregularidadeAcoesExecutar>("IdIrregularidade=" + irr.Id);

                foreach (IrregularidadeAcoesExecutar acao in acoes)
                {
                    IrregularidadeAcoesExecutar newAcao = new IrregularidadeAcoesExecutar();
                    newAcao = (IrregularidadeAcoesExecutar)acao.Clone();
                    newAcao.Id = 0;
                    newAcao.IdIrregularidade.Id = newIrr.Id;
                    newAcao.Save();
                }

                List<IrregularidadeFotoLocal>
                    fotos = new IrregularidadeFotoLocal().Find<IrregularidadeFotoLocal>("IdIrregularidade=" + irr.Id);

                foreach (IrregularidadeFotoLocal foto in fotos)
                {
                    IrregularidadeFotoLocal newFoto = new IrregularidadeFotoLocal();
                    newFoto = (IrregularidadeFotoLocal)foto.Clone();
                    newFoto.Id = 0;
                    newFoto.IdIrregularidade.Id = newIrr.Id;

                    foto.IdLocalIrregularidade.Find();

                    LocalIrregularidade local = new LocalIrregularidade();
                    local.Find("IdCliente=" + this.IdCliente.Id
                        + " AND NomeLocal='" + foto.IdLocalIrregularidade.NomeLocal + "'");

                    if (local.Id == 0)
                    {
                        local.Inicialize();
                        local.IdCliente.Id = this.IdCliente.Id;
                        local.NomeLocal = foto.IdLocalIrregularidade.NomeLocal;
                        local.IsGenerico = foto.IdLocalIrregularidade.IsGenerico;
                        local.Save();
                    }

                    newFoto.IdLocalIrregularidade.Id = local.Id;
                    newFoto.Save();
                }
            }
        }


        #endregion

        #endregion
    }

    #endregion

    #region class Nr

    [Database("opsa", "Nr", "IdNr", "", "NR")]
    public class Nr : Table
    {
        private int _IdNr;
        private int _Numero;
        private string _Descricao = string.Empty;
        private bool _IsInativo;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Nr()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Nr(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdNr; }
            set { _IdNr = value; }
        }
        public int Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public bool IsInativo
        {
            get { return _IsInativo; }
            set { _IsInativo = value; }
        }
        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return this.Numero.ToString("00").Substring(0, 2) + " - " + this.Descricao;
        }
    }
    #endregion

    #region class LocalIrregularidade

    [Database("opsa", "LocalIrregularidade", "IdLocalIrregularidade", "", "Local Irregularidade")]
    public class LocalIrregularidade : Table
    {
        private int _IdLocalIrregularidade;
        private Cliente _IdCliente;
        private string _NomeLocal = string.Empty;
        private bool _IsGenerico = false;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public LocalIrregularidade()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public LocalIrregularidade(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdLocalIrregularidade; }
            set { _IdLocalIrregularidade = value; }
        }
        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        [Obrigatorio(true, "O nome do Local é campo obrigatório!")]
        public string NomeLocal
        {
            get { return _NomeLocal; }
            set { _NomeLocal = value; }
        }
        public bool IsGenerico
        {
            get { return _IsGenerico; }
            set { _IsGenerico = value; }
        }
        public override int Save()
        {
            if (this.IsGenerico)
                this.IdCliente.Id = 0;

            if (!this.IsGenerico && this.IdCliente.Id == 0)
                throw new Exception("Cadastro Inválido!");

            return base.Save();
        }
    }
    #endregion

    #region class IrregularidadeFotoLocal

    [Database("opsa", "IrregularidadeFotoLocal", "IdIrregularidadeFotoLocal")]
    public class IrregularidadeFotoLocal : Table
    {
        private int _IdIrregularidadeFotoLocal;
        private Irregularidade _IdIrregularidade;
        private string _NumeroFoto;
        private LocalIrregularidade _IdLocalIrregularidade;
        private bool _IsFotoPadrao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public IrregularidadeFotoLocal()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public IrregularidadeFotoLocal(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdIrregularidadeFotoLocal; }
            set { _IdIrregularidadeFotoLocal = value; }
        }
        public Irregularidade IdIrregularidade
        {
            get { return _IdIrregularidade; }
            set { _IdIrregularidade = value; }
        }
        public string NumeroFoto
        {
            get { return _NumeroFoto; }
            set { _NumeroFoto = value; }
        }
        public LocalIrregularidade IdLocalIrregularidade
        {
            get { return _IdLocalIrregularidade; }
            set { _IdLocalIrregularidade = value; }
        }
        public bool IsFotoPadrao
        {
            get { return _IsFotoPadrao; }
            set { _IsFotoPadrao = value; }
        }
    }
    #endregion

    #region class IrregularidadeAcoesExecutar

    [Database("opsa", "IrregularidadeAcoesExecutar", "IdIrregularidadeAcoesExecutar")]
    public class IrregularidadeAcoesExecutar : Table
    {
        private int _IdIrregularidadeAcoesExecutar;
        private Irregularidade _IdIrregularidade;
        private AcoesExecutar _IdAcoesExecutar;
        private string _FrasePersonalizada = string.Empty;
        private int _Ordem;
        private bool _IsPersonalizado = false;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public IrregularidadeAcoesExecutar()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public IrregularidadeAcoesExecutar(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdIrregularidadeAcoesExecutar; }
            set { _IdIrregularidadeAcoesExecutar = value; }
        }
        public Irregularidade IdIrregularidade
        {
            get { return _IdIrregularidade; }
            set { _IdIrregularidade = value; }
        }
        public AcoesExecutar IdAcoesExecutar
        {
            get { return _IdAcoesExecutar; }
            set { _IdAcoesExecutar = value; }
        }
        public string FrasePersonalizada
        {
            get { return _FrasePersonalizada; }
            set { _FrasePersonalizada = value; }
        }
        public int Ordem
        {
            get { return _Ordem; }
            set { _Ordem = value; }
        }
        public bool IsPersonalizado
        {
            get { return _IsPersonalizado; }
            set { _IsPersonalizado = value; }
        }
    }
    #endregion

    #region class AcoesExecutar

    [Database("opsa", "AcoesExecutar", "IdAcoesExecutar", "", "Ações a Executar para uma infração na Auditoria")]
    public class AcoesExecutar : Table
    {
        private int _IdAcoesExecutar;
        private Norma _IdNorma;
        private string _FraseAbreviada;
        private string _FraseCompleta;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AcoesExecutar()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AcoesExecutar(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdAcoesExecutar; }
            set { _IdAcoesExecutar = value; }
        }
        public Norma IdNorma
        {
            get { return _IdNorma; }
            set { _IdNorma = value; }
        }
        [Obrigatorio(true, "A Frase Abreviada é obrigatória!")]
        public string FraseAbreviada
        {
            get { return _FraseAbreviada; }
            set { _FraseAbreviada = value; }
        }
        [Obrigatorio(true, "A Frase Completa é obrigatória!")]
        public string FraseCompleta
        {
            get { return _FraseCompleta; }
            set { _FraseCompleta = value; }
        }
    }
    #endregion

    #region class Irregularidade

    [Database("opsa", "Irregularidade", "IdIrregularidade", "", "Irregularidade da Auditoria")]
    public class Irregularidade : Table
    {
        private int _IdIrregularidade;
        private Auditoria _IdAuditoria;
        private Norma _IdNorma;
        //private string _NumeroFoto="0";
        private DateTime _DataInicioRegul = new DateTime();
        private DateTime _DataPrevisaoRegul = new DateTime();
        private DateTime _DataFinalRegul = new DateTime();
        private Single _Orcamento;
        private Single _CustoFinal;
        private Prestador _IdRespAdm;
        private Prestador _IdRespOpe;
        private string _ObjetivoRegul = string.Empty;
        private string _ObservacaoRegul = string.Empty;
        private TipoIrregularidade _IdTipoIrregularidade;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Irregularidade()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Irregularidade(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdIrregularidade; }
            set { _IdIrregularidade = value; }
        }
        public Auditoria IdAuditoria
        {
            get { return _IdAuditoria; }
            set { _IdAuditoria = value; }
        }
        public Norma IdNorma
        {
            get { return _IdNorma; }
            set { _IdNorma = value; }
        }
        //public string NumeroFoto
        //{
        //    get{return _NumeroFoto;}
        //    set{_NumeroFoto = value;}
        //}
        public DateTime DataInicioRegul
        {
            get { return _DataInicioRegul; }
            set { _DataInicioRegul = value; }
        }
        public DateTime DataPrevisaoRegul
        {
            get { return _DataPrevisaoRegul; }
            set { _DataPrevisaoRegul = value; }
        }
        public DateTime DataFinalRegul
        {
            get { return _DataFinalRegul; }
            set { _DataFinalRegul = value; }
        }
        public Single Orcamento
        {
            get { return _Orcamento; }
            set { _Orcamento = value; }
        }
        public Single CustoFinal
        {
            get { return _CustoFinal; }
            set { _CustoFinal = value; }
        }
        public Prestador IdRespAdm
        {
            get { return _IdRespAdm; }
            set { _IdRespAdm = value; }
        }
        public Prestador IdRespOpe
        {
            get { return _IdRespOpe; }
            set { _IdRespOpe = value; }
        }
        public string ObjetivoRegul
        {
            get { return _ObjetivoRegul; }
            set { _ObjetivoRegul = value; }
        }
        public string ObservacaoRegul
        {
            get { return _ObservacaoRegul; }
            set { _ObservacaoRegul = value; }
        }
        public TipoIrregularidade IdTipoIrregularidade
        {
            get { return _IdTipoIrregularidade; }
            set { _IdTipoIrregularidade = value; }
        }

        public ArrayList FindIrregularidades(string IdAuditoria)
        {
            return FindIrregularidades(IdAuditoria, string.Empty);
        }

        public ArrayList FindIrregularidades(string IdAuditoria, string strbusca)
        {
            return FindIrregularidades(IdAuditoria, strbusca, (int)IndIrregularidade.Todas);
        }

        public ArrayList FindIrregularidades(string IdAuditoria, string strbusca, int indIrregularidade)
        {
            StringBuilder sqlstm = new StringBuilder();

            sqlstm.Append("IdAuditoria=" + IdAuditoria);

            if (!strbusca.Equals(string.Empty))
            {
                sqlstm.Append(" AND (IdNorma IN (SELECT IdNorma FROM Norma WHERE UPPER(CodigoItem) COLLATE SQL_Latin1_General_Cp1251_CS_AS LIKE '%" + strbusca.ToUpper() + "%')");
                sqlstm.Append(" OR IdIrregularidade IN (SELECT IdIrregularidade FROM IrregularidadeFotoLocal WHERE IdLocalIrregularidade IS NOT NULL AND IdLocalIrregularidade IN (SELECT IdLocalIrregularidade FROM LocalIrregularidade WHERE UPPER(NomeLocal) COLLATE SQL_Latin1_General_Cp1251_CS_AS LIKE '%" + strbusca.ToUpper() + "%'))");
                sqlstm.Append(" OR IdIrregularidade IN (SELECT IdIrregularidade FROM IrregularidadeAcoesExecutar WHERE (IdAcoesExecutar IS NOT NULL AND IdAcoesExecutar IN (SELECT IdAcoesExecutar FROM AcoesExecutar WHERE UPPER(FraseCompleta) COLLATE SQL_Latin1_General_Cp1251_CS_AS LIKE '%" + strbusca.ToUpper() + "%'))");
                sqlstm.Append(" OR (IsPersonalizado=1 AND UPPER(FrasePersonalizada) COLLATE SQL_Latin1_General_Cp1251_CS_AS LIKE '%" + strbusca.ToUpper() + "%')))");
            }

            switch (indIrregularidade)
            {
                case (int)IndIrregularidade.Regularizadas:
                    sqlstm.Append(" AND DataFinalRegul IS NOT NULL");
                    break;
                case (int)IndIrregularidade.NaoRegularizadas:
                    sqlstm.Append(" AND DataFinalRegul IS NULL");
                    break;
            }

            //sqlstm.Append(" ORDER BY (SELECT Ordem FROM Norma WHERE Norma.IdNorma = Irregularidade.IdNorma)");
            sqlstm.Append(" ORDER BY (SELECT IdNr FROM Norma WHERE Norma.IdNorma = Irregularidade.IdNorma), ( Select NomeLocal from LocalIrregularidade where IdLocalIrregularidade in ( SELECT top 1 IdLocalIrregularidade FROM IrregularidadeFotoLocal WHERE IdIrregularidade=Irregularidade.IdIrregularidade ))");

            return new Irregularidade().Find(sqlstm.ToString());
        }

        public int GetIdInfracaoValorMulta(int QuantidadeEmpregados, int CodigoInfracao)
        {
            ArrayList list = new Infracao().Find("CodigoInfracao=" + CodigoInfracao
                + " AND FuncMin<=" + QuantidadeEmpregados
                + " AND FuncMax>=" + QuantidadeEmpregados);

            int IdInfracao = 0;

            if (list.Count > 0)
                IdInfracao = ((Infracao)list[0]).Id;

            return IdInfracao;
        }

        public string strLocalIrregularidade()
        {
            StringBuilder sqlstm = new StringBuilder();
            StringBuilder strLocais = new StringBuilder();

            sqlstm.Append("IdLocalIrregularidade IN ");
            sqlstm.Append("(SELECT IdLocalIrregularidade FROM IrregularidadeFotoLocal WHERE IdIrregularidade=");
            sqlstm.Append(this.Id);
            sqlstm.Append(") ORDER BY NomeLocal");

            DataSet dsLocaisIrregularidade = new LocalIrregularidade().Get(sqlstm.ToString());

            foreach (DataRow rowLocal in dsLocaisIrregularidade.Tables[0].Select())
            {
                if (strLocais.ToString().Equals(string.Empty))
                    strLocais.Append(rowLocal["NomeLocal"]);
                else
                {
                    strLocais.Append(", ");
                    strLocais.Append(rowLocal["NomeLocal"]);
                }
            }

            return strLocais.ToString();
        }

        public string strAcoesExecutar()
        {
            return strAcoesExecutar("\n");
        }

        public string strAcoesExecutar(string breakLine)
        {
            StringBuilder sqlstm = new StringBuilder();
            StringBuilder strAcoesExecutar = new StringBuilder();

            sqlstm.Append("IdIrregularidade=");
            sqlstm.Append(this.Id);
            sqlstm.Append(" ORDER BY Ordem");

            ArrayList alIrregularidadeAcoesExecutar = new IrregularidadeAcoesExecutar().Find(sqlstm.ToString());

            foreach (IrregularidadeAcoesExecutar acoesExecutar in alIrregularidadeAcoesExecutar)
            {
                strAcoesExecutar.Append((acoesExecutar.Ordem + 1).ToString("00"));
                strAcoesExecutar.Append(" - ");

                if (acoesExecutar.IsPersonalizado)
                    strAcoesExecutar.Append(acoesExecutar.FrasePersonalizada);
                else
                {
                    acoesExecutar.IdAcoesExecutar.Find();
                    strAcoesExecutar.Append(acoesExecutar.IdAcoesExecutar.FraseCompleta);
                }

                strAcoesExecutar.Append(breakLine);
            }

            return strAcoesExecutar.ToString();
        }

        public string strResponsabilidadeLegal()
        {
            StringBuilder strResponsabilidadeLegal = new StringBuilder();

            Norma norma = new Norma(this.IdNorma.Id);

            if (!norma.RespCivil.Trim().Equals(string.Empty))
            {
                strResponsabilidadeLegal.Append("Responsabilidade Civil\n");
                strResponsabilidadeLegal.Append(norma.RespCivil.Trim());
                strResponsabilidadeLegal.Append("\n\n");
            }
            if (!norma.RespPenal.Trim().Equals(string.Empty))
            {
                strResponsabilidadeLegal.Append("Responsabilidade Penal\n");
                strResponsabilidadeLegal.Append(norma.RespPenal.Trim());
                strResponsabilidadeLegal.Append("\n\n");
            }
            if (!norma.RespTrabalhista.Trim().Equals(string.Empty))
            {
                strResponsabilidadeLegal.Append("Responsabilidade Trabalhista\n");
                strResponsabilidadeLegal.Append(norma.RespTrabalhista.Trim());
                strResponsabilidadeLegal.Append("\n\n");
            }
            if (!norma.RespAmbiental.Trim().Equals(string.Empty))
            {
                strResponsabilidadeLegal.Append("Responsabilidade Ambiental\n");
                strResponsabilidadeLegal.Append(norma.RespAmbiental.Trim());
                strResponsabilidadeLegal.Append("\n\n");
            }
            if (!norma.RespPrevidenciaria.Trim().Equals(string.Empty))
            {
                strResponsabilidadeLegal.Append("Responsabilidade Previdenciária\n");
                strResponsabilidadeLegal.Append(norma.RespPrevidenciaria.Trim());
                strResponsabilidadeLegal.Append("\n\n");
            }
            if (!norma.RespNormativa.Trim().Equals(string.Empty))
            {
                strResponsabilidadeLegal.Append("Responsabilidade Normativa\n");
                strResponsabilidadeLegal.Append(norma.RespNormativa.Trim());
                strResponsabilidadeLegal.Append("\n\n");
            }

            return strResponsabilidadeLegal.ToString();
        }
    }
    #endregion

    #region class Norma

    [Database("opsa", "Norma", "IdNorma", "", "Norma")]
    public class Norma : Table
    {
        private int _IdNorma;
        private int _Ordem;
        private string _CodigoItem = string.Empty;
        private Nr _IdNr;
        private string _Titulo = string.Empty;
        private string _Historico = string.Empty;
        private string _Lei = string.Empty;
        private string _Portaria = string.Empty;
        private int _CodigoInfracao;
        private int _Codigo;
        private string _CaminhoDesenho = string.Empty;
        private string _CaminhoFoto = string.Empty;
        private string _Pergunta = string.Empty;
        private string _PalavrasChaves = string.Empty;

        private string _NaoConformidadeLegal = string.Empty;
        private string _RespCivil = string.Empty;
        private string _RespPenal = string.Empty;
        private string _RespTrabalhista = string.Empty;
        private string _RespAmbiental = string.Empty;
        private string _RespPrevidenciaria = string.Empty;
        private string _RespNormativa = string.Empty;

        private bool _IsAuditavel;
        private bool _hasOutrasProvidencias;
        private bool _EnviarRelatorio;

        private string _Manchete = string.Empty;
        private string _CaputResumo= string.Empty;
        private string _AlineaResumo = string.Empty;
        private string _DanosRelacionados = string.Empty;
        private string _CorrecoesExecutar = string.Empty;
        private string _FundamentacaoLegal = string.Empty;
        private string _ParecerJuridico = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Norma()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Norma(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdNorma; }
            set { _IdNorma = value; }
        }
        public int Ordem
        {
            get { return _Ordem; }
            set { _Ordem = value; }
        }
        public string CodigoItem
        {
            get { return _CodigoItem; }
            set { _CodigoItem = value; }
        }
        public Nr IdNr
        {
            get { return _IdNr; }
            set { _IdNr = value; }
        }
        public string Titulo
        {
            get { return _Titulo; }
            set { _Titulo = value; }
        }
        public string Historico
        {
            get { return _Historico; }
            set { _Historico = value; }
        }
        public string Lei
        {
            get { return _Lei; }
            set { _Lei = value; }
        }
        public string Portaria
        {
            get { return _Portaria; }
            set { _Portaria = value; }
        }
        public int CodigoInfracao
        {
            get { return _CodigoInfracao; }
            set { _CodigoInfracao = value; }
        }
        public int Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }
        public string CaminhoDesenho
        {
            get { return _CaminhoDesenho; }
            set { _CaminhoDesenho = value; }
        }
        public string CaminhoFoto
        {
            get { return _CaminhoFoto; }
            set { _CaminhoFoto = value; }
        }
        public string Pergunta
        {
            get { return _Pergunta; }
            set { _Pergunta = value; }
        }
        public string PalavrasChaves
        {
            get { return _PalavrasChaves; }
            set { _PalavrasChaves = value; }
        }
        public string NaoConformidadeLegal
        {
            get { return _NaoConformidadeLegal; }
            set { _NaoConformidadeLegal = value; }
        }
        public string RespCivil
        {
            get { return _RespCivil; }
            set { _RespCivil = value; }
        }
        public string RespPenal
        {
            get { return _RespPenal; }
            set { _RespPenal = value; }
        }
        public string RespTrabalhista
        {
            get { return _RespTrabalhista; }
            set { _RespTrabalhista = value; }
        }
        public string RespAmbiental
        {
            get { return _RespAmbiental; }
            set { _RespAmbiental = value; }
        }
        public string RespPrevidenciaria
        {
            get { return _RespPrevidenciaria; }
            set { _RespPrevidenciaria = value; }
        }
        public string RespNormativa
        {
            get { return _RespNormativa; }
            set { _RespNormativa = value; }
        }
        public bool IsAuditavel
        {
            get { return _IsAuditavel; }
            set { _IsAuditavel = value; }
        }
        public bool hasOutrasProvidencias
        {
            get { return _hasOutrasProvidencias; }
            set { _hasOutrasProvidencias = value; }
        }
        public bool EnviarRelatorio
        {
            get { return _EnviarRelatorio; }
            set { _EnviarRelatorio = value; }
        }
        public string Manchete
        {
            get { return _Manchete; }
            set { _Manchete = value; }
        }
        public string CaputResumo
        {
            get { return _CaputResumo; }
            set { _CaputResumo = value; }
        }
        public string AlineaResumo
        {
            get { return _AlineaResumo; }
            set { _AlineaResumo = value; }
        }
        public string DanosRelacionados
        {
            get { return _DanosRelacionados; }
            set { _DanosRelacionados = value; }
        }
        public string CorrecoesExecutar
        {
            get { return _CorrecoesExecutar; }
            set { _CorrecoesExecutar = value; }
        }
        public string FundamentacaoLegal
        {
            get { return _FundamentacaoLegal; }
            set { _FundamentacaoLegal = value; }
        }
        public string ParecerJuridico
        {
            get { return _ParecerJuridico; }
            set { _ParecerJuridico = value; }
        }

        public Infracao GetInfracao(int QuantidadeEmpregados)
        {
            if (QuantidadeEmpregados == 0)
                QuantidadeEmpregados = 1;

            string criteria = "CodigoInfracao=" + this.CodigoInfracao
                            + " AND FuncMin<=" + QuantidadeEmpregados
                            + " AND FuncMax>=" + QuantidadeEmpregados;

            List<Infracao> list = new Infracao().Find<Infracao>(criteria);

            Infracao infracao;

            if (list.Count > 0)
                infracao = list[0];
            else
                infracao = new Infracao();

            return infracao;
        }

        public string GetEnquadramentoLegal()
        {
            StringBuilder strEnquadramentoLegal = new StringBuilder();

            if (this.FundamentacaoLegal.Trim().Equals(string.Empty))
            {
                if (!this.Titulo.Trim().Equals(string.Empty))
                    strEnquadramentoLegal.Append(this.Titulo + " ");

                strEnquadramentoLegal.Append(this.Historico);
                strEnquadramentoLegal.Append(" (Portaria MTb nº ");
                strEnquadramentoLegal.Append(this.Portaria);
                strEnquadramentoLegal.Append(" item " + this.CodigoItem);
                strEnquadramentoLegal.Append(" Infração Grau ");
                strEnquadramentoLegal.Append(this.CodigoInfracao.ToString());
                strEnquadramentoLegal.Append(")");
            }
            else
                strEnquadramentoLegal.Append(this.FundamentacaoLegal.Trim());

            return strEnquadramentoLegal.ToString();
        }
    }
    #endregion

    #region class TipoIrregularidade

    [Database("opsa", "TipoIrregularidade", "IdTipoIrregularidade", "", "Tipo da Irregularidade da Auditoria")]
    public class TipoIrregularidade : Table
    {
        private int _IdTipoIrregularidade;
        private string _Nome = string.Empty;
        private Cliente _IdCliente;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TipoIrregularidade()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TipoIrregularidade(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdTipoIrregularidade; }
            set { _IdTipoIrregularidade = value; }
        }
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }
        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
    }
    #endregion

    #region class NrAuditavel

    [Database("opsa", "NrAuditavel", "IdNrAuditavel", "", "Nr Auditável para execução da Auditoria")]
    public class NrAuditavel : Table
    {
        private int _IdNrAuditavel;
        private Cliente _IdCliente;
        private Nr _IdNr;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public NrAuditavel()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public NrAuditavel(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdNrAuditavel; }
            set { _IdNrAuditavel = value; }
        }
        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        public Nr IdNr
        {
            get { return _IdNr; }
            set { _IdNr = value; }
        }
    }
    #endregion

    #region class Infracao

    [Database("opsa", "Infracao", "IdInfracao", "", "Infração para Auditoria")]
    public class Infracao : Table
    {
        private int _IdInfracao;
        private int _CodigoInfracao;
        private int _FuncMin;
        private int _FuncMax;
        private int _ValorMin;
        private int _ValorMax;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Infracao()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Infracao(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdInfracao; }
            set { _IdInfracao = value; }
        }
        public int CodigoInfracao
        {
            get { return _CodigoInfracao; }
            set { _CodigoInfracao = value; }
        }
        public int FuncMin
        {
            get { return _FuncMin; }
            set { _FuncMin = value; }
        }
        public int FuncMax
        {
            get { return _FuncMax; }
            set { _FuncMax = value; }
        }
        public int ValorMin
        {
            get { return _ValorMin; }
            set { _ValorMin = value; }
        }
        public int ValorMax
        {
            get { return _ValorMax; }
            set { _ValorMax = value; }
        }
        public float GetValorMinReal()
        {
            return Convert.ToSingle(this._ValorMin) * ValoresUfir.GetValorUfir();
        }
        public float GetValorMaxReal()
        {
            return Convert.ToSingle(this._ValorMax) * ValoresUfir.GetValorUfir();
        }
    }
    #endregion

    #region class ValoresUfir

    [Database("opsa", "ValoresUfir", "IdValoresUfir", "", "Valores Ufir")]
    public class ValoresUfir : Table
    {
        private int _IdValoresUfir;
        private int _Ano;
        private float _Valor;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ValoresUfir()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ValoresUfir(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdValoresUfir; }
            set { _IdValoresUfir = value; }
        }
        public int Ano
        {
            get { return _Ano; }
            set { _Ano = value; }
        }
        public float Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }

        private static ValoresUfir ufir;

        public static float GetValorUfir()
        {
            if (ufir == null)
            {
                ufir = new ValoresUfir();
                //ufir.FindMax("Ano", "IdValoresUfir=IdValoresUfir");
                ufir.Find("Ano=2000");
            }
            return ufir.Valor;
        }
    }
    #endregion

    #region class AvisoAuditoria

    [Database("opsa", "AvisoAuditoria", "IdAvisoAuditoria", "", "Serviço de Avisos da Auditoria")]
    public class AvisoAuditoria : Table
    {
        private int _IdAvisoAuditoria;
        private Auditoria _IdAuditoria;
        private bool _IsAtivo;
        private int _DiasInicio;
        private int _Periodicidade;
        private string _Emails = string.Empty;
        private DateTime _DataUltimaExecucao;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AvisoAuditoria()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AvisoAuditoria(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdAvisoAuditoria; }
            set { _IdAvisoAuditoria = value; }
        }
        public Auditoria IdAuditoria
        {
            get { return _IdAuditoria; }
            set { _IdAuditoria = value; }
        }
        public bool IsAtivo
        {
            get { return _IsAtivo; }
            set { _IsAtivo = value; }
        }
        public int DiasInicio
        {
            get { return _DiasInicio; }
            set { _DiasInicio = value; }
        }
        public int Periodicidade
        {
            get { return _Periodicidade; }
            set { _Periodicidade = value; }
        }
        public string Emails
        {
            get { return _Emails; }
            set { _Emails = value; }
        }
        public DateTime DataUltimaExecucao
        {
            get { return _DataUltimaExecucao; }
            set { _DataUltimaExecucao = value; }
        }
    }       
    #endregion
}