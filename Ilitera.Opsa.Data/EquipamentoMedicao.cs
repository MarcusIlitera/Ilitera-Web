using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    [Database("opsa", "EquipamentoMedicao", "IdEquipamentoMedicao")]
    public class EquipamentoMedicao : Ilitera.Data.Table
    {

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EquipamentoMedicao()
        {

        }

        private int _IdEquipamentoMedicao;

        public override int Id
        {
            get { return _IdEquipamentoMedicao; }
            set { _IdEquipamentoMedicao = value; }
        }

        private Risco _IdRisco;

        public Risco IdRisco
        {
            get { return _IdRisco; }
            set { _IdRisco = value; }
        }

        private AgenteQuimico _IdAgenteQuimico;

        public AgenteQuimico IdAgenteQuimico
        {
            get { return _IdAgenteQuimico; }
            set { _IdAgenteQuimico = value; }
        }

        private bool _Sugestao;

        public bool Sugestao
        {
            get { return _Sugestao; }
            set { _Sugestao = value; }
        }

        private bool _IsInativo;

        public bool IsInativo
        {
            get { return _IsInativo; }
            set { _IsInativo = value; }
        }

        private string _Nome = string.Empty;
        
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        private string _Descricao = string.Empty;

        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        private string _Tipo = string.Empty;

        public string Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        private string _Marca = string.Empty;

        public string Marca
        {
            get { return _Marca; }
            set { _Marca = value; }
        }
        private string _Modelo = string.Empty;

        public string Modelo
        {
            get { return _Modelo; }
            set { _Modelo = value; }
        }
        private string _NumeroSerie = string.Empty;

        public string NumeroSerie
        {
            get { return _NumeroSerie; }
            set { _NumeroSerie = value; }
        }
        private string _ArquivoFoto = string.Empty;

        public string ArquivoFoto
        {
            get { return _ArquivoFoto; }
            set { _ArquivoFoto = value; }
        }
        private string _Loboratorio = string.Empty;

        public string Loboratorio
        {
            get { return _Loboratorio; }
            set { _Loboratorio = value; }
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return Nome;
        }

        public string GetDataCalibracao(DateTime dataLaudo)
        {
            string ret = string.Empty;

            if (dataLaudo == new DateTime())
                return ret;

            string strWhere = "IdEquipamentoMedicao=" + this.Id
                + " AND DataCalibracao <='" + dataLaudo.ToString("yyyy-MM-dd") + "'";

            ArrayList listCalibracao = new EquipamentoMedicaoCalibracao().FindMax("DataCalibracao", strWhere);

            if (listCalibracao.Count > 0)
                ret = " " + ((EquipamentoMedicaoCalibracao)listCalibracao[0]).ToString() + ".";

            return ret;
        }

        public static string GetListaEquipamentoMedicao(ArrayList listEquipMed, DateTime dataLaudo)
        {
            StringBuilder str = new StringBuilder();

            foreach (EquipamentoMedicao equip in listEquipMed)
            {
                equip.IdRisco.Find();

                //if (equip.Nome != "Vide Laudo" && str.ToString().IndexOf(equip.Descricao) == -1)
                if (str.ToString().IndexOf(equip.Descricao) == -1)
                {
                    if (equip.IdRisco.Id == 0 && equip.IdAgenteQuimico.Id != 0)
                    {
                        equip.IdAgenteQuimico.Find();

                        str.Append(equip.IdAgenteQuimico.Nome);
                        str.Append('\n');
                    }
                    else if (equip.IdRisco.Id == 0 && equip.IdAgenteQuimico.Id == 0)
                    {
                        str.Append("Ruído Contínuo ou Intermitente\n");
                    }
                    else
                    {
                        // str.Append(equip.IdRisco.DescricaoResumido);
                        // str.Append('\n');
                        AgenteQuimico xAgente = new AgenteQuimico();
                        string xAg = "";

                        xAgente = equip.IdAgenteQuimico;

                        //colocando esta primeira linha, a segunda funciona, caso contrário, xAg fica sempre igual a "" - Wagner
                        xAg = xAgente.ToString();
                        xAg = xAgente.Nome.ToString().Trim();

                        str.Append(equip.IdRisco.DescricaoResumido);
                        if (xAg != "")
                        {
                            str.Append(" (" + xAg + ")");
                        }
                        str.Append('\n');
                    }
                    str.Append(equip.Descricao.Replace("DATA_CALIBRACAO", equip.GetDataCalibracao(dataLaudo)));
                    str.Append('\n');
                    str.Append('\n');
                }
            }

            return str.ToString();
        }
    }

    [Database("opsa", "EquipamentoCalibrador", "IdEquipamentoCalibrador","", "Equipamento Calibrador")]
    public class EquipamentoCalibrador : Ilitera.Data.Table
    {

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EquipamentoCalibrador()
        {

        }

        private int _IdEquipamentoCalibrador;

        public override int Id
        {
            get { return _IdEquipamentoCalibrador; }
            set { _IdEquipamentoCalibrador = value; }
        }

        private string _Descricao = string.Empty;

        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }
    }


    [Database("opsa", "EquipamentoMedicaoCalibracao", "IdEquipamentoMedicaoCalibracao")]
    public class EquipamentoMedicaoCalibracao : Ilitera.Data.Table
    {

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EquipamentoMedicaoCalibracao()
        {

        }

        private int _IdEquipamentoMedicaoCalibracao;

        public override int Id
        {
            get { return _IdEquipamentoMedicaoCalibracao; }
            set { _IdEquipamentoMedicaoCalibracao = value; }
        }

        private EquipamentoMedicao _IdEquipamentoMedicao;

        public EquipamentoMedicao IdEquipamentoMedicao
        {
            get { return _IdEquipamentoMedicao; }
            set { _IdEquipamentoMedicao = value; }
        }

        private EquipamentoCalibrador _IdEquipamentoCalibrador;

        public EquipamentoCalibrador IdEquipamentoCalibrador
        {
            get { return _IdEquipamentoCalibrador; }
            set { _IdEquipamentoCalibrador = value; }
        }

        private DateTime _DataEnvio;

        public DateTime DataEnvio
        {
            get { return _DataEnvio; }
            set { _DataEnvio = value; }
        }

        private DateTime _DataCalibracao;

        public DateTime DataCalibracao
        {
            get { return _DataCalibracao; }
            set { _DataCalibracao = value; }
        }

        private DateTime _Validade;

        public DateTime Validade
        {
            get { return _Validade; }
            set { _Validade = value; }
        }

        private string _ArquivoCertificadoCalibracao = string.Empty;

        public string ArquivoCertificadoCalibracao
        {
            get { return _ArquivoCertificadoCalibracao; }
            set { _ArquivoCertificadoCalibracao = value; }
        }

        public override string ToString()
        {
            return " Data da Calibração "+ this.DataCalibracao.ToString("dd-MM-yyyy");
        }
    }


    [Database("opsa", "EquipamentoMedicaoPrestador", "IdEquipamentoMedicaoPrestador")]
    public class EquipamentoMedicaoPrestador : Ilitera.Data.Table
    {

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EquipamentoMedicaoPrestador()
        {

        }

        private int _IdEquipamentoMedicaoPrestador;

        public override int Id
        {
            get { return _IdEquipamentoMedicaoPrestador; }
            set { _IdEquipamentoMedicaoPrestador = value; }
        }

        private EquipamentoMedicao _IdEquipamentoMedicao;

        public EquipamentoMedicao IdEquipamentoMedicao
        {
            get { return _IdEquipamentoMedicao; }
            set { _IdEquipamentoMedicao = value; }
        }

        private Prestador _IdPrestador;

        public Prestador IdPrestador
        {
            get { return _IdPrestador; }
            set { _IdPrestador = value; }
        }

        private DateTime _Recebido;

        public DateTime Recebido
        {
            get { return _Recebido; }
            set { _Recebido = value; }
        }

        private DateTime _Devolvido;

        public DateTime Devolvido
        {
            get { return _Devolvido; }
            set { _Devolvido = value; }
        }
    }



