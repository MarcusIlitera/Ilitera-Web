using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Empregado
    {
        public int IdEmpregado { get; set; }
        public int IdEmpresa { get; set; }
        public string NomeEmpregado { get; set; }
        public string ApelidoEmpregado { get; set; }
        public string pesoEmpregado { get; set; }
        public string AlturaEmpregado { get; set; }
        public string Sexoempregado { get; set; }
        public DateTime DataNascimento { get; set; }
        public string matricula {get; set;}
        public string numCTPS {get; set;}
        public string serieCTPS {get; set;}
        public string ufCTPS { get; set; }
        public DateTime DataAdmissao {get; set;}
        public DateTime DataDemissao {get; set;}
        public string endLogradouro {get; set;}
        public string endNumero {get; set;}
        public string endComplemento {get; set;}
        public string endBairro {get; set;}
        public string endMunicipio {get; set;}
        public string endUF {get; set;}
        public string endCEP {get; set;}
        public string telefone {get; set;}
        public string Cpf { get; set; }
        public string Identidade { get; set; }
        public string PISPASEP { get; set; }
        public string nNOFOTO { get; set; }
        public string nInd_Beneficiario { get; set; }
        public string teMail { get; set; }
        public string Funcao { get; set; }
        public string Setor { get; set; }
        public DateTime InicioFuncao { get; set; }
        public bool gTerceiro { get; set; }
        public string tObs { get; set; }
    }
}
