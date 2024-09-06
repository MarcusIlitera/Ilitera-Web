using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using DAL;

namespace BLL
{
    public class EnderecoBLL
    {
        public Endereco RetornarEndereco(int IdPessoa)
        {
            EnderecoDAL enderecoDal = new EnderecoDAL();
            return enderecoDal.RetornarEndereco(IdPessoa);
        }
    }
}
