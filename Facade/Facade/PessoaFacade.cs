using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;
using Entities;

namespace Facade
{
    public class PessoaFacade
    {
        public static Pessoa RetornarPessoa(int IdPessoa)
        {
            PessoaBLL pessoaBll = new PessoaBLL();
            return pessoaBll.RetornarPessoa(IdPessoa);
        }
    }
}
