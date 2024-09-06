using System;
using System.Collections.Generic;
using System.Text;

using Ilitera.Data;

namespace Ilitera.Common
{
    #region class AtividadeBase

    [Database("opsa", "AtividadeBase", "IdAtividadeBase")]
    public class AtividadeBase : Ilitera.Data.Table, Ilitera.Common.IDataInicioTermino
    {
        public enum TipoAtividade : int
        {
            Compromisso,
            Chamada,
            Tarefa,
            Email,
            TimeSheet,
            ControlePedido,
            Telemarketing
        }

        private int _IdAtividadeBase;
        private Pessoa _IdPessoa;
        private Pessoa _IdPessoaContato;
        private Pessoa _IdPessoaCriador;
        private TipoAtividade _IndAtividade;
        private Categoria _IdCategoria;
        private DateTime _DataInicio;
        private DateTime _DataTermino;
        private string _Descricao = string.Empty;
        private string _Solucao = string.Empty;
        private DateTime _DataSolucao;
        private DateTime _DataCriacao;
        private bool _Particular;
        private AtividadeBase _IdAtividadeOrigem;
        private Repetir _IdRepetir;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public AtividadeBase()
        {

        }
        public override int Id
        {
            get { return _IdAtividadeBase; }
            set { _IdAtividadeBase = value; }
        }
        public Pessoa IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }
        public Pessoa IdPessoaContato
        {
            get { return _IdPessoaContato; }
            set { _IdPessoaContato = value; }
        }
        public Pessoa IdPessoaCriador
        {
            get { return _IdPessoaCriador; }
            set { _IdPessoaCriador = value; }
        }
        public TipoAtividade IndAtividade
        {
            get { return _IndAtividade; }
            set { _IndAtividade = value; }
        }
        [Obrigatorio(true, "A Categoria é obrigatório")]
        public Categoria IdCategoria
        {
            get { return _IdCategoria; }
            set { _IdCategoria = value; }
        }
        public DateTime DataInicio
        {
            get { return _DataInicio; }
            set { _DataInicio = value; }
        }
        public DateTime DataTermino
        {
            get { return _DataTermino; }
            set { _DataTermino = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public string Solucao
        {
            get { return _Solucao; }
            set { _Solucao = value; }
        }
        public DateTime DataSolucao
        {
            get { return _DataSolucao; }
            set { _DataSolucao = value; }
        }
        public DateTime DataCriacao
        {
            get { return _DataCriacao; }
            set { _DataCriacao = value; }
        }
        public bool Particular
        {
            get { return _Particular; }
            set { _Particular = value; }
        }
        public AtividadeBase IdAtividadeOrigem
        {
            get { return _IdAtividadeOrigem; }
            set { _IdAtividadeOrigem = value; }
        }
        public Repetir IdRepetir
        {
            get { return _IdRepetir; }
            set { _IdRepetir = value; }
        }

        public string GetAtividade()
        {
            if (this.mirrorOld == null)
                this.Find();

            return GetAtividade(this.IndAtividade);
        }
        
        public static string GetAtividade(TipoAtividade atividade)
        {
            string ret;

            if (atividade == TipoAtividade.ControlePedido)
                ret = "Controle Pedido";
            else
                ret = atividade.ToString();

            return ret;
        }

        public virtual void VerificarPermissao()
        {
            Usuario usuario = Usuario.Login();

            if (this.Particular && this.IdPessoaCriador.Id != usuario.IdPessoa.Id)
                throw new Exception("Esta atividade só pode ser alterado por " + this.IdPessoaCriador.ToString() + "!");

            Usuario usuarioCriador = new Usuario();
            usuarioCriador.Find("IdPessoa=" + this.IdPessoaCriador.Id);

            if (usuarioCriador.IsGrupo(Grupos.Administracao))
            {
                if (!(usuario.IdPessoa.Id == this.IdPessoaCriador.Id || usuario.IdPessoa.Id == this.IdPessoa.Id))
                {
                    if (this.IdPessoaCriador.Id == this.IdPessoa.Id)
                        throw new Exception("Esta atividade só pode ser alterado por " + this.IdPessoaCriador.ToString() + "!");
                    else
                        throw new Exception("Esta atividade só pode ser alterado por " + this.IdPessoaCriador.ToString() + " ou " + this.IdPessoa.ToString() + "!");
                }
            }
        }

        public override void Validate()
        {
            base.Validate();

            if (this.DataTermino != new DateTime() && this.DataInicio >= this.DataTermino)
                throw new Exception("A data de início deve ser maior que a data de término!");

            VerificarPermissao();
        }

        public override void Delete()
        {
            VerificarPermissao();

            base.Delete();
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return this.Descricao 
                    + " " + this.DataInicio.ToString("dd-MM-yyyy hh:mm") + " hs";
        }

        #region GetAviso

        public Aviso GetAviso()
        {
            Aviso aviso = new Aviso();
            aviso.Find("IdAtividadeBase=" + this.Id);

            if (aviso.Id == 0)
            {
                aviso.Inicialize();
                aviso.IdAtividadeBase = this;
            }

            return aviso;
        }
        #endregion
    }

    #endregion

    #region class AtividadeLogAcesso

    [Database("opsa", "AtividadeLogAcesso", "IdAtividadeLogAcesso")]
    public class AtividadeLogAcesso : Ilitera.Data.Table
    {
        private int _IdAtividadeLogAcesso;
        private AtividadeBase.TipoAtividade _IndAtividade;
        private DateTime _DataAcesso = DateTime.Now;
        private Pessoa _IdPessoaLogin;
        private Pessoa _IdPessoaTarget;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AtividadeLogAcesso()
        {

        }
        public override int Id
        {
            get { return _IdAtividadeLogAcesso; }
            set { _IdAtividadeLogAcesso = value; }
        }
        public AtividadeBase.TipoAtividade IndAtividade
        {
            get { return _IndAtividade; }
            set { _IndAtividade = value; }
        }
        public DateTime DataAcesso
        {
            get { return _DataAcesso; }
            set { _DataAcesso = value; }
        }
        public Pessoa IdPessoaLogin
        {
            get { return _IdPessoaLogin; }
            set { _IdPessoaLogin = value; }
        }
        public Pessoa IdPessoaTarget
        {
            get { return _IdPessoaTarget; }
            set { _IdPessoaTarget = value; }
        }

        public static void AddLogAcesso(AtividadeBase.TipoAtividade IndAtividade,
                                        bool IsContato,
                                        Pessoa IdPessoaLogin,
                                        Pessoa IdPessoaTarget)
        {
            try
            {
                if (IsContato || IdPessoaLogin.Id == IdPessoaTarget.Id)
                    return;

                AtividadeLogAcesso log = new AtividadeLogAcesso();
                log.DataAcesso = DateTime.Now;
                log.IndAtividade = IndAtividade;
                log.IdPessoaLogin = IdPessoaLogin;
                log.IdPessoaTarget = IdPessoaTarget;
                log.Save();
            }
            catch{}
        }
    }
    #endregion
}
