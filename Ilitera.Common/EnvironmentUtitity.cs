using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Ilitera.Common
{
    public static class EnvironmentUtitity
    {
        public const string NetworkDriveFotos = @"I:";
        public const string Domain = "";
        public const string AdminUserName = "";
        public const string AdminPassword = "";
        public const string SmtpInterno = "";
        private const string Root = @"I:\fotos\";
        private const string RootLocal = @"I:\fotos\";
        public const string FolderFtp = "";
        public const string FolderQueue = "";
        public const string FolderReceiveMail = "";
        public const string FolderSendMail = "";
        public const string FolderLuceneIndex = "";
        public const string FolderCobrancaBancaria = "";
        public const string FolderContabilidadeComum = "";
        public const string FolderAudiograma = NetworkDriveFotos + "";
        public const string FolderErgonomia = NetworkDriveFotos + "";
        public const string FolderLogoEmpresas = NetworkDriveFotos + "";
        public const string FolderFichasAgentesQuimicos = NetworkDriveFotos + "";
        public const string FileReportBI = "";
        public const string FileAsoPciEmBranco = "";
        public const string FileAssinaturaEmBranco = "";
        public const string UriPinEditor = ""; 
        public const string UriLoginSenhaClinica = "";

        public static string GetRaizPathFoto()
        {
            string RaizPath;

            if (Ilitera.Data.Table.IsWeb)
                RaizPath = GetShareFolder(ShareFolders.DocsDigitais);
            else
                RaizPath = NetworkDriveFotos + @"\";

            return RaizPath;
        }

        public static string GetPdfFileName(string reportName)
        {
            if (reportName == string.Empty)
                reportName = "report";

            return GetTempDirectory() + @"\" + reportName + ".pdf";
        }

        public static string GetTempDirectory()
        {
            string directory = Path.Combine(Path.GetTempPath() + @"\NetPDT", Guid.NewGuid().ToString());

            DirectoryInfo dir = new DirectoryInfo(directory);

            if (!dir.Exists)
                dir.Create();

            return dir.FullName;
        }

        public static void DeleteTempPath()
        {
            DirectoryInfo dir = new DirectoryInfo(Path.GetTempPath() + @"\NetPDT");

            if (dir.Exists)
                dir.Delete(true);
        }

        public enum ShareFolders : int
        {
            Administracao, 
            DocsDigitais,
            Arquitetura, 
            Contabilidade,
            Engenharia, 
            Juridico , 
            Marketing,
            Medicina, 
            Suporte,
            Treinamento
        }

        public static string GetRootFolder(ShareFolders folder)
        {
            string ret;

            switch (folder)
            {
                case ShareFolders.Administracao:
                case ShareFolders.Arquitetura:
                case ShareFolders.Contabilidade:
                case ShareFolders.Engenharia:
                case ShareFolders.Juridico:
                case ShareFolders.Medicina:
                case ShareFolders.Treinamento:
                    ret = RootLocal;
                    break;
                case ShareFolders.DocsDigitais:
                case ShareFolders.Marketing:
                case ShareFolders.Suporte:
                    ret = Root;
                    break;
                default:
                    ret = Root;
                    break;
            }

            return ret;
        }

        public static string[] GetShareFolders()
        {
            Array elements = Enum.GetValues(typeof(ShareFolders));

            string[] pastas = new string[elements.Length];

            int i = 0;

            foreach (ShareFolders folder in elements)
                pastas[i++] = GetShareFolder(folder); 

            return pastas;
        }

        public static string GetShareFolder(ShareFolders folder)
        {
            return GetRootFolder(folder) + folder.ToString() + @"\";
        }

        public static string[] GetSubFolders()
        {
            string[] subPastas = { "ImgSup", "DocsIN", "DocsProcess", "DocsOUT", "Outros" };

            return subPastas;
        }

        public enum SubFoldersFotos : int
        {
            Audiograma, 
            Auditoria, 
            CNPJ, 
            Fachada, 
            MapaRisco, 
            OrdemServico, 
            Organogramas, 
            Paradigmas, 
            Prontuario, 
            VasoPressao,
            BioSeg
        }

        public const string SubFolderTomadoras = ""; // "Tomadoras";

        public static string[] GetSubFoldersFotos()
        {
            Array elements = Enum.GetValues(typeof(SubFoldersFotos));

            string[] subPastas = new string[elements.Length];

            int i = 0;

            foreach(SubFoldersFotos folder in elements)
                subPastas[i++] = folder.ToString();

            return subPastas;
        }
    }
}
