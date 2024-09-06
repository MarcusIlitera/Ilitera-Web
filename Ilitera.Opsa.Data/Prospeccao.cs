using System;
using System.Collections;
using System.Data;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    public enum StatusTelemarketing : int
    {
        NaoEstava = 1,
        SemInteresse,
        EnviarRelacao,
        LigarNovamente,
        EnviarProposta,
        Ocupado,
        Visita,
        RetornoEspontaneo
    }

    public enum CadastrosImportacao : int
    {
        Nenhuma,
        Geral,
        Porte,
        RamoAtividade,
        Exportadora,
        Importadora
    }

    public enum PorteEmpresa : int
    {
        Nenhuma,
        Micro,
        Pequena,
        Media,
        Grande
    }

    public enum StatusProspeccao : int
    {
        Nenhuma = 0,
        EmProspeccao = 1,
        PossivelFechamento = 2,
        Fechamento = 3
    }

    [Database("opsa", "Prospeccao", "IdProspeccao")]
    public class Prospeccao : Ilitera.Common.Juridica
    {
        private int _IdProspeccao;
        private AcaoProspeccaoStatus _IdAcaoProspeccaoStatus;
        private CadastroImportacao _IdCadastroImportacao;
        private int _IndPorteEmpresa;
        private DateTime _Fechamento;
        private bool _CIPA;
        private int _VasoPressao;
        private int _Caldeira;
        private int _Empilhadeira;
        private int _PonteRolante;
        private int _Prensas;
        private int _NumeroSetores;
        private float _Turnover;
        private bool _Ruido;
        private bool _Quimico;
        private string _AssesoriaAtual = string.Empty;
        private float _ValorContrato;
        private string _Filiais = string.Empty;
        private int _Vendas;
        private bool _EnviarEmail = true;
        private DateTime _VencimentoContrato;
        private bool _ContratoVencIndeterminado;
        private decimal _SalarioMedio;
        private float _ValorSeguranca;
        private float _ValorPCMSO;

        public Prospeccao()
        {

        }
        public override int Id
        {
            get { return _IdProspeccao; }
            set { _IdProspeccao = value; }
        }
        public AcaoProspeccaoStatus IdAcaoProspeccaoStatus
        {
            get { return _IdAcaoProspeccaoStatus; }
            set { _IdAcaoProspeccaoStatus = value; }
        }
        public CadastroImportacao IdCadastroImportacao
        {
            get { return _IdCadastroImportacao; }
            set { _IdCadastroImportacao = value; }
        }
        public int IndPorteEmpresa
        {
            get { return _IndPorteEmpresa; }
            set { _IndPorteEmpresa = value; }
        }
        public bool CIPA
        {
            get { return _CIPA; }
            set { _CIPA = value; }
        }
        public DateTime Fechamento
        {
            get { return _Fechamento; }
            set { _Fechamento = value; }
        }
        public int VasoPressao
        {
            get { return _VasoPressao; }
            set { _VasoPressao = value; }
        }
        public int Caldeira
        {
            get { return _Caldeira; }
            set { _Caldeira = value; }
        }
        public int Empilhadeira
        {
            get { return _Empilhadeira; }
            set { _Empilhadeira = value; }
        }
        public int PonteRolante
        {
            get { return _PonteRolante; }
            set { _PonteRolante = value; }
        }
        public int Prensas
        {
            get { return _Prensas; }
            set { _Prensas = value; }
        }
        public int NumeroSetores
        {
            get { return _NumeroSetores; }
            set { _NumeroSetores = value; }
        }
        public float Turnover
        {
            get { return _Turnover; }
            set { _Turnover = value; }
        }
        public bool Ruido
        {
            get { return _Ruido; }
            set { _Ruido = value; }
        }
        public bool Quimico
        {
            get { return _Quimico; }
            set { _Quimico = value; }
        }
        public string AssesoriaAtual
        {
            get { return _AssesoriaAtual; }
            set { _AssesoriaAtual = value; }
        }
        public float ValorContrato
        {
            get { return _ValorContrato; }
            set { _ValorContrato = value; }
        }
        public string Filiais
        {
            get { return _Filiais; }
            set { _Filiais = value; }
        }
        public int Vendas
        {
            get { return _Vendas; }
            set { _Vendas = value; }
        }
        public bool EnviarEmail
        {
            get { return _EnviarEmail; }
            set { _EnviarEmail = value; }
        }
        public DateTime VencimentoContrato
        {
            get { return _VencimentoContrato; }
            set { _VencimentoContrato = value; }
        }
        public bool ContratoVencIndeterminado
        {
            get { return _ContratoVencIndeterminado; }
            set { _ContratoVencIndeterminado = value; }
        }
        public float ValorSeguranca
        {
            get { return _ValorSeguranca; }
            set { _ValorSeguranca = value; }
        }
        public float ValorPCMSO
        {
            get { return _ValorPCMSO; }
            set { _ValorPCMSO = value; }
        }
        public decimal SalarioMedio
        {
            get { return _SalarioMedio; }
            set { _SalarioMedio = value; }
        }

        public static PorteEmpresa GetPorteEmpresa(string strPorte)
        {
            PorteEmpresa ret;

            if (strPorte == "PEQUENA")
                ret = PorteEmpresa.Pequena;
            else if (strPorte == "GRANDE")
                ret = PorteEmpresa.Grande;
            else if (strPorte == "MICRO")
                ret = PorteEmpresa.Micro;
            else if (strPorte == "MEDIA")
                ret = PorteEmpresa.Media;
            else
                ret = PorteEmpresa.Nenhuma;
            return ret;
        }
    }


    [Database("opsa", "CadastroImportacao", "IdCadastroImportacao")]
    public class CadastroImportacao : Ilitera.Data.Table
    {
        private int _IdCadastroImportacao;
        private string _Descricao = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CadastroImportacao()
        {

        }
        public override int Id
        {
            get { return _IdCadastroImportacao; }
            set { _IdCadastroImportacao = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public override string ToString()
        {
            return _Descricao;
        }
    }

    [Database("opsa", "TelemarketingAcao", "IdTelemarketingAcao")]
    public class TelemarketingAcao : Ilitera.Data.Table
    {
        private int _IdTelemarketingAcao;
        private string _Descricao = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TelemarketingAcao()
        {

        }
        public override int Id
        {
            get { return _IdTelemarketingAcao; }
            set { _IdTelemarketingAcao = value; }
        }
        [Obrigatorio(true, "O descrição é campo obrigatório!")]
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

    [Database("opsa", "TelemarketingStatus", "IdTelemarketingStatus")]
    public class TelemarketingStatus : Ilitera.Data.Table
    {
        private int _IdTelemarketingStatus;
        private string _Descricao = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TelemarketingStatus()
        {

        }
        public override int Id
        {
            get { return _IdTelemarketingStatus; }
            set { _IdTelemarketingStatus = value; }
        }
        [Obrigatorio(true, "O descrição é campo obrigatório!")]
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


    [Database("opsa", "AcaoProspeccaoStatus", "IdAcaoProspeccaoStatus")]
    public class AcaoProspeccaoStatus : Ilitera.Data.Table
    {
        private int _IdAcaoProspeccaoStatus;
        private string _Nome = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AcaoProspeccaoStatus()
        {

        }
        public override int Id
        {
            get { return _IdAcaoProspeccaoStatus; }
            set { _IdAcaoProspeccaoStatus = value; }
        }
        [Obrigatorio(true, "O nome é campo obrigatório!")]
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }
        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return Nome;
        }
    }


    [Database("opsa", "ProspeccaoFilial", "IdProspeccaoFilial")]
    public class ProspeccaoFilial : Ilitera.Data.Table
    {
        private int _IdProspeccaoFilial;
        private Prospeccao _IdProspeccao;
        private string _NomeFilial = string.Empty;
        private int _QtdEmpregado;
        private bool _TemCipa;
        private int _VasoPressao;
        private int _Caldeira;
        private int _NumeroSetor;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProspeccaoFilial()
        {

        }
        public override int Id
        {
            get { return _IdProspeccaoFilial; }
            set { _IdProspeccaoFilial = value; }
        }
        public Prospeccao IdProspeccao
        {
            get { return _IdProspeccao; }
            set { _IdProspeccao = value; }
        }
        public string NomeFilial
        {
            get { return _NomeFilial; }
            set { _NomeFilial = value; }
        }
        public int QtdEmpregado
        {
            get { return _QtdEmpregado; }
            set { _QtdEmpregado = value; }
        }
        public bool TemCipa
        {
            get { return _TemCipa; }
            set { _TemCipa = value; }
        }
        public int VasoPressao
        {
            get { return _VasoPressao; }
            set { _VasoPressao = value; }
        }
        public int Caldeira
        {
            get { return _Caldeira; }
            set { _Caldeira = value; }
        }
        public int NumeroSetor
        {
            get { return _NumeroSetor; }
            set { _NumeroSetor = value; }
        }
        public override string ToString()
        {
            return _NomeFilial;
        }
    }
}
