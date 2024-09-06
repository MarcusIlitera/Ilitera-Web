using System;
using System.Collections;
using System.Data;
using System.Text;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	public enum EventoBase : int 
	{
		ComissaoEleitoral=1,
		Edital, 
		Publicacao, 
		ComunicacaoSindicato, 
		InicioInscricao, 
		TerminoInscricao, 
		ListaDosCandidatos, 
		CedulaVotacao, 
		Eleicao, 
		Posse, 
		Calendario,
		RegistroDRT,
		Reuniao1,
		Reuniao2,
		Reuniao3,
		Reuniao4,
		Reuniao5,
		Reuniao6,
		Reuniao7,
		Reuniao8,
		Reuniao9,
		Reuniao10,
		Reuniao11,
		Reuniao12,
		ReuniaoExtraordinaria,
		TerminoMandato,
		TodasReunioes
	}

	[Database("opsa","EventoBaseCipa","IdEventoBaseCipa")]
	public class EventoBaseCipa : Ilitera.Data.Table 
	{
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EventoBaseCipa()
		{

		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EventoBaseCipa(EventoBase eventoBase)
		{
			this.Find((int)eventoBase);
		}
		private	int _IdEventoBaseCipa;
		private	string _Descricao;

		public override int Id
		{
			get{return _IdEventoBaseCipa;}
			set{_IdEventoBaseCipa = value;}
		}
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}

		public override string ToString()
		{
			return this.Descricao;
		}

        public static Obrigacao GetObrigacao(EventoBase eventoBase)
        {
            Obrigacao obrigacao = new Obrigacao();
            obrigacao.Find("IdEventoBaseCipa=" + (int)eventoBase);

            return obrigacao;
        }
		
		public DataSet DataSourceEventoBaseCipa()
		{
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("Id", Type.GetType("System.Int32"));
			table.Columns.Add("Eventos Cipa", Type.GetType("System.String"));
			ds.Tables.Add(table);
			DataRow newRow;
			ArrayList list = new EventoBaseCipa().FindAll();
			for(int i = 0; i < list.Count; i++)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["Id"] = ((EventoBaseCipa)list[i]).Id;
				newRow["Eventos Cipa"] = ((EventoBaseCipa)list[i]).Descricao;
				ds.Tables[0].Rows.Add(newRow);
			}
			return ds;
		}
	}

}
