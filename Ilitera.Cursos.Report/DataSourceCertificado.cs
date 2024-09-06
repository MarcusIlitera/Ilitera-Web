using System;
using System.Data;
using System.Collections;
using Ilitera.Opsa.Data;
using Ilitera.Data;
using Ilitera.Common;
using System.Drawing;

namespace Ilitera.Cursos.Report
{
    public class DataSouceCertificado : DataSourceBase
    {
        private Treinamento treinamento;
        private TreinamentoDicionario treinamentoDicionario;
        private bool isColetivo;
        private Endereco endereco;
        private DataSet dsCertificado, dsParticipantes;

        public DataSouceCertificado(Treinamento treinamento, bool isColetivo)
        {
            this.treinamento = treinamento;
            this.treinamento.IdCliente.Find();
            this.treinamento.IdResponsavel.Find();

            this.endereco = treinamento.IdCliente.GetEndereco();

            this.isColetivo = isColetivo;


            PopulaDataSources();
        }

        public DataSouceCertificado(ArrayList alParticipantes)
        {
            this.treinamento = ((ParticipanteTreinamento)alParticipantes[0]).IdTreinamento;
            this.treinamento.Find();

            this.treinamento.IdCliente.Find();
            this.treinamento.IdResponsavel.Find();

            this.endereco = this.treinamento.IdCliente.GetEndereco();
            this.isColetivo = false;

            PopulaDataSources(alParticipantes);
        }

        public DataSouceCertificado(TreinamentoDicionario treinamentoDicionario, bool isColetivo)
        {
            this.treinamentoDicionario = treinamentoDicionario;
            this.treinamentoDicionario.IdCliente.Find();
            this.endereco = this.treinamentoDicionario.IdCliente.GetEndereco();
            this.isColetivo = isColetivo;

            PopulaDataSourcesPreview();
        }

        public RptCertificado GetReport()
        {
            RptCertificado report = new RptCertificado();
            report.OpenSubreport("Participantes").SetDataSource(dsParticipantes);
            report.SetDataSource(dsCertificado);
            report.Refresh();

            SetTempoProcessamento(report);

            return report;
        }

        private void PopulaDataSourcesPreview()
        {
            dsParticipantes = new DataSet();
            DataTable table = new DataTable("Result");
            table.Columns.Add("NomeParticipante", typeof(string));
            dsParticipantes.Tables.Add(table);

            dsCertificado = new DataSet();
            DataRow newRow;
            System.Text.StringBuilder strTexto = new System.Text.StringBuilder();
            dsCertificado.Tables.Add(GetDataTableCertidicado());

            if (isColetivo)
                strTexto.Append(treinamentoDicionario.TextoColetivo);
            else
                strTexto.Append(treinamentoDicionario.TextoIndividual);

            newRow = dsCertificado.Tables[0].NewRow();

            if (treinamentoDicionario.IdCliente.Logotipo.Trim() != "")
            {
                //newRow["LogoEmpresa"] = Fotos.PathSmallFoto_Certificado(treinamentoDicionario.IdCliente.Logotipo);
                newRow["iFoto"] = Ilitera.Common.Fotos.GetByteFoto_Uri(Fotos.PathFoto_Certificado_Uri(treinamentoDicionario.IdCliente.Logotipo));
                newRow["ComFoto"] = true;

            }


            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Mappas") > 0)
            {
                if (isColetivo)
                    newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA MAPPAS - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que a empresa ";
                else
                    newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA MAPPAS - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que ";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Daiti") > 0)
            {
                if (isColetivo)
                    newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA DAIITI - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que a empresa ";
                else
                    newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA DAIITI - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que ";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("SDTOURIN") > 0)
            {
                if (isColetivo)
                    newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA TURIN - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que a empresa ";
                else
                    newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA TURIN - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que ";
            }
            else
            {
                if (isColetivo)
                    newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que a empresa ";
                else
                    newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que ";

            }

            newRow["NomeEmpresa"] = treinamentoDicionario.IdCliente.NomeCompleto;
            newRow["EnderecoEmpresa"] = endereco.GetEndereco();
            newRow["CidadeEmpresa"] = endereco.GetCidadeEstado();
            newRow["NomeTreinador"] = "----------";
            newRow["NumeroTreinador"] = "xxxxx";
            newRow["TituloTreinador"] = "xxxxxxxxxx";
            newRow["Assinatura"] = string.Empty;

            newRow["Titulo_Treinador"] = "Auditor e Responsável Técnico";
            newRow["Titulo_Responsavel"] = "Instrutor";

            newRow["NomeResponsavel"] = "----------";
            newRow["NumeroResponsavel"] = "xxxxx";
            newRow["TituloResponsavel"] = "xxxxxxxxxx";
            newRow["Assinatura_Responsavel"] = string.Empty;

            newRow["Nome_Aluno"] = "";
            newRow["CPF_Aluno"] = "";

            if (isColetivo)
                newRow["NomeParticipante"] = treinamentoDicionario.IdCliente.NomeCompleto;


            //A SIPAT não possue participantes
            if (treinamentoDicionario.IdObrigacao.Id == (int)Obrigacoes.SIPAT)
                newRow["isColetivo"] = false;
            else
                newRow["isColetivo"] = isColetivo;

            newRow["isFromIlitera"] = false;

            Utility.FormatHtmlToCrystalSuport(strTexto);

            newRow["Texto"] = strTexto.ToString();

            dsCertificado.Tables[0].Rows.Add(newRow);
        }

