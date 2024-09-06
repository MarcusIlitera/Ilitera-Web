using System;
namespace Ilitera.Common
{
    public interface IPessoa
    {
        string NomeAbreviado { get; set; }
        string NomeCodigo { get; set; }
        string NomeCompleto { get; set; }
    }
}
