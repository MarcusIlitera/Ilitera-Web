using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Ilitera.Data;
using Ilitera.Common;



namespace Ilitera.Opsa.Data
{
    //#region EquipamentoBase

    //[Database("opsa", "EquipamentoBase", "IdEquipamentoBase")]
    //public class LaudoEletrico : Ilitera.Data.Table
    //{
    //    public LaudoEletrico()
    //    {
    //    }

        //    public enum EquipamentoBaseTipo : int
        //    {
        //        LaudoEletrico,
        //        Caldeira,
        //        Prensas
        //    }

        //    private int _IdEquipamentoBase;

        //    public override int Id
        //    {
        //        get { return _IdEquipamentoBase; }
        //        set { _IdEquipamentoBase = value; }
        //    }

        //    private EquipamentoBaseTipo _IndTipoEquipamento;

        //    public EquipamentoBaseTipo IndTipoEquipamento
        //    {
        //        get { return _IndTipoEquipamento; }
        //        set { _IndTipoEquipamento = value; }
        //    }

        //    private Cliente _IdCliente;

        //    [Obrigatorio(true, "Cliente é campo obrigatório!")]
        //    public Cliente IdCliente
        //    {
        //        get { return _IdCliente; }
        //        set { _IdCliente = value; }
        //    }

        //    private bool _IsInativo;

        //    public bool IsInativo
        //    {
        //        get { return _IsInativo; }
        //        set { _IsInativo = value; }
        //    }

        //    private string _Descricao = string.Empty;

        //    public string Descricao
        //    {
        //        get { return _Descricao; }
        //        set { _Descricao = value; }
        //    }

        //    private string _Foto = string.Empty;

        //    public string Foto
        //    {
        //        get { return _Foto; }
        //        set { _Foto = value; }
        //    }
        //}
        //#endregion

        //#region VasoCaldeiraBase

        //[Database("opsa", "VasoCaldeiraBase", "IdVasoCaldeiraBase")]
        //public class VasoCaldeiraBase : EquipamentoBase, Ilitera.Opsa.Data.ICalculoVasoCaldeira
        //{
        //    public VasoCaldeiraBase()
        //    {

        //    }

        //    public enum VasoCaldeiraTipo : int
        //    {
        //        LaudoEletrico,
        //        Caldeira
        //    }

        //    private int _IdVasoCaldeiraBase;

        //    public override int Id
        //    {
        //        get { return _IdVasoCaldeiraBase; }
        //        set { _IdVasoCaldeiraBase = value; }
        //    }

        //    private VasoCaldeiraTipo _IndVasoCaldeira;

        //    public VasoCaldeiraTipo IndVasoCaldeira
        //    {
        //        get { return _IndVasoCaldeira; }
        //        set { _IndVasoCaldeira = value; }
        //    }

        //    private string _Localizacao = string.Empty;

        //    public string Localizacao
        //    {
        //        get { return _Localizacao; }
        //        set { _Localizacao = value; }
        //    }
        //    private string _NumeroIdentificacao = string.Empty;

        //    [Obrigatorio(true, "Número de Identificação é campo obrigatório!")]
        //    public string NumeroIdentificacao
        //    {
        //        get { return _NumeroIdentificacao; }
        //        set { _NumeroIdentificacao = value; }
        //    }

        //    private FabricanteVasoCaldeira _IdFabricanteVasoCaldeira;

        //    public FabricanteVasoCaldeira IdFabricanteVasoCaldeira
        //    {
        //        get { return _IdFabricanteVasoCaldeira; }
        //        set { _IdFabricanteVasoCaldeira = value; }
        //    }

        //    private int _AnoFabricacao;

        //    public int AnoFabricacao
        //    {
        //        get { return _AnoFabricacao; }
        //        set { _AnoFabricacao = value; }
        //    }

        //    private TipoVasoCaldeira _IdTipoVasoCaldeira;

        //    public TipoVasoCaldeira IdTipoVasoCaldeira
        //    {
        //        get { return _IdTipoVasoCaldeira; }
        //        set { _IdTipoVasoCaldeira = value; }
        //    }

        //    private UnidadePressao _IdUnidadePressao;

        //    public UnidadePressao IdUnidadePressao
        //    {
        //        get { return _IdUnidadePressao; }
        //        set { _IdUnidadePressao = value; }
        //    }
        //    private string _CodigoProjeto = string.Empty;

        //    public string CodigoProjeto
        //    {
        //        get { return _CodigoProjeto; }
        //        set { _CodigoProjeto = value; }
        //    }
        //    private int _AnoEdicao;

        //    public int AnoEdicao
        //    {
        //        get { return _AnoEdicao; }
        //        set { _AnoEdicao = value; }
        //    }
        //    private double _MedidaCorpo;

        //    public double MedidaCorpo
        //    {
        //        get { return _MedidaCorpo; }
        //        set { _MedidaCorpo = value; }
        //    }
        //    private double _MedidaTampoSuperior;

        //    public double MedidaTampoSuperior
        //    {
        //        get { return _MedidaTampoSuperior; }
        //        set { _MedidaTampoSuperior = value; }
        //    }
        //    private double _MedidaTampoInferior;

        //    public double MedidaTampoInferior
        //    {
        //        get { return _MedidaTampoInferior; }
        //        set { _MedidaTampoInferior = value; }
        //    }
        //    private double _DiametroExterno;

        //    public double DiametroExterno
        //    {
        //        get { return _DiametroExterno; }
        //        set { _DiametroExterno = value; }
        //    }
        //    private double _MenorEspessuraCorpo;

        //    public double MenorEspessuraCorpo
        //    {
        //        get { return _MenorEspessuraCorpo; }
        //        set { _MenorEspessuraCorpo = value; }
        //    }
        //    private double _MenorEspessuraTampo;

        //    public double MenorEspessuraTampo
        //    {
        //        get { return _MenorEspessuraTampo; }
        //        set { _MenorEspessuraTampo = value; }
        //    }
        //    private double _MenorEspessura;

        //    public double MenorEspessura
        //    {
        //        get { return _MenorEspessura; }
        //        set { _MenorEspessura = value; }
        //    }
        //    private double _VolumeInterno;

        //    public double VolumeInterno
        //    {
        //        get { return _VolumeInterno; }
        //        set { _VolumeInterno = value; }
        //    }
        //    private double _PressaoMaximaTrabalho;

        //    public double PressaoMaximaTrabalho
        //    {
        //        get { return _PressaoMaximaTrabalho; }
        //        set { _PressaoMaximaTrabalho = value; }
        //    }

        //    private double _PressaoTesteHidrostatico;

        //    public double PressaoTesteHidrostatico
        //    {
        //        get { return _PressaoTesteHidrostatico; }
        //        set { _PressaoTesteHidrostatico = value; }
        //    }
        //    private double _RelacaoPV;

        //    public double RelacaoPV
        //    {
        //        get { return _RelacaoPV; }
        //        set { _RelacaoPV = value; }
        //    }