        private void PopulaDataSources()
        {
            this.PopulaDataSources(new ArrayList());
        }

        private void PopulaDataSources(ArrayList alParticipantes)
        {
            if (isColetivo)
            {
                dsCertificado = GetCertificado();
                dsParticipantes = GetParticipantes();
            }
            else
            {
                DataSet dsCertificadoAux = GetCertificado();
                DataSet dsParticipantesAux = new DataSet();

                if (alParticipantes.Count.Equals(0))
                    dsParticipantesAux = GetParticipantes();
                else
                    dsParticipantesAux = GetParticipantes(alParticipantes);

                dsCertificado = dsCertificadoAux.Clone();
                dsParticipantes = dsParticipantesAux.Clone();
                DataRow newRow;

                if (dsParticipantesAux.Tables[0].Rows.Count == 0)
                    throw new Exception("Nenhum participante cadastrado nesse curso!");

                foreach (DataRow rowParticipantes in dsParticipantesAux.Tables[0].Select())
                {
                    newRow = dsCertificado.Tables[0].NewRow();


                    if (treinamento.IdCliente.Logotipo.Trim() != "")
                    {
                        //newRow["LogoEmpresa"] = Fotos.PathSmallFoto_Certificado(treinamentoDicionario.IdCliente.Logotipo);
                        newRow["iFoto"] = Ilitera.Common.Fotos.GetByteFoto_Uri(Fotos.PathFoto_Certificado_Uri(treinamento.IdCliente.Logotipo));
                        newRow["ComFoto"] = true;

                    }


                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Mappas") > 0)
                    {
                        if (isColetivo)
                            newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA MAPPAS - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que a empresa ";
                        else
                            newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA MAPPAS - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que ";
                    }
                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Daiti") > 0)
                    {
                        if (isColetivo)
                            newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA DAIITI - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que a empresa ";
                        else
                            newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA DAIITI - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que ";
                    }
                    else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("SDTOURIN") > 0)
                    {
                        if (isColetivo)
                            newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA TURIN - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que a empresa ";
                        else
                            newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA TURIN - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que ";
                    }
                    else
                    {
                        if (isColetivo)
                            newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que a empresa ";
                        else
                            newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que ";
                    }

                    newRow["LogoEmpresa"] = dsCertificadoAux.Tables[0].Rows[0]["LogoEmpresa"];
                    newRow["NomeEmpresa"] = dsCertificadoAux.Tables[0].Rows[0]["NomeEmpresa"];
                    newRow["EnderecoEmpresa"] = dsCertificadoAux.Tables[0].Rows[0]["EnderecoEmpresa"];
                    newRow["CidadeEmpresa"] = dsCertificadoAux.Tables[0].Rows[0]["CidadeEmpresa"];

                    newRow["NomeTreinador"] = dsCertificadoAux.Tables[0].Rows[0]["NomeTreinador"];
                    newRow["NumeroTreinador"] = dsCertificadoAux.Tables[0].Rows[0]["NumeroTreinador"];
                    newRow["TituloTreinador"] = dsCertificadoAux.Tables[0].Rows[0]["TituloTreinador"];
                    newRow["Titulo_Treinador"] = dsCertificadoAux.Tables[0].Rows[0]["Titulo_Treinador"];
                    newRow["Assinatura"] = dsCertificadoAux.Tables[0].Rows[0]["Assinatura"];

                    newRow["NomeResponsavel"] =  dsCertificadoAux.Tables[0].Rows[0]["NomeResponsavel"];
                    newRow["NumeroResponsavel"] =  dsCertificadoAux.Tables[0].Rows[0]["NumeroResponsavel"];
                    newRow["TituloResponsavel"] =  dsCertificadoAux.Tables[0].Rows[0]["TituloResponsavel"];
                    newRow["Titulo_Responsavel"] = dsCertificadoAux.Tables[0].Rows[0]["Titulo_Responsavel"];
                    newRow["Assinatura_Responsavel"] =  dsCertificadoAux.Tables[0].Rows[0]["Assinatura_Responsavel"];

                    newRow["Nome_Aluno"] = rowParticipantes["Nome_Aluno"].ToString().Trim();
                    newRow["CPF_Aluno"] = rowParticipantes["CPF_Aluno"].ToString().Trim();

                    newRow["isColetivo"] = dsCertificadoAux.Tables[0].Rows[0]["isColetivo"];
                    newRow["isFromIlitera"] = dsCertificadoAux.Tables[0].Rows[0]["isFromIlitera"];                    
                    newRow["Texto"] = dsCertificadoAux.Tables[0].Rows[0]["Texto"];
                    newRow["Conteudo_Programatico"] = dsCertificadoAux.Tables[0].Rows[0]["Conteudo_Programatico"];

                    //newRow["Texto"] = newRow["Texto"].ToString().Replace("NOME_EMPRESA", dsCertificadoAux.Tables[0].Rows[0]["NomeEmpresa"].ToString());

                    //if (isColetivo)
                    //    newRow["NomeParticipante"] = dsCertificadoAux.Tables[0].Rows[0]["NomeEmpresa"].ToString();
                    //else
                        newRow["NomeParticipante"] = rowParticipantes["NomeParticipante"];

                    dsCertificado.Tables[0].Rows.Add(newRow);
                }
            }
        }

