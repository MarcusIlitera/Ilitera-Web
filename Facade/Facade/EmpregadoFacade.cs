using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using BLL;

namespace Facade
{
    public class EmpregadoFacade
    {
        public static List<Empregado> RetornarEmpregados(int idEmpresa, int idJuridicaPai, int idPrestador, Int32 IdSetor, string xAtivo_Classificacao)
        {
            EmpregadoBLL empregadoBll = new EmpregadoBLL();
            return empregadoBll.RetornarEmpregados(idEmpresa, idJuridicaPai, idPrestador, IdSetor, xAtivo_Classificacao);
        }

        public static Empregado RetornarEmpregadoDadosCadastrais(int idEmpregado)
        {
            EmpregadoBLL empregadoBll = new EmpregadoBLL();
            return empregadoBll.RetornarEmpregadoDadosCadastrais(idEmpregado);
        }

        public static List<ClassificacaoFuncionalDTO> RetornarClassificacaoFuncionalEmpregado(int idEmpregado)
        {
            EmpregadoBLL empregadoBLL = new EmpregadoBLL();
            return empregadoBLL.RetornarClassificacaoFuncionalEmpregado(idEmpregado);
        }
    }
}