        //    private double _PressaoAbertura;

        //    public double PressaoAbertura
        //    {
        //        get { return _PressaoAbertura; }
        //        set { _PressaoAbertura = value; }
        //    }

        //    private double _Escala;

        //    public double Escala
        //    {
        //        get { return _Escala; }
        //        set { _Escala = value; }
        //    }

        //    private ValvulaSeguranca _IdValvulaSeguranca;

        //    public ValvulaSeguranca IdValvulaSeguranca
        //    {
        //        get { return _IdValvulaSeguranca; }
        //        set { _IdValvulaSeguranca = value; }
        //    }

        //    private MarcaTipoManometro _IdMarcaTipoManometro;

        //    public MarcaTipoManometro IdMarcaTipoManometro
        //    {
        //        get { return _IdMarcaTipoManometro; }
        //        set { _IdMarcaTipoManometro = value; }
        //    }

        //    public double GetPressaoOperacao()
        //    {
        //        return this.PressaoMaximaTrabalho * 0.9D;
        //    }

        //    public double GetPMTA_MPa()
        //    {
        //        return this.PressaoMaximaTrabalho * 0.0981D;
        //    }

        //    public static string GetUnidadeConvertida(double val_Kgf_cm2, UnidadePressao unidadePressao)
        //    {
        //        string ret;

        //        if (val_Kgf_cm2 == 0)
        //            ret = "Desconhecido";
        //        else if (unidadePressao.Id == 0)
        //            ret = val_Kgf_cm2.ToString("n") + " " + "Kgf/cm²";
        //        else
        //        {
        //            if (unidadePressao.mirrorOld == null)
        //                unidadePressao.Find();

        //            ret = (val_Kgf_cm2 * unidadePressao.Conversao).ToString("n") + " " + unidadePressao.Descricao;
        //        }

        //        return ret;
        //    }

        //    public string GetNumeroIdentificacao()
        //    {
        //        return "Nº " + this.NumeroIdentificacao;
        //    }

        //    public string GetFoto()
        //    {
        //        string ret = Path.Combine(Ilitera.Common.Fotos.GetRaizPath(),
        //                     Path.Combine(Path.Combine(this.IdCliente.GetFotoDiretorioPadrao(), "LaudoEletrico"), 
        //                                  this.Foto));
        //        return ret;
        //    }

        //    private ProjetoVasoCaldeira projetoInstalacao;

        //    public ProjetoVasoCaldeira GetProjetoInstalacao()
        //    {
        //        if (projetoInstalacao == null)
        //        {
        //            projetoInstalacao = new ProjetoVasoCaldeira();
        //            projetoInstalacao.Find("IdVasoCaldeiraBase=" + this.Id
        //                + " AND IndTipoProjeto =" + (int)TipoProjetoVasoCaldeira.ProjetoInstalacao);
        //        }
        //        return projetoInstalacao;
        //    }

        //    public InspecaoVasoCaldeira GetUltimaInspecao()
        //    {
        //        ArrayList list = new InspecaoVasoCaldeira().Find("IdVasoCaldeiraBase=" + this.Id
        //            +" AND IdPedido IN (SELECT IdPedido FROM Pedido WHERE DataConclusao IS NOT NULL AND DataCancelamento IS NULL)"
        //            + " ORDER BY DataLevantamento DESC");

        //        if (list.Count == 0)
        //            return new InspecaoVasoCaldeira();
        //        else
        //            return (InspecaoVasoCaldeira)list[0];
        //    }

        //    public override int Save()
        //    {
        //        int ret;

        //        this.Descricao = this.GetNumeroIdentificacao();

        //        bool AtualizarPedido = (this.Id == 0);

        //        if (projetoInstalacao != null)
        //        {
        //            ret = base.Save();

        //            if (projetoInstalacao.IdPrestador.Id != 0)
        //            {
        //                projetoInstalacao.IdCliente.Id = this.IdCliente.Id;
        //                projetoInstalacao.IdVasoCaldeiraBase.Id = ret;
        //                projetoInstalacao.Save();
        //            }

        //        }
        //        else
        //            ret = base.Save();

        //        if (AtualizarPedido)
        //        {
        //            Obrigacao obrigacaoProjeto = new Obrigacao();
        //            Obrigacao obrigacaoInspecao = new Obrigacao();

        //            if (this.IndVasoCaldeira == VasoCaldeiraTipo.LaudoEletrico)
        //            {
        //                obrigacaoProjeto.Find((int)Obrigacoes.VasosProjeto);
        //                obrigacaoInspecao.Find((int)Obrigacoes.VasosInspecao);
        //            }
        //            else
        //            {
        //                obrigacaoProjeto.Find((int)Obrigacoes.CaldeiraProjeto);
        //                obrigacaoInspecao.Find((int)Obrigacoes.CaldeiraInspecao);
        //            }

        //            ObrigacaoCliente obrigCliente1 = ObrigacaoCliente.GetObrigacaoCliente(this.IdCliente, obrigacaoProjeto);
        //            obrigCliente1.Atualizar();

        //            ObrigacaoCliente obrigCliente2 = ObrigacaoCliente.GetObrigacaoCliente(this.IdCliente, obrigacaoInspecao);
        //            obrigCliente2.Atualizar();

        //            //ObrigacaoCliente.CriaObrigacaoCliente(this.IdCliente, obrigacaoProjeto, true);
        //            //ObrigacaoCliente.CriaObrigacaoCliente(this.IdCliente, obrigacaoInspecao, true);
        //        }

        //        return ret;
        //    }
        //}
        //#endregion

        //#region LaudoEletrico

        [Database("opsa", "LaudoEletrico", "IdLaudoEletrico")]
        public class LaudoEletrico : Ilitera.Data.Table
        {

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

            public LaudoEletrico()
            {
                //        this.IndVasoCaldeira = VasoCaldeiraTipo.LaudoEletrico;
                //        this.IndTipoEquipamento = EquipamentoBaseTipo.LaudoEletrico;
            }

            //    public enum VasoTipo : int
            //    {
            //        LaudoEletrico,
            //        Reator
            //    }

            //    #region properties

            private int _IdLaudoEletrico;

            public override int Id
            {
                get { return _IdLaudoEletrico; }
                set { _IdLaudoEletrico = value; }
            }

            private string _Descricao;
            public string Descricao
            {
                get { return _Descricao; }
                set { _Descricao = value; }
            }

            private DateTime _Data_Laudo;
            public DateTime Data_Laudo
            {
                get { return _Data_Laudo; }
                set { _Data_Laudo = value; }
            }


            private Cliente _IdCliente;

            public Cliente IdCliente
            {
                get { return _IdCliente; }
                set { _IdCliente = value; }
            }


            private Prestador _Id_Responsavel;

            public Prestador Id_Responsavel
            {
                get { return _Id_Responsavel; }
                set { _Id_Responsavel = value; }
            }


