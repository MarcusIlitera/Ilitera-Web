using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Entities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Configuration;
using Ilitera.Data.SQLServer;
using System.Data;
using System.Data.SqlClient;



namespace Ilitera.Data
{
    public class Comunicacao
    {




        public void Criar_Empresa( int xIdEmpresa, string xIE, string xCCM,  string xObservacao, int xCNAE, int xGrupoEmpresa, int xIdJuridicaPapel, string xNomeEmpresa )
        {

            try
            {

                using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
                {
                    cnn.Open();


                    ////                    USE Opsa_Prajna_Hom
                    ////INSERT INTO Pessoa(IdPessoa, NomeCodigo, NomeAbreviado, NomeCompleto, IndTipoPessoa, Email, Site, DataCadastro, Foto, IsInativo) VALUES(1819716480, '17.855.616/0001-47', 'Empresa Exemplo', 'Empresa Exemplo SA', 0, 'contato@empresaexemplo.com.br', 'www.empresaexemplo.com.br', '2019-02-12 07:56:20', '', 0)


                    ////USE Opsa_Prajna_Hom
                    ////INSERT INTO Juridica(IdJuridica, IdPessoa, IdJuridicaPapel, Atividade, IE, CCM, DataFundacao, QtdEmpregados, IdGrupoEmpresa, IdJuridicaPai, Observacao, Diretor, IdTransporte, IdCNAE, IsOptanteSimples, AtivarLocalDeTrabalho, Logotipo, CodigoLojaObra, DiretorioPadrao, IsPesonalizarCarimboCnpj, CarimboCnpjHtml, IsPersonalizarDadosEmpresa, DadosEmpresaHtml) VALUES(1819716480, 1819716480, 1, '', '', '', NULL, 0, 81440016, NULL, '', '', NULL, 703068917, 0, 0, '', '', 'Empresa Exemplo', 0, '', 0, '')

                    ////USE Opsa_Prajna_Hom
                    ////INSERT INTO Cliente(IdCliente, IdJuridica, CodigoAntigo, IdOrigem, OrigemOutros, IdSindicato, IdDRT, DataRegistroDRT, NumeroRegistroDRT, LocalAtividades, HoraAtividades, DiaAtividades, QtdAutoclaves, QtdCaldeiras, QtdVasoPressao, QtdEmpilhadeiras, QtdPontes, QtdPrensas, QtdTanques, QtdPericiaContratadaAno, ValorPericiaRealizadaParte, ContrataCipa, ContrataPCMSO, ContrataOS, PosseCipa, RecargaExtintor, HasObraCivil, IsMineradora, ArqFotoEmpregInicio, ArqFotoEmpregTermino, ArqFotoEmpregExtensao, ArqFotoEmrpegQteDigitos, IdRespEleicaoCIPA, IdRespPCMSO, IdRespPPP, IdRespPPP2, Turnover, HasExameComplementar, DataUltimoPeriodico, DataRetiradaProntuario, ObservacaoPcmso, IndRealizacaoPeriodico, IndAtualizacaoPeriodico, Turnos, Permitir_Colaboradores, Exibir_Riscos_ASO, Exibir_Datas_Exames_ASO, Bloquear_Novo_Setor, Permitir_Edicao_Nome, Bloquear_Data_Demissao, CNPJ_Matriz, Endereco_Matriz, Bloquear_ASO_Planejamento, Municipio_Data_ASO, Carga_Horaria, Logo_Laudos, InibirGHE, GHEAnterior_MudancaFuncao, Microbiologia, Riscos_PPRA, Ativar_DesconsiderarCompl, Dias_Desconsiderar, FatorRH, Gerar_Complementares_Guia, PPP_Checar_Entrega_EPI, Mail_Alerta_Absenteismo, PPP_CNPJ_Nome_Matriz) VALUES(1819716480, 1819716480, 0, NULL, '', NULL, NULL, '2019-02-12 00:00:00', '', 'Sala de Reuniões', '09:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, '2019-02-12 00:00:00', 0, 0, 0, 'MVC-', 'S', '.JPG', 5, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '', 0, 0, '', 1, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0)

                    //string xSQL1 = "INSERT INTO Pessoa(IdPessoa, NomeCodigo, NomeAbreviado, NomeCompleto, IndTipoPessoa, Email, Site, DataCadastro, Foto, IsInativo) VALUES(1819716480, '17.855.616/0001-47', 'Empresa Exemplo', 'Empresa Exemplo SA', 0, 'contato@empresaexemplo.com.br', 'www.empresaexemplo.com.br', '2019-02-12 07:56:20', '', 0)";

                    //SqlCommand com = new SqlCommand(xSQL1, cnn);

                    //com.ExecuteNonQuery();

                    string rCNAE;

                    if (xCNAE == 0)
                        rCNAE = " NULL ";
                    else
                        rCNAE = xCNAE.ToString();



                    string xSQL2 = "INSERT INTO Juridica(IdJuridica, IdPessoa, IdJuridicaPapel, Atividade, IE, CCM, DataFundacao, QtdEmpregados, IdGrupoEmpresa, IdJuridicaPai, Observacao, Diretor, IdTransporte, IdCNAE, IsOptanteSimples, AtivarLocalDeTrabalho, Logotipo, CodigoLojaObra, DiretorioPadrao, IsPesonalizarCarimboCnpj, CarimboCnpjHtml, IsPersonalizarDadosEmpresa, DadosEmpresaHtml) VALUES(" + 
                                    xIdEmpresa.ToString() + ", " + xIdEmpresa.ToString() + ", " + xIdJuridicaPapel.ToString() + " , '', '" + xIE + "', '" + xCCM + "', NULL, 0, " + xGrupoEmpresa.ToString() +  ", NULL, '" + xObservacao + "', '', NULL, " + rCNAE +  " , " +  rCNAE +  " , 0, '', '', '" + xNomeEmpresa + "', 0, '', 0, '')";

                    SqlCommand com2 = new SqlCommand(xSQL2, cnn);

                    com2.ExecuteNonQuery();



                    string xSQL3 = "INSERT INTO Cliente(IdCliente, IdJuridica, CodigoAntigo, IdOrigem, OrigemOutros, IdSindicato, IdDRT, DataRegistroDRT, NumeroRegistroDRT, LocalAtividades, HoraAtividades, DiaAtividades, QtdAutoclaves, QtdCaldeiras, QtdVasoPressao, QtdEmpilhadeiras, QtdPontes, QtdPrensas, QtdTanques, QtdPericiaContratadaAno, ValorPericiaRealizadaParte, ContrataCipa, ContrataPCMSO, ContrataOS, PosseCipa, RecargaExtintor, HasObraCivil, IsMineradora, ArqFotoEmpregInicio, ArqFotoEmpregTermino, ArqFotoEmpregExtensao, ArqFotoEmrpegQteDigitos, IdRespEleicaoCIPA, IdRespPCMSO, IdRespPPP, IdRespPPP2, Turnover, HasExameComplementar, DataUltimoPeriodico, DataRetiradaProntuario, ObservacaoPcmso, IndRealizacaoPeriodico, IndAtualizacaoPeriodico, Turnos, Permitir_Colaboradores, Exibir_Riscos_ASO, Exibir_Datas_Exames_ASO, Bloquear_Novo_Setor, Permitir_Edicao_Nome, Bloquear_Data_Demissao, CNPJ_Matriz, Endereco_Matriz, Bloquear_ASO_Planejamento, Municipio_Data_ASO, Carga_Horaria, Logo_Laudos, InibirGHE, GHEAnterior_MudancaFuncao, Microbiologia, Riscos_PPRA, Ativar_DesconsiderarCompl, Dias_Desconsiderar, FatorRH, Gerar_Complementares_Guia, PPP_Checar_Entrega_EPI, Mail_Alerta_Absenteismo, PPP_CNPJ_Nome_Matriz) VALUES( " +
                                   xIdEmpresa.ToString() + ", " + xIdEmpresa.ToString() + ", 0, NULL, '', NULL, NULL, getdate(), '', 'Sala de Reuniões', '09:00', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, getdate(), 0, 0, 0, 'MVC-', 'S', '.JPG', 5, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '', 0, 0, '', 1, 0, 0, 0, 0, 0, 0, 0, 0, '', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '', 0)";

                    SqlCommand com3 = new SqlCommand(xSQL3, cnn);

                    com3.ExecuteNonQuery();



                }
            }
            catch //( Exception Ex)
            {

            }
            finally
            {
                
            }

            return;        

        }




