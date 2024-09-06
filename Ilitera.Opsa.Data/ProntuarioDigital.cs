using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
	public enum TipoDocumentoDigitalizado: int
	{
		PCI=0,
		Periodico=1,
		Audiometria=2,
		Admissional=3,
		Demissional=4,
		MudancaDeFuncao=5,
		RetornoAoTrabalho=6,
		Complementar=7
	}

	[Database("opsa", "ProntuarioDigital", "IdProntuarioDigital", "", "Exame Digitalizado")]
	public class ProntuarioDigital: Ilitera.Data.Table
    {
        #region Properties
        private int _IdProntuarioDigital;
		private ExameBase _IdExameBase;
		private Empregado _IdEmpregado;
		private DateTime _DataProntuario;
		private string _Descricao = string.Empty;
		private string _Arquivo = string.Empty;
		private DateTime _DataDigitalizacao = DateTime.Now;
		private int _IndTipoDocumento;
		private Prestador _IdDigitalizador;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProntuarioDigital()
		{

		}
		public override int Id
		{
			get{return _IdProntuarioDigital;}
			set{_IdProntuarioDigital = value;}
		}
		public ExameBase IdExameBase
		{
			get{return _IdExameBase;}
			set{_IdExameBase = value;}
		}
		public Empregado IdEmpregado
		{
			get{return _IdEmpregado;}
			set{_IdEmpregado = value;}
		}
		public DateTime DataProntuario
		{
			get{return _DataProntuario;}
			set{_DataProntuario = value;}
		}
		public string Descricao
		{
			get{return _Descricao;}
			set{_Descricao = value;}
		}
		[Obrigatorio(true, "É necessário informar o documento digitalizado para ser cadastrado!")]
		public string Arquivo
		{
			get{return _Arquivo;}
			set{_Arquivo = value;}
		}
		public DateTime DataDigitalizacao
		{
			get{return _DataDigitalizacao;}
			set{_DataDigitalizacao = value;}
		}
		public int IndTipoDocumento
		{
			get{return _IndTipoDocumento;}
			set{_IndTipoDocumento = value;}
		}
        [Obrigatorio(true, "É necessário informar o digitalizador do documento!")]
        public Prestador IdDigitalizador
		{
			get{return _IdDigitalizador;}
			set{_IdDigitalizador = value;}
		}
		public override string ToString()
		{
			return this.DataProntuario.ToString("dd-MM-yyyy")
				+ " - " + this.GetDescricao();
        }
        #endregion

        #region Metodos

        #region GetTipoDocumento

        public string GetTipoDocumento()
        {
            return GetTipoDocumento(this.IndTipoDocumento);
        }

        public static string GetTipoDocumento(int TipoDocumento)
        {
            string ret;

            switch (TipoDocumento)
            {
                case (int)TipoDocumentoDigitalizado.Audiometria:
                        ret = "Audiometria";
                        break;

                case(int)TipoDocumentoDigitalizado.PCI:
                        ret = "Outros (PCI)";
                        break;

                case(int)TipoDocumentoDigitalizado.Periodico:
                        ret = "Periódico";
                        break;

                case(int)TipoDocumentoDigitalizado.Admissional:
                        ret = "Admissional";
                        break;

                case(int)TipoDocumentoDigitalizado.Demissional:
                        ret = "Demissional";
                        break;

                case(int)TipoDocumentoDigitalizado.MudancaDeFuncao:
                        ret = "Mudanca de Função";
                        break;

                case(int)TipoDocumentoDigitalizado.RetornoAoTrabalho:
                        ret = "Retorno ao Trabalho";
                        break;

                case(int)TipoDocumentoDigitalizado.Complementar:
                        ret = "Complementar";
                        break;

                default:
                    ret = string.Empty;
                    break;
            }

            return ret;
        }
        #endregion

        #region GetDescricao

        public static string GetDescricao(int TipoDocumento, string descricao)
        {
            string ret = string.Empty;

            if (descricao == string.Empty)
                ret = GetTipoDocumento(TipoDocumento);
            else
                ret = descricao;

            return ret;
        }

		public string GetDescricao()
		{
			string ret = string.Empty;
			
			if(this.IdExameBase.Id!=0)
			{
				this.IdExameBase.Find();
				this.IdExameBase.IdExameDicionario.Find();

				ret = this.GetTipoDocumento()+" (" + this.IdExameBase.IdExameDicionario.Nome+")";
			}
			else
			{
				if(this.Descricao==string.Empty)
					ret = this.GetTipoDocumento();
				else
					ret = this.GetTipoDocumento()+" ("+this.Descricao+")";
			}

			return ret;
        }
        #endregion

        #region GetArquivo

        public string GetArquivo(Cliente cliente)
		{
            if (this.Arquivo.ToUpper().IndexOf(cliente.GetFotoDiretorioPadrao().ToUpper() + @"\PRONTUARIO\") < 0)
                return Path.Combine(Fotos.GetRaizPath(), cliente.GetFotoDiretorioPadrao() + @"\Prontuario\" + this.Arquivo);
            else
                return this.Arquivo;
		}

        public string GetArquivo_Copia(Cliente cliente)
        {
            return this.Arquivo;
        }


        public static string GetArquivo(Cliente cliente, string arquivo)
		{
			string nomeArquivo =  System.IO.Path.GetFileName(arquivo);

            return Path.Combine(Fotos.GetRaizPath(), cliente.GetFotoDiretorioPadrao() + @"\Prontuario\" + nomeArquivo);
        }

        public static string GetArquivo_Copia(Cliente cliente, string arquivo)
        {
            string nomeArquivo = System.IO.Path.GetFileName(arquivo);

            return Path.Combine(Fotos.GetRaizPath() + "FotosDocsDigitais", cliente.GetFotoDiretorioPadrao() + @"\Prontuario\" + nomeArquivo);
        }

        #endregion

        #region GetArquivoUrl

        public string GetArquivoUrl(Cliente cliente)
        {
            return Path.Combine("http://" + EnvironmentUtitity.Domain + "/DocsDigitais",
                                cliente.GetFotoDiretorioPadrao()
                                + @"\Prontuario\"
                                + this.Arquivo).Replace(@"\", "/");
        }
        #endregion

        #region GetNovoArquivo

        public string GetNovoArquivo(Cliente cliente)
        {
            if (this.IdEmpregado.mirrorOld == null)
                this.IdEmpregado.Find();

            string sFotoDiretorioPadrao = cliente.GetFotoDiretorioPadrao();

            if (sFotoDiretorioPadrao == string.Empty)
            {
                cliente.CriarDiretorio();
                cliente.Save();
            }

            string path = Path.Combine(Fotos.GetRaizPath(), cliente.GetFotoDiretorioPadrao());

            path = Path.Combine(path, "Prontuario");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string strFile = Path.Combine(path, Utility.RemoveAcentosECaracteresEspeciais(this.IdEmpregado.tNO_EMPG)
                                                + " - "
                                                + this.DataProntuario.ToString("yyyy-MM-dd")
                                                + ".pdf");

            for (int i = 1; File.Exists(strFile); i++)
                strFile = Path.Combine(path, Utility.RemoveAcentosECaracteresEspeciais(this.IdEmpregado.tNO_EMPG)
                                                + " - "
                                                + this.DataProntuario.ToString("yyyy-MM-dd")
                                                + "_" + i.ToString()
                                                + ".pdf");

            return strFile;
        }
        #endregion

        #region CopiaProntuarioDigital

        public static void CopiaProntuarioDigital(Empregado empregado, Empregado empregadoDestino)
        {
            List<ProntuarioDigital> prontuarios = new ProntuarioDigital().Find<ProntuarioDigital>("IdEmpregado=" + empregado.Id);

            foreach (ProntuarioDigital prontuarioDigital in prontuarios)
            {
                string whereProntuario = "IdEmpregado=" + empregadoDestino.Id
                                        + " AND Arquivo='" + prontuarioDigital.Arquivo + "'";

                int count = new ProntuarioDigital().ExecuteCount(whereProntuario);

                if (count > 0)
                    continue;

                ProntuarioDigital novoProntuarioDigital = (ProntuarioDigital)prontuarioDigital.Clone();
                novoProntuarioDigital.Id = 0;
                novoProntuarioDigital.IdEmpregado.Id = empregadoDestino.Id;
                novoProntuarioDigital.Save();

                //System.IO.FileInfo arquivo = new System.IO.FileInfo(prontuarioDigital.GetArquivo(empregado.nID_EMPR));
                System.IO.FileInfo arquivo = new System.IO.FileInfo(prontuarioDigital.GetArquivo_Copia(empregado.nID_EMPR));

                string pathDestino = ProntuarioDigital.GetArquivo_Copia(empregadoDestino.nID_EMPR, prontuarioDigital.Arquivo);

                arquivo.CopyTo(pathDestino);
            }
        }
        #endregion

        #region TransfereProntuarioDigital

        public static void TransfereProntuarioDigital(Empregado empregado, Empregado empregadoDestino)
        {
            List<ProntuarioDigital> prontuarios = new ProntuarioDigital().Find<ProntuarioDigital>("IdEmpregado=" + empregado.Id);

            foreach (ProntuarioDigital prontuarioDigital in prontuarios)
            {
                prontuarioDigital.IdEmpregado.Id = empregadoDestino.Id;
                prontuarioDigital.Save();

                if (empregado.nID_EMPR.Id == empregadoDestino.nID_EMPR.Id)
                    continue;

                System.IO.FileInfo arquivo = new System.IO.FileInfo(prontuarioDigital.GetArquivo(empregado.nID_EMPR));

                if (!arquivo.Exists)
                    continue;

                string pathDestino = ProntuarioDigital.GetArquivo(empregadoDestino.nID_EMPR, prontuarioDigital.Arquivo);

                arquivo.CopyTo(pathDestino);

                arquivo.Delete();
            }
        }
        #endregion

        #region TransfereProntuarioDigital

        public static void TransfereProntuarioDigital(Empregado empregado, Cliente clienteOrigem, Cliente clienteDestino)
        {
            List<ProntuarioDigital> prontuarios = new ProntuarioDigital().Find<ProntuarioDigital>("IdEmpregado=" + empregado.Id);

            foreach (ProntuarioDigital prontuarioDigital in prontuarios)
            {
                string pathOrigem = prontuarioDigital.GetArquivo(clienteOrigem);

                System.IO.FileInfo arquivo = new System.IO.FileInfo(pathOrigem);

                if (!arquivo.Exists)
                    continue;

                string pathDestino = ProntuarioDigital.GetArquivo(clienteDestino, prontuarioDigital.Arquivo);

                arquivo.CopyTo(pathDestino, true);

                arquivo.Delete();
            }
        }
        #endregion

        #region CorrigeProntuarioDigital

        public void CorrigeProntuarioDigital()
        {
            List<ProntuarioDigital> 
                prontuarios = new ProntuarioDigital().Find<ProntuarioDigital>("(SUBSTRING(Arquivo, 0, Len(ARQUIVO) - 3)) Like '%.%'");

            foreach (ProntuarioDigital prontuario in prontuarios)
            {
                //string filePart1 = prontuario.Arquivo.Substring(0, prontuario.Arquivo.IndexOf(".") + 1);
                //string filePart2 = prontuario.Arquivo.Substring(prontuario.Arquivo.IndexOf(".") + 1);
                //string extensao = filePart2.Substring(0, filePart2.Length - 4);

                prontuario.IdEmpregado.Find();
                prontuario.IdEmpregado.nID_EMPR.Find();

                string fileName = GetArquivo(prontuario.IdEmpregado.nID_EMPR, prontuario.Arquivo);

                FileInfo file = new FileInfo(fileName);

                if (!file.Exists)
                    continue;

                string arquivoNew = prontuario.Arquivo.Substring(0, prontuario.Arquivo.Length - 3).Replace(".", string.Empty) + ".pdf";

                string fileNew = GetArquivo(prontuario.IdEmpregado.nID_EMPR, arquivoNew);

                file.CopyTo(fileNew);

                prontuario.Arquivo = arquivoNew;
                prontuario.Save();

                file.Delete();
            }

        }

        #endregion

        #region override Save

        public override int Save()
        {
            if (this.IdExameBase.Id != 0 && this.IdExameBase.mirrorOld == null)
                SetTipoDocumento(this.IdExameBase.Id);
            else if (this.IdExameBase.Id != 0 && this.IdExameBase.mirrorOld != null)
                SetTipoDocumento(this.IdExameBase, this);

            return base.Save();
        }

        private void SetTipoDocumento(int IdExameBase)
        {
            if (IdExameBase == 0)
                return;

            ExameBase exame = new ExameBase();
            exame.Find(this.IdExameBase.Id);

            SetTipoDocumento(exame, this);
        }

        public static void SetTipoDocumento(ExameBase exame, 
                                            ProntuarioDigital prontuarioDigital)
        {
            if (exame.Id == 0)
                return;

            if (exame.mirrorOld == null)
                exame.Find();

            if (exame.IdExameDicionario.Id == 0)
                return;

            switch (exame.IdExameDicionario.Id)
            {
                case (int)IndExameClinico.Admissional:
                    prontuarioDigital.IndTipoDocumento = (int)TipoDocumentoDigitalizado.Admissional;
                    break;

                case (int)IndExameClinico.Demissional:
                    prontuarioDigital.IndTipoDocumento = (int)TipoDocumentoDigitalizado.Demissional;
                    break;

                case (int)IndExameClinico.MudancaDeFuncao:
                    prontuarioDigital.IndTipoDocumento = (int)TipoDocumentoDigitalizado.MudancaDeFuncao;
                    break;

                case (int)IndExameClinico.Periodico:
                    prontuarioDigital.IndTipoDocumento = (int)TipoDocumentoDigitalizado.Periodico;
                    break;

                case (int)IndExameClinico.RetornoAoTrabalho:
                    prontuarioDigital.IndTipoDocumento = (int)TipoDocumentoDigitalizado.RetornoAoTrabalho;
                    break;

                case (int)Exames.Audiometria:
                    prontuarioDigital.IndTipoDocumento = (int)TipoDocumentoDigitalizado.Audiometria;
                    break;

                default:
                    prontuarioDigital.IndTipoDocumento = (int)TipoDocumentoDigitalizado.Complementar;
                    break;
            }
        }
        #endregion

        #endregion
    }
}
