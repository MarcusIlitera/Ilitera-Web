using System;
using System.Collections;
using System.Data;
using System.Drawing;
using Ilitera.Data;
using Ilitera.Common;
using System.Text;

namespace Ilitera.Opsa.Data
{
	[Database("sied_novo", "tblCARGO", "nID_CARGO")]
	public class Cargo : Ilitera.Data.Table
	{
		private int _nID_CARGO;
		private string _tNO_CARGO = string.Empty;
		private Cliente _nID_EMPR;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Cargo()
		{

		}
		public override int Id
		{														  
			get	{return _nID_CARGO;}
			set {_nID_CARGO = value;}
		}
		[Obrigatorio(true, "O nome do cargo é obrigatório!")]
		public string tNO_CARGO
		{														  
			get	{return _tNO_CARGO;}
			set {_tNO_CARGO = value;}
		}
		[Obrigatorio(true, "O Cliente é campo obrigatório!")]
		public Cliente nID_EMPR
		{
			get {return _nID_EMPR;}
			set {_nID_EMPR = value;}
		}
		public override string ToString()
		{
            if (this.mirrorOld == null)
                this.Find();

			return _tNO_CARGO;
		}
	}

	[Database("sied_novo", "tblFUNC_EMPREGADO", "nID_FUNC_EMPREGADO")]
	public class GheEmpregado : Ilitera.Data.Table
	{
		private int _nID_FUNC_EMPREGADO;
		private EmpregadoFuncao _nID_EMPREGADO_FUNCAO;
		private LaudoTecnico _nID_LAUD_TEC;
		private Ghe _nID_FUNC;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public GheEmpregado()
		{

		}
		public override int Id
		{														  
			get	{return _nID_FUNC_EMPREGADO;}
			set {_nID_FUNC_EMPREGADO = value;}
		}
		public EmpregadoFuncao nID_EMPREGADO_FUNCAO
		{														  
			get	{return _nID_EMPREGADO_FUNCAO;}
			set {_nID_EMPREGADO_FUNCAO = value;}
		}
		public LaudoTecnico nID_LAUD_TEC
		{														  
			get	{return _nID_LAUD_TEC;}
			set {_nID_LAUD_TEC = value;}
		}	
		public Ghe nID_FUNC
		{														  
			get	{return _nID_FUNC;}
			set {_nID_FUNC = value;}
		}

        public override void Validate()
        {
            if (this.Transaction == null)
            {
                //				if(this.nID_EMPREGADO_FUNCAO.nID_EMPR==null)
                //					this.nID_EMPREGADO_FUNCAO.Find();
                //
                //				if(this.nID_EMPREGADO_FUNCAO.hDT_TERMINO!=new DateTime())
                //				{
                //					if(this.nID_LAUD_TEC.nID_EMPR==null)
                //						this.nID_LAUD_TEC.Find();
                //
                //					if(this.nID_EMPREGADO_FUNCAO.hDT_TERMINO < this.nID_LAUD_TEC.hDT_LAUDO)
                //						throw new Exception("A data do laudo é maior que a data de término da classificação funcional!");
                //				}
                //
                //				if(this.nID_EMPREGADO_FUNCAO.hDT_INICIO!=new DateTime()
                //					&& this.nID_EMPREGADO_FUNCAO.hDT_TERMINO==new DateTime())
                //				{
                //					if(this.nID_LAUD_TEC.nID_EMPR==null)
                //						this.nID_LAUD_TEC.Find();
                //
                //					LaudoTecnico laudo = LaudoTecnico.GetUltimoLaudo(this.nID_LAUD_TEC.nID_EMPR.Id);
                //
                //					if(laudo.Id!=this.nID_LAUD_TEC.Id)
                //						if(this.nID_EMPREGADO_FUNCAO.hDT_INICIO	> this.nID_LAUD_TEC.hDT_LAUDO.AddYears(1))
                //							throw new Exception("A data do laudo é menor que a data de inicio da classificação funcional!");
                //				}

                try
                {
                    if (this.Id == 0)
                    {
                        if (this.nID_EMPREGADO_FUNCAO.mirrorOld == null)
                            this.nID_EMPREGADO_FUNCAO.Find();

                        if (this.nID_EMPREGADO_FUNCAO.nID_EMPREGADO.mirrorOld == null)
                            this.nID_EMPREGADO_FUNCAO.nID_EMPREGADO.Find();

                        this.nID_EMPREGADO_FUNCAO.nID_EMPREGADO.GerarExamePlanejamento(this.nID_EMPREGADO_FUNCAO, this.nID_FUNC);
                    }
                }
                catch { }
            }
            base.Validate();
        }
	}

