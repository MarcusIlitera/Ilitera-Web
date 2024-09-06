using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using BLL;

namespace Facade
{
    public class EmpresaFacade
    {
        public static List<EmpresaDTO> RetornarEmpresas()
        {
            EmpresaBLL empresaBll = new EmpresaBLL();

            return empresaBll.RetornarEmpresas();
        }

        public static List<EmpresaDTO> RetornarEmpresas(int idPrestador, int idJuridicaPapel, string nomeFuncionario, string xAtivo, string Ordem)
        {
            EmpresaBLL empresaBll = new EmpresaBLL();

            return empresaBll.RetornarEmpresas(idPrestador, idJuridicaPapel, nomeFuncionario, xAtivo, Ordem);
        }

        public static List<EmpresaDTO> RetornarEmpresas_Nome(int idPrestador, int idJuridicaPapel, string nomeFuncionario, string Colaborador)
        {
            EmpresaBLL empresaBll = new EmpresaBLL();

            return empresaBll.RetornarEmpresas_Nome(idPrestador, idJuridicaPapel, nomeFuncionario, Colaborador);
        }

        public static System.Data.IDataReader RetornarEmpresas_Nome2(int idPrestador, int idJuridicaPapel, string nomeFuncionario, string Colaborador, string zStatus)
        {
            EmpresaBLL empresaBll = new EmpresaBLL();

            return empresaBll.RetornarEmpresas_Nome2(idPrestador, idJuridicaPapel, nomeFuncionario, Colaborador, zStatus);
        }

        public static List<EmpresaDTO> RetornarEmpresas_Matricula(int idPrestador, int idJuridicaPapel, string nomeFuncionario, string Matricula)
        {
            EmpresaBLL empresaBll = new EmpresaBLL();

            return empresaBll.RetornarEmpresas_Matricula(idPrestador, idJuridicaPapel, nomeFuncionario, Matricula);
        }

        public static System.Data.IDataReader RetornarEmpresas_Matricula2(int idPrestador, int idJuridicaPapel, string nomeFuncionario, string Matricula, string zStatus)
        {
            EmpresaBLL empresaBll = new EmpresaBLL();

            return empresaBll.RetornarEmpresas_Matricula2(idPrestador, idJuridicaPapel, nomeFuncionario, Matricula, zStatus);
        }

        public static EmpresaDTO RetornarDadosEmpresa(int idPessoa)
        {
            EmpresaBLL empresaBll = new EmpresaBLL();

            return empresaBll.RetornarDadosEmpresa(idPessoa);
        }

        public static string RetornarDadosPreposto(int idEmpresa)
        {
            EmpresaBLL empresaBll = new EmpresaBLL();

            return empresaBll.RetornarDadosPreposto(idEmpresa);
        }

        public static string RetornarDadosPreposto2(int idEmpresa)
        {
            EmpresaBLL empresaBll = new EmpresaBLL();

            return empresaBll.RetornarDadosPreposto2(idEmpresa);
        }

    }
}