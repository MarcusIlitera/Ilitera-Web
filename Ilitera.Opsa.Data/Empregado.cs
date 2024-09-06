using System;
using System.IO;
using System.Text;
using System.Data;
using System.Collections.Generic;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    #region enum

    public enum TipoBeneficiario : int
    {
        BeneficiarioReabilitado = 1,
        PortadorDeficiencia,
        NaoAplicavel
    }

    public enum TipoAdicional : int
    {
        NaoRecebe,//0
        Insalubridade, //10, 20, 40
        Periculosidade //30
    }

    public enum Vinculo : int
    {
        CLT,
        Terceiro,
        Estagiario,
        MenorAprendiz,
        Candidato
    }
    #endregion

    [Database("sied_novo", "tblEMPREGADO", "nID_EMPREGADO")]
    public class Empregado : Ilitera.Data.Table, IFoto
    {
        #region Properties
        private int _nID_EMPREGADO;
        private Cliente _nID_EMPR;
        private string _tNO_EMPG = string.Empty;
        private string _tNO_APELIDO = string.Empty;
        private string _tCOD_EMPR = string.Empty;
        private string _tNUM_CTPS = string.Empty;
        private string _tSER_CTPS = string.Empty;
        private string _tUF_CTPS = string.Empty;
        private DateTime _hDT_ADM;
        private DateTime _hDT_DEM;
        private int _nNO_FOTO;
        private string _tNO_CPF = String.Empty;
        private long _nNO_PIS_PASEP;
        private string _tNO_IDENTIDADE = String.Empty;
        private string _tSEXO = String.Empty;
        private DateTime _hDT_NASC;
        private float _nPESO;
        private float _nALTURA;
        private string _tEND_NOME = String.Empty;
        private string _tEND_NUMERO = String.Empty;
        private string _tEND_COMPL = String.Empty;
        private string _tEND_BAIRRO = String.Empty;
        private string _tEND_MUNICIPIO = String.Empty;
        private string _tEND_UF = String.Empty;
        private string _tEND_CEP = String.Empty;
        private string _tTELEFONE = string.Empty;
        private bool _gTERCEIRO;
        private int _nID_EMPR_TERCEIRO;
        private RegimeRevezamento _nID_REGIME_REVEZAMENTO;
        private int _nIND_BENEFICIARIO;
        private float _nAD_INSALUBRIDADE;
        private TipoAdicional _nIND_ADICIONAL;
        private string _teMail = string.Empty;
        private string _teMail_Resp = string.Empty;
        private string _CNH = string.Empty;
        private bool _isBrigadista;   //usar esse campo antigo para travar exames na transferência de colaboradores - origem.

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Empregado()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Empregado(int id)
        {
            this.Find(id);
        }

        public override int Id
        {
            get { return _nID_EMPREGADO; }
            set { _nID_EMPREGADO = value; }
        }

        [Obrigatorio(true, "O nome do Empregado é obrigatório!")]
        public string tNO_EMPG
        {
            get { return _tNO_EMPG; }
            set { _tNO_EMPG = value; }
        }
        public string tNO_APELIDO
        {
            get { return _tNO_APELIDO; }
            set { _tNO_APELIDO = value; }
        }
        public string tEND_NOME
        {
            get { return _tEND_NOME; }
            set { _tEND_NOME = value; }
        }
        public string tEND_NUMERO
        {
            get { return _tEND_NUMERO; }
            set { _tEND_NUMERO = value; }
        }

        public string tEND_COMPL
        {
            get { return _tEND_COMPL; }
            set { _tEND_COMPL = value; }
        }

        public string tEND_BAIRRO
        {
            get { return _tEND_BAIRRO; }
            set { _tEND_BAIRRO = value; }
        }

        public string tEND_MUNICIPIO
        {
            get { return _tEND_MUNICIPIO; }
            set { _tEND_MUNICIPIO = value; }
        }

        public string tEND_UF
        {
            get { return _tEND_UF; }
            set { _tEND_UF = value; }
        }

        public string tEND_CEP
        {
            get { return _tEND_CEP; }
            set { _tEND_CEP = value; }
        }

        public string tTELEFONE
        {
            get { return _tTELEFONE; }
            set { _tTELEFONE = value; }
        }

        public Cliente nID_EMPR
        {
            get { return _nID_EMPR; }
            set { _nID_EMPR = value; }
        }
        public string tCOD_EMPR
        {
            get { return _tCOD_EMPR; }
            set { _tCOD_EMPR = value; }
        }

        public string tNUM_CTPS
        {
            get { return _tNUM_CTPS; }
            set { _tNUM_CTPS = value; }
        }

        public string tSER_CTPS
        {
            get { return _tSER_CTPS; }
            set { _tSER_CTPS = value; }
        }
        public string tUF_CTPS
        {
            get { return _tUF_CTPS; }
            set { _tUF_CTPS = value; }
        }
        public DateTime hDT_ADM
        {
            get { return _hDT_ADM; }
            set { _hDT_ADM = value; }
        }
        public DateTime hDT_DEM
        {
            get { return _hDT_DEM; }
            set { _hDT_DEM = value; }
        }
        public int nNO_FOTO
        {
            get { return _nNO_FOTO; }
            set { _nNO_FOTO = value; }
        }
        public int nID_EMPR_TERCEIRO
        {
            get { return _nID_EMPR_TERCEIRO; }
            set { _nID_EMPR_TERCEIRO = value; }
        }
        public RegimeRevezamento nID_REGIME_REVEZAMENTO
        {
            get { return _nID_REGIME_REVEZAMENTO; }
            set { _nID_REGIME_REVEZAMENTO = value; }
        }
        public int nIND_BENEFICIARIO
        {
            get { return _nIND_BENEFICIARIO; }
            set { _nIND_BENEFICIARIO = value; }
        }
        public string tNO_CPF
        {
            get { return _tNO_CPF; }
            set { _tNO_CPF = value; }
        }
        public long nNO_PIS_PASEP
        {
            get { return _nNO_PIS_PASEP; }
            set
            {
//				CÁLCULO DO DÍGITO VERIFICADOR - NIT/PIS/PASEP
//
//					Formato : NNNNNNNNNND
//					Onde:
//					NNNNNNNNN - Número do Identificador
//					D - Dígito Verificador 
//
//					a) Multiplicar os 11 últimos algarismos pelos pesos conforme ilustração abaixo: 
//
//					pesos 3 2 9 8 7 6 5 4 3 2  dígito verificador 
//					X X X X X X X X X X - D 
//
//					b) Somar todos os produtos obtidos no item "a".
//					c) Dividir o somatório do item "b" por 11.
//					d) Subtrair de 11 o resto da divisão do item "c".
//
//					O resultado será o dígito verificador. Caso o resultado da subtração seja 10 ou 11, o dígito será 0.
                _nNO_PIS_PASEP = value;
            }
        }

        public string tNO_IDENTIDADE
        {
            get { return _tNO_IDENTIDADE; }
            set { _tNO_IDENTIDADE = value; }
        }

        public string tSEXO
        {
            get
            {
                return _tSEXO;
            }
            set
            {
                if (value.Length > 1)
                    value = value.Substring(0, 1);

                if (value == "M" || value == "F" || value == "S" || value.Trim() == string.Empty)
                    _tSEXO = value;
            }
        }

        public DateTime hDT_NASC
        {
            get { return _hDT_NASC; }
            set { _hDT_NASC = value; }
        }
        public float nPESO
        {
            get { return _nPESO; }
            set { _nPESO = value; }
        }
        public float nALTURA
        {
            get { return _nALTURA; }
            set { _nALTURA = value; }
        }
        public bool gTERCEIRO
        {
            get { return _gTERCEIRO; }
            set { _gTERCEIRO = value; }
        }
        public bool isBrigadista
        {
            get { return _isBrigadista; }
            set { _isBrigadista = value; }
        }
        public float nAD_INSALUBRIDADE
        {
            get { return _nAD_INSALUBRIDADE; }
            set { _nAD_INSALUBRIDADE = value; }
        }
        public TipoAdicional nIND_ADICIONAL
        {
            get { return _nIND_ADICIONAL; }
            set { _nIND_ADICIONAL = value; }
        }
        public string teMail
        {
            get { return _teMail; }
            set { _teMail = value; }
        }
        public string teMail_Resp
        {
            get { return _teMail_Resp; }
            set { _teMail_Resp = value; }
        }
        public string CNH
        {
            get { return _CNH; }
            set { _CNH = value; }
        }


        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return _tNO_EMPG;
        }
        #endregion

        #region IFoto

        [Persist(false)]
        public string FotoDiretorio
        {
            get
            {
                return GetDiretorioPadrao(this.nID_EMPR);
            }
        }
        [Persist(false)]
        public string FotoInicio
        {
            get
            {
                if (this.nID_EMPR.mirrorOld == null)
                    this.nID_EMPR.Find();

                return this.nID_EMPR.ArqFotoEmpregInicio;
            }
        }
        [Persist(false)]
        public string FotoTermino
        {
            get
            {
                if (this.nID_EMPR.mirrorOld == null)
                    this.nID_EMPR.Find();

                return this.nID_EMPR.ArqFotoEmpregTermino;
            }
        }
        [Persist(false)]
        public byte FotoTamanho
        {
            get
            {
                if (this.nID_EMPR.mirrorOld == null)
                    this.nID_EMPR.Find();

                return Convert.ToByte(this.nID_EMPR.ArqFotoEmrpegQteDigitos);
            }
        }
        [Persist(false)]
        public string FotoExtensao
        {
            get
            {
                if (this.nID_EMPR.mirrorOld == null)
                    this.nID_EMPR.Find();

                return this.nID_EMPR.ArqFotoEmpregExtensao;
            }
        }
        #endregion

        #region Override Table

        #region Validate

        public override void Validate()
        {
            if (this.nID_EMPR.mirrorOld == null)
                this.nID_EMPR.Find();

            //if (this.nID_EMPR.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Tomadora)
            //    throw new Exception("O Empregado não pode ser cadastrado"
            //        + " em empresa do tipo tomadora de serviços!");

            //if (this.nID_EMPR.IdJuridicaPapel.Id == (int)IndJuridicaPapel.Obras)
            //    throw new Exception("O Empregado não pode ser cadastrado"
            //        + " em empresa do tipo Obras!");

            if (this.nIND_ADICIONAL == TipoAdicional.Periculosidade)
                this.nAD_INSALUBRIDADE = 30;

            if (this.nAD_INSALUBRIDADE == 30)
                this.nIND_ADICIONAL = TipoAdicional.Periculosidade;

            if ((this.nAD_INSALUBRIDADE == 10
                || this.nAD_INSALUBRIDADE == 20
                || this.nAD_INSALUBRIDADE == 40)
                && this.nIND_ADICIONAL == TipoAdicional.NaoRecebe)
                this.nIND_ADICIONAL = TipoAdicional.Insalubridade;

            if (tNO_CPF != string.Empty)
            {
                StringBuilder cpf = new StringBuilder();
                cpf.Append(this.tNO_CPF)
                    .Replace(".", string.Empty)
                    .Replace("-", string.Empty)
                    .Replace(" ", string.Empty);

                if (!Pessoa.VeriricaCPF(cpf.ToString()))
                    this.tNO_CPF = string.Empty;
                else
                    this.tNO_CPF = cpf.ToString();
            }

            base.Validate();
        }
        #endregion

        #region Save

        public int Save(bool checkDuplicado)
        {
            if (checkDuplicado)
            {
                if (this.Id == 0)
                {
                    string criteria = "nID_EMPR=" + this.nID_EMPR.Id
                                    + " AND hDT_DEM IS NULL"
                                    + " AND tNO_EMPG='" + this.tNO_EMPG.Replace("'", "''") + "'";

                    List<Empregado> list = new Empregado().Find<Empregado>(criteria);

                    if (list.Count > 0)
                        throw new ExceptionEmpregadoExistente("O Empregado já está cadastrado!");
                }
                return base.Save();
            }
            else
                return base.Save();
        }

        public override int Save()
        {
            return this.Save(true);
        }
        #endregion

        #region Delete

        public override void Delete()
        {
            DataSet ds = new ExameBase().Get("IdEmpregado=" + this.Id);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                throw new Exception("Este empregado não pode ser excluído por não possuir exames");

            base.Delete();
        }
        #endregion

        #endregion

        #region Metodos

        #region Outros (GetNomeEmpregadoComRE / GetStatusEmpregado / GetDataNascimento / IdadeEmpregado / TempoEmpresaEmpregado)

        public string GetNomeEmpregadoComRE()
        {
            if (this.mirrorOld == null)
                this.Find();

            if (this.tCOD_EMPR == string.Empty)
                return this.tNO_EMPG;
            else
                return this.tCOD_EMPR + " - " + this.tNO_EMPG;
        }

        public string GetStatusEmpregado()
        {
            string ret;

            List<Afastamento> list = new Afastamento().Find<Afastamento>("IdEmpregado=" + this.Id
                                                                        + " AND DataVolta IS NULL");
            if (list.Count > 0)
                ret = "Afastado";
            else if (this.hDT_DEM != new DateTime())
                ret = "Demitido";
            else
                ret = "Ativo";

            return ret;
        }

        public string GetDataNascimento()
        {
            return (this.hDT_NASC != new DateTime()) ? this.hDT_NASC.ToString("dd-MM-yyyy") : string.Empty;
        }

        public int IdadeEmpregado()
        {
            if (this.hDT_NASC == new DateTime())
            {
                return 0;
            }
            else
            {
                DateTime BirthDate = this.hDT_NASC;
                // get the difference in years
                int years = DateTime.Now.Year - BirthDate.Year;
                // subtract another year if we're before the
                // birth day in the current year
                if (DateTime.Now.Month < BirthDate.Month ||
                    (DateTime.Now.Month == BirthDate.Month && DateTime.Now.Day < BirthDate.Day))
                    years--;

                return years;
            }
        }

        public string TempoEmpresaEmpregado()
        {
            System.TimeSpan periodo = new TimeSpan();

            if (this.hDT_DEM == new DateTime() || this.hDT_DEM == new DateTime(1753, 1, 1))
                periodo = DateTime.Today - this.hDT_ADM;
            else
                periodo = this.hDT_DEM - this.hDT_ADM;

            if ((int)periodo.TotalDays / 31 <= 0)
                return "&nbsp;";
            else if ((int)periodo.TotalDays / 31 == 1)
                return "1 mês";
            else
                return (int)periodo.TotalDays / 31 + " meses";
        }

        public string GetRecebePericulosidade()
        {
            if (this.nIND_ADICIONAL == TipoAdicional.Periculosidade)
                return "S";
            else
                return "N";
        }

        public char GetRecebeInsalubridade()
        {
            if (this.nIND_ADICIONAL == TipoAdicional.Insalubridade)
                return 'S';
            else
                return 'N';
        }

        public void SetAdicionalEmpregado(char RecebeInsalubridade,
                                            char RecebePericulosidade)
        {
            if (RecebeInsalubridade == 'N' && RecebePericulosidade == 'N')
                this.nIND_ADICIONAL = TipoAdicional.NaoRecebe;
            else if (RecebeInsalubridade == 'S' && RecebePericulosidade == 'N')
                this.nIND_ADICIONAL = TipoAdicional.Insalubridade;
            else if (RecebeInsalubridade == 'N' && RecebePericulosidade == 'S')
                this.nIND_ADICIONAL = TipoAdicional.Periculosidade;
        }

        #endregion

        #region GetCTPS

        public string GetCTPS()
        {
            return GetCTPS(this.tNUM_CTPS, this.tSER_CTPS, this.tUF_CTPS);
        }

        public static string GetCTPS(string sNum, string sSerie, string sUf)
        {
            string ret;
            int numero = 0;
            int serie = 0;
            try
            {
                if (sNum.Trim().IndexOf("-") != -1)
                    numero = Convert.ToInt32(sNum.Trim().Substring(0, sNum.Trim().IndexOf("-")));
                else if (sNum.Trim().IndexOf("/") != -1)
                    numero = Convert.ToInt32(sNum.Trim().Substring(0, sNum.Trim().IndexOf("/")));
                else if (sNum.Trim().IndexOf(".") != -1)
                    numero = Convert.ToInt32(sNum.Trim().Substring(0, sNum.Trim().IndexOf(".")));
                else
                    numero = Convert.ToInt32(sNum.Trim());
            }
            catch
            {

            }
            try
            {
                if (sNum.Trim().IndexOf("-") != -1)
                    serie = Convert.ToInt32(sNum.Trim().Substring(sNum.Trim().IndexOf("-") + 1, sNum.Length - (sNum.Trim().IndexOf("-") + 1)));
                else if (sNum.Trim().IndexOf("/") != -1)
                    serie = Convert.ToInt32(sNum.Trim().Substring(sNum.Trim().IndexOf("/") + 1, sNum.Length - (sNum.Trim().IndexOf("/") + 1)));
                else if (sNum.Trim().IndexOf(".") != -1)
                    serie = Convert.ToInt32(sNum.Trim().Substring(sNum.Trim().IndexOf(".") + 1, sNum.Length - (sNum.Trim().IndexOf(".") + 1)));
                else
                    serie = Convert.ToInt32(sSerie.Trim());
            }
            catch
            {

            }
            if (numero != 0 && serie != 0)
                ret = numero.ToString() + "-" + serie.ToString() + " " + sUf.Trim();
            else if (numero != 0 && serie == 0)
            {
                if (sSerie.Trim() != string.Empty)
                    ret = numero.ToString() + "-" + sSerie + " " + sUf.Trim();
                else
                    ret = numero.ToString() + "-" + serie.ToString() + " " + sUf.Trim();
            }
            else
            {
                if (sNum.Trim() != string.Empty)
                    ret = sNum + "-" + sSerie + " " + sUf.Trim();
                else if (sNum.Trim() == string.Empty && sSerie.Trim() != string.Empty)
                    ret = numero.ToString() + "-" + sSerie + " " + sUf.Trim();
                else
                    ret = sSerie.Trim() + " " + sUf.Trim();
            }
            return ret;
        }

        #endregion

        #region GetDataUltimoExame

        public DateTime GetDataUltimoExame( ExameDicionario exameDicionario, 
                                            DateTime dataInicioPcmso, 
                                            DateTime dataTerminoPcmso)
        {
            StringBuilder criteria = new StringBuilder();

            criteria.Append("IdEmpregado=" + this.Id);

            if (exameDicionario.Id == (int)IndExameClinico.Periodico)
                criteria.Append(" AND IdExameDicionario IN (1, 2, 3, 4, 5)");
            else
            {
                if (exameDicionario.Nome == "Audiometria")
                    criteria.Append(" AND IdExameDicionario in ( 6, " + exameDicionario.Id.ToString() + " ) ");
                else
                    criteria.Append(" AND IdExameDicionario =" + exameDicionario.Id);
            }

            criteria.Append(" AND IndResultado <>" + (int)ResultadoExame.NaoRealizado);

            if (dataInicioPcmso != new DateTime()
                && dataTerminoPcmso != new DateTime())
                criteria.Append(" AND DataExame BETWEEN '"
                                    + dataInicioPcmso.ToString("yyyy-MM-dd")
                                    + "' AND '"
                                    + dataTerminoPcmso.ToString("yyyy-MM-dd") + "'");
            
            ExameBase ultimoExame = new ExameBase();

            System.Collections.ArrayList 
                listExameBase = ultimoExame.FindMax("DataExame", criteria.ToString());

            DateTime dataUltima;

            if (listExameBase.Count == 1)
                dataUltima = new DateTime(ultimoExame.DataExame.Year, ultimoExame.DataExame.Month, ultimoExame.DataExame.Day);
            else
                dataUltima = new DateTime();

            return dataUltima;
        }

        #endregion

        #region GerarExamePlanejamento

        public void GerarExamePlanejamento(EmpregadoFuncao empregadoFuncao, Ghe ghe)
        {
            Pcmso pcmso = empregadoFuncao.nID_EMPR.GetUltimoPcmso();

            if (pcmso.Id == 0)
                return;

            string str = "IdGhe=" + ghe.Id + " AND IdPcmso=" + pcmso.Id + " AND Periodico=1";

            List<PcmsoPlanejamento> list = new PcmsoPlanejamento().Find<PcmsoPlanejamento>(str);

            if (list.Count == 0)
                return;

            DateTime dataProxima = pcmso.GetDataProximoPeriodico();

            Pedido ultimoPedido = Pedido.GetUltimoPedido((int)Obrigacoes.ExamesPeriodicos,
                                                        pcmso.IdCliente.Id);


            foreach (PcmsoPlanejamento pcmsoPlanejamento in list)
            {
                this.GerarExamePlanejamento(pcmso,
                                            pcmsoPlanejamento,
                                            pcmsoPlanejamento.IdExameDicionario,
                                            ultimoPedido,
                                            dataProxima,
                                            ghe.DoseUltrapassada(), "N" );
            }
        }

        public void GerarExamePlanejamento(Pcmso pcmso,
                                            PcmsoPlanejamento pcmsoPlanejamento,
                                            ExameDicionario exameDicionario,
                                            Pedido ultimoPedido,
                                            DateTime dataProxima,
                                            bool bPossuiRisco, string zConsiderar_Mudanca_Funcao)
        {
            DateTime dataUltima;
            DateTime dataVencimento;

            int intervalo = 0;
            int periodicidade = 0;

            if (exameDicionario.mirrorOld == null)
                exameDicionario.Find();

            try
            {

                if (exameDicionario.Nome.ToUpper().IndexOf("AVALIAÇÃO CL") > 0 || exameDicionario.Nome.ToUpper().IndexOf("AVALIACAO CL") > 0)
                    return;

                ExamePlanejamento examePlanejamento = new ExamePlanejamento();
                examePlanejamento.Find("IdPcmso=" + pcmso.Id
                                        + " AND IdEmpregado=" + this.Id
                                        + " AND IdExameDicionario=" + exameDicionario.Id);

                //Verificação
                if ((exameDicionario.IsObservacao)
                    || (exameDicionario.IndSexo == (int)IndTipoSexo.Feminino && this.tSEXO != "F")
                    || (exameDicionario.IndSexo == (int)IndTipoSexo.Masculino && this.tSEXO != "M"))
                {
                    if (examePlanejamento.Id != 0)
                        examePlanejamento.Delete();

                    return;
                }

                dataUltima = GetDataUltimoExame(exameDicionario,
                                                pcmso.DataPcmso.AddYears(-1),
                                                pcmso.GetTerninoPcmso(), zConsiderar_Mudanca_Funcao);

                if (dataUltima == new DateTime())
                    dataUltima = GetDataUltimoExame(exameDicionario,
                                                    new DateTime(),
                                                    new DateTime(), zConsiderar_Mudanca_Funcao);

                //Colocar os exames digitalizados
                if (dataUltima == new DateTime() && exameDicionario.IndExame == (int)IndTipoExame.Clinico)
                {
                    string criteria = "IdEmpregado=" + this.Id
                                    + " AND IndTipoDocumento IN ("
                                    + (int)TipoDocumentoDigitalizado.Periodico
                                    + ", " + (int)TipoDocumentoDigitalizado.Admissional
                                    + ", " + (int)TipoDocumentoDigitalizado.Demissional
                                    + ", " + (int)TipoDocumentoDigitalizado.MudancaDeFuncao
                                    + ", " + (int)TipoDocumentoDigitalizado.RetornoAoTrabalho
                                    + ") ORDER BY DataProntuario DESC";

                    List<ProntuarioDigital> listProntuariosDig
                        = new ProntuarioDigital().Find<ProntuarioDigital>(criteria);

                    if (listProntuariosDig.Count > 0)
                        dataUltima = listProntuariosDig[0].DataProntuario;
                }

                //Admissao menos 90 dias
                if (dataUltima == new DateTime() && exameDicionario.IndExame == (int)IndTipoExame.Clinico
                    && this.hDT_ADM >= DateTime.Today.AddDays(-90))
                    dataUltima = new DateTime(this.hDT_ADM.Year, this.hDT_ADM.Month, this.hDT_ADM.Day);

                //Depende da idade
                if (this.IdadeEmpregado() != 0 && pcmsoPlanejamento.DependeIdade)
                {
                    //Possui risco e é periódico
                    if (bPossuiRisco && exameDicionario.Id == (int)IndExameClinico.Periodico)
                    {
                        intervalo = 1;
                        periodicidade = (int)Periodicidade.Ano;
                    }
                    else
                    {
                        List<PcmsoPlanejamentoIdade>
                            list = new PcmsoPlanejamentoIdade().Find<PcmsoPlanejamentoIdade>("IdPcmsoPlanejamento=" + pcmsoPlanejamento.Id);

                        foreach (PcmsoPlanejamentoIdade pcmsoPlanIdade in list)
                        {
                            if ((pcmsoPlanIdade.IndSexoIdade == (int)IndTipoSexo.Feminino && this.tSEXO != "F")
                                || (pcmsoPlanIdade.IndSexoIdade == (int)IndTipoSexo.Masculino && this.tSEXO != "M"))
                                continue;
                            if (this.IdadeEmpregado() >= pcmsoPlanIdade.AnoInicio && this.IdadeEmpregado() <= pcmsoPlanIdade.AnoTermino)
                            {
                                intervalo = pcmsoPlanIdade.Intervalo;
                                periodicidade = pcmsoPlanIdade.IndPeriodicidade;

                                if (intervalo == 0) return;
                            }
                        }
                    }
                }
                else//não depende da idade
                {
                    intervalo = pcmsoPlanejamento.Intervalo;
                    periodicidade = pcmsoPlanejamento.IndPeriodicidade;
                }


                //if (dataUltima == new DateTime())
                //{
                //    dataUltima = this.hDT_ADM;
                //}

                //Vencimento do Exame
                if (dataUltima == new DateTime())
                    dataVencimento = new DateTime();
                else
                    dataVencimento = ObrigacaoCliente.AddData(dataUltima, intervalo, periodicidade);



                if (pcmsoPlanejamento.AposAdmissao == true)
                {
                    int intervaloadm = 0;
                    int periodicidadeadm = 0;

                    intervaloadm = pcmsoPlanejamento.IntervaloAposAdmissao;
                    periodicidadeadm = pcmsoPlanejamento.IndPeriodicidadeAposAdmissao;

                    DateTime dataVencimentoAdm;

                    dataVencimentoAdm = ObrigacaoCliente.AddData(this.hDT_ADM, intervaloadm, periodicidadeadm);

                    if (dataVencimentoAdm.AddMonths(3) > DateTime.Now && dataVencimentoAdm < dataVencimento && dataUltima.AddMonths(1) < dataVencimentoAdm)
                        dataVencimento = dataVencimentoAdm;

                }


                if (dataProxima != new DateTime())//Se vazia a atualização é por vencimento do exame
                {
                    //Data Previsão da Próxima
                    if (ultimoPedido.Id != 0)
                    {
                        int diasDif = dataVencimento.Subtract(dataProxima).Days;

                        if (diasDif >= 365 && diasDif <= (365 * 2))
                            dataProxima = dataProxima.AddYears(1);
                        else if (diasDif >= (365 * 2) && diasDif <= (365 * 3))
                            dataProxima = dataProxima.AddYears(2);
                        else if (diasDif >= (365 * 3))
                            dataProxima = dataProxima.AddYears(3);
                    }
                }
                else
                {
                    if (dataVencimento != new DateTime())
                        dataProxima = dataVencimento;
                    else
                        dataProxima = pcmso.DataPcmso;
                }

                //Cria o exame planejamento
                if (examePlanejamento.Id == 0)
                {
                    examePlanejamento.Inicialize();
                    examePlanejamento.IdEmpregado = this;
                    examePlanejamento.IdPcmso = pcmso;
                    examePlanejamento.IdExameDicionario = exameDicionario;
                }
                examePlanejamento.IdPcmsoPlanejamento = pcmsoPlanejamento;
                examePlanejamento.PossuiRisco = bPossuiRisco;
                examePlanejamento.Intervalo = intervalo;
                examePlanejamento.IndPeriodicidade = periodicidade;
                examePlanejamento.DataVencimento = dataVencimento;
                examePlanejamento.DataUltima = dataUltima;
                examePlanejamento.DataProxima = dataProxima;
                examePlanejamento.Save();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
        }




        public DateTime GetDataUltimoExame(ExameDicionario exameDicionario,
                                            DateTime dataInicioPcmso,
                                            DateTime dataTerminoPcmso, string zConsiderar_Mudanca_Funcao)
        {
            StringBuilder criteria = new StringBuilder();

            criteria.Append("IdEmpregado=" + this.Id);

            if (exameDicionario.Id == (int)IndExameClinico.Periodico)
                if (zConsiderar_Mudanca_Funcao == "N")
                    criteria.Append(" AND IdExameDicionario IN (1, 2, 4, 5)");  //retirei mudança de função
                else
                    criteria.Append(" AND IdExameDicionario IN (1, 2, 3, 4, 5)");
            else
            {
                if (exameDicionario.Nome == "Audiometria")
                    criteria.Append(" AND IdExameDicionario in ( 6, " + exameDicionario.Id.ToString() + " ) ");
                else
                    criteria.Append(" AND IdExameDicionario =" + exameDicionario.Id);

            }



            // if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Prajna") > 0)
            // {
            criteria.Append(" AND IndResultado <>" + (int)ResultadoExame.NaoRealizado + " AND IndResultado <>" + (int)ResultadoExame.EmEspera);
            // }
            // else
            // {
            //     criteria.Append(" AND IndResultado <>" + (int)ResultadoExame.NaoRealizado);
            // }

            if (dataInicioPcmso != new DateTime()
                && dataTerminoPcmso != new DateTime())
                criteria.Append(" AND DataExame BETWEEN '"
                                    + dataInicioPcmso.ToString("yyyy-MM-dd")
                                    + "' AND '"
                                    + dataTerminoPcmso.ToString("yyyy-MM-dd") + "'");

            ExameBase ultimoExame = new ExameBase();

            System.Collections.ArrayList
                listExameBase = ultimoExame.FindMax("DataExame", criteria.ToString());

            DateTime dataUltima;

            if (listExameBase.Count > 1)
            {
                ultimoExame = (ExameBase)listExameBase[0];
            }


            if (listExameBase.Count >= 1)
                dataUltima = new DateTime(ultimoExame.DataExame.Year, ultimoExame.DataExame.Month, ultimoExame.DataExame.Day);
            else
                dataUltima = new DateTime();

            return dataUltima;
        }


        #endregion

        #region Foto

        public static string GetDiretorioPadrao(Cliente cliente)
        {
            string ret = Path.Combine(Fotos.GetRaizPath(), 
                         Path.Combine(cliente.GetFotoDiretorioPadrao(), "Organogramas"));

            return ret;
        }

        public string FotoEmpregado()
        {
            if (this.Id != 0 && this.mirrorOld == null)
                this.Find();

            if (this.nID_EMPR.mirrorOld == null)
                this.nID_EMPR.Find();

            return FotoEmpregado(this.nID_EMPR);
        }

        public string FotoEmpregado(Cliente cliente)
        {
            string path = string.Empty;

            if (nNO_FOTO != 0)
            {
                string fileName = Fotos.GetFileName(this, this.nNO_FOTO);

                try
                {
                    path = Fotos.PathSmallFoto(Path.Combine(GetDiretorioPadrao(cliente), fileName));

                    if ( System.IO.File.Exists(path)==false)
                    {
                        path = Fotos.PathFoto(Path.Combine(GetDiretorioPadrao(cliente), fileName));
                    }
                }
                catch
                {
                    path = Path.Combine(GetDiretorioPadrao(cliente), fileName);
                }
            }

            if (path.ToUpper().IndexOf("FOTOSDOCSDIGITAIS") < 1)
            {
                path = path.Substring(0, path.IndexOf("\\") + 1) + "FOTOSDOCSDIGITAIS\\" + path.Substring(path.IndexOf("\\") + 1);
            }


            return path;
        }

        public string FotoEmpregadoUrl()
        {
            string path = string.Empty;

            if (nNO_FOTO != 0)
            {
                path = Fotos.PathFoto_Uri(this, this.nNO_FOTO);

                path = Ilitera.Common.Fotos.UrlFoto(path);
            }
            else
                path = "../img/foto_null.gif";

            return path;
        }
        #endregion

        #region PPP

        public string GetTipoBeneficiario()
        {
            return GetTipoBeneficiario(this.nIND_BENEFICIARIO);
        }

        public static string GetTipoBeneficiario(int IndTipoBeneficiario)
        {
            switch (IndTipoBeneficiario)
            {
                case (int)TipoBeneficiario.NaoAplicavel:
                    return "NA";

                case (int)TipoBeneficiario.BeneficiarioReabilitado:
                    return "BR";

                case (int)TipoBeneficiario.PortadorDeficiencia:
                    return "PDH";

                default:
                    return "NA";
            }
        }

        public string GetStrPPPNullValues()
        {
         
            if (this.nID_EMPR.mirrorOld == null)
                this.nID_EMPR.Find();

            if (this.nID_EMPR.IdRespPPP.mirrorOld == null)
                this.nID_EMPR.IdRespPPP.Find();

            StringBuilder st = new StringBuilder();

            if (this.nNO_PIS_PASEP == 0)
                st.Append("NIT(PIS/PASEP)\n");

            if (this.hDT_NASC == new DateTime() || this.hDT_NASC == new DateTime(1753, 1, 1))
                st.Append("Data de Nascimento\n");

            if (this.tSEXO.Trim() == string.Empty)
                st.Append("Sexo(F/M)\n");

            if (this.tNUM_CTPS.Trim() == string.Empty)
                st.Append("CTPS\n");

            if (this.hDT_ADM == new DateTime() || this.hDT_ADM == new DateTime(1753, 1, 1))
                st.Append("Data de Admissão\n");

            if (this.hDT_DEM == new DateTime() || this.hDT_DEM == new DateTime(1753, 1, 1))
                st.Append("Data de Demissão\n");

            //if (this.nID_EMPR.IdRespPPP.Id == 0)
              //  st.Append("Preposto da Empresa\n");
            //else if (this.nID_EMPR.IdRespPPP.NomeCodigo == string.Empty)
              //  st.Append("NIT do Preposto da Empresa\n");

            return st.ToString();
        }

        public bool UsaEPI()
        {
            StringBuilder criteria = new StringBuilder();
            criteria.Append("nID_EMPREGADO=" + this.Id)
                    .Append(" AND nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM tblEMPREGADO_FUNCAO WHERE")
                    .Append(" nID_EMPREGADO_FUNCAO IN (SELECT nID_EMPREGADO_FUNCAO FROM tblFUNC_EMPREGADO WHERE")
                    .Append(" nID_FUNC IN (SELECT nID_FUNC FROM tblPPRA1 WHERE")
                    .Append(" nID_PPRA IN (SELECT nID_PPRA FROM tblEPI_EXTE))))");

            int count = new Empregado().ExecuteCount(criteria.ToString());

            return count > 0;
        }

        #endregion

        #region GetProcedimento

        public DataSet GetProcedimentos()
        {
            StringBuilder st = new StringBuilder();
            st.Append("SELECT DISTINCT " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nNO_POPS, " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.tNO_PROCEDIMENTO, ");
            st.Append("				 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nID_PROCEDIMENTO, " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nTIPO_PROCEDIMENTO ");
            st.Append("FROM         " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblCONJUNTO_EMPREGADO INNER JOIN " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblCONJUNTO ");
            st.Append("			ON	 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblCONJUNTO_EMPREGADO.nID_CONJUNTO = " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblCONJUNTO.nID_CONJUNTO INNER JOIN " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblCONJUNTO_PROCEDIMENTO ");
            st.Append("			ON	 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblCONJUNTO.nID_CONJUNTO = " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblCONJUNTO_PROCEDIMENTO.nID_CONJUNTO INNER JOIN " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO ");
            st.Append("			ON	 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblCONJUNTO_PROCEDIMENTO.nID_PROCEDIMENTO = " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nID_PROCEDIMENTO ");
            st.Append("WHERE		( " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblCONJUNTO_EMPREGADO.nID_EMPREGADO = " + this.Id + ") AND ( " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nTIPO_PROCEDIMENTO <> 2) ");
            st.Append("UNION ");
            st.Append("SELECT DISTINCT " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nNO_POPS, " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.tNO_PROCEDIMENTO, ");
            st.Append("				 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nID_PROCEDIMENTO, " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nTIPO_PROCEDIMENTO ");
            st.Append("FROM			 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO INNER JOIN " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO_EMPREGADO ");
            st.Append("			ON	 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nID_PROCEDIMENTO = " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO_EMPREGADO.nID_PROCEDIMENTO ");
            st.Append("WHERE		( " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO_EMPREGADO.nID_EMPREGADO = " + this.Id + ") AND ( " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nTIPO_PROCEDIMENTO <> 2) ");
            st.Append("ORDER BY  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nNO_POPS");

            return new Empregado().ExecuteDataset(st.ToString());
        }

        public DataSet GetProcedimentosGenericos()
        {
            StringBuilder st = new StringBuilder();
            st.Append("SELECT DISTINCT " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nNO_POPS, " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.tNO_PROCEDIMENTO, ");
            st.Append("				 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nID_PROCEDIMENTO, " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nTIPO_PROCEDIMENTO ");
            st.Append("FROM         " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblCONJUNTO_EMPREGADO INNER JOIN " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblCONJUNTO ");
            st.Append("			ON	 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblCONJUNTO_EMPREGADO.nID_CONJUNTO = " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblCONJUNTO.nID_CONJUNTO INNER JOIN " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblCONJUNTO_PROCEDIMENTO ");
            st.Append("			ON	 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblCONJUNTO.nID_CONJUNTO = " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblCONJUNTO_PROCEDIMENTO.nID_CONJUNTO INNER JOIN " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO ");
            st.Append("			ON	 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblCONJUNTO_PROCEDIMENTO.nID_PROCEDIMENTO = " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nID_PROCEDIMENTO ");
            st.Append("WHERE		( " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblCONJUNTO_EMPREGADO.nID_EMPREGADO = " + this.Id + ") AND ( " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nTIPO_PROCEDIMENTO = 2) ");
            st.Append("UNION ");
            st.Append("SELECT DISTINCT " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nNO_POPS, " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.tNO_PROCEDIMENTO, ");
            st.Append("				 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nID_PROCEDIMENTO, " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nTIPO_PROCEDIMENTO ");
            st.Append("FROM			 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO INNER JOIN " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO_EMPREGADO ");
            st.Append("			ON	 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nID_PROCEDIMENTO = " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO_EMPREGADO.nID_PROCEDIMENTO ");
            st.Append("WHERE		( " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO_EMPREGADO.nID_EMPREGADO = " + this.Id + ") AND ( " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nTIPO_PROCEDIMENTO = 2) ");
            st.Append("ORDER BY  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblPROCEDIMENTO.nNO_POPS");

            return new Empregado().ExecuteDataset(st.ToString());
        }

        #endregion

        #region VerificaNull

        private string VerificaNull(string campo, string cnull)
        {
            if (campo.Equals("") || campo.Equals("0"))
                return cnull;
            else
                return campo;
        }

        public string VerificaNullCampoString(string campo, string caracnull)
        {
            string vcampo;

            switch ("_" + campo)
            {
                case "_tCOD_EMPR":
                    vcampo = VerificaNull(this.tCOD_EMPR.ToString(), caracnull);
                    break;
                case "_tNUM_CTPS":
                    vcampo = VerificaNull(this.tNUM_CTPS.ToString(), caracnull);
                    break;
                case "_tSER_CTPS":
                    vcampo = VerificaNull(this.tSER_CTPS.ToString(), caracnull);
                    break;
                case "_tUF_CTPS":
                    vcampo = VerificaNull(this.tUF_CTPS.ToString(), caracnull);
                    break;
                case "_tNO_IDENTIDADE":
                    vcampo = VerificaNull(this.tNO_IDENTIDADE.ToString(), caracnull);
                    break;
                case "_tEND_NOME":
                    vcampo = VerificaNull(this.tEND_NOME.ToString(), caracnull);
                    break;
                case "_tEND_NUMERO":
                    vcampo = VerificaNull(this.tEND_NUMERO.ToString(), caracnull);
                    break;
                case "_tEND_COMPL":
                    vcampo = VerificaNull(this.tEND_COMPL.ToString(), caracnull);
                    break;
                case "_tEND_BAIRRO":
                    vcampo = VerificaNull(this.tEND_BAIRRO.ToString(), caracnull);
                    break;
                case "_tEND_MUNICIPIO":
                    vcampo = VerificaNull(this.tEND_MUNICIPIO.ToString(), caracnull);
                    break;
                case "_tEND_UF":
                    vcampo = VerificaNull(this.tEND_UF.ToString(), caracnull);
                    break;
                case "_tEND_CEP":
                    vcampo = VerificaNull(this.tEND_CEP.ToString(), caracnull);
                    break;
                case "_nALTURA":
                    vcampo = VerificaNull(this.nALTURA.ToString(), caracnull);
                    break;
                case "_nPESO":
                    vcampo = VerificaNull(this.nPESO.ToString(), caracnull);
                    break;
                case "_nNO_PIS_PASEP":
                    vcampo = VerificaNull(this.nNO_PIS_PASEP.ToString(), caracnull);
                    break;
                default:
                    vcampo = "Item não encontrado!";
                    break;
            }
            return vcampo;
        }

        #endregion

        #region ExamesComplementaresIndicados

        #region List<ExameComplementar>

        public struct ExameComplementar
        {
            public string NomeExame;
            public int IdExameDicionario;
            public bool isNaAdmissao;
            public bool isNaDemissao;
            public bool isNaMudancaFuncao;
            public bool isNoPeriodico;
            public bool isNoRetornoTrabalho;
        }

        private List<ExameComplementar> _ComplementaresIndicados;

        [Persist(false)]
        public List<ExameComplementar> ComplementaresIndicados
        {
            get
            {
                if (this._ComplementaresIndicados == null)
                    this._ComplementaresIndicados = this.GetExamesComplementaresIndicados();

                return _ComplementaresIndicados;

            }
        }

        private List<ExameComplementar> GetExamesComplementaresIndicados()
        {
            List<ExameComplementar> listComplementar = new List<ExameComplementar>();
            ExameComplementar exameComplementar;

            EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(this);
            empregadoFuncao.nID_EMPR.Find();

            Pcmso pcmso = empregadoFuncao.nID_EMPR.GetUltimoPcmso();
            Ghe gheAtual = empregadoFuncao.GetGheEmpregado(true);

            StringBuilder sqlstm = new StringBuilder();
            sqlstm.Append("IdPcmso=" + pcmso.Id);
            sqlstm.Append(" AND IdGHE=" + gheAtual.Id);
            sqlstm.Append(" AND IdExameDicionario IN (SELECT IdExameDicionario FROM ExameDicionario WHERE IndExame<>" + (int)IndTipoExame.Clinico);
            sqlstm.Append(" AND IndExame<>" + (int)IndTipoExame.NaoOcupacional + ")");
            sqlstm.Append(" ORDER BY (SELECT Nome FROM ExameDicionario WHERE ExameDicionario.IdExameDicionario=PcmsoPlanejamento.IdExameDicionario)");

            List<PcmsoPlanejamento> listPlanejamento = new PcmsoPlanejamento().Find<PcmsoPlanejamento>(sqlstm.ToString());

            foreach (PcmsoPlanejamento pcmsoPlanejamento in listPlanejamento)
            {
                exameComplementar = new ExameComplementar();

                exameComplementar.NomeExame = pcmsoPlanejamento.IdExameDicionario.ToString();
                exameComplementar.IdExameDicionario = pcmsoPlanejamento.IdExameDicionario.Id;
                exameComplementar.isNaAdmissao = pcmsoPlanejamento.NaAdmissao;
                exameComplementar.isNaDemissao = pcmsoPlanejamento.NaDemissao;
                exameComplementar.isNaMudancaFuncao = pcmsoPlanejamento.NaMudancaFuncao;
                exameComplementar.isNoPeriodico = pcmsoPlanejamento.Periodico;
                exameComplementar.isNoRetornoTrabalho = pcmsoPlanejamento.NoRetornoTrabalho;

                listComplementar.Add(exameComplementar);
            }

            return listComplementar;
        }

        #endregion

        #region ExamesComplementaresNaAdmissao

        public List<ExameComplementar> ListExamesComplementaresIndicadosNaAdmissao()
        {
            List<ExameComplementar> listExameComplementar = new List<ExameComplementar>();

            foreach (ExameComplementar exameComplementar in ExamesComplementaresIndicadosNaAdmissao())
                listExameComplementar.Add(exameComplementar);

            return listExameComplementar;
        }

        private IEnumerable<ExameComplementar> ExamesComplementaresIndicadosNaAdmissao()
        {
            foreach (ExameComplementar complementar in ComplementaresIndicados)
                if (complementar.isNaAdmissao)
                    yield return complementar;
        }

        #endregion

        #region ExamesComplementaresNaDemissao

        public List<ExameComplementar> ListExamesComplementaresIndicadosNaDemissao()
        {
            List<ExameComplementar> listExameComplementar = new List<ExameComplementar>();

            foreach (ExameComplementar exameComplementar in ExamesComplementaresIndicadosNaDemissao())
                listExameComplementar.Add(exameComplementar);

            return listExameComplementar;
        }

        private IEnumerable<ExameComplementar> ExamesComplementaresIndicadosNaDemissao()
        {
            foreach (ExameComplementar complementar in ComplementaresIndicados)
                if (complementar.isNaDemissao)
                    yield return complementar;
        }

        #endregion

        #region ExamesComplementaresNaMudancaFuncao

        public List<ExameComplementar> ListExamesComplementaresIndicadosNaMudancaFuncao()
        {
            List<ExameComplementar> listExameComplementar = new List<ExameComplementar>();

            foreach (ExameComplementar exameComplementar in ExamesComplementaresIndicadosNaMudancaFuncao())
                listExameComplementar.Add(exameComplementar);

            return listExameComplementar;
        }

        private IEnumerable<ExameComplementar> ExamesComplementaresIndicadosNaMudancaFuncao()
        {
            foreach (ExameComplementar complementar in ComplementaresIndicados)
                if (complementar.isNaMudancaFuncao)
                    yield return complementar;
        }

        #endregion

        #region ExamesComplementaresNoPeriodico

        public List<ExameComplementar> ListExamesComplementaresIndicadosNoPeriodico()
        {
            List<ExameComplementar> listExameComplementar = new List<ExameComplementar>();

            foreach (ExameComplementar exameComplementar in ExamesComplementaresIndicadosNoPeriodico())
                listExameComplementar.Add(exameComplementar);

            return listExameComplementar;
        }

        private IEnumerable<ExameComplementar> ExamesComplementaresIndicadosNoPeriodico()
        {
            foreach (ExameComplementar complementar in ComplementaresIndicados)
                if (complementar.isNoPeriodico)
                    yield return complementar;
        }

        #endregion

        #region ExamesComplementaresNoRetornoTrabalho

        public List<ExameComplementar> ListExamesComplementaresIndicadosNoRetornoTrabalho()
        {
            List<ExameComplementar> listExameComplementar = new List<ExameComplementar>();

            foreach (ExameComplementar exameComplementar in ExamesComplementaresIndicadosNoRetornoTrabalho())
                listExameComplementar.Add(exameComplementar);

            return listExameComplementar;
        }

        private IEnumerable<ExameComplementar> ExamesComplementaresIndicadosNoRetornoTrabalho()
        {
            foreach (ExameComplementar complementar in ComplementaresIndicados)
                if (complementar.isNoRetornoTrabalho)
                    yield return complementar;
        }

        #endregion

        #endregion

        #endregion

        #region Metodos Estaticos

        #region Quantidades

        public static int GetQtdEmpregadosPCMSO()
        {
            int ret = 0;

            StringBuilder str = new StringBuilder();
            str.Append("nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO)");
            str.Append(" AND nID_EMPR IN (SELECT IdJuridica FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica WHERE IdJuridicaPapel = 1)");
            str.Append(" AND nID_EMPR IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Cliente WHERE ContrataPCMSO = 1)");
            str.Append(" AND hDT_DEM IS NULL");
            str.Append(" AND gTERCEIRO = 0");

            ret = new Empregado().ExecuteCount(str.ToString());

            return ret;
        }

        public static int GetQtdEmpregadosPorGrupo(int IdGrupoEmpresa)
        {
            int ret = 0;

            StringBuilder str = new StringBuilder();
            str.Append("nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO)");
            str.Append(" AND nID_EMPR IN (SELECT IdJuridica FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica WHERE IdJuridicaPapel = 1)");
            str.Append(" AND nID_EMPR IN (SELECT IdJuridica FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica WHERE IdGrupoEmpresa=" + IdGrupoEmpresa + ")");
            str.Append(" AND hDT_DEM IS NULL ");
            str.Append(" AND gTERCEIRO = 0");

            ret = new Empregado().ExecuteCount(str.ToString());

            return ret;
        }
        #endregion

        #endregion

        #region class CriteriaBuilder

        public class CriteriaBuilder
        {
            #region Constructor

            public CriteriaBuilder()
            {

            }

            public CriteriaBuilder(Cliente cliente)
            {
                this.cliente = cliente;
            }
            #endregion

            #region Enums

            public enum Nivel : int
            {
                Empresa,
                TodosGrupo,
                TodosPcmso,
                TodosClientes
            }

            public enum Empregados : int
            {
                Todos = -1,
                Ativos = 1,
                Inativos = 2,
                AtivosEm = 3
            }

            public enum Atastados : int
            {
                NaoAfastados = 0,
                Afastados = 1,
                Todos = 2,
            }

            public enum EmpregadosPelo : int
            {
                LocalTrabalho,
                OutroLocal,
                Registro,
                Alocados
            }

            public enum ClassificacaoFuncional : int
            {
                Com,
                SemEmAberto,
                Todos,
                SemNenhuma,
                ComMaisUmaEmAberto
            }

            public enum VinculoEmpregaticio : int
            {
                Todos = 0,
                Empregado = 1,
                Terceiro = 2
            }

            #endregion

            #region Properties

            private Cliente _cliente;

            public Cliente cliente
            {
                get { return _cliente; }
                set { _cliente = value; }
            }

            private LaudoTecnico _laudoTecnico;

            public LaudoTecnico laudoTecnico
            {
                get { return _laudoTecnico; }
                set { _laudoTecnico = value; }
            }

            private Nivel _IndNivel = Nivel.Empresa;

            public Nivel IndNivel
            {
                get { return _IndNivel; }
                set { _IndNivel = value; }
            }
            private bool _NaoPossueGhe;

            public bool NaoPossueGhe
            {
                get { return _NaoPossueGhe; }
                set { _NaoPossueGhe = value; }
            }

            private bool _AdmitidosApos90Dias;

            public bool AdmitidosApos90Dias
            {
                get { return _AdmitidosApos90Dias; }
                set { _AdmitidosApos90Dias = value; }
            }

            private int _IdGhe;

            public int IdGhe
            {
                get { return _IdGhe; }
                set { _IdGhe = value; }
            }

            private Empregados _IndEmpregados = Empregados.Todos;

            public Empregados IndEmpregados
            {
                get { return _IndEmpregados; }
                set { _IndEmpregados = value; }
            }

            private Atastados _IndAtastados = Atastados.Todos;

            public Atastados IndAtastados
            {
                get { return _IndAtastados; }
                set { _IndAtastados = value; }
            }

            private ClassificacaoFuncional _IndClassificacaoFuncional = ClassificacaoFuncional.Todos;

            public ClassificacaoFuncional IndClassificacaoFuncional
            {
                get { return _IndClassificacaoFuncional; }
                set { _IndClassificacaoFuncional = value; }
            }

            private EmpregadosPelo _IndEmpregadosPelo = EmpregadosPelo.Registro;

            public EmpregadosPelo IndEmpregadosPelo
            {
                get { return _IndEmpregadosPelo; }
                set { _IndEmpregadosPelo = value; }
            }

            private VinculoEmpregaticio _IndVinculoEmpregaticio = VinculoEmpregaticio.Todos;

            public VinculoEmpregaticio IndVinculoEmpregaticio
            {
                get { return _IndVinculoEmpregaticio; }
                set { _IndVinculoEmpregaticio = value; }
            }

            private DateTime _AtivosEm;

            public DateTime AtivosEm
            {
                get { return _AtivosEm; }
                set { _AtivosEm = value; }
            }

            private bool _HasOrderBy = false;

            public bool HasOrderBy
            {
                get { return _HasOrderBy; }
                set { _HasOrderBy = value; }
            }

            #endregion

            #region CriteriaString

            public string CriteriaString
            {
                get
                {
                    StringBuilder criteria = new StringBuilder();

                    criteria.Append("nID_EMPREGADO=nID_EMPREGADO");

                    AddIndEmpregados(criteria);

                    AddAdmitidosApos90Dias(criteria);

                    AddIndVindulo(criteria);

                    if (IndNivel == Nivel.Empresa)
                    {
                        AddEmpregadosPelo(criteria);
                    }
                    else
                    {
                        if (IndNivel == Nivel.TodosGrupo)
                            AddTodosGrupo(criteria);
                        else if (IndNivel == Nivel.TodosPcmso)
                            AddTodosPcmso(criteria);
                        else if (IndNivel == Nivel.TodosClientes)
                            AddTodosClientes(criteria);

                        AddClassificacaoFuncional(criteria);
                    }

                    AddIndAfastados(criteria);

                    if (HasOrderBy)
                        criteria.Append(" ORDER BY tNO_EMPG");

                    return criteria.ToString();
                }
            }

            #endregion

            #region Metodos

            #region AddIndEmpregados

            private void AddIndEmpregados(StringBuilder criteria)
            {
                if (IndEmpregados == Empregados.Ativos)
                    criteria.Append(" AND hDT_DEM IS NULL ");
                else if (IndEmpregados == Empregados.Inativos)
                    criteria.Append(" AND hDT_DEM IS NOT NULL ");
                else if (IndEmpregados == Empregados.AtivosEm)
                {
                    criteria.Append(" AND hDT_ADM <='" + AtivosEm.ToString("yyyy-MM-dd") + "'")
                            .Append(" AND (hDT_DEM IS NULL OR hDT_DEM >='" + AtivosEm.ToString("yyyy-MM-dd") + "')")
                            .Append(" AND nID_EMPREGADO IN ")
                            .Append("(SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO ")
                            .Append(" WHERE (hDT_INICIO  <='" + AtivosEm.ToString("yyyy-MM-dd") + "')")
                            .Append(" AND (hDT_TERMINO IS NULL OR hDT_TERMINO >='" + AtivosEm.ToString("yyyy-MM-dd") + "')")
                            .Append(")");
                }
            }
            #endregion

            #region AddAdmitidosApos90Dias

            private void AddAdmitidosApos90Dias(StringBuilder criteria)
            {
                if (AdmitidosApos90Dias)
                    criteria.Append(" AND hDT_ADM <='" + DateTime.Today.AddDays(-90).ToString("yyyy-MM-dd") + "'");
            }
            #endregion

            #region AddIndVindulo

            private void AddIndVindulo(StringBuilder criteria)
            {
                if (IndVinculoEmpregaticio == VinculoEmpregaticio.Empregado)
                    criteria.Append(" AND gTERCEIRO=0");
                else if (IndVinculoEmpregaticio == VinculoEmpregaticio.Terceiro)
                    criteria.Append(" AND gTERCEIRO=1");
            }
            #endregion

            #region AddTodosGrupo

            private void AddTodosGrupo(StringBuilder criteria)
            {
                if (cliente == null || cliente.IdGrupoEmpresa == null)
                    return;

                criteria.Append(" AND nID_EMPR IN (SELECT IdJuridica FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica WHERE ");
                criteria.Append(" IdGrupoEmpresa=" + cliente.IdGrupoEmpresa.Id + ")");
                criteria.Append(" AND nID_EMPR IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryClienteAtivos)");
            }
            #endregion

            #region AddTodosPcmso

            private static void AddTodosPcmso(StringBuilder criteria)
            {
                criteria.Append(" AND nID_EMPR IN (SELECT IdJuridica FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica WHERE ")
                    .Append(" IdJuridica IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryClienteAtivos)")
                    .Append(" AND IdJuridica IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Cliente WHERE ContrataPCMSO = 1))");
            }
            #endregion

            #region AddTodosClientes

            private static void AddTodosClientes(StringBuilder criteria)
            {
                criteria.Append(" AND nID_EMPR IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryClienteAtivos)");
            }
            #endregion

            #region AddClassificacaoFuncional

            private void AddClassificacaoFuncional(StringBuilder criteria)
            {
                if (IndClassificacaoFuncional == ClassificacaoFuncional.Com)
                    criteria.Append(" AND nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO)");
                else if (IndClassificacaoFuncional == ClassificacaoFuncional.ComMaisUmaEmAberto)
                    criteria.Append(" AND nID_EMPREGADO IN (SELECT  nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE hDT_TERMINO IS NULL GROUP BY nID_EMPREGADO HAVING COUNT(nID_EMPREGADO) > 1)");
                else if (IndClassificacaoFuncional == ClassificacaoFuncional.SemEmAberto)
                {
                    criteria.Append(" AND nID_EMPREGADO NOT IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE hDT_TERMINO IS NULL)")
                        .Append(" AND nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE hDT_TERMINO IS NOT NULL)")
                        .Append(" AND nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO)");
                }
                else if (IndClassificacaoFuncional == ClassificacaoFuncional.SemNenhuma)
                    criteria.Append(" AND nID_EMPREGADO NOT IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO)");
            }
            #endregion

            #region AddEmpregadosPelo

            private void AddEmpregadosPelo(StringBuilder criteria)
            {
                if (IndClassificacaoFuncional == ClassificacaoFuncional.SemNenhuma
                    && IndEmpregadosPelo != EmpregadosPelo.Registro)
                    throw new Exception("Filtro sem nenhuma classificação funcional somente para empregados por registro!");

                if (IndEmpregadosPelo == EmpregadosPelo.Registro)
                {
                    AddEmpregadosPeloRegistro(criteria);
                    AddClassificacaoFuncional(criteria);
                }
                else if (IndEmpregadosPelo == EmpregadosPelo.LocalTrabalho)
                    AddEmpregadosPeloLocalTrabalho(criteria);
                else if (IndEmpregadosPelo == EmpregadosPelo.Alocados)
                    AddEmpregadosPeloAlocados(criteria);
                else if (IndEmpregadosPelo == EmpregadosPelo.OutroLocal)
                    AddEmpreagadosPeloOutroLocal(criteria);
            }
            #endregion

            #region AddEmpregadosPeloRegistro

            private void AddEmpregadosPeloRegistro(StringBuilder criteria)
            {
                if (cliente == null)
                    return;

                criteria.Append(" AND nID_EMPR=" + cliente.Id);
            }

            #endregion

            #region AddEmpregadosPeloLocalTrabalho

            private void AddEmpregadosPeloLocalTrabalho(StringBuilder criteria)
            {
                criteria.Append(" AND nID_EMPREGADO IN ")
                        .Append("(SELECT nID_EMPREGADO")
                        .Append(" FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO")
                        .Append(" WHERE nID_EMPR=" + cliente.Id);

                if (IndEmpregados == Empregados.Ativos)
                    criteria.Append(" AND hDT_TERMINO IS NULL ");

                if (IdGhe != 0)
                    criteria.Append(" AND nID_EMPREGADO_FUNCAO IN")
                            .Append(" (SELECT nID_EMPREGADO_FUNCAO")
                            .Append(" FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO")
                            .Append(" WHERE nID_FUNC=" + IdGhe + ")");

                AddNaoPossueGhe(criteria);

                if (!cliente.IsInativo && (IndEmpregados == Empregados.Ativos))
                    criteria.Append(" AND nID_EMPR IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryClienteAtivos)");

                criteria.Append(")");
            }

            private void AddNaoPossueGhe(StringBuilder criteria)
            {
                if (NaoPossueGhe)
                {
                    if (laudoTecnico == null)
                        laudoTecnico = LaudoTecnico.GetUltimoLaudo(cliente.Id);

                    criteria.Append(" AND nID_EMPREGADO_FUNCAO NOT IN")
                            .Append(" (SELECT nID_EMPREGADO_FUNCAO")
                            .Append(" FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO");

                    if (IndEmpregados == Empregados.Ativos)
                        criteria.Append(" WHERE nID_LAUD_TEC=" + laudoTecnico.Id);

                    criteria.Append(")");
                }
            }
            #endregion

            #region AddEmpregadosPeloAlocados

            private void AddEmpregadosPeloAlocados(StringBuilder criteria)
            {
                criteria.Append(" AND nID_EMPR<>" + cliente.Id)
                        .Append(" AND nID_EMPREGADO IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPR=" + cliente.Id);

                if (IndEmpregados == Empregados.Ativos)
                    criteria.Append(" AND hDT_TERMINO IS NULL ");
                else if (IndEmpregados == Empregados.Inativos)
                    criteria.Append(" AND hDT_TERMINO IS NOT NULL ");

                criteria.Append(")");
            }
            #endregion

            #region AddEmpreagadosPeloOutroLocal

            private void AddEmpreagadosPeloOutroLocal(StringBuilder criteria)
            {
                criteria.Append(" AND nID_EMPR=" + cliente.Id);
                criteria.Append(" AND nID_EMPREGADO NOT IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO_FUNCAO WHERE nID_EMPR=" + cliente.Id + ")");
            }
            #endregion

            #region AddIndAfastados

            private void AddIndAfastados(StringBuilder criteria)
            {
                if (IndAtastados == Atastados.Afastados)
                {
                    criteria.Append(" AND nID_EMPREGADO IN ")
                        .Append(" (SELECT IdEmpregado FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Afastamento")
                        .Append(" WHERE DataVolta IS NULL)");
                }
                else if (IndAtastados == Atastados.NaoAfastados)
                {
                    criteria.Append(" AND nID_EMPREGADO NOT IN ")
                        .Append(" (SELECT IdEmpregado FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Afastamento")
                        .Append(" WHERE DataVolta IS NULL)");
                }
            }
            #endregion

            #endregion
        }
        #endregion
    }


    [Database("sied_novo", "tblEMPREGADO_APTIDAO", "nID_EMPREGADO_APTIDAO")]
    public class Empregado_Aptidao : Ilitera.Data.Table
    {

        public enum Aptidao : int
        {
            EspacoConfinado = 1,
            TrabalhoAltura,
            Transportes,
            Submerso,
            Eletricidade,
            Aquaviario,
            Alimento,
            Brigadista,
            Socorrista,            
            PPR,
            RadiacaoIonizante,
            Trabalho_Bordo
        }

        private int _nID_EMPREGADO_APTIDAO;
        private int _nID_EMPREGADO;
        private bool _apt_Espaco_Confinado;
        private bool _apt_Trabalho_Altura;
        private bool _apt_Transporte;
        private bool _apt_Submerso;
        private bool _apt_Eletricidade;
        private bool _apt_Aquaviario;
        private bool _apt_Alimento;
        private bool _apt_Brigadista;
        private bool _apt_Socorrista;
        private bool _apt_Respirador;
        private bool _apt_PPR;
        private bool _apt_Radiacao;
        private bool _apt_Trabalho_Bordo;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Empregado_Aptidao()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Empregado_Aptidao(int id)
        {
            this.Find(id);
        }

        public override int Id
        {
            get { return _nID_EMPREGADO_APTIDAO; }
            set { _nID_EMPREGADO_APTIDAO = value; }
        }

        public int nId_Empregado
        {
            get { return _nID_EMPREGADO; }
            set { _nID_EMPREGADO = value; }
        }

        public bool apt_Espaco_Confinado
        {
            get { return _apt_Espaco_Confinado; }
            set { _apt_Espaco_Confinado = value; }
        }
        public bool apt_Trabalho_Altura
        {
            get { return _apt_Trabalho_Altura; }
            set { _apt_Trabalho_Altura = value; }
        }
        public bool apt_Transporte
        {
            get { return _apt_Transporte; }
            set { _apt_Transporte = value; }
        }
        public bool apt_Submerso
        {
            get { return _apt_Submerso; }
            set { _apt_Submerso = value; }
        }
        public bool apt_Eletricidade
        {
            get { return _apt_Eletricidade; }
            set { _apt_Eletricidade = value; }
        }
        public bool apt_Aquaviario
        {
            get { return _apt_Aquaviario; }
            set { _apt_Aquaviario = value; }
        }
        public bool apt_Alimento
        {
            get { return _apt_Alimento; }
            set { _apt_Alimento = value; }
        }
        public bool apt_Brigadista
        {
            get { return _apt_Brigadista; }
            set { _apt_Brigadista = value; }
        }
        public bool apt_Socorrista
        {
            get { return _apt_Socorrista; }
            set { _apt_Socorrista = value; }
        }
        public bool apt_Respirador
        {
            get { return _apt_Respirador; }
            set { _apt_Respirador = value; }
        }
        public bool apt_PPR
        {
            get { return _apt_PPR; }
            set { _apt_PPR = value; }
        }
        public bool apt_Radiacao
        {
            get { return _apt_Radiacao; }
            set { _apt_Radiacao = value; }
        }
        public bool apt_Trabalho_Bordo
        {
            get { return _apt_Trabalho_Bordo; }
            set { _apt_Trabalho_Bordo = value; }
        }

    }



    [Database("sied_novo", "tblEMPREGADO_APTIDAO_PLANEJAMENTO", "nId_Planejamento")]
    public class Empregado_Aptidao_Planejamento : Ilitera.Data.Table, IPeriodicidadeExame
    {


        private int _IdPcmsoPlanejamento;
        private int _nId_Empr;
        private int _nId_Aptidao;
        private ExameDicionario _IdExameDicionario;
        private bool _NaAdmissao;
        private bool _NaDemissao;
        private bool _NoRetornoTrabalho;
        private bool _NaMudancaFuncao;
        private bool _AposAdmissao;
        private int _IndPeriodicidadeAposAdmissao;
        private int _IntervaloAposAdmissao;
        private bool _Periodico;
        private int _IndPeriodicidade;
        private int _Intervalo;
        private bool _DependeIdade;
        private string _Observacao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Empregado_Aptidao_Planejamento()
        {

        }
        public override int Id
        {
            get { return _IdPcmsoPlanejamento; }
            set { _IdPcmsoPlanejamento = value; }
        }
        public int nId_Empr
        {
            get { return _nId_Empr; }
            set { _nId_Empr = value; }
        }
        public int nId_Aptidao
        {
            get { return _nId_Aptidao; }
            set { _nId_Aptidao = value; }
        }
        public ExameDicionario IdExameDicionario
        {
            get { return _IdExameDicionario; }
            set { _IdExameDicionario = value; }
        }
        public bool NaAdmissao
        {
            get { return _NaAdmissao; }
            set { _NaAdmissao = value; }
        }
        public bool NaDemissao
        {
            get { return _NaDemissao; }
            set { _NaDemissao = value; }
        }
        public bool NoRetornoTrabalho
        {
            get { return _NoRetornoTrabalho; }
            set { _NoRetornoTrabalho = value; }
        }
        public bool NaMudancaFuncao
        {
            get { return _NaMudancaFuncao; }
            set { _NaMudancaFuncao = value; }
        }
        public bool AposAdmissao
        {
            get { return _AposAdmissao; }
            set { _AposAdmissao = value; }
        }
        public int IndPeriodicidadeAposAdmissao
        {
            get { return _IndPeriodicidadeAposAdmissao; }
            set { _IndPeriodicidadeAposAdmissao = value; }
        }
        public int IntervaloAposAdmissao
        {
            get { return _IntervaloAposAdmissao; }
            set { _IntervaloAposAdmissao = value; }
        }
        public bool Periodico
        {
            get { return _Periodico; }
            set { _Periodico = value; }
        }
        public int IndPeriodicidade
        {
            get { return _IndPeriodicidade; }
            set { _IndPeriodicidade = value; }
        }
        public int Intervalo
        {
            get { return _Intervalo; }
            set { _Intervalo = value; }
        }
        public bool DependeIdade
        {
            get { return _DependeIdade; }
            set { _DependeIdade = value; }
        }
        public string Observacao
        {
            get { return _Observacao; }
            set { _Observacao = value; }
        }

        public override string ToString()
        {
            if (this.IdExameDicionario == null)
                this.Inicialize();

            if (this.IdExameDicionario.mirrorOld == null)
                this.IdExameDicionario.Find();

            return this.IdExameDicionario.Nome + "  - " + ExameDicionario.GetPeriodicidadeExame(this);
        }
    }



    [Database("sied_novo", "tblEMPREGADO_APTIDAO_PLANEJAMENTO_IDADE", "nIdPlanejamentoIdade")]
    public class Empregado_Aptidao_Planejamento_Idade : Ilitera.Data.Table
    {
        private int _nIdPlanejamentoIdade;
        private Empregado_Aptidao_Planejamento _nIdPlanejamento;
        private int _AnoInicio;
        private int _AnoTermino;
        private int _IndPeriodicidade;
        private int _Intervalo;
        private int _IndSexoIdade = 1;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Empregado_Aptidao_Planejamento_Idade()
        {

        }

        public override int Id
        {
            get { return _nIdPlanejamentoIdade; }
            set { _nIdPlanejamentoIdade = value; }
        }
        public Empregado_Aptidao_Planejamento nIdPlanejamento
        {
            get { return _nIdPlanejamento; }
            set { _nIdPlanejamento = value; }
        }
        public int AnoInicio
        {
            get { return _AnoInicio; }
            set { _AnoInicio = value; }
        }
        public int IndPeriodicidade
        {
            get { return _IndPeriodicidade; }
            set { _IndPeriodicidade = value; }
        }
        public int Intervalo
        {
            get { return _Intervalo; }
            set { _Intervalo = value; }
        }
        public int AnoTermino
        {
            get { return _AnoTermino; }
            set { _AnoTermino = value; }
        }
        public int IndSexoIdade
        {
            get { return _IndSexoIdade; }
            set { _IndSexoIdade = value; }
        }
    }





    [Database("sied_novo", "tblImportacao", "nID_IMPORTACAO")]
    public class Importacao_Planilha : Ilitera.Data.Table
    {

        private int _nID_IMPORTACAO;
        private int _nID_EMPR;
        private int _IdUsuario;
        private DateTime _DataImportacao;
        private string _Status;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Importacao_Planilha()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Importacao_Planilha(int id)
        {
            this.Find(id);
        }

        public override int Id
        {
            get { return _nID_IMPORTACAO; }
            set { _nID_IMPORTACAO = value; }
        }

        public int nId_Empr
        {
            get { return _nID_EMPR; }
            set { _nID_EMPR = value; }
        }

        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
              
        public DateTime DataImportacao
        {
            get { return _DataImportacao; }
            set { _DataImportacao = value; }
        }

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
    }




    [Database("sied_novo", "tblImportacao_Detalhes", "nID_IMPORTACAO_DETALHES")]
    public class Importacao_Planilha_Detalhes : Ilitera.Data.Table
    {

        private int _nID_IMPORTACAO_DETALHES;
        private int _nID_IMPORTACAO;
        private int _nID_EMPREGADO;
        private string _Status;

        private Int32 _nID_EMPR;
        private string _tNO_EMPG = string.Empty;        
        private string _tCOD_EMPR = string.Empty;
        private string _tNUM_CTPS = string.Empty;
        private string _tSER_CTPS = string.Empty;
        private string _tUF_CPTS = string.Empty;
        private DateTime _hDT_ADM;
        private string _tNO_CPF = String.Empty;
        private long _nNO_PIS_PASEP;
        private string _tNO_IDENTIDADE = String.Empty;
        private string _tSEXO = String.Empty;
        private DateTime _hDT_NASC;
        private string _teMail = string.Empty;
        private string _teMail_Resp = string.Empty;
        private string _CARGO = string.Empty;
        private string _SETOR = string.Empty;
        private string _FUNCAO = string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Importacao_Planilha_Detalhes()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Importacao_Planilha_Detalhes(int id)
        {
            this.Find(id);
        }

        public override int Id
        {
            get { return _nID_IMPORTACAO_DETALHES; }
            set { _nID_IMPORTACAO_DETALHES = value; }
        }

        public int nId_Importacao
        {
            get { return _nID_IMPORTACAO; }
            set { _nID_IMPORTACAO = value; }
        }

        public int nId_Empregado
        {
            get { return _nID_EMPREGADO; }
            set { _nID_EMPREGADO = value; }
        }

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        
        public string tNO_EMPG
        {
            get { return _tNO_EMPG; }
            set { _tNO_EMPG = value; }
        }
        public Int32 nID_EMPR
        {
            get { return _nID_EMPR; }
            set { _nID_EMPR = value; }
        }
        public string tCOD_EMPR
        {
            get { return _tCOD_EMPR; }
            set { _tCOD_EMPR = value; }
        }

        public string tNUM_CTPS
        {
            get { return _tNUM_CTPS; }
            set { _tNUM_CTPS = value; }
        }

        public string tSER_CTPS
        {
            get { return _tSER_CTPS; }
            set { _tSER_CTPS = value; }
        }
        public string tUF_CPTS
        {
            get { return _tUF_CPTS; }
            set { _tUF_CPTS = value; }
        }
        public DateTime hDT_ADM
        {
            get { return _hDT_ADM; }
            set { _hDT_ADM = value; }
        }
    
        public string tNO_CPF
        {
            get { return _tNO_CPF; }
            set { _tNO_CPF = value; }
        }
        public long nNO_PIS_PASEP
        {
            get { return _nNO_PIS_PASEP; }
            set { _nNO_PIS_PASEP = value;    }
        }

        public string tNO_IDENTIDADE
        {
            get { return _tNO_IDENTIDADE; }
            set { _tNO_IDENTIDADE = value; }
        }

        public string tSEXO
        {
            get  {   return _tSEXO;    }
            set  {  _tSEXO = value;   }
        }

        public DateTime hDT_NASC
        {
            get { return _hDT_NASC; }
            set { _hDT_NASC = value; }
        }
     
        public string teMail
        {
            get { return _teMail; }
            set { _teMail = value; }
        }
        public string teMail_Resp
        {
            get { return _teMail_Resp; }
            set { _teMail_Resp = value; }
        }
        public string CARGO
        {
            get { return _CARGO; }
            set { _CARGO = value; }
        }
        public string SETOR
        {
            get { return _SETOR; }
            set { _SETOR = value; }
        }
        public string FUNCAO
        {
            get { return _FUNCAO; }
            set { _FUNCAO = value; }
        }




    }



    [Database("sied_novo", "tblImportacaoTransf", "nID_IMPORTACAOTransf")]
    public class Importacao_Planilha_Transf : Ilitera.Data.Table
    {

        private int _nID_IMPORTACAOTransf;        
        private int _IdUsuario;
        private DateTime _DataImportacao;
        private string _Status;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Importacao_Planilha_Transf()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Importacao_Planilha_Transf(int id)
        {
            this.Find(id);
        }

        public override int Id
        {
            get { return _nID_IMPORTACAOTransf; }
            set { _nID_IMPORTACAOTransf = value; }
        }


        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }

        public DateTime DataImportacao
        {
            get { return _DataImportacao; }
            set { _DataImportacao = value; }
        }

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
    }




    [Database("sied_novo", "tblImportacaoTransf_Detalhes", "nID_IMPORTACAOTransf_DETALHES")]
    public class Importacao_Planilha_Transf_Detalhes : Ilitera.Data.Table
    {

        private int _nID_IMPORTACAOTransf_DETALHES;
        private int _nID_IMPORTACAOTransf;
        private string _Status;

        private Int32 _nID_EMPR;
        private string _CPF = string.Empty;
        private string _Filial_Origem = string.Empty;
        private string _CNPJ_Origem = string.Empty;
        private string _Filial_Destino = string.Empty;
        private string _CNPJ_Destino = string.Empty;
        private DateTime _Data_Inicial;
        private string _Setor = String.Empty;        
        private string _Funcao = String.Empty;
        private string _Inativar_Origem = String.Empty;
        private DateTime _Data_Demissao;
        private string _GHE = string.Empty;
        private Int32 _IdEmpr_Origem;
        private Int32 _IdEmpr_Destino;
        private Int32 _nIdEmpregado_Origem;
        private Int32 _nIdEmpregado_Destino;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Importacao_Planilha_Transf_Detalhes()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Importacao_Planilha_Transf_Detalhes(int id)
        {
            this.Find(id);
        }

        public override int Id
        {
            get { return _nID_IMPORTACAOTransf_DETALHES; }
            set { _nID_IMPORTACAOTransf_DETALHES = value; }
        }

        public int nId_ImportacaoTransf
        {
            get { return _nID_IMPORTACAOTransf; }
            set { _nID_IMPORTACAOTransf = value; }
        }

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }


        public string CPF
        {
            get { return _CPF; }
            set { _CPF = value; }
        }

        public string Filial_Origem
        {
            get { return _Filial_Origem; }
            set { _Filial_Origem = value; }
        }

        public string CNPJ_Origem
        {
            get { return _CNPJ_Origem; }
            set { _CNPJ_Origem = value; }
        }

        public string Filial_Destino
        {
            get { return _Filial_Destino; }
            set { _Filial_Destino = value; }
        }
        public string CNPJ_Destino
        {
            get { return _CNPJ_Destino; }
            set { _CNPJ_Destino = value; }
        }
        public DateTime Data_Inicial
        {
            get { return _Data_Inicial; }
            set { _Data_Inicial = value; }
        }

        public string Setor
        {
            get { return _Setor; }
            set { _Setor = value; }
        }

        public string Funcao
        {
            get { return _Funcao; }
            set { _Funcao = value; }
        }

        public string Inativar_Origem
        {
            get { return _Inativar_Origem; }
            set { _Inativar_Origem = value; }
        }

        public DateTime Data_Demissao
        {
            get { return _Data_Demissao; }
            set { _Data_Demissao = value; }
        }

        public string GHE
        {
            get { return _GHE; }
            set { _GHE = value; }
        }

        public Int32 IdEmpr_Origem
        {
            get { return _IdEmpr_Origem; }
            set { _IdEmpr_Origem = value; }
        }

        public Int32 IdEmpr_Destino
        {
            get { return _IdEmpr_Destino; }
            set { _IdEmpr_Destino = value; }
        }

        public Int32 nIdEmpregado_Origem
        {
            get { return _nIdEmpregado_Origem; }
            set { _nIdEmpregado_Origem = value; }
        }

        public Int32 nIdEmpregado_Destino
        {
            get { return _nIdEmpregado_Destino; }
            set { _nIdEmpregado_Destino = value; }
        }
    }



}
