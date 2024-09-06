using System;
using System.Text;
using Ilitera.Data;
using Ilitera.Common;
using System.Data;
using System.Collections;
namespace Ilitera.Opsa.Data
{

	[Database("opsa","ContratoServicoReajuste", "IdContratoServicoReajuste")]
	public class ContratoServicoReajuste : Ilitera.Data.Table 
	{
		private int _IdContratoServicoReajuste;
		private ContratoServico _IdContratoServico;
		private Cotacao _IdCotacao;
		private Cotacao _IdCotacaoAnterior;
		private float _ValorAnterior;
		private float _ValorComReajuste;
		private DateTime _DataReajuste;
		private float _ValorCotacao;
		private string _Observacao = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ContratoServicoReajuste()
		{

		}
		public override int Id
		{
			get{return _IdContratoServicoReajuste;}
			set{_IdContratoServicoReajuste = value;}
		}
		public ContratoServico IdContratoServico
		{
			get{return _IdContratoServico;}
			set{_IdContratoServico = value;}
		}
		public Cotacao IdCotacao
		{
			get{return _IdCotacao;}
			set{_IdCotacao = value;}
		}
		public Cotacao IdCotacaoAnterior
		{
			get{return _IdCotacaoAnterior;}
			set{_IdCotacaoAnterior = value;}
		}
		public float ValorAnterior
		{
			get{return _ValorAnterior;}
			set{_ValorAnterior = value;}
		}
		public float ValorComReajuste
		{
			get{return _ValorComReajuste;}
			set{_ValorComReajuste = value;}
		}
		public DateTime DataReajuste
		{
			get{return _DataReajuste;}
			set{_DataReajuste = value;}
		}
		public float ValorCotacao
		{
			get{return _ValorCotacao;}
			set{_ValorCotacao = value;}
		}
		public string Observacao
		{
			get{return _Observacao;}
			set{_Observacao = value;}
		}

		public override int Save()
		{
			if(this.IdCotacao.Id!=0 && this.IdCotacao.Id==this.IdCotacaoAnterior.Id)
				throw new Exception("Cotação anterior é igual a nova cotação!");

            if (ValorAnterior != 0 && ValorAnterior == ValorComReajuste)
                throw new Exception("O Valor anterior é igual o valor com reajuste!");
            
			this.IdContratoServico.Find();

			this.IdContratoServico.Valor = this.ValorComReajuste;
			this.IdContratoServico.IdCotacaoAtual.Id = this.IdCotacao.Id;
			this.IdContratoServico.Save();

			return base.Save ();
		}

		public override void Delete()
		{
			this.IdContratoServico.Find();

			this.IdContratoServico.Valor = this.ValorAnterior;
			this.IdContratoServico.IdCotacaoAtual = this.IdCotacaoAnterior;

			this.IdContratoServico.Save();

			base.Delete ();
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public float CalculaValorComReajuste()
		{
			float ret = 0F;

            if (this.IdCotacao.mirrorOld == null)
                this.IdCotacao.Find();

            if (this.IdCotacao.Id == 0)
                return this.ValorAnterior;

            if (this.ValorAnterior == 0)
                return ret;

            if (this.IdCotacaoAnterior.mirrorOld == null)
                this.IdCotacaoAnterior.Find();

            if (this.IdCotacao.mirrorOld == null)
                this.IdCotacao.Find();

            if (this.IdCotacao.IdIndice.mirrorOld == null)
                this.IdCotacao.IdIndice.Find();

            if (this.IdCotacaoAnterior.ValorCotacao == 0 
                && this.IdCotacao.IdIndice.IndTipoValor == (int)TipoValor.ValoresSM)
                return this.ValorAnterior;
            
            if (this.IdCotacao.IdIndice.IndTipoValor == (int)TipoValor.ValoresSM)
                ret = (this.ValorAnterior / this.IdCotacaoAnterior.ValorCotacao) * this.ValorCotacao;
            else
                ret = (this.ValorAnterior * this.ValorCotacao) + this.ValorAnterior;
			
			ret = Convert.ToSingle(System.Math.Round(Convert.ToDecimal(ret), 2));
			
			return ret;
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public static void ReajustarContratosValores(DateTime dateReajuste, Cotacao cotacaoAnterior, Cotacao cotacaoNova)
        {
            StringBuilder str = new StringBuilder();
            str.Append("IdCotacaoAtual=" + cotacaoAnterior.Id);
            str.Append(" AND Automatica = 1");
            str.Append(" AND (IdContrato IN ");
            str.Append("				(SELECT IdContrato");
            str.Append("				 FROM Contrato");
            str.Append("				 WHERE DataRecisao IS NULL");
            str.Append("                  AND IdCliente IN (SELECT IdCliente");
            str.Append("                                 FROM qryClienteAtivos )))");

            ArrayList list = new ContratoServico().Find(str.ToString());

            foreach (ContratoServico contratoServico in list)
            {
                ContratoServicoReajuste reajuste = new ContratoServicoReajuste();
                reajuste.DataReajuste = dateReajuste;
                reajuste.IdContratoServico = contratoServico;
                reajuste.IdCotacao = cotacaoNova;
                reajuste.IdCotacaoAnterior = cotacaoAnterior;
                reajuste.ValorAnterior = contratoServico.Valor;
                reajuste.ValorCotacao = cotacaoNova.ValorCotacao;
                reajuste.ValorComReajuste = reajuste.CalculaValorComReajuste();
                reajuste.Save();
            }
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public static void ReajustarContratosPercentuais(DateTime dateReajuste, Cotacao cotacaoNova)
        {
            StringBuilder str = new StringBuilder();

            if (cotacaoNova.mirrorOld == null)
                cotacaoNova.Find();

            if (cotacaoNova.IdIndice.mirrorOld == null)
                cotacaoNova.IdIndice.Find();

            str.Append("IdIndice=" + cotacaoNova.IdIndice.Id);

            if (cotacaoNova.IdIndice.IndTipoValor == (int)TipoValor.Percentuais)
                str.Append(" AND MesReajuste=" + cotacaoNova.DataCotacao.Month.ToString());

            str.Append(" AND Automatica = 1");
            str.Append(" AND (IdContrato IN ");
            str.Append("	(SELECT IdContrato");
            str.Append("	FROM Contrato");
            str.Append("	WHERE DataRecisao IS NULL ");
            str.Append("    AND IdCliente IN (SELECT IdCliente FROM qryClienteAtivos)))");

            ArrayList list = new ContratoServico().Find(str.ToString());

            foreach (ContratoServico contratoServico in list)
            {
                Contrato contrato = contratoServico.IdContrato;

                if (contrato.mirrorOld == null)
                    contrato.Find();

                if (cotacaoNova.IdIndice.IndTipoValor == (int)TipoValor.Percentuais)
                {
                    //Ex: Inicio Contrato 01/08/2005 e a Cotacao é 01/08/2005
                    if (contrato.Inicio.AddYears(1).AddMonths(-2) > cotacaoNova.DataCotacao)
                        continue;
                }

                ContratoServicoReajuste reajuste = new ContratoServicoReajuste();

                reajuste.Inicialize();

                reajuste.DataReajuste = dateReajuste;
                reajuste.IdContratoServico = contratoServico;
                reajuste.IdCotacaoAnterior = contratoServico.IdCotacaoAtual;
                reajuste.IdCotacao = cotacaoNova;
                reajuste.ValorAnterior = contratoServico.Valor;
                reajuste.ValorCotacao = cotacaoNova.ValorCotacao;
                reajuste.ValorComReajuste = reajuste.CalculaValorComReajuste();

                reajuste.Save();
            }
        }
	}
}
