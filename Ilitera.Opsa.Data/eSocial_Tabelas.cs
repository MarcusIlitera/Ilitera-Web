using System;
using System.Collections;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{


    
    [Database("sied_novo", "tbl_eSocial", "IdeSocial")]
    public class tbleSocial : Ilitera.Data.Table
    {
        private int _IdeSocial;
        private int _IdUsuario;
        private string _Evento;
        private DateTime _DataHora_Criacao;
        private string _Ambiente;

        
        public tbleSocial()
        {
        }

        public override int Id
        {
            get { return _IdeSocial; }
            set { _IdeSocial = value; }
        }
        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public string Evento
        {
            get { return _Evento; }
            set { _Evento = value; }
        }
        public string Ambiente
        {
            get { return _Ambiente; }
            set { _Ambiente = value; }
        }
        public DateTime DataHora_Criacao
        {
            get { return _DataHora_Criacao; }
            set { _DataHora_Criacao = value; }
        }
    }


    
    [Database("sied_novo", "tbl_eSocial_Envio", "IdeSocial_Envio")]
    public class tbleSocial_Envio : Ilitera.Data.Table
    {
        private int _IdeSocial_Envio;
        private int _IdeSocial_Deposito;
        private int _IdUsuario;
        private DateTime _Data_Envio;
        private string _Tipo_Envio;
        private string _Processamento_Lote;
        private string _Processamento_Retorno;
        private string _Protocolo;

        public tbleSocial_Envio()
        {
        }

        public override int Id
        {
            [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
            get { return _IdeSocial_Envio; }
            [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
            set { _IdeSocial_Envio = value; }
        }
        public int IdeSocial_Deposito
        {
            get { return _IdeSocial_Deposito; }
            set { _IdeSocial_Deposito = value; }
        }
        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public DateTime Data_Envio
        {
            get { return _Data_Envio; }
            set { _Data_Envio = value; }
        }
        public string Tipo_Envio
        {
            get { return _Tipo_Envio; }
            set { _Tipo_Envio = value; }
        }
        public string Processamento_Lote
        {
            get { return _Processamento_Lote; }
            set { _Processamento_Lote = value; }
        }
        public string Processamento_Retorno
        {
            get { return _Processamento_Retorno; }
            set { _Processamento_Retorno = value; }
        }
        public string Protocolo
        {
            get { return _Protocolo; }
            set { _Protocolo = value; }
        }

    }


    [Database("sied_novo", "tbl_eSocial_Token", "IdeSocial_Token")]
    public class tbleSocial_Token : Ilitera.Data.Table
    {
        private int _IdeSocial_Token;
        private string _Token;
        private DateTime _Criado;        
        private string _Usuario;
        private string _Senha;
        private bool _Utilizado;


        public tbleSocial_Token()
        {
            
        }

        public tbleSocial_Token(int id )
        {
            this.Find(id);
        }


        public override int Id
        {            
            get { return _IdeSocial_Token; }         
            set { _IdeSocial_Token = value; }
        }


        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }
        public DateTime Criado
        {
            get { return _Criado; }
            set { _Criado = value; }
        }
        public string Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }
        public string Senha
        {
            get { return _Senha; }
            set { _Senha = value; }
        }
        public bool Utilizado
        {
            get { return _Utilizado; }
            set { _Utilizado = value; }
        }

    }




    [Database("sied_novo", "tbl_eSocial_API_Log", "IdeSocial_API_Log")]
    public class tbleSocial_API_Log : Ilitera.Data.Table
    {
        private int _IdeSocial_API_Log;
        private string _Token;
        private DateTime _DataHora;
        private string _Modulo;
        private string _CNPJ;
        private DateTime _DataInicial;
        private DateTime _DataFinal;


        public tbleSocial_API_Log()
        {

        }

        public tbleSocial_API_Log(int id)
        {
            this.Find(id);
        }


        public override int Id
        {
            get { return _IdeSocial_API_Log; }
            set { _IdeSocial_API_Log = value; }
        }
        
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }
        public DateTime DataHora
        {
            get { return _DataHora; }
            set { _DataHora = value; }
        }
        public string Modulo
        {
            get { return _Modulo; }
            set { _Modulo = value; }
        }
        public string CNPJ
        {
            get { return _CNPJ; }
            set { _CNPJ = value; }
        }
        public DateTime DataInicial
        {
            get { return _DataInicial; }
            set { _DataInicial = value; }
        }
        public DateTime DataFinal
        {
            get { return _DataFinal; }
            set { _DataFinal = value; }
        }

    }



    [Database("sied_novo", "tbl_eSocial_13_Parte_Corpo_Atingida", "Codigo")]
    public class eSocial_13_Parte_Corpo_Atingida : Ilitera.Data.Table
    {
        private int _Codigo;
        private string _Descricao;

        
        public eSocial_13_Parte_Corpo_Atingida()
        {
        }

        public override int Id
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
    }


    [Database("sied_novo", "tbl_eSocial_14_Agente_Acidente_Trabalho", "Codigo")]
    public class eSocial_14_Agente_Acidente_Trabalho : Ilitera.Data.Table
    {
        private int _Codigo;
        private string _Descricao;

        

        public eSocial_14_Agente_Acidente_Trabalho()
        {
        }


        public override int Id
        {
            get { return _Codigo; }
            
            set { _Codigo = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
    }


    
    [Database("sied_novo", "tbl_eSocial_15_Agente_Situacao_Doenca_Profissional", "Codigo")]
    public class eSocial_15_Agente_Situacao_Doenca_Profissional : Ilitera.Data.Table
    {
        private int _Codigo;
        private string _Descricao;

        
        public eSocial_15_Agente_Situacao_Doenca_Profissional()
        {
        }

        public override int Id
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
    }



   
    [Database("sied_novo", "tbl_eSocial_16_Situacao_Geradora_Acidente_Trabalho", "Codigo")]
    public class eSocial_16_Situacao_Geradora_Acidente_Trabalho : Ilitera.Data.Table
    {
        private int _Codigo;
        private string _Descricao;

        
        public eSocial_16_Situacao_Geradora_Acidente_Trabalho()
        {
        }


        public override int Id
        {
            get { return _Codigo; }
            
            set { _Codigo = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
    }


    
    [Database("sied_novo", "tbl_eSocial_17_Natureza_Lesao", "Codigo")]
    public class eSocial_17_Natureza_Lesao : Ilitera.Data.Table
    {
        private int _Codigo;
        private string _Descricao;

        public eSocial_17_Natureza_Lesao()
        {
        }

        public override int Id
        {
            get { return _Codigo; }
            
            set { _Codigo = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
    }


    
    [Database("sied_novo", "tbl_eSocial_24", "Codigo")]
    public class eSocial_24_Tipo_Acidente : Ilitera.Data.Table
    {
        private int _Codigo;
        private string _Situacao;

        

        public eSocial_24_Tipo_Acidente()
        {

        }


        
        public override int Id
        {
            get { return _Codigo; }
            
            set { _Codigo = value; }
        }
        public string Situacao
        {
            get { return _Situacao; }
            set { _Situacao = value; }
        }
    }

    
    [Database("sied_novo", "tbl_eSocial_Municipios", "Cod_Municipio_Completo")]
    public class eSocial_Municipios : Ilitera.Data.Table
    {
        private int _Cod_Municipio_Completo;
        private int _UF;
        private string _Nome_UF;
        private string _Cod_Municipio;
        private string _Nome_Município;

        
        public eSocial_Municipios()
        {
        }

        
        public override int Id
        {
            get { return _Cod_Municipio_Completo; }
            
            set { _Cod_Municipio_Completo = value; }
        }

        public int UF
        {
            get { return _UF; }
            set { _UF = value; }
        }

        public string Nome_UF
        {
            get { return _Nome_UF; }
            set { _Nome_UF = value; }
        }

        public string Cod_Municipio
        {
            get { return _Cod_Municipio; }
            set { _Cod_Municipio = value; }
        }

        public string Nome_Município
        {
            get { return _Nome_Município; }
            set { _Nome_Município = value; }
        }


    }


    [Database("sied_novo", "tbl_eSocial_Deposito", "IdeSocial_Deposito")]
    public class tbleSocial_Deposito : Ilitera.Data.Table
    {
        private int _IdeSocial_Deposito;
        private int _IdeSocial;
        private string _XMLData;
        private string _NomeArquivo;
        private DateTime _DataHora;
        private Int16 _Seq;
        private string _CPF;
        private Int64 _NIT;
        private string _CNPJ;
        private DateTime _Dt_Ini;
        private DateTime _Dt_Fim;
        private Int32 _nId_Chave;

        public tbleSocial_Deposito()
        {
        }

        public override int Id
        {
            get { return _IdeSocial_Deposito; }
            set { _IdeSocial_Deposito = value; }
        }
        public int IdeSocial
        {
            get { return _IdeSocial; }
            set { _IdeSocial = value; }
        }
        public string XMLData
        {
            get { return _XMLData; }
            set { _XMLData = value; }
        }
        public string NomeArquivo
        {
            get { return _NomeArquivo; }
            set { _NomeArquivo = value; }
        }
        public DateTime DataHora
        {
            get { return _DataHora; }
            set { _DataHora = value; }
        }
        public Int16 Seq
        {
            get { return _Seq; }
            set { _Seq = value; }
        }
        public string CPF
        {
            get { return _CPF; }
            set { _CPF = value; }
        }
        public Int64 NIT
        {
            get { return _NIT; }
            set { _NIT = value; }
        }
        public string CNPJ
        {
            get { return _CNPJ; }
            set { _CNPJ = value; }
        }
        public DateTime Dt_Ini
        {
            get { return _Dt_Ini; }
            set { _Dt_Ini = value; }
        }
        public DateTime Dt_Fim
        {
            get { return _Dt_Fim; }
            set { _Dt_Fim = value; }
        }
        public Int32 nId_Chave
        {
            get { return _nId_Chave; }
            set { _nId_Chave = value; }
        }


    }







}
