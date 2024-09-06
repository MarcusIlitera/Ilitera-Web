using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Pessoa
    {
        public int IdPessoa { get; set; }
        public string NomeCodigo { get; set; }
        public string NomeAbreviado { get; set; }
        public string NomeCompleto { get; set; }
        public int IndTipoPessoa { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
