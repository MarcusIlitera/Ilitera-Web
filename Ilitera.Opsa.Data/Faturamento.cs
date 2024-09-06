using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Ilitera.Common;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
    #region class Faturamento

    [Database("opsa", "Faturamento", "IdFaturamento")]
    public class Faturamento : Ilitera.Data.Table
    {
        #region Enum

        public enum EntregaBoleto : int
        {
            NaoEntregue,
            Motorista,
            Correio,
            Email,
            Internet
        }

        public enum OrigemFaturamento : int
        {
            Contrato,
            Pedido,
            Avulso
        }

        #endregion

        #region Properties

        private int _IdFaturamento;
        private Juridica _IdSacado;
        private Cedente _IdCedente;
        private Cobranca _IdCobranca;
        private string _Referencia = string.Empty;
        private DateTime _Emissao = DateTime.Today;
        private DateTime _Vencimento = DateTime.Today;
        private float _ValorTotal;
        private float _ValorPisCofinsCsll;
        private float _ValorIR;
        private bool _Contabilizado;
        private int _Numero;
        private int _IndOrigemFaturamento = (int)OrigemFaturamento.Contrato;
        private Contrato _IdContrato;
        private int _IndEntregaBoleto;
        private DateTime _DataEntrega;
        private DateTime _DataAcesso;

        public const float IR = 0.0150F;
        public const float PIS_COFINS_CSLL = 0.0465F;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Faturamento()
        {

        }
        public override int Id
        {
            get { return _IdFaturamento; }
            set { _IdFaturamento = value; }
        }
        [Obrigatorio(true, "Sacado é campo obrigatório!")]
        public Juridica IdSacado
        {
            get { return _IdSacado; }
            set { _IdSacado = value; }
        }
        [Obrigatorio(true, "Cedente é campo obrigatório!")]
        public Cedente IdCedente
        {
            get { return _IdCedente; }
            set { _IdCedente = value; }
        }
        public Cobranca IdCobranca
        {
            get { return _IdCobranca; }
            set { _IdCobranca = value; }
        }
        public int Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
        public string Referencia
        {
            get { return _Referencia; }
            set { _Referencia = value; }
        }
        public DateTime Emissao
        {
            get { return _Emissao; }
            set { _Emissao = value; }
        }
        public DateTime Vencimento
        {
            get { return _Vencimento; }
            set { _Vencimento = value; }
        }
        public float ValorTotal
        {
            get { return _ValorTotal; }
            set { _ValorTotal = value; }
        }
        public float ValorPisCofinsCsll
        {
            get { return _ValorPisCofinsCsll; }
            set { _ValorPisCofinsCsll = value; }
        }
        public float ValorIR
        {
            get { return _ValorIR; }
            set { _ValorIR = value; }
        }
        public bool Contabilizado
        {
            get { return _Contabilizado; }
            set { _Contabilizado = value; }
        }
        public int IndOrigemFaturamento
        {
            get { return _IndOrigemFaturamento; }
            set { _IndOrigemFaturamento = value; }
        }
        public Contrato IdContrato
        {
            get { return _IdContrato; }
            set { _IdContrato = value; }
        }
        public int IndEntregaBoleto
        {
            get { return _IndEntregaBoleto; }
            set { _IndEntregaBoleto = value; }
        }
        public DateTime DataEntrega
        {
            get { return _DataEntrega; }
            set { _DataEntrega = value; }
        }
        public DateTime DataAcesso
        {
            get { return _DataAcesso; }
            set { _DataAcesso = value; }
        }
        #endregion

        #region Override Save/Delete

        public override int Save()
        {
            if (this.Numero == 0)
            {
                if (this.IdCedente.IdCobranca == null)
                    this.IdCedente.Find();

                if (this.Emissao < new DateTime(2005, 1, 1)
                    && this.IdCedente.IndTipoNota == (int)TipoNota.Recibo)
                {
                    Cliente cliente = new Cliente(this.IdSacado.Id);

                    Recibos recibo = new Recibos();
                    recibo.Find("CODIGO=" + cliente.CodigoAntigo + " AND Dt_processamento='" + this.Referencia + "'");

                    if (recibo.Id != 0 && recibo.NossoNumero != 0 && recibo.NossoNumero != 1)
                        this.Numero = recibo.NossoNumero;
                    else
                        throw new Exception("Use o sistema antigo de recibos!");
                }
                else if (this.Emissao > new DateTime(2005, 1, 1)
                    && this.IdCedente.IndTipoNota == (int)TipoNota.Recibo)
                {
                    this.IdCedente.Find();
                    this.Numero = this.IdCedente.ProximoNumero++;
                    this.IdCedente.Save();
                }
            }
            return base.Save();
        }

        public override void Delete()
        {
            //if (this.Contabilizado && this.Transaction == null)
            //    throw new Exception("Faturamento já contabilizado na Contabilidade. Não pode ser excluído!");

            base.Delete();
        }
        #endregion

        #region Metodos

        public float ValorSalarioMinimo()
        {
            float ret = 0F;

            ArrayList list = new ContratoServico().Find(
            "IdContrato=" + this.IdContrato.Id + " AND IdIndice=" + (int)Indices.SM);

            foreach (ContratoServico contratoServico in list)
                ret += contratoServico.ValorEmIndice();

            return Convert.ToSingle(System.Math.Round(Convert.ToDouble(ret), 2));
        }

        private float ValorTipoServico(TipoServico tipoServico)
        {
            float ret = 0.0F;

            ArrayList list = new FaturamentoServico().Find("IdFaturamento=" + this.Id
                + " AND IdServico IN (SELECT IdServico FROM Servico WHERE IndTipoServico="
                + (int)tipoServico + ")");

            foreach (FaturamentoServico faturamentoServico in list)
                ret += faturamentoServico.ValorTotal;

            return ret;
        }

        public float ValorSegurancaTrabalho()
        {
            return ValorTipoServico(TipoServico.SegurancaTrabalho);
        }

        public float ValorPCMSO()
        {
            return ValorTipoServico(TipoServico.Pcmso);
        }

        public float ValorJuridico()
        {
            return ValorTipoServico(TipoServico.Juridico);
        }

        public float PctImpostos()
        {
            float ret = 0.0F;

            if (this.ValorIR != 0.0F)
                ret += IR;

            if (this.ValorPisCofinsCsll != 0.0F)
                ret += PIS_COFINS_CSLL;

            return ret;
        }

        public float ValorTotalSemImpostos()
        {
            return this.ValorTotal - Convert.ToSingle(System.Math.Round(Convert.ToDouble(this.ValorIR + this.ValorPisCofinsCsll), 2));
        }

        public float ValorTotalImpostos()
        {
            return Convert.ToSingle(System.Math.Round(Convert.ToDouble(this.ValorIR + this.ValorPisCofinsCsll), 2));
        }

        public int GetMesReferencia()
        {
            return Convert.ToInt32(this.Referencia.Substring(0, 2));
        }

        public int GetAnoReferencia()
        {
            return Convert.ToInt32(this.Referencia.Substring(3, 4));
        }

        public enum DemostrativoBoleto : int
        {
            Sem,
            ComAnalitico,
            ComPedido,
            ComCentroCusto
        }

        public DemostrativoBoleto GetDemostrativoBoleto()
        {
            if (this.IdContrato.mirrorOld == null)
                this.IdContrato.Find();

            if (this.IdSacado.mirrorOld == null)
                this.IdSacado.Find();


            if (this.Id == 0 || !this.IdContrato.BoletoComDemostrativo)
                return DemostrativoBoleto.Sem;
            else
            {
                if (this.IdContrato.IndTipoContrato == (int)TipoDeContrato.Mensalista)
                    return DemostrativoBoleto.ComAnalitico;
                else
                    return DemostrativoBoleto.ComPedido;
            }
        }

        public static int GetProximoNumeroNF()
        {
            ArrayList listUltimoFaturamento = new Faturamento().FindMax("Numero", 
                "IdCedente IN (SELECT IdCedente FROM Cedente WHERE IndTipoNota=" 
                + (int)TipoNota.NotaFiscal + ")");

            return ++((Faturamento)listUltimoFaturamento[0]).Numero;
        }

        #endregion

        #region Codigo de Barras

        public string FormataNossoNumero(string agencia, string nossoNumero)
        {
            int intNumero;
            int intTotalNumero = 0;
            System.Text.StringBuilder ret = new System.Text.StringBuilder();
            ret.Append(Convert.ToDecimal(agencia.Trim().Replace(" ", "").Replace("-", "")).ToString("000"));
            ret.Append(Convert.ToDecimal(nossoNumero.Trim().Replace(" ", "").Replace("-", "")).ToString("0000000"));
            intNumero = Convert.ToInt32(ret.ToString().Substring(0, 1)) * 7;
            intTotalNumero = intTotalNumero + intNumero;
            intNumero = Convert.ToInt32(ret.ToString().Substring(1, 1)) * 3;
            intTotalNumero = intTotalNumero + intNumero;
            intNumero = Convert.ToInt32(ret.ToString().Substring(2, 1)) * 1;
            intTotalNumero = intTotalNumero + intNumero;
            intNumero = Convert.ToInt32(ret.ToString().Substring(3, 1)) * 9;
            intTotalNumero = intTotalNumero + intNumero;
            intNumero = Convert.ToInt32(ret.ToString().Substring(4, 1)) * 7;
            intTotalNumero = intTotalNumero + intNumero;
            intNumero = Convert.ToInt32(ret.ToString().Substring(5, 1)) * 3;
            intTotalNumero = intTotalNumero + intNumero;
            intNumero = Convert.ToInt32(ret.ToString().Substring(6, 1)) * 1;
            intTotalNumero = intTotalNumero + intNumero;
            intNumero = Convert.ToInt32(ret.ToString().Substring(7, 1)) * 9;
            intTotalNumero = intTotalNumero + intNumero;
            intNumero = Convert.ToInt32(ret.ToString().Substring(8, 1)) * 7;
            intTotalNumero = intTotalNumero + intNumero;
            intNumero = Convert.ToInt32(ret.ToString().Substring(9, 1)) * 3;
            intTotalNumero = intTotalNumero + intNumero;
            int unidade = Convert.ToInt32(intTotalNumero.ToString().Substring(intTotalNumero.ToString().Length - 1, 1));
            int digito = 0;
            if (unidade != 0)
                digito = 10 - unidade;
            return Convert.ToDecimal(agencia.Trim().Replace(" ", "").Replace("-", "")).ToString("000")
                    + " "
                    + Convert.ToDecimal(nossoNumero.Trim().Replace(" ", "").Replace("-", "")).ToString("0000000")
                    + " "
                    + digito.ToString();
        }

        private string CampoLivre()
        {
            //			return CampoLivre("40013012168", "7469108", "00", "033");
            return CampoLivre("14813026478", "0004952", "00", "033");
        }

        private string CampoLivre(string codigoCedente, string nossoNumero, string filler, string banco)
        {
            System.Text.StringBuilder ret = new System.Text.StringBuilder();
            //Posição(11)
            ret.Append(Convert.ToDecimal(codigoCedente.Trim().Replace(" ", "").Replace("-", "")).ToString("00000000000"));
            //Posição(07)
            ret.Append(Convert.ToDecimal(nossoNumero.Trim().Replace(" ", "").Replace("-", "")).ToString("0000000"));
            //Posição(02)
            ret.Append(Convert.ToDecimal(filler.Trim().Replace(" ", "").Replace("-", "")).ToString("00"));
            //Posição(03)
            ret.Append(Convert.ToDecimal(banco.Trim().Replace(" ", "").Replace("-", "")).ToString("000"));
            ret.Append(Digitos1e2(ret.ToString()));
            return ret.ToString();
        }

        private string Digitos1e2(string strNumero)
        {
            int dig1 = CalculoDig1(strNumero);
            return Digitos1e2(dig1, strNumero);
        }

        private string Digitos1e2(int dig1, string strNumero)
        {
            int dig2 = CalculoDig2(strNumero + dig1.ToString());
            switch (dig2)
            {
                case 0:
                    dig2 = 0;
                    return dig1.ToString() + dig2.ToString();
                case 1:
                    if (dig1 == 9)
                    {
                        dig1 = 0;
                        return Digitos1e2(dig1, strNumero);
                    }
                    else
                    {
                        dig1++;
                        return Digitos1e2(dig1, strNumero);
                    }
                case 10:
                    dig2 = 11 - dig2;
                    return dig1.ToString() + dig2.ToString();
                default:
                    dig2 = 11 - dig2;
                    return dig1.ToString() + dig2.ToString();
            }
        }

        private int Fator(DateTime vencimento)
        {
            DateTime dataBase = new DateTime(1997, 10, 7);
            System.TimeSpan dif = vencimento.Subtract(dataBase);
            return Convert.ToInt32(dif.TotalDays);
        }

        public string LinhaDigitavel(DateTime vencimento, string codigoCedente, string nossoNumero, string filler, string banco, double valor, string codigoBarras)
        {
            StringBuilder campoLivre = new StringBuilder();
            campoLivre.Append(CampoLivre(codigoCedente, nossoNumero, filler, banco));
            valor = valor * 100F;
            //separa a sequencia e prepara o valor
            string seq1 = banco + "9" + campoLivre.ToString().Substring(0, 5);
            string seq2 = campoLivre.ToString().Substring(5, 10);
            string seq3 = campoLivre.ToString().Substring(15, 10);
            string seq4 = codigoBarras.Substring(4, 1);
            string seq5 = Fator(vencimento) + valor.ToString("0000000000");
            //calcula os dvs
            int dv1 = CalculoMod10(seq1);
            int dv2 = CalculoMod10(seq2);
            int dv3 = CalculoMod10(seq3);
            //formata a sequencia
            seq1 = seq1.Substring(0, 5) + "." + seq1.Substring(5, 4) + dv1;
            seq2 = seq2.Substring(0, 5) + "." + seq2.Substring(5, 5) + dv2;
            seq3 = seq3.Substring(0, 5) + "." + seq3.Substring(5, 5) + dv3;
            return seq1 + " " + seq2 + " " + seq3 + " " + seq4 + " " + seq5;
        }

        public string CodigoBarras(string banco, string moeda, double valor, DateTime vencimento, string codigoCedente, string nossoNumero, string filler)
        {
            string livre = CampoLivre(codigoCedente, nossoNumero, filler, banco);
            return CodigoBarras(banco, moeda, valor, vencimento, livre);
        }

        private string CodigoBarras(string Banco, string Moeda, double valor, DateTime vencimento, string livre)
        {
            System.Text.StringBuilder codigo_sequencia = new System.Text.StringBuilder();
            // cálculo do fator
            int fator = Fator(vencimento);
            valor = Convert.ToInt32(valor * 100F);
            // sequencia sem o DV
            codigo_sequencia.Append(Banco);
            codigo_sequencia.Append(Moeda);
            codigo_sequencia.Append(fator.ToString());
            codigo_sequencia.Append(valor.ToString("0000000000"));
            codigo_sequencia.Append(livre);
            // calculo do DV
            int intDac = CalculoDigCodBarras(codigo_sequencia.ToString());
            //monta a sequencia para o codigo de barras com o DV
            codigo_sequencia.Insert(4, intDac);
            return codigo_sequencia.ToString();
        }

        private int CalculoDigCodBarras(string strNumero)
        {
            int intTotalNumero = 0;
            int intMultiplicador = 2;
            if (strNumero.Length != 43)
                System.Diagnostics.Trace.WriteLine(strNumero.Length.ToString());
            //pega cada caracter do numero a partir da direita
            for (int intcontador = (strNumero.Length - 1); intcontador > -1; intcontador--)
            {
                //extrai o caracter e multiplica prlo multiplicador
                int intNumero = Convert.ToInt32(Convert.ToInt32(strNumero.Substring(intcontador, 1)) * intMultiplicador);
                //soma o resultado para totalização
                intTotalNumero = intTotalNumero + intNumero;
                //se o multiplicador for maior que 2 decrementa-o caso contrario atribuir valor padrao original
                intMultiplicador = (intMultiplicador < 9) ? ++intMultiplicador : 2;
            }
            int intResto = intTotalNumero % 11;
            int intresultado = 11 - intResto;
            if (intresultado == 10 || intresultado == 11)
                return 1;
            else
                return intresultado;
        }

        private int CalculoDig1(string strNumero)
        {
            int intNumero, intTotalNumero = 0, intMultiplicador, intResto = 0;
            //inicia o multiplicador
            intMultiplicador = 2;
            //pega cada caracter do numero a partir da direita
            //		For intContador = Len(strNumero) To 1 Step -1
            for (int intContador = 0; intContador < strNumero.Length; intContador++)
            {
                //extrai o caracter e multiplica prlo multiplicador
                intNumero = Convert.ToInt32(strNumero.Substring(intContador, 1));
                intNumero = intNumero * intMultiplicador;
                // se o resultado for maior que nove soma os algarismos do resultado
                if (intNumero > 9)
                {
                    intNumero = Convert.ToInt32(intNumero.ToString().Substring(0, 1))
                        + Convert.ToInt32(intNumero.ToString().Substring(intNumero.ToString().Length - 1, 1));
                }
                //soma o resultado para totalização
                intTotalNumero = intTotalNumero + intNumero;
                //se o multiplicador for igual a 2 atribuir valor 1 se for 1 atribui 2
                intMultiplicador = (intMultiplicador == 2) ? 1 : 2;
            }
            //calcula o resto da divisao do total por 10
            intResto = intTotalNumero % 10;
            //verifica as exceções ( 0 -> DV=0 )
            if (intResto == 0)
                intResto = 0;
            else
                intResto = 10 - intResto;

            return intResto;
        }

        private int CalculoDig2(string strNumero)
        {
            int intcontador;
            int intnumero;
            int intTotalNumero = 0;
            int intMultiplicador;
            int intResto;
            //inicia o multiplicador
            intMultiplicador = 2;
            //pega cada caracter do numero a partir da direita
            for (intcontador = (strNumero.Length - 1); intcontador > -1; intcontador--)
            {
                //extrai o caracter e multiplica prlo multiplicador
                intnumero = Convert.ToInt32(Convert.ToInt32(strNumero.Substring(intcontador, 1)) * intMultiplicador);
                //soma o resultado para totalização
                intTotalNumero = intTotalNumero + intnumero;
                //se o multiplicador for maior que 2 decrementa-o caso contrario atribuir valor padrao original
                intMultiplicador = (intMultiplicador < 7) ? ++intMultiplicador : 2;
            }
            //calcula o resto da divisao do total por 11
            intResto = intTotalNumero % 11;
            return intResto;
        }

        private int CalculoMod10(string strNumero)
        {
            int intNumero, intTotalNumero = 0, intMultiplicador, intResto = 0;
            //inicia o multiplicador
            intMultiplicador = 2;
            //pega cada caracter do numero a partir da direita
            //		For intContador = Len(strNumero) To 1 Step -1
            for (int intContador = (strNumero.Length - 1); intContador > -1; intContador--)
            {
                //extrai o caracter e multiplica prlo multiplicador
                intNumero = Convert.ToInt32(strNumero.Substring(intContador, 1));
                intNumero = intNumero * intMultiplicador;
                // se o resultado for maior que nove soma os algarismos do resultado
                if (intNumero > 9)
                {
                    intNumero = Convert.ToInt32(intNumero.ToString().Substring(0, 1))
                        + Convert.ToInt32(intNumero.ToString().Substring(intNumero.ToString().Length - 1, 1));
                }
                //soma o resultado para totalização
                intTotalNumero = intTotalNumero + intNumero;
                //se o multiplicador for igual a 2 atribuir valor 1 se for 1 atribui 2
                intMultiplicador = (intMultiplicador == 2) ? 1 : 2;
            }
            //calcula o resto da divisao do total por 10
            intResto = intTotalNumero % 10;
            //verifica as exceções ( 0 -> DV=0 )
            if (intResto == 0)
                intResto = 0;
            else
                intResto = 10 - intResto;

            return intResto;
        }

        #endregion

        #region Faturamento

        #region Faturar

        public void Faturar(bool Recalcular)
        {
            DateTime dataReferencia = new DateTime(this.GetAnoReferencia(),
                                    this.GetMesReferencia(), 1).AddMonths(1).AddDays(-1);

            Faturar(dataReferencia, Recalcular);
        }

        public void Faturar(DateTime dataReferencia, bool Recalcular)
        {
            this.Contabilizado = false;

            if (this.IdContrato.mirrorOld == null)
                this.IdContrato.Find();

            if (this.IndOrigemFaturamento != (int)OrigemFaturamento.Avulso)
            {
                if (Recalcular)
                {
                    if (this.IdContrato.IndTipoContrato == (int)TipoDeContrato.Mensalista)
                    {
                        this.ValorTotal = RecalculaFaturamentoServico();
                    }
                    else if (this.IdContrato.IndTipoContrato == (int)TipoDeContrato.Pedido)
                    {
                        this.ValorTotal = RecalculaFaturamentoPedido();
                    }
                    else if (this.IdContrato.IndTipoContrato == (int)TipoDeContrato.Misto)
                    {
                        throw new Exception("Faturamento de Contrato do tipo misto não pode ser recalculado!");
                    }
                }
                else
                {
                    if (this.IdContrato.IndTipoContrato == (int)TipoDeContrato.Mensalista)
                    {
                        this.ValorTotal = GerarFaturamentoServicos(this.IdContrato, this, dataReferencia);
                    }
                    else if (this.IdContrato.IndTipoContrato == (int)TipoDeContrato.Pedido)
                    {
                        this.ValorTotal = GerarFaturamentoPedido(this.IdContrato, this);
                    }
                    else if (this.IdContrato.IndTipoContrato == (int)TipoDeContrato.Misto)
                    {
                        this.ValorTotal = GerarFaturamentoServicos(this.IdContrato, this, dataReferencia);
                        this.ValorTotal += GerarFaturamentoPedido(this.IdContrato, this);
                    }
                }
            }

            if (this.ValorTotal == 0.0F && this.IndOrigemFaturamento != (int)OrigemFaturamento.Avulso)
            {
                this.Delete();
                throw new Exception(this.IdContrato.Descricao + " - O Valor do Contrato não foi informado!");
            }

            if (this.IdSacado.IdJuridicaPapel == null)
                this.IdSacado.Find();

            if (this.IdCedente.Id == -4411682)//Só calcula a Retenção para OPSA
            {
                //IR
                if ((this.ValorTotal * IR) > 10.0F)
                    this.ValorIR = this.ValorTotal * IR;
                else
                    this.ValorIR = 0.0F;

                double totalCNPJ = 0.0D;
                this.IdContrato.IdSacado.Find();

                totalCNPJ = TotalFaturamentoPorCNPJ(dataReferencia, this.IdContrato.IdSacado);

                if (this.IdSacado.IsOptanteSimples)
                    this.ValorPisCofinsCsll = 0.0F;
                else if (totalCNPJ > 5000.0D || this.ValorTotal > 5000.0F)
                    this.ValorPisCofinsCsll = this.ValorTotal * PIS_COFINS_CSLL;
                else
                    this.ValorPisCofinsCsll = 0.0F;
            }
            this.Save();
        }
        #endregion

        #region TotalFaturamentoPorCNPJ

        public float TotalFaturamentoPorCNPJ(DateTime dataReferenica, Juridica juridica)
        {
            float ret = 0F;

            StringBuilder str = new StringBuilder();
            str.Append(" (Inicio <='" + dataReferenica.ToString("yyyy-MM-dd") + "' OR Inicio IS NULL)");
            str.Append(" AND (Termino >='" + dataReferenica.ToString("yyyy-MM-dd") + "' OR Termino IS NULL)");
            str.Append(" AND DataRecisao IS NULL");
            str.Append(" AND IdCliente IN (SELECT IdCliente FROM qryClienteAtivos)");
            str.Append(" AND IdCedente IN (SELECT IdCedente	FROM Cedente WHERE IndTipoNota=" + (int)TipoNota.Recibo + ")");
            if (juridica.NomeCodigo != string.Empty && juridica.NomeCodigo.IndexOf("/") != -1)
                str.Append(" AND IdCliente IN (SELECT IdPessoa FROM Pessoa WHERE NomeCodigo LIKE '" + juridica.NomeCodigo.Substring(0, juridica.NomeCodigo.IndexOf("/")) + "%')");
            else
                str.Append(" AND IdCliente = " + juridica.Id);

            str.Append(" ORDER BY (SELECT NomeAbreviado FROM Pessoa WHERE IdPessoa = IdCliente)");

            ArrayList listContratos = new Contrato().Find(str.ToString());

            foreach (Contrato c in listContratos)
                ret += Convert.ToSingle(c.TotalServicos());

            return ret;
        }
        #endregion

        #region GerarFaturamentoServicos

        private static float GerarFaturamentoServicos( Contrato contrato, 
                                                       Faturamento faturamento, 
                                                       DateTime dataReferencia)
        {
            int IdServico = 0;
            float ValorTotal = 0.0F;
            float Quantidade = 0.0F;
            float ValorTotalServico = 0.0F;
            string ObservacaoNota = string.Empty;

            string criteria = "IdContrato=" + contrato.Id
                             + " AND DataInicio < '" + dataReferencia.ToString("yyyy-MM-dd") + "'"
                             + " AND Valor <> 0 ORDER BY IdServico, IdCliente";

            List<ContratoServico> contratoServicos = new ContratoServico().Find<ContratoServico>(criteria);

            FaturamentoServico faturamentoServico = new FaturamentoServico();

            foreach (ContratoServico contratoServico in contratoServicos)
            {
                if (!contrato.AgruparServico && IdServico != contratoServico.IdServico.Id)
                {
                    IdServico = contratoServico.IdServico.Id;
                    ValorTotalServico = 0.0F;
                    Quantidade = 0.0F;

                    faturamentoServico = new FaturamentoServico();
                    faturamentoServico.Inicialize();
                    faturamentoServico.IdFaturamento.Id = faturamento.Id;
                    faturamentoServico.IdServico.Id = contratoServico.IdServico.Id;
                }

                Quantidade += contratoServico.QuantidadeUnidade();
                ValorTotalServico += contratoServico.ValorTotal();
                ValorTotal += contratoServico.ValorTotal();
                ObservacaoNota += contratoServico.ObservacaoNota;

                int IdFatServicoAnalitico = AddLogServicoAnalitico(faturamento, contratoServico);

                if (contratoServico.IndUnidade == (int)UnidadeServico.EmpregadosAtivos)
                {
                    try { AddLogEmpregado(contratoServico, IdFatServicoAnalitico); }
                    catch { }
                }

                if (!contrato.AgruparServico)
                {
                    faturamentoServico.ValorTotal = ValorTotalServico;
                    faturamentoServico.Quantidade = Quantidade;
                    faturamentoServico.ValorUnitario = Quantidade == 0 ? contratoServico.Valor : (ValorTotalServico / Quantidade);
                    faturamentoServico.ObservacaoNota = ObservacaoNota;
                    faturamentoServico.Save();
                }
            }//end foreach

            if (contrato.AgruparServico)
            {
                faturamentoServico = new FaturamentoServico();
                faturamentoServico.Inicialize();
                faturamentoServico.IdFaturamento = faturamento;
                faturamentoServico.IdServico = contrato.IdServico;
                faturamentoServico.Quantidade = 1.0F;
                faturamentoServico.ValorUnitario = ValorTotal;
                faturamentoServico.ValorTotal = ValorTotal;
                faturamentoServico.ObservacaoNota = ObservacaoNota;
                faturamentoServico.Save();
            }

            return ValorTotal;
        }

        private static int AddLogServicoAnalitico(Faturamento faturamento, 
                                                  ContratoServico contratoServico)
        {
            FaturamentoServicoAnalitico faturamentoServicoAnalitico = new FaturamentoServicoAnalitico();
            faturamentoServicoAnalitico.Inicialize();
            faturamentoServicoAnalitico.IdFaturamento.Id = faturamento.Id;
            faturamentoServicoAnalitico.IdServico.Id = contratoServico.IdServico.Id;
            faturamentoServicoAnalitico.IdCliente.Id = contratoServico.IdCliente.Id;
            faturamentoServicoAnalitico.Quantidade = contratoServico.QuantidadeUnidade();
            faturamentoServicoAnalitico.ValorUnitario = contratoServico.Valor;
            faturamentoServicoAnalitico.ValorTotal = contratoServico.ValorTotal();

            return faturamentoServicoAnalitico.Save(); 
        }

        private static void AddLogEmpregado(ContratoServico contratoServico, 
                                            int IdFaturamentoServicoAnalitico)
        {
            Empregado.CriteriaBuilder filtro = new Empregado.CriteriaBuilder(contratoServico.IdCliente);
            filtro.IndEmpregados = Empregado.CriteriaBuilder.Empregados.Ativos;
            filtro.IndEmpregadosPelo = Empregado.CriteriaBuilder.EmpregadosPelo.LocalTrabalho;
            filtro.IndClassificacaoFuncional = Empregado.CriteriaBuilder.ClassificacaoFuncional.Com;

            DataSet ds = new Empregado().GetIdNome("tNO_EMPG", filtro.CriteriaString);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                FaturamentoServicoAnaliticoEmpregado fsae = new FaturamentoServicoAnaliticoEmpregado();
                fsae.Inicialize();
                fsae.IdFaturamentoServicoAnalitico.Id = IdFaturamentoServicoAnalitico;
                fsae.IdEmpregado.Id = Convert.ToInt32(row["Id"]);
                fsae.Save();
            }
        }
        #endregion

        #region RecalculaFaturamentoServico

        private float RecalculaFaturamentoServico()
        {
            string criteria = "IdFaturamento=" + this.Id;

            float ValorTotal = 0.0F;

            List<FaturamentoServico> 
                list = new FaturamentoServico().Find<FaturamentoServico>(criteria);

            foreach (FaturamentoServico faturamentoServico in list)
                ValorTotal += faturamentoServico.ValorTotal;

            return ValorTotal;
        }
        #endregion

        #region GerarFaturamentoPedido

        private static float GerarFaturamentoPedido(Contrato contrato, Faturamento faturamento)
        {
            int IdServico = 0;
            decimal ValorTotal = 0.0M;
            decimal ValorTotalServico = 0.0M;

            string criteria = "IdContrato=" + contrato.Id + " ORDER BY IdServico, IdCliente";

            ArrayList listContratoServico = new ContratoServico().Find(criteria);

            FaturamentoServico faturamentoServico = new FaturamentoServico();

            foreach (ContratoServico contratoServico in listContratoServico)
            {
                contratoServico.IdServico.Find();

                if (!contrato.AgruparServico && IdServico != contratoServico.IdServico.Id)
                {
                    IdServico = contratoServico.IdServico.Id;
                    ValorTotalServico = 0.0M;
                    faturamentoServico = new FaturamentoServico();
                    faturamentoServico.Inicialize();
                    faturamentoServico.IdFaturamento.Id = faturamento.Id;
                    faturamentoServico.IdServico.Id = contratoServico.IdServico.Id;
                }

                DateTime dataMaxima = new DateTime(faturamento.GetAnoReferencia(), 
                                                   faturamento.GetMesReferencia(), 
                                                   1).AddMonths(1);

                AddExames(faturamento, contratoServico, ref ValorTotal, ref ValorTotalServico, contrato.Inicio, dataMaxima);

                AddPedidos(faturamento, contratoServico, ref ValorTotal, ref ValorTotalServico, dataMaxima);

                if (ValorTotalServico == 0)
                    continue;
                
                if (!contrato.AgruparServico)
                {
                    faturamentoServico.Quantidade = 1.0F;
                    faturamentoServico.ValorUnitario = Convert.ToSingle(ValorTotalServico);
                    faturamentoServico.ValorTotal = Convert.ToSingle(ValorTotalServico);
                    faturamentoServico.ObservacaoNota += contratoServico.ObservacaoNota;
                    faturamentoServico.Save();
                }
            }

            if (contrato.AgruparServico)
            {
                faturamentoServico = new FaturamentoServico();
                faturamentoServico.Inicialize();
                faturamentoServico.IdFaturamento.Id = faturamento.Id;
                faturamentoServico.IdServico.Id = contrato.IdServico.Id;
                faturamentoServico.Quantidade = 1.0F;
                faturamentoServico.ValorUnitario = Convert.ToSingle(ValorTotal);
                faturamentoServico.ValorTotal = Convert.ToSingle(ValorTotal);
                faturamentoServico.Save();
            }

            return Convert.ToSingle(ValorTotal);
        }

        #region AddPedidos

        private static void AddPedidos( Faturamento faturamento, 
                                        ContratoServico contratoServico, 
                                        ref decimal ValorTotal, 
                                        ref decimal ValorTotalServico, 
                                        DateTime dataMaxima)
        {
            //Obrigacoes

            string criteria = "IdContratoServico=" + contratoServico.Id;

            ArrayList listContratoServicoObrigacao = new ContratoServicoObrigacao().Find(criteria);
            
            foreach (ContratoServicoObrigacao contratoServicoObrigacao in listContratoServicoObrigacao)
            {
                if (contratoServicoObrigacao.MesFaturamento != 0)
                {
                    DateTime dataReferencia = new DateTime(faturamento.GetAnoReferencia(), faturamento.GetMesReferencia(), 1);
                    DateTime dataFaturamento = new DateTime(faturamento.GetAnoReferencia(), contratoServicoObrigacao.MesFaturamento, 1);

                    if (dataFaturamento >= dataReferencia)
                        continue;
                }
                StringBuilder strFindPedido = new StringBuilder();
                strFindPedido.Append("DataSolicitacao IS NOT NULL");
                strFindPedido.Append(" AND DataConclusao IS NOT NULL");
                strFindPedido.Append(" AND DataConclusao<'" + dataMaxima.ToString("yyyy-MM-dd") + "'");
                strFindPedido.Append(" AND IdCliente=" + contratoServico.IdCliente.Id);
                strFindPedido.Append(" AND IdObrigacao=" + contratoServicoObrigacao.IdObrigacao.Id);
                strFindPedido.Append(" AND IdPedido NOT IN (SELECT IdPedido FROM FaturamentoPedido)");

                ArrayList listPedido = new Pedido().Find(strFindPedido.ToString());

                foreach (Pedido pedido in listPedido)
                {
                    FaturamentoPedido faturamentoPedido = new FaturamentoPedido();
                    faturamentoPedido.Inicialize();
                    faturamentoPedido.IdFaturamento.Id = faturamento.Id;
                    faturamentoPedido.IdPedido.Id = pedido.Id;
                    faturamentoPedido.IdServico.Id = contratoServico.IdServico.Id;
                    faturamentoPedido.IdCliente.Id = contratoServico.IdCliente.Id;
                    faturamentoPedido.ValorUnitario = contratoServicoObrigacao.ValorUnitario;
                    faturamentoPedido.Save();

                    ValorTotal += Convert.ToDecimal(contratoServicoObrigacao.ValorUnitario);
                    ValorTotalServico += Convert.ToDecimal(contratoServicoObrigacao.ValorUnitario);
                }
            }
        }
        #endregion

        #region AddExames

        private static void AddExames(  Faturamento faturamento, 
                                        ContratoServico contratoServico, 
                                        ref decimal ValorTotal, 
                                        ref decimal ValorTotalServico, 
                                        DateTime dataInicio, 
                                        DateTime dataMaxima)
        {
            string criteria = "IdContratoServico=" + contratoServico.Id;

            List<ContratoServicoExameDicionario>
                listExamesDicionarios = new ContratoServicoExameDicionario().Find<ContratoServicoExameDicionario>(criteria);

            foreach (ContratoServicoExameDicionario contratoServicoExameDic in listExamesDicionarios)
            {
                StringBuilder strFindExame = new StringBuilder();
                strFindExame.Append("IdExameDicionario=" + contratoServicoExameDic.IdExameDicionario.Id);
                strFindExame.Append(" AND DataExame>='" + dataInicio.ToString("yyyy-MM-dd") + "'");
                strFindExame.Append(" AND DataExame<'" + dataMaxima.ToString("yyyy-MM-dd") + "'");
                strFindExame.Append(" AND IdJuridica NOT IN (SELECT IdClinica FROM Clinica WHERE IsClinicaInterna = 1)");
                strFindExame.Append(" AND IndResultado<>0");
                strFindExame.Append(" AND IdEmpregado IN (SELECT nID_EMPREGADO FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblEMPREGADO WHERE nID_EMPR=" + contratoServico.IdCliente.Id + ")");
                strFindExame.Append(" AND IdExameBase NOT IN (SELECT IdExameBase FROM FaturamentoExameBase)");

                List<ExameBase> exames = new ExameBase().Find<ExameBase>(strFindExame.ToString());

                foreach (ExameBase exameBase in exames)
                {
                    FaturamentoExameBase faturamentoExameBase = new FaturamentoExameBase();
                    faturamentoExameBase.Inicialize();
                    faturamentoExameBase.IdExameBase.Id = exameBase.Id;
                    faturamentoExameBase.IdFaturamento.Id = faturamento.Id;
                    faturamentoExameBase.IdServico.Id = contratoServico.IdServico.Id;
                    faturamentoExameBase.IdCliente.Id = contratoServico.IdCliente.Id;
                    faturamentoExameBase.ValorUnitario = contratoServicoExameDic.ValorUnitario;
                    faturamentoExameBase.Save();
                }
                ValorTotal += (exames.Count * Convert.ToDecimal(contratoServicoExameDic.ValorUnitario));
                ValorTotalServico += (exames.Count * Convert.ToDecimal(contratoServicoExameDic.ValorUnitario));
            }
        }
        #endregion

        #endregion

        #region RecalculaFaturamentoPedido

        private float RecalculaFaturamentoPedido()
        {
            decimal ValorTotal = 0.0M;

            ArrayList listFaturamentoServico = new FaturamentoServico().Find("IdFaturamento=" + this.Id);

            foreach (FaturamentoServico faturamentoServico in listFaturamentoServico)
            {
                decimal ValorTotalServico = 0.0M;

                string criteria = "IdFaturamento=" + this.Id
                                  + " AND IdServico=" + faturamentoServico.IdServico.Id;

                ArrayList listPedidos = new FaturamentoPedido().Find(criteria);
                
                foreach (FaturamentoPedido faturamentoPedido in listPedidos)
                    ValorTotalServico += Convert.ToDecimal(faturamentoPedido.ValorUnitario);

                ArrayList listExames = new FaturamentoExameBase().Find(criteria);
                
                foreach (FaturamentoExameBase faturamentoExameBase in listExames)
                    ValorTotalServico += Convert.ToDecimal(faturamentoExameBase.ValorUnitario);

                faturamentoServico.ValorUnitario = Convert.ToSingle(ValorTotalServico);
                faturamentoServico.ValorTotal = Convert.ToSingle(ValorTotalServico);
                faturamentoServico.Save();

                ValorTotal += ValorTotalServico;
            }

            return Convert.ToSingle(ValorTotal);
        }
        #endregion

        #endregion

        #region NotaFiscal

        public string GetTextoNotaFiscal()
        {
            Juridica juridica = new Juridica();
            juridica.Find(this.IdSacado.Id);

            if (this.IdCedente.IdCobranca == null)
                this.IdCedente.Find();

            if (this.IdCedente.IndTipoNota != (int)TipoNota.NotaFiscal)
                throw new Exception("O cedente não é do tipo nota fiscal!");

            Endereco endereco = juridica.GetEndereco();
            StringBuilder ret = new StringBuilder();
            ret.Append(PrintDirect.INICIALIZA);
            ret.Append(PrintDirect.SELECT_10CPI);
            ret.Append("\r\n");
            //			ret.Append(PrintDirect.LINHA_7_72);
            ret.Append("\r\n");
            //			ret.Append(PrintDirect.LINHA_1_6);
            ret.Append("\r\n");
            ret.Append(AddLine(67, PrintDirect.DRAF, this.Numero.ToString("000000")));
            ret.Append("\r\n");
            ret.Append("\r\n");
            ret.Append("\r\n");
            ret.Append(AddLine(55, PrintDirect.DRAF, "PRESTAÇÃO DE SERVIÇO"));

            ArrayList listServicos = new FaturamentoServico().Find("IdFaturamento=" + this.Id);

            ((FaturamentoServico)listServicos[0]).IdServico.Find();

            ret.Append(AddLine(55, PrintDirect.CONDENSED, ((FaturamentoServico)listServicos[0]).GetPrestacaoDeSerivosNF() + PrintDirect.DES_CONDENSED));
            ret.Append(AddLine(52, PrintDirect.DRAF, this.Emissao.Day.ToString() + "  " + this.Emissao.Month.ToString() + "  " + this.Emissao.Year.ToString()));
            ret.Append("\r\n");
            ret.Append(AddLine(10, PrintDirect.DRAF, juridica.NomeCompleto.ToUpper()));
            ret.Append(AddLine(10, PrintDirect.DRAF, endereco.GetEndereco().ToUpper()));

            string line = Add(10, string.Empty, endereco.GetCidade().ToUpper());
            line += Add(58, line, endereco.GetEstado().ToUpper());
            line += Add(68, line, endereco.Cep.ToUpper());
            line += "\r\n";
            ret.Append(line);

            string line2 = Add(15, string.Empty, juridica.NomeCodigo.ToUpper());
            line2 += Add(42, line2, PrintDirect.CONDENSED + juridica.IE + PrintDirect.DES_CONDENSED);
            line2 += Add(70, line2, "-");//ISCR.CCM
            line2 += "\r\n";
            ret.Append(line2);

            ret.Append(AddLine(15, PrintDirect.DRAF, "REFERÊNCIA " + this.Emissao.ToString("MMM/yyyy").ToUpper() + " VENCIMENTO EM " + this.Vencimento.ToString("dd-MM-yyyy").ToUpper()));

            ret.Append("\r\n");
            ret.Append("\r\n");
            ret.Append("\r\n");

            foreach (FaturamentoServico servico in listServicos)
            {
                servico.IdServico.Find();
                string lineServico = servico.GetDescricaoServicoNF();
                ret.Append(lineServico.Substring(0, lineServico.LastIndexOf("\r\n")));
                int LastIndex = ret.ToString().LastIndexOf("\r\n");
                string line3 = ret.ToString().Substring(LastIndex);
                line3 += Add(80 - servico.ValorTotal.ToString("##,###,###0.00").Length, line3, servico.ValorTotal.ToString("##,###,###0.00"));

                ret.Remove(LastIndex, ret.ToString().Length - LastIndex).ToString();

                ret.Append(line3);
                ret.Append("\r\n");
            }


            char[] CRLF = { Convert.ToChar("\n") };
            string[] paragrafos = ret.ToString().Split(CRLF);

            for (int i = 0; i < (30 - paragrafos.Length); i++)
                ret.Append("\r\n");

            ret.Append(AddLine(5, PrintDirect.DRAF, @"I - ""DOCUMENTO EMITIDO POR ME OU EPP OPTANTE PELO SIMPLES NACIONAL""; e"));
            ret.Append(AddLine(5, PrintDirect.DRAF, @"II - ""NÃO GERA DIREITO A CRÉDITO FISCAL DE ICMS E DE ISS"""));

            ret.Append("\r\n");

            ret.Append(AddLine(5, PrintDirect.DRAF, "SERVIÇOS PRESTADOS SEM A CESSÃO DE MÃO-DE-OBRA E DE COLOCAÇÃO DE TRA-"));
            ret.Append(AddLine(5, PrintDirect.DRAF, "BALHADORES A DISPOSIÇÃO DA EMPRESA CONTRATANTE EM SUAS DEPENDÊNCIAS."));

            ret.Append("\r\n");

            ret.Append(AddLine(78 - this.ValorTotal.ToString("##,###,###0.00").Length, PrintDirect.DRAF, this.ValorTotal.ToString("##,###,###0.00")));

            ret.Append("\r\n");
            ret.Append("\r\n");
            ret.Append("\r\n");
            ret.Append("\r\n");
            ret.Append("\r\n");
            ret.Append("\r\n");
            ret.Append("\r\n");
            ret.Append("\r\n");
            ret.Append("\r\n");
            ret.Append("\r\n");
            ret.Append("\r\n");
            ret.Append("\r\n");

            ret.Append(AddLine(67, PrintDirect.DRAF, this.Numero.ToString("000000")));

            //			ret.Append(PrintDirect.NOVA_PAGINA);
            string[] paragrafosTotal = ret.ToString().Split(CRLF);
            for (int i = paragrafosTotal.Length; i <= 54; i++)
                ret.Append("\r\n");

            //			return ImprimirComLinhas(ret.ToString());

            return Ilitera.Common.Utility.RemoveAcentos(ret.ToString());
        }

        private string ImprimirComLinhas(string ret)
        {
            StringBuilder Linhas = new StringBuilder();

            char[] CRLF = { Convert.ToChar("\n") };

            string[] Paragrafos = ret.ToString().Split(CRLF);

            int i = 1;

            foreach (string paragrafo in Paragrafos)
            {
                string append;

                if (paragrafo.Length > 2)
                    append = i.ToString("00") + paragrafo.Substring(2);
                else
                    append = i.ToString("00");

                Linhas.Append(append);

                Linhas.Append("\n");

                i++;
            }
            return Linhas.ToString();
        }

        private string Add(int position, string line, string text)
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < position - line.Length; i++)
                str.Append(" ");

            str.Append(text);

            return str.ToString();
        }


        private string AddLine(int position, string font, string text)
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < position; i++)
                str.Append(" ");

            str.Append(font);

            str.Append(text);

            str.Append("\r\n");

            return str.ToString();
        }

        #endregion
    }
    #endregion

    #region Class Recibos

    [Database("recibos", "tblRECIBO", "ID")]
    public class Recibos : Ilitera.Data.Table
    {
        private int _IdRecibos;
        private DateTime _Dt_criacao_recibo = DateTime.Today;
        private string _Nm_razao_social = string.Empty;
        private string _Ds_endereco = string.Empty;
        private string _Nm_cidade = string.Empty;
        private string _Nm_bairro = string.Empty;
        private string _Sg_estado = string.Empty;
        private string _Cd_cep = string.Empty;
        private string _Cd_inscr_estadual = string.Empty;
        private string _Cd_cnpj = string.Empty;
        private DateTime _Dt_vencimento = DateTime.Today;
        private DateTime _Dt_emissao = DateTime.Today;
        private float _Vl_total_recibo;
        private float _QT_ASSES;
        private string _Dt_processamento;
        private int _CODIGO;
        private string _RECIBO_NF = string.Empty;
        private int _MOTORISTA;
        private string _DESCR_NF_5 = string.Empty;
        private float _VALOR_NF_5;
        private float _VALOR_NF_1;
        private string _DESCR_NF_6 = string.Empty;
        private float _VALOR_NF_6;
        private bool _Contabilizado;
        private int _NossoNumero;
        private DateTime _DataEntrega;
        private int _IndEntregaBoleto;
        private DateTime _DataAcesso;
        private bool _NaoReterIRAbaixo10;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Recibos()
        {

        }
        public override int Id
        {
            get { return _IdRecibos; }
            set { _IdRecibos = value; }
        }
        public DateTime Dt_criacao_recibo
        {
            get { return _Dt_criacao_recibo; }
            set { _Dt_criacao_recibo = value; }
        }
        public string Nm_razao_social
        {
            get { return _Nm_razao_social; }
            set { _Nm_razao_social = value; }
        }
        public string Ds_endereco
        {
            get { return _Ds_endereco; }
            set { _Ds_endereco = value; }
        }
        public string Nm_cidade
        {
            get { return _Nm_cidade; }
            set { _Nm_cidade = value; }
        }
        public string Nm_bairro
        {
            get { return _Nm_bairro; }
            set { _Nm_bairro = value; }
        }
        public string Sg_estado
        {
            get { return _Sg_estado; }
            set { _Sg_estado = value; }
        }
        public string Cd_cep
        {
            get { return _Cd_cep; }
            set { _Cd_cep = value; }
        }
        public string Cd_inscr_estadual
        {
            get { return _Cd_inscr_estadual; }
            set { _Cd_inscr_estadual = value; }
        }
        public string Cd_cnpj
        {
            get { return _Cd_cnpj; }
            set { _Cd_cnpj = value; }
        }
        public DateTime Dt_vencimento
        {
            get { return _Dt_vencimento; }
            set { _Dt_vencimento = value; }
        }
        public DateTime Dt_emissao
        {
            get { return _Dt_emissao; }
            set { _Dt_emissao = value; }
        }
        public float Vl_total_recibo
        {
            get { return _Vl_total_recibo; }
            set { _Vl_total_recibo = value; }
        }
        public float QT_ASSES
        {
            get { return _QT_ASSES; }
            set { _QT_ASSES = value; }
        }
        public string Dt_processamento
        {
            get { return _Dt_processamento; }
            set { _Dt_processamento = value; }
        }
        public int CODIGO
        {
            get { return _CODIGO; }
            set { _CODIGO = value; }
        }
        public string RECIBO_NF
        {
            get { return _RECIBO_NF; }
            set { _RECIBO_NF = value; }
        }
        public int MOTORISTA
        {
            get { return _MOTORISTA; }
            set { _MOTORISTA = value; }
        }
        public string DESCR_NF_5
        {
            get { return _DESCR_NF_5; }
            set { _DESCR_NF_5 = value; }
        }
        public float VALOR_NF_5
        {
            get { return _VALOR_NF_5; }
            set { _VALOR_NF_5 = value; }
        }
        public float VALOR_NF_1
        {
            get { return _VALOR_NF_1; }
            set { _VALOR_NF_1 = value; }
        }
        public string DESCR_NF_6
        {
            get { return _DESCR_NF_6; }
            set { _DESCR_NF_6 = value; }
        }
        public float VALOR_NF_6
        {
            get { return _VALOR_NF_6; }
            set { _VALOR_NF_6 = value; }
        }
        public bool Contabilizado
        {
            get { return _Contabilizado; }
            set { _Contabilizado = value; }
        }

        public int NossoNumero
        {
            get { return _NossoNumero; }
            set { _NossoNumero = value; }
        }
        public DateTime DataEntrega
        {
            get { return _DataEntrega; }
            set { _DataEntrega = value; }
        }
        public int IndEntregaBoleto
        {
            get { return _IndEntregaBoleto; }
            set { _IndEntregaBoleto = value; }
        }
        public DateTime DataAcesso
        {
            get { return _DataAcesso; }
            set { _DataAcesso = value; }
        }
        public bool NaoReterIRAbaixo10
        {
            get { return _NaoReterIRAbaixo10; }
            set { _NaoReterIRAbaixo10 = value; }
        }
        public override int Save()
        {
            if (this.NossoNumero == 0)
                throw new Exception("Crie novo boleto através do Faturamento!");

            return base.Save();
        }
    }
    #endregion
}