            private string _Introducao;

            public string Introducao
            {
                get { return _Introducao; }
                set { _Introducao = value; }
            }


            private string _Conclusao;

            public string Conclusao
            {
                get { return _Conclusao; }
                set { _Conclusao = value; }
            }


            private string _Indice;

            public string Indice
            {
                get { return _Indice; }
                set { _Indice = value; }
            }


            private string _Objetivos;

            public string Objetivos
            {
                get { return _Objetivos; }
                set { _Objetivos = value; }
            }


            private string _Normas;

            public string Normas
            {
                get { return _Normas; }
                set { _Normas = value; }
            }


            private string _Generalidades;

            public string Generalidades
            {
                get { return _Generalidades; }
                set { _Generalidades = value; }
            }


            private Int32 _Id_Eletricista;

            public Int32 Id_Eletricista
            {
                get { return _Id_Eletricista; }
                set { _Id_Eletricista = value; }
            }

            private Int32 _Id_Responsavel_Empresa;

            public Int32 Id_Responsavel_Empresa
            {
                get { return _Id_Responsavel_Empresa; }
                set { _Id_Responsavel_Empresa = value; }
            }

            private string _IsInativo;

            public string IsInativo
            {
                get { return _IsInativo; }
                set { _IsInativo = value; }
            }


            private string _NR1023;

            public string NR1023
            {
                get { return _NR1023; }
                set { _NR1023 = value; }
            }

            private string _NR1024a;

            public string NR1024a
            {
                get { return _NR1024a; }
                set { _NR1024a = value; }
            }

            private string _NR1024b;

            public string NR1024b
            {
                get { return _NR1024b; }
                set { _NR1024b = value; }
            }

            private string _NR1024c;

            public string NR1024c
            {
                get { return _NR1024c; }
                set { _NR1024c = value; }
            }

            private string _NR1024d;

            public string NR1024d
            {
                get { return _NR1024d; }
                set { _NR1024d = value; }
            }

            private string _NR1024e;

            public string NR1024e
            {
                get { return _NR1024e; }
                set { _NR1024e = value; }
            }

            private string _NR1024f;

            public string NR1024f
            {
                get { return _NR1024f; }
                set { _NR1024f = value; }
            }

            private string _NR1024g;

            public string NR1024g
            {
                get { return _NR1024g; }
                set { _NR1024g = value; }
            }

            private string _NR1025a;

            public string NR1025a
            {
                get { return _NR1025a; }
                set { _NR1025a = value; }
            }

            private string _NR1025b;

            public string NR1025b
            {
                get { return _NR1025b; }
                set { _NR1025b = value; }
            }

            private string _NR1088a;

            public string NR1088a
            {
                get { return _NR1088a; }
                set { _NR1088a = value; }
            }

            private string _NR1088b;

            public string NR1088b
            {
                get { return _NR1088b; }
                set { _NR1088b = value; }
            }

            private string _NR1088c;

            public string NR1088c
            {
                get { return _NR1088c; }
                set { _NR1088c = value; }
            }

            private string _PRa;

            public string PRa
            {
                get { return _PRa; }
                set { _PRa = value; }
            }

            private string _PRb;

            public string PRb
            {
                get { return _PRb; }
                set { _PRb = value; }
            }

            private string _PRc;

            public string PRc
            {
                get { return _PRc; }
                set { _PRc = value; }
            }


	        private string _Inspecao_BT;

            public string Inspecao_BT
            {
                get { return _Inspecao_BT; }
                set { _Inspecao_BT = value; }
            }

	        private string _Inspecao_MT;

            public string Inspecao_MT
            {
                get { return _Inspecao_MT; }
                set { _Inspecao_MT = value; }
            }

	        private string _Inspecao_AT;

            public string Inspecao_AT
            {
                get { return _Inspecao_AT; }
                set { _Inspecao_AT = value; }
            }

	        private string _BT_Foto1;

            public string BT_Foto1
            {
                get { return _BT_Foto1; }
                set { _BT_Foto1 = value; }
            }

	        private string _BT_Foto2;

            public string BT_Foto2
            {
                get { return _BT_Foto2; }
                set { _BT_Foto2 = value; }
            }

            private string _MT_Foto1;

            public string MT_Foto1
            {
                get { return _MT_Foto1; }
                set { _MT_Foto1 = value; }
            }

	        private string _MT_Foto2;

            public string MT_Foto2
            {
                get { return _MT_Foto2; }
                set { _MT_Foto2 = value; }
            }

	        private string _AT_Foto1;

            public string AT_Foto1
            {
                get { return _AT_Foto1; }
                set { _AT_Foto1 = value; }
            }

            private string _AT_Foto2;

            public string AT_Foto2
            {
                get { return _AT_Foto2; }
                set { _AT_Foto2 = value; }
            }

            private string _Anexo1;

            public string Anexo1
            {
                get { return _Anexo1; }
                set { _Anexo1 = value; }
            }

            private string _Anexo2;

            public string Anexo2
            {
                get { return _Anexo2; }
                set { _Anexo2 = value; }
            }

            private string _Anexo3;

            public string Anexo3
            {
                get { return _Anexo3; }
                set { _Anexo3 = value; }
            }

            private string _Anexo4;

            public string Anexo4
            {
                get { return _Anexo4; }
                set { _Anexo4 = value; }
            }

            private string _Descr_Anexo1;

            public string Descr_Anexo1
            {
                get { return _Descr_Anexo1; }
                set { _Descr_Anexo1 = value; }
            }

            private string _Descr_Anexo2;

            public string Descr_Anexo2
            {
                get { return _Descr_Anexo2; }
                set { _Descr_Anexo2 = value; }
            }

            private string _Descr_Anexo3;

            public string Descr_Anexo3
            {
                get { return _Descr_Anexo3; }
                set { _Descr_Anexo3 = value; }
            }

            private string _Descr_Anexo4;

            public string Descr_Anexo4
            {
                get { return _Descr_Anexo4; }
                set { _Descr_Anexo4 = value; }
            }


            private string _Anexo_1023;

            public string Anexo_1023
            {
                get { return _Anexo_1023; }
                set { _Anexo_1023 = value; }
            }

            private string _Anexo_1024;

            public string Anexo_1024
            {
                get { return _Anexo_1024; }
                set { _Anexo_1024 = value; }
            }

            private string _Anexo_1025;

            public string Anexo_1025
            {
                get { return _Anexo_1025; }
                set { _Anexo_1025 = value; }
            }

            private string _Anexo_1088;

            public string Anexo_1088
            {
                get { return _Anexo_1088; }
                set { _Anexo_1088 = value; }
            }

            private string _Anexo_PR;

            public string Anexo_PR
            {
                get { return _Anexo_PR; }
                set { _Anexo_PR = value; }
            }


            private string _Anexo_Generalidade1;