	[Database("sied_novo", "tblEPI_RCM", "nID_EPI_RCM")]
	public class EpiRecomendado : Ilitera.Data.Table
	{
		private int _nID_EPI_RCM; 
		private PPRA _nID_PPRA;
		private Risco _nID_RSC;
		private Epi _nID_EPI;
		private int _nTPO_EPI;
		private bool _bCONDICAO;
		private string _tDS_CONDICAO=string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EpiRecomendado()
		{

		}
		public override int Id
		{														  
			get	{return _nID_EPI_RCM;}
			set {_nID_EPI_RCM = value;}
		}

		public PPRA nID_PPRA
		{
			get{return _nID_PPRA;}
			set{_nID_PPRA = value;}
		}
		public Risco nID_RSC
		{
			get{return _nID_RSC;}
			set{_nID_RSC = value;}
		}
		public Epi nID_EPI
		{
			get{return _nID_EPI;}
			set{_nID_EPI = value;}
		}
		public int nTPO_EPI
		{
			get{return _nTPO_EPI;}
			set{_nTPO_EPI = value;}
		}
		public bool bCONDICAO
		{
			get{return _bCONDICAO;}
			set{_bCONDICAO = value;}
		}
		public string tDS_CONDICAO
		{
			get{return _tDS_CONDICAO;}
			set{_tDS_CONDICAO = value;}
		}	
	}

	[Database("sied_novo", "tblEPI_EXTE", "nID_EPI_EXTE")]
	public class EpiExistente : Ilitera.Data.Table
	{
		private int _nID_EPI_EXTE; 
		private PPRA _nID_PPRA;
		private Risco _nID_RSC;
		private Epi _nID_EPI;
		private int _nTPO_EPI;
		private bool _bCONDICAO;
		private string _tDS_CONDICAO=string.Empty;
		private bool _bFORCADO;
		private string _tDS_FORCADO=string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EpiExistente()
		{
		}
		public override int Id
		{														  
			get	{return _nID_EPI_EXTE;}
			set {_nID_EPI_EXTE = value;}
		}

		public PPRA nID_PPRA
		{
			get{return _nID_PPRA;}
			set{_nID_PPRA = value;}
		}
		public Risco nID_RSC
		{
			get{return _nID_RSC;}
			set{_nID_RSC = value;}
		}
		public Epi nID_EPI
		{
			get{return _nID_EPI;}
			set{_nID_EPI = value;}
		}
		public int nTPO_EPI
		{
			get{return _nTPO_EPI;}
			set{_nTPO_EPI = value;}
		}
		public bool bCONDICAO
		{
			get{return _bCONDICAO;}
			set{_bCONDICAO = value;}
		}
		public string tDS_CONDICAO
		{
			get{return _tDS_CONDICAO;}
			set{_tDS_CONDICAO = value;}
		}
		public bool bFORCADO
		{
			get{return _bFORCADO;}
			set{_bFORCADO = value;}
		}
		public string tDS_FORCADO
		{
			get{return _tDS_FORCADO;}
			set{_tDS_FORCADO = value;}
		}
		public override string ToString()
		{
			string ret=string.Empty;
			if(this.nID_EPI!=null && this.nID_EPI.Id!=0 && this.nID_EPI.ToString()==string.Empty)
				this.nID_EPI.Find();
			if(this.tDS_CONDICAO==string.Empty)
				ret = this.nID_EPI.ToString();
			else
				ret = this.nID_EPI.ToString()+" ("+this.tDS_CONDICAO+")";
			return ret;
		}
	}

	[Database("sied_novo", "tblFUNC_EPI_RCM", "nID_FUNC_EPI_RCM")]
	public class GheEpiRecomendado : Ilitera.Data.Table
	{
		private int _nID_FUNC_EPI_RCM; 
		private Ghe _nID_FUNC;
		private Epi _nID_EPI;
		private bool _bCONDICAO;
		private string _tDS_CONDICAO=string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public GheEpiRecomendado()
		{
		}
		public override int Id
		{														  
			get	{return _nID_FUNC_EPI_RCM;}
			set {_nID_FUNC_EPI_RCM = value;}
		}
		public Ghe nID_FUNC
		{														  
			get	{return _nID_FUNC;}
			set {_nID_FUNC = value;}
		}
		public Epi nID_EPI
		{
			get{return _nID_EPI;}
			set{_nID_EPI = value;}
		}
		public bool bCONDICAO
		{
			get{return _bCONDICAO;}
			set{_bCONDICAO = value;}
		}
		public string tDS_CONDICAO
		{
			get{return _tDS_CONDICAO;}
			set{_tDS_CONDICAO = value;}
		}		
		public override int Save()
		{
			this.bCONDICAO = (this.tDS_CONDICAO!=string.Empty);
			return base.Save ();
		}
	}


