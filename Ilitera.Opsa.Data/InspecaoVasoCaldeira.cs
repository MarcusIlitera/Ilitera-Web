using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Ilitera.Data;
using Ilitera.Common;


namespace Ilitera.Opsa.Data
{
    public enum TipoInspecao : int
    {
        Inicial,
        Periodica,
        Extraordinaria
    }

    public enum CondicaoUso : int
    {
        SemResultado,
        Otima,
        Boa,
        Regular,
        Pessima
    }

    [Database("opsa", "InspecaoVasoCaldeira", "IdInspecaoVasoCaldeira")]
    public class InspecaoVasoCaldeira : Ilitera.Opsa.Data.Documento
    {
        public InspecaoVasoCaldeira()
        {

        }

        private int _IdInspecaoVasoCaldeira;

        public override int Id
        {
            get { return _IdInspecaoVasoCaldeira; }
            set { _IdInspecaoVasoCaldeira = value; }
        }

        private VasoCaldeiraBase _IdVasoCaldeiraBase;

        public VasoCaldeiraBase IdVasoCaldeiraBase
        {
            get { return _IdVasoCaldeiraBase; }
            set { _IdVasoCaldeiraBase = value; }
        }

        private int _IndTipoInspecao;

        public int IndTipoInspecao
        {
            get { return _IndTipoInspecao; }
            set { _IndTipoInspecao = value; }
        }

        private Prestador _IdAutoria;

        public Prestador IdAutoria
        {
            get { return _IdAutoria; }
            set { _IdAutoria = value; }
        }

        private int _IndExameInterno;

        public int IndExameInterno
        {
            get { return _IndExameInterno; }
            set { _IndExameInterno = value; }
        }

        private CondicaoUso _IndExameExterno;

        public CondicaoUso IndExameExterno
        {
            get { return _IndExameExterno; }
            set { _IndExameExterno = value; }
        }

        private string _DescricaoExames = string.Empty;

        public string DescricaoExames
        {
            get { return _DescricaoExames; }
            set { _DescricaoExames = value; }
        }

        private string _Resultado = string.Empty;

        public string Resultado
        {
            get { return _Resultado; }
            set { _Resultado = value; }
        }

        private string _Recomendacao = string.Empty;

        public string Recomendacao
        {
            get { return _Recomendacao; }
            set { _Recomendacao = value; }
        }

        private string _Conclusao = string.Empty;

        public string Conclusao
        {
            get { return _Conclusao; }
            set { _Conclusao = value; }
        }

        private DateTime _ProximoExameExterno;

        public DateTime ProximoExameExterno
        {
            get { return _ProximoExameExterno; }
            set { _ProximoExameExterno = value; }
        }

        private DateTime _ProximoExameInterno;

        public DateTime ProximoExameInterno
        {
            get { return _ProximoExameInterno; }
            set { _ProximoExameInterno = value; }
        }

        private DateTime _ProximoTesteHidrostatico;

        public DateTime ProximoTesteHidrostatico
        {
            get { return _ProximoTesteHidrostatico; }
            set { _ProximoTesteHidrostatico = value; }
        }


        private string _eMail1;

        public string eMail1
        {
            get { return _eMail1; }
            set { _eMail1 = value; }
        }


        private string _eMail2;

        public string eMail2
        {
            get { return _eMail2; }
            set { _eMail2 = value; }
        }

        private Int16 _Dias;

        public Int16 Dias
        {
            get { return _Dias; }
            set { _Dias = value; }
        }


        private DateTime _Data_Inspecao;

        public DateTime Data_Inspecao
        {
            get { return _Data_Inspecao; }
            set { _Data_Inspecao = value; }
        }

        private string _Numero_Relatorio;

        public string Numero_Relatorio
        {
            get { return _Numero_Relatorio; }
            set { _Numero_Relatorio = value; }
        }

        private bool _Inspecao_Visual;

        public bool Inspecao_Visual
        {
            get { return _Inspecao_Visual; }
            set { _Inspecao_Visual = value; }
        }

        private bool _Teste_Hidrostatico;

