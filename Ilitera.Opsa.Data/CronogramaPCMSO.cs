using System;
using System.Collections;
using System.Data;
using Ilitera.Data;
using Ilitera.Common;


namespace Ilitera.Opsa.Data
{

    public enum CronogramaTipoPCMSO : int
    {
        PCMSO
    }

    [Database("opsa", "CronogramaPCMSO", "IdCronogramaPCMSO")]
    public class CronogramaPCMSO : Ilitera.Opsa.Data.Cronograma
    {
        private int _IdCronogramaPCMSO;
        private Pcmso _IdPcmso;
        private string _PlanejamentoAnual = string.Empty;
        private string _MetodologiaAcao = string.Empty;
        private string _FormaRegistro = string.Empty;
        private Int16 _Prioridade = 0;


        public CronogramaPCMSO()
        {
            this.IndCronograma = (int)CronogramaTipoPCMSO.PCMSO;
        }
        public override int Id
        {
            get { return _IdCronogramaPCMSO; }
            set { _IdCronogramaPCMSO = value; }
        }
        
        public Pcmso IdPcmso
        {
            get { return _IdPcmso; }
            set { _IdPcmso = value; }
        }

        public string PlanejamentoAnual
        {
            get { return _PlanejamentoAnual; }
            set { _PlanejamentoAnual = value; }
        }
        public string MetodologiaAcao
        {
            get { return _MetodologiaAcao; }
            set { _MetodologiaAcao = value; }
        }
        public string FormaRegistro
        {
            get { return _FormaRegistro; }
            set { _FormaRegistro = value; }
        }
        public Int16 Prioridade
        {
            get { return _Prioridade; }
            set { _Prioridade = value; }
        }

        public void PopularPrazo()
        {
            if (this.Prazo.Month == 1)
                this.Mes01 = true;
            else if (this.Prazo.Month == 2)
                this.Mes02 = true;
            else if (this.Prazo.Month == 3)
                this.Mes03 = true;
            else if (this.Prazo.Month == 4)
                this.Mes04 = true;
            else if (this.Prazo.Month == 5)
                this.Mes05 = true;
            else if (this.Prazo.Month == 6)
                this.Mes06 = true;
            else if (this.Prazo.Month == 7)
                this.Mes07 = true;
            else if (this.Prazo.Month == 8)
                this.Mes08 = true;
            else if (this.Prazo.Month == 9)
                this.Mes09 = true;
            else if (this.Prazo.Month == 10)
                this.Mes10 = true;
            else if (this.Prazo.Month == 11)
                this.Mes11 = true;
            else if (this.Prazo.Month == 12)
                this.Mes12 = true;

            if (this.Ano == string.Empty)
                this.Ano = this.Prazo.Year.ToString();
            else
            {
                if (this.Ano.IndexOf(this.Prazo.Year.ToString()) == -1)
                    this.Ano = this.Ano + " e " + this.Prazo.Year.ToString();
            }
        }

        public static ArrayList GetCronograma(Pcmso pcmso)
        {
            ArrayList list = new CronogramaPCMSO().Find("IdPCMSO=" + pcmso.Id
                + " ORDER BY PlanejamentoAnual");

            if (list.Count == 0)
                pcmso.GerarCronogramaPadrao();

            return list;
        }

        public static ArrayList GetCronograma_Ant(Pcmso pcmso)
        {
            ArrayList list = new CronogramaPCMSO().Find("IdPCMSO=" + pcmso.Id
                + " ORDER BY PlanejamentoAnual");

            if (list.Count == 0)
                pcmso.GerarCronogramaPadraoPCMSOAnt(pcmso);

            return list;
        }

    }
}