        public void Alterar_Empresa(int xIdEmpresa, string xIE, string xCCM, string xObservacao, int xCNAE)
        {

            try
            {

                using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
                {
                    cnn.Open();

                    string xSQL2 = "";
                                        
                    xSQL2 = "Update Juridica set ";

                    if (xIE != "")
                        xSQL2 = xSQL2 + " IE = '" + xIE + "', ";

                    if ( xCCM != "" )
                        xSQL2 = xSQL2 + " CCM '" + xCCM + "', ";

                    if ( xObservacao != "" )
                        xSQL2 = xSQL2 + " Observacao = '" + xObservacao + "', ";

                    if ( xCNAE > 0)
                        xSQL2 = xSQL2 + " IdCNAE = " + xCNAE.ToString() + " , ";

                    xSQL2 = xSQL2.Substring(0, xSQL2.Length - 2);

                    xSQL2 = xSQL2 + " where IdJuridica = " + xIdEmpresa.ToString();

                    
                    SqlCommand com2 = new SqlCommand(xSQL2, cnn);

                    com2.ExecuteNonQuery();
                    

                }
            }
            catch //(Exception Ex)
            {

            }
            finally
            {

            }

            return;

        }


        public Int32 Trazer_Clinica_CEP(string xCEP)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdClinica", Type.GetType("System.Int32"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select top 1 b.IdPessoa as IdClinica from ");
            strSQL.Append(" ( ");
            strSQL.Append("  select idpessoa, idendereco, convert(int, substring(cep, 1, 5)) as CEP, abs(convert(int, substring(cep, 1, 5)) - "  + xCEP.Substring(0,5) + " ) as CEP2 ");
            strSQL.Append("  from endereco ");
            strSQL.Append("  where isnumeric(substring(cep, 1, 5)) = 1  ");
            strSQL.Append("  and convert(int, substring(cep, 1, 5)) > 999  ");
            strSQL.Append("  and idpessoa in (  ");
            strSQL.Append("     select idclinica from clinica  ");
            strSQL.Append("   )  ");
            strSQL.Append(" and idpessoa in (  ");
            strSQL.Append("    select idpessoa from pessoa where isinativo = 0  ");
            strSQL.Append("   )  ");
            strSQL.Append(" )  ");
            strSQL.Append(" as tx90  ");
            strSQL.Append(" left join pessoa as b on(tx90.idpessoa = b.idpessoa)  ");
            strSQL.Append(" order by CEP2  ");


                       

            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            if (m_ds.Tables[0].Rows.Count == 0)
                return 0;
            else
                return System.Convert.ToInt32( m_ds.Tables[0].Rows[0][0] );

        }



        public DataSet Trazer_Clinicas_CEP(string xCEP, string xNumero_Clinicas, string xValor_Maximo)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdClinica", Type.GetType("System.Int32"));
            table.Columns.Add("Clinica", Type.GetType("System.String"));
            table.Columns.Add("Endereco", Type.GetType("System.String"));
            table.Columns.Add("Contato", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select top " + xNumero_Clinicas + " b.IdPessoa as IdClinica, b.NomeAbreviado as Clinica, ");
            strSQL.Append("         max(  case when e.NomeAbreviado is null or e.NomeAbreviado='' then '' else e.NomeAbreviado  end  + ' ' + case when c.Logradouro is null or c.Logradouro='' then '' else c.Logradouro  end + ' ' + c.numero + ' ' + case when c.Complemento is null or c.Complemento='' then '' else c.Complemento  end + ' ' + c.Municipio + ' / ' + c.Uf + '  CEP: ' + c.Cep) as Endereco,  ");
            strSQL.Append("         max(  case when d.ddd is null or d.ddd='' then '' else ' ( ' + d.ddd + ' ) ' end + case when d.numero is null or d.numero='' then '' else d.numero end  +  case when d.nome is null or d.nome='' then '' else ' - ' + d.nome end  + case when d.eMail is null or d.email='' then '' else ' - ' + d.eMail end ) as Contato   ");
            strSQL.Append(" from ");
            strSQL.Append(" ( ");
            strSQL.Append("  select idpessoa, idendereco, convert(int, substring(cep, 1, 5)) as CEP, abs(convert(int, substring(cep, 1, 5)) - " + xCEP.Substring(0, 5) + " ) as CEP2 ");
            strSQL.Append("  from endereco ");
            strSQL.Append("  where isnumeric(substring(cep, 1, 5)) = 1  ");
            strSQL.Append("  and convert(int, substring(cep, 1, 5)) > 999  ");
            strSQL.Append("  and idpessoa in (  ");
            strSQL.Append("     select idclinica from clinica  ");
            strSQL.Append("   )  ");
            strSQL.Append(" and idpessoa in (  ");
            strSQL.Append("    select idpessoa from pessoa where isinativo = 0  ");
            strSQL.Append("   )  ");
            strSQL.Append(" )  ");
            strSQL.Append(" as tx90  ");
            strSQL.Append(" left join pessoa as b on(tx90.idpessoa = b.idpessoa)  ");
            strSQL.Append(" left join endereco as c on(tx90.idpessoa = c.idpessoa) ");
            strSQL.Append(" left join ContatoTelefonico as d on(tx90.idpessoa = d.idpessoa) ");
            strSQL.Append(" left join tipologradouro as e on(c.IdTipoLogradouro = e.IdTipoLogradouro) ");
            strSQL.Append(" where tx90.idpessoa in ");
            strSQL.Append("     ( ");
            strSQL.Append("       select idclinica from clinicaexamedicionario ");
            strSQL.Append("       where idexamedicionario between 1 and 5 ");
            strSQL.Append("       and valorpadrao <= " + xValor_Maximo +  " ");
            strSQL.Append("     ) ");
            strSQL.Append(" and b.NomeAbreviado not like '%atendimento in loco%' ");

            strSQL.Append(" group by b.idpessoa, b.NomeAbreviado, cep2 ");
            strSQL.Append(" order by CEP2  ");




            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }



        public DataSet Trazer_Clinicas_CEP(Int32 xIdPessoa, string xCEP, string xNumero_Clinicas, string xValor_Maximo)
        {

            StringBuilder strSQL = new StringBuilder();


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdClinica", Type.GetType("System.Int32"));
            table.Columns.Add("Clinica", Type.GetType("System.String"));
            table.Columns.Add("Endereco", Type.GetType("System.String"));
            table.Columns.Add("Contato", Type.GetType("System.String"));
            table.Columns.Add("HorarioAtendimento", Type.GetType("System.String"));

            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            strSQL.Append(" select top " + xNumero_Clinicas + " b.IdPessoa as IdClinica, b.NomeAbreviado as Clinica, ");
            strSQL.Append("         max(  case when e.NomeAbreviado is null or e.NomeAbreviado='' then '' else e.NomeAbreviado  end  + ' ' + case when c.Logradouro is null or c.Logradouro='' then '' else c.Logradouro  end + ' ' + c.numero + ' ' + case when c.Complemento is null or c.Complemento='' then '' else c.Complemento  end + ' ' + c.Municipio + ' / ' + c.Uf + '  CEP: ' + c.Cep) as Endereco,  ");
            strSQL.Append("         max(  case when d.ddd is null or d.ddd='' then '' else ' ( ' + d.ddd + ' ) ' end + case when d.numero is null or d.numero='' then '' else d.numero end  +  case when d.nome is null or d.nome='' then '' else ' - ' + d.nome end  + case when d.eMail is null or d.email='' then '' else ' - ' + d.eMail end ) as Contato, HorarioAtendimento   ");
            strSQL.Append(" from ");
            strSQL.Append(" ( ");
            strSQL.Append("  select idpessoa, idendereco, convert(int, substring(cep, 1, 5)) as CEP, abs(convert(int, substring(cep, 1, 5)) - " + xCEP.Substring(0, 5) + " ) as CEP2 ");
            strSQL.Append("  from endereco ");
            strSQL.Append("  where isnumeric(substring(cep, 1, 5)) = 1  ");
            strSQL.Append("  and convert(int, substring(cep, 1, 5)) > 999  ");
            strSQL.Append("  and idpessoa in (  ");
            strSQL.Append("     select idclinica from clinica where idclinica in ( select idclinica from clinicacliente where idcliente = " + xIdPessoa.ToString() + " )  ");
            strSQL.Append("   )  ");
            strSQL.Append(" and idpessoa in (  ");
            strSQL.Append("    select idpessoa from pessoa where isinativo = 0  ");
            strSQL.Append("   )  ");
            strSQL.Append(" )  ");
            strSQL.Append(" as tx90  ");
            strSQL.Append(" left join pessoa as b on(tx90.idpessoa = b.idpessoa)  ");
            strSQL.Append(" left join endereco as c on(tx90.idpessoa = c.idpessoa) ");
            strSQL.Append(" left join ContatoTelefonico as d on(tx90.idpessoa = d.idpessoa) ");
            strSQL.Append(" left join tipologradouro as e on(c.IdTipoLogradouro = e.IdTipoLogradouro) ");
            strSQL.Append(" left join clinica as cl on(tx90.idpessoa = cl.idclinica) ");
            strSQL.Append(" where tx90.idpessoa in ");
            strSQL.Append("     ( ");
            strSQL.Append("       select idclinica from clinicaexamedicionario ");
            strSQL.Append("       where idexamedicionario between 1 and 5 ");
            strSQL.Append("       and valorpadrao <= " + xValor_Maximo + " ");
            strSQL.Append("     ) ");
            strSQL.Append(" and b.NomeAbreviado not like '%atendimento in loco%' ");

            strSQL.Append(" group by b.idpessoa, b.NomeAbreviado, cep2, HorarioAtendimento ");
            strSQL.Append(" order by CEP2  ");




            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                SqlDataAdapter da;
                cnn.Open();


                da = new SqlDataAdapter(strSQL.ToString(), cnn);
                da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }


    }

}