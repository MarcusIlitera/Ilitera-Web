using System;
using Ilitera.Data;

namespace Ilitera.Common
{
	[Database("opsa","TimeSheet","IdTimeSheet")]
    public class TimeSheet : Ilitera.Data.Table, Ilitera.Common.IDataInicioTermino
	{
		private int _IdTimeSheet;
		private Pessoa _IdPessoa;
		private Pessoa _IdPessoaContato;
		private Pessoa _IdPessoaAprovador;
		private DateTime _DataInicio;
		private DateTime _DataTermino;
		private bool _IntervaloAlmoco;
		private DateTime _DataAprovacao;
		private int _IndStatusAprovacao;
        private string _Descricao = string.Empty;
        private string _DescricaoAprovacao = string.Empty;
        private float _ValorHora;

        public enum TimeSheetStatus : int
        {
            EmAberto,
            NaoAprovado,
            Aprovado
        }


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TimeSheet()
		{

		}
		public override int Id
		{
			get{return _IdTimeSheet;}
			set{_IdTimeSheet = value;}
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
		public Pessoa IdPessoaAprovador
		{
			get{return _IdPessoaAprovador;}
			set{_IdPessoaAprovador = value;}
		}
		public DateTime DataInicio
		{
			get{return _DataInicio;}
			set{_DataInicio = value;}
		}
		public DateTime DataTermino
		{
			get{return _DataTermino;}
			set{_DataTermino = value;}
		}
		public DateTime DataAprovacao
		{
			get{return _DataAprovacao;}
			set{_DataAprovacao = value;}
		}
		public bool IntervaloAlmoco
		{
			get{return _IntervaloAlmoco;}
			set{_IntervaloAlmoco = value;}
		}
		public int IndStatusAprovacao
		{
			get{return _IndStatusAprovacao;}
			set{_IndStatusAprovacao = value;}
		}
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
		public string DescricaoAprovacao
		{
			get{return _DescricaoAprovacao;}
			set{_DescricaoAprovacao = value;}
		}
        public float ValorHora
        {
            get { return _ValorHora; }
            set { _ValorHora = value; }
        }

        public string GetStatus()
        {
            string ret;

            if (this.IndStatusAprovacao == (int)TimeSheetStatus.EmAberto)
                ret = "Em Aberto";
            else if (this.IndStatusAprovacao == (int)TimeSheetStatus.NaoAprovado)
                ret = "Não Aprovado";
            else
                ret = "Aprovado";

            return ret;
        }

        public static TimeSheet GetTimeSheet(Chamada chamada)
        {
            TimeSheet ts;
            ts = new TimeSheet();
            ts.Inicialize();
            ts.IdPessoa.Id = chamada.IdPessoa.Id;
            ts.IdPessoaContato.Id = chamada.IdPessoaContato.Id;
            ts.DataInicio = chamada.DataInicio;
            ts.DataTermino = chamada.DataTermino;
            ts.Descricao = chamada.Descricao;
            return ts;
        }

        public static TimeSheet GetTimeSheet(Tarefa tarefa)
        {
            TimeSheet ts;
            ts = new TimeSheet();
            ts.Inicialize();
            ts.IdPessoa.Id = tarefa.IdPessoa.Id;
            ts.IdPessoaContato.Id = tarefa.IdPessoaContato.Id;
            ts.DataInicio = tarefa.DataInicio;
            ts.DataTermino = tarefa.DataConclusao;
            ts.Descricao = tarefa.Descricao;
            return ts;
        }

        public static TimeSheet GetTimeSheet(Compromisso compromisso)
        {
            TimeSheet ts;
            ts = new TimeSheet();
            ts.Inicialize();
            ts.IdPessoa.Id = compromisso.IdPessoa.Id;
            ts.IdPessoaContato.Id = compromisso.IdPessoaContato.Id;
            ts.DataInicio = compromisso.DataInicio;
            ts.DataTermino = compromisso.DataTermino;
            ts.Descricao = compromisso.Assunto + " " + compromisso.Descricao;
            return ts;
        }


        //public static TimeSheet GetTimeSheet(EmailReceive email)
        //{
        //    TimeSheet ts;
        //    ts = new TimeSheet();
        //    ts.Inicialize();
        //    ts.IdPessoa.Id = email.IdPessoa.Id;
        //    ts.IdPessoaContato.Id = email.IdPessoaContato.Id;
        //    ts.DataInicio = email.DataInicio;
        //    ts.DataTermino = email.DataConclusao;
        //    ts.Descricao = email.Descricao;
        //    return ts;
        //}

        public void VerificarPermissao(Usuario usuario)
        {
            if (this.IndStatusAprovacao == (int)TimeSheetStatus.Aprovado)
                throw new Exception("Este evento já está aprovado, não pode ser editado!");

            if (this.IdPessoa.Id != usuario.IdPessoa.Id)
                throw new Exception("Este evento só pode ser alterado por " + this.IdPessoa.ToString() + "!");
        }

        public static float GetQtdHoras(DateTime termino, DateTime inicio)
        {
            TimeSpan dif = termino.Subtract(inicio);

            return Convert.ToSingle(dif.TotalHours);
        }

        public float GetQtdHoras()
        {
            TimeSpan dif = this.DataTermino.Subtract(this.DataInicio);

            return Convert.ToSingle(dif.TotalHours);
        }

        public float GetValorTotal()
        {
            return this.ValorHora * this.GetQtdHoras();
        }

        public override int Save()
        {
            if(this.DataInicio >= DateTime.Now)
                throw new Exception("A data de início não ser maior que agora!");

            System.TimeSpan dif = this.DataInicio.Subtract(DateTime.Now);

            //if (this.Id == 0 && dif.Days < -14)
            //    throw new Exception("O TimeSheet não pode ser lançado com mais de duas semanas!");

            if (this.DataInicio >= this.DataTermino)
                throw new Exception("A data de início deve ser maior que a data de término!");

            if (!(this.DataInicio.Day == this.DataTermino.Day
                && this.DataInicio.Month == this.DataTermino.Month
                && this.DataInicio.Year == this.DataTermino.Year))
                throw new Exception("A data de início e a data de término devem estar no mesmo dia!");

            if (this.DataAprovacao == new DateTime() 
                && this.IndStatusAprovacao == (int)TimeSheetStatus.Aprovado)
                throw new Exception("Selecione a data de aprovação!");

            if (this.DataAprovacao != new DateTime()
                && this.IndStatusAprovacao != (int)TimeSheetStatus.Aprovado)
                throw new Exception("Remova a data de aprovação!");

            if (this.Descricao.ToUpper().IndexOf("ALMOÇO") != -1 && !this.IntervaloAlmoco)
                throw new Exception("Selecione a opção almoço!");

            if (this.Id == 0)
            {
                Prestador prestador = new Prestador();
                prestador.Find("IdPessoa=" + this.IdPessoa.Id);

                this.ValorHora = prestador.ValorHora;
            }

            return base.Save();
        }
	}
}
