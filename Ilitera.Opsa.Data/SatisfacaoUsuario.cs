using System;
using System.Data;
using System.Text;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    public enum Conceito : int
    {
        Pessimo = 1,
        Ruim,
        Regular,
        Bom,
        Otimo
    }
    
    [Database("opsa", "SatisfacaoUsuario", "IdSatisfacaoUsuario", "", "Índice de Satisfação do Usuário")]
	public class SatisfacaoUsuario: Table
	{
        private int _IdSatisfacaoUsuario;
		private Usuario _IdUsuario;
		private DateTime _DataCadastro = DateTime.Now;
		private ItemAnalise _IndItemAnalise;
		private Conceito _IndConceito;

        public enum ItemAnalise : int
        {
            AtendimentoSuporte = 1,
            FerramentaMestraNET,
            TempoResposta,
            QualidadeServicos,
            ExpertiseColaboradores,
            MestraGeral
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public SatisfacaoUsuario()
		{
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public SatisfacaoUsuario(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{
            get { return _IdSatisfacaoUsuario; }
            set { _IdSatisfacaoUsuario = value; }
		}
		public Usuario IdUsuario
		{														  
			get{return _IdUsuario;}
			set	{_IdUsuario = value;}
		}
		public DateTime DataCadastro
		{														  
			get{return _DataCadastro;}
			set	{_DataCadastro = value;}
		}
        [Obrigatorio(true, "O Item de Análise deve ser informado!")]
        public ItemAnalise IndItemAnalise
		{
            get { return _IndItemAnalise; }
            set { _IndItemAnalise = value; }
		}
        [Obrigatorio(true, "O Conceito para o item analisado deve ser informado!")]
		public Conceito IndConceito
		{
            get { return _IndConceito; }
            set { _IndConceito = value; }
		}

        public static string strSatisfacaoUsuario(Usuario usuario, ItemAnalise itemAnalise)
        {
            SatisfacaoUsuario satisfacaoUsuario = new SatisfacaoUsuario();
            
            ArrayList alSatisfacaoUsuario = satisfacaoUsuario.FindMax("DataCadastro", "IdUsuario=" + usuario.Id
                + " AND IndItemAnalise=" + (int)itemAnalise);

            if (alSatisfacaoUsuario.Count.Equals(1))
                satisfacaoUsuario = (SatisfacaoUsuario)alSatisfacaoUsuario[0];

            return satisfacaoUsuario.strConceito();
        }

        public string                  strConceito()
        {
            switch (this.IndConceito)
            {
                case Conceito.Pessimo:
                    return "Péssimo";
                case Conceito.Ruim:
                    return "Ruim";
                case Conceito.Regular:
                    return "Regular";
                case Conceito.Bom:
                    return "Bom";
                case Conceito.Otimo:
                    return "Ótimo";
                default:
                    return "Não Informado";
            }
        }

        public static string strItemAnalise(int IndItemAnalise)
        {
            switch (IndItemAnalise)
            {
                case (int)ItemAnalise.AtendimentoSuporte:
                    return "Atendimento do suporte";
                case (int)ItemAnalise.FerramentaMestraNET:
                    return "Ferramenta Ilitera.NET";
                case (int)ItemAnalise.TempoResposta:
                    return "Tempo de resposta a uma solicitação";
                case (int)ItemAnalise.QualidadeServicos:
                    return "Qualidade dos serviços prestados";
                case (int)ItemAnalise.ExpertiseColaboradores:
                    return "Expertise dos colaboradores";
                case (int)ItemAnalise.MestraGeral:
                    return "Atendimento Mestra no geral";
                default:
                    return "Não Identificado";
            }
        }

        public static ItemAnalise EnumItemAnalise(int IndItemAnalise)
        {
            switch (IndItemAnalise)
            {
                case (int)ItemAnalise.AtendimentoSuporte:
                    return ItemAnalise.AtendimentoSuporte;
                case (int)ItemAnalise.FerramentaMestraNET:
                    return ItemAnalise.FerramentaMestraNET;
                case (int)ItemAnalise.TempoResposta:
                    return ItemAnalise.TempoResposta;
                case (int)ItemAnalise.QualidadeServicos:
                    return ItemAnalise.QualidadeServicos;
                case (int)ItemAnalise.ExpertiseColaboradores:
                    return ItemAnalise.ExpertiseColaboradores;
                case (int)ItemAnalise.MestraGeral:
                    return ItemAnalise.MestraGeral;
                default:
                    throw new Exception("Não há Enum ItemAnalise para o respectivo Int32 fornecido!");
            }
        }

        public static Conceito EnumConceito(int IndConceito)
        {
            switch (IndConceito)
            {
                case (int)Conceito.Pessimo:
                    return Conceito.Pessimo;
                case (int)Conceito.Ruim:
                    return Conceito.Ruim;
                case (int)Conceito.Regular:
                    return Conceito.Regular;
                case (int)Conceito.Bom:
                    return Conceito.Bom;
                case (int)Conceito.Otimo:
                    return Conceito.Otimo;
                default:
                    throw new Exception("Não há Enum Conceito para o respectivo Int32 fornecido!");
            }
        }
	}
}