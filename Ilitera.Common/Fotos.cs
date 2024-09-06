using System;
using System.IO;
using System.Drawing;
using System.Web.Services.Discovery;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Net;

using Ilitera.Data;

namespace Ilitera.Common
{
    #region interface IFoto

    public interface IFoto
    {
        string FotoDiretorio
        {
            get;
        }
        string FotoInicio
        {
            get;
        }
        string FotoTermino
        {
            get;
        }
        byte FotoTamanho
        {
            get;
        }
        string FotoExtensao
        {
            get;
        }
    }
    #endregion

    public abstract class Fotos
    {
        public static bool IsRemoto = false;   //testar - Wagner

        public const string UrlFotos = "";
        public const string HttpFotos = "";

        private const string MapLocalDrive = EnvironmentUtitity.NetworkDriveFotos;

        #region string GetRaizPath

        public static string GetRaizPath()
        {
            return EnvironmentUtitity.GetRaizPathFoto();
        }
        #endregion

        #region string GetFolderClient

        private static string GetFolderClient()
        {
            return EnvironmentUtitity.GetShareFolder(EnvironmentUtitity.ShareFolders.DocsDigitais);
        }
        #endregion

        #region Bitmap GetImageFoto

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public static Bitmap GetImageFoto(IFoto iFoto, int FotoNumero)
        {
            return GetImageFoto(PathFoto(iFoto, FotoNumero));
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public static Bitmap GetImageFoto(string sPathFoto)
        {
            Bitmap bitmap = null;

            sPathFoto = PathFoto_Uri(sPathFoto);

            if (sPathFoto == string.Empty)
                return bitmap;

            //if (IsRemoto)
            //{
                Uri uri = new Uri(sPathFoto);

                sPathFoto = uri.ToString();

                DiscoveryClientProtocol discover = new DiscoveryClientProtocol();

                using (Stream stream = discover.Download(ref sPathFoto))
                {
                    bitmap = new Bitmap(stream);
                    stream.Close();
                }
            //}
            //else
            //{
            //    using (StreamReader streamReader = new StreamReader(sPathFoto))
            //    {
            //        bitmap = new Bitmap(streamReader.BaseStream);
            //        streamReader.Close();
            //    }
            //}

            return bitmap;
        }
        #endregion

        #region byte[] GetByteFoto


        public static byte[] GetByteFoto_Uri(string sURIFoto)
        {
            string xLoc = "";
            string xLoc2 = "";
            WebClient webclient = new WebClient();
            byte[] bytesArray = null;


            xLoc = sURIFoto.Replace("\\", "/");


            //xLoc2 = xLoc.Replace("I:", "http://187.45.232.35/driveI");

            //xLoc2 = xLoc.Replace("I:", "http://ilitera.dyndns.ws:8888/driveI");
            //xLoc2 = xLoc.Replace("I:", "http://54.94.157.244/driveI");
            //xLoc2 = xLoc.Replace("I:", "http://www.ilitera.net.br/driveI");

            
            xLoc2 = xLoc.Replace("http://www.ilitera.net.br/driveI", "I:");
            xLoc2 = xLoc2.Replace("/", "\\");



            bytesArray = webclient.DownloadData(xLoc2);


            return bytesArray;

        }




        public static byte[] GetByteFoto_Uri(string sURIFoto, string xWeb_Server)
        {
            string xLoc = "";
            string xLoc2 = "";
            WebClient webclient = new WebClient();
            byte[] bytesArray = null;


            xLoc = sURIFoto.Replace("\\", "/");
                        

            if (xWeb_Server == "ILITERA")
               xLoc2 = xLoc.Replace("I:", "http://ilitera.dyndns.ws:8888/driveI");
            else
                //xLoc2 = xLoc.Replace("I:", "http://54.94.157.244/driveI");
                //xLoc2 = xLoc.Replace("I:", "http://www.ilitera.net.br/driveI");
            xLoc2 = xLoc.Replace("http://www.ilitera.net.br/driveI", "I:");
            xLoc2 = xLoc2.Replace("/", "\\");

            bytesArray = webclient.DownloadData(xLoc2);


            return bytesArray;

        }

        public static byte[] GetByteFoto(string sPathFoto)
        {
            if (sPathFoto == string.Empty ) //|| IsRemoto)  //Wagner
                return null;

            byte[] bytesArray = null;

            Bitmap bitmap = null;

            sPathFoto = PathFoto_Uri(sPathFoto);
                     
            using (System.IO.StreamReader sr = new System.IO.StreamReader(sPathFoto))
            {
                bitmap = new Bitmap(sr.BaseStream);


                if (sPathFoto.IndexOf("LOW") < 0)
                {
                    if (bitmap.Size.Width > 900)
                    {
                        float zIndice = 2;
                        zIndice = bitmap.Size.Width / 900;

                        bitmap = BitmapManipulator.ResizeBitmap(bitmap,
                                                                (int)(bitmap.Size.Width / zIndice),
                                                                (int)(bitmap.Size.Height / zIndice));
                    }
                }
                else
                {
                    if (bitmap.Size.Width > 400)
                    {
                        float zIndice = 2;
                        zIndice = bitmap.Size.Width / 400;

                        bitmap = BitmapManipulator.ResizeBitmap(bitmap,
                                                                (int)(bitmap.Size.Width / zIndice),
                                                                (int)(bitmap.Size.Height / zIndice));
                    }
                }


                MemoryStream stream = new MemoryStream();

                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

                stream.Flush();

                bytesArray = stream.ToArray();

                sr.Close();
            }
            return bytesArray;
        }
        #endregion

        #region string PathFoto




        public static string PathSmallFoto_Auditoria(IFoto iFoto, int FotoNumero)
        {
            string sPathFoto = PathFoto(iFoto, FotoNumero);

            return PathSmallFoto_Auditoria(sPathFoto);
        }

        public static string PathSmallFoto_Auditoria(string sPathFoto)
        {
            string extensao = sPathFoto.Substring(sPathFoto.Length - 4);
            string fileLow = sPathFoto;

            if (sPathFoto.IndexOf("LOW") < 0)
                fileLow = sPathFoto.Substring(0, sPathFoto.Length - 4) + "LOW" + extensao;

            FileInfo fileInfo = new FileInfo(fileLow);

            //if (IsRemoto || !fileInfo.Exists)
            //    return sPathFoto;

            Bitmap workingBitmap;

            if (!fileInfo.Exists)
            {
                //FileInfo fileInfo2 = new FileInfo(sPathFoto);
                //if (fileInfo2.Exists)
                //{
                float zIndice = 2;

                workingBitmap = new Bitmap(sPathFoto);

                if (workingBitmap.Size.Width > 600)
                {
                    zIndice = workingBitmap.Size.Width / (float)512;
                }

                workingBitmap = BitmapManipulator.ResizeBitmap(workingBitmap,
                                                                (int)(workingBitmap.Size.Width / zIndice),
                                                                (int)(workingBitmap.Size.Height / zIndice));
                workingBitmap.Save(fileLow);
                //}
            }
            return fileLow;
        }



        public static string PathFoto_Uri(IFoto iFoto, int FotoNumero)
        {
            
            string fileName = GetFileName(iFoto, FotoNumero);

            string diretorio = System.IO.Path.Combine(MapLocalDrive + @"\", iFoto.FotoDiretorio.Substring( iFoto.FotoDiretorio.IndexOf("I:")));

            string path = System.IO.Path.Combine(diretorio, fileName);

            return PathFoto_Uri(path);
        }

        public static string PathFoto_Certificado_Uri(string sUriFoto)
        {
            string xLoc = "";
            string xLoc2 = "";

            xLoc = sUriFoto.Replace("\\", "/");
            //xLoc2 = xLoc.Replace("I:", "http://187.45.232.35/driveI");

            //xLoc2 = xLoc.Replace("I:", "http://ilitera.dyndns.ws:8888/driveI");
            //xLoc2 = xLoc.Replace("I:", "http://54.94.157.244/driveI");
            //xLoc2 = xLoc.Replace("I:", "http://www.ilitera.net.br/driveI");
            xLoc2 = xLoc.Replace("http://www.ilitera.net.br/driveI", "I:");
            xLoc2 = xLoc2.Replace("/", "\\");

            if (xLoc2.ToUpper().IndexOf("LOWCERT") < 0)
            {
                xLoc2 = xLoc2.ToLower().Replace(".jpg", "LOWCERT.jpg");
                xLoc2 = xLoc2.ToLower().Replace(".png", "LOWCERT.png");
            }

            return xLoc2.ToString();
        }

        public static string PathFoto_Uri(string sUriFoto)
        {
            string xLoc = "";
            string xLoc2 = "";


            xLoc = sUriFoto.Replace("\\", "/");
            //xLoc2 = xLoc.Replace("I:", "http://187.45.232.35/driveI");


            //xLoc2 = xLoc.Replace("I:", "http://ilitera.dyndns.ws:8888/driveI");

            //xLoc2 = xLoc.Replace("I:", "http://54.94.157.244/driveI");
            //xLoc2 = xLoc.Replace("I:", "http://www.ilitera.net.br/driveI");
            xLoc2 = xLoc.Replace("http://www.ilitera.net.br/driveI", "I:");
            xLoc2 = xLoc2.Replace("/", "\\");

            return xLoc2.ToString();
        }


      



        public static string PathFoto_Certificado_Uri(string sUriFoto, string xWeb_Server)
        {
            string xLoc = "";
            string xLoc2 = "";

            xLoc = sUriFoto.Replace("\\", "/");


            //if (xWeb_Server == "ILITERA")
            //   xLoc2 = xLoc.Replace("I:", "http://ilitera.dyndns.ws:8888/driveI");
            //else
            //    xLoc2 = xLoc.Replace("I:", "http://www.ilitera.net.br/driveI");
            xLoc2 = xLoc.Replace("http://www.ilitera.net.br/driveI", "I:");
            xLoc2 = xLoc2.Replace("/", "\\");

            //xLoc2 = xLoc.Replace("I:", "http://54.94.157.244/driveI");


            xLoc2 = xLoc2.ToLower().Replace(".jpg", "LOWCERT.jpg");
            xLoc2 = xLoc2.ToLower().Replace(".png", "LOWCERT.png");

            return xLoc2.ToString();
        }


        public static string PathFoto_Uri(string sUriFoto, string xWeb_Server)
        {
            string xLoc = "";
            string xLoc2 = "";


            xLoc = sUriFoto.Replace("\\", "/");

            //if (xWeb_Server == "ILITERA")
            //   xLoc2 = xLoc.Replace("I:", "http://ilitera.dyndns.ws:8888/driveI");
            //else
            //xLoc2 = xLoc.Replace("I:", "http://www.ilitera.net.br/driveI");
            xLoc2 = xLoc.Replace( "http://www.ilitera.net.br/driveI", "I:");
            xLoc2 = xLoc2.Replace("/", "\\");

            //xLoc2 = xLoc.Replace("I:", "http://54.94.157.244/driveI");


            return xLoc2.ToString();
        }



        public static string PathFoto(string sPathFoto)
        {
            StringBuilder str = new StringBuilder();
            str.Append(sPathFoto);

            if (sPathFoto == string.Empty)
                return sPathFoto;

            if (IsRemoto)
            {
                str.Replace(MapLocalDrive, HttpFotos);
                str.Replace(GetFolderClient(), HttpFotos);
                str.Replace( @"\", @"/");
            }

            if (Table.IsWeb)
                str.Replace(MapLocalDrive, GetFolderClient());

            return str.ToString();
        }

        public static string PathFoto(IFoto iFoto, int FotoNumero)
        {
            string fileName = GetFileName(iFoto, FotoNumero);

            string diretorio = System.IO.Path.Combine(MapLocalDrive + @"\", iFoto.FotoDiretorio);

            string path = System.IO.Path.Combine(diretorio, fileName);

            return PathFoto(path);
        }

        #endregion

        #region string PathSmallFoto

        public static string PathNormalFoto(IFoto iFoto, int FotoNumero)
        {
            string sPathFoto = PathFoto(iFoto, FotoNumero);

            return sPathFoto;
        }


        public static string PathSmallFoto(IFoto iFoto, int FotoNumero)
        {
            string sPathFoto = PathFoto(iFoto, FotoNumero);

            return PathSmallFoto(sPathFoto);
        }

        public static string PathSmallFoto(string sPathFoto)
        {
            string extensao = sPathFoto.Substring(sPathFoto.Length - 4);
            string fileLow = sPathFoto.Substring(0, sPathFoto.Length - 4) + "LOW" + extensao;

            FileInfo fileInfo = new FileInfo(fileLow);

            if (IsRemoto && !fileInfo.Exists)
                return sPathFoto;

            //Bitmap workingBitmap;

            //if (!fileInfo.Exists)
            //{
            //    workingBitmap = new Bitmap(sPathFoto);
            //    workingBitmap = BitmapManipulator.ResizeBitmap(workingBitmap,
            //                                                    workingBitmap.Size.Width / 2,
            //                                                    workingBitmap.Size.Height / 2);
            //    workingBitmap.Save(fileLow);
            //}
            return fileLow;
        }


        public static string PathSmallFoto_Uri(IFoto iFoto, int FotoNumero)
        {
            string sPathFoto = PathFoto(iFoto, FotoNumero);

            return PathSmallFoto_Uri(sPathFoto);
        }

        public static string PathSmallFoto_Uri(string sPathFoto)
        {
            string extensao = sPathFoto.Substring(sPathFoto.Length - 4);
            string fileLow = sPathFoto.Substring(0, sPathFoto.Length - 4) + "LOW" + extensao;

            return PathFoto_Uri(fileLow);
        }

        #endregion

        #region string UrlFoto

        public static string UrlFoto(string sPath)
        {
            StringBuilder sPathFoto = new StringBuilder();

            sPathFoto.Append(UrlFotos)
                     .Append(sPath)
                     .Replace(MapLocalDrive, string.Empty)
                     .Replace(GetFolderClient(), string.Empty)
                     .Replace(@"\", @"/");

            return sPathFoto.ToString();
        }
        #endregion

        #region Upload Metodos

        public static void UploadFile(string pasta, string filename)
        {
            string address = UrlFotos + pasta;

            DiscoveryClientProtocol discover = new DiscoveryClientProtocol();

            System.Net.WebClient webCliente = new System.Net.WebClient();

            webCliente.UploadFile(address, "POST", filename);
        }

        public static void UploadWriteToFile(IFoto foto, short FotoNumero, ref byte[] Buffer)
        {
            string strPath = Fotos.PathFoto(foto, FotoNumero);
            // Create a file
            FileStream newFile = new FileStream(strPath, FileMode.Create);
            // Write data to the file
            newFile.Write(Buffer, 0, Buffer.Length);
            // Close file
            newFile.Close();
        }

        public static void UploadWriteToFile(string strPath, ref byte[] Buffer)
        {
            // Create a file
            FileStream newFile = new FileStream(strPath, FileMode.Create);
            // Write data to the file
            newFile.Write(Buffer, 0, Buffer.Length);
            // Close file
            newFile.Close();
        }
        #endregion

        #region Outros Metodos

        public static string GetFileName(IFoto iFoto, int FotoNumero)
        {
            string file = iFoto.FotoInicio
                        + FotoNumero.ToString(FormatarTamanho(iFoto.FotoTamanho))
                        + iFoto.FotoTermino
                        + iFoto.FotoExtensao;

            return file;
        }

        public static string GetNumeroFoto(int numFoto, int numCasas)
        {
            string ret = numFoto.ToString();

            return ret.PadLeft(numCasas, '0');
        }

        public static string Diretorio(string strPath)
        {
            return strPath.Substring(0, TamanhoDiretorio(strPath));
        }

        public static string Inicio(string strPath)
        {
            string retorno = string.Empty;

            int inicioStrRetorno = TamanhoDiretorio(strPath);

            int fimStrRetorno = 0;

            while (IsNumber(strPath[inicioStrRetorno + fimStrRetorno]) != true)
                fimStrRetorno++;

            retorno = strPath.Substring(inicioStrRetorno, fimStrRetorno);

            return retorno;
        }

        private static int TamanhoDiretorio(string strPath)
        {
            int length = strPath.Length;

            while (!(strPath[length - 1].Equals('\\') || strPath[length - 1].Equals('/')))
                length--;

            return length;
        }

        public static string Termino(string strPath)
        {
            string retorno = string.Empty;

            int inicioStrRetorno = strPath.Length;

            int fimStrRetorno = strPath.Length;

            while (IsNumber(strPath[inicioStrRetorno - 1]) != true)
                inicioStrRetorno--;

            while (!strPath[fimStrRetorno - 1].Equals('.'))
                fimStrRetorno--;

            retorno = strPath.Substring(inicioStrRetorno, (fimStrRetorno - inicioStrRetorno) - 1);

            return retorno;
        }

        public static string Extensao(string strPath)
        {
            string retorno = string.Empty;

            int inicioStrRetorno = strPath.Length;

            while (!strPath[inicioStrRetorno - 1].Equals('.'))
                inicioStrRetorno--;

            retorno = strPath.Substring(inicioStrRetorno - 1);

            return retorno;
        }

        public static byte Tamanho(string strPath)
        {
            int tamDir = TamanhoDiretorio(strPath);

            string nomeArquivo = strPath.Substring(tamDir);

            int qtdCasas = 0;

            for (int i = 0; i < nomeArquivo.Length; i++)
            {
                if (IsNumber(nomeArquivo[i]))
                    qtdCasas++;
            }
            return (byte)qtdCasas;
        }

        public static short Numero(string strPath)
        {
            int tamDir = TamanhoDiretorio(strPath);

            string nomeArquivo = strPath.Substring(tamDir);
            string numeroRetorno = string.Empty;

            for (int i = 0; i < nomeArquivo.Length; i++)
            {
                if (IsNumber(nomeArquivo[i]))
                    numeroRetorno = numeroRetorno + nomeArquivo[i].ToString();
            }

            return Convert.ToInt16(numeroRetorno);
        }

        public static string FormatarTamanho(byte num)
        {
            System.Text.StringBuilder ret = new System.Text.StringBuilder();

            for (int i = 0; i < num; i++)
                ret.Append("0");

            return ret.ToString();
        }

        private static bool IsNumber(char str)
        {
            bool strVal = false;

            switch (str)
            {
                case '0': strVal = true;
                    break;
                case '1': strVal = true;
                    break;
                case '2': strVal = true;
                    break;
                case '3': strVal = true;
                    break;
                case '4': strVal = true;
                    break;
                case '5': strVal = true;
                    break;
                case '6': strVal = true;
                    break;
                case '7': strVal = true;
                    break;
                case '8': strVal = true;
                    break;
                case '9': strVal = true;
                    break;
            }
            return strVal;
        }
        #endregion
    }
}
