using System;
using System.Collections.Generic;
using System.Text;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "HistogramaRuido", "IdHistogramaRuido")]
    public class HistogramaRuido : Ilitera.Data.Table
    {
        private int _IdHistogramaRuido;
        private Ghe _IdGhe;
        private DateTime _Inicio;
        private decimal _NivelRuido;
        private int _NumeroAmostras;
        private int _Amostras8h;
        private decimal _Percentual;
        private decimal _PercentualJornada;
        private decimal _CalculoLeg;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public HistogramaRuido()
        {

        }

        public override int Id
        {
            get { return _IdHistogramaRuido; }
            set { _IdHistogramaRuido = value; }
        }
        public Ghe IdGhe
        {
            get { return _IdGhe; }
            set { _IdGhe = value; }
        }
        public DateTime Inicio
        {
            get { return _Inicio; }
            set { _Inicio = value; }
        }
        public decimal NivelRuido
        {
            get { return _NivelRuido; }
            set { _NivelRuido = value; }
        }
        public int NumeroAmostras
        {
            get { return _NumeroAmostras; }
            set { _NumeroAmostras = value; }
        }
        public int Amostras8h
        {
            get { return _Amostras8h; }
            set { _Amostras8h = value; }
        }
        public decimal Percentual
        {
            get { return _Percentual; }
            set { _Percentual = value; }
        }
        public decimal PercentualJornada
        {
            get { return _PercentualJornada; }
            set { _PercentualJornada = value; }
        }
        public decimal CalculoLeg
        {
            get { return _CalculoLeg; }
            set { _CalculoLeg = value; }
        }
    }
}