        public bool Teste_Hidrostatico
        {
            get { return _Teste_Hidrostatico; }
            set { _Teste_Hidrostatico = value; }
        }

        private bool _Liquido_Penetante;

        public bool Liquido_Penetante
        {
            get { return _Liquido_Penetante; }
            set { _Liquido_Penetante = value; }
        }

        private bool _Particulas_Magnetica_Fluorescente;

        public bool Particulas_Magnetica_Fluorescente
        {
            get { return _Particulas_Magnetica_Fluorescente; }
            set { _Particulas_Magnetica_Fluorescente = value; }
        }

        private bool _UltraSom_Medicao_Espessura;

        public bool UltraSom_Medicao_Espessura
        {
            get { return _UltraSom_Medicao_Espessura; }
            set { _UltraSom_Medicao_Espessura = value; }
        }

        private bool _UltraSom_Integridade_Solda;

        public bool UltraSom_Integridade_Solda
        {
            get { return _UltraSom_Integridade_Solda; }
            set { _UltraSom_Integridade_Solda = value; }
        }

        private bool _Analise_Metaografico_Replica;

        public bool Analise_Metaografico_Replica
        {
            get { return _Analise_Metaografico_Replica; }
            set { _Analise_Metaografico_Replica = value; }
        }

        private bool _Ensaio_Mecanico_Amostra;

        public bool Ensaio_Mecanico_Amostra
        {
            get { return _Ensaio_Mecanico_Amostra; }
            set { _Ensaio_Mecanico_Amostra = value; }
        }

        private bool _Endoscopia_Industrial;

        public bool Endoscopia_Industrial
        {
            get { return _Endoscopia_Industrial; }
            set { _Endoscopia_Industrial = value; }
        }

        private bool _Correntes_Parasitas;

        public bool Correntes_Parasitas
        {
            get { return _Correntes_Parasitas; }
            set { _Correntes_Parasitas = value; }
        }

        private bool _Ensaio_Iris;

        public bool Ensaio_Iris
        {
            get { return _Ensaio_Iris; }
            set { _Ensaio_Iris = value; }
        }

        private bool _Emissao_Acustica;

        public bool Emissao_Acustica
        {
            get { return _Emissao_Acustica; }
            set { _Emissao_Acustica = value; }
        }

        private string _Equipamentos_Utilizados;

        public string Equipamentos_Utilizados
        {
            get { return _Equipamentos_Utilizados; }
            set { _Equipamentos_Utilizados = value; }
        }


        private bool _Manometro;

        public bool Manometro
        {
            get { return _Manometro; }
            set { _Manometro = value; }
        }

        private bool _Valvula_Pressostatica;

        public bool Valvula_Pressostatica
        {
            get { return _Valvula_Pressostatica; }
            set { _Valvula_Pressostatica = value; }
        }

        private bool _Valvula_Seguranca;

        public bool Valvula_Seguranca
        {
            get { return _Valvula_Seguranca; }
            set { _Valvula_Seguranca = value; }
        }

        private bool _Controle_Nivel_Agua;

        public bool Controle_Nivel_Agua
        {
            get { return _Controle_Nivel_Agua; }
            set { _Controle_Nivel_Agua = value; }
        }

        private string _Especificacao;

        public string Especificacao
        {
            get { return _Especificacao; }
            set { _Especificacao = value; }
        }

        private float _Pressao_Teste;

        public float Pressao_Teste
        {
            get { return _Pressao_Teste; }
            set { _Pressao_Teste = value; }
        }

        private string _Fluido_Utilizado;

        public string Fluido_Utilizado
        {
            get { return _Fluido_Utilizado; }
            set { _Fluido_Utilizado = value; }
        }

        private string _Temperatura_Fluido;

        public string Temperatura_Fluido
        {
            get { return _Temperatura_Fluido; }
            set { _Temperatura_Fluido = value; }
        }

        private Int16 _Duracao_Teste_Minutos;

        public Int16 Duracao_Teste_Minutos
        {
            get { return _Duracao_Teste_Minutos; }
            set { _Duracao_Teste_Minutos = value; }
        }

