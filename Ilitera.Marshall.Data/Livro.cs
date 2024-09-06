using System;
using System.Data;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using Ilitera.Data;
using Ilitera.Common;
using Ilitera.Opsa.Data;

namespace Ilitera.Marshall.Data
{
    [Database("marshall", "Livro", "ID", true)]
    public class Livro : Ilitera.Data.Table
    {
        #region Properties

        private int _ID;
        private DateTime _DataTrans;
        private Contas _Saida;
        private Contas _Entrada;
        private float _ValorTrans;
        private int _HistoricoPadrao;
        private string _Complemento;
        private int _MicroEmpresa;
        private DateTime _DataFechamento;
        private bool _Balanco;
        private bool _SaldoInicial;
        private int _CodigoLancamento;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Livro()
        {

        }
        public override int Id
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public DateTime DataTrans
        {
            get { return _DataTrans; }
            set { _DataTrans = value; }
        }
        public Contas Saida
        {
            get { return _Saida; }
            set { _Saida = value; }
        }
        public Contas Entrada
        {
            get { return _Entrada; }
            set { _Entrada = value; }
        }
        public float ValorTrans
        {
            get { return _ValorTrans; }
            set { _ValorTrans = value; }
        }
        public int HistoricoPadrao
        {
            get { return _HistoricoPadrao; }
            set { _HistoricoPadrao = value; }
        }
        public string Complemento
        {
            get { return _Complemento; }
            set { _Complemento = value; }
        }
        public int MicroEmpresa
        {
            get { return _MicroEmpresa; }
            set { _MicroEmpresa = value; }
        }
        public DateTime DataFechamento
        {
            get { return _DataFechamento; }
            set { _DataFechamento = value; }
        }
        public bool Balanco
        {
            get { return _Balanco; }
            set { _Balanco = value; }
        }
        public bool SaldoInicial
        {
            get { return _SaldoInicial; }
            set { _SaldoInicial = value; }
        }
        public int CodigoLancamento
        {
            get { return _CodigoLancamento; }
            set { _CodigoLancamento = value; }
        }
        #endregion

        #region DataUltimoBalanco

        public static DateTime DataUltimoBalanco()
        {
            DataSet ds = new Livro().ExecuteDataset("SELECT MAX(DataFechamento) FROM marshall.dbo.Livro");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return Convert.ToDateTime(ds.Tables[0].Rows[0][0]);
            else
                return new DateTime();
        }
        #endregion

        #region GerarContasAReceber

        public static void GerarContasAReceber(List<Faturamento> listFaturamentos)
        {
            foreach (Faturamento faturamento in listFaturamentos)
                GerarContasAReceber(faturamento);
        }

