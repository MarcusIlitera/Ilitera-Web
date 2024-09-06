using System;
using System.Data;
using System.Globalization;
using System.Collections;
using System.Text;

namespace Ilitera.Common
{
    public abstract class Utility
    {
        public delegate void EventProgress(int val);
        public delegate void EventProgressFinalizar();

        public static void FormatHtmlToCrystalSuport(StringBuilder strTexto)
        {
            //Corrige BOLD e Italico
            strTexto.Replace("<STRONG>", "<B>");
            strTexto.Replace("</STRONG>", "</B>");
            strTexto.Replace("<EM>", "<I>");
            strTexto.Replace("</EM>", "</I>");
        }

        public static string FormatHtmlToCrystalSuport(string sHtml)
        {
            StringBuilder strTexto = new StringBuilder();
            strTexto.Append(sHtml);

            FormatHtmlToCrystalSuport(strTexto);

            return strTexto.ToString();
        }

        public static string SubstituirQuebraLinhaPor(string text, string concat)
        {
            StringBuilder ret = new StringBuilder();

            ret.Append(text);
            ret.Replace("\r\n", concat);
            ret.Replace("\n", concat);

            try
            {
                if (ret.ToString().Substring(ret.Length - concat.Length, concat.Length) == concat)
                    ret.Remove(ret.Length - concat.Length, concat.Length);
            }
            catch { }

            return ret.ToString();
        }

        public static bool IsNull(object value)
        {
            bool ret;

            if (value == null || value == System.DBNull.Value)
                ret = true;
            else
            {
                if (value.GetType() == typeof(Int32))
                    ret = Convert.ToInt32(value) == 0;
                else if (value.GetType() == typeof(DateTime))
                    ret = Convert.ToDateTime(value) == new DateTime();
                else
                    ret = false;
            }

            return ret;
        }

        public static string TratarData(DateTime datetime)
        {
            if (datetime != new DateTime() && datetime != new DateTime(1753, 1, 1))
                return datetime.ToString("dd-MM-yyyy");
            else
                return string.Empty;
        }

        public static System.Object TratarDateTime(DateTime datetime)
        {
            if (datetime != new DateTime() && datetime != new DateTime(1753, 1, 1))
                return datetime;
            else
                return System.DBNull.Value;
        }

        public static DateTimeFormatInfo GetDataTimeFormat()
        {
            DateTimeFormatInfo myDTFI = new CultureInfo("pt-BR", false).DateTimeFormat;
            myDTFI.TimeSeparator = "-";
            myDTFI.ShortDatePattern = "dd-MM-yyyy";
            return myDTFI;
        }

        public static DateTime GetDateTime(DateTime tempDate, string strTime)
        {
            DateTime tempTime;
            try
            {
                tempTime = DateTime.Parse(strTime);
            }
            catch
            {
                tempTime = new DateTime();
            }
            return new DateTime(tempDate.Year, tempDate.Month, tempDate.Day, tempTime.Hour, tempTime.Minute, 0);
        }

        public static bool intersects(DateTime r1Start, DateTime r1End, DateTime r2Start, DateTime r2End)
        {
            return (r1Start == r2Start) || (r1Start > r2Start ? r1Start <= r2End : r2Start <= r1End);
        }

        public static string[] ConvertToStringArray(System.Array values)
        {
            string[] theArray = new string[values.Length];

            for (int i = 1; i <= values.Length; i++)
            {
                if (values.GetValue(1, i) == null)
                    theArray[i - 1] = "";
                else
                    theArray[i - 1] = (string)values.GetValue(1, i).ToString();
            }

            return theArray;
        }

        public static string GetMesExtenso(int mes)
        {
            string stringMes = string.Empty;

            switch (mes)
            {
                case 1:
                    stringMes = "Janeiro";
                    break;
                case 2:
                    stringMes = "Fevereiro";
                    break;
                case 3:
                    stringMes = "Março";
                    break;
                case 4:
                    stringMes = "Abril";
                    break;
                case 5:
                    stringMes = "Maio";
                    break;
                case 6:
                    stringMes = "Junho";
                    break;
                case 7:
                    stringMes = "Julho";
                    break;
                case 8:
                    stringMes = "Agosto";
                    break;
                case 9:
                    stringMes = "Setembro";
                    break;
                case 10:
                    stringMes = "Outubro";
                    break;
                case 11:
                    stringMes = "Novembro";
                    break;
                case 12:
                    stringMes = "Dezembro";
                    break;
            }
            return stringMes;
        }

        public static DateTime PrimeiroDiaMes(DateTime data)
        {
            return new DateTime(data.Year, data.Month, 1);
        }

        public static DateTime UltimoDiaMes(DateTime data)
        {
            DateTime dataPart = new DateTime(data.AddMonths(1).Year,
                                                data.AddMonths(1).Month, 1);
            return dataPart.AddDays(-1);
        }

