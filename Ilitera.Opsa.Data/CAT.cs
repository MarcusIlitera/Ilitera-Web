using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    #region Enum's

    public enum TipoEmitenteCAT : int
    {
        Empregador = 1,
        Sindicato,
        Medico,
        SeguradoDependente,
        AutoridadePublica,
        Cooperativa,
        Orgao_Gestor,        
        Sindicato_Avulsos,
        Empregado
    }

    public enum TipoCAT : int
    {
        Inicial = 1,
        Reabertura,
        ComunicacaoObito
    }

    public enum TipoAcidente : int
    {
        Tipico = 1,
        Doenca,
        Trajeto
    }

    public enum TipoAfastamento : int
    {
        Ocupacional = 1,
        Assistencial
    }
    public enum TipoSetor : int
    {
        SetorNormal = 1,
        OutroSetor,
        NaoAplicavel
    }
    #endregion

    #region CAT

    [Database("opsa", "CAT", "IdCAT")]
    public class CAT : Ilitera.Data.Table
    {
        private int _IdCAT;
        private Empregado _IdEmpregado;
        private DateTime _DataEmissao;
        private string _NumeroCAT = string.Empty;
        private int _IndEmitente;
        private int _IndTipoCAT;
        private bool _hasRegPolicial;
        private string _BO = string.Empty;
        private bool _hasMorte;
        private DateTime _DataObito;
        private string _Arquivo_CAT;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CAT()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CAT(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdCAT; }
            set { _IdCAT = value; }
        }
        public Empregado IdEmpregado
        {
            get { return _IdEmpregado; }
            set { _IdEmpregado = value; }
        }
        public DateTime DataEmissao
        {
            get { return _DataEmissao; }
            set { _DataEmissao = value; }
        }
        
        public string NumeroCAT
        {
            get { return _NumeroCAT; }
            set { _NumeroCAT = value; }
        }
        public int IndEmitente
        {
            get { return _IndEmitente; }
            set { _IndEmitente = value; }
        }
        public int IndTipoCAT
        {
            get { return _IndTipoCAT; }
            set { _IndTipoCAT = value; }
        }
        public bool hasRegPolicial
        {
            get { return _hasRegPolicial; }
            set { _hasRegPolicial = value; }
        }
        public string BO
        {
            get { return _BO; }
            set { _BO = value; }
        }
        public bool hasMorte
        {
            get { return _hasMorte; }
            set { _hasMorte = value; }
        }
        public DateTime DataObito
        {
            get { return _DataObito; }
            set { _DataObito = value; }
        }
        public string Arquivo_Cat
        {
            get { return _Arquivo_CAT; }
            set { _Arquivo_CAT = value; }
        }
    }
    #endregion

    #region Acidente

    [Database("opsa", "Acidente", "IdAcidente", "", "Acidente")]
    public class Acidente : Ilitera.Data.Table
    {
        private int _IdAcidente;
        private Empregado _IdEmpregado;
        private CAT _IdCAT;
        private DateTime _DataAcidente;
        private int _IndTipoAcidente;
        private Juridica _IdJuridica;
        private LocalAcidente _IdLocalAcidente;
        private string _EspecLocal = string.Empty;
        private string _MembroAtingido = string.Empty;
        private string _AgenteCausador = string.Empty;
        private string _NaturezaLesao = string.Empty;
        private string _Descricao = string.Empty;
        private bool _hasAfastamento;
        private int _indTipoSetor;
        private Setor _IdSetor;
        private CID _IdCID;
        private bool _isTransfSetor;
        private bool _isAposInval;
        private float _PerdaMaterial;
        private string _Observacoes;
        private Int32 _IdCID2;
        private Int32 _IdCID3;
        private Int32 _IdCID4;
        private Int32 _Codigo_Parte_Corpo_Atingida;
        private Int32 _Codigo_Agente_Causador;
        private Int32 _Codigo_Descricao_Lesao;
        private Int32 _Codigo_Situacao_Geradora;
        private Int16 _IdIniciativaCat;
        private Int16 _IdTipoLocal;
        private string _Logradouro;
        private string _Nr_Logradouro;
        private string _Municipio;
        private string _UF;

        private bool _HasInternacao;
        private string _CNES;
        private DateTime _DataInternacao;
        private Int16 _DuracaoInternacao;
        private string _MedicoInternacao;
        private string _CRMInternacao;
        private string _UFInternacao;
        private string _DiagnosticoProvavel;
        private Int16 _IdLateralidade;

        private string _Codigo_Acidente_Trabalho;

        private string _codPostal;
        private string _dscLocal;

        private string _Bairro;
        private string _Complemento;
        private string _CEP;
        private string _hrsTrabAntesAcid;

        private string _CNPJ_Terceiro;
        private bool _Reabertura;
        private string _nrRecCatOrig;
        private DateTime _UltDiaTrab;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Acidente()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Acidente(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdAcidente; }
            set { _IdAcidente = value; }
        }
        public Empregado IdEmpregado
        {
            get { return _IdEmpregado; }
            set { _IdEmpregado = value; }
        }
        public CAT IdCAT
        {
            get { return _IdCAT; }
            set { _IdCAT = value; }
        }
        public DateTime DataAcidente
        {
            get { return _DataAcidente; }
            set { _DataAcidente = value; }
        }
        public int IndTipoAcidente
        {
            get { return _IndTipoAcidente; }
            set { _IndTipoAcidente = value; }
        }
        public Juridica IdJuridica
        {
            get { return _IdJuridica; }
            set { _IdJuridica = value; }
        }
        public LocalAcidente IdLocalAcidente
        {
            get { return _IdLocalAcidente; }
            set { _IdLocalAcidente = value; }
        }
        public string EspecLocal
        {
            get { return _EspecLocal; }
            set { _EspecLocal = value; }
        }
        public string MembroAtingido
        {
            get { return _MembroAtingido; }
            set { _MembroAtingido = value; }
        }
        public string AgenteCausador
        {
            get { return _AgenteCausador; }
            set { _AgenteCausador = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public string NaturezaLesao
        {
            get { return _NaturezaLesao; }
            set { _NaturezaLesao = value; }
        }
        public bool hasAfastamento
        {
            get { return _hasAfastamento; }
            set { _hasAfastamento = value; }
        }
        public int indTipoSetor
        {
            get { return _indTipoSetor; }
            set { _indTipoSetor = value; }
        }
        public Setor IdSetor
        {
            get { return _IdSetor; }
            set { _IdSetor = value; }
        }
        public CID IdCID
        {
            get { return _IdCID; }
            set { _IdCID = value; }
        }
        public bool isTransfSetor
        {
            get { return _isTransfSetor; }
            set { _isTransfSetor = value; }
        }
        public bool isAposInval
        {
            get { return _isAposInval; }
            set { _isAposInval = value; }
        }
        public float PerdaMaterial
        {
            get { return _PerdaMaterial; }
            set { _PerdaMaterial = value; }
        }
        public string Observacoes
        {
            get { return _Observacoes; }
            set { _Observacoes = value; }
        }

        public Int32 IdCID2
        {
            get { return _IdCID2; }
            set { _IdCID2 = value; }
        }
        public Int32 IdCID3
        {
            get { return _IdCID3; }
            set { _IdCID3 = value; }
        }
        public Int32 IdCID4
        {
            get { return _IdCID4; }
            set { _IdCID4 = value; }
        }

        public Int32 Codigo_Parte_Corpo_Atingida
        {
            get { return _Codigo_Parte_Corpo_Atingida; }
            set { _Codigo_Parte_Corpo_Atingida = value; }
        }

        public Int32 Codigo_Agente_Causador
        {
            get { return _Codigo_Agente_Causador; }
            set { _Codigo_Agente_Causador = value; }
        }

        public Int32 Codigo_Descricao_Lesao
        {
            get { return _Codigo_Descricao_Lesao; }
            set { _Codigo_Descricao_Lesao = value; }
        }

        public Int32 Codigo_Situacao_Geradora
        {
            get { return _Codigo_Situacao_Geradora; }
            set { _Codigo_Situacao_Geradora = value; }
        }

        public Int16 IdIniciativaCat
        {
            get { return _IdIniciativaCat; }
            set { _IdIniciativaCat = value; }
        }

        public Int16 IdTipoLocal
        {
            get { return _IdTipoLocal; }
            set { _IdTipoLocal = value; }
        }

        public string Logradouro
        {
            get { return _Logradouro; }
            set { _Logradouro = value; }
        }

        public string Nr_Logradouro
        {
            get { return _Nr_Logradouro; }
            set { _Nr_Logradouro = value; }
        }

        public string Municipio
        {
            get { return _Municipio; }
            set { _Municipio = value; }
        }

        public string UF
        {
            get { return _UF; }
            set { _UF = value; }
        }


        public bool HasInternacao
        {
            get { return _HasInternacao; }
            set { _HasInternacao = value; }
        }

        public string CNES
        {
            get { return _CNES; }
            set { _CNES = value; }
        }

        public DateTime DataInternacao
        {
            get { return _DataInternacao; }
            set { _DataInternacao = value; }
        }

        public Int16 DuracaoInternacao
        {
            get { return _DuracaoInternacao; }
            set { _DuracaoInternacao = value; }
        }

        public string MedicoInternacao
        {
            get { return _MedicoInternacao; }
            set { _MedicoInternacao = value; }
        }

        public string CRMInternacao
        {
            get { return _CRMInternacao; }
            set { _CRMInternacao = value; }
        }

        public string UFInternacao
        {
            get { return _UFInternacao; }
            set { _UFInternacao = value; }
        }

        public string DiagnosticoProvavel
        {
            get { return _DiagnosticoProvavel; }
            set { _DiagnosticoProvavel = value; }
        }

        public Int16 IdLateralidade
        {
            get { return _IdLateralidade; }
            set { _IdLateralidade = value; }
        }

        public string Codigo_Acidente_Trabalho
        {
            get { return _Codigo_Acidente_Trabalho; }
            set { _Codigo_Acidente_Trabalho = value; }
        }

        public string codPostal
        {
            get { return _codPostal; }
            set { _codPostal = value; }
        }

        public string dscLocal
        {
            get { return _dscLocal; }
            set { _dscLocal = value; }
        }

        public string Bairro
        {
            get { return _Bairro; }
            set { _Bairro = value; }
        }

        public string Complemento
        {
            get { return _Complemento; }
            set { _Complemento = value; }
        }

        public string CEP
        {
            get { return _CEP; }
            set { _CEP = value; }
        }

        public string hrsTrabAntesAcid
        {
            get { return _hrsTrabAntesAcid; }
            set { _hrsTrabAntesAcid = value; }
        }

        public string CNPJ_Terceiro
        {
            get { return _CNPJ_Terceiro; }
            set { _CNPJ_Terceiro = value; }
        }

        public bool Reabertura
        {
            get { return _Reabertura; }
            set { _Reabertura = value; }
        }
        public string nrRecCatOrig
        {
            get { return _nrRecCatOrig; }
            set { _nrRecCatOrig = value; }
        }

        public DateTime UltDiaTrab
        {
            get { return _UltDiaTrab; }
            set { _UltDiaTrab = value; }
        }

    }
    #endregion

    #region Incidente

    [Database("opsa", "Incidente", "IdIncidente", "", "Incidente")]
    public class Incidente : Ilitera.Data.Table
    {
        private int _IdIncidente;
        private Cliente _IdCliente;
        private DateTime _DataIncidente;
        private Setor _IdSetor;
        private float _PerdaMaterial;
        private string _Observacoes;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Incidente()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Incidente(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdIncidente; }
            set { _IdIncidente = value; }
        }
        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        public DateTime DataIncidente
        {
            get { return _DataIncidente; }
            set { _DataIncidente = value; }
        }
        [Obrigatorio(true, "É necessário informar o Setor!")]
        public Setor IdSetor
        {
            get { return _IdSetor; }
            set { _IdSetor = value; }
        }
        public float PerdaMaterial
        {
            get { return _PerdaMaterial; }
            set { _PerdaMaterial = value; }
        }
        public string Observacoes
        {
            get { return _Observacoes; }
            set { _Observacoes = value; }
        }
    }
    #endregion

    #region AfastamentoTipo

    [Database("opsa", "AfastamentoTipo", "IdAfastamentoTipo")]
    public class AfastamentoTipo : Ilitera.Data.Table
    {
        private int _IdAfastamentoTipo;
        private string _Descricao = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AfastamentoTipo()
        {

        }

        public override int Id
        {
            get { return _IdAfastamentoTipo; }
            set { _IdAfastamentoTipo = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
    }
    #endregion

    #region Afastamento

    [Database("opsa", "Afastamento", "IdAfastamento")]
    public class Afastamento : Ilitera.Data.Table
    {
        private int _IdAfastamento;
        private Empregado _IdEmpregado;
        private Acidente _IdAcidente;
        private DateTime _DataInicial;
        private DateTime _DataVolta;
        private DateTime _DataPrevista;
        private CID _IdCID;
        private Int32 _IdCID2;
        private Int32 _IdCID3;
        private Int32 _IdCID4;
        private int _IndTipoAfastamento;
        private AfastamentoTipo _IdAfastamentoTipo;
        private string _Obs;
        private string _Atestado;
        private bool _INSS;
        private string _Atestado_Emitente;
        private string _Atestado_ideOC;
        private string _Atestado_nrOC;
        private string _Atestado_ufOC;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Afastamento()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Afastamento(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdAfastamento; }
            set { _IdAfastamento = value; }
        }
        public Empregado IdEmpregado
        {
            get { return _IdEmpregado; }
            set { _IdEmpregado = value; }
        }
        public Acidente IdAcidente
        {
            get { return _IdAcidente; }
            set { _IdAcidente = value; }
        }
        [Obrigatorio(true, "Data Inicial é campo obrigatório!")]
        public DateTime DataInicial
        {
            get { return _DataInicial; }
            set { _DataInicial = value; }
        }
        public DateTime DataVolta
        {
            get { return _DataVolta; }
            set { _DataVolta = value; }
        }
        public DateTime DataPrevista
        {
            get { return _DataPrevista; }
            set { _DataPrevista = value; }
        }
        [Obrigatorio(true, "O Tipo de Absentismo é campo obrigatório!")]
        public int IndTipoAfastamento
        {
            get { return _IndTipoAfastamento; }
            set { _IndTipoAfastamento = value; }
        }
        public CID IdCID
        {
            get { return _IdCID; }
            set { _IdCID = value; }
        }
        public AfastamentoTipo IdAfastamentoTipo
        {
            get { return _IdAfastamentoTipo; }
            set { _IdAfastamentoTipo = value; }
        }

        public string Obs
        {
            get { return _Obs; }
            set { _Obs = value; }
        }

        public string Atestado
        {
            get { return _Atestado; }
            set { _Atestado = value; }
        }


        public Int32 IdCID2
        {
            get { return _IdCID2; }
            set { _IdCID2 = value; }
        }
        public Int32 IdCID3
        {
            get { return _IdCID3; }
            set { _IdCID3 = value; }
        }
        public Int32 IdCID4
        {
            get { return _IdCID4; }
            set { _IdCID4 = value; }
        }
        public bool INSS
        {
            get { return _INSS; }
            set { _INSS = value; }
        }

        public string Atestado_Emitente
        {
            get { return _Atestado_Emitente; }
            set { _Atestado_Emitente = value; }
        }

        public string Atestado_ideOC
        {
            get { return _Atestado_ideOC; }
            set { _Atestado_ideOC = value; }
        }

        public string Atestado_nrOC
        {
            get { return _Atestado_nrOC; }
            set { _Atestado_nrOC = value; }
        }

        public string Atestado_ufOC
        {
            get { return _Atestado_ufOC; }
            set { _Atestado_ufOC = value; }
        }


        public override void Validate()
        {
            if (this.DataVolta != new DateTime() && this.DataVolta < this.DataInicial)
                throw new Exception("A Data de Retorno do Absentismo deve ser superior à Data de Início!");

            ArrayList list = new Afastamento().Find("IdEmpregado=" + this.IdEmpregado.Id
                + " AND DataVolta IS NULL"
                + " AND IdAfastamento<>" + this.Id);

            if (list.Count > 0)
                throw new Exception("Este empregado não pode ter mais de um afastamento em aberto!");

            base.Validate();
        }

        public int Save(bool bVal)
        {
            return base.Save();
        }
    }
    #endregion

    #region CID

    [Database("opsa", "CID", "IdCID")]
    public class CID : Ilitera.Data.Table
    {
        private int _IdCID;
        private string _CodigoCID = string.Empty;
        private bool _IsCategoria;
        private bool _IsSubCategoria;
        private string _Descricao = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CID()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CID(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdCID; }
            set { _IdCID = value; }
        }
        public string CodigoCID
        {
            get { return _CodigoCID; }
            set { _CodigoCID = value; }
        }
        public bool IsSubCategoria
        {
            get { return _IsSubCategoria; }
            set { _IsSubCategoria = value; }
        }
        public bool IsCategoria
        {
            get { return _IsCategoria; }
            set { _IsCategoria = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
    }

    [Database("opsa", "CidCapitulo", "IdCidCapitulo")]
    public class CidCapitulo : Ilitera.Data.Table
    {
        private int _IdCidCapitulo;
        private string _Descricao = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CidCapitulo()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CidCapitulo(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdCidCapitulo; }
            set { _IdCidCapitulo = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
    }

    [Database("opsa", "CidGrupo", "IdCidGrupo")]
    public class CidGrupo : Ilitera.Data.Table
    {
        private int _IdCidGrupo;
        private CidCapitulo _IdCidCapitulo;
        private string _Descricao = string.Empty;
        private string _NumerosCNAE = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CidGrupo()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CidGrupo(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdCidGrupo; }
            set { _IdCidGrupo = value; }
        }
        public CidCapitulo IdCidCapitulo
        {
            get { return _IdCidCapitulo; }
            set { _IdCidCapitulo = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public string NumerosCNAE
        {
            get { return _NumerosCNAE; }
            set { _NumerosCNAE = value; }
        }

        public override int Save()
        {
            int id = base.Save();

            CriarCidGrupoCnae();

            return id;
        }

        public static void AtualizarCidGrupo()
        {
            ArrayList grupos = new CidGrupo().FindAll();

            foreach (CidGrupo grupo in grupos)
            {
                string sfaixa = grupo.Descricao.Substring(0, grupo.Descricao.IndexOf(" "));

                char[] seps = { '-' };

                string[] faixas = sfaixa.Split(seps);

                if (faixas.Length != 2)
                    continue;

                string alfa1 = faixas[0].Substring(0, 1);
                int val1 = Convert.ToInt32(faixas[0].Substring(1, 2));

                string alfa2 = faixas[1].Substring(0, 1);
                int val2 = Convert.ToInt32(faixas[1].Substring(1, 2));

                //if (faixas[0] != faixas[1])
                //    continue;

                if (alfa1 != alfa2)
                    continue;

                StringBuilder cmd = new StringBuilder();

                cmd.Append("UPDATE CID SET IdCidGrupo = " + grupo.Id);
                cmd.Append(" WHERE ");
                cmd.Append(" (CodigoCID LIKE '" + faixas[0] + "%')");

                if(alfa1 == alfa2)
                    for (int i = ++val1; i <= val2; i++)
                        cmd.Append(" OR (CodigoCID LIKE '" + alfa1 + i.ToString("00") + "%')");

                new CidGrupo().ExecuteScalar(cmd.ToString());
            }
        }

        public void CriarCidGrupoCnae()
        {
            new CidGrupoCnae().Delete("IdCidGrupo=" + this.Id);

            char[] seps = { ' ' };

            string[] scnaes = this.NumerosCNAE.Split(seps);

            foreach (string scnae in scnaes)
            {
                if (scnae == string.Empty)
                    continue;

                ArrayList cnaes = new CNAE().Find("(IndCNAE = 1) AND (Codigo LIKE '" + scnae + "%')");

                foreach (CNAE cnae in cnaes)
                {
                    CidGrupoCnae cidGrupoCnae = new CidGrupoCnae();
                    cidGrupoCnae.Inicialize();
                    cidGrupoCnae.IdCidGrupo = this;
                    cidGrupoCnae.IdCNAE = cnae;
                    cidGrupoCnae.Save();
                }
            }
        }
    }

    [Database("opsa", "CidGrupoCnae", "IdCidGrupoCnae")]
    public class CidGrupoCnae : Ilitera.Data.Table
    {
        private int _IdCidGrupoCnae;
        private CidGrupo _IdCidGrupo;
        private CNAE _IdCNAE;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CidGrupoCnae()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CidGrupoCnae(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdCidGrupoCnae; }
            set { _IdCidGrupoCnae = value; }
        }
        public CidGrupo IdCidGrupo
        {
            get { return _IdCidGrupo; }
            set { _IdCidGrupo = value; }
        }
        public CNAE IdCNAE
        {
            get { return _IdCNAE; }
            set { _IdCNAE = value; }
        }
    }


    #endregion

    #region Testemunha

    [Database("opsa", "Testemunha", "IdTestemunha")]
    public class Testemunha : Ilitera.Data.Table
    {
        private int _IdTestemunha;
        private CAT _IdCAT;
        private Empregado _IdEmpregado;
        private string _NomeCompleto = string.Empty;
        private string _Endereco = string.Empty;
        private string _Bairro = string.Empty;
        private string _CEP = string.Empty;
        private string _Municipio = string.Empty;
        private string _UF = string.Empty;
        private string _Telefone = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Testemunha()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Testemunha(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdTestemunha; }
            set { _IdTestemunha = value; }
        }
        public CAT IdCAT
        {
            get { return _IdCAT; }
            set { _IdCAT = value; }
        }
        public Empregado IdEmpregado
        {
            get { return _IdEmpregado; }
            set { _IdEmpregado = value; }
        }
        public string NomeCompleto
        {
            get { return _NomeCompleto; }
            set { _NomeCompleto = value; }
        }
        public string Endereco
        {
            get { return _Endereco; }
            set { _Endereco = value; }
        }
        public string Bairro
        {
            get { return _Bairro; }
            set { _Bairro = value; }
        }
        public string CEP
        {
            get { return _CEP; }
            set { _CEP = value; }
        }
        public string Municipio
        {
            get { return _Municipio; }
            set { _Municipio = value; }
        }
        public string UF
        {
            get { return _UF; }
            set { _UF = value; }
        }
        public string Telefone
        {
            get { return _Telefone; }
            set { _Telefone = value; }
        }
    }
    #endregion

    #region LocalAcidente

    [Database("opsa", "LocalAcidente", "IdLocalAcidente")]
    public class LocalAcidente : Ilitera.Data.Table
    {
        private int _IdLocalAcidente;
        private Cliente _IdCliente;
        private string _Nome = string.Empty;
        private string _CNPJ = string.Empty;
        private string _Municipio = string.Empty;
        private string _UF = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public LocalAcidente()
        {
        }


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public LocalAcidente(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdLocalAcidente; }
            set { _IdLocalAcidente = value; }
        }
        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        [Obrigatorio(true, "O Nome Completo do Local de Acidente é obrigatório!")]
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }
        [Obrigatorio(true, "O CNPJ do Local de Acidente é obrigatório!")]
        public string CNPJ
        {
            get { return _CNPJ; }
            set { _CNPJ = value; }
        }
        public string Municipio
        {
            get { return _Municipio; }
            set { _Municipio = value; }
        }
        public string UF
        {
            get { return _UF; }
            set { _UF = value; }
        }
    }
    #endregion
}