        public static void GerarContasAReceber(Faturamento faturamento)
        {
            const int contaSaida = 215; //3.1.1.03.01 Serviço de Seg. do Trabalhol Mensal
            const int contaEntrada = 20;  //1.1.4.01.01 Contas a Receber
            const int contaIR = 345; //1.1.5.01.03 IR a Recuperar	
            const int contaCofins = 543; //1.1.5.01.05 PIS/CONFIS/CSLL a Recuperar

//			DateTime dataBalanco = Livro.DataUltimoBalanco();
//
//			if(faturamento.Emissao.Month!=dataBalanco.AddDays(1).Month 
//				&& faturamento.Emissao.Year!=dataBalanco.AddDays(1).Year)
//				throw new Exception("Data de emissão incompátivel com os lançamentos em aberto!");

            if (faturamento.Emissao <= new DateTime(2005, 1, 1))
                throw new Exception("Data de emissão inferior a 2005.");

            ReceberPagar receber = new ReceberPagar();
            receber.Find("CodigoLancamento=" + faturamento.Id);

            if (receber.Id != 0 && receber.DataPagamento != new DateTime())
            {
                faturamento.Contabilizado = true;
                faturamento.Save();

                return;
            }

            faturamento.IdCedente.Find();
            faturamento.IdSacado.Find();

            Livro livro = new Livro();

            if (receber.Id != 0)
            {
                livro.Find("CodigoLancamento=" + faturamento.Id
                    + " AND Saida=" + contaSaida
                    + " AND Entrada=" + contaEntrada);
            }
            else
                livro.Inicialize();

            livro.DataTrans = faturamento.Emissao;
            livro.Saida.Id = contaSaida;
            livro.Entrada.Id = contaEntrada;
            livro.ValorTrans = faturamento.ValorTotalSemImpostos();
            livro.HistoricoPadrao = 4;
            livro.Complemento = faturamento.IdSacado.NomeAbreviado + " para vencimento em " + faturamento.Vencimento.ToString("dd-MM-yyyy") + " - Boleto Nº" + faturamento.Numero;
            livro.MicroEmpresa = faturamento.IdCedente.IdEmpresa.Id == (int)Empresas.Opsa ? 1 : 3;
            livro.CodigoLancamento = faturamento.Id;
            livro.Save();

            if (faturamento.ValorIR != 0)
            {
                livro = new Livro();

                if (receber.Id != 0)
                {
                    livro.Find("CodigoLancamento=" + faturamento.Id
                        + " AND Saida=" + contaSaida
                        + " AND Entrada=" + contaIR);
                }
                else
                    livro.Inicialize();

                livro.DataTrans = faturamento.Emissao;
                livro.Saida.Id = contaSaida;
                livro.Entrada.Id = contaIR;
                livro.ValorTrans = Convert.ToSingle(System.Math.Round(Convert.ToDouble(faturamento.ValorIR)));
                livro.HistoricoPadrao = 4;
                livro.Complemento = faturamento.IdSacado.NomeAbreviado + " para vencimento em " + faturamento.Vencimento.ToString("dd-MM-yyyy") + " - Boleto Nº" + faturamento.Numero;
                livro.MicroEmpresa = faturamento.IdCedente.IdEmpresa.Id == (int)Empresas.Opsa ? 1 : 3;
                livro.CodigoLancamento = faturamento.Id;
                livro.Save();
            }

            if (faturamento.ValorPisCofinsCsll != 0)
            {
                livro = new Livro();
                livro = new Livro();

                if (receber.Id != 0)
                {
                    livro.Find("CodigoLancamento=" + faturamento.Id
                        + " AND Saida=" + contaSaida
                        + " AND Entrada=" + contaCofins);
                }
                else
                    livro.Inicialize();

                livro.DataTrans = faturamento.Emissao;
                livro.Saida.Id = contaSaida;
                livro.Entrada.Id = contaCofins;
                livro.ValorTrans = Convert.ToSingle(System.Math.Round(Convert.ToDouble(faturamento.ValorPisCofinsCsll)));
                livro.HistoricoPadrao = 4;
                livro.Complemento = faturamento.IdSacado.NomeAbreviado + " para vencimento em " + faturamento.Vencimento.ToString("dd-MM-yyyy") + " - Boleto Nº" + faturamento.Numero;
                livro.MicroEmpresa = faturamento.IdCedente.IdEmpresa.Id == (int)Empresas.Opsa ? 1 : 3;
                livro.CodigoLancamento = faturamento.Id;
                livro.Save();
            }

            if (receber.Id == 0)
                receber.Inicialize();

            receber.DC = false;
            receber.Conta.Id = contaEntrada;
            receber.IdCliente.Id = faturamento.IdSacado.Id;
            receber.MicroEmpresa = faturamento.IdCedente.IdEmpresa.Id == (int)Empresas.Opsa ? 1 : 3;
            receber.Documento = faturamento.Numero.ToString();
            receber.DataEmissão = faturamento.Emissao;
            receber.DataVencimento = faturamento.Vencimento;
            receber.TipoNota = faturamento.IdCedente.IndTipoNota == (int)TipoNota.Recibo ? "R" : "N";
            receber.ValorDocumento = faturamento.ValorTotal - faturamento.ValorTotalImpostos();
            receber.ValorImposto = Convert.ToSingle(System.Math.Round(Convert.ToDouble(faturamento.ValorIR)));
            receber.CodigoLancamento = faturamento.Id;
            receber.ValorImpostoPisConfisCsll = faturamento.ValorPisCofinsCsll;
            receber.Save();

            faturamento.Contabilizado = true;
            faturamento.Save();
        }
        #endregion

        #region LerArquivoDeLayout

        public static DataTable LerArquivoDeLayout(string filePath)
        {
            TextFieldParser tfp;

            string schemaFile = Path.Combine(Environment.CurrentDirectory, "Banespa.xml");

            tfp = new TextFieldParser(filePath, schemaFile);

            tfp.RecordFailed += new RecordFailedHandler(tfp_RecordFailed);

            DataTable dt = tfp.ParseToDataTable();

            return dt;
        }

