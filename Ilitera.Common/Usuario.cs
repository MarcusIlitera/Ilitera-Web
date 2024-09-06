using System;
using System.Data;
using System.Text;
using System.Collections;
using Ilitera.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Ilitera.Common
{
    [Database("opsa", "Usuario", "IdUsuario")]
    public class Usuario : Ilitera.Data.Table
    {
        #region Properties

        private int _IdUsuario;
        private Pessoa _IdPessoa;
        //Excluir banco
        //private int _IndUsuarioPessoa;
        private string _NomeUsuario = string.Empty;
        private DateTime _datCadastro = DateTime.Today;
        private DateTime _datUltLogin = new DateTime();
        private DateTime _datUltLoginHist = new DateTime();
        private Usuario _IdUsuarioPai;
        private string _desSenha = string.Empty;
        private string _desSenhaConfirma = string.Empty;
        private DateTime _datSenha = DateTime.Today;
        private string _emailSenha = string.Empty;
        private int _numTentLogin;
        private int _numLogin = 0;

        private static Usuario m_usuario;
        private static Prestador m_prestador;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Usuario()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Usuario(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        [Obrigatorio(true, "A pessoa é obrigatório!")]
        public Pessoa IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }
        //public int IndUsuarioPessoa
        //{
        //    get { return _IndUsuarioPessoa; }
        //    set { _IndUsuarioPessoa = value; }
        //}
        public string NomeUsuario
        {
            get { return _NomeUsuario; }
            set { _NomeUsuario = value; }
        }
        public DateTime DatCadastro
        {
            get { return _datCadastro; }
            set { _datCadastro = value; }
        }
        public DateTime DatUltLogin
        {
            get { return _datUltLogin; }
            set { _datUltLogin = value; }
        }
        public DateTime DatUltLoginHist
        {
            get { return _datUltLoginHist; }
            set { _datUltLoginHist = value; }
        }
        public Usuario IdUsuarioPai
        {
            get { return _IdUsuarioPai; }
            set { _IdUsuarioPai = value; }
        }
        [Obrigatorio(true, "A senha é obrigatório!")]
        public string DesSenha
        {
            get { return _desSenha; }
            set { _desSenha = value; }
        }
        [Persist(false)]
        public string DesSenhaConfirma
        {
            get { return _desSenhaConfirma; }
            set { _desSenhaConfirma = value; }
        }
        public DateTime datSenha
        {
            get { return _datSenha; }
            set { _datSenha = value; }
        }
        [Persist(false)]
        public string EmailSenha
        {
            get
            {
                if (_IdPessoa.Email != null)
                {
                    if (this._IdPessoa.Email == string.Empty)
                        this._IdPessoa.Find();
                    return _IdPessoa.Email;
                }
                else
                    return string.Empty;
            }
        }
        public int NumTentLogin
        {
            get { return _numTentLogin; }
            set { _numTentLogin = value; }
        }

        public int NumLogin
        {
            get { return _numLogin; }
            set { _numLogin = value; }
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            if (this.IdPessoa.mirrorOld == null)
                this.IdPessoa.Find();

            return this.IdPessoa.NomeAbreviado;
        }
        #endregion

        #region Save

        public override int Save()
        {
            try
            {
                return base.Save();
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("IX_Usuario") != -1)
                    throw new Exception("O nome de login é duplicado! Por favor, escolha outro nome para o login.");
                else
                    throw ex;
            }
        }

        #endregion

        #region Login

        public static Usuario Login(string sUsuario, string sSenha, bool TrocarSenha)
        {
            m_usuario = new Usuario();

            m_usuario.Find("nomeUsuario='" + VerificaNomeUsuario(sUsuario) + "'");

            if (m_usuario.Id == 0)
                throw new Exception("Usuário Inexistente!");

            if (m_usuario.NumTentLogin > 2)
                throw new Exception("Contacte o administrador do sistema! Login Bloqueado!");

            if (m_usuario.DesSenha.ToUpper() != sSenha.ToUpper())
            {
                m_usuario.NumTentLogin++;
                m_usuario.Save();
                m_usuario = new Usuario();

                throw new Exception("Login Inválido!");
            }
            else if (!TrocarSenha && m_usuario.datSenha.AddMonths(12) <= DateTime.Today)
            {
                m_usuario.NumLogin++;
                m_usuario.Save();

                throw new ExceptionSenhaExpirada("A sua senha ultrapassou a data de expiração! Ela deve ser alterada!");
            }
            else
            {
                if (!TrocarSenha)
                    VerificaPassword(m_usuario.DesSenha);

                m_usuario.DatUltLoginHist = m_usuario.DatUltLogin;
                m_usuario.DatUltLogin = DateTime.Now;
                m_usuario.NumTentLogin = 0;
                m_usuario.Save();
            }

            Ilitera.Data.Table.usuario = m_usuario.Id;

            return m_usuario;
        }
        
        public void LoginWeb(string sUsuario, string sSenha, int validadesenha, int IdJuridica)
        {
            //Tipo de Conexão com o banco
            //Ilitera.Data.Table.conexao = Conexao.SQLServer;

            this.Find("nomeUsuario='" + VerificaNomeUsuario(sUsuario) + "'");

            Juridica juridica = new Juridica();
            juridica.Find(IdJuridica);

            if ((juridica.IdJuridicaPapel.Id != (int)IndJuridicaPapel.Cliente &&
                juridica.IdJuridicaPapel.Id != (int)IndJuridicaPapel.Clinica &&
                juridica.IdJuridicaPapel.Id != (int)IndJuridicaPapel.Empresa &&
                !juridica.IsLocalDeTrabalho()))
                throw new ExceptionLogin("Usuário Inexistente!");

            if (juridica.IsLocalDeTrabalho())
            {
                DataSet dsClientePaiInativo = new Juridica().Get("IdJuridica=" + IdJuridica
                    + " AND IdJuridicaPai IS NOT NULL"
                    + " AND IdJuridicaPai IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryClienteAtivos)");

                if (dsClientePaiInativo.Tables[0].Rows.Count.Equals(0))
                    throw new ExceptionLogin("Usuário Inexistente!");
            }

            Ilitera.Data.Table.IsWeb = true;

            if (this.Id == 0)
                throw new ExceptionLogin("Usuário Inexistente!");

            if (this.NumTentLogin > 2)
                throw new ExceptionLogin("Contacte o administrador do sistema! Login Bloqueado!");

            if (this.NomeUsuario == "Admin")
            {
                if (Convert.ToString((DateTime.Now.Hour + 2)) + this.DesSenha.ToUpper() != sSenha.ToUpper())
                {
                    this.NumTentLogin++;
                    this.Save();
                    throw new ExceptionSenhaInvalida("Senha Atual Inválida!");
                }
            }
            else if (this.DesSenha.ToUpper() != sSenha.ToUpper())
            {
                this.NumTentLogin++;
                this.Save();
                throw new ExceptionSenhaInvalida("Senha Atual Inválida!");
            }
            else
            {
                if (this.DatUltLogin == new DateTime() || this.DatUltLogin == new DateTime(1753, 1, 1))
                {
                    this.NumLogin++;
                    this.Save();
                    throw new ExceptionFirstTimeLogin("Esta é a sua primeira entrada no sistema!\\nConfirme alguns dados na janela seguinte!");
                }
                else if (this.datSenha.AddMonths(validadesenha) <= DateTime.Today)
                {
                    this.NumLogin++;
                    this.Save();
                    throw new ExceptionSenhaExpirada("A sua senha ultrapassou a data de expiração! Ela deve ser alterada!");
                }
                else
                {
                    this.DatUltLoginHist = this.DatUltLogin;
                    this.DatUltLogin = DateTime.Now;
                    this.NumTentLogin = 0;
                    this.NumLogin++;
                    this.Save();
                }
            }
        }


        public void LoginWeb(string sUsuario,
                             string sSenha,
                             bool IsLocal)
        {
            LoginWeb(sUsuario, sSenha, 12, IsLocal);
        }

        public void LoginWeb(string sUsuario,
                      string sSenha,
                      int validadesenha,
                      bool IsLocal)
        {
            Ilitera.Data.Table.IsWeb = true;

            this.Find("nomeUsuario='" + VerificaNomeUsuario(sUsuario) + "'");

            if (this.Id == 0)
                throw new ExceptionLogin("Usuário Inexistente!");

            JuridicaPessoa juridicaPessoa = new JuridicaPessoa();
            juridicaPessoa.Find("IdPessoa=" + this.IdPessoa.Id);
            juridicaPessoa.IdJuridica.Find();

            //usado para trocar a senha
            if (validadesenha == 0)
                return;

            Juridica juridica = juridicaPessoa.IdJuridica;

            if (juridica.IdJuridicaPapel.Id != (int)IndJuridicaPapel.Cliente
                 && juridica.IdJuridicaPapel.Id != (int)IndJuridicaPapel.Clinica
                 && juridica.IdJuridicaPapel.Id != (int)IndJuridicaPapel.Empresa
                 && !juridica.IsLocalDeTrabalho())
                throw new ExceptionLogin("Usuário Inexistente!");

            if (juridica.IsLocalDeTrabalho())
            {
                DataSet dsClientePaiInativo = new Juridica().Get("IdJuridica=" + juridica.Id
                    + " AND IdJuridicaPai IS NOT NULL"
                    + " AND IdJuridicaPai IN (SELECT IdCliente FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.qryClienteAtivos)");

                if (dsClientePaiInativo.Tables[0].Rows.Count.Equals(0))
                    throw new ExceptionLogin("Usuário Inexistente!");
            }

            if (this.NumTentLogin > 2)
                throw new ExceptionLogin("Contacte o administrador do sistema! Login Bloqueado!");


            if (this.NomeUsuario == "Admin")
            {
                if (Convert.ToString((DateTime.Now.Hour + 2))
                    + this.DesSenha.ToUpper() != sSenha.ToUpper())
                {
                    this.NumTentLogin++;
                    this.Save();
                    throw new ExceptionSenhaInvalida("Senha Atual Inválida!");
                }
            }
            else if (this.DesSenha.ToUpper() != sSenha.ToUpper())
            {
                this.NumTentLogin++;
                this.Save();
                throw new ExceptionSenhaInvalida("Senha Atual Inválida!");
            }
            else
            {
                if (this.DatUltLogin == new DateTime())
                {
                    this.NumLogin++;
                    this.Save();
                    throw new ExceptionFirstTimeLogin("Esta é a sua primeira entrada no sistema!\\nConfirme alguns dados na janela seguinte!");
                }
                else if (this.datSenha.AddMonths(validadesenha) <= DateTime.Today)
                {
                    this.NumLogin++;
                    this.Save();
                    throw new ExceptionSenhaExpirada("A sua senha ultrapassou a data de expiração! Ela deve ser alterada!");
                }
                else
                {
                    if (!IsLocal)
                    {
                        this.DatUltLoginHist = this.DatUltLogin;
                        this.DatUltLogin = DateTime.Now;
                        this.NumTentLogin = 0;
                        this.NumLogin++;
                        this.Save();
                    }
                }
            }
        }

        private static string VerificaNomeUsuario(string sUsuario)
        {
            //ScapeCaracteres
            //% Any string of zero or more characters.
            //_ Any single character.
            //[ ] Any single character within the specified range (for example, [a-f]) or set (for example, [abcdef]).
            //[^] Any single character not within the specified range (for example, [^a - f]) or set (for example, [^abcdef]).

            if (sUsuario.Length > 25)
                throw new Exception("Usuário Inexistente!");

            if (sUsuario.IndexOf("%") != -1)
                throw new Exception("Usuário Inexistente!");

            if (sUsuario.IndexOf("[") != -1)
                throw new Exception("Usuário Inexistente!");

            if (sUsuario.IndexOf("]") != -1)
                throw new Exception("Usuário Inexistente!");

            if (sUsuario.IndexOf("^") != -1)
                throw new Exception("Usuário Inexistente!");

            if (sUsuario.IndexOf("'") != -1)
                throw new Exception("Usuário Inexistente!");

            if (sUsuario.ToUpper().IndexOf("LIKE") != -1)
                throw new Exception("Usuário Inexistente!");

            if (sUsuario.ToUpper().IndexOf("USUARIO") != -1)
                throw new Exception("Usuário Inexistente!");

            if (sUsuario.ToUpper().IndexOf("NOMEUSUARIO") != -1)
                throw new Exception("Usuário Inexistente!");

            return sUsuario;
        }

        private static string VerificaPassword(string sPwd)
        {
            if (sPwd.Length < 3)
                throw new ExceptionSenhaInvalida("A Senha deve possuir pelo menos 3 caracteres!");

            if (sPwd.Length > 8)
                throw new ExceptionSenhaInvalida("A Senha deve possuir no máximo 8 caracteres!");

            if (sPwd.IndexOf("%") != -1)
                throw new ExceptionSenhaInvalida("A Senha não deve possuir caracteres especiais!");

            if (sPwd.IndexOf("^") != -1)
                throw new ExceptionSenhaInvalida("A Senha não deve possuir caracteres especiais!");

            if (sPwd.IndexOf("[") != -1)
                throw new ExceptionSenhaInvalida("A Senha não deve possuir caracteres especiais!");

            if (sPwd.IndexOf("]") != -1)
                throw new ExceptionSenhaInvalida("A Senha não deve possuir caracteres especiais!");

            //if (sPwd.ToUpper().IndexOf("MESTRA") != -1)
            //    throw new ExceptionSenhaInvalida("Senha Inválida!\n\nA Senha não pode conter a palavra Mestra!");

            if (sPwd.ToUpper().IndexOf("USUARIO") != -1)
                throw new ExceptionSenhaInvalida("Senha Inválida!\n\nA Senha não pode conter a palavra usuario!");

            if (sPwd.Substring(0, 3) == "123")
                throw new ExceptionSenhaInvalida("Senha Inválida!\n\nA Senha não pode conter 123 como caracteres iniciais!");

            if (sPwd.ToUpper().Substring(0, 3) == "ABC")
                throw new ExceptionSenhaInvalida("Senha Inválida!\n\nA Senha não pode conter ABC como caracteres iniciais!");

            if (sPwd.ToUpper().Substring(0, 3) == "QWE")
                throw new ExceptionSenhaInvalida("Senha Inválida!\n\nA Senha não pode conter QWE como caracteres iniciais!");

            return sPwd;
        }


        public static bool IsStrongPassword(string strPwd)
        {
            //Validates a strong password. It must be between 3 and 8 characters, 
            //contain at least one digit and one alphabetic character, and must not contain special characters.

            string strRegex = "(?!^[0-9]*$)"
                                + "(?!^[a-zA-Z]*$)"
                                + "^"
                                + "([a-zA-Z0-9]{3,8})"
                                + "$";

//            Regex regex = new Regex(@"
//                        ^           # anchor at the start
//                       (?=.*\d)     # must contain at least one numeric character
//                       (?=.*[a-z])  # must contain one lowercase character
//                       (?=.*[A-Z])  # must contain one uppercase character
//                       .{8,10}      # From 8 to 10 characters in length
//                       \s           # allows a space 
//                       $            # anchor at the end",
//                       RegexOptions.IgnorePatternWhitespace);


            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(strRegex);

            if (!re.IsMatch(strPwd))
                return (false);

            return (true);
        }

        public static bool ChangePassword(Usuario user, string txtNewPass, string txtConfPass)
        {
            user.DatUltLoginHist = user.DatUltLogin;
            user.DatUltLogin = DateTime.Now;
            user.DesSenha = txtNewPass;
            user.DesSenhaConfirma = txtConfPass;
                
            Usuario usuario = new Usuario();
            usuario.Find(user.Id);

            if (user.DesSenha != usuario.DesSenha)
            {
                if (user.DesSenha == string.Empty || user.DesSenhaConfirma == string.Empty)
                    throw new ExceptionTrocaSenha("A Nova Senha e a Confirmação devem ser preenchidas!");

                VerificaPassword(user.DesSenha);

                if (user.DesSenha != user.DesSenhaConfirma)
                    throw new ExceptionTrocaSenha("A nova senha e a confirmação devem ser idênticas!");

                user.datSenha = System.DateTime.Now;
                user.Save();
                
                if (user.IdPessoa.mirrorOld == null)
                    user.IdPessoa.Find();

                if (!user.IdPessoa.Email.Equals(string.Empty))
                    user.SendEMailTrocaSenha();

                return true;
            }
            else
                throw new ExceptionTrocaSenha("A Nova Senha deve ser diferente da antiga!");
        }

        #endregion
        
        #region Permissao

        public static void Permissao(Type c)
        {
            Permissao(c, Usuario.Login(), AcaoPermissao.Executar);
        }

        public static void Permissao(Type c, AcaoPermissao acao)
        {
            Permissao(c, Usuario.Login(), acao);
        }

        public static void Permissao(Type c, Usuario usuario, AcaoPermissao acao)
        {
            Funcionalidade funcionalidade = new Funcionalidade();
            funcionalidade.Find("Pacote='" + c.ToString() + "'");

            if (funcionalidade.Id == 0)
                return;

            StringBuilder str = new StringBuilder();
            str.Append("USE " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + " ");
            str.Append(" SELECT IndSelecionar, IndIncluir, IndAlterar, IndExcluir, IndExecutar");
            str.Append(" FROM FuncionalidadeGrupo ");
            str.Append(" WHERE IdGrupo IN (SELECT  IdGrupo FROM UsuarioGrupo WHERE  IdUsuario = " + usuario.Id + ")");
            str.Append("	AND IdFuncionalidade = " + funcionalidade.Id);
            str.Append(" UNION ");
            str.Append(" SELECT  IndSelecionar, IndIncluir, IndAlterar, IndExcluir, IndExecutar");
            str.Append(" FROM FuncionalidadeUsuario");
            str.Append(" WHERE IdUsuario = " + usuario.Id + "");
            str.Append("	AND IdFuncionalidade = " + funcionalidade.Id);

            DataSet ds = funcionalidade.ExecuteDataset(str.ToString());

            if (ds.Tables.Count == 0)
                return;

            bool bVal = false;

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (funcionalidade.IndFuncionalidadeTipo == (int)FuncionalidadeTipo.Formulario
                    || funcionalidade.IndFuncionalidadeTipo == (int)FuncionalidadeTipo.Metodo
                    || funcionalidade.IndFuncionalidadeTipo == (int)FuncionalidadeTipo.Relatorio
                    || funcionalidade.IndFuncionalidadeTipo == (int)FuncionalidadeTipo.WebPage)
                {
                    if (Convert.ToInt32(row["IndExecutar"]) == (int)TipoPermissao.NegarPermissao)
                        throw new Exception("O usuário " + usuario.NomeUsuario + " não possui permissão para acessar esta área do sistema!");
                    else if (Convert.ToInt32(row["IndExecutar"]) == (int)TipoPermissao.ComPermissao && !bVal)
                        bVal = true;
                }
                else if (funcionalidade.IndFuncionalidadeTipo == (int)FuncionalidadeTipo.Entity)
                {
                    if (acao == AcaoPermissao.Selecionar)
                    {
                        if (Convert.ToInt32(row["IndSelecionar"]) == (int)TipoPermissao.NegarPermissao)
                            throw new Exception("O usuário " + usuario.NomeUsuario + " não possui permissão para selecionar esse cadastro!");
                        else if (Convert.ToInt32(row["IndSelecionar"]) == (int)TipoPermissao.ComPermissao && !bVal)
                            bVal = true;
                    }
                    else if (acao == AcaoPermissao.Alterar)
                    {
                        if (Convert.ToInt32(row["IndAlterar"]) == (int)TipoPermissao.NegarPermissao)
                            throw new Exception("O usuário " + usuario.NomeUsuario + " não possui permissão para alterar esse cadastro!");
                        else if (Convert.ToInt32(row["IndAlterar"]) == (int)TipoPermissao.ComPermissao && !bVal)
                            bVal = true;
                    }
                    else if (acao == AcaoPermissao.Incluir)
                    {
                        if (Convert.ToInt32(row["IndIncluir"]) == (int)TipoPermissao.NegarPermissao)
                            throw new Exception("O usuário " + usuario.NomeUsuario + " não possui permissão para incluir esse cadastro!");
                        else if (Convert.ToInt32(row["IndIncluir"]) == (int)TipoPermissao.ComPermissao && !bVal)
                            bVal = true;
                    }
                    else if (acao == AcaoPermissao.Excluir)
                    {
                        if (Convert.ToInt32(row["IndExcluir"]) == (int)TipoPermissao.NegarPermissao)
                            throw new Exception("O usuário " + usuario.NomeUsuario + " não possui permissão para excluir esse cadastro!");
                        else if (Convert.ToInt32(row["IndExcluir"]) == (int)TipoPermissao.ComPermissao && !bVal)
                            bVal = true;
                    }
                }
            }

            if (!bVal)
                throw new Exception("O usuário " + usuario.IdPessoa.ToString() + " não possui permissão para acessar esta área do sistema!");
        }


        public static void Permissao_Web( Int32 xUsuario, Int32 xIdFuncionalidade)
        {
            
            //StringBuilder str = new StringBuilder();

            //str.Append("Select IndExecutar from ( " );
            //str.Append(" SELECT IndExecutar ");
            //str.Append(" FROM FuncionalidadeGrupo ");
            //str.Append(" WHERE IdGrupo IN (SELECT  IdGrupo FROM UsuarioGrupo WHERE  IdUsuario = " + xUsuario.ToString() + ")");
            //str.Append("	AND IdFuncionalidade = " + xIdFuncionalidade.ToString());
            //str.Append(" UNION ");
            //str.Append(" SELECT  IndExecutar");
            //str.Append(" FROM FuncionalidadeUsuario");
            //str.Append(" WHERE IdUsuario = " + xUsuario.ToString() + "");
            //str.Append("	AND IdFuncionalidade = " + xIdFuncionalidade.ToString());
            //str.Append(" ) as tx1 order by IndExecutar desc " );

            DataTable table = new DataTable("Result");
            table.Columns.Add("IndExecutar", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xUsuario", SqlDbType.NChar).Value = xUsuario.ToString();
            rCommand.Parameters.Add("@xIdFuncionalidade", SqlDbType.NChar).Value = xIdFuncionalidade.ToString();

            rCommand.CommandText = "Select IndExecutar from ( "
            + " SELECT IndExecutar "
            + " FROM FuncionalidadeGrupo "
            + " WHERE IdGrupo IN (SELECT  IdGrupo FROM UsuarioGrupo WHERE  IdUsuario = @xUsuario ) "
            + "	AND IdFuncionalidade = @xIdFuncionalidade "
            + " UNION "
            + " SELECT  IndExecutar "
            + " FROM FuncionalidadeUsuario "
            + " WHERE IdUsuario = @xUsuario "
            + "	AND IdFuncionalidade = @xIdFuncionalidade "
            + " ) as tx1 order by IndExecutar desc ";


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();

                rCommand.Connection = cnn;
                SqlDataAdapter da;

                da = new SqlDataAdapter(rCommand);
                da.Fill(ds, "Result");

                //SqlDataAdapter da;
                //cnn.Open();
                //da = new SqlDataAdapter(str.ToString(), cnn);
                //da.Fill(ds, "Result");

                cnn.Close();

                da.Dispose();
            }


            //DataSet ds = funcionalidade.ExecuteDataset(str.ToString());

            if (ds.Tables.Count == 0)
                throw new Exception("O usuário não possui permissão para acessar esta área do sistema!");

            if ( ds.Tables[0].Rows.Count < 1 )
                throw new Exception("O usuário não possui permissão para acessar esta área do sistema!");


            bool bVal = false;

            foreach (DataRow row in ds.Tables[0].Rows)
            {

                if (row[0].ToString().Trim() == "1")
                {
                    bVal = true;
                    break;
                }
                else
                {
                    throw new Exception("O usuário não possui permissão para acessar esta área do sistema!");
                }
                             
            }

            if (!bVal)
                throw new Exception("O usuário não possui permissão para acessar esta área do sistema!");
        }


        public static void Permissao_Web_Completo(Int32 xUsuario)
        {         

            DataTable table = new DataTable("Result");
            table.Columns.Add("IndExecutar", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xUsuario", SqlDbType.NChar).Value = xUsuario.ToString();
            

            rCommand.CommandText = "Select IndExecutar from ( "
            + " SELECT IndExecutar "
            + " FROM FuncionalidadeGrupo "
            + " WHERE IdGrupo IN (SELECT  IdGrupo FROM UsuarioGrupo WHERE  IdUsuario = @xUsuario ) "
            + "	AND IdGrupo in ( Select IdGrupo from Grupo where NomeGrupo like 'Web%' and NomeGrupo like '%Completo' ) "
            + " ) as tx1 order by IndExecutar desc ";


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();

                rCommand.Connection = cnn;
                SqlDataAdapter da;

                da = new SqlDataAdapter(rCommand);
                da.Fill(ds, "Result");

                //SqlDataAdapter da;
                //cnn.Open();
                //da = new SqlDataAdapter(str.ToString(), cnn);
                //da.Fill(ds, "Result");

                cnn.Close();

                da.Dispose();
            }


            //DataSet ds = funcionalidade.ExecuteDataset(str.ToString());

            if (ds.Tables.Count == 0)
                throw new Exception("O usuário não possui permissão para acessar esta área do sistema!");

            if (ds.Tables[0].Rows.Count < 1)
                throw new Exception("O usuário não possui permissão para acessar esta área do sistema!");


            bool bVal = false;

            foreach (DataRow row in ds.Tables[0].Rows)
            {

                if (row[0].ToString().Trim() == "1")
                {
                    bVal = true;
                    break;
                }
                else
                {
                    throw new Exception("O usuário não possui permissão para acessar esta área do sistema!");
                }

            }

            if (!bVal)
                throw new Exception("O usuário não possui permissão para acessar esta área do sistema!");
        }




        public bool Permissao_API_eSocial(Int32 xUsuario)
        {

            DataTable table = new DataTable("Result");
            table.Columns.Add("IndExecutar", Type.GetType("System.String"));

            DataSet ds = new DataSet();
            ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xUsuario", SqlDbType.NChar).Value = xUsuario.ToString();


            rCommand.CommandText = "select top 1 indExecutar from funcionalidadeusuario " +
                                   "where IdFuncionalidade in (select idfuncionalidade from funcionalidade where nome like '%esocial%' ) " +
                                   " and idusuario = @xUsuario order by indExecutar desc ";


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();

                rCommand.Connection = cnn;
                SqlDataAdapter da;

                da = new SqlDataAdapter(rCommand);
                da.Fill(ds, "Result");

                //SqlDataAdapter da;
                //cnn.Open();
                //da = new SqlDataAdapter(str.ToString(), cnn);
                //da.Fill(ds, "Result");

                cnn.Close();

                da.Dispose();
            }


            //DataSet ds = funcionalidade.ExecuteDataset(str.ToString());

            if (ds.Tables.Count == 0)
                return false;

            if (ds.Tables[0].Rows.Count < 1)
                return false;


            bool bVal = false;

            foreach (DataRow row in ds.Tables[0].Rows)
            {

                if (row[0].ToString().Trim() == "1")
                {
                    bVal = true;
                    break;
                }
                else
                {
                    bVal = false;
                    break;
                }

            }

            return bVal;

        }


        #endregion

        #region Metodos

        public static Usuario Login()
        {
            return m_usuario;
        }

        public bool IsGrupo(Grupos grupos)
        {
            return IsGrupo(grupos, this.Id);
        }

        public static bool IsGrupo(Grupos grupos, int IdUsuario)
        {
            UsuarioGrupo usuarioGrupo = new UsuarioGrupo();
            usuarioGrupo.Find("IdUsuario=" + IdUsuario + " AND IdGrupo=" + (int)grupos);
            return (usuarioGrupo.Id != 0);
        }

        public string GetVisibilidadePrestadores()
        {
            if (this.IsGrupo(Grupos.Telemarketing)
                || this.IsGrupo(Grupos.Telemarketing2))
                return "IdPessoa IN (-680734952, -680386666, -409835444, -2081331047)";
            else
                return "(IdPessoa = IdPessoa OR IdPessoa IS NULL)";
        }
        
        public static Prestador GetPrestador()
        {
            if (m_prestador == null)
            {
                m_prestador = new Prestador();
                m_prestador.Find("IdPessoa=" + m_usuario.IdPessoa.Id);
            }

            return m_prestador;
        }

        
        public string GetStatusUsuario()
        {
            if (this.Id == 0)
                return "Sem Login";
            else
            {
                if (this.NumTentLogin > 2)
                    return "Usuário Bloqueado";
                else
                    return "Usuário Ativo";
            }
        }

        public List<JuridicaPapel> GetJuridicaPapel()
        {
            StringBuilder str = new StringBuilder();

            str.Append("((IsLocalTrabalho = 0 AND IsVisualizar = 1))");

            //Grupo Diretoria / Administração	
            if (!this.IsGrupo(Grupos.Diretoria) && !this.IsGrupo(Grupos.Administracao))
            {
                //Coordenador PCMSO
                if (this.IsGrupo(Grupos.Pcmso))
                    str.Append(" AND IdJuridicaPapel IN (1,3,8)");
                else
                {
                    //Vendas
                    if (this.IsGrupo(Grupos.Vendas))
                        str.Append(" AND IdJuridicaPapel IN (1,4,8,10,14)");
                    else if (this.IsGrupo(Grupos.Telemarketing2))
                        str.Append(" AND IdJuridicaPapel IN (10)");
                    else if (this.IsGrupo(Grupos.Telemarketing))
                        str.Append(" AND IdJuridicaPapel IN (4,10)");
                    else
                        str.Append(" AND IdJuridicaPapel IN (1,3)");
                }
            }
            str.Append(" ORDER BY Descricao");

            return new JuridicaPapel().Find<JuridicaPapel>(str.ToString());
        }

        #endregion

        #region Avisos

        #region AvisoTrocaSenha

        private void SendEMailTrocaSenha()
        {

        }

        #endregion
        
        #region AvisoUsuarioSenha

        public void EnviarEmailPrimeiraVez(string Subject, Pessoa pessoaContato)
        {

        }

        public void EnviarEmailPrimeiraVez(string Subject, string pathEmail, Pessoa pessoaContato)
        {

        }

        #endregion

        #region AvisoUsuarioSemAcesso

        public static void CheckUserSemAcessoMaiorMes()
        {

        }

        #endregion

        #region Avisos Metodos Comuns

        private static string GetHtmlTableListaUsuarios(List<Usuario> listUsuario)
        {
            Juridica juridica = new Juridica();
            juridica.Find("IdJuridica IN (SELECT IdJuridica FROM JuridicaPessoa WHERE IdPessoa=" + listUsuario[0].IdPessoa.Id + ")");
    
            string nomeCliente = juridica.NomeAbreviado;
            StringBuilder htmlTableUsuario = new StringBuilder();

            htmlTableUsuario.Append(@"<table width=""450"" border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0"" class=""normalFont"">");
            htmlTableUsuario.Append("<tr>");
            htmlTableUsuario.Append(@"<td align=""center"" bgcolor=""#FFFFCC""><strong>");
            htmlTableUsuario.Append(nomeCliente);
            htmlTableUsuario.Append("</strong></td>");
            htmlTableUsuario.Append("</tr>");
            htmlTableUsuario.Append("<tr>");
            htmlTableUsuario.Append("<td>");

            foreach (Usuario usuario in listUsuario)
            {
                usuario.IdPessoa.Find();
                
                htmlTableUsuario.Append(@"<img src=""\..\/Images/5pixel.gif"" width=""5"" height=""5"" border=""0"">");
                htmlTableUsuario.Append(@"<table width=""450"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""normalFont"">");
                htmlTableUsuario.Append("<tr>");
                htmlTableUsuario.Append(@"<td width=""10"" valign=""middle"">-</td>");
                htmlTableUsuario.Append(@"<td width=""440""><strong>");
                htmlTableUsuario.Append(usuario.NomeUsuario);
                htmlTableUsuario.Append("</strong> (");
                htmlTableUsuario.Append(usuario.IdPessoa.NomeCompleto);
                htmlTableUsuario.Append(")<br>");

                if (usuario.DatUltLogin != new DateTime())
                {
                    htmlTableUsuario.Append("&Uacute;ltimo acesso em ");
                    htmlTableUsuario.Append(usuario.DatUltLogin.ToString("dd-MM-yyyy"));
                }
                else
                    htmlTableUsuario.Append("Sem acesso");

                htmlTableUsuario.Append("</td>");
                htmlTableUsuario.Append("</tr></table>");
            }

            htmlTableUsuario.Append("</td>");
            htmlTableUsuario.Append("</tr>");
            htmlTableUsuario.Append("</table>");

            return htmlTableUsuario.ToString();
        }

        #endregion

        #endregion
    }
}