    [Database("sied_novo", "tblLAUDO_TEC_Biologica", "nID_LAUD_TEC_Biologica")]
    public class Laudo_Monitoracao_Biologica : Ilitera.Data.Table
    {
        private int _nID_LAUD_TEC_Biologica;
        private int _nID_LAUD_TEC;
        private DateTime _Data_Inicial;
        private DateTime _Data_Final;
        private string _NIT;
        private string _Registro;
        private string _Nome;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Laudo_Monitoracao_Biologica()
        {
        }
        public override int Id
        {
            get { return _nID_LAUD_TEC_Biologica; }
            set { _nID_LAUD_TEC_Biologica = value; }
        }
        public int nID_LAUD_TEC
        {
            get { return _nID_LAUD_TEC; }
            set { _nID_LAUD_TEC = value; }
        }

        public DateTime Data_Inicial
        {
            get { return _Data_Inicial; }
            set { _Data_Inicial = value; }
        }
        public DateTime Data_Final
        {
            get { return _Data_Final; }
            set { _Data_Final = value; }
        }
        public string NIT
        {
            get { return _NIT; }
            set { _NIT = value; }
        }
        public string Registro
        {
            get { return _Registro; }
            set { _Registro = value; }
        }
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        public override int Save()
        {
            return base.Save();
        }
    }





    [Database("sied_novo", "tblLAUDO_TEC_Nr20_Item", "nID_LAUD_TEC_Nr20_Item")]
    public class Laudo_NR20_Item : Ilitera.Data.Table
    {
        private int _nID_LAUD_TEC_Nr20_Item;
        private int _nID_LAUD_TEC;
        private string _Item;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Laudo_NR20_Item()
        {
        }
        public override int Id
        {
            get { return _nID_LAUD_TEC_Nr20_Item; }
            set { _nID_LAUD_TEC_Nr20_Item = value; }
        }
        public int nID_LAUD_TEC
        {
            get { return _nID_LAUD_TEC; }
            set { _nID_LAUD_TEC = value; }
        }
        public string Item
        {
            get { return _Item; }
            set { _Item = value; }
        }
        public override int Save()
        {
            return base.Save();
        }
    }




    [Database("sied_novo", "tblLAUDO_TEC_Nr20_Empr_Treinado", "nID_LAUD_TEC_Nr20_Empr_Treinado")]
    public class Laudo_NR20_Empregado_Treinado : Ilitera.Data.Table
    {
        private int _nID_LAUD_TEC_Nr20_Empr_Treinado;
        private int _nID_LAUD_TEC_Nr20_Item;
        private string _Empregado_Treinado;
        private float _Horas_Formacao;
        private string _Imagem_Certificado;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Laudo_NR20_Empregado_Treinado()
        {
        }
        public override int Id
        {
            get { return _nID_LAUD_TEC_Nr20_Empr_Treinado; }
            set { _nID_LAUD_TEC_Nr20_Empr_Treinado = value; }
        }
        public int nID_LAUD_TEC_Nr20_Item
        {
            get { return _nID_LAUD_TEC_Nr20_Item; }
            set { _nID_LAUD_TEC_Nr20_Item = value; }
        }
        public string Empregado_Treinado
        {
            get { return _Empregado_Treinado; }
            set { _Empregado_Treinado = value; }
        }
        public float Horas_Formacao
        {
            get { return _Horas_Formacao; }
            set { _Horas_Formacao = value; }
        }
        public string Imagem_Certificado
        {
            get { return _Imagem_Certificado; }
            set { _Imagem_Certificado = value; }
        }

        public override int Save()
        {
            return base.Save();
        }
    }



