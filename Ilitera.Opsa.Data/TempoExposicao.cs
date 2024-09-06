using System;
using System.Collections.Generic;
using System.Text;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
    [Database("sied_novo", "tblTEMPO_EXP", "nID_TEMPO_EXP")]
    public class TempoExposicao : Ilitera.Data.Table
    {
        private int _Id;
        private string _tHORA = string.Empty;
        private string _tHORA_EXTENSO = string.Empty;
        private string _tHORA_EXTENSO_SEMANAL = string.Empty;
        private float _sVL_HOR_NUM;
        private float _sVL_HOR_SEM;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TempoExposicao()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TempoExposicao(int Id)
        {
            this.Id = Id;
            this.Find();
        }
        public override int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string tHORA
        {
            get { return _tHORA; }
            set { _tHORA = value; }
        }
        public string tHORA_EXTENSO
        {
            get { return _tHORA_EXTENSO; }
            set { _tHORA_EXTENSO = value; }
        }
        public string tHORA_EXTENSO_SEMANAL
        {
            get { return _tHORA_EXTENSO_SEMANAL; }
            set { _tHORA_EXTENSO_SEMANAL = value; }
        }
        public float sVL_HOR_NUM
        {
            get { return _sVL_HOR_NUM; }
            set { _sVL_HOR_NUM = value; }
        }
        public float sVL_HOR_SEM
        {
            get { return _sVL_HOR_SEM; }
            set { _sVL_HOR_SEM = value; }
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return _tHORA_EXTENSO + " (" + tHORA_EXTENSO_SEMANAL + ")";
        }
    }
}
