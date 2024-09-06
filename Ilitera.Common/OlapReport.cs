//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Xml;
//using System.Data;
//using System.Data.Sql;
//using System.Data.SqlClient;
//using System.Data.SqlTypes;

//using Ilitera.Data;

//namespace Ilitera.Common
//{
//    [Database("opsa", "OlapReport", "IdOlapReport")]
//    public class OlapReport: Ilitera.Data.Table
//    {
//        private int _IdOlapReport;
//        private string _Descricao = string.Empty;
//        private string _ConnectionString = string.Empty;
//        private string _ChartTemplate = string.Empty;

//        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

//        public OlapReport()
//        {

//        }
//        public override int Id
//        {
//            get { return _IdOlapReport; }
//            set { _IdOlapReport = value; }
//        }
//        public string Descricao
//        {
//            get { return _Descricao; }
//            set { _Descricao = value; }
//        }
//        public string ConnectionString
//        {
//            get { return _ConnectionString; }
//            set { _ConnectionString = value; }
//        }
//        public string ChartTemplate
//        {
//            get { return _ChartTemplate; }
//            set { _ChartTemplate = value; }
//        }

//        #region SaveXML

//        public void SaveXML(System.IO.Stream xmlStream)
//        {
//            SqlXml sqlXml = new SqlXml(xmlStream);

//            XmlReader reader = sqlXml.CreateReader();

//            if (sqlXml.IsNull)
//                return;

//            //Get the connection string from the web.config file
//            string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();
            
//            using (SqlConnection conn = new SqlConnection(connString))
//            {
//                conn.Open();

//                SqlCommand cmd = conn.CreateCommand();
//                cmd.CommandText =   "UPDATE OlapReport"
//                                 + " SET ReportTemplate = @SecondCol"
//                                 + " WHERE IdOlapReport=@FirstCol";

//                //Set value of parameters
//                SqlParameter firstColParameter = cmd.Parameters.Add("@FirstCol", SqlDbType.Int);
//                firstColParameter.Value = this.Id;

//                SqlParameter secondColParameter = cmd.Parameters.Add("@SecondCol", SqlDbType.Xml);
//                secondColParameter.Value = sqlXml;

//                //Execute update and close connection
//                cmd.ExecuteNonQuery();
//            }
//        }
//        #endregion

//        #region LoadXML

//        public System.IO.Stream LoadXML()
//        {
//            //Get the connection string from the web.config file
//            string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

//            using (SqlConnection conn = new SqlConnection(connString))
//            {
//                conn.Open();
//                SqlCommand command = conn.CreateCommand();
//                command.CommandText = "SELECT ReportTemplate FROM OlapReport"
//                                      + " WHERE IdOlapReport = " + this.Id.ToString();

//                SqlDataReader reader = command.ExecuteReader();

//                System.IO.MemoryStream m_Stream = new System.IO.MemoryStream();

//                if (reader.Read())
//                {
//                    SqlXml sqlXml = reader.GetSqlXml(0);

//                    if (sqlXml.IsNull)
//                        return m_Stream;

//                    byte[] buffer = Encoding.ASCII.GetBytes(sqlXml.Value.ToString());

//                    m_Stream.Write(buffer, 0, buffer.Length);
//                }

//                return m_Stream;
//            }
//        }
//        #endregion

//        #region LoadXML2

//        public string LoadXML2()
//        {
//            //Get the connection string from the web.config file
//            string connString = Ilitera.Data.SQLServer.EntitySQLServer.GetConnection();

//            using (SqlConnection conn = new SqlConnection(connString))
//            {
//                conn.Open();
//                SqlCommand command = conn.CreateCommand();
//                command.CommandText = "SELECT ReportTemplate FROM OlapReport"
//                                      + " WHERE IdOlapReport = " + this.Id.ToString();

//                SqlDataReader reader = command.ExecuteReader();

//                string Filename = System.IO.Path.Combine(System.IO.Path.GetTempPath(), 
//                                                         Guid.NewGuid() + ".xml");

//                System.IO.FileStream fs = new System.IO.FileStream(Filename, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);

//                fs.Position = 0;

//                if (reader.Read())
//                {
//                    SqlXml sqlXml = reader.GetSqlXml(0);

//                    if (sqlXml.IsNull)
//                        return string.Empty;

//                    byte[] buffer = Encoding.ASCII.GetBytes(sqlXml.Value.ToString());

//                    fs.Write(buffer, 0, buffer.Length);

//                    fs.Close();
//                }

//                return Filename;
//            }
//        }
//        #endregion
//    }
//}
