using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{
    #region EquipamentoBase

    [Database("opsa", "EquipamentoBase", "IdEquipamentoBase")]
    public class EquipamentoBase : Ilitera.Data.Table
    {
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]
        public EquipamentoBase()
        {

        }

        public enum EquipamentoBaseTipo : int
        {
            VasoPressao,
            Caldeira,
            Prensas
        }

        private int _IdEquipamentoBase;

        public override int Id
        {
            get { return _IdEquipamentoBase; }
            set { _IdEquipamentoBase = value; }
        }

        private EquipamentoBaseTipo _IndTipoEquipamento;

        public EquipamentoBaseTipo IndTipoEquipamento
        {
            get { return _IndTipoEquipamento; }
            set { _IndTipoEquipamento = value; }
        }

        private Cliente _IdCliente;

        [Obrigatorio(true, "Cliente é campo obrigatório!")]
        public Cliente IdCliente
        {
            get { return _IdCliente; }
            set { _IdCliente = value; }
        }

        private bool _IsInativo;

        public bool IsInativo
        {
            get { return _IsInativo; }
            set { _IsInativo = value; }
        }

        private string _Descricao = string.Empty;

        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        private string _Foto = string.Empty;

        public string Foto
        {
            get { return _Foto; }
            set { _Foto = value; }
        }
    }
    #endregion

    #region VasoCaldeiraBase

    [Database("opsa", "VasoCaldeiraBase", "IdVasoCaldeiraBase")]
    public class VasoCaldeiraBase : EquipamentoBase, Ilitera.Opsa.Data.ICalculoVasoCaldeira
    {

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public VasoCaldeiraBase()
        {

        }

        public enum VasoCaldeiraTipo : int
        {
            VasoPressao,
            Caldeira
        }

        private int _IdVasoCaldeiraBase;

        public override int Id
        {
            get { return _IdVasoCaldeiraBase; }
            set { _IdVasoCaldeiraBase = value; }
        }

        private VasoCaldeiraTipo _IndVasoCaldeira;

        public VasoCaldeiraTipo IndVasoCaldeira
        {
            get { return _IndVasoCaldeira; }
            set { _IndVasoCaldeira = value; }
        }

        private string _Localizacao = string.Empty;

        public string Localizacao
        {
            get { return _Localizacao; }
            set { _Localizacao = value; }
        }
        private string _NumeroIdentificacao = string.Empty;

        [Obrigatorio(true, "Número de Identificação é campo obrigatório!")]
        public string NumeroIdentificacao
        {
            get { return _NumeroIdentificacao; }
            set { _NumeroIdentificacao = value; }
        }

        private FabricanteVasoCaldeira _IdFabricanteVasoCaldeira;

        public FabricanteVasoCaldeira IdFabricanteVasoCaldeira
        {
            get { return _IdFabricanteVasoCaldeira; }
            set { _IdFabricanteVasoCaldeira = value; }
        }

        private int _AnoFabricacao;

        public int AnoFabricacao
        {
            get { return _AnoFabricacao; }
            set { _AnoFabricacao = value; }
        }

        private TipoVasoCaldeira _IdTipoVasoCaldeira;

        public TipoVasoCaldeira IdTipoVasoCaldeira
        {
            get { return _IdTipoVasoCaldeira; }
            set { _IdTipoVasoCaldeira = value; }
        }

        private UnidadePressao _IdUnidadePressao;

        public UnidadePressao IdUnidadePressao
        {
            get { return _IdUnidadePressao; }
            set { _IdUnidadePressao = value; }
        }
        private string _CodigoProjeto = string.Empty;

        public string CodigoProjeto
        {
            get { return _CodigoProjeto; }
            set { _CodigoProjeto = value; }
        }
        private int _AnoEdicao;

        public int AnoEdicao
        {
            get { return _AnoEdicao; }
            set { _AnoEdicao = value; }
        }
        private double _MedidaCorpo;

        public double MedidaCorpo
        {
            get { return _MedidaCorpo; }
            set { _MedidaCorpo = value; }
        }
        private double _MedidaTampoSuperior;

        public double MedidaTampoSuperior
        {
            get { return _MedidaTampoSuperior; }
            set { _MedidaTampoSuperior = value; }
        }
        private double _MedidaTampoInferior;

        public double MedidaTampoInferior
        {
            get { return _MedidaTampoInferior; }
            set { _MedidaTampoInferior = value; }
        }
        private double _DiametroExterno;

        public double DiametroExterno
        {
            get { return _DiametroExterno; }
            set { _DiametroExterno = value; }
        }
        private double _MenorEspessuraCorpo;

        public double MenorEspessuraCorpo
        {
            get { return _MenorEspessuraCorpo; }
            set { _MenorEspessuraCorpo = value; }
        }
        private double _MenorEspessuraTampo;

        public double MenorEspessuraTampo
        {
            get { return _MenorEspessuraTampo; }
            set { _MenorEspessuraTampo = value; }
        }
        private double _MenorEspessura;

        public double MenorEspessura
        {
            get { return _MenorEspessura; }
            set { _MenorEspessura = value; }
        }
        private double _VolumeInterno;

        public double VolumeInterno
        {
            get { return _VolumeInterno; }
            set { _VolumeInterno = value; }
        }
        private double _PressaoMaximaTrabalho;

        public double PressaoMaximaTrabalho
        {
            get { return _PressaoMaximaTrabalho; }
            set { _PressaoMaximaTrabalho = value; }
        }

        private double _PressaoTesteHidrostatico;

        public double PressaoTesteHidrostatico
        {
            get { return _PressaoTesteHidrostatico; }
            set { _PressaoTesteHidrostatico = value; }
        }
        private double _RelacaoPV;

        public double RelacaoPV
        {
            get { return _RelacaoPV; }
            set { _RelacaoPV = value; }
        }

        private double _PressaoAbertura;

        public double PressaoAbertura
        {
            get { return _PressaoAbertura; }
            set { _PressaoAbertura = value; }
        }

        private double _Escala;

        public double Escala
        {
            get { return _Escala; }
            set { _Escala = value; }
        }

        private ValvulaSeguranca _IdValvulaSeguranca;

        public ValvulaSeguranca IdValvulaSeguranca
        {
            get { return _IdValvulaSeguranca; }
            set { _IdValvulaSeguranca = value; }
        }

        private MarcaTipoManometro _IdMarcaTipoManometro;

        public MarcaTipoManometro IdMarcaTipoManometro
        {
            get { return _IdMarcaTipoManometro; }
            set { _IdMarcaTipoManometro = value; }
        }

        private string _Producao_Vapor;

        public string Producao_Vapor
        {
            get { return _Producao_Vapor; }
            set { _Producao_Vapor = value; }
        }

        private string _Material;

        public string Material
        {
            get { return _Material; }
            set { _Material = value; }
        }

        private string _Modelo;

        public string Modelo
        {
            get { return _Modelo; }
            set { _Modelo = value; }
        }

        private string _Superficie_Aquecimento;

        public string Superficie_Aquecimento
        {
            get { return _Superficie_Aquecimento; }
            set { _Superficie_Aquecimento = value; }
        }




        public double GetPressaoOperacao()
        {
            return this.PressaoMaximaTrabalho * 0.9D;
        }

        public double GetPMTA_MPa()
        {
            return this.PressaoMaximaTrabalho * 0.0981D;
        }

        public static string GetUnidadeConvertida(double val_Kgf_cm2, UnidadePressao unidadePressao)
        {
            string ret;

            if (val_Kgf_cm2 == 0)
                ret = "Desconhecido";
            else if (unidadePressao.Id == 0)
                ret = val_Kgf_cm2.ToString("n") + " " + "Kgf/cm²";
            else
            {
                if (unidadePressao.mirrorOld == null)
                    unidadePressao.Find();

                ret = (val_Kgf_cm2 * unidadePressao.Conversao).ToString("n") + " " + unidadePressao.Descricao;
            }

            return ret;
        }

        public string GetNumeroIdentificacao()
        {
            return "Nº " + this.NumeroIdentificacao;
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public string GetFoto()
        {
            string ret = Path.Combine(Ilitera.Common.Fotos.GetRaizPath(),
                         Path.Combine(Path.Combine(this.IdCliente.GetFotoDiretorioPadrao(), "VasoPressao"), 
                                      this.Foto));
            return ret;
        }

        private ProjetoVasoCaldeira projetoInstalacao;

        public ProjetoVasoCaldeira GetProjetoInstalacao()
        {
            if (projetoInstalacao == null)
            {
                projetoInstalacao = new ProjetoVasoCaldeira();
                projetoInstalacao.Find("IdVasoCaldeiraBase=" + this.Id
                    + " AND IndTipoProjeto =" + (int)TipoProjetoVasoCaldeira.ProjetoInstalacao);
            }
            return projetoInstalacao;
        }

        public InspecaoVasoCaldeira GetUltimaInspecao()
        {
            ArrayList list = new InspecaoVasoCaldeira().Find("IdVasoCaldeiraBase=" + this.Id
                +" AND IdPedido IN (SELECT IdPedido FROM Pedido WHERE DataConclusao IS NOT NULL AND DataCancelamento IS NULL)"
                + " ORDER BY DataLevantamento DESC");

            if (list.Count == 0)
                return new InspecaoVasoCaldeira();
            else
                return (InspecaoVasoCaldeira)list[0];
        }


        public override int Save()
        {
            int ret;
            
            this.Descricao = this.GetNumeroIdentificacao();
            
            bool AtualizarPedido = (this.Id == 0);
            
            if (projetoInstalacao != null)
            {
                ret = base.Save();

                if (projetoInstalacao.IdPrestador.Id != 0)
                {
                    projetoInstalacao.IdCliente.Id = this.IdCliente.Id;
                    projetoInstalacao.IdVasoCaldeiraBase.Id = ret;
                    projetoInstalacao.Save();
                }

            }
            else
                ret = base.Save();

            if (AtualizarPedido)
            {
                Obrigacao obrigacaoProjeto = new Obrigacao();
                Obrigacao obrigacaoInspecao = new Obrigacao();

                if (this.IndVasoCaldeira == VasoCaldeiraTipo.VasoPressao)
                {
                    obrigacaoProjeto.Find((int)Obrigacoes.VasosProjeto);
                    obrigacaoInspecao.Find((int)Obrigacoes.VasosInspecao);
                }
                else
                {
                    obrigacaoProjeto.Find((int)Obrigacoes.CaldeiraProjeto);
                    obrigacaoInspecao.Find((int)Obrigacoes.CaldeiraInspecao);
                }

                ObrigacaoCliente obrigCliente1 = ObrigacaoCliente.GetObrigacaoCliente(this.IdCliente, obrigacaoProjeto);
                obrigCliente1.Atualizar();

                ObrigacaoCliente obrigCliente2 = ObrigacaoCliente.GetObrigacaoCliente(this.IdCliente, obrigacaoInspecao);
                obrigCliente2.Atualizar();

                //ObrigacaoCliente.CriaObrigacaoCliente(this.IdCliente, obrigacaoProjeto, true);
                //ObrigacaoCliente.CriaObrigacaoCliente(this.IdCliente, obrigacaoInspecao, true);
            }

            return ret;
        }
    }
    #endregion

    #region VasoPressao

    [Database("opsa", "VasoPressao", "IdVasoPressao")]
    public class VasoPressao : Ilitera.Opsa.Data.VasoCaldeiraBase
    {
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public VasoPressao()
        {
            this.IndVasoCaldeira = VasoCaldeiraTipo.VasoPressao;
            this.IndTipoEquipamento = EquipamentoBaseTipo.VasoPressao;
        }

        public enum VasoTipo : int
        {
            VasoPressao,
            Reator
        }

        #region properties

        private int _IdVasoPressao;

        public override int Id
        {
            get { return _IdVasoPressao; }
            set { _IdVasoPressao = value; }
        }

        private ClasseDeFluido _IdClasseDeFluido;

        public ClasseDeFluido IdClasseDeFluido
        {
            get { return _IdClasseDeFluido; }
            set { _IdClasseDeFluido = value; }
        }
        private FluidoServico _IdFluidoServico;

        public FluidoServico IdFluidoServico
        {
            get { return _IdFluidoServico; }
            set { _IdFluidoServico = value; }
        }
        private GrupoRisco _IdGrupoRisco;

        public GrupoRisco IdGrupoRisco
        {
            get { return _IdGrupoRisco; }
            set { _IdGrupoRisco = value; }
        }
        private CategoriaVasoPressao _IdCategoriaVasoPressao;

        public CategoriaVasoPressao IdCategoriaVasoPressao
        {
            get { return _IdCategoriaVasoPressao; }
            set { _IdCategoriaVasoPressao = value; }
        }

        private VasoTipo _IndVasoTipo;

        public VasoTipo IndVasoTipo
        {
            get { return _IndVasoTipo; }
            set { _IndVasoTipo = value;}

        }
        #endregion

        #region metodos

        public int PrazoExameExterno()
        {
            if (this.IdCategoriaVasoPressao.mirrorOld == null)
                this.IdCategoriaVasoPressao.Find();

            return this.IdCategoriaVasoPressao.ExameExterno;
        }

        public int PrazoExameInterno()
        {
            if (this.IdCategoriaVasoPressao.mirrorOld == null)
                this.IdCategoriaVasoPressao.Find();

            return this.IdCategoriaVasoPressao.ExameInterno;
        }

        public int PrazoTesteHidrostatico()
        {
            if (this.IdCategoriaVasoPressao.mirrorOld == null)
                this.IdCategoriaVasoPressao.Find();

            return this.IdCategoriaVasoPressao.TesteHidrostatico;
        }

        public override void Validate()
        {
            this.IndVasoCaldeira = VasoCaldeiraTipo.VasoPressao;

            base.Validate();
        }

        public static ProjetoVasoCaldeira GetProjetoInstalacao(Pedido pedido)
        {
            ProjetoVasoCaldeira projeto = new ProjetoVasoCaldeira();
            projeto.Find("IdPedido=" + pedido.Id);

            if (projeto.Id == 0)
            {
                if (pedido.DataConclusao != new DateTime())
                    throw new Exception("Projeto não localizado!");

                pedido.IdPedidoGrupo.Find();
                pedido.IdPedidoGrupo.IdCompromisso.Find();

                projeto.Inicialize();
                projeto.IdDocumentoBase.Id = (int)Documentos.VasosProjeto;
                projeto.IdPedido = pedido;
                projeto.IdCliente = pedido.IdCliente;
                projeto.IdPrestador = pedido.IdPrestador;
                projeto.IndTipoProjeto = TipoProjetoVasoCaldeira.ProjetoInstalacao;
                projeto.DataLevantamento = DateTime.Today;
            }

            return projeto;
        }

        #endregion
    }

    #endregion

    #region Caldeira

    [Database("opsa", "Caldeira", "IdCaldeira")]
    public class Caldeira : Ilitera.Opsa.Data.VasoCaldeiraBase
    {
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Caldeira()
        {
            this.IndVasoCaldeira = VasoCaldeiraTipo.Caldeira;
            this.IndTipoEquipamento = EquipamentoBaseTipo.Caldeira;
        }

        public enum CategoriaCaldeira : int
        {
            A = 1,
            B = 2,
            C = 3
        }

        private int _IdCaldeira;

        public override int Id
        {
            get { return _IdCaldeira; }
            set { _IdCaldeira = value; }
        }

        private CategoriaCaldeira _IndCategoriaCaldeira;

        public CategoriaCaldeira IndCategoriaCaldeira
        {
            get { return _IndCategoriaCaldeira; }
            set { _IndCategoriaCaldeira = value; }
        }

        private double _CapacidadeProducaoVapor;

        public double CapacidadeProducaoVapor
        {
            get { return _CapacidadeProducaoVapor; }
            set { _CapacidadeProducaoVapor = value; }
        }

        private double _AreaSuperficieAquecimento;

        public double AreaSuperficieAquecimento
        {
            get { return _AreaSuperficieAquecimento; }
            set { _AreaSuperficieAquecimento = value; }
        }

        private CombustivelCaldeira _IdCombustivelCaldeira;

        public CombustivelCaldeira IdCombustivelCaldeira
        {
            get { return _IdCombustivelCaldeira; }
            set { _IdCombustivelCaldeira = value; }
        }

        public int PrazoExameExterno()
        {
            return 1;
        }

        public int PrazoExameInterno()
        {
            return 1;
        }

        public int PrazoTesteHidrostatico()
        {
            return 1;
        }

        public static ProjetoVasoCaldeira GetProjetoInstalacao(Pedido pedido)
        {
            ProjetoVasoCaldeira projeto = new ProjetoVasoCaldeira();
            projeto.Find("IdPedido=" + pedido.Id);

            if (projeto.Id == 0)
            {
                if (pedido.DataConclusao != new DateTime())
                    throw new Exception("Projeto não localizado!");

                pedido.IdPedidoGrupo.Find();
                pedido.IdPedidoGrupo.IdCompromisso.Find();

                Prestador prestador = new Prestador();
                prestador.Find("IdPessoa=" + Usuario.Login().IdPessoa.Id);

                projeto.Inicialize();
                projeto.IdDocumentoBase.Id = (int)Documentos.CaldeiraProjeto;
                projeto.IdPedido = pedido;
                projeto.IdCliente = pedido.IdCliente;
                projeto.IdPrestador = prestador;
                projeto.IdAutoria = pedido.IdPrestador;
                projeto.IndTipoProjeto = TipoProjetoVasoCaldeira.ProjetoInstalacao;
                projeto.DataLevantamento = DateTime.Today;
            }

            return projeto;
        }
    }
    #endregion

    #region CombustivelCaldeira

    [Database("opsa", "CombustivelCaldeira", "IdCombustivelCaldeira")]

    public class CombustivelCaldeira : Ilitera.Data.Table
    {
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CombustivelCaldeira()
        {

        }
        private int _IdCombustivelCaldeira;

        public override int Id
        {
            get { return _IdCombustivelCaldeira; }
            set { _IdCombustivelCaldeira = value; }
        }

        private string _Descricao = string.Empty;

        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return _Descricao;
        }
    }
    #endregion

    #region UnidadePressao

    [Database("opsa", "UnidadePressao", "IdUnidadePressao")]
    public class UnidadePressao : Ilitera.Data.Table
    {
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public UnidadePressao()
        {

        }
        private int _IdUnidadePressao;

        public override int Id
        {
            get { return _IdUnidadePressao; }
            set { _IdUnidadePressao = value; }
        }

        private string _Descricao = string.Empty;

        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        private double _Conversao;

        public double Conversao
        {
            get { return _Conversao; }
            set { _Conversao = value; }
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return _Descricao;
        }
    }
    #endregion

    #region MarcaTipoManometro

    [Database("opsa", "MarcaTipoManometro", "IdMarcaTipoManometro")]
    public class MarcaTipoManometro : Ilitera.Data.Table
    {
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public MarcaTipoManometro()
        {

        }
        private int _IdMarcaTipoManometro;

        public override int Id
        {
            get { return _IdMarcaTipoManometro; }
            set { _IdMarcaTipoManometro = value; }
        }

        private string _Descricao = string.Empty;

        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return _Descricao;
        }
    }
    #endregion

    #region ValvulaSeguranca

    [Database("opsa", "ValvulaSeguranca", "IdValvulaSeguranca")]
    public class ValvulaSeguranca : Ilitera.Data.Table
    {
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ValvulaSeguranca()
        {

        }
        private int _IdValvulaSegurancaTipo;

        public override int Id
        {
            get { return _IdValvulaSegurancaTipo; }
            set { _IdValvulaSegurancaTipo = value; }
        }

        private string _Descricao = string.Empty;

        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return _Descricao;
        }
    }
    #endregion

    #region FabricanteVasoCaldeira

    [Database("opsa", "FabricanteVasoCaldeira", "IdFabricanteVasoCaldeira", "", "Fabricante de Caldeiras e Vasos de Pressão")]
    public class FabricanteVasoCaldeira : Ilitera.Data.Table
    {
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FabricanteVasoCaldeira()
        {

        }
        private int _IdFabricanteVasoPressao;

        public override int Id
        {
            get { return _IdFabricanteVasoPressao; }
            set { _IdFabricanteVasoPressao = value; }
        }

        private string _Descricao = string.Empty;

        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return _Descricao;
        }
    }
    #endregion

    #region TipoVasoCaldeira

    [Database("opsa", "TipoVasoCaldeira", "IdTipoVasoCaldeira")]
    public class TipoVasoCaldeira : Ilitera.Data.Table
    {
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public TipoVasoCaldeira()
        {

        }
        private int _IdTipoVasoCaldeira;

        public override int Id
        {
            get { return _IdTipoVasoCaldeira; }
            set { _IdTipoVasoCaldeira = value; }
        }

        private string _Descricao = string.Empty;

        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return _Descricao;
        }
    }
    #endregion

    #region ClasseDeFluido

    [Database("opsa", "ClasseDeFluido", "IdClasseDeFluido")]
    public class ClasseDeFluido : Ilitera.Data.Table
    {
        public enum ClasseDeFluidos : int
        {
            A = 1,
            B = 2,
            C = 3,
            D = 4
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public ClasseDeFluido()
        {

        }
        private int _IdClasseDeFluido;

        public override int Id
        {
            get { return _IdClasseDeFluido; }
            set { _IdClasseDeFluido = value; }
        }

        private string _Descricao = string.Empty;

        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return _Descricao;
        }
    }
    #endregion

    #region FluidoServico

    [Database("opsa", "FluidoServico", "IdFluidoServico", "", "Fluídos de Serviços")]
    public class FluidoServico : Ilitera.Data.Table
    {
        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public FluidoServico()
        {

        }
        private int _IdFluidoServico;

        public override int Id
        {
            get { return _IdFluidoServico; }
            set { _IdFluidoServico = value; }
        }

        private string _Descricao = string.Empty;

        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return _Descricao;
        }
    }
    #endregion

    #region GrupoRisco

    [Database("opsa", "GrupoRisco", "IdGrupoRisco")]
    public class GrupoRisco : Ilitera.Data.Table
    {
        public enum GrupoRiscos : int
        {
            G1_PV_Maior_Igual_100 = 1,
            G2_PV_Menor_100_E_Maior_Igual30 = 2,
            G3_PV_Menor_30_E_Maior_Igual2_5 = 3,
            G4_PV_Menor_2_5_E_Maior_Igual1 = 4,
            G5_PV_Menor_1 = 5
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public GrupoRisco()
        {

        }
        private int _IdGrupoRisco;

        public override int Id
        {
            get { return _IdGrupoRisco; }
            set { _IdGrupoRisco = value; }
        }

        private string _Descricao = string.Empty;

        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return _Descricao;
        }
    }
    #endregion

    #region CategoriaVasoPressao

    [Database("opsa", "CategoriaVasoPressao", "IdCategoriaVasoPressao")]
    public class CategoriaVasoPressao : Ilitera.Data.Table
    {
        public enum CategoriasVasoPressao : int
        {
            I = 1,
            II = 2,
            III = 3,
            IV = 4,
            V = 5
        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CategoriaVasoPressao()
        {

        }
        private int _IdCategoriaVasoPressao;

        public override int Id
        {
            get { return _IdCategoriaVasoPressao; }
            set { _IdCategoriaVasoPressao = value; }
        }

        private string _Descricao = string.Empty;

        public string Descricao
        {
            get { return _Descricao; }
            set { _Descricao = value; }
        }

        private int _ExameExterno;

        public int ExameExterno
        {
            get { return _ExameExterno; }
            set { _ExameExterno = value; }
        }

        private int _ExameInterno;

        public int ExameInterno
        {
            get { return _ExameInterno; }
            set { _ExameInterno = value; }
        }

        private int _TesteHidrostatico;

        public int TesteHidrostatico
        {
            get { return _TesteHidrostatico; }
            set { _TesteHidrostatico = value; }
        }

        public override string ToString()
        {
            if (this.mirrorOld == null)
                this.Find();

            return _Descricao;
        }

    }
    #endregion

    #region CategoriaClasse

    [Database("opsa", "CategoriaClasse", "IdCategoriaClasse")]
    public class CategoriaClasse : Ilitera.Data.Table
    {

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public CategoriaClasse()
        {

        }
        private int _IdCategoriaClasse;

        public override int Id
        {
            get { return _IdCategoriaClasse; }
            set { _IdCategoriaClasse = value; }
        }

        private CategoriaVasoPressao _IdCategoriaVasoPressao;

        public CategoriaVasoPressao IdCategoriaVasoPressao
        {
            get { return _IdCategoriaVasoPressao; }
            set { _IdCategoriaVasoPressao = value; }
        }

        private ClasseDeFluido _IdClasseDeFluido;

        public ClasseDeFluido IdClasseDeFluido
        {
            get { return _IdClasseDeFluido; }
            set { _IdClasseDeFluido = value; }
        }
        private GrupoRisco _IdGrupoRisco;

        public GrupoRisco IdGrupoRisco
        {
            get { return _IdGrupoRisco; }
            set { _IdGrupoRisco = value; }
        }
    }
    #endregion
}