        private DataSet GetParticipantes()
        {
            return this.GetParticipantes(new ArrayList());
        }

        private DataSet GetParticipantes(ArrayList alSelectedPartic)
        {
            DataSet ds = new DataSet(), sortedDS = new DataSet();
            DataTable table = new DataTable("Result"), sortedTable = new DataTable("Result");
            DataRow newRow, newSortedRow;

            table.Columns.Add("NomeParticipante", typeof(string));
            table.Columns.Add("CPF_Aluno", typeof(string));
            table.Columns.Add("Nome_Aluno", typeof(string));
            ds.Tables.Add(table);

            sortedTable.Columns.Add("NomeParticipante", typeof(string));
            sortedTable.Columns.Add("CPF_Aluno", typeof(string));
            sortedTable.Columns.Add("Nome_Aluno", typeof(string));
            sortedDS.Tables.Add(sortedTable);

            ArrayList alParticipantes = new ArrayList();

            if (alSelectedPartic.Count.Equals(0))
                alParticipantes = new ParticipanteTreinamento().Find("IdTreinamento=" + treinamento.Id);
            else
                alParticipantes = alSelectedPartic;

            foreach (ParticipanteTreinamento participante in alParticipantes)
            {
                newRow = ds.Tables[0].NewRow();

                //newRow["NomeParticipante"] = participante.GetNomeIdentidadeParticipante();
                string xParticipante = participante.GetNomeCPFParticipante();
                newRow["NomeParticipante"] = xParticipante;

                if (xParticipante.IndexOf("CPF") > 0)
                {

                    newRow["Nome_Aluno"] = xParticipante.Substring(0, xParticipante.IndexOf("CPF") - 2);
                    newRow["CPF_Aluno"] = xParticipante.Substring(xParticipante.IndexOf("CPF"));

                }
                else
                {
                    newRow["Nome_Aluno"] = xParticipante;
                    newRow["CPF_Aluno"] = "";
                }

                ds.Tables[0].Rows.Add(newRow);
            }

            DataRow[] rows = ds.Tables[0].Select("", "NomeParticipante");

            if (this.isColetivo == true)
            {
                newSortedRow = sortedDS.Tables[0].NewRow();
                newSortedRow["NomeParticipante"] = treinamento.IdTreinamentoDicionario.ToString().ToUpper();
                sortedDS.Tables[0].Rows.Add(newSortedRow);
                newSortedRow = sortedDS.Tables[0].NewRow();
                newSortedRow["NomeParticipante"] = treinamento.Periodo.ToUpper();
                sortedDS.Tables[0].Rows.Add(newSortedRow);
                //newSortedRow = sortedDS.Tables[0].NewRow();
                //newSortedRow["NomeParticipante"] = " ";
                //sortedDS.Tables[0].Rows.Add(newSortedRow);
            }

            foreach (DataRow row in rows)
            {
                newSortedRow = sortedDS.Tables[0].NewRow();
                newSortedRow["NomeParticipante"] = row["NomeParticipante"];
                newSortedRow["Nome_Aluno"] = row["Nome_Aluno"];
                newSortedRow["CPF_Aluno"] = row["CPF_Aluno"];
                sortedDS.Tables[0].Rows.Add(newSortedRow);
            }

            return sortedDS;
        }

