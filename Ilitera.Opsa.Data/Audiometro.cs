using System;
using System.Data;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;


namespace Ilitera.Opsa.Data
{
	/// <summary>
	/// Summary description for Audiometro.
	/// </summary>
	[Database("opsa", "Audiometro", "IdAudiometro", "", "Audiometro")]
	public class Audiometro: Ilitera.Data.Table
	{
		private int _IdAudiometro;
		private Clinica _IdClinica;
		private string _Nome=string.Empty;
		private string _Fabricante=string.Empty;
		private DateTime _DataUltimaAfericao=DateTime.Today;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Audiometro()
		{

		}
		public override int Id
		{
			get{return _IdAudiometro;}
			set{_IdAudiometro = value;}
		}
		[Obrigatorio(true, "A Clínica é campo obrigatório!")]
		public Clinica IdClinica
		{
			get{return _IdClinica;}
			set{_IdClinica = value;}
		}
		[Obrigatorio(true, "Nome do Audiometro é campo obrigatório!")]
		public string Nome
		{
			get{return _Nome;}
			set{_Nome = value;}
		}
		public string Fabricante
		{
			get{return _Fabricante;}
			set{_Fabricante = value;}
		}
		[Obrigatorio(true, "Data da última Aferição é campo obrigatório!")]
		public DateTime DataUltimaAfericao
		{
			get{return _DataUltimaAfericao;}
			set{_DataUltimaAfericao = value;}
		}
		public override string ToString()
		{
			if(this.Nome==string.Empty)
				this.Find();

			return this.Nome + " - " + this.DataUltimaAfericao.ToString("dd-MM-yyyy");
		}

		public string GetNomeAudiometro()
		{
			if(this.Nome==string.Empty)
				this.Find();

			return this.Nome;
		}
	}
}