[Database("Sied_Novo", "tblEquipamento_Calibracao", "IdEquipamento_Calibracao")]
    public class EquipamentoCalibracao : Ilitera.Data.Table
    {

        private int _IdEquipamento_Calibracao;
        private int _nId_Empr;
        private string _Tipo_Equipamento;
        private string _Equipamento;
        private string _Fabricante;
        private string _Modelo;
        private DateTime _Data_Aquisicao;
        private string _Numero_Serie;
        private string _Localizacao;
        private Int16 _IdPeriodicidade;
        private Int16 _Periodicidade_Calibracao;
        private string _Certificado;
        private string _Assistencia_Tecnica;
        private string _Plano_Manutencao_Preventiva;
        private string _Relatorio_Afericao;
        private string _TAG;
        private string _Faixa_Utilizacao;
        private string _Setor;
        private string _Tipo_Monitoramento;
        private DateTime _Proximo_Monitoramento;
        private string _Resultado;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EquipamentoCalibracao()
        {

        }

        public override int Id
        {
            get { return _IdEquipamento_Calibracao; }
            set { _IdEquipamento_Calibracao = value; }
        }
        public int nId_Empr
        {
            get { return _nId_Empr; }
            set { _nId_Empr = value; }
        }
        public string Tipo_Equipamento
        {
            get { return _Tipo_Equipamento; }
            set { _Tipo_Equipamento = value; }
        }
        public string Equipamento
        {
            get { return _Equipamento; }
            set { _Equipamento = value; }
        }
        public string Fabricante
        {
            get { return _Fabricante; }
            set { _Fabricante = value; }
        }
        public string Modelo
        {
            get { return _Modelo; }
            set { _Modelo = value; }
        }
        public DateTime Data_Aquisicao
        {
            get { return _Data_Aquisicao; }
            set { _Data_Aquisicao = value; }
        }
        public string Numero_Serie
        {
            get { return _Numero_Serie; }
            set { _Numero_Serie = value; }
        }
        public string Localizacao
        {
            get { return _Localizacao; }
            set { _Localizacao = value; }
        }
        public Int16 IdPeriodicidade
        {
            get { return _IdPeriodicidade; }
            set { _IdPeriodicidade = value; }
        }
        public Int16 Periodicidade_Calibracao
        {
            get { return _Periodicidade_Calibracao; }
            set { _Periodicidade_Calibracao = value; }
        }
        public string Certificado
        {
            get { return _Certificado; }
            set { _Certificado = value; }
        }
        public string Assistencia_Tecnica
        {
            get { return _Assistencia_Tecnica; }
            set { _Assistencia_Tecnica = value; }
        }
        public string Plano_Manutencao_Preventiva
        {
            get { return _Plano_Manutencao_Preventiva; }
            set { _Plano_Manutencao_Preventiva= value; }
        }
        public string Relatorio_Afericao
        {
            get { return _Relatorio_Afericao; }
            set { _Relatorio_Afericao = value; }
        }
        public string TAG
        {
            get { return _TAG; }
            set { _TAG = value; }
        }
        public string Faixa_Utilizacao
        {
            get { return _Faixa_Utilizacao; }
            set { _Faixa_Utilizacao = value; }
        }
        public string Setor
        {
            get { return _Setor; }
            set { _Setor = value; }
        }
        public string Tipo_Monitoramento
        {
            get { return _Tipo_Monitoramento; }
            set { _Tipo_Monitoramento = value; }
        }
        public DateTime Proximo_Monitoramento
        {
            get { return _Proximo_Monitoramento; }
            set { _Proximo_Monitoramento = value; }
        }
        public string Resultado
        {
            get { return _Resultado; }
            set { _Resultado = value; }
        }



    }





    [Database("Sied_Novo", "tblEquipamento_Calibracao_Anexos", "IdEquipamento_Calibracao_Anexos")]
    public class EquipamentoCalibracao_Anexos : Ilitera.Data.Table
    {

        private int _IdEquipamento_Calibracao_Anexos;
        private int _IdEquipamento_Calibracao;        
        private string _Arquivo;
        private string _Descricao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EquipamentoCalibracao_Anexos()
        {

        }

        public override int Id
        {
            get { return _IdEquipamento_Calibracao_Anexos; }
            set { _IdEquipamento_Calibracao_Anexos = value; }
        }
        public int IdEquipamento_Calibracao
        {
            get { return _IdEquipamento_Calibracao; }
            set { _IdEquipamento_Calibracao = value; }
        }
        public string Arquivo
        {
            get { return _Arquivo; }
            set { _Arquivo = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

    }





    [Database("Sied_Novo", "tblEquipamento_Calibracao_Manutencao", "IdEquipamento_Calibracao_Manutencao")]
    public class EquipamentoCalibracao_Manutencao : Ilitera.Data.Table
    {

        private int _IdEquipamento_Calibracao_Manutencao;
        private int _IdEquipamento_Calibracao;
        private DateTime _Data_Manutencao;
        private string _Manutencao_Corretiva;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public EquipamentoCalibracao_Manutencao()
        {

        }

        public override int Id
        {
            get { return _IdEquipamento_Calibracao_Manutencao; }
            set { _IdEquipamento_Calibracao_Manutencao = value; }
        }
        public int IdEquipamento_Calibracao
        {
            get { return _IdEquipamento_Calibracao; }
            set { _IdEquipamento_Calibracao = value; }
        }
        public DateTime Data_Manutencao
        {
            get { return _Data_Manutencao; }
            set { _Data_Manutencao = value; }
        }
        public string Manutencao_Corretiva
        {
            get { return _Manutencao_Corretiva; }
            set { _Manutencao_Corretiva = value; }
        }

    }







    [Database("Sied_Novo", "tblAnalise_Laboratorial", "IdAnalise")]
    public class Analise_Laboratorial : Ilitera.Data.Table
    {

        private int _IdAnalise;
        private int _IdTipoAnalise;
        private int _IdManipulador;
        private Int16 _Periodicidade;
        private Int16 _PeriodicidadeAnalise;
        private DateTime _UltimaAnalise;
        private DateTime _ProximaAnalise;
        private string _Resultado;
        private int _nId_Empr;        
        private string _Obs;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Analise_Laboratorial()
        {

        }

        public override int Id
        {
            get { return _IdAnalise; }
            set { _IdAnalise = value; }
        }
        public int IdTipoAnalise
        {
            get { return _IdTipoAnalise; }
            set { _IdTipoAnalise = value; }
        }
        public int IdManipulador
        {
            get { return _IdManipulador; }
            set { _IdManipulador = value; }
        }
        public Int16 Periodicidade
        {
            get { return _Periodicidade; }
            set { _Periodicidade = value; }
        }
        public Int16 PeriodicidadeAnalise
        {
            get { return _PeriodicidadeAnalise; }
            set { _PeriodicidadeAnalise = value; }
        }
        public DateTime UltimaAnalise
        {
            get { return _UltimaAnalise; }
            set { _UltimaAnalise = value; }
        }
        public DateTime ProximaAnalise
        {
            get { return _ProximaAnalise; }
            set { _ProximaAnalise = value; }
        }
        public string Resultado
        {
            get { return _Resultado; }
            set { _Resultado = value; }
        }
        public int nId_Empr
        {
            get { return _nId_Empr; }
            set { _nId_Empr = value; }
        }
        public string Obs
        {
            get { return _Obs; }
            set { _Obs = value; }
        }

    }


    
    [Database("Sied_Novo", "tblAnalise_Laboratorial_Anexos", "IdAnalise_Laboratorial_Anexos")]
    public class Analise_Laboratorial_Anexos : Ilitera.Data.Table
    {

        private int _IdAnalise_Laboratorial_Anexos;
        private int _IdAnalise;
        private string _Arquivo;
        private string _Descricao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Analise_Laboratorial_Anexos()
        {

        }

        public override int Id
        {
            get { return _IdAnalise_Laboratorial_Anexos; }
            set { _IdAnalise_Laboratorial_Anexos = value; }
        }
        public int IdAnalise
        {
            get { return _IdAnalise; }
            set { _IdAnalise = value; }
        }
        public string Arquivo
        {
            get { return _Arquivo; }
            set { _Arquivo = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

    }


    [Database("Sied_Novo", "tblAnalise_Laboratorial_Tipo", "IdTipoAnalise")]
    public class Analise_Laboratorial_Tipo : Ilitera.Data.Table
    {

        private int _IdTipoAnalise;        
        private string _Descricao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Analise_Laboratorial_Tipo()
        {

        }

        public override int Id
        {
            get { return _IdTipoAnalise; }
            set { _IdTipoAnalise= value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

    }


    [Database("Sied_Novo", "tblAnalise_Laboratorial_Manipulador", "IdManipulador")]
    public class Analise_Laboratorial_Manipulador : Ilitera.Data.Table
    {

        private int _IdManipulador;
        private int _nId_Empr;
        private string _Descricao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Analise_Laboratorial_Manipulador()
        {

        }

        public override int Id
        {
            get { return _IdManipulador; }
            set { _IdManipulador = value; }
        }
        public int nId_Empr
        {
            get { return _nId_Empr; }
            set { _nId_Empr = value; }
        }
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

    }




}
