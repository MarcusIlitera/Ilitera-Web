using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using DAL;

namespace BLL
{
    public class UsuarioBLL
    {
        public Usuario AutenticarUsuaro(string usuario, string senha)
        {
            UsuarioDAL usuarioDal = new UsuarioDAL();
            return usuarioDal.AutenticarUsuaro(usuario, senha);
        }

        public Usuario VerificarEmail(string nomeUsuario)
        {
            UsuarioDAL usuarioDal = new UsuarioDAL();
            return usuarioDal.VerificarEmail(nomeUsuario);
        }

        public UsuarioDTO RetornarDados(int idUsuario)
        {
            UsuarioDAL usuarioDal = new UsuarioDAL();
            return usuarioDal.RetornarDados(idUsuario);
        }

        public bool AlterarSenha(string senha, string novaSenha, int idUsuario)
        {
            UsuarioDAL usuarioDal = new UsuarioDAL();
            return usuarioDal.AlterarSenha(senha, novaSenha, idUsuario);
        }
    }
}
