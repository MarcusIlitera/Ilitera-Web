using System;
using System.Collections;
using System.Text;

using Ilitera.Data;

namespace Ilitera.Common
{
    #region Tarefa New Class

    [Database("opsa", "TarefaNew", "IdTarefa")]
    public class Tarefa : Ilitera.Common.AtividadeBase
    {
        #region Enum

        public enum Prioridade : int
        {
            Baixa,
            Media,
            Alta
        }

        public enum FiltroTarefa : int
        {
            Ativas,
            EmAtrazo,
            Concluida
        }

        #endregion

        #region Properties

        private int _IdTarefa;
        private bool _SemData;
        private DateTime _DataPrazo;
        private Prioridade _IndPrioridade;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Tarefa()
        {
            this.IndAtividade = TipoAtividade.Tarefa;
        }
        public override int Id
        {
            get { return _IdTarefa; }
            set { _IdTarefa = value; }
        }
        [Persist(false)]
        public DateTime DataConclusao
        {
            get { return DataTermino; }
            set { DataTermino = value; }
        }
        public DateTime DataPrazo
        {
            get { return _DataPrazo; }
            set { _DataPrazo = value; }
        }
        public bool SemData
        {
            get { return _SemData; }
            set { _SemData = value; }
        }
        public Prioridade IndPrioridade
        {
            get { return _IndPrioridade; }
            set { _IndPrioridade = value; }
        }

        #endregion

        #region Metodos

        public override int Save()
        {
            if (this.SemData)
            {
                this.DataInicio = new DateTime();
                this.DataPrazo = new DateTime();
            }
            else
            {
                if (this.DataPrazo != new DateTime() && this.DataInicio >= this.DataPrazo)
                    throw new Exception("A data de início deve ser maior que o prazo!");
            }

            this.IndAtividade = TipoAtividade.Tarefa;

            return base.Save();
        }

        public string GetStatusTarefa()
        {
            string ret = string.Empty;

            if (this.DataSolucao != new DateTime())
                ret = "Concluída";
            else
                ret = "Em Andamento";

            return ret;
        }

        public void PopularTextoPadrao()
        {
            PopularTextoPadrao(this.IdCategoria.Id);
        }

        public void PopularTextoPadrao(int IdCategoria)
        {
            this.IdCategoria.Id = IdCategoria;

            Categoria categoria = new Categoria();
            categoria.Find(IdCategoria);

            if (categoria.TextoTarefa != string.Empty)
                this.Descricao = categoria.TextoTarefa;
        }

        #endregion
    }

    #endregion

    #region Tarefa Class