        public static string PrimeiraMaiuscula(string strCaractere)
        {
            StringBuilder ret = new StringBuilder();

            char[] seps = { ' ' };

            String[] values = strCaractere.Split(seps);

            foreach (string str in values)
            {
                if (str.Length == 0)
                    continue;
                else if (str.Substring(0, 1) == "(")
                    ret.Append(str + " ");
                else if (str == "de")
                    ret.Append(str + " ");
                else if (str.Length == 1)
                    ret.Append(str.ToUpper());
                else
                    ret.Append(str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1).ToLower() + " ");
            }

            return ret.ToString().TrimEnd();
        }

        public static string GetTamanhoDoArquivo(long Length)
        {
            string ret;

            if (Length > 1024 * 1024 * 1024)
            {
                double total = Convert.ToDouble(Length) / 1024D / 1024D / 1024D;

                ret = total.ToString("n") + " GB";
            }
            else if (Length > 1024 * 1024)
            {
                double total = Convert.ToDouble(Length) / 1024D / 1024D;

                ret = total.ToString("n") + " MB";
            }
            else if (Length > 1024)
            {
                ret = Convert.ToString(Length / 1024) + " KB";
            }
            else
                ret = Convert.ToString(Length) + " bytes";

            return ret;
        }

        public static string OnlyFirstLetterUpper(string strSentence)
        {
            StringBuilder ret = new StringBuilder();

            ret.Append(strSentence.Substring(0, 1).ToUpper());
            ret.Append(strSentence.Substring(1));

            return ret.ToString();
        }

        public static string RemoveEspacosDuplos(string original)
        {
            StringBuilder str = new StringBuilder(original);

            while (str.ToString().IndexOf("  ") != -1)
                str.Replace("  ", " ");

            return str.ToString(); ;
        }

        public static string RemoveAcentosECaracteresEspeciais(string original)
        {
            return RemoveEspacosDuplos(RemoveCaracteresEspeciais(RemoveAcentos(original)));
        }

        public static string RemoveCaracteresEspeciais(string original)
        {
            StringBuilder str = new StringBuilder();

            str.Append(original);

            char[] especiais = { '`', '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '=', '+', '[', ']', '{', '}', '\\', '|', ';', ':', '\'', '"', ',', '<', '.', '>', '/', '?' };

            foreach (char esp in especiais)
                str.Replace(esp, '@');

            str.Replace("@", " ");

            return str.ToString();
        }

        public static string RemoveAcentos(string original)
        {
            StringBuilder str = new StringBuilder();

            str.Append(original);

            char[] vogais_A = { 'Á', 'À', 'Â', 'Ã' };
            char[] vogais_a = { 'á', 'à', 'â', 'ã', 'ª' };
            char[] vogais_E = { 'É', 'È', 'Ê' };
            char[] vogais_e = { 'é', 'è', 'ê' };
            char[] vogais_I = { 'Í', 'Ì', 'Î' };
            char[] vogais_i = { 'í', 'ì', 'î' };
            char[] vogais_O = { 'Ó', 'Ò', 'Ô', 'Õ' };
            char[] vogais_o = { 'ó', 'ò', 'ô', 'õ' };
            char[] vogais_U = { 'Ú', 'Ù', 'Û' };
            char[] vogais_u = { 'ú', 'ù', 'û' };

            foreach (char A in vogais_A)
                str.Replace(A, 'A');
            foreach (char a in vogais_a)
                str.Replace(a, 'a');

            foreach (char E in vogais_E)
                str.Replace(E, 'E');
            foreach (char e in vogais_e)
                str.Replace(e, 'e');

            foreach (char I in vogais_I)
                str.Replace(I, 'I');
            foreach (char i in vogais_i)
                str.Replace(i, 'i');

            foreach (char O in vogais_O)
                str.Replace(O, 'O');
            foreach (char o in vogais_o)
                str.Replace(o, 'o');

            foreach (char U in vogais_U)
                str.Replace(U, 'U');
            foreach (char u in vogais_u)
                str.Replace(u, 'u');

            str.Replace('ç', 'c');
            str.Replace('Ç', 'C');

            return str.ToString();
        }

        public static bool IsURL(string strURL)
        {
            //Validates a URL 

            string strRegex = @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$";

            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(strRegex);

            if (!re.IsMatch(strURL))
                return (false);

            return (true);
        }

        public static Int32 GetInt32(Object value)
        {
            int ret = 0;

            Int32.TryParse(Convert.ToString(value), out ret);

            return ret;
        }

        public static Int32 GetInt32(DataRow row, string col)
        {
            int ret = 0;

            Int32.TryParse(Convert.ToString(row[col]), out ret);

            return ret;
        }

    }
}
