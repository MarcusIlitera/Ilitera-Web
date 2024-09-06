using System;
using System.Data;
using System.Collections;
using Ilitera.Data;
using Ilitera.Common;

namespace Ilitera.Opsa.Data
{

    public enum AudiometriaTipo : int
    {
        Admissional,
        Semestral,
        Periodico,
        MudancaFuncao,
        RetornoTrabalho,
        Demissional
    }

    enum Interpretacao : int
    {
        Aceitavel = 1,
        Sugestivo,
        NaoSugestivo,
        Desencadeamento,
        Agravamento
    }

    [Database("opsa", "Audiometria", "IdAudiometria", "", "Exame Audiométrico")]
    public class Audiometria : Ilitera.Opsa.Data.ExameBase
    {
        private int _IdAudiometria;
        private DateTime _DataUltimaAfericao = DateTime.Today;
        private int _IndAudiometriaTipo;
        private Audiometro _IdAudiometro;
        private float _TempoRepouso = 14F;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Audiometria()
        {

        }

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        public Audiometria(int Id)
        {
            this.Find(Id);
        }
        public override int Id
        {
            get { return _IdAudiometria; }
            set { _IdAudiometria = value; }
        }
        public int IndAudiometriaTipo
        {
            get { return _IndAudiometriaTipo; }
            set { _IndAudiometriaTipo = value; }
        }
        public Audiometro IdAudiometro
        {
            get { return _IdAudiometro; }
            set { _IdAudiometro = value; }
        }
        public DateTime DataUltimaAfericao
        {
            get { return _DataUltimaAfericao; }
            set { _DataUltimaAfericao = value; }
        }
        public float TempoRepouso
        {
            get { return _TempoRepouso; }
            set
            {
                if (value == 0)
                    _TempoRepouso = value;
                else if (value >= 14 && value <= 24)
                    _TempoRepouso = value;
                else if (value > 24)
                    throw new Exception("Valor indevido!");
                else
                    throw new Exception("O período mínimo de repouso é de 14 horas!");
            }
        }

        #region Metodos

        public static string GetDiretorioPadrao(Cliente cliente)
        {
            string ret = string.Empty;

            ret = System.IO.Path.Combine(Fotos.GetRaizPath(), cliente.GetFotoDiretorioPadrao() + @"\Audiograma\");

            return ret;
        }

        public AudiometriaAudiograma GetAudiograma(Orelha orelha)
        {
            AudiometriaAudiograma audiograma = new AudiometriaAudiograma();
            audiograma.Find("IdAudiometria=" + this.Id + " AND IndOrelha=" + (int)orelha);

            if (audiograma.Id == 0)
            {
                audiograma.Inicialize();
                audiograma.IdAudiometria.Id = this.Id;
                audiograma.IndOrelha = (int)orelha;
            }

            return audiograma;
        }

        public string GetResultadoExameOrelhaDireita()
        {
            return this.GetAudiograma(Orelha.Direita).GetResultadoExame();
        }

        public string GetResultadoExameOrelhaEsquerda()
        {
            return this.GetAudiograma(Orelha.Esquerda).GetResultadoExame();
        }

        public string GetAudiometriaTipo()
        {
            string ret = string.Empty;

            if (this.IndAudiometriaTipo == (int)AudiometriaTipo.Admissional)
                ret = "Admissional";
            else if (this.IndAudiometriaTipo == (int)AudiometriaTipo.Demissional)
                ret = "Demissional";
            else if (this.IndAudiometriaTipo == (int)AudiometriaTipo.MudancaFuncao)
                ret = "Mudança de Função";
            else if (this.IndAudiometriaTipo == (int)AudiometriaTipo.Periodico)
                ret = "Periódico";
            else if (this.IndAudiometriaTipo == (int)AudiometriaTipo.RetornoTrabalho)
                ret = "Retorno ao Trabalho";
            else if (this.IndAudiometriaTipo == (int)AudiometriaTipo.Semestral)
                ret = "Semestral";

            return ret;
        }


        public override int Save()
        {
            this.IdExameDicionario.Id = (int)Exames.Audiometria;

            return base.Save();
        }

        public static void SalvarAudiometria(Audiometria audiometria,
                                            AudiometriaAudiograma audiogramaOD,
                                            AudiometriaAudiograma audiogramaOE,
                                            AnamneseAudiologica anamnese)
        {
            if (audiogramaOD.IsAudiogramaCamposObrigatorios())
                throw new Exception("Campos obrigatórios não cadastratos na orelha direita!");

            if (audiogramaOE.IsAudiogramaCamposObrigatorios())
                throw new Exception("Campos obrigatórios não cadastratos na orelha esquerda!");

            if (!audiogramaOD.IsValidarEntradas())
                throw new Exception("Valores incorretos no cadastro dos dados na orelha direita!");

            if (!audiogramaOE.IsValidarEntradas())
                throw new Exception("Valores incorretos no cadastro dos dados na orelha esquerda!");

            if (!(audiogramaOD.IsAudiogramaEmBranco() && audiogramaOE.IsAudiogramaEmBranco()))
            {
                audiogramaOD.AtualizarDadosPPP();
                audiogramaOE.AtualizarDadosPPP();

                if (audiometria.IndResultado == (int)ResultadoExame.NaoRealizado)
                {
                    if (audiogramaOD.IsAnormal || audiogramaOE.IsAnormal)
                        audiometria.IndResultado = (int)ResultadoExame.Alterado;
                    else
                        audiometria.IndResultado = (int)ResultadoExame.Normal;
                }
            }

            IDbTransaction trans = audiometria.GetTransaction();

            try
            {
                int Id = audiometria.Save();

                if (audiogramaOD != null)
                {
                    audiogramaOD.IdAudiometria.Id = Id;
                    if ( audiogramaOD.Id == 0 ) audiogramaOD.IndOrelha = 0;
                    audiogramaOD.Transaction = trans;
                    audiogramaOD.Save();
                }

                if (audiogramaOE != null)
                {
                    audiogramaOE.IdAudiometria.Id = Id;
                    if ( audiogramaOE.Id == 0 )   audiogramaOE.IndOrelha = 1;
                    audiogramaOE.Transaction = trans;
                    audiogramaOE.Save();
                }

                if (anamnese != null)
                {
                    anamnese.IdAudiometria.Id = Id;
                    anamnese.Transaction = trans;
                    anamnese.Save();
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();

                throw ex;
            }
        }

        #endregion

    }
}
