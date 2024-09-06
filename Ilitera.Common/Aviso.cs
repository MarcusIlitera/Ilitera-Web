using System;
using System.Collections.Generic;
using System.Text;

using Ilitera.Data;

namespace Ilitera.Common
{
    [Database("opsa", "Aviso", "IdAviso")]
    public class Aviso : Ilitera.Data.Table
    {
        #region Properties

        private int _IdAviso;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Aviso()
        {

        }
        public override int Id
        {
            get { return _IdAviso; }
            set { _IdAviso = value; }
        }

        private Compromisso _IdCompromisso;

        public Compromisso IdCompromisso
        {
            get { return _IdCompromisso; }
            set { _IdCompromisso = value; }
        }
        private AtividadeBase _IdAtividadeBase;

        public AtividadeBase IdAtividadeBase
        {
            get { return _IdAtividadeBase; }
            set { _IdAtividadeBase = value; }
        }
        private Periodicidade _IndPeriodicidade = Periodicidade.Minuto;

        public Periodicidade IndPeriodicidade
        {
            get { return _IndPeriodicidade; }
            set { _IndPeriodicidade = value; }
        }
        private int _Intervalo = 1;

        public int Intervalo
        {
            get { return _Intervalo; }
            set { _Intervalo = value; }
        }
        //private FormaAviso _IndFormaAviso;

        //public FormaAviso IndFormaAviso
        //{
        //    get { return _IndFormaAviso; }
        //    set { _IndFormaAviso = value; }
        //}
        private bool _IsCancelarAviso;

        public bool IsCancelarAviso
        {
            get { return _IsCancelarAviso; }
            set { _IsCancelarAviso = value; }
        }

        private DateTime _Executar;

        public DateTime Executar
        {
            get { return _Executar; }
            set { _Executar = value; }
        }

        private DateTime _AvisadoPorEmail;

        public DateTime AvisadoPorEmail
        {
            get { return _AvisadoPorEmail; }
            set { _AvisadoPorEmail = value; }
        }
        #endregion

        #region Metodos

        #region override ToString

        public override string ToString()
        {
            if (this.Id == 0)
                return "Sem Alerta";
            else
            {
                if (this.IsCancelarAviso)
                    return this.GetPeriodicidade() + " - Cancelado";
                else
                    return this.GetPeriodicidade();
            }
        }
        #endregion

        #region override Save

        public override int Save()
        {
            SetDataExecutar();

            return base.Save();
        }
        #endregion

        #region GetPessoa

        public Pessoa GetPessoa()
        {
            Pessoa pessoa = new Pessoa(); ;

            if (this.IdCompromisso.Id != 0)
            {
                if (this.IdCompromisso.mirrorOld == null)
                    this.IdCompromisso.Find();

                pessoa.Find(this.IdCompromisso.IdPessoa.Id);
            }
            else if (this.IdAtividadeBase.Id != 0)
            {
                if (this.IdAtividadeBase.mirrorOld == null)
                    this.IdAtividadeBase.Find();

                pessoa.Find(this.IdAtividadeBase.IdPessoa.Id);
            }

            return pessoa;
        }
        #endregion

        #region GetMensagem

        public string GetMensagem()
        {
            string ret;

            if (this.IdCompromisso.Id != 0)
                ret = "Compromisso " + this.IdCompromisso.ToString();
            else if (this.IdAtividadeBase.Id != 0)
                ret = this.IdAtividadeBase.GetAtividade() + " " + this.IdAtividadeBase.ToString();
            else
                ret = string.Empty;

            return ret;
        }
        #endregion

        #region SetDataExecutar

        public Ilitera.Common.IDataInicioTermino GetIDataInicioTermino()
        {
            Ilitera.Common.IDataInicioTermino iData = null;

            if (this.IdCompromisso.Id != 0)
            {
                if(this.IdCompromisso.mirrorOld == null)
                    this.IdCompromisso.Find();

                iData = this.IdCompromisso;
            }
            else if (this.IdAtividadeBase.Id != 0)
            {
                if (this.IdAtividadeBase.mirrorOld == null)
                    this.IdAtividadeBase.Find();

                iData = this.IdAtividadeBase;
            }

            return iData;
          }

        public void SetDataExecutar()
        {
            SetDataExecutar(GetIDataInicioTermino());
        }

