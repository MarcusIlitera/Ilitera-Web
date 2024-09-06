using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Data;
using System.Drawing;
using Ilitera.Data;
using Ilitera.Common;
using System.Text;

namespace Ilitera.Opsa.Data
{
	[Database("opsa", "Funcao", "IdFuncao", "", "Função")]
	public class Funcao : Ilitera.Data.Table
	{
		private int _IdFuncao;
		private Cliente _IdCliente;
		private string _CodigoFuncao   = string.Empty;
		private string _NomeFuncao = string.Empty;
		private string _DescricaoFuncao = string.Empty;
		private string _RequisitoFuncao = string.Empty;
		private string _NumeroCBO = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Funcao()
		{

		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Funcao(int Id)
		{
			this.Find(Id);
		}
		public override int Id
		{														  
			get	{return _IdFuncao;}
			set {_IdFuncao = value;}
		}	
		public Cliente IdCliente
		{														  
			get	{return _IdCliente;}
			set {_IdCliente = value;}
		}
		public string CodigoFuncao
		{														  
			get	{return _CodigoFuncao;}
			set {_CodigoFuncao = value;}
		}
		[Obrigatorio(true, "O nome da Função é obrigatório!")]
		public string NomeFuncao
		{														  
			get	{return _NomeFuncao;}
			set {_NomeFuncao = value;}
		}
        public string DescricaoFuncao
        {
            get { return _DescricaoFuncao; }
            set
            {
                //if (value.Length > 1000)
                //    throw new Exception("Limite de 1000 caracteres ultrapassados! Total:" + value.Length);

                _DescricaoFuncao = value;
            }
        }
		public string RequisitoFuncao
		{														  
			get	{return _RequisitoFuncao;}
			set {_RequisitoFuncao = value;}
		}
		public string NumeroCBO
		{														  
			get	{return _NumeroCBO;}
			set {_NumeroCBO = value;}
		}
		public override string ToString()
		{
            if (this.mirrorOld == null)
                this.Find();

			return this.NomeFuncao;
		}

        public string GetCodigoNomeFuncao()
		{
            if (this.CodigoFuncao != string.Empty)
                return this.CodigoFuncao + " - " +this.NomeFuncao;
            else
                return this.NomeFuncao;
		}

		public override void Delete()
		{
            int count = new EmpregadoFuncao().ExecuteCount("nID_FUNCAO=" + this.Id);

            if (count > 0)
				throw new Exception("Esta função não pode ser excluída, por estar associada a classificação funcional do empregado.");
			
			base.Delete ();
		}

		public string GetDefaultRequisitoFuncao()
		{
			System.Text.StringBuilder str = new System.Text.StringBuilder();
			str.Append("1) Capacitação profissional com a escolaridade adequada à função;");
			str.Append('\n');
			str.Append("2) Competência legal para o exercício da profissão em ");
			str.Append("conformidade com a regulamentação profissional vigente;");
			str.Append('\n');
			str.Append("3) Aptidão em saúde ocupacional atestada por exame ");
			str.Append("médico com avaliação clínica, abrangendo anamnese ocupacional, ");
			str.Append("exame físico e mental, e ainda, se necessário, exames complementares;");
			str.Append('\n');
			str.Append("4) Conhecimento dos riscos inerentes ao trabalho a executar e ");
			str.Append("das precauções a tomar no sentido de evitar acidentes ou doenças do trabalho;");
			str.Append('\n');
			str.Append("5) Assinatura de termo de compromisso expressando a vontade firme e ");
			str.Append("deliberada em observar as normas de segurança e saúde do trabalho e ");
			str.Append("utilizar os equipamentos de proteção individual fornecidos pela empresa;");
			return str.ToString();
		}

        public static bool IsCBO(string numCBO)
        {
            string padrao = @"\d{4}-\d{2}";

            Regex re = new Regex(padrao);

            return re.IsMatch(numCBO);
        }

        public static string FormatarCBO(string numCBO)
        {
            string ret;

            if (IsCBO(numCBO))
                ret = numCBO;
            else if (numCBO.Length == 6)
            {
                string padrao = @"^(\d{4})(\d{2})";

                string cbo = Utility.RemoveCaracteresEspeciais(numCBO);

                Regex re = new Regex(padrao);

                Match m = re.Match(cbo);

                if (m.Success)
                    ret = m.Groups[1].Value + "-" + m.Groups[2].Value;
                else
                    throw new Exception("CBO Inválido!");
            }
            else
                ret = numCBO;

            return ret;
        }
	}
}
