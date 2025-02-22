using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Data;

using System.Windows.Forms;

namespace Ilitera.Common
{
	public delegate void RecordFoundHandler(ref int CurrentLineNumber, TextFieldCollection TextFields);
	public delegate void RecordFailedHandler(ref int CurrentLineNumber, string LineText, string ErrorMessage, ref bool Continue);

	public class TextFieldParser
	{
		/// <summary>
		/// Raised once for each record found in the file.
		/// </summary>
		public event RecordFoundHandler RecordFound;

		/// <summary>
		/// Raised when an error occurs on a record
		/// </summary>
		public event RecordFailedHandler RecordFailed;

		private TextFieldSchema m_TextFieldSchema = null;
		private string m_FileName = "";
		private string m_SchemaFile = "";
		private int m_CurrentLineNumber = 0;

		public TextFieldParser(string fileName)
		{
			m_FileName = fileName;
			m_TextFieldSchema = new TextFieldSchema();
		}

		public TextFieldParser(string fileName, string schemaFile)
		{
			m_FileName = fileName;
			m_TextFieldSchema = new TextFieldSchema(schemaFile);
		}

		/// <summary>
		/// Path to the data file.
		/// </summary>
		public string FileName
		{
			get{return m_FileName;}
			set{m_FileName = value;}
		}

		/// <summary>
		/// Path to a schema file.
		/// </summary>
		public string SchemaFile
		{
			get{return m_SchemaFile;}
			set{m_SchemaFile = value;}
		}

		//The current line number when parsing
		public int CurrentLineNumber
		{	
			get{return m_CurrentLineNumber;}
		}

		/// <summary>
		/// Parses the file data.
		/// To get the data, subcribe to the RecordFound event
		/// </summary>
		public void ParseFile()
		{
			ParseFile(m_FileName);
		}

		/// <summary>
		/// Parses the file data.
		/// To get the data, subcribe to the RecordFound event
		/// </summary>
		public void ParseFile(string fileName)
		{
			StreamReader reader = new StreamReader(fileName);
			int actualLineNumber = 0;
			string fileRecord = "";
			m_FileName = fileName;

			while(reader.Peek() != -1)
			{
				//get the data and execute the pattern match
				fileRecord = reader.ReadLine();
				actualLineNumber += 1;

				//don't process lines that we are supposed to skip
				if(actualLineNumber >= m_CurrentLineNumber)
				{
					//make sure the line number property stays in sync
					m_CurrentLineNumber = actualLineNumber;
					//fill the fields array based on file type
					Array fields = GetFieldArray(fileRecord);
					//make sure we found a match
					if(fields.Length == m_TextFieldSchema.TextFields.Count)
					{
						//the record matches our pattern
						try
						{
							//loop through the fields and assign the values
							for(int x = 0; x < m_TextFieldSchema.TextFields.Count; x++)
							{		
								m_TextFieldSchema.TextFields[x].Value = fields.GetValue(x);
							}
							//let the caller know what we found
							int currentLineNumber = actualLineNumber;

							if(RecordFound != null)
								RecordFound(ref currentLineNumber, m_TextFieldSchema.TextFields);
							m_CurrentLineNumber = currentLineNumber;
						}
						catch(Exception ex)
						{
							//pass the unhandled error back to the caller
							//the most likely problem is a conversion/cast error
							bool bContinue = true;
							int currentLineNumber = actualLineNumber;

							if(RecordFailed != null)
							{
								//See if they want to continue
								RecordFailed(ref currentLineNumber, fileRecord, ex.Message, ref bContinue);
								if(!bContinue)
									break;
							}
							else
							{
								//If they didn't subscribe to the error event, quit
								break;
							}
							m_CurrentLineNumber = currentLineNumber;
						}
					}
					else
					{
						//the number of fields located doesn't match the configuration
						bool bContinue = true;
						int currentLineNumber = actualLineNumber;
						if(RecordFailed != null)
						{
							//See if we shoul continue
							RecordFailed(ref currentLineNumber, fileRecord, "The number of fields identified in the file record does not match the number of TextField objects specified.", ref bContinue);
							if(!bContinue)
								break;
						}
						else
						{
							//if not subscribing to the event - quit
							break;
						}

						m_CurrentLineNumber = currentLineNumber;
					}
				}
			}
			//clean up our stream
			reader.Close();
		}

		/// <summary>
		/// Parses a line in the data file
		/// </summary>
		private string[] GetFieldArray(string fileRecord)
		{
			string[] fields = null;
			switch(m_TextFieldSchema.FileFormat)
			{
				case FileFormat.Delimited:
					//split the fields
					string[] srawFields = fileRecord.Split(m_TextFieldSchema.Delimeter);
					TrimFields(ref srawFields);
					//recombine any with quotes
					RecombineQuotedFields(ref srawFields);
					//remove the extra elements
					ExtractNullArrayElements(ref srawFields, ref fields);
					break;
				case FileFormat.FixedWidth:
					ArrayList rawFields = new ArrayList();
					int mark = 0;
					for(int x = 0; x < m_TextFieldSchema.TextFields.Count; x++)
					{
						//extract the value and move the book mark
						rawFields.Add(fileRecord.Substring(mark, m_TextFieldSchema.TextFields[x].Length));
						mark += m_TextFieldSchema.TextFields[x].Length;
					}

					fields = (string[])rawFields.ToArray(typeof(string));
					break;
				default:
					throw new ApplicationException("The specified FileType is not valid.");
			}
			return fields;
		}

