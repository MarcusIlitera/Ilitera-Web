using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "ImportacaoAutomatica", "IdImportacaoAutomatica")]
    public class ImportacaoAutomatica : Ilitera.Data.Table
    {
        #region Enum

        public enum ImportacaoTabela : int
        {
            LocalTrabalho,
            Empregado,
            Funcao,
            Setor,
            Afastamento,
            ClassificacaoFuncional,
        }

        public enum ImportacaoArquivo : int
        {
            Excel,
            FixedWidth,
        }

        #endregion

        #region Properties

        private int _IdImportacaoAutomatica;
        private GrupoEmpresa _IdGrupoEmpresa;
        private DateTime _DataImportacao;
        private DateTime _DataExecucao;
        private string _NomeArquivo = string.Empty;
        private int _IndStatusImportacao;
        private string _MensagemErro = string.Empty;
        public StringBuilder LogErro;

        public delegate void EventChangePosition(int OrdinalPosition);
        //public event EventChangePosition PositionChanged;

        private DataSet dsCampos;

        public void SetCampos(DataSet dsCampos)
        {
            this.dsCampos = dsCampos;
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ImportacaoAutomatica()
        {

        }

        public override int Id
        {
            get { return _IdImportacaoAutomatica; }
            set { _IdImportacaoAutomatica = value; }
        }
        public GrupoEmpresa IdGrupoEmpresa
        {
            get { return _IdGrupoEmpresa; }
            set { _IdGrupoEmpresa = value; }
        }
        public DateTime DataImportacao
        {
            get { return _DataImportacao; }
            set { _DataImportacao = value; }
        }
        public DateTime DataExecucao
        {
            get { return _DataExecucao; }
            set { _DataExecucao = value; }
        }
        public string NomeArquivo
        {
            get { return _NomeArquivo; }
            set { _NomeArquivo = value; }
        }
        public int IndStatusImportacao
        {
            get { return _IndStatusImportacao; }
            set { _IndStatusImportacao = value; }
        }
        public string MensagemErro
        {
            get { return _MensagemErro; }
            set { _MensagemErro = value; }
        }

        #endregion

    //    #region Servico Importar

    //    public static void ImportarDadosDoArquivoFTP()
    //    {
    //        DirectoryInfo[] empresasFolder = new DirectoryInfo(EnvironmentUtitity.FolderFtp).GetDirectories();

    //        foreach (DirectoryInfo empresaFolder in empresasFolder)
    //        {
    //            if (!(
    //                empresaFolder.Name.ToUpper() == "Wickbold".ToUpper()
    //                //|| empresaFolder.Name.ToUpper() == "Galvani".ToUpper()
    //                || empresaFolder.Name.ToUpper() == "Produquimica".ToUpper()
    //                || empresaFolder.Name.ToUpper() == "Icecards".ToUpper()
    //                ))
    //                continue;

    //            FileInfo[] filesInfo = empresaFolder.GetFiles();

    //            try
    //            {
    //                Array.Sort(filesInfo, new FilesComparer());
    //            }
    //            catch { }

    //            foreach (FileInfo fileInfo in filesInfo)
    //            {
    //                ImportacaoAutomatica importacaoAutomatica = new ImportacaoAutomatica();
    //                importacaoAutomatica.Inicialize();
    //                importacaoAutomatica.LogErro = new StringBuilder();
    //                importacaoAutomatica.NomeArquivo = fileInfo.FullName;
    //                importacaoAutomatica.UsuarioId = 0;
    //                importacaoAutomatica.DataExecucao = DateTime.Now;

    //                try
    //                {
    //                    importacaoAutomatica.ImportarDadosDoArquivo(empresaFolder.Name, fileInfo);

    //                    fileInfo.MoveTo(Path.Combine(fileInfo.DirectoryName, @"Executado\" + fileInfo.Name));
    //                }
    //                catch (OleDbException ex)
    //                {
    //                    importacaoAutomatica.LogErro.Append((ex.Message + "\n"));
    //                    continue;
    //                }
    //                catch (IOException ex)
    //                {
    //                    importacaoAutomatica.LogErro.Append((ex.Message + "\n"));
    //                    continue;
    //                }
    //                catch (Exception ex)
    //                {
    //                    fileInfo.MoveTo(Path.Combine(fileInfo.DirectoryName, @"ExecutadoComErro\" + fileInfo.Name));
    //                    importacaoAutomatica.LogErro.Append((ex.Message + "\n"));
    //                }
    //                try
    //                {
    //                    importacaoAutomatica.MensagemErro += importacaoAutomatica.LogErro.ToString();
    //                }
    //                catch
    //                {
    //                    importacaoAutomatica.MensagemErro = "Erro ao salvar log - Length: " + importacaoAutomatica.LogErro.Length.ToString();
    //                }

    //                importacaoAutomatica.Save();
    //            }
    //        }
    //    }

    //    #region InnerClass Compare

    //    class FilesComparer : IComparer
    //    {
    //        public int Compare(object x, object y)
    //        {
    //            int iResult;

    //            FileInfo oFileX = (FileInfo)x;
    //            FileInfo oFileY = (FileInfo)y;

    //            char[] seps = { '_', ' ', '.' };

    //            String[] valuesX = oFileX.Name.Split(seps);
    //            String[] valuesY = oFileY.Name.Split(seps);

    //            int valueX = (int)Enum.Parse(typeof(ImportacaoTabela), valuesX[valuesX.Length - 4], true);
    //            int valueY = (int)Enum.Parse(typeof(ImportacaoTabela), valuesY[valuesX.Length - 4], true);

    //            DateTime dateTimeX = GetDateTime(valuesX[3]);
    //            DateTime dateTimeY = GetDateTime(valuesY[3]);

    //            if (valueX == valueY)
    //            {
    //                if (dateTimeX == dateTimeY)
    //                {
    //                    iResult = 0;
    //                }
    //                else if (dateTimeX > dateTimeY)
    //                {
    //                    iResult = 1;
    //                }
    //                else
    //                {
    //                    iResult = -1;
    //                }
    //            }
    //            else if (valueX > valueY)
    //            {
    //                iResult = 1;
    //            }
    //            else
    //            {
    //                iResult = -1;
    //            }
    //            return iResult;
    //        }
    //    }

    //    class FilesComparerDateTime : IComparer
    //    {
    //        public int Compare(object x, object y)
    //        {
    //            int iResult;

    //            FileInfo oFileX = (FileInfo)x;
    //            FileInfo oFileY = (FileInfo)y;

    //            char[] seps = { '_', ' ', '.' };

    //            String[] valuesX = oFileX.Name.Split(seps);
    //            String[] valuesY = oFileY.Name.Split(seps);

    //            DateTime dateTimeX = GetDateTime(valuesX[3]);
    //            DateTime dateTimeY = GetDateTime(valuesY[3]);

    //            if (dateTimeX == dateTimeY)
    //            {
    //                iResult = 0;
    //            }
    //            else if (dateTimeX > dateTimeY)
    //            {
    //                iResult = 1;
    //            }
    //            else
    //            {
    //                iResult = -1;
    //            }
    //            return iResult;
    //        }
    //    }

    //    class SheetsComparer : IComparer
    //    {
    //        public int Compare(object x, object y)
    //        {
    //            int iResult;

    //            string strX = (string)x;
    //            string strY = (string)y;

    //            int valueX = 0;
    //            int valueY = 0;

    //            try
    //            {
    //                valueX = (int)Enum.Parse(typeof(ImportacaoTabela), strX.Replace("$", ""), true);
    //            }
    //            catch { }

    //            try
    //            {
    //                valueY = (int)Enum.Parse(typeof(ImportacaoTabela), strY.Replace("$", ""), true);
    //            }
    //            catch { }

    //            if (valueX == valueY)
    //            {
    //                iResult = 0;
    //            }
    //            else if (valueX > valueY)
    //            {
    //                iResult = 1;
    //            }
    //            else
    //            {
    //                iResult = -1;
    //            }
    //            return iResult;
    //        }
    //    }

    //    #endregion

    //    #region ImportarDadosDoArquivo

    //    public void ImportarDadosDoArquivo(string strGrupoEmpresa, FileInfo fileInfo)
    //    {
    //        ImportarDadosDoArquivo(strGrupoEmpresa, string.Empty, fileInfo);
    //    }

    //    public void ImportarDadosDoArquivo(string strGrupoEmpresa, string strSchema, FileInfo fileInfo)
    //    {
    //        if (strGrupoEmpresa == string.Empty)
    //            strGrupoEmpresa = GetNomeGrupoEmpresa(fileInfo);

    //        string strNomeTabela = GetNomeTabela(fileInfo);

    //        Cliente cliente = this.GetCliente(strGrupoEmpresa);

    //        if (fileInfo.Extension.ToUpper() == ".XLS" && strNomeTabela != string.Empty)
    //            ImportarDadosDoArquivoExcel(cliente, fileInfo, strNomeTabela);
    //        else if (fileInfo.Extension.ToUpper() == ".XLS")
    //            ImportarDadosDoArquivoExcelPastas(cliente, fileInfo);
    //        else if (fileInfo.Extension.ToUpper() == ".TXT")
    //            ImportarDadosDoArquivoTXT(cliente, strSchema, fileInfo);
    //        else if (fileInfo.Extension.ToUpper() == ".XML")
    //            throw new Exception("Arquivo ainda XML não suportado!");
    //        else
    //            throw new Exception("Arquivo não suportado!");
    //    }

    //    private void ImportarDadosDoArquivoTXT(Cliente cliente, string strSchema, FileInfo fileInfo)
    //    {
    //        string strNomeTabela = GetNomeTabela(fileInfo);

    //        if (strSchema == string.Empty)
    //            strSchema = Path.Combine(Path.Combine(fileInfo.DirectoryName, "Layout"), strNomeTabela + ".xml");

    //        FileInfo schemaInfo = new FileInfo(strSchema);

    //        if (!schemaInfo.Exists)
    //            throw new Exception("O arquivo " + strSchema + " não foi encontrado!");

    //        int importacaoTabela = (int)Enum.Parse(typeof(ImportacaoTabela), strNomeTabela, true);

    //        DataSet dsDados = this.GetDataSetTextFile(fileInfo.FullName, strSchema);

    //        this.ImportaRegistros(cliente, importacaoTabela, dsDados);
    //    }

    //    private void ImportarDadosDoArquivoExcel(Cliente cliente, FileInfo fileInfo, string strNomeTabela)
    //    {
    //        try
    //        {
    //            string[] excelSheets = this.GetExcelSheetNames(fileInfo.FullName);

    //            int importacaoTabela = (int)Enum.Parse(typeof(ImportacaoTabela), strNomeTabela, true);

    //            DataSet dsDados = this.GetDataSetExcel(fileInfo.FullName, excelSheets[0]);

    //            this.ImportaRegistros(cliente, importacaoTabela, dsDados);
    //        }
    //        catch
    //        {
    //            this.LogErro.Append("A pasta " + strNomeTabela + " com nome inválido!\n");
    //        }
    //    }

    //    private void ImportarDadosDoArquivoExcelPastas(Cliente cliente, FileInfo fileInfo)
    //    {
    //        string[] excelSheets = this.GetExcelSheetNames(fileInfo.FullName);

    //        Array.Sort(excelSheets, new SheetsComparer());

    //        foreach (string excelSheet in excelSheets)
    //        {
    //            try
    //            {
    //                DataSet dsDados = this.GetDataSetExcel(fileInfo.FullName, excelSheet);

    //                int importacaoTabela = (int)Enum.Parse(typeof(ImportacaoTabela), excelSheet.Replace("$", ""), true);

    //                this.ImportaRegistros(cliente, importacaoTabela, dsDados);
    //            }
    //            catch
    //            {
    //                this.LogErro.Append("A pasta " + excelSheet + " com nome inválido!\n");
    //            }
    //        }
    //    }

    //    private string GetNomeGrupoEmpresa(FileInfo fileInfo)
    //    {
    //        char[] seps = { '_', ' ' };

    //        String[] values = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length).Split(seps);

    //        return values[0].ToUpper();
    //    }

    //    private string GetNomeTabela(FileInfo fileInfo)
    //    {
    //        //string strTipoArquivo = "DELIMETED";
    //        string strNomeTabela = string.Empty;
    //        string strDataArquivo = string.Empty;

    //        char[] seps = { '_' };

    //        String[] values = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length).Split(seps);

    //        if (values.Length == 4)
    //        {
    //            strNomeTabela = values[1];
    //            //strTipoArquivo = values[2];
    //            strDataArquivo = values[3];
    //        }
    //        else if (values.Length == 5)
    //        {
    //            strNomeTabela = values[2];
    //            strDataArquivo = values[4];
    //        }
    //        else if (values.Length == 3 && fileInfo.Extension == ".xls")
    //            strDataArquivo = values[2];
    //        else
    //            throw new Exception("Arquivo com formato inválido!");

    //        this.DataImportacao = GetDateTime(strDataArquivo);

    //        return strNomeTabela;
    //    }

    //    public Cliente GetCliente(string strGrupoEmpresa)
    //    {
    //        GrupoEmpresa grupoEmpresa = new GrupoEmpresa();
    //        grupoEmpresa.Find("Descricao='" + strGrupoEmpresa + "'");

    //        if (grupoEmpresa.Id == 0)
    //            throw new Exception("Grupo Empresa " + strGrupoEmpresa + " não encontrado!");

    //        ArrayList listClientes = new Cliente().Find("IdGrupoEmpresa=" + grupoEmpresa.Id + " ORDER BY NomeAbreviado");

    //        if (listClientes.Count == 0)
    //            throw new Exception("Nenhuma Empresa do Grupo " + strGrupoEmpresa + " foi encontrada!");

    //        this.IdGrupoEmpresa = grupoEmpresa;

    //        //Salva para registrar o Id na ClassificacaoFuncional
    //        if (this.Id == 0)
    //            this.Save();

    //        return (Cliente)listClientes[0];
    //    }
        
    //    #endregion

    //    #endregion

    //    #region Excel

    //    private bool ExitsSheet(string[] excelSheets, string sheet)
    //    {
    //        foreach (string st in excelSheets)
    //            if (st == sheet)
    //                return true;
    //        return false;
    //    }

    //    public string[] GetExcelSheetNames(string excelFile)
    //    {
    //        string[] excelSheets = null;

    //        OleDbConnection objConn = null;
    //        System.Data.DataTable dt = null;

    //        try
    //        {
    //            // Connection String. Change the excel file to the file you
    //            // will search.
    //            string connString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
    //                "Data Source=" + excelFile + ";Extended Properties=Excel 8.0;";
    //            // Create connection object by using the preceding connection string.
    //            objConn = new OleDbConnection(connString);
    //            // Open connection with the database.
    //            objConn.Open();
    //            // Get the data table containg the schema guid.
    //            dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

    //            if (dt == null)
    //            {
    //                return null;
    //            }

    //            excelSheets = new string[dt.Rows.Count];
    //            int i = 0;

    //            // Add the sheet name to the string array.
    //            foreach (DataRow row in dt.Rows)
    //            {
    //                excelSheets[i] = row["TABLE_NAME"].ToString();
    //                i++;
    //            }

    //            return excelSheets;

    //        }
    //        catch (Exception ex)
    //        {
    //            this.LogErro.Append("Erro ao ler pastas do Excel: " + ex.Message + "\n");
    //            throw ex;
    //        }
    //        finally
    //        {
    //            // Clean up.
    //            if (objConn != null)
    //            {
    //                objConn.Close();
    //                objConn.Dispose();
    //            }
    //            if (dt != null)
    //            {
    //                dt.Dispose();
    //            }
    //        }
    //    }

    //    public DataSet GetDataSetExcel(string strPathFile, string worksheet)
    //    {
    //        try
    //        {
    //            DataSet ret;

    //            StringBuilder sbConn = new StringBuilder();

    //            sbConn.Append(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strPathFile);
    //            sbConn.Append(";Extended Properties=");
    //            sbConn.Append(Convert.ToChar(34));
    //            sbConn.Append("Excel 8.0;HDR=Yes;IMEX=2");
    //            sbConn.Append(Convert.ToChar(34));

    //            OleDbConnection cnExcel = new OleDbConnection(sbConn.ToString());
    //            cnExcel.Open();

    //            OleDbCommand cmdExcel
    //                = new OleDbCommand("Select * From [" + worksheet + "]", cnExcel);

    //            OleDbDataReader drExcel = cmdExcel.ExecuteReader();

    //            ret = ConvertDataReaderToDataSet(drExcel);

    //            drExcel.Close();
    //            cnExcel.Close();

    //            return ret;
    //        }
    //        catch (Exception ex)
    //        {
    //            this.LogErro.Append("Erro ao ler dados do Excel: " + ex.Message + "\n");
    //            throw ex;
    //        }
    //    }

    //    private DataSet ConvertDataReaderToDataSet(OleDbDataReader reader)
    //    {
    //        DataSet dataSet = new DataSet("Result");
    //        do
    //        {
    //            // Create new data table

    //            DataTable schemaTable = reader.GetSchemaTable();
    //            DataTable dataTable = new DataTable();

    //            if (schemaTable != null)
    //            {
    //                // A query returning records was executed

    //                for (int i = 0; i < schemaTable.Rows.Count; i++)
    //                {
    //                    DataRow dataRow = schemaTable.Rows[i];
    //                    // Create a column name that is unique in the data table
    //                    string columnName = (string)dataRow["ColumnName"]; //+ "<C" + i + "/>";
    //                    // Add the column definition to the data table
    //                    DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
    //                    dataTable.Columns.Add(column);
    //                }

    //                dataSet.Tables.Add(dataTable);

    //                // Fill the data table we just created

    //                while (reader.Read())
    //                {
    //                    DataRow dataRow = dataTable.NewRow();

    //                    for (int i = 0; i < reader.FieldCount; i++)
    //                        dataRow[i] = reader.GetValue(i);

    //                    dataTable.Rows.Add(dataRow);
    //                }
    //            }
    //            else
    //            {
    //                // No records were returned
    //                DataColumn column = new DataColumn("RowsAffected");
    //                dataTable.Columns.Add(column);
    //                dataSet.Tables.Add(dataTable);

    //                DataRow dataRow = dataTable.NewRow();
    //                dataRow[0] = reader.RecordsAffected;
    //                dataTable.Rows.Add(dataRow);
    //            }
    //        }
    //        while (reader.NextResult());

    //        return dataSet;
    //    }

    //    #endregion

    //    # region Text File

    //    public DataSet GetDataSetTextFile(string filePath, string strSchema)
    //    {
    //        DataSet ds = new DataSet();

    //        TextFieldParser tfp;

    //        tfp = new TextFieldParser(filePath, strSchema);

    //        tfp.RecordFailed += new RecordFailedHandler(tfp_RecordFailed);

    //        DataTable dt = tfp.ParseToDataTable();

    //        ds.Tables.Add(dt);

    //        return ds;
    //    }

    //    private void tfp_RecordFailed(ref int CurrentLineNumber, string LineText, string ErrorMessage, ref bool Continue)
    //    {
    //        if (this.LogErro != null)
    //            this.LogErro.Append("Error: " + ErrorMessage + Environment.NewLine + "Line: " + LineText + "\n");
    //    }

    //    #endregion

    //    #region ImportarDados

    //    #region ImportarRegistros

    //    public void ImportaRegistros(Cliente cliente, int importacaoTabela, DataSet dsDados)
    //    {
    //        int OrdinalPosition = 0;

    //        foreach (DataRow row in dsDados.Tables[0].Rows)
    //        {
    //            OrdinalPosition++;

    //            if (PositionChanged != null)
    //                PositionChanged(OrdinalPosition);

    //            try
    //            {
    //                switch (importacaoTabela)
    //                {
    //                    case (int)ImportacaoTabela.Empregado:
    //                        ImportaRegistrosEmpregado(cliente, row);
    //                        break;
    //                    case (int)ImportacaoTabela.Funcao:
    //                        ImportaRegistrosFuncao(cliente, row);
    //                        break;

    //                    case (int)ImportacaoTabela.Setor:
    //                        ImportaRegistrosSetor(cliente, row);
    //                        break;

    //                    case (int)ImportacaoTabela.LocalTrabalho:
    //                        ImportaRegistrosLocalTrabalho(cliente, row);
    //                        break;

    //                    case (int)ImportacaoTabela.ClassificacaoFuncional:
    //                        ImportaRegistrosClassificacaoFuncional(cliente, row);
    //                        break;

    //                    case (int)ImportacaoTabela.Afastamento:
    //                        ImportaRegistrosAfastamento(cliente, row);
    //                        break;
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                if(this.LogErro == null)
    //                    this.LogErro = new System.Text.StringBuilder();

    //                this.LogErro.Append("Linha: " + OrdinalPosition.ToString("00000") 
    //                                    + " Erro: " + ex.Message + " (" + Convert.ToString((row[0])) + ")\n");
    //            }
    //        }
    //    }
    //    #endregion

    //    #region Empregado

    //    public void ImportaRegistrosEmpregado(Cliente cliente, DataRow row)
    //    {
    //        ////////////////////////////////////////////////////////
    //        ///Codigo do Empresa
    //        ////////////////////////////////////////////////////////
    //        Cliente clienteEmpregado = GetClienteEmpregado(cliente, row);

    //        if (clienteEmpregado.Id == 0)
    //            return;

    //        ////////////////////////////////////////////////////////
    //        ///Local de Trabalho
    //        ////////////////////////////////////////////////////////
    //        Cliente localTrabalho = GetLocalDeTrabalho(cliente, row, clienteEmpregado);

    //        if (localTrabalho.Id == 0)
    //            return;

    //        ////////////////////////////////////////////////////////
    //        ///Empregados
    //        ////////////////////////////////////////////////////////

    //        Empregado empregado = GetEmpregado(row, clienteEmpregado);

    //        if (empregado.Id == 0)
    //            return;

    //        ////////////////////////////////////////////////////////
    //        ///Função
    //        ////////////////////////////////////////////////////////
    //        Funcao funcao = GetFuncao(row, localTrabalho);

    //        if (funcao.Id == 0)
    //            return;

    //        ////////////////////////////////////////////////////////
    //        ///Setor
    //        ////////////////////////////////////////////////////////
    //        Setor setor =  GetSetor(row, localTrabalho);

    //        if (setor.Id == 0)
    //            return;

    //        ////////////////////////////////////////////////////////
    //        ///Classificação Funcional
    //        ////////////////////////////////////////////////////////
    //        EmpregadoFuncao empregadoFuncao = GetEmpregadoFuncao(row, localTrabalho, empregado, funcao, setor);

    //        if (empregadoFuncao.Id == 0)
    //            return;

    //        ////////////////////////////////////////////////////////
    //        ///GHE
    //        ////////////////////////////////////////////////////////
    //        GetGheEmpregado(row, localTrabalho, empregadoFuncao);
    //    }

    //    #region GetClienteEmpregado

    //    private Cliente GetClienteEmpregado(Cliente cliente, DataRow row)
    //    {
    //        Cliente clienteEmpregado = new Cliente();

    //        string strCodigoEmpresa = TratarCodigo(GetValueCol(string.Empty, row, "CodigoEmpresa"));

    //        if (strCodigoEmpresa != string.Empty)
    //        {
    //            if (this.IdGrupoEmpresa == null || this.IdGrupoEmpresa.Id ==0)
    //            {
    //                this.IdGrupoEmpresa = new GrupoEmpresa();
    //                this.IdGrupoEmpresa.Find(cliente.IdGrupoEmpresa.Id);
    //            }

    //            ArrayList listLojas = clienteEmpregado.Find("IdGrupoEmpresa=" + this.IdGrupoEmpresa.Id
    //                + " AND CodigoLojaObra = '" + strCodigoEmpresa.Replace("'", "''") + "'");

    //            if (listLojas.Count != 1)
    //                return new Cliente();
    //        }
    //        else
    //        {
    //            if (cliente.IdJuridicaPai.Id != 0)
    //                clienteEmpregado.Id = cliente.IdJuridicaPai.Id;
    //            else
    //                clienteEmpregado.Id = cliente.Id;
    //        }
    //        return clienteEmpregado;
    //    }
    //    #endregion

    //    #region GetLocalDeTrabalho

    //    private Cliente GetLocalDeTrabalho(Cliente cliente, DataRow row, Cliente clienteEmpregado)
    //    {
    //        Cliente localTrabalho = new Cliente();

    //        string strLocalDeTrabalho = GetValueCol(string.Empty, row, "LocalDeTrabalho");

    //        if (strLocalDeTrabalho != string.Empty)
    //        {
    //            string criteria = "IdJuridicaPai=" + cliente.Id
    //                            + " AND (CodigoLojaObra = '" + strLocalDeTrabalho.Replace("'", "''") + "'"
    //                            + " OR NomeAbreviado='" + strLocalDeTrabalho.Replace("'", "''") + "'"
    //                            + " OR NomeCodigo='" + strLocalDeTrabalho.Replace("'", "''") + "'" + ")";

    //            ArrayList locais = localTrabalho.Find(criteria);

    //            if (locais.Count != 1)
    //                return new Cliente();
    //        }
    //        else
    //        {
    //            if (cliente.IdJuridicaPai.Id != 0)
    //                localTrabalho.Id = cliente.Id;
    //            else
    //                localTrabalho.Id = clienteEmpregado.Id;
    //        }
    //        return localTrabalho;
    //    }
    //    #endregion

    //    #region GetEmpregado

    //    private Empregado GetEmpregado(DataRow row, Cliente clienteEmpregado)
    //    {
    //        string codigoEmpregado = TratarCodigo(GetValueCol(string.Empty, row, "CodigoEmpregado"));
    //        string nomeEmpregado = GetValueCol(string.Empty, row, "NomeEmpregado");
    //        string ctpsCompleta = GetValueCol(string.Empty, row, "Ctps");
    //        string identidade = GetValueCol(string.Empty, row, "Identidade");

    //        if (nomeEmpregado == string.Empty)
    //            return new Empregado();

    //        string ctps_numero = string.Empty;
    //        string ctps_serie = string.Empty;
    //        string ctps_uf = string.Empty;

    //        if (ctpsCompleta != string.Empty)
    //        {
    //            char[] seps = { '-', '/' };

    //            string[] values = ctpsCompleta.Split(seps);

    //            if (values.Length > 0)
    //                ctps_numero = values[0];

    //            if (values.Length > 1)
    //                ctps_serie = values[1];

    //            if (values.Length > 2)
    //                ctps_uf = values[2];
    //        }
    //        else
    //        {
    //            ctps_numero = GetValueCol(string.Empty, row, "CtpsNumero");
    //            ctps_serie = GetValueCol(string.Empty, row, "CtpsSerie");
    //            ctps_uf = GetValueCol(string.Empty, row, "CtpsUf");
    //        }

    //        List<Empregado> empregados;

    //        StringBuilder str = new StringBuilder();

    //        str.Append("nID_EMPR=" + clienteEmpregado.Id);

    //        if (codigoEmpregado != string.Empty)
    //        {
    //            str.Append(" AND tCOD_EMPR='" + codigoEmpregado + "'");

    //            empregados = new Empregado().Find<Empregado>(str.ToString());

    //            if (empregados.Count == 0)
    //            {
    //                str.Replace(" AND tCOD_EMPR='" + codigoEmpregado + "'",
    //                            " AND (tCOD_EMPR IS NULL OR tCOD_EMPR = '')");

    //                if (identidade == string.Empty)
    //                    str.Append(" AND tNO_EMPG='" + nomeEmpregado + "'");
    //                else
    //                {
    //                    str.Append(" AND (tNO_EMPG='" + nomeEmpregado + "'");
    //                    str.Append(" OR tNO_IDENTIDADE ='" + identidade + "')");
    //                }
    //                empregados = new Empregado().Find<Empregado>(str.ToString());
    //            }
    //        }
    //        else
    //        {
    //            str.Append(" AND tNO_EMPG='" + nomeEmpregado.Replace("'", "''") + "'");

    //            empregados = new Empregado().Find<Empregado>(str.ToString());
    //        }

    //        Empregado empregado;

    //        if (empregados.Count == 1)
    //        {
    //            empregado = empregados[0];
    //        }
    //        else if (empregados.Count > 1)
    //        {
    //            return new Empregado();
    //        }
    //        else
    //        {
    //            empregado = new Empregado();
    //            empregado.Inicialize();
    //            empregado.nID_EMPR.Id = clienteEmpregado.Id;
    //        }

    //        empregado.tNO_EMPG = nomeEmpregado;
    //        empregado.tCOD_EMPR = codigoEmpregado;
    //        empregado.nNO_PIS_PASEP = GetInt64(Convert.ToString(GetValueCol(empregado.nNO_PIS_PASEP.ToString(), row, "Pis")));
    //        empregado.hDT_ADM = GetDateTimeCol(empregado.hDT_ADM, row, "Admissao");
    //        empregado.hDT_DEM = GetDateTimeCol(empregado.hDT_DEM, row, "Demissao");
    //        empregado.hDT_NASC = GetDateTimeCol(empregado.hDT_NASC, row, "Nascimento");
    //        empregado.nNO_FOTO = GetInt32Col(empregado.nNO_FOTO, row, "NumeroFoto");
    //        empregado.tNO_IDENTIDADE = GetValueCol(empregado.tNO_IDENTIDADE, row, "Identidade");
    //        empregado.tSEXO = GetValueCol(empregado.tSEXO, row, "Sexo");
    //        empregado.tNO_CPF = TratarCodigo(Convert.ToString(GetValueCol(empregado.tNO_CPF.ToString(), row, "Cpf")));

    //        if (row.Table.Columns.IndexOf("IndAdicional") != -1 && row.Table.Columns.IndexOf("PctAdicional") != -1)
    //        {
    //            empregado.nIND_ADICIONAL = (TipoAdicional)Enum.Parse(typeof(TipoAdicional), GetValueCol(string.Empty, row, "IndAdicional"));

    //            int pctAdicional = Convert.ToInt32(GetValueCol(Convert.ToInt32(empregado.nAD_INSALUBRIDADE).ToString(), row, "PctAdicional"));

    //            if (pctAdicional == 0)
    //                empregado.nAD_INSALUBRIDADE = 0;
    //            else if (pctAdicional == 1 || pctAdicional == 2 || pctAdicional == 3 || pctAdicional == 4)
    //                empregado.nAD_INSALUBRIDADE = Convert.ToSingle(Int32.Parse(pctAdicional.ToString() + "0"));
    //            else if (pctAdicional == 10 || pctAdicional == 20 || pctAdicional == 30 || pctAdicional == 40)
    //                empregado.nAD_INSALUBRIDADE = Convert.ToSingle(pctAdicional);
    //        }
    //        else
    //        {
    //            empregado.nAD_INSALUBRIDADE = GetSingle(GetValueCol(empregado.nAD_INSALUBRIDADE.ToString(), row, "PorcentagemAdicional"));

    //            char RecebeInsalubridade = GetValueColSimNao(Convert.ToString(empregado.GetRecebeInsalubridade()), row, "RecebeInsalubridade");

    //            char RecebePericulosidade = GetValueColSimNao(empregado.GetRecebePericulosidade(), row, "RecebePericulosidade");

    //            empregado.SetAdicionalEmpregado(RecebeInsalubridade, RecebePericulosidade);
    //        }

    //        if (ctps_numero != string.Empty)
    //        {
    //            empregado.tNUM_CTPS = ctps_numero;
    //            empregado.tSER_CTPS = ctps_serie;
    //            empregado.tUF_CTPS = ctps_uf;
    //        }

    //        empregado.tEND_NOME = Convert.ToString(GetValueCol(empregado.tEND_NOME, row, "Logradouro"));
    //        empregado.tEND_NUMERO = Convert.ToString(GetValueCol(empregado.tEND_NUMERO, row, "Numero"));
    //        empregado.tEND_COMPL = Convert.ToString(GetValueCol(empregado.tEND_COMPL, row, "Complemento"));
    //        empregado.tEND_BAIRRO = Convert.ToString(GetValueCol(empregado.tEND_BAIRRO, row, "Bairro"));
    //        empregado.tEND_MUNICIPIO = Convert.ToString(GetValueCol(empregado.tEND_MUNICIPIO, row, "Municipio"));
    //        empregado.tEND_UF = Convert.ToString(GetValueCol(empregado.tEND_UF, row, "Uf"));
    //        empregado.tEND_CEP = Convert.ToString(GetValueCol(empregado.tEND_CEP, row, "Cep"));

    //        if (empregado.hDT_ADM == new DateTime() 
    //            && GetDateTimeCol(new DateTime(), row, "Inicio") != new DateTime())
    //            empregado.hDT_ADM = GetDateTimeCol(new DateTime(), row, "Inicio");

    //        //Passou de Candidato a Empregado
    //        if (empregado.Id != 0
    //            && empregado.gTERCEIRO
    //            && empregado.tCOD_EMPR != string.Empty)
    //            empregado.gTERCEIRO = false;

    //        empregado.Save(false);

    //        return empregado;
    //    }
    //    #endregion

    //    #region GetFuncao

    //    private Funcao GetFuncao(DataRow row, Cliente localTrabalho)
    //    {
    //        string codigoFuncao = TratarCodigo(GetValueCol(string.Empty, row, "CodigoFuncao"));
    //        string nomeFuncao = GetValueCol(string.Empty, row, "NomeFuncao");
    //        string cbo = GetValueCol(string.Empty, row, "Cbo");

    //        if (codigoFuncao == string.Empty && nomeFuncao == string.Empty)
    //            return new Funcao();

    //        StringBuilder strFuncao = new StringBuilder();

    //        strFuncao.Append("IdCliente=" + localTrabalho.Id);

    //        if (codigoFuncao != string.Empty)
    //            strFuncao.Append(" AND CodigoFuncao='" + codigoFuncao.Replace("'", "") + "'");
    //        else
    //            strFuncao.Append(" AND NomeFuncao='" + nomeFuncao.Replace("'", "") + "'");

    //        List<Funcao> funcoes = new Funcao().Find<Funcao>(strFuncao.ToString());

    //        Funcao funcao;

    //        if (funcoes.Count >= 1)
    //        {
    //            funcao = funcoes[0];
    //        }
    //        else
    //        {
    //            funcao = new Funcao();
    //            funcao.Inicialize();
    //            funcao.IdCliente.Id = localTrabalho.Id;
    //        }

    //        funcao.CodigoFuncao = codigoFuncao;
    //        funcao.NomeFuncao = nomeFuncao;
    //        funcao.NumeroCBO = cbo;
    //        funcao.Save();

    //        return funcao;
    //    }
    //    #endregion

    //    #region GetSetor

    //    private Setor GetSetor(DataRow row, Cliente localTrabalho)
    //    {
    //        string codigoSetor = TratarCodigo(GetValueCol(string.Empty, row, "CodigoSetor"));
    //        string nomeSetor = GetValueCol(string.Empty, row, "NomeSetor");

    //        if (nomeSetor == string.Empty && codigoSetor == string.Empty)
    //            return new Setor();

    //        StringBuilder strSetor = new StringBuilder();
    //        strSetor.Append("nID_EMPR=" + localTrabalho.Id);

    //        if (codigoSetor != string.Empty)
    //            strSetor.Append(" AND tCOD_SETOR='" + codigoSetor.Replace("'", "") + "'");
    //        else
    //            strSetor.Append(" AND tNO_STR_EMPR='" + nomeSetor.Replace("'", "") + "'");

    //        List<Setor> setores = new Setor().Find<Setor>(strSetor.ToString());

    //        Setor setor;

    //        if (setores.Count >= 1)
    //        {
    //            setor = setores[0];
    //        }
    //        else
    //        {
    //            setor = new Setor();
    //            setor.Inicialize();
    //            setor.nID_EMPR.Id = localTrabalho.Id;
    //        }

    //        setor.tCOD_SETOR = codigoSetor;
    //        setor.tNO_STR_EMPR = nomeSetor;
    //        setor.Save();

    //        return setor;
    //    }
    //    #endregion

    //    #region GetEmpregadoFuncao

    //    private EmpregadoFuncao GetEmpregadoFuncao(DataRow row, Cliente localTrabalho, Empregado empregado, Funcao funcao, Setor setor)
    //    {
    //        EmpregadoFuncao empregadoFuncao = new EmpregadoFuncao();

    //        DateTime dataInicio = GetDateTimeCol(new DateTime(), row, "Inicio");
    //        DateTime dataTermino = GetDateTimeCol(new DateTime(), row, "Termino");

    //        if (dataTermino == new DateTime() && empregado.hDT_DEM != new DateTime())
    //            dataTermino = empregado.hDT_DEM;

    //        if (dataInicio == new DateTime())
    //            dataInicio = empregado.hDT_ADM;

    //        if (dataInicio == new DateTime() || funcao.Id == 0 || setor.Id == 0)
    //            return empregadoFuncao;

    //        empregadoFuncao.Find("nID_EMPREGADO=" + empregado.Id
    //                            + " AND nID_FUNCAO=" + funcao.Id
    //                            + " AND nID_SETOR=" + setor.Id
    //                            + " AND hDT_INICIO='" + dataInicio.ToString("yyyy-MM-dd") + "'");

    //        if (empregadoFuncao.Id == 0)
    //            empregadoFuncao.Find("nID_EMPREGADO=" + empregado.Id
    //                                + " AND nID_FUNCAO=" + funcao.Id
    //                                + " AND hDT_INICIO='" + dataInicio.ToString("yyyy-MM-dd") + "'");

    //        if (empregadoFuncao.Id == 0)
    //            empregadoFuncao.Find("nID_EMPREGADO=" + empregado.Id
    //                                + " AND hDT_INICIO='" + dataInicio.ToString("yyyy-MM-dd") + "'");

    //        if (empregadoFuncao.Id == 0 && dataTermino == new DateTime())
    //        {
    //            EmpregadoFuncao emprFuncEmAberto = new EmpregadoFuncao();
    //            emprFuncEmAberto.Find("nID_EMPREGADO=" + empregado.Id
    //                                + " AND hDT_INICIO='" + dataInicio.ToString("yyyy-MM-dd") + "'");

    //            if (emprFuncEmAberto.Id != 0 && emprFuncEmAberto.hDT_INICIO == dataInicio)
    //                return empregadoFuncao;
    //        }

    //        if (empregadoFuncao.Id == 0)
    //            empregadoFuncao.Inicialize();

    //        empregadoFuncao.nID_EMPR.Id = localTrabalho.Id;
    //        empregadoFuncao.nID_EMPREGADO.Id = empregado.Id;
    //        empregadoFuncao.nID_FUNCAO.Id = funcao.Id;
    //        empregadoFuncao.nID_SETOR.Id = setor.Id;
    //        empregadoFuncao.hDT_INICIO = dataInicio;
    //        empregadoFuncao.hDT_TERMINO = dataTermino;
    //        empregadoFuncao.nIND_GFIP = (int)Ghe.GetGFIP(GetValueCol(Ghe.GetGFIP(empregadoFuncao.nIND_GFIP), row, "Gfip"));

    //        if (dataInicio > dataTermino && dataTermino != new DateTime())
    //            return empregadoFuncao;

    //        empregadoFuncao.Save();

    //        return empregadoFuncao;
    //    }
    //    #endregion

    //    #region GetGheEmpregado

    //    private void GetGheEmpregado(DataRow row, Cliente localTrabalho, EmpregadoFuncao empregadoFuncao)
    //    {
    //        Ghe ghe = new Ghe();

    //        string strGHE = Convert.ToString(GetValueCol(string.Empty, row, "CodigoGhe"));

    //        if (strGHE != string.Empty)
    //        {
    //            LaudoTecnico laudoTecnico = LaudoTecnico.GetUltimoLaudo(localTrabalho.Id, false);

    //            ghe.Find("nID_LAUD_TEC=" + laudoTecnico.Id
    //                + " AND (tNO_FUNC='" + strGHE
    //                + "' OR  LEFT(tNO_FUNC, 6) = '" + strGHE + "')");
    //        }

    //        if (ghe.Id != 0)
    //        {
    //            GheEmpregado gheEmpregado = new GheEmpregado();

    //            gheEmpregado.Find("nID_EMPREGADO_FUNCAO=" + empregadoFuncao.Id
    //                                + " AND nID_LAUD_TEC=" + ghe.nID_LAUD_TEC.Id
    //                                + " AND nID_FUNC=" + ghe.Id);

    //            if (gheEmpregado.Id == 0)
    //                gheEmpregado.Inicialize();

    //            gheEmpregado.nID_EMPREGADO_FUNCAO.Id = empregadoFuncao.Id;
    //            gheEmpregado.nID_LAUD_TEC.Id = ghe.nID_LAUD_TEC.Id;
    //            gheEmpregado.nID_FUNC.Id = ghe.Id;
    //            gheEmpregado.Save();
    //        }
    //    }
    //    #endregion

    //    #endregion

    //    #region Funcao

    //    private Funcao AddFuncaoFilial(Cliente cliente, string codigoFuncao)
    //    {
    //        StringBuilder str = new StringBuilder();

    //        if (cliente.IdGrupoEmpresa.Id != 0)
    //        {
    //            str.Append("IdCliente IN (SELECT IdJuridica FROM Juridica");
    //            str.Append(" WHERE IdGrupoEmpresa IS NOT NULL");
    //            str.Append(" AND IdGrupoEmpresa=" + cliente.IdGrupoEmpresa.Id + ")");
    //        }
    //        else
    //            str.Append("IdCliente=" + cliente.Id);

    //        str.Append(" AND CodigoFuncao='" + codigoFuncao.Replace("'", "") + "'");

    //        ArrayList listFuncao = new Funcao().Find(str.ToString());

    //        Funcao funcao = new Funcao();

    //        if (listFuncao.Count > 0)
    //        {
    //            ((Funcao)listFuncao[0]).Id = 0;
    //            ((Funcao)listFuncao[0]).IdCliente.Id = cliente.Id;
    //            ((Funcao)listFuncao[0]).Save();

    //            funcao = ((Funcao)listFuncao[0]);
    //        }
    //        return funcao;
    //    }

    //    public void ImportaRegistrosFuncao(Cliente cliente, DataRow row)
    //    {
    //        ////////////////////////////////////////////////////////
    //        ///Codigo do Empresa
    //        ////////////////////////////////////////////////////////
    //        Cliente localTrabalho = new Cliente();

    //        string strCodigoEmpresa = TratarCodigo(GetValueCol(string.Empty, row, "CodigoEmpresa"));

    //        if (strCodigoEmpresa != string.Empty)
    //            localTrabalho = this.GetLocalDeTrabalho(cliente, strCodigoEmpresa);
    //        else
    //            localTrabalho.Id = cliente.Id;

    //        if (localTrabalho.Id == 0)
    //            return;

    //        ////////////////////////////////////////////////////////
    //        ///Função
    //        ////////////////////////////////////////////////////////
    //        string codigoFuncao = TratarCodigo(GetValueCol(string.Empty, row, "CodigoFuncao"));
    //        string nomeFuncao = GetValueCol(string.Empty, row, "NomeFuncao");

    //        if (nomeFuncao == string.Empty)
    //            return;

    //        Funcao funcao = new Funcao();

    //        StringBuilder strFuncao = new StringBuilder();

    //        strFuncao.Append("IdCliente=" + localTrabalho.Id);

    //        if (codigoFuncao != string.Empty)
    //        {
    //            strFuncao.Append(" AND (CodigoFuncao='" + codigoFuncao.Replace("'", "") + "'");
    //            strFuncao.Append(" OR ((CodigoFuncao IS NULL OR CodigoFuncao='') AND NomeFuncao='" + nomeFuncao.Replace("'", "") + "'))");
    //        }
    //        else
    //            strFuncao.Append(" AND NomeFuncao='" + nomeFuncao.Replace("'", "") + "'");

    //        ArrayList listFuncao = funcao.Find(strFuncao.ToString());

    //        if (listFuncao.Count > 1)
    //            return;

    //        if (funcao.Id == 0)
    //            funcao.Inicialize();

    //        string descricaoFuncao = GetValueCol(funcao.DescricaoFuncao, row, "DescricaoFuncao");
    //        string cbo = GetValueCol(funcao.NumeroCBO, row, "Cbo");

    //        funcao.IdCliente.Id = localTrabalho.Id;
    //        funcao.CodigoFuncao = codigoFuncao;
    //        funcao.NomeFuncao = nomeFuncao;
    //        funcao.DescricaoFuncao = descricaoFuncao;
    //        funcao.NumeroCBO = Funcao.FormatarCBO(cbo);
    //        funcao.Save();
    //    }

    //    #endregion

    //    #region Setor
        
    //    private Setor AddSetorFilial(Cliente cliente, string codigoSetor)
    //    {
    //        StringBuilder str = new StringBuilder();

    //        if (cliente.IdGrupoEmpresa.Id != 0)
    //        {
    //            str.Append("nID_EMPR IN (SELECT IdJuridica FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Juridica");
    //            str.Append(" WHERE IdGrupoEmpresa IS NOT NULL");
    //            str.Append(" AND IdGrupoEmpresa=" + cliente.IdGrupoEmpresa.Id + ")");
    //        }
    //        else
    //            str.Append("nID_EMPR=" + cliente.Id);

    //        str.Append(" AND tCOD_SETOR='" + codigoSetor.Replace("'", "") + "'");

    //        ArrayList listSetor = new Setor().Find(str.ToString());

    //        Setor setor = new Setor();

    //        if (listSetor.Count > 0)
    //        {
    //            ((Setor)listSetor[0]).Id = 0;
    //            ((Setor)listSetor[0]).nID_EMPR.Id = cliente.Id;
    //            ((Setor)listSetor[0]).Save();

    //            setor = ((Setor)listSetor[0]);
    //        }
    //        return setor;
    //    }

    //    public void ImportaRegistrosSetor(Cliente cliente, DataRow row)
    //    {
    //        ////////////////////////////////////////////////////////
    //        ///Codigo do Empresa
    //        ////////////////////////////////////////////////////////		
    //        Cliente localTrabalho = new Cliente();

    //        string codigoEmpresa = TratarCodigo(GetValueCol(string.Empty, row, "CodigoEmpresa"));

    //        if (codigoEmpresa != string.Empty)
    //            localTrabalho = this.GetLocalDeTrabalho(cliente, codigoEmpresa);
    //        else
    //            localTrabalho.Id = cliente.Id;

    //        if (localTrabalho.Id == 0)
    //            return;

    //        ////////////////////////////////////////////////////////
    //        ///Setor
    //        ////////////////////////////////////////////////////////
    //        string codigoSetor = TratarCodigo(GetValueCol(string.Empty, row, "CodigoSetor"));
    //        string nomeSetor = GetValueCol(string.Empty, row, "NomeSetor");

    //        if (nomeSetor == string.Empty)
    //            throw new Exception("Nome do setor em branco - CodigoSetor (" + codigoSetor + ")");

    //        Setor setor = new Setor();

    //        StringBuilder strSetor = new StringBuilder();

    //        strSetor.Append("nID_EMPR=" + localTrabalho.Id);

    //        if (codigoSetor != string.Empty)
    //        {
    //            strSetor.Append(" AND (tCOD_SETOR='" + codigoSetor.Replace("'", "") + "'");
    //            strSetor.Append(" OR ((tCOD_SETOR IS NULL OR tCOD_SETOR='') AND tNO_STR_EMPR='" + nomeSetor.Replace("'", "") + "'))");
    //        }
    //        else
    //        {
    //            strSetor.Append(" AND tNO_STR_EMPR='" + nomeSetor.Replace("'", "") + "'");
    //        }

    //        ArrayList listSetor = setor.Find(strSetor.ToString());

    //        if (listSetor.Count > 1)
    //            return;

    //        if (setor.Id == 0)
    //            setor.Inicialize();

    //        setor.nID_EMPR.Id = localTrabalho.Id;
    //        setor.tCOD_SETOR = codigoSetor;
    //        setor.tNO_STR_EMPR = nomeSetor;
    //        setor.Save();
    //    }

    //    #endregion

    //    #region LocalDeTrabalho

    //    private Cliente GetLocalDeTrabalho(Cliente cliente, string strCodigoEmpresa)
    //    {
    //        if (strCodigoEmpresa == string.Empty)
    //            return new Cliente();

    //        StringBuilder str = new StringBuilder();

    //        ////////////////////////////////////////////////////////
    //        ///Atenção!!! Buscar o Codigo também em locais de trabalho
    //        ////////////////////////////////////////////////////////
    //        str.Append("IdGrupoEmpresa=" + cliente.IdGrupoEmpresa.Id);
    //        str.Append(" AND CodigoLojaObra = '" + strCodigoEmpresa + "'");

    //        ArrayList list = new Cliente().Find(str.ToString());

    //        if (list.Count == 0)
    //            return new Cliente();
    //        else
    //            return (Cliente)list[0];
    //    }

    //    public void ImportaRegistrosLocalTrabalho(Cliente cliente, DataRow row)
    //    {
    //        ////////////////////////////////////////////////////////
    //        ///Local de Trabalho
    //        ////////////////////////////////////////////////////////
    //        Cliente clienteEmpresaPrincipal = new Cliente();

    //        string strClientePrincipal = GetValueCol(string.Empty, row, "CodigoEmpresaPrincipal");

    //        if (strClientePrincipal != string.Empty)
    //        {
    //            ArrayList listEmpresas = clienteEmpresaPrincipal.Find("IdGrupoEmpresa=" + cliente.IdGrupoEmpresa.Id
    //                + " AND CodigoLojaObra = '" + strClientePrincipal + "'");

    //            if (listEmpresas.Count != 1)
    //                return;
    //        }
    //        else
    //            clienteEmpresaPrincipal.Id = cliente.Id;

    //        if (!clienteEmpresaPrincipal.AtivarLocalDeTrabalho)
    //            return;

    //        Cliente clienteLocaldeTrabalho = new Cliente();
    //        clienteLocaldeTrabalho.Find("IdJuridicaPai=" + clienteEmpresaPrincipal.Id
    //            + " AND CodigoLojaObra ='" + GetValueCol(string.Empty, row, "CodigoEmpresa") + "'");

    //        if (clienteLocaldeTrabalho.Id == 0)
    //        {
    //            clienteLocaldeTrabalho.Inicialize();
    //            clienteLocaldeTrabalho.IdJuridicaPapel.Id = (int)IndJuridicaPapel.Tomadora;
    //            clienteLocaldeTrabalho.IdJuridicaPai.Id = clienteEmpresaPrincipal.Id;
    //            clienteLocaldeTrabalho.CodigoLojaObra = GetValueCol(clienteLocaldeTrabalho.CodigoLojaObra, row, "CodigoEmpresa");
    //        }

    //        clienteLocaldeTrabalho.IdGrupoEmpresa.Id = clienteEmpresaPrincipal.IdGrupoEmpresa.Id;
    //        clienteLocaldeTrabalho.ContrataPCMSO = clienteEmpresaPrincipal.ContrataPCMSO;
    //        clienteLocaldeTrabalho.ContrataCipa = clienteEmpresaPrincipal.ContrataCipa;

    //        clienteLocaldeTrabalho.NomeCodigo = GetValueCol(clienteLocaldeTrabalho.NomeCodigo, row, "Cnpj");
    //        clienteLocaldeTrabalho.NomeAbreviado = GetValueCol(clienteLocaldeTrabalho.NomeAbreviado, row, "NomeAbreviado");
    //        clienteLocaldeTrabalho.NomeCompleto = GetValueCol(clienteLocaldeTrabalho.NomeCompleto, row, "RazaoSocial");
    //        clienteLocaldeTrabalho.IE = GetValueCol(clienteLocaldeTrabalho.IE, row, "IE");

    //        string strCnae = GetValueCol(string.Empty, row, "Cnae");

    //        if (strCnae != string.Empty)
    //        {
    //            CNAE cnae = new CNAE();
    //            cnae.Find("Codigo='" + strCnae + "'");
    //            clienteLocaldeTrabalho.IdCNAE.Id = cnae.Id;
    //        }

    //        clienteLocaldeTrabalho.Save();

    //        Endereco endereco = clienteLocaldeTrabalho.GetEndereco();

    //        if (endereco.Id == 0)
    //        {
    //            endereco.Inicialize();
    //            endereco.IdPessoa.Id = clienteLocaldeTrabalho.Id;
    //        }
    //        endereco.Logradouro = GetValueCol(endereco.Logradouro, row, "Logradouro");
    //        endereco.Numero = GetValueCol(endereco.Numero, row, "Numero");
    //        endereco.Complemento = GetValueCol(endereco.Complemento, row, "Complemento");
    //        endereco.Bairro = GetValueCol(endereco.Complemento, row, "Bairro");
    //        endereco.Cep = GetValueCol(endereco.Cep, row, "Cep");

    //        string strUF = GetValueCol(string.Empty, row, "Uf");
    //        string strMunicipio = GetValueCol(string.Empty, row, "Municipio");

    //        endereco.IdMunicipio.Id = Municipio.GetMunicipio(strUF, strMunicipio).Id;
    //        endereco.Municipio = strMunicipio;
    //        endereco.Uf = strUF;
    //        endereco.Save();
    //    }

    //    #endregion

    //    #region ClassificacaoFuncional
        
    //    public void ImportaRegistrosClassificacaoFuncional(Cliente cliente, DataRow row)
    //    {
    //        ////////////////////////////////////////////////////////
    //        ///Código do Empresa
    //        ////////////////////////////////////////////////////////
    //        string codigoSetor = TratarCodigo(GetValueCol(string.Empty, row, "CodigoSetor"));
    //        string codigoEmpresa = TratarCodigo(GetValueCol(string.Empty, row, "CodigoEmpresa"));
    //        string codigoFuncao = TratarCodigo(GetValueCol(string.Empty, row, "CodigoFuncao"));
    //        string codigoEmpregado = TratarCodigo(GetValueCol(string.Empty, row, "CodigoEmpregado"));

    //        Cliente localTrabalho = new Cliente();

    //        if (codigoEmpresa != string.Empty)
    //            localTrabalho = this.GetLocalDeTrabalho(cliente, codigoEmpresa);

    //        if (localTrabalho.Id == 0)
    //            return;

    //        ////////////////////////////////////////////////////////
    //        ///Código do Empregado
    //        ////////////////////////////////////////////////////////
    //        Empregado empregado = this.GetEmpregadoByCodigo(localTrabalho, codigoEmpregado);

    //        if (empregado.Id == 0)
    //            return;

    //        ////////////////////////////////////////////////////////
    //        ///Código da Função
    //        ////////////////////////////////////////////////////////
    //        Funcao funcao = new Funcao();

    //        if (codigoFuncao != "0" || codigoFuncao != string.Empty)
    //        {
    //            ArrayList listFuncao = funcao.Find("IdCliente=" + localTrabalho.Id + " AND CodigoFuncao='" + codigoFuncao.Replace("'", "") + "'");

    //            if (listFuncao.Count > 1)
    //                funcao = (Funcao)listFuncao[0];
    //        }
    //        else
    //            funcao.Id = EmpregadoFuncao.GetEmpregadoFuncao(localTrabalho, empregado).nID_FUNCAO.Id;

    //        //Para outras empresas ex: wickbold
    //        if (funcao.Id == 0 && (codigoFuncao != "0" || codigoFuncao != string.Empty))
    //            funcao = AddFuncaoFilial(localTrabalho, codigoFuncao);

    //        if (funcao.Id == 0)
    //            throw new Exception("Função não encontrada - CodigoFuncao (" + codigoFuncao + ")");

    //        ////////////////////////////////////////////////////////
    //        ///Código do Setor
    //        ////////////////////////////////////////////////////////
    //        Setor setor = new Setor();

    //        if (codigoSetor != "0" || codigoSetor != string.Empty)
    //            setor.Find("nID_EMPR=" + localTrabalho.Id + " AND tCOD_SETOR='" + codigoSetor.Replace("'", "") + "'");
    //        else
    //            setor.Id = EmpregadoFuncao.GetEmpregadoFuncao(localTrabalho, empregado).nID_SETOR.Id;

    //        if (setor.Id == 0)
    //            setor = AddSetorFilial(localTrabalho, codigoSetor);

    //        if (setor.Id == 0)
    //            throw new Exception("Setor não encontrado - CodigoSetor (" + codigoSetor + ")");

    //        ////////////////////////////////////////////////////////
    //        ///Classificação Funcional
    //        ////////////////////////////////////////////////////////

    //        DateTime dataInicio = GetDateTimeCol(new DateTime(), row, "Inicio");
    //        DateTime dataTermino = GetDateTimeCol(new DateTime(), row, "Termino");

    //        if (dataInicio == new DateTime())
    //            dataInicio = empregado.hDT_ADM;

    //        if (dataInicio == new DateTime())
    //            throw new Exception("Data Inicio é campo obrigatório! - DataInicio (" + dataInicio + ")");

    //        EmpregadoFuncao empregadoFuncao = new EmpregadoFuncao();

    //        empregadoFuncao.Find( "nID_EMPREGADO=" + empregado.Id
    //                            + " AND hDT_INICIO='" + dataInicio.ToString("yyyy-MM-dd") + "'"
    //                            + " AND nID_FUNCAO=" + funcao.Id
    //                            + " AND nID_SETOR=" + setor.Id);

    //        if (empregadoFuncao.Id == 0)
    //            empregadoFuncao.Inicialize();

    //        empregadoFuncao.nID_EMPREGADO.Id = empregado.Id;
    //        empregadoFuncao.nID_EMPR.Id = localTrabalho.Id;
    //        empregadoFuncao.nID_FUNCAO.Id = funcao.Id;
    //        empregadoFuncao.nID_SETOR.Id = setor.Id;
    //        empregadoFuncao.hDT_INICIO = dataInicio;
    //        empregadoFuncao.hDT_TERMINO = dataTermino;
    //        empregadoFuncao.nID_IMPORTACAO_AUTOMATICA.Id = this.Id;

    //        empregadoFuncao.Save(false);

    //        ////////////////////////////////////////////////////////
    //        ///GHE
    //        ////////////////////////////////////////////////////////
    //        string strGHE = Convert.ToString(GetValueCol(string.Empty, row, "CodigoGhe"));

    //        Ghe ghe = new Ghe();

    //        if (strGHE != string.Empty)
    //        {
    //            LaudoTecnico laudoTecnico = LaudoTecnico.GetUltimoLaudo(localTrabalho.Id, false);

    //            ghe.Find("nID_LAUD_TEC=" + laudoTecnico.Id
    //                + " AND (tNO_FUNC='" + strGHE
    //                + "' OR  LEFT(tNO_FUNC, 6) = '" + strGHE + "')");

    //            if (ghe.Id != 0)
    //            {
    //                GheEmpregado gheEmpregado = new GheEmpregado();

    //                gheEmpregado.Find("nID_EMPREGADO_FUNCAO=" + empregadoFuncao.Id
    //                    + " AND nID_LAUD_TEC=" + ghe.nID_LAUD_TEC.Id
    //                    + " AND nID_FUNC=" + ghe.Id);

    //                if (gheEmpregado.Id == 0)
    //                    gheEmpregado.Inicialize();

    //                gheEmpregado.nID_EMPREGADO_FUNCAO.Id = empregadoFuncao.Id;
    //                gheEmpregado.nID_LAUD_TEC.Id = ghe.nID_LAUD_TEC.Id;
    //                gheEmpregado.nID_FUNC.Id = ghe.Id;
    //                gheEmpregado.Save();
    //            }
    //        }
    //    }

    //    #endregion

    //    #region Afastamento

    //    public void ImportaRegistrosAfastamento(Cliente cliente, DataRow row)
    //    {
    //        ////////////////////////////////////////////////////////
    //        ///Código do Empresa
    //        ////////////////////////////////////////////////////////
    //        string codigoEmpresa = TratarCodigo(GetValueCol(string.Empty, row, "CodigoEmpresa"));

    //        Cliente localTrabalho = new Cliente();

    //        if (codigoEmpresa != string.Empty)
    //        {
    //            localTrabalho = this.GetLocalDeTrabalho(cliente, codigoEmpresa);

    //            if (localTrabalho.Id == 0)
    //                return;
    //        }

    //        ////////////////////////////////////////////////////////
    //        ///CodigoEmpregado
    //        ////////////////////////////////////////////////////////
    //        string codigoEmpregado = TratarCodigo(GetValueCol(string.Empty, row, "CodigoEmpregado"));

    //        Empregado empregado;

    //        if (codigoEmpresa == string.Empty)
    //            empregado = this.GetEmpregadoByCodigo(cliente, codigoEmpregado);
    //        else
    //            empregado = this.GetEmpregadoByCodigo(localTrabalho, codigoEmpregado);

    //        if (empregado.Id == 0)
    //            return;

    //        int IndTipoAfastamento = 0;

    //        DateTime dataInicial = GetDateTimeCol(new DateTime(), row, "Inicial");

    //        int IdAfastamentoTipo = GetInt32Col(0, row, "IndTipoAfastamento");

    //        switch (IdAfastamentoTipo)
    //        {
    //            case 2: //01-ATIVO 
    //                return;
    //            case 23: //DEMISSÃO
    //                if (empregado.hDT_DEM == new DateTime())
    //                {
    //                    empregado.hDT_DEM = dataInicial;
    //                    empregado.Save();
    //                }
    //                return;
    //            case 4:  //AUX.ACIDENTE DE TRABALHO
    //            case 32: //RETORNO ACID.TRABALHO
    //            case 33: //RETORNO AUX.DOENÇA
    //                IndTipoAfastamento = (int)TipoAfastamento.Ocupacional;
    //                break;

    //            default:
    //                IndTipoAfastamento = (int)TipoAfastamento.Assistencial;
    //                break;
    //        }

    //        Afastamento afastamento = new Afastamento();
    //        afastamento.Find("IdEmpregado=" + empregado.Id
    //                        + " AND DataInicial='" + dataInicial.ToString("yyyy-MM-dd") + "'");

    //        if (afastamento.Id == 0)
    //        {
    //            afastamento.Inicialize();
    //            afastamento.IdEmpregado.Id = empregado.Id;
    //            afastamento.DataInicial = GetDateTimeCol(afastamento.DataInicial, row, "Inicial");
    //        }
    //        afastamento.IndTipoAfastamento = IndTipoAfastamento;
    //        afastamento.IdAfastamentoTipo.Id = IdAfastamentoTipo;
    //        afastamento.DataPrevista = GetDateTimeCol(afastamento.DataPrevista, row, "Previsao");
    //        afastamento.DataVolta = GetDateTimeCol(afastamento.DataVolta, row, "Retorno");
    //        afastamento.Save(true);
    //    }

    //    #endregion

    //    #region GetEmpregadoByCodigo

    //    private Empregado GetEmpregadoByCodigo(Cliente cliente, string codigoEmpregado)
    //    {
    //        StringBuilder str = new StringBuilder();
    //        str.Append("nID_EMPR=" + cliente.Id);
    //        str.Append(" AND tCOD_EMPR='" + codigoEmpregado + "'");

    //        ArrayList listEmpregado = new Empregado().Find(str.ToString());

    //        if (listEmpregado.Count == 0)
    //            return new Empregado();
    //        else
    //            return (Empregado)listEmpregado[0];
    //    }
    //    #endregion

    //    #region Metodos Dados

    //    private string GetOrigem(string destino)
    //    {
    //        string ret = string.Empty;

    //        DataRow[] rows = dsCampos.Tables[0].Select("Destino='" + destino + "'");

    //        if (rows.Length == 1)
    //            ret = Convert.ToString(rows[0][0]);

    //        return ret;
    //    }

    //    private Int32 GetInt32Col(int originalValue, DataRow row, string destino)
    //    {
    //        string col;

    //        if (dsCampos == null)
    //            col = destino;
    //        else
    //            col = GetOrigem(destino);

    //        int ret = 0;

    //        try
    //        {
    //            if (row.Table.Columns.IndexOf(col) != -1)
    //                ret = Convert.ToInt32(row[col]);
    //            else
    //                ret = originalValue;
    //        }
    //        catch
    //        {
    //            ret = originalValue;
    //        }

    //        return ret;
    //    }

    //    private DateTime GetDateTimeCol(DateTime originalValue, DataRow row, string destino)
    //    {
    //        DateTime ret;

    //        string col;

    //        if (dsCampos == null)
    //            col = destino;
    //        else
    //            col = GetOrigem(destino);

    //        try
    //        {
    //            if (row.Table.Columns.IndexOf(col) != -1)
    //            {
    //                if (row[col] != System.DBNull.Value)
    //                    ret = Convert.ToDateTime(row[col]);
    //                else
    //                {
    //                    if (originalValue != new DateTime())
    //                        ret = originalValue;
    //                    else
    //                        ret = new DateTime();
    //                }
    //            }
    //            else
    //                ret = originalValue;

    //            if (ret >= new DateTime(9999, 1, 1) || ret <= new DateTime(1900, 12, 31))
    //                ret = new DateTime();
    //        }
    //        catch
    //        {
    //            ret = originalValue;
    //        }

    //        return ret;
    //    }

    //    private string GetValueCol(string originalValue, DataRow row, string destino)
    //    {
    //        string col;
    //        string ret;

    //        if (dsCampos == null)
    //            col = destino;
    //        else
    //            col = GetOrigem(destino);

    //        try
    //        {
    //            if (row.Table.Columns.IndexOf(col) != -1)
    //            {
    //                if (row[col] != System.DBNull.Value)
    //                    ret = Convert.ToString(row[col]).Replace("''", string.Empty).Replace("'", string.Empty).Replace("    F", string.Empty).Trim();
    //                else
    //                    ret = originalValue;
    //            }
    //            else
    //                ret = originalValue;

    //            if (ret == "NULL")
    //                throw new Exception("Valor Nulo");
    //        }
    //        catch
    //        {
    //            ret = originalValue;
    //        }
    //        return ret;
    //    }

    //    private char GetValueColSimNao(string originalValue, DataRow row, string destino)
    //    {
    //        string val = GetValueCol(originalValue, row, destino);

    //        if(val.ToUpper() == "SIM" || val.ToUpper() == "S")
    //            return 'S';
    //        else
    //            return 'N';
    //    }

    //    private string TratarCodigo(string codigo)
    //    {
    //        try
    //        {
    //            if (codigo == string.Empty)
    //                return string.Empty;
    //            else
    //            {
    //                long result;

    //                Int64.TryParse(Ilitera.Common.Utility.RemoveAcentosECaracteresEspeciais(codigo), out result);

    //                if (result == 0)
    //                    return codigo;
    //                else
    //                    return result.ToString();
    //            }
    //        }
    //        catch
    //        {
    //            return string.Empty;
    //        }
    //    }

    //    private Int64 GetInt64(string numPis)
    //    {
    //        try
    //        {
    //            return Int64.Parse(Ilitera.Common.Utility.RemoveAcentosECaracteresEspeciais(numPis));
    //        }
    //        catch
    //        {
    //            return 0L;
    //        }
    //    }

    //    private Single GetSingle(string val)
    //    {
    //        try
    //        {
    //            return Single.Parse(Ilitera.Common.Utility.RemoveAcentosECaracteresEspeciais(val));
    //        }
    //        catch
    //        {
    //            return 0F;
    //        }
    //    }

    //    #endregion

    //    #region Metodos Estaticos

    //    public static void WriteXmlToFile(DataSet thisDataSet, string filename)
    //    {
    //        if (thisDataSet == null) { return; }

    //        System.IO.FileStream myFileStream = new System.IO.FileStream
    //            (filename, System.IO.FileMode.Create);

    //        System.Xml.XmlTextWriter myXmlWriter =
    //            new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode);

    //        thisDataSet.WriteXml(myXmlWriter);

    //        myXmlWriter.Close();
    //    }

    //    private static void ExcecuteSQLCommand(string strSQL)
    //    {
    //        string m_connectionString = "data source=" + Ilitera.Data.SQLServer.EntitySQLServer.Server + ";"
    //                                    + "user id=" + Ilitera.Data.SQLServer.EntitySQLServer.User + ";"
    //                                    + "password=" + Ilitera.Data.SQLServer.EntitySQLServer.Password + ";"
    //                                    + "persist security info=False;"
    //                                    + "initial catalog=" + Ilitera.Data.SQLServer.EntitySQLServer.Database + ";"
    //                                    + "Packet Size=4096;"
    //                                    + "Connect Timeout = 36000";

    //        using (SqlConnection cnn = new SqlConnection(m_connectionString))
    //        {
    //            cnn.Open();

    //            SqlCommand cmd = new SqlCommand();

    //            cmd.CommandText = strSQL;
    //            cmd.Connection = cnn;
    //            cmd.CommandTimeout = 36000;
    //            cmd.ExecuteNonQuery();

    //            cnn.Close();
    //        }
    //    }

    //    private static DateTime GetDateTime(string strDateTime)
    //    {
    //        int year = DateTime.Now.Year;
    //        int month = DateTime.Now.Month;
    //        int day = DateTime.Now.Day;
    //        int hour = DateTime.Now.Hour;
    //        int minute = DateTime.Now.Minute;
    //        int second = DateTime.Now.Second;

    //        try
    //        {
    //            year = Convert.ToInt32(strDateTime.Substring(0, 4));
    //            month = Convert.ToInt32(strDateTime.Substring(4, 2));
    //            day = Convert.ToInt32(strDateTime.Substring(6, 2));
    //            hour = Convert.ToInt32(strDateTime.Substring(8, 2));
    //            if (strDateTime.Length > 10)
    //                minute = Convert.ToInt32(strDateTime.Substring(10, 2));
    //            if (strDateTime.Length > 12)
    //                second = Convert.ToInt32(strDateTime.Substring(12, 2));
    //        }
    //        catch { }

    //        return new DateTime(year, month, day, hour, minute, second);
    //    }

    //    #endregion

    //    #endregion
    }
}
