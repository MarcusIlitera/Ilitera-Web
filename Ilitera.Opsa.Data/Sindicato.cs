using System;
using Ilitera.Data;
using System.Collections;
using Ilitera.Common;
using System.Data;

namespace Ilitera.Opsa.Data
{
	[Database("opsa", "Sindicato", "IdSindicato")]
	public class Sindicato : Ilitera.Common.Juridica
	{
		private int _IdSindicato;	
		private int _Edital;
		private int _InicioInscricao;
		private int _TerminoInscricao;
		private int _Publicacao;
		private string _Texto = string.Empty;
		private static ArrayList listSindicato;

		public override int Id
		{
			get{return _IdSindicato;}
			set{_IdSindicato = value;}
		}		
		public int Edital
		{
			get{return _Edital;}
			set{_Edital = value;}
		}		
		public int InicioInscricao
		{
			get{return _InicioInscricao;}
			set{_InicioInscricao = value;}
		}
		public int TerminoInscricao
		{
			get{return _TerminoInscricao;}
			set{_TerminoInscricao = value;}
		}
		public int Publicacao
		{
			get{return _Publicacao;}
			set{_Publicacao = value;}
		}	
		public string Texto
		{
			get{return _Texto;}
			set{_Texto = value;}
		}	

		public static ArrayList GetFindAll()
		{
			if(listSindicato == null)
				RefleshFindAll();
			return listSindicato;
		}

		public static void RefleshFindAll()
		{
			listSindicato = new Sindicato().FindAll();
			listSindicato.Sort();
		}