		/// <summary>
		/// Removes extra spaces from around the data
		/// </summary>
		/// <param name="fields"></param>
		private void TrimFields(ref string[] fields)
		{
			for(int x = 0; x < fields.Length; x++)
				fields[x] = fields[x].Trim();
		}

		/// <summary>
		/// Removes quotes from around the field
		/// </summary>
		/// <param name="fields"></param>
		private void RecombineQuotedFields(ref string[] fields)
		{
			char firstChar;
			char lastChar;

			for(int x = 0; x < fields.Length; x++)
			{
				//get the potential delimitters
				if(fields[x].Length > 0)
				{
					firstChar = fields[x][0];
					lastChar = fields[x][fields[x].Length - 1];
				}
				else
				{
					firstChar = char.MinValue;
					lastChar = char.MinValue;
				}
				//start the comparisons
				if(firstChar == m_TextFieldSchema.QuoteCharacter)
				{
					//we started with a valid quote character
					if(firstChar == lastChar)
					{
						//strip off the matched quotes
						fields.SetValue(fields[x].Substring(1, fields[x].Length - 2), x);
					}
					else
					{
						int startIndex = x;
						char quoteChar = firstChar;
						do
						{
							//skip to the next item
							x += 1;
							//get the new "endpoints"
							firstChar = fields[x][0];
							lastChar = fields[x][fields[x].Length - 1];
							//this field better not start with a quote
							if(firstChar == m_TextFieldSchema.QuoteCharacter && fields[x].Length > 1)
								throw new ApplicationException("There was an unclosed quotation mark.");
					
							// recombine the items
							fields.SetValue(String.Concat(fields[startIndex].ToString(), m_TextFieldSchema.Delimeter, fields[x].ToString()), startIndex);
							//flush the unused array element
							Array.Clear(fields, x, 1);

						}while(lastChar != quoteChar);
			
						//strip off the outer quotes
						fields.SetValue(fields[startIndex].Substring(1, fields[startIndex].Length - 2), startIndex);
					}
				}
			}
		}

		private void ExtractNullArrayElements(ref string[] input, ref string[] output)
		{
			int x;
			int maxInputIndex = input.Length - 1;
			int count = 0;
			int mark = 0;

			//get the actual field count
			for(x = 0; x <= maxInputIndex; x++)
			{
				if(input[x] != null)
					count += 1;
			}
		
			//resize the output array
			output = (string[])Array.CreateInstance(typeof(String), count);
			for(x = 0; x <= maxInputIndex; x++)
			{
				if(input[x] != null)
				{
					//save the value and incriment the book mark
					output.SetValue(input[x], mark);
					mark += 1;
				}
			}
		}

		/// <summary>
		/// Does the same as Parse, but puts the results in a datatable.
		/// </summary>
		/// <returns></returns>
		public DataTable ParseToDataTable()
		{
			DataTable dt = MakeTable();

			StreamReader reader = new StreamReader(m_FileName, System.Text.Encoding.UTF7);
			int actualLineNumber = 0;
			string fileRecord = "";

			while(reader.Peek() != -1)
			{
				//get the data and execute the pattern match
				fileRecord = reader.ReadLine();
				actualLineNumber += 1;

				//don't process lines that we are supposed to skip
				if(actualLineNumber >= m_CurrentLineNumber)
				{
					//make sure the line number property stays in sync
					m_CurrentLineNumber = actualLineNumber;
					//fill the fields array based on file type

					Array fields = null;
					
					try
					{
						fields = GetFieldArray(fileRecord);
					}
					catch(Exception ex)
					{
						bool bContinue = true;

						int currentLineNumber = actualLineNumber;

						if(RecordFailed != null)
						{
							RecordFailed(ref currentLineNumber, fileRecord, ex.Message, ref bContinue);
							
							if(bContinue)
								continue;
							else				
								break;
						}
						else
						{
							m_CurrentLineNumber = currentLineNumber;
							//If they didn't subscribe to the error event, quit
							break;
						}
					}
					//make sure we found a match
					if(fields.Length == m_TextFieldSchema.TextFields.Count)
					{
						//the record matches our pattern
						try
						{
							//loop through the fields and assign the values
							for(int x = 0; x < m_TextFieldSchema.TextFields.Count; x++)
							{
								m_TextFieldSchema.TextFields[x].Value = fields.GetValue(x);
							}
							//let the caller know what we found
							int currentLineNumber = actualLineNumber;

                            AddRow(dt, m_TextFieldSchema.TextFields);

							if(RecordFound != null)
								RecordFound(ref currentLineNumber, m_TextFieldSchema.TextFields);
							
							m_CurrentLineNumber = currentLineNumber;
						}
						catch(Exception ex)
						{
							//pass the unhandled error back to the caller
							//the most likely problem is a conversion/cast error
							bool bContinue = true;
							int currentLineNumber = actualLineNumber;

							if(RecordFailed != null)
							{
								//See if they want to continue
								RecordFailed(ref currentLineNumber, fileRecord, ex.Message, ref bContinue);
								if(!bContinue)
									break;
							}
							else
							{
								//If they didn't subscribe to the error event, quit
								break;
							}
							m_CurrentLineNumber = currentLineNumber;
						}
					}
					else
					{
						//the number of fields located doesn't match the configuration
						bool bContinue = true;
						int currentLineNumber = actualLineNumber;
						if(RecordFailed != null)
						{
							//See if we shoul continue
							RecordFailed(ref currentLineNumber, fileRecord, "The number of fields identified in the file record does not match the number of TextField objects specified.", ref bContinue);
							if(!bContinue)
								break;
						}
						else
						{
							//if not subscribing to the event - quit
							break;
						}

						m_CurrentLineNumber = currentLineNumber;
					}
				}
			}
			//clean up our stream
			reader.Close();

			return dt;
		}