        public void SetDataExecutar(Ilitera.Common.IDataInicioTermino iData)
        {
            if (this.IndPeriodicidade == Periodicidade.Minuto)
                this.Executar = iData.DataInicio.AddMinutes(-this.Intervalo);
            else if (this.IndPeriodicidade == Periodicidade.Hora)
                this.Executar = iData.DataInicio.AddHours(-this.Intervalo);
            else if (this.IndPeriodicidade == Periodicidade.Dia)
                this.Executar = iData.DataInicio.AddDays(-this.Intervalo);
            else if (this.IndPeriodicidade == Periodicidade.Semana)
                this.Executar = iData.DataInicio.AddDays(-(this.Intervalo * 7));
            else if (this.IndPeriodicidade == Periodicidade.Mes)
                this.Executar = iData.DataInicio.AddMonths(-this.Intervalo);
            else if (this.IndPeriodicidade == Periodicidade.Semestral)
                this.Executar = iData.DataInicio.AddMonths(-(this.Intervalo * 6));
            else if (this.IndPeriodicidade == Periodicidade.Ano)
                this.Executar = iData.DataInicio.AddYears(-this.Intervalo);
            else
                this.Executar = iData.DataInicio;
        }
        #endregion

        #region GetPeriodicidade

        public static string GetPeriodicidade(Periodicidade IndPeriodicidade, int IndIntervalo)
        {
            string ret;

            if (IndPeriodicidade == Periodicidade.Dia)
            {
                if (IndIntervalo == 1)
                    ret = "Diário";
                else
                    ret = IndIntervalo.ToString() + " dias";
            }
            else if (IndPeriodicidade == Periodicidade.Semana)
            {
                if (IndIntervalo == 1)
                    ret = "Semanal";
                else
                    ret = IndIntervalo.ToString() + " semanas";
            }
            else if (IndPeriodicidade == Periodicidade.Mes)
            {
                if (IndIntervalo == 1)
                    ret = "Mensal";
                else
                    ret = IndIntervalo.ToString() + " meses";
            }
            else if (IndPeriodicidade == Periodicidade.Ano)
            {
                if (IndIntervalo == 1)
                    ret = "Anual";
                else
                    ret = IndIntervalo.ToString() + " anos";
            }
            else if (IndPeriodicidade == Periodicidade.Semestral)
            {
                if (IndIntervalo == 1)
                    ret = "Semestral";
                else
                    ret = IndIntervalo.ToString() + " semestre";
            }
            else if (IndPeriodicidade == Periodicidade.Minuto)
            {
                if (IndIntervalo == 1)
                    ret = "1 minuto";
                else
                    ret = IndIntervalo.ToString() + " minutos";
            }
            else if (IndPeriodicidade == Periodicidade.Hora)
            {
                if (IndIntervalo == 1)
                    ret = "1 hora";
                else
                    ret = IndIntervalo.ToString() + " horas";
            }
            else
                ret = string.Empty;

            return ret;
        }

        public string GetPeriodicidade()
        {
            return GetPeriodicidade(this.IndPeriodicidade, this.Intervalo);
        }
        #endregion

        #region GetAvisosEmail

        public static List<Aviso> GetAvisosEmail()
        {
            StringBuilder criteria = new StringBuilder();

            criteria.Append("IsCancelarAviso=0");
            criteria.Append(" AND AvisadoPorEmail IS NULL");

            criteria.Append(" AND Executar <='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            criteria.Append(" AND (");
            criteria.Append(" IdCompromisso IN (SELECT IdCompromisso FROM Compromisso WHERE "
                                + " DataInicio >='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ")");
            criteria.Append(" OR ");
            criteria.Append(" IdAtividadeBase IN (SELECT IdAtividadeBase FROM AtividadeBase WHERE "
                            + "DataInicio >='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ")");
            criteria.Append(" )");

            List<Aviso> avisos = new Aviso().Find<Aviso>(criteria.ToString());

            return avisos;
        }
        #endregion

        #region GetAvisos

        public static List<Aviso> GetAvisos(Pessoa pessoa)
        {
            StringBuilder criteria = new StringBuilder();

            criteria.Append("IsCancelarAviso=0");

            criteria.Append(" AND Executar <='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            criteria.Append(" AND (");
            criteria.Append(" IdCompromisso IN (SELECT IdCompromisso FROM Compromisso WHERE "
                                + "IdPessoa=" + pessoa.Id
                                + " AND DataInicio >='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                                + ")");
            criteria.Append(" OR ");
            criteria.Append(" IdAtividadeBase IN (SELECT IdAtividadeBase FROM AtividadeBase WHERE "
                            + "IdPessoa=" + pessoa.Id
                            + " AND DataInicio >='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                            + ")");
            criteria.Append(" )");

            List<Aviso> avisos = new Aviso().Find<Aviso>(criteria.ToString());

            return avisos;
        }
        #endregion

        #region EnviarAvisosPorEmail

        public static void EnviarAvisosPorEmail()
        {

        }

        #endregion

        #endregion
    }
}