            public string Anexo_Generalidade1
            {
                get { return _Anexo_Generalidade1; }
                set { _Anexo_Generalidade1 = value; }
            }

            private string _Descr_Anexo_Generalidade1;

            public string Descr_Anexo_Generalidade1
            {
                get { return _Descr_Anexo_Generalidade1; }
                set { _Descr_Anexo_Generalidade1 = value; }
            }


            private string _Anexo_Generalidade2;

            public string Anexo_Generalidade2
            {
                get { return _Anexo_Generalidade2; }
                set { _Anexo_Generalidade2 = value; }
            }

            private string _Descr_Anexo_Generalidade2;

            public string Descr_Anexo_Generalidade2
            {
                get { return _Descr_Anexo_Generalidade2; }
                set { _Descr_Anexo_Generalidade2 = value; }
            }


            //    private GrupoRisco _IdGrupoRisco;

            //    public GrupoRisco IdGrupoRisco
            //    {
            //        get { return _IdGrupoRisco; }
            //        set { _IdGrupoRisco = value; }
            //    }
            //    private CategoriaLaudoEletrico _IdCategoriaLaudoEletrico;


            //    private VasoTipo _IndVasoTipo;

            //    public VasoTipo IndVasoTipo
            //    {
            //        get { return _IndVasoTipo; }
            //        set { _IndVasoTipo = value;}

            //    }
            //    #endregion

            //    #region metodos

            //    public int PrazoExameExterno()
            //    {
            //        if (this.IdCategoriaLaudoEletrico.mirrorOld == null)
            //            this.IdCategoriaLaudoEletrico.Find();

            //        return this.IdCategoriaLaudoEletrico.ExameExterno;
            //    }

            //    public int PrazoExameInterno()
            //    {
            //        if (this.IdCategoriaLaudoEletrico.mirrorOld == null)
            //            this.IdCategoriaLaudoEletrico.Find();

            //        return this.IdCategoriaLaudoEletrico.ExameInterno;
            //    }

            //    public int PrazoTesteHidrostatico()
            //    {
            //        if (this.IdCategoriaLaudoEletrico.mirrorOld == null)
            //            this.IdCategoriaLaudoEletrico.Find();

            //        return this.IdCategoriaLaudoEletrico.TesteHidrostatico;
            //    }

            //    public override void Validate()
            //    {
            //        this.IndVasoCaldeira = VasoCaldeiraTipo.LaudoEletrico;

            //        base.Validate();
            //    }

            //    public static ProjetoVasoCaldeira GetProjetoInstalacao(Pedido pedido)
            //    {
            //        ProjetoVasoCaldeira projeto = new ProjetoVasoCaldeira();
            //        projeto.Find("IdPedido=" + pedido.Id);

            //        if (projeto.Id == 0)
            //        {
            //            if (pedido.DataConclusao != new DateTime())
            //                throw new Exception("Projeto não localizado!");

            //            pedido.IdPedidoGrupo.Find();
            //            pedido.IdPedidoGrupo.IdCompromisso.Find();

            //            projeto.Inicialize();
            //            projeto.IdDocumentoBase.Id = (int)Documentos.VasosProjeto;
            //            projeto.IdPedido = pedido;
            //            projeto.IdCliente = pedido.IdCliente;
            //            projeto.IdPrestador = pedido.IdPrestador;
            //            projeto.IndTipoProjeto = TipoProjetoVasoCaldeira.ProjetoInstalacao;
            //            projeto.DataLevantamento = DateTime.Today;
            //        }

            //        return projeto;
            //    }

            //    #endregion


            public ArrayList GerarCronogramaPadrao( DateTime xData_Laudo)
            {
                ArrayList list = new ArrayList();

                //Padrão 01            
                CronogramaLaudoEletrico cronogramaLaudoEletrico1 = new CronogramaLaudoEletrico();
                cronogramaLaudoEletrico1.Inicialize();
                cronogramaLaudoEletrico1.IdLaudoEletrico = this.Id;
                cronogramaLaudoEletrico1.Prazo = xData_Laudo.AddMonths(1);                
                cronogramaLaudoEletrico1.PlanejamentoAnual = "1) Renovação do LaudoEletrico.";
                cronogramaLaudoEletrico1.MetodologiaAcao = "";
                cronogramaLaudoEletrico1.FormaRegistro = "";
                //cronogramaLaudoEletrico1.PopularPrazo();
                cronogramaLaudoEletrico1.Mes10 = true;
                cronogramaLaudoEletrico1.Mes11 = true;
                cronogramaLaudoEletrico1.Ano = xData_Laudo.Year.ToString();
                cronogramaLaudoEletrico1.Save();
                list.Add(cronogramaLaudoEletrico1);
                
                //Padrão 02
                CronogramaLaudoEletrico cronogramaLaudoEletrico2 = new CronogramaLaudoEletrico();
                cronogramaLaudoEletrico2.Inicialize();
                cronogramaLaudoEletrico2.IdLaudoEletrico = this.Id;
                cronogramaLaudoEletrico2.Prazo = xData_Laudo.AddYears(1);
                cronogramaLaudoEletrico2.PlanejamentoAnual = "2) Emissão de Relatório Anual.";
                cronogramaLaudoEletrico2.MetodologiaAcao = "";
                cronogramaLaudoEletrico2.FormaRegistro = "";
                //cronogramaLaudoEletrico2.PopularPrazo();
                cronogramaLaudoEletrico2.Mes10 = true;
                cronogramaLaudoEletrico2.Mes11 = true;
                cronogramaLaudoEletrico2.Ano = xData_Laudo.Year.ToString();
                cronogramaLaudoEletrico2.Save();
                list.Add(cronogramaLaudoEletrico2);

                //Padrão 03
                CronogramaLaudoEletrico cronogramaLaudoEletrico3 = new CronogramaLaudoEletrico();
                cronogramaLaudoEletrico3.Inicialize();
                cronogramaLaudoEletrico3.IdLaudoEletrico = this.Id;
                cronogramaLaudoEletrico3.Prazo = xData_Laudo.AddMonths(1);
                cronogramaLaudoEletrico3.PlanejamentoAnual = "3) Realização e Exames Periódicos.";
                cronogramaLaudoEletrico3.MetodologiaAcao = "";
                cronogramaLaudoEletrico3.FormaRegistro = "";
                cronogramaLaudoEletrico3.Mes12 = true;
                cronogramaLaudoEletrico3.Mes01 = true;
                cronogramaLaudoEletrico3.Ano = xData_Laudo.Year + " e " + xData_Laudo.AddYears(1).Year;
                cronogramaLaudoEletrico3.Save();
                list.Add(cronogramaLaudoEletrico3);

                //Padrão 04
                CronogramaLaudoEletrico cronogramaLaudoEletrico4 = new CronogramaLaudoEletrico();
                cronogramaLaudoEletrico4.Inicialize();
                cronogramaLaudoEletrico4.IdLaudoEletrico = this.Id;
                cronogramaLaudoEletrico4.Prazo = xData_Laudo.AddMonths(1);
                cronogramaLaudoEletrico4.PlanejamentoAnual = "4) Semana Interna de Prevenção de Acidentes - SIPAT.";
                cronogramaLaudoEletrico4.MetodologiaAcao = "";
                cronogramaLaudoEletrico4.FormaRegistro = "";
                cronogramaLaudoEletrico4.Mes04 = true;
                cronogramaLaudoEletrico4.Mes05 = true;
                cronogramaLaudoEletrico4.Ano = xData_Laudo.AddYears(1).Year.ToString();
                cronogramaLaudoEletrico4.Save();
                list.Add(cronogramaLaudoEletrico4);
                return list;
            }
        }




