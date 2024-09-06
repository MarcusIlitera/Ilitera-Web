using System;
namespace Ilitera.Opsa.Data
{
    public interface ICalculoVasoCaldeira
    {
        double DiametroExterno { get; set; }
        double MedidaCorpo { get; set; }
        double MedidaTampoInferior { get; set; }
        double MedidaTampoSuperior { get; set; }
        double MenorEspessuraCorpo { get; set; }
        double MenorEspessuraTampo { get; set; }
        double MenorEspessura { get; set; }
        double PressaoMaximaTrabalho { get; set; }
        double VolumeInterno { get; set; }
        double PressaoTesteHidrostatico { get; set; }
        double RelacaoPV { get; set; }
    }
}
