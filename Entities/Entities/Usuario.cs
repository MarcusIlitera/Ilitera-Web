using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int IdPessoa { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataUltimoLogin { get; set; }
        public DateTime? DataUltimoLoginHist { get; set; }
        public int IdUsuarioPai { get; set; }
        public string Senha { get; set; }
        public DateTime? DataSenha { get; set; }
        public int NumeroTentativas { get; set; }
        public int NumeroLogin { get; set; }
        public int IdPrestador { get; set; }
        public string Email { get; set; }
    }
}
