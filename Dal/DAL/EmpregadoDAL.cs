using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Configuration;
using System.Data.Common;

namespace DAL
{
    public class EmpregadoDAL
    {
        public List<Empregado> RetornarEmpregados(int idEmpresa, int idJuridicaPai, int IdPrestador, Int32 IdSetor, string xAtivo_Classificacao)
        {
            List<Empregado> lstEmpregado = new List<Empregado>();

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraSiedNovo"].ConnectionString);

            //StringBuilder sbEmpregado = new StringBuilder();
            //sbEmpregado.Append("select distinct e.nID_Empregado, e.tNO_Empg, e.tNO_Apelido, e.hDT_ADM, e.hDT_DEM, e.tNO_CPF, e.tNO_Identidade, ");
            //sbEmpregado.Append("ef.nID_EMPR, e.hDT_NASC ");
            //sbEmpregado.Append("from tblEmpregado e ");
            //sbEmpregado.Append("left join tblEmpregado_Funcao ef on e.nID_Empregado = ef.nID_Empregado ");


            StringBuilder sbEmpregado = new StringBuilder();
            sbEmpregado.Append("select distinct e.nID_Empregado, e.tNO_Empg, e.tNO_Apelido, e.hDT_ADM, e.hDT_DEM, e.tNO_CPF, e.tNO_Identidade, ");
            sbEmpregado.Append("ef.nID_EMPR, e.hDT_NASC, ef.hDt_Inicio, d.tno_str_empr as Setor, g.NomeFuncao as Funcao, e.tcod_Empr as matricula ");
            sbEmpregado.Append("from tblEmpregado e ");
            sbEmpregado.Append("left join tblEmpregado_Funcao ef on e.nID_Empregado = ef.nID_Empregado ");
            sbEmpregado.Append("left join tblSetor as d on ef.nid_Setor = d.nId_Setor ");
            sbEmpregado.Append("left join " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Funcao as g on ef.nid_Funcao = g.IdFuncao ");

            //if (idJuridicaPai > 0)
            //{
            //  sbEmpregado.AppendFormat("where e.nID_EMPR = {0} ", idJuridicaPai);
            //}
            //else
            //{
            //sbEmpregado.AppendFormat("where e.nID_EMPR = {0} ", idEmpresa);
            ////}

            //sbEmpregado.AppendFormat("and ( ef.nID_EMPR = {0}  or ef.nID_EMPR is null ) ", idEmpresa);
            sbEmpregado.AppendFormat("where ef.nID_EMPR = {0} ", idEmpresa);

            
            

            if (IdSetor != 0)
            {
                sbEmpregado.AppendFormat(" and ( ef.nid_setor = "  + IdSetor.ToString() +  " ) ");
            }

            if (xAtivo_Classificacao == "A")  //classif.funcional ativa
            {
                sbEmpregado.AppendFormat(" and ef.hdt_termino is null  ");
            }
            else if (xAtivo_Classificacao == "I")  //classif.funcional inativa
            {
                sbEmpregado.AppendFormat(" and ef.hdt_termino is not null  ");
            }
            



            //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
            //{
            //    sbEmpregado.AppendFormat("and  ef.nId_Empregado in  ");
            //    sbEmpregado.AppendFormat("(select nId_Empregado ");
            //    sbEmpregado.AppendFormat("from tblEmpregado_Funcao ");
            //    sbEmpregado.AppendFormat("where ef.nID_EMPR = {0} ", idEmpresa);
            //    sbEmpregado.AppendFormat("group by nId_Empregado ");
            //    sbEmpregado.AppendFormat(") ");
            //}
            //else
            //{
            sbEmpregado.AppendFormat(" and convert(char(15), ef.nId_Empregado) +' ' + convert(char(10), ef.hdt_Inicio, 103) in ");
                sbEmpregado.AppendFormat("(select convert(char(15), nId_Empregado) + ' ' + convert(char(10), max(hdt_Inicio), 103) ");
                sbEmpregado.AppendFormat("from tblEmpregado_Funcao ");
            //sbEmpregado.AppendFormat("where ef.nID_EMPR = {0} ", idEmpresa);
            sbEmpregado.AppendFormat("where nID_EMPR = {0} ", idEmpresa);
            sbEmpregado.AppendFormat("group by nId_Empregado ");
                sbEmpregado.AppendFormat(") ");
           // }

            sbEmpregado.Append("order by tNO_Empg");



            var objCmd = objDB.GetSqlStringCommand(sbEmpregado.ToString());
            
            //var objCmd = objDB.GetStoredProcCommand("ili_sp_RetornarEmpregados"); //.GetSqlStringCommand(sbEmpregado.ToString());

            //objDB.AddInParameter(objCmd, "IdEmpresa", System.Data.DbType.Int32,  idEmpresa);
            //objDB.AddInParameter(objCmd, "IdPrestador", System.Data.DbType.Int32, IdPrestador );
            objCmd.CommandTimeout = 900;
            var readerRetorno = objDB.ExecuteReader(objCmd);

            while (readerRetorno.Read())
            {
                if (readerRetorno["tNO_Empg"].ToString().Length > 1)
                {
                    Empregado empregado = new Empregado();
                    empregado.IdEmpregado = Convert.ToInt32(readerRetorno["nID_Empregado"]);
                    empregado.NomeEmpregado = readerRetorno["tNO_Empg"].ToString();
                    empregado.ApelidoEmpregado = readerRetorno["tNO_Apelido"].ToString();

                    if(readerRetorno["hDT_ADM"] != null && readerRetorno["hDT_ADM"].ToString() != String.Empty)
                        empregado.DataAdmissao = Convert.ToDateTime(readerRetorno["hDT_ADM"]);

                    if(readerRetorno["hDT_DEM"] != null && readerRetorno["hDT_DEM"].ToString() != String.Empty)
                        empregado.DataDemissao = Convert.ToDateTime(readerRetorno["hDT_DEM"]);
                    
                    if (readerRetorno["hDT_NASC"] != null && readerRetorno["hDT_NASC"].ToString() != String.Empty)
                        empregado.DataNascimento = Convert.ToDateTime(readerRetorno["hDT_NASC"]);

                    if (readerRetorno["hDt_Inicio"] != null && readerRetorno["hDt_Inicio"].ToString() != String.Empty)
                        empregado.InicioFuncao = Convert.ToDateTime(readerRetorno["hdt_Inicio"]);

                    empregado.Setor = readerRetorno["Setor"].ToString();
                    empregado.Funcao = readerRetorno["Funcao"].ToString();

                    empregado.matricula = readerRetorno["matricula"].ToString();

                    empregado.Cpf = readerRetorno["tNO_CPF"].ToString();
                    empregado.Identidade = readerRetorno["tNO_Identidade"].ToString();

                    lstEmpregado.Add(empregado);
                }
            }

            return lstEmpregado;
        }

        public Empregado RetornarEmpregadoDadosCadastrais(int idEmpregado)
        {
            Empregado empregado = new Empregado();

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraSiedNovo"].ConnectionString);

            var objCmd = objDB.GetStoredProcCommand("ili_sp_EmpregadoDadosCadastrais");
            objCmd.CommandTimeout = 900;

            objDB.AddInParameter(objCmd, "IdEmpregado", System.Data.DbType.Int32, idEmpregado);

            var readerRetorno = objDB.ExecuteReader(objCmd);

            
            while (readerRetorno.Read())
            {
                
                empregado.IdEmpregado = Convert.ToInt32(readerRetorno["idEmpregado"]);

                if(readerRetorno["idEmpresa"] != null && readerRetorno["idEmpresa"].ToString() != String.Empty)
                    empregado.IdEmpresa = Convert.ToInt32(readerRetorno["idEmpresa"]);

                empregado.NomeEmpregado = readerRetorno["nome"].ToString();
                empregado.ApelidoEmpregado = readerRetorno["apelido"].ToString();
                empregado.pesoEmpregado = readerRetorno["peso"].ToString();
                empregado.AlturaEmpregado = readerRetorno["altura"].ToString();
                empregado.Sexoempregado = readerRetorno["sexo"].ToString();
                empregado.teMail = readerRetorno["teMail"].ToString();
                empregado.tObs = readerRetorno["tObs"].ToString();

                if (readerRetorno["gTerceiro"].ToString().ToUpper() == "TRUE") empregado.gTerceiro = true;
                else empregado.gTerceiro = false;

                if (readerRetorno["dataNascimento"] != null && readerRetorno["dataNascimento"].ToString() != String.Empty)
                    empregado.DataNascimento = Convert.ToDateTime(readerRetorno["dataNascimento"]);

                empregado.matricula = readerRetorno["matricula"].ToString();
                empregado.numCTPS = readerRetorno["numCTPS"].ToString();
                empregado.serieCTPS = readerRetorno["serieCTPS"].ToString();
                empregado.ufCTPS = readerRetorno["ufCTPS"].ToString();

                if(readerRetorno["dataAdmissao"] != null && readerRetorno["dataAdmissao"].ToString() != String.Empty)
                    empregado.DataAdmissao = Convert.ToDateTime(readerRetorno["dataAdmissao"]);
                
                if(readerRetorno["dataDemissao"] != null && readerRetorno["dataDemissao"].ToString() != String.Empty)
                    empregado.DataDemissao = Convert.ToDateTime(readerRetorno["dataDemissao"]);

                empregado.endLogradouro = readerRetorno["endLogradouro"].ToString();
                empregado.endNumero = readerRetorno["endNumero"].ToString();
                empregado.endComplemento = readerRetorno["endComplemento"].ToString();
                empregado.endBairro = readerRetorno["endBairro"].ToString();
                empregado.endMunicipio = readerRetorno["endMunicipio"].ToString();
                empregado.endUF = readerRetorno["endUF"].ToString();
                empregado.endCEP = readerRetorno["endCEP"].ToString();
                empregado.telefone = readerRetorno["telefone"].ToString();
                empregado.Cpf = readerRetorno["CPF"].ToString();
                empregado.Identidade = readerRetorno["RG"].ToString();
                empregado.PISPASEP = readerRetorno["PISPASEP"].ToString();
                empregado.nNOFOTO = readerRetorno["nNO_FOTO"].ToString();
                
                if (readerRetorno["nInd_Beneficiario"] == null)
                    empregado.nInd_Beneficiario = "0";
                else if (readerRetorno["nInd_Beneficiario"].ToString().Trim() == "")
                    empregado.nInd_Beneficiario = "0";
                else
                    empregado.nInd_Beneficiario = readerRetorno["nInd_Beneficiario"].ToString().Trim();

                               
                
            }
            return empregado;
        }

        public List<ClassificacaoFuncionalDTO> RetornarClassificacaoFuncionalEmpregado(int idEmpregado)
        {
            List<ClassificacaoFuncionalDTO> lstEmpregado = new List<ClassificacaoFuncionalDTO>();

            Database objDB = new SqlDatabase(ConfigurationManager.ConnectionStrings["ConexaoIliteraSiedNovo"].ConnectionString);

            var objCmd = objDB.GetStoredProcCommand("ili_sp_EmpregadoClassificacaoFuncional");
            objCmd.CommandTimeout = 900;

            objDB.AddInParameter(objCmd, "IdEmpregado", System.Data.DbType.Int32, idEmpregado);

            var readerRetorno = objDB.ExecuteReader(objCmd);

            while (readerRetorno.Read())
            {
                ClassificacaoFuncionalDTO cFuncional = new ClassificacaoFuncionalDTO();

                cFuncional.IdClassificacaoFuncional = Convert.ToInt32(readerRetorno["idEmpregadoFuncao"]);
                cFuncional.IdEmpregado = Convert.ToInt32(readerRetorno["idEmpregado"]);
                cFuncional.Funcao = readerRetorno["funcao"].ToString();
                cFuncional.Setor   = readerRetorno["setor"].ToString();

                if(readerRetorno["inicioFuncao"] != null && readerRetorno["inicioFuncao"].ToString() != String.Empty)
                    cFuncional.InicioFuncao = Convert.ToDateTime(readerRetorno["inicioFuncao"]);

                if(readerRetorno["terminoFuncao"] != null && readerRetorno["terminoFuncao"].ToString() != String.Empty)
                    cFuncional.TerminoFuncao = Convert.ToDateTime(readerRetorno["terminoFuncao"]);

                cFuncional.Cbo = readerRetorno["cbo"].ToString();
                cFuncional.DescricaoFuncao = readerRetorno["descricaoFuncao"].ToString();
                cFuncional.NomeAlocado = readerRetorno["idLocalTrabalho"].ToString();
                cFuncional.atual = readerRetorno["localTrabalho"].ToString();
                cFuncional.tCentro_Custo = readerRetorno["tCentro_Custo"].ToString();

                if (readerRetorno["GFIP"] != null && readerRetorno["GFIP"].ToString() != String.Empty)
                    cFuncional.CodGFIP = Convert.ToInt32(readerRetorno["GFIP"]);

                cFuncional.DescricaoCargo = readerRetorno["descricaoCargo"].ToString();

                lstEmpregado.Add(cFuncional);
            }
            return lstEmpregado;
        }
    }
}