        [Database("opsa", "LaudoEletrico_Adequacao", "IdLaudoEletrico_Adequacao")]
        public class LaudoEletrico_Adequacao : Ilitera.Data.Table
        {
            private int _IdLaudoEletrico_Adequacao;
            private int _IdLaudoEletrico;
            private string _Titulo;
            private string _Descricao;
            private string _Foto;
            private int _Ordem;
            private string _Foto2;
            private string _Foto3;
            private string _FotoTermografia;
            private string _Tensao;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public LaudoEletrico_Adequacao()
            {

            }
            public override int Id
            {
                get { return _IdLaudoEletrico_Adequacao; }
                set { _IdLaudoEletrico_Adequacao = value; }
            }
            public int IdLaudoEletrico
            {
                get { return _IdLaudoEletrico; }
                set { _IdLaudoEletrico = value; }
            }
            public string Titulo
            {
                get { return _Titulo; }
                set { _Titulo = value; }
            }
            public string Descricao
            {
                get { return _Descricao; }
                set { _Descricao = value; }
            }
            public string Foto
            {
                get { return _Foto; }
                set { _Foto = value; }
            }
            public string Foto2
            {
                get { return _Foto2; }
                set { _Foto2 = value; }
            }
            public string Foto3
            {
                get { return _Foto3; }
                set { _Foto3 = value; }
            }
            public string FotoTermografia
            {
                get { return _FotoTermografia; }
                set { _FotoTermografia = value; }
            }
            public int Ordem
            {
                get { return _Ordem; }
                set { _Ordem = value; }
            }
            public string Tensao
            {
                get { return _Tensao; }
                set { _Tensao = value; }
            }


            public override string ToString()
            {
                return this._Titulo.ToString();
            }
        }


        [Database("opsa", "LaudoEletrico_Adequacao_Item", "IdLaudoEletrico_Adequacao_Item")]
        public class LaudoEletrico_Adequacao_Item : Ilitera.Data.Table
        {
            private int _IdLaudoEletrico_Adequacao_Item;
            private int _IdLaudoEletrico_Adequacao;            
            private string _Irregularidade;
            private string _Recomendacoes;
            private string _Situacao;
            private int _Ordem;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public LaudoEletrico_Adequacao_Item()
            {

            }
            public override int Id
            {
                get { return _IdLaudoEletrico_Adequacao_Item; }
                set { _IdLaudoEletrico_Adequacao_Item = value; }
            }     
            public int IdLaudoEletrico_Adequacao
            {
                get { return _IdLaudoEletrico_Adequacao; }
                set { _IdLaudoEletrico_Adequacao = value; }
            }
            public string Irregularidade
            {
                get { return _Irregularidade; }
                set { _Irregularidade = value; }
            }
            public string Recomendacoes
            {
                get { return _Recomendacoes; }
                set { _Recomendacoes = value; }
            }
            public string Situacao
            {
                get { return _Situacao; }
                set { _Situacao = value; }
            }
            public int Ordem
            {
                get { return _Ordem; }
                set { _Ordem = value; }
            }

            public override string ToString()
            {
                return this._Irregularidade.ToString();
            }
        }




        [Database("opsa", "LaudoSPDA", "IdLaudoSPDA")]
        public class LaudoSPDA : Ilitera.Data.Table
        {

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public LaudoSPDA()
            {
                //        this.IndVasoCaldeira = VasoCaldeiraTipo.LaudoEletrico;
                //        this.IndTipoEquipamento = EquipamentoBaseTipo.LaudoEletrico;
            }

            //    public enum VasoTipo : int
            //    {
            //        LaudoEletrico,
            //        Reator
            //    }

            //    #region properties

            private int _IdLaudoSPDA;

            public override int Id
            {
                get { return _IdLaudoSPDA; }
                set { _IdLaudoSPDA = value; }
            }

            private string _Descricao;
            public string Descricao
            {
                get { return _Descricao; }
                set { _Descricao = value; }
            }

            private DateTime _Data_Laudo;
            public DateTime Data_Laudo
            {
                get { return _Data_Laudo; }
                set { _Data_Laudo = value; }
            }


            private Cliente _IdCliente;

            public Cliente IdCliente
            {
                get { return _IdCliente; }
                set { _IdCliente = value; }
            }


            private Prestador _Id_Responsavel;

            public Prestador Id_Responsavel
            {
                get { return _Id_Responsavel; }
                set { _Id_Responsavel = value; }
            }


            private string _Introducao;

            public string Introducao
            {
                get { return _Introducao; }
                set { _Introducao = value; }
            }


            private string _Conclusao;

            public string Conclusao
            {
                get { return _Conclusao; }
                set { _Conclusao = value; }
            }


            private string _Observacoes;

            public string Observacoes
            {
                get { return _Observacoes; }
                set { _Observacoes = value; }
            }

            private string _Recomendacoes;

            public string Recomendacoes
            {
                get { return _Recomendacoes; }
                set { _Recomendacoes = value; }
            }


            private Int32 _Id_Eletricista;

            public Int32 Id_Eletricista
            {
                get { return _Id_Eletricista; }
                set { _Id_Eletricista = value; }
            }

            private Int32 _Id_Responsavel_Empresa;

            public Int32 Id_Responsavel_Empresa
            {
                get { return _Id_Responsavel_Empresa; }
                set { _Id_Responsavel_Empresa = value; }
            }

            private string _IsInativo;

            public string IsInativo
            {
                get { return _IsInativo; }
                set { _IsInativo = value; }
            }



            private string _PRa;

            public string PRa
            {
                get { return _PRa; }
                set { _PRa = value; }
            }

            private string _PRb;

            public string PRb
            {
                get { return _PRb; }
                set { _PRb = value; }
            }

            private string _Croqui;

            public string Croqui
            {
                get { return _Croqui; }
                set { _Croqui = value; }
            }

            private string _Indice;

            public string Indice
            {
                get { return _Indice; }
                set { _Indice = value; }
            }

            private string _Objetivos;

            public string Objetivos
            {
                get { return _Objetivos; }
                set { _Objetivos = value; }
            }

            private string _Normas;

            public string Normas
            {
                get { return _Normas; }
                set { _Normas = value; }
            }

