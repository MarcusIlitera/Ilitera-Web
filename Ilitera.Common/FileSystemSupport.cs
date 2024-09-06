#region Using directives

using System;
using System.Collections;
using System.Text;
using System.Drawing;
using System.IO;
using Microsoft.Win32;
using System.Reflection;
using System.Runtime.InteropServices;

#endregion Using directives

namespace Ilitera.Common
{
	#region SHFILEINFO struct 

	struct SHFILEINFO 
    {
        public IntPtr hIcon;
        public IntPtr iIcon;
        public uint dwAttributes;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;

    };

	#endregion SHFILEINFO struct 

	#region IconSize enumeration
    public enum IconSize : uint
    {
        Large = 0x0000,
        Small = 0x0001
    }
	#endregion IconSize enumeration

	#region DriveType enumeration
	public enum DriveType
	{
		Unknown = 0,
		Removable = 2,
		Fixed = 3,
		Remote = 4,
		CDRom = 5,
	}
	#endregion DriveType enumeration

	#region FileSystemSupport class
	/// <summary>
	/// Provides access to directories and files for the Infragistics File Explorer sample.
	/// </summary>
	public class FileSystemSupport
	{
		#region Constants

		private static readonly string desktopLocationTemplate = @"C:\Documents and Settings\{0}\Desktop";
		private static readonly string myDocumentsLocationTemplate = @"C:\Documents and Settings\{0}\My Documents";

		private const int			DRIVE_UNKNOWN				= 0;
		private const int			DRIVE_REMOVABLE				= 2;
		private const int			DRIVE_FIXED					= 3;
		private const int			DRIVE_REMOTE				= 4;
		private const int			DRIVE_CDROM					= 5;
		private const uint			SHGFI_ICON					= 0x0100;
        private const uint			SHGFI_USEFILEATTRIBUTES		= 0x0010;
		private const uint			SHGFI_TYPENAME				= 0x0400;

		#endregion Constants

		#region Member variables
		private ArrayList					_iconHandles = null;
		#endregion Member variables

		#region Windows API function declarations
		
		[DllImport("kernel32")]
		private static extern int GetDriveType(IntPtr lpString);
		
