using System;
using System.Net;
using System.IO;
using System.Text;

namespace Ilitera.Common
{
	public class BuscaWebPage
	{
		System.Uri uri;

		public BuscaWebPage(string url)
		{
			uri = new System.Uri(url);
		}

		public string GetPage()
		{
			HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(uri);

            HttpWebResponse myWebResponse=(HttpWebResponse)myWebRequest.GetResponse();

			Stream ReceiveStream = myWebResponse.GetResponseStream();
                
			Encoding encode = System.Text.Encoding.GetEncoding("iso-8859-1");

			StreamReader readStream = new StreamReader(ReceiveStream, encode);
			Char[] read = new Char[256];

			StringBuilder page = new StringBuilder();

			int count = readStream.Read(read, 0, 256);

			while (count > 0) 
			{
				String str = new String(read, 0, count);
				page.Append(str);
				count = readStream.Read(read, 0, 256);
			}

			readStream.Close();
			myWebResponse.Close();

			return page.ToString();
		}
	}
}
