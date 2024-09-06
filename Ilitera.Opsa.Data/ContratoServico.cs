using System;
using System.Data;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;


namespace Ilitera.Opsa.Data
{
	public enum UnidadeServico:int
	{
		Manual,
		EmpregadosAtivos,
		LocaisDeTrabalho
	}

	[Database("opsa","ContratoServico","IdContratoServico")]
	public class ContratoServico: Ilitera.Data.Table 
	{
		private int _IdContratoServico;
		private Contrato _IdContrato;
		private Cliente _IdCliente;
		private Servico _IdServico;
		private Indice _IdIndice;
        private DateTime _DataInicio = DateTime.Today;
		private int _MesReajuste;
		private bool _Automatica=true;
		private Cotacao _IdCotacaoAtual;
		private float _Valor;
		private int _IndUnidade=(int)UnidadeServico.Manual;
		private float _Quantidade=1.0F;
		private string _ObservacaoNota=string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ContratoServico()
		{

		}
		public override int Id
		{
			get{return _IdContratoServico;}
			set{_IdContratoServico = value;}
		}
		[Obrigatorio(true, "Contrato é campo obrigatório!")]
		public Contrato IdContrato
		{
			get{return _IdContrato;}
			set{_IdContrato = value;}
		}
		[Obrigatorio(true, "Cliente é campo obrigatório!")]
		public Cliente IdCliente
		{
			get{return _IdCliente;}
			set{_IdCliente = value;}
		}
		[Obrigatorio(true, "Serviço é campo obrigatório!")]
		public Servico IdServico
		{
			get{return _IdServico;}
			set{_IdServico = value;}
		}
		public Indice IdIndice
		{
			get{return _IdIndice;}
			set{_IdIndice = value;}
		}
        public DateTime DataInicio
        {
            get { return _DataInicio; }
            set { _DataInicio = value; }
        }
		public int MesReajuste
		{
			get{return _MesReajuste;}
			set{_MesReajuste = value;}
		}
		public bool Automatica
		{
			get{return _Automatica;}
			set{_Automatica = value;}
		}
		public Cotacao IdCotacaoAtual
		{
			get{return _IdCotacaoAtual;}
			set{_IdCotacaoAtual = value;}
		}
		public float Valor
		{
			get{return _Valor;}
			set{_Valor = value;}
		}
		public int IndUnidade
		{
			get{return _IndUnidade;}
			set{_IndUnidade = value;}
		}
		public float Quantidade
		{
			get{return _Quantidade;}
			set{_Quantidade = value;}
		}
		public string ObservacaoNota
		{
			get{return _ObservacaoNota;}
			set{_ObservacaoNota = value;}
		}

        public float QuantidadeUnidade()
        {
            float qdeEmpregados;

            if (this.IndUnidade == (int)UnidadeServico.EmpregadosAtivos)
                qdeEmpregados = this.IdCliente.GetEmpregadosAtivos();
            else if (this.IndUnidade == (int)UnidadeServico.LocaisDeTrabalho)
                qdeEmpregados = this.IdCliente.GetNumeroLocaisDeTrabalho();
            else
                qdeEmpregados = this.Quantidade;

            return qdeEmpregados;
        }

        public float ValorEmIndice()
        {
            return Convert.ToSingle(System.Math.Round(Convert.ToDouble(this.Valor / this.ValorCotacao()), 2));
        }

		public float ValorCotacao()
		{
			float cotacao;
			if(this.IdCotacaoAtual.Id!=0)
			{
				this.IdCotacaoAtual.Find();
				cotacao = this.IdCotacaoAtual.ValorCotacao;
			}
			else
			{
				Cotacao cotacaoIndice = new Cotacao();
				cotacaoIndice.FindMax("DataCotacao", "IdIndice="+this.IdIndice.Id);
				if(cotacaoIndice.Id!=0)
					cotacao = cotacaoIndice.ValorCotacao;
				else
					cotacao = 1;
			}
			return cotacao;
		}

		public float ValorTotal(float qtde)
		{
			float total;
			total =  this.Valor * qtde;
			total = Convert.ToSingle(System.Math.Round(Convert.ToDecimal(total), 2));
			return total;
		}

		public float ValorTotal()
		{
			return ValorTotal(QuantidadeUnidade());
		}

		public override int Save()
		{
            if (this.IdIndice.Id == (int)Indices.SM)
            {
                this.Automatica = true;
                this.MesReajuste = 0;
            }
            else
            {
                if (this.MesReajuste == 0)
                    this.MesReajuste = this.DataInicio.AddYears(1).AddMonths(-1).Month;
            }
			
            this.Valor = Convert.ToSingle(System.Math.Round(Convert.ToDecimal(this.Valor), 2));
			
            return base.Save ();
		}
	}
}
