using System;
using System.Text;
using System.Collections;
using System.Data;
using Ilitera.Common;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "AposentadoriaEspecial", "IdAposentadoriaEspecial")]
    public class AposentadoriaEspecial : Ilitera.Opsa.Data.Documento
    {
        private int _IdAposentadoriaEspecial;
        private Empregado _IdEmpregado;
        private DateTime _DataLaudo = new DateTime(2003, 12, 29);
        private bool _IsHouveMudanca;
        private bool _IsPossuiLaudo = true;
        private bool _IsManualPeriodo;
        private bool _CarimboCnpjRegistroEmpregado;
        private DateTime _Inicio = DateTime.Today;
        private DateTime _Termino = DateTime.Today;
        private Ghe _IdGhe;
        private bool _IsManualTempoExposicao;
        private string _TempoExposicao = string.Empty;
        private bool _IsManualSetorNome;
        private string _SetorNome = string.Empty;
        private bool _IsManualSetorDescricao;
        private string _SetorDescricao = string.Empty;
        private bool _IsManualFuncaoNome;
        private string _FuncaoNome = string.Empty;
        private bool _IsManualFuncaoDescricao;
        private string _FuncaoDescricao = string.Empty;

        private ArrayList listEmpregadoFuncao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AposentadoriaEspecial()
        {


        }

        public override int Id
        {
            get { return _IdAposentadoriaEspecial; }
            set { _IdAposentadoriaEspecial = value; }
        }
        public Empregado IdEmpregado
        {
            get { return _IdEmpregado; }
            set { _IdEmpregado = value; }
        }
        public DateTime DataLaudo
        {
            get { return _DataLaudo; }
            set { _DataLaudo = value; }
        }
        public bool IsHouveMudanca
        {
            get { return _IsHouveMudanca; }
            set { _IsHouveMudanca = value; }
        }
        public bool IsPossuiLaudo
        {
            get { return _IsPossuiLaudo; }
            set { _IsPossuiLaudo = value; }
        }
        public bool IsManualPeriodo
        {
            get { return _IsManualPeriodo; }
            set { _IsManualPeriodo = value; }
        }
        public bool CarimboCnpjRegistroEmpregado
        {
            get { return _CarimboCnpjRegistroEmpregado; }
            set { _CarimboCnpjRegistroEmpregado = value; }
        }
        public DateTime Inicio
        {
            get { return _Inicio; }
            set { _Inicio = value; }
        }
        public DateTime Termino
        {
            get { return _Termino; }
            set { _Termino = value; }
        }
        public Ghe IdGhe
        {
            get { return _IdGhe; }
            set { _IdGhe = value; }
        }
        public bool IsManualTempoExposicao
        {
            get { return _IsManualTempoExposicao; }
            set { _IsManualTempoExposicao = value; }
        }
        public string TempoExposicao
        {
            get { return _TempoExposicao; }
            set { _TempoExposicao = value; }
        }
        public bool IsManualSetorNome
        {
            get { return _IsManualSetorNome; }
            set { _IsManualSetorNome = value; }
        }
        public string SetorNome
        {
            get { return _SetorNome; }
            set { _SetorNome = value; }
        }
        public bool IsManualSetorDescricao
        {
            get { return _IsManualSetorDescricao; }
            set { _IsManualSetorDescricao = value; }
        }
        public string SetorDescricao
        {
            get { return _SetorDescricao; }
            set { _SetorDescricao = value; }
        }
        public bool IsManualFuncaoNome
        {
            get { return _IsManualFuncaoNome; }
            set { _IsManualFuncaoNome = value; }
        }
        public string FuncaoNome
        {
            get { return _FuncaoNome; }
            set { _FuncaoNome = value; }
        }
        public bool IsManualFuncaoDescricao
        {
            get { return _IsManualFuncaoDescricao; }
            set { _IsManualFuncaoDescricao = value; }
        }
        public string FuncaoDescricao
        {
            get { return _FuncaoDescricao; }
            set { _FuncaoDescricao = value; }
        }

        public override int Save()
        {
            this.IdDocumentoBase.Id = (int)Documentos.AE;

            return base.Save();
        }

        #region Metodos

        public ArrayList GetEmpregadoFuncao()
        {
            if (this.listEmpregadoFuncao == null)
            {
                string where = "nID_EMPREGADO=" + this.IdEmpregado.Id
                                + " AND hDT_INICIO <='" + this.DataLaudo.ToString("yyyy-MM-dd") + "'"
                                //+ " AND (hDT_TERMINO IS NULL OR  hDT_TERMINO <='2003-12-31')"
                                + " ORDER BY hDT_INICIO, hDT_TERMINO";
                
                listEmpregadoFuncao = new EmpregadoFuncao().Find(where);
            }

            return listEmpregadoFuncao;

        }

        public string GetJordadaTrabalho()
        {
            string ret;

            //			"44 horas semanais"
            //			"48 horas semanais"
            //			"48 h semanais até 04/10/88 e após 44 h semanais"

            if (this.IsManualTempoExposicao)
                ret = this.TempoExposicao;
            else
            {
                EmpregadoFuncao empregadoFuncao
                    = ((EmpregadoFuncao)listEmpregadoFuncao[listEmpregadoFuncao.Count - 1]);

                if (empregadoFuncao.nID_TEMPO_EXP.mirrorOld == null)
                    empregadoFuncao.nID_TEMPO_EXP.Find();

                ret = empregadoFuncao.nID_TEMPO_EXP.tHORA_EXTENSO_SEMANAL;
            }

            return ret;
        }

        public bool IsPertenceAoPeriodo85()
        {
            bool ret = false;

            foreach (EmpregadoFuncao empregadoFuncao in listEmpregadoFuncao)
            {
                ret = empregadoFuncao.IsPertenceAoPeriodo85();

                if (ret)
                    break;
            }

            return ret;
        }

        public bool IsPertenceAoPeriodo90()
        {
            bool ret = false;

            foreach (EmpregadoFuncao empregadoFuncao in listEmpregadoFuncao)
            {
                ret = empregadoFuncao.IsPertenceAoPeriodo90();

                if (ret)
                    break;
            }

            return ret;
        }

        public bool IsPertenceAoPeriodo80()
        {
            GetEmpregadoFuncao();

            bool ret = false;

            foreach (EmpregadoFuncao empregadoFuncao in listEmpregadoFuncao)
            {
                ret = empregadoFuncao.IsPertenceAoPeriodo80();

                if (ret)
                    break;
            }

            return ret;
        }

        public string GetConclusaoLTCAT()
        {
            string ret;

            ret = this.IdGhe.ConclusaoAE();

            return ret;
        }

        public DateTime GetInicioPeriodo()
        {
            GetEmpregadoFuncao();

            DateTime ret;

            if (this.IsManualPeriodo)
                ret = this.Inicio;
            else
                ret = ((EmpregadoFuncao)listEmpregadoFuncao[0]).hDT_INICIO;

            return ret;
        }

        public DateTime GetTerminoPeriodo()
        {
            GetEmpregadoFuncao();

            DateTime ret;

            if (this.IsManualPeriodo)
                ret = this.Termino;
            else
                ret = ((EmpregadoFuncao)listEmpregadoFuncao[listEmpregadoFuncao.Count - 1]).hDT_TERMINO;

            if (ret == new DateTime() || ret > new DateTime(2003, 12, 31))
                ret = new DateTime(2003, 12, 29);

            return ret;
        }

        public Ghe GetGheAtual()
        {
            GetEmpregadoFuncao();

            EmpregadoFuncao empregadoFuncao
                = ((EmpregadoFuncao)listEmpregadoFuncao[listEmpregadoFuncao.Count - 1]);

            return empregadoFuncao.GetGheEmpregado(false);
        }

        private string GetDataFormat(DateTime data)
        {
            string ret;

            if (data == new DateTime())
                ret = "-";
            else
                ret = data.ToString("dd-MM-yyyy");

            return ret;
        }

        public string GetInicio()
        {
            return GetDataFormat(GetInicioPeriodo());
        }

        public string GetTermino()
        {
            return GetDataFormat(GetTerminoPeriodo());
        }

        public string GetPeriodoAtividade()
        {
            string ret;

            if (this.IsManualPeriodo)
                ret = this.GetPeriodoAtividade(this.Inicio, this.Termino);
            else
                ret = this.GetPeriodoAtividade(GetInicioPeriodo(), GetTerminoPeriodo());

            return ret;
        }

        private string GetPeriodoAtividade(DateTime inicio, DateTime termino)
        {
            string ret;

            if (termino == new DateTime())
                ret = "De " + inicio.ToString("dd-MM-yyyy") + " ATÉ A PRESENTE DATA";
            else
            {
                if(termino >= new DateTime(2003, 12, 31))
                    ret =  "De " + inicio.ToString("dd-MM-yyyy") + " ATÉ 29-12-2003";
                else
                    ret =  "De " + inicio.ToString("dd-MM-yyyy") + " ATÉ " + termino.ToString("dd-MM-yyyy");
            }

            return ret;
        }

        public string GetCidade()
        {
            if (this.IdCliente.mirrorOld == null)
                this.IdCliente.Find();

            return this.IdCliente.GetEndereco().GetCidade();
        }

        public string GetRamoAtividade()
        {
            if (this.IdCliente.mirrorOld == null)
                this.IdCliente.Find();

            return this.IdCliente.Atividade;
        }

        public Prestador GetResponsavalPPP()
        {
            if (this.IdCliente.mirrorOld == null)
                this.IdCliente.Find();

            return this.IdCliente.IdRespPPP;
        }

        public static ArrayList GetClientes(ArrayList listEmpregadoFuncao)
        {
            ArrayList listCliente = new ArrayList();

            foreach (EmpregadoFuncao empregadoFuncao in listEmpregadoFuncao)
            {
                if (empregadoFuncao.nID_EMPR.mirrorOld == null)
                    empregadoFuncao.nID_EMPR.Find();

                bool bExists = false;

                foreach (Cliente cliente in listCliente)
                {
                    bExists = empregadoFuncao.nID_EMPR.Id == cliente.Id;

                    if (bExists)
                        break;
                }

                if (!bExists)
                    listCliente.Add(empregadoFuncao.nID_EMPR);
            }

            return listCliente;
        }

        public string GetNomeCliente(ArrayList listCliente, DateTime dataDocumento)
        {
            StringBuilder str = new StringBuilder();

            foreach (Cliente cliente in listCliente)
            {
                IPessoa iPessoa;
                IJuridica iJuridica;
                IEndereco iEndereco;

                if (cliente.IdJuridicaPai.Id != 0 && cliente.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Tomadora)
                {
                    if (cliente.IdJuridicaPai.mirrorOld == null)
                        cliente.IdJuridicaPai.Find();

                    cliente.IdJuridicaPai.GetDadosEmpresa(dataDocumento, out iPessoa, out iJuridica, out iEndereco);
                }
                else
                    cliente.GetDadosEmpresa(dataDocumento, out iPessoa, out iJuridica, out iEndereco);

                str.Append(iPessoa.NomeCompleto + "\r\n");
            }

            return str.ToString();
        }

        public string GetEnderecosCliente(ArrayList listCliente, DateTime dataDocumento)
        {
            StringBuilder str = new StringBuilder();

            foreach (Cliente cliente in listCliente)
            {
                IPessoa iPessoa;
                IJuridica iJuridica;
                IEndereco iEndereco;

                if (cliente.IdJuridicaPai.Id != 0 && cliente.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Tomadora)
                    cliente.IdJuridicaPai.GetDadosEmpresa(dataDocumento, out iPessoa, out iJuridica, out iEndereco);
                else
                    cliente.GetDadosEmpresa(dataDocumento, out iPessoa, out iJuridica, out iEndereco);

                str.Append(Endereco.GetEnderecoCompleto(iEndereco));

                if (listCliente.Count == 1)
                    break;

                str.Append(" (" + iPessoa.NomeAbreviado + ")\r\n");
            }

            return str.ToString();
        }

        public string GetCnpj(ArrayList listCliente)
        {
            StringBuilder str = new StringBuilder();

            foreach (Cliente cliente in listCliente)
            {
                str.Append(cliente.GetCnpj());

                if (listCliente.Count == 1)
                    break;

                str.Append(" (" + cliente.NomeAbreviado + ")\r\n");
            }

            return str.ToString();
        }

        public string GetNomeFuncao(ArrayList listCliente)
        {
            StringBuilder str = new StringBuilder();

            if (this.IsManualFuncaoNome)
                str.Append(this.FuncaoNome);
            else
            {
                foreach (EmpregadoFuncao empregadoFuncao in listEmpregadoFuncao)
                {
                    str.Append(empregadoFuncao.GetNomeFuncao());

                    if (listEmpregadoFuncao.Count == 1)
                        break;

                    str.Append(" - " + empregadoFuncao.GetPeriodoAE());

                    if (listCliente.Count > 1)
                        str.Append(" (" + empregadoFuncao.nID_EMPR.ToString() + ")");

                    str.Append("\r\n");
                }
            }

            return str.ToString();
        }

        public string GetDescricaoFuncao()
        {
            StringBuilder str = new StringBuilder();

            if (this.IsManualFuncaoDescricao)
                str.Append(this.FuncaoDescricao);
            else
            {
                foreach (EmpregadoFuncao empregadoFuncao in listEmpregadoFuncao)
                {
                    if (empregadoFuncao.nID_FUNCAO.mirrorOld == null)
                        empregadoFuncao.nID_FUNCAO.Find();

                    if (listEmpregadoFuncao.Count == 1)
                        str.Append(empregadoFuncao.GetDescricaoFuncao());
                    else
                    {
                        if (str.ToString().IndexOf(empregadoFuncao.GetNomeFuncao() + " - ") == -1)
                        {
                            str.Append(empregadoFuncao.GetNomeFuncao() + " - " + empregadoFuncao.GetDescricaoFuncao());
                            str.Append("\r\n");
                        }
                    }
                }
            }

            if (str.ToString().Length == 0)
                return string.Empty;
            else
                return str.ToString().Substring(0, str.ToString().Length - 1);
        }

        public string GetNomeSetor()
        {
            StringBuilder str = new StringBuilder();

            if (this.IsManualSetorNome)
                str.Append(this.SetorNome);
            else
            {
                foreach (EmpregadoFuncao empregadoFuncao in listEmpregadoFuncao)
                {
                    if (empregadoFuncao.nID_SETOR.mirrorOld == null)
                        empregadoFuncao.nID_SETOR.Find();

                    if (str.ToString().IndexOf(empregadoFuncao.nID_SETOR.tNO_STR_EMPR) == -1)
                    {
                        str.Append(empregadoFuncao.nID_SETOR.tNO_STR_EMPR);
                        str.Append(";");
                    }
                }
            }

            return str.ToString().Substring(0, str.ToString().Length - 1);
        }

        public string GetDescricaoSetor()
        {
            StringBuilder str = new StringBuilder();

            if (this.IsManualSetorDescricao)
                str.Append(this.SetorDescricao);
            else
            {
                foreach (EmpregadoFuncao empregadoFuncao in listEmpregadoFuncao)
                {
                    if (empregadoFuncao.nID_SETOR.mirrorOld == null)
                        empregadoFuncao.nID_SETOR.Find();

                    if (str.ToString().IndexOf(empregadoFuncao.nID_SETOR.mDS_STR_EMPR) == -1)
                    {
                        str.Append(empregadoFuncao.nID_SETOR.mDS_STR_EMPR);
                        str.Append(";");
                    }
                }
            }
            if (str.Length == 0)
                return string.Empty;
            else
                return str.ToString().Substring(0, str.ToString().Length - 1);
        }


        #endregion
    }
}