        private static void tfp_RecordFailed(ref int CurrentLineNumber, string LineText, string ErrorMessage, ref bool Continue)
        {
            //System.Diagnostics.Debug.WriteLine("Error: " + ErrorMessage + Environment.NewLine + "Line: " + LineText);
        }

        #endregion

        #region BaixarTitulo

        public static void BaixarTitulo(Faturamento faturamento,
                                        DateTime dataPagto,
                                        float valorPago,
                                        float valorJuros)
        {
            ReceberPagar receberPagar = new ReceberPagar();
            receberPagar.Find("CodigoLancamento=" + faturamento.Id);

            if (receberPagar.Id == 0 || receberPagar.DataPagamento != new DateTime())
                return;

            faturamento.IdCedente.Find();
            faturamento.IdSacado.Find();

            int contaBanco;

            if (faturamento.IdCedente.Id == (int)Cedentes.BanespaOpsa)
                contaBanco = (int)PlanoDeContas.BanespaOPSA;
            else if (faturamento.IdCedente.Id == (int)Cedentes.BanespaMestra)
                contaBanco = (int)PlanoDeContas.BanespaMestra;
            else
                contaBanco = 0;

            Livro livro = new Livro();
            livro.Inicialize();
            livro.DataTrans = dataPagto;
            livro.Saida.Id = (int)PlanoDeContas.ContasReceber;
            livro.Entrada.Id = contaBanco;
            livro.ValorTrans = valorPago;
            livro.HistoricoPadrao = 21; //Recebimento de Pagamento Mensal de
            livro.Complemento = faturamento.IdSacado.NomeAbreviado + " pago em " + dataPagto.ToString("dd-MM-yyyy") + " - Boleto Nº" + faturamento.Numero;
            livro.MicroEmpresa = faturamento.IdCedente.IdEmpresa.Id == (int)Empresas.Opsa ? 1 : 3;
            livro.CodigoLancamento = faturamento.Id;
            livro.Save();

            if (valorJuros != 0)
            {
                livro = new Livro();
                livro.Inicialize();
                livro.DataTrans = dataPagto;
                livro.Saida.Id = (int)PlanoDeContas.JurosClientes;
                livro.Entrada.Id = contaBanco;
                livro.ValorTrans = valorJuros;
                livro.HistoricoPadrao = 38; //Juros de cobranças em atraso de
                livro.Complemento = faturamento.IdSacado.NomeAbreviado + " para vencimento em " + faturamento.Vencimento.ToString("dd-MM-yyyy") + " - Boleto Nº" + faturamento.Numero;
                livro.MicroEmpresa = faturamento.IdCedente.IdEmpresa.Id == (int)Empresas.Opsa ? 1 : 3;
                livro.CodigoLancamento = faturamento.Id;
                livro.Save();
            }

            receberPagar.DataPagamento = dataPagto;
            receberPagar.ValorPago = valorPago;
            receberPagar.Save();
        }
        #endregion

        #region BaixaAutomatica

        public static CedenteBaixaAutomatica BaixaAutomatica(string filePath)
        {
            return BaixaAutomatica(LerArquivoDeLayout(filePath), System.IO.Path.GetFileName(filePath));
        }

        public static CedenteBaixaAutomatica BaixaAutomatica(DataTable table, string FileName)
        {
            int IdCedente = 0;

            CedenteBaixaAutomatica baixaArquivo = new CedenteBaixaAutomatica();
            baixaArquivo.Find("ArquivoBaixa='" + FileName + "'");

            if (baixaArquivo.Id != 0)
                throw new Exception("Arquivo já baixado!");

            StringBuilder strObs = new StringBuilder();

            foreach (DataRow row in table.Rows)
            {
                if (IdCedente == 0)
                    IdCedente = Cedente.GetCedente(Convert.ToString(row["Cedente"])).Id;

                Faturamento faturamento = new Faturamento();
                faturamento.Find("IdCedente=" + IdCedente
                    + " AND Numero=" + Convert.ToInt32(row["Numero"]));

                if (faturamento.Id == 0)
                {
                    strObs.Append("\nErro: no Tírutlo " + Convert.ToString(row["Numero"]) + " não encontrado!");
                    continue;
                }

                if (faturamento.Id != 0 && !faturamento.Contabilizado)
                {
                    strObs.Append("\nErro: no Tírutlo " + Convert.ToString(row["Numero"]) + " não foi exportado para o Marshall!");
                    continue;
                }

                try
                {
                    Livro.BaixarTitulo(faturamento,
                        Convert.ToDateTime(row["DataPgto"]),
                        Convert.ToSingle(row["ValorPago"]),
                        Convert.ToSingle(row["ValorJuros"]));
                }
                catch (Exception ex)
                {
                    strObs.Append("\nErro: no Tírutlo " + Convert.ToString(row["Numero"]) + " - " + ex.Message);
                }
            }
            baixaArquivo = new CedenteBaixaAutomatica();
            baixaArquivo.Inicialize();
            baixaArquivo.DataBaixa = DateTime.Now;
            baixaArquivo.IdCedente.Id = IdCedente;
            baixaArquivo.ArquivoBaixa = FileName;
            baixaArquivo.Observacao = strObs.ToString();
            baixaArquivo.Save();

            return baixaArquivo;
        }
        #endregion

