using System;
using System.Data;
using System.Collections;
using System.Text;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	public enum TipoProcedimento: int
	{
		Resumo,
		Especifico,
		Instrutivo
	}

    public enum GrauSeveridadeRisco: int
    {
        Marginal = 1,
        Critica,
        ExtremamentePrejudicial,
        Desprezivel
        //LevementePrejudicial = 1,
        //Prejudicial,
        //ExtremamentePrejudicial,
        //PrejuizoDesprezivel
    }

    public enum ProbabilidadeRisco : int
    {
        Desprezivel = 1,
        Moderado,
        Critico
        //Baixa = 1,
        //Media,
        //Alta
    }

    public enum GrauSeveridadeImpacto : int
    {
        Benefico = 1,
        AdversoMedio,
        AdversoAlto
    }

    public enum ProbabilidadeImpacto : int
    {
        Superior1Ano = 1,
        Superior1Mes,
        Inferior1Mes
    }

    public enum IndTipoSituacao : int
    {
        Normal = 1,
        Anormal,
        Emergencial
    }
	
	[Database("opsa", "AtualizacaoProcedimento", "IdAtualizacaoProcedimento", "", "atualização do procedimento na Ordem de Serviço")]
	public class AtualizacaoProcedimento : Ilitera.Data.Table
	{
		private int _IdAtualizacaoProcedimento;
		private Procedimento _IdProcedimento;
		private DateTime _Data = new DateTime();
		private string _Descricao = string.Empty;
		private Prestador _IdResponsavel;
		private Prestador _IdOperador;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AtualizacaoProcedimento()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AtualizacaoProcedimento(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get{return _IdAtualizacaoProcedimento;}
			set{_IdAtualizacaoProcedimento = value;}
		}
		public Procedimento IdProcedimento
		{
			get{return _IdProcedimento;}
			set{_IdProcedimento = value;}
		}
		public DateTime Data
		{
			get{return _Data;}
			set{_Data = value;}
		}
		[Obrigatorio(true, "A Descrição da Atualização do Procedimento é obrigatória!")]
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
		public Prestador IdResponsavel
		{
			get{return _IdResponsavel;}
			set{_IdResponsavel = value;}
		}
		public Prestador IdOperador
		{
			get{return _IdOperador;}
			set{_IdOperador = value;}
		}
	}

	[Database("opsa", "ConjuntoEmpregado", "IdConjuntoEmpregado")]
	public class ConjuntoEmpregado : Ilitera.Data.Table
	{
		private int	_IdConjuntoEmpregado;
		private Conjunto _IdConjunto;
		private Empregado _IdEmpregado;
		private DateTime _DataInicio = new DateTime();
		private DateTime _DataTermino = new DateTime();

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ConjuntoEmpregado()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ConjuntoEmpregado(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get{return _IdConjuntoEmpregado;}
			set{_IdConjuntoEmpregado = value;}
		}
		public Conjunto IdConjunto
		{
			get{return _IdConjunto;}
			set{_IdConjunto = value;}
		}
		public Empregado IdEmpregado
		{
			get{return _IdEmpregado;}
			set{_IdEmpregado = value;}
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
		public override string ToString()
		{
			return this.IdConjunto.ToString() +" - " + this.IdEmpregado.ToString();
		}

		public override int Save()
		{
			if (this.DataTermino != new DateTime() && this.DataTermino != new DateTime(1753, 1, 1))
			{
				if (this.DataInicio == new DateTime() || this.DataInicio == new DateTime(1753, 1, 1))
					throw new Exception("É necessário informar a Data de Início para o Conjunto de Procedimentos ou Procedimento!");
				if (this.DataInicio > this.DataTermino)
					throw new Exception("A Data de Término deve ser uma data maior do que a Data de Início!");
			}

			return base.Save();
		}

		public string GetDataInicio()
		{
			string DataInicio = string.Empty;
			
			if (this.DataInicio != new DateTime() && this.DataInicio != new DateTime(1753, 1, 1))
				DataInicio = this.DataInicio.ToString("dd-MM-yyyy");
			
			return DataInicio;
		}

		public string GetDataTermino()
		{
			string DataTermino = string.Empty;
			
			if (this.DataTermino != new DateTime() && this.DataTermino != new DateTime(1753, 1, 1))
				DataTermino = this.DataTermino.ToString("dd-MM-yyyy");
			
			return DataTermino;
		}
	}

	[Database("opsa", "ProcedimentoEmpregado", "IdProcedimentoEmpregado")]
	public class ProcedimentoEmpregado : Ilitera.Data.Table
	{
		private int	_IdProcedimentoEmpregado;
		private Procedimento _IdProcedimento;
		private Empregado _IdEmpregado;
		private DateTime _DataInicio = new DateTime();
		private DateTime _DataTermino = new DateTime();

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcedimentoEmpregado()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcedimentoEmpregado(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get{return _IdProcedimentoEmpregado;}
			set{_IdProcedimentoEmpregado = value;}
		}
		public Procedimento IdProcedimento
		{
			get{return _IdProcedimento;}
			set{_IdProcedimento = value;}
		}
		public Empregado IdEmpregado
		{
			get{return _IdEmpregado;}
			set{_IdEmpregado = value;}
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

		public string GetDataInicio()
		{
			string DataInicio = string.Empty;
			
			if (this.DataInicio != new DateTime() && this.DataInicio != new DateTime(1753, 1, 1))
				DataInicio = this.DataInicio.ToString("dd-MM-yyyy");
			
			return DataInicio;
		}

		public string GetDataTermino()
		{
			string DataTermino = string.Empty;
			
			if (this.DataTermino != new DateTime() && this.DataTermino != new DateTime(1753, 1, 1))
				DataTermino = this.DataTermino.ToString("dd-MM-yyyy");
			
			return DataTermino;
		}
	}

	[Database("opsa", "Conjunto", "IdConjunto", "", "conjunto de procedimentos na Ordem de Serviço")]
	public class Conjunto : Ilitera.Data.Table
	{
		private int _IdConjunto;
		private Cliente _IdCliente;
		private string _Nome = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Conjunto()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Conjunto(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get{return _IdConjunto;}
			set{_IdConjunto = value;}
		}
		public Cliente IdCliente
		{
			get{return _IdCliente;}
			set{_IdCliente = value;}
		}
		[Obrigatorio(true, "O Nome do conjunto de procedimentos é obrigatório!")]
		public string Nome
		{
			get{return _Nome;}
			set{_Nome = value;}
		}
		public override string ToString()
		{
			return this.Nome;
		}
	}

	[Database("opsa", "ConjuntoProcedimento", "IdConjuntoProcedimento")]
	public class ConjuntoProcedimento : Ilitera.Data.Table
	{
		private int	_IdConjuntoProcedimento;
		private Conjunto _IdConjunto;
		private Procedimento _IdProcedimento;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ConjuntoProcedimento()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ConjuntoProcedimento(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get{return _IdConjuntoProcedimento;}
			set{_IdConjuntoProcedimento = value;}
		}
		public Conjunto IdConjunto
		{
			get{return _IdConjunto;}
			set{_IdConjunto = value;}
		}
		public Procedimento IdProcedimento
		{
			get{return _IdProcedimento;}
			set{_IdProcedimento = value;}
		}
	}

	[Database("opsa", "Procedimento", "IdProcedimento", "", "procedimento na Ordem de Serviço")]
	public class Procedimento : Ilitera.Data.Table, Ilitera.Common.IFoto
	{
		private int	_IdProcedimento;
		private Cliente _IdCliente;
		private TipoProcedimento _IndTipoProcedimento;
		private Procedimento _IdProcedimentoResumo;		
		private short _Numero;
		private string _Nome = string.Empty;
		private string _Descricao = string.Empty;
		private string _Epc = string.Empty;
		private string _MedidasAdm = string.Empty;
		private string _MedidasEdu = string.Empty;
		private short _FotoNumero;
		private bool _isGenerico;
        private short _IndTipoIncidencia;
        private short _IndTipoSituacao;
        private Prestador _IdElaboradorAPP;
        private DateTime _DataElaboracaoAPP;
        private Prestador _IdRevisorAPP;
        private DateTime _DataRevisaoAPP;
        private string _Foto_Procedimento;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Procedimento()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Procedimento(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get{return _IdProcedimento;}
			set{_IdProcedimento = value;}
		}
		public Cliente IdCliente
		{
			get{return _IdCliente;}
			set{_IdCliente = value;}
		}
		public TipoProcedimento IndTipoProcedimento
		{
			get{return _IndTipoProcedimento;}
			set{_IndTipoProcedimento = value;}
		}
		public Procedimento IdProcedimentoResumo
		{
			get{return _IdProcedimentoResumo;}
			set{_IdProcedimentoResumo = value;}
		}
		public short Numero
		{
			get{return _Numero;}
			set{_Numero = value;}
		}
		[Obrigatorio(true, "O Nome do Procedimento é obrigatório!")]
		public string Nome
		{
			get{return _Nome;}
			set{_Nome = value;}
		}
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
		public string Epc
		{
			get{return _Epc;}
			set{_Epc = value;}
		}
		public string MedidasAdm
		{
			get{return _MedidasAdm;}
			set{_MedidasAdm = value;}
		}
		public string MedidasEdu
		{
			get{return _MedidasEdu;}
			set{_MedidasEdu = value;}
		}
        public short FotoNumero
		{
			get{return _FotoNumero;}
			set{_FotoNumero = value;}
		}
		public bool isGenerico
		{
			get{return _isGenerico;}
			set{_isGenerico = value;}
		}
        public short IndTipoIncidencia
        {
            get { return _IndTipoIncidencia; }
            set { _IndTipoIncidencia = value; }
        }
        public short IndTipoSituacao
        {
            get { return _IndTipoSituacao; }
            set { _IndTipoSituacao = value; }
        }
        public Prestador IdElaboradorAPP
        {
            get { return _IdElaboradorAPP; }
            set { _IdElaboradorAPP = value; }
        }
        public DateTime DataElaboracaoAPP
        {
            get { return _DataElaboracaoAPP; }
            set { _DataElaboracaoAPP = value; }
        }
        public Prestador IdRevisorAPP
        {
            get { return _IdRevisorAPP; }
            set { _IdRevisorAPP = value; }
        }
        public DateTime DataRevisaoAPP
        {
            get { return _DataRevisaoAPP; }
            set { _DataRevisaoAPP = value; }
        }
        public string Foto_Procedimento
        {
            get { return _Foto_Procedimento; }
            set { _Foto_Procedimento = value; }
        }


		//Interface Foto
		[Persist(false)]
		public string FotoDiretorio
		{
			get
			{
                if (this.IdCliente.mirrorOld == null)
                    this.IdCliente.Find();

                string sFotoDiretorioPadrao = this.IdCliente.GetFotoDiretorioPadrao();

                if (sFotoDiretorioPadrao.Trim().Equals(string.Empty))
					throw new Exception("A empresa não possui diretório padrão de fotos cadastrado! Por favor, entre em contato com a Ilitera!");

                return @"\" + sFotoDiretorioPadrao + @"\OrdemServico\";
			}
		}
		[Persist(false)]
		public string FotoInicio
		{
			get
			{
                if (this.IdCliente.mirrorOld == null)
                    this.IdCliente.Find();

				if (this.IdCliente.ArqFotoEmpregInicio.Trim().Equals(string.Empty))
					throw new Exception("A empresa não possui o padrão dos aquivos de fotos cadastrado! Por favor, entre em contato com a Ilitera.");
				
                return this.IdCliente.ArqFotoEmpregInicio;
			}
		}
		[Persist(false)]
		public string FotoTermino
		{
			get
			{
                if (this.IdCliente.mirrorOld == null)
                    this.IdCliente.Find();

                return this.IdCliente.ArqFotoEmpregTermino;
			}
		}
		[Persist(false)]
		public byte FotoTamanho
		{
			get
			{
				return 3;
			}
		}
		[Persist(false)]
		public string FotoExtensao
		{
			get
			{
                if (this.IdCliente.mirrorOld == null)
                    this.IdCliente.Find();

                return this.IdCliente.ArqFotoEmpregExtensao;
			}
		}

		public override int Save()
		{
			if (this.Numero == 0)
				this.Numero = GeraNumeroPOPS();
			
			return base.Save();
		}

		private short GeraNumeroPOPS()
		{
			Procedimento procedimento = new Procedimento();
			procedimento.FindMax("Numero", "IdCliente=" + this.IdCliente.Id);
			
			return Convert.ToInt16(procedimento.Numero + 1);
		}
	
		public Procedimento GetNewProcedimento(int IdUsuario)
		{
			return GetNewProcedimento(IdUsuario, this.IdCliente.Id);
		}

		public Procedimento GetNewProcedimento(int IdUsuario, int IdCliente)
		{			
			Procedimento newProcedimento = new Procedimento();
			newProcedimento.Inicialize();
			newProcedimento.IdCliente.Id = IdCliente;
			newProcedimento.Nome = this.Nome;
			newProcedimento.Descricao = this.Descricao;

            if (this.IndTipoProcedimento == TipoProcedimento.Resumo)
            {
                newProcedimento.Epc = this.Epc;
                newProcedimento.MedidasAdm = this.MedidasAdm;
                newProcedimento.MedidasEdu = this.MedidasEdu;
            }

			newProcedimento.IdProcedimentoResumo = this.IdProcedimentoResumo;
			newProcedimento.IndTipoProcedimento = this.IndTipoProcedimento;
            newProcedimento.IndTipoIncidencia = this.IndTipoIncidencia;
            newProcedimento.IndTipoSituacao = this.IndTipoSituacao;
			newProcedimento.FotoNumero = this.FotoNumero;
			newProcedimento.UsuarioId = IdUsuario;
			newProcedimento.Save();

			//ProcedimentoSetor
			ArrayList alProcedimentoSetor = new ProcedimentoSetor().Find("IdProcedimento=" + this.Id);
			
			foreach (ProcedimentoSetor procedimentoSetor in alProcedimentoSetor)
			{
				ProcedimentoSetor newProcedimentoSetor = new ProcedimentoSetor();
				newProcedimentoSetor.Inicialize();
				
				newProcedimentoSetor.IdProcedimento = newProcedimento;
				newProcedimentoSetor.IdSetor = procedimentoSetor.IdSetor;
				newProcedimentoSetor.UsuarioId = IdUsuario;
				newProcedimentoSetor.Save();
			}

            //ProcedimentoCelula
            ArrayList alProcedimentoCelula = new ProcedimentoCelula().Find("IdProcedimento=" + this.Id);

            foreach (ProcedimentoCelula procedimentoCelula in alProcedimentoCelula)
            {
                ProcedimentoCelula newProcedimentoCelula = new ProcedimentoCelula();
                newProcedimentoCelula.Inicialize();

                newProcedimentoCelula.IdProcedimento = newProcedimento;
                newProcedimentoCelula.IdCelula = procedimentoCelula.IdCelula;
                newProcedimentoCelula.UsuarioId = IdUsuario;
                newProcedimentoCelula.Save();
            }

            if (this.IndTipoProcedimento == TipoProcedimento.Resumo)
            {
                //ProcedimentoEquipamento
                ArrayList alProcedimentoEquipamento = new ProcedimentoEquipamento().Find("IdProcedimento=" + this.Id);

                foreach (ProcedimentoEquipamento procedimentoEquipamento in alProcedimentoEquipamento)
                {
                    ProcedimentoEquipamento newProcedimentoEquipamento = new ProcedimentoEquipamento();
                    newProcedimentoEquipamento.Inicialize();

                    newProcedimentoEquipamento.IdProcedimento = newProcedimento;
                    newProcedimentoEquipamento.IdEquipamento = procedimentoEquipamento.IdEquipamento;
                    newProcedimentoEquipamento.UsuarioId = IdUsuario;
                    newProcedimentoEquipamento.Save();
                }

                //ProcedimentoFerramenta
                ArrayList alProcedimentoFerramenta = new ProcedimentoFerramenta().Find("IdProcedimento=" + this.Id);

                foreach (ProcedimentoFerramenta procedimentoFerramenta in alProcedimentoFerramenta)
                {
                    ProcedimentoFerramenta newProcedimentoFerramenta = new ProcedimentoFerramenta();
                    newProcedimentoFerramenta.Inicialize();

                    newProcedimentoFerramenta.IdProcedimento = newProcedimento;
                    newProcedimentoFerramenta.IdFerramenta = procedimentoFerramenta.IdFerramenta;
                    newProcedimentoFerramenta.UsuarioId = IdUsuario;
                    newProcedimentoFerramenta.Save();
                }

                //ProcedimentoProduto
                ArrayList alProcedimentoProduto = new ProcedimentoProduto().Find("IdProcedimento=" + this.Id);

                foreach (ProcedimentoProduto procedimentoProduto in alProcedimentoProduto)
                {
                    ProcedimentoProduto newProcedimentoProduto = new ProcedimentoProduto();
                    newProcedimentoProduto.Inicialize();

                    newProcedimentoProduto.IdProcedimento = newProcedimento;
                    newProcedimentoProduto.IdProduto = procedimentoProduto.IdProduto;
                    newProcedimentoProduto.UsuarioId = IdUsuario;
                    newProcedimentoProduto.Save();
                }

                //ProcedimentoEPI
                ArrayList alProcedimentoEPI = new ProcedimentoEPI().Find("IdProcedimento=" + this.Id);

                foreach (ProcedimentoEPI procedimentoEPI in alProcedimentoEPI)
                {
                    ProcedimentoEPI newProcedimentoEPI = new ProcedimentoEPI();
                    newProcedimentoEPI.Inicialize();

                    newProcedimentoEPI.IdProcedimento = newProcedimento;
                    newProcedimentoEPI.IdEpi = procedimentoEPI.IdEpi;
                    newProcedimentoEPI.UsuarioId = IdUsuario;
                    newProcedimentoEPI.Save();
                }
            }

			//Operacao
			ArrayList alOperacao  = new Operacao().Find("IdProcedimento=" + this.Id + " ORDER BY Sequencia");
					
			foreach (Operacao operacao in alOperacao)
			{
				Operacao newOperacao = new Operacao();
				newOperacao.Inicialize();
			
				newOperacao.IdProcedimento = newProcedimento;
				newOperacao.Sequencia = operacao.Sequencia;
				newOperacao.Descricao = operacao.Descricao;
				newOperacao.Precaucoes = operacao.Precaucoes;
				newOperacao.Save();

                if (this.IndTipoProcedimento == TipoProcedimento.Resumo)
                {
                    //OperacaoPerigo
                    ArrayList alOperacaoPerigo = new OperacaoPerigo().Find("IdOperacao=" + operacao.Id);

                    foreach (OperacaoPerigo operacaoPerigo in alOperacaoPerigo)
                    {
                        OperacaoPerigo newOperacaoPerigo = new OperacaoPerigo();
                        newOperacaoPerigo.Inicialize();

                        newOperacaoPerigo.IdOperacao = newOperacao;
                        newOperacaoPerigo.IdPerigo = operacaoPerigo.IdPerigo;
                        newOperacaoPerigo.UsuarioId = IdUsuario;
                        newOperacaoPerigo.Save();

                        //OperacaoPerigoRisco
                        ArrayList alOperacaoPerigoRisco = new OperacaoPerigoRiscoAcidente().Find("IdOperacaoPerigo=" + operacaoPerigo.Id);

                        foreach (OperacaoPerigoRiscoAcidente operacaoPerigoRisco in alOperacaoPerigoRisco)
                        {
                            OperacaoPerigoRiscoAcidente newOperacaoPerigoRisco = new OperacaoPerigoRiscoAcidente();
                            newOperacaoPerigoRisco.Inicialize();

                            newOperacaoPerigoRisco.IdOperacaoPerigo = newOperacaoPerigo;
                            newOperacaoPerigoRisco.IdRiscoAcidente = operacaoPerigoRisco.IdRiscoAcidente;
                            newOperacaoPerigoRisco.IndGrauSeveridadeRisco = operacaoPerigoRisco.IndGrauSeveridadeRisco;
                            newOperacaoPerigoRisco.IndProbabilidadeRisco = operacaoPerigoRisco.IndProbabilidadeRisco;
                            newOperacaoPerigoRisco.UsuarioId = IdUsuario;
                            newOperacaoPerigoRisco.Save();
                        }
                    }

                    //OperacaoAspecto
                    ArrayList alOperacaoAspecto = new OperacaoAspectoAmbiental().Find("IdOperacao=" + operacao.Id);

                    foreach (OperacaoAspectoAmbiental operacaoAspecto in alOperacaoAspecto)
                    {
                        OperacaoAspectoAmbiental newOperacaoAspecto = new OperacaoAspectoAmbiental();
                        newOperacaoAspecto.Inicialize();

                        newOperacaoAspecto.IdOperacao = newOperacao;
                        newOperacaoAspecto.IdAspectoAmbiental = operacaoAspecto.IdAspectoAmbiental;
                        newOperacaoAspecto.UsuarioId = IdUsuario;
                        newOperacaoAspecto.Save();

                        //OperacaoAspectoImpacto
                        ArrayList alOperacaoAspectoImpacto = new OperacaoAspectoAmbientalImpacto().Find("IdOperacaoAspectoAmbiental=" + operacaoAspecto.Id);

                        foreach (OperacaoAspectoAmbientalImpacto operacaoAspectoImpacto in alOperacaoAspectoImpacto)
                        {
                            OperacaoAspectoAmbientalImpacto newOperacaoAspectoImpacto = new OperacaoAspectoAmbientalImpacto();
                            newOperacaoAspectoImpacto.Inicialize();

                            newOperacaoAspectoImpacto.IdOperacaoAspectoAmbiental = newOperacaoAspecto;
                            newOperacaoAspectoImpacto.IdImpactoAmbiental = operacaoAspectoImpacto.IdImpactoAmbiental;
                            newOperacaoAspectoImpacto.IndGrauSeveridadeImpacto = operacaoAspectoImpacto.IndGrauSeveridadeImpacto;
                            newOperacaoAspectoImpacto.IndProbabilidadeImpacto = operacaoAspectoImpacto.IndProbabilidadeImpacto;
                            newOperacaoAspectoImpacto.UsuarioId = IdUsuario;
                            newOperacaoAspectoImpacto.Save();
                        }
                    }
                }
			}
			
			//AtualizacaoProcedimento
			ArrayList alAtualizacaoProcedimento = new AtualizacaoProcedimento().Find("IdProcedimento=" + this.Id);
			
			foreach (AtualizacaoProcedimento atualizacaoProcedimento in alAtualizacaoProcedimento)
			{
				AtualizacaoProcedimento newAtualizacaoProcedimento = new AtualizacaoProcedimento();
				newAtualizacaoProcedimento.Inicialize();
				
				newAtualizacaoProcedimento.IdProcedimento = newProcedimento;
				newAtualizacaoProcedimento.Data = atualizacaoProcedimento.Data;
				newAtualizacaoProcedimento.Descricao = atualizacaoProcedimento.Descricao;
				newAtualizacaoProcedimento.IdResponsavel = atualizacaoProcedimento.IdResponsavel;
				newAtualizacaoProcedimento.IdOperador = atualizacaoProcedimento.IdOperador;

				newAtualizacaoProcedimento.UsuarioId = 0;
				newAtualizacaoProcedimento.Save();
			}

			return newProcedimento;
		}

        public string FotoProcedimento()
        {
            string ret = string.Empty;

            if (this.FotoNumero == 0)
                return ret;

            try
            {
                string path = Fotos.PathFoto_Uri(this, this.FotoNumero);

                ret = Fotos.PathFoto_Uri(path);
            }
            catch{}

            return ret;
        }	

		public string FotoProcedimentoUrl()
		{
            string ret = string.Empty;

            if (this.FotoNumero == 0)
                return string.Empty;

			try
			{
				string path = "/" + this.FotoDiretorio + Fotos.GetFileName(this, this.FotoNumero);

				ret = Fotos.UrlFoto(path);
			}
			catch{}

            return ret;
		}	

		public string strEpi()
		{
			StringBuilder str = new StringBuilder();

			bool firstTime = true;

            ArrayList alEpi = new Epi().Find("IdEpi IN (SELECT IdEpi FROM ProcedimentoEPI WHERE IdProcedimento=" + this.Id + ") order by Descricao");
			
			foreach (Epi epi in alEpi)
			{
				if (!firstTime)
					str.Append(", ");

                ProcedimentoEPI zProcEPI = new ProcedimentoEPI();
                zProcEPI.Find(" IdProcedimento = " + this.Id + " and IdEpi = " + epi.Id + " ");

                if (zProcEPI.Condicao != null) str.Append(epi.ToString() + zProcEPI.Condicao);
                else 	  			           str.Append(epi.ToString());

				firstTime = false;
			}

			return str.ToString();
		}

        public string GetSetores()
        {
            StringBuilder sbSetores = new StringBuilder();
            
            ArrayList alSetor = new Setor().Find("nID_SETOR IN (SELECT IdSetor FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.ProcedimentoSetor WHERE IdProcedimento=" + this.Id
                + ") ORDER BY tNO_STR_EMPR");

            foreach (Setor setor in alSetor)
            {
                if (!sbSetores.ToString().Equals(string.Empty))
                    sbSetores.Append(", ");

                sbSetores.Append(setor.tNO_STR_EMPR);
            }

            return sbSetores.ToString();
        }

        public string GetEquipamentos()
        {
            StringBuilder sbEquipamentos = new StringBuilder();

            ArrayList alEquipamento = new Equipamento().Find("IdEquipamento IN (SELECT IdEquipamento FROM ProcedimentoEquipamento WHERE IdProcedimento=" + this.Id
                + ") ORDER BY Nome");

            foreach (Equipamento equipamento in alEquipamento)
            {
                if (!sbEquipamentos.ToString().Equals(string.Empty))
                    sbEquipamentos.Append(", ");

                sbEquipamentos.Append(equipamento.Nome);
            }

            return sbEquipamentos.ToString();
        }
	}

	[Database("opsa", "Operacao", "IdOperacao", "", "operação do procedimento na Ordem de Serviço")]
	public class Operacao : Ilitera.Data.Table
	{
		private int	_IdOperacao;
		private Procedimento _IdProcedimento;
		private short _Sequencia;
		private string _Descricao;
		private string _Precaucoes;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Operacao()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Operacao(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get{return _IdOperacao;}
			set{_IdOperacao = value;}
		}
		public Procedimento IdProcedimento
		{
			get{return _IdProcedimento;}
			set{_IdProcedimento = value;}
		}
		public short Sequencia
		{
			get{return _Sequencia;}
			set{_Sequencia = value;}
		}
		[Obrigatorio(true, "A Descrição da operação é obrigatória!")]
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
		public string Precaucoes
		{
			get{return _Precaucoes;}
			set{_Precaucoes = value;}
		}

		public override string ToString()
		{
			return this.Descricao;
		}

		public override int Save()
		{
			if(this.Id==0 || this.Sequencia == 0)
			{
				Operacao operacao = new Operacao();
				operacao.FindMax("Sequencia", "IdProcedimento=" + this.IdProcedimento.Id);
				this.Sequencia = ++operacao.Sequencia;
			}
			return base.Save();
		}

		public override void Delete()
		{
			ArrayList alOperacao = new Operacao().Find("IdProcedimento=" + this.IdProcedimento.Id
				+" AND Sequencia>" + this.Sequencia);

			foreach (Operacao operacaoSeq in alOperacao)
			{
				operacaoSeq.Sequencia -= 1;
				operacaoSeq.Save();
			}
			
			base.Delete();
		}
		
		public string strAcidentes()
		{
            DataSet dsRiscos = new RiscoAcidente().Get("IdRiscoAcidente IN (SELECT IdRiscoAcidente FROM OperacaoRiscoAcidente"
                + " WHERE IdOperacao=" + this.Id + ")"
                + " OR IdRiscoAcidente IN (SELECT IdRiscoAcidente FROM OperacaoPerigoRiscoAcidente"
                + " WHERE IdOperacaoPerigo IN (SELECT IdOperacaoPerigo FROM OperacaoPerigo WHERE IdOperacao=" + this.Id + "))"
                + " ORDER BY Nome");

			StringBuilder str = new StringBuilder();

			foreach (DataRow rowRisco in dsRiscos.Tables[0].Select())
			{
                if (!str.ToString().Equals(string.Empty))
                    str.Append(", ");
                
                str.Append(rowRisco["Nome"].ToString());
			}

			return str.ToString();
		}
	}

	[Database("opsa", "RiscoAcidente", "IdRiscoAcidente", "", "risco de acidente na Ordem de Serviço")]
	public class RiscoAcidente : Ilitera.Data.Table
	{
		private int	_IdRiscoAcidente;
		private Cliente _IdCliente;
		private string _Nome = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public RiscoAcidente()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public RiscoAcidente(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get{return _IdRiscoAcidente;}
			set{_IdRiscoAcidente = value;}
		}
		public Cliente IdCliente
		{
			get{return _IdCliente;}
			set{_IdCliente = value;}
		}
		[Obrigatorio(true, "O Nome do Risco é obrigatório!")]
		public string Nome
		{
			get{return _Nome;}
			set{_Nome = value;}
		}
	}

	[Database("opsa", "OperacaoRiscoAcidente", "IdOperacaoRiscoAcidente")]
	public class OperacaoRiscoAcidente : Ilitera.Data.Table
	{
		private int	_IdOperacaoRiscoAcidente;
		private Operacao _IdOperacao;
		private RiscoAcidente _IdRiscoAcidente;
		private short _GrauSeveridade;
		private short _GrauFrequencia;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public OperacaoRiscoAcidente()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public OperacaoRiscoAcidente(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get{return _IdOperacaoRiscoAcidente;}
			set{_IdOperacaoRiscoAcidente = value;}
		}
		public Operacao IdOperacao
		{
			get{return _IdOperacao;}
			set{_IdOperacao = value;}
		}
		public RiscoAcidente IdRiscoAcidente
		{
			get{return _IdRiscoAcidente;}
			set{_IdRiscoAcidente = value;}
		}
		public short GrauSeveridade
		{
			get{return _GrauSeveridade;}
			set{_GrauSeveridade = value;}
		}
		public short GrauFrequencia
		{
			get{return _GrauFrequencia;}
			set{_GrauFrequencia = value;}
		}
	}

	[Database("opsa", "ProcedimentoEPI", "IdProcedimentoEPI")]
	public class ProcedimentoEPI : Ilitera.Data.Table
	{
		private int _IdProcedimentoEPI;
		private Procedimento _IdProcedimento;
		private Epi _IdEpi;
        private string _Condicao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcedimentoEPI()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcedimentoEPI(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{														  
			get	{return _IdProcedimentoEPI;}
			set {_IdProcedimentoEPI = value;}
		}
		public Procedimento IdProcedimento
		{														  
			get	{return _IdProcedimento;}
			set {_IdProcedimento = value;}
		}
		public Epi IdEpi
		{														  
			get	{return _IdEpi;}
			set {_IdEpi = value;}
		}
        public string Condicao
        {
            get { return _Condicao; }
            set { _Condicao = value; }
        }
	}

	[Database("opsa", "Produto", "IdProduto", "", "produto na Ordem de Serviço")]
	public class Produto : Ilitera.Data.Table
	{
		private int _IdProduto;
		private Cliente _IdCliente;
		private string _Nome = string.Empty;
		private string _Descricao = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Produto()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Produto(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get {return _IdProduto;}
			set {_IdProduto = value;}
		}
		public Cliente IdCliente 
		{
			get {return _IdCliente;}
			set {_IdCliente = value;}
		}
		[Obrigatorio(true, "O Nome do Produto é obrigatório!")]
		public string Nome 
		{
			get {return _Nome;}
			set {_Nome = value;}
		}
		public string Descricao 
		{
			get {return _Descricao;}
			set {_Descricao = value;}
		}
	}

	[Database("opsa", "Equipamento", "IdEquipamento", "", "equipamento na Ordem de Serviço")]
	public class Equipamento : Ilitera.Data.Table
	{
		private int _IdEquipamento;
		private Cliente _IdCliente;
		private string _Nome = string.Empty;
		private string _Descricao = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Equipamento()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Equipamento(int Id)
		{
			Find(Id);
		}
		public override int Id
		{
			get {return _IdEquipamento;}
			set {_IdEquipamento = value;}
		}
		public Cliente IdCliente 
		{
			get {return _IdCliente;}
			set {_IdCliente = value;}
		}
		[Obrigatorio(true, "O Nome do Equipamento é obrigatório!")]
		public string Nome
		{
			get {return _Nome;}
			set {_Nome = value;}
		}
		public string Descricao
		{
			get {return _Descricao;}
			set {_Descricao = value;}
		}
	}

	[Database("opsa", "Ferramenta", "IdFerramenta", "", "ferramenta na Ordem de Serviço")]
	public class Ferramenta : Ilitera.Data.Table
	{
		private int _IdFerramenta;
		private Cliente _IdCliente;
		private string _Nome = string.Empty;
		private string _Descricao = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Ferramenta()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Ferramenta(int Id)
		{
			this.Find(Id);	
		}
		public override int Id
		{
			get {return _IdFerramenta;}
			set {_IdFerramenta = value;}
		}
		public Cliente IdCliente 
		{
			get {return _IdCliente;}
			set {_IdCliente = value;}
		}
		[Obrigatorio(true, "O Nome da Ferramenta é obrigatório!")]
		public string Nome
		{
			get {return _Nome;}
			set {_Nome = value;}
		}
		public string Descricao
		{
			get {return _Descricao;}
			set {_Descricao = value;}
		}
	}

	[Database("opsa", "Celula", "IdCelula", "", "celula na Ordem de Serviço")]
	public class Celula : Ilitera.Data.Table
	{
		private int _IdCelula;
		private Cliente _IdCliente;
		private string _Nome = string.Empty;
		private string _Descricao = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Celula()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Celula(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get {return _IdCelula;}
			set {_IdCelula = value;}
		}
		public Cliente IdCliente 
		{
			get {return _IdCliente;}
			set {_IdCliente = value;}
		}
		[Obrigatorio(true, "O Nome da Célula é obrigatório!")]
		public string Nome
		{
			get {return _Nome;}
			set {_Nome = value;}
		}
		public string Descricao
		{
			get {return _Descricao;}
			set {_Descricao = value;}
		}
	}

	[Database("opsa", "ProcedimentoCelula", "IdProcedimentoCelula")]
	public class ProcedimentoCelula : Ilitera.Data.Table
	{
		private int _IdProcedimentoCelula;
		private Procedimento _IdProcedimento;
		private Celula _IdCelula;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcedimentoCelula()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcedimentoCelula(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get {return _IdProcedimentoCelula;}
			set {_IdProcedimentoCelula = value;}
		}
		public Procedimento IdProcedimento
		{
			get {return _IdProcedimento;}
			set {_IdProcedimento = value;}
		}
		public Celula IdCelula
		{
			get {return _IdCelula;}
			set {_IdCelula = value;}
		}
	}

	[Database("opsa", "ProcedimentoProduto", "IdProcedimentoProduto")]
	public class ProcedimentoProduto : Ilitera.Data.Table
	{
		private int _IdProcedimentoProduto;
		private Procedimento _IdProcedimento;
		private Produto _IdProduto;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcedimentoProduto()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcedimentoProduto(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get {return _IdProcedimentoProduto;}
			set {_IdProcedimentoProduto = value;}
		}
		public Procedimento IdProcedimento
		{
			get {return _IdProcedimento;}
			set {_IdProcedimento = value;}
		}
		public Produto IdProduto
		{
			get {return _IdProduto;}
			set {_IdProduto = value;}
		}
	}

	[Database("opsa", "ProcedimentoSetor", "IdProcedimentoSetor")]
	public class ProcedimentoSetor : Ilitera.Data.Table
	{
		private int _IdProcedimentoSetor;
		private Procedimento _IdProcedimento;
		private Setor _IdSetor;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcedimentoSetor()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcedimentoSetor(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get {return _IdProcedimentoSetor;}
			set	{_IdProcedimentoSetor = value;}
		}
		public Procedimento IdProcedimento
		{
			get	{return _IdProcedimento;}
			set	{_IdProcedimento = value;}
		}
		public Setor IdSetor
		{
			get {return _IdSetor;}
			set	{_IdSetor = value;}
		}
	}

	[Database("opsa", "ProcedimentoEquipamento", "IdProcedimentoEquipamento")]
	public class ProcedimentoEquipamento: Ilitera.Data.Table
	{
		private int _IdProcedimentoEquipamento;
		private Procedimento _IdProcedimento;
		private Equipamento _IdEquipamento;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcedimentoEquipamento()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcedimentoEquipamento(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get {return _IdProcedimentoEquipamento;}
			set {_IdProcedimentoEquipamento = value;}
		}
		public Procedimento IdProcedimento
		{
			get {return _IdProcedimento;}
			set {_IdProcedimento = value;}
		}
		public Equipamento IdEquipamento
		{
			get {return _IdEquipamento;}
			set {_IdEquipamento = value;}
		}
	}

	[Database("opsa", "ProcedimentoFerramenta", "IdProcedimentoFerramenta")]
	public class ProcedimentoFerramenta: Ilitera.Data.Table
	{
		private int _IdProcedimentoFerramenta;
		private Procedimento _IdProcedimento;
		private Ferramenta _IdFerramenta;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcedimentoFerramenta()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProcedimentoFerramenta(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
			get {return _IdProcedimentoFerramenta;}
			set {_IdProcedimentoFerramenta = value;}
		}
		public Procedimento IdProcedimento
		{
			get {return _IdProcedimento;}
			set {_IdProcedimento = value;}
		}
		public Ferramenta IdFerramenta
		{
			get {return _IdFerramenta;}
			set {_IdFerramenta = value;}
		}
	}

    [Database("opsa", "Perigo", "IdPerigo", "", "Perigo na Ordem de Serviço")]
    public class Perigo : Ilitera.Data.Table
    {
        private int _IdPerigo;
        private Cliente _IdCliente;
        private string _Nome;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Perigo()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Perigo(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdPerigo; }
            set { _IdPerigo = value; }
        }
        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        [Obrigatorio(true, "O Nome do Perigo é obrigatório!")]
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }
    }

    [Database("opsa", "OperacaoPerigo", "IdOperacaoPerigo")]
    public class OperacaoPerigo : Ilitera.Data.Table
    {
        private int _IdOperacaoPerigo;
        private Operacao _IdOperacao;
        private Perigo _IdPerigo;
        private string _Precaucoes;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public OperacaoPerigo()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public OperacaoPerigo(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdOperacaoPerigo; }
            set { _IdOperacaoPerigo = value; }
        }
        public Operacao IdOperacao
        {
            get { return _IdOperacao; }
            set { _IdOperacao = value; }
        }
        public Perigo IdPerigo
        {
            get { return _IdPerigo; }
            set { _IdPerigo = value; }
        }
        public string Precaucoes
        {
            get { return _Precaucoes; }
            set { _Precaucoes = value; }
        }

    }

    [Database("opsa", "PerigoRiscoAcidente", "IdPerigoRiscoAcidente")]
    public class PerigoRiscoAcidente : Ilitera.Data.Table
    {
        private int _IdPerigoRiscoAcidente;
        private Perigo _IdPerigo;
        private RiscoAcidente _IdRiscoAcidente;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PerigoRiscoAcidente()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public PerigoRiscoAcidente(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdPerigoRiscoAcidente; }
            set { _IdPerigoRiscoAcidente = value; }
        }
        public Perigo IdPerigo
        {
            get { return _IdPerigo; }
            set { _IdPerigo = value; }
        }
        public RiscoAcidente IdRiscoAcidente
        {
            get { return _IdRiscoAcidente; }
            set { _IdRiscoAcidente = value; }
        }
    }

    [Database("opsa", "OperacaoPerigoRiscoAcidente", "IdOperacaoPerigoRiscoAcidente")]
    public class OperacaoPerigoRiscoAcidente : Ilitera.Data.Table
    {
        private int _IdOperacaoPerigoRiscoAcidente;
        private OperacaoPerigo _IdOperacaoPerigo;
        private RiscoAcidente _IdRiscoAcidente;
        private GrauSeveridadeRisco _IndGrauSeveridadeRisco;
        private ProbabilidadeRisco _IndProbabilidadeRisco;
        private string _Epc;
        private string _MedidasAdm;
        private string _MedidasEdu;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public OperacaoPerigoRiscoAcidente()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public OperacaoPerigoRiscoAcidente(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdOperacaoPerigoRiscoAcidente; }
            set { _IdOperacaoPerigoRiscoAcidente = value; }
        }
        public OperacaoPerigo IdOperacaoPerigo
        {
            get { return _IdOperacaoPerigo; }
            set { _IdOperacaoPerigo = value; }
        }
        public RiscoAcidente IdRiscoAcidente
        {
            get { return _IdRiscoAcidente; }
            set { _IdRiscoAcidente = value; }
        }
        public GrauSeveridadeRisco IndGrauSeveridadeRisco
        {
            get { return _IndGrauSeveridadeRisco; }
            set { _IndGrauSeveridadeRisco = value; }
        }
        public ProbabilidadeRisco IndProbabilidadeRisco
        {
            get { return _IndProbabilidadeRisco; }
            set { _IndProbabilidadeRisco = value; }
        }
        public string Epc
        {
            get { return _Epc; }
            set { _Epc = value; }
        }
        public string MedidasAdm
        {
            get { return _MedidasAdm; }
            set { _MedidasAdm = value; }
        }
        public string MedidasEdu
        {
            get { return _MedidasEdu; }
            set { _MedidasEdu = value; }
        }

        public string GetIndiceProbabilidade()
        {
            string indiceProbabilidade = string.Empty;

            switch (this.IndProbabilidadeRisco)
            {
                case ProbabilidadeRisco.Desprezivel:
                    indiceProbabilidade = "1";
                    break;
                case ProbabilidadeRisco.Moderado:
                    indiceProbabilidade = "2";
                    break;
                case ProbabilidadeRisco.Critico:
                    indiceProbabilidade = "3";
                    break;
            }

            return indiceProbabilidade;
        }

        public string GetIndiceSeveridade()
        {
            string indiceSeveridade = string.Empty;

            switch (this.IndGrauSeveridadeRisco)
            {
                case GrauSeveridadeRisco.Marginal:
                    indiceSeveridade = "1";
                    break;
                case GrauSeveridadeRisco.Critica:
                    indiceSeveridade = "2";
                    break;
                case GrauSeveridadeRisco.ExtremamentePrejudicial:
                    indiceSeveridade = "3";
                    break;
                case GrauSeveridadeRisco.Desprezivel:
                    indiceSeveridade = "4";
                    break;

            }

            return indiceSeveridade;
        }

        public string GetIndiceGrauRisco()
        {
            string grauRisco = string.Empty;

            switch (this.IndProbabilidadeRisco)
            {
                case ProbabilidadeRisco.Desprezivel:
                    switch (this.IndGrauSeveridadeRisco)
                    {
                        case GrauSeveridadeRisco.Marginal:
                            grauRisco = "1";
                            break;
                        case GrauSeveridadeRisco.Critica:
                            grauRisco = "2";
                            break;
                        case GrauSeveridadeRisco.ExtremamentePrejudicial:
                            grauRisco = "3";
                            break;
                        case GrauSeveridadeRisco.Desprezivel:
                            grauRisco = "1";
                            break;
                    }
                    break;
                case ProbabilidadeRisco.Moderado:
                    switch (this.IndGrauSeveridadeRisco)
                    {
                        case GrauSeveridadeRisco.Marginal:
                            grauRisco = "2";
                            break;
                        case GrauSeveridadeRisco.Critica:
                            grauRisco = "3";
                            break;
                        case GrauSeveridadeRisco.ExtremamentePrejudicial:
                            grauRisco = "4";
                            break;
                        case GrauSeveridadeRisco.Desprezivel:
                            grauRisco = "1";
                            break;

                    }
                    break;
                case ProbabilidadeRisco.Critico:
                    switch (this.IndGrauSeveridadeRisco)
                    {
                        case GrauSeveridadeRisco.Marginal:
                            grauRisco = "3";
                            break;
                        case GrauSeveridadeRisco.Critica:
                            grauRisco = "4";
                            break;
                        case GrauSeveridadeRisco.ExtremamentePrejudicial:
                            grauRisco = "5";
                            break;
                        case GrauSeveridadeRisco.Desprezivel:
                            grauRisco = "1";
                            break;

                    }
                    break;
            }

            return grauRisco;
        }
    }

    [Database("opsa", "AspectoAmbiental", "IdAspectoAmbiental", "", "Aspecto Ambiental na Ordem de Serviço")]
    public class AspectoAmbiental : Ilitera.Data.Table
    {
        private int _IdAspectoAmbiental;
        private Cliente _IdCliente;
        private string _Nome;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AspectoAmbiental()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AspectoAmbiental(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdAspectoAmbiental; }
            set { _IdAspectoAmbiental = value; }
        }
        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }
    }

    [Database("opsa", "ImpactoAmbiental", "IdImpactoAmbiental", "", "Impacto Ambiental na Ordem de Serviço")]
    public class ImpactoAmbiental : Ilitera.Data.Table
    {
        private int _IdImpactoAmbiental;
        private Cliente _IdCliente;
        private string _Nome;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ImpactoAmbiental()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ImpactoAmbiental(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdImpactoAmbiental; }
            set { _IdImpactoAmbiental = value; }
        }
        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }
    }

    [Database("opsa", "AspectoImpacto", "IdAspectoImpacto")]
    public class AspectoImpacto : Ilitera.Data.Table
    {
        private int _IdAspectoImpacto;
        private AspectoAmbiental _IdAspectoAmbiental;
        private ImpactoAmbiental _IdImpactoAmbiental;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AspectoImpacto()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AspectoImpacto(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdAspectoImpacto; }
            set { _IdAspectoImpacto = value; }
        }
        public AspectoAmbiental IdAspectoAmbiental
        {
            get { return _IdAspectoAmbiental; }
            set { _IdAspectoAmbiental = value; }
        }
        public ImpactoAmbiental IdImpactoAmbiental
        {
            get { return _IdImpactoAmbiental; }
            set { _IdImpactoAmbiental = value; }
        }
    }

    [Database("opsa", "OperacaoAspectoAmbiental", "IdOperacaoAspectoAmbiental")]
    public class OperacaoAspectoAmbiental : Ilitera.Data.Table
    {
        private int _IdOperacaoAspectoAmbiental;
        private Operacao _IdOperacao;
        private AspectoAmbiental _IdAspectoAmbiental;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public OperacaoAspectoAmbiental()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public OperacaoAspectoAmbiental(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdOperacaoAspectoAmbiental; }
            set { _IdOperacaoAspectoAmbiental = value; }
        }
        public Operacao IdOperacao
        {
            get { return _IdOperacao; }
            set { _IdOperacao = value; }
        }
        public AspectoAmbiental IdAspectoAmbiental
        {
            get { return _IdAspectoAmbiental; }
            set { _IdAspectoAmbiental = value; }
        }
    }

    [Database("opsa", "OperacaoAspectoAmbientalImpacto", "IdOperacaoAspectoAmbientalImpacto")]
    public class OperacaoAspectoAmbientalImpacto : Ilitera.Data.Table
    {
        private int _IdOperacaoAspectoAmbientalImpacto;
        private OperacaoAspectoAmbiental _IdOperacaoAspectoAmbiental;
        private ImpactoAmbiental _IdImpactoAmbiental;
        private GrauSeveridadeImpacto _IndGrauSeveridadeImpacto;
        private ProbabilidadeImpacto _IndProbabilidadeImpacto;
        private string _MedidasOpe;
        private string _MedidasEdu;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public OperacaoAspectoAmbientalImpacto()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public OperacaoAspectoAmbientalImpacto(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdOperacaoAspectoAmbientalImpacto; }
            set { _IdOperacaoAspectoAmbientalImpacto = value; }
        }
        public OperacaoAspectoAmbiental IdOperacaoAspectoAmbiental
        {
            get { return _IdOperacaoAspectoAmbiental; }
            set { _IdOperacaoAspectoAmbiental = value; }
        }
        public ImpactoAmbiental IdImpactoAmbiental
        {
            get { return _IdImpactoAmbiental; }
            set { _IdImpactoAmbiental = value; }
        }
        public GrauSeveridadeImpacto IndGrauSeveridadeImpacto
        {
            get { return _IndGrauSeveridadeImpacto; }
            set { _IndGrauSeveridadeImpacto = value; }
        }
        public ProbabilidadeImpacto IndProbabilidadeImpacto
        {
            get { return _IndProbabilidadeImpacto; }
            set { _IndProbabilidadeImpacto = value; }
        }
        public string MedidasOpe
        {
            get { return _MedidasOpe; }
            set { _MedidasOpe = value; }
        }
        public string MedidasEdu
        {
            get { return _MedidasEdu; }
            set { _MedidasEdu = value; }
        }

        public string GetIndiceProbabilidade()
        {
            string indiceProbabilidade = string.Empty;

            switch (this.IndProbabilidadeImpacto)
            {
                case ProbabilidadeImpacto.Superior1Ano:
                    indiceProbabilidade = "1";
                    break;
                case ProbabilidadeImpacto.Superior1Mes:
                    indiceProbabilidade = "2";
                    break;
                case ProbabilidadeImpacto.Inferior1Mes:
                    indiceProbabilidade = "3";
                    break;
            }

            return indiceProbabilidade;
        }

        public string GetIndiceSeveridade()
        {
            string indiceSeveridade = string.Empty;

            switch (this.IndGrauSeveridadeImpacto)
            {
                case GrauSeveridadeImpacto.Benefico:
                    indiceSeveridade = "1";
                    break;
                case GrauSeveridadeImpacto.AdversoMedio:
                    indiceSeveridade = "2";
                    break;
                case GrauSeveridadeImpacto.AdversoAlto:
                    indiceSeveridade = "3";
                    break;
            }

            return indiceSeveridade;
        }

        public string GetIndiceGrauImpacto()
        {
            string grauImpacto = string.Empty;

            switch (this.IndProbabilidadeImpacto)
            {
                case ProbabilidadeImpacto.Superior1Ano:
                    switch (this.IndGrauSeveridadeImpacto)
                    {
                        case GrauSeveridadeImpacto.Benefico:
                            grauImpacto = "1";
                            break;
                        case GrauSeveridadeImpacto.AdversoMedio:
                            grauImpacto = "2";
                            break;
                        case GrauSeveridadeImpacto.AdversoAlto:
                            grauImpacto = "3";
                            break;
                    }
                    break;
                case ProbabilidadeImpacto.Superior1Mes:
                    switch (this.IndGrauSeveridadeImpacto)
                    {
                        case GrauSeveridadeImpacto.Benefico:
                            grauImpacto = "2";
                            break;
                        case GrauSeveridadeImpacto.AdversoMedio:
                            grauImpacto = "3";
                            break;
                        case GrauSeveridadeImpacto.AdversoAlto:
                            grauImpacto = "4";
                            break;
                    }
                    break;
                case ProbabilidadeImpacto.Inferior1Mes:
                    switch (this.IndGrauSeveridadeImpacto)
                    {
                        case GrauSeveridadeImpacto.Benefico:
                            grauImpacto = "3";
                            break;
                        case GrauSeveridadeImpacto.AdversoMedio:
                            grauImpacto = "4";
                            break;
                        case GrauSeveridadeImpacto.AdversoAlto:
                            grauImpacto = "5";
                            break;
                    }
                    break;
            }

            return grauImpacto;
        }
    }

    [Database("opsa", "Dano", "IdDano", "", "Dano na Ordem de Serviço")]
    public class Dano : Ilitera.Data.Table
    {
        private int _IdDano;
        private Cliente _IdCliente;
        private string _Nome;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Dano()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Dano(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdDano; }
            set { _IdDano = value; }
        }
        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }
        [Obrigatorio(true, "O Nome do Dano é obrigatório!")]
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }
    }

    [Database("opsa", "OperacaoPerigoRiscoDano", "IdOperacaoPerigoRiscoDano")]
    public class OperacaoPerigoRiscoDano : Ilitera.Data.Table
    {
        private int _IdOperacaoPerigoRiscoDano;
        private OperacaoPerigoRiscoAcidente _IdOperacaoPerigoRiscoAcidente;
        private Dano _IdDano;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public OperacaoPerigoRiscoDano()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public OperacaoPerigoRiscoDano(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdOperacaoPerigoRiscoDano; }
            set { _IdOperacaoPerigoRiscoDano = value; }
        }
        public OperacaoPerigoRiscoAcidente IdOperacaoPerigoRiscoAcidente
        {
            get { return _IdOperacaoPerigoRiscoAcidente; }
            set { _IdOperacaoPerigoRiscoAcidente = value; }
        }
        public Dano IdDano
        {
            get { return _IdDano; }
            set { _IdDano = value; }
        }
    }

    [Database("opsa", "OperacaoPerigoRiscoEPI", "IdOperacaoPerigoRiscoEPI")]
    public class OperacaoPerigoRiscoEPI : Ilitera.Data.Table
    {
        private int _IdOperacaoPerigoRiscoEPI;
        private OperacaoPerigoRiscoAcidente _IdOperacaoPerigoRiscoAcidente;
        private Epi _IdEpi;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public OperacaoPerigoRiscoEPI()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public OperacaoPerigoRiscoEPI(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdOperacaoPerigoRiscoEPI; }
            set { _IdOperacaoPerigoRiscoEPI = value; }
        }
        public OperacaoPerigoRiscoAcidente IdOperacaoPerigoRiscoAcidente
        {
            get { return _IdOperacaoPerigoRiscoAcidente; }
            set { _IdOperacaoPerigoRiscoAcidente = value; }
        }
        public Epi IdEpi
        {
            get { return _IdEpi; }
            set { _IdEpi = value; }
        }
    }
}