    [Database("sied_novo", "tblLAUDO_TEC_Nr20_Inventario", "nID_LAUD_TEC_Nr20_Inventario")]
    public class Laudo_NR20_Inventario : Ilitera.Data.Table
    {
        private int _nID_LAUD_TEC_Nr20_Inventario;
        private int _nID_LAUD_TEC_Nr20_Item;
        private string _Produto_Quimico;
        private float _Qtde_Minima;
        private float _Qtde_Maxima;
        private string _Tipo_Embalagem;
        private string _Responsavel_Armazenamento;
        private string _Imagem_Certificado_Inmetro;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Laudo_NR20_Inventario()
        {
        }
        public override int Id
        {
            get { return _nID_LAUD_TEC_Nr20_Inventario; }
            set { _nID_LAUD_TEC_Nr20_Inventario = value; }
        }
        public int nID_LAUD_TEC_Nr20_Item
        {
            get { return _nID_LAUD_TEC_Nr20_Item; }
            set { _nID_LAUD_TEC_Nr20_Item = value; }
        }
        public string Produto_Quimico
        {
            get { return _Produto_Quimico; }
            set { _Produto_Quimico = value; }
        }
        public float Qtde_Minima
        {
            get { return _Qtde_Minima; }
            set { _Qtde_Minima = value; }
        }
        public float Qtde_Maxima
        {
            get { return _Qtde_Maxima; }
            set { _Qtde_Maxima = value; }
        }
        public string Tipo_Embalagem
        {
            get { return _Tipo_Embalagem; }
            set { _Tipo_Embalagem = value; }
        }
        public string Responsavel_Armazenamento
        {
            get { return _Responsavel_Armazenamento; }
            set { _Responsavel_Armazenamento = value; }
        }
        public string Imagem_Certificado_Inmetro
        {
            get { return _Imagem_Certificado_Inmetro; }
            set { _Imagem_Certificado_Inmetro = value; }
        }

        public override int Save()
        {
            return base.Save();
        }
    }




    [Database("sied_novo", "tblLAUDO_TEC_Nr20_Planos_Prevencao", "nID_LAUD_TEC_Nr20_Planos_Prevencao")]
    public class Laudo_NR20_Planos_Prevencao : Ilitera.Data.Table
    {
        private int _nID_LAUD_TEC_Nr20_Planos_Prevencao;
        private int _nID_LAUD_TEC_Nr20_Item;
        private string _Planos_Prevencao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Laudo_NR20_Planos_Prevencao()
        {
        }
        public override int Id
        {
            get { return _nID_LAUD_TEC_Nr20_Planos_Prevencao; }
            set { _nID_LAUD_TEC_Nr20_Planos_Prevencao = value; }
        }
        public int nID_LAUD_TEC_Nr20_Item
        {
            get { return _nID_LAUD_TEC_Nr20_Item; }
            set { _nID_LAUD_TEC_Nr20_Item = value; }
        }
        public string Planos_Prevencao
        {
            get { return _Planos_Prevencao; }
            set { _Planos_Prevencao = value; }
        }
        public override int Save()
        {
            return base.Save();
        }
    }



    [Database("sied_novo", "tblLAUDO_TEC_Nr20_Procedimentos_Emergencia", "nID_LAUD_TEC_Nr20_Procedimentos_Emergencia")]
    public class Laudo_NR20_Procedimentos_Emergencia : Ilitera.Data.Table
    {
        private int _nID_LAUD_TEC_Nr20_Procedimentos_Emergencia;
        private int _nID_LAUD_TEC_Nr20_Item;
        private string _Procedimentos;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Laudo_NR20_Procedimentos_Emergencia()
        {
        }
        public override int Id
        {
            get { return _nID_LAUD_TEC_Nr20_Procedimentos_Emergencia; }
            set { _nID_LAUD_TEC_Nr20_Procedimentos_Emergencia = value; }
        }
        public int nID_LAUD_TEC_Nr20_Item
        {
            get { return _nID_LAUD_TEC_Nr20_Item; }
            set { _nID_LAUD_TEC_Nr20_Item = value; }
        }
        public string Procedimentos
        {
            get { return _Procedimentos; }
            set { _Procedimentos = value; }
        }
        public override int Save()
        {
            return base.Save();
        }
    }



    [Database("sied_novo", "tblLAUDO_TEC_Nr20_Procedimentos_Prevencao", "nID_LAUD_TEC_Nr20_Procedimentos_Prevencao")]
    public class Laudo_NR20_Procedimentos_Prevencao : Ilitera.Data.Table
    {
        private int _nID_LAUD_TEC_Nr20_Procedimentos_Prevencao;
        private int _nID_LAUD_TEC_Nr20_Item;
        private string _Procedimentos;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        
        public Laudo_NR20_Procedimentos_Prevencao()
        {
        }
        public override int Id
        {
            get { return _nID_LAUD_TEC_Nr20_Procedimentos_Prevencao; }
            set { _nID_LAUD_TEC_Nr20_Procedimentos_Prevencao = value; }
        }
        public int nID_LAUD_TEC_Nr20_Item
        {
            get { return _nID_LAUD_TEC_Nr20_Item; }
            set { _nID_LAUD_TEC_Nr20_Item = value; }
        }
        public string Procedimentos
        {
            get { return _Procedimentos; }
            set { _Procedimentos = value; }
        }
        public override int Save()
        {
            return base.Save();
        }
    }


