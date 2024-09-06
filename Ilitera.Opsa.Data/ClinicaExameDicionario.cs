using System;
using System.Data;
using System.Collections;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
	[Database("opsa", "ClinicaExameDicionario", "IdClinicaExameDicionario")]
	public class ClinicaExameDicionario : Ilitera.Data.Table
	{
		private int	_IdClinicaExameDicionario;	
		private Clinica	_IdClinica;
		private ExameDicionario	_IdExameDicionario;
		private double _ValorPadrao;
        private string _Preparo;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ClinicaExameDicionario()
		{

		}
		public override int Id
		{														  
			get{return _IdClinicaExameDicionario;}
			set	{_IdClinicaExameDicionario = value;}
		}		
		public Clinica IdClinica
		{														  
			get{return _IdClinica;}
			set	{_IdClinica = value;}
		}		
		public ExameDicionario IdExameDicionario
		{														  
			get{return _IdExameDicionario;}
			set	{_IdExameDicionario = value;}
		}				
		public double ValorPadrao
		{														  
			get{return _ValorPadrao;}
			set	{_ValorPadrao = value;}
		}
        public string Preparo
        {
            get { return _Preparo; }
            set { _Preparo = value; }
        }		

		public override string ToString()
		{
			this.IdExameDicionario.Find();
			return this.IdExameDicionario.Nome;
		}

        public override void Delete()
        {
            new ClinicaClienteExameDicionario().Delete("IdClinicaExameDicionario=" + this.Id);

            base.Delete();
        }

		public DataSet ListaTipoExames(Clinica clinica)
		{	
			DataSet ds = new DataSet();
			DataTable table = new DataTable("Result");
			table.Columns.Add("Id", Type.GetType("System.String"));
			table.Columns.Add("Nome", Type.GetType("System.String"));
			table.Columns.Add("ValorPadrao", Type.GetType("System.String"));					
			ds.Tables.Add(table);
			DataRow newRow;
			ArrayList list  = this.Find("IdClinica=" + clinica.Id + " ORDER BY ValorPadrao");		
			for(int i = 0; i< list.Count; i++)
			{
				newRow = ds.Tables[0].NewRow();
				newRow["Id"] = ((ClinicaExameDicionario)list[i]).Id;
				((ClinicaExameDicionario)list[i]).IdExameDicionario.Find();
				newRow["Nome"] = ((ClinicaExameDicionario)list[i]).IdExameDicionario.Nome;
				newRow["ValorPadrao"]	= ((ClinicaExameDicionario)list[i]).ValorPadrao;						
				ds.Tables[0].Rows.Add(newRow);
			}
			return ds;
		}
	}
}