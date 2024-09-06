using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using BLL;

namespace Facade
{
    public class ContatoTelefonicoFacade
    {
        public static List<ContatoTelefonico> RetornarTelefones(int IdPessoa)
        {
            ContatoTelefonicoBLL contatoTelefonicoBll = new ContatoTelefonicoBLL();
            return contatoTelefonicoBll.RetornarTelefones(IdPessoa);
        }
    }
}
