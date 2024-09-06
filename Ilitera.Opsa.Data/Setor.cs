using System;
using System.Text;
using System.Data;
using System.Collections;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    [Database("sied_novo", "tblSETOR", "nID_SETOR", "", "Setor")]
    public class Setor : Ilitera.Data.Table
    {
        private int _nID_SETOR;
        private Cliente _nID_EMPR;
        private string _tCOD_SETOR = string.Empty;
        private string _tNO_STR_EMPR = string.Empty;
        private bool _gDS_NAO_AUTOM;
        private string _mDS_STR_EMPR = string.Empty;
        private string _tOUTROS_00 = string.Empty;
        private string _tOUTROS_01 = string.Empty;
        private string _tOUTROS_02 = string.Empty;
        private string _tOUTROS_03 = string.Empty;
        private string _tOUTROS_04 = string.Empty;
        private string _tOUTROS_05 = string.Empty;
        private string _tOUTROS_06 = string.Empty;
        private string _tOUTROS_07 = string.Empty;
        private string _tOUTROS_08 = string.Empty;
        private bool _bCHK_01_0;
        private bool _bCHK_01_1;
        private bool _bCHK_01_2;
        private bool _bCHK_01_3;
        private bool _bCHK_01_4;
        private bool _bCHK_04_0;
        private bool _bCHK_04_1;
        private bool _bCHK_04_2;
        private bool _bCHK_04_3;
        private bool _bCHK_04_4;
        private bool _bCHK_04_5;
        private bool _bCHK_04_6;
        private bool _bCHK_05_0;
        private bool _bCHK_05_1;
        private bool _bCHK_05_2;
        private bool _bCHK_05_3;
        private bool _bCHK_05_4;
        private bool _bCHK_05_5;
        private bool _bCHK_05_6;
        private bool _bCHK_05_7;
        private bool _bCHK_6N_0;
        private bool _bCHK_6N_1;
        private bool _bCHK_6N_2;
        private bool _bCHK_6F_0;
        private bool _bCHK_6F_1;
        private bool _bCHK_6F_2;
        private bool _bCHK_6F_3;
        private bool _bCHK_6F_4;
        private bool _bCHK_7N_0;
        private bool _bCHK_7N_1;
        private bool _bCHK_7N_2;
        private bool _bCHK_7N_3;
        private bool _bCHK_7A_0;
        private bool _bCHK_7A_1;
        private bool _bCHK_7A_2;
        private bool _bCHK_7A_3;
        private bool _bCHK_7A_4;
        private string _tDS_OUTROS_ASP = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Setor()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Setor(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _nID_SETOR; }
            set { _nID_SETOR = value; }
        }
        public Cliente nID_EMPR
        {
            get { return _nID_EMPR; }
            set { _nID_EMPR = value; }
        }
        public string tCOD_SETOR
        {
            get { return _tCOD_SETOR; }
            set { _tCOD_SETOR = value; }
        }
        [Obrigatorio(true, "O Nome do Setor deve ser preenchido!")]
        public string tNO_STR_EMPR
        {
            get { return _tNO_STR_EMPR; }
            set { _tNO_STR_EMPR = value; }
        }
        public bool gDS_NAO_AUTOM
        {
            get { return _gDS_NAO_AUTOM; }
            set { _gDS_NAO_AUTOM = value; }
        }
        public string mDS_STR_EMPR
        {
            get
            {
                if (this.gDS_NAO_AUTOM)
                    return _mDS_STR_EMPR;
                else
                    return this.DescricaoSetor();
            }
            set
            {
                if (this.gDS_NAO_AUTOM)
                    _mDS_STR_EMPR = value;
                else
                    _mDS_STR_EMPR = string.Empty;
            }
        }
        // Tela Administração
        public string tOUTROS_00
        {
            get { return _tOUTROS_00; }
            set { _tOUTROS_00 = value; }
        }
        public string tOUTROS_01
        {
            get { return _tOUTROS_01; }
            set { _tOUTROS_01 = value; }
        }
        public string tOUTROS_02
        {
            get { return _tOUTROS_02; }
            set { _tOUTROS_02 = value; }
        }
        public string tOUTROS_03
        {
            get { return _tOUTROS_03; }
            set { _tOUTROS_03 = value; }
        }
        public string tOUTROS_04
        {
            get { return _tOUTROS_04; }
            set { _tOUTROS_04 = value; }
        }
        public string tOUTROS_05
        {
            get { return _tOUTROS_05; }
            set { _tOUTROS_05 = value; }
        }
        public string tOUTROS_06
        {
            get { return _tOUTROS_06; }
            set { _tOUTROS_06 = value; }
        }
        public string tOUTROS_07
        {
            get { return _tOUTROS_07; }
            set { _tOUTROS_07 = value; }
        }
        public string tOUTROS_08
        {
            get { return _tOUTROS_08; }
            set { _tOUTROS_08 = value; }
        }
        //Grupo1
        public bool bCHK_01_0
        {
            get { return _bCHK_01_0; }
            set { _bCHK_01_0 = value; }
        }
        public bool bCHK_01_1
        {
            get { return _bCHK_01_1; }
            set { _bCHK_01_1 = value; }
        }
        public bool bCHK_01_2
        {
            get { return _bCHK_01_2; }
            set { _bCHK_01_2 = value; }
        }
        public bool bCHK_01_3
        {
            get { return _bCHK_01_3; }
            set { _bCHK_01_3 = value; }
        }
        public bool bCHK_01_4
        {
            get { return _bCHK_01_4; }
            set { _bCHK_01_4 = value; }
        }
        //Grupo4
        public bool bCHK_04_0
        {
            get { return _bCHK_04_0; }
            set { _bCHK_04_0 = value; }
        }
        public bool bCHK_04_1
        {
            get { return _bCHK_04_1; }
            set { _bCHK_04_1 = value; }
        }
        public bool bCHK_04_2
        {
            get { return _bCHK_04_2; }
            set { _bCHK_04_2 = value; }
        }
        public bool bCHK_04_3
        {
            get { return _bCHK_04_3; }
            set { _bCHK_04_3 = value; }
        }
        public bool bCHK_04_4
        {
            get { return _bCHK_04_4; }
            set { _bCHK_04_4 = value; }
        }
        public bool bCHK_04_5
        {
            get { return _bCHK_04_5; }
            set { _bCHK_04_5 = value; }
        }
        public bool bCHK_04_6
        {
            get { return _bCHK_04_6; }
            set { _bCHK_04_6 = value; }
        }

        //Grupo5
        public bool bCHK_05_0
        {
            get { return _bCHK_05_0; }
            set { _bCHK_05_0 = value; }
        }
        public bool bCHK_05_1
        {
            get { return _bCHK_05_1; }
            set { _bCHK_05_1 = value; }
        }
        public bool bCHK_05_2
        {
            get { return _bCHK_05_2; }
            set { _bCHK_05_2 = value; }
        }
        public bool bCHK_05_3
        {
            get { return _bCHK_05_3; }
            set { _bCHK_05_3 = value; }
        }
        public bool bCHK_05_4
        {
            get { return _bCHK_05_4; }
            set { _bCHK_05_4 = value; }
        }
        public bool bCHK_05_5
        {
            get { return _bCHK_05_5; }
            set { _bCHK_05_5 = value; }
        }
        public bool bCHK_05_6
        {
            get { return _bCHK_05_6; }
            set { _bCHK_05_6 = value; }
        }
        public bool bCHK_05_7
        {
            get { return _bCHK_05_7; }
            set { _bCHK_05_7 = value; }
        }
        //Grupo6_N
        public bool bCHK_6N_0
        {
            get { return _bCHK_6N_0; }
            set { _bCHK_6N_0 = value; }
        }
        public bool bCHK_6N_1
        {
            get { return _bCHK_6N_1; }
            set { _bCHK_6N_1 = value; }
        }
        public bool bCHK_6N_2
        {
            get { return _bCHK_6N_2; }
            set { _bCHK_6N_2 = value; }
        }
        //Grupo6_F
        public bool bCHK_6F_0
        {
            get { return _bCHK_6F_0; }
            set { _bCHK_6F_0 = value; }
        }
        public bool bCHK_6F_1
        {
            get { return _bCHK_6F_1; }
            set { _bCHK_6F_1 = value; }
        }
        public bool bCHK_6F_2
        {
            get { return _bCHK_6F_2; }
            set { _bCHK_6F_2 = value; }
        }
        public bool bCHK_6F_3
        {
            get { return _bCHK_6F_3; }
            set { _bCHK_6F_3 = value; }
        }
        public bool bCHK_6F_4
        {
            get { return _bCHK_6F_4; }
            set { _bCHK_6F_4 = value; }
        }
        //Grupo7_N
        public bool bCHK_7N_0
        {
            get { return _bCHK_7N_0; }
            set { _bCHK_7N_0 = value; }
        }
        public bool bCHK_7N_1
        {
            get { return _bCHK_7N_1; }
            set { _bCHK_7N_1 = value; }
        }
        public bool bCHK_7N_2
        {
            get { return _bCHK_7N_2; }
            set { _bCHK_7N_2 = value; }
        }
        public bool bCHK_7N_3
        {
            get { return _bCHK_7N_3; }
            set { _bCHK_7N_3 = value; }
        }
        //Grupo7_A
        public bool bCHK_7A_0
        {
            get { return _bCHK_7A_0; }
            set { _bCHK_7A_0 = value; }
        }
        public bool bCHK_7A_1
        {
            get { return _bCHK_7A_1; }
            set { _bCHK_7A_1 = value; }
        }
        public bool bCHK_7A_2
        {
            get { return _bCHK_7A_2; }
            set { _bCHK_7A_2 = value; }
        }
        public bool bCHK_7A_3
        {
            get { return _bCHK_7A_3; }
            set { _bCHK_7A_3 = value; }
        }
        public bool bCHK_7A_4
        {
            get { return _bCHK_7A_4; }
            set { _bCHK_7A_4 = value; }
        }
        //Outros
        public string tDS_OUTROS_ASP
        {
            get { return _tDS_OUTROS_ASP; }
            set { _tDS_OUTROS_ASP = value; }
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return this.tNO_STR_EMPR;
        }

        public string GetCodigoNomeSetor()
        {
            if (this.tCOD_SETOR != string.Empty)
                return this.tCOD_SETOR + " - " + this.tNO_STR_EMPR;
            else
                return this.tNO_STR_EMPR;
        }

        #region DescricaoSetor

        public string DescricaoSetor()
        {
            StringBuilder ret = new StringBuilder();
            ret.Append("Local de trabalho consistente em recinto situado em edificação de alvenaria com paredes e ");
            ret.Append("coberturas que asseguram proteção contra insolação excessiva e os rigores das variações das ");
            ret.Append("condições atmosféricas, tais como, temperatura, chuvas, ventos, etc, além de solo adequado ");
            ret.Append("que permite a circulação de pessoas e movimentação de materiais e ainda altura livre do piso ");
            ret.Append("ao teto superior a 3 metros em conformidade com o disposto no art. 171 da CLT, bem como ");
            ret.Append("iluminação ILUMINACAO_VARIAVEL e aberturas para ventilação VENTILACAO_VARIAVEL que permitem ");
            ret.Append("razoável conforto térmico para o exercício de atividades laborais.");
            
            if ((_bCHK_7N_0 || _bCHK_7N_1 || _bCHK_7N_2 || _bCHK_7N_3) && (_bCHK_7A_0 || _bCHK_7A_1 || _bCHK_7A_2 || _bCHK_7A_3 || _bCHK_7A_4))
                ret.Replace("ILUMINACAO_VARIAVEL", "natural e artificial");
            else if ((_bCHK_7N_0 || _bCHK_7N_1 || _bCHK_7N_2 || _bCHK_7N_3) && !(_bCHK_7A_0 || _bCHK_7A_1 || _bCHK_7A_2 || _bCHK_7A_3 || _bCHK_7A_4))
                ret.Replace("ILUMINACAO_VARIAVEL", "natural");
            else if (!(_bCHK_7N_0 || _bCHK_7N_1 || _bCHK_7N_2 || _bCHK_7N_3) && (_bCHK_7A_0 || _bCHK_7A_1 || _bCHK_7A_2 || _bCHK_7A_3 || _bCHK_7A_4))
                ret.Replace("ILUMINACAO_VARIAVEL", "artificial");
            else
                ret.Replace("ILUMINACAO_VARIAVEL", "natural e artificial");

            if ((_bCHK_6N_0 || _bCHK_6N_1 || _bCHK_6N_2) && (_bCHK_6F_0 || _bCHK_6F_1 || _bCHK_6F_2 || _bCHK_6F_3 || _bCHK_6F_4))
                ret.Replace("VENTILACAO_VARIAVEL", "natural e artificial");
            else if ((_bCHK_6N_0 || _bCHK_6N_1 || _bCHK_6N_2) && !(_bCHK_6F_0 || _bCHK_6F_1 || _bCHK_6F_2 || _bCHK_6F_3 || _bCHK_6F_4))
                ret.Replace("VENTILACAO_VARIAVEL", "natural");
            else if (!(_bCHK_6N_0 || _bCHK_6N_1 || _bCHK_6N_2) && (_bCHK_6F_0 || _bCHK_6F_1 || _bCHK_6F_2 || _bCHK_6F_3 || _bCHK_6F_4))
                ret.Replace("VENTILACAO_VARIAVEL", "artificial");
            else
                ret.Replace("VENTILACAO_VARIAVEL", "natural e artificial");

            return ret.ToString();
        }
        #endregion

        #region DescricaoLocalTrabalho

        public string DescricaoLocalTrabalho()
        {
            string[] strFrs = new string[9];
            string[] strTxt = new string[10];
            string[] str01 = new string[5];	//ArrayGrupo1
            string[] str04 = new string[7];	//ArrayGrupo4
            string[] str05 = new string[7];	//ArrayGrupo5
            string[] str6N = new string[3];	//ArrayGrupo6_N
            string[] str6F = new string[5];	//ArrayGrupo6_F
            string[] str7N = new string[4];	//ArrayGrupo7_N
            string[] str7A = new string[5];	//ArrayGrupo7_A

            //Grupo1    
            if (this._bCHK_01_0 == true) str01[0] = ", alvenaria";
            if (this._bCHK_01_1 == true) str01[1] = ", concreto";
            if (this._bCHK_01_2 == true) str01[2] = ", divisória";
            if (this._bCHK_01_3 == true) str01[3] = ", madeira";
            if (this._bCHK_01_4 == true) str01[4] = ", " + _tOUTROS_00;

            //Grupo4
            if (this._bCHK_04_0 == true) str04[0] = ", telha de fibro amianto";
            if (this._bCHK_04_1 == true) str04[1] = ", telha de argila";
            if (this._bCHK_04_2 == true) str04[2] = ", laje";
            if (this._bCHK_04_3 == true) str04[3] = ", zinco";
            if (this._bCHK_04_4 == true) str04[4] = ", " + _tOUTROS_03;
            if (this._bCHK_04_5 == true) str04[5] = ", telha de concreto";
            if (this._bCHK_04_6 == true) str04[6] = ", metálico";

            //Grupo5
            if (this._bCHK_05_0 == true) str05[0] = ", cimentado";
            if (this._bCHK_05_1 == true) str05[1] = ", cerâmico";
            if (this._bCHK_05_2 == true) str05[2] = ", carpete";
            if (this._bCHK_05_3 == true) str05[3] = ", madeira";
            if (this._bCHK_05_4 == true) str05[4] = ", granilite";
            if (this._bCHK_05_5 == true) str05[5] = ", paviflex";
            if (this._bCHK_05_6 == true) str05[6] = ", " + _tOUTROS_04;

            //Grupo6_N
            if (this._bCHK_6N_0 == true) str6N[0] = ", portas";
            if (this._bCHK_6N_1 == true) str6N[1] = ", janelas";
            if (this._bCHK_6N_2 == true) str6N[2] = ", " + _tOUTROS_05;

            //Grupo6_F
            if (this._bCHK_6F_0 == true) str6F[0] = ", exaustores";
            if (this._bCHK_6F_1 == true) str6F[1] = ", exaustores eólicos";
            if (this._bCHK_6F_2 == true) str6F[2] = ", ventiladores";
            if (this._bCHK_6F_3 == true) str6F[3] = ", ar-condicionado";
            if (this._bCHK_6F_4 == true) str6F[4] = ", " + _tOUTROS_06;

            //Grupo7_N
            if (this._bCHK_7N_0 == true) str7N[0] = ", janelas";
            if (this._bCHK_7N_1 == true) str7N[1] = ", portas";
            if (this._bCHK_7N_2 == true) str7N[2] = ", telhas translúcidas";
            if (this._bCHK_7N_3 == true) str7N[3] = ", " + _tOUTROS_07;

            //Grupo7_A
            if (this._bCHK_7A_0 == true) str7A[0] = ", lâmpada fluorescente";
            if (this._bCHK_7A_1 == true) str7A[1] = ", lâmpada incandescente";
            if (this._bCHK_7A_2 == true) str7A[2] = ", lâmpada mista";
            if (this._bCHK_7A_3 == true) str7A[3] = ", lâmpada LED"; //", lâmpada PL Eletrônica";
            if (this._bCHK_7A_4 == true) str7A[4] = ", " + _tOUTROS_08;


            //Concatena Arrays
            strFrs[0] = str01[0] + str01[1] + str01[2] + str01[3] + str01[4];
            strFrs[1] = _tOUTROS_01;
            strFrs[2] = _tOUTROS_02;
            strFrs[3] = str04[0] + str04[1] + str04[2] + str04[3] + str04[4] + str04[5] + str04[6];
            strFrs[4] = str05[0] + str05[1] + str05[2] + str05[3] + str05[4] + str05[5] + str05[6];
            strFrs[5] = str6N[0] + str6N[1] + str6N[2];
            strFrs[6] = str6F[0] + str6F[1] + str6F[2] + str6F[3] + str6F[4];
            strFrs[7] = str7N[0] + str7N[1] + str7N[2] + str7N[3];
            strFrs[8] = str7A[0] + str7A[1] + str7A[2] + str7A[3] + str7A[4];

            //Tira a virgula do primeiro 
            if (strFrs[0].Length > 0) strFrs[0] = strFrs[0].Substring(2); else strFrs[0] = "inexistente";
            if (strFrs[1].Length > 0) strFrs[1] = strFrs[1] + " m²"; else strFrs[1] = "inexistente";
            if (strFrs[2].Length > 0) strFrs[2] = strFrs[2] + " metros"; else strFrs[2] = "inexistente";
            if (strFrs[3].Length > 0) strFrs[3] = strFrs[3].Substring(2); else strFrs[3] = "inexistente";
            if (strFrs[4].Length > 0) strFrs[4] = strFrs[4].Substring(2); else strFrs[4] = "inexistente";
            if (strFrs[5].Length > 0) strFrs[5] = strFrs[5].Substring(2); else strFrs[5] = "inexistente";
            if (strFrs[6].Length > 0) strFrs[6] = strFrs[6].Substring(2); else strFrs[6] = "inexistente";
            if (strFrs[7].Length > 0) strFrs[7] = strFrs[7].Substring(2); else strFrs[7] = "inexistente";
            if (strFrs[8].Length > 0) strFrs[8] = strFrs[8].Substring(2); else strFrs[8] = "inexistente";

            strTxt[0] = "Construção: " + strFrs[0] + ". ";
            strTxt[1] = "Área aproximada: " + strFrs[1] + ". ";
            strTxt[2] = "Pé-direito aproximado: " + strFrs[2] + ". ";
            strTxt[3] = "Cobertura: " + strFrs[3] + ". ";
            strTxt[4] = "Piso existente: " + strFrs[4] + ". ";
            strTxt[5] = "Ventilação natural: " + strFrs[5] + ". ";
            strTxt[6] = "Ventilação mecanizada: " + strFrs[6] + ". ";
            strTxt[7] = "Iluminação natural: " + strFrs[7] + ". ";
            strTxt[8] = "Iluminação artificial: " + strFrs[8] + ". ";
            strTxt[9] = _tDS_OUTROS_ASP.ToString();

            StringBuilder ret = new StringBuilder();

            for (int i = 0; i <= 9; i++)
                ret.Append(strTxt[i]);

            return ret.ToString();
        }
        #endregion
    }
} 