    [Database("sied_novo", "tblLAUDO_TEC_Nr20_Riscos_Acidentes", "nID_LAUD_TEC_Nr20_Riscos_Acidentes")]
    public class Laudo_NR20_Riscos_Acidentes : Ilitera.Data.Table
    {
        private int _nID_LAUD_TEC_Nr20_Riscos_Acidentes;
        private int _nID_LAUD_TEC_Nr20_Item;
        private string _Riscos_Acidentes;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        
        public Laudo_NR20_Riscos_Acidentes()
        {
        }
        public override int Id
        {
            get { return _nID_LAUD_TEC_Nr20_Riscos_Acidentes; }
            set { _nID_LAUD_TEC_Nr20_Riscos_Acidentes = value; }
        }
        public int nID_LAUD_TEC_Nr20_Item
        {
            get { return _nID_LAUD_TEC_Nr20_Item; }
            set { _nID_LAUD_TEC_Nr20_Item = value; }
        }
        public string Riscos_Acidentes
        {
            get { return _Riscos_Acidentes; }
            set { _Riscos_Acidentes = value; }
        }
        public override int Save()
        {
            return base.Save();
        }
    }


    [Database("sied_novo", "tblLAUDO_TEC_Nr20_Riscos_Quimicos", "nID_LAUD_TEC_Nr20_Riscos_Quimicos")]
    public class Laudo_NR20_Riscos_Quimicos : Ilitera.Data.Table
    {
        private int _nID_LAUD_TEC_Nr20_Riscos_Quimicos;
        private int _nID_LAUD_TEC_Nr20_Item;
        private string _Riscos_Quimicos;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        
        public Laudo_NR20_Riscos_Quimicos()
        {
        }
        public override int Id
        {
            get { return _nID_LAUD_TEC_Nr20_Riscos_Quimicos; }
            set { _nID_LAUD_TEC_Nr20_Riscos_Quimicos = value; }
        }
        public int nID_LAUD_TEC_Nr20_Item
        {
            get { return _nID_LAUD_TEC_Nr20_Item; }
            set { _nID_LAUD_TEC_Nr20_Item = value; }
        }
        public string Riscos_Quimicos
        {
            get { return _Riscos_Quimicos; }
            set { _Riscos_Quimicos = value; }
        }
        public override int Save()
        {
            return base.Save();
        }
    }






	[Database("sied_novo", "tblFUNC_EPI_EXTE", "nID_FUNC_EPI_EXTE")]
	public class GheEpiExistente : Ilitera.Data.Table
	{
		private int _nID_FUNC_EPI_EXTE; 
		private Ghe _nID_FUNC;
		private Epi _nID_EPI;
		private bool _bFORCADO;
		private string _tDS_FORCADO=string.Empty;
		private bool _bCONDICAO;
		private string _tDS_CONDICAO=string.Empty; 

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

		public GheEpiExistente()
		{
		}
		public override int Id
		{														  
			get	{return _nID_FUNC_EPI_EXTE;}
			set {_nID_FUNC_EPI_EXTE = value;}
		}
		public Ghe nID_FUNC
		{														  
			get	{return _nID_FUNC;}
			set {_nID_FUNC = value;}
		}
		public Epi nID_EPI
		{
			get{return _nID_EPI;}
			set{_nID_EPI = value;}
		}
		public bool bFORCADO
		{
			get{return _bFORCADO;}
			set{_bFORCADO = value;}
		}
		public string tDS_FORCADO
		{
			get{return _tDS_FORCADO;}
			set{_tDS_FORCADO = value;}
		}
		public bool bCONDICAO
		{
			get{return _bCONDICAO;}
			set{_bCONDICAO = value;}
		}
		public string tDS_CONDICAO
		{
			get{return _tDS_CONDICAO;}
			set{_tDS_CONDICAO = value;}
		}
		public override int Save()
		{
			this.bCONDICAO = (this.tDS_CONDICAO!=string.Empty);
			this.bFORCADO = (this.tDS_FORCADO!=string.Empty);
			return base.Save ();
		}

