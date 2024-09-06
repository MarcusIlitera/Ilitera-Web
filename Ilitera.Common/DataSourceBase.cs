using System;

namespace Ilitera.Common
{
    public class DataSourceBase
    {
        #region Eventos

        public delegate void EventProgress(int val);
        public delegate void EventProgressFinalizar();

        public virtual event EventProgress ProgressIniciar;
        public virtual event EventProgress ProgressAtualizar;
        public virtual event EventProgressFinalizar ProgressFinalizar;


        private void NaoUsar()
        {
            if (ProgressIniciar != null)
                ProgressIniciar(0);

            if (ProgressAtualizar != null)
                ProgressAtualizar(0);

            if (ProgressFinalizar != null)
                ProgressFinalizar();
        }

        #endregion

        #region Constructor

        protected DataSourceBase()
        {
            InicioProcessamento = DateTime.Now;

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
        }
        #endregion

        #region Properties

        private DateTime _InicioProcessamento;

        private DateTime InicioProcessamento
        {
            get { return _InicioProcessamento; }
            set { _InicioProcessamento = value; }
        }
        #endregion

        #region SetTempoProcessamento

        public void SetTempoProcessamento(CrystalDecisions.CrystalReports.Engine.ReportClass report)
        {
            if (report == null)
                return;

            TimeSpan timeSpan = DateTime.Now.Subtract(InicioProcessamento);

            report.SummaryInfo.KeywordsInReport = timeSpan.Ticks.ToString();
        }
        #endregion

        #region GetDateTimeToString

        protected string GetDateTimeToString(object dateTime)
        {
            if (dateTime == System.DBNull.Value)
                return "-";

            try
            {
                if (Convert.ToDateTime(dateTime) != new DateTime())
                    return Convert.ToDateTime(dateTime).ToString("dd-MM-yyyy");
                else
                    return "-";
            }
            catch
            {
                return "-";
            }
        }
        #endregion

        #region GetInt32

        protected int GetInt32(object value)
        {
            int ret = 0;

            Int32.TryParse(Convert.ToString(value), out ret);

            return ret;
        }
        #endregion

        #region GetInt64

        protected long GetInt64(object value)
        {
            long ret = 0;

            Int64.TryParse(Convert.ToString(value), out ret);

            return ret;
        }
        #endregion

        #region VerificarProvider

        public static void VerificarProvider(CrystalDecisions.CrystalReports.Engine.ReportClass report)
        {

        }

        #endregion
    }
}
