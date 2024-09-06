using System;
using System.Collections;
using System.Data;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Opsa.Report
{
	public class DataSourceFaxAvisoAgendamento
	{
        private PedidoGrupo pedidoGrupo;
        private Juridica juridica;
        private string AC = string.Empty;
        private string Texto = string.Empty;

        public DataSourceFaxAvisoAgendamento(PedidoGrupo pedidoGrupo, string Texto)
        {
            this.pedidoGrupo = pedidoGrupo;
            this.juridica = pedidoGrupo.IdCliente;
            this.Texto = Texto;
        }

        public DataSourceFaxAvisoAgendamento(PedidoGrupo pedidoGrupo)
		{
            this.pedidoGrupo = pedidoGrupo;
            this.juridica = pedidoGrupo.IdCliente;
		}

        public DataSourceFaxAvisoAgendamento(Juridica juridica, string AC)
        {
            this.juridica = juridica;
            this.AC = AC;
        }

		public RptFaxAvisoAgendamento GetReport()
		{
            RptFaxAvisoAgendamento report = new RptFaxAvisoAgendamento();
            report.SetDataSource(GetDataSource());
			report.Refresh();			
			return report;
		}

        public RptFaxPagInicial GetReportFaxPagInicial()
        {
            RptFaxPagInicial report = new RptFaxPagInicial();
            report.SetDataSource(GetDataSourceFaxPagInicial());
            report.Refresh();
            return report;
        }

        private DataSet GetDataSource()
		{
			DataSet ds = new DataSet();
            ds.Tables.Add(GetTable());

            Prestador prestador = GetPrestador();
            
            Empresa empresa = new Empresa();
            empresa.Find(prestador.IdJuridica.Id);

            if (juridica.mirrorOld == null)
                juridica.Find();

			DataRow newRow = ds.Tables[0].NewRow();

            newRow["DADOS_EMPRESA"] = empresa.GetEndereco().GetEnderecoCompletoPorLinhaSemCep() 
                                        +"\r\n" + "Telefones: " + empresa.GetContatoTelefonico().GetDDDTelefone()
                                        +"\r\n" + "Fax: " + empresa.GetFax().GetDDDTelefone() ;


            newRow["AVISO"] = pedidoGrupo.GetObrigacoes();
            newRow["PRESTADOR"] = pedidoGrupo.IdPrestador.NomeCompleto;
            newRow["PARA"] = juridica.NomeCompleto;
            newRow["AC"] = pedidoGrupo.Solicitante;
            newRow["DATA"] = DateTime.Today.ToString("dd-MM-yyyy");
            newRow["FONE"] = juridica.GetContatoTelefonico().GetDDDTelefone();
            newRow["FAX"] = juridica.GetFax().GetDDDTelefone();
            newRow["TEXTO"] = this.Texto;
            newRow["ASSINATURA"] = prestador.NomeCompleto + "\r\n" + prestador.Titulo;

			ds.Tables[0].Rows.Add(newRow);

			return ds;
		}

        private DataSet GetDataSourceFaxPagInicial()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(GetTable());

            Prestador prestador = GetPrestador();

            Empresa empresa = new Empresa();
            empresa.Find(prestador.IdJuridica.Id);

            if (juridica.mirrorOld == null)
                juridica.Find();

            DataRow newRow = ds.Tables[0].NewRow();

            newRow["DADOS_EMPRESA"] = empresa.GetEndereco().GetEnderecoCompletoPorLinhaSemCep()
                                        + "\r\n" + "Telefones: " + empresa.GetContatoTelefonico().GetDDDTelefone()
                                        + "\r\n" + "Fax: " + empresa.GetFax().GetDDDTelefone();


            newRow["AVISO"] = string.Empty;
            newRow["PRESTADOR"] = string.Empty;
            newRow["PARA"] = juridica.NomeCompleto;
            newRow["AC"] = this.AC;
            newRow["DATA"] = DateTime.Today.ToString("dd-MM-yyyy");
            newRow["FONE"] = juridica.GetContatoTelefonico().GetDDDTelefone();
            newRow["FAX"] = juridica.GetFax().GetDDDTelefone();
            newRow["TEXTO"] = this.Texto;
            newRow["ASSINATURA"] = prestador.NomeCompleto + "\r\n" + prestador.Titulo;

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private static Prestador GetPrestador()
        {
            Prestador prestador = new Prestador();
            Usuario usuario = Usuario.Login();
            prestador.Find("IdPessoa=" + usuario.IdPessoa.Id);
            return prestador;
        }

        public static DataTable GetTable()
        {
            DataTable table = new DataTable("Result");
            table.Columns.Add("DADOS_EMPRESA", Type.GetType("System.String"));
            table.Columns.Add("AVISO", Type.GetType("System.String"));
            table.Columns.Add("PRESTADOR", Type.GetType("System.String"));
            table.Columns.Add("PARA", Type.GetType("System.String"));
            table.Columns.Add("AC", Type.GetType("System.String"));
            table.Columns.Add("DATA", Type.GetType("System.String"));
            table.Columns.Add("FONE", Type.GetType("System.String"));
            table.Columns.Add("FAX", Type.GetType("System.String"));
            table.Columns.Add("TEXTO", Type.GetType("System.String"));
            table.Columns.Add("AGENDAMENTO_DATA", Type.GetType("System.String"));
            table.Columns.Add("AGENDAMENTO_HORA", Type.GetType("System.String"));
            table.Columns.Add("ASSINATURA", Type.GetType("System.String"));
            table.Columns.Add("NUMERO_PAGINAS", Type.GetType("System.String"));
            table.Columns.Add("COM_AGENDAMENTO", Type.GetType("System.Boolean"));
            return table;
        }
	}
}