        #region ProcessarBaixaAutomatica

        public delegate void EventMessage(string Mensagem);
        public virtual event EventMessage EnviarMensagem;

        public static void ProcessarBaixaAutomatica()
        {
            ProcessarBaixaAutomatica(new Livro());
        }

        public static void ProcessarBaixaAutomatica(Livro livro)
        {
            FileInfo[] filesInfo = GetArquivosParaBaixa();

            if (filesInfo.Length == 0)
                throw new Exception("Nenhum arquivo encontrato no diretório '"
                                   + EnvironmentUtitity.FolderCobrancaBancaria + "'");

            foreach (FileInfo fileInfo in filesInfo)
            {
                if (fileInfo.Extension.ToUpper() != ".TXT")
                    continue;

                string[] sfileName = fileInfo.Name.Split('_');

                if (sfileName[sfileName.Length - 1].ToUpper() != "MOV.TXT")
                {
                    MoveFile(fileInfo, "FRA");
                    continue;
                }

                CedenteBaixaAutomatica baixaArquivo = new CedenteBaixaAutomatica();
                baixaArquivo.Find("ArquivoBaixa='" + Path.GetFileName(fileInfo.Name) + "'");

                if (baixaArquivo.Id != 0)
                {
                    try
                    {
                        MoveFile(fileInfo, "Baixados");
                        continue;
                    }
                    catch (IOException iOException)
                    {
                        if (iOException.Message == "Cannot create a file when that file already exists.\r\n")
                            fileInfo.Delete();
                    }
                }

                CedenteBaixaAutomatica baixa;

                try
                {
                    baixa = Livro.BaixaAutomatica(Livro.LerArquivoDeLayout(fileInfo.FullName), 
                                                  Path.GetFileName(fileInfo.Name));

                    if (baixa.Observacao == string.Empty)
                        MoveFile(fileInfo, "Baixados");
                    else
                        MoveFile(fileInfo, "BaixadosComErro");
                }
                catch (Exception ex)
                {
                    MoveFile(fileInfo, "BaixadosComErro");
                    throw ex;
                }

                if (livro.EnviarMensagem != null)
                {
                    if (baixa.Observacao == string.Empty)
                        livro.EnviarMensagem("O arquivo " 
                                            + Path.GetFileName(fileInfo.Name) 
                                            + " foi baixado com sucesso!");
                    else
                        livro.EnviarMensagem("O arquivo " 
                                            + Path.GetFileName(fileInfo.Name) 
                                            + " não foi baixado com sucesso!" 
                                            + baixa.Observacao);
                }
            }
        }

        public static FileInfo[] GetArquivosParaBaixa()
        {
            DirectoryInfo fotosFolder = new DirectoryInfo(EnvironmentUtitity.FolderCobrancaBancaria);

            FileInfo[] filesInfo = fotosFolder.GetFiles();

            return filesInfo;
        }
        #endregion

        #region MoveFile

        private static void MoveFile(FileInfo fileInfo, string sDirectory)
        {
            string newPath = Path.Combine(fileInfo.DirectoryName, sDirectory + @"\" + fileInfo.Name);

            FileInfo newFileInfo = new FileInfo(newPath);

            if (newFileInfo.Exists)
            {
                fileInfo.Delete();
                return;
            }

            fileInfo.MoveTo(newPath);
        }
        #endregion
    }
}