		public override string ToString()
		{
			string ret=string.Empty;

			if(this.nID_EPI!=null 
				&& this.nID_EPI.Id!=0 
				&& this.nID_EPI.ToString()==string.Empty)
				this.nID_EPI.Find();
			
			if(this.tDS_CONDICAO==string.Empty)
				ret = this.nID_EPI.ToString();
			else
				ret = this.nID_EPI.ToString()+" ("+this.tDS_CONDICAO+")";

			return ret;
		}
	}

	[Database("sied_novo","CBO_CORRIGIDO")]
	public class CboCorrigido : Ilitera.Data.Table 
	{
		public override int Id
		{														  
			get	{return 0;}
			set {}
		}
		private string _CBO = string.Empty; 
		private string _NOME = string.Empty;  
		private string _DESCRICAO = string.Empty; 
		private string _REQUISITO = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CboCorrigido()
		{

		}
		public string CBO
		{														  
			get	{return _CBO;}
			set {_CBO = value;}
		}
		public string NOME
		{														  
			get	{return _NOME;}
			set {_NOME = value;}
		}
		public string DESCRICAO
		{														  
			get	{return _DESCRICAO;}
			set {_DESCRICAO = value;}
		}
		public string REQUISITO
		{														  
			get	{return _REQUISITO;}
			set {_REQUISITO = value;}
		}
		public override string ToString()
		{
			return this.CBO + " " + this.NOME;
		}
	}

	public enum TipoCbo {CBO94=1, CBO2002=2};

	[Database("cbo", "TableCBO", "Id")]
	public class Cbo : Ilitera.Data.Table 
	{
		private int _Id;
		private string _CBO = string.Empty; 
		private byte _TIPO;		
		private string _NOME = string.Empty; 
		private string _DESCRICAO = string.Empty; 
		private string _REQUISITO = string.Empty; 
		private string _ATIVIDADES = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        
        public Cbo()
		{
		}
		public override int Id
		{														  
			get	{return _Id;}
			set {_Id = value;}
		}
		public string CBO
		{														  
			get	{return _CBO;}
			set {_CBO = value;}
		}
		public byte TIPO
		{														  
			get	{return _TIPO;}
			set {_TIPO = value;}
		}
		public string NOME
		{														  
			get	{return _NOME;}
			set {_NOME = value;}
		}
		public string DESCRICAO
		{														  
			get	{return _DESCRICAO;}
			set {_DESCRICAO = value;}
		}
		public string REQUISITO
		{														  
			get	{return _REQUISITO;}
			set {_REQUISITO = value;}
		}
		public string ATIVIDADES
		{														  
			get	{return _ATIVIDADES;}
			set {_ATIVIDADES = value;}
		}
		public override string ToString()
		{
			return this.CBO + " " + this.NOME;
		}
	}

	[Database("sied_novo", "tblERG_NRs", "nID_NRs")]
	public class ErgonomiaNRs : Ilitera.Data.Table
	{
		private int _nID_NRs;
		private bool _bAnaliseErgonomica;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ErgonomiaNRs()
		{

		}
		public override int Id
		{														  
			get	{return _nID_NRs;}
			set {_nID_NRs = value;}
		}
		public bool bAnaliseErgonomica
		{														  
			get	{return _bAnaliseErgonomica;}
			set {_bAnaliseErgonomica = value;}
		}
	}

    [Database("sied_novo", "tblERG_NRs_New", "nID_NRs")]
    public class ErgonomiaNRs_New : Ilitera.Data.Table
    {
        private int _nID_NRs;
        private bool _bAnaliseErgonomica;

        public ErgonomiaNRs_New()
        {

        }
        public override int Id
        {
            get { return _nID_NRs; }
            set { _nID_NRs = value; }
        }
        public bool bAnaliseErgonomica
        {
            get { return _bAnaliseErgonomica; }
            set { _bAnaliseErgonomica = value; }
        }
    }