        private float _G0_Tampo_Esquerdo_1;
        private float _G0_Costado_2;
        private float _G0_Costado_3;
        private float _G0_Costado_4;
        private float _G0_Tampo_Direito;

        public float G0_Tampo_Esquerdo_1
        {
            get { return _G0_Tampo_Esquerdo_1; }
            set { _G0_Tampo_Esquerdo_1 = value; }
        }
        public float G0_Costado_2
        {
            get { return _G0_Costado_2; }
            set { _G0_Costado_2 = value; }
        }
        public float G0_Costado_3
        {
            get { return _G0_Costado_3; }
            set { _G0_Costado_3 = value; }
        }
        public float G0_Costado_4
        {
            get { return _G0_Costado_4; }
            set { _G0_Costado_4 = value; }
        }
        public float G0_Tampo_Direito
        {
            get { return _G0_Tampo_Direito; }
            set { _G0_Tampo_Direito = value; }
        }

        private float _G90_Tampo_Esquerdo_1;
        private float _G90_Costado_2;
        private float _G90_Costado_3;
        private float _G90_Costado_4;
        private float _G90_Tampo_Direito;

        public float G90_Tampo_Esquerdo_1
        {
            get { return _G90_Tampo_Esquerdo_1; }
            set { _G90_Tampo_Esquerdo_1 = value; }
        }
        public float G90_Costado_2
        {
            get { return _G90_Costado_2; }
            set { _G90_Costado_2 = value; }
        }
        public float G90_Costado_3
        {
            get { return _G90_Costado_3; }
            set { _G90_Costado_3 = value; }
        }
        public float G90_Costado_4
        {
            get { return _G90_Costado_4; }
            set { _G90_Costado_4 = value; }
        }
        public float G90_Tampo_Direito
        {
            get { return _G90_Tampo_Direito; }
            set { _G90_Tampo_Direito = value; }
        }


        private float _G180_Tampo_Esquerdo_1;
        private float _G180_Costado_2;
        private float _G180_Costado_3;
        private float _G180_Costado_4;
        private float _G180_Tampo_Direito;

        public float G180_Tampo_Esquerdo_1
        {
            get { return _G180_Tampo_Esquerdo_1; }
            set { _G180_Tampo_Esquerdo_1 = value; }
        }
        public float G180_Costado_2
        {
            get { return _G180_Costado_2; }
            set { _G180_Costado_2 = value; }
        }
        public float G180_Costado_3
        {
            get { return _G180_Costado_3; }
            set { _G180_Costado_3 = value; }
        }
        public float G180_Costado_4
        {
            get { return _G180_Costado_4; }
            set { _G180_Costado_4 = value; }
        }
        public float G180_Tampo_Direito
        {
            get { return _G180_Tampo_Direito; }
            set { _G180_Tampo_Direito = value; }
        }


        private float _G270_Tampo_Esquerdo_1;
        private float _G270_Costado_2;
        private float _G270_Costado_3;
        private float _G270_Costado_4;
        private float _G270_Tampo_Direito;

        public float G270_Tampo_Esquerdo_1
        {
            get { return _G270_Tampo_Esquerdo_1; }
            set { _G270_Tampo_Esquerdo_1 = value; }
        }
        public float G270_Costado_2
        {
            get { return _G270_Costado_2; }
            set { _G270_Costado_2 = value; }
        }
        public float G270_Costado_3
        {
            get { return _G270_Costado_3; }
            set { _G270_Costado_3 = value; }
        }
        public float G270_Costado_4
        {
            get { return _G270_Costado_4; }
            set { _G270_Costado_4 = value; }
        }
        public float G270_Tampo_Direito
        {
            get { return _G270_Tampo_Direito; }
            set { _G270_Tampo_Direito = value; }
        }


        private string _Material_Adotado_Espessura_Vaso;

        public string Material_Adotado_Espessura_Vaso
        {
            get { return _Material_Adotado_Espessura_Vaso; }
            set { _Material_Adotado_Espessura_Vaso = value; }
        }

        private float _G0_Espessura;