        private DataSet GetCertificado()
        {
            DataSet ds = new DataSet();
            DataRow newRow;
            System.Text.StringBuilder strTexto = new System.Text.StringBuilder();
            ds.Tables.Add(GetDataTableCertidicado());

            if (treinamento.IdTreinamentoDicionario.mirrorOld == null)
                treinamento.IdTreinamentoDicionario.Find();

            newRow = ds.Tables[0].NewRow();

            if (treinamento.IdCliente.Logotipo.Trim() != "")
            {
                //newRow["LogoEmpresa"] = Fotos.PathSmallFoto_Certificado(treinamento.IdCliente.Logotipo);
                newRow["iFoto"] = Ilitera.Common.Fotos.GetByteFoto_Uri(Fotos.PathFoto_Certificado_Uri(treinamento.IdCliente.Logotipo));
                newRow["ComFoto"] = true;

            }

            if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Mappas") > 0)
            {
                if (isColetivo)
                    newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA MAPPAS - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que a empresa ";
                else
                    newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA MAPPAS - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que ";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Daiti") > 0)
            {
                if (isColetivo)
                    newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA DAIITI - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que a empresa ";
                else
                    newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA DAIITI - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que ";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.ToUpper().IndexOf("SDTOURIN") > 0)
            {
                if (isColetivo)
                    newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA TURIN - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que a empresa ";
                else
                    newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA TURIN - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que ";
            }
            else
            {
                if (isColetivo)
                    newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que a empresa ";
                else
                    newRow["Texto_Ilitera"] = "<p align='right'>A <b>ILITERA - SEGURANÇA, SAÚDE E QUALIDADE DE VIDA LTDA</b> certifica que ";
            }

            newRow["NomeEmpresa"] = treinamento.IdCliente.NomeCompleto + " CNPJ " + treinamento.IdCliente.NomeCodigo;
            newRow["EnderecoEmpresa"] = endereco.GetEndereco();
            newRow["CidadeEmpresa"] = endereco.GetCidadeEstado();


            if (isColetivo)
                newRow["NomeParticipante"] = treinamento.IdCliente.NomeCompleto + " CNPJ " + treinamento.IdCliente.NomeCodigo;

            if (treinamento.IdTreinamentoDicionario.IdObrigacao.Id != (int)Obrigacoes.SIPAT)
            {
                if (treinamento.IdPrestador.NomeCompleto.Trim() == treinamento.IdResponsavel.NomeCompleto.Trim())
                {
                    newRow["NomeResponsavel"] = "";
                    newRow["NumeroResponsavel"] = "";
                    newRow["TituloResponsavel"] = "";
                    newRow["Titulo_Responsavel"] = "";
                    newRow["Assinatura_Responsavel"] = "";
                }
                else
                {
                    newRow["NomeResponsavel"] = treinamento.IdPrestador.NomeCompleto;
                    newRow["NumeroResponsavel"] = treinamento.IdPrestador.Numero;
                    newRow["TituloResponsavel"] = treinamento.IdPrestador.Titulo;
                    newRow["Titulo_Responsavel"] = "Instrutor";
                    newRow["Assinatura_Responsavel"] = Fotos.PathFoto(treinamento.IdPrestador.FotoAss);
                }
            }



            if (treinamento.IdTreinamentoDicionario.IdObrigacao.Id != (int)Obrigacoes.SIPAT)
            {
                newRow["NomeTreinador"] = treinamento.IdResponsavel.NomeCompleto;
                newRow["NumeroTreinador"] = treinamento.IdResponsavel.Numero;
                if (treinamento.IdPrestador.NomeCompleto.Trim() == treinamento.IdResponsavel.NomeCompleto.Trim())
                {
                    newRow["Titulo_Treinador"] = "Instrutor / Auditor e Responsável Técnico";
                }
                else
                {
                    newRow["Titulo_Treinador"] = "Auditor e Responsável Técnico";
                    //newRow["Titulo_Treinador"] = "Instrutor";
                }
                newRow["TituloTreinador"] = treinamento.IdResponsavel.Titulo;
                newRow["Assinatura"] = Fotos.PathFoto(treinamento.IdResponsavel.FotoAss);
            }


            
            //A SIPAT não possue participantes
            if (treinamento.IdTreinamentoDicionario.IdObrigacao.Id == (int)Obrigacoes.SIPAT)
                newRow["isColetivo"] = false;
            else
                newRow["isColetivo"] = isColetivo;

            newRow["isFromIlitera"] = false;//!treinamento.IsFromCliente;

            if (isColetivo)
            {
                if (treinamento.TextoColetivo.Equals(string.Empty))
                    throw new Exception("O Modelo Coletivo não está disponível!");

                strTexto.Append(treinamento.TextoColetivo);
            }
            else
            {
                if (treinamento.IdTreinamentoDicionario.IdObrigacao.Id == (int)Obrigacoes.SIPAT)
                    throw new Exception("O curso de SIPAT não possuir certificado individual!");

                if (treinamento.TextoIndividual.Equals(string.Empty))
                    throw new Exception("O Modelo Individual não está disponível!");

                strTexto.Append(treinamento.TextoIndividual);
            }

            //Troca as variáveis
            strTexto.Replace("NOME_EMPRESA", treinamento.IdCliente.NomeCompleto + " CNPJ " + treinamento.IdCliente.NomeCodigo);
            strTexto.Replace("PERIODO_CURSO", treinamento.Periodo);
            strTexto.Replace("ENDERECO_EMPRESA", endereco.GetEnderecoCompleto());
            strTexto.Replace("CARGA_HORARIA", treinamento.Carga_Horaria.ToString().Trim());

            strTexto.Replace("DATA_EXTENSO", treinamento.DataLevantamento.Day.ToString().Trim() + " de " + Ilitera.Common.Utility.GetMesExtenso( treinamento.DataLevantamento.Month ) + " de " + treinamento.DataLevantamento.Year.ToString().Trim() );
            
            //Corrige BOLD e Italico
            strTexto.Replace("<STRONG>", "<B>");
            strTexto.Replace("</STRONG>", "</B>");
            strTexto.Replace("<EM>", "<I>");
            strTexto.Replace("</EM>", "</I>");

            string xConteudo_Programatico;

            if ( strTexto.ToString().IndexOf("<CONTEUDO_PROG>") < 0 )
            {
                xConteudo_Programatico = "";
                newRow["Texto"] = strTexto.ToString();
                newRow["Conteudo_Programatico"] = "";
            }
            else
            {
                xConteudo_Programatico = strTexto.ToString().Substring(strTexto.ToString().IndexOf("<CONTEUDO_PROG>")+15);                
                newRow["Texto"] = strTexto.ToString().Substring(0, strTexto.ToString().IndexOf("<CONTEUDO_PROG>"));
                xConteudo_Programatico.Replace("<CONTEUDO_PROG>", "");
                newRow["Conteudo_Programatico"] = xConteudo_Programatico;
            }

            

            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }

