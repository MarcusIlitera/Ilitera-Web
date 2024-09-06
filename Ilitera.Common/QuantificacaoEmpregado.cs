using System;
using System.Collections.Generic;
using System.Text;

using Ilitera.Data;

namespace Ilitera.Common
{
    [Database("opsa", "QuantificacaoEmpregado", "IdQuantificacaoEmpregado")]
    
    public class QuantificacaoEmpregado: Table
    {
        private int _IdQuantificacaoEmpregado;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public QuantificacaoEmpregado()
        {

        }

        public override int Id
        {
            get { return _IdQuantificacaoEmpregado; }
            set { _IdQuantificacaoEmpregado = value; }
        }

        private Juridica _IdJuridica;

        public Juridica IdJuridica
        {
            get { return _IdJuridica; }
            set { _IdJuridica = value; }
        }
        private CNAE _IdCNAE;

        public CNAE IdCNAE
        {
            get { return _IdCNAE; }
            set { _IdCNAE = value; }
        }
        private DateTime _DataContagem;

        public DateTime DataContagem
        {
            get { return _DataContagem; }
            set { _DataContagem = value; }
        }
        private bool _HasCipa;

        public bool HasCipa
        {
            get { return _HasCipa; }
            set { _HasCipa = value; }
        }
        private bool _HasSesmt;

        public bool HasSesmt
        {
            get { return _HasSesmt; }
            set { _HasSesmt = value; }
        }
        private int _QtdEmpregados;

        public int QtdEmpregados
        {
            get { return _QtdEmpregados; }
            set { _QtdEmpregados = value; }
        }
        private int _QtdTitulares;

        public int QtdTitulares
        {
            get { return _QtdTitulares; }
            set { _QtdTitulares = value; }
        }
        private int _QtdSuplentes;

        public int QtdSuplentes
        {
            get { return _QtdSuplentes; }
            set { _QtdSuplentes = value; }
        }
        private int _QtdTecnicos;

        public int QtdTecnicos
        {
            get { return _QtdTecnicos; }
            set { _QtdTecnicos = value; }
        }
        private int _QtdEngenheiro;

        public int QtdEngenheiro
        {
            get { return _QtdEngenheiro; }
            set { _QtdEngenheiro = value; }
        }
        private int _QtdAuxEnfermeiro;

        public int QtdAuxEnfermeiro
        {
            get { return _QtdAuxEnfermeiro; }
            set { _QtdAuxEnfermeiro = value; }
        }
        private int _QtdEnfermeiro;

        public int QtdEnfermeiro
        {
            get { return _QtdEnfermeiro; }
            set { _QtdEnfermeiro = value; }
        }
        private int _QtdMedico;

        public int QtdMedico
        {
            get { return _QtdMedico; }
            set { _QtdMedico = value; }
        }

        public static void Quantificar()
        {
            List<Juridica> juridicas = new Juridica().Find<Juridica>("IdJuridica IN (SELECT IdCliente FROM qryClienteAtivos)");

            foreach (Juridica juridica in juridicas)
            {
                try
                {
                    Quantificar(juridica);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }

        private static void Quantificar(Juridica juridica)
        {
            QuantificacaoEmpregado qtde = new QuantificacaoEmpregado();
            qtde.Find("IdJuridica=" + juridica.Id
                    + " AND DataContagem='" + DateTime.Today.ToString("yyyy-MM-dd") + "'");

            if (qtde.Id == 0)
                qtde.Inicialize();

            qtde.IdJuridica = juridica;
            qtde.IdCNAE = juridica.GetCnaeCipa();
            qtde.DataContagem = DateTime.Today;
            qtde.HasCipa = juridica.HasCipa();
            qtde.HasSesmt = juridica.HasSesmt();
            qtde.QtdEmpregados = juridica.QtdEmpregados;

            DimensionamentoCipa dim = juridica.GetDimensionamentoCipa();

            qtde.QtdTitulares = dim.Efetivo;
            qtde.QtdSuplentes = dim.Suplente;

            Sesmt sesmt = juridica.GetSesmt();

            qtde.QtdTecnicos = sesmt.Tecnico;
            qtde.QtdEngenheiro = sesmt.Engenheiro;
            qtde.QtdAuxEnfermeiro = sesmt.AuxiliarEnfermagem;
            qtde.QtdEnfermeiro = sesmt.Enfermeiro;
            qtde.QtdMedico = sesmt.Medico;

            qtde.Save();
        }
    }
}
