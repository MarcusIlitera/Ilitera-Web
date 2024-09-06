using System;
using Ilitera.Data;
using Ilitera.Common;
using System.Data;
using System.Collections;
using System.Text;

namespace Ilitera.Opsa.Data
{
	public enum Medicos: int
	{
		PcmsoNaoContratada = 1111,
		Chang = 881690900,
		DraMarcela = 264104418,
        DraRosana = 1157250958,
	}

    [Database("opsa", "Medico", "IdMedico")]
    public class Medico : Ilitera.Common.Prestador
    {
        private int _IdMedico;

        public Medico()
        {
            this.IndTipoPrestador = (int)TipoPrestador.Medico;
            this.IndPessoaPapel = (int)PessoaPapeis.Prestador;
        }
        public Medico(int Id)
        {
            this.Find(Id);
        }
        public Medico(string NomePessoa)
            : base(NomePessoa)
        {
            this.IdPessoa = new Pessoa();
            this.IdPessoa.NomeAbreviado = NomePessoa;
            this.IndTipoPrestador = (int)TipoPrestador.Medico;
            this.IndPessoaPapel = (int)PessoaPapeis.Prestador;
        }
        public override int Id
        {
            get { return _IdMedico; }
            set { _IdMedico = value; }
        }

        #region ListaHorariosDisponiveis

        public ArrayList ListaHorariosDisponiveis(DateTime dataExame)
        {
            ArrayList ret = new ArrayList();
            Clinica clinica = new Clinica();
            clinica.Find(this.IdJuridica.Id);
            bool conflito = true;

            //Primeiro verifica disponibilidade na agenda do Médico
            ArrayList list = new Compromisso().Find("IdPessoa=" + this.IdPessoa.Id
                + " AND DataInicio>='" + dataExame.ToString("yyyy-MM-dd") + " 00:00:00.000'"
                + " AND DataInicio<='" + dataExame.ToString("yyyy-MM-dd") + " 23:59:59.999'"
                + " AND IdCategoria=" + (int)Categorias.Medico
                + " AND HorarioDisponivel=1"
                + " ORDER BY DataInicio");

            //Verifica todos os compromissos na agenda do Médico
            ArrayList listCompromisos = new Compromisso().Find("IdPessoa=" + this.IdPessoa.Id
                + " AND DataInicio>='" + dataExame.ToString("yyyy-MM-dd") + " 00:00:00.000'"
                + " AND DataInicio<='" + dataExame.ToString("yyyy-MM-dd") + " 23:59:59.999'"
                + " AND AvisoConflito=1"
                + " AND HorarioDisponivel=0");

            foreach (Compromisso compromisso in list)
            {
                for (DateTime data = new DateTime(compromisso.DataInicio.Year, compromisso.DataInicio.Month, compromisso.DataInicio.Day, compromisso.DataInicio.Hour, compromisso.DataInicio.Minute, compromisso.DataInicio.Second)
                        ; data < compromisso.DataTermino
                    ; data = data.AddMinutes(clinica.DuracaoExame))
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
                    {
                        StringBuilder time = new StringBuilder();
                        time.Append(data.ToString("t"));

                        if (time.ToString().Length < 5)
                            time.Insert(0, "0");

                        ret.Add(time.ToString());
                    }
                }
            }

            if (ret.Count == 0)
                ret.Add("--:--");

            return ret;
        }
        #endregion
    }
    
    #region qryMedico

    [Database("opsa", "qryMedico", "IdMedico")]
    public class qryMedico : Ilitera.Data.Table
    {
        private int _IdMedico;
        private string _NomeCompleto;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public qryMedico()
        {

        }
        public override int Id
        {
            get { return _IdMedico; }
            set { _IdMedico = value; }
        }
        public string NomeCompleto
        {
            get { return _NomeCompleto; }
            set { _NomeCompleto = value; }
        }
    }

    #endregion
}