    [Database("sied_novo", "tblERG_GHE", "nID_ERG_GHE")]
	public class ErgonomiaGhe : Ilitera.Data.Table
	{
		private int _nID_ERG_GHE;
		private LaudoTecnico _nID_LAUD_TEC;
		private Ghe _nID_FUNC ;
		private int _nID_NRs;
		private int _nRSP ;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ErgonomiaGhe()
		{

		}		
		public override int Id
		{														  
			get	{return _nID_ERG_GHE;}
			set {_nID_ERG_GHE = value;}
		}
		public LaudoTecnico nID_LAUD_TEC
		{														  
			get	{return _nID_LAUD_TEC;}
			set {_nID_LAUD_TEC = value;}
		}
		public Ghe nID_FUNC
		{														  
			get	{return _nID_FUNC;}
			set {_nID_FUNC = value;}
		}
		public int nID_NRs
		{														  
			get	{return _nID_NRs;}
			set {_nID_NRs = value;}
		}
		public int nRSP
		{														  
			get	{return _nRSP;}
			set {_nRSP = value;}
		}		

		public static void CriarQuestionario(Ghe ghe)
		{
			ArrayList list = new ErgonomiaNRs().Find("bAnaliseErgonomica = 1" );
			
			foreach(ErgonomiaNRs ergonomiaNRs in list)
			{
				ErgonomiaGhe ergonomiaGhe = new ErgonomiaGhe();

				ergonomiaGhe.Find( "nID_LAUD_TEC="+ghe.nID_LAUD_TEC.Id
									+" AND nID_FUNC="+ghe.Id
									+" AND nID_NRs="+ergonomiaNRs.Id);

				if(ergonomiaGhe.Id==0)
				{
					ergonomiaGhe.Inicialize();
					ergonomiaGhe.nID_LAUD_TEC.Id = ghe.nID_LAUD_TEC.Id;
					ergonomiaGhe.nID_FUNC.Id = ghe.Id;
					ergonomiaGhe.nID_NRs = ergonomiaNRs.Id;
					ergonomiaGhe.Save();
				}
			}
		}
	}



    [Database("sied_novo", "tblERG_GHE_New", "nID_ERG_GHE")]
    public class ErgonomiaGhe_New : Ilitera.Data.Table
    {
        private int _nID_ERG_GHE;
        private LaudoTecnico _nID_LAUD_TEC;
        private Ghe _nID_FUNC;
        private int _nID_NRs;
        private int _nRSP;

        public ErgonomiaGhe_New()
        {

        }
        public override int Id
        {
            get { return _nID_ERG_GHE; }
            set { _nID_ERG_GHE = value; }
        }
        public LaudoTecnico nID_LAUD_TEC
        {
            get { return _nID_LAUD_TEC; }
            set { _nID_LAUD_TEC = value; }
        }
        public Ghe nID_FUNC
        {
            get { return _nID_FUNC; }
            set { _nID_FUNC = value; }
        }
        public int nID_NRs
        {
            get { return _nID_NRs; }
            set { _nID_NRs = value; }
        }
        public int nRSP
        {
            get { return _nRSP; }
            set { _nRSP = value; }
        }

        public static void CriarQuestionario(Ghe ghe)
        {
            ArrayList list = new ErgonomiaNRs_New().Find("bAnaliseErgonomica = 1");

            foreach (ErgonomiaNRs_New ergonomiaNRs in list)
            {
                ErgonomiaGhe_New ergonomiaGhe = new ErgonomiaGhe_New();

                ergonomiaGhe.Find("nID_LAUD_TEC=" + ghe.nID_LAUD_TEC.Id
                                    + " AND nID_FUNC=" + ghe.Id
                                    + " AND nID_NRs=" + ergonomiaNRs.Id);

                if (ergonomiaGhe.Id == 0)
                {
                    ergonomiaGhe.Inicialize();
                    ergonomiaGhe.nID_LAUD_TEC.Id = ghe.nID_LAUD_TEC.Id;
                    ergonomiaGhe.nID_FUNC.Id = ghe.Id;
                    ergonomiaGhe.nID_NRs = ergonomiaNRs.Id;
                    ergonomiaGhe.Save();
                }
            }
        }
    }


    [Database("sied_novo", "tblLIM_TOL", "nID_LIM_TOL")]
	public class LimiteTolerancia : Ilitera.Data.Table
	{
		private int _nID_LIM_TOL;
		private Risco _nID_RSC;
		private int _tVL_MED ;
		private string _tVL_LIM_TOL=string.Empty;
		private string _tUN_LIM_TOL=string.Empty;
		private float _sVL_LIM_TOL_MIN;
		private float _sVL_LIM_TOL_MAX;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public LimiteTolerancia()
		{
		}		
		public override int Id
		{														  
			get	{return _nID_LIM_TOL;}
			set {_nID_LIM_TOL = value;}
		}	
		public Risco nID_RSC
		{														  
			get	{return _nID_RSC;}
			set {_nID_RSC = value;}
		}
		public int tVL_MED
		{														  
			get	{return _tVL_MED;}
			set {_tVL_MED = value;}
		}
		public string tVL_LIM_TOL
		{														  
			get	{return _tVL_LIM_TOL;}
			set {_tVL_LIM_TOL = value;}
		}
		public float sVL_LIM_TOL_MIN
		{														  
			get	{return _sVL_LIM_TOL_MIN;}
			set {_sVL_LIM_TOL_MIN = value;}
		}	
		public float sVL_LIM_TOL_MAX
		{														  
			get	{return _sVL_LIM_TOL_MAX;}
			set {_sVL_LIM_TOL_MAX = value;}
		}	
	}		

