using System;
using System.Data;
using System.Text;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;
using Ilitera.Opsa.Data;


namespace Ilitera.Opsa.Data
{
	[Database("opsa", "FaturamentoServico", "IdFaturamentoServico")]
	public class FaturamentoServico: Ilitera.Data.Table
	{
		private int _IdFaturamentoServico;
		private Faturamento _IdFaturamento;
		private Servico _IdServico;
		private float _Quantidade;
		private float _ValorUnitario;
		private float _ValorTotal;
		private string _ObservacaoNota=string.Empty;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FaturamentoServico()
		{

		}
		public override int Id
		{														  
			get{return _IdFaturamentoServico;}
			set	{_IdFaturamentoServico = value;}
		}
		public Faturamento IdFaturamento
		{														  
			get{return _IdFaturamento;}
			set	{_IdFaturamento = value;}
		}
		public Servico IdServico
		{														  
			get{return _IdServico;}
			set	{_IdServico = value;}
		}
		public float Quantidade
		{														  
			get{return _Quantidade;}
			set	{_Quantidade = value;}
		}
		public float ValorUnitario
		{														  
			get{return _ValorUnitario;}
			set	{_ValorUnitario = value;}
		}
		public float ValorTotal
		{														  
			get{return _ValorTotal;}
			set	{_ValorTotal = value;}
		}
		public string ObservacaoNota
		{
			get{return _ObservacaoNota;}
			set{_ObservacaoNota = value;}
		}

        public string GetDescricaoServico()
        {
            StringBuilder ret = new StringBuilder();

            if (this.IdServico.mirrorOld == null)
                this.IdServico.Find();

            ret.Append(this.IdServico.DescricaoNF);

            if (this.IdServico.IndTipoServico == (int)TipoServico.Pcmso
                && this.Quantidade != 1
                && this.Quantidade != 0)
            {
                if (this.IdFaturamento.mirrorOld == null)
                    this.IdFaturamento.Find();

                if (this.IdFaturamento.IdContrato.mirrorOld == null)
                    this.IdFaturamento.IdContrato.Find();

                if(!this.IdFaturamento.IdContrato.BoletoComDemostrativo)
                    ret.Append(" (" + (this.ValorTotal / this.Quantidade).ToString("n") + " X " + this.Quantidade.ToString() + ")");
            }

            if (this.ObservacaoNota != string.Empty)
                ret.Append(" - " + this.ObservacaoNota);

            return ret.ToString();
        }

		public string GetDescricaoServicoNF()
		{
            if (this.IdServico.mirrorOld == null)
                this.IdServico.Find();

            if (this.ObservacaoNota != string.Empty)
                return Justificado(this.IdServico.DescricaoNF + "\n " + this.ObservacaoNota, 37, 17);
            else
                return Justificado(this.IdServico.DescricaoNF, 37, 17); 
		}

        public string GetPrestacaoDeSerivosNF()
        {
            if (this.IdServico.mirrorOld == null)
                this.IdServico.Find();

            string ret;

            int i = this.IdServico.DescricaoNF.IndexOf(".");

            if (i != -1 && i < 40)
                ret = this.IdServico.DescricaoNF.Substring(0, i);
            else if (this.IdServico.DescricaoNF.Length > 40)
                ret = this.IdServico.DescricaoNF.Substring(0, 40);
            else
                ret = this.IdServico.DescricaoNF;

            return ret;
        }

        public string Justificado(string tmpTxt, int MaxLin, int MargemLeft)
        {
            char[] CRLF = { Convert.ToChar("\r"), Convert.ToChar("\n") };
            StringBuilder Linhas = new StringBuilder();
            string[] Paragrafos = tmpTxt.Split(CRLF);
            StringBuilder Espacos = new StringBuilder();

            for (int i = 0; i < MargemLeft; i++)
                Espacos.Append(" ");

            foreach (string paragrafo in Paragrafos)
            {
                if (paragrafo != String.Empty)
                    foreach (string Lin in SeparaParagrafos(paragrafo, MaxLin))
                    {
                        Linhas.Append(Espacos.ToString() + Lin);
                        Linhas.Append("\r\n");
                    }
            }
            return Linhas.ToString();
        }


        private string[] SeparaParagrafos(string Paragrafo, int MaxLin)
        {
            StringBuilder A = new StringBuilder();

            foreach (string Palavra in Paragrafo.Split(' '))
                if (Palavra != String.Empty)
                {
                    A.Append(Palavra);
                    A.Append(' ');
                }

            Hifenizar Hyph = new Hifenizar(A.ToString().TrimEnd());
            A = new StringBuilder(Hyph.Texto);
            int j = 0;
            int IniLin = 0;
            int LastPos = 0;
            int LastBr = 0;
           
            ArrayList Linhas = new ArrayList();
            
            for (int i = 0; i < A.Length; i++)
            {
                while (Hyph.Separacoes[j] != 0 && i > Hyph.Separacoes[j]) j++;
                if ((i - IniLin) < MaxLin)
                {
                    if (A[i] == ' ') LastBr = i;
                    if ((i != 0) && (i == Hyph.Separacoes[j])) LastPos = Hyph.Separacoes[j++];
                }
                else
                {
                    if (LastBr > LastPos)
                    {
                        Linhas.Add(ColocaEspacos((A.ToString()).Substring(IniLin, LastBr - IniLin), MaxLin));
                        IniLin = LastBr;
                    }
                    else
                    {
                        Linhas.Add(ColocaEspacos((A.ToString()).Substring(IniLin, LastPos - IniLin) + "-", MaxLin));
                        IniLin = LastPos;
                    }
                }
            }
            if (IniLin < A.Length) Linhas.Add((A.ToString().Substring(IniLin).Trim()));

            return ((string[])Linhas.ToArray(typeof(string)));
        }

		private string ColocaEspacos(string tmpLinha,int MaxLin)
		{
			string a=tmpLinha.Trim();
			System.Text.StringBuilder A = new System.Text.StringBuilder();
			int X=MaxLin-a.Length;
			string[] Palavras = a.Split(' ');
			int passo;
			if (X!=0)
			{
				passo=(X/(Palavras.Length==1 ? 1 : Palavras.Length-1)+1);
				foreach (string palavra in Palavras)
				{
					A.Append(palavra);
					for (int j=0;((X>0)&&(j<passo));j++)
					{ A.Append(" "); X--;  }
					A.Append(" "); }
				a = A.ToString().TrimEnd();  }
			return a; 
		}
	}
}
