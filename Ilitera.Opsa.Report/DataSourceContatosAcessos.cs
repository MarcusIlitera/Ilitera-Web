using System;
using System.Collections;
using System.Data;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Opsa.Report
{
	public class DataSourceContatosAcessos
	{
		public DataSourceContatosAcessos()
		{
            //Verifica se o usuário tem permissão para o objeto
            Ilitera.Common.Usuario.Permissao(this.GetType());
		}

		public RptContatosAcessos GetReport(DataSet dsEmpresas)
		{
			RptContatosAcessos report = new RptContatosAcessos();
			report.SetDataSource(GetListaEmpresa(dsEmpresas));
			report.Refresh();			
			return report;
		}

        public ReportAcessos GetReportAcessos()
        {

            ReportAcessos report = new ReportAcessos();
            report.Load();
            report.Refresh();
            report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
                                    Ilitera.Data.SQLServer.EntitySQLServer.Password,
                                    Ilitera.Data.SQLServer.EntitySQLServer.Server,
                                    Ilitera.Data.SQLServer.EntitySQLServer.Database);

            return report;
        }

        public ReportNaoAcessos GetReportNaoAcessos()
        {

            ReportNaoAcessos report = new ReportNaoAcessos();
            report.Load();
            report.Refresh();
            report.SetDatabaseLogon(Ilitera.Data.SQLServer.EntitySQLServer.User,
                                    Ilitera.Data.SQLServer.EntitySQLServer.Password,
                                    Ilitera.Data.SQLServer.EntitySQLServer.Server,
                                    Ilitera.Data.SQLServer.EntitySQLServer.Database);

            return report;
        }

		private DataSet GetListaEmpresa(DataSet dsEmpresas)
		{
			Juridica juridica;			
			DataSet ds = new DataSet();
			DataRow newRow;	

			DataTable table = new DataTable("Result");		
			table.Columns.Add("Empresa", Type.GetType("System.String"));
			table.Columns.Add("Colaborador", Type.GetType("System.String"));
			table.Columns.Add("UltimoLogin", Type.GetType("System.String"));
			table.Columns.Add("Acessos", Type.GetType("System.String"));
			ds.Tables.Add(table);

			foreach(DataRow row in dsEmpresas.Tables[0].Rows)
			{
				juridica = new Juridica();

				juridica.Find(Convert.ToInt32(row["Id"]));	
				ArrayList prestadores = new Prestador().Find(
					"IdJuridica="+juridica.Id
//					+"(IdJuridica="+juridica.Id +" OR IdPrestador IN (SELECT IdPrestador FROM PrestadorCliente WHERE IdCliente="+juridica.Id+"))"
					+" AND IsInativo=0");
				
				if(prestadores.Count==0)
				{
					newRow = ds.Tables[0].NewRow();	
					newRow["Empresa"] = juridica.NomeAbreviado;
					newRow["Colaborador"]	= "-";
					newRow["UltimoLogin"]	= "Sem Login";
					newRow["Acessos"]		= string.Empty;
					ds.Tables[0].Rows.Add(newRow);
				}
				else
				{
					foreach(Prestador prestador in prestadores)
					{
						Usuario usuario = new Usuario();
						usuario.Find("IdPessoa="+prestador.IdPessoa.Id);

						string ultimoLogin=string.Empty;;
						string acessos=string.Empty;
						if(usuario.Id==0)
						{
							ultimoLogin = "Sem Login";
						}
						else
						{
							ArrayList listAcessos = new HistoricoLogin().Find("IdUsuario="+usuario.Id);
							ultimoLogin = usuario.DatUltLogin==new DateTime()?"-":usuario.DatUltLogin.ToString("dd-MM-yyyy hh:mm");
							acessos = listAcessos.Count==0?string.Empty:listAcessos.Count.ToString();
						}
						newRow = ds.Tables[0].NewRow();	
						newRow["Empresa"]		= juridica.NomeAbreviado;
						newRow["Colaborador"]	= prestador.NomeAbreviado;
						newRow["UltimoLogin"]	= ultimoLogin;
						newRow["Acessos"]		= acessos;
						ds.Tables[0].Rows.Add(newRow);
					}
				}
			}
			return ds;
		}
	}
}
