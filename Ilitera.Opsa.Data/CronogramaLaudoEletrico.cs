using System;
using System.Collections;
using System.Data;
using Ilitera.Data;
using Ilitera.Common;


namespace Ilitera.Opsa.Data
{


    [Database("opsa", "CronogramaLaudoEletrico", "IdCronogramaLaudoEletrico")]
    public class CronogramaLaudoEletrico : Ilitera.Opsa.Data.Cronograma
    {
        private int _IdCronogramaLaudoEletrico;
        private int _IdLaudoEletrico;
        private string _PlanejamentoAnual = string.Empty;
        private string _MetodologiaAcao = string.Empty;
        private string _FormaRegistro = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CronogramaLaudoEletrico()
        {
            this.IndCronograma = 1;
        }
        public override int Id
        {
            get { return _IdCronogramaLaudoEletrico; }
            set { _IdCronogramaLaudoEletrico = value; }
        }

        public int IdLaudoEletrico
        {
            get { return _IdLaudoEletrico; }
            set { _IdLaudoEletrico = value; }
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

        public static ArrayList GetCronograma(int xIdLaudoEletrico, DateTime xDataLaudo)
        {
            ArrayList list = new CronogramaLaudoEletrico().Find("IdLaudoEletrico=" + xIdLaudoEletrico
                + " ORDER BY PlanejamentoAnual");

            if (list.Count == 0)
            {
                LaudoEletrico xLaudo = new LaudoEletrico();
                xLaudo.GerarCronogramaPadrao(xDataLaudo);
            }
            return list;
        }

        public static ArrayList GetCronograma(int xIdLaudoEletrico)
        {
            ArrayList list = new CronogramaLaudoEletrico().Find("IdLaudoEletrico=" + xIdLaudoEletrico
                + " ORDER BY PlanejamentoAnual");

            return list;
        }

    }
}