        public float G0_Espessura
        {
            get { return _G0_Espessura; }
            set { _G0_Espessura = value; }
        }

        private float _G90_Espessura;

        public float G90_Espessura
        {
            get { return _G90_Espessura; }
            set { _G90_Espessura = value; }
        }

        private float _G180_Espessura;

        public float G180_Espessura
        {
            get { return _G180_Espessura; }
            set { _G180_Espessura = value; }
        }

        private float _G270_Espessura;

        public float G270_Espessura
        {
            get { return _G270_Espessura; }
            set { _G270_Espessura = value; }
        }

        private string _Material_Adotado_Espessura_Tubulacao;

        public string Material_Adotado_Espessura_Tubulacao
        {
            get { return _Material_Adotado_Espessura_Tubulacao; }
            set { _Material_Adotado_Espessura_Tubulacao = value; }
        }

        private float _Tensao_Maxima_Admissivel;

        public float Tensao_Maxima_Admissivel
        {
            get { return _Tensao_Maxima_Admissivel; }
            set { _Tensao_Maxima_Admissivel = value; }
        }

        private float _Eficiencia_Junta_Solda;

        public float Eficiencia_Junta_Solda
        {
            get { return _Eficiencia_Junta_Solda; }
            set { _Eficiencia_Junta_Solda = value; }
        }

        private float _Espessura_Minima_Encontrada;

        public float Espessura_Minima_Encontrada
        {
            get { return _Espessura_Minima_Encontrada; }
            set { _Espessura_Minima_Encontrada = value; }
        }

        private float _Raio_Interno_Corpo;

        public float Raio_Interno_Corpo
        {
            get { return _Raio_Interno_Corpo; }
            set { _Raio_Interno_Corpo = value; }
        }

        private float _PMTA;

        public float PMTA
        {
            get { return _PMTA; }
            set { _PMTA = value; }
        }








        public string GetTipoInspecao()
        {
            string ret = string.Empty;

            if (this.IndTipoInspecao == (int)TipoInspecao.Periodica)
                ret = "Periódica";
            else if (this.IndTipoInspecao == (int)TipoInspecao.Inicial)
                ret = "Inicial";
            else if (this.IndTipoInspecao == (int)TipoInspecao.Extraordinaria)
                ret = "Extraordinária";

            return ret;
        }

        public override void Validate()
        {
            if (this.IdDocumentoBase != null)
            {
                if (this.IdVasoCaldeiraBase.mirrorOld == null)
                    this.IdVasoCaldeiraBase.Find();

                if (this.IdVasoCaldeiraBase.IndVasoCaldeira == VasoCaldeiraBase.VasoCaldeiraTipo.VasoPressao)
                    this.IdDocumentoBase.Id = (int)Documentos.VasosInspecao;
                else
                    this.IdDocumentoBase.Id = (int)Documentos.CaldeiraInspecao;
            }

            base.Validate();
        }

        public override int Save()
        {
            if (this.IdPedido.Id != 0)
            {
                if (this.IdPedido.mirrorOld == null)
                    this.IdPedido.Find();

                if (this.IdPedido.IdEquipamentoBase.Id == 0)
                {
                    this.IdPedido.IdEquipamentoBase.Id = this.IdVasoCaldeiraBase.Id;
                    this.IdPedido.Observacao = this.IdVasoCaldeiraBase.GetNumeroIdentificacao();
                    this.IdPedido.Save();
                }
            }

            return base.Save();
        }
    }

    public enum TipoProjetoVasoCaldeira : int
    {
        ProjetoInstalacao,
        ProjetoAlternativo,
        ProjetoAlteracaoOuReparo
    }

    [Database("opsa", "ProjetoVasoCaldeira", "IdProjetoVasoCaldeira")]
    public class ProjetoVasoCaldeira : Ilitera.Opsa.Data.Documento
    {

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ProjetoVasoCaldeira()
        {

        }

        private int _IdProjetoVasoCaldeira;

        public override int Id
        {
            get { return _IdProjetoVasoCaldeira; }
            set { _IdProjetoVasoCaldeira = value; }
        }

        private VasoCaldeiraBase _IdVasoCaldeiraBase;

