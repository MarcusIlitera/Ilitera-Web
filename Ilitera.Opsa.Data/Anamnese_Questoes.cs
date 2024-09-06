using System;
using System.Data;
using System.Collections;
using Ilitera.Data;

namespace Ilitera.Opsa.Data
{


    [Database("opsa", "Anamnese_Exame", "IdAnamneseExame")]
    public class Anamnese_Exame : Ilitera.Data.Table
    {
        private int _IdAnamneseExame;        
        private int _IdAnamneseDinamica;
        private int _IdExameBase;        
        private string _Resultado;
        private Int16 _Peso;   //histórico do peso no momento da salva
        private string _Obs;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Anamnese_Exame()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Anamnese_Exame(int Id)
        {
            this.Find(Id);
        }

        public override int Id
        {
            get { return _IdAnamneseExame; }
            set { _IdAnamneseExame = value; }
        }


        public int IdAnamneseDinamica
        {
            get { return _IdAnamneseDinamica; }
            set { _IdAnamneseDinamica = value; }
        }
        
        public int IdExameBase
        {
            get { return _IdExameBase; }
            set { _IdExameBase = value; }
        }

        public string Resultado
        {
            get { return _Resultado; }
            set { _Resultado = value; }
        }

        public Int16 Peso
        {
            get { return _Peso; }
            set { _Peso = value; }
        }

        public string Obs
        {
            get { return _Obs; }
            set { _Obs = value; }
        }


    }




    [Database("opsa", "Anamnese_Dinamica", "IdAnamneseDinamica")]
    public class Anamnese_Dinamica : Ilitera.Data.Table
    {
        private int _IdAnamneseDinamica;
        private int _IdPessoa;
        private int _IdAnamneseQuestao;               
        private Int16 _Peso;
        private bool _Ativo;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Anamnese_Dinamica()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Anamnese_Dinamica(int Id)
        {
            this.Find(Id);
        }

        public override int Id
        {
            get { return _IdAnamneseDinamica; }
            set { _IdAnamneseDinamica = value; }
        }

        public int IdPessoa
        {
            get { return _IdPessoa; }
            set { _IdPessoa = value; }
        }

        public int IdAnamneseQuestao
        {
            get { return _IdAnamneseQuestao; }
            set { _IdAnamneseQuestao = value; }
        }


        public Int16 Peso
        {
            get { return _Peso; }
            set { _Peso = value; }
        }


        public bool Ativo
        {
            get { return _Ativo; }
            set { _Ativo = value; }
        }

    }



    [Database("opsa", "Anamnese_Questao", "IdAnamneseQuestao")]
    public class Anamnese_Questao : Ilitera.Data.Table
    {
        private int _IdAnamneseQuestao;
        private Anamnese_Sistema _IdAnamneseSistema;
        //private Anamnese_Sistema _Sistema;
        private string _Questao;
        private Int16 _Peso_Padrao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Anamnese_Questao()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Anamnese_Questao(int Id)
        {
            this.Find(Id);
        }

        public override int Id
        {
            get { return _IdAnamneseQuestao; }
            set { _IdAnamneseQuestao = value; }
        }
    
        //public int IdAnamneseQuestao
        //{
        //    get { return _IdAnamneseQuestao; }
        //    set { _IdAnamneseQuestao = value; }
        //}

        public Anamnese_Sistema IdAnamneseSistema
        {
            get { return _IdAnamneseSistema; }
            set { _IdAnamneseSistema = value; }
        }

        public string Sistema
        {
            get {   return _IdAnamneseSistema.Sistema; }
            //set { this.IdAnamneseSistema.Sistema = value; }
        }

        public string Questao
        {
            get { return _Questao; }
            set { _Questao = value; }
        }

        public Int16 Peso_Padrao
        {
            get { return _Peso_Padrao; }
            set { _Peso_Padrao = value; }
        }

        public override string ToString()
        {
            return _Questao;
        }

    }


    [Database("opsa", "Anamnese_Sistema", "IdAnamneseSistema")]
    public class Anamnese_Sistema : Ilitera.Data.Table
    {
        private int _IdAnamneseSistema;
        private string _Sistema;
        private Int16 _Escore_Critico;
        private Int16 _Peso_Padrao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Anamnese_Sistema()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Anamnese_Sistema(int Id)
        {
            this.Find(Id);
        }

        public override int Id
        {
            get { return _IdAnamneseSistema; }
            set { _IdAnamneseSistema = value; }
        }

        public int IdAnamneseSistema
        {
            get { return _IdAnamneseSistema; }
            set { _IdAnamneseSistema = value; }
        }


        public string Sistema
        {
            get { return _Sistema; }
            set { _Sistema = value; }
        }

        public Int16 Escore_Critico
        {
            get { return _Escore_Critico; }
            set { _Escore_Critico = value; }
        }

        public Int16 Peso_Padrao
        {
            get { return _Peso_Padrao; }
            set { _Peso_Padrao = value; }
        }

        public override string ToString()
        {
            return _Sistema;
        }
    }

}
