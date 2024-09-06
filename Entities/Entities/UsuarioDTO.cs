using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataUltimoLogin { get; set; }
        public int NumeroLogin { get; set; }
        public string NomeAbreviado { get; set; }
        public string Email { get; set; }
    }
}
