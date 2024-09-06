using System;
using System.Collections;
using System.Data;
using System.Text;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "CAFornecedorEPI", "IdCAFornecedorEPI")]
    public class CAFornecedorEPI : Ilitera.Data.Table
    {
        private int _IdCAFornecedorEPI;
        private CA _IdCA;
        private FornecedorEPI _IdFornecedorEPI;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CAFornecedorEPI()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CAFornecedorEPI(int Id)
        {
            this.Find(Id);
        }

        public override int Id
        {
            get { return _IdCAFornecedorEPI; }
            set { _IdCAFornecedorEPI = value; }
        }

        public CA IdCA
        {
            get { return _IdCA; }
            set { _IdCA = value; }
        }

        public FornecedorEPI IdFornecedorEPI
        {
            get { return _IdFornecedorEPI; }
            set { _IdFornecedorEPI = value; }
        }
    }

    [Database("opsa", "CompraEPIDetalhe", "IdCompraEPIDetalhe")]
    public class CompraEPIDetalhe : Ilitera.Data.Table
    {
        private int _IdCompraEPIDetalhe;
        private CompraEPI _IdCompraEPI;
        private CA _IdCA;
        private int _Quantidade;
        private double _ValorItem;
        private string _LoteFabricacao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CompraEPIDetalhe()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CompraEPIDetalhe(int Id)
        {
            this.Find(Id);
        }

        public override int Id
        {
            get { return _IdCompraEPIDetalhe; }
            set { _IdCompraEPIDetalhe = value; }
        }

        public CompraEPI IdCompraEPI
        {
            get { return _IdCompraEPI; }
            set { _IdCompraEPI = value; }
        }

        public CA IdCA
        {
            get { return _IdCA; }
            set { _IdCA = value; }
        }

        public int Quantidade
        {
            get { return _Quantidade; }
            set { _Quantidade = value; }
        }

        public double ValorItem
        {
            get { return _ValorItem; }
            set { _ValorItem = value; }
        }

        public string LoteFabricacao
        {
            get { return _LoteFabricacao; }
            set { _LoteFabricacao = value; }
        }
    }

    [Database("opsa", "CompraEPI", "IdCompraEPI")]
    public class CompraEPI : Ilitera.Data.Table
    {
        private int _IdCompraEPI;
        private Cliente _IdCliente;
        private FornecedorEPI _IdFornecedorEPI;
        private string _Documento = string.Empty;
        private DateTime _DataCompra = new DateTime();
        private double _ValorTotal;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CompraEPI()
        {
        }


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CompraEPI(int Id)
        {
            this.Find(Id);
        }

        public override int Id
        {
            get { return _IdCompraEPI; }
            set { _IdCompraEPI = value; }
        }

        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }

        [Obrigatorio(true, "Você deve selecionar o Fornecedor")]
        public FornecedorEPI IdFornecedorEPI
        {
            get { return _IdFornecedorEPI; }
            set { _IdFornecedorEPI = value; }
        }

        [Obrigatorio(true, "Você deve preencher o Documento")]
        public string Documento
        {
            get { return _Documento; }
            set { _Documento = value; }
        }

        public DateTime DataCompra
        {
            get { return _DataCompra; }
            set { _DataCompra = value; }
        }

        public double ValorTotal
        {
            get { return _ValorTotal; }
            set { _ValorTotal = value; }
        }
    }

    [Database("opsa", "FornecedorEPI", "IdFornecedorEPI")]
    public class FornecedorEPI : Ilitera.Common.Juridica
    {
        private int _IdFornecedorEPI;
        private Cliente _IdCliente;

        public FornecedorEPI()
        {
        }

        public FornecedorEPI(int Id)
        {
            this.Find(Id);
        }

        public override int Id
        {
            get { return _IdFornecedorEPI; }
            set { _IdFornecedorEPI = value; }
        }

        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }

        public DataSet GetFornecedor(string IdCliente)
        {
            return this.Get("IdCliente=" + IdCliente + " ORDER BY NomeCompleto");
        }
    }

    [Database("opsa", "TipoEPI", "IdTipoEPI")]
    public class TipoEPI : Ilitera.Data.Table
    {
        private int _IdTipoEPI;
        private string _Nome;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TipoEPI()
        {
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TipoEPI(int Id)
        {
            this.Find(Id);
        }

        public override int Id
        {
            get { return _IdTipoEPI; }
            set { _IdTipoEPI = value; }
        }

        [Obrigatorio(true, "Você deve preencher o Nome do Equipamento")]
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        public DataSet GetAllEquipamentos()
        {
            return this.Get("Nome <> '@' order by Nome desc");
        }

        public DataSet GetEquipamentoByNome(string nome)
        {
            return this.Get("Nome='" + nome + "'");
        }

        public static TipoEPI GetTipoEPI(string nome)
        {
            TipoEPI equipamento = new TipoEPI();
            equipamento.Find("Nome='" + nome + "'");
            return equipamento;
        }
    }
    [Database("opsa", "Fabricante", "IdFabricante")]
    public class Fabricante : Ilitera.Data.Table
    {
        private int _IdFabricante;
        private string _Nome;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Fabricante()
        {

        }


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Fabricante(int Id)
        {
            this.Find(Id);
        }

        public override int Id
        {
            get { return _IdFabricante; }
            set { _IdFabricante = value; }
        }

        [Obrigatorio(true, "Você deve preencher o Nome do Fabricante")]
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        public DataSet GetAllFabricantes()
        {
            return this.Get("Nome <> '@' order by Nome desc " );
        }

        public DataSet GetFabricanteByNome(string nome)
        {
            return this.Get("Nome='" + nome.Replace("'", "''") + "'");
        }

        public static Fabricante GetFabricante(string nome)
        {
            Fabricante fabricante = new Fabricante();
            fabricante.Find("Nome='" + nome.Replace("'", "''") + "'");
            return fabricante;
        }
    }

    [Database("opsa", "CA", "IdCA")]
    public class CA : Ilitera.Data.Table
    {
        private int _IdCA;
        private int _NumeroCA;
        private Fabricante _IdFabricante;
        private TipoEPI _IdTipoEPI;
        private DateTime _DataEmissao = new DateTime();
        private DateTime _Validade = new DateTime();
        private string _DescricaoEPI = string.Empty;
        private string _AprovadoEPI = string.Empty;
        private string _Arquivo = string.Empty;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CA()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public CA(int Id)
        {
            this.Find(Id);
        }

        public override int Id
        {
            get { return _IdCA; }
            set { _IdCA = value; }
        }

        public int NumeroCA
        {
            get { return _NumeroCA; }
            set { _NumeroCA = value; }
        }

        public Fabricante IdFabricante
        {
            get { return _IdFabricante; }
            set { _IdFabricante = value; }
        }

        public TipoEPI IdTipoEPI
        {
            get { return _IdTipoEPI; }
            set { _IdTipoEPI = value; }
        }

        public DateTime DataEmissao
        {
            get { return _DataEmissao; }
            set { _DataEmissao = value; }
        }

        public DateTime Validade
        {
            get { return _Validade; }
            set { _Validade = value; }
        }

        public string DescricaoEPI
        {
            get { return _DescricaoEPI; }
            set { _DescricaoEPI = value; }
        }

        public string AprovadoEPI
        {
            get { return _AprovadoEPI; }
            set { _AprovadoEPI = value; }
        }

        public string Arquivo
        {
            get { return _Arquivo; }
            set { _Arquivo = value; }
        }

        public override int Save()
        {
            if (this.IdFabricante.Id == 0 && this.IdFabricante.Nome != null)
                this.IdFabricante.Save();
            if (this.IdTipoEPI.Id == 0 && this.IdTipoEPI.Nome != null)
                this.IdTipoEPI.Save();
            return base.Save();
        }

        public DataSet GetCACadDispoEstoque(string IdCliente, string IdEPI)
        {
            return this.Get("IdCA IN (SELECT IdCA FROM EPIClienteCA WHERE IdCliente=" + IdCliente
                + " AND IdEPI=" + IdEPI
                + " AND QtdEstoque>0)"
                + " ORDER BY NumeroCA");
        }

        public DataSet GetCAAssociadoEPI(int IdCliente, int IdEPI)
        {
            StringBuilder sqlstm = new StringBuilder();

            sqlstm.Append("IdCA IN (SELECT IdCA FROM EPIClienteCA WHERE IdCliente=" + IdCliente);
            sqlstm.Append(" AND IdEPI=" + IdEPI + ")");
            sqlstm.Append(" ORDER BY NumeroCA");

            return this.Get(sqlstm.ToString());
        }

        public static CA ProcuraCA_MTE(int NumeroCA)
        {
            CA novoCA = new CA();

            string page = new BuscaWebPage("http://www.mte.gov.br/Empregador/segsau/Pesquisa/CA_DET.asp?vNRCAProc=" + NumeroCA.ToString()).GetPage();
            
            StringBuilder message = new StringBuilder();
            
            if (page.IndexOf("não foi encontrado.") != -1)
                throw new CA.NumeroNaoEncontrado(@"O CA de número """ + NumeroCA + @""" não foi encontrado.");
            else
            {
                string numCa = PegaCampo(page, "Nº do CA:</strong></td>\r\n\t\t\t<td valign=\"top\">", "</td>");
                string numProcesso = PegaCampo(page, "Nº do Processo:</strong></td>\r\n\t\t\t<td valign=\"top\">", "</td>");
                string dataEmissaoCAT = PegaCampo(page, "Data de Emisão:</strong></td>\r\n\t\t\t<td valign=\"top\">", "</td>");
                string dataValidade = PegaCampo(page, "Validade:</strong></td>\r\n\t\t\t<td valign=\"top\">", "</td>");
                string equipamento = PegaCampo(page, "Tipo do Equipamento:</strong></td>\r\n\t\t\t \r\n\t\t\t<td colspan=\"3\" valign=\"top\">", "</td>");

                if (equipamento.Equals(string.Empty))
                    equipamento = PegaCampo(page, "Tipo do Equipamento:</strong></td>\r\n\t\t\t\r\n\t\t\t<td colspan=\"3\" valign=\"top\">", "</td>");

                string fabricante = PegaCampo(page, "Fabricante:</strong></td>\r\n\t\t\t\r\n\t\t\t<td align=\"left\" valign=\"top\">", "<br>");

                if (numCa != string.Empty)
                {
                    novoCA.NumeroCA = Convert.ToInt32(numCa);
                }
                else
                {
                    novoCA.NumeroCA = 0;
                }

                novoCA.DataEmissao = Convert.ToDateTime(dataEmissaoCAT);
                novoCA.Validade = Convert.ToDateTime(dataValidade);
                novoCA.DescricaoEPI = PegaCampo(page, "Descrição do Equipamento:</strong></td>\r\n\t\t\t<td colspan=\"3\" valign=\"top\">", "</td>");
                novoCA.AprovadoEPI = PegaCampo(page, "Aprovado:</strong></td>\r\n\t\t\t\r\n\t\t\t<td align=\"left\" valign=\"top\">", "</td>");
                novoCA.IdTipoEPI = TipoEPI.GetTipoEPI(equipamento);
                novoCA.IdFabricante = Fabricante.GetFabricante(fabricante);



                if (novoCA.IdTipoEPI.Id.Equals(0))
                {
                    TipoEPI newEquipamento = new TipoEPI();
                    newEquipamento.Inicialize();
                    newEquipamento.Nome = equipamento;
                    newEquipamento.Save();

                    novoCA.IdTipoEPI = newEquipamento;
                }

                if (novoCA.IdFabricante.Id.Equals(0))
                {
                    Fabricante newFabricante = new Fabricante();
                    newFabricante.Inicialize();
                    newFabricante.Nome = fabricante;
                    newFabricante.Save();

                    novoCA.IdFabricante = newFabricante;
                }
            }
            return novoCA;
        }

        private static string PegaCampo(string page, string tagInicio, string tagTermino)
        {
            int posicaoInicialTag = page.IndexOf(tagInicio) + tagInicio.Length;

            int posicaoFinalTag = page.IndexOf(tagTermino, posicaoInicialTag);
            
            if (posicaoInicialTag < 2560)
                return string.Empty;
            else
                return page.Substring(posicaoInicialTag, posicaoFinalTag - posicaoInicialTag).Trim();
        }

        public class NumeroNaoEncontrado : Exception
        {
            public NumeroNaoEncontrado(string Message)
                : base(Message)
            {

            }
        }
    }
    
    [Database("opsa", "EPICAEntregaDetalhe", "IdEPICAEntregaDetalhe")]
    public class EPICAEntregaDetalhe : Ilitera.Data.Table
    {
        private int _IdEPICAEntregaDetalhe;
        private EPICAEntrega _IdEPICAEntrega;
        private EPIClienteCA _IdEPIClienteCA;
        private int _QtdEntregue;
        private string _Origem;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EPICAEntregaDetalhe()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EPICAEntregaDetalhe(int Id)
        {
            this.Find(Id);
        }

        public override int Id
        {
            get { return _IdEPICAEntregaDetalhe; }
            set { _IdEPICAEntregaDetalhe = value; }
        }

        public EPICAEntrega IdEPICAEntrega
        {
            get { return _IdEPICAEntrega; }
            set { _IdEPICAEntrega = value; }
        }

        public EPIClienteCA IdEPIClienteCA
        {
            get { return _IdEPIClienteCA; }
            set { _IdEPIClienteCA = value; }
        }

        public int QtdEntregue
        {
            get { return _QtdEntregue; }
            set { _QtdEntregue = value; }
        }

        public string Origem
        {
            get { return _Origem; }
            set { _Origem = value; }
        }

    }

    [Database("opsa", "EPICAEntrega", "IdEPICAEntrega")]
    public class EPICAEntrega : Ilitera.Data.Table
    {
        private int _IdEPICAEntrega;
        private Empregado _IdEmpregado;
        private DateTime _DataRecebimento = new DateTime();

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EPICAEntrega()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EPICAEntrega(int Id)
        {
            this.Find(Id);
        }

        public override int Id
        {
            get { return _IdEPICAEntrega; }
            set { _IdEPICAEntrega = value; }
        }

        public Empregado IdEmpregado
        {
            get { return _IdEmpregado; }
            set { _IdEmpregado = value; }
        }

        public DateTime DataRecebimento
        {
            get { return _DataRecebimento; }
            set { _DataRecebimento = value; }
        }
    }

    [Database("opsa", "EPIClienteCA", "IdEPIClienteCA")]
    public class EPIClienteCA : Ilitera.Data.Table
    {
        private int _IdEPIClienteCA;
        private Epi _IdEPI;
        private int _QtdEstoque;
        private int _QtdEstoqueMin;
        private int _QtdEstoqueMax;
        private Cliente _IdCliente;
        private CA _IdCA;
        private int _NumPeriodicidade;
        private int _Periodicidade;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EPIClienteCA()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EPIClienteCA(int Id)
        {
            this.Find(Id);
        }

        public override int Id
        {
            get { return _IdEPIClienteCA; }
            set { _IdEPIClienteCA = value; }
        }

        [Obrigatorio(true, "Você deve selecionar um EPI!")]
        public Epi IdEPI
        {
            get { return _IdEPI; }
            set { _IdEPI = value; }
        }

        public int QtdEstoque
        {
            get { return _QtdEstoque; }
            set { _QtdEstoque = value; }
        }

        public int QtdEstoqueMin
        {
            get { return _QtdEstoqueMin; }
            set { _QtdEstoqueMin = value; }
        }

        public int QtdEstoqueMax
        {
            get { return _QtdEstoqueMax; }
            set { _QtdEstoqueMax = value; }
        }

        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }

        public int NumPeriodicidade
        {
            get { return _NumPeriodicidade; }
            set { _NumPeriodicidade = value; }
        }

        public int Periodicidade
        {
            get { return _Periodicidade; }
            set { _Periodicidade = value; }
        }

        [Obrigatorio(true, "Você deve selecionar um CA!")]
        public CA IdCA
        {
            get { return _IdCA; }
            set { _IdCA = value; }
        }

        public DataSet GetEPIClienteCA(int IdEPI, int IdCliente)
        {
            DataSet ds = new DataSet();

            StringBuilder st = new StringBuilder();

            st.Append("USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " SELECT		EPIClienteCA.IdEPIClienteCA, ");
            st.Append("						EPIClienteCA.QtdEstoque, ");
            st.Append("						CA.NumeroCA, ");
            st.Append("						CA.IdCA, ");
            st.Append("						TipoEPI.Nome ");
            st.Append("FROM					EPIClienteCA INNER JOIN CA ");
            st.Append("ON					EPIClienteCA.IdCA = CA.IdCA INNER JOIN TipoEPI ");
            st.Append("ON					CA.IdTipoEPI = TipoEPI.IdTipoEPI ");
            st.Append("WHERE				(EPIClienteCA.IdEPI = " + IdEPI + ") AND (EPIClienteCA.IdCliente = " + IdCliente + ") ");
            st.Append("ORDER BY				CA.NumeroCA");

            ds = this.ExecuteDataset(st.ToString());

            return ds;
        }

        public DataSet GetEPIClienteCAExistente(string IdCliente, string IdCA)
        {
            return this.Get("IdCliente=" + IdCliente + " AND IdCA=" + IdCA);
        }

        public ArrayList FindEPIClienteCAExistente(string IdCliente, string IdCA)
        {
            return this.Find("IdCliente=" + IdCliente + " AND IdCA=" + IdCA);
        }

        public DataSet GetEPIClienteCAExistente(string IdEPI, string IdCliente, string IdCA)
        {
            return this.Get("IdEPI=" + IdEPI + " AND IdCliente=" + IdCliente + " AND IdCA=" + IdCA + "");
        }

        public DataSet GetEPIemUtilizacaoEmpregado(int IdEmpregado)
        {
            DataSet ds = new DataSet();

            StringBuilder st = new StringBuilder();

            st.Append("USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " SELECT		Epi.IdEPI, ");
            st.Append("						Epi.Nome, ");
            st.Append("						EPICAEntregaDetalhe.QtdEntregue, ");
            st.Append("						EPIClienteCA.NumPeriodicidade, ");
            st.Append("						EPIClienteCA.Periodicidade, ");
            st.Append("						EPICAEntrega.DataRecebimento ");
            st.Append("FROM					EPIClienteCA ");
            st.Append("INNER JOIN			Epi ");
            st.Append("ON					EPIClienteCA.IdEPI = Epi.IdEPI ");
            st.Append("INNER JOIN			EPICAEntregaDetalhe ");
            st.Append("ON					EPIClienteCA.IdEPIClienteCA = EPICAEntregaDetalhe.IdEPIClienteCA ");
            st.Append("INNER JOIN			EPICAEntrega ");
            st.Append("ON					EPICAEntregaDetalhe.IdEPICAEntrega = EPICAEntrega.IdEPICAEntrega ");
            st.Append("WHERE				EPICAEntrega.IdEmpregado=" + IdEmpregado + " ");
            st.Append("ORDER BY				Epi.Nome");

            ds = this.ExecuteDataset(st.ToString());

            return ds;
        }

        public DataSet GetEPIemUtilizacaoTodos(int IdCliente)
        {
            DataSet ds = new DataSet();

            StringBuilder st = new StringBuilder();

            st.Append("USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " SELECT		Epi.IdEPI, ");
            st.Append("						Epi.Nome, ");
            st.Append("						EPICAEntregaDetalhe.QtdEntregue, ");
            st.Append("						EPIClienteCA.NumPeriodicidade, ");
            st.Append("						EPIClienteCA.Periodicidade, ");
            st.Append("						EPICAEntrega.DataRecebimento ");
            st.Append("FROM					EPIClienteCA ");
            st.Append("INNER JOIN			Epi ");
            st.Append("ON					EPIClienteCA.IdEPI = Epi.IdEPI ");
            st.Append("INNER JOIN			EPICAEntregaDetalhe ");
            st.Append("ON					EPIClienteCA.IdEPIClienteCA = EPICAEntregaDetalhe.IdEPIClienteCA ");
            st.Append("INNER JOIN			EPICAEntrega ");
            st.Append("ON					EPICAEntregaDetalhe.IdEPICAEntrega = EPICAEntrega.IdEPICAEntrega ");
            st.Append("WHERE				EPIClienteCA.IdCliente=" + IdCliente);
            st.Append("ORDER BY				Epi.Nome");

            ds = this.ExecuteDataset(st.ToString());

            return ds;
        }

        public DataSet GetDetalheEPIemUtilizacao(int IdEPI, int IdCliente, int IdEmpregado, string tipo)
        {
            DataSet ds = new DataSet();
            StringBuilder st = new StringBuilder();

            st.Append("USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " SELECT		CA.NumeroCA, ");
            st.Append("						TipoEPI.Nome As NomeE, ");
            st.Append("						EPICAEntregaDetalhe.QtdEntregue, ");
            st.Append("						EPICAEntrega.DataRecebimento, ");
            st.Append("						EPIClienteCA.NumPeriodicidade, ");
            st.Append("						EPIClienteCA.Periodicidade, ");
            st.Append("						Fabricante.Nome AS NomeF ");
            st.Append("FROM					CA ");
            st.Append("INNER JOIN			EPIClienteCA ");
            st.Append("ON					CA.IdCA = EPIClienteCA.IdCA ");
            st.Append("INNER JOIN			TipoEPI ");
            st.Append("ON					CA.IdTipoEPI = TipoEPI.IdTipoEPI ");
            st.Append("LEFT JOIN			EPICAEntregaDetalhe ");
            st.Append("ON					EPIClienteCA.IdEPIClienteCA = EPICAEntregaDetalhe.IdEPIClienteCA ");
            st.Append("LEFT JOIN			EPICAEntrega ");
            st.Append("ON					EPICAEntregaDetalhe.IdEPICAEntrega = EPICAEntrega.IdEPICAEntrega ");
            st.Append("INNER JOIN           Fabricante ");
            st.Append("ON					CA.IdFabricante = Fabricante.IdFabricante ");

            if (tipo == "Cliente")
                st.Append("WHERE			(EPIClienteCA.IdCliente = " + IdCliente + ") ");
            else if (tipo == "Empregado")
                st.Append("WHERE			(EPICAEntrega.IdEmpregado = " + IdEmpregado + ") ");

            st.Append("AND					(EPIClienteCA.IdEPI = " + IdEPI + ") ");
            st.Append("ORDER BY				TipoEPI.Nome, CA.NumeroCA");

            ds = this.ExecuteDataset(st.ToString());

            return ds;
        }

        public DataSet GetEPIemUtilizacao(int IdEmpregado)
        {
            StringBuilder st = new StringBuilder();

            st.Append(" " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEPI.nID_EPI ");
            st.Append("IN				(SELECT  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.EPIModeloEspecifico.IdEPI ");
            st.Append("FROM		         " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.EPIModeloEspecifico INNER JOIN  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.EPIEspecificoEmpregado ");
            st.Append("ON				 " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.EPIModeloEspecifico.IdEPIModeloEspecifico =  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.EPIEspecificoEmpregado.IdEPIModeloEspecifico ");
            st.Append("WHERE		    ( " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.EPIEspecificoEmpregado.IdEmpregado=" + IdEmpregado + ") ");
            st.Append("GROUP BY  " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.EPIModeloEspecifico.IdEPI) ORDER BY tNO_EPI");

            return new Epi().Get(st.ToString());
        }

        public DataSet GetLastEPIToEmpregado(int IdEPI)
        {
            StringBuilder st = new StringBuilder();

            st.Append("USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " SELECT		EPIModeloEspecifico.NumPeriodicidade, ");
            st.Append("						EPIModeloEspecifico.Periodicidade, ");
            st.Append("						EPIEspecificoEmpregado.DataRecebimento ");
            st.Append("FROM					EPIModeloEspecifico INNER JOIN EPIEspecificoEmpregado ");
            st.Append("ON					EPIModeloEspecifico.IdEPIModeloEspecifico = EPIEspecificoEmpregado.IdEPIModeloEspecifico ");
            st.Append("WHERE				(EPIModeloEspecifico.IdEPI = " + IdEPI + ") ");
            st.Append("AND					(EPIEspecificoEmpregado.DataRecebimento = ");
            st.Append("						(SELECT			MAX(EPIEspecificoEmpregado.DataRecebimento) ");
            st.Append("                     FROM			EPIModeloEspecifico INNER JOIN EPIEspecificoEmpregado ");
            st.Append("						ON EPIModeloEspecifico.IdEPIModeloEspecifico = EPIEspecificoEmpregado.IdEPIModeloEspecifico ");
            st.Append("                     WHERE			(EPIModeloEspecifico.IdEPI = " + IdEPI + ")))");

            return this.ExecuteDataset(st.ToString());
        }

        public string GetPeriodicidade()
        {
            this.Find();
            string sPeriodicidade = string.Empty;
            sPeriodicidade = this.NumPeriodicidade.ToString();
            switch (this.Periodicidade)
            {
                case 0:
                    if (this.NumPeriodicidade == 1)
                        sPeriodicidade = sPeriodicidade + " dia";
                    else
                        sPeriodicidade = sPeriodicidade + " dias";
                    break;
                case 1:
                    if (this.NumPeriodicidade == 1)
                        sPeriodicidade = sPeriodicidade + " mês";
                    else
                        sPeriodicidade = sPeriodicidade + " meses";
                    break;
                case 2:
                    if (this.NumPeriodicidade == 1)
                        sPeriodicidade = sPeriodicidade + " ano";
                    else
                        sPeriodicidade = sPeriodicidade + " anos";
                    break;
            }
            return sPeriodicidade;
        }
    }

    public enum TipoPeriodicidade : int
    {
        Dia,
        Mês,
        Ano
    }
}
