using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using DAL;

namespace BLL
{
    public class EmpresaBLL
    {
        public List<EmpresaDTO> RetornarEmpresas()
        {
            EmpresaDAL empresaDal = new EmpresaDAL();

            return empresaDal.RetornarEmpresas();
        }

        public List<EmpresaDTO> RetornarEmpresas(int idPrestador, int idJuridicaPapel, string nomeFuncionario, string xAtivo, string zOrdem)
        {
            EmpresaDAL empresaDal = new EmpresaDAL();

            return empresaDal.RetornarEmpresas(idPrestador, idJuridicaPapel, nomeFuncionario, xAtivo, zOrdem);
        }


        public List<EmpresaDTO> RetornarEmpresas_Nome(int idPrestador, int idJuridicaPapel, string nomeFuncionario, string Colaborador)
        {
            EmpresaDAL empresaDal = new EmpresaDAL();

            return empresaDal.RetornarEmpresas_Nome(idPrestador, idJuridicaPapel, nomeFuncionario, Colaborador);
        }

        public System.Data.IDataReader RetornarEmpresas_Nome2(int idPrestador, int idJuridicaPapel, string nomeFuncionario, string Colaborador, string zStatus)
        {
            EmpresaDAL empresaDal = new EmpresaDAL();

            return empresaDal.RetornarEmpresas_Nome2(idPrestador, idJuridicaPapel, nomeFuncionario, Colaborador, zStatus);
        }

        public List<EmpresaDTO> RetornarEmpresas_Matricula(int idPrestador, int idJuridicaPapel, string nomeFuncionario, string Matricula)
        {
            EmpresaDAL empresaDal = new EmpresaDAL();

            return empresaDal.RetornarEmpresas_Matricula(idPrestador, idJuridicaPapel, nomeFuncionario, Matricula);
        }

        public System.Data.IDataReader RetornarEmpresas_Matricula2(int idPrestador, int idJuridicaPapel, string nomeFuncionario, string Matricula, string zStatus)
        {
            EmpresaDAL empresaDal = new EmpresaDAL();

            return empresaDal.RetornarEmpresas_Matricula2(idPrestador, idJuridicaPapel, nomeFuncionario, Matricula, zStatus);
        }

        public EmpresaDTO RetornarDadosEmpresa(int idPessoa)
        {
            EmpresaDAL empresaDal = new EmpresaDAL();

            return empresaDal.RetornarDadosEmpresa(idPessoa);
        }

        public string RetornarDadosPreposto(int idEmpresa)
        {
            EmpresaDAL empresaDal = new EmpresaDAL();

            return empresaDal.RetornarDadosPreposto(idEmpresa);
        }

        public string RetornarDadosPreposto2(int idEmpresa)
        {
            EmpresaDAL empresaDal = new EmpresaDAL();

            return empresaDal.RetornarDadosPreposto2(idEmpresa);
        }

    }
}
