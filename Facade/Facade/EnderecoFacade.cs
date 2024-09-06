using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using BLL;

namespace Facade
{
    public class EnderecoFacade
    {
        public static Endereco RetornarEndereco(int IdPessoa)
        {
            EnderecoBLL enderecoBll = new EnderecoBLL();
            return enderecoBll.RetornarEndereco(IdPessoa);
        }
    }
}
