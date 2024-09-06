using System;
namespace Ilitera.Common
{
    public interface IEndereco
    {
        string Bairro { get; set; }
        string Cep { get; set; }
        string Complemento { get; set; }
        Municipio IdMunicipio { get; set; }
        TipoLogradouro IdTipoLogradouro { get; set; }
        string Logradouro { get; set; }
        string Numero { get; set; }
    }
}