	[Database("sied_novo", "tblTRAJ_RSC", "nID_TRAJ_RSC")]
	public class TrajetoriaRiscos : Ilitera.Data.Table
	{
		private int _nID_TRAJ_RSC;
		private Risco _nID_RSC;
		private AgenteQuimico _nID_AG_NCV ;
		private string _tDS_TRAJ_RSC = string.Empty;
		private bool _gIN_SUG ;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TrajetoriaRiscos()
		{
		}		
		public override int Id
		{														  
			get	{return _nID_TRAJ_RSC;}
			set {_nID_TRAJ_RSC = value;}
		}	
		public Risco nID_RSC
		{														  
			get	{return _nID_RSC;}
			set {_nID_RSC = value;}
		}
		public AgenteQuimico nID_AG_NCV
		{														  
			get	{return _nID_AG_NCV;}
			set {_nID_AG_NCV = value;}
		}
		[Obrigatorio(true, "Nome da trajetória é campo obrigatório!")]
		public string tDS_TRAJ_RSC
		{														  
			get	{return _tDS_TRAJ_RSC;}
			set {_tDS_TRAJ_RSC = value;}
		}
		public bool gIN_SUG
		{														  
			get	{return _gIN_SUG;}
			set {_gIN_SUG = value;}
		}		
		public override string ToString()
		{
			return this.tDS_TRAJ_RSC;
		}

	}

	[Database("sied_novo", "tblMEIO_PROP", "nID_MEIO_PROP")]
	public class MeioPropagacao : Ilitera.Data.Table
	{
		private int _nID_MEIO_PROP;
		private Risco _nID_RSC;
		private AgenteQuimico _nID_AG_NCV ;
		private string _tDS_MEIO_PROP = string.Empty;
		private bool _gIN_SUG ;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public MeioPropagacao()
		{
		}		
		public override int Id
		{														  
			get	{return _nID_MEIO_PROP;}
			set {_nID_MEIO_PROP = value;}
		}	
		public Risco nID_RSC
		{														  
			get	{return _nID_RSC;}
			set {_nID_RSC = value;}
		}
		public AgenteQuimico nID_AG_NCV
		{														  
			get	{return _nID_AG_NCV;}
			set {_nID_AG_NCV = value;}
		}
		[Obrigatorio(true, "Nome do meio de propagação é campo obrigatório!")]
		public string tDS_MEIO_PROP
		{														  
			get	{return _tDS_MEIO_PROP;}
			set {_tDS_MEIO_PROP = value;}
		}
		public bool gIN_SUG
		{														  
			get	{return _gIN_SUG;}
			set {_gIN_SUG = value;}
		}			
		public override string ToString()
		{
			return this.tDS_MEIO_PROP;
		}
	}

	[Database("sied_novo", "tblTP_ATV", "nID_TP_ATV")]
	public class AtividadeAgenteBiologico : Ilitera.Data.Table
	{
		private int _nID_TP_ATV;
		private string _tNO_TP_ATV	= string.Empty;
		private string _tDS_TP_ATV	= string.Empty;
		private string _mFRS_TP_ATV = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public AtividadeAgenteBiologico()
		{

		}		
		public override int Id
		{														  
			get	{return _nID_TP_ATV;}
			set {_nID_TP_ATV = value;}
		}	
		public string tNO_TP_ATV
		{														  
			get	{return _tNO_TP_ATV;}
			set {_tNO_TP_ATV = value;}
		}
		public string tDS_TP_ATV
		{														  
			get	{return _tDS_TP_ATV;}
			set {_tDS_TP_ATV = value;}
		}
		public string mFRS_TP_ATV
		{														  
			get	{return _mFRS_TP_ATV;}
			set {_mFRS_TP_ATV = value;}
		}	
		public override string ToString()
		{
			return _tNO_TP_ATV;
		}
	}
}