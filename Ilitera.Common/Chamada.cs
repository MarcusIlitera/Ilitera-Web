using System;
using Ilitera.Data;

namespace Ilitera.Common
{
    public enum ChamadaStatus : int
    {
        Solucionado = 1,
        NaoSolucionado,
        DeixouRecado
    }

    public enum ChamadaTipo : int
    {
        Recebida,
        Realizada
    }

    [Database("opsa", "Chamada", "IdChamada")]
    public class Chamada : Ilitera.Data.Table
    {
        private int _IdChamada;
        private Pessoa _IdPessoa;
        private Pessoa _IdPessoaContato;
        private Pessoa _IdPessoaCriador;
        private ChamadaTipo _IndChamadaTipo;
        private DateTime _DataInicio;
        private DateTime _DataTermino;
        private int _IndStatusChamada;
        private string _Descricao = string.Empty;
        private string _Solucao = string.Empty;
        private DateTime _DataSolucao;
        private OrigemProblema _IndOrigemProblema;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Chamada()
        {

        }

        public enum OrigemProblema : int
        {
            NaoInformado,
            ErroNetPDT,
            ErroMestraNet,
            ConfiguracaoNetPDT,
            ConfiguracaoMestraNet,
            Usuario,
            Cliente,
            Mestra
        }

        public override int Id
        {
            get { return _IdChamada; }
            set { _IdChamada = value; }
        }
        [Obrigatorio(true, "A pessoa é obrigatório!")]
        public Pessoa IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }
        public Pessoa IdPessoaContato
        {
            get { return _IdPessoaContato; }
            set { _IdPessoaContato = value; }
        }
        public Pessoa IdPessoaCriador
        {
            get { return _IdPessoaCriador; }
            set { _IdPessoaCriador = value; }
        }
        public ChamadaTipo IndChamadaTipo
        {
            get { return _IndChamadaTipo; }
            set { _IndChamadaTipo = value; }
        }
        public DateTime DataInicio
        {
            get { return _DataInicio; }
            set { _DataInicio = value; }
        }
        public DateTime DataTermino
        {
            get { return _DataTermino; }
            set { _DataTermino = value; }
        }
        [Obrigatorio(true, "Status é campo obrigatório!")]
        public int IndStatusChamada
        {
            get { return _IndStatusChamada; }
            set { _IndStatusChamada = value; }
        }
        [Obrigatorio(true, "Descrição é campo obrigatório!")]
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
        public string Solucao
        {
            get { return _Solucao; }
            set { _Solucao = value; }
        }
        public DateTime DataSolucao
        {
            get { return _DataSolucao; }
            set { _DataSolucao = value; }
        }

        public OrigemProblema IndOrigemProblema
        {
            get { return _IndOrigemProblema; }
            set { _IndOrigemProblema = value; }
        }

        public override void Validate()
        {
            if (this.IndStatusChamada == (int)ChamadaStatus.Solucionado
                && this.DataSolucao == new DateTime())
                throw new Exception("Data de Solução é campo obrigatório!");

            base.Validate();
        }
    }
}
