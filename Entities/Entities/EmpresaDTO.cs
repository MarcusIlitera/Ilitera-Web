using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class EmpresaDTO
    {
        public int IdJuridica { get; set; }
        public int IdPessoa { get; set; }
        public int QtdEmpregados { get; set; }
        public string NomeAbreviado { get; set; }
        public DateTime DataCadastro { get; set; }
        public string NomeCompleto { get; set; }
        public int IndTipoPessoa { get; set; }
        public int IdCliente { get; set; }
        public int AtivarLocalDeTrabalho { get; set; }
        public int IdJuridicaPai { get; set; }
        public string NomeCodigo { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public string CNPJ { get; set; }
        public string Atividade { get; set; }
        public string IE { get; set; }
        public string CCM { get; set; }
        public int IdGrupoEmpresa { get; set; }
        public string Observacao { get; set; }
        public string Diretor { get; set; }
        public int IdCnae { get; set; }
    }
}
