using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using DAL;

namespace BLL
{
    public class EmpregadoBLL
    {
        public List<Empregado> RetornarEmpregados(int idEmpresa, int idJuridicaPai, int idPrestador, Int32 IdSetor, string xAtivo_Classificacao)
        {
            EmpregadoDAL empregadoDal = new EmpregadoDAL();
            return empregadoDal.RetornarEmpregados(idEmpresa, idJuridicaPai, idPrestador, IdSetor, xAtivo_Classificacao );
        }

        public Empregado RetornarEmpregadoDadosCadastrais(int idEmpregado)
        {
            EmpregadoDAL empregadoDal = new EmpregadoDAL();
            return empregadoDal.RetornarEmpregadoDadosCadastrais(idEmpregado);
        }

        public List<ClassificacaoFuncionalDTO> RetornarClassificacaoFuncionalEmpregado(int idEmpregado)
        {
            EmpregadoDAL empregadoDal = new EmpregadoDAL();
            return empregadoDal.RetornarClassificacaoFuncionalEmpregado(idEmpregado);
        }
    }
}
