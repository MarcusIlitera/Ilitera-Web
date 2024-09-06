using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Endereco
    {
        public int IdEndereco { get; set; }
        public string Municipio { get; set; }
        public string UF{ get; set; }
        public string CEP { get; set; }
        public int IdTipoLogradouro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
    }
}