        [DllImport("shell32.dll")]
        static extern IntPtr SHGetFileInfo( string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        [DllImport("Shell32", CharSet=CharSet.Auto)]
        static extern int ExtractIconEx( System.Text.StringBuilder lpszFile, int nIconIndex, IntPtr[] phIconLarge, IntPtr[] phIconSmall, int nIcons);

		[DllImport("user32.dll")]
		private static extern bool DestroyIcon(IntPtr hIcon);
		
		#endregion Windows API function declarations

		#region Properties

			#region DesktopPath
		/// <summary>
		/// Returns the path to the current user's Desktop.
		/// </summary>
		public string DesktopPath
		{
			get{ return string.Format( FileSystemSupport.desktopLocationTemplate, Environment.UserName ); }
		}
			#endregion DesktopPath

			#region MyDocumentsPath
		/// <summary>
		/// Returns the path to the current user's My Documents folder.
		/// </summary>
		public string MyDocumentsPath
		{
			get{ return string.Format( FileSystemSupport.myDocumentsLocationTemplate, Environment.UserName ); }
		}
			#endregion MyDocumentsPath

			#region IconHandles
		/// <summary>
		/// Returns an ArrayList containing the handles of icons that must
		/// be destroyed before the application terminates.
		/// </summary>
		public ArrayList IconHandles
		{
			get
			{
				if ( this._iconHandles == null )
					this._iconHandles = new ArrayList( 256 );

				return this._iconHandles;
			}
		}
			#endregion IconHandles

		#endregion Properties

		#region Methods
		
			#region GetLogicalDrives
		/// <summary>
		/// Returns the names of the logical drives on this machine.
		/// </summary>
		public static string[] GetLogicalDrives()
		{
			return Directory.GetLogicalDrives();
		}
        #endregion GetLogicalDrives

        #region GetDriveType
        /// <summary>
        /// Returns the type of drive the specified path represents.
        /// </summary>
        /// 
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public static DriveType GetDriveType( string path )
		{
			IntPtr lpString = Marshal.StringToCoTaskMemAnsi(path);

			int driveType = GetDriveType(lpString);

			Marshal.FreeCoTaskMem(lpString);

			switch ( driveType )
			{
				case DRIVE_REMOVABLE:{ return DriveType.Removable; }
				case DRIVE_FIXED:{ return DriveType.Fixed; }
				case DRIVE_REMOTE:{ return DriveType.Remote; }
				case DRIVE_CDROM:{ return DriveType.CDRom; }

				default:{ return DriveType.Unknown; }
			}
		}

			#endregion GetDriveType

			#region GetDisplayString
		/// <summary>
		/// Returns the display text for the specified DriveType
		/// </summary>
		public static string GetDisplayString( DriveType driveType )
		{
			switch ( driveType )
			{
				case DriveType.CDRom:{ return "Compact Disc"; }
				case DriveType.Fixed:{ return "Local Disc"; }
				case DriveType.Remote:{ return "Network Drive"; }
				case DriveType.Removable:{ return "Removable Drive"; }
				case DriveType.Unknown:{ return "Unknown"; }

				default:{ return "Unknown"; }
			}
		}

		/// <summary>
		/// Returns the display text for the specified DriveType
		/// </summary>
		public static string GetDisplayString( FileAttributes fileAttributes )
		{
			string retVal = string.Empty;

			if ( (fileAttributes & FileAttributes.Archive) == FileAttributes.Archive )
				retVal += "A ";
			if ( (fileAttributes & FileAttributes.Compressed) == FileAttributes.Compressed )
				retVal += "C ";
			if ( (fileAttributes & FileAttributes.Hidden) == FileAttributes.Hidden )
				retVal += "H ";
			if ( (fileAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly )
				retVal += "R ";

			return retVal;
		}
        #endregion GetDisplayString

        #region GetFileType
        /// <summary>
        /// Returns the type of the specified file.
        /// </summary>
        /// 
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public string GetFileType( string fileName )
		{
            SHFILEINFO shFileInfo = new SHFILEINFO();
            
            SHGetFileInfo(	fileName,
							0,
							ref shFileInfo,
							(uint)Marshal.SizeOf(shFileInfo),
							SHGFI_TYPENAME );
			
			return shFileInfo.szTypeName;
		}
			#endregion GetFileType

			#region IconFromFile

		/// <summary>
		/// Returns the icon associated with the specified file name.
		/// </summary>
		/// <param name="fileExtension">The file extension for which the associated icon should be returned.</param>
		/// <param name="iconSize">The desired icon size, i.e. 16 X 16 or 32 X 32.</param>
		/// <param name="handle">The handle of the icon returned.</param>
        public static Icon IconFromFile( string fileName, IconSize iconSize, out IntPtr handle )
		{
			return FileSystemSupport.IconFromFile( fileName, iconSize, 0, out handle );
		}

		/// <summary>
		/// Returns the icon associated with the specified file name
		/// </summary>
		/// <param name="fileExtension">The file extension for which the associated icon should be returned.</param>
		/// <param name="iconSize">The desired icon size, i.e. 16 X 16 or 32 X 32.</param>
		/// <param name="index">The index of the desired icon</param>
		/// <param name="handle">The handle of the icon returned.</param>
        private static Icon IconFromFile( string fileName, IconSize iconSize, int index, out IntPtr handle )
        {
			handle = IntPtr.Zero;

			try
			{
				System.Text.StringBuilder sb = new System.Text.StringBuilder( fileName );
				int iconCount = ExtractIconEx( sb, -1, null, null, 0 );
				if ( index >= iconCount )
					return null;

				IntPtr[] iconPtr = new IntPtr[1];

				if ( iconSize == IconSize.Small )
					ExtractIconEx( sb, index, null, iconPtr, 1 );
				else
					ExtractIconEx( sb, index, iconPtr, null, 1 );

				handle = iconPtr[0];
				return Icon.FromHandle( iconPtr[0] );
			}
			catch
			{
				handle = IntPtr.Zero;
				return null;
			}
        }

        #endregion IconFromFile

        #region IconFromFileExtension
        /// <summary>
        /// Returns the icon associated with the specified file extension
        /// </summary>
        /// <param name="fileExtension">The file extension for which the associated icon should be returned.</param>
        /// <param name="iconSize">The desired icon size, i.e. 16 X 16 or 32 X 32.</param>
        /// <param name="handle">The handle of the icon returned.</param>
        /// 
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public static Icon IconFromFileExtension( string fileExtension, IconSize iconSize, out IntPtr handle )
        {
			handle = IntPtr.Zero;

            try
            {
                if ( fileExtension.StartsWith(".") == false )
					fileExtension = "." + fileExtension;

                SHFILEINFO shFileInfo = new SHFILEINFO();
                
                SHGetFileInfo(	fileExtension,
								0,
								ref shFileInfo,
								(uint)Marshal.SizeOf(shFileInfo),
								SHGFI_ICON | SHGFI_USEFILEATTRIBUTES | (uint)iconSize);

				handle = shFileInfo.hIcon;
                return Icon.FromHandle(shFileInfo.hIcon);
            }
            catch
            {
				handle = IntPtr.Zero;
                return null;
            }
        }
			#endregion IconFromFileExtension

			#region VerifyFileSystemObject
		/// <summary>
		/// Verifies that the specified file system object can be deleted or renamed.
		/// </summary>
		private bool VerifyFileSystemObject( object fileSystemObject )
		{
			//	Do not allow any sub-directory of the Windows directory to be
			//	changed or deleted, and do not allow any file whose extension
			//	is EXE, DLL, or SYS to be changed or deleted.
			DirectoryInfo directoryInfo = fileSystemObject as DirectoryInfo;
			FileInfo fileInfo = fileSystemObject as FileInfo;

			if ( directoryInfo == null && fileInfo == null )
				return false;

			if ( directoryInfo != null )
			{
				if ( directoryInfo.FullName.ToLower() == "c:\\" ||
					 directoryInfo.FullName.ToLower().IndexOf("c:\\windows") >= 0 )
					return false;
			}
			else
			if ( fileInfo != null )
			{
				if ( fileInfo.Extension.ToLower().IndexOf("exe") >= 0 ||
					 fileInfo.Extension.ToLower().IndexOf("dll") >= 0 ||
					 fileInfo.Extension.ToLower().IndexOf("sys") >= 0 )
					return false;
			}

			return true;
		}
			#endregion VerifyFileSystemObject

			#region OnDispose
		/// <summary>
		/// Called when the object is to be disposed of
		/// </summary>
		public void OnDispose()
		{
			ArrayList iconHandles = this.IconHandles;

			for ( int i = 0; i < iconHandles.Count; i ++ )
			{
				IntPtr iconHandle = (IntPtr)iconHandles[i];
				DestroyIcon( iconHandle );
			}
		}
			#endregion OnDispose

		#endregion Methods

	}
	#endregion FileSystemSupport class

}