using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class ContatoTelefonico
    {
        public int IdContatoTelefonico { get; set; }
        public int IdPessoa { get; set; }
        public int IndTipoTelefone { get; set; }
        public string Numero { get; set; }
        public string DDD { get; set; }
        public string Nome { get; set; }
        public string Departamento { get; set; }
    }
}