		/// <summary>
		/// Builds a datatable based on the TextFieldSchema
		/// </summary>
		/// <returns></returns>
		private DataTable MakeTable()
		{
			DataTable dt = new DataTable(m_TextFieldSchema.TableName);
			DataColumn column;
			foreach(TextField field in m_TextFieldSchema.TextFields)
			{
				column = new DataColumn(field.Name);

				//I don't really like this.
				//I could not find a way to convert from TypeCode to Type.
				//If you find the way, please let me know.
				switch(field.DataType)
				{
					case TypeCode.Boolean:
						column.DataType = Type.GetType("System.Boolean");
						break;
					case TypeCode.Byte:
						column.DataType = Type.GetType("System.Byte");
						break;
					case TypeCode.Char:
						column.DataType = Type.GetType("System.Char");
						break;
					case TypeCode.DateTime:
						column.DataType = Type.GetType("System.DateTime");
						break;
					case TypeCode.Decimal:
						column.DataType = Type.GetType("System.Decimal");
						break;
					case TypeCode.Double:
						column.DataType = Type.GetType("System.Double");
						break;
					case TypeCode.Int16:
						column.DataType = Type.GetType("System.Int16");
						break;
					case TypeCode.Int32:
						column.DataType = Type.GetType("System.Int32");
						break;
					case TypeCode.Int64:
						column.DataType = Type.GetType("System.Int64");
						break;
					case TypeCode.Object:
						column.DataType = Type.GetType("System.Object");
						break;
					case TypeCode.Single:
						column.DataType = Type.GetType("System.Single");
						break;
					case TypeCode.String:
						column.DataType = Type.GetType("System.String");
						break;
				}

				dt.Columns.Add(column);
			}

			return dt;
		}

		/// <summary>
		/// Adds a row to the datatable
		/// </summary>
		private void AddRow(DataTable dt, TextFieldCollection textFields)
		{
			DataRow dr = dt.NewRow();
			
			foreach(TextField field in textFields)
			{
				dr[field.Name] = field.Value;
			}

			dt.Rows.Add(dr);
		}

		/// <summary>
		/// Gets - sets the path to the data.
		/// </summary>
		public string FilePath
		{
			get{return m_TextFieldSchema.FilePath;}
			set{m_TextFieldSchema.FilePath = value;}
		}

		/// <summary>
		/// Gets - sets the name of the datatable
		/// </summary>
		public string TableName
		{
			get{return m_TextFieldSchema.TableName;}
			set{m_TextFieldSchema.TableName = value;}
		}

		/// <summary>
		/// Gets - sets the delimiter in a delimitted file
		/// </summary>
		public char Delimeter
		{
			get{return m_TextFieldSchema.Delimeter;}
			set{m_TextFieldSchema.Delimeter = value;}
		}

		/// <summary>
		/// Gets sets the character used for quoted fields
		/// </summary>
		public char QuoteCharacter
		{
			get{return m_TextFieldSchema.QuoteCharacter;}
			set{m_TextFieldSchema.QuoteCharacter = value;}
		}

		/// <summary>
		/// Gets - sets the format of the file
		/// </summary>
		public FileFormat FileFormat
		{
			get{return m_TextFieldSchema.FileFormat;}
			set{m_TextFieldSchema.FileFormat = value;}
		}

		/// <summary>
		/// Gets - sets the TextFields
		/// </summary>
		public TextFieldCollection TextFields
		{
			get{return m_TextFieldSchema.TextFields;}
			set{m_TextFieldSchema.TextFields = value;}
		}
	}
}