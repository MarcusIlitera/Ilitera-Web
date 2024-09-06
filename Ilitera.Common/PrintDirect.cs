using System;
using System.Collections;
using System.Text;
using System.Runtime.InteropServices;

namespace Ilitera.Common 
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DOCINFO
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pDocName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pOutputFile;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pDataType;
    }

	/// <summary>
	/// EXAMPLE:
	///		using( PrintingLibrary.PrintDirect pt = new PrintingLibrary.PrintDirect(null) )
	///		{
	///			ArrayList lines = new ArrayList();
	///			lines.Add("Line 1");
	///			lines.Add("line2");			
	///			pt.PrintDocument(lines);
	///		}
	/// </summary>
	public class PrintDirect : IDisposable
	{

		// Track whether Dispose has been called.
		private bool disposed = false;

		DOCINFOA di;
		System.IntPtr lhPrinter;
		string PrinterName = "";

//		ESC ( t 3 0 0 0 0 Assigns the italic table to active Table 0.
//		ESC ( t 3 0 1 1 0 Assigns the PC437 (US) table to active Table 1.
//		ESC ( t 3 0 2 8 0 Assigns the PC865 (Canada-French) Table to active Table 2.
//		ESC ( t 3 0 3 3 0 Assigns the PC850 (Multilingual) table to active Table 3.

		public static string INICIALIZA		= (char)27+"@"; 
		public static string PAGE_LENGTH	= (char)27+"C54";
		public static string LINHA_1_8		= (char)27+"0";
		public static string LINHA_7_72		= (char)27+"1";
		public static string LINHA_1_6		= (char)27+"2";
		public static string CHARACTER		= (char)27+"(t303250"; 
		public static string BELL			= Convert.ToString((char)7);		
		public static string ESC			= Convert.ToString((char)27);
		public static string NOVA_PAGINA	= Convert.ToString((char)12);
		public static string DRAF			= (char)27+"x0";
		public static string NLQ			= (char)27+"x1";
		public static string CONDENSED		= Convert.ToString((char)15);
		public static string DES_CONDENSED	= Convert.ToString((char)18);
		public static string SANS_SERIF		= (char)27+"k1";
		public static string ROMAN			= (char)27+"k0";
		public static string SELECT_10CPI	= (char)27+"P";
		public static string SELECT_12CPI	= (char)27+"M";
		public static string TAMANHO_PAG	= (char)27+"C011";
		public static string MARGEM_ESQ		= (char)27+"15";
		public static string MARGEM_DIR		= (char)27+"Q15";
		public static string LF				= Convert.ToString((char)10);


		public PrintDirect(string PrinterName)
		{
			if( PrinterName != null && PrinterName != "" )
				this.PrinterName = PrinterName;

			lhPrinter=new System.IntPtr();

			di = new DOCINFOA();
			di.pDocName="MINI-MI Document";
			di.pDataType="RAW";

			PrintDirect.OpenPrinter(this.PrinterName, out lhPrinter,0);
			if( lhPrinter == System.IntPtr.Zero )
				throw new Exception("Printer " + this.PrinterName + " not open/found!" );
			PrintDirect.StartDocPrinter(lhPrinter, 1, di);
			PrintDirect.StartPagePrinter(lhPrinter);
		}//constructor

		// Implement Idisposable.
		// Do not make this method virtual.
		// A derived class should not be able to override this method.
		public void Dispose()
		{
			Dispose(true);
			// Take yourself off of the Finalization queue 
			// to prevent finalization code for this object
			// from executing a second time.
			GC.SuppressFinalize(this);
		}


		protected virtual void Dispose(bool disposing)
		{
			try
			{
				// Check to see if Dispose has already been called.
				if(!this.disposed)
				{
					// If disposing equals true, dispose all managed 
					// and unmanaged resources.
					if(disposing)
					{
						PrintDirect.ClosePrinter(lhPrinter);
					}
				}
			}
			catch (Exception ex)
			{//do nothing
				ex.ToString();
			}
			disposed = true;         
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        
        public void PrintDocument(string  stringToPrint)
		{
			try
			{
				IntPtr pBytes;
				Int32 dwCount;
				dwCount = stringToPrint.Length;
				pBytes = Marshal.StringToCoTaskMemAnsi(stringToPrint);
				SendBytesToPrinter(this.PrinterName, pBytes, dwCount);
				Marshal.FreeCoTaskMem(pBytes);
				PrintDirect.EndPagePrinter(lhPrinter);
				PrintDirect.EndDocPrinter(lhPrinter);
			}
			catch( Exception ex )
			{
				throw ex;
			}	
		}

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public void PrintDocument( ArrayList Lines )
		{
			try
			{
				IntPtr pBytes;
				Int32 dwCount;
				// How many characters are in the string?

				foreach( string line in Lines )
				{
					string lineToPrint = line + "\r\n";			

					dwCount = lineToPrint.Length;
					// Assume that the printer is expecting ANSI text, and then convert
					// the string to ANSI text.
					pBytes = Marshal.StringToCoTaskMemAnsi(lineToPrint);
					// Send the converted ANSI string to the printer.
					SendBytesToPrinter(this.PrinterName, pBytes, dwCount);
					Marshal.FreeCoTaskMem(pBytes);
				}//foreach

				PrintDirect.EndPagePrinter(lhPrinter);
				PrintDirect.EndDocPrinter(lhPrinter);
			}
			catch( Exception ex )
			{
				throw ex;
			}	
		}//PrintDocument

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public static bool SendBytesToPrinter( string szPrinterName, IntPtr pBytes, Int32 dwCount)
		{
			Int32		dwError = 0, dwWritten = 0;
			IntPtr		hPrinter = new IntPtr(0);
			DOCINFOA	di = new DOCINFOA();
			bool    bSuccess = false; // Assume failure unless you specifically succeed.

			di.pDocName	= "My C#.NET RAW Document";
			di.pDataType = "RAW";

			// Open the printer.
			if( OpenPrinter( szPrinterName, out hPrinter, 0 ) )
			{
				// Start a document.
				if( StartDocPrinter(hPrinter, 1, di) )
				{
					// Start a page.
					if( StartPagePrinter(hPrinter) )
					{
						// Write your bytes.
						bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
						EndPagePrinter(hPrinter);
					}
					EndDocPrinter(hPrinter);
				}
				ClosePrinter(hPrinter);
			}
			// If you did not succeed, GetLastError may give more information
			// about why not.
			if( bSuccess == false )
			{
				dwError = Marshal.GetLastWin32Error();
			}
			return bSuccess;
		}

		// Structure and API declarions:
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
			public class DOCINFOA
		{
			[MarshalAs(UnmanagedType.LPStr)] public string pDocName;
			[MarshalAs(UnmanagedType.LPStr)] public string pOutputFile;
			[MarshalAs(UnmanagedType.LPStr)] public string pDataType;
		}
		[DllImport("winspool.Drv", EntryPoint="OpenPrinterA", SetLastError=true, CharSet=CharSet.Ansi, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, long pd);

		[DllImport("winspool.Drv", EntryPoint="ClosePrinter", SetLastError=true, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool ClosePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint="StartDocPrinterA", SetLastError=true, CharSet=CharSet.Ansi, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool StartDocPrinter( IntPtr hPrinter, Int32 level,  [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

		[DllImport("winspool.Drv", EntryPoint="EndDocPrinter", SetLastError=true, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool EndDocPrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint="StartPagePrinter", SetLastError=true, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool StartPagePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint="EndPagePrinter", SetLastError=true, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool EndPagePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint="WritePrinter", SetLastError=true, ExactSpelling=true, CallingConvention=CallingConvention.StdCall)]
		public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten );

	}//class PrintDirect

}//namespace PrintingLibrary




