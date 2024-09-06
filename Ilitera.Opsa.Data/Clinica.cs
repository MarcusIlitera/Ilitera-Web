using System;
using System.Collections;
using Ilitera.Common;
using Ilitera.Data;
using Ilitera.Opsa.Data;

namespace Ilitera.Opsa.Data
{	
	[Database("opsa", "Clinica", "IdClinica", "", "Clínica")]
	public class Clinica : Ilitera.Common.Juridica
	{
		private int	_IdClinica;
		private string _HorarioAtendimento = string.Empty;
		private bool _FazClinico = true;
		private bool _FazComplementar;
		private bool _FazAudiometrico;
		private bool _NecessitaAgendamento;
		private int _DiasMaxAgendamento;
		private int _DuracaoExame;
		private bool _IsClinicaInterna;
		private int _DiaDeposito;
		private string _ObservacaoDeposito = string.Empty;

		public Clinica()
		{
			this.Inicialize();
			this.IdJuridicaPapel.Id=(int)IndJuridicaPapel.Clinica;
		}
		public Clinica(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{														  
			get{return _IdClinica;}
			set	{_IdClinica = value;}
		}
		public string HorarioAtendimento
		{														  
			get{return _HorarioAtendimento;}
			set {_HorarioAtendimento = value;}
		}
		public bool FazClinico
		{
			get{return _FazClinico;}
			set {_FazClinico = value;}
		}
		public bool FazComplementar
		{
			get{return _FazComplementar;}
			set {_FazComplementar = value;}
		}
		public bool FazAudiometrico
		{
			get{return _FazAudiometrico;}
			set {_FazAudiometrico = value;}
		}
		public bool NecessitaAgendamento
		{
			get{return _NecessitaAgendamento;}
			set {_NecessitaAgendamento = value;}
		}

		public int DiasMaxAgendamento
		{
			get{return _DiasMaxAgendamento;}
			set {_DiasMaxAgendamento = value;}
		}

		public int DuracaoExame
		{
			get{return _DuracaoExame;}
			set {_DuracaoExame = value;}
		}

		public bool IsClinicaInterna
		{
			get{return _IsClinicaInterna;}
			set {_IsClinicaInterna = value;}
		}

		public int DiaDeposito
		{
			get{return _DiaDeposito;}
			set {_DiaDeposito = value;}
		}

		public string ObservacaoDeposito
		{
			get{return _ObservacaoDeposito;}
			set {_ObservacaoDeposito = value;}
		}

		public override int Save()
		{
			return base.Save(true);
		}

		public bool HasHorariosDisponivel(DateTime dataExame)
		{
			ArrayList ret = new ArrayList();
			
			ArrayList list = new Compromisso().Find("IdPessoa IN (SELECT IdPessoa FROM JuridicaPessoa WHERE IdJuridica="+this.Id+")"
				+" AND DataInicio>='"+dataExame.ToString("yyyy-MM-dd")+" 00:00:00.000'"
				+" AND DataInicio<='"+dataExame.ToString("yyyy-MM-dd")+" 23:59:59.999'"
				+" AND HorarioDisponivel=1");

			foreach(Compromisso compromisso in list)
			{
				for(DateTime data = new DateTime(compromisso.DataInicio.Year,compromisso.DataInicio.Month,compromisso.DataInicio.Day, compromisso.DataInicio.Hour, compromisso.DataInicio.Minute,compromisso.DataInicio.Second)
						;data<compromisso.DataTermino
					;data = data.AddMinutes(this.DuracaoExame))
				{
					Compromisso compr = new Compromisso();
					ArrayList listCompromisos = compr.Find("IdPessoa IN (SELECT IdPessoa FROM JuridicaPessoa WHERE IdJuridica="+this.Id+")"
						+" AND DataInicio>='"+dataExame.ToString("yyyy-MM-dd")+" " +data.ToString("t")+":00.000'"
						+" AND DataInicio<'"+dataExame.ToString("yyyy-MM-dd")+" " +data.AddMinutes(this.DuracaoExame).ToString("t")+":00.000'"
						+" AND AvisoConflito=1"
						+" AND HorarioDisponivel=0");
					if(listCompromisos.Count==0)
						ret.Add(data.ToString("t"));
				}
			}

			if (ret.Count == 0)
				return false;
			else
				return true;
		}

		public ArrayList ListaMedicosDisponiveis(DateTime dataSelecionada)
		{
			ArrayList pessoa = new ArrayList();
			ArrayList medicos = new ArrayList();
			bool conflito = true;
			
			ArrayList pessoaJuridica = new JuridicaPessoa().Find("IdJuridica=" + this.Id + " AND IdPessoa IN (SELECT IdPessoa FROM Compromisso)");

			foreach (JuridicaPessoa jp in pessoaJuridica)
			{
				ArrayList listMedicos = new Compromisso().Find("IdPessoa=" + jp.IdPessoa.Id
					+" AND DataInicio>='"+dataSelecionada.ToString("yyyy-MM-dd")+" 00:00:00.000'"
					+" AND DataInicio<='"+dataSelecionada.ToString("yyyy-MM-dd")+" 23:59:59.999'"
                    + " AND IdCategoria=" + (int)Categorias.Medico
					+" AND HorarioDisponivel=1");

				ArrayList listCompromisos = new Compromisso().Find("IdPessoa=" + jp.IdPessoa.Id
					+" AND DataInicio>='"+dataSelecionada.ToString("yyyy-MM-dd")+" 00:00:00.000'"
					+" AND DataInicio<='"+dataSelecionada.ToString("yyyy-MM-dd")+" 23:59:59.999'"
					+" AND AvisoConflito=1"
					+" AND HorarioDisponivel=0");

				foreach(Compromisso compromisso in listMedicos)
				{
					ArrayList ret = new ArrayList();
					compromisso.IdPessoa.Find();

                    for (DateTime data = new DateTime(compromisso.DataInicio.Year, compromisso.DataInicio.Month, compromisso.DataInicio.Day, compromisso.DataInicio.Hour, compromisso.DataInicio.Minute, compromisso.DataInicio.Second)
                            ; data < compromisso.DataTermino
                        ; data = data.AddMinutes(this.DuracaoExame))
                    {
                        if (listCompromisos.Count > 0)
                        {
                            foreach (Compromisso compromiss in listCompromisos)
                            {
                                if (compromiss.DataInicio <= data && compromiss.DataTermino > data)
                                {
                                    conflito = true;
                                    break;
                                }
                                else
                                    conflito = false;
                            }
                        }
                        else
                            conflito = false;

                        if (!conflito && data >= DateTime.Now)
                            ret.Add(data.ToString("t"));
                    }

					if (ret.Count>0)
					{
						if (pessoa.Count == 0)
							pessoa.Add(compromisso.IdPessoa);
						else
						{
							bool hasPessoa = false;

							foreach (Pessoa pessoas in pessoa)
								if (pessoas.Id == compromisso.IdPessoa.Id)
								{
									hasPessoa = true;
									break;
								}

							if (!hasPessoa)
								pessoa.Add(compromisso.IdPessoa);
						}
					}
				}
			}

			pessoa.Sort();

			//Verifica se é médico
			foreach (Pessoa pessoas in pessoa)
			{
				Medico medico = new Medico();
				medico.FindByPessoa(pessoas);

				if (medico.Id != 0)
					medicos.Add(medico);
			}

			return medicos;
		}
	}
}