        public VasoCaldeiraBase IdVasoCaldeiraBase
        {
            get { return _IdVasoCaldeiraBase; }
            set { _IdVasoCaldeiraBase = value; }
        }

        private TipoProjetoVasoCaldeira _IndTipoProjeto;

        public TipoProjetoVasoCaldeira IndTipoProjeto
        {
            get { return _IndTipoProjeto; }
            set { _IndTipoProjeto = value; }
        }

        private Prestador _IdAutoria;

        public Prestador IdAutoria
        {
            get { return _IdAutoria; }
            set { _IdAutoria = value; }
        }

        private string _Recomendacao = string.Empty;

        public string Recomendacao
        {
            get { return _Recomendacao; }
            set { _Recomendacao = value; }
        }

        private string _PlantaBaixa = string.Empty;

        public string PlantaBaixa
        {
            get { return _PlantaBaixa; }
            set { _PlantaBaixa = value; }
        }

        private string _Sugestao = string.Empty;

        public string Sugestao
        {
            get { return _Sugestao; }
            set { _Sugestao = value; }
        }

        public override void Validate()
        {
            if (this.IdDocumentoBase != null)
            {
                if (this.IdVasoCaldeiraBase.mirrorOld == null)
                    this.IdVasoCaldeiraBase.Find();

                if (this.IdVasoCaldeiraBase.IndVasoCaldeira == VasoCaldeiraBase.VasoCaldeiraTipo.VasoPressao)
                    this.IdDocumentoBase.Id = (int)Documentos.VasosProjeto;
                else
                    this.IdDocumentoBase.Id = (int)Documentos.CaldeiraProjeto;
            }

            base.Validate();
        }


        public string GetSugestao()
        {
            string ret = Path.Combine(Ilitera.Common.Fotos.GetRaizPath(),
                         Path.Combine(Path.Combine(this.IdCliente.GetFotoDiretorioPadrao(), "VasoPressao"),
                                      this.Sugestao));
            return ret;
        }

        public string GetPlantaBaixa()
        {
            string ret = Path.Combine(Ilitera.Common.Fotos.GetRaizPath(),
                         Path.Combine(Path.Combine(this.IdCliente.GetFotoDiretorioPadrao(), "VasoPressao"), 
                                      this.PlantaBaixa));
            return ret;
        }

        public override int Save()
        {
            if (this.IdPedido.Id != 0)
            {
                if (this.IdPedido.mirrorOld == null)
                    this.IdPedido.Find();

                if (this.IdPedido.IdEquipamentoBase.Id == 0)
                {
                    this.IdPedido.IdEquipamentoBase.Id = this.IdVasoCaldeiraBase.Id;
                    this.IdPedido.Observacao = this.IdVasoCaldeiraBase.GetNumeroIdentificacao();
                    this.IdPedido.Save();
                }
            }
            return base.Save();
        }
    }

    public enum FrasePadraoInspecao : int
    {
        DescricaoExames,
        Resultado,
        Recomendacao,
        Conclusao,
    }

    [Database("opsa", "FrasePadraoInspecaoVasoCaldeira", "IdFrasePadraoInspecaoVasoCaldeira")]
    public class FrasePadraoInspecaoVasoCaldeira : Ilitera.Data.Table
    {

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FrasePadraoInspecaoVasoCaldeira()
        {

        }

        private int _IdFrasePadraoInspecaoVasoCaldeira;

        public override int Id
        {
            get { return _IdFrasePadraoInspecaoVasoCaldeira; }
            set { _IdFrasePadraoInspecaoVasoCaldeira = value; }
        }

        private int _IndFrasePadraoInspecao;

        public int IndFrasePadraoInspecao
        {
            get { return _IndFrasePadraoInspecao; }
            set { _IndFrasePadraoInspecao = value; }
        }

        private string _Descricao;

        [Obrigatorio(true, "A descrição é campo obrigatório!")]
        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        private string _TextoRtf;

        public string TextoRtf
        {
            get { return _TextoRtf; }
            set { _TextoRtf = value; }
        }

    }
}