            private string _Caracteristicas_gerais;

            public string Caracteristicas_gerais
            {
                get { return _Caracteristicas_gerais; }
                set { _Caracteristicas_gerais = value; }
            }

            private string _Caracteristicas_gerais_Foto1;

            public string Caracteristicas_gerais_Foto1
            {
                get { return _Caracteristicas_gerais_Foto1; }
                set { _Caracteristicas_gerais_Foto1 = value; }
            }

            private string _Inspecao_Detalhada;

            public string Inspecao_Detalhada
            {
                get { return _Inspecao_Detalhada; }
                set { _Inspecao_Detalhada = value; }
            }
            
            private string _Inspecao_Detalhada_Foto1;

            public string Inspecao_Detalhada_Foto1
            {
                get { return _Inspecao_Detalhada_Foto1; }
                set { _Inspecao_Detalhada_Foto1 = value; }
            }

            private string _Inspecao_Detalhada_Foto2;

            public string Inspecao_Detalhada_Foto2
            {
                get { return _Inspecao_Detalhada_Foto2; }
                set { _Inspecao_Detalhada_Foto2 = value; }
            }

            private string _Anexo1;

            public string Anexo1
            {
                get { return _Anexo1; }
                set { _Anexo1 = value; }
            }

            private string _Anexo2;

            public string Anexo2
            {
                get { return _Anexo2; }
                set { _Anexo2 = value; }
            }

            private string _Anexo3;

            public string Anexo3
            {
                get { return _Anexo3; }
                set { _Anexo3 = value; }
            }

            private string _Anexo4;

            public string Anexo4
            {
                get { return _Anexo4; }
                set { _Anexo4 = value; }
            }

            private string _Descr_Anexo1;

            public string Descr_Anexo1
            {
                get { return _Descr_Anexo1; }
                set { _Descr_Anexo1 = value; }
            }

            private string _Descr_Anexo2;

            public string Descr_Anexo2
            {
                get { return _Descr_Anexo2; }
                set { _Descr_Anexo2 = value; }
            }

            private string _Descr_Anexo3;

            public string Descr_Anexo3
            {
                get { return _Descr_Anexo3; }
                set { _Descr_Anexo3 = value; }
            }

            private string _Descr_Anexo4;

            public string Descr_Anexo4
            {
                get { return _Descr_Anexo4; }
                set { _Descr_Anexo4 = value; }
            }


            private string _Generalidades;

            public string Generalidades
            {
                get { return _Generalidades; }
                set { _Generalidades = value; }
            }


            private string _Anexo_Caracteristica1;

            public string Anexo_Caracteristica1
            {
                get { return _Anexo_Caracteristica1; }
                set { _Anexo_Caracteristica1 = value; }
            }

            private string _Descr_Anexo_Caracteristica1;

            public string Descr_Anexo_Caracteristica1
            {
                get { return _Descr_Anexo_Caracteristica1; }
                set { _Descr_Anexo_Caracteristica1 = value; }
            }


            private string _Anexo_Caracteristica2;

            public string Anexo_Caracteristica2
            {
                get { return _Anexo_Caracteristica2; }
                set { _Anexo_Caracteristica2 = value; }
            }

            private string _Descr_Anexo_Caracteristica2;

            public string Descr_Anexo_Caracteristica2
            {
                get { return _Descr_Anexo_Caracteristica2; }
                set { _Descr_Anexo_Caracteristica2 = value; }
            }



        }



        [Database("opsa", "SPDA_Adequacao2", "IdSPDA_Adequacao")]
        public class SPDA_Adequacao2 : Ilitera.Data.Table
        {
            private int _IdSPDA_Adequacao;
            private int _IdSPDA;
            private string _Titulo;
            private string _Descricao;
            private string _Foto;
            private int _Ordem;
            private string _Foto2;
            private string _Foto3;
            private string _FotoTermografia;
            private string _Tensao;


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public SPDA_Adequacao2()
            {

            }
            public override int Id
            {
                get { return _IdSPDA_Adequacao; }
                set { _IdSPDA_Adequacao = value; }
            }
            public int IdSPDA
            {
                get { return _IdSPDA; }
                set { _IdSPDA = value; }
            }
            public string Titulo
            {
                get { return _Titulo; }
                set { _Titulo = value; }
            }
            public string Descricao
            {
                get { return _Descricao; }
                set { _Descricao = value; }
            }
            public string Foto
            {
                get { return _Foto; }
                set { _Foto = value; }
            }
            public string Foto2
            {
                get { return _Foto2; }
                set { _Foto2 = value; }
            }
            public string Foto3
            {
                get { return _Foto3; }
                set { _Foto3 = value; }
            }
            public string FotoTermografia
            {
                get { return _FotoTermografia; }
                set { _FotoTermografia = value; }
            }
            public int Ordem
            {
                get { return _Ordem; }
                set { _Ordem = value; }
            }
            public string Tensao
            {
                get { return _Tensao; }
                set { _Tensao = value; }
            }


            public override string ToString()
            {
                return this._Titulo.ToString();
            }
        }


        [Database("opsa", "SPDA_Adequacao_Item2", "IdSPDA_Adequacao_Item")]
        public class SPDA_Adequacao_Item2 : Ilitera.Data.Table
        {
            private int _IdSPDA_Adequacao_Item;
            private int _IdSPDA_Adequacao;
            private string _Irregularidade;
            private string _Recomendacoes;
            private string _Situacao;
            private int _Ordem;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public SPDA_Adequacao_Item2()
            {

            }
            public override int Id
            {
                get { return _IdSPDA_Adequacao_Item; }
                set { _IdSPDA_Adequacao_Item = value; }
            }
            public int IdSPDA_Adequacao
            {
                get { return _IdSPDA_Adequacao; }
                set { _IdSPDA_Adequacao = value; }
            }
            public string Irregularidade
            {
                get { return _Irregularidade; }
                set { _Irregularidade = value; }
            }
            public string Recomendacoes
            {
                get { return _Recomendacoes; }
                set { _Recomendacoes = value; }
            }
            public string Situacao
            {
                get { return _Situacao; }
                set { _Situacao = value; }
            }
            public int Ordem
            {
                get { return _Ordem; }
                set { _Ordem = value; }
            }

            public override string ToString()
            {
                return this._Irregularidade.ToString();
            }
        }




        //#endregion

        //#region Caldeira

        //[Database("opsa", "Caldeira", "IdCaldeira")]
        //public class Caldeira : Ilitera.Opsa.Data.VasoCaldeiraBase
        //{
        //    public Caldeira()
        //    {
        //        this.IndVasoCaldeira = VasoCaldeiraTipo.Caldeira;
        //        this.IndTipoEquipamento = EquipamentoBaseTipo.Caldeira;
        //    }

        //    public enum CategoriaCaldeira : int
        //    {
        //        A = 1,
        //        B = 2,
        //        C = 3
        //    }

