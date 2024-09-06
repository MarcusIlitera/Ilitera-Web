using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Entities;

namespace BLL
{
    public class PessoaBLL
    {
        public Pessoa RetornarPessoa(int IdPessoa)
        {
            PessoaDAL pessoaDal = new PessoaDAL();
            return pessoaDal.RetornarPessoa(IdPessoa);
        }
    }
}
