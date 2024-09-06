using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using DAL;

namespace BLL
{
    public class ContatoTelefonicoBLL
    {
        public List<ContatoTelefonico> RetornarTelefones(int IdPessoa)
        {
            ContatoTelefonicoDAL contatoTelefonicoDal = new ContatoTelefonicoDAL();
            return contatoTelefonicoDal.RetornarTelefones(IdPessoa);
        }
    }
}