        //    private int _IdCaldeira;

        //    public override int Id
        //    {
        //        get { return _IdCaldeira; }
        //        set { _IdCaldeira = value; }
        //    }

        //    private CategoriaCaldeira _IndCategoriaCaldeira;

        //    public CategoriaCaldeira IndCategoriaCaldeira
        //    {
        //        get { return _IndCategoriaCaldeira; }
        //        set { _IndCategoriaCaldeira = value; }
        //    }

        //    private double _CapacidadeProducaoVapor;

        //    public double CapacidadeProducaoVapor
        //    {
        //        get { return _CapacidadeProducaoVapor; }
        //        set { _CapacidadeProducaoVapor = value; }
        //    }

        //    private double _AreaSuperficieAquecimento;

        //    public double AreaSuperficieAquecimento
        //    {
        //        get { return _AreaSuperficieAquecimento; }
        //        set { _AreaSuperficieAquecimento = value; }
        //    }

        //    private CombustivelCaldeira _IdCombustivelCaldeira;

        //    public CombustivelCaldeira IdCombustivelCaldeira
        //    {
        //        get { return _IdCombustivelCaldeira; }
        //        set { _IdCombustivelCaldeira = value; }
        //    }

        //    public int PrazoExameExterno()
        //    {
        //        return 1;
        //    }

        //    public int PrazoExameInterno()
        //    {
        //        return 1;
        //    }

        //    public int PrazoTesteHidrostatico()
        //    {
        //        return 1;
        //    }

        //    public static ProjetoVasoCaldeira GetProjetoInstalacao(Pedido pedido)
        //    {
        //        ProjetoVasoCaldeira projeto = new ProjetoVasoCaldeira();
        //        projeto.Find("IdPedido=" + pedido.Id);

        //        if (projeto.Id == 0)
        //        {
        //            if (pedido.DataConclusao != new DateTime())
        //                throw new Exception("Projeto não localizado!");

        //            pedido.IdPedidoGrupo.Find();
        //            pedido.IdPedidoGrupo.IdCompromisso.Find();

        //            Prestador prestador = new Prestador();
        //            prestador.Find("IdPessoa=" + Usuario.Login().IdPessoa.Id);

        //            projeto.Inicialize();
        //            projeto.IdDocumentoBase.Id = (int)Documentos.CaldeiraProjeto;
        //            projeto.IdPedido = pedido;
        //            projeto.IdCliente = pedido.IdCliente;
        //            projeto.IdPrestador = prestador;
        //            projeto.IdAutoria = pedido.IdPrestador;
        //            projeto.IndTipoProjeto = TipoProjetoVasoCaldeira.ProjetoInstalacao;
        //            projeto.DataLevantamento = DateTime.Today;
        //        }

        //        return projeto;
        //    }
        //}
        //#endregion

        //#region CombustivelCaldeira

        //[Database("opsa", "CombustivelCaldeira", "IdCombustivelCaldeira")]
        //public class CombustivelCaldeira : Ilitera.Data.Table
        //{
        //    public CombustivelCaldeira()
        //    {

        //    }
        //    private int _IdCombustivelCaldeira;

        //    public override int Id
        //    {
        //        get { return _IdCombustivelCaldeira; }
        //        set { _IdCombustivelCaldeira = value; }
        //    }

        //    private string _Descricao = string.Empty;

        //    public string Descricao
        //    {
        //        get { return _Descricao; }
        //        set { _Descricao = value; }
        //    }

        //    public override string ToString()
        //    {
        //        if (this.mirrorOld == null)
        //            this.Find();

        //        return _Descricao;
        //    }
        //}
        //#endregion

        //#region UnidadePressao

        //[Database("opsa", "UnidadePressao", "IdUnidadePressao")]
        //public class UnidadePressao : Ilitera.Data.Table
        //{
        //    public UnidadePressao()
        //    {

        //    }
        //    private int _IdUnidadePressao;

        //    public override int Id
        //    {
        //        get { return _IdUnidadePressao; }
        //        set { _IdUnidadePressao = value; }
        //    }

        //    private string _Descricao = string.Empty;

        //    public string Descricao
        //    {
        //        get { return _Descricao; }
        //        set { _Descricao = value; }
        //    }

        //    private double _Conversao;

        //    public double Conversao
        //    {
        //        get { return _Conversao; }
        //        set { _Conversao = value; }
        //    }

        //    public override string ToString()
        //    {
        //        if (this.mirrorOld == null)
        //            this.Find();

        //        return _Descricao;
        //    }
        //}
        //#endregion

        //#region MarcaTipoManometro

        //[Database("opsa", "MarcaTipoManometro", "IdMarcaTipoManometro")]
        //public class MarcaTipoManometro : Ilitera.Data.Table
        //{
        //    public MarcaTipoManometro()
        //    {

        //    }
        //    private int _IdMarcaTipoManometro;

        //    public override int Id
        //    {
        //        get { return _IdMarcaTipoManometro; }
        //        set { _IdMarcaTipoManometro = value; }
        //    }

        //    private string _Descricao = string.Empty;

        //    public string Descricao
        //    {
        //        get { return _Descricao; }
        //        set { _Descricao = value; }
        //    }

        //    public override string ToString()
        //    {
        //        if (this.mirrorOld == null)
        //            this.Find();

        //        return _Descricao;
        //    }
        //}
        //#endregion

        //#region ValvulaSeguranca

        //[Database("opsa", "ValvulaSeguranca", "IdValvulaSeguranca")]
        //public class ValvulaSeguranca : Ilitera.Data.Table
        //{
        //    public ValvulaSeguranca()
        //    {

        //    }
        //    private int _IdValvulaSegurancaTipo;

        //    public override int Id
        //    {
        //        get { return _IdValvulaSegurancaTipo; }
        //        set { _IdValvulaSegurancaTipo = value; }
        //    }

        //    private string _Descricao = string.Empty;

        //    public string Descricao
        //    {
        //        get { return _Descricao; }
        //        set { _Descricao = value; }
        //    }

        //    public override string ToString()
        //    {
        //        if (this.mirrorOld == null)
        //            this.Find();

        //        return _Descricao;
        //    }
        //}
        //#endregion

        //#region FabricanteVasoCaldeira

        //[Database("opsa", "FabricanteVasoCaldeira", "IdFabricanteVasoCaldeira", "", "Fabricante de Caldeiras e Vasos de Pressão")]
        //public class FabricanteVasoCaldeira : Ilitera.Data.Table
        //{
        //    public FabricanteVasoCaldeira()
        //    {

        //    }
        //    private int _IdFabricanteLaudoEletrico;

        //    public override int Id
        //    {
        //        get { return _IdFabricanteLaudoEletrico; }
        //        set { _IdFabricanteLaudoEletrico = value; }
        //    }

        //    private string _Descricao = string.Empty;

