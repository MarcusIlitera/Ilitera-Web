using System;
using System.Collections;
using Ilitera.Data;



namespace Ilitera.Common
{
	public enum RepetirTipo: int
	{
		Vezes,
		Semanalmente,
		DatasEspecificas
	}

	[Database("opsa","Repetir","IdRepetir")]
	public class Repetir: Ilitera.Data.Table
	{
		private int _IdRepetir;
		private Compromisso _IdCompromissoPai;
		private int _IndRepetir;
		private int _NumDiasVezes;
		private bool _IncluiFinalSemanaFeriado;
		private DateTime _De;
		private DateTime _Ate;
		private int _NumSemanas;
		private bool _Domingo;
		private bool _Segunda;
		private bool _Terca;
		private bool _Quarta;
		private bool _Quinta;
		private bool _Sexta;
		private bool _Sabado;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Repetir()
		{

		}
		public override int Id
		{
			get{return _IdRepetir;}
			set{_IdRepetir = value;}
		}
		public Compromisso IdCompromissoPai
		{
			get{return _IdCompromissoPai;}
			set{_IdCompromissoPai = value;}
		}
		public int IndRepetir
		{
			get{return _IndRepetir;}
			set{_IndRepetir = value;}
		}
		public int NumDiasVezes
		{
			get{return _NumDiasVezes;}
			set{_NumDiasVezes = value;}
		}
		public bool IncluiFinalSemanaFeriado
		{
			get{return _IncluiFinalSemanaFeriado;}
			set{_IncluiFinalSemanaFeriado = value;}
		}
		public DateTime De
		{
			get{return _De;}
			set{_De = value;}
		}
		public DateTime Ate
		{
			get{return _Ate;}
			set{_Ate = value;}
		}
		public int NumSemanas
		{
			get{return _NumSemanas;}
			set{_NumSemanas = value;}
		}
		public bool Domingo
		{
			get{return _Domingo;}
			set{_Domingo = value;}
		}
		public bool Segunda
		{
			get{return _Segunda;}
			set{_Segunda = value;}
		}
		public bool Terca
		{
			get{return _Terca;}
			set{_Terca = value;}
		}
		public bool Quarta
		{
			get{return _Quarta;}
			set{_Quarta = value;}
		}
		public bool Quinta
		{
			get{return _Quinta;}
			set{_Quinta = value;}
		}
		public bool Sexta
		{
			get{return _Sexta;}
			set{_Sexta = value;}
		}
		public bool Sabado
		{
			get{return _Sabado;}
			set{_Sabado = value;}
		}

		public override void Delete()
		{
			try
			{
				base.Delete ();
			}
			catch(Exception ex)
			{
				if(ex.Message.IndexOf("FK_Compromisso_Repetir")!=-1)
					throw new Exception("Compromisso possuí repetições, para excluir esse compromisso exclua a repetição.");
				else if(ex.Message.IndexOf("FK_PedidoGrupo_Compromisso")!=-1)
					throw new Exception("Compromisso relacionado com pedido, só pode ser excluído através do cadastro de pedidos");
				else if(ex.Message.IndexOf("FK_ExameBase_Compromisso")!=-1)
					throw new Exception("Compromisso relacionado com exame, só pode ser excluído através do cadastro de exames.");
				else
					throw ex;
			}
		}


		public void GerarRepeticoes(Compromisso compromisso)
		{
			if(compromisso.DataInicio==new DateTime())
				compromisso.Find();
			
			new Compromisso().Delete("IdCompromisso<>"+compromisso.Id+" AND IdRepetir="+this.Id);
			
			if(this.IndRepetir==(int)RepetirTipo.Vezes)
			{
				Municipio municipio = new Municipio();
				municipio.Find(98);//São Paulo
				int numVezes = this.NumDiasVezes;
				for(int i=1; i<=numVezes; i++)
				{
					DateTime dt = compromisso.DataInicio.AddDays(i);
					if(!this.IncluiFinalSemanaFeriado)
					{
						if(Feriado.IsFinalSemana(dt) || Feriado.IsFeriado(dt, municipio))
						{
							numVezes++;
							continue;
						}
					}
					RepeteCompromisso(compromisso, compromisso.IdPessoa, dt, compromisso.DataTermino.AddDays(i));
				}
			}
			else if(this.IndRepetir==(int)RepetirTipo.Semanalmente)
			{
				for(int i=1; i<=this.NumSemanas; i++)
					RepeteCompromisso(compromisso, compromisso.IdPessoa, compromisso.DataInicio.AddDays(i*7), compromisso.DataTermino.AddDays(i*7));
			}
			else if(this.IndRepetir==(int)RepetirTipo.DatasEspecificas)
			{
				ArrayList listDatas = new RepetirDatas().Find("IdRepetir="+this.Id);
				
				foreach(RepetirDatas datas in listDatas)
				{
					if(datas.IdPessoa!=null && datas.IdPessoa.Id!=0)
						RepeteCompromisso(compromisso, datas.IdPessoa, datas.Inicio, datas.Termino);
					else
						RepeteCompromisso(compromisso, compromisso.IdPessoa, datas.Inicio, datas.Termino);
				}
			}
		}

		private void RepeteCompromisso(Compromisso compromisso, Pessoa pessoa, DateTime Inicio, DateTime Termino)
		{
			Compromisso cr = new Compromisso();
			
			cr.DataInicio = Inicio;
			cr.DataTermino = Termino;
			cr.IdRepetir = this;
			cr.IdCategoria = compromisso.IdCategoria;
			cr.Assunto	 = compromisso.Assunto;
			cr.Descricao = compromisso.Descricao;
			cr.AConfirmar = compromisso.AConfirmar;
			cr.ColorName = compromisso.ColorName;
			cr.ODiaTodo	= compromisso.ODiaTodo;
			cr.HorarioDisponivel= compromisso.HorarioDisponivel;
			cr.IdPessoa	= pessoa;
			cr.IdPessoaContato = compromisso.IdPessoaContato;
			cr.IdPessoaCriador = compromisso.IdPessoaCriador;
            //cr.AvisoDataExecutar = compromisso.AvisoDataExecutar;
            //cr.AvisoIntervalo = compromisso.AvisoIntervalo;
            //cr.Dia	= compromisso.Dia;
            //cr.IndAntesDepois = compromisso.IndAntesDepois;
            //cr.IndAvisoPeriodicidade = compromisso.IndAvisoPeriodicidade;
            //cr.IndFeriado = compromisso.IndFeriado;
            //cr.IndFormaAviso = compromisso.IndFormaAviso;
            //cr.IndPeriodicidade = compromisso.IndPeriodicidade;
            //cr.IndPrazo = compromisso.IndPrazo;
            //cr.Mensagem = compromisso.Mensagem;
            //cr.Mes = compromisso.Mes;
            //cr.PrazoAte	= compromisso.PrazoAte;
            //cr.PrazoPorIndPeriodiciade = compromisso.PrazoPorIndPeriodiciade;
            //cr.ProzoPorIntervalo = compromisso.ProzoPorIntervalo;
            //cr.Domingo	= compromisso.Domingo;
            //cr.Segunda	= compromisso.Segunda;
            //cr.Terca	= compromisso.Terca;
            //cr.Quarta	= compromisso.Quarta;
            //cr.Quinta	= compromisso.Quinta;
            //cr.Sexta	= compromisso.Sexta;
            //cr.Sabado	= compromisso.Sabado;

			cr.Save();
		}
	}
}