		public override int Save()
		{
			listSindicato = null;
			return base.Save();
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public void GerarRegraSindicato()
		{
			Obrigacao.Atualiza = false;
			Obrigacao obrigacaoEdital = new Obrigacao();
			obrigacaoEdital.Find("IdEventoBaseCipa="+(int)EventoBase.Edital);
			Obrigacao obrigacaoEleicao = new Obrigacao();
			obrigacaoEleicao.Find("IdEventoBaseCipa="+(int)EventoBase.Eleicao);
			Obrigacao obrigacaoPublicacao = new Obrigacao();
			obrigacaoPublicacao.Find("IdEventoBaseCipa="+(int)EventoBase.Publicacao);
			Obrigacao obrigacaoInicioInscricao = new Obrigacao();
			obrigacaoInicioInscricao.Find("IdEventoBaseCipa="+(int)EventoBase.InicioInscricao);
			Obrigacao obrigacaoTerminoInscricao = new Obrigacao();
			obrigacaoTerminoInscricao.Find("IdEventoBaseCipa="+(int)EventoBase.TerminoInscricao);
			Obrigacao obrigacaoComunicacaoSindicato = new Obrigacao();
			obrigacaoComunicacaoSindicato.Find("IdEventoBaseCipa="+(int)EventoBase.ComunicacaoSindicato);
			/*
			 * ELEIÇÃO 
			 */
			RegraSindicato regraEleicao = new RegraSindicato();
			regraEleicao.Find("IdObrigacao="+obrigacaoEleicao.Id+" AND IdSindicato="+this.Id);
			if(regraEleicao.Id==0)
			{
				regraEleicao.Inicialize();
				regraEleicao.IdObrigacao = obrigacaoEleicao;
				regraEleicao.IdSindicato = this;
			}
			regraEleicao.IndTipoPeriodicidade		= (int)IndTipoPeriodicidades.EventoBase;
			regraEleicao.IdObrigacaoBase			= obrigacaoEdital;
			regraEleicao.IdObrigacaoBasePrimeira	= obrigacaoEdital;
			regraEleicao.DiasExecutar				= this.Edital-1;
			regraEleicao.DiasExecutarPrimeira		= this.Edital-1;
			regraEleicao.IndPeriodoExecutar			= (int)Periodicidade.Dia;
			regraEleicao.IndPeriodoExecutarPrimeira	= (int)Periodicidade.Dia;
			regraEleicao.IndFeriado					= (short)FeriadoTipo.Antecipa;
			regraEleicao.IndFeriadoPrimeira			= (short)FeriadoTipo.Antecipa;
			regraEleicao.Save();
			/*
			 * COMUNICAÇÃO AO SINDICATO
			 */
			RegraSindicato regraComunicacaoSindicato = new RegraSindicato();
			regraComunicacaoSindicato.Find("IdObrigacao="+obrigacaoComunicacaoSindicato.Id+" AND IdSindicato="+this.Id);
			if(regraComunicacaoSindicato.Id==0)
			{
				regraComunicacaoSindicato.Inicialize();
				regraComunicacaoSindicato.IdObrigacao = obrigacaoComunicacaoSindicato;
				regraComunicacaoSindicato.IdSindicato = this;
			}
			regraComunicacaoSindicato.IndTipoPeriodicidade		= (int)IndTipoPeriodicidades.EventoBase;
			regraComunicacaoSindicato.IdObrigacaoBase			= obrigacaoEdital;
			regraComunicacaoSindicato.IdObrigacaoBasePrimeira	= obrigacaoEdital;
			regraComunicacaoSindicato.DiasExecutar				= 9;
			regraComunicacaoSindicato.DiasExecutarPrimeira		= 9;
			regraComunicacaoSindicato.IndPeriodoExecutar		= (int)Periodicidade.Dia;
			regraComunicacaoSindicato.IndPeriodoExecutarPrimeira= (int)Periodicidade.Dia;
			regraComunicacaoSindicato.IndFeriado				= (short)FeriadoTipo.Antecipa;
			regraComunicacaoSindicato.IndFeriadoPrimeira		= (short)FeriadoTipo.Antecipa;
			regraComunicacaoSindicato.Save();
			/*
			 * PUBLICACAO
			 */
			RegraSindicato regraPublicacao = new RegraSindicato();
			regraPublicacao.Find("IdObrigacao="+obrigacaoPublicacao.Id+" AND IdSindicato="+this.Id);
			if(regraPublicacao.Id==0)
			{
				regraPublicacao.Inicialize();
				regraPublicacao.IdObrigacao = obrigacaoPublicacao;
				regraPublicacao.IdSindicato = this;
			}
			regraPublicacao.IndTipoPeriodicidade		= (int)IndTipoPeriodicidades.EventoBase;
			regraPublicacao.IdObrigacaoBase				= obrigacaoEleicao;
			regraPublicacao.IdObrigacaoBasePrimeira		= obrigacaoEleicao;
			regraPublicacao.DiasExecutar				= -(this.Publicacao-1);
			regraPublicacao.DiasExecutarPrimeira		= -(this.Publicacao-1);
			regraPublicacao.IndPeriodoExecutar			= (int)Periodicidade.Dia;
			regraPublicacao.IndPeriodoExecutarPrimeira	= (int)Periodicidade.Dia;
			regraPublicacao.IndFeriado					= (short)FeriadoTipo.Postecipa;
			regraPublicacao.IndFeriadoPrimeira			= (short)FeriadoTipo.Postecipa;
			regraPublicacao.Save();
			/*
			 * Inicio da Inscrição
			 */
			RegraSindicato regraInicioIncricao = new RegraSindicato();
			regraInicioIncricao.Find("IdObrigacao="+obrigacaoInicioInscricao.Id+" AND IdSindicato="+this.Id);
			if(regraInicioIncricao.Id==0)
			{
				regraInicioIncricao.Inicialize();
				regraInicioIncricao.IdObrigacao = obrigacaoInicioInscricao;
				regraInicioIncricao.IdSindicato = this;
			}
			regraInicioIncricao.IndTipoPeriodicidade		= (int)IndTipoPeriodicidades.EventoBase;
			regraInicioIncricao.IdObrigacaoBase				= obrigacaoEdital;
			regraInicioIncricao.IdObrigacaoBasePrimeira		= obrigacaoEdital;
			regraInicioIncricao.DiasExecutar				= this.InicioInscricao-1;
			regraInicioIncricao.DiasExecutarPrimeira		= this.InicioInscricao-1;
			regraInicioIncricao.IndPeriodoExecutar			= (int)Periodicidade.Dia;
			regraInicioIncricao.IndPeriodoExecutarPrimeira	= (int)Periodicidade.Dia;
			regraInicioIncricao.IndFeriado					= (short)FeriadoTipo.Antecipa;
			regraInicioIncricao.IndFeriadoPrimeira			= (short)FeriadoTipo.Antecipa;
			regraInicioIncricao.Save();
			/*
			 * Término da Inscrição
			 */
			RegraSindicato regraTerminoIncricao = new RegraSindicato();
			regraTerminoIncricao.Find("IdObrigacao="+obrigacaoTerminoInscricao.Id+" AND IdSindicato="+this.Id);
			if(regraTerminoIncricao.Id==0)
			{
				regraTerminoIncricao.Inicialize();
				regraTerminoIncricao.IdObrigacao = obrigacaoTerminoInscricao;
				regraTerminoIncricao.IdSindicato = this;
			}
			regraTerminoIncricao.IndTipoPeriodicidade		= (int)IndTipoPeriodicidades.EventoBase;
			regraTerminoIncricao.IdObrigacaoBase			= obrigacaoInicioInscricao;
			regraTerminoIncricao.IdObrigacaoBasePrimeira	= obrigacaoInicioInscricao;
			regraTerminoIncricao.DiasExecutar				= this.TerminoInscricao-1;
			regraTerminoIncricao.DiasExecutarPrimeira		= this.TerminoInscricao-1;
			regraTerminoIncricao.IndPeriodoExecutar			= (int)Periodicidade.Dia;
			regraTerminoIncricao.IndPeriodoExecutarPrimeira	= (int)Periodicidade.Dia;
			regraTerminoIncricao.IndFeriado					= (short)FeriadoTipo.Postecipa;
			regraTerminoIncricao.IndFeriadoPrimeira			= (short)FeriadoTipo.Postecipa;
			regraTerminoIncricao.Save();
			Obrigacao.Atualiza = true;
		}
	}
}
