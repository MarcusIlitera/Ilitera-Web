using System;

namespace Ilitera.Common
{
	/// <summary>
	/// Summary description for DisplayAttribute.
	/// </summary>
	
	// This attribute specifies that a class can be displayed
	// in our input forms.
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
	public sealed class DisplayInFormAttribute : Attribute
	{
	}

	// This attribute specifies which property will be used as a
	// caption for the input form.
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
	public sealed class DisplayFormCaptionAttribute : Attribute
	{

		private String _propertyName;

		public DisplayFormCaptionAttribute(String PropertyName)
		{
			_propertyName = PropertyName;
		}

		public String PropertyName
		{
			get {return _propertyName;}
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public sealed class DisplayComboAttribute : Attribute
	{

		private String _metodoPopulaName;

		public DisplayComboAttribute(String MetodoPopulaName)
		{
			_metodoPopulaName = MetodoPopulaName;
		}

		public String MetodoPopulaName
		{
			get {return _metodoPopulaName;}
		}
	}

	/*This attribute is used to designate which properties appear as
	input fields on the data entry form. In addition, the attribute
	specifies the field max size (MaxLength), and whether or not the user 
	should be able to change the property value (ReadOnly) */
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class DisplayFieldAttribute : Attribute
	{
		private int _maxlength;
//		private int _width;
		private bool _readOnly;
		private string _label=string.Empty;

		public DisplayFieldAttribute(int MaxLength)
		{
			_maxlength = MaxLength;
		}

		public DisplayFieldAttribute(int MaxLength, bool ReadOnly)
		{
			_maxlength = MaxLength;
			_readOnly = ReadOnly;
		}

		public DisplayFieldAttribute(int MaxLength, bool ReadOnly, string Label)
		{
			_maxlength = MaxLength;
			_readOnly = ReadOnly;
			_label = Label;
		}

		public int MaxLength
		{
			get {return _maxlength;}
		}

		public bool ReadOnly
		{
			get {return _readOnly;}
			set	{_readOnly = value;}
		}

		public string Label
		{
			get {return _label;}
			set	{_label = value;}
		}
	}
}