        //    public string Descricao
        //    {
        //        get { return _Descricao; }
        //        set { _Descricao = value; }
        //    }

        //    public override string ToString()
        //    {
        //        if (this.mirrorOld == null)
        //            this.Find();

        //        return _Descricao;
        //    }
        //}
        //#endregion

        //#region TipoVasoCaldeira

        //[Database("opsa", "TipoVasoCaldeira", "IdTipoVasoCaldeira")]
        //public class TipoVasoCaldeira : Ilitera.Data.Table
        //{
        //    public TipoVasoCaldeira()
        //    {

        //    }
        //    private int _IdTipoVasoCaldeira;

        //    public override int Id
        //    {
        //        get { return _IdTipoVasoCaldeira; }
        //        set { _IdTipoVasoCaldeira = value; }
        //    }

        //    private string _Descricao = string.Empty;

        //    public string Descricao
        //    {
        //        get { return _Descricao; }
        //        set { _Descricao = value; }
        //    }

        //    public override string ToString()
        //    {
        //        if (this.mirrorOld == null)
        //            this.Find();

        //        return _Descricao;
        //    }
        //}
        //#endregion

        //#region ClasseDeFluido

        //[Database("opsa", "ClasseDeFluido", "IdClasseDeFluido")]
        //public class ClasseDeFluido : Ilitera.Data.Table
        //{
        //    public enum ClasseDeFluidos : int
        //    {
        //        A = 1,
        //        B = 2,
        //        C = 3,
        //        D = 4
        //    }

        //    public ClasseDeFluido()
        //    {

        //    }
        //    private int _IdClasseDeFluido;

        //    public override int Id
        //    {
        //        get { return _IdClasseDeFluido; }
        //        set { _IdClasseDeFluido = value; }
        //    }

        //    private string _Descricao = string.Empty;

        //    public string Descricao
        //    {
        //        get { return _Descricao; }
        //        set { _Descricao = value; }
        //    }

        //    public override string ToString()
        //    {
        //        if (this.mirrorOld == null)
        //            this.Find();

        //        return _Descricao;
        //    }
        //}
        //#endregion

        //#region FluidoServico

        //[Database("opsa", "FluidoServico", "IdFluidoServico", "", "Fluídos de Serviços")]
        //public class FluidoServico : Ilitera.Data.Table
        //{
        //    public FluidoServico()
        //    {

        //    }
        //    private int _IdFluidoServico;

        //    public override int Id
        //    {
        //        get { return _IdFluidoServico; }
        //        set { _IdFluidoServico = value; }
        //    }

        //    private string _Descricao = string.Empty;

        //    public string Descricao
        //    {
        //        get { return _Descricao; }
        //        set { _Descricao = value; }
        //    }

        //    public override string ToString()
        //    {
        //        if (this.mirrorOld == null)
        //            this.Find();

        //        return _Descricao;
        //    }
        //}
        //#endregion

        //#region GrupoRisco

        //[Database("opsa", "GrupoRisco", "IdGrupoRisco")]
        //public class GrupoRisco : Ilitera.Data.Table
        //{
        //    public enum GrupoRiscos : int
        //    {
        //        G1_PV_Maior_Igual_100 = 1,
        //        G2_PV_Menor_100_E_Maior_Igual30 = 2,
        //        G3_PV_Menor_30_E_Maior_Igual2_5 = 3,
        //        G4_PV_Menor_2_5_E_Maior_Igual1 = 4,
        //        G5_PV_Menor_1 = 5
        //    }

        //    public GrupoRisco()
        //    {

        //    }
        //    private int _IdGrupoRisco;

        //    public override int Id
        //    {
        //        get { return _IdGrupoRisco; }
        //        set { _IdGrupoRisco = value; }
        //    }

        //    private string _Descricao = string.Empty;

        //    public string Descricao
        //    {
        //        get { return _Descricao; }
        //        set { _Descricao = value; }
        //    }

        //    public override string ToString()
        //    {
        //        if (this.mirrorOld == null)
        //            this.Find();

        //        return _Descricao;
        //    }
        //}
        //#endregion

        //#region CategoriaLaudoEletrico

        //[Database("opsa", "CategoriaLaudoEletrico", "IdCategoriaLaudoEletrico")]
        //public class CategoriaLaudoEletrico : Ilitera.Data.Table
        //{
        //    public enum CategoriasLaudoEletrico : int
        //    {
        //        I = 1,
        //        II = 2,
        //        III = 3,
        //        IV = 4,
        //        V = 5
        //    }

        //    public CategoriaLaudoEletrico()
        //    {

        //    }
        //    private int _IdCategoriaLaudoEletrico;

        //    public override int Id
        //    {
        //        get { return _IdCategoriaLaudoEletrico; }
        //        set { _IdCategoriaLaudoEletrico = value; }
        //    }

        //    private string _Descricao = string.Empty;

        //    public string Descricao
        //    {
        //        get { return _Descricao; }
        //        set { _Descricao = value; }
        //    }

        //    private int _ExameExterno;

        //    public int ExameExterno
        //    {
        //        get { return _ExameExterno; }
        //        set { _ExameExterno = value; }
        //    }

        //    private int _ExameInterno;

        //    public int ExameInterno
        //    {
        //        get { return _ExameInterno; }
        //        set { _ExameInterno = value; }
        //    }

        //    private int _TesteHidrostatico;

        //    public int TesteHidrostatico
        //    {
        //        get { return _TesteHidrostatico; }
        //        set { _TesteHidrostatico = value; }
        //    }

        //    public override string ToString()
        //    {
        //        if (this.mirrorOld == null)
        //            this.Find();

        //        return _Descricao;
        //    }

        //}
        //#endregion

        //#region CategoriaClasse

        //[Database("opsa", "CategoriaClasse", "IdCategoriaClasse")]
        //public class CategoriaClasse : Ilitera.Data.Table
        //{
        //    public CategoriaClasse()
        //    {

        //    }
        //    private int _IdCategoriaClasse;

        //    public override int Id
        //    {
        //        get { return _IdCategoriaClasse; }
        //        set { _IdCategoriaClasse = value; }
        //    }

        //    private CategoriaLaudoEletrico _IdCategoriaLaudoEletrico;

        //    public CategoriaLaudoEletrico IdCategoriaLaudoEletrico
        //    {
        //        get { return _IdCategoriaLaudoEletrico; }
        //        set { _IdCategoriaLaudoEletrico = value; }
        //    }

        //    private ClasseDeFluido _IdClasseDeFluido;

        //    public ClasseDeFluido IdClasseDeFluido
        //    {
        //        get { return _IdClasseDeFluido; }
        //        set { _IdClasseDeFluido = value; }
        //    }
        //    private GrupoRisco _IdGrupoRisco;

        //    public GrupoRisco IdGrupoRisco
        //    {
        //        get { return _IdGrupoRisco; }
        //        set { _IdGrupoRisco = value; }
        //    }
        //}
        //#endregion

    
}
