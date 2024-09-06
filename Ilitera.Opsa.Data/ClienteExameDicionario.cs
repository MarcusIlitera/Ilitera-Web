using System;
using System.Data;
using System.Collections;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{
    //[Database("opsa", "ClienteExameDicionario", "IdClienteExameDicionario")]
    //public class ClienteExameDicionario : Ilitera.Data.Table
    //{
    //    private int _IdClienteExameDicionario;
    //    private ExameDicionario _IdExameDicionario;
    //    private Cliente _IdCliente;
    //    private float _ValorUnitario = 0F;

    //    public ClienteExameDicionario()
    //    {

    //    }
    //    public override int Id
    //    {
    //        get { return _IdClienteExameDicionario; }
    //        set { _IdClienteExameDicionario = value; }
    //    }
    //    public Cliente IdCliente
    //    {
    //        get { return _IdCliente; }
    //        set { _IdCliente = value; }
    //    }
    //    public ExameDicionario IdExameDicionario
    //    {
    //        get { return _IdExameDicionario; }
    //        set { _IdExameDicionario = value; }
    //    }
    //    public float ValorUnitario
    //    {
    //        get { return _ValorUnitario; }
    //        set { _ValorUnitario = value; }
    //    }

    //    public void Reajustar(ContratoReajuste contratoReajuste)
    //    {
    //        ClienteExameDicionarioReajuste reajuste = new ClienteExameDicionarioReajuste();
    //        reajuste.Inicialize();
    //        reajuste.IdClienteExameDicionario = this;
    //        reajuste.IdContratoReajuste = contratoReajuste;
    //        reajuste.ValorAnterior = this.ValorUnitario;
    //        reajuste.ValorComReajuste = Convert.ToSingle(System.Math.Round(Convert.ToDecimal((this.ValorUnitario * contratoReajuste.IdCotacao.ValorCotacao) + this.ValorUnitario), 2));

    //        this.ValorUnitario = reajuste.ValorComReajuste;

    //        reajuste.Transaction = contratoReajuste.Transaction;
    //        reajuste.Save();

    //        this.Transaction = contratoReajuste.Transaction;
    //        this.Save();
    //    }
    //}
}