        private DataTable GetDataTableCertidicado()
        {
            DataTable table = new DataTable("Result");

            table.Columns.Add("LogoEmpresa", typeof(string));
            table.Columns.Add("Texto", typeof(string));
            table.Columns.Add("NomeEmpresa", typeof(string));
            table.Columns.Add("EnderecoEmpresa", typeof(string));
            table.Columns.Add("CidadeEmpresa", typeof(string));
            table.Columns.Add("NomeTreinador", typeof(string));
            table.Columns.Add("NumeroTreinador", typeof(string));
            table.Columns.Add("TituloTreinador", typeof(string));
            table.Columns.Add("Assinatura", typeof(string));
            table.Columns.Add("NomeResponsavel", typeof(string));
            table.Columns.Add("NumeroResponsavel", typeof(string));
            table.Columns.Add("TituloResponsavel", typeof(string));
            table.Columns.Add("Assinatura_Responsavel", typeof(string));
            table.Columns.Add("isColetivo", typeof(bool));
            table.Columns.Add("isFromIlitera", typeof(bool));
            table.Columns.Add("NomeParticipante", typeof(string));
            table.Columns.Add("Titulo_Treinador", typeof(string));
            table.Columns.Add("Titulo_Responsavel", typeof(string));
            table.Columns.Add("Conteudo_Programatico", typeof(string));
            table.Columns.Add("Texto_Ilitera", typeof(string));
            table.Columns.Add("iFoto", typeof(Byte[]));
            table.Columns.Add("ComFoto", typeof(Boolean));
            table.Columns.Add("CPF_Aluno", typeof(string));
            table.Columns.Add("Nome_Aluno", typeof(string));

            return table;
        }
    }
}


