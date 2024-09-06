using System;
using System.Collections;
using Ilitera.Data;

namespace Ilitera.Common
{
    #region CNAE Class

    [Database("opsa","CNAE","IdCNAE")]
	public class CNAE : Ilitera.Data.Table 
	{
        public enum VersaoCnae : int
        {
            Ver10,
            Ver20
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CNAE()
		{

		}

		private	int _IdCNAE;
        private int _IndCNAE;
		private	string _Codigo = String.Empty;
		private	string _Descricao = String.Empty;
		private	int _GrauRisco;
		private	GrupoCipa _IdGrupoCipa;
        private float _SAT;
		
		public override int Id
		{
			get{return _IdCNAE;}
			set{_IdCNAE = value;}
		}
        public int IndCNAE
        {
            get { return _IndCNAE; }
            set { _IndCNAE = value; }
        }
		public string Codigo
		{
			get{return _Codigo;}
			set{_Codigo = value;}
		}
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
		public int GrauRisco
		{
			get{return _GrauRisco;}
			set{_GrauRisco = value;}
		}
		public GrupoCipa IdGrupoCipa
		{
			get{return _IdGrupoCipa;}
			set{_IdGrupoCipa = value;}
		}
        public float SAT
        {
            get { return _SAT; }
            set { _SAT = value; }
        }
        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return Descricao;
        }
        public string GetCodigo()
        {
            if (this.mirrorOld == null)
                this.Find();

            return this.Codigo;
        }

        public float GetSat()
        {
            if (this.IndCNAE == (int)VersaoCnae.Ver20)
                return this.SAT;
            else
            {
                if (this.GrauRisco == 1)
                    return 0.01F;
                else if (this.GrauRisco == 2)
                    return 0.02F;
                else if (this.GrauRisco == 3)
                    return 0.03F;
                else if (this.GrauRisco == 4)
                    return 0.03F;
                else
                    return 0;
            }
        }
    }
    #endregion

    #region TabelaCnae20x10 Class

    [Database("opsa", "TabelaCnae20x10", "ID")]
    public class TabelaCnae20x10 : Ilitera.Data.Table
    {
        private int _Id;
        private string _CNAE1 = String.Empty;
        private string _CNAE2 = String.Empty;
        private string _Observacoes = String.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TabelaCnae20x10()
        {

        }
        public override int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string CNAE1
        {
            get { return _CNAE1; }
            set { _CNAE1 = value; }
        }
        public string CNAE2
        {
            get { return _CNAE2; }
            set { _CNAE2 = value; }
        }

        public string Observacoes
        {
            get { return _Observacoes; }
            set { _Observacoes = value; }
        }

        public static void CorrigeCnae20()
        {
            ArrayList list = new CNAE().Find("IndCNAE=1 ORDER BY Codigo");

            foreach (CNAE cnae in list)
            {
                string where = "CNAE2='" + cnae.Codigo.Substring(0, 6) + "'";

                ArrayList listCnae2 = new TabelaCnae20x10().Find(where);

                if (listCnae2.Count == 0)
                    continue;

                string where2 = "Codigo='" + ((TabelaCnae20x10)listCnae2[0]).CNAE1 + "'"
                                + " AND IndCnae=0";

                ArrayList listCnae1 = new CNAE().Find(where2);

                if (listCnae1.Count == 0)
                    continue;

                cnae.Find();
                cnae.GrauRisco = ((CNAE)listCnae1[0]).GrauRisco;
                cnae.IdGrupoCipa = ((CNAE)listCnae1[0]).IdGrupoCipa;
                cnae.Save();
            }
        }
    }
    #endregion

    #region GrupoCipa Class

    [Database("opsa","GrupoCipa","IdGrupoCipa")]
	public class GrupoCipa : Ilitera.Data.Table 
	{
		private	int _IdGrupoCipa;
		private	string _Descricao = String.Empty;
        private string _CNAE20 = String.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public GrupoCipa()
		{

		}
		public override int Id
		{
			get{return _IdGrupoCipa;}
			set{_IdGrupoCipa = value;}
		}
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
        public string CNAE20
        {
            get { return _CNAE20; }
            set { _CNAE20 = value; }
        }
        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return this.Descricao;
        }

        public static void AtualizaGrupoCipa()
        {
            char[] seps = { ' ' };

            ArrayList listGrupoCipa = new GrupoCipa().FindAll();

            foreach (GrupoCipa grupoCipa in listGrupoCipa)
            {
                string[] scnaes = grupoCipa.CNAE20.Split(seps);

                foreach (string scnae in scnaes)
                {
                    if (scnae == string.Empty)
                        continue;

                    ArrayList cnaes = new CNAE().Find("(IndCNAE = 1) AND (Codigo LIKE '" + scnae.Replace(".", string.Empty) + "%')");

                    foreach (CNAE cnae in cnaes)
                    {
                        cnae.IdGrupoCipa = grupoCipa;
                        cnae.Save();
                    }
                }
            }

        }
    }
    #endregion
}
