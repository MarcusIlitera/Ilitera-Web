<%@ Page language="c#" Inherits="Ajuda.InstrucoesPreenchimento" CodeFile="InstrucoesPreenchimento.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Ilitera.NET</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="CadastroEPI" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="750" align="center" border="0">
				<TR>
					<TD align="center">
						<P><SPAN lang="PT" style="FONT-SIZE: 10pt; FONT-FAMILY: Arial;"><FONT face="Verdana" color="#003300" size="1"><STRONG><U><BR>
											INSTRUÇÕES DE PREENCHIMENTO</U></STRONG></FONT></SPAN><BR>
											<BR>
						</P>
						<TABLE class="normalFont" id="Table2" borderColor="silver" cellSpacing="0" cellPadding="2"
							width="750" border="1">
							<TR>
								<TD align="center" width="100" style="height: 18px"><SPAN lang="PT" style="FONT-SIZE: 10pt; FONT-FAMILY: Arial;"><STRONG><FONT face="Verdana" size="1">CAMPO</FONT></STRONG></SPAN></TD>
								<TD align="center" width="200" style="height: 18px"><SPAN lang="PT" style="FONT-SIZE: 10pt; FONT-FAMILY: Arial;"><STRONG><FONT face="Verdana" size="1">DESCRIÇÃO</FONT></STRONG></SPAN></TD>
								<TD align="center" width="450" style="height: 18px"><SPAN lang="PT" style="FONT-SIZE: 10pt; FONT-FAMILY: Arial;"><STRONG><FONT face="Verdana" size="1">INSTRUÇÃO 
												DE PREENCHIMENTO</FONT></STRONG></SPAN></TD>
							</TR>
							<TR>
								<TD align="center" width="100"></TD>
								<TD align="center" width="650" colSpan="2"><B><SPAN lang="PT" style="FONT-SIZE: 10pt; FONT-FAMILY: Arial;"><FONT size="1"><FONT face="Verdana">SEÇÃO 
													I - <B><SPAN lang="PT">SEÇÃO DE DADOS ADMINISTRATIVOS</SPAN></B></FONT></FONT></SPAN></B></TD>
							</TR>
							<TR>
								<TD align="center" width="100">1</TD>
								<TD align="center" width="200"><SPAN lang="PT" style="FONT-SIZE: 10pt; FONT-FAMILY: Arial;"><SPAN lang="PT" style="FONT-SIZE: 10pt; FONT-FAMILY: Verdana;"><FONT size="1">CNPJ 
												do Domicílio Tributário/CEI</FONT></SPAN></SPAN></TD>
								<TD align="left" width="450">
									<P class="MsoNormal"><FONT size="1"><SPAN lang="PT">CNPJ relativo ao estabelecimento 
												escolhido como domicílio tributário, nos termos do art. 127 do CTN, no formato 
												XXXXXXXX/XXXX-XX; ou </SPAN><SPAN lang="PT">Matrícula no Cadastro Específico do 
												INSS (Matrícula CEI) relativa à obra realizada por Contribuinte Individual ou 
												ao estabelecimento escolhido como domicílio tributário que não possua CNPJ, no 
												formato XX.XXX.XXXXX/XX, ambos compostos por<SPAN style="COLOR: red"> </SPAN>caracteres 
												numéricos.</SPAN></FONT></P>
								</TD>
							</TR>
							<TR>
								<TD align="center" width="100">2</TD>
								<TD align="center" width="200"><SPAN lang="PT" style="FONT-SIZE: 6pt; FONT-FAMILY: Verdana;"><FONT size="1">Nome 
											Empresarial</FONT></SPAN></TD>
								<TD align="left" width="450"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">Até 
											40 (quarenta) caracteres alfanuméricos.</FONT></SPAN></TD>
							</TR>
							<TR>
								<TD align="center" width="100">3</TD>
								<TD align="center" width="200"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">CNAE</FONT></SPAN></TD>
								<TD align="left" width="450">
									<P class="MsoNormal"><FONT size="1"><SPAN lang="PT">Classificação Nacional de Atividades 
												Econômicas da<SPAN style="COLOR: red"> </SPAN>empresa, completo, com 7 (sete) 
												caracteres numéricos, no formato XXXXXX-X<SPAN>, instituído pelo IBGE </SPAN>através 
												da Resolução CONCLA nº 07, de 16/12/2002. </SPAN><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;">
												A tabela de códigos CNAE-Fiscal pode ser consultada na Internet, no site <SPAN style="COLOR: blue">
													<A href="http://www.cnae.ibge.gov.br/"><SPAN style="TEXT-DECORATION: none;">
															www.cnae.ibge.gov.br</SPAN></A></SPAN>.</SPAN></FONT></P>
								</TD>
							</TR>
							<TR>
								<TD align="center" width="100">4</TD>
								<TD align="center" width="200"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">Nome 
											do Trabalhador</FONT></SPAN></TD>
								<TD align="left" width="450"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">Até 
											40 (quarenta) caracteres alfabéticos.</FONT></SPAN></TD>
							</TR>
							<TR>
								<TD align="center" width="100">5</TD>
								<TD align="center" width="200"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">BR/PDH</FONT></SPAN></TD>
								<TD align="left" width="450">
									<P class="MsoNormal"><SPAN lang="PT">BR – Beneficiário Reabilitado; PDH – Portador de 
											Deficiência Habilitado; NA – Não Aplicável. </SPAN><SPAN lang="PT">Preencher 
											com base no art. 93, da Lei nº 8.213, de 1991, que estabelece a obrigatoriedade 
											do preenchimento dos cargos de empresas com 100 (cem) ou mais empregados com 
											beneficiários reabilitados ou pessoas portadoras de deficiência, habilitadas, 
											na seguinte proporção:
										</SPAN></P>
									<P class="MsoBodyText"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana"><FONT size="1">I 
												- até 200 
												empregados................................................................2%;<BR>
											</FONT></SPAN><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana"><FONT size="1">
												II - de 201 a 
												500.........................................................................3%;<BR>
											</FONT></SPAN><FONT size="1"><SPAN lang="PT">III - de 501 a 
												1.000.....................................................................4%;<BR>
											</SPAN><SPAN lang="PT">IV - de 1.001 em diante. 
												.............................................................5%.</SPAN></FONT></P>
								</TD>
							</TR>
							<TR>
								<TD align="center" width="100">6</TD>
								<TD align="center" width="200"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">NIT</FONT></SPAN></TD>
								<TD align="left" width="450">
									<P class="MsoNormal"><FONT size="1"><SPAN lang="PT">Número de Identificação do Trabalhador 
												com 11 (onze) caracteres numéricos, no formato XXX.XXXXX.XX-X. </SPAN><SPAN lang="PT">
												O NIT corresponde ao número do PIS/PASEP/CI sendo que, no caso de Contribuinte 
												Individual (CI), pode ser utilizado o número de inscrição no Sistema Único de 
												Saúde (SUS) ou na Previdência Social.</SPAN></FONT></P>
								</TD>
							</TR>
							<TR>
								<TD align="center" width="100">7</TD>
								<TD align="center" width="200"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">Data 
											do Nascimento</FONT></SPAN></TD>
								<TD align="left" width="450"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">No 
											formato DD/MM/AAAA.</FONT></SPAN></TD>
							</TR>
							<TR>
								<TD align="center" width="100">8</TD>
								<TD align="center" width="200"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">Sexo 
											(F/M)</FONT></SPAN></TD>
								<TD align="left" width="450"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">F 
											– Feminino; M – Masculino.</FONT></SPAN></TD>
							</TR>
							<TR>
								<TD align="center" width="100">9</TD>
								<TD align="center" width="200"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">CTPS 
											(Nº, Série e UF)</FONT></SPAN></TD>
								<TD align="left" width="450"><SPAN lang="PT" style="FONT-SIZE: 7pt; FONT-FAMILY: Verdana;"><FONT size="1">Número, 
											com 7 (sete) caracteres numéricos, Série, com 5 (cinco) caracteres numéricos e 
											UF, com 2 (dois) caracteres alfabéticos, da Carteira de Trabalho e Previdência 
											Social.</FONT></SPAN></TD>
							</TR>
							<TR>
								<TD align="center" width="100">10</TD>
								<TD align="center" width="200">Data de Admissão</TD>
								<TD align="left" width="450">No formato DD/MM/AAAA.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">11</TD>
								<TD align="center" width="200">Regime de Revezamento</TD>
								<TD align="left" width="450">Regime de Revezamento de trabalho, para trabalhos em 
									turnos ou escala, especificando tempo trabalhado e tempo de descanso, com até 
									15 (quinze) caracteres alfanuméricos. Exemplo: 24 x 72 horas; 14 x 21 dias; 2 x 
									1 meses.</TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 17px" align="center" width="100">12</TD>
								<TD style="HEIGHT: 17px" align="center" width="200">CAT REGISTRADA</TD>
								<TD style="HEIGHT: 17px" align="left" width="450">Informações sobre as Comunicações 
									de Acidente do Trabalho registradas pela empresa na Previdência Social, nos 
									termos do art. 22 da Lei nº 8.213, de 1991, do art. 169 da CLT, do art. 336 do 
									RPS, aprovado pelo Dec. nº 3.048, de 1999, do item 7.4.8, alínea “a” da NR-07 
									do MTE e dos itens 4.3.1 e 6.1.2 do Anexo 13-A da NR-15 do MTE, disciplinado 
									pela Portaria MPAS nº 5.051, de 1999, que aprova o Manual de Instruções para 
									Preenchimento da CAT.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">12.1</TD>
								<TD align="center" width="200">Data do Registro</TD>
								<TD align="left" width="450">No formato DD/MM/AAAA.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">12.2</TD>
								<TD align="center" width="200">Número da CAT</TD>
								<TD align="left" width="450">Com 13 (treze) caracteres numéricos, com formato 
									XXXXXXXXXX-X/XX. Os dois últimos caracteres correspondem a um número seqüencial 
									relativo ao mesmo acidente, identificado por NIT, CNPJ e data do acidente.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">13</TD>
								<TD align="center" width="200">LOTAÇÃO E ATRIBUIÇÃO</TD>
								<TD align="left" width="450">Informações sobre o histórico de lotação e atribuições 
									do trabalhador, por período. A alteração de qualquer um dos campos - 13.2 a 
									13.7 - implica, obrigatoriamente, a criação de nova linha, com discriminação do 
									período, repetindo as informações que não foram alteradas.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">13.1</TD>
								<TD align="center" width="200">Período</TD>
								<TD align="left" width="450">Data de início e data de fim do período, ambas no 
									formato DD/MM/AAAA. No caso de trabalhador ativo, a data de fim do último 
									período não deverá ser preenchida.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">13.2</TD>
								<TD align="center" width="200">CNPJ/CEI</TD>
								<TD align="left" width="450">Local onde efetivamente o trabalhador exerce suas 
									atividades. Deverá ser informado o CNPJ do estabelecimento de lotação do 
									trabalhador ou da empresa tomadora de serviços, no formato XXXXXXXX/XXXX-XX; ou 
									Matrícula CEI da obra ou do estabelecimento que não possua CNPJ, no formato 
									XX.XXX.XXXXX/XX, ambos compostos por caracteres numéricos.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">13.3</TD>
								<TD align="center" width="200">Setor</TD>
								<TD align="left" width="450">Lugar administrativo na estrutura organizacional da 
									empresa, onde o trabalhador exerce suas atividades laborais, com até 15 
									(quinze) caracteres alfanuméricos.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">13.4</TD>
								<TD align="center" width="200">Cargo</TD>
								<TD align="left" width="450">Cargo do trabalhador, constante na CTPS, se empregado 
									ou trabalhador avulso, ou constante no Recibo de Produção e Livro de Matrícula, 
									se cooperado, com até 30 (trinta) caracteres alfanuméricos.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">13.5</TD>
								<TD align="center" width="200">Função</TD>
								<TD align="left" width="450">Lugar administrativo na estrutura organizacional da 
									empresa, onde o trabalhador tenha atribuição de comando, chefia, coordenação, 
									supervisão ou gerência. Quando inexistente a função, preencher com NA – Não 
									Aplicável, com até 30 (trinta) caracteres alfanuméricos.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">13.6</TD>
								<TD align="center" width="200">CBO</TD>
								<TD align="left" width="450">Classificação Brasileira de Ocupação vigente à época, 
									com 6 (seis) caracteres numéricos:<BR>
									1- No caso de utilização da tabela CBO relativa a 1994, utilizar a CBO completa 
									com 5 (cinco) caracteres, completando com “0” (zero) a primeira posição;<BR>
									2- No caso de utilização da tabela CBO relativa a 2002, utilizar a CBO completa 
									com 6 (seis) caracteres.<BR>
									<BR>
									<U>Alternativamente</U>, pode ser utilizada a CBO, com 5 (cinco) caracteres 
									numéricos, conforme Manual da GFIP para usuários do SEFIP, publicado por 
									Instrução Normativa da Diretoria Colegiada do INSS:<BR>
									1- No caso de utilização da tabela CBO relativa a 1994, utilizar a CBO completa 
									com 5 (cinco) caracteres;<BR>
									2- No caso de utilização da tabela CBO relativa a 2002, utilizar a família do 
									CBO com 4 (quatro) caracteres, completando com “0” (zero) a primeira posição.<BR>
									<BR>
									A tabela de CBO pode ser consultada na Internet, no site <A href="http://www.mtecbo.gov.br">
										www.mtecbo.gov.br</A>.<BR>
									<BR>
									OBS: Após a alteração da GFIP, somente será aceita a CBO completa, com 6 (seis) 
									caracteres numéricos, conforme a nova tabela CBO relativa a 2002.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">13.7</TD>
								<TD align="center" width="200">Código Ocorrência da GFIP</TD>
								<TD align="left" width="450">Código Ocorrência da GFIP para o trabalhador, com 2 
									(dois) caracteres numéricos, conforme Manual da GFIP para usuários do SEFIP, 
									publicado por Instrução Normativa da Diretoria Colegiada do INSS.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">14</TD>
								<TD align="center" width="200">PROFISSIOGRAFIA</TD>
								<TD align="left" width="450">Informações sobre a profissiografia do trabalhador, 
									por período. A alteração do campo 14.2 implica, obrigatoriamente, a criação de 
									nova linha, com discriminação do período.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">14.1</TD>
								<TD align="center" width="200">Período</TD>
								<TD align="left" width="450">Data de início e data de fim do período, ambas no 
									formato DD/MM/AAAA. No caso de trabalhador ativo, a data de fim do último 
									período não deverá ser preenchida.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">14.2</TD>
								<TD align="center" width="200">Descrição das Atividades</TD>
								<TD align="left" width="450">Descrição das atividades, físicas ou mentais, 
									realizadas pelo trabalhador, por força do poder de comando a que se submete, 
									com até 400 (quatrocentos) caracteres alfanuméricos. As atividades deverão ser 
									descritas com exatidão, e de forma sucinta, com a utilização de verbos no 
									infinitivo impessoal.</TD>
							</TR>
							<TR>
								<TD align="center" width="100"></TD>
								<TD align="center" width="650" colSpan="2"><STRONG>SEÇÃO II - SEÇÃO DE REGISTROS 
										AMBIENTAIS</STRONG></TD>
							</TR>
							<TR>
								<TD align="center" width="100">15</TD>
								<TD align="center" width="200">EXPOSIÇÃO A FATORES DE RISCOS</TD>
								<TD align="left" width="450">Informações sobre a exposição do trabalhador a fatores 
									de riscos ambientais, por período, ainda que estejam neutralizados, atenuados 
									ou exista proteção eficaz.<BR>
									Facultativamente, também poderão ser indicados os fatores de riscos ergonômicos 
									e mecânicos.<BR>
									A alteração de qualquer um dos campos - 15.2 a 15.8 - implica, 
									obrigatoriamente, a criação de nova linha, com discriminação do período, 
									repetindo as informações que não foram alteradas.<BR>
									OBS.: Após a implantação da migração dos dados do PPP em meio magnético pela 
									Previdência Social, as informações relativas aos fatores de riscos ergonômicos 
									e mecânicos passarão a ser obrigatórias.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">15.1</TD>
								<TD align="center" width="200">Período</TD>
								<TD align="left" width="450">Data de início e data de fim do período, ambas no 
									formato DD/MM/AAAA. No caso de trabalhador ativo, a data de fim do último 
									período não deverá ser preenchida.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">15.2</TD>
								<TD align="center" width="200">Tipo</TD>
								<TD align="left" width="450">F – Físico; Q – Químico; B – Biológico; E – 
									Ergonômico/Psicossocial, M – Mecânico/de Acidente, conforme classificação 
									adotada pelo Ministério da Saúde, em “Doenças Relacionadas ao Trabalho: Manual 
									de Procedimentos para os Serviços de Saúde”, de 2001.<BR>
									A indicação do Tipo “E” e “M” é facultativa.<BR>
									O que determina a associação de agentes é a superposição de períodos com 
									fatores de risco diferentes.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">15.3</TD>
								<TD align="center" width="200">Fator de Risco</TD>
								<TD align="left" width="450">Descrição do fator de risco, com até 40 (quarenta) 
									caracteres alfanuméricos. Em se tratando do Tipo “Q”, deverá ser informado o 
									nome da substância ativa, não sendo aceitas citações de nomes comerciais.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">15.4</TD>
								<TD align="center" width="200">Intensidade / Concentração</TD>
								<TD align="left" width="450">Intensidade ou Concentração, dependendo do tipo de 
									agente, com até 15 (quinze) caracteres alfanuméricos. Caso o fator de risco não 
									seja passível de mensuração, preencher com NA – Não Aplicável.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">15.5</TD>
								<TD align="center" width="200">Técnica Utilizada</TD>
								<TD align="left" width="450">Técnica utilizada para apuração do item 15.4, com até 
									40 (quarenta) caracteres alfanuméricos. Caso o fator de risco não seja passível 
									de mensuração, preencher com NA – Não Aplicável.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">15.6</TD>
								<TD align="center" width="200">EPC Eficaz (S/N)</TD>
								<TD align="left" width="450">S – Sim; N – Não, considerando se houve ou não a 
									eliminação ou a neutralização, com base no informado nos itens 15.2 a 15.5, 
									assegurada as condições de funcionamento do EPC ao longo do tempo, conforme 
									especificação técnica do fabricante e respectivo plano de manutenção.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">15.7</TD>
								<TD align="center" width="200">EPI Eficaz (S/N)</TD>
								<TD align="left" width="450">S – Sim; N – Não, considerando se houve ou não a 
									atenuação, com base no informado nos itens 15.2 a 15.5, observado o disposto na 
									NR-06 do MTE, assegurada a observância:<BR>
									1- da hierarquia estabelecida no item 9.3.5.4 da NR-09 do MTE (medidas de 
									proteção coletiva, medidas de caráter administrativo ou de organização do 
									trabalho e utilização de EPI, nesta ordem, admitindo-se a utilização de EPI 
									somente em situações de inviabilidade técnica, insuficiência ou interinidade à 
									implementação do EPC, ou ainda em caráter complementar ou emergencial);<BR>
									2- das condições de funcionamento do EPI ao longo do tempo, conforme 
									especificação técnica do fabricante ajustada às condições de campo;<BR>
									3- do prazo de validade, conforme Certificado de Aprovação do MTE;<BR>
									4- da periodicidade de troca definida pelos programas ambientais, devendo esta 
									ser comprovada mediante recibo; e<BR>
									5- dos meios de higienização.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">15.8</TD>
								<TD align="center" width="200">C.A. EPI</TD>
								<TD align="left" width="450">Número do Certificado de Aprovação do MTE para o 
									Equipamento de Proteção Individual referido no campo 154.7, com 5 (cinco) 
									caracteres numéricos. Caso não seja utilizado EPI, preencher com NA – Não 
									Aplicável.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">16</TD>
								<TD align="center" width="200">RESPONSÁVEL PELOS REGISTROS AMBIENTAIS</TD>
								<TD align="left" width="450">Informações sobre os responsáveis pelos registros 
									ambientais, por período.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">16.1</TD>
								<TD align="center" width="200">Período</TD>
								<TD align="left" width="450">Data de início e data de fim do período, ambas no 
									formato DD/MM/AAAA. No caso de trabalhador ativo sem alteração do responsável, 
									a data de fim do último período não deverá ser preenchida.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">16.2</TD>
								<TD align="center" width="200">NIT</TD>
								<TD align="left" width="450">Número de Identificação do Trabalhador com 11 (onze) 
									caracteres numéricos, no formato XXX.XXXXX.XX-X.<BR>
									O NIT corresponde ao número do PIS/PASEP/CI sendo que, no caso de Contribuinte 
									Individual (CI), pode ser utilizado o número de inscrição no Sistema Único de 
									Saúde (SUS) ou na Previdência Social.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">16.3</TD>
								<TD align="center" width="200">Registro Conselho de Classe</TD>
								<TD align="left" width="450">Número do registro profissional no Conselho de Classe, 
									com 9 (nove) caracteres alfanuméricos, no formato XXXXXX-X/XX ou XXXXXXX/XX.<BR>
									A parte “-X” corresponde à D – Definitivo ou P – Provisório.<BR>
									A parte “/XX” deve ser preenchida com a UF, com 2 (dois) caracteres 
									alfabéticos.<BR>
									A parte numérica deverá ser completada com zeros à esquerda.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">16.4</TD>
								<TD align="center" width="200">Nome do Profissional Legalmente Habilitado</TD>
								<TD align="left" width="450">Até 40 (quarenta) caracteres alfabéticos.</TD>
							</TR>
							<TR>
								<TD align="center" width="100"></TD>
								<TD align="center" width="650" colSpan="2"><STRONG>SEÇÃO III -&nbsp;SEÇÃO DE RESULTADOS 
										DE MONITORAÇÃO BIOLÓGICA</STRONG></TD>
							</TR>
							<TR>
								<TD align="center" width="100">17</TD>
								<TD align="center" width="200">EXAMES MÉDICOS CLÍNICOS E COMPLEMENTARES</TD>
								<TD align="left" width="450">Informações sobre os exames médicos obrigatórios, 
									clínicos e complementares, realizados para o trabalhador, constantes nos 
									Quadros I e II, da NR-07 do MTE.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">17.1</TD>
								<TD align="center" width="200">Data</TD>
								<TD align="left" width="450">No formato DD/MM/AAAA.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">17.2</TD>
								<TD align="center" width="200">Tipo</TD>
								<TD align="left" width="450">A – Admissional; P – Periódico; R – Retorno ao 
									Trabalho; M – Mudança de Função; D – Demissional.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">17.3</TD>
								<TD align="center" width="200">Natureza</TD>
								<TD align="left" width="450">Natureza do exame realizado, com até 50 (cinqüenta) 
									caracteres alfanuméricos.<BR>
									No caso dos exames relacionados no Quadro I da NR-07, do MTE, deverá ser 
									especificada a análise realizada, além do material biológico coletado.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">17.4</TD>
								<TD align="center" width="200">Exame (R/S)</TD>
								<TD align="left" width="450">R – Referencial; S – Seqüencial.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">17.5</TD>
								<TD align="center" width="200">Indicação de Resultados</TD>
								<TD align="left" width="450">Preencher Normal ou Alterado. Só deve ser preenchido 
									Estável ou Agravamento no caso de Alterado em exame Seqüencial. Só deve ser 
									preenchido Ocupacional ou Não Ocupacional no caso de Agravamento.<BR>
									OBS: No caso de Natureza do Exame “Audiometria”, a alteração unilateral poderá 
									ser classificada como ocupacional, apesar de a maioria das alterações 
									ocupacionais serem constatadas bilateralmente.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">18</TD>
								<TD align="center" width="200">RESPONSÁVEL PELA MONITORAÇÃO BIOLÓGICA</TD>
								<TD align="left" width="450">Informações sobre os responsáveis pela monitoração 
									biológica, por período.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">18.1</TD>
								<TD align="center" width="200">Período</TD>
								<TD align="left" width="450">Data de início e data de fim do período, ambas no 
									formato DD/MM/AAAA. No caso de trabalhador ativo sem alteração do responsável, 
									a data de fim do último período não deverá ser preenchida.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">18.2</TD>
								<TD align="center" width="200">NIT</TD>
								<TD align="left" width="450">Número de Identificação do Trabalhador com 11 (onze) 
									caracteres numéricos, no formato XXX.XXXXX.XX-X.<BR>
									O NIT corresponde ao número do PIS/PASEP/CI sendo que, no caso de Contribuinte 
									Individual (CI), pode ser utilizado o número de inscrição no Sistema Único de 
									Saúde (SUS) ou na Previdência Social.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">18.3</TD>
								<TD align="center" width="200">Registro Conselho de Classe</TD>
								<TD align="left" width="450">Número do registro profissional no Conselho de Classe, 
									com 9 (nove) caracteres alfanuméricos, no formato XXXXXX-X/XX ou XXXXXXX/XX.<BR>
									A parte “-X” corresponde à D – Definitivo ou P – Provisório.<BR>
									A parte “/XX” deve ser preenchida com a UF, com 2 (dois) caracteres 
									alfabéticos.<BR>
									A parte numérica deverá ser completada com zeros à esquerda.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">18.4</TD>
								<TD align="center" width="200">Nome do Profissional Legalmente Habilitado</TD>
								<TD align="left" width="450">Até 40 (quarenta) caracteres alfabéticos.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">19</TD>
								<TD align="center" width="200">Data de Emissão do PPP</TD>
								<TD align="left" width="450">Data em que o PPP é impresso e assinado pelos 
									responsáveis, no formato DD/MM/AAAA.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">20</TD>
								<TD align="center" width="200">REPRESENTANTE LEGAL DA EMPRESA</TD>
								<TD align="left" width="450">Informações sobre o Representante Legal da empresa, 
									com poderes específicos outorgados por procuração.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">20.1</TD>
								<TD align="center" width="200">NIT</TD>
								<TD align="left" width="450">Número de Identificação do Trabalhador com 11 (onze) 
									caracteres numéricos, no formato XXX.XXXXX.XX-X.<BR>
									O NIT corresponde ao número do PIS/PASEP/CI sendo que, no caso de contribuinte 
									individual (CI), pode ser utilizado o número de inscrição no Sistema Único de 
									Saúde (SUS) ou na Previdência Social.</TD>
							</TR>
							<TR>
								<TD align="center" width="100">20.2</TD>
								<TD align="center" width="200">Nome</TD>
								<TD align="left" width="450">Até 40 caracteres alfabéticos.</TD>
							</TR>
							<TR>
								<TD align="center" width="100"></TD>
								<TD align="center" width="200">Carimbo e Assinatura</TD>
								<TD align="left" width="450">Carimbo da Empresa e Assinatura do Representante 
									Legal.</TD>
							</TR>
							<TR>
								<TD align="center" width="100"></TD>
								<TD align="center" width="650" colSpan="2"><STRONG>OBSERVAÇÕES</STRONG></TD>
							</TR>
							<TR>
								<TD align="center" width="100"></TD>
								<TD align="center" width="650" colSpan="2">Devem ser incluídas neste campo, 
									informações necessárias à análise do PPP, bem como facilitadoras do 
									requerimento do benefício, como por exemplo, esclarecimento sobre alteração de 
									razão social da empresa, no caso de sucessora ou indicador de empresa 
									pertencente a grupo econômico.<BR>
									<BR>
									OBS: É facultada a inclusão de informações complementares ou adicionais ao PPP.
								</TD>
							</TR>
						</TABLE><br>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