    [Database("opsa","Tarefa","IdTarefa")]
	public class TarefaBak: Ilitera.Data.Table//, IAviso, IPeriodicidade
    {
        #region Properties
        private int _IdTarefa;
		private Pessoa _IdPessoa;
		private Pessoa _IdPessoaContato;
		private Pessoa _IdPessoaCriador;
		private bool _SemData;
		private DateTime _DataInicio;
		private DateTime _DataPrazo;
		private string _Descricao=string.Empty;
		private Categoria _IdCategoria;
		private Repetir _IdRepetir;
		private int _IndPrioridade;
		private DateTime _DataConclusao;
		private bool _Particular;
        //private int _IndAntesDepois;
        //private int _IndAvisoPeriodicidade;
        //private int _AvisoIntervalo;
        //private DateTime _AvisoDataExecutar;
        //private int _IndFormaAviso;
        //private string _Mensagem=string.Empty;
        //private int _IndPeriodicidade;
        //private int _Intervalo;
        //private int _IndFeriado;
        //private int _IndPrazo;
        //private DateTime _PrazoAte;
        //private int _PrazoPorIndPeriodiciade;
        //private int _ProzoPorIntervalo;
        //private DateTime _DataExecutar;
        //private bool _Segunda;
        //private bool _Terca;
        //private bool _Quarta;
        //private bool _Quinta;
        //private bool _Sexta;
        //private bool _Sabado;
        //private bool _Domingo;
        //private int _Dia;
        //private int _Mes;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TarefaBak()
		{

		}
		public override int Id
		{
			get{return _IdTarefa;}
			set{_IdTarefa = value;}
		}
		[Obrigatorio(true, "A pessoa é obrigatório!")]
		public Pessoa IdPessoa
		{
			get{return _IdPessoa;}
			set{_IdPessoa = value;}
		}
		public Pessoa IdPessoaContato
		{
			get{return _IdPessoaContato;}
			set{_IdPessoaContato = value;}
		}
		[Obrigatorio(true, "A pessoa que criou o compromisso é obrigatório!")]
		public Pessoa IdPessoaCriador
		{
			get{return _IdPessoaCriador;}
			set{_IdPessoaCriador = value;}
		}
		public DateTime DataInicio
		{
			get{return _DataInicio;}
			set{_DataInicio = value;}
		}
		public DateTime DataPrazo
		{
			get{return _DataPrazo;}
			set{_DataPrazo = value;}
		}
		public bool SemData
		{
			get{return _SemData;}
			set{_SemData = value;}
		}
		public int IndPrioridade
		{
			get{return _IndPrioridade;}
			set{_IndPrioridade = value;}
		}
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
		public DateTime DataConclusao
		{
			get{return _DataConclusao;}
			set{_DataConclusao = value;}
		}
		[Obrigatorio(true, "A Categoria é obrigatório")]
		public Categoria IdCategoria
		{
			get{return _IdCategoria;}
			set{_IdCategoria = value;}
		}
		public Repetir IdRepetir
		{
			get{return _IdRepetir;}
			set{_IdRepetir = value;}
		}
		public bool Particular
		{
			get{return _Particular;}
			set{_Particular = value;}
		}
        //public int IndAntesDepois
        //{
        //    get{return _IndAntesDepois;}
        //    set{_IndAntesDepois = value;}
        //}
        //public int IndAvisoPeriodicidade
        //{
        //    get{return _IndAvisoPeriodicidade;}
        //    set{_IndAvisoPeriodicidade = value;}
        //}
        //public int AvisoIntervalo
        //{
        //    get{return _AvisoIntervalo;}
        //    set{_AvisoIntervalo = value;}
        //}
        //public DateTime AvisoDataExecutar
        //{
        //    get{return _AvisoDataExecutar;}
        //    set{_AvisoDataExecutar = value;}
        //}
        //public int IndFormaAviso
        //{
        //    get{return _IndFormaAviso;}
        //    set{_IndFormaAviso = value;}
        //}
        //public string Mensagem
        //{
        //    get{return _Mensagem;}
        //    set{_Mensagem = value;}
        //}
        //public int IndPeriodicidade
        //{
        //    get{return _IndPeriodicidade;}
        //    set{_IndPeriodicidade = value;}
        //}
        //public int Intervalo
        //{
        //    get{return _Intervalo;}
        //    set{_Intervalo = value;}
        //}
        //public int IndFeriado
        //{
        //    get{return _IndFeriado;}
        //    set{_IndFeriado = value;}
        //}
        //public int IndPrazo
        //{
        //    get{return _IndPrazo;}
        //    set{_IndPrazo = value;}
        //}
        //public DateTime PrazoAte
        //{
        //    get{return _PrazoAte;}
        //    set{_PrazoAte = value;}
        //}
        //public int PrazoPorIndPeriodiciade
        //{
        //    get{return _PrazoPorIndPeriodiciade;}
        //    set{_PrazoPorIndPeriodiciade = value;}
        //}
        //public int ProzoPorIntervalo
        //{
        //    get{return _ProzoPorIntervalo;}
        //    set{_ProzoPorIntervalo = value;}
        //}
        //public DateTime DataExecutar
        //{
        //    get{return _DataExecutar;}
        //    set{_DataExecutar = value;}
        //}
        //public bool Segunda
        //{
        //    get{return _Segunda;}
        //    set{_Segunda = value;}
        //}
        //public bool Terca
        //{
        //    get{return _Terca;}
        //    set{_Terca = value;}
        //}
        //public bool Quarta
        //{
        //    get{return _Quarta;}
        //    set{_Quarta = value;}
        //}
        //public bool Quinta
        //{
        //    get{return _Quinta;}
        //    set{_Quinta = value;}
        //}
        //public bool Sexta
        //{
        //    get{return _Sexta;}
        //    set{_Sexta = value;}
        //}
        //public bool Sabado
        //{
        //    get{return _Sabado;}
        //    set{_Sabado = value;}
        //}
        //public bool Domingo
        //{
        //    get{return _Domingo;}
        //    set{_Domingo = value;}
        //}
        //public int Dia
        //{
        //    get{return _Dia;}
        //    set{_Dia = value;}
        //}
        //public int Mes
        //{
        //    get{return _Mes;}
        //    set{_Mes = value;}
        //}
        #endregion

        #region Metodos

        public override int Save()
		{
			if(this.SemData)
			{
				this.DataInicio = new DateTime();
				this.DataPrazo = new DateTime();
			}
			return base.Save ();
		}

        public string GetStatusTarefa()
        {
            string ret = string.Empty;

            if (this.DataConclusao != new DateTime())
                ret = "Concluída";
            else
                ret = "Em Andamento";

            return ret;
        }

		public enum FiltroTarefa : int
		{
			Ativas,
			EmAtrazo,
			Concluida
		}

        public void VerificarPermissao()
        {
            Usuario usuario = Usuario.Login();

            if (this.Particular && this.IdPessoaCriador.Id != usuario.IdPessoa.Id)
                throw new Exception("Esta tarefa só pode ser alterado por " + this.IdPessoaCriador.ToString() + "!");

            Usuario usuarioCriador = new Usuario();
            usuarioCriador.Find("IdPessoa=" + this.IdPessoaCriador.Id);

            if (usuarioCriador.IsGrupo(Grupos.Administracao))
            {
                if (!(usuario.IdPessoa.Id == this.IdPessoaCriador.Id || usuario.IdPessoa.Id == this.IdPessoa.Id))
                {
                    if (this.IdPessoaCriador.Id == this.IdPessoa.Id)
                        throw new Exception("Esta tarefa só pode ser alterado por " + this.IdPessoaCriador.ToString() + "!");
                    else
                        throw new Exception("Esta tarefa só pode ser alterado por " + this.IdPessoaCriador.ToString() + " ou " + this.IdPessoa.ToString() + "!");
                }
            }
        }
        #endregion
    }
    #endregion
}
